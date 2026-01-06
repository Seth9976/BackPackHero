using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions;

// Token: 0x02000039 RID: 57
[CreateMenu("ItemInteractions/DestroyChosenItem", 0)]
public class DestroyItem : ActionDataBase
{
	// Token: 0x06000119 RID: 281 RVA: 0x00007E57 File Offset: 0x00006057
	public override void OnStart()
	{
		Overworld_InventoryManager.main.DestroyItem();
	}
}
