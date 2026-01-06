using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000F5 RID: 245
public class ValueChangerEx : MonoBehaviour
{
	// Token: 0x06000885 RID: 2181 RVA: 0x000592D9 File Offset: 0x000574D9
	private void Start()
	{
		this.FindReferences();
		this.item = base.GetComponent<Item2>();
		if (!this.item)
		{
			this.item = base.GetComponentInParent<Item2>();
		}
		this.FindValue(base.gameObject);
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x00059313 File Offset: 0x00057513
	private void FindReferences()
	{
		this.player = Player.main;
		this.gameManager = GameManager.main;
		this.saveManager = Object.FindObjectOfType<SaveManager>();
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x00059336 File Offset: 0x00057536
	public void FindEffectTemp()
	{
		this.item = base.GetComponent<Item2>();
		if (!this.item)
		{
			this.item = base.GetComponentInParent<Item2>();
		}
		this.FindValue(base.gameObject);
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x0005936A File Offset: 0x0005756A
	private bool SetReference(object reference, ValueChangerEx.ValueType type)
	{
		this.valueType = type;
		this.reference = reference;
		this.ChangeValue();
		return true;
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x00059384 File Offset: 0x00057584
	private void SetValue(object reference, ValueChangerEx.ValueType type, float value)
	{
		switch (type)
		{
		case ValueChangerEx.ValueType.Cost:
			((Item2.Cost)reference).baseValue = (int)value;
			((Item2.Cost)reference).currentValue = (int)value;
			return;
		case ValueChangerEx.ValueType.CostMod:
			((Item2.Cost.CostModifier)reference).value = (int)value;
			return;
		case ValueChangerEx.ValueType.UseLimit:
			if (((Item2.LimitedUses)reference).currentValue <= -1337000f)
			{
				((Item2.LimitedUses)reference).currentValue = (float)Mathf.FloorToInt(value);
			}
			else
			{
				((Item2.LimitedUses)reference).currentValue += Mathf.Max(0f, (float)Mathf.FloorToInt(value) - ((Item2.LimitedUses)reference).value);
			}
			((Item2.LimitedUses)reference).value = (float)Mathf.FloorToInt(value);
			return;
		case ValueChangerEx.ValueType.EffectValue:
			((Item2.Effect)reference).value = (float)((int)value);
			return;
		case ValueChangerEx.ValueType.ItemStatusEffectValue:
			((Item2.ItemStatusEffect)reference).num = (int)value;
			return;
		case ValueChangerEx.ValueType.CreateNumOfItems:
			((Item2.CreateEffect)reference).numberToCreate = (int)value;
			return;
		case ValueChangerEx.ValueType.MovementX:
			((Item2.MovementEffect)reference).movementAmount.x = (float)Mathf.FloorToInt(value);
			return;
		case ValueChangerEx.ValueType.MovementY:
			((Item2.MovementEffect)reference).movementAmount.y = (float)Mathf.FloorToInt(value);
			return;
		case ValueChangerEx.ValueType.MovementRotation:
			((Item2.MovementEffect)reference).rotationAmount = (float)(Mathf.FloorToInt(value) * 90);
			return;
		case ValueChangerEx.ValueType.Mana:
			((ManaStone)reference).currentPower = (int)value;
			return;
		case ValueChangerEx.ValueType.MaxMana:
			((ManaStone)reference).maxPower = (int)value;
			if (this.mana_just_initialized)
			{
				if (((ManaStone)reference).startingPower != 1 && (float)((ManaStone)reference).startingPower != this.valueToReplace)
				{
					((ManaStone)reference).currentPower = ((ManaStone)reference).startingPower;
				}
				else
				{
					((ManaStone)reference).currentPower = (int)value;
				}
				Debug.Log("Setting manastone " + ((ManaStone)reference).currentPower.ToString() + " " + value.ToString());
				this.mana_just_initialized = false;
				return;
			}
			break;
		case ValueChangerEx.ValueType.ValueChangerMultiplier:
			((ValueChangerEx)reference).multiplier = value;
			return;
		case ValueChangerEx.ValueType.ValueChangerBase:
			((ValueChangerEx)reference).baseValue = value;
			break;
		default:
			return;
		}
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x00059588 File Offset: 0x00057788
	private bool FindValue(object parent)
	{
		if (parent == null)
		{
			return false;
		}
		if (this.reference != null)
		{
			return false;
		}
		GameObject gameObject = parent as GameObject;
		if (gameObject == null)
		{
			Item2 item = parent as Item2;
			ManaStone manaStone;
			if (item == null)
			{
				manaStone = parent as ManaStone;
				if (manaStone == null)
				{
					Carving carving = parent as Carving;
					ValueChangerEx valueChangerEx;
					if (carving == null)
					{
						valueChangerEx = parent as ValueChangerEx;
						if (valueChangerEx == null)
						{
							Item2.Cost cost = parent as Item2.Cost;
							Item2.LimitedUses limitedUses;
							if (cost == null)
							{
								limitedUses = parent as Item2.LimitedUses;
								if (limitedUses == null)
								{
									Item2.Effect effect = parent as Item2.Effect;
									Item2.ItemStatusEffect itemStatusEffect;
									if (effect == null)
									{
										itemStatusEffect = parent as Item2.ItemStatusEffect;
										if (itemStatusEffect == null)
										{
											Item2.MovementEffect movementEffect = parent as Item2.MovementEffect;
											if (movementEffect == null)
											{
												return false;
											}
											if (movementEffect.movementAmount.x == this.valueToReplace)
											{
												return this.SetReference(movementEffect, ValueChangerEx.ValueType.MovementX);
											}
											if (movementEffect.movementAmount.y == this.valueToReplace)
											{
												return this.SetReference(movementEffect, ValueChangerEx.ValueType.MovementX);
											}
											if (movementEffect.rotationAmount == this.valueToReplace)
											{
												return this.SetReference(movementEffect, ValueChangerEx.ValueType.MovementRotation);
											}
											return false;
										}
									}
									else
									{
										if (effect.value == this.valueToReplace)
										{
											return this.SetReference(effect, ValueChangerEx.ValueType.EffectValue);
										}
										using (List<Item2.ItemStatusEffect>.Enumerator enumerator = effect.itemStatusEffect.GetEnumerator())
										{
											while (enumerator.MoveNext())
											{
												Item2.ItemStatusEffect itemStatusEffect2 = enumerator.Current;
												if (this.FindValue(itemStatusEffect2))
												{
													return true;
												}
											}
											return false;
										}
									}
									if ((float)itemStatusEffect.num == this.valueToReplace)
									{
										return this.SetReference(itemStatusEffect, ValueChangerEx.ValueType.ItemStatusEffectValue);
									}
									return false;
								}
							}
							else
							{
								if ((float)cost.baseValue == this.valueToReplace)
								{
									return this.SetReference(cost, ValueChangerEx.ValueType.Cost);
								}
								using (List<Item2.Cost.CostModifier>.Enumerator enumerator2 = cost.costModifiers.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										Item2.Cost.CostModifier costModifier = enumerator2.Current;
										if ((float)costModifier.value == this.valueToReplace)
										{
											return this.SetReference(costModifier, ValueChangerEx.ValueType.CostMod);
										}
									}
									return false;
								}
							}
							if (limitedUses.value == this.valueToReplace)
							{
								return this.SetReference(limitedUses, ValueChangerEx.ValueType.UseLimit);
							}
							return false;
						}
					}
					else
					{
						using (List<Item2.Cost>.Enumerator enumerator3 = carving.summoningCosts.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								Item2.Cost cost2 = enumerator3.Current;
								if (this.FindValue(cost2))
								{
									return true;
								}
							}
							return false;
						}
					}
					if (valueChangerEx.multiplier == this.valueToReplace)
					{
						return this.SetReference(valueChangerEx, ValueChangerEx.ValueType.ValueChangerMultiplier);
					}
					if (valueChangerEx.baseValue == this.valueToReplace)
					{
						return this.SetReference(valueChangerEx, ValueChangerEx.ValueType.ValueChangerBase);
					}
					return false;
				}
			}
			else
			{
				foreach (Item2.Cost cost3 in item.costs)
				{
					if (this.FindValue(cost3))
					{
						return true;
					}
				}
				foreach (Item2.LimitedUses limitedUses2 in item.usesLimits)
				{
					if (this.FindValue(limitedUses2))
					{
						return true;
					}
				}
				foreach (Item2.CombattEffect combattEffect in item.combatEffects)
				{
					if (this.FindValue(combattEffect.effect))
					{
						return true;
					}
				}
				foreach (Item2.CreateEffect createEffect in item.createEffects)
				{
					if ((float)createEffect.numberToCreate == this.valueToReplace)
					{
						return this.SetReference(createEffect, ValueChangerEx.ValueType.CreateNumOfItems);
					}
				}
				foreach (Item2.Modifier modifier in item.modifiers)
				{
					foreach (Item2.Effect effect2 in modifier.effects)
					{
						if (this.FindValue(effect2))
						{
							return true;
						}
					}
				}
				foreach (Item2.MovementEffect movementEffect2 in item.movementEffects)
				{
					if (this.FindValue(movementEffect2))
					{
						return true;
					}
				}
				foreach (Item2.AddModifier addModifier in item.addModifiers)
				{
					foreach (Item2.Effect effect3 in addModifier.modifier.effects)
					{
						if (this.FindValue(effect3))
						{
							return true;
						}
					}
				}
				foreach (Item2.ItemStatusEffect itemStatusEffect3 in item.activeItemStatusEffects)
				{
					if (this.FindValue(itemStatusEffect3))
					{
						return true;
					}
				}
				using (List<ContextMenuButton.ContextMenuButtonAndCost>.Enumerator enumerator11 = item.contextMenuOptions.GetEnumerator())
				{
					while (enumerator11.MoveNext())
					{
						ContextMenuButton.ContextMenuButtonAndCost contextMenuButtonAndCost = enumerator11.Current;
						if (this.FindValue(contextMenuButtonAndCost.costs))
						{
							return true;
						}
					}
					return false;
				}
			}
			if ((float)manaStone.maxPower == this.valueToReplace)
			{
				this.mana_just_initialized = true;
				return this.SetReference(manaStone, ValueChangerEx.ValueType.MaxMana);
			}
			if ((float)manaStone.startingPower == this.valueToReplace)
			{
				manaStone.startingPower = 0;
				return this.SetReference(manaStone, ValueChangerEx.ValueType.Mana);
			}
			return false;
		}
		foreach (Component component in gameObject.GetComponents<Component>())
		{
			if (this.FindValue(component))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x00059C08 File Offset: 0x00057E08
	private void Update()
	{
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x00059C0C File Offset: 0x00057E0C
	public static void ResetAllValueChangesForSaving()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("ItemParent");
		if (!gameObject)
		{
			Debug.Log("The itemParent transform couldn't be found at all");
			return;
		}
		foreach (object obj in gameObject.transform)
		{
			Transform transform = (Transform)obj;
			ValueChangerEx.SetValueChanger(transform);
			foreach (object obj2 in transform)
			{
				ValueChangerEx.SetValueChanger((Transform)obj2);
			}
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Dungeon Event");
		for (int i = 0; i < array.Length; i++)
		{
			foreach (object obj3 in array[i].transform)
			{
				ValueChangerEx.SetValueChanger((Transform)obj3);
			}
		}
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x00059D28 File Offset: 0x00057F28
	public static void SetValueChanger(Transform t)
	{
		if (!t || !t.gameObject)
		{
			return;
		}
		if (t.gameObject.activeInHierarchy)
		{
			ValueChangerEx component = t.GetComponent<ValueChangerEx>();
			if (component)
			{
				component.ResetValueForSaving();
				return;
			}
		}
		else
		{
			t.gameObject.SetActive(true);
			ValueChangerEx component2 = t.GetComponent<ValueChangerEx>();
			if (component2)
			{
				component2.ResetValueForSaving();
			}
			t.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x00059D9B File Offset: 0x00057F9B
	public void ResetValueForSaving()
	{
		if (this.reference != null)
		{
			this.SetValue(this.reference, this.valueType, this.valueToReplace);
		}
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x00059DC0 File Offset: 0x00057FC0
	public void ChangeValue()
	{
		if (!this.gameManager || !this.saveManager)
		{
			this.FindReferences();
		}
		if (!this.gameManager || !this.saveManager)
		{
			return;
		}
		if (this.saveManager.isSavingOrLoading)
		{
			return;
		}
		if (!this.item)
		{
			return;
		}
		if (this.reference == null)
		{
			this.FindValue(base.gameObject);
		}
		if (this.reference == null)
		{
			return;
		}
		float num = 0f;
		Status statusFromInventory = PetMaster.GetStatusFromInventory(this.item.lastParentInventoryGrid, this.player);
		Status selectedEnemyStatus = Enemy.GetSelectedEnemyStatus();
		Enemy.GetAllEnemyStats();
		switch (this.replaceWithValue)
		{
		case ValueChangerEx.ReplaceWithValue.AP:
			num = (float)this.player.AP;
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.EXP:
			num = (float)this.player.experience;
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.Level:
			num = (float)this.player.level;
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.Gold:
			num = (float)this.gameManager.goldAmount;
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.Floor:
			num = (float)this.gameManager.floor;
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.Loop:
			num = (float)(Mathf.RoundToInt((float)(this.gameManager.floor / 9)) + 1);
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.PlayerHP:
			if (!statusFromInventory)
			{
				return;
			}
			num = (float)statusFromInventory.health;
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.PlayerMaxHP:
			if (!statusFromInventory)
			{
				return;
			}
			num = (float)statusFromInventory.maxHealth;
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.PlayerBlock:
			if (!statusFromInventory)
			{
				return;
			}
			num = (float)statusFromInventory.armor;
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.PlayerBurn:
			num = (float)Mathf.Max(0, statusFromInventory.GetStatusEffectValue(StatusEffect.Type.fire));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.PlayerFreeze:
			num = (float)Mathf.Max(0, statusFromInventory.GetStatusEffectValue(StatusEffect.Type.freeze));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.PlayerPoison:
			num = (float)Mathf.Max(0, statusFromInventory.GetStatusEffectValue(StatusEffect.Type.poison));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.PlayerHaste:
			num = (float)Mathf.Max(0, statusFromInventory.GetStatusEffectValue(StatusEffect.Type.haste));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.PlayerSlow:
			num = (float)Mathf.Max(0, statusFromInventory.GetStatusEffectValue(StatusEffect.Type.slow));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.PlayerWeak:
			num = (float)Mathf.Max(0, statusFromInventory.GetStatusEffectValue(StatusEffect.Type.weak));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.PlayerRage:
			num = (float)Mathf.Max(0, statusFromInventory.GetStatusEffectValue(StatusEffect.Type.rage));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.PlayerDodge:
			num = (float)Mathf.Max(0, statusFromInventory.GetStatusEffectValue(StatusEffect.Type.dodge));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.PlayerRegen:
			num = (float)Mathf.Max(0, statusFromInventory.GetStatusEffectValue(StatusEffect.Type.regen));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.PlayerSpikes:
			num = (float)Mathf.Max(0, statusFromInventory.GetStatusEffectValue(StatusEffect.Type.spikes));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.TargetHP:
			if (selectedEnemyStatus == null)
			{
				num = 0f;
				goto IL_0B43;
			}
			num = (float)selectedEnemyStatus.health;
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.TargetMaxHP:
			if (!statusFromInventory)
			{
				return;
			}
			if (selectedEnemyStatus == null)
			{
				num = 0f;
				goto IL_0B43;
			}
			num = (float)selectedEnemyStatus.maxHealth;
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.TargetBlock:
			if (selectedEnemyStatus == null)
			{
				num = 0f;
				goto IL_0B43;
			}
			num = (float)selectedEnemyStatus.armor;
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.TargetBurn:
			if (selectedEnemyStatus == null)
			{
				num = 0f;
				goto IL_0B43;
			}
			num = (float)Mathf.Max(0, selectedEnemyStatus.GetStatusEffectValue(StatusEffect.Type.fire));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.TargetFreeze:
			if (selectedEnemyStatus == null)
			{
				num = 0f;
				goto IL_0B43;
			}
			num = (float)Mathf.Max(0, selectedEnemyStatus.GetStatusEffectValue(StatusEffect.Type.freeze));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.TargetPoison:
			if (selectedEnemyStatus == null)
			{
				num = 0f;
				goto IL_0B43;
			}
			num = (float)Mathf.Max(0, selectedEnemyStatus.GetStatusEffectValue(StatusEffect.Type.poison));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.TargetHaste:
			if (selectedEnemyStatus == null)
			{
				num = 0f;
				goto IL_0B43;
			}
			num = (float)Mathf.Max(0, selectedEnemyStatus.GetStatusEffectValue(StatusEffect.Type.haste));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.TargetSlow:
			if (selectedEnemyStatus == null)
			{
				num = 0f;
				goto IL_0B43;
			}
			num = (float)Mathf.Max(0, selectedEnemyStatus.GetStatusEffectValue(StatusEffect.Type.slow));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.TargetWeak:
			if (selectedEnemyStatus == null)
			{
				num = 0f;
				goto IL_0B43;
			}
			num = (float)Mathf.Max(0, selectedEnemyStatus.GetStatusEffectValue(StatusEffect.Type.weak));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.TargetRage:
			if (selectedEnemyStatus == null)
			{
				num = 0f;
				goto IL_0B43;
			}
			num = (float)Mathf.Max(0, selectedEnemyStatus.GetStatusEffectValue(StatusEffect.Type.rage));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.TargetDodge:
			if (selectedEnemyStatus == null)
			{
				num = 0f;
				goto IL_0B43;
			}
			num = (float)Mathf.Max(0, selectedEnemyStatus.GetStatusEffectValue(StatusEffect.Type.dodge));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.TargetRegen:
			if (selectedEnemyStatus == null)
			{
				num = 0f;
				goto IL_0B43;
			}
			num = (float)Mathf.Max(0, selectedEnemyStatus.GetStatusEffectValue(StatusEffect.Type.regen));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.TargetSpikes:
			if (selectedEnemyStatus == null)
			{
				num = 0f;
				goto IL_0B43;
			}
			num = (float)Mathf.Max(0, selectedEnemyStatus.GetStatusEffectValue(StatusEffect.Type.spikes));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.TargetCharm:
			if (selectedEnemyStatus == null)
			{
				num = 0f;
				goto IL_0B43;
			}
			num = (float)Mathf.Max(0, selectedEnemyStatus.GetStatusEffectValue(StatusEffect.Type.charm));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.TargetSleep:
			if (selectedEnemyStatus == null)
			{
				num = 0f;
				goto IL_0B43;
			}
			num = (float)Mathf.Max(0, selectedEnemyStatus.GetStatusEffectValue(StatusEffect.Type.sleep));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.TargetToughHide:
			if (selectedEnemyStatus == null)
			{
				num = 0f;
				goto IL_0B43;
			}
			num = (float)Mathf.Max(0, selectedEnemyStatus.GetStatusEffectValue(StatusEffect.Type.toughHide));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.ManastonePower:
			if (base.GetComponent<ManaStone>() != null)
			{
				num = (float)base.GetComponent<ManaStone>().currentPower;
				goto IL_0B43;
			}
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.ManastoneMaxPower:
			if (base.GetComponent<ManaStone>() != null)
			{
				num = (float)base.GetComponent<ManaStone>().maxPower;
				goto IL_0B43;
			}
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.ManastoneConnectedPower:
		{
			using (List<ManaStone>.Enumerator enumerator = ConnectionManager.main.FindManaStonesForItem(this.item, 0).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ManaStone manaStone = enumerator.Current;
					num += (float)manaStone.currentPower;
				}
				goto IL_0B43;
			}
			break;
		}
		case ValueChangerEx.ReplaceWithValue.ManastoneConnectedMaxPower:
			break;
		case ValueChangerEx.ReplaceWithValue.numOfEnemies:
			goto IL_066A;
		case ValueChangerEx.ReplaceWithValue.numOfEnemiesDefeated:
		case ValueChangerEx.ReplaceWithValue.numOfPetsAlive:
			num = (float)CombatPet.GetLivePets().Count;
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.numOfPetsDefeated:
			num = (float)CombatPet.GetDeadPets();
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.numOfPets:
			num = (float)CombatPet.GetPets().Count;
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.enemyTargetPosition:
		{
			List<Enemy> list = Enemy.GetEnemiesAlive();
			list = list.OrderBy((Enemy x) => x.transform.position.x).ToList<Enemy>();
			Enemy selectedEnemy = Enemy.GetSelectedEnemy();
			if (!(selectedEnemy != null))
			{
				num = 0f;
				goto IL_0B43;
			}
			if (list.Contains(selectedEnemy))
			{
				num = (float)(list.IndexOf(selectedEnemy) + 1);
				goto IL_0B43;
			}
			num = 0f;
			goto IL_0B43;
		}
		case ValueChangerEx.ReplaceWithValue.numOfTurns:
			num = (float)GameFlowManager.main.turnNumber;
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.numOfItems:
		{
			List<Item2> list2 = new List<Item2>();
			List<GridSquare> list3 = new List<GridSquare>();
			this.item.FindItemsAndGridsinArea(list2, list3, this.areas, this.areaDistance, null, null, null, true, false, true);
			num = 0f;
			using (List<GameObject>.Enumerator enumerator2 = this.itemPrefabs.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					GameObject gameObject = enumerator2.Current;
					if (gameObject != null)
					{
						num += (float)Item2.GetAllItemsCopies(gameObject.GetComponent<Item2>(), list2).Count;
					}
				}
				goto IL_0B43;
			}
			goto IL_07B7;
		}
		case ValueChangerEx.ReplaceWithValue.numOfItemTypes:
			goto IL_07B7;
		case ValueChangerEx.ReplaceWithValue.carvingsPlayed:
			num = (float)Mathf.Max(0, GameFlowManager.main.GetCombatStat(GameFlowManager.CombatStat.Type.carvingsUsed, GameFlowManager.CombatStat.Length.combat));
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.carvingsInDiscard:
			num = (float)Mathf.Max(0, Object.FindObjectOfType<Tote>().GetCarvingsInDiscard());
			goto IL_0B43;
		case ValueChangerEx.ReplaceWithValue.getSizeOfItem:
		{
			List<Item2> list4 = new List<Item2>();
			List<GridSquare> list5 = new List<GridSquare>();
			this.item.FindItemsAndGridsinArea(list4, list5, this.areas, this.areaDistance, null, null, null, true, false, true);
			num = 0f;
			if (list4.Count > 0)
			{
				using (List<Item2>.Enumerator enumerator3 = list4.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						Item2 item = enumerator3.Current;
						if (!(item == this.item) && item)
						{
							num += (float)Mathf.Max(0, list4[0].GetComponent<ItemMovement>().GetSpacesNeeded());
						}
					}
					goto IL_0B43;
				}
				goto IL_08D5;
			}
			goto IL_0B43;
		}
		case ValueChangerEx.ReplaceWithValue.numOfPockets:
			goto IL_08D5;
		case ValueChangerEx.ReplaceWithValue.numOfSpacesInThisPocket:
		{
			List<PocketManager.Pocket> pocketsFromItem = PocketManager.GetPocketsFromItem(this.item);
			int num2 = 0;
			foreach (PocketManager.Pocket pocket in pocketsFromItem)
			{
				num2 += PocketManager.GetSpacesInPocket(pocket);
			}
			num = (float)num2;
			goto IL_0B43;
		}
		case ValueChangerEx.ReplaceWithValue.numOfSpacesInAllOtherPockets:
		{
			List<PocketManager.Pocket> pocketsFromItem2 = PocketManager.GetPocketsFromItem(this.item);
			int num3 = Object.FindObjectsOfType<GridSquare>().Length;
			int num4 = 0;
			foreach (PocketManager.Pocket pocket2 in pocketsFromItem2)
			{
				num4 += PocketManager.GetSpacesInPocket(pocket2);
			}
			num = (float)(num3 - num4);
			goto IL_0B43;
		}
		case ValueChangerEx.ReplaceWithValue.numOfPocketsInArea:
		{
			List<PocketManager.Pocket> list6 = new List<PocketManager.Pocket>();
			List<Item2> list7 = new List<Item2>();
			List<GridSquare> list8 = new List<GridSquare>();
			this.item.FindItemsAndGridsinArea(list7, list8, this.areas, this.areaDistance, null, null, null, true, false, true);
			foreach (GridSquare gridSquare in list8)
			{
				PocketManager.Pocket pocket3 = PocketManager.GetPocket(gridSquare);
				if (pocket3 != null && !list6.Contains(pocket3))
				{
					list6.Add(pocket3);
				}
			}
			num = (float)list6.Count;
			goto IL_0B43;
		}
		case ValueChangerEx.ReplaceWithValue.numOfItemsInThisPouch:
		{
			ItemPouch component = base.GetComponent<ItemPouch>();
			if (component)
			{
				num = (float)component.itemsInside.Count;
				goto IL_0B43;
			}
			goto IL_0B43;
		}
		case ValueChangerEx.ReplaceWithValue.numOfCursesOnThisItem:
		{
			int num5 = 0;
			using (List<Item2.Modifier>.Enumerator enumerator6 = this.item.appliedModifiers.GetEnumerator())
			{
				while (enumerator6.MoveNext())
				{
					if (enumerator6.Current.name.ToLower() == "curse")
					{
						num5++;
					}
				}
			}
			foreach (Item2.ItemStatusEffect itemStatusEffect in this.item.activeItemStatusEffects)
			{
				if (itemStatusEffect.source != null && itemStatusEffect.source == "curse")
				{
					num5++;
				}
			}
			num = (float)num5;
			goto IL_0B43;
		}
		default:
			Debug.LogError("ValueChangerEx: ReplaceWithValue not implemented: " + this.replaceWithValue.ToString());
			goto IL_0B43;
		}
		using (List<ManaStone>.Enumerator enumerator = ConnectionManager.main.FindManaStonesForItem(this.item, 0).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ManaStone manaStone2 = enumerator.Current;
				num += (float)manaStone2.maxPower;
			}
			goto IL_0B43;
		}
		IL_066A:
		num = (float)Enemy.EnemiesAliveCount();
		goto IL_0B43;
		IL_07B7:
		List<Item2> list9 = new List<Item2>();
		List<GridSquare> list10 = new List<GridSquare>();
		this.item.FindItemsAndGridsinArea(list9, list10, this.areas, this.areaDistance, null, null, null, true, false, true);
		num = (float)Item2.FilterByTypes(this.types, list9).Count;
		goto IL_0B43;
		IL_08D5:
		if (!this.pm)
		{
			this.pm = PocketManager.main;
		}
		if (this.pm)
		{
			num = (float)Mathf.Max(0, this.pm.GetNumOfPockets());
		}
		IL_0B43:
		float num6 = this.baseValue + num * this.multiplier;
		this.SetValue(this.reference, this.valueType, num6);
		if (num6 != this.oldValue && this.item && this.item.itemMovement)
		{
			base.StartCoroutine(this.item.itemMovement.ModifiedAnimation());
		}
		this.oldValue = num6;
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x0005A9EC File Offset: 0x00058BEC
	public void AddHighlights()
	{
		List<Item2> list = new List<Item2>();
		if (this.replaceWithValue == ValueChangerEx.ReplaceWithValue.numOfItems)
		{
			List<Item2> list2 = new List<Item2>();
			List<GridSquare> list3 = new List<GridSquare>();
			this.item.FindItemsAndGridsinArea(list2, list3, this.areas, Item2.AreaDistance.all, null, null, null, true, false, true);
			using (List<GameObject>.Enumerator enumerator = this.itemPrefabs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					GameObject gameObject = enumerator.Current;
					list.AddRange(Item2.GetAllItemsCopies(gameObject.GetComponent<Item2>(), list2));
				}
				goto IL_00BB;
			}
		}
		if (this.replaceWithValue == ValueChangerEx.ReplaceWithValue.numOfItemTypes)
		{
			List<Item2> list4 = new List<Item2>();
			List<GridSquare> list5 = new List<GridSquare>();
			this.item.FindItemsAndGridsinArea(list4, list5, this.areas, Item2.AreaDistance.all, null, null, null, true, false, true);
			list = Item2.FilterByTypes(this.types, list4);
		}
		IL_00BB:
		foreach (Item2 item in list)
		{
			item.GetComponent<ItemMovement>().CreateHighlight(Color.yellow, null);
		}
	}

	// Token: 0x040006AC RID: 1708
	[SerializeField]
	public string triggerOverrideKey;

	// Token: 0x040006AD RID: 1709
	[SerializeField]
	public string descriptionOverrideKey;

	// Token: 0x040006AE RID: 1710
	public ValueChangerEx.ReplaceWithValue replaceWithValue;

	// Token: 0x040006AF RID: 1711
	public float baseValue;

	// Token: 0x040006B0 RID: 1712
	[SerializeField]
	public float valueToReplace = -999f;

	// Token: 0x040006B1 RID: 1713
	private GameManager gameManager;

	// Token: 0x040006B2 RID: 1714
	[SerializeField]
	public float multiplier = 1f;

	// Token: 0x040006B3 RID: 1715
	private float oldValue = -999f;

	// Token: 0x040006B4 RID: 1716
	[SerializeField]
	public List<GameObject> itemPrefabs;

	// Token: 0x040006B5 RID: 1717
	[SerializeField]
	public List<Item2.Area> areas;

	// Token: 0x040006B6 RID: 1718
	[SerializeField]
	public List<Item2.ItemType> types;

	// Token: 0x040006B7 RID: 1719
	[SerializeField]
	public Item2.AreaDistance areaDistance;

	// Token: 0x040006B8 RID: 1720
	private Player player;

	// Token: 0x040006B9 RID: 1721
	private SaveManager saveManager;

	// Token: 0x040006BA RID: 1722
	private bool found;

	// Token: 0x040006BB RID: 1723
	private bool mana_just_initialized;

	// Token: 0x040006BC RID: 1724
	public ValueChangerEx.ValueType valueType;

	// Token: 0x040006BD RID: 1725
	public object reference;

	// Token: 0x040006BE RID: 1726
	private PocketManager pm;

	// Token: 0x040006BF RID: 1727
	private Item2 item;

	// Token: 0x02000372 RID: 882
	public enum ReplaceWithValue
	{
		// Token: 0x040014AE RID: 5294
		AP,
		// Token: 0x040014AF RID: 5295
		EXP,
		// Token: 0x040014B0 RID: 5296
		Level,
		// Token: 0x040014B1 RID: 5297
		Gold,
		// Token: 0x040014B2 RID: 5298
		Floor,
		// Token: 0x040014B3 RID: 5299
		Loop,
		// Token: 0x040014B4 RID: 5300
		PlayerHP,
		// Token: 0x040014B5 RID: 5301
		PlayerMaxHP,
		// Token: 0x040014B6 RID: 5302
		PlayerBlock,
		// Token: 0x040014B7 RID: 5303
		PlayerBurn,
		// Token: 0x040014B8 RID: 5304
		PlayerFreeze,
		// Token: 0x040014B9 RID: 5305
		PlayerPoison,
		// Token: 0x040014BA RID: 5306
		PlayerHaste,
		// Token: 0x040014BB RID: 5307
		PlayerSlow,
		// Token: 0x040014BC RID: 5308
		PlayerWeak,
		// Token: 0x040014BD RID: 5309
		PlayerRage,
		// Token: 0x040014BE RID: 5310
		PlayerDodge,
		// Token: 0x040014BF RID: 5311
		PlayerRegen,
		// Token: 0x040014C0 RID: 5312
		PlayerSpikes,
		// Token: 0x040014C1 RID: 5313
		TargetHP,
		// Token: 0x040014C2 RID: 5314
		TargetMaxHP,
		// Token: 0x040014C3 RID: 5315
		TargetBlock,
		// Token: 0x040014C4 RID: 5316
		TargetBurn,
		// Token: 0x040014C5 RID: 5317
		TargetFreeze,
		// Token: 0x040014C6 RID: 5318
		TargetPoison,
		// Token: 0x040014C7 RID: 5319
		TargetHaste,
		// Token: 0x040014C8 RID: 5320
		TargetSlow,
		// Token: 0x040014C9 RID: 5321
		TargetWeak,
		// Token: 0x040014CA RID: 5322
		TargetRage,
		// Token: 0x040014CB RID: 5323
		TargetDodge,
		// Token: 0x040014CC RID: 5324
		TargetRegen,
		// Token: 0x040014CD RID: 5325
		TargetSpikes,
		// Token: 0x040014CE RID: 5326
		TargetCharm,
		// Token: 0x040014CF RID: 5327
		TargetSleep,
		// Token: 0x040014D0 RID: 5328
		TargetToughHide,
		// Token: 0x040014D1 RID: 5329
		ManastonePower,
		// Token: 0x040014D2 RID: 5330
		ManastoneMaxPower,
		// Token: 0x040014D3 RID: 5331
		ManastoneConnectedPower,
		// Token: 0x040014D4 RID: 5332
		ManastoneConnectedMaxPower,
		// Token: 0x040014D5 RID: 5333
		numOfEnemies,
		// Token: 0x040014D6 RID: 5334
		numOfEnemiesDefeated,
		// Token: 0x040014D7 RID: 5335
		numOfPetsAlive,
		// Token: 0x040014D8 RID: 5336
		numOfPetsDefeated,
		// Token: 0x040014D9 RID: 5337
		numOfPets,
		// Token: 0x040014DA RID: 5338
		enemyTargetPosition,
		// Token: 0x040014DB RID: 5339
		numOfTurns,
		// Token: 0x040014DC RID: 5340
		numOfItems,
		// Token: 0x040014DD RID: 5341
		numOfItemTypes,
		// Token: 0x040014DE RID: 5342
		carvingsPlayed,
		// Token: 0x040014DF RID: 5343
		carvingsInDiscard,
		// Token: 0x040014E0 RID: 5344
		getSizeOfItem,
		// Token: 0x040014E1 RID: 5345
		numOfPockets,
		// Token: 0x040014E2 RID: 5346
		numOfSpacesInThisPocket,
		// Token: 0x040014E3 RID: 5347
		numOfSpacesInAllOtherPockets,
		// Token: 0x040014E4 RID: 5348
		numOfPocketsInArea,
		// Token: 0x040014E5 RID: 5349
		numOfItemsInThisPouch,
		// Token: 0x040014E6 RID: 5350
		numOfCursesOnThisItem
	}

	// Token: 0x02000373 RID: 883
	public enum ValueType
	{
		// Token: 0x040014E8 RID: 5352
		Cost,
		// Token: 0x040014E9 RID: 5353
		CostMod,
		// Token: 0x040014EA RID: 5354
		UseLimit,
		// Token: 0x040014EB RID: 5355
		EffectValue,
		// Token: 0x040014EC RID: 5356
		ItemStatusEffectValue,
		// Token: 0x040014ED RID: 5357
		CreateNumOfItems,
		// Token: 0x040014EE RID: 5358
		MovementX,
		// Token: 0x040014EF RID: 5359
		MovementY,
		// Token: 0x040014F0 RID: 5360
		MovementRotation,
		// Token: 0x040014F1 RID: 5361
		Mana,
		// Token: 0x040014F2 RID: 5362
		MaxMana,
		// Token: 0x040014F3 RID: 5363
		ValueChangerMultiplier,
		// Token: 0x040014F4 RID: 5364
		ValueChangerBase
	}
}
