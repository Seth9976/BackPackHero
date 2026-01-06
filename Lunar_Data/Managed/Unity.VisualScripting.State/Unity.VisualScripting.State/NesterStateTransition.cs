using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200000D RID: 13
	public abstract class NesterStateTransition<TGraph, TMacro> : StateTransition, INesterStateTransition, IStateTransition, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable, IConnection<IState, IState>, IGraphNesterElement, IGraphParentElement, IGraphParent, IGraphNester where TGraph : class, IGraph, new() where TMacro : Macro<TGraph>
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002631 File Offset: 0x00000831
		protected NesterStateTransition()
		{
			this.nest.nester = this;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002650 File Offset: 0x00000850
		protected NesterStateTransition(IState source, IState destination)
			: base(source, destination)
		{
			this.nest.nester = this;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002671 File Offset: 0x00000871
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002679 File Offset: 0x00000879
		[Serialize]
		public GraphNest<TGraph, TMacro> nest { get; private set; } = new GraphNest<TGraph, TMacro>();

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002682 File Offset: 0x00000882
		[DoNotSerialize]
		IGraphNest IGraphNester.nest
		{
			get
			{
				return this.nest;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000268A File Offset: 0x0000088A
		[DoNotSerialize]
		IGraph IGraphParent.childGraph
		{
			get
			{
				return this.nest.graph;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000269C File Offset: 0x0000089C
		[DoNotSerialize]
		bool IGraphParent.isSerializationRoot
		{
			get
			{
				return this.nest.source == GraphSource.Macro;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000026AC File Offset: 0x000008AC
		[DoNotSerialize]
		Object IGraphParent.serializedObject
		{
			get
			{
				return this.nest.macro;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000026BE File Offset: 0x000008BE
		[DoNotSerialize]
		public override IEnumerable<ISerializationDependency> deserializationDependencies
		{
			get
			{
				return this.nest.deserializationDependencies;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000026CB File Offset: 0x000008CB
		public override IEnumerable<object> GetAotStubs(HashSet<object> visited)
		{
			return LinqUtility.Concat<object>(new IEnumerable[]
			{
				base.GetAotStubs(visited),
				this.nest.GetAotStubs(visited)
			});
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000026F1 File Offset: 0x000008F1
		protected void CopyFrom(NesterStateTransition<TGraph, TMacro> source)
		{
			base.CopyFrom(source);
			this.nest = source.nest;
		}

		// Token: 0x0600004E RID: 78
		public abstract TGraph DefaultGraph();

		// Token: 0x0600004F RID: 79 RVA: 0x00002706 File Offset: 0x00000906
		IGraph IGraphParent.DefaultGraph()
		{
			return this.DefaultGraph();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002713 File Offset: 0x00000913
		void IGraphNester.InstantiateNest()
		{
			base.InstantiateNest();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000271B File Offset: 0x0000091B
		void IGraphNester.UninstantiateNest()
		{
			base.UninstantiateNest();
		}
	}
}
