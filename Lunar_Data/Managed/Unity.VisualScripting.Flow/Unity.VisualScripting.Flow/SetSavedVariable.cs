using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200014F RID: 335
	[UnitSurtitle("Save")]
	public sealed class SetSavedVariable : SetVariableUnit, ISavedVariableUnit, IVariableUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x060008A6 RID: 2214 RVA: 0x0000FE8F File Offset: 0x0000E08F
		public SetSavedVariable()
		{
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0000FE97 File Offset: 0x0000E097
		public SetSavedVariable(string defaultName)
			: base(defaultName)
		{
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0000FEA0 File Offset: 0x0000E0A0
		protected override VariableDeclarations GetDeclarations(Flow flow)
		{
			return Variables.Saved;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0000FEA7 File Offset: 0x0000E0A7
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
