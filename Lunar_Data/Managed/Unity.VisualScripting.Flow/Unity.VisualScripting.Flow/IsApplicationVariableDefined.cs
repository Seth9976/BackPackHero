using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000143 RID: 323
	[UnitSurtitle("Application")]
	public sealed class IsApplicationVariableDefined : IsVariableDefinedUnit, IApplicationVariableUnit, IVariableUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x06000879 RID: 2169 RVA: 0x0000FC37 File Offset: 0x0000DE37
		public IsApplicationVariableDefined()
		{
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0000FC3F File Offset: 0x0000DE3F
		public IsApplicationVariableDefined(string defaultName)
			: base(defaultName)
		{
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0000FC48 File Offset: 0x0000DE48
		protected override VariableDeclarations GetDeclarations(Flow flow)
		{
			return Variables.Application;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0000FC4F File Offset: 0x0000DE4F
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
