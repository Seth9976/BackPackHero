using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200015D RID: 349
	[SpecialUnit]
	public abstract class NesterUnit<TGraph, TMacro> : Unit, INesterUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable, IGraphNesterElement, IGraphParentElement, IGraphParent, IGraphNester where TGraph : class, IGraph, new() where TMacro : Macro<TGraph>
	{
		// Token: 0x0600090E RID: 2318 RVA: 0x00010553 File Offset: 0x0000E753
		protected NesterUnit()
		{
			this.nest.nester = this;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00010572 File Offset: 0x0000E772
		protected NesterUnit(TMacro macro)
		{
			this.nest.nester = this;
			this.nest.macro = macro;
			this.nest.source = GraphSource.Macro;
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x000105A9 File Offset: 0x0000E7A9
		public override bool canDefine
		{
			get
			{
				return this.nest.graph != null;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x000105BE File Offset: 0x0000E7BE
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x000105C6 File Offset: 0x0000E7C6
		[Serialize]
		public GraphNest<TGraph, TMacro> nest { get; private set; } = new GraphNest<TGraph, TMacro>();

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x000105CF File Offset: 0x0000E7CF
		[DoNotSerialize]
		IGraphNest IGraphNester.nest
		{
			get
			{
				return this.nest;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x000105D7 File Offset: 0x0000E7D7
		[DoNotSerialize]
		IGraph IGraphParent.childGraph
		{
			get
			{
				return this.nest.graph;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x000105E9 File Offset: 0x0000E7E9
		[DoNotSerialize]
		bool IGraphParent.isSerializationRoot
		{
			get
			{
				return this.nest.source == GraphSource.Macro;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x000105F9 File Offset: 0x0000E7F9
		[DoNotSerialize]
		Object IGraphParent.serializedObject
		{
			get
			{
				return this.nest.macro;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0001060B File Offset: 0x0000E80B
		[DoNotSerialize]
		public override IEnumerable<ISerializationDependency> deserializationDependencies
		{
			get
			{
				return this.nest.deserializationDependencies;
			}
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00010618 File Offset: 0x0000E818
		public override IEnumerable<object> GetAotStubs(HashSet<object> visited)
		{
			return LinqUtility.Concat<object>(new IEnumerable[]
			{
				base.GetAotStubs(visited),
				this.nest.GetAotStubs(visited)
			});
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0001063E File Offset: 0x0000E83E
		protected void CopyFrom(NesterUnit<TGraph, TMacro> source)
		{
			base.CopyFrom(source);
			this.nest = source.nest;
		}

		// Token: 0x0600091A RID: 2330
		public abstract TGraph DefaultGraph();

		// Token: 0x0600091B RID: 2331 RVA: 0x00010653 File Offset: 0x0000E853
		IGraph IGraphParent.DefaultGraph()
		{
			return this.DefaultGraph();
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00010660 File Offset: 0x0000E860
		void IGraphNester.InstantiateNest()
		{
			base.InstantiateNest();
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00010668 File Offset: 0x0000E868
		void IGraphNester.UninstantiateNest()
		{
			base.UninstantiateNest();
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00010670 File Offset: 0x0000E870
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
