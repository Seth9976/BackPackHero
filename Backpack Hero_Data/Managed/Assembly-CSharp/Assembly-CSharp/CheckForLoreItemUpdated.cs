using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;

// Token: 0x0200002C RID: 44
[CreateMenu("ItemInteractions/CheckForLoreItemUpdated", 0)]
public class CheckForLoreItemUpdated : ConditionDataBase
{
	// Token: 0x060000FC RID: 252 RVA: 0x00007553 File Offset: 0x00005753
	public override bool OnGetIsValid(INode parent)
	{
		return Overworld_InventoryManager.main.chosenItem && Overworld_InventoryManager.main.chosenItem.GetComponent<LoreUnlocker>();
	}
}
