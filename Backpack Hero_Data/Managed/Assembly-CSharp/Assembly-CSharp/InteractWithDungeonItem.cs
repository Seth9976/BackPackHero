using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions;
using UnityEngine;

// Token: 0x0200003E RID: 62
[CreateMenu("Dungeon Actions/InteractWithDungeonItem", 0)]
public class InteractWithDungeonItem : ActionDataBase
{
	// Token: 0x06000123 RID: 291 RVA: 0x00007F24 File Offset: 0x00006124
	public override void OnStart()
	{
		if (this.interactType == InteractWithDungeonItem.InteractType.Destroy)
		{
			foreach (GameObject gameObject in this.itemsToCheckFor)
			{
				Item2 itemByName = Item2.GetItemByName(gameObject.name);
				if (!(itemByName == null) && itemByName.itemMovement)
				{
					itemByName.itemMovement.DelayDestroy();
					if (this.amountNecessary == InteractWithDungeonItem.AmountNecessary.Any)
					{
						break;
					}
				}
			}
		}
	}

	// Token: 0x040000BA RID: 186
	[SerializeField]
	private List<GameObject> itemsToCheckFor;

	// Token: 0x040000BB RID: 187
	[SerializeField]
	private InteractWithDungeonItem.AmountNecessary amountNecessary;

	// Token: 0x040000BC RID: 188
	[SerializeField]
	private InteractWithDungeonItem.InteractType interactType;

	// Token: 0x0200025F RID: 607
	private enum AmountNecessary
	{
		// Token: 0x04000EF1 RID: 3825
		Any,
		// Token: 0x04000EF2 RID: 3826
		All
	}

	// Token: 0x02000260 RID: 608
	private enum InteractType
	{
		// Token: 0x04000EF4 RID: 3828
		Destroy,
		// Token: 0x04000EF5 RID: 3829
		Spawn
	}
}
