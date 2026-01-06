using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200013A RID: 314
	[UnitSurtitle("Application")]
	public sealed class GetApplicationVariable : GetVariableUnit, IApplicationVariableUnit, IVariableUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x0600085B RID: 2139 RVA: 0x0000FA42 File Offset: 0x0000DC42
		public GetApplicationVariable()
		{
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0000FA4A File Offset: 0x0000DC4A
		public GetApplicationVariable(string defaultName)
			: base(defaultName)
		{
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0000FA53 File Offset: 0x0000DC53
		protected override VariableDeclarations GetDeclarations(Flow flow)
		{
			return Variables.Application;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0000FA5A File Offset: 0x0000DC5A
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
