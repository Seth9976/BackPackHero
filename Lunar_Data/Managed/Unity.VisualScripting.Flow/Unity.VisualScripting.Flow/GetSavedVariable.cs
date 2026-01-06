using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200013D RID: 317
	[UnitSurtitle("Save")]
	public sealed class GetSavedVariable : GetVariableUnit, ISavedVariableUnit, IVariableUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x0600086A RID: 2154 RVA: 0x0000FAF6 File Offset: 0x0000DCF6
		public GetSavedVariable()
		{
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0000FAFE File Offset: 0x0000DCFE
		public GetSavedVariable(string defaultName)
			: base(defaultName)
		{
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0000FB07 File Offset: 0x0000DD07
		protected override VariableDeclarations GetDeclarations(Flow flow)
		{
			return Variables.Saved;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0000FB0E File Offset: 0x0000DD0E
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
