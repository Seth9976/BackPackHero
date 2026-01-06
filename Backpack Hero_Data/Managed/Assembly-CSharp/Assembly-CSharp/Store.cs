using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x020000D2 RID: 210
public class Store : CustomInputHandler
{
	// Token: 0x0600060F RID: 1551 RVA: 0x0003BA18 File Offset: 0x00039C18
	private void Start()
	{
		this.gameManager = GameManager.main;
		this.conversationText.text = LangaugeManager.main.GetTextByKey("evss");
		LangaugeManager.main.SetFont(this.conversationText.transform);
		if (this.carvingStore && this.dungeonEvent.GetEventPropertyValue(DungeonEvent.EventProperty.Type.boughtItems) < this.willAllowForThisManySales)
		{
			Tote tote = Object.FindObjectOfType<Tote>();
			if (tote && Player.main.characterName == Character.CharacterName.Tote && tote.undrawnCarvingsIcon)
			{
				tote.undrawnCarvingsIcon.GetChild(1).gameObject.SetActive(true);
				tote.undrawnCarvingsIcon.GetChild(1).gameObject.GetComponentInChildren<TextMeshPro>().text = LangaugeManager.main.GetTextByKey("removeHere");
			}
		}
		Item2.MarkAllAsOwned();
		this.SetPrices();
		this.HideSellText();
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x0003BAF8 File Offset: 0x00039CF8
	private void Update()
	{
		this.time += Time.deltaTime;
		if (this.time > 0.0125f)
		{
			TextMeshProUGUI textMeshProUGUI = this.conversationText;
			int maxVisibleCharacters = textMeshProUGUI.maxVisibleCharacters;
			textMeshProUGUI.maxVisibleCharacters = maxVisibleCharacters + 1;
			this.time = 0f;
		}
		if (this.sellHereText && this.sellHereText.activeInHierarchy && this.gameManager.draggingItem == null)
		{
			this.sellHereText.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x0003BB84 File Offset: 0x00039D84
	public void ShowSellText(Item2 myItem)
	{
		if (myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.cantBeSold))
		{
			return;
		}
		if (this.text)
		{
			this.text.SetActive(false);
		}
		this.sellHereText.gameObject.SetActive(true);
		LangaugeManager.main.SetFont(this.sellHereText.transform);
		if (this.dungeonEvent && this.dungeonEvent.GetEventPropertyValue(DungeonEvent.EventProperty.Type.boughtItems) >= this.willAllowForThisManySales)
		{
			this.sellHereText.GetComponentInChildren<TextMeshPro>().text = LangaugeManager.main.GetTextByKey("noMoreSale");
			return;
		}
		if ((!this.carvingStore && myItem.itemType.Contains(Item2.ItemType.Carving)) || (this.carvingStore && !myItem.itemType.Contains(Item2.ItemType.Carving)))
		{
			this.sellHereText.GetComponentInChildren<TextMeshPro>().text = LangaugeManager.main.GetTextByKey("cantSell");
			return;
		}
		if (this.carvingStore)
		{
			this.sellHereText.GetComponentInChildren<TextMeshPro>().text = LangaugeManager.main.GetTextByKey("removeHere");
			return;
		}
		this.sellHereText.GetComponentInChildren<TextMeshPro>().text = LangaugeManager.main.GetTextByKey("sellHere");
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x0003BCB1 File Offset: 0x00039EB1
	public void HideSellText()
	{
		if (this.text)
		{
			this.text.SetActive(true);
		}
		this.sellHereText.gameObject.SetActive(false);
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x0003BCE0 File Offset: 0x00039EE0
	public void AddFakeGold()
	{
		Vector3 position = new Vector3(-5.5f, 5f, -7f);
		foreach (StackableItem stackableItem in Object.FindObjectsOfType<StackableItem>())
		{
			if (stackableItem.GetComponent<Item2>().itemType.Contains(Item2.ItemType.Gold))
			{
				position = stackableItem.transform.position;
				break;
			}
		}
		Object.Instantiate<GameObject>(this.fakeGoldPrefab, position, Quaternion.identity).GetComponent<FakeGold>().dest = base.transform;
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x0003BD5E File Offset: 0x00039F5E
	private IEnumerator DeleteGold()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		yield return new WaitForSeconds(0.1f);
		yield break;
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x0003BD68 File Offset: 0x00039F68
	public void CannotAfford()
	{
		string randomTextFromMasterKey = LangaugeManager.main.GetRandomTextFromMasterKey("evsca", 10);
		this.conversationText.text = randomTextFromMasterKey;
		this.conversationText.maxVisibleCharacters = 0;
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x0003BDA0 File Offset: 0x00039FA0
	public void GoodBye()
	{
		if (this.carvingStore && Singleton.Instance.character == Character.CharacterName.Tote)
		{
			Tote tote = Object.FindObjectOfType<Tote>();
			if (tote)
			{
				tote.undrawnCarvingsIcon.GetChild(1).gameObject.SetActive(false);
			}
		}
		List<GameObject> list = new List<GameObject>();
		foreach (ItemMovement itemMovement in Object.FindObjectsOfType<ItemMovement>())
		{
			if (itemMovement.returnsToOutOfInventoryPosition && !itemMovement.inGrid && itemMovement.GetComponent<Item2>().isForSale)
			{
				list.Add(itemMovement.gameObject);
			}
		}
		if (this.dungeonEvent)
		{
			this.dungeonEvent.StoreItems(list);
		}
		string randomTextFromMasterKey = LangaugeManager.main.GetRandomTextFromMasterKey("evsgb", 10);
		this.conversationText.text = randomTextFromMasterKey;
		this.conversationText.maxVisibleCharacters = 0;
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x0003BE7C File Offset: 0x0003A07C
	public void AddCost()
	{
		string randomTextFromMasterKey = LangaugeManager.main.GetRandomTextFromMasterKey("evsa", 7);
		this.conversationText.text = randomTextFromMasterKey;
		this.conversationText.maxVisibleCharacters = 0;
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x0003BEB4 File Offset: 0x0003A0B4
	public void GetFly()
	{
		AchievementAbstractor.instance.ConsiderAchievement("Haggler");
		string randomTextFromMasterKey = LangaugeManager.main.GetRandomTextFromMasterKey("evsf", 10);
		this.conversationText.text = randomTextFromMasterKey;
		this.conversationText.maxVisibleCharacters = 0;
		this.discountAll = true;
		foreach (Item2 item in Object.FindObjectsOfType<Item2>())
		{
			if (!item.itemType.Contains(Item2.ItemType.Gold) && !item.itemType.Contains(Item2.ItemType.Mana))
			{
				item.cost = -999;
			}
		}
		this.SetPrices();
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x0003BF48 File Offset: 0x0003A148
	public void RemoveCost(int amount, Item2 item)
	{
		string text = LangaugeManager.main.GetRandomTextFromMasterKey("evss", 10);
		if (item.isForSale)
		{
			text = LangaugeManager.main.GetRandomTextFromMasterKey("evsr", 10);
		}
		this.conversationText.text = text;
		this.conversationText.maxVisibleCharacters = 0;
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x0003BF99 File Offset: 0x0003A199
	public bool CorrectStoreType(Item2 item)
	{
		return (this.carvingStore || !item.itemType.Contains(Item2.ItemType.Carving)) && (!this.carvingStore || item.itemType.Contains(Item2.ItemType.Carving));
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x0003BFCC File Offset: 0x0003A1CC
	public bool BuyPlayerItem(Item2 item)
	{
		if (this.dungeonEvent && this.dungeonEvent.GetEventPropertyValue(DungeonEvent.EventProperty.Type.boughtItems) >= this.willAllowForThisManySales)
		{
			this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("noMoreSale"));
			return false;
		}
		if (item.CheckForStatusEffect(Item2.ItemStatusEffect.Type.cantBeSold))
		{
			PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm98"), item.transform.position);
			return false;
		}
		if (RunTypeManager.CheckIfAssignedItemIsInProperty(RunType.RunProperty.Type.mustKeep, item.gameObject))
		{
			PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm98"), item.transform.position);
			return false;
		}
		if ((!this.carvingStore && item.itemType.Contains(Item2.ItemType.Carving)) || (this.carvingStore && !item.itemType.Contains(Item2.ItemType.Carving)))
		{
			return false;
		}
		if (item.itemType.Contains(Item2.ItemType.Carving))
		{
			if (!this.carvingStore)
			{
				return false;
			}
			if (item.cost > this.gameManager.GetCurrentGold())
			{
				this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm35"));
				return false;
			}
			this.gameManager.ChangeGold(item.cost * -1);
			Object.FindObjectOfType<Tote>().DeleteCarving(item.gameObject);
		}
		else
		{
			AnalyticsManager analyticsManager = Object.FindObjectOfType<AnalyticsManager>();
			if (analyticsManager)
			{
				analyticsManager.AddItem("itemsSold", Item2.GetDisplayName(item.name));
			}
			item.itemMovement.DelayDestroy();
			SoundManager.main.PlaySFX("destroy" + Random.Range(1, 3).ToString());
			this.gameManager.ChangeGold(item.cost);
		}
		GameFlowManager.main.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onSale, item, null, true, false);
		if (this.dungeonEvent)
		{
			this.dungeonEvent.AddEventProperty(DungeonEvent.EventProperty.Type.boughtItems, 1);
		}
		return true;
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x0003C1AF File Offset: 0x0003A3AF
	public void ResetSales()
	{
		if (this.dungeonEvent)
		{
			this.dungeonEvent.RemoveEventProperty(DungeonEvent.EventProperty.Type.boughtItems);
		}
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x0003C1CA File Offset: 0x0003A3CA
	public override void OnPressStart(string keyName, bool overrideKeyName)
	{
		if (keyName != "confirm" && !overrideKeyName)
		{
			return;
		}
		this.OpenStore();
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x0003C1E3 File Offset: 0x0003A3E3
	private void OnMouseUpAsButton()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.OpenStore();
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x0003C1F8 File Offset: 0x0003A3F8
	public void OpenStore()
	{
		if (this.isOpen || this.gameManager.travelling)
		{
			return;
		}
		MetaProgressSaveManager.main.AddRunEvent(MetaProgressSaveManager.LastRun.RunEvents.wentShopping);
		Object.Destroy(this.text);
		DigitalInputSelectOnButton componentInParent = base.GetComponentInParent<DigitalInputSelectOnButton>();
		if (componentInParent)
		{
			componentInParent.RemoveSymbol();
		}
		InputHandler componentInParent2 = base.GetComponentInParent<InputHandler>();
		if (componentInParent2)
		{
			Object.Destroy(componentInParent2);
		}
		Item2.MarkAllAsOwned();
		string randomTextFromMasterKey = LangaugeManager.main.GetRandomTextFromMasterKey("evso", 10);
		this.conversationText.text = randomTextFromMasterKey;
		this.conversationText.maxVisibleCharacters = 0;
		this.gameManager.ShowInventory();
		SoundManager.main.PlaySFX("openChest");
		this.isOpen = true;
		List<GameObject> list = new List<GameObject>();
		bool flag = false;
		if (this.dungeonEvent)
		{
			flag = this.dungeonEvent.GetItems(out list);
		}
		List<GameObject> list2 = new List<GameObject>();
		if (!flag)
		{
			if (!this.carvingStore)
			{
				list2 = this.gameManager.GetItems(base.transform.position, 7, 10f, 0, true, null);
				list2.Add(this.gameManager.SpawnItem(new List<Item2.ItemType> { Item2.ItemType.Any }, new List<Item2.Rarity> { Item2.Rarity.Rare }, true, null));
				list2.Add(this.gameManager.SpawnItem(new List<Item2.ItemType> { Item2.ItemType.Any }, new List<Item2.Rarity> { Item2.Rarity.Legendary }, true, null));
				int num = 0;
				int num2 = 0;
				int num3 = 4;
				foreach (GameObject gameObject in list2)
				{
					ItemMovement component = gameObject.GetComponent<ItemMovement>();
					gameObject.GetComponent<SpriteRenderer>().color = Color.white;
					gameObject.transform.rotation = Quaternion.identity;
					gameObject.transform.position = new Vector3((float)(num * 2 - num3 / 2 - 1), -4f - (float)num2 * 1.5f, 0f);
					component.outOfInventoryPosition = gameObject.transform.position;
					component.outOfInventoryRotation = gameObject.transform.rotation;
					component.returnsToOutOfInventoryPosition = true;
					gameObject.GetComponent<Item2>().isForSale = true;
					num++;
					if (num >= 4 && num2 == 0)
					{
						num3 = 6;
						num = 0;
						num2++;
					}
				}
				if (this.canBeCurseReplaced)
				{
					this.gameManager.ConsiderCurseReplacement(list2);
					goto IL_0311;
				}
				goto IL_0311;
			}
			else
			{
				list2 = Object.FindObjectOfType<Tote>().SpawnCarvings();
				using (List<GameObject>.Enumerator enumerator = list2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameObject gameObject2 = enumerator.Current;
						gameObject2.GetComponent<Item2>().isForSale = true;
					}
					goto IL_0311;
				}
			}
		}
		list2 = list;
		foreach (GameObject gameObject3 in list2)
		{
			gameObject3.GetComponent<ItemMovement>().SetupPositionAndParent();
			gameObject3.GetComponent<SpriteRenderer>().color = Color.white;
			gameObject3.transform.rotation = Quaternion.identity;
			gameObject3.transform.localScale = Vector3.one;
		}
		IL_0311:
		this.gameManager.numOfItemsAllowedToTake = 99;
		this.gameManager.totalNumOfItemsAllowedToTake = 99;
		this.SetPrices();
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x0003C560 File Offset: 0x0003A760
	public void SetPrices()
	{
		foreach (Item2 item in Object.FindObjectsOfType<Item2>())
		{
			if ((!item.CheckForStatusEffect(Item2.ItemStatusEffect.Type.cantBeSold) || !item.isOwned) && item.cost == -999)
			{
				if (item.originalCost != -999)
				{
					item.cost = item.originalCost;
				}
				else
				{
					if (item.rarity == Item2.Rarity.Common)
					{
						item.cost = 6;
					}
					else if (item.rarity == Item2.Rarity.Uncommon)
					{
						item.cost = 12;
					}
					else if (item.rarity == Item2.Rarity.Rare)
					{
						item.cost = 20;
					}
					else if (item.rarity == Item2.Rarity.Legendary)
					{
						item.cost = 35;
					}
					item.isDiscounted = false;
					if (item.isOwned)
					{
						item.cost /= 2;
						if (item.itemType.Contains(Item2.ItemType.Carving))
						{
							item.cost = 10;
						}
					}
					else if (this.discountAll)
					{
						item.isDiscounted = true;
						item.cost /= 2;
					}
					else if (Random.Range(0, 7) == 0)
					{
						item.isDiscounted = true;
						item.cost /= 2;
					}
				}
			}
		}
	}

	// Token: 0x040004DF RID: 1247
	[SerializeField]
	public bool carvingStore;

	// Token: 0x040004E0 RID: 1248
	[SerializeField]
	private GameObject goldPrefab;

	// Token: 0x040004E1 RID: 1249
	[SerializeField]
	private GameObject fakeGoldPrefab;

	// Token: 0x040004E2 RID: 1250
	[HideInInspector]
	public bool isOpen;

	// Token: 0x040004E3 RID: 1251
	[SerializeField]
	private TextMeshProUGUI conversationText;

	// Token: 0x040004E4 RID: 1252
	[SerializeField]
	private GameObject text;

	// Token: 0x040004E5 RID: 1253
	[SerializeField]
	private GameObject sellHereText;

	// Token: 0x040004E6 RID: 1254
	public bool canBeCurseReplaced;

	// Token: 0x040004E7 RID: 1255
	public DungeonEvent dungeonEvent;

	// Token: 0x040004E8 RID: 1256
	private GameManager gameManager;

	// Token: 0x040004E9 RID: 1257
	private int totalMoney;

	// Token: 0x040004EA RID: 1258
	private bool discountAll;

	// Token: 0x040004EB RID: 1259
	private float time;

	// Token: 0x040004EC RID: 1260
	[SerializeField]
	public int willAllowForThisManySales = 3;
}
