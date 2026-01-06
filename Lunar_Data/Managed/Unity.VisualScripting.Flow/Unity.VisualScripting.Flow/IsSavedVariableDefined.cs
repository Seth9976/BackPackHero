using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000148 RID: 328
	[UnitSurtitle("Save")]
	public sealed class IsSavedVariableDefined : IsVariableDefinedUnit, ISavedVariableUnit, IVariableUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x06000888 RID: 2184 RVA: 0x0000FCEB File Offset: 0x0000DEEB
		public IsSavedVariableDefined()
		{
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0000FCF3 File Offset: 0x0000DEF3
		public IsSavedVariableDefined(string defaultName)
			: base(defaultName)
		{
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0000FCFC File Offset: 0x0000DEFC
		protected override VariableDeclarations GetDeclarations(Flow flow)
		{
			return Variables.Saved;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0000FD03 File Offset: 0x0000DF03
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
