using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000146 RID: 326
	[UnitSurtitle("Graph")]
	public sealed class IsGraphVariableDefined : IsVariableDefinedUnit, IGraphVariableUnit, IVariableUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x0600087D RID: 2173 RVA: 0x0000FC57 File Offset: 0x0000DE57
		public IsGraphVariableDefined()
		{
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0000FC5F File Offset: 0x0000DE5F
		public IsGraphVariableDefined(string defaultName)
			: base(defaultName)
		{
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0000FC68 File Offset: 0x0000DE68
		protected override VariableDeclarations GetDeclarations(Flow flow)
		{
			return Variables.Graph(flow.stack);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0000FC75 File Offset: 0x0000DE75
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
