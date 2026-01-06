using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F1 RID: 241
public class SpecialItemUnique : SpecialItem
{
	// Token: 0x06000866 RID: 2150 RVA: 0x00057848 File Offset: 0x00055A48
	public override void UseSpecialEffect(Status stat)
	{
		if (this.type == SpecialItemUnique.Type.reOrganize)
		{
			this.gameManager.StartReorganization();
			return;
		}
		if (this.type == SpecialItemUnique.Type.createCR8charge)
		{
			Object.Instantiate<GameObject>(this.prefabs[0], base.transform.position + Vector3.back * 0.01f, base.transform.rotation, base.transform.parent).GetComponent<EnergyBall>().energyValue = Mathf.RoundToInt(this.value);
			return;
		}
		if (this.type == SpecialItemUnique.Type.eatBreadBowl)
		{
			ItemPouch component = base.GetComponent<ItemPouch>();
			component.OpenPouch();
			while (component.itemsInside.Count > 0)
			{
				GameObject gameObject = component.itemsInside[0];
				Item2 component2 = gameObject.GetComponent<Item2>();
				ItemMovement component3 = component2.GetComponent<ItemMovement>();
				if (component2 && component3)
				{
					this.gameFlowManager.ConsiderItemUseIndirect(component2);
					while (component2.usesLimits.Count > 0 && component2.usesLimits[0].type == Item2.LimitedUses.Type.total && component2.usesLimits[0].currentValue > 0f)
					{
						this.gameFlowManager.ConsiderItemUseIndirect(component2);
					}
					component3.DelayDestroy();
				}
				if (component.itemsInside.Contains(gameObject))
				{
					component.itemsInside.Remove(gameObject);
				}
			}
			this.itemMovement.DelayDestroy();
			return;
		}
		if (this.type != SpecialItemUnique.Type.MoveToFront)
		{
			if (this.type == SpecialItemUnique.Type.moveBackOneSpot)
			{
				PetMaster petMasterFromInventory = PetMaster.GetPetMasterFromInventory(this.item.parentInventoryGrid);
				if (petMasterFromInventory == null)
				{
					if (this.player.mySpacerLocation.GetSiblingIndex() > 0)
					{
						this.player.mySpacerLocation.SetSiblingIndex(this.player.mySpacerLocation.GetSiblingIndex() - 1);
						return;
					}
				}
				else if (petMasterFromInventory.combatPetCom.mySpacerLocation.GetSiblingIndex() > 0)
				{
					petMasterFromInventory.combatPetCom.mySpacerLocation.SetSiblingIndex(petMasterFromInventory.combatPetCom.mySpacerLocation.GetSiblingIndex() - 1);
					return;
				}
			}
			else
			{
				if (this.type == SpecialItemUnique.Type.ChangeValueOfEffect)
				{
					using (List<Item2.Modifier>.Enumerator enumerator = this.item.modifiers.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Item2.Modifier modifier = enumerator.Current;
							foreach (Item2.Effect effect in modifier.effects)
							{
								effect.value = this.value;
							}
						}
						return;
					}
				}
				if (this.type == SpecialItemUnique.Type.ChangeValueOfEffectRandom)
				{
					using (List<Item2.Modifier>.Enumerator enumerator = this.item.modifiers.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Item2.Modifier modifier2 = enumerator.Current;
							foreach (Item2.Effect effect2 in modifier2.effects)
							{
								effect2.value = (float)Mathf.RoundToInt(Random.Range(-this.value, this.value));
							}
						}
						return;
					}
				}
				if (this.type == SpecialItemUnique.Type.moveEnemyToFront)
				{
					Enemy backmostEnemy = Enemy.GetBackmostEnemy();
					if (backmostEnemy)
					{
						backmostEnemy.mySpacerLocation.SetAsFirstSibling();
						this.gameManager.SelectEnemy(backmostEnemy);
						return;
					}
				}
				else if (this.type == SpecialItemUnique.Type.moveEnemyToBack)
				{
					Enemy frontMostEnemy = Enemy.GetFrontMostEnemy();
					if (frontMostEnemy)
					{
						frontMostEnemy.mySpacerLocation.SetAsLastSibling();
						this.gameManager.SelectEnemy(frontMostEnemy);
						return;
					}
				}
				else if (this.type == SpecialItemUnique.Type.ChangeValueOfThisManaStone)
				{
					ManaStone component4 = base.GetComponent<ManaStone>();
					if (component4)
					{
						component4.ChangeMana(Mathf.RoundToInt(this.value));
						return;
					}
				}
				else
				{
					if (this.type == SpecialItemUnique.Type.DeleteAllManaStonesMaybe)
					{
						using (List<Item2>.Enumerator enumerator3 = Item2.GetItemsOfTypes(new List<Item2.ItemType> { Item2.ItemType.ManaStone }, Item2.GetAllItems()).GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								Item2 item = enumerator3.Current;
								if ((float)Random.Range(0, 100) < this.value)
								{
									item.GetComponent<ItemMovement>().DelayDestroy();
								}
							}
							return;
						}
					}
					if (this.type == SpecialItemUnique.Type.AddToCombatRewards)
					{
						this.gameManager.ChangeItemsAllowedToTakePerm(Mathf.RoundToInt(this.value));
						return;
					}
					if (this.type == SpecialItemUnique.Type.RerollCombatRewards)
					{
						int num = this.gameManager.numOfItemsAllowedToTake;
						foreach (Item2 item2 in Item2.allItems)
						{
							if (item2.itemMovement.returnsToOutOfInventoryPosition)
							{
								if (item2.itemMovement.inGrid && item2 != this.item)
								{
									num++;
								}
								item2.itemMovement.DelayDestroy();
							}
						}
						int num2;
						ItemPouch.DeleteAllitemsRoll(out num2);
						num += num2;
						Chest chest = Object.FindObjectOfType<Chest>();
						if (chest && chest.type == Chest.Type.carving)
						{
							Object.FindObjectOfType<Tote>().SpawnCarvings();
							this.gameManager.StartSimpleLimitedItemGetPeriod(num);
							return;
						}
						if (chest && chest.type == Chest.Type.components)
						{
							this.gameManager.SpawnItemsOfType(Item2.ItemType.Component, 5);
							this.gameManager.StartSimpleLimitedItemGetPeriod(num);
							return;
						}
						if (chest && chest.type == Chest.Type.pets)
						{
							ItemSpawner.InstantiateItems(ItemSpawner.GetItems(3, new List<Item2.ItemType> { Item2.ItemType.Pet }, false, false));
							this.gameManager.StartSimpleLimitedItemGetPeriod(num);
							return;
						}
						this.gameManager.StartLimitedItemGetPeriod();
						this.gameManager.StartSimpleLimitedItemGetPeriod(num);
						return;
					}
					else
					{
						if (this.type == SpecialItemUnique.Type.RemovePlayerAP)
						{
							Player.main.SetAP(0);
							return;
						}
						if (this.type == SpecialItemUnique.Type.FeedbackMicrophone)
						{
							foreach (Enemy enemy in Enemy.allEnemies)
							{
								enemy.ConsiderIntentSwapFeedbackMicrophone();
							}
						}
					}
				}
			}
			return;
		}
		PetMaster petMasterFromInventory2 = PetMaster.GetPetMasterFromInventory(this.item.parentInventoryGrid);
		if (petMasterFromInventory2 == null)
		{
			this.player.mySpacerLocation.SetAsLastSibling();
			return;
		}
		petMasterFromInventory2.combatPetCom.mySpacerLocation.SetAsLastSibling();
	}

	// Token: 0x04000681 RID: 1665
	[SerializeField]
	private List<GameObject> prefabs;

	// Token: 0x04000682 RID: 1666
	[SerializeField]
	private SpecialItemUnique.Type type;

	// Token: 0x0200036A RID: 874
	public enum Type
	{
		// Token: 0x04001467 RID: 5223
		reOrganize,
		// Token: 0x04001468 RID: 5224
		createCR8charge,
		// Token: 0x04001469 RID: 5225
		createCR8TutorialItems,
		// Token: 0x0400146A RID: 5226
		eatBreadBowl,
		// Token: 0x0400146B RID: 5227
		MoveToFront,
		// Token: 0x0400146C RID: 5228
		ChangeValueOfEffect,
		// Token: 0x0400146D RID: 5229
		ChangeValueOfEffectRandom,
		// Token: 0x0400146E RID: 5230
		moveBackOneSpot,
		// Token: 0x0400146F RID: 5231
		moveEnemyToFront,
		// Token: 0x04001470 RID: 5232
		moveEnemyToBack,
		// Token: 0x04001471 RID: 5233
		ChangeValueOfThisManaStone,
		// Token: 0x04001472 RID: 5234
		DeleteAllManaStonesMaybe,
		// Token: 0x04001473 RID: 5235
		AddToCombatRewards,
		// Token: 0x04001474 RID: 5236
		RerollCombatRewards,
		// Token: 0x04001475 RID: 5237
		RemovePlayerAP,
		// Token: 0x04001476 RID: 5238
		FeedbackMicrophone
	}
}
