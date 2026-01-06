using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200013B RID: 315
	[UnitSurtitle("Graph")]
	public sealed class GetGraphVariable : GetVariableUnit, IGraphVariableUnit, IVariableUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x0600085F RID: 2143 RVA: 0x0000FA62 File Offset: 0x0000DC62
		public GetGraphVariable()
		{
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0000FA6A File Offset: 0x0000DC6A
		public GetGraphVariable(string defaultName)
			: base(defaultName)
		{
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0000FA73 File Offset: 0x0000DC73
		protected override VariableDeclarations GetDeclarations(Flow flow)
		{
			return Variables.Graph(flow.stack);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0000FA80 File Offset: 0x0000DC80
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
