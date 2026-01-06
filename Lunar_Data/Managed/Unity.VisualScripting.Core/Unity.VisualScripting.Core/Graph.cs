using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200005B RID: 91
	public abstract class Graph : IGraph, IDisposable, IPrewarmable, IAotStubbable, ISerializationDepender, ISerializationCallbackReceiver
	{
		// Token: 0x0600028D RID: 653 RVA: 0x00006621 File Offset: 0x00004821
		protected Graph()
		{
			this.elements = new MergedGraphElementCollection();
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000664A File Offset: 0x0000484A
		public override string ToString()
		{
			return StringUtility.FallbackWhitespace(this.title, base.ToString());
		}

		// Token: 0x0600028F RID: 655
		public abstract IGraphData CreateData();

		// Token: 0x06000290 RID: 656 RVA: 0x0000665D File Offset: 0x0000485D
		public virtual IGraphDebugData CreateDebugData()
		{
			return new GraphDebugData(this);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00006668 File Offset: 0x00004868
		public virtual void Instantiate(GraphReference instance)
		{
			foreach (IGraphElement graphElement in this.elements)
			{
				graphElement.Instantiate(instance);
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000066BC File Offset: 0x000048BC
		public virtual void Uninstantiate(GraphReference instance)
		{
			foreach (IGraphElement graphElement in this.elements)
			{
				graphElement.Uninstantiate(instance);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00006710 File Offset: 0x00004910
		[DoNotSerialize]
		public MergedGraphElementCollection elements { get; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00006718 File Offset: 0x00004918
		// (set) Token: 0x06000295 RID: 661 RVA: 0x00006720 File Offset: 0x00004920
		[Serialize]
		public string title { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00006729 File Offset: 0x00004929
		// (set) Token: 0x06000297 RID: 663 RVA: 0x00006731 File Offset: 0x00004931
		[Serialize]
		[InspectorTextArea(minLines = 1f, maxLines = 10f)]
		public string summary { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000673A File Offset: 0x0000493A
		// (set) Token: 0x06000299 RID: 665 RVA: 0x00006742 File Offset: 0x00004942
		[Serialize]
		public Vector2 pan { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000674B File Offset: 0x0000494B
		// (set) Token: 0x0600029B RID: 667 RVA: 0x00006753 File Offset: 0x00004953
		[Serialize]
		public float zoom { get; set; } = 1f;

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000675C File Offset: 0x0000495C
		public IEnumerable<ISerializationDependency> deserializationDependencies
		{
			get
			{
				return this._elements.SelectMany((IGraphElement e) => e.deserializationDependencies);
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00006788 File Offset: 0x00004988
		public virtual void OnBeforeSerialize()
		{
			this._elements.Clear();
			this._elements.AddRange(this.elements);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000067A6 File Offset: 0x000049A6
		public void OnAfterDeserialize()
		{
			Serialization.AwaitDependencies(this);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000067B0 File Offset: 0x000049B0
		public virtual void OnAfterDependenciesDeserialized()
		{
			this.elements.Clear();
			List<IGraphElement> list = ListPool<IGraphElement>.New();
			foreach (IGraphElement graphElement in this._elements)
			{
				list.Add(graphElement);
			}
			list.Sort((IGraphElement a, IGraphElement b) => a.dependencyOrder.CompareTo(b.dependencyOrder));
			foreach (IGraphElement graphElement2 in list)
			{
				try
				{
					if (graphElement2.HandleDependencies())
					{
						this.elements.Add(graphElement2);
					}
				}
				catch (Exception ex)
				{
					Debug.LogWarning(string.Format("Failed to add element to graph during deserialization: {0}\n{1}", graphElement2, ex));
				}
			}
			ListPool<IGraphElement>.Free(list);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000068B0 File Offset: 0x00004AB0
		public IEnumerable<object> GetAotStubs(HashSet<object> visited)
		{
			return this.elements.Where((IGraphElement element) => !visited.Contains(element)).Select(delegate(IGraphElement element)
			{
				visited.Add(element);
				return element;
			}).SelectMany((IGraphElement element) => element.GetAotStubs(visited));
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00006904 File Offset: 0x00004B04
		public void Prewarm()
		{
			if (this.prewarmed)
			{
				return;
			}
			foreach (IGraphElement graphElement in this.elements)
			{
				graphElement.Prewarm();
			}
			this.prewarmed = true;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00006964 File Offset: 0x00004B64
		public virtual void Dispose()
		{
			foreach (IGraphElement graphElement in this.elements)
			{
				graphElement.Dispose();
			}
		}

		// Token: 0x040000B9 RID: 185
		[SerializeAs("elements")]
		private List<IGraphElement> _elements = new List<IGraphElement>();

		// Token: 0x040000BF RID: 191
		private bool prewarmed;
	}
}
