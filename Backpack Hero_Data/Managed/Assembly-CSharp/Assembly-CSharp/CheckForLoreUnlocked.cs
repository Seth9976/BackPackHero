using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

// Token: 0x0200000B RID: 11
[CreateMenu("CheckForLoreUnlocked", 0)]
public class CheckForLoreUnlocked : ConditionDataBase
{
	// Token: 0x0600002C RID: 44 RVA: 0x00002C50 File Offset: 0x00000E50
	public override bool OnGetIsValid(INode parent)
	{
		int num = 0;
		foreach (string text in MetaProgressSaveManager.main.oneOfAKindItems)
		{
			string displayName = Item2.GetDisplayName(text);
			Item2 item2ByName = DebugItemManager.main.GetItem2ByName(text);
			if (!MetaProgressSaveManager.main.storedItems.Contains(displayName) && item2ByName.itemType.Contains(this.itemType))
			{
				num++;
			}
		}
		return num >= this.num;
	}

	// Token: 0x04000012 RID: 18
	[SerializeField]
	public Item2.ItemType itemType;

	// Token: 0x04000013 RID: 19
	[SerializeField]
	public int num;
}
