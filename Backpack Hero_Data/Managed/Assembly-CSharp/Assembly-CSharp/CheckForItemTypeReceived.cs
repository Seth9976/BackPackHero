using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

// Token: 0x0200002A RID: 42
[CreateMenu("ItemInteractions/CheckForItemTypeReceived", 0)]
public class CheckForItemTypeReceived : ConditionDataBase
{
	// Token: 0x060000F8 RID: 248 RVA: 0x0000744C File Offset: 0x0000564C
	public override bool OnGetIsValid(INode parent)
	{
		return Overworld_InventoryManager.main.chosenItem && Item2.ShareItemTypes(this.itemsToCheckFor, Overworld_InventoryManager.main.chosenItem.itemType);
	}

	// Token: 0x04000091 RID: 145
	[SerializeField]
	private List<Item2.ItemType> itemsToCheckFor = new List<Item2.ItemType>();
}
