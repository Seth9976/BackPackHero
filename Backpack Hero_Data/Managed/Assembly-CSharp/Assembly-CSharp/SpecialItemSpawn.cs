using System;
using UnityEngine;

// Token: 0x020000EF RID: 239
public class SpecialItemSpawn : SpecialItem
{
	// Token: 0x06000862 RID: 2146 RVA: 0x000574B8 File Offset: 0x000556B8
	public override void UseSpecialEffect(Status stat)
	{
		foreach (GameObject gameObject in this.rewardItems)
		{
			GameObject itemByName = DebugItemManager.main.GetItemByName(gameObject.name);
			if (itemByName == null)
			{
				Debug.Log("Couldn't find item " + gameObject.name);
			}
			else
			{
				this.gameManager.rewardItems.Add(itemByName.gameObject);
			}
		}
	}

	// Token: 0x0400067F RID: 1663
	[SerializeField]
	private GameObject[] rewardItems;
}
