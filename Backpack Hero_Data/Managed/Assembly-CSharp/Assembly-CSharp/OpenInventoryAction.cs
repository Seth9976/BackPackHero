using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions;
using UnityEngine;

// Token: 0x02000040 RID: 64
[CreateMenu("Town Actions/Open Inventory", 0)]
public class OpenInventoryAction : ActionDataBase
{
	// Token: 0x06000127 RID: 295 RVA: 0x00008018 File Offset: 0x00006218
	public override void OnStart()
	{
		this.inventoryInterface = Overworld_InventoryManager.main.inventoryInterface;
		Overworld_InventoryManager.main.ToggleInventory(Overworld_InventoryManager.ClickAction.SELECTITEM);
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00008035 File Offset: 0x00006235
	public override ActionStatus OnUpdate()
	{
		if (Overworld_InventoryManager.main.currentInventoryScreen != null)
		{
			return ActionStatus.Continue;
		}
		return ActionStatus.Success;
	}

	// Token: 0x040000BE RID: 190
	private GameObject inventoryInterface;
}
