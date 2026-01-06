using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

// Token: 0x02000027 RID: 39
[CreateMenu("Dungeon Actions/CheckForItemInDungeon", 0)]
public class CheckForItemInDungeon : ConditionDataBase
{
	// Token: 0x060000F1 RID: 241 RVA: 0x00007148 File Offset: 0x00005348
	public override bool OnGetIsValid(INode parent)
	{
		int num = 0;
		foreach (GameObject gameObject in this.itemsToCheckFor)
		{
			if (Item2.CheckForItemOfName(this.itemsToCheckFor[0].GetComponent<Item2>()))
			{
				num++;
			}
		}
		return (this.amountNecessary == CheckForItemInDungeon.AmountNecessary.Any && num >= 1) || (this.amountNecessary == CheckForItemInDungeon.AmountNecessary.All && num == this.itemsToCheckFor.Count);
	}

	// Token: 0x0400008A RID: 138
	[SerializeField]
	private List<GameObject> itemsToCheckFor;

	// Token: 0x0400008B RID: 139
	[SerializeField]
	private CheckForItemInDungeon.AmountNecessary amountNecessary;

	// Token: 0x0200025A RID: 602
	private enum AmountNecessary
	{
		// Token: 0x04000EE4 RID: 3812
		Any,
		// Token: 0x04000EE5 RID: 3813
		All
	}
}
