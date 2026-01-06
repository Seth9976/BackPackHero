using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;

// Token: 0x02000025 RID: 37
[CreateMenu("ItemInteractions/CheckForAnyItemGiven", 0)]
public class CheckForAnyItemGiven : ConditionDataBase
{
	// Token: 0x060000ED RID: 237 RVA: 0x00007107 File Offset: 0x00005307
	public override bool OnGetIsValid(INode parent)
	{
		return Overworld_InventoryManager.main.chosenItem == null;
	}
}
