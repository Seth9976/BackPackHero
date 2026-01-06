using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000059 RID: 89
public class HazardManager : MonoBehaviour
{
	// Token: 0x06000195 RID: 405 RVA: 0x0000A32C File Offset: 0x0000852C
	public static void FindAllHazards()
	{
		foreach (Item2 item in Item2.allItems)
		{
			if (item)
			{
				for (int i = 0; i < item.activeItemStatusEffects.Count; i++)
				{
					if (item.activeItemStatusEffects[i].length == Item2.ItemStatusEffect.Length.whileCoveredByHazards)
					{
						item.activeItemStatusEffects.RemoveAt(i);
						i--;
					}
				}
			}
		}
		foreach (Item2 item2 in Item2.allItems)
		{
			if (item2 && item2.itemMovement && item2.gridObject && item2.itemMovement.inGrid && !item2.CheckForStatusEffect(Item2.ItemStatusEffect.Type.cannotBeDisabledByHazards) && !item2.itemType.Contains(Item2.ItemType.Hazard))
			{
				List<Item2> list = new List<Item2>();
				item2.FindItemsAndGridsinArea(list, new List<GridSquare>(), new List<Item2.Area> { Item2.Area.myPlaySpace }, Item2.AreaDistance.all, null, null, null, true, false, true);
				int num = 0;
				foreach (Item2 item3 in list)
				{
					if (item3.itemType.Contains(Item2.ItemType.Hazard) && !item3.destroyed && item3.itemMovement && item3.itemMovement.inGrid)
					{
						num++;
					}
				}
				if (num >= item2.gridObject.gridPositions.Count)
				{
					if (GameFlowManager.main)
					{
						GameFlowManager.main.AddItemStatusEffectToApplyAtEndOfQueuedActions(item2, Item2.ItemStatusEffect.Type.disabled, Item2.ItemStatusEffect.Length.whileCoveredByHazards, 1);
					}
					else
					{
						item2.ChangeStatusEffectValue(Item2.ItemStatusEffect.Type.disabled, Item2.ItemStatusEffect.Length.whileCoveredByHazards, 1);
					}
				}
			}
		}
	}
}
