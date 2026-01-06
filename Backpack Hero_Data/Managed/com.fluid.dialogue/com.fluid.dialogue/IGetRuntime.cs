using System;
using CleverCrow.Fluid.Dialogues.Graphs;

namespace CleverCrow.Fluid.Dialogues
{
	// Token: 0x02000010 RID: 16
	public interface IGetRuntime<T> : ISetup, IUniqueId
	{
		// Token: 0x06000056 RID: 86
		T GetRuntime(IGraph graphRuntime, IDialogueController dialogue);
	}
}
