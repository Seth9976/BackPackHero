using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004D RID: 77
public class ItemDropper : MonoBehaviour
{
	// Token: 0x06000245 RID: 581 RVA: 0x0000BEC0 File Offset: 0x0000A0C0
	public void DropItem()
	{
		if (HordeRemainingDisplay.instance.wonLevel)
		{
			return;
		}
		float num = 0f;
		foreach (ItemDropper.ItemDrop itemDrop in this.itemsToDrop)
		{
			num += itemDrop.dropChance;
		}
		float num2 = Random.Range(0f, num);
		float num3 = 0f;
		foreach (ItemDropper.ItemDrop itemDrop2 in this.itemsToDrop)
		{
			num3 += itemDrop2.dropChance;
			if (num2 <= num3)
			{
				if (itemDrop2.item == null)
				{
					break;
				}
				Object.Instantiate<GameObject>(itemDrop2.item, base.transform.position, Quaternion.identity);
				break;
			}
		}
	}

	// Token: 0x040001AE RID: 430
	public List<ItemDropper.ItemDrop> itemsToDrop = new List<ItemDropper.ItemDrop>();

	// Token: 0x020000E5 RID: 229
	[Serializable]
	public class ItemDrop
	{
		// Token: 0x04000459 RID: 1113
		public GameObject item;

		// Token: 0x0400045A RID: 1114
		public float dropChance;
	}
}
