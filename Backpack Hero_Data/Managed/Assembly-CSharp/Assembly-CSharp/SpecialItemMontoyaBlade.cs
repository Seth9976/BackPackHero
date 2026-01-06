using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000ED RID: 237
public class SpecialItemMontoyaBlade : SpecialItem
{
	// Token: 0x0600085C RID: 2140 RVA: 0x00057374 File Offset: 0x00055574
	private int GetItemsInArea()
	{
		List<Item2> list = new List<Item2>();
		List<GridSquare> list2 = new List<GridSquare>();
		this.item.FindItemsAndGridsinArea(list, list2, new List<Item2.Area> { this.area }, Item2.AreaDistance.all, null, null, null, true, false, true);
		return list.Count;
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x000573B8 File Offset: 0x000555B8
	public override void UseSpecialEffect(Status stat)
	{
		int itemsInArea = this.GetItemsInArea();
		Debug.Log(itemsInArea);
		stat.Attack(this.player.stats, itemsInArea * -1, null, false, false, false);
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x000573F0 File Offset: 0x000555F0
	public override string GetDescription()
	{
		int itemsInArea = this.GetItemsInArea();
		return this.description.Replace("/x", itemsInArea.ToString() ?? "");
	}

	// Token: 0x0400067E RID: 1662
	[SerializeField]
	private Item2.Area area;
}
