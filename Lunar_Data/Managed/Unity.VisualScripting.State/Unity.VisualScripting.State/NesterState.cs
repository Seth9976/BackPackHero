using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200000C RID: 12
	public abstract class NesterState<TGraph, TMacro> : State, INesterState, IState, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable, IGraphElementWithData, IGraphNesterElement, IGraphParentElement, IGraphParent, IGraphNester where TGraph : class, IGraph, new() where TMacro : Macro<TGraph>
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002521 File Offset: 0x00000721
		protected NesterState()
		{
			this.nest.nester = this;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002540 File Offset: 0x00000740
		protected NesterState(TMacro macro)
		{
			this.nest.nester = this;
			this.nest.macro = macro;
			this.nest.source = GraphSource.Macro;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002577 File Offset: 0x00000777
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000257F File Offset: 0x0000077F
		[Serialize]
		public GraphNest<TGraph, TMacro> nest { get; private set; } = new GraphNest<TGraph, TMacro>();

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002588 File Offset: 0x00000788
		[DoNotSerialize]
		IGraphNest IGraphNester.nest
		{
			get
			{
				return this.nest;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002590 File Offset: 0x00000790
		[DoNotSerialize]
		IGraph IGraphParent.childGraph
		{
			get
			{
				return this.nest.graph;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000025A2 File Offset: 0x000007A2
		[DoNotSerialize]
		bool IGraphParent.isSerializationRoot
		{
			get
			{
				return this.nest.source == GraphSource.Macro;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000025B2 File Offset: 0x000007B2
		[DoNotSerialize]
		Object IGraphParent.serializedObject
		{
			get
			{
				return this.nest.macro;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000025C4 File Offset: 0x000007C4
		[DoNotSerialize]
		public override IEnumerable<ISerializationDependency> deserializationDependencies
		{
			get
			{
				return this.nest.deserializationDependencies;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000025D1 File Offset: 0x000007D1
		protected void CopyFrom(NesterState<TGraph, TMacro> source)
		{
			base.CopyFrom(source);
			this.nest = source.nest;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000025E6 File Offset: 0x000007E6
		public override IEnumerable<object> GetAotStubs(HashSet<object> visited)
		{
			return LinqUtility.Concat<object>(new IEnumerable[]
			{
				base.GetAotStubs(visited),
				this.nest.GetAotStubs(visited)
			});
		}

		// Token: 0x0600003E RID: 62
		public abstract TGraph DefaultGraph();

		// Token: 0x0600003F RID: 63 RVA: 0x0000260C File Offset: 0x0000080C
		IGraph IGraphParent.DefaultGraph()
		{
			return this.DefaultGraph();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002619 File Offset: 0x00000819
		void IGraphNester.InstantiateNest()
		{
			base.InstantiateNest();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002621 File Offset: 0x00000821
		void IGraphNester.UninstantiateNest()
		{
			base.UninstantiateNest();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002629 File Offset: 0x00000829
		StateGraph IState.get_graph()
		{
			return base.graph;
		}
	}
}
