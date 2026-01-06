using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions;
using UnityEngine;

// Token: 0x02000049 RID: 73
[CreateMenu("Town Actions/Unlock Lore", 0)]
public class UnlockLore : ActionDataBase
{
	// Token: 0x0600013F RID: 319 RVA: 0x00008556 File Offset: 0x00006756
	public override void OnStart()
	{
		this.window = Overworld_InventoryManager.main.UnlockLore();
	}

	// Token: 0x06000140 RID: 320 RVA: 0x00008568 File Offset: 0x00006768
	public override ActionStatus OnUpdate()
	{
		if (this.window != null)
		{
			return ActionStatus.Continue;
		}
		return ActionStatus.Success;
	}

	// Token: 0x040000D2 RID: 210
	private GameObject window;
}
