using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200014C RID: 332
	[UnitSurtitle("Application")]
	public sealed class SetApplicationVariable : SetVariableUnit, IApplicationVariableUnit, IVariableUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x06000897 RID: 2199 RVA: 0x0000FDDB File Offset: 0x0000DFDB
		public SetApplicationVariable()
		{
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0000FDE3 File Offset: 0x0000DFE3
		public SetApplicationVariable(string defaultName)
			: base(defaultName)
		{
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0000FDEC File Offset: 0x0000DFEC
		protected override VariableDeclarations GetDeclarations(Flow flow)
		{
			return Variables.Application;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0000FDF3 File Offset: 0x0000DFF3
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
