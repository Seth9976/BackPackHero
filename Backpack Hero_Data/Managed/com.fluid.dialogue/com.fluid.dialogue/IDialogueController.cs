using System;
using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Dialogues.Graphs;

namespace CleverCrow.Fluid.Dialogues
{
	// Token: 0x02000006 RID: 6
	public interface IDialogueController
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000012 RID: 18
		IDatabaseInstance LocalDatabase { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000013 RID: 19
		IDatabaseInstanceExtended LocalDatabaseExtended { get; }

		// Token: 0x06000014 RID: 20
		void PlayChild(IGraphData graph);
	}
}
