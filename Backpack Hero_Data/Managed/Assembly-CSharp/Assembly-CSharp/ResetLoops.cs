using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions;

// Token: 0x02000044 RID: 68
[CreateMenu("ResetLoops", 0)]
public class ResetLoops : ActionDataBase
{
	// Token: 0x06000133 RID: 307 RVA: 0x000083CF File Offset: 0x000065CF
	public override ActionStatus OnUpdate()
	{
		CheckForTooManyLoops.currentLoops = 0;
		return ActionStatus.Success;
	}
}
