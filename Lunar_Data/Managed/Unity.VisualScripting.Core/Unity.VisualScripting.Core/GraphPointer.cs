using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.VisualScripting
{
	// Token: 0x02000062 RID: 98
	public abstract class GraphPointer
	{
		// Token: 0x06000305 RID: 773 RVA: 0x00007A08 File Offset: 0x00005C08
		protected static bool IsValidRoot(IGraphRoot root)
		{
			return ((root != null) ? root.childGraph : null) != null && root as Object != null;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00007A26 File Offset: 0x00005C26
		protected static bool IsValidRoot(Object rootObject)
		{
			if (rootObject != null)
			{
				IGraphRoot graphRoot = rootObject as IGraphRoot;
				return ((graphRoot != null) ? graphRoot.childGraph : null) != null;
			}
			return false;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00007A48 File Offset: 0x00005C48
		internal GraphPointer()
		{
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00007A88 File Offset: 0x00005C88
		protected void Initialize(IGraphRoot root)
		{
			if (!GraphPointer.IsValidRoot(root))
			{
				throw new ArgumentException("Graph pointer root must be a valid Unity object with a non-null child graph.", "root");
			}
			if ((!(root is IMachine) || !(root is MonoBehaviour)) && (!(root is IMacro) || !(root is ScriptableObject)))
			{
				throw new ArgumentException("Graph pointer root must be either a machine or a macro.", "root");
			}
			this.root = root;
			this.parentStack.Add(root);
			this.graphStack.Add(root.childGraph);
			List<IGraphData> list = this.dataStack;
			IMachine machine = this.machine;
			list.Add((machine != null) ? machine.graphData : null);
			List<IGraphDebugData> list2 = this.debugDataStack;
			Func<IGraphRoot, IGraphDebugData> fetchRootDebugDataBinding = GraphPointer.fetchRootDebugDataBinding;
			list2.Add((fetchRootDebugDataBinding != null) ? fetchRootDebugDataBinding(root) : null);
			if (this.machine == null)
			{
				this.gameObject = null;
				return;
			}
			if (this.machine.threadSafeGameObject != null)
			{
				this.gameObject = this.machine.threadSafeGameObject;
				return;
			}
			if (UnityThread.allowsAPI)
			{
				this.gameObject = this.component.gameObject;
				return;
			}
			throw new GraphPointerException("Could not fetch graph pointer root game object.", this);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00007B94 File Offset: 0x00005D94
		protected void Initialize(IGraphRoot root, IEnumerable<IGraphParentElement> parentElements, bool ensureValid)
		{
			this.Initialize(root);
			Ensure.That("parentElements").IsNotNull<IEnumerable<IGraphParentElement>>(parentElements);
			foreach (IGraphParentElement graphParentElement in parentElements)
			{
				string text;
				if (!this.TryEnterParentElement(graphParentElement, out text, null, false))
				{
					if (ensureValid)
					{
						throw new GraphPointerException(text, this);
					}
					break;
				}
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00007C0C File Offset: 0x00005E0C
		protected void Initialize(Object rootObject, IEnumerable<Guid> parentElementGuids, bool ensureValid)
		{
			this.Initialize(rootObject as IGraphRoot);
			Ensure.That("parentElementGuids").IsNotNull<IEnumerable<Guid>>(parentElementGuids);
			foreach (Guid guid in parentElementGuids)
			{
				string text;
				if (!this.TryEnterParentElement(guid, out text, null))
				{
					if (ensureValid)
					{
						throw new GraphPointerException(text, this);
					}
					break;
				}
			}
		}

		// Token: 0x0600030B RID: 779
		public abstract GraphReference AsReference();

		// Token: 0x0600030C RID: 780 RVA: 0x00007C88 File Offset: 0x00005E88
		public virtual void CopyFrom(GraphPointer other)
		{
			this.root = other.root;
			this.gameObject = other.gameObject;
			this.parentStack.Clear();
			this.parentElementStack.Clear();
			this.graphStack.Clear();
			this.dataStack.Clear();
			this.debugDataStack.Clear();
			foreach (IGraphParent graphParent in other.parentStack)
			{
				this.parentStack.Add(graphParent);
			}
			foreach (IGraphParentElement graphParentElement in other.parentElementStack)
			{
				this.parentElementStack.Add(graphParentElement);
			}
			foreach (IGraph graph in other.graphStack)
			{
				this.graphStack.Add(graph);
			}
			foreach (IGraphData graphData in other.dataStack)
			{
				this.dataStack.Add(graphData);
			}
			foreach (IGraphDebugData graphDebugData in other.debugDataStack)
			{
				this.debugDataStack.Add(graphDebugData);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00007E54 File Offset: 0x00006054
		// (set) Token: 0x0600030E RID: 782 RVA: 0x00007E5C File Offset: 0x0000605C
		public IGraphRoot root { get; protected set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00007E65 File Offset: 0x00006065
		public Object rootObject
		{
			get
			{
				return this.root as Object;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00007E72 File Offset: 0x00006072
		public IMachine machine
		{
			get
			{
				return this.root as IMachine;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00007E7F File Offset: 0x0000607F
		public IMacro macro
		{
			get
			{
				return this.root as IMacro;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00007E8C File Offset: 0x0000608C
		public MonoBehaviour component
		{
			get
			{
				return this.root as MonoBehaviour;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00007E99 File Offset: 0x00006099
		// (set) Token: 0x06000314 RID: 788 RVA: 0x00007EA1 File Offset: 0x000060A1
		public GameObject gameObject { get; private set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00007EAA File Offset: 0x000060AA
		public GameObject self
		{
			get
			{
				return this.gameObject;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00007EB2 File Offset: 0x000060B2
		public ScriptableObject scriptableObject
		{
			get
			{
				return this.root as ScriptableObject;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00007EC0 File Offset: 0x000060C0
		public Scene? scene
		{
			get
			{
				if (this.gameObject == null)
				{
					return null;
				}
				Scene scene = this.gameObject.scene;
				if (!scene.IsValid())
				{
					return null;
				}
				return new Scene?(scene);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00007F0C File Offset: 0x0000610C
		public Object serializedObject
		{
			get
			{
				for (int i = this.depth; i > 0; i--)
				{
					IGraphParent graphParent = this.parentStack[i - 1];
					if (graphParent.isSerializationRoot)
					{
						return graphParent.serializedObject;
					}
				}
				throw new GraphPointerException("Could not find serialized object.", this);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00007F53 File Offset: 0x00006153
		public IEnumerable<Guid> parentElementGuids
		{
			get
			{
				return this.parentElementStack.Select((IGraphParentElement parentElement) => parentElement.guid);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00007F7F File Offset: 0x0000617F
		public int depth
		{
			get
			{
				return this.parentStack.Count;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00007F8C File Offset: 0x0000618C
		public bool isRoot
		{
			get
			{
				return this.depth == 1;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00007F97 File Offset: 0x00006197
		public bool isChild
		{
			get
			{
				return this.depth > 1;
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00007FA2 File Offset: 0x000061A2
		public void EnsureDepthValid(int depth)
		{
			Ensure.That("depth").IsGte<int>(depth, 1);
			if (depth > this.depth)
			{
				throw new GraphPointerException(string.Format("Trying to fetch a graph pointer level above depth: {0} > {1}", depth, this.depth), this);
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00007FE0 File Offset: 0x000061E0
		public void EnsureChild()
		{
			if (!this.isChild)
			{
				throw new GraphPointerException("Graph pointer does not point to a child graph.", this);
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00007FF6 File Offset: 0x000061F6
		public bool IsWithin<T>() where T : IGraphParent
		{
			return this.parent is T;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00008006 File Offset: 0x00006206
		public void EnsureWithin<T>() where T : IGraphParent
		{
			if (!this.IsWithin<T>())
			{
				throw new GraphPointerException(string.Format("Graph pointer must be within a {0} for this operation.", typeof(T)), this);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000802B File Offset: 0x0000622B
		public IGraphParent parent
		{
			get
			{
				return this.parentStack[this.parentStack.Count - 1];
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00008045 File Offset: 0x00006245
		public T GetParent<T>() where T : IGraphParent
		{
			this.EnsureWithin<T>();
			return (T)((object)this.parent);
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00008058 File Offset: 0x00006258
		public IGraphParentElement parentElement
		{
			get
			{
				this.EnsureChild();
				return this.parentElementStack[this.parentElementStack.Count - 1];
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00008078 File Offset: 0x00006278
		public IGraph rootGraph
		{
			get
			{
				return this.graphStack[0];
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00008086 File Offset: 0x00006286
		public IGraph graph
		{
			get
			{
				return this.graphStack[this.graphStack.Count - 1];
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000326 RID: 806 RVA: 0x000080A0 File Offset: 0x000062A0
		// (set) Token: 0x06000327 RID: 807 RVA: 0x000080BA File Offset: 0x000062BA
		protected IGraphData _data
		{
			get
			{
				return this.dataStack[this.dataStack.Count - 1];
			}
			set
			{
				this.dataStack[this.dataStack.Count - 1] = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000328 RID: 808 RVA: 0x000080D5 File Offset: 0x000062D5
		public IGraphData data
		{
			get
			{
				this.EnsureDataAvailable();
				return this._data;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000329 RID: 809 RVA: 0x000080E3 File Offset: 0x000062E3
		protected IGraphData _parentData
		{
			get
			{
				return this.dataStack[this.dataStack.Count - 2];
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600032A RID: 810 RVA: 0x000080FD File Offset: 0x000062FD
		public bool hasData
		{
			get
			{
				return this._data != null;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00008108 File Offset: 0x00006308
		public void EnsureDataAvailable()
		{
			if (!this.hasData)
			{
				throw new GraphPointerException("Graph data is not available.", this);
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00008120 File Offset: 0x00006320
		public T GetGraphData<T>() where T : IGraphData
		{
			IGraphData data = this.data;
			if (data is T)
			{
				return (T)((object)data);
			}
			throw new GraphPointerException(string.Format("Graph data type mismatch. Found {0}, expected {1}.", data.GetType(), typeof(T)), this);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00008164 File Offset: 0x00006364
		public T GetElementData<T>(IGraphElementWithData element) where T : IGraphElementData
		{
			IGraphElementData graphElementData;
			if (!this._data.TryGetElementData(element, out graphElementData))
			{
				throw new GraphPointerException(string.Format("Missing graph element data for {0}.", element), this);
			}
			if (graphElementData is T)
			{
				return (T)((object)graphElementData);
			}
			throw new GraphPointerException(string.Format("Graph element data type mismatch. Found {0}, expected {1}.", graphElementData.GetType(), typeof(T)), this);
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600032E RID: 814 RVA: 0x000081C2 File Offset: 0x000063C2
		// (set) Token: 0x0600032F RID: 815 RVA: 0x000081C9 File Offset: 0x000063C9
		public static Func<IGraphRoot, IGraphDebugData> fetchRootDebugDataBinding { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000330 RID: 816 RVA: 0x000081D1 File Offset: 0x000063D1
		public bool hasDebugData
		{
			get
			{
				return this._debugData != null;
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000081DC File Offset: 0x000063DC
		public void EnsureDebugDataAvailable()
		{
			if (!this.hasDebugData)
			{
				throw new GraphPointerException("Graph debug data is not available.", this);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000332 RID: 818 RVA: 0x000081F2 File Offset: 0x000063F2
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000820C File Offset: 0x0000640C
		protected IGraphDebugData _debugData
		{
			get
			{
				return this.debugDataStack[this.debugDataStack.Count - 1];
			}
			set
			{
				this.debugDataStack[this.debugDataStack.Count - 1] = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00008227 File Offset: 0x00006427
		public IGraphDebugData debugData
		{
			get
			{
				this.EnsureDebugDataAvailable();
				return this._debugData;
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00008238 File Offset: 0x00006438
		public T GetGraphDebugData<T>() where T : IGraphDebugData
		{
			IGraphDebugData debugData = this.debugData;
			if (debugData is T)
			{
				return (T)((object)debugData);
			}
			throw new GraphPointerException(string.Format("Graph debug data type mismatch. Found {0}, expected {1}.", debugData.GetType(), typeof(T)), this);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000827C File Offset: 0x0000647C
		public T GetElementDebugData<T>(IGraphElementWithDebugData element)
		{
			IGraphElementDebugData orCreateElementData = this.debugData.GetOrCreateElementData(element);
			if (orCreateElementData is T)
			{
				return (T)((object)orCreateElementData);
			}
			throw new GraphPointerException(string.Format("Graph element runtime debug data type mismatch. Found {0}, expected {1}.", orCreateElementData.GetType(), typeof(T)), this);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000082C8 File Offset: 0x000064C8
		protected bool TryEnterParentElement(Guid parentElementGuid, out string error, int? maxRecursionDepth = null)
		{
			IGraphElement graphElement;
			if (!this.graph.elements.TryGetValue(parentElementGuid, out graphElement))
			{
				error = "Trying to enter a graph parent element with a GUID that is not within the current graph.";
				return false;
			}
			if (!(graphElement is IGraphParentElement))
			{
				error = "Provided element GUID does not point to a graph parent element.";
				return false;
			}
			IGraphParentElement graphParentElement = (IGraphParentElement)graphElement;
			return this.TryEnterParentElement(graphParentElement, out error, maxRecursionDepth, false);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00008318 File Offset: 0x00006518
		protected bool TryEnterParentElement(IGraphParentElement parentElement, out string error, int? maxRecursionDepth = null, bool skipContainsCheck = false)
		{
			if (!skipContainsCheck && !this.graph.elements.Contains(parentElement))
			{
				error = "Trying to enter a graph parent element that is not within the current graph.";
				return false;
			}
			IGraph childGraph = parentElement.childGraph;
			if (childGraph == null)
			{
				error = "Trying to enter a graph parent element without a child graph.";
				return false;
			}
			if (Recursion.safeMode)
			{
				int num = 0;
				int num2 = maxRecursionDepth ?? Recursion.defaultMaxDepth;
				using (List<IGraph>.Enumerator enumerator = this.graphStack.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == childGraph)
						{
							num++;
						}
					}
				}
				if (num > num2)
				{
					error = string.Format("Max recursion depth of {0} has been exceeded. Are you nesting a graph within itself?\nIf not, consider increasing '{1}.{2}'.", num2, "Recursion", "defaultMaxDepth");
					return false;
				}
			}
			this.EnterValidParentElement(parentElement);
			error = null;
			return true;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x000083F0 File Offset: 0x000065F0
		protected void EnterParentElement(IGraphParentElement parentElement)
		{
			string text;
			if (!this.TryEnterParentElement(parentElement, out text, null, false))
			{
				throw new GraphPointerException(text, this);
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000841C File Offset: 0x0000661C
		protected void EnterParentElement(Guid parentElementGuid)
		{
			string text;
			if (!this.TryEnterParentElement(parentElementGuid, out text, null))
			{
				throw new GraphPointerException(text, this);
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00008448 File Offset: 0x00006648
		private void EnterValidParentElement(IGraphParentElement parentElement)
		{
			IGraph childGraph = parentElement.childGraph;
			this.parentStack.Add(parentElement);
			this.parentElementStack.Add(parentElement);
			this.graphStack.Add(childGraph);
			IGraphData graphData = null;
			IGraphData data = this._data;
			if (data != null)
			{
				data.TryGetChildGraphData(parentElement, out graphData);
			}
			this.dataStack.Add(graphData);
			IGraphDebugData debugData = this._debugData;
			IGraphDebugData graphDebugData = ((debugData != null) ? debugData.GetOrCreateChildGraphData(parentElement) : null);
			this.debugDataStack.Add(graphDebugData);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000084C4 File Offset: 0x000066C4
		protected void ExitParentElement()
		{
			if (!this.isChild)
			{
				throw new GraphPointerException("Trying to exit the root graph.", this);
			}
			this.parentStack.RemoveAt(this.parentStack.Count - 1);
			this.parentElementStack.RemoveAt(this.parentElementStack.Count - 1);
			this.graphStack.RemoveAt(this.graphStack.Count - 1);
			this.dataStack.RemoveAt(this.dataStack.Count - 1);
			this.debugDataStack.RemoveAt(this.debugDataStack.Count - 1);
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00008560 File Offset: 0x00006760
		public bool isValid
		{
			get
			{
				bool flag;
				try
				{
					if (this.rootObject == null)
					{
						flag = false;
					}
					else if (this.rootGraph != this.root.childGraph)
					{
						flag = false;
					}
					else if (this.serializedObject == null)
					{
						flag = false;
					}
					else
					{
						for (int i = 1; i < this.depth; i++)
						{
							IGraphParentElement graphParentElement = this.parentElementStack[i - 1];
							IGraph graph = this.graphStack[i - 1];
							IGraph graph2 = this.graphStack[i];
							if (!graph.elements.Contains(graphParentElement))
							{
								return false;
							}
							if (graphParentElement.childGraph != graph2)
							{
								return false;
							}
						}
						flag = true;
					}
				}
				catch (Exception ex)
				{
					string text = "Failed to check graph pointer validity: \n";
					Exception ex2 = ex;
					Debug.LogWarning(text + ((ex2 != null) ? ex2.ToString() : null));
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00008644 File Offset: 0x00006844
		public void EnsureValid()
		{
			if (!this.isValid)
			{
				throw new GraphPointerException("Graph pointer is invalid.", this);
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000865C File Offset: 0x0000685C
		public bool InstanceEquals(GraphPointer other)
		{
			if (this == other)
			{
				return true;
			}
			if (!UnityObjectUtility.TrulyEqual(this.rootObject, other.rootObject))
			{
				return false;
			}
			if (!this.DefinitionEquals(other))
			{
				return false;
			}
			int depth = this.depth;
			for (int i = 0; i < depth; i++)
			{
				IGraphData graphData = this.dataStack[i];
				IGraphData graphData2 = other.dataStack[i];
				if (graphData != graphData2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000086C4 File Offset: 0x000068C4
		public bool DefinitionEquals(GraphPointer other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.rootGraph != other.rootGraph)
			{
				return false;
			}
			int depth = this.depth;
			if (depth != other.depth)
			{
				return false;
			}
			for (int i = 1; i < depth; i++)
			{
				IGraphParentElement graphParentElement = this.parentElementStack[i - 1];
				IGraphParentElement graphParentElement2 = other.parentElementStack[i - 1];
				if (graphParentElement != graphParentElement2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00008728 File Offset: 0x00006928
		public int ComputeHashCode()
		{
			int num = 17;
			int num2 = num * 23;
			Object @object = this.rootObject.AsUnityNull<Object>();
			num = num2 + ((@object != null) ? @object.GetHashCode() : 0);
			int num3 = num * 23;
			IGraph rootGraph = this.rootGraph;
			num = num3 + ((rootGraph != null) ? rootGraph.GetHashCode() : 0);
			int depth = this.depth;
			for (int i = 1; i < depth; i++)
			{
				Guid guid = this.parentElementStack[i - 1].guid;
				num = num * 23 + guid.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000087A8 File Offset: 0x000069A8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ ");
			stringBuilder.Append(this.rootObject.ToSafeString());
			for (int i = 1; i < this.depth; i++)
			{
				stringBuilder.Append(" > ");
				int num = i - 1;
				if (num >= this.parentElementStack.Count)
				{
					stringBuilder.Append("?");
					break;
				}
				IGraphParentElement graphParentElement = this.parentElementStack[num];
				stringBuilder.Append(graphParentElement);
			}
			stringBuilder.Append(" ]");
			return stringBuilder.ToString();
		}

		// Token: 0x040000D9 RID: 217
		protected readonly List<IGraphParent> parentStack = new List<IGraphParent>();

		// Token: 0x040000DA RID: 218
		protected readonly List<IGraphParentElement> parentElementStack = new List<IGraphParentElement>();

		// Token: 0x040000DB RID: 219
		protected readonly List<IGraph> graphStack = new List<IGraph>();

		// Token: 0x040000DC RID: 220
		protected readonly List<IGraphData> dataStack = new List<IGraphData>();

		// Token: 0x040000DD RID: 221
		protected readonly List<IGraphDebugData> debugDataStack = new List<IGraphDebugData>();
	}
}
