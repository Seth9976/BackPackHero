using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

// Token: 0x02000026 RID: 38
[CreateMenu("CheckForCostumeUnlocked", 0)]
public class CheckForCostumeUnlocked : ConditionDataBase
{
	// Token: 0x060000EF RID: 239 RVA: 0x00007121 File Offset: 0x00005321
	public override bool OnGetIsValid(INode parent)
	{
		return MetaProgressSaveManager.main.CostumeUnlocked(this.costume) == this.isUnlocked;
	}

	// Token: 0x04000088 RID: 136
	[SerializeField]
	private RuntimeAnimatorController costume;

	// Token: 0x04000089 RID: 137
	[SerializeField]
	private bool isUnlocked;
}
