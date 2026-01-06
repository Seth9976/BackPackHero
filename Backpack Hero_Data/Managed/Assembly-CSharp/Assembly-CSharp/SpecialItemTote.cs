using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F0 RID: 240
public class SpecialItemTote : SpecialItem
{
	// Token: 0x06000864 RID: 2148 RVA: 0x00057530 File Offset: 0x00055730
	public override void UseSpecialEffect(Status stat)
	{
		Tote tote = Object.FindObjectOfType<Tote>();
		if (!tote)
		{
			return;
		}
		if (this.type == SpecialItemTote.Type.reduceAllTo0)
		{
			Carving[] array = Object.FindObjectsOfType<Carving>();
			for (int i = 0; i < array.Length; i++)
			{
				foreach (Item2.Cost cost in array[i].summoningCosts)
				{
					Item2.Cost.CostModifier costModifier = new Item2.Cost.CostModifier();
					costModifier.length = Item2.Modifier.Length.forTurn;
					costModifier.value = -999;
					cost.costModifiers.Add(costModifier);
				}
			}
			return;
		}
		if (this.type == SpecialItemTote.Type.redrawHand)
		{
			tote.DrawNewHand();
			return;
		}
		if (this.type == SpecialItemTote.Type.banishRandom)
		{
			Carving[] array2 = Object.FindObjectsOfType<Carving>();
			if (array2.Length != 0)
			{
				int num = Random.Range(0, array2.Length);
				Carving carving = array2[num];
				this.gameManager.AddParticles(carving.transform.position, carving.GetComponentInChildren<SpriteRenderer>(), null);
				tote.BanishCarving(carving.gameObject);
				return;
			}
		}
		else
		{
			if (this.type == SpecialItemTote.Type.banishSelf)
			{
				this.gameManager.AddParticles(base.transform.position, base.GetComponentInChildren<SpriteRenderer>(), null);
				tote.BanishCarving(base.gameObject);
				return;
			}
			if (this.type == SpecialItemTote.Type.getEnergyForEachSpaceDestroyed)
			{
				List<Item2> list = new List<Item2>();
				this.item.FindItemsAndGridsinArea(list, new List<GridSquare>(), new List<Item2.Area> { Item2.Area.myPlaySpace }, Item2.AreaDistance.all, null, null, null, true, false, true);
				using (List<Item2>.Enumerator enumerator2 = list.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Item2 item = enumerator2.Current;
						int num2 = item.FindGridSpaces().Count * Mathf.RoundToInt(this.value);
						this.player.ChangeAP(num2);
					}
					return;
				}
			}
			if (this.type == SpecialItemTote.Type.getGoldForEachSpaceDestroyed)
			{
				List<Item2> list2 = new List<Item2>();
				this.item.FindItemsAndGridsinArea(list2, new List<GridSquare>(), new List<Item2.Area> { Item2.Area.myPlaySpace }, Item2.AreaDistance.all, null, null, null, true, false, true);
				using (List<Item2>.Enumerator enumerator2 = list2.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Item2 item2 = enumerator2.Current;
						int num3 = item2.FindGridSpaces().Count * Mathf.RoundToInt(this.value);
						this.gameManager.ChangeGold(num3);
					}
					return;
				}
			}
			if (this.type == SpecialItemTote.Type.getManaForEachSpaceDestroyed)
			{
				List<Item2> list3 = new List<Item2>();
				this.item.FindItemsAndGridsinArea(list3, new List<GridSquare>(), new List<Item2.Area> { Item2.Area.myPlaySpace }, Item2.AreaDistance.all, null, null, null, true, false, true);
				foreach (Item2 item3 in list3)
				{
					int num4 = item3.FindGridSpaces().Count * Mathf.RoundToInt(this.value);
					ManaStone component = base.GetComponent<ManaStone>();
					if (component)
					{
						component.ChangeMana(num4);
					}
				}
			}
		}
	}

	// Token: 0x04000680 RID: 1664
	[SerializeField]
	private SpecialItemTote.Type type;

	// Token: 0x02000369 RID: 873
	public enum Type
	{
		// Token: 0x0400145F RID: 5215
		reduceAllTo0,
		// Token: 0x04001460 RID: 5216
		redrawHand,
		// Token: 0x04001461 RID: 5217
		banishRandom,
		// Token: 0x04001462 RID: 5218
		banishSelf,
		// Token: 0x04001463 RID: 5219
		getEnergyForEachSpaceDestroyed,
		// Token: 0x04001464 RID: 5220
		getGoldForEachSpaceDestroyed,
		// Token: 0x04001465 RID: 5221
		getManaForEachSpaceDestroyed
	}
}
