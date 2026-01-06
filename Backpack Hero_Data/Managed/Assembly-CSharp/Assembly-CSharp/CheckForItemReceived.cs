using System;
using System.Collections.Generic;
using System.Linq;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

// Token: 0x02000028 RID: 40
[CreateMenu("ItemInteractions/CheckForItemReceived", 0)]
public class CheckForItemReceived : ConditionDataBase
{
	// Token: 0x060000F3 RID: 243 RVA: 0x000071E4 File Offset: 0x000053E4
	public override bool OnGetIsValid(INode parent)
	{
		if (!Overworld_InventoryManager.main.chosenItem)
		{
			return false;
		}
		string text = Item2.GetDisplayName(Overworld_InventoryManager.main.chosenItem.name).ToLower().Trim();
		using (List<Item2>.Enumerator enumerator = this.itemsToCheckFor.Select((GameObject x) => x.GetComponent<Item2>()).ToList<Item2>().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (Item2.GetDisplayName(enumerator.Current.name).ToLower().Trim() == text)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0400008C RID: 140
	[SerializeField]
	private List<GameObject> itemsToCheckFor = new List<GameObject>();
}
