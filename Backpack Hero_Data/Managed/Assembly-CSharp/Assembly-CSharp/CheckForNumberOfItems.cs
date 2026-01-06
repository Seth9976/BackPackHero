using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

// Token: 0x0200002F RID: 47
[CreateMenu("ItemInteractions/CheckForNumberOfItems", 0)]
public class CheckForNumberOfItems : ConditionDataBase
{
	// Token: 0x06000102 RID: 258 RVA: 0x000076D0 File Offset: 0x000058D0
	public override bool OnGetIsValid(INode parent)
	{
		return (MetaProgressSaveManager.main.storedItems.Count >= this.numberOfItems && this.isGreaterThanOrEqual) || (MetaProgressSaveManager.main.storedItems.Count < this.numberOfItems && !this.isGreaterThanOrEqual);
	}

	// Token: 0x0400009D RID: 157
	[SerializeField]
	private int numberOfItems;

	// Token: 0x0400009E RID: 158
	[SerializeField]
	private bool isGreaterThanOrEqual;
}
