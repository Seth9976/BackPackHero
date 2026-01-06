using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unity.VisualScripting
{
	// Token: 0x0200005E RID: 94
	public abstract class GraphElement<TGraph> : IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable where TGraph : class, IGraph
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00006C57 File Offset: 0x00004E57
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x00006C5F File Offset: 0x00004E5F
		[Serialize]
		public Guid guid { get; set; } = Guid.NewGuid();

		// Token: 0x060002B7 RID: 695 RVA: 0x00006C68 File Offset: 0x00004E68
		public virtual void Instantiate(GraphReference instance)
		{
			IGraphElementWithData graphElementWithData = this as IGraphElementWithData;
			if (graphElementWithData != null)
			{
				instance.data.CreateElementData(graphElementWithData);
			}
			IGraphNesterElement graphNesterElement = this as IGraphNesterElement;
			if (graphNesterElement != null && graphNesterElement.nest.graph != null)
			{
				GraphInstances.Instantiate(instance.ChildReference(graphNesterElement, true, null));
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00006CBC File Offset: 0x00004EBC
		public virtual void Uninstantiate(GraphReference instance)
		{
			IGraphNesterElement graphNesterElement = this as IGraphNesterElement;
			if (graphNesterElement != null && graphNesterElement.nest.graph != null)
			{
				GraphInstances.Uninstantiate(instance.ChildReference(graphNesterElement, true, null));
			}
			IGraphElementWithData graphElementWithData = this as IGraphElementWithData;
			if (graphElementWithData != null)
			{
				instance.data.FreeElementData(graphElementWithData);
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00006D0C File Offset: 0x00004F0C
		public virtual void BeforeAdd()
		{
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00006D10 File Offset: 0x00004F10
		public virtual void AfterAdd()
		{
			HashSet<GraphReference> hashSet = GraphInstances.OfPooled(this.graph);
			foreach (GraphReference graphReference in hashSet)
			{
				this.Instantiate(graphReference);
			}
			hashSet.Free<GraphReference>();
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00006D78 File Offset: 0x00004F78
		public virtual void BeforeRemove()
		{
			HashSet<GraphReference> hashSet = GraphInstances.OfPooled(this.graph);
			foreach (GraphReference graphReference in hashSet)
			{
				this.Uninstantiate(graphReference);
			}
			hashSet.Free<GraphReference>();
			this.Dispose();
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00006DE4 File Offset: 0x00004FE4
		public virtual void AfterRemove()
		{
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00006DE6 File Offset: 0x00004FE6
		public virtual void Dispose()
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00006DE8 File Offset: 0x00004FE8
		protected void InstantiateNest()
		{
			IGraphNesterElement graphNesterElement = (IGraphNesterElement)this;
			if (this.graph == null)
			{
				return;
			}
			HashSet<GraphReference> hashSet = GraphInstances.OfPooled(this.graph);
			foreach (GraphReference graphReference in hashSet)
			{
				GraphInstances.Instantiate(graphReference.ChildReference(graphNesterElement, true, null));
			}
			hashSet.Free<GraphReference>();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00006E70 File Offset: 0x00005070
		protected void UninstantiateNest()
		{
			HashSet<GraphReference> hashSet = GraphInstances.ChildrenOfPooled((IGraphNesterElement)this);
			foreach (GraphReference graphReference in hashSet)
			{
				GraphInstances.Uninstantiate(graphReference);
			}
			hashSet.Free<GraphReference>();
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00006ED0 File Offset: 0x000050D0
		[DoNotSerialize]
		public virtual int dependencyOrder
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00006ED3 File Offset: 0x000050D3
		public virtual bool HandleDependencies()
		{
			return true;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00006ED6 File Offset: 0x000050D6
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x00006EDE File Offset: 0x000050DE
		[DoNotSerialize]
		public TGraph graph { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00006EE7 File Offset: 0x000050E7
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00006EF4 File Offset: 0x000050F4
		[DoNotSerialize]
		IGraph IGraphElement.graph
		{
			get
			{
				return this.graph;
			}
			set
			{
				Ensure.That("value").IsOfType<TGraph>(value);
				this.graph = (TGraph)((object)value);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00006F12 File Offset: 0x00005112
		[DoNotSerialize]
		IGraph IGraphItem.graph
		{
			get
			{
				return this.graph;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00006F1F File Offset: 0x0000511F
		public virtual IEnumerable<ISerializationDependency> deserializationDependencies
		{
			get
			{
				return Enumerable.Empty<ISerializationDependency>();
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00006F26 File Offset: 0x00005126
		public virtual IEnumerable<object> GetAotStubs(HashSet<object> visited)
		{
			return Enumerable.Empty<object>();
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00006F2D File Offset: 0x0000512D
		public virtual void Prewarm()
		{
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00006F2F File Offset: 0x0000512F
		protected void CopyFrom(GraphElement<TGraph> source)
		{
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00006F34 File Offset: 0x00005134
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.GetType().Name);
			stringBuilder.Append("#");
			stringBuilder.Append(this.guid.ToString().Substring(0, 5));
			stringBuilder.Append("...");
			return stringBuilder.ToString();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00006F97 File Offset: 0x00005197
		public virtual AnalyticsIdentifier GetAnalyticsIdentifier()
		{
			throw new NotImplementedException();
		}
	}
}
