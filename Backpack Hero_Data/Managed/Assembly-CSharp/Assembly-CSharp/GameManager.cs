using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000123 RID: 291
public class GameManager : MonoBehaviour
{
	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000A34 RID: 2612 RVA: 0x00067736 File Offset: 0x00065936
	private ES3Settings settings
	{
		get
		{
			if (this._settings == null)
			{
				this._settings = new ES3Settings(null, null);
			}
			return this._settings;
		}
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x00067753 File Offset: 0x00065953
	public void ClearEvent()
	{
		this.viewingEventObject = new List<GameObject>();
		this.viewingEvent = false;
		this.viewingEventThroughObject = false;
		this.SetInterfaceButtons(true);
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x00067775 File Offset: 0x00065975
	public void ViewEvent(GameObject item)
	{
		this.viewingEventObject.Insert(0, item);
		this.viewingEventThroughObject = true;
		this.SetInterfaceButtons(false);
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x00067794 File Offset: 0x00065994
	public void SetInterfaceButtons(bool myChange)
	{
		List<CanvasGroup> list = new List<CanvasGroup>();
		if (this.viewingEventObject.Count > 0 && this.viewingEventObject[0])
		{
			list.AddRange(this.viewingEventObject[0].GetComponentsInParent<CanvasGroup>());
		}
		foreach (CanvasGroup canvasGroup in Object.FindObjectsOfType<CanvasGroup>())
		{
			if (list.Contains(canvasGroup))
			{
				canvasGroup.interactable = true;
			}
			else
			{
				canvasGroup.interactable = myChange;
			}
		}
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x00067810 File Offset: 0x00065A10
	public void SetZones(List<DungeonLevel> dungeonLevels)
	{
		this.zonesPlayList.Clear();
		foreach (DungeonLevel dungeonLevel in dungeonLevels)
		{
			GameManager.ZonePlaylist zonePlaylist = new GameManager.ZonePlaylist();
			zonePlaylist.zones = new List<DungeonLevel.Zone> { dungeonLevel.zone };
			this.zonesPlayList.Add(zonePlaylist);
		}
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x0006788C File Offset: 0x00065A8C
	private void Awake()
	{
		GameManager.main = this;
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x00067894 File Offset: 0x00065A94
	private void OnDestroy()
	{
		GameManager.main = null;
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x0006789C File Offset: 0x00065A9C
	public int GetDoorsAllowed()
	{
		if (this.zoneNumber >= this.zonesPlayList.Count - 1)
		{
			return 0;
		}
		return this.zonesPlayList[this.zoneNumber + 1].zones.Count;
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x000678D4 File Offset: 0x00065AD4
	private void Start()
	{
		if (!Singleton.Instance.storyMode)
		{
			this.quitToMenuButton.SetActive(true);
			this.retryRunButton.SetActive(true);
			this.quitToOrderiaButton.SetActive(false);
		}
		Singleton.Instance.speedRunTime = 0f;
		this.mainGridParent = GameObject.FindGameObjectWithTag("GridParent").transform;
		this.runTypeManager = Object.FindObjectOfType<RunTypeManager>();
		this.runTypeText = GameObject.FindGameObjectWithTag("RunTypeText").GetComponentInChildren<TextMeshProUGUI>();
		this.ChangeGold(0);
		if (Singleton.Instance.character == Character.CharacterName.Tote)
		{
			this.carvingSpacerForTote.SetActive(true);
		}
		else
		{
			this.carvingSpacerForTote.SetActive(false);
		}
		this.rewardItems = new List<GameObject>();
		this.cR8Manager = base.GetComponent<CR8Manager>();
		this.saveManager = Object.FindObjectOfType<SaveManager>();
		this.tutorialManager = Object.FindObjectOfType<TutorialManager>();
		this.player = Player.main;
		this.playerAnimator = this.playerTransform.GetComponent<Animator>();
		this.analyticsManager = base.GetComponent<AnalyticsManager>();
		this.promptText = this.promptTrans.GetComponentInChildren<TextMeshProUGUI>();
		this.gameFlowManager = GameFlowManager.main;
		this.inventoryStartPosition = this.inventoryTransform.position;
		this.inventoryPhase = GameManager.InventoryPhase.open;
		this.BuildItemLists();
		this.player.ChoseCharacter();
		this.SetLevel(this.zonesPlayList[0].zones[0]);
		Object.FindObjectOfType<BackgroundController>().UpdateAllSprites();
		this.SetBrightness();
		Object.FindObjectOfType<OptionsSaveManager>().ApplySettings();
		this.ChangeCurse(0);
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x00067A5C File Offset: 0x00065C5C
	public void AnalyzeEnemies()
	{
		if (this.analyticsManager)
		{
			this.currentEnemyEncounter = "";
			this.currentHealthAmount = this.player.stats.health;
			foreach (Enemy enemy in Object.FindObjectsOfType<Enemy>().ToList<Enemy>())
			{
				this.currentEnemyEncounter = this.currentEnemyEncounter + ", " + Item2.GetDisplayName(enemy.name);
			}
		}
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x00067AFC File Offset: 0x00065CFC
	public void BuildItemLists()
	{
		DebugItemManager debugItemManager = Object.FindObjectOfType<DebugItemManager>();
		Object.FindObjectOfType<Tote>();
		this.defaultItems = new List<Item2>();
		this.relics = new List<Item2>();
		this.curses = new List<Item2>();
		this.blessings = new List<Item2>();
		this.nonStandardItems = new List<Item2>();
		IEnumerable<Item2> enumerable = debugItemManager.item2s;
		if (ModItemLoader.main != null)
		{
			enumerable = debugItemManager.item2s.Concat(ModItemLoader.main.modItems);
		}
		foreach (Item2 item in enumerable)
		{
			if (item)
			{
				if (!item.isStandard)
				{
					this.nonStandardItems.Add(item);
				}
				else if (item.itemType.Contains(Item2.ItemType.Carving))
				{
					this.defaultItems.Add(item);
				}
				else if (item.itemType.Contains(Item2.ItemType.Curse))
				{
					this.curses.Add(item);
				}
				else if (item.itemType.Contains(Item2.ItemType.Blessing))
				{
					this.blessings.Add(item);
				}
				else if (item.itemType.Contains(Item2.ItemType.Relic))
				{
					this.relics.Add(item);
				}
				else
				{
					this.defaultItems.Add(item);
				}
			}
		}
	}

	// Token: 0x06000A3F RID: 2623 RVA: 0x00067C54 File Offset: 0x00065E54
	public void SetBrightness()
	{
		this.globalLight.intensity = Singleton.Instance.brightness;
	}

	// Token: 0x06000A40 RID: 2624 RVA: 0x00067C6C File Offset: 0x00065E6C
	private void SortRarities()
	{
		this.itemsToSpawn.Sort((Item2 a, Item2 b) => b.rarity.CompareTo(a.rarity));
		this.itemsToSpawn.Reverse();
		this.rarities = new List<int>();
		int num = 0;
		for (int i = 0; i < this.itemsToSpawn.Count; i++)
		{
			if (this.itemsToSpawn[i])
			{
				Item2.Rarity rarity = this.itemsToSpawn[i].rarity;
				if (rarity == Item2.Rarity.Common && this.rarities.Count == 0)
				{
					this.rarities.Add(num);
				}
				else if (rarity == Item2.Rarity.Uncommon && this.rarities.Count == 1)
				{
					this.rarities.Add(num);
				}
				else if (rarity == Item2.Rarity.Rare && this.rarities.Count == 2)
				{
					this.rarities.Add(num);
				}
				else if (rarity == Item2.Rarity.Legendary && this.rarities.Count == 3)
				{
					this.rarities.Add(num);
				}
				num++;
			}
		}
		this.rarities.Add(num);
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x00067D8C File Offset: 0x00065F8C
	public void SetItemsAllowToTake()
	{
		this.numOfItemsAllowedToTake = this.totalNumOfItemsAllowedToTake;
		List<Item2> list = new List<Item2>(Item2.allItems);
		list.AddRange(ItemPouch.GetAllItem2sFromPouches());
		if (Tote.main)
		{
			list.AddRange(Tote.main.GetAllCarvings());
		}
		list = list.Distinct<Item2>().ToList<Item2>();
		foreach (Item2 item in list)
		{
			if (item && item.itemMovement && !item.isOwned)
			{
				if (item.itemMovement.returnsToOutOfInventoryPosition && item.itemMovement.inGrid)
				{
					this.numOfItemsAllowedToTake--;
				}
				if (Tote.main && Tote.main.CardIsOwned(item.gameObject))
				{
					this.numOfItemsAllowedToTake--;
				}
			}
		}
		this.ChangeItemsAllowedToTake(0);
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x00067E98 File Offset: 0x00066098
	public void ChangeItemsAllowedToTakePerm(int num)
	{
		this.totalNumOfItemsAllowedToTake += num;
		this.SetItemsAllowToTake();
	}

	// Token: 0x06000A43 RID: 2627 RVA: 0x00067EB0 File Offset: 0x000660B0
	public void ChangeItemsAllowedToTake(int num)
	{
		if (this.totalNumOfItemsAllowedToTake == 0)
		{
			return;
		}
		ReplacementText component = this.promptText.GetComponent<ReplacementText>();
		ChestForLimitedItemGet chestForLimitedItemGet = Object.FindObjectOfType<ChestForLimitedItemGet>();
		LangaugeManager.main.SetFont(this.promptText.transform);
		this.numOfItemsAllowedToTake += num;
		if (this.numOfItemsAllowedToTake <= 0)
		{
			this.promptText.text = LangaugeManager.main.GetTextByKey("gm9");
			component.key = "gm9";
			component.textPreprocessor = null;
			if (chestForLimitedItemGet)
			{
				chestForLimitedItemGet.chestState = ChestForLimitedItemGet.ChestState.close;
				return;
			}
		}
		else if (this.numOfItemsAllowedToTake != 1)
		{
			this.promptText.text = LangaugeManager.main.GetTextByKey("gm10");
			component.key = "gm10";
			component.textPreprocessor = (string text) => text.Replace("/x", this.numOfItemsAllowedToTake.ToString() ?? "");
			this.promptText.text = this.promptText.text.Replace("/x", this.numOfItemsAllowedToTake.ToString() ?? "");
			if (chestForLimitedItemGet)
			{
				chestForLimitedItemGet.chestState = ChestForLimitedItemGet.ChestState.open;
				return;
			}
		}
		else
		{
			this.promptText.text = LangaugeManager.main.GetTextByKey("gm11");
			component.key = "gm11";
			component.textPreprocessor = null;
			if (chestForLimitedItemGet)
			{
				chestForLimitedItemGet.chestState = ChestForLimitedItemGet.ChestState.open;
			}
		}
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x00068004 File Offset: 0x00066204
	public void StartSimpleLimitedItemGetPeriod(int numToGet)
	{
		this.totalNumOfItemsAllowedToTake = numToGet;
		this.numOfItemsAllowedToTake = numToGet;
		this.limitedItemReorganize = true;
		this.ChangeItemsAllowedToTake(0);
		this.ShowFinishedButton();
		base.StartCoroutine(this.ShowPromptText(this.promptTrans, 95f));
		if (this.analyticsManager)
		{
			foreach (ItemMovement itemMovement in Object.FindObjectsOfType<ItemMovement>())
			{
				if (itemMovement && itemMovement.returnsToOutOfInventoryPosition)
				{
					this.analyticsManager.AddItem("itemsOffered", Item2.GetDisplayName(itemMovement.name));
				}
			}
		}
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x0006809C File Offset: 0x0006629C
	public void StartLimitedItemGetPeriod()
	{
		this.tutorialManager.ConsiderTutorial("inspector");
		this.standardSpawnAfter = false;
		Item2 item = null;
		if (TwitchManager.isRunningPolls())
		{
			item = TwitchManager.Instance.pollManager.onCombatEnd();
		}
		if (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.noRelics) == null && (this.dungeonLevel.currentFloor == DungeonLevel.Floor.boss || (this.tutorialManager.getRelics && this.tutorialManager.playType == TutorialManager.PlayType.testing)))
		{
			base.StartCoroutine(this.ShowPromptText(this.promptTrans, 95f));
			this.totalNumOfItemsAllowedToTake = 1;
			this.numOfItemsAllowedToTake = 1;
			this.SpawnRelics();
		}
		else
		{
			if (TwitchManager.isRunningPolls() && TwitchManager.Instance.pollManager.setting.disableLoot)
			{
				return;
			}
			if (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.replaceItemGetWithChest) != null)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.chest, Vector3.zero, Quaternion.identity, this.player.transform.parent);
				gameObject.transform.localPosition = new Vector3(this.spawnPosition.position.x, this.chest.transform.position.y, -2.5f);
				gameObject.GetComponentInChildren<Chest>().canBeCurseReplaced = true;
				return;
			}
			if (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.replaceItemGetWithStore) != null)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(this.store, Vector3.zero, Quaternion.identity, this.player.transform.parent);
				gameObject2.transform.localPosition = new Vector3(this.spawnPosition.position.x, this.store.transform.position.y, -2.5f);
				gameObject2.GetComponentInChildren<Store>().canBeCurseReplaced = true;
				return;
			}
			base.StartCoroutine(this.ShowPromptText(this.promptTrans, 95f));
			this.totalNumOfItemsAllowedToTake = 3;
			this.numOfItemsAllowedToTake = 3;
			RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.getExtraItem);
			if (runProperty != null)
			{
				this.numOfItemsAllowedToTake += runProperty.value;
				this.totalNumOfItemsAllowedToTake += runProperty.value;
			}
			this.numOfItemsAllowedToTake = Mathf.Max(1, this.numOfItemsAllowedToTake);
			this.totalNumOfItemsAllowedToTake = Mathf.Max(1, this.totalNumOfItemsAllowedToTake);
			List<GameObject> list;
			if (this.player.characterName == Character.CharacterName.Tote)
			{
				this.tutorialManager.ConsiderTutorial("toteC");
				int num = 3;
				this.totalNumOfItemsAllowedToTake = 1;
				this.numOfItemsAllowedToTake = 1;
				if (this.currentEventType == DungeonEvent.DungeonEventType.Shambler)
				{
					this.totalNumOfItemsAllowedToTake = 2;
					this.numOfItemsAllowedToTake = 2;
					num += 2;
				}
				RunType.RunProperty runProperty2 = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.getExtraItem);
				if (runProperty2)
				{
					this.totalNumOfItemsAllowedToTake += runProperty2.value;
					this.numOfItemsAllowedToTake += runProperty2.value;
				}
				this.totalNumOfItemsAllowedToTake = Mathf.Max(1, this.totalNumOfItemsAllowedToTake);
				this.numOfItemsAllowedToTake = Mathf.Max(1, this.numOfItemsAllowedToTake);
				list = new List<GameObject>();
				if (item)
				{
					num--;
					GameObject gameObject3 = Object.Instantiate<GameObject>(item.gameObject, Vector3.zero, Quaternion.identity, this.inventoryItemsParent);
					list.Add(gameObject3);
				}
				for (int i = 0; i < num; i++)
				{
					bool flag;
					GameObject gameObject4 = this.SpawnItem(new List<Item2.ItemType> { Item2.ItemType.Carving }, new List<Item2.Rarity> { this.ChooseRarity(out flag, 0f, true) }, false, null);
					list.Add(gameObject4);
					this.ShowGotLucky(gameObject4.transform, flag);
				}
				this.ConsiderCurseReplacement(list);
			}
			else
			{
				list = new List<GameObject>();
				int num2 = 7;
				if (item)
				{
					num2--;
					GameObject gameObject5 = Object.Instantiate<GameObject>(item.gameObject, Vector3.zero, Quaternion.identity, this.inventoryItemsParent);
					list.Add(gameObject5);
				}
				List<GameObject> list2 = ItemSpawner.InstantiateItems(ItemSpawner.GetItemsWithLuck(num2, Item2.GetAllItemTypesExcluding(null), true, false, 0f));
				list = list.Concat(list2).ToList<GameObject>();
			}
			int num3 = 0;
			int num4 = 0;
			int num5 = 3;
			int num6 = 0;
			if (!this.inventoryItemsParent)
			{
				this.inventoryItemsParent = GameObject.FindGameObjectWithTag("ItemParent").transform;
			}
			foreach (GameObject gameObject6 in list)
			{
				if (gameObject6.GetComponent<Item2>().rarity == Item2.Rarity.Legendary)
				{
					this.legendaries += 1f;
				}
				else if (gameObject6.GetComponent<Item2>().rarity == Item2.Rarity.Rare)
				{
					this.rares += 1f;
				}
				else if (gameObject6.GetComponent<Item2>().rarity == Item2.Rarity.Uncommon)
				{
					this.uncommons += 1f;
				}
				else if (gameObject6.GetComponent<Item2>().rarity == Item2.Rarity.Common)
				{
					this.commons += 1f;
				}
				ItemMovement component = gameObject6.GetComponent<ItemMovement>();
				gameObject6.transform.SetParent(this.inventoryItemsParent);
				if (list.Count == 5 && this.player.characterName == Character.CharacterName.Tote)
				{
					gameObject6.transform.position = new Vector3((float)((num3 + num4 * 3) * 2) - 2.5f - 1.5f, -1.4f, 0f);
				}
				else
				{
					gameObject6.transform.position = new Vector3((float)(num3 * 2 - num5 / 2 - 1), -1.4f - (float)num4 * 1.6f, 0f);
				}
				component.outOfInventoryPosition = component.transform.parent.InverseTransformPoint(gameObject6.transform.position);
				component.outOfInventoryRotation = component.transform.rotation;
				component.returnsToOutOfInventoryPosition = true;
				num3++;
				num6++;
				if (num3 >= 3 && num4 == 0)
				{
					num5 = 5;
					num3 = 0;
					num4++;
				}
			}
			this.ConsiderCurseReplacement(list);
		}
		this.limitedItemReorganize = true;
		this.ChangeItemsAllowedToTake(0);
		this.ShowFinishedButton();
		if (this.analyticsManager)
		{
			foreach (ItemMovement itemMovement in Object.FindObjectsOfType<ItemMovement>())
			{
				if (itemMovement && itemMovement.returnsToOutOfInventoryPosition)
				{
					this.analyticsManager.AddItem("itemsOffered", Item2.GetDisplayName(itemMovement.name));
				}
			}
		}
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x000686D0 File Offset: 0x000668D0
	public void ConsiderCurseReplacement(List<Item2> itemsToReplace)
	{
		this.ConsiderCurseReplacement(itemsToReplace.Select((Item2 x) => x.gameObject).ToList<GameObject>());
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x00068702 File Offset: 0x00066902
	public bool IsConsideringCurseReplacement()
	{
		return this.spawnCurses != null;
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x0006870D File Offset: 0x0006690D
	public void EndCurseReplacement()
	{
		if (this.spawnCurses != null)
		{
			base.StopCoroutine(this.spawnCurses);
		}
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x00068723 File Offset: 0x00066923
	public void ShowFinishedButton()
	{
		this.ShowBasicButtons(GameManager.ButtonsToEnable.finishReorganizingButton, LangaugeManager.main.GetTextByKey("gm8"), "gm8");
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x00068740 File Offset: 0x00066940
	public List<Item2> getPossibleRelics()
	{
		List<string> list = new List<string>();
		foreach (Item2 item in this.itemsParent.GetComponentsInChildren<Item2>())
		{
			if (item.itemType.Contains(Item2.ItemType.Relic))
			{
				list.Add(Item2.GetDisplayName(item.name));
			}
		}
		ItemPouch[] array = Object.FindObjectsOfType<ItemPouch>();
		for (int i = 0; i < array.Length; i++)
		{
			foreach (GameObject gameObject in array[i].itemsInside)
			{
				list.Add(Item2.GetDisplayName(gameObject.name));
			}
		}
		IEnumerable<Item2> enumerable = DebugItemManager.main.item2s;
		if (ModItemLoader.main != null)
		{
			enumerable = DebugItemManager.main.item2s.Concat(ModItemLoader.main.modItems);
		}
		List<Item2> list2 = new List<Item2>();
		foreach (Item2 item2 in enumerable)
		{
			if (item2.itemType.Contains(Item2.ItemType.Relic) && !list.Contains(Item2.GetDisplayName(item2.name)) && this.ItemValidToSpawn(item2, false))
			{
				list2.Add(item2);
			}
		}
		return list2;
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x000688B0 File Offset: 0x00066AB0
	private IEnumerator CurseRoutine(List<GameObject> itemsToReplace)
	{
		yield return new WaitForSeconds(0.5f);
		List<GameObject> curses = CurseManager.Instance.GetAllCurses();
		int numOfItemsReplaced = 0;
		itemsToReplace.Reverse();
		foreach (GameObject gameObject in itemsToReplace)
		{
			if (numOfItemsReplaced >= 4)
			{
				break;
			}
			Item2 component = gameObject.GetComponent<Item2>();
			if (component && !component.destroyed)
			{
				ItemMovement itemMovement = component.itemMovement;
				if (itemMovement)
				{
					if (curses.Count == 0)
					{
						break;
					}
					GameObject gameObject2 = curses[0];
					curses.RemoveAt(0);
					if (!gameObject2)
					{
						break;
					}
					ItemMovement component2 = gameObject2.GetComponent<ItemMovement>();
					Item2 component3 = gameObject2.GetComponent<Item2>();
					gameObject2.transform.position = itemMovement.transform.position;
					if (itemMovement.myItem)
					{
						component3.isForSale = itemMovement.myItem.isForSale;
						component3.isOwned = itemMovement.myItem.isOwned;
					}
					if (itemMovement.returnsToOutOfInventoryPosition)
					{
						ItemMovement.CopyBounceTime(itemMovement, component2);
						component2.returnsToOutOfInventoryPosition = true;
						component2.outOfInventoryPosition = itemMovement.outOfInventoryPosition;
						component2.outOfInventoryRotation = Quaternion.identity;
					}
					itemMovement.DelayDestroy();
					int num = numOfItemsReplaced;
					numOfItemsReplaced = num + 1;
					EffectParticleSystem.Instance.CopySprite(itemMovement.GetComponent<SpriteRenderer>(), EffectParticleSystem.ParticleType.curse);
					SoundManager.main.PlaySFX("cantMoveHere");
					yield return new WaitForSeconds(0.45f);
				}
			}
		}
		List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
		GameObject gameObject3 = null;
		while (curses.Count > 0 && curses.Count != 0)
		{
			GameObject gameObject4 = curses[0];
			curses.RemoveAt(0);
			ItemMovement component4 = gameObject4.GetComponent<ItemMovement>();
			component4.returnsToOutOfInventoryPosition = false;
			component4.outOfInventoryPosition = Vector3.zero;
			component4.outOfInventoryRotation = Quaternion.identity;
			gameObject4.transform.position = new Vector3(0f, 0f, 0f);
			gameObject3 = gameObject4;
		}
		if (gameObject3)
		{
			EffectParticleSystem.Instance.CopySprite(gameObject3.GetComponent<SpriteRenderer>(), EffectParticleSystem.ParticleType.curse);
			this.MoveAllItems();
		}
		Store store = Object.FindObjectOfType<Store>();
		if (store)
		{
			store.SetPrices();
		}
		this.spawnCurses = null;
		yield break;
		yield break;
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x000688C8 File Offset: 0x00066AC8
	private void SpawnRelics()
	{
		if (TwitchManager.isRunningPolls() && TwitchManager.Instance.pollManager.pollRelic != null)
		{
			new List<Item2>().Add(TwitchManager.Instance.pollManager.pollRelic);
			TwitchManager.Instance.pollManager.pollRelic = null;
		}
		else
		{
			this.getPossibleRelics();
		}
		List<Item2> list = new List<Item2>();
		list.AddRange(ItemSpawner.GetItems(1, new List<Item2.ItemType> { Item2.ItemType.Relic }, new List<Item2.SpawnGrouping> { Item2.SpawnGrouping.relic2 }, false, true));
		list.AddRange(ItemSpawner.GetItems(1, new List<Item2.ItemType> { Item2.ItemType.Relic }, new List<Item2.SpawnGrouping> { Item2.SpawnGrouping.relic3 }, false, true));
		int num = 3 - list.Count;
		list.AddRange(ItemSpawner.GetItems(num, new List<Item2.ItemType> { Item2.ItemType.Relic }, new List<Item2.SpawnGrouping> { Item2.SpawnGrouping.relic1 }, false, true));
		ItemSpawner.InstantiateItems(list);
		MetaProgressSaveManager.main.AddRunEvent(MetaProgressSaveManager.LastRun.RunEvents.gotARelic);
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x000689BD File Offset: 0x00066BBD
	public void ConsiderCurseReplacement(List<GameObject> itemsToReplace)
	{
		this.spawnCurses = base.StartCoroutine(this.CurseRoutine(itemsToReplace));
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x000689D2 File Offset: 0x00066BD2
	public List<GameObject> ItemsValidToSpawn(GameObject[] originalItemList, bool store = false)
	{
		return this.ItemsValidToSpawn(originalItemList.ToList<GameObject>(), store);
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x000689E4 File Offset: 0x00066BE4
	public List<GameObject> ItemsValidToSpawn(List<GameObject> originalItemList, bool store = false)
	{
		return (from x in this.ItemsValidToSpawn(originalItemList.Select((GameObject x) => x.GetComponent<Item2>()).ToList<Item2>(), store)
			select x.gameObject).ToList<GameObject>();
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x00068A4C File Offset: 0x00066C4C
	public List<Item2> ItemsValidToSpawn(List<Item2> originalItemList, bool store = false)
	{
		List<Item2> list = new List<Item2>(originalItemList);
		for (int i = 0; i < list.Count; i++)
		{
			Item2 item = list[i];
			if (!item)
			{
				list.RemoveAt(i);
				i--;
			}
			else if (!this.ItemValidToSpawn(item, store))
			{
				list.RemoveAt(i);
				i--;
			}
		}
		if (list.Count == 0)
		{
			list = new List<Item2>(this.itemsToSpawn.Select((Item2 x) => x.GetComponent<Item2>()));
		}
		return list;
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x00068ADC File Offset: 0x00066CDC
	public bool ItemValidToSpawn(Item2 itemScript, bool store = false)
	{
		if (Singleton.Instance.storyMode)
		{
			if (itemScript.storyModeAvailabilityType == Item2.AvailabilityType.Never)
			{
				return false;
			}
			if (itemScript.storyModeAvailabilityType == Item2.AvailabilityType.MarkerDependent && !MetaProgressSaveManager.ConditionsMet(itemScript.conditions))
			{
				return false;
			}
			if (itemScript.storyModeAvailabilityType == Item2.AvailabilityType.UnlockDependent && (!MetaProgressSaveManager.main.itemsUnlocked.Contains(Item2.GetDisplayName(itemScript.name)) || !MetaProgressSaveManager.ConditionsMet(itemScript.conditions)))
			{
				return false;
			}
			if ((itemScript.storyModeAvailabilityType == Item2.AvailabilityType.MarkerDependent || (itemScript.storyModeAvailabilityType == Item2.AvailabilityType.UnlockDependent && itemScript.conditions != null && itemScript.conditions.Count > 0) || itemScript.oneOfAKindType == Item2.OneOfAKindType.OneTotal) && RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.storyModeProgressDisabled))
			{
				return false;
			}
		}
		else
		{
			if (itemScript.quickGameAvailabilityType == Item2.AvailabilityType.Never)
			{
				return false;
			}
			if (itemScript.quickGameAvailabilityType == Item2.AvailabilityType.MarkerDependent && !MetaProgressSaveManager.ConditionsMet(itemScript.conditions))
			{
				return false;
			}
			if (itemScript.quickGameAvailabilityType == Item2.AvailabilityType.UnlockDependent && !MetaProgressSaveManager.main.itemsUnlocked.Contains(Item2.GetDisplayName(itemScript.name)))
			{
				return false;
			}
		}
		if ((itemScript.oneOfAKindType == Item2.OneOfAKindType.OnePerRun || itemScript.CheckForStatusEffect(Item2.ItemStatusEffect.Type.unique) || itemScript.itemType.Contains(Item2.ItemType.Relic)) && Item2.CheckForItemOfName(itemScript))
		{
			return false;
		}
		if (itemScript.oneOfAKindType == Item2.OneOfAKindType.OneTotal && (Item2.CheckForItemOfName(itemScript) || MetaProgressSaveManager.main.HasOneOfAKindItem(itemScript)))
		{
			return false;
		}
		RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.cannotFind);
		if (runProperty && runProperty.assignedPrefabs.Contains(itemScript.gameObject))
		{
			return false;
		}
		RunType.RunProperty runProperty2 = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.youCanStillFindItemsOfExludedTypeIfTheyAlsoHaveThisType);
		RunType.RunProperty runProperty3 = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.cannotFindItemOfType);
		if (runProperty3 && Item2.ShareItemTypes(runProperty3.itemTypes, itemScript.itemType) && (runProperty2 == null || !Item2.ShareItemTypes(runProperty2.itemTypes, itemScript.itemType)))
		{
			return false;
		}
		RunType.RunProperty runProperty4 = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.itemSizeExact);
		if (runProperty4 && ItemMovement.GetSpacesNeededStatic(itemScript.gameObject) != runProperty4.value)
		{
			return false;
		}
		runProperty4 = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.itemSizeLarger);
		if (runProperty4 && ItemMovement.GetSpacesNeededStatic(itemScript.gameObject) < runProperty4.value)
		{
			return false;
		}
		runProperty4 = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.allItemsCommonOrUncommon);
		return (!runProperty4 || itemScript.rarity == Item2.Rarity.Common || itemScript.rarity == Item2.Rarity.Uncommon || itemScript.itemType.Contains(Item2.ItemType.Relic)) && (!store || !itemScript.CheckForStatusEffect(Item2.ItemStatusEffect.Type.cannotBeFoundInStores)) && (itemScript.eventTypeAvailabilityType == EventManager.EventType.None || !EventManager.instance || EventManager.instance.eventType == itemScript.eventTypeAvailabilityType) && ((itemScript.validForCharacters.Count == 0 || itemScript.validForCharacters.Contains(Character.CharacterName.Any) || itemScript.validForCharacters.Contains(this.player.characterName)) && (itemScript.validForZones.Count == 0 || itemScript.validForZones.Contains(this.dungeonLevel.zone)) && (itemScript.isAvailableInDemo || !Singleton.Instance.isDemo));
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x00068DAF File Offset: 0x00066FAF
	public void SetLevelFromBeginning()
	{
		this.SetLevel(this.zonesPlayList[0].zones[0]);
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x00068DD0 File Offset: 0x00066FD0
	public void SetFirstLevel()
	{
		if (Singleton.Instance.IsStoryModeLevels())
		{
			Missions mission = Singleton.Instance.mission;
			this.SetLevel(mission.GetNextDungeonLevel(0));
			Object.FindObjectOfType<BackgroundController>().UpdateAllSprites();
			return;
		}
		this.SetLevel(this.zonesPlayList[0].zones[0]);
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x00068E29 File Offset: 0x00067029
	public void SetLevel(DungeonLevel.Zone zone)
	{
		Debug.Log("SetLevel " + zone.ToString());
		this.dungeonLevel = DungeonSpawner.main.GetDungeonLevel(zone);
		this.SetLevel(this.dungeonLevel);
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x00068E64 File Offset: 0x00067064
	public void SetLevel(DungeonLevel dungeonLevel)
	{
		this.dungeonLevel = dungeonLevel;
		this.SetupValidItems();
		if (this.tutorialManager.playType != TutorialManager.PlayType.tutorial || !Singleton.Instance.storyMode)
		{
			SoundManager.main.PlayOrContinueSong(dungeonLevel.levelSong);
		}
		Object.FindObjectOfType<BackgroundController>();
		this.backgroundColorSquareRenderer.color = dungeonLevel.backgroundColor;
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x00068EC0 File Offset: 0x000670C0
	public void SetupValidItems()
	{
		if (this.tutorialManager.playType == TutorialManager.PlayType.testing && this.tutorialManager.itemOnly != null)
		{
			this.itemsToSpawn = new List<Item2> { this.tutorialManager.itemOnly.GetComponent<Item2>() };
			return;
		}
		this.itemsToSpawn = new List<Item2>();
		foreach (Item2 item in this.defaultItems)
		{
			RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.increaseChanceOfFindingItemType);
			if (this.ItemValidToSpawn(item, false) && item.isStandard)
			{
				this.itemsToSpawn.Add(item);
				if (runProperty && Item2.ShareItemTypes(runProperty.itemTypes, item.itemType))
				{
					this.itemsToSpawn.Add(item);
				}
			}
		}
		this.SortRarities();
		this.itemsToSpawn.RemoveAll((Item2 x) => x == null || !x);
		this.itemsToSpawn = this.itemsToSpawn.OrderBy((Item2 x) => x.name).ToList<Item2>();
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x00069010 File Offset: 0x00067210
	public void SetAllSpritesToLayer0()
	{
		SpriteRenderer[] componentsInChildren = this.inventoryTransform.GetComponentsInChildren<SpriteRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].sortingOrder = 0;
		}
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x00069040 File Offset: 0x00067240
	public string GetNextLevelName(int doorNumber)
	{
		if (this.dungeonLevel.currentFloor == DungeonLevel.Floor.first || this.dungeonLevel.currentFloor == DungeonLevel.Floor.second)
		{
			return this.dungeonLevel.areaName;
		}
		int num = this.zoneNumber;
		num++;
		if (this.floor == 0 && this.dungeonLevel.currentFloor == DungeonLevel.Floor.intro)
		{
			num--;
		}
		if (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.finalRun) && num >= this.zonesPlayList.Count)
		{
			return "Chaotic Darkness";
		}
		if (num >= this.zonesPlayList.Count)
		{
			num = 0;
		}
		if (!Singleton.Instance.storyMode || !(Singleton.Instance.mission != null) || Singleton.Instance.mission.dungeonLevels.Count <= 0)
		{
			DungeonLevel.Zone zone = this.zonesPlayList[num].zones[doorNumber];
			return Object.FindObjectOfType<DungeonSpawner>().GetDungeonLevel(zone).areaName;
		}
		Missions mission = Singleton.Instance.mission;
		int num2 = this.zoneNumber;
		if (GameManager.main.floor != 0)
		{
			num2++;
		}
		DungeonLevel nextDungeonLevel = mission.GetNextDungeonLevel(num2);
		if (!nextDungeonLevel)
		{
			return "opt21";
		}
		return nextDungeonLevel.areaName;
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x00069166 File Offset: 0x00067366
	public List<DungeonLevel.EnemyEncounter2> GetEncountersForFloor(DungeonLevel.Floor floorType)
	{
		return this.dungeonLevel.enemyEncounters;
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x00069174 File Offset: 0x00067374
	public DungeonLevel.Zone GetNextLevel(int doorNumber)
	{
		if (doorNumber == -1)
		{
			return this.zonesPlayList[0].zones[0];
		}
		int num = this.zoneNumber;
		if (this.floor != 0)
		{
			num++;
		}
		if (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.finalRun) && num >= this.zonesPlayList.Count)
		{
			return this.chaoticDarkness;
		}
		if (num >= this.zonesPlayList.Count)
		{
			num = 0;
		}
		return this.zonesPlayList[num].zones[Mathf.Min(doorNumber, this.zonesPlayList[num].zones.Count - 1)];
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x00069217 File Offset: 0x00067417
	public IEnumerator NextLevel(int doorNumber)
	{
		if (this.ending)
		{
			yield break;
		}
		DungeonSpawner dungeonSpawner = Object.FindObjectOfType<DungeonSpawner>();
		RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.oneFloorBeforeBoss);
		this.floor++;
		int num = this.dungeonLevel.floors.IndexOf(this.dungeonLevel.currentFloor);
		if (this.floor == 1 && this.dungeonLevel.currentFloor == DungeonLevel.Floor.intro)
		{
			this.zoneNumber = -1;
			num = 999;
		}
		if (doorNumber == -1)
		{
			this.SetLevel(this.zonesPlayList[0].zones[0]);
			this.zoneNumber = -1;
			this.dungeonLevel.currentFloor = DungeonLevel.Floor.intro;
		}
		else if (num < this.dungeonLevel.floors.Count - 1)
		{
			if (runProperty != null)
			{
				num = this.dungeonLevel.floors.Count - 1;
			}
			else
			{
				num++;
			}
			this.dungeonLevel.currentFloor = this.dungeonLevel.floors[num];
			dungeonSpawner.ClearPropertiesByLength(DungeonSpawner.DungeonProperty.Length.forFloor);
		}
		else
		{
			dungeonSpawner.ClearPropertiesByType(DungeonSpawner.DungeonProperty.Type.chosenBoss);
			if (this.analyticsManager)
			{
				this.analyticsManager.AddItem("zonesCompleted", this.dungeonLevel.zone.ToString());
			}
			num = 0;
			this.zoneNumber++;
			if (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.finalRun) && this.zoneNumber >= this.zonesPlayList.Count)
			{
				this.zoneNumber++;
				this.SetLevel(this.chaoticDarkness);
			}
			else
			{
				if (this.zoneNumber >= this.zonesPlayList.Count)
				{
					this.zoneNumber = 0;
				}
				if (Singleton.Instance.IsStoryModeLevels())
				{
					DungeonLevel nextDungeonLevel = Singleton.Instance.mission.GetNextDungeonLevel(this.zoneNumber);
					if (!nextDungeonLevel)
					{
						Debug.LogError("No dungeon level found for zone " + this.zoneNumber.ToString());
						Options.ReturnToOrderiaInternal();
						Debug.LogError("Returning to orderia");
						yield break;
					}
					this.SetLevel(nextDungeonLevel);
				}
				else
				{
					this.SetLevel(this.zonesPlayList[this.zoneNumber].zones[Mathf.Min(doorNumber, this.zonesPlayList[this.zoneNumber].zones.Count - 1)]);
				}
			}
			dungeonSpawner.ClearPropertiesByLength(DungeonSpawner.DungeonProperty.Length.forFloor);
			dungeonSpawner.ClearPropertiesByLength(DungeonSpawner.DungeonProperty.Length.forZone);
			if (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.bossRush) != null)
			{
				this.dungeonLevel.currentFloor = this.dungeonLevel.floors[this.dungeonLevel.floors.Count - 1];
			}
			else
			{
				this.dungeonLevel.currentFloor = this.dungeonLevel.floors[0];
			}
		}
		if (this.floor != 0 && MetaProgressSaveManager.main.lastRun != null && MetaProgressSaveManager.main.lastRun.result == MetaProgressSaveManager.LastRun.Result.none)
		{
			MetaProgressSaveManager.main.AddRunEvent(this.dungeonLevel.runEvent);
		}
		this.levelTransition.gameObject.SetActive(true);
		this.levelName.text = LangaugeManager.main.GetTextByKey(this.dungeonLevel.areaName);
		LangaugeManager.main.SetFont(this.levelName.transform);
		LangaugeManager.main.SetFont(this.levelPart.transform);
		if (doorNumber != -1)
		{
			if (this.dungeonLevel.currentFloor == DungeonLevel.Floor.boss)
			{
				this.levelPart.text = LangaugeManager.main.GetTextByKey("gm6");
			}
			else
			{
				this.levelPart.text = LangaugeManager.main.GetTextByKey("gm7");
			}
			this.levelPart.text = this.levelPart.text.Replace("/x", (num + 1).ToString() ?? "");
			if (runProperty != null)
			{
				this.levelPart.text = this.levelPart.text.Replace("/y", 2.ToString() ?? "");
			}
			else
			{
				this.levelPart.text = this.levelPart.text.Replace("/y", this.dungeonLevel.floors.Count.ToString() ?? "");
			}
		}
		else
		{
			this.levelName.text = LangaugeManager.main.GetTextByKey("gm7bn");
			this.levelPart.text = LangaugeManager.main.GetTextByKey("gm7b").Replace("/x", (Mathf.RoundToInt((float)(this.floor / 9)) + 1).ToString() ?? "");
		}
		this.levelTransition.GetComponent<Animator>().Play("levelTransition", 0, 0f);
		yield return new WaitForSeconds(1.1f);
		if (this.floor >= 4 && Singleton.Instance.endGameAfterBoss)
		{
			this.EndDemo();
			yield break;
		}
		dungeonSpawner.StartCoroutine(dungeonSpawner.SpawnChambers());
		Object.FindObjectOfType<BackgroundController>().UpdateAllSprites();
		this.player.transform.position = new Vector3(-3.93f, -3.6f, 0f);
		this.playerAnimator.Play("Player_Idle", 0, 0f);
		this.playerTransform.GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
		yield return new WaitForSeconds(0.5f);
		SaveManager saveManager = Object.FindObjectOfType<SaveManager>();
		saveManager.StartCoroutine(saveManager.Save(null, "Game Saved"));
		yield return new WaitForSeconds(0.5f);
		this.DespawnAllDungeonVisuals();
		yield return new WaitForSeconds(0.5f);
		this.inventoryPhase = GameManager.InventoryPhase.open;
		this.levelTransition.GetComponent<Animator>().Play("fadeOut", 0, 0f);
		this.travelling = false;
		this.ShowMap(false);
		yield break;
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x00069230 File Offset: 0x00067430
	public void CreatePopUp(string text)
	{
		Canvas component = GameObject.FindGameObjectWithTag("UI Canvas").GetComponent<Canvas>();
		Vector2 vector;
		if (component.renderMode == RenderMode.ScreenSpaceCamera)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(component.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(DigitalCursor.main.transform.position), component.worldCamera, out vector);
		}
		else
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(component.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(DigitalCursor.main.transform.position), null, out vector);
		}
		this.CreatePopUp(text, vector);
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x000692C0 File Offset: 0x000674C0
	public void CreatePopUp(string text, float speed)
	{
		Canvas component = GameObject.FindGameObjectWithTag("UI Canvas").GetComponent<Canvas>();
		Vector2 vector;
		if (component.renderMode == RenderMode.ScreenSpaceCamera)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(component.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(DigitalCursor.main.transform.position), component.worldCamera, out vector);
		}
		else
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(component.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(DigitalCursor.main.transform.position), null, out vector);
		}
		this.CreatePopUp(text, vector, speed);
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x00069350 File Offset: 0x00067550
	public void CreatePopUpWorld(string text, Vector2 worldPoint)
	{
		Vector2 vector = GameObject.FindGameObjectWithTag("UI Canvas").GetComponent<Canvas>().transform.InverseTransformPoint(worldPoint);
		this.CreatePopUp(text, vector);
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x0006938A File Offset: 0x0006758A
	public void CreatePopUp(string text, Vector2 localPoint)
	{
		this.CreatePopUp(text, localPoint, 1f);
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x0006939C File Offset: 0x0006759C
	public void CreatePopUp(string text, Vector2 localPoint, float speed)
	{
		Canvas component = GameObject.FindGameObjectWithTag("UI Canvas").GetComponent<Canvas>();
		GameObject gameObject = Object.Instantiate<GameObject>(this.popUpPrefab, Vector3.zero, Quaternion.identity, component.transform);
		gameObject.GetComponentInChildren<TextMeshProUGUI>().text = text;
		gameObject.GetComponent<RectTransform>().anchoredPosition = localPoint;
		gameObject.GetComponentInChildren<Animator>().speed = speed;
		LangaugeManager.main.SetFont(gameObject.transform);
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x0006940C File Offset: 0x0006760C
	public void Retry()
	{
		if (Singleton.Instance.storyMode)
		{
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.runsDied, 1);
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.runsPlayed, 1);
			MetaProgressSaveManager.main.SaveLastRun(MetaProgressSaveManager.LastRun.Result.died);
			SaveAnItemOnLoss saveAnItemOnLoss = Object.FindObjectOfType<SaveAnItemOnLoss>();
			if (saveAnItemOnLoss)
			{
				saveAnItemOnLoss.SaveItem();
			}
		}
		SoundManager.main.StopAllSongs();
		Singleton.Instance.completedTutorial = true;
		Singleton.Instance.loadSave = false;
		SceneLoader.main.LoadScene("Game", LoadSceneMode.Single, null, null);
		SoundManager.main.PlaySongSudden("roaming_1", 0f, 0f, true);
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x000694AC File Offset: 0x000676AC
	public void QuitOnLoss()
	{
		if (Singleton.Instance.storyMode)
		{
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.runsDied, 1);
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.runsPlayed, 1);
			MetaProgressSaveManager.main.SaveLastRun(MetaProgressSaveManager.LastRun.Result.died);
			SaveAnItemOnLoss saveAnItemOnLoss = Object.FindObjectOfType<SaveAnItemOnLoss>();
			if (saveAnItemOnLoss)
			{
				saveAnItemOnLoss.SaveItem();
			}
		}
		this.EndDemo();
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x00069504 File Offset: 0x00067704
	public bool InEmptyRoom(bool ignoreText = false)
	{
		if (!this.dungeonPlayer)
		{
			this.dungeonPlayer = Object.FindObjectOfType<DungeonPlayer>();
		}
		return !this.saveManager.isSavingOrLoading && this.combatVisualTransform.childCount <= 1 && (!this.viewingEvent || ignoreText) && this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.outOfBattle && this.inventoryPhase == GameManager.InventoryPhase.open && !this.limitedItemReorganize && this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.trulyDone && !this.dungeonPlayer.isMoving;
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x00069595 File Offset: 0x00067795
	public IEnumerator SaveAndQuit()
	{
		SaveManager saveManager = Object.FindObjectOfType<SaveManager>();
		if (saveManager)
		{
			this.ShowInventory();
			yield return saveManager.Save(null, "Game Saved");
			SceneLoader.main.LoadScene("MainMenu", LoadSceneMode.Single, null, null);
		}
		yield break;
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x000695A4 File Offset: 0x000677A4
	public bool CanSave()
	{
		if (this.saveManager.isSavingOrLoading)
		{
			return false;
		}
		CombatPet[] array = Object.FindObjectsOfType<CombatPet>();
		if (this.combatVisualTransform.childCount > 1 + array.Length || (this.viewingEvent && !Singleton.Instance.showingOptions) || this.travelling)
		{
			this.CreatePopUp(LangaugeManager.main.GetTextByKey("gm2"));
			return false;
		}
		if (this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.outOfBattle || this.inventoryPhase != GameManager.InventoryPhase.open || this.limitedItemReorganize)
		{
			this.CreatePopUp(LangaugeManager.main.GetTextByKey("gm3"));
			return false;
		}
		if (this.tutorialManager.tutorialSequence != TutorialManager.TutorialSequence.trulyDone)
		{
			this.CreatePopUp(LangaugeManager.main.GetTextByKey("gm4"));
			return false;
		}
		if (Object.FindObjectOfType<DungeonPlayer>().isMoving)
		{
			this.CreatePopUp(LangaugeManager.main.GetTextByKey("gm5"));
			return false;
		}
		return true;
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x00069689 File Offset: 0x00067889
	public void SaveGame()
	{
		if (this.CanSave())
		{
			this.ShowInventory();
			this.saveManager.StartCoroutine(this.saveManager.Save(null, "Game Saved"));
			return;
		}
		Debug.LogError("Could not save");
	}

	// Token: 0x06000A67 RID: 2663 RVA: 0x000696C4 File Offset: 0x000678C4
	public void ShowOptions()
	{
		if (this.saveManager.isSavingOrLoading || this.viewingEvent)
		{
			return;
		}
		if (Singleton.Instance.showingOptions)
		{
			Options options = Object.FindObjectOfType<Options>();
			if (options)
			{
				options.EndEvent();
				return;
			}
			Singleton.Instance.showingOptions = false;
		}
		GameObject gameObject = Object.Instantiate<GameObject>(this.options, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("UI Canvas").GetComponent<Canvas>().transform);
		gameObject.transform.SetAsLastSibling();
		gameObject.transform.localPosition = Vector3.zero;
		Singleton.Instance.showingOptions = true;
		this.viewingEvent = true;
	}

	// Token: 0x06000A68 RID: 2664 RVA: 0x00069768 File Offset: 0x00067968
	public void SetAllItemColliders(bool enable)
	{
		foreach (Item2 item in GameObject.FindGameObjectWithTag("ItemParent").GetComponentsInChildren<Item2>())
		{
			if (!item.destroyed)
			{
				BoxCollider2D[] array = item.GetComponentsInChildren<BoxCollider2D>();
				for (int j = 0; j < array.Length; j++)
				{
					array[j].enabled = enable;
				}
			}
		}
		GridSquare[] array2 = Object.FindObjectsOfType<GridSquare>();
		for (int i = 0; i < array2.Length; i++)
		{
			BoxCollider2D[] array = array2[i].GetComponentsInChildren<BoxCollider2D>();
			for (int j = 0; j < array.Length; j++)
			{
				array[j].enabled = enable;
			}
		}
	}

	// Token: 0x06000A69 RID: 2665 RVA: 0x00069800 File Offset: 0x00067A00
	private void KeyBoardControls()
	{
		if (this.saveManager.isSavingOrLoading)
		{
			return;
		}
		if (this.viewingEvent)
		{
			return;
		}
		if (Singleton.Instance.modDebugging && Input.GetKey(KeyCode.LeftControl))
		{
			return;
		}
		if (Singleton.Instance.modDebugging && Input.GetKey(KeyCode.RightControl))
		{
			return;
		}
		if (ItemSpawnMenu.main != null)
		{
			return;
		}
		if ((DigitalCursor.main.GetInputDown("rotateRight") && (this.draggingItem || GameFlowManager.main.battlePhase == GameFlowManager.BattlePhase.outOfBattle)) || Input.GetKeyDown("e") || Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow))
		{
			this.RotateAll(-90f);
		}
		else if ((DigitalCursor.main.GetInputDown("rotateLeft") && (this.draggingItem || GameFlowManager.main.battlePhase == GameFlowManager.BattlePhase.outOfBattle)) || Input.GetKeyDown("q") || Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			this.RotateAll(90f);
		}
		if ((DigitalCursor.main.GetInputDown("toggleMapAndBackpack") || Input.GetKeyDown("tab")) && !this.draggingItem)
		{
			if (this.mapButton.gameObject.activeInHierarchy)
			{
				this.ShowMap(false);
				return;
			}
			this.ShowInventory();
			return;
		}
		else
		{
			if (DigitalCursor.main.GetInputDown("centerOnBackpack"))
			{
				this.RecenterCursor();
				return;
			}
			if (Input.GetKeyDown("space"))
			{
				this.NextTurnButton();
				return;
			}
			if (!Input.GetKeyDown("right shift") && !DigitalCursor.main.GetInputDown("rotateRight"))
			{
				if (Input.GetKeyDown("left shift") || DigitalCursor.main.GetInputDown("rotateLeft"))
				{
					if (!this.targetedEnemy || this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.playerTurn || this.draggingItem)
					{
						return;
					}
					this.TargetPreviousEnemy(false);
				}
				return;
			}
			if (!this.targetedEnemy || this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.playerTurn || this.draggingItem)
			{
				return;
			}
			this.TargetNextEnemy(false);
			return;
		}
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x00069A2C File Offset: 0x00067C2C
	public void RecenterCursor()
	{
		if (this.mapButton.gameObject.activeInHierarchy)
		{
			DigitalCursor.main.FollowGameElement(GameObject.FindGameObjectWithTag("Inventory").transform, false);
			return;
		}
		DigitalCursor.main.FollowGameElement(Object.FindObjectOfType<DungeonPlayer>().transform, false);
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x00069A7C File Offset: 0x00067C7C
	public void TargetPreviousEnemy(bool tryToIgnoreCharmedEnemies = true)
	{
		Enemy enemy = null;
		Enemy[] array = Object.FindObjectsOfType<Enemy>();
		if (array.Length < 1)
		{
			return;
		}
		if (tryToIgnoreCharmedEnemies)
		{
			foreach (Enemy enemy2 in array)
			{
				if (!(enemy2 == this.targetedEnemy) && !enemy2.dead && enemy2.stats && !enemy2.stats.IsCharmed() && (!this.targetedEnemy || enemy2.transform.position.x < this.targetedEnemy.transform.position.x || this.targetedEnemy.stats.IsCharmed()) && (!enemy || enemy2.transform.position.x > enemy.transform.position.x))
				{
					enemy = enemy2;
				}
			}
		}
		if (enemy == null)
		{
			foreach (Enemy enemy3 in array)
			{
				if (!(enemy3 == this.targetedEnemy) && !enemy3.dead && enemy3.stats && (!this.targetedEnemy || (enemy3.transform.position.x < this.targetedEnemy.transform.position.x && (!enemy || enemy3.transform.position.x > enemy.transform.position.x))))
				{
					enemy = enemy3;
				}
			}
		}
		if (enemy == null)
		{
			foreach (Enemy enemy4 in array)
			{
				if (!(enemy4 == this.targetedEnemy) && !enemy4.dead && (!enemy || enemy4.transform.position.x > enemy.transform.position.x))
				{
					enemy = enemy4;
				}
			}
		}
		if (enemy)
		{
			this.SelectEnemy(enemy);
		}
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x00069C90 File Offset: 0x00067E90
	public void TargetNextEnemy(bool tryToIgnoreCharmedEnemies = true)
	{
		Enemy enemy = null;
		Enemy[] array = Object.FindObjectsOfType<Enemy>();
		if (array.Length < 1)
		{
			return;
		}
		if (tryToIgnoreCharmedEnemies)
		{
			foreach (Enemy enemy2 in array)
			{
				if (!(enemy2 == this.targetedEnemy) && !enemy2.dead && enemy2.stats && !enemy2.stats.IsCharmed() && (!this.targetedEnemy || enemy2.transform.position.x > this.targetedEnemy.transform.position.x || this.targetedEnemy.stats.IsCharmed()) && (!enemy || enemy2.transform.position.x < enemy.transform.position.x))
				{
					enemy = enemy2;
				}
			}
		}
		if (enemy == null)
		{
			foreach (Enemy enemy3 in array)
			{
				if (!(enemy3 == this.targetedEnemy) && !enemy3.dead && enemy3.stats && (!this.targetedEnemy || (enemy3.transform.position.x > this.targetedEnemy.transform.position.x && (!enemy || enemy3.transform.position.x < enemy.transform.position.x))))
				{
					enemy = enemy3;
				}
			}
		}
		if (enemy == null)
		{
			foreach (Enemy enemy4 in array)
			{
				if (!(enemy4 == this.targetedEnemy) && !enemy4.dead && (!enemy || enemy4.transform.position.x < enemy.transform.position.x))
				{
					enemy = enemy4;
				}
			}
		}
		if (enemy)
		{
			this.SelectEnemy(enemy);
		}
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x00069EA4 File Offset: 0x000680A4
	private void Update()
	{
		if (Singleton.Instance.enableTimeout)
		{
			Singleton.Instance.speedRunTime += Time.deltaTime;
			float num = Singleton.Instance.demoTime - Singleton.Instance.speedRunTime;
			num = Mathf.Max(0f, num);
			string text = string.Format("{0:00}:{1:00}", Mathf.Floor(num / 60f), Mathf.Floor(num % 60f));
			this.timerText.text = text;
			if (num < 60f)
			{
				this.timerText.transform.localScale = Vector3.Lerp(Vector3.one * 3f, Vector3.one, num / 60f);
				this.timerText.color = Color.Lerp(Color.red, Color.white, num / 60f);
			}
			if (num <= 0f)
			{
				this.EndDemo();
			}
		}
		else
		{
			this.timerText.gameObject.SetActive(false);
		}
		while (this.viewingEventObject.Count > 0 && (!this.viewingEventObject[0] || !this.viewingEventObject[0].activeInHierarchy))
		{
			this.viewingEventObject.RemoveAt(0);
			if (this.viewingEventObject.Count > 0 && this.viewingEventObject[0])
			{
				this.ViewEvent(this.viewingEventObject[0]);
				this.viewingEventObject.RemoveAt(0);
			}
		}
		if (this.viewingEventObject.Count > 0 && this.viewingEventObject[0])
		{
			this.viewingEvent = true;
		}
		if (this.viewingEventObject.Count == 0 && this.viewingEventThroughObject)
		{
			this.ClearEvent();
		}
		if (Singleton.Instance.rotateButton)
		{
			this.rotateButton.SetActive(true);
		}
		else
		{
			this.rotateButton.SetActive(false);
		}
		if ((Input.GetKey("`") || Input.GetKey(KeyCode.F1)) && Singleton.Instance.developmentMode)
		{
			this.debugTimer += Time.deltaTime;
			if (this.debugTimer > 0.5f && this.tutorialManager.playType != TutorialManager.PlayType.testing)
			{
				this.tutorialManager.playType = TutorialManager.PlayType.testing;
				this.tutorialManager.tutorialSequence = TutorialManager.TutorialSequence.trulyDone;
				Debug.Log("Entered testing mode!");
			}
		}
		else
		{
			this.debugTimer = 0f;
		}
		if (Input.GetKeyDown("l") && !SingleUI.IsViewingPopUp() && Singleton.Instance.isTestingMode)
		{
			this.tutorialManager.playType = TutorialManager.PlayType.testing;
		}
		if (this.tutorialManager.playType == TutorialManager.PlayType.testing && !this.viewingEvent && Singleton.Instance.developmentMode && !SingleUI.IsViewingPopUp())
		{
			this.runTypeManager.SetRunText();
			TextMeshProUGUI textMeshProUGUI = this.runTypeText;
			textMeshProUGUI.text += " DEBUGGING MODE";
			if (Input.GetKeyDown("8"))
			{
				this.CreatePopUp("Test Pop Up!", new Vector2(0f, 650f), 0.4f);
			}
			if (Input.GetKeyDown("9"))
			{
				SaveManager saveManager = Object.FindObjectOfType<SaveManager>();
				saveManager.StartCoroutine(saveManager.Load(Singleton.Instance.saveNumber));
			}
			if (Input.GetKeyDown("r"))
			{
				base.StartCoroutine(this.ShowPromptText(this.promptTrans, 95f));
				this.totalNumOfItemsAllowedToTake = 3;
				this.numOfItemsAllowedToTake = 3;
				this.SpawnRelics();
			}
			if (Input.GetKeyDown("v"))
			{
				GameObject gameObject = this.CreateCursePublic();
				base.StartCoroutine(this.CreateCurseReorg(new List<GameObject> { gameObject }, false));
			}
			if (Input.GetKeyDown("n"))
			{
				GameObject gameObject2 = this.GetItems(Vector2.zero, 1, 1f, 0, false, null)[0];
				base.StartCoroutine(this.StartOverwriteOnlyReorg(new List<GameObject> { gameObject2 }, true));
			}
			if (Input.GetKeyDown("j"))
			{
				Enemy[] array = Object.FindObjectsOfType<Enemy>();
				for (int i = 0; i < array.Length; i++)
				{
					array[i].stats.Attack(null, -999, null, false, false, false);
				}
			}
			if (Input.GetKeyDown("l"))
			{
				if (Singleton.Instance.isTestingMode)
				{
					this.tutorialManager.playType = TutorialManager.PlayType.testing;
				}
				this.ChangeGold(6 + 6 * this.floor);
				base.StopAllCoroutines();
				if (GameManager.main.floor != 0 && GameManager.main.floor <= 8)
				{
					ResourceGainedManager.main.ShowResourcePanels();
				}
				base.StartCoroutine(this.NextLevel(0));
			}
			if (Input.GetKeyDown("o"))
			{
				this.player.stats.AddStatusEffect(StatusEffect.Type.curse, 1f, Item2.Effect.MathematicalType.summative);
			}
			if (Input.GetKeyDown("["))
			{
				this.StartLimitedItemGetPeriod();
			}
			if (Input.GetKeyDown("x"))
			{
				this.GetItems(Vector2.zero, 3, 1f, 0, false, null);
			}
			if (Input.GetKey("k"))
			{
				this.player.AddExperience(3, this.player.transform.position);
			}
			if (Input.GetKey("'"))
			{
				this.player.stats.ChangeHealth(-999, null, false);
			}
			if (!Input.GetKeyDown("0"))
			{
				goto IL_060D;
			}
			using (List<Item2>.Enumerator enumerator = DebugItemManager.main.item2s.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Item2 item = enumerator.Current;
					try
					{
						JObject jobject = ItemExporter.SerializeItem(item, "exportedItems/");
						StreamWriter streamWriter = new StreamWriter("exportedItems/" + new CultureInfo("en-US", false).TextInfo.ToTitleCase(LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(item.name)).ToLower()) + ".item.json", false);
						streamWriter.Write(jobject);
						streamWriter.Close();
					}
					catch (Exception ex)
					{
						Debug.LogError(ex);
					}
				}
				goto IL_060D;
			}
		}
		if (Singleton.Instance.developmentMode)
		{
			this.runTypeManager.SetRunText();
			TextMeshProUGUI textMeshProUGUI2 = this.runTypeText;
			textMeshProUGUI2.text += " Dev MODE";
		}
		IL_060D:
		this.KeyBoardControls();
		if (this.tutorialManager && this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.organizeChest)
		{
			int num2 = 0;
			foreach (Item2 item2 in Item2.allItems)
			{
				if (item2 && item2.itemMovement && item2.itemMovement.inGrid)
				{
					num2++;
				}
			}
			if (num2 >= 3)
			{
				this.ShowBasicButtons(GameManager.ButtonsToEnable.finishReorganizingButton, LangaugeManager.main.GetTextByKey("gm1"), "gm1");
				base.StartCoroutine(this.HidePromptText(this.tutorialText, -240f));
			}
		}
		this.floorText.text = LangaugeManager.main.GetTextByKey("h1") + ":" + this.floor.ToString();
		if (this.reorganizePrompt.activeInHierarchy)
		{
			if (this.reorgnizeItem)
			{
				this.reorgnizeItem.transform.position = new Vector3(this.reorganizePrompt.transform.position.x, this.reorgGlobal.y, this.reorgnizeItem.transform.position.z);
			}
		}
		else if (this.reorgnizeItem && this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.playerTurn)
		{
			Object.Destroy(this.reorgnizeItem);
		}
		if (this.player.stats.health <= 0 && !this.dead && !this.gameFlowManager.isCheckingEffects)
		{
			MetaProgressSaveManager.main.SaveLastRun(MetaProgressSaveManager.LastRun.Result.died);
			this.dead = true;
			foreach (BoxCollider2D boxCollider2D in Object.FindObjectsOfType<BoxCollider2D>())
			{
				if (!boxCollider2D.GetComponentInParent<Item2>())
				{
					boxCollider2D.enabled = false;
				}
			}
			Item2.SetAllItemColors();
			if (Singleton.Instance.ironMan)
			{
				SaveManager.DeleteSave(Singleton.Instance.saveNumber);
			}
			if (Singleton.Instance.twitchIntegrationEnabled && Singleton.Instance.twitchEnableDeathCounter)
			{
				TwitchManager.Instance.deathCounter.OnDeath();
			}
			Item2.DestroyAllItemsOutsideGrid();
			foreach (Item2 item3 in Item2.GetItemsOfTypes(Item2.ItemType.Fragment, Item2.allItems))
			{
				if (item3 && item3.itemMovement)
				{
					item3.itemMovement.DelayDestroy();
				}
			}
			if (Singleton.Instance.storyMode)
			{
				Object.Instantiate<GameObject>(this.selectItemOnDeadPrefab, GameObject.FindGameObjectWithTag("FrontCanvas").transform);
			}
			this.EndRunAnalytics("died");
			this.FinishLimitedItemSelection();
			this.fadeOut.SetActive(true);
			this.fadeOut.GetComponent<Animator>().Play("fadeIn");
			this.fadeOut.transform.SetAsLastSibling();
			this.playerAnimator.Play("Player_Die");
			this.retryButton.SetActive(true);
			this.retryButton.transform.SetAsLastSibling();
			SoundManager.main.FadeOutAllSongs(0f);
			SoundManager.main.PlaySFX("player_die");
			base.StartCoroutine(SoundManager.main.PlaySongSudden2("game_over", 2f, false, 0f));
			SpriteRenderer[] componentsInChildren = this.inventoryTransform.GetComponentsInChildren<SpriteRenderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].sortingOrder = 2;
			}
			TextMeshPro[] componentsInChildren2 = this.inventoryTransform.GetComponentsInChildren<TextMeshPro>();
			for (int i = 0; i < componentsInChildren2.Length; i++)
			{
				componentsInChildren2[i].sortingOrder = 2;
			}
			base.StartCoroutine(this.MoveOverTime(this.inventoryTransform, new Vector3(0f, 0f, this.inventoryTransform.position.z), 0.3f));
			this.inventoryPhase = GameManager.InventoryPhase.locked;
		}
		if (this.viewingTop == GameManager.ViewingTop.map)
		{
			this.mapButton.SetActive(false);
			this.inventoryButton.SetActive(true);
		}
		else if (this.viewingTop == GameManager.ViewingTop.inventory)
		{
			this.mapButton.SetActive(true);
			this.inventoryButton.SetActive(false);
		}
		if (this.targetedEnemy)
		{
			this.targetTransform.gameObject.SetActive(true);
			this.targetTransform.position = this.targetedEnemy.transform.position + Vector3.back;
		}
		if (this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.playerTurn && !this.targetedEnemy)
		{
			this.targetTransform.gameObject.SetActive(false);
			this.TargetNextEnemy(true);
		}
		if (this.targetTransform && this.targetedEnemy && this.targetedEnemy.dead)
		{
			this.targetTransform.gameObject.SetActive(false);
		}
		if (this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.outOfBattle)
		{
			this.targetTransform.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x0006AA04 File Offset: 0x00068C04
	public void EndRunAnalytics(string result)
	{
		if (Singleton.Instance.analyticsSent)
		{
			return;
		}
		AnalyticsManager analyticsManager = Object.FindObjectOfType<AnalyticsManager>();
		if (analyticsManager)
		{
			this.settings.path = "bphRun" + Singleton.Instance.saveNumber.ToString() + ".sav";
			ES3.Save<bool>("analyticsSent", true, this.settings);
			analyticsManager.AddItem("versionNumber", Object.FindObjectOfType<TutorialManager>().versionTextString);
			analyticsManager.AddItem("level", this.player.level.ToString());
			analyticsManager.AddItem("character", this.player.characterName.ToString());
			if (Singleton.Instance.runType)
			{
				analyticsManager.AddItem("runType", Singleton.Instance.runType.name);
			}
			analyticsManager.AddItem("runOutcome", result);
			analyticsManager.AddItem("zoneName", this.dungeonLevel.zone.ToString());
			analyticsManager.AddItem("zoneFloor", this.dungeonLevel.currentFloor.ToString());
			foreach (Item2 item in Item2.GetAllItemsInGrid())
			{
				analyticsManager.AddItem("itemsHeld", Item2.GetDisplayName(item.name));
			}
			if (result == "died")
			{
				analyticsManager.AddItem("diedWithEnemies", this.currentEnemyEncounter);
			}
			analyticsManager.FlushGameData();
		}
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x0006ABAC File Offset: 0x00068DAC
	public IEnumerator ShowFadeOut()
	{
		this.fadeOut.SetActive(true);
		this.fadeOut.GetComponent<Animator>().enabled = false;
		this.fadeOut.GetComponent<Image>().color = new Color(0f, 0f, 0f, 1f);
		yield return new WaitForSeconds(2f);
		this.fadeOut.GetComponent<Animator>().enabled = true;
		this.fadeOut.GetComponent<Animator>().Play("fadeOut");
		yield return new WaitForSeconds(1f);
		this.fadeOut.SetActive(false);
		yield break;
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x0006ABBC File Offset: 0x00068DBC
	public bool EndBattle()
	{
		if (GameFlowManager.main.combatEndedAbruptly)
		{
			return false;
		}
		ActionButtonManager actionButtonManager = Object.FindObjectOfType<ActionButtonManager>();
		if (actionButtonManager)
		{
			actionButtonManager.DisableBattleButtons();
		}
		Card.RemoveAllCursorCards();
		bool flag = false;
		this.buttonPressed = false;
		this.announcementAnimator.gameObject.SetActive(true);
		this.announcementAnimator.Play("bigAnnouncement", 0, 0f);
		this.announcementAnimator.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("gm12");
		this.afterBattleButton.SetActive(true);
		this.inventoryPhase = GameManager.InventoryPhase.notInteractable;
		if (this.dungeonLevel.currentFloor == DungeonLevel.Floor.boss && ((this.zoneNumber == 0 && RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.runEndsAfterZone1)) || (this.zoneNumber == 1 && RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.runEndsAfterZone2)) || (this.zoneNumber == 2 && RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.runEndsAfterZone3)) || this.dungeonLevel.zone == DungeonLevel.Zone.Chaos))
		{
			this.afterBattleButton.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("b7");
		}
		else if (this.player.level + 1 < this.player.chosenCharacter.levelUps.Count && (this.player.chosenCharacter.levelUps[this.player.level + 1].demoEnabled || !Singleton.Instance.isDemo) && this.player.experience >= this.player.chosenCharacter.levelUps[this.player.level + 1].xpRequired)
		{
			this.afterBattleButton.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("gm13");
			flag = true;
		}
		else if (this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.cr8battle1 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.cr8battle2 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.cr8battle3 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.cr8battle4 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.cr8battle5 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.cr8battle6 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.cr8battle7 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.cr8battle8 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.toteBattle1 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.toteBattle2 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.toteBattle3 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.toteBattle4 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.toteChest)
		{
			this.afterBattleButton.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("gm14b");
		}
		if (flag)
		{
			SoundManager.main.MuteAllSongs();
			SoundManager.main.PlaySongSudden("LevelUp", 0f, 0f, true);
		}
		else if (this.dungeonLevel.currentFloor == DungeonLevel.Floor.boss && !this.EnemiesInDungeon())
		{
			SoundManager.main.PlayOrContinueSong("bph_game_roaming_after_boss", 0f);
		}
		else
		{
			SoundManager.main.PlayOrContinueSong(this.dungeonLevel.levelSong);
		}
		return flag;
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x0006AEE8 File Offset: 0x000690E8
	public bool EnemiesInDungeon()
	{
		foreach (DungeonEvent dungeonEvent in Object.FindObjectsOfType<DungeonEvent>())
		{
			if (!dungeonEvent.IsFinished() && (dungeonEvent.dungeonEventType == DungeonEvent.DungeonEventType.Enemy || dungeonEvent.dungeonEventType == DungeonEvent.DungeonEventType.Elite))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x0006AF29 File Offset: 0x00069129
	public void StartCombatUI()
	{
		this.mapInputHandler.ChangeKeys(InputHandler.Key.Select, new List<InputHandler.Key>());
		this.inventoryInputHandler.ChangeKeys(InputHandler.Key.Select, new List<InputHandler.Key>());
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x0006AF50 File Offset: 0x00069150
	public void EndCombatUI()
	{
		this.mapInputHandler.ChangeKeys(InputHandler.Key.Y, new List<InputHandler.Key> { InputHandler.Key.Select });
		this.inventoryInputHandler.ChangeKeys(InputHandler.Key.Y, new List<InputHandler.Key> { InputHandler.Key.Select });
		this.targetTransform.gameObject.SetActive(false);
		if (this.endTurnButton.activeInHierarchy)
		{
			this.endTurnButton.GetComponent<Animator>().Play("Out");
		}
		this.StopRepeatActionButton();
		this.RemoveReorganizeItem();
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x0006AFD0 File Offset: 0x000691D0
	public void RemoveAllHazards()
	{
		foreach (Item2 item in Item2.allItems)
		{
			if (item && item.itemMovement && !item.destroyed && (item.itemType.Contains(Item2.ItemType.Hazard) || item.itemType.Contains(Item2.ItemType.Blessing)))
			{
				item.itemMovement.DelayDestroy();
			}
		}
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x0006B064 File Offset: 0x00069264
	public void MoveAllItems()
	{
		if (this.moveAllItemsCoroutine != null)
		{
			base.StopCoroutine(this.moveAllItemsCoroutine);
		}
		this.moveAllItemsCoroutine = base.StartCoroutine(this.MoveItems());
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x0006B08C File Offset: 0x0006928C
	private IEnumerator MoveItems()
	{
		yield return new WaitForEndOfFrame();
		ItemMovement[] array = Object.FindObjectsOfType<ItemMovement>();
		foreach (ItemMovement itemMovement in array)
		{
			if (itemMovement && !itemMovement.inGrid && (!this.reorgnizeItem || !(itemMovement == this.reorgnizeItem.GetComponent<ItemMovement>())) && (!itemMovement.GetComponent<Carving>() || this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.outOfBattle) && !itemMovement.isDragging && !itemMovement.returnsToOutOfInventoryPosition)
			{
				SpriteRenderer component = itemMovement.GetComponent<SpriteRenderer>();
				if (component)
				{
					component.enabled = false;
				}
				itemMovement.StartCoroutine(itemMovement.Move(itemMovement.transform.position, 0));
				yield return new WaitForEndOfFrame();
				yield return new WaitForFixedUpdate();
			}
		}
		ItemMovement[] array2 = null;
		yield break;
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x0006B09C File Offset: 0x0006929C
	public List<GameObject> GetItems(Vector2 spawnPosition, int numOfItems, float rarity, int minRarity, bool store, List<Item2.ItemType> excludingItemTypes = null)
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < numOfItems; i++)
		{
			GameObject gameObject = this.SpawnItem(spawnPosition, rarity, minRarity, store, excludingItemTypes);
			list.Add(gameObject);
		}
		return list;
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x0006B0D4 File Offset: 0x000692D4
	public GameObject SpawnCurse()
	{
		GameObject gameObject = this.SpawnCurseOrBlessing(Random.Range(0, 4), Curse.Type.Curse);
		Curse component = gameObject.GetComponent<Curse>();
		if (component)
		{
			component.Setup();
		}
		return gameObject;
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x0006B104 File Offset: 0x00069304
	public GameObject SpawnItemOfType(Item2.ItemType type)
	{
		bool flag;
		Item2.Rarity rarity = this.ChooseRarity(out flag, 0f, true);
		this.ShowGotLucky(this.spawnPosition, flag);
		List<Item2> itemsOfRarities = Item2.GetItemsOfRarities(new List<Item2.Rarity> { rarity }, this.itemsToSpawn);
		return Object.Instantiate<GameObject>(Item2.ChooseRandomFromList(Item2.GetItemOfType(type, itemsOfRarities), true).gameObject, Vector2.one * -999f, Quaternion.identity);
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x0006B178 File Offset: 0x00069378
	public List<GameObject> SpawnItemsOfType(Item2.ItemType type, int num)
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < num; i++)
		{
			GameObject gameObject = this.SpawnItemOfType(type);
			float num2 = ((float)i - (float)(num - 1) / 2f) * 1.5f;
			gameObject.transform.position = new Vector3(num2, -5f, 0f);
			ItemMovement component = gameObject.GetComponent<ItemMovement>();
			component.outOfInventoryPosition = gameObject.transform.localPosition;
			component.outOfInventoryRotation = Quaternion.identity;
			component.returnsToOutOfInventoryPosition = true;
			list.Add(gameObject);
		}
		return list;
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x0006B200 File Offset: 0x00069400
	public GameObject SpawnCurseOrBlessing(int size, Curse.Type curseOrBlessing = Curse.Type.Curse)
	{
		List<Item2> list = new List<Item2>();
		if (curseOrBlessing == Curse.Type.Curse)
		{
			list = ItemSpawner.GetItems(1, new List<Item2.ItemType> { Item2.ItemType.Curse }, false, true);
		}
		else if (curseOrBlessing == Curse.Type.Blessing)
		{
			list = ItemSpawner.GetItems(1, new List<Item2.ItemType> { Item2.ItemType.Blessing }, false, true);
		}
		return ItemSpawner.InstantiateItemFree(list);
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x0006B250 File Offset: 0x00069450
	public GameObject CreateCursePublic()
	{
		List<GameObject> list = new List<GameObject>();
		return this.CreateCursePublic(list);
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x0006B26C File Offset: 0x0006946C
	public GameObject CreateCursePublic(List<GameObject> curses)
	{
		GameObject gameObject;
		if (curses.Count == 0)
		{
			gameObject = this.SpawnCurseOrBlessing(1, Curse.Type.Curse);
		}
		else
		{
			gameObject = Object.Instantiate<GameObject>(curses[Random.Range(0, curses.Count)], Vector3.zero, Quaternion.identity);
		}
		gameObject.transform.position = base.transform.position;
		gameObject.GetComponent<Curse>();
		return gameObject;
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x0006B2CE File Offset: 0x000694CE
	public IEnumerator StartOverwriteOnlyReorg(List<GameObject> itemsThatMustBePlaced, bool anchorItem)
	{
		yield return new WaitForEndOfFrame();
		this.anchorItemsAfterSpecialReorganization = anchorItem;
		foreach (GameObject gameObject in itemsThatMustBePlaced)
		{
			ItemMovement component = gameObject.GetComponent<ItemMovement>();
			if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.cursor)
			{
				component.StartCoroutine(component.Move(new Vector2(0f, 2.4f), 0));
			}
			component.mousePreview.gameObject.SetActive(true);
			component.mousePreview.GetComponent<SpriteRenderer>().sortingOrder = 2;
		}
		List<Vector2> list = new List<Vector2>();
		this.areasForSpecialReorganizations = new List<GameManager.AreasForSpecialReorganization>();
		List<GameObject> list2 = GameObject.FindGameObjectsWithTag("GridParent").ToList<GameObject>();
		list2.AddRange(GameObject.FindGameObjectsWithTag("PetGridParent").ToList<GameObject>());
		foreach (GameObject gameObject2 in list2)
		{
			list = new List<Vector2>();
			foreach (GridSquare gridSquare in gameObject2.GetComponentsInChildren<GridSquare>())
			{
				list.Add(new Vector2(gridSquare.transform.localPosition.x, gridSquare.transform.localPosition.y));
			}
			this.EnterSpecialOrganization(itemsThatMustBePlaced, list, false, gameObject2.transform);
		}
		yield return new WaitForEndOfFrame();
		Item2.SetAllItemColors();
		this.MoveAllItems();
		yield break;
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x0006B2EC File Offset: 0x000694EC
	private void MoveItemTowardsGrid(ItemMovement im)
	{
		Vector2 vector = im.transform.position;
		foreach (RaycastHit2D raycastHit2D in Physics2D.RaycastAll(vector, (this.itemsParent.position - vector).normalized, 1000f))
		{
			if (raycastHit2D.collider.CompareTag("GridSquare") || (raycastHit2D.collider.CompareTag("Item") && raycastHit2D.collider.gameObject != im.gameObject))
			{
				ItemMovement component = raycastHit2D.collider.GetComponent<ItemMovement>();
				if (!component || component.inGrid)
				{
					Vector2 vector2 = new Vector2(raycastHit2D.collider.transform.position.x, raycastHit2D.collider.transform.position.y);
					vector2 -= (vector2 - vector).normalized * 1.5f;
					im.transform.position = new Vector3(vector2.x, vector2.y, 0f);
					im.transform.localPosition = new Vector3(im.transform.localPosition.x, im.transform.localPosition.y, -1f);
					im.mousePreview.position = vector;
					im.StartCoroutine(im.MoveOverTime(16f));
					return;
				}
			}
		}
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x0006B491 File Offset: 0x00069691
	public IEnumerator CreateCurseReorg(List<GameObject> curses, bool skippable = false)
	{
		this.tutorialManager.ConsiderTutorial("hazards");
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		if (!this.itemsParent)
		{
			this.itemsParent = GameObject.Find("ItemParent").transform;
		}
		foreach (GameObject gameObject in curses)
		{
			ItemMovement component = gameObject.GetComponent<ItemMovement>();
			component.mousePreview.gameObject.SetActive(true);
			component.mousePreview.GetComponent<SpriteRenderer>().sortingOrder = 2;
			gameObject.transform.SetParent(this.itemsParent, true);
		}
		List<Vector2> list = new List<Vector2>();
		this.areasForSpecialReorganizations = new List<GameManager.AreasForSpecialReorganization>();
		List<GameObject> list2 = GameObject.FindGameObjectsWithTag("GridParent").ToList<GameObject>();
		list2.AddRange(GameObject.FindGameObjectsWithTag("PetGridParent").ToList<GameObject>());
		foreach (GameObject gameObject2 in list2)
		{
			list = new List<Vector2>();
			foreach (GridSquare gridSquare in gameObject2.GetComponentsInChildren<GridSquare>())
			{
				list.Add(new Vector2(gridSquare.transform.localPosition.x, gridSquare.transform.localPosition.y));
			}
			this.EnterSpecialOrganization(curses, list, skippable, gameObject2.transform);
		}
		yield return new WaitForEndOfFrame();
		Item2.SetAllItemColors();
		yield break;
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x0006B4B0 File Offset: 0x000696B0
	public GameObject SpawnItemOfSize(int sizeNeeded)
	{
		List<Item2> list = new List<Item2>(this.itemsToSpawn);
		list = this.ItemsValidToSpawn(list, false);
		List<Item2> list2 = new List<Item2>();
		foreach (Item2 item in list)
		{
			ItemMovement component = item.GetComponent<ItemMovement>();
			if (component && component.GetSpacesNeeded() == sizeNeeded)
			{
				list2.Add(item);
			}
		}
		bool flag;
		Item2.Rarity rarity = this.ChooseRarity(out flag, 0f, true);
		List<Item2> itemsOfRarities = Item2.GetItemsOfRarities(new List<Item2.Rarity> { rarity }, list2);
		if (itemsOfRarities.Count >= 1)
		{
			Item2 item2 = Item2.ChooseRandomFromList(itemsOfRarities, true);
			if (item2)
			{
				return Object.Instantiate<GameObject>(item2.gameObject, Vector3.zero, Quaternion.identity);
			}
		}
		if (list2.Count >= 1)
		{
			Item2 item3 = Item2.ChooseRandomFromList(list2, true);
			if (item3)
			{
				return Object.Instantiate<GameObject>(item3.gameObject, Vector3.zero, Quaternion.identity);
			}
		}
		this.ShowGotLucky(Vector3.zero, flag);
		return Item2.ChooseRandomFromList(list, true).gameObject;
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x0006B5E0 File Offset: 0x000697E0
	public GameObject SpawnItem(List<Item2.ItemType> itemTypes, List<Item2.Rarity> itemRarities, bool store = false, List<Item2.ItemType> excludingItemTypes = null)
	{
		List<Item2> list = new List<Item2>(this.itemsToSpawn);
		list = this.ItemsValidToSpawn(list, store);
		if (itemTypes.Count > 0 && !itemTypes.Contains(Item2.ItemType.Any))
		{
			list = Item2.GetItemsOfTypes(itemTypes, list);
		}
		if (itemRarities.Count > 0)
		{
			list = Item2.GetItemsOfRarities(itemRarities, list);
		}
		if (excludingItemTypes != null && excludingItemTypes.Count > 0)
		{
			list = Item2.RemoveItemsOfTypes(excludingItemTypes, list);
		}
		List<Item2.CategoryClass> list2 = Item2.BuildCategoryClassesFromItemsInBag();
		return Object.Instantiate<GameObject>(Item2.ChooseRandomFromListConsideringLuckGroup(list, list2).gameObject, Vector3.zero, Quaternion.identity);
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x0006B668 File Offset: 0x00069868
	public GameObject SpawnItemStore(List<Item2.ItemType> itemTypes, List<Item2.Rarity> itemRarities)
	{
		List<Item2> list = new List<Item2>(this.itemsToSpawn);
		list = this.ItemsValidToSpawn(list, false);
		if (itemTypes.Count > 0 && !itemTypes.Contains(Item2.ItemType.Any))
		{
			list = Item2.GetItemsOfTypes(itemTypes, list);
		}
		if (itemRarities.Count > 0)
		{
			list = Item2.GetItemsOfRarities(itemRarities, list);
		}
		return Object.Instantiate<GameObject>(Item2.ChooseRandomFromList(list, true).gameObject, Vector3.zero, Quaternion.identity);
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x0006B6D0 File Offset: 0x000698D0
	public GameObject SpawnItemNoStacks(List<Item2.ItemType> itemTypes, List<Item2.Rarity> itemRarities)
	{
		return this.SpawnItem(itemTypes, itemRarities, false, null);
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x0006B6DC File Offset: 0x000698DC
	public Item2.Rarity ChooseRarity(out bool gotLucky, float rarityBonus = 0f, bool effectedByLuck = true)
	{
		return this.ChooseRarityWithList(new List<Item2.Rarity>
		{
			Item2.Rarity.Common,
			Item2.Rarity.Uncommon,
			Item2.Rarity.Rare,
			Item2.Rarity.Legendary
		}, out gotLucky, rarityBonus, effectedByLuck);
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x0006B708 File Offset: 0x00069908
	public Item2.Rarity ChooseRarityWithList(List<Item2.Rarity> allowedRarities, out bool gotLucky, float rarityBonus = 0f, bool effectedByLuck = true)
	{
		gotLucky = false;
		if (allowedRarities.Count == 0)
		{
			allowedRarities = new List<Item2.Rarity>
			{
				Item2.Rarity.Common,
				Item2.Rarity.Uncommon,
				Item2.Rarity.Rare,
				Item2.Rarity.Legendary
			};
		}
		if (allowedRarities.Count == 1)
		{
			return allowedRarities[0];
		}
		float[] array = new float[] { 0f, 95f, 101.5f, 105.65f, 999f };
		float num = 1f;
		if (this.zoneNumber == 2 || this.floor > 9)
		{
			num = 2.2f;
		}
		else if (this.zoneNumber == 1)
		{
			num = 1.5f;
		}
		else if (this.zoneNumber == 0)
		{
			num = 0.9f;
		}
		if (Singleton.Instance.isDemo)
		{
			array = new float[] { 0f, 78f, 90f, 100f, 999f };
		}
		if (Item2.GetItemWithStatusEffect(Item2.ItemStatusEffect.Type.pauperRun, null, false))
		{
			allowedRarities.Remove(Item2.Rarity.Legendary);
			allowedRarities.Remove(Item2.Rarity.Rare);
		}
		float num2 = (float)Random.Range(0, 100);
		Item2.Rarity rarity = Item2.Rarity.Legendary;
		if (effectedByLuck)
		{
			float num3 = 1f * num;
			RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.increaseLuck);
			if (runProperty != null)
			{
				num3 += (float)(runProperty.value / 100);
			}
			if (allowedRarities.Contains(Item2.Rarity.Uncommon))
			{
				this.player.uncommonLuck += 1f * num3;
				array[1] -= this.player.uncommonLuck;
				array[1] -= rarityBonus * 2.75f;
			}
			if (allowedRarities.Contains(Item2.Rarity.Rare))
			{
				this.player.rareLuck += 0.5f * num3;
				array[2] -= this.player.rareLuck;
				array[2] -= rarityBonus * 1.2f;
			}
			if (allowedRarities.Contains(Item2.Rarity.Legendary))
			{
				this.player.legendaryLuck += 0.15f * num3;
				array[3] -= this.player.legendaryLuck;
				array[3] -= rarityBonus * 0.25f;
			}
			rarity = this.GetRarity(array, num2, false, allowedRarities);
			if (allowedRarities.Contains(Item2.Rarity.Uncommon))
			{
				array[1] -= this.player.uncommonLuckFromItems;
			}
			if (allowedRarities.Contains(Item2.Rarity.Rare))
			{
				array[2] -= this.player.rareLuckFromItems;
			}
			if (allowedRarities.Contains(Item2.Rarity.Legendary))
			{
				array[3] -= this.player.legendaryLuckFromItems;
			}
		}
		if (this.floor % this.floorsPerLevel == 0 && this.floor != 0)
		{
			num2 += 2.5f;
		}
		num2 = Mathf.Clamp(num2, 0f, 100f);
		Item2.Rarity rarity2 = this.GetRarity(array, num2, effectedByLuck, allowedRarities);
		if (rarity2 > rarity)
		{
			gotLucky = true;
		}
		return rarity2;
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x0006B9A8 File Offset: 0x00069BA8
	private Item2.Rarity GetRarity(float[] randomRange, float randomNum, bool effectedByLuck, List<Item2.Rarity> allowedRarities)
	{
		if (randomNum >= randomRange[3] && allowedRarities.Contains(Item2.Rarity.Legendary))
		{
			if (effectedByLuck)
			{
				this.player.legendaryLuck = 0f;
				this.player.legendaryLuckFromItems = Mathf.Clamp(this.player.legendaryLuckFromItems - 50f, 0f, 100f);
			}
			return Item2.Rarity.Legendary;
		}
		if (randomNum >= randomRange[2] && allowedRarities.Contains(Item2.Rarity.Rare))
		{
			if (effectedByLuck)
			{
				this.player.rareLuck = 0f;
				this.player.rareLuckFromItems = Mathf.Clamp(this.player.rareLuckFromItems - 50f, 0f, 100f);
			}
			return Item2.Rarity.Rare;
		}
		if (randomNum >= randomRange[1] && allowedRarities.Contains(Item2.Rarity.Uncommon))
		{
			if (effectedByLuck)
			{
				this.player.uncommonLuck = 0f;
				this.player.uncommonLuckFromItems = Mathf.Clamp(this.player.uncommonLuckFromItems - 50f, 0f, 100f);
			}
			return Item2.Rarity.Uncommon;
		}
		return Item2.Rarity.Common;
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x0006BAA8 File Offset: 0x00069CA8
	public void ShowGotLucky(Transform item, bool gotLucky)
	{
		if (!gotLucky || !item)
		{
			return;
		}
		Object.Instantiate<GameObject>(this.luckyMessagePrefab, item.position, Quaternion.identity, GameObject.FindGameObjectWithTag("UI Canvas").transform).GetComponent<Follow>().follow = item.transform;
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x0006BAF6 File Offset: 0x00069CF6
	private void ShowGotLucky(Vector2 position, bool gotLucky)
	{
		if (!gotLucky)
		{
			return;
		}
		Object.Instantiate<GameObject>(this.luckyMessagePrefab, position, Quaternion.identity, GameObject.FindGameObjectWithTag("UI Canvas").transform);
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x0006BB24 File Offset: 0x00069D24
	public GameObject SpawnItem(Vector2 spawnPosition, float rarityBonus, int minRarity, bool store, List<Item2.ItemType> excludingItemTypes = null)
	{
		bool flag;
		Item2.Rarity rarity = this.ChooseRarity(out flag, rarityBonus, !store);
		GameObject gameObject = this.SpawnItem(new List<Item2.ItemType> { Item2.ItemType.Any }, new List<Item2.Rarity> { rarity }, store, excludingItemTypes);
		gameObject.transform.position = spawnPosition + Vector3.back;
		this.ShowGotLucky(spawnPosition, flag);
		if (store)
		{
			gameObject.GetComponent<Item2>().isForSale = true;
		}
		return gameObject;
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x0006BB9C File Offset: 0x00069D9C
	public int GetCurrentMana()
	{
		foreach (Item2 item in Object.FindObjectsOfType<Item2>())
		{
			if (item.itemType.Contains(Item2.ItemType.Mana))
			{
				return item.GetComponent<StackableItem>().amount;
			}
		}
		return 0;
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x0006BBE0 File Offset: 0x00069DE0
	public void ChangeMana(int amount)
	{
		foreach (Item2 item in Object.FindObjectsOfType<Item2>())
		{
			if (item.itemType.Contains(Item2.ItemType.Mana))
			{
				item.GetComponent<StackableItem>().ChangeAmount(amount);
				return;
			}
		}
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x0006BC21 File Offset: 0x00069E21
	public int GetCurrentGold()
	{
		return this.goldAmount;
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x0006BC29 File Offset: 0x00069E29
	public void OpenSelectItemDialogue(string name, List<Item2> objs)
	{
		SelectItemDialogue component = Object.Instantiate<GameObject>(this.selectItemDialoguePrefab, GameObject.FindGameObjectWithTag("FrontCanvas").transform).GetComponent<SelectItemDialogue>();
		component.SetupName(name);
		component.SetupItems(objs, false);
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x0006BC58 File Offset: 0x00069E58
	public void ChangeCurse(int amount)
	{
		if (amount > 0)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.curseIndicatorPrefab, new Vector3(this.curseText.transform.position.x, this.curseText.transform.position.y, -6f), Quaternion.identity, GameObject.FindGameObjectWithTag("UI Canvas").GetComponent<Canvas>().transform);
			gameObject.GetComponent<TextMeshProUGUI>().text = "+" + Mathf.Abs(amount).ToString();
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -0.9f);
		}
		else if (amount < 0)
		{
			GameObject gameObject2 = Object.Instantiate<GameObject>(this.curseIndicatorPrefab, new Vector3(this.curseText.transform.position.x, this.curseText.transform.position.y, -6f), Quaternion.identity, GameObject.FindGameObjectWithTag("UI Canvas").GetComponent<Canvas>().transform);
			gameObject2.GetComponent<TextMeshProUGUI>().text = "-" + Mathf.Abs(amount).ToString();
			gameObject2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -0.9f);
		}
		if (CurseManager.Instance.CursesStored() > 0)
		{
			this.curseText.transform.parent.gameObject.SetActive(true);
			this.curseText.text = CurseManager.Instance.CursesStored().ToString();
			return;
		}
		this.curseText.transform.parent.gameObject.SetActive(false);
	}

	// Token: 0x06000A90 RID: 2704 RVA: 0x0006BE04 File Offset: 0x0006A004
	public void ChangeGold(int amount)
	{
		if (amount > 0 && Item2.GetItemWithStatusEffect(Item2.ItemStatusEffect.Type.cannotGainGold, null, true))
		{
			return;
		}
		if (amount != 0)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.goldIndicatorPrefab, new Vector3(this.goldText.transform.position.x, this.goldText.transform.position.y, -6f), Quaternion.identity, GameObject.FindGameObjectWithTag("UI Canvas").GetComponent<Canvas>().transform);
			if (amount > 0)
			{
				gameObject.GetComponent<TextMeshProUGUI>().text = "+" + Mathf.Abs(amount).ToString();
			}
			else
			{
				gameObject.GetComponent<TextMeshProUGUI>().text = "-" + Mathf.Abs(amount).ToString();
			}
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -0.9f);
			SoundManager.main.PlaySFX("gold");
		}
		this.goldAmount += amount;
		this.goldAmount = Mathf.Max(this.goldAmount, 0);
		this.goldText.text = this.goldAmount.ToString() ?? "";
	}

	// Token: 0x06000A91 RID: 2705 RVA: 0x0006BF3C File Offset: 0x0006A13C
	public void AddParticles(Vector3 position, SpriteRenderer spriteRenderer, GameObject particles = null)
	{
		if (!spriteRenderer || !spriteRenderer.sprite || !spriteRenderer.sprite.texture)
		{
			return;
		}
		if (!particles)
		{
			particles = this.itemDestroyParticlePrefab;
		}
		GameObject gameObject = Object.Instantiate<GameObject>(particles, position, spriteRenderer.transform.rotation, this.itemsParent);
		ParticleSystem component = gameObject.GetComponent<ParticleSystem>();
		ParticleSystem.MainModule mainModule = component.main;
		gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder = Mathf.Max(spriteRenderer.sortingOrder, 2);
		ParticleSystem.ShapeModule shape = component.shape;
		shape.sprite = spriteRenderer.sprite;
		shape.texture = spriteRenderer.sprite.texture;
		mainModule.startColor = spriteRenderer.color;
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x0006BFF5 File Offset: 0x0006A1F5
	public void EndChooseItem()
	{
		GameFlowManager.main.DeselectItem();
		if (GameFlowManager.main.battlePhase == GameFlowManager.BattlePhase.outOfBattle || this.inSpecialReorg)
		{
			GameManager.main.inventoryPhase = GameManager.InventoryPhase.open;
			return;
		}
		GameManager.main.inventoryPhase = GameManager.InventoryPhase.locked;
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x0006C02C File Offset: 0x0006A22C
	public void HideBackpackAndMapButtons()
	{
		this.mapandInventoryButtons.SetActive(false);
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x0006C03A File Offset: 0x0006A23A
	public void ShowBackpackAndMapButtons()
	{
		this.mapandInventoryButtons.SetActive(true);
		this.mapButton.AddComponent<PulseImage>();
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x0006C054 File Offset: 0x0006A254
	public void MoveInventoryUpTop()
	{
		this.inventoryTransform.position = new Vector3(this.inventoryTransform.position.x, 8.5f, this.inventoryTransform.position.z);
		this.HideBackpackAndMapButtons();
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x0006C091 File Offset: 0x0006A291
	public void MoveInventoryDown()
	{
		base.StartCoroutine(this.MoveInventoryDownCoroutine());
		this.ShowBackpackAndMapButtons();
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x0006C0A6 File Offset: 0x0006A2A6
	private IEnumerator MoveInventoryDownCoroutine()
	{
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime * 0.25f;
			this.inventoryTransform.position = Vector3.Lerp(this.inventoryTransform.position, new Vector3(this.inventoryTransform.position.x, 2.65f, this.inventoryTransform.position.z), t);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x0006C0B8 File Offset: 0x0006A2B8
	public void ShowInventory()
	{
		if (this.moving != null)
		{
			return;
		}
		if (this.saveManager.isSavingOrLoading && this.saveManager.isUnstableState)
		{
			return;
		}
		if ((this.tutorialManager && this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.openingMessage) || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.moveToChest)
		{
			return;
		}
		if (this.travelling)
		{
			return;
		}
		this.EndChooseItem();
		if (this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.outOfBattle)
		{
			this.playerAnimator.Play("Player_Idle");
		}
		else
		{
			this.playerAnimator.Play("Player_Idle");
			if (this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.playerTurn)
			{
				this.ShowBasicButtons(GameManager.ButtonsToEnable.endTurnButton);
				if (this.endTurnButton)
				{
					InputHandler componentInChildren = this.endTurnButton.GetComponentInChildren<InputHandler>();
					if (componentInChildren)
					{
						componentInChildren.enabled = true;
					}
				}
				this.battleActionButtons.SetActive(true);
				if (this.battleActionButtons)
				{
					Animator componentInChildren2 = this.battleActionButtons.GetComponentInChildren<Animator>();
					if (componentInChildren2)
					{
						componentInChildren2.Play("BattleActionButtonsIn", 0, 0f);
					}
				}
				if (this.reorgnizeItem)
				{
					this.reorganizePrompt.SetActive(true);
					if (this.reorganizePrompt)
					{
						Animator componentInChildren3 = this.reorganizePrompt.GetComponentInChildren<Animator>();
						if (componentInChildren3)
						{
							componentInChildren3.Play("ReorganizeIn", 0, 0f);
						}
					}
				}
			}
		}
		this.moving = base.StartCoroutine(this.MoveOverTime(this.inventoryTransform, new Vector3(0f, this.inventoryStartPosition.y, this.inventoryTransform.position.z), 0.25f));
		base.StartCoroutine(this.MoveOverTime(this.mapTransform, new Vector3(25f, this.mapTransform.position.y, this.mapTransform.position.z), 0.35f));
		base.StartCoroutine(this.SetAlphaOverTime(this.inventoryTransform, Object.FindObjectOfType<ModularBackpack>().GetComponentInChildren<SpriteRenderer>().color.a, 1f, 0.25f));
		this.viewingTop = GameManager.ViewingTop.inventory;
		Transform transform = GameObject.FindGameObjectWithTag("Inventory").transform;
		DigitalCursor.main.MoveToPositionInBackpack();
		DigitalCursor.main.UpdateContextControls();
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x0006C300 File Offset: 0x0006A500
	public void ShowInventoryInstant()
	{
		if (this.moving != null)
		{
			base.StopCoroutine(this.moving);
		}
		this.inventoryTransform.transform.position = new Vector3(0f, this.inventoryStartPosition.y, this.inventoryTransform.position.z);
		this.mapTransform.transform.position = new Vector3(25f, this.mapTransform.position.y, this.mapTransform.position.z);
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x0006C390 File Offset: 0x0006A590
	public void ShowMap(bool override1 = false)
	{
		if (this.startMusicOnMap)
		{
			SoundManager.main.PlayOrContinueSong("roaming_1", 0f);
			this.startMusicOnMap = false;
		}
		if (!override1)
		{
			if (this.moving != null)
			{
				return;
			}
			if (this.cR8Manager && this.cR8Manager.isRunning)
			{
				return;
			}
			if (this.saveManager.isSavingOrLoading)
			{
				return;
			}
			if (Object.FindObjectOfType<LevelUpManager>().levelingUp)
			{
				return;
			}
			if (this.tutorialManager && this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.organizeChest)
			{
				return;
			}
			if (this.tutorialManager && this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.openMap)
			{
				this.tutorialManager.tutorialSequence = TutorialManager.TutorialSequence.moveToCombat;
			}
			if (this.travelling)
			{
				return;
			}
			if (!this.buttonPressed)
			{
				return;
			}
			if (this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.toteChestOpen)
			{
				return;
			}
			if (RandomEventMaster.IsOpen() && this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.trulyDone)
			{
				return;
			}
			if (this.gameFlowManager && this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.outOfBattle && this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.playerTurn)
			{
				return;
			}
			if (SingleUI.IsViewingPopUp())
			{
				return;
			}
			if (this.inventoryPhase == GameManager.InventoryPhase.choose)
			{
				return;
			}
		}
		if (this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.purseAfterOpenMap)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("MapButton");
			PulseImage pulseImage = ((gameObject != null) ? gameObject.GetComponent<PulseImage>() : null);
			if (pulseImage)
			{
				Object.Destroy(pulseImage);
			}
			this.tutorialManager.tutorialSequence = TutorialManager.TutorialSequence.moveToCombat;
		}
		if (this.endTurnButton.activeInHierarchy)
		{
			this.endTurnButton.GetComponent<Animator>().Play("Out");
		}
		if (this.battleActionButtons.activeInHierarchy)
		{
			this.battleActionButtons.GetComponent<Animator>().Play("BattleActionButtonsOut");
		}
		if (this.reorganizePrompt.activeInHierarchy)
		{
			this.reorganizePrompt.GetComponent<Animator>().Play("ReorganizeOut");
		}
		DungeonPlayer dungeonPlayer = DungeonPlayer.main;
		dungeonPlayer.MoveToClosestRoom();
		if (!dungeonPlayer.isMovingFromLockedDoor)
		{
			dungeonPlayer.FindReachableEvents();
		}
		this.moving = base.StartCoroutine(this.MoveOverTime(this.inventoryTransform, new Vector3(-25f, this.inventoryTransform.position.y, this.inventoryTransform.position.z), 0.35f));
		base.StartCoroutine(this.MoveOverTime(this.mapTransform, new Vector3(0f, this.mapTransform.position.y, this.mapTransform.position.z), 0.25f));
		base.StartCoroutine(this.SetAlphaOverTime(this.inventoryTransform, 1f, 0f, 0.25f));
		this.viewingTop = GameManager.ViewingTop.map;
		if (!this.playerAnimator)
		{
			this.playerAnimator = Player.main.GetComponentInChildren<Animator>();
		}
		this.playerAnimator.Play("Player_ReadMap");
		Transform transform = dungeonPlayer.transform;
		DigitalCursor.main.SetGameWorldDestinationTransform(transform);
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x0006C66C File Offset: 0x0006A86C
	public IEnumerator DisableGameObjectAfterTime(GameObject obj, bool onOrOff, float time)
	{
		yield return new WaitForSeconds(time);
		obj.SetActive(onOrOff);
		yield break;
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x0006C689 File Offset: 0x0006A889
	public IEnumerator SetAlphaOverTime(Transform trans, float alphaStart, float alphaDest, float time)
	{
		SpriteRenderer[] spriteRenderers = trans.GetComponentsInChildren<SpriteRenderer>();
		float t = 0f;
		while (t < time)
		{
			foreach (SpriteRenderer spriteRenderer in spriteRenderers)
			{
				if (spriteRenderer)
				{
					spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(alphaStart, alphaDest, t / time));
				}
			}
			t += Time.deltaTime;
			yield return null;
		}
		foreach (SpriteRenderer spriteRenderer2 in spriteRenderers)
		{
			if (spriteRenderer2)
			{
				spriteRenderer2.color = new Color(spriteRenderer2.color.r, spriteRenderer2.color.g, spriteRenderer2.color.b, alphaDest);
			}
		}
		yield break;
	}

	// Token: 0x06000A9D RID: 2717 RVA: 0x0006C6AE File Offset: 0x0006A8AE
	public IEnumerator MoveOverTime(Transform trans, Vector3 dest, float time)
	{
		Vector3 position = trans.position;
		float t = 0f;
		float speed = Vector2.Distance(position, dest) / time;
		while (t < time)
		{
			t += Time.deltaTime;
			trans.position = Vector3.MoveTowards(trans.position, dest, speed * Time.deltaTime);
			yield return null;
		}
		trans.position = dest;
		this.moving = null;
		DigitalCursor.main.UpdateContextControls();
		yield break;
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x0006C6D4 File Offset: 0x0006A8D4
	public static Transform FindClosest(Transform start, List<Transform> monoBehaviours)
	{
		float num = 999f;
		Transform transform = null;
		foreach (Transform transform2 in monoBehaviours)
		{
			float num2 = Vector2.Distance(transform2.transform.position, start.position);
			if (num2 < num)
			{
				transform = transform2.transform;
				num = num2;
			}
		}
		return transform;
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x0006C758 File Offset: 0x0006A958
	public void ClearLevelUp()
	{
		this.playerAnimator.Play("Player_SearchPack");
		this.announcementAnimator.Play("bigAnnouncementEnd", 0, 0f);
		if (this.afterBattleButton && this.afterBattleButton.activeInHierarchy)
		{
			Animator component = this.afterBattleButton.GetComponent<Animator>();
			if (component)
			{
				component.Play("Out", 0, 0f);
			}
		}
		this.buttonPressed = true;
		this.gameFlowManager.StopAllCoroutines();
		this.gameFlowManager.battlePhase = GameFlowManager.BattlePhase.outOfBattle;
		this.inventoryPhase = GameManager.InventoryPhase.open;
		Item2[] array = Object.FindObjectsOfType<Item2>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetColor();
		}
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x0006C80C File Offset: 0x0006AA0C
	public void AfterBattleButtonPressed()
	{
		if (Object.FindObjectOfType<LevelUpManager>().levelingUp)
		{
			return;
		}
		if (this.dungeonLevel.currentFloor == DungeonLevel.Floor.boss && ((this.zoneNumber == 0 && RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.runEndsAfterZone1)) || (this.zoneNumber == 1 && RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.runEndsAfterZone2)) || (this.zoneNumber == 2 && RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.runEndsAfterZone3)) || this.dungeonLevel.zone == DungeonLevel.Zone.Chaos))
		{
			this.SpawnGoldAndMana();
			this.GetSpecialItemsAfterBattle();
			this.SafetyProtocolAgainstSoftLock();
			this.ClearLevelUp();
			ActionButtonManager actionButtonManager = Object.FindObjectOfType<ActionButtonManager>();
			if (actionButtonManager)
			{
				actionButtonManager.EnableOutOfBattleButtons();
				return;
			}
		}
		else if (this.player.level + 1 < this.player.chosenCharacter.levelUps.Count && (this.player.chosenCharacter.levelUps[this.player.level + 1].demoEnabled || !Singleton.Instance.isDemo) && this.player.experience >= this.player.chosenCharacter.levelUps[this.player.level + 1].xpRequired && (!this.tutorialManager || this.tutorialManager.playType != TutorialManager.PlayType.testing || !this.tutorialManager.disableLevelingUp))
		{
			if (this.player.characterName == Character.CharacterName.Pochette && this.player.chosenCharacter.levelUps[this.player.level + 1].rewards[0].rewardType == Character.LevelUp.Reward.RewardType.Pets)
			{
				this.standardSpawnAfter = true;
			}
			this.player.LevelUp();
			if (this.player.level + 1 >= this.player.chosenCharacter.levelUps.Count || this.player.experience < this.player.chosenCharacter.levelUps[this.player.level + 1].xpRequired || (this.player.chosenCharacter.levelUps[this.player.level + 1].demoEnabled && Singleton.Instance.isDemo))
			{
				this.afterBattleButton.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("gm14");
				return;
			}
		}
		else if (this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.cr8battle1 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.cr8battle2 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.cr8battle3 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.cr8battle4 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.cr8battle5 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.cr8battle6 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.cr8battle7 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.cr8battle8 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.cr8battle9 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.toteBattle1 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.toteBattle2 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.toteBattle3 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.toteBattle4 || this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.toteChest)
		{
			this.ClearLevelUp();
			ActionButtonManager actionButtonManager2 = Object.FindObjectOfType<ActionButtonManager>();
			if (actionButtonManager2)
			{
				actionButtonManager2.EnableOutOfBattleButtons();
				return;
			}
		}
		else
		{
			if (this.player.stats.GetStatusEffectValue(StatusEffect.Type.curse) > 0)
			{
				List<Item2> items = ItemSpawner.GetItems(3, new List<Item2.ItemType> { Item2.ItemType.Curse }, false, true);
				this.OpenSelectItemDialogue("Select a curse", items);
				this.player.stats.AddStatusEffect(StatusEffect.Type.curse, -999f, Item2.Effect.MathematicalType.summative);
				return;
			}
			if (!this.buttonPressed)
			{
				this.buttonPressed = true;
				this.ClearLevelUp();
				this.SpawnGoldAndMana();
				this.StartLimitedItemGetPeriod();
				this.GetSpecialItemsAfterBattle();
				this.ConsiderReplaceAllRunType();
				this.SafetyProtocolAgainstSoftLock();
				ActionButtonManager actionButtonManager3 = Object.FindObjectOfType<ActionButtonManager>();
				if (actionButtonManager3)
				{
					actionButtonManager3.EnableOutOfBattleButtons();
				}
			}
		}
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x0006CC18 File Offset: 0x0006AE18
	private void SafetyProtocolAgainstSoftLock()
	{
		ItemMovementManager itemMovementManager = Object.FindObjectOfType<ItemMovementManager>();
		itemMovementManager.StopAllCoroutines();
		this.gameFlowManager.StopAllCoroutines();
		this.inventoryPhase = GameManager.InventoryPhase.open;
		this.gameFlowManager.isCheckingEffects = false;
		itemMovementManager.isRunning = false;
		itemMovementManager.isCheckingForPuzzle = false;
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x0006CC50 File Offset: 0x0006AE50
	private void ConsiderReplaceAllRunType()
	{
		if (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.replaceAllItems) != null)
		{
			foreach (Item2 item in Item2.GetAllItemsInGrid())
			{
				if (!Item2.ShareItemTypes(item.itemType, new List<Item2.ItemType>
				{
					Item2.ItemType.Gold,
					Item2.ItemType.ManaStone,
					Item2.ItemType.Core,
					Item2.ItemType.Pet
				}))
				{
					this.SpawnItem(item.itemType, new List<Item2.Rarity>(), false, null);
					if (item.itemMovement)
					{
						item.itemMovement.DelayDestroy();
					}
				}
			}
		}
	}

	// Token: 0x06000AA3 RID: 2723 RVA: 0x0006CD08 File Offset: 0x0006AF08
	private void GetSpecialItemsAfterBattle()
	{
		foreach (GameObject gameObject in this.rewardItems)
		{
			string text = "Reward item is ";
			GameObject gameObject2 = gameObject;
			Debug.Log(text + ((gameObject2 != null) ? gameObject2.ToString() : null));
			Item2 component = gameObject.GetComponent<Item2>();
			if (component)
			{
				string text2 = "Reward item has item script ";
				GameObject gameObject3 = gameObject;
				Debug.Log(text2 + ((gameObject3 != null) ? gameObject3.ToString() : null));
				if (this.ItemValidToSpawn(component, false))
				{
					string text3 = "Reward item is valid to spawn ";
					GameObject gameObject4 = gameObject;
					Debug.Log(text3 + ((gameObject4 != null) ? gameObject4.ToString() : null));
					if (component.oneOfAKindType != Item2.OneOfAKindType.OneTotal || !MetaProgressSaveManager.main.oneOfAKindItems.Contains(Item2.GetDisplayName(component.name)))
					{
						string text4 = "Reward item is spawning ";
						GameObject gameObject5 = gameObject;
						Debug.Log(text4 + ((gameObject5 != null) ? gameObject5.ToString() : null));
						Object.Instantiate<GameObject>(gameObject, Vector3.zero + Vector3.back, Quaternion.identity, this.itemsParent);
					}
				}
			}
		}
		foreach (GameObject gameObject6 in this.objectsToRecover)
		{
			gameObject6.gameObject.SetActive(true);
		}
		this.objectsToRecover = new List<GameObject>();
	}

	// Token: 0x06000AA4 RID: 2724 RVA: 0x0006CE7C File Offset: 0x0006B07C
	private void ConsiderCurseItem()
	{
		Status stats = this.player.stats;
		if (!stats)
		{
			return;
		}
		while (stats.GetStatusEffectValue(StatusEffect.Type.curse) > 0)
		{
			GameObject gameObject = ItemSpawner.InstantiateItemFree(ItemSpawner.GetItems(1, new List<Item2.ItemType> { Item2.ItemType.Curse }, false, true));
			EffectParticleSystem.Instance.CopySprite(gameObject.GetComponent<SpriteRenderer>(), EffectParticleSystem.ParticleType.curse);
			Transform transform = gameObject.transform;
			SoundManager.main.PlaySFX("cantMoveHere");
			stats.AddStatusEffect(StatusEffect.Type.curse, -1f, Item2.Effect.MathematicalType.summative);
		}
	}

	// Token: 0x06000AA5 RID: 2725 RVA: 0x0006CEFC File Offset: 0x0006B0FC
	public void SpawnGoldAndMana()
	{
		int num = Random.Range(4, 7);
		RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.increaseGold);
		if (runProperty != null)
		{
			num = Mathf.RoundToInt((float)num * ((100f + (float)runProperty.value) / 100f));
		}
		this.ChangeGold(num);
		int num2 = Random.Range(0, this.turnsSinceStone);
		if (Singleton.Instance.isDemo)
		{
			num2 = 8;
		}
		if (num2 == 0)
		{
			List<Item2.ItemType> list = new List<Item2.ItemType>
			{
				Item2.ItemType.Structure,
				Item2.ItemType.Bow,
				Item2.ItemType.Pet
			};
			ItemSpawner.InstantiateItemsFree(ItemSpawner.GetItems(1, new List<Item2.Rarity>
			{
				Item2.Rarity.Common,
				Item2.Rarity.Uncommon
			}, new List<Item2.ItemType> { Item2.ItemType.ManaStone }, list, false, true, true), true, default(Vector2));
			this.turnsSinceStone = 3;
		}
		else
		{
			this.turnsSinceStone--;
		}
		if (this.floor == 9 && !Singleton.Instance.storyMode)
		{
			this.ShowStats();
		}
		Object.FindObjectOfType<ItemMovementManager>().CheckForMovementPublic();
		this.MoveAllItems();
	}

	// Token: 0x06000AA6 RID: 2726 RVA: 0x0006CFF8 File Offset: 0x0006B1F8
	public void ShowStats()
	{
		if (this.saveManager.isSavingOrLoading || this.viewingEvent)
		{
			return;
		}
		PlayerStatsDisplay playerStatsDisplay = Object.FindObjectOfType<PlayerStatsDisplay>();
		if (!playerStatsDisplay)
		{
			this.viewingEvent = true;
			Object.Instantiate<GameObject>(this.statsBoxPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("UI Canvas").GetComponent<Canvas>().transform);
			return;
		}
		playerStatsDisplay.EndEvent();
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x0006D064 File Offset: 0x0006B264
	public void DespawnAllDungeonVisuals()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("InteractiveVisual");
		for (int i = 0; i < array.Length; i++)
		{
			Animator component = array[i].GetComponent<Animator>();
			if (component)
			{
				component.Play("dungeonEventDespawn");
			}
		}
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x0006D0A8 File Offset: 0x0006B2A8
	public void ShowItemAtlas()
	{
		ItemAtlas itemAtlas = Object.FindObjectOfType<ItemAtlas>();
		if (itemAtlas)
		{
			itemAtlas.EndEvent();
			return;
		}
		if (this.viewingEvent)
		{
			return;
		}
		this.viewingEvent = true;
		Object.Instantiate<GameObject>(this.itemAtlasPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").transform);
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x0006D100 File Offset: 0x0006B300
	public void Announcement(string text, string key)
	{
		this.announcementAnimator.gameObject.SetActive(true);
		this.announcementAnimator.Play("announcementBegin", 0, 0f);
		this.announcementAnimator.GetComponentInChildren<TextMeshProUGUI>().text = text;
		this.announcementAnimator.GetComponentInChildren<ReplacementText>().key = key;
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x0006D158 File Offset: 0x0006B358
	public void ConsiderReorganizeItem()
	{
		if (TwitchManager.isRunningPolls() && TwitchManager.Instance.pollManager.playerTurnAction != null)
		{
			return;
		}
		if (this.battleSpawnItems < 3 && this.player.characterName == Character.CharacterName.Purse && (Random.Range(0, 5) >= 4 || (this.tutorialManager && this.tutorialManager.playType == TutorialManager.PlayType.testing && this.tutorialManager.alwaysGenerateReorganizeItem)))
		{
			this.CreateReorganizeItem();
			this.battleSpawnItems++;
		}
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x0006D1DC File Offset: 0x0006B3DC
	private void CreateReorganizeItem()
	{
		if (this.tutorialManager && this.tutorialManager.playType == TutorialManager.PlayType.testing && this.tutorialManager.alwaysGenerateReorganizeItem && this.tutorialManager.reorganizeItem)
		{
			this.reorgnizeItem = Object.Instantiate<GameObject>(this.tutorialManager.reorganizeItem, this.reorganizePrompt.transform.position, Quaternion.identity);
		}
		else
		{
			this.reorgnizeItem = this.SpawnItem(this.reorganizePrompt.transform.position, 0f, 0, false, null);
		}
		this.reorganizePrompt.SetActive(true);
		this.reorgnizeItem.transform.position = new Vector3(-999f, this.reorganizePrompt.transform.position.y - this.reorgnizeItem.GetComponent<BoxCollider2D>().size.y / 2f - 0.5f, this.reorgnizeItem.transform.position.z);
		this.reorgGlobal = this.reorgnizeItem.transform.position;
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x0006D301 File Offset: 0x0006B501
	private void RemoveReorganizeItem()
	{
		if (this.reorganizePrompt.activeInHierarchy)
		{
			this.reorganizePrompt.GetComponent<Animator>().Play("ReorganizeOut");
		}
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x0006D325 File Offset: 0x0006B525
	public IEnumerator SetEnemyIntentions()
	{
		Enemy[] array = Object.FindObjectsOfType<Enemy>();
		foreach (Enemy enemy in array)
		{
			if (enemy && !enemy.dead)
			{
				yield return enemy.SetIntention();
			}
		}
		Enemy[] array2 = null;
		yield break;
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x0006D330 File Offset: 0x0006B530
	public void NextTurnButton()
	{
		if (SingleUI.IsViewingPopUp())
		{
			return;
		}
		if (this.inSpecialReorg || (this.inventoryPhase != GameManager.InventoryPhase.locked && this.inventoryPhase != GameManager.InventoryPhase.inCombatMove))
		{
			return;
		}
		if (CR8Manager.instance && EnergyBall.GetCurrentEnergyBall() != null)
		{
			CR8Manager.instance.RemoveAllEnergies();
			GameFlowManager.main.StartCoroutine(GameFlowManager.main.AutoEndTurn());
		}
		if (this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.playerTurn || this.gameFlowManager.isCheckingEffects)
		{
			return;
		}
		if (this.gameFlowManager.isRunningAutoEnd)
		{
			return;
		}
		this.StopRepeatActionButton();
		this.DeselectItem();
		this.targetTransform.gameObject.SetActive(false);
		this.endTurnButton.GetComponent<Animator>().Play("Out");
		this.battleActionButtons.GetComponent<Animator>().Play("BattleActionButtonsOut");
		this.RemoveReorganizeItem();
		this.gameFlowManager.EndTurn();
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x0006D419 File Offset: 0x0006B619
	public void ShowStopButton()
	{
		this.ShowBasicButtons(GameManager.ButtonsToEnable.stopActionButton);
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x0006D424 File Offset: 0x0006B624
	public void StopRepeatActionButton()
	{
		if (this.cR8Manager)
		{
			this.cR8Manager.exit = true;
		}
		ContextMenuManager.main.exit = true;
		if (this.stopActionButton.activeInHierarchy)
		{
			this.stopActionButton.GetComponent<Animator>().Play("Out");
			return;
		}
		this.stopActionButton.SetActive(false);
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x0006D484 File Offset: 0x0006B684
	public void ReselectEnemyForDefenders()
	{
		if (this.reselectEnemyForDefendersRoutine != null)
		{
			base.StopCoroutine(this.reselectEnemyForDefendersRoutine);
		}
		this.reselectEnemyForDefendersRoutine = base.StartCoroutine(this.ReselectEnemyForDefendersCoroutine());
	}

	// Token: 0x06000AB2 RID: 2738 RVA: 0x0006D4AC File Offset: 0x0006B6AC
	private IEnumerator ReselectEnemyForDefendersCoroutine()
	{
		yield return null;
		yield return null;
		yield return null;
		this.SelectEnemy(this.targetedEnemy);
		this.reselectEnemyForDefendersRoutine = null;
		yield break;
	}

	// Token: 0x06000AB3 RID: 2739 RVA: 0x0006D4BC File Offset: 0x0006B6BC
	public void SelectEnemy(Enemy enemy)
	{
		foreach (Enemy enemy2 in Enemy.SortEnemiesLeftToRight(Object.FindObjectsOfType<Enemy>().ToList<Enemy>()))
		{
			if (enemy2 && enemy2.stats)
			{
				if (enemy2 == enemy)
				{
					break;
				}
				if (enemy2.CheckForProperty(Enemy.EnemyProperty.Type.blocking) && !enemy2.stats.IsCharmed())
				{
					enemy = enemy2;
					break;
				}
			}
		}
		this.targetTransform.gameObject.SetActive(false);
		this.targetTransform.gameObject.SetActive(true);
		BoxCollider2D component = enemy.GetComponent<BoxCollider2D>();
		if (component)
		{
			Vector2 size = component.size;
			Vector3 vector = enemy.transform.position + component.offset;
			float num = vector.y + size.y / 4f;
			float num2 = vector.y - size.y / 4f;
			float num3 = vector.x - size.x / 4f;
			float num4 = vector.x + size.x / 4f;
			this.targetTransform.localScale = Vector3.one;
			this.targetTransform.rotation = Quaternion.identity;
			this.targetTransform.position = enemy.transform.position + Vector3.back;
			this.targetTransform.GetChild(0).position = new Vector3(num3, num, this.targetTransform.position.z);
			this.targetTransform.GetChild(1).position = new Vector3(num4, num2, this.targetTransform.position.z);
		}
		this.targetedEnemy = enemy;
	}

	// Token: 0x06000AB4 RID: 2740 RVA: 0x0006D690 File Offset: 0x0006B890
	public void ScratchButton()
	{
		if (this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.playerTurn || !this.targetedEnemy || this.targetedEnemy.dead || !this.gameFlowManager.IsComplete())
		{
			return;
		}
		if (this.player.AP >= 1)
		{
			this.gameFlowManager.StartCoroutine(this.gameFlowManager.Scratch(3, 1, false));
			return;
		}
		this.CreatePopUp(LangaugeManager.main.GetTextByKey("gm15"));
	}

	// Token: 0x06000AB5 RID: 2741 RVA: 0x0006D714 File Offset: 0x0006B914
	public void WhistleButton()
	{
		if (this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.playerTurn || !this.targetedEnemy || this.targetedEnemy.dead)
		{
			return;
		}
		if (this.player.AP >= 1)
		{
			this.gameFlowManager.StartCoroutine(this.gameFlowManager.Whistle());
			return;
		}
		this.CreatePopUp(LangaugeManager.main.GetTextByKey("gm15"));
	}

	// Token: 0x06000AB6 RID: 2742 RVA: 0x0006D785 File Offset: 0x0006B985
	public IEnumerator StartTutorial()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		SpriteRenderer[] componentsInChildren = this.inventoryTransform.GetComponentsInChildren<SpriteRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].sortingOrder = 2;
		}
		TextMeshPro[] componentsInChildren2 = this.inventoryTransform.GetComponentsInChildren<TextMeshPro>();
		for (int i = 0; i < componentsInChildren2.Length; i++)
		{
			componentsInChildren2[i].sortingOrder = 2;
		}
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		base.StartCoroutine(this.MoveOverTime(this.inventoryTransform, new Vector3(0f, 0.75f, this.inventoryTransform.position.z), 0.3f));
		this.inSpecialReorg = true;
		this.fadeOut.SetActive(true);
		this.fadeOut.GetComponent<Animator>().Play("fadeInPartial", 0, 0f);
		base.StartCoroutine(this.ShowPromptText(this.tutorialText, 230f));
		yield break;
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x0006D794 File Offset: 0x0006B994
	public IEnumerator StartTutorialInit()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		string text = LangaugeManager.main.GetTextByKey("tut2");
		if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.cursor)
		{
			text = text.Replace("/x", "<sprite name=\"spriteemojiatlas_62\">");
		}
		else if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.controller)
		{
			string text2 = " <sprite name=\"" + DigitalCursor.main.GetCurrentImageNameForButtonName("rotateLeft") + "\">";
			string text3 = " <sprite name=\"" + DigitalCursor.main.GetCurrentImageNameForButtonName("rotateRight") + "\">";
			text = text.Replace("/x", text2 + text3);
		}
		this.tutorialText.GetComponentInChildren<TextMeshProUGUI>().text = text;
		yield break;
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x0006D7A4 File Offset: 0x0006B9A4
	public void StartTutorial2()
	{
		this.tutorialText.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(940f, 180f);
		this.tutorialText.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("tut1");
		base.StartCoroutine(this.ShowPromptText(this.tutorialText, 335f));
	}

	// Token: 0x06000AB9 RID: 2745 RVA: 0x0006D814 File Offset: 0x0006BA14
	public void StartPetPlacement()
	{
		this.inventoryPhase = GameManager.InventoryPhase.placePet;
		Transform transform = GameObject.FindGameObjectWithTag("PlayerSpacerParent").transform;
		for (int i = 0; i <= transform.childCount; i += 2)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.petPlacementIcon, new Vector3(999f, 999f, 0f), this.petPlacementIcon.transform.rotation);
			GameObject gameObject2 = Object.Instantiate<GameObject>(this.enemySpacer, new Vector3(999f, 999f, 0f), Quaternion.identity, transform);
			gameObject2.transform.SetSiblingIndex(i);
			gameObject.GetComponent<Follow>().follow = gameObject2.transform;
		}
	}

	// Token: 0x06000ABA RID: 2746 RVA: 0x0006D8BC File Offset: 0x0006BABC
	public void SetPetPlacement(int num)
	{
		foreach (PetPlacementIcon petPlacementIcon in Object.FindObjectsOfType<PetPlacementIcon>())
		{
			Follow component = petPlacementIcon.GetComponent<Follow>();
			if (component.follow.GetSiblingIndex() < num)
			{
				num--;
			}
			Object.Destroy(component.follow.gameObject);
			Object.Destroy(petPlacementIcon.gameObject);
		}
		this.petPlacementNumber = num;
		this.inventoryPhase = GameManager.InventoryPhase.locked;
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x0006D920 File Offset: 0x0006BB20
	public void CancelPetPlacement()
	{
		foreach (PetPlacementIcon petPlacementIcon in Object.FindObjectsOfType<PetPlacementIcon>())
		{
			Object.Destroy(petPlacementIcon.GetComponent<Follow>().follow.gameObject);
			Object.Destroy(petPlacementIcon.gameObject);
		}
		this.petPlacementNumber = -999;
		this.inventoryPhase = GameManager.InventoryPhase.locked;
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x0006D975 File Offset: 0x0006BB75
	public void StartReorganization()
	{
		if (this.startReorganizationCoroutine != null)
		{
			base.StopCoroutine(this.startReorganizationCoroutine);
		}
		this.startReorganizationCoroutine = base.StartCoroutine(this.StartReorganizationCoroutine());
	}

	// Token: 0x06000ABD RID: 2749 RVA: 0x0006D99D File Offset: 0x0006BB9D
	public IEnumerator StartReorganizationCoroutine()
	{
		GameFlowManager.main.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onReorganize, null, null, true, false);
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		while (GameFlowManager.main.isCheckingEffects)
		{
			yield return null;
		}
		this.DeselectItem();
		if (this.reorgnizeItem)
		{
			BoxCollider2D[] components = this.reorgnizeItem.GetComponents<BoxCollider2D>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].enabled = true;
			}
			StackableItem component = this.reorgnizeItem.GetComponent<StackableItem>();
			if (component)
			{
				component.StartCoroutine(component.Fake());
			}
		}
		this.inSpecialReorg = true;
		this.reorgnizeItem = null;
		this.RemoveReorganizeItem();
		this.inventoryPhase = GameManager.InventoryPhase.open;
		SpriteRenderer[] componentsInChildren = this.inventoryTransform.GetComponentsInChildren<SpriteRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].sortingOrder = 2;
		}
		ParticleSystem[] componentsInChildren2 = this.inventoryTransform.GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < componentsInChildren2.Length; i++)
		{
			componentsInChildren2[i].GetComponent<Renderer>().sortingOrder = 2;
		}
		TextMeshPro[] componentsInChildren3 = this.inventoryTransform.GetComponentsInChildren<TextMeshPro>();
		for (int i = 0; i < componentsInChildren3.Length; i++)
		{
			componentsInChildren3[i].sortingOrder = 2;
		}
		base.StartCoroutine(this.MoveOverTime(this.inventoryTransform, new Vector3(0f, 0.75f, this.inventoryTransform.position.z), 0.3f));
		this.fadeOut.SetActive(true);
		this.fadeOut.GetComponent<Animator>().Play("fadeInPartial", 0, 0f);
		this.promptText.text = LangaugeManager.main.GetTextByKey("gm16");
		ReplacementText component2 = this.promptText.GetComponent<ReplacementText>();
		component2.key = "gm16";
		component2.textPreprocessor = null;
		base.StartCoroutine(this.ShowPromptText(null, 95f));
		this.ShowBasicButtons(GameManager.ButtonsToEnable.finishReorganizingButton, LangaugeManager.main.GetTextByKey("gm17"), "gm17");
		Item2.SetAllItemColors();
		yield break;
	}

	// Token: 0x06000ABE RID: 2750 RVA: 0x0006D9AC File Offset: 0x0006BBAC
	public void ReorganizeButton()
	{
		if (this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.playerTurn || !this.targetedEnemy || this.targetedEnemy.dead || (this.cR8Manager && (this.cR8Manager.isRunning || this.cR8Manager.turnSpent)))
		{
			return;
		}
		int num = 3;
		if (Player.main.characterName == Character.CharacterName.Pochette)
		{
			num = 2;
		}
		num += Item2.GetEffectValues(Item2.Effect.Type.ChangeCostOfReorganize);
		if (this.player.AP >= num)
		{
			this.player.ChangeAP(num * -1);
			this.StartReorganization();
			return;
		}
		this.CreatePopUp(LangaugeManager.main.GetTextByKey("gm15"));
	}

	// Token: 0x06000ABF RID: 2751 RVA: 0x0006DA5C File Offset: 0x0006BC5C
	public void RotateAll(float direction)
	{
		if (SingleUI.IsViewingPopUp())
		{
			return;
		}
		ItemMovement[] array = Object.FindObjectsOfType<ItemMovement>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Rotate(direction);
		}
		GridBlock[] array2 = Object.FindObjectsOfType<GridBlock>();
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].Rotate(direction);
		}
	}

	// Token: 0x06000AC0 RID: 2752 RVA: 0x0006DAAB File Offset: 0x0006BCAB
	public IEnumerator SaveCheck()
	{
		yield return new WaitForSeconds(4f);
		if (this.saveManager.isSavingOrLoading)
		{
			this.CreatePopUp(LangaugeManager.main.GetTextByKey("gm18"));
			this.saveManager.isSavingOrLoading = false;
		}
		yield break;
	}

	// Token: 0x06000AC1 RID: 2753 RVA: 0x0006DABC File Offset: 0x0006BCBC
	public void CreateAreasForSpecialReorganization(List<Vector2> areasToPlace, Transform gridParent = null)
	{
		GameManager.AreasForSpecialReorganization areasForSpecialReorganization = new GameManager.AreasForSpecialReorganization();
		areasForSpecialReorganization.localPositions = areasToPlace;
		areasForSpecialReorganization.gridParent = gridParent;
		this.areasForSpecialReorganizations.Add(areasForSpecialReorganization);
	}

	// Token: 0x06000AC2 RID: 2754 RVA: 0x0006DAE9 File Offset: 0x0006BCE9
	public void ClearAreasForSpecialReorganization()
	{
		this.areasForSpecialReorganizations.Clear();
		Tiler.FadeAllTilers(Tiler.Type.SpecialReorganizationArea);
	}

	// Token: 0x06000AC3 RID: 2755 RVA: 0x0006DAFC File Offset: 0x0006BCFC
	public bool AreasForSpecialReorganizationIncludes(Vector2 pos, Transform gridParent)
	{
		foreach (GameManager.AreasForSpecialReorganization areasForSpecialReorganization in this.areasForSpecialReorganizations)
		{
			if (areasForSpecialReorganization.localPositions.Contains(pos) && areasForSpecialReorganization.gridParent == gridParent)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000AC4 RID: 2756 RVA: 0x0006DB6C File Offset: 0x0006BD6C
	private void GetDamageForHazards(out int numOfItemsOutside, out int numOfHazardsSkipped, out int damageToTake)
	{
		List<Item2> list = (from x in GameManager.main.itemsForSpecialReorganization.ConvertAll<Item2>((GameObject x) => x.GetComponent<Item2>())
			where x.itemMovement && !x.itemMovement.inGrid
			select x).ToList<Item2>();
		numOfItemsOutside = list.Count;
		int num = 0;
		int num2 = 0;
		using (List<Item2>.Enumerator enumerator = list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.itemType.Contains(Item2.ItemType.Hazard))
				{
					num2++;
					num += num2 + this.floor + this.cursesSkipped;
				}
			}
		}
		numOfHazardsSkipped = num2;
		damageToTake = num;
	}

	// Token: 0x06000AC5 RID: 2757 RVA: 0x0006DC40 File Offset: 0x0006BE40
	public void UpdateFinishButton()
	{
		if (!this.inSpecialReorg)
		{
			return;
		}
		if (!this.finishReorganizingButton || !this.finishReorganizingButton.activeInHierarchy)
		{
			return;
		}
		int num;
		int num2;
		int num3;
		this.GetDamageForHazards(out num, out num2, out num3);
		if (num == 0)
		{
			this.finishReorganizingButton.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("gm17");
			return;
		}
		string text = LangaugeManager.main.GetTextByKey("gm20");
		text = text.Replace("/x", num.ToString() ?? "");
		if (num3 > 0)
		{
			text = text + " " + LangaugeManager.main.GetTextByKey("gm20b");
			text = text.Replace("/x", num3.ToString() ?? "");
		}
		this.finishReorganizingButton.GetComponentInChildren<TextMeshProUGUI>().text = text;
	}

	// Token: 0x06000AC6 RID: 2758 RVA: 0x0006DD1C File Offset: 0x0006BF1C
	public void EnterSpecialOrganization(List<GameObject> itemsCreated, List<Vector2> areasToPlace, bool skippable = true, Transform gridParent = null)
	{
		this.SetAllItemColliders(true);
		this.inSpecialReorg = true;
		this.skippableReorganization = skippable;
		this.CreateAreasForSpecialReorganization(areasToPlace, gridParent);
		this.itemsForSpecialReorganization = itemsCreated;
		if (this.reorgnizeItem)
		{
			BoxCollider2D[] components = this.reorgnizeItem.GetComponents<BoxCollider2D>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].enabled = false;
			}
		}
		foreach (GameObject gameObject in this.itemsForSpecialReorganization)
		{
			Item2 component = gameObject.GetComponent<Item2>();
			if (component.itemType.Contains(Item2.ItemType.Curse) || component.itemType.Contains(Item2.ItemType.Hazard))
			{
				skippable = false;
				this.skippableReorganization = false;
			}
			if (component && component.itemMovement)
			{
				this.MoveItemTowardsGrid(component.itemMovement);
			}
		}
		this.inventoryPhase = GameManager.InventoryPhase.specialReorganization;
		foreach (SpriteRenderer spriteRenderer in this.inventoryTransform.GetComponentsInChildren<SpriteRenderer>())
		{
			if (!this.reorgnizeItem || this.reorgnizeItem != spriteRenderer.gameObject)
			{
				spriteRenderer.sortingOrder = 2;
			}
		}
		ItemMovement.ShowSquares(areasToPlace, this.highlightAreaForValidPlacement, gridParent);
		Item2.SetAllItemColors();
		foreach (TextMeshPro textMeshPro in this.inventoryTransform.GetComponentsInChildren<TextMeshPro>())
		{
			ItemMovement componentInParent = textMeshPro.GetComponentInParent<ItemMovement>();
			if (componentInParent && componentInParent.inGrid)
			{
				textMeshPro.sortingOrder = 2;
			}
		}
		if (this.itemsForSpecialReorganization.Count == 1 && DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.controller)
		{
			base.StartCoroutine(this.StartDragWithObj(this.itemsForSpecialReorganization[0]));
		}
		this.fadeOut.SetActive(true);
		this.fadeOut.GetComponent<Animator>().Play("fadeInPartial", 0, 0f);
		base.StartCoroutine(this.MoveOverTime(this.inventoryTransform, new Vector3(0f, 0.75f, this.inventoryTransform.position.z), 0.3f));
		this.ShowBasicButtons(GameManager.ButtonsToEnable.finishReorganizingButton, LangaugeManager.main.GetTextByKey("gm17"), "gm17");
		this.UpdateFinishButton();
	}

	// Token: 0x06000AC7 RID: 2759 RVA: 0x0006DF68 File Offset: 0x0006C168
	private IEnumerator StartDragWithObj(GameObject obj)
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		ItemMovement itemMovement = this.itemsForSpecialReorganization[0].GetComponent<ItemMovement>();
		while (itemMovement && (itemMovement.isAnimating || itemMovement.isTransiting))
		{
			yield return null;
		}
		if (!itemMovement || !this.itemsForSpecialReorganization[0])
		{
			yield break;
		}
		this.itemsForSpecialReorganization[0].transform.position = DigitalCursor.main.transform.position;
		this.itemsForSpecialReorganization[0].GetComponent<ItemMovement>().StartDrag();
		yield break;
	}

	// Token: 0x06000AC8 RID: 2760 RVA: 0x0006DF78 File Offset: 0x0006C178
	public void ShowHighlights()
	{
		if (this.inventoryPhase == GameManager.InventoryPhase.specialReorganization || this.inventoryPhase == GameManager.InventoryPhase.inCombatMove)
		{
			foreach (GameManager.AreasForSpecialReorganization areasForSpecialReorganization in this.areasForSpecialReorganizations)
			{
				ItemMovement.ShowSquares(areasForSpecialReorganization.localPositions, this.highlightAreaForValidPlacement, areasForSpecialReorganization.gridParent);
			}
		}
	}

	// Token: 0x06000AC9 RID: 2761 RVA: 0x0006DFF0 File Offset: 0x0006C1F0
	public void BeginSelectPeriod(EventButton mySearch, bool willDestroy)
	{
		this.destroySelectedItem = willDestroy;
		this.eventButton = mySearch;
		this.inventoryPhase = GameManager.InventoryPhase.choose;
	}

	// Token: 0x06000ACA RID: 2762 RVA: 0x0006E008 File Offset: 0x0006C208
	public void ChooseMatchingItem(EventButton mySearch, bool willDestroy)
	{
		this.ClearEvent();
		this.BeginSelectPeriod(mySearch, willDestroy);
		this.SetAllItemColliders(true);
		SpriteRenderer[] componentsInChildren = this.inventoryTransform.GetComponentsInChildren<SpriteRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].sortingOrder = 2;
		}
		TextMeshPro[] componentsInChildren2 = this.inventoryTransform.GetComponentsInChildren<TextMeshPro>();
		for (int i = 0; i < componentsInChildren2.Length; i++)
		{
			componentsInChildren2[i].sortingOrder = 2;
		}
		base.StartCoroutine(this.MoveOverTime(this.inventoryTransform, new Vector3(0f, 0f, this.inventoryTransform.position.z), 0.3f));
		this.fadeOut.SetActive(true);
		this.fadeOut.GetComponent<Animator>().Play("fadeInPartial", 0, 0f);
		if (mySearch.requirement != EventButton.Requirements.itemSacrifice || !mySearch.requiredItemType.Contains(Item2.ItemType.Any))
		{
			string text = "";
			for (int j = 0; j < mySearch.requiredItemType.Count; j++)
			{
				if (j > 0 && mySearch.requiredItemType.Count > 2)
				{
					text += ",";
				}
				text += " ";
				if (j > 0 && j == mySearch.requiredItemType.Count - 1)
				{
					text += "or ";
				}
				text += mySearch.requiredItemType[j].ToString();
			}
		}
		Item2[] array = Object.FindObjectsOfType<Item2>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetColor();
		}
		if (mySearch.skippable)
		{
			this.ShowBasicButtons(GameManager.ButtonsToEnable.finishReorganizingButton, LangaugeManager.main.GetTextByKey("gm22"), "gm22");
		}
	}

	// Token: 0x06000ACB RID: 2763 RVA: 0x0006E1BD File Offset: 0x0006C3BD
	public void ShowPromptTextWithKey(string x, string key)
	{
		this.promptText.text = x;
		ReplacementText component = this.promptText.GetComponent<ReplacementText>();
		component.key = key;
		component.textPreprocessor = null;
		base.StartCoroutine(this.ShowPromptText(null, 95f));
	}

	// Token: 0x06000ACC RID: 2764 RVA: 0x0006E1F6 File Offset: 0x0006C3F6
	public void ShowBasicButtons(GameManager.ButtonsToEnable b)
	{
		this.ShowBasicButtons(new List<GameManager.ButtonsToEnable> { b }, new List<string>(), new List<string>());
	}

	// Token: 0x06000ACD RID: 2765 RVA: 0x0006E214 File Offset: 0x0006C414
	public void ShowBasicButtons(GameManager.ButtonsToEnable b, string text, string key)
	{
		this.ShowBasicButtons(new List<GameManager.ButtonsToEnable> { b }, new List<string> { text }, new List<string> { key });
	}

	// Token: 0x06000ACE RID: 2766 RVA: 0x0006E240 File Offset: 0x0006C440
	public void ShowBasicButtons(List<GameManager.ButtonsToEnable> b, List<string> texts, List<string> keys)
	{
		this.finishReorganizingButton.transform.parent.gameObject.SetActive(true);
		if (b.Contains(GameManager.ButtonsToEnable.finishReorganizingButton))
		{
			this.finishReorganizingButton.SetActive(true);
			this.finishReorganizingButton.transform.GetChild(0).gameObject.SetActive(true);
			int num = b.IndexOf(GameManager.ButtonsToEnable.finishReorganizingButton);
			if (num < texts.Count)
			{
				this.finishReorganizingButton.GetComponentInChildren<TextMeshProUGUI>().text = texts[num];
				ReplacementText componentInChildren = this.finishReorganizingButton.GetComponentInChildren<ReplacementText>();
				if (componentInChildren != null)
				{
					componentInChildren.key = keys[num];
				}
			}
		}
		if (b.Contains(GameManager.ButtonsToEnable.endTurnButton))
		{
			this.endTurnButton.SetActive(true);
			int num2 = b.IndexOf(GameManager.ButtonsToEnable.endTurnButton);
			if (num2 < texts.Count)
			{
				this.endTurnButton.GetComponentInChildren<TextMeshProUGUI>().text = texts[num2];
				ReplacementText componentInChildren2 = this.endTurnButton.GetComponentInChildren<ReplacementText>();
				if (componentInChildren2 != null)
				{
					componentInChildren2.key = keys[num2];
				}
			}
			if (this.endTurnButton)
			{
				InputHandler componentInChildren3 = this.endTurnButton.GetComponentInChildren<InputHandler>();
				if (componentInChildren3)
				{
					componentInChildren3.enabled = true;
				}
			}
		}
		if (b.Contains(GameManager.ButtonsToEnable.stopActionButton))
		{
			this.stopActionButton.SetActive(true);
			int num3 = b.IndexOf(GameManager.ButtonsToEnable.stopActionButton);
			if (num3 < texts.Count)
			{
				this.stopActionButton.GetComponentInChildren<TextMeshProUGUI>().text = texts[num3];
				ReplacementText componentInChildren4 = this.stopActionButton.GetComponentInChildren<ReplacementText>();
				if (componentInChildren4 != null)
				{
					componentInChildren4.key = keys[num3];
				}
			}
		}
	}

	// Token: 0x06000ACF RID: 2767 RVA: 0x0006E3D8 File Offset: 0x0006C5D8
	public void HideBasicButtons()
	{
		this.finishReorganizingButton.transform.parent.gameObject.SetActive(false);
		this.finishReorganizingButton.SetActive(false);
		this.skipReorganizingButton.SetActive(false);
		this.endTurnButton.SetActive(false);
		this.stopActionButton.SetActive(false);
	}

	// Token: 0x06000AD0 RID: 2768 RVA: 0x0006E430 File Offset: 0x0006C630
	public Vector3 FindClosestGrid(Vector3 position)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("GridSquare");
		float num = 999f;
		GameObject gameObject = null;
		foreach (GameObject gameObject2 in array)
		{
			float num2 = Vector2.Distance(gameObject2.transform.position, position);
			if (num2 < num)
			{
				gameObject = gameObject2;
				num = num2;
			}
		}
		return gameObject.transform.localPosition;
	}

	// Token: 0x06000AD1 RID: 2769 RVA: 0x0006E498 File Offset: 0x0006C698
	public void ResetColors()
	{
		foreach (SpriteRenderer spriteRenderer in this.inventoryTransform.GetComponentsInChildren<SpriteRenderer>())
		{
			if (spriteRenderer.CompareTag("Item") || spriteRenderer.CompareTag("GridSquare"))
			{
				GridSquare component = spriteRenderer.GetComponent<GridSquare>();
				if (component)
				{
					spriteRenderer.color = Color.white;
					component.SetColor();
				}
				Item2 component2 = spriteRenderer.GetComponent<Item2>();
				if (component2)
				{
					component2.SetColor();
				}
			}
		}
	}

	// Token: 0x06000AD2 RID: 2770 RVA: 0x0006E518 File Offset: 0x0006C718
	public void SkipReorganizeButton()
	{
		if (!this.inSpecialReorg)
		{
			return;
		}
		foreach (GameObject gameObject in this.itemsForSpecialReorganization)
		{
			gameObject.GetComponent<ItemMovement>().DelayDestroy();
		}
		this.player.stats.ChangeHealth((this.cursesSkipped + this.floor + 1) * -1, null, false);
		this.cursesSkipped++;
		this.skippableReorganization = true;
		this.FinishReorganizeButton();
		this.ClearAreasForSpecialReorganization();
	}

	// Token: 0x06000AD3 RID: 2771 RVA: 0x0006E5BC File Offset: 0x0006C7BC
	private void RemoveItemsFromLimitedItemGetPeriod()
	{
		int num = 0;
		foreach (ItemMovement itemMovement in Object.FindObjectsOfType<ItemMovement>())
		{
			if (!itemMovement.inGrid && itemMovement.returnsToOutOfInventoryPosition)
			{
				if (this.analyticsManager)
				{
					this.analyticsManager.AddItem("itemsRejected", itemMovement.GetComponent<Item2>().displayName);
				}
				if (itemMovement.myItem && itemMovement.myItem.itemType.Contains(Item2.ItemType.Curse))
				{
					itemMovement.PretendDestroyCurse();
					num++;
				}
				else
				{
					itemMovement.DelayDestroy();
				}
			}
			else
			{
				itemMovement.returnsToOutOfInventoryPosition = false;
			}
		}
		GameManager.main.ChangeCurse(num);
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x0006E668 File Offset: 0x0006C868
	public void FinishLimitedItemSelection()
	{
		this.RemoveItemsFromLimitedItemGetPeriod();
		this.limitedItemReorganize = false;
		this.totalNumOfItemsAllowedToTake = 0;
		this.numOfItemsAllowedToTake = 0;
		base.StartCoroutine(this.HidePromptText(null, -105f));
		if (this.finishReorganizingButton && this.finishReorganizingButton.activeInHierarchy)
		{
			this.finishReorganizingButton.SetActive(false);
		}
		if (!this.travelling && this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.outOfBattle && !this.viewingEvent)
		{
			DungeonPlayer dungeonPlayer = Object.FindObjectOfType<DungeonPlayer>();
			DungeonEvent relevantEvent = DungeonPlayer.GetRelevantEvent(dungeonPlayer.transform.position);
			if (relevantEvent)
			{
				bool flag = false;
				using (List<GameObject>.Enumerator enumerator = relevantEvent.itemsToSpawn.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == null)
						{
							dungeonPlayer.FindReachableEvents();
							flag = true;
							break;
						}
					}
				}
				if (!flag && this.inventoryPhase == GameManager.InventoryPhase.open)
				{
					dungeonPlayer.StartEvent();
				}
			}
		}
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x0006E774 File Offset: 0x0006C974
	public void FinishReorganizeButton()
	{
		if (this.eventButton && this.eventButton.randomEventMaster)
		{
			this.viewingEvent = true;
			this.eventButton.randomEventMaster.gameObject.SetActive(true);
		}
		Tote tote = Object.FindObjectOfType<Tote>();
		if (tote)
		{
			tote.MarkAllAsOwned();
		}
		foreach (EnergyEmitter energyEmitter in Object.FindObjectsOfType<EnergyEmitter>())
		{
			if (energyEmitter.type == EnergyEmitter.Type.creator && !energyEmitter.GetComponent<ItemMovement>().inGrid)
			{
				this.CreatePopUp(LangaugeManager.main.GetTextByKey("gmCR81"));
				SoundManager.main.PlaySFX("cantMoveHere");
				return;
			}
		}
		foreach (Item2 item in Object.FindObjectsOfType<Item2>())
		{
			if (item && item.itemMovement && !item.itemMovement.inGrid && !item.destroyed && item.itemType.Contains(Item2.ItemType.Curse) && !item.GetComponentInParent<RandomEventMaster>() && this.itemsForSpecialReorganization.Contains(item.gameObject))
			{
				this.CreatePopUp(LangaugeManager.main.GetTextByKey("gm44") + " " + LangaugeManager.main.GetTextByKey(item.displayName));
				SoundManager.main.PlaySFX("cantMoveHere");
				return;
			}
		}
		if (!this.inSpecialReorg && this.inventoryPhase != GameManager.InventoryPhase.choose && !this.limitedItemReorganize)
		{
			return;
		}
		if (this.reorgnizeItem)
		{
			BoxCollider2D[] components = this.reorgnizeItem.GetComponents<BoxCollider2D>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].enabled = true;
			}
		}
		int num;
		int num2;
		int num3;
		this.GetDamageForHazards(out num, out num2, out num3);
		this.cursesSkipped += num2;
		if (num3 > 0)
		{
			this.player.stats.ChangeHealth(num3 * -1, null, false);
		}
		List<ItemMovement> list = new List<ItemMovement>();
		foreach (GameObject gameObject in this.itemsForSpecialReorganization)
		{
			foreach (ItemMovement itemMovement in gameObject.GetComponent<ItemMovement>().itemsReplacedByThisCurse)
			{
				list.Add(itemMovement);
			}
		}
		foreach (ItemMovement itemMovement2 in Object.FindObjectsOfType<ItemMovement>())
		{
			if (!itemMovement2.inGrid && list.Contains(itemMovement2))
			{
				itemMovement2.DelayDestroy();
			}
		}
		Curse[] array4 = Object.FindObjectsOfType<Curse>();
		for (int i = 0; i < array4.Length; i++)
		{
			array4[i].Play();
		}
		if (this.tutorialManager.tutorialSequence == TutorialManager.TutorialSequence.organizeChest)
		{
			this.tutorialManager.tutorialSequence = TutorialManager.TutorialSequence.openMap;
		}
		if (this.anchorItemsAfterSpecialReorganization)
		{
			foreach (GameObject gameObject2 in this.itemsForSpecialReorganization)
			{
				Item2 component = gameObject2.GetComponent<Item2>();
				Item2.ItemStatusEffect itemStatusEffect = new Item2.ItemStatusEffect();
				itemStatusEffect.type = Item2.ItemStatusEffect.Type.locked;
				itemStatusEffect.length = Item2.ItemStatusEffect.Length.turns;
				itemStatusEffect.num = 3;
				itemStatusEffect.source = "TwitchOrigin";
				component.activeItemStatusEffects.Add(itemStatusEffect);
			}
		}
		this.itemsForSpecialReorganization.Clear();
		this.itemsReplacedBySpecialReorgItem.Clear();
		this.anchorItemsAfterSpecialReorganization = false;
		this.inSpecialReorg = false;
		this.skippableReorganization = true;
		SoundManager.main.PlaySFX("menuBlip");
		GameObject gameObject3 = GameObject.FindGameObjectWithTag("ToteCardOverlay");
		if (gameObject3)
		{
			RandomEventMaster componentInChildren = gameObject3.GetComponentInChildren<RandomEventMaster>();
			if (gameObject3 && componentInChildren)
			{
				componentInChildren.EndEvent();
			}
		}
		if (this.standardSpawnAfter)
		{
			this.standardSpawnAfter = false;
			this.RemoveItemsFromLimitedItemGetPeriod();
			this.StartLimitedItemGetPeriod();
			return;
		}
		this.ClearAreasForSpecialReorganization();
		this.FinishLimitedItemSelection();
		this.ShowInventory();
		this.DeselectItem();
		if (this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.outOfBattle)
		{
			int num4 = 0;
			foreach (ItemMovement itemMovement3 in Object.FindObjectsOfType<ItemMovement>())
			{
				if (!itemMovement3.inGrid && this.reorgnizeItem != itemMovement3.gameObject && !tote.GetAllCards().Contains(itemMovement3.gameObject))
				{
					if (itemMovement3.myItem && itemMovement3.myItem.itemType.Contains(Item2.ItemType.Curse))
					{
						itemMovement3.PretendDestroyCurse();
						num4++;
					}
					else
					{
						itemMovement3.DelayDestroy();
					}
				}
			}
			GameManager.main.ChangeCurse(num4);
			this.RemoveItemsOutsideGrid();
			this.inventoryPhase = GameManager.InventoryPhase.locked;
			if (TwitchManager.isRunningPolls() && TwitchManager.Instance.pollManager.pollReorganizing)
			{
				TwitchManager.Instance.pollManager.onPlayerTurnStartLate();
				this.ConsiderReorganizeItem();
			}
		}
		else
		{
			this.inventoryPhase = GameManager.InventoryPhase.open;
		}
		SpriteRenderer[] componentsInChildren = this.inventoryTransform.GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer spriteRenderer in componentsInChildren)
		{
			if (spriteRenderer.CompareTag("GridSquare"))
			{
				spriteRenderer.color = Color.white;
				GridSquare component2 = spriteRenderer.GetComponent<GridSquare>();
				if (component2)
				{
					component2.SetColor();
				}
			}
			spriteRenderer.sortingOrder = 0;
		}
		foreach (SpriteRenderer spriteRenderer2 in componentsInChildren)
		{
			if (spriteRenderer2.CompareTag("Item"))
			{
				spriteRenderer2.color = Color.white;
				Item2 component3 = spriteRenderer2.GetComponent<Item2>();
				if (component3)
				{
					component3.SetColor();
				}
			}
		}
		ItemBorderBackground.SetAllColors();
		TextMeshPro[] componentsInChildren2 = this.inventoryTransform.GetComponentsInChildren<TextMeshPro>();
		for (int i = 0; i < componentsInChildren2.Length; i++)
		{
			componentsInChildren2[i].sortingOrder = 0;
		}
		this.finishReorganizingButton.SetActive(false);
		this.skipReorganizingButton.SetActive(false);
		if (this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.playerTurn)
		{
			this.ShowBasicButtons(GameManager.ButtonsToEnable.endTurnButton);
			if (this.endTurnButton)
			{
				InputHandler componentInChildren2 = this.endTurnButton.GetComponentInChildren<InputHandler>();
				if (componentInChildren2)
				{
					componentInChildren2.enabled = true;
				}
			}
		}
		this.fadeOut.GetComponent<Animator>().Play("fadeOutPartial", 0, 0f);
		if (this.cR8Manager && this.cR8Manager.skippedTurnToReorg)
		{
			this.cR8Manager.skippedTurnToReorg = false;
			this.gameFlowManager.EndTurn();
		}
	}

	// Token: 0x06000AD6 RID: 2774 RVA: 0x0006EE28 File Offset: 0x0006D028
	public void LeaveItemBehind(Item2 item, ItemMovement im)
	{
		if (!item || !im || item.destroyed)
		{
			return;
		}
		GameFlowManager.main.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.whenLeftBehind, item, null, true, false);
		SpriteRenderer component = item.GetComponent<SpriteRenderer>();
		this.AddParticles(item.transform.position + Vector3.back, component, null);
		im.DelayDestroy();
	}

	// Token: 0x06000AD7 RID: 2775 RVA: 0x0006EE88 File Offset: 0x0006D088
	public void RemoveItemsOutsideGrid()
	{
		Tote tote = Object.FindObjectOfType<Tote>();
		foreach (ItemMovement itemMovement in Object.FindObjectsOfType<ItemMovement>())
		{
			if (!itemMovement.inGrid && (!this.reorgnizeItem || this.reorgnizeItem.GetComponent<ItemMovement>() != itemMovement) && !tote.GetAllCards().Contains(itemMovement.gameObject))
			{
				Item2 component = itemMovement.GetComponent<Item2>();
				this.LeaveItemBehind(component, itemMovement);
			}
			else
			{
				itemMovement.GetComponent<Item2>().isForSale = false;
				itemMovement.GetComponent<Item2>().cost = itemMovement.GetComponent<Item2>().originalCost;
			}
		}
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x0006EF23 File Offset: 0x0006D123
	public void DeselectItem()
	{
		this.selectedItem = null;
		base.StartCoroutine(this.HidePromptText(null, -105f));
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x0006EF3F File Offset: 0x0006D13F
	public IEnumerator ShowPromptText(Transform t = null, float destY = 95f)
	{
		if (!t)
		{
			t = this.promptTrans;
		}
		RectTransform rectTransform = t.GetComponent<RectTransform>();
		Vector3 dest = new Vector3(0f, destY, 0f);
		float timeOut = 0f;
		while (timeOut < 1f)
		{
			timeOut += Time.deltaTime * 2f;
			rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, dest, timeOut / 1f);
			yield return null;
		}
		rectTransform.anchoredPosition = dest;
		yield break;
	}

	// Token: 0x06000ADA RID: 2778 RVA: 0x0006EF5C File Offset: 0x0006D15C
	public IEnumerator HidePromptText(Transform t = null, float destY = -105f)
	{
		if (!t)
		{
			t = this.promptTrans;
		}
		RectTransform rectTransform = t.GetComponent<RectTransform>();
		Vector3 dest = new Vector3(0f, destY, 0f);
		float timeOut = 0f;
		while (timeOut < 1f)
		{
			timeOut += Time.deltaTime * 2f;
			rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, dest, timeOut / 1f);
			yield return null;
		}
		rectTransform.anchoredPosition = dest;
		yield break;
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x0006EF79 File Offset: 0x0006D179
	public void EndMissionSuccessfully()
	{
		if (this.ending)
		{
			return;
		}
		MetaProgressSaveManager.main.CompleteMission(Singleton.Instance.mission);
		this.ending = true;
		base.StartCoroutine(this.EndOverTime());
	}

	// Token: 0x06000ADC RID: 2780 RVA: 0x0006EFAC File Offset: 0x0006D1AC
	public IEnumerator EndOverTime()
	{
		yield return new WaitForSeconds(1.1f);
		GameObject gameObject = GameObject.FindGameObjectWithTag("UI Canvas");
		Object.Instantiate<GameObject>(this.fadeOutPrefab, gameObject.transform).transform.SetAsLastSibling();
		yield return new WaitForSeconds(2.2f);
		SceneLoader.main.LoadScene("Overworld", LoadSceneMode.Single, null, null);
		yield break;
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x0006EFBC File Offset: 0x0006D1BC
	public void ReturnToOrderia()
	{
		SaveAnItemOnLoss saveAnItemOnLoss = Object.FindObjectOfType<SaveAnItemOnLoss>();
		if (saveAnItemOnLoss)
		{
			saveAnItemOnLoss.SaveItem();
		}
		SceneLoader.main.LoadScene("Overworld", LoadSceneMode.Single, null, null);
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x0006EFEF File Offset: 0x0006D1EF
	public void EndDemo()
	{
		GameManager.QuitGame(true);
	}

	// Token: 0x06000ADF RID: 2783 RVA: 0x0006EFF8 File Offset: 0x0006D1F8
	public static void QuitGame(bool overrideIronMan = false)
	{
		SoundManager.main.PlaySFX("menuBlip");
		if (Singleton.Instance.ironMan)
		{
			if (overrideIronMan || GameManager.main.CanSave())
			{
				GameManager gameManager = GameManager.main;
				gameManager.StartCoroutine(gameManager.SaveAndQuit());
				return;
			}
		}
		else
		{
			SceneLoader.main.LoadScene("MainMenu", LoadSceneMode.Single, null, null);
		}
	}

	// Token: 0x04000838 RID: 2104
	public GameObject emitterHeatInformationGameObject;

	// Token: 0x04000839 RID: 2105
	public List<Item2> victoryItems = new List<Item2>();

	// Token: 0x0400083A RID: 2106
	public bool startMusicOnMap;

	// Token: 0x0400083B RID: 2107
	public GameManager.InventoryPhase inventoryPhase;

	// Token: 0x0400083C RID: 2108
	public GameManager.ViewingTop viewingTop;

	// Token: 0x0400083D RID: 2109
	[SerializeField]
	private float commons;

	// Token: 0x0400083E RID: 2110
	[SerializeField]
	private float uncommons;

	// Token: 0x0400083F RID: 2111
	[SerializeField]
	private float rares;

	// Token: 0x04000840 RID: 2112
	[SerializeField]
	private float legendaries;

	// Token: 0x04000841 RID: 2113
	[Header("Items")]
	[SerializeField]
	public List<Item2> itemsToSpawn;

	// Token: 0x04000842 RID: 2114
	[SerializeField]
	public List<Item2> defaultItems;

	// Token: 0x04000843 RID: 2115
	[SerializeField]
	public List<Item2> curses;

	// Token: 0x04000844 RID: 2116
	[SerializeField]
	public List<Item2> blessings;

	// Token: 0x04000845 RID: 2117
	[SerializeField]
	public List<Item2> relics;

	// Token: 0x04000846 RID: 2118
	[SerializeField]
	public List<Item2> nonStandardItems;

	// Token: 0x04000847 RID: 2119
	[Header("Other")]
	[SerializeField]
	private GameObject selectItemOnDeadPrefab;

	// Token: 0x04000848 RID: 2120
	[SerializeField]
	private TextMeshProUGUI timerText;

	// Token: 0x04000849 RID: 2121
	[SerializeField]
	private GameObject pauseMenu;

	// Token: 0x0400084A RID: 2122
	public Transform mainGridParent;

	// Token: 0x0400084B RID: 2123
	public static GameManager main;

	// Token: 0x0400084C RID: 2124
	private RunTypeManager runTypeManager;

	// Token: 0x0400084D RID: 2125
	private TextMeshProUGUI runTypeText;

	// Token: 0x0400084E RID: 2126
	[SerializeField]
	private InputHandler mapInputHandler;

	// Token: 0x0400084F RID: 2127
	[SerializeField]
	private InputHandler inventoryInputHandler;

	// Token: 0x04000850 RID: 2128
	[SerializeField]
	public GameObject caveInParticles;

	// Token: 0x04000851 RID: 2129
	[SerializeField]
	public GameObject caveInCollapseParticles;

	// Token: 0x04000852 RID: 2130
	[SerializeField]
	public GameObject highlightAreaForValidPlacement;

	// Token: 0x04000853 RID: 2131
	[SerializeField]
	public GameObject highlightAreaForValidMovement;

	// Token: 0x04000854 RID: 2132
	public int goldAmount;

	// Token: 0x04000855 RID: 2133
	public TextMeshProUGUI goldText;

	// Token: 0x04000856 RID: 2134
	public TextMeshProUGUI curseText;

	// Token: 0x04000857 RID: 2135
	[SerializeField]
	private GameObject goldIndicatorPrefab;

	// Token: 0x04000858 RID: 2136
	[SerializeField]
	private GameObject curseIndicatorPrefab;

	// Token: 0x04000859 RID: 2137
	public string currentEnemyEncounter = "";

	// Token: 0x0400085A RID: 2138
	public int currentHealthAmount;

	// Token: 0x0400085B RID: 2139
	[SerializeField]
	private GameObject itemAtlasPrefab;

	// Token: 0x0400085C RID: 2140
	public DungeonLevel.EnemyEncounter2 chosenBoss;

	// Token: 0x0400085D RID: 2141
	[SerializeField]
	public Material standardItemMaterial;

	// Token: 0x0400085E RID: 2142
	[SerializeField]
	public Material outlineItemMaterial;

	// Token: 0x0400085F RID: 2143
	public GameObject carvingSpacerForTote;

	// Token: 0x04000860 RID: 2144
	public GameObject draggingCard;

	// Token: 0x04000861 RID: 2145
	[SerializeField]
	private GameObject reorganizeButton;

	// Token: 0x04000862 RID: 2146
	[SerializeField]
	private GameObject scratchButton;

	// Token: 0x04000863 RID: 2147
	[SerializeField]
	private List<GameObject> EventPrefabsForSaving;

	// Token: 0x04000864 RID: 2148
	[SerializeField]
	private Light2D globalLight;

	// Token: 0x04000865 RID: 2149
	public int floor;

	// Token: 0x04000866 RID: 2150
	[HideInInspector]
	public int floorsPerLevel = 3;

	// Token: 0x04000867 RID: 2151
	[HideInInspector]
	public Vector3 inventoryStartPosition;

	// Token: 0x04000868 RID: 2152
	[SerializeField]
	private TextMeshProUGUI floorText;

	// Token: 0x04000869 RID: 2153
	[SerializeField]
	private GameObject enemy;

	// Token: 0x0400086A RID: 2154
	[SerializeField]
	private GameObject button;

	// Token: 0x0400086B RID: 2155
	[SerializeField]
	private GameObject retryButton;

	// Token: 0x0400086C RID: 2156
	[SerializeField]
	private GameObject retryRunButton;

	// Token: 0x0400086D RID: 2157
	[SerializeField]
	private GameObject quitToMenuButton;

	// Token: 0x0400086E RID: 2158
	[SerializeField]
	private GameObject quitToOrderiaButton;

	// Token: 0x0400086F RID: 2159
	[SerializeField]
	public Transform itemsParent;

	// Token: 0x04000870 RID: 2160
	[SerializeField]
	private Transform combatVisualTransform;

	// Token: 0x04000871 RID: 2161
	[SerializeField]
	private Transform playerTransform;

	// Token: 0x04000872 RID: 2162
	private Animator playerAnimator;

	// Token: 0x04000873 RID: 2163
	public Item2 selectedItem;

	// Token: 0x04000874 RID: 2164
	public List<Item2.Cost> selectedCosts = new List<Item2.Cost>();

	// Token: 0x04000875 RID: 2165
	[SerializeField]
	public Transform inventoryTransform;

	// Token: 0x04000876 RID: 2166
	[SerializeField]
	public Transform mapTransform;

	// Token: 0x04000877 RID: 2167
	[SerializeField]
	private Transform promptTrans;

	// Token: 0x04000878 RID: 2168
	[HideInInspector]
	public TextMeshProUGUI promptText;

	// Token: 0x04000879 RID: 2169
	[SerializeField]
	private GameObject mapandInventoryButtons;

	// Token: 0x0400087A RID: 2170
	[SerializeField]
	private GameObject mapButton;

	// Token: 0x0400087B RID: 2171
	[SerializeField]
	private GameObject inventoryButton;

	// Token: 0x0400087C RID: 2172
	[SerializeField]
	public GameObject endTurnButton;

	// Token: 0x0400087D RID: 2173
	[SerializeField]
	public GameObject stopActionButton;

	// Token: 0x0400087E RID: 2174
	[SerializeField]
	private GameObject afterBattleButton;

	// Token: 0x0400087F RID: 2175
	[SerializeField]
	private GameObject fadeOut;

	// Token: 0x04000880 RID: 2176
	[SerializeField]
	private Animator heartAnimator;

	// Token: 0x04000881 RID: 2177
	public List<GameObject> objectsToRecover = new List<GameObject>();

	// Token: 0x04000882 RID: 2178
	public Player player;

	// Token: 0x04000883 RID: 2179
	public bool travelling;

	// Token: 0x04000884 RID: 2180
	[SerializeField]
	public Transform upperLeft;

	// Token: 0x04000885 RID: 2181
	[SerializeField]
	private Animator announcementAnimator;

	// Token: 0x04000886 RID: 2182
	[SerializeField]
	public GameObject finishReorganizingButton;

	// Token: 0x04000887 RID: 2183
	[SerializeField]
	private GameObject skipReorganizingButton;

	// Token: 0x04000888 RID: 2184
	[SerializeField]
	public GameObject battleActionButtons;

	// Token: 0x04000889 RID: 2185
	[SerializeField]
	public GameObject outOfBattleButtons;

	// Token: 0x0400088A RID: 2186
	[SerializeField]
	private GameObject reorganizePrompt;

	// Token: 0x0400088B RID: 2187
	public GameObject reorgnizeItem;

	// Token: 0x0400088C RID: 2188
	public bool buttonPressed = true;

	// Token: 0x0400088D RID: 2189
	[HideInInspector]
	public EventButton eventButton;

	// Token: 0x0400088E RID: 2190
	[SerializeField]
	public Transform spawnPosition;

	// Token: 0x0400088F RID: 2191
	[SerializeField]
	public Transform inventoryItemsParent;

	// Token: 0x04000890 RID: 2192
	[SerializeField]
	private GameObject itemDestroyParticlePrefab;

	// Token: 0x04000891 RID: 2193
	[SerializeField]
	public Transform eventsParent;

	// Token: 0x04000892 RID: 2194
	public List<int> rarities;

	// Token: 0x04000893 RID: 2195
	[HideInInspector]
	public int battleSpawnItems;

	// Token: 0x04000894 RID: 2196
	private TutorialManager tutorialManager;

	// Token: 0x04000895 RID: 2197
	[SerializeField]
	public Transform tutorialText;

	// Token: 0x04000896 RID: 2198
	[SerializeField]
	private GameObject clickMeDungeon;

	// Token: 0x04000897 RID: 2199
	public int objsInGrid;

	// Token: 0x04000898 RID: 2200
	[SerializeField]
	private GameObject event1;

	// Token: 0x04000899 RID: 2201
	private GameFlowManager gameFlowManager;

	// Token: 0x0400089A RID: 2202
	[HideInInspector]
	public Enemy targetedEnemy;

	// Token: 0x0400089B RID: 2203
	[SerializeField]
	private Transform targetTransform;

	// Token: 0x0400089C RID: 2204
	[SerializeField]
	private GameObject options;

	// Token: 0x0400089D RID: 2205
	[SerializeField]
	private GameObject popUpPrefab;

	// Token: 0x0400089E RID: 2206
	[SerializeField]
	private GameObject levelTransition;

	// Token: 0x0400089F RID: 2207
	[SerializeField]
	private TextMeshProUGUI levelName;

	// Token: 0x040008A0 RID: 2208
	[SerializeField]
	private TextMeshProUGUI levelPart;

	// Token: 0x040008A1 RID: 2209
	[SerializeField]
	public GameObject rotateButton;

	// Token: 0x040008A2 RID: 2210
	public List<GameObject> rewardItems;

	// Token: 0x040008A3 RID: 2211
	public bool dead;

	// Token: 0x040008A4 RID: 2212
	public GameObject draggingItem;

	// Token: 0x040008A5 RID: 2213
	public bool viewingEvent;

	// Token: 0x040008A6 RID: 2214
	[SerializeField]
	private List<GameObject> viewingEventObject = new List<GameObject>();

	// Token: 0x040008A7 RID: 2215
	public bool viewingEventThroughObject;

	// Token: 0x040008A8 RID: 2216
	public DungeonEvent.DungeonEventType currentEventType;

	// Token: 0x040008A9 RID: 2217
	public bool anchorItemsAfterSpecialReorganization;

	// Token: 0x040008AA RID: 2218
	private ES3Settings _settings;

	// Token: 0x040008AB RID: 2219
	public int cursesSkipped;

	// Token: 0x040008AC RID: 2220
	public int totalNumOfItemsAllowedToTake;

	// Token: 0x040008AD RID: 2221
	public int numOfItemsAllowedToTake;

	// Token: 0x040008AE RID: 2222
	public bool limitedItemReorganize;

	// Token: 0x040008AF RID: 2223
	private int turnsSinceStone = 2;

	// Token: 0x040008B0 RID: 2224
	public GameObject textAmountPrefab;

	// Token: 0x040008B1 RID: 2225
	[SerializeField]
	private GameObject chestForLimitedReorganization;

	// Token: 0x040008B2 RID: 2226
	[SerializeField]
	private DungeonLevel.Zone chaoticDarkness;

	// Token: 0x040008B3 RID: 2227
	[SerializeField]
	private List<GameManager.ZonePlaylist> zonesPlayList;

	// Token: 0x040008B4 RID: 2228
	public int zoneNumber;

	// Token: 0x040008B5 RID: 2229
	public DungeonLevel dungeonLevel;

	// Token: 0x040008B6 RID: 2230
	[SerializeField]
	private SpriteRenderer backgroundColorSquareRenderer;

	// Token: 0x040008B7 RID: 2231
	[SerializeField]
	private GameObject statsBoxPrefab;

	// Token: 0x040008B8 RID: 2232
	private SaveManager saveManager;

	// Token: 0x040008B9 RID: 2233
	public List<GameObject> itemsForSpecialReorganization;

	// Token: 0x040008BA RID: 2234
	public bool skippableReorganization = true;

	// Token: 0x040008BB RID: 2235
	[SerializeField]
	private GameObject specialPrefab;

	// Token: 0x040008BC RID: 2236
	private Vector3 reorgGlobal;

	// Token: 0x040008BD RID: 2237
	public bool inSpecialReorg;

	// Token: 0x040008BE RID: 2238
	public bool destroySelectedItem;

	// Token: 0x040008BF RID: 2239
	private CR8Manager cR8Manager;

	// Token: 0x040008C0 RID: 2240
	private AnalyticsManager analyticsManager;

	// Token: 0x040008C1 RID: 2241
	public List<ItemMovement> itemsReplacedBySpecialReorgItem = new List<ItemMovement>();

	// Token: 0x040008C2 RID: 2242
	public bool standardSpawnAfter;

	// Token: 0x040008C3 RID: 2243
	[SerializeField]
	private GameObject chest;

	// Token: 0x040008C4 RID: 2244
	[SerializeField]
	private GameObject store;

	// Token: 0x040008C5 RID: 2245
	private Coroutine spawnCurses;

	// Token: 0x040008C6 RID: 2246
	private DungeonPlayer dungeonPlayer;

	// Token: 0x040008C7 RID: 2247
	private float debugTimer;

	// Token: 0x040008C8 RID: 2248
	private Coroutine moveAllItemsCoroutine;

	// Token: 0x040008C9 RID: 2249
	[SerializeField]
	private GameObject luckyMessagePrefab;

	// Token: 0x040008CA RID: 2250
	[SerializeField]
	private GameObject selectItemDialoguePrefab;

	// Token: 0x040008CB RID: 2251
	[SerializeField]
	public GameObject carvingSummonParticles;

	// Token: 0x040008CC RID: 2252
	private Coroutine moving;

	// Token: 0x040008CD RID: 2253
	private Coroutine reselectEnemyForDefendersRoutine;

	// Token: 0x040008CE RID: 2254
	public int petPlacementNumber;

	// Token: 0x040008CF RID: 2255
	[SerializeField]
	private GameObject petPlacementIcon;

	// Token: 0x040008D0 RID: 2256
	[SerializeField]
	private GameObject enemySpacer;

	// Token: 0x040008D1 RID: 2257
	private Coroutine startReorganizationCoroutine;

	// Token: 0x040008D2 RID: 2258
	public List<GameManager.AreasForSpecialReorganization> areasForSpecialReorganizations = new List<GameManager.AreasForSpecialReorganization>();

	// Token: 0x040008D3 RID: 2259
	private Enemy.Attack curseAttack;

	// Token: 0x040008D4 RID: 2260
	private bool ending;

	// Token: 0x040008D5 RID: 2261
	[SerializeField]
	private GameObject fadeOutPrefab;

	// Token: 0x020003AA RID: 938
	public enum InventoryPhase
	{
		// Token: 0x0400161A RID: 5658
		locked,
		// Token: 0x0400161B RID: 5659
		open,
		// Token: 0x0400161C RID: 5660
		choose,
		// Token: 0x0400161D RID: 5661
		specialReorganization,
		// Token: 0x0400161E RID: 5662
		dead,
		// Token: 0x0400161F RID: 5663
		inCombatMove,
		// Token: 0x04001620 RID: 5664
		clickItem,
		// Token: 0x04001621 RID: 5665
		placePet,
		// Token: 0x04001622 RID: 5666
		notInteractable
	}

	// Token: 0x020003AB RID: 939
	public enum ViewingTop
	{
		// Token: 0x04001624 RID: 5668
		inventory,
		// Token: 0x04001625 RID: 5669
		map
	}

	// Token: 0x020003AC RID: 940
	[Serializable]
	private class ZonePlaylist
	{
		// Token: 0x04001626 RID: 5670
		public List<DungeonLevel.Zone> zones;
	}

	// Token: 0x020003AD RID: 941
	[Serializable]
	public class AreasForSpecialReorganization
	{
		// Token: 0x04001627 RID: 5671
		public List<Vector2> localPositions;

		// Token: 0x04001628 RID: 5672
		public Transform gridParent;
	}

	// Token: 0x020003AE RID: 942
	public enum ButtonsToEnable
	{
		// Token: 0x0400162A RID: 5674
		finishReorganizingButton,
		// Token: 0x0400162B RID: 5675
		skipReorganizingButton,
		// Token: 0x0400162C RID: 5676
		endTurnButton,
		// Token: 0x0400162D RID: 5677
		stopActionButton
	}
}
