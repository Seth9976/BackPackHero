using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200014D RID: 333
	[UnitSurtitle("Graph")]
	public sealed class SetGraphVariable : SetVariableUnit, IGraphVariableUnit, IVariableUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x0600089B RID: 2203 RVA: 0x0000FDFB File Offset: 0x0000DFFB
		public SetGraphVariable()
		{
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0000FE03 File Offset: 0x0000E003
		public SetGraphVariable(string defaultName)
			: base(defaultName)
		{
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0000FE0C File Offset: 0x0000E00C
		protected override VariableDeclarations GetDeclarations(Flow flow)
		{
			return Variables.Graph(flow.stack);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0000FE19 File Offset: 0x0000E019
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
