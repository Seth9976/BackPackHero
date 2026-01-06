using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

// Token: 0x020000C8 RID: 200
public class EventButton : MonoBehaviour
{
	// Token: 0x060005C1 RID: 1473 RVA: 0x00038618 File Offset: 0x00036818
	public void SetItemType(Item2.ItemType itemType)
	{
		this.requiredItemType = new List<Item2.ItemType> { itemType };
		this.possibleOutcomes[0].eventButtonActions[0].itemTypes = new List<Item2.ItemType> { itemType };
		TextMeshProUGUI componentInChildren = base.GetComponentInChildren<TextMeshProUGUI>();
		if (componentInChildren)
		{
			string textByKey = LangaugeManager.main.GetTextByKey(this.overrideButtonTextKey);
			componentInChildren.text = textByKey.Replace("/y", LangaugeManager.main.GetTextByKey(itemType.ToString()));
		}
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x000386A3 File Offset: 0x000368A3
	private void Start()
	{
		this.gameManager = GameManager.main;
		this.player = Player.main;
		this.randomEventMaster = base.GetComponentInParent<RandomEventMaster>();
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x000386C7 File Offset: 0x000368C7
	private void Update()
	{
		if (this.sacrificeAccepted && !this.played)
		{
			base.StartCoroutine(this.PerfromEffect());
		}
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x000386E8 File Offset: 0x000368E8
	public void PayCosts()
	{
		if (this.requiredGold > 0 && this.gameManager.GetCurrentGold() < this.requiredGold)
		{
			this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm27"));
			return;
		}
		if (this.requirement == EventButton.Requirements.none || this.requirement == EventButton.Requirements.goldCost)
		{
			base.StartCoroutine(this.PerfromEffect());
			return;
		}
		if (this.requirement == EventButton.Requirements.itemSacrifice)
		{
			this.randomEventMaster.gameObject.SetActive(false);
			this.gameManager.ChooseMatchingItem(this, true);
			return;
		}
		if (this.requirement == EventButton.Requirements.itemSelect || this.requirement == EventButton.Requirements.itemSelectNonForged)
		{
			this.randomEventMaster.gameObject.SetActive(false);
			this.gameManager.ChooseMatchingItem(this, false);
			return;
		}
		if (this.requirement == EventButton.Requirements.carvingSelectNonForged)
		{
			Object.FindObjectOfType<Tote>().SelectCarvingForFromEvent(this, false);
			return;
		}
		if (this.requirement == EventButton.Requirements.pumpkinKing)
		{
			foreach (ItemPouch itemPouch in Object.FindObjectsOfType<ItemPouch>())
			{
				if (Item2.GetDisplayName(itemPouch.gameObject.name).ToLower().Contains("candy pail") && itemPouch.itemsInside.Count >= 7)
				{
					base.StartCoroutine(this.PerfromEffect());
					return;
				}
			}
			this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm27Halloween"));
			return;
		}
		if (this.requirement == EventButton.Requirements.specificItemSacrifice)
		{
			this.randomEventMaster.gameObject.SetActive(false);
			this.gameManager.ChooseMatchingItem(this, true);
		}
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x0003885A File Offset: 0x00036A5A
	public void ButtonPress()
	{
		SoundManager.main.PlaySFX("menuBlip");
		this.PayCosts();
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x00038871 File Offset: 0x00036A71
	private IEnumerator PerfromEffect()
	{
		if (this.randomEventMaster && this.randomEventMaster.finished)
		{
			yield break;
		}
		if (this.possibleOutcomes == null)
		{
			yield break;
		}
		if (this.requiredGold > 0)
		{
			this.gameManager.ChangeGold(this.requiredGold * -1);
		}
		List<EventButton.PossibleOutcome> possibleOutcomesThatAreUnlocked = new List<EventButton.PossibleOutcome>();
		for (int i = 0; i < this.possibleOutcomes.Length; i++)
		{
			bool flag = true;
			if (!MetaProgressSaveManager.ConditionsMet(this.possibleOutcomes[i].conditions))
			{
				flag = false;
			}
			if (this.onlyGiveValidItems && this.possibleOutcomes[i].eventButtonActions[0].action == EventButton.EventButtonAction.Action.getSpecificItems)
			{
				flag = true;
				foreach (GameObject gameObject in this.possibleOutcomes[i].eventButtonActions[0].items)
				{
					if (gameObject)
					{
						Item2 component = gameObject.GetComponent<Item2>();
						if (!GameManager.main.ItemValidToSpawn(component, false))
						{
							flag = false;
							break;
						}
					}
				}
			}
			if (flag)
			{
				possibleOutcomesThatAreUnlocked.Add(this.possibleOutcomes[i]);
			}
		}
		this.played = true;
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		int num = 0;
		for (int k = 0; k < possibleOutcomesThatAreUnlocked.Count; k++)
		{
			num += Mathf.Max(1, possibleOutcomesThatAreUnlocked[k].probability);
		}
		int l = Random.Range(0, num);
		int num2 = -1;
		while (l >= 0)
		{
			num2++;
			l -= possibleOutcomesThatAreUnlocked[num2].probability;
		}
		this.chosenOutCome = possibleOutcomesThatAreUnlocked[num2];
		this.chosenOutComeNumber = num2 + 1;
		if (this.randomEventMaster)
		{
			this.sacrificeAccepted = false;
			string text = this.overrideButtonTextKey + "o" + this.chosenOutComeNumber.ToString();
			if (LangaugeManager.main.KeyExists(text + this.player.characterName.ToString()))
			{
				this.randomEventMaster.NewText(this, LangaugeManager.main.GetTextByKey(text + this.player.characterName.ToString()));
			}
			else if (LangaugeManager.main.KeyExists(text))
			{
				this.randomEventMaster.NewText(this, LangaugeManager.main.GetTextByKey(text));
			}
			else if (LangaugeManager.main.KeyExists(this.overrideButtonTextKey + "os"))
			{
				this.randomEventMaster.NewText(this, LangaugeManager.main.GetTextByKey(this.overrideButtonTextKey + "os"));
			}
			else if (LangaugeManager.main.KeyExists(this.randomEventMaster.eventTextKey + "bsos"))
			{
				this.randomEventMaster.NewText(this, LangaugeManager.main.GetTextByKey(this.randomEventMaster.eventTextKey + "bsos"));
			}
			else
			{
				this.randomEventMaster.NewText(this, LangaugeManager.main.GetTextByKey("evso"));
			}
			this.randomEventMaster.RemoveButtons();
			this.randomEventMaster.PlaySpecials();
		}
		else
		{
			this.FullEffect(null);
		}
		yield break;
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x00038880 File Offset: 0x00036A80
	public void FullEffect(DungeonEvent dungeonEvent)
	{
		this.chosenOutCome.npcConversationText = new List<string>();
		if (this.randomEventMaster)
		{
			for (int i = 0; i < 10; i++)
			{
				string text = string.Concat(new string[]
				{
					this.randomEventMaster.eventTextKey,
					"b",
					(base.transform.GetSiblingIndex() + 1).ToString(),
					"o",
					this.chosenOutComeNumber.ToString(),
					"t",
					i.ToString()
				});
				if (this.overrideButtonTextKey.Length > 1)
				{
					text = string.Concat(new string[]
					{
						this.overrideButtonTextKey,
						"o",
						this.chosenOutComeNumber.ToString(),
						"t",
						i.ToString()
					});
				}
				if (LangaugeManager.main.KeyExists(text + this.player.characterName.ToString()))
				{
					this.chosenOutCome.npcConversationText.Add(LangaugeManager.main.GetTextByKey(text + this.player.characterName.ToString()));
				}
				else if (LangaugeManager.main.KeyExists(text))
				{
					this.chosenOutCome.npcConversationText.Add(LangaugeManager.main.GetTextByKey(text));
				}
				else if (LangaugeManager.main.KeyExists(this.randomEventMaster.eventTextKey + "bsosts"))
				{
					this.chosenOutCome.npcConversationText.Add(LangaugeManager.main.GetTextByKey(this.randomEventMaster.eventTextKey + "bsosts"));
				}
			}
			if (this.randomEventMaster.npc && this.chosenOutCome.npcConversationText.Count >= 1)
			{
				this.randomEventMaster.npc.SetText(this.chosenOutCome.npcConversationText);
			}
		}
		if (dungeonEvent)
		{
			dungeonEvent.FinishEvent();
		}
		foreach (EventButton.EventButtonAction eventButtonAction in this.chosenOutCome.eventButtonActions)
		{
			if (eventButtonAction.action == EventButton.EventButtonAction.Action.gainMaxHP)
			{
				this.gameManager.player.stats.SetMaxHP(this.gameManager.player.stats.maxHealth + eventButtonAction.value);
				this.gameManager.player.stats.ChangeHealth(eventButtonAction.value, null, false);
			}
			else
			{
				if (eventButtonAction.action == EventButton.EventButtonAction.Action.getItemOfTypeAndRarity)
				{
					List<Item2.ItemType> list = new List<Item2.ItemType>
					{
						Item2.ItemType.Curse,
						Item2.ItemType.Relic
					};
					using (List<GameObject>.Enumerator enumerator2 = ItemSpawner.InstantiateItemsFree(ItemSpawner.GetItems(eventButtonAction.value, eventButtonAction.itemRarities, eventButtonAction.itemTypes, list, false, eventButtonAction.allowNonStandardItems, false), true, default(Vector2)).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							GameObject gameObject = enumerator2.Current;
							ItemMovement component = gameObject.GetComponent<ItemMovement>();
							component.StartCoroutine(component.Move(new Vector3(0f, 2.5f, 0f), 0));
						}
						goto IL_0FC7;
					}
				}
				if (eventButtonAction.action == EventButton.EventButtonAction.Action.getCurse)
				{
					ItemSpawner.InstantiateItemsFree(ItemSpawner.GetItems(Mathf.RoundToInt((float)eventButtonAction.value), new List<Item2.ItemType> { Item2.ItemType.Curse }, false, true), true, default(Vector2));
					this.gameManager.MoveAllItems();
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.getSpecificItems)
				{
					int num = eventButtonAction.items.Length;
					List<GameObject> list2 = this.gameManager.ItemsValidToSpawn(eventButtonAction.items, false);
					while (num > 0 && list2.Count > 0)
					{
						GameObject gameObject2 = Object.Instantiate<GameObject>(Item2.ChooseRandomAndRemoveFromList(list2), Vector3.zero, Quaternion.identity, this.gameManager.itemsParent);
						num--;
						Carving component2 = gameObject2.GetComponent<Carving>();
						if (component2)
						{
							component2.AddToDeckAfterDelay();
						}
					}
					this.gameManager.MoveAllItems();
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.SpawnItemNewIfPossible)
				{
					List<GameObject> list3 = this.gameManager.ItemsValidToSpawn(DebugItemManager.main.items, false);
					List<GameObject> list4 = DebugItemManager.main.items.ToList<GameObject>().Except(list3).ToList<GameObject>();
					while (list4.Count > 0)
					{
						GameObject gameObject3 = Item2.ChooseRandomAndRemoveFromList(list4);
						Item2 component3 = gameObject3.GetComponent<Item2>();
						component3.isStandardOnlyAfterUnlock = false;
						if (this.gameManager.ItemValidToSpawn(component3, false))
						{
							Object.Instantiate<GameObject>(gameObject3, Vector3.zero, Quaternion.identity, this.gameManager.itemsParent);
							component3.isStandardOnlyAfterUnlock = true;
							return;
						}
						component3.isStandardOnlyAfterUnlock = true;
					}
					Item2.ItemType itemType = Item2.ItemType.Weapon;
					if (eventButtonAction.itemTypes.Count > 0)
					{
						itemType = eventButtonAction.itemTypes[0];
					}
					List<Item2> list5 = Item2.GetItemsFromGameObjects(list3);
					list5 = Item2.GetItemOfType(itemType, list5);
					Object.Instantiate<GameObject>(Item2.ChooseRandomFromList(Item2.GetGameObjectsFromItems(list5), true), Vector3.zero, Quaternion.identity, this.gameManager.itemsParent).GetComponent<ItemMovement>();
					this.gameManager.MoveAllItems();
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.getRandomItems)
				{
					ItemMovement component4 = Object.Instantiate<GameObject>(Item2.ChooseRandomFromList(this.gameManager.ItemsValidToSpawn(eventButtonAction.items, false), true), Vector3.zero, Quaternion.identity, this.gameManager.itemsParent).GetComponent<ItemMovement>();
					component4.StartCoroutine(component4.Move(new Vector3(0f, 2.5f, 0f), 0));
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.heal)
				{
					this.gameManager.player.stats.ChangeHealth(eventButtonAction.value, null, false);
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.getGold)
				{
					this.gameManager.ChangeGold(eventButtonAction.value);
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.spawnEnemyEncounter)
				{
					DungeonLevel.EnemyEncounter2 enemyEncounter = Object.FindObjectOfType<DungeonSpawner>().GetEnemyEncounter(false);
					Player main = Player.main;
					for (int j = 0; j < enemyEncounter.enemiesInGroup.Count; j++)
					{
						GameObject gameObject4 = Object.Instantiate<GameObject>(enemyEncounter.enemiesInGroup[j], Vector3.zero, Quaternion.identity, main.transform.parent);
						gameObject4.transform.localPosition = new Vector3(this.gameManager.spawnPosition.position.x - (float)(j * 2) + 1f - gameObject4.GetComponent<BoxCollider2D>().size.x / 2f, -4.8f + gameObject4.GetComponent<BoxCollider2D>().size.y / 2f, 1f);
					}
					GameFlowManager.main.StartCombat();
					DungeonPlayer dungeonPlayer = Object.FindObjectOfType<DungeonPlayer>();
					dungeonPlayer.StartCoroutine(dungeonPlayer.WalkIntoEvent());
					Object.Destroy(this.randomEventMaster.npc.gameObject);
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.StartSpecialFight)
				{
					Player main2 = Player.main;
					int num2 = 0;
					GameObject[] items = eventButtonAction.items;
					for (int k = 0; k < items.Length; k++)
					{
						GameObject gameObject5 = Object.Instantiate<GameObject>(items[k], this.randomEventMaster.npc.transform.position, Quaternion.identity, main2.transform.parent);
						gameObject5.transform.localPosition = new Vector3(this.gameManager.spawnPosition.position.x - (float)(num2 * 2) + 1f - gameObject5.GetComponent<BoxCollider2D>().size.x / 2f, -4.8f + gameObject5.GetComponent<BoxCollider2D>().size.y / 2f, 1f);
						num2++;
					}
					GameFlowManager.main.StartCombat();
					DungeonPlayer dungeonPlayer2 = Object.FindObjectOfType<DungeonPlayer>();
					dungeonPlayer2.StartCoroutine(dungeonPlayer2.WalkIntoEvent());
					Object.Destroy(this.randomEventMaster.npc.gameObject);
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.leaveLockedDoor)
				{
					dungeonEvent.RemoveEventProperty(DungeonEvent.EventProperty.Type.finished);
					DungeonPlayer dungeonPlayer3 = Object.FindObjectOfType<DungeonPlayer>();
					dungeonPlayer3.StopAllCoroutines();
					dungeonPlayer3.StartCoroutine(dungeonPlayer3.FindWay());
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.openLockedDoor)
				{
					if (dungeonEvent)
					{
						Object.Destroy(dungeonEvent.gameObject);
					}
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.removeAllHazards)
				{
					CurseManager.Instance.RemoveAllCurses();
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.newGame)
				{
					GameManager.main.EndDemo();
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.nextLevel)
				{
					this.gameManager.floor--;
					this.gameManager.StartCoroutine(this.gameManager.NextLevel(-1));
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.allowReopen)
				{
					this.gameManager.StartCoroutine(this.ShowButtons());
					EventButton[] componentsInChildren = base.GetComponentsInChildren<EventButton>();
					for (int k = 0; k < componentsInChildren.Length; k++)
					{
						componentsInChildren[k].played = false;
					}
					if (dungeonEvent)
					{
						dungeonEvent.RemoveEventProperty(DungeonEvent.EventProperty.Type.finished);
					}
					this.randomEventMaster.finished = true;
					this.randomEventMaster.chosenButton = null;
					this.randomEventMaster.npc.AllowReOpen();
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.getSameTypeBetterRarity)
				{
					Item2.Rarity rarity;
					if (this.sacrificedItem.rarity == Item2.Rarity.Common)
					{
						rarity = Item2.Rarity.Uncommon;
					}
					else if (this.sacrificedItem.rarity == Item2.Rarity.Uncommon)
					{
						rarity = Item2.Rarity.Rare;
					}
					else
					{
						rarity = Item2.Rarity.Legendary;
					}
					bool flag = false;
					if (this.sacrificedItem.itemType.Contains(Item2.ItemType.Curse) || this.sacrificedItem.itemType.Contains(Item2.ItemType.Relic))
					{
						flag = true;
					}
					List<Item2> list6 = new List<Item2>();
					if (this.specialItemToSpawn && this.sacrificedItem.itemType.Contains(Item2.ItemType.Relic) && Random.Range(0, 2) == 0)
					{
						Item2 component5 = this.specialItemToSpawn.GetComponent<Item2>();
						if (component5)
						{
							list6 = new List<Item2> { component5 };
						}
					}
					if (list6.Count == 0)
					{
						list6 = ItemSpawner.GetItems(1, new List<Item2.Rarity> { rarity }, this.sacrificedItem.itemType, new List<Item2.ItemType>(), false, flag, false);
					}
					if (list6.Count > 0)
					{
						ItemSpawner.InstantiateItemsFree(list6, true, default(Vector2));
					}
					else
					{
						list6 = ItemSpawner.GetItems(1, new List<Item2.Rarity> { this.sacrificedItem.rarity }, this.sacrificedItem.itemType, new List<Item2.ItemType>(), false, flag, false);
						if (list6.Count > 0)
						{
							ItemSpawner.InstantiateItemsFree(list6, true, default(Vector2));
						}
						else
						{
							Item2 item2ByName = DebugItemManager.main.GetItem2ByName(Item2.GetDisplayName(this.sacrificedItem.name));
							if (item2ByName)
							{
								ItemSpawner.InstantiateItemsFree(new List<Item2> { item2ByName }, true, default(Vector2));
							}
						}
					}
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.getItemOfSameSize)
				{
					int spacesNeeded = this.sacrificedItem.GetComponent<ItemMovement>().GetSpacesNeeded();
					this.gameManager.SpawnItemOfSize(spacesNeeded);
					this.gameManager.MoveAllItems();
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.increaseCost)
				{
					dungeonEvent.AddEventProperty(DungeonEvent.EventProperty.Type.increaseCost, eventButtonAction.value);
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.upgradeItem)
				{
					Item2.Modifier modifier = eventButtonAction.modifier[0];
					if (modifier.typesToModify.Count == 0)
					{
						modifier.typesToModify = new List<Item2.ItemType> { Item2.ItemType.Any };
					}
					if (modifier.areasToModify.Count == 0)
					{
						modifier.areasToModify = new List<Item2.Area> { Item2.Area.self };
					}
					if (modifier.effects[0].type == Item2.Effect.Type.ItemStatusEffect)
					{
						MetaProgressSaveManager.main.AddRunEvent(MetaProgressSaveManager.LastRun.RunEvents.forgedAnItem);
						Item2.ApplyItemStatusEffect(this.sacrificedItem, modifier.effects[0], modifier.origin);
					}
					else
					{
						this.sacrificedItem.appliedModifiers.Add(modifier);
					}
					SpriteRenderer component6 = this.sacrificedItem.GetComponent<SpriteRenderer>();
					if (component6)
					{
						EffectParticleSystem.Instance.CopySprite(component6, EffectParticleSystem.ParticleType.forge);
						SoundManager.main.PlaySFX("forge");
					}
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.addNewModifier)
				{
					this.sacrificedItem.modifiers.Add(eventButtonAction.modifier[0].Clone());
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.getDifferentItemSameRarity)
				{
					List<Item2> list7 = new List<Item2>();
					if (this.specialItemToSpawn && this.sacrificedItem.itemType.Contains(Item2.ItemType.Relic) && Random.Range(0, 2) == 0)
					{
						Item2 component7 = this.specialItemToSpawn.GetComponent<Item2>();
						if (component7)
						{
							list7 = new List<Item2> { component7 };
						}
					}
					if (list7.Count == 0 && this.sacrificedItem.itemType.Contains(Item2.ItemType.Relic))
					{
						list7 = ItemSpawner.GetItems(1, new List<Item2.Rarity> { this.sacrificedItem.rarity }, new List<Item2.ItemType> { Item2.ItemType.Relic }, false, true);
					}
					if (list7.Count == 0)
					{
						list7 = ItemSpawner.GetItems(1, new List<Item2.Rarity> { this.sacrificedItem.rarity }, true, false);
					}
					ItemSpawner.InstantiateItemsFree(list7, true, default(Vector2));
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.upgradeMana)
				{
					ManaStone component8 = this.sacrificedItem.GetComponent<ManaStone>();
					if (component8)
					{
						component8.maxPower += eventButtonAction.value;
						component8.currentPower = component8.maxPower;
					}
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.DestroySelectedItem)
				{
					ItemMovement component9 = this.sacrificedItem.GetComponent<ItemMovement>();
					if (component9)
					{
						component9.DelayDestroy();
					}
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.AddEventNextFloor)
				{
					DungeonSpawner dungeonSpawner = Object.FindObjectOfType<DungeonSpawner>();
					if (eventButtonAction.dungeonEventSpawn[0].num == Vector2.zero)
					{
						eventButtonAction.dungeonEventSpawn[0].num = Vector2.one;
					}
					dungeonSpawner.AddDungeonPropertyEvent(eventButtonAction.dungeonEventSpawn[0], eventButtonAction.value);
				}
				else if (eventButtonAction.action == EventButton.EventButtonAction.Action.SpecialEffectViaScript)
				{
					DungeonEventSpecial component10 = this.randomEventMaster.GetComponent<DungeonEventSpecial>();
					if (component10)
					{
						component10.SpecialEffect();
					}
					else
					{
						Debug.Log("You tried to call a special effect via a script but there isn't one");
					}
				}
				else if (eventButtonAction.action != EventButton.EventButtonAction.Action.AddForgeSlots)
				{
					if (eventButtonAction.action == EventButton.EventButtonAction.Action.healAllPets)
					{
						PetItem2.HealAllPets();
					}
					else
					{
						if (eventButtonAction.action == EventButton.EventButtonAction.Action.copyARandomItem)
						{
							using (List<Item2>.Enumerator enumerator3 = Item2.GetAllItemsInGrid().GetEnumerator())
							{
								if (enumerator3.MoveNext())
								{
									Item2 item = enumerator3.Current;
									if (!item.itemType.Contains(Item2.ItemType.Relic))
									{
										Item2.CopyItem(item.gameObject, Vector3.zero);
									}
								}
								goto IL_0FC7;
							}
						}
						if (eventButtonAction.action == EventButton.EventButtonAction.Action.SetMetaProgressMarkerBoolean)
						{
							MetaProgressSaveManager.main.AddMetaProgressMarker(eventButtonAction.metaProgressMarker);
						}
						else if (eventButtonAction.action == EventButton.EventButtonAction.Action.SetMetaProgressMarkerValue)
						{
							MetaProgressSaveManager.main.AddMetaProgressMarker(eventButtonAction.metaProgressMarker, eventButtonAction.value);
						}
						else if (eventButtonAction.action == EventButton.EventButtonAction.Action.DestroyAllCurrentItems)
						{
							foreach (Item2 item2 in Item2.GetAllItems())
							{
								if (!Item2.ShareItemTypes(item2.itemType, new List<Item2.ItemType>
								{
									Item2.ItemType.Core,
									Item2.ItemType.Pet
								}))
								{
									item2.GetComponent<ItemMovement>().DelayDestroy();
								}
							}
							if (Tote.main)
							{
								Tote.main.RemoveAllCarvings();
							}
						}
						else
						{
							EventButton.EventButtonAction.Action action = eventButtonAction.action;
						}
					}
				}
			}
			IL_0FC7:
			GameFlowManager.main.CheckConstants();
			ItemMovementManager.main.CheckAllForValidPlacementPublic();
			ItemMovementManager.main.CheckForMovementPublic();
		}
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x000398F4 File Offset: 0x00037AF4
	public int GetModifierValue(int modNumber)
	{
		foreach (EventButton.PossibleOutcome possibleOutcome in this.possibleOutcomes)
		{
			if (possibleOutcome.eventButtonActions.Count > modNumber && possibleOutcome.eventButtonActions[0].modifier.Count > modNumber && possibleOutcome.eventButtonActions[0].modifier[modNumber].effects.Count > 0)
			{
				return Mathf.RoundToInt(possibleOutcome.eventButtonActions[0].modifier[modNumber].effects[0].value);
			}
		}
		return -1;
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x00039999 File Offset: 0x00037B99
	private IEnumerator ShowButtons()
	{
		yield return new WaitForSeconds(0.2f);
		this.randomEventMaster.doneButton.SetActive(false);
		this.randomEventMaster.buttons.gameObject.SetActive(true);
		yield break;
	}

	// Token: 0x04000479 RID: 1145
	[SerializeField]
	private GameObject specialItemToSpawn;

	// Token: 0x0400047A RID: 1146
	[SerializeField]
	private bool onlyGiveValidItems;

	// Token: 0x0400047B RID: 1147
	public bool alwaysOn;

	// Token: 0x0400047C RID: 1148
	[SerializeField]
	public string overrideButtonTextKey;

	// Token: 0x0400047D RID: 1149
	[SerializeField]
	public List<Character.CharacterName> validForCharacters;

	// Token: 0x0400047E RID: 1150
	[SerializeField]
	public EventButton.Requirements requirement;

	// Token: 0x0400047F RID: 1151
	[SerializeField]
	public int requiredGold;

	// Token: 0x04000480 RID: 1152
	[NonSerialized]
	public int startingGold = -1;

	// Token: 0x04000481 RID: 1153
	[SerializeField]
	public List<Item2.ItemType> requiredItemType;

	// Token: 0x04000482 RID: 1154
	[SerializeField]
	public List<Item2.Rarity> requiredRarities;

	// Token: 0x04000483 RID: 1155
	[HideInInspector]
	public bool sacrificeAccepted;

	// Token: 0x04000484 RID: 1156
	[HideInInspector]
	public Item2 sacrificedItem;

	// Token: 0x04000485 RID: 1157
	[SerializeField]
	public GameObject requiredItem;

	// Token: 0x04000486 RID: 1158
	[SerializeField]
	public bool skippable = true;

	// Token: 0x04000487 RID: 1159
	[SerializeField]
	private string buttonText;

	// Token: 0x04000488 RID: 1160
	[SerializeField]
	private EventButton.PossibleOutcome[] possibleOutcomes;

	// Token: 0x04000489 RID: 1161
	private GameManager gameManager;

	// Token: 0x0400048A RID: 1162
	private Player player;

	// Token: 0x0400048B RID: 1163
	public RandomEventMaster randomEventMaster;

	// Token: 0x0400048C RID: 1164
	[HideInInspector]
	public EventButton.PossibleOutcome chosenOutCome;

	// Token: 0x0400048D RID: 1165
	[HideInInspector]
	public bool played;

	// Token: 0x0400048E RID: 1166
	private DungeonEvent dungeonEvent;

	// Token: 0x0400048F RID: 1167
	private int chosenOutComeNumber;

	// Token: 0x02000301 RID: 769
	public enum Requirements
	{
		// Token: 0x040011FC RID: 4604
		none,
		// Token: 0x040011FD RID: 4605
		itemSacrifice,
		// Token: 0x040011FE RID: 4606
		goldCost,
		// Token: 0x040011FF RID: 4607
		itemSelect,
		// Token: 0x04001200 RID: 4608
		itemSelectNonForged,
		// Token: 0x04001201 RID: 4609
		carvingSelectNonForged,
		// Token: 0x04001202 RID: 4610
		pumpkinKing,
		// Token: 0x04001203 RID: 4611
		specificItemSacrifice
	}

	// Token: 0x02000302 RID: 770
	[Serializable]
	public class EventButtonAction
	{
		// Token: 0x04001204 RID: 4612
		public EventButton.EventButtonAction.Action action;

		// Token: 0x04001205 RID: 4613
		public int value;

		// Token: 0x04001206 RID: 4614
		public List<Item2.ItemType> itemTypes;

		// Token: 0x04001207 RID: 4615
		public List<Item2.Rarity> itemRarities;

		// Token: 0x04001208 RID: 4616
		[SerializeField]
		public List<Item2.Modifier> modifier;

		// Token: 0x04001209 RID: 4617
		[SerializeField]
		public List<DungeonSpawner.DungeonEventSpawn> dungeonEventSpawn;

		// Token: 0x0400120A RID: 4618
		public GameObject[] items;

		// Token: 0x0400120B RID: 4619
		public bool allowNonStandardItems;

		// Token: 0x0400120C RID: 4620
		[SerializeField]
		public MetaProgressSaveManager.MetaProgressMarker metaProgressMarker;

		// Token: 0x0200049B RID: 1179
		public enum Action
		{
			// Token: 0x04001AE3 RID: 6883
			gainMaxHP,
			// Token: 0x04001AE4 RID: 6884
			getSpecificItems,
			// Token: 0x04001AE5 RID: 6885
			getCurse,
			// Token: 0x04001AE6 RID: 6886
			heal,
			// Token: 0x04001AE7 RID: 6887
			getGold,
			// Token: 0x04001AE8 RID: 6888
			spawnEnemyEncounter,
			// Token: 0x04001AE9 RID: 6889
			leaveLockedDoor,
			// Token: 0x04001AEA RID: 6890
			openLockedDoor,
			// Token: 0x04001AEB RID: 6891
			removeAllHazards,
			// Token: 0x04001AEC RID: 6892
			newGame,
			// Token: 0x04001AED RID: 6893
			nextLevel,
			// Token: 0x04001AEE RID: 6894
			getItemOfTypeAndRarity,
			// Token: 0x04001AEF RID: 6895
			StartSpecialFight,
			// Token: 0x04001AF0 RID: 6896
			getRandomItems,
			// Token: 0x04001AF1 RID: 6897
			repeatWithFlavorText,
			// Token: 0x04001AF2 RID: 6898
			allowReopen,
			// Token: 0x04001AF3 RID: 6899
			getSameTypeBetterRarity,
			// Token: 0x04001AF4 RID: 6900
			getDifferentItemSameRarity,
			// Token: 0x04001AF5 RID: 6901
			upgradeItem,
			// Token: 0x04001AF6 RID: 6902
			winRun,
			// Token: 0x04001AF7 RID: 6903
			addNewModifier,
			// Token: 0x04001AF8 RID: 6904
			getItemOfSameSize,
			// Token: 0x04001AF9 RID: 6905
			increaseCost,
			// Token: 0x04001AFA RID: 6906
			upgradeMana,
			// Token: 0x04001AFB RID: 6907
			DestroySelectedItem,
			// Token: 0x04001AFC RID: 6908
			AddEventNextFloor,
			// Token: 0x04001AFD RID: 6909
			SpecialEffectViaScript,
			// Token: 0x04001AFE RID: 6910
			AddForgeSlots,
			// Token: 0x04001AFF RID: 6911
			healAllPets,
			// Token: 0x04001B00 RID: 6912
			SpawnItemNewIfPossible,
			// Token: 0x04001B01 RID: 6913
			copyARandomItem,
			// Token: 0x04001B02 RID: 6914
			SetMetaProgressMarkerBoolean,
			// Token: 0x04001B03 RID: 6915
			SetMetaProgressMarkerValue,
			// Token: 0x04001B04 RID: 6916
			DestroyAllCurrentItems
		}
	}

	// Token: 0x02000303 RID: 771
	[Serializable]
	public class PossibleOutcome
	{
		// Token: 0x0400120D RID: 4621
		public List<MetaProgressSaveManager.MetaProgressCondition> conditions = new List<MetaProgressSaveManager.MetaProgressCondition>();

		// Token: 0x0400120E RID: 4622
		public List<EventButton.EventButtonAction> eventButtonActions;

		// Token: 0x0400120F RID: 4623
		public int probability = 100;

		// Token: 0x04001210 RID: 4624
		public string flavorText;

		// Token: 0x04001211 RID: 4625
		public List<string> npcConversationText;
	}
}
