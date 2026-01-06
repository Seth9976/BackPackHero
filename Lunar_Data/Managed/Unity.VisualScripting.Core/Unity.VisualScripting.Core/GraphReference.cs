using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000064 RID: 100
	public sealed class GraphReference : GraphPointer
	{
		// Token: 0x06000345 RID: 837 RVA: 0x0000886C File Offset: 0x00006A6C
		static GraphReference()
		{
			ReferenceCollector.onSceneUnloaded += GraphReference.FreeInvalidInterns;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00008889 File Offset: 0x00006A89
		private GraphReference()
		{
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00008891 File Offset: 0x00006A91
		public static GraphReference New(IGraphRoot root, bool ensureValid)
		{
			if (!ensureValid && !GraphPointer.IsValidRoot(root))
			{
				return null;
			}
			GraphReference graphReference = new GraphReference();
			graphReference.Initialize(root);
			graphReference.Hash();
			return graphReference;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000088B2 File Offset: 0x00006AB2
		public static GraphReference New(IGraphRoot root, IEnumerable<IGraphParentElement> parentElements, bool ensureValid)
		{
			if (!ensureValid && !GraphPointer.IsValidRoot(root))
			{
				return null;
			}
			GraphReference graphReference = new GraphReference();
			graphReference.Initialize(root, parentElements, ensureValid);
			graphReference.Hash();
			return graphReference;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000088D5 File Offset: 0x00006AD5
		public static GraphReference New(Object rootObject, IEnumerable<Guid> parentElementGuids, bool ensureValid)
		{
			if (!ensureValid && !GraphPointer.IsValidRoot(rootObject))
			{
				return null;
			}
			GraphReference graphReference = new GraphReference();
			graphReference.Initialize(rootObject, parentElementGuids, ensureValid);
			graphReference.Hash();
			return graphReference;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000088F8 File Offset: 0x00006AF8
		private static GraphReference New(GraphPointer model)
		{
			GraphReference graphReference = new GraphReference();
			graphReference.CopyFrom(model);
			return graphReference;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00008908 File Offset: 0x00006B08
		public override void CopyFrom(GraphPointer other)
		{
			base.CopyFrom(other);
			GraphReference graphReference = other as GraphReference;
			if (graphReference != null)
			{
				this.hashCode = graphReference.hashCode;
				return;
			}
			this.Hash();
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00008939 File Offset: 0x00006B39
		public GraphReference Clone()
		{
			return GraphReference.New(this);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00008941 File Offset: 0x00006B41
		public override GraphReference AsReference()
		{
			return this;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00008944 File Offset: 0x00006B44
		public GraphStack ToStackPooled()
		{
			return GraphStack.New(this);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000894C File Offset: 0x00006B4C
		public void CreateGraphData()
		{
			if (base._data != null)
			{
				throw new GraphPointerException("Graph data already exists.", this);
			}
			if (base.isRoot)
			{
				if (base.machine != null)
				{
					base._data = (base.machine.graphData = base.graph.CreateData());
					return;
				}
				throw new GraphPointerException("Root graph data can only be created on machines.", this);
			}
			else
			{
				if (base._parentData == null)
				{
					throw new GraphPointerException("Child graph data can only be created from parent graph data.", this);
				}
				base._data = base._parentData.CreateChildGraphData(base.parentElement);
				return;
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000089D4 File Offset: 0x00006BD4
		public void FreeGraphData()
		{
			if (base._data == null)
			{
				throw new GraphPointerException("Graph data does not exist.", this);
			}
			if (base.isRoot)
			{
				if (base.machine != null)
				{
					base._data = (base.machine.graphData = null);
					return;
				}
				throw new GraphPointerException("Root graph data can only be freed on machines.", this);
			}
			else
			{
				if (base._parentData == null)
				{
					throw new GraphPointerException("Child graph data can only be freed from parent graph data.", this);
				}
				base._parentData.FreeChildGraphData(base.parentElement);
				base._data = null;
				return;
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00008A54 File Offset: 0x00006C54
		public override bool Equals(object obj)
		{
			GraphReference graphReference = obj as GraphReference;
			return graphReference != null && base.InstanceEquals(graphReference);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00008A74 File Offset: 0x00006C74
		private void Hash()
		{
			this.hashCode = base.ComputeHashCode();
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00008A82 File Offset: 0x00006C82
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00008A8A File Offset: 0x00006C8A
		public static bool operator ==(GraphReference x, GraphReference y)
		{
			return x == y || (x != null && y != null && x.Equals(y));
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00008AA1 File Offset: 0x00006CA1
		public static bool operator !=(GraphReference x, GraphReference y)
		{
			return !(x == y);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00008AAD File Offset: 0x00006CAD
		public GraphReference ParentReference(bool ensureValid)
		{
			if (!base.isRoot)
			{
				GraphReference graphReference = this.Clone();
				graphReference.ExitParentElement();
				graphReference.Hash();
				return graphReference;
			}
			if (ensureValid)
			{
				throw new GraphPointerException("Trying to get parent graph reference of a root.", this);
			}
			return null;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00008ADC File Offset: 0x00006CDC
		public GraphReference ChildReference(IGraphParentElement parentElement, bool ensureValid, int? maxRecursionDepth = null)
		{
			GraphReference graphReference = this.Clone();
			string text;
			if (graphReference.TryEnterParentElement(parentElement, out text, maxRecursionDepth, false))
			{
				graphReference.Hash();
				return graphReference;
			}
			if (ensureValid)
			{
				throw new GraphPointerException(text, this);
			}
			return null;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00008B14 File Offset: 0x00006D14
		public GraphReference Revalidate(bool ensureValid)
		{
			GraphReference graphReference;
			try
			{
				graphReference = GraphReference.New(base.rootObject, base.parentElementGuids, ensureValid);
			}
			catch (Exception ex)
			{
				if (ensureValid)
				{
					throw;
				}
				string text = "Failed to revalidate graph pointer: \n";
				Exception ex2 = ex;
				Debug.LogWarning(text + ((ex2 != null) ? ex2.ToString() : null));
				graphReference = null;
			}
			return graphReference;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00008B70 File Offset: 0x00006D70
		public IEnumerable<GraphReference> GetBreadcrumbs()
		{
			int num;
			for (int depth = 0; depth < base.depth; depth = num + 1)
			{
				yield return GraphReference.New(base.root, this.parentElementStack.Take(depth), true);
				num = depth;
			}
			yield break;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00008B80 File Offset: 0x00006D80
		public static GraphReference Intern(GraphPointer pointer)
		{
			int num = pointer.ComputeHashCode();
			List<GraphReference> list;
			if (GraphReference.internPool.TryGetValue(num, out list))
			{
				foreach (GraphReference graphReference in list)
				{
					if (graphReference.InstanceEquals(pointer))
					{
						return graphReference;
					}
				}
				GraphReference graphReference2 = GraphReference.New(pointer);
				list.Add(graphReference2);
				return graphReference2;
			}
			GraphReference graphReference3 = GraphReference.New(pointer);
			GraphReference.internPool.Add(graphReference3.hashCode, new List<GraphReference> { graphReference3 });
			return graphReference3;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00008C2C File Offset: 0x00006E2C
		public static void FreeInvalidInterns()
		{
			List<int> list = ListPool<int>.New();
			foreach (KeyValuePair<int, List<GraphReference>> keyValuePair in GraphReference.internPool)
			{
				int key = keyValuePair.Key;
				List<GraphReference> value = keyValuePair.Value;
				List<GraphReference> list2 = ListPool<GraphReference>.New();
				foreach (GraphReference graphReference in value)
				{
					if (!graphReference.isValid)
					{
						list2.Add(graphReference);
					}
				}
				foreach (GraphReference graphReference2 in list2)
				{
					value.Remove(graphReference2);
				}
				if (value.Count == 0)
				{
					list.Add(key);
				}
				list2.Free<GraphReference>();
			}
			foreach (int num in list)
			{
				GraphReference.internPool.Remove(num);
			}
			list.Free<int>();
		}

		// Token: 0x040000E0 RID: 224
		[DoNotSerialize]
		private int hashCode;

		// Token: 0x040000E1 RID: 225
		private static readonly Dictionary<int, List<GraphReference>> internPool = new Dictionary<int, List<GraphReference>>();
	}
}
