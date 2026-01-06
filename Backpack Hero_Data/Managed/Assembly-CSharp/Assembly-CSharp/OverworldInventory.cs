using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200007B RID: 123
public class OverworldInventory : MonoBehaviour
{
	// Token: 0x06000273 RID: 627 RVA: 0x0000EC51 File Offset: 0x0000CE51
	private void OnEnable()
	{
		Overworld_InventoryManager.currentInventories++;
	}

	// Token: 0x06000274 RID: 628 RVA: 0x0000EC5F File Offset: 0x0000CE5F
	private void OnDisable()
	{
		Overworld_InventoryManager.currentInventories--;
	}

	// Token: 0x06000275 RID: 629 RVA: 0x0000EC70 File Offset: 0x0000CE70
	public void SortBy()
	{
		int value = this.sortDropdown.value;
		if (value == 0)
		{
			this.originOfItems.Sort((string x, string y) => Item2.GetDisplayName(x).CompareTo(Item2.GetDisplayName(y)));
		}
		else if (value == 1)
		{
			this.originOfItems.Sort((string x, string y) => DebugItemManager.main.GetItem2ByName(x).itemType[0].ToString().CompareTo(DebugItemManager.main.GetItem2ByName(y).itemType[0].ToString()));
		}
		else if (value == 2)
		{
			this.originOfItems.Sort((string x, string y) => DebugItemManager.main.GetItem2ByName(x).rarity.CompareTo(DebugItemManager.main.GetItem2ByName(y).rarity));
		}
		this.SetupItemsForInventory(this.currentPage * 50, (this.currentPage + 1) * 50);
		this.SelectFirstItem();
	}

	// Token: 0x06000276 RID: 630 RVA: 0x0000ED36 File Offset: 0x0000CF36
	public void SelectFirstItem()
	{
		if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.controller)
		{
			DigitalCursor.main.SelectFirstSelectableInElement(this.itemsParent.transform);
		}
	}

	// Token: 0x06000277 RID: 631 RVA: 0x0000ED5A File Offset: 0x0000CF5A
	private void Start()
	{
		this.buildingInterface = base.GetComponentInParent<Overworld_BuildingInterface>();
		this.gridLayoutGroup = this.itemsParent.GetComponent<GridLayoutGroup>();
		if (this.isFilteredInventory)
		{
			return;
		}
		this.UpdateBuildingInterface();
		OverworldInventory.UpdateAllPages();
	}

	// Token: 0x06000278 RID: 632 RVA: 0x0000ED8D File Offset: 0x0000CF8D
	private void Update()
	{
	}

	// Token: 0x06000279 RID: 633 RVA: 0x0000ED8F File Offset: 0x0000CF8F
	public string GetFirstItemString()
	{
		if (this.originOfItems == null || this.originOfItems.Count == 0)
		{
			return "";
		}
		return this.originOfItems[0];
	}

	// Token: 0x0600027A RID: 634 RVA: 0x0000EDB8 File Offset: 0x0000CFB8
	public int GetItemNumber()
	{
		return this.currentPage * 50;
	}

	// Token: 0x0600027B RID: 635 RVA: 0x0000EDC4 File Offset: 0x0000CFC4
	private void SetupItemNamesProperly()
	{
		if (this.originOfItems == null)
		{
			return;
		}
		for (int i = 0; i < this.originOfItems.Count; i++)
		{
			this.originOfItems[i] = Item2.GetDisplayName(this.originOfItems[i]);
			Item2 item2ByName = DebugItemManager.main.GetItem2ByName(this.originOfItems[i]);
			if (!item2ByName)
			{
				this.originOfItems.RemoveAt(i);
				i--;
			}
			else if (item2ByName.markerIfBroughtHome != MetaProgressSaveManager.MetaProgressMarker.none)
			{
				MetaProgressSaveManager.main.AddMetaProgressMarker(item2ByName.markerIfBroughtHome);
			}
		}
	}

	// Token: 0x0600027C RID: 636 RVA: 0x0000EE58 File Offset: 0x0000D058
	public void ConsiderPageButtonsToShow()
	{
		if (this.currentPage <= 0)
		{
			this.previousPageButton.SetActive(false);
		}
		else
		{
			this.previousPageButton.SetActive(true);
		}
		if (this.currentPage >= this.maxPage - 1 || this.maxPage <= 1)
		{
			this.nextPageButton.SetActive(false);
			return;
		}
		this.nextPageButton.SetActive(true);
	}

	// Token: 0x0600027D RID: 637 RVA: 0x0000EEBA File Offset: 0x0000D0BA
	public void ToggleTakeAll(bool active)
	{
		List<string> list = this.originOfItems;
	}

	// Token: 0x0600027E RID: 638 RVA: 0x0000EEC4 File Offset: 0x0000D0C4
	public static void UpdateAllPages()
	{
		OverworldInventory[] array = Object.FindObjectsOfType<OverworldInventory>();
		if (array.Length > 1)
		{
			Overworld_InventoryManager.main.ToggleSwapButtons(true);
			if (array[0].originOfItems.Count >= 1)
			{
				Overworld_InventoryManager.main.ToggleSwapButton2(true);
			}
			else
			{
				Overworld_InventoryManager.main.ToggleSwapButton2(false);
			}
			if (array[1].HasItemsThatAreValid(array[0]))
			{
				Overworld_InventoryManager.main.ToggleSwapButton1(true);
			}
			else
			{
				Overworld_InventoryManager.main.ToggleSwapButton1(false);
			}
			array[0].ToggleTakeAll(true);
			array[1].ToggleTakeAll(true);
		}
		else if (array.Length == 1)
		{
			Overworld_InventoryManager.main.ToggleSwapButtons(false);
			array[0].ToggleTakeAll(false);
		}
		foreach (OverworldInventory overworldInventory in array)
		{
			overworldInventory.UpdatePages();
			overworldInventory.UpdateBuildingInterface();
		}
	}

	// Token: 0x0600027F RID: 639 RVA: 0x0000EF84 File Offset: 0x0000D184
	public void UpdatePages()
	{
		this.maxPage = Mathf.Max(1, Mathf.CeilToInt((float)this.originOfItems.Count / 50f));
		this.currentPage = Mathf.Clamp(this.currentPage, 0, this.maxPage - 1);
		this.SetupItemsForInventory(this.currentPage * 50, (this.currentPage + 1) * 50);
		this.ConsiderPageButtonsToShow();
	}

	// Token: 0x06000280 RID: 640 RVA: 0x0000EFEE File Offset: 0x0000D1EE
	public void PreviousPage()
	{
		if (this.currentPage <= 0)
		{
			return;
		}
		this.currentPage--;
		this.SetupItemsForInventory(this.currentPage * 50, (this.currentPage + 1) * 50);
		this.ConsiderPageButtonsToShow();
	}

	// Token: 0x06000281 RID: 641 RVA: 0x0000F028 File Offset: 0x0000D228
	public void NextPage()
	{
		if (this.currentPage >= this.maxPage)
		{
			return;
		}
		this.currentPage++;
		this.SetupItemsForInventory(this.currentPage * 50, (this.currentPage + 1) * 50);
		this.ConsiderPageButtonsToShow();
	}

	// Token: 0x06000282 RID: 642 RVA: 0x0000F068 File Offset: 0x0000D268
	public void Setup(List<string> originOfItems, Sprite sprite = null, Overworld_InventoryManager.ClickAction clickAction = Overworld_InventoryManager.ClickAction.NONE)
	{
		this.isFilteredInventory = false;
		if (sprite != null)
		{
			this.topSprite.sprite = sprite;
		}
		if (sprite)
		{
			this.inputHandlerForSortingMenu.SetKey(InputHandler.Key.RightBumper);
		}
		this.originOfItems = originOfItems;
		this.clickAction = clickAction;
		this.SetupItemNamesProperly();
		this.currentPage = 0;
		this.maxPage = Mathf.Max(1, Mathf.CeilToInt((float)originOfItems.Count / 50f));
		this.ConsiderPageButtonsToShow();
		this.SetupItemsForInventory(0, 50);
	}

	// Token: 0x06000283 RID: 643 RVA: 0x0000F0F0 File Offset: 0x0000D2F0
	public void SetupItemsOfTypeOrRarity(List<Item2.ItemType> types, List<Item2.Rarity> rarities)
	{
		this.isFilteredInventory = true;
		for (int i = this.itemsParent.childCount - 1; i >= 0; i--)
		{
			Object.Destroy(this.itemsParent.GetChild(i).gameObject);
		}
		if (types == null)
		{
			types = new List<Item2.ItemType> { Item2.ItemType.Any };
		}
		if (rarities == null)
		{
			rarities = new List<Item2.Rarity>
			{
				Item2.Rarity.Common,
				Item2.Rarity.Uncommon,
				Item2.Rarity.Rare,
				Item2.Rarity.Legendary
			};
		}
		rarities.Sort((Item2.Rarity x, Item2.Rarity y) => x.CompareTo(y));
		for (int j = 0; j < this.originOfItems.Count; j++)
		{
			Item2 item2ByName = DebugItemManager.main.GetItem2ByName(this.originOfItems[j]);
			if (item2ByName && Item2.ShareItemTypes(item2ByName.itemType, types) && item2ByName.rarity >= rarities[0])
			{
				Overworld_InventoryItemButton component = Object.Instantiate<GameObject>(this.inventoryItemPrefab, this.itemsParent).GetComponent<Overworld_InventoryItemButton>();
				component.Setup(item2ByName.gameObject, j);
				component.SetupAction(this.clickAction);
			}
		}
	}

	// Token: 0x06000284 RID: 644 RVA: 0x0000F210 File Offset: 0x0000D410
	public void SetupItemsOfSpecificItemType(string name)
	{
		this.isFilteredInventory = true;
		for (int i = this.itemsParent.childCount - 1; i >= 0; i--)
		{
			Object.Destroy(this.itemsParent.GetChild(i).gameObject);
		}
		Item2 item2ByName = DebugItemManager.main.GetItem2ByName(name);
		if (!item2ByName)
		{
			return;
		}
		for (int j = 0; j < this.originOfItems.Count; j++)
		{
			Item2 item2ByName2 = DebugItemManager.main.GetItem2ByName(this.originOfItems[j]);
			if (item2ByName2 && !(Item2.GetDisplayName(item2ByName2.name) != Item2.GetDisplayName(item2ByName.name)))
			{
				Overworld_InventoryItemButton component = Object.Instantiate<GameObject>(this.inventoryItemPrefab, this.itemsParent).GetComponent<Overworld_InventoryItemButton>();
				component.Setup(item2ByName2.gameObject, j);
				component.SetupAction(this.clickAction);
			}
		}
	}

	// Token: 0x06000285 RID: 645 RVA: 0x0000F2E8 File Offset: 0x0000D4E8
	private void SetupItemsForInventory(int startingNumber, int endingNumber)
	{
		if (this.originOfItems == null)
		{
			return;
		}
		for (int i = this.itemsParent.childCount - 1; i >= 0; i--)
		{
			Object.Destroy(this.itemsParent.GetChild(i).gameObject);
		}
		int num = startingNumber;
		while (num < endingNumber && num < this.originOfItems.Count && num >= 0)
		{
			string text = this.originOfItems[num];
			if (text != null)
			{
				Item2 item2ByName = DebugItemManager.main.GetItem2ByName(text);
				if (item2ByName)
				{
					Overworld_InventoryItemButton component = Object.Instantiate<GameObject>(this.inventoryItemPrefab, this.itemsParent).GetComponent<Overworld_InventoryItemButton>();
					component.Setup(item2ByName.gameObject, -1);
					component.SetupAction(this.clickAction);
				}
			}
			num++;
		}
	}

	// Token: 0x06000286 RID: 646 RVA: 0x0000F39C File Offset: 0x0000D59C
	private void UpdateBuildingInterface()
	{
		if (this.buildingInterface != null)
		{
			this.buildingInterface.UpdateGrindingValue();
		}
	}

	// Token: 0x06000287 RID: 647 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
	public void RemoveItem(int num, string name)
	{
		if (this.originOfItems == null)
		{
			return;
		}
		if (num >= this.originOfItems.Count)
		{
			return;
		}
		if (num == -1)
		{
			this.originOfItems.Remove(name);
			return;
		}
		if (Item2.GetDisplayName(this.originOfItems[num]) != Item2.GetDisplayName(name))
		{
			return;
		}
		this.originOfItems.RemoveAt(num);
	}

	// Token: 0x06000288 RID: 648 RVA: 0x0000F41A File Offset: 0x0000D61A
	public void AddItem(int num, string name)
	{
		if (this.originOfItems == null)
		{
			return;
		}
		this.originOfItems.Insert(Mathf.Clamp(num, 0, this.originOfItems.Count), Item2.GetDisplayName(name));
	}

	// Token: 0x06000289 RID: 649 RVA: 0x0000F448 File Offset: 0x0000D648
	public List<Overworld_InventoryItemButton> GetAllItemButtons()
	{
		return new List<Overworld_InventoryItemButton>(this.itemsParent.GetComponentsInChildren<Overworld_InventoryItemButton>());
	}

	// Token: 0x0600028A RID: 650 RVA: 0x0000F45C File Offset: 0x0000D65C
	public void RemoveAll()
	{
		this.originOfItems.Clear();
		for (int i = this.itemsParent.childCount - 1; i >= 0; i--)
		{
			Object.Destroy(this.itemsParent.GetChild(i).gameObject);
		}
	}

	// Token: 0x0600028B RID: 651 RVA: 0x0000F4A2 File Offset: 0x0000D6A2
	public void RemoveItemButton(Overworld_InventoryItemButton itemButton, int oldSiblingIndex = -1)
	{
		if (this.isFilteredInventory)
		{
			this.RemoveItem(-1, itemButton.item.name);
		}
		if (oldSiblingIndex == -1)
		{
			oldSiblingIndex = itemButton.GetSiblingIndex();
		}
		this.RemoveItem(oldSiblingIndex, itemButton.item.name);
	}

	// Token: 0x0600028C RID: 652 RVA: 0x0000F4DC File Offset: 0x0000D6DC
	public void TakeAll()
	{
		SoundManager.main.PlaySFX("putdown");
		foreach (OverworldInventory overworldInventory in Object.FindObjectsOfType<OverworldInventory>())
		{
			if (!(overworldInventory == this))
			{
				int num = 0;
				while (overworldInventory.originOfItems.Count > num)
				{
					if (!this.CanAcceptItem(overworldInventory.originOfItems[num]))
					{
						num++;
					}
					else
					{
						string text = overworldInventory.originOfItems[num];
						overworldInventory.RemoveItem(num, text);
						this.AddItem(0, text);
					}
				}
			}
		}
		OverworldInventory.UpdateAllPages();
	}

	// Token: 0x0600028D RID: 653 RVA: 0x0000F56C File Offset: 0x0000D76C
	public void TakeAllResearch(bool onlyResearch)
	{
		if (!this.buildingInterface)
		{
			this.TakeAll();
			return;
		}
		SoundManager.main.PlaySFX("putdown");
		foreach (OverworldInventory overworldInventory in Object.FindObjectsOfType<OverworldInventory>())
		{
			if (!(overworldInventory == this))
			{
				int num = 0;
				while (overworldInventory.originOfItems.Count > num)
				{
					string text = overworldInventory.originOfItems[num];
					if (!this.CanAcceptItem(text))
					{
						num++;
					}
					else
					{
						Item2 item2ByName = DebugItemManager.main.GetItem2ByName(text);
						if (item2ByName && item2ByName.oneOfAKindType == Item2.OneOfAKindType.OneTotal)
						{
							num++;
						}
						else
						{
							List<Overworld_BuildingInterface.Research> list;
							List<Overworld_BuildingInterface.Research> list2;
							DebugItemManager.main.FindResearch(item2ByName, out list, out list2);
							if (list.Count > 0 || list2.Count > 0)
							{
								num++;
							}
							else
							{
								overworldInventory.RemoveItem(num, text);
								this.AddItem(0, text);
							}
						}
					}
				}
			}
		}
		OverworldInventory.UpdateAllPages();
	}

	// Token: 0x0600028E RID: 654 RVA: 0x0000F664 File Offset: 0x0000D864
	public bool HasItemsThatAreValid(OverworldInventory validFor)
	{
		if (this.originOfItems == null)
		{
			return false;
		}
		foreach (string text in this.originOfItems)
		{
			if (validFor.CanAcceptItem(text))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600028F RID: 655 RVA: 0x0000F6CC File Offset: 0x0000D8CC
	public bool HasItemsThatAreValidAndDontHaveResarch(OverworldInventory validFor)
	{
		if (this.originOfItems == null)
		{
			return false;
		}
		foreach (string text in this.originOfItems)
		{
			if (validFor.CanAcceptItem(text))
			{
				Item2 item2ByName = DebugItemManager.main.GetItem2ByName(text);
				if (!item2ByName || item2ByName.oneOfAKindType != Item2.OneOfAKindType.OneTotal)
				{
					List<Overworld_BuildingInterface.Research> list;
					List<Overworld_BuildingInterface.Research> list2;
					DebugItemManager.main.FindResearch(item2ByName, out list, out list2);
					if (list.Count <= 0 && list2.Count <= 0)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06000290 RID: 656 RVA: 0x0000F774 File Offset: 0x0000D974
	public bool CanAcceptItem(Overworld_InventoryItemButton itemButton)
	{
		return this.CanAcceptItem(itemButton.item);
	}

	// Token: 0x06000291 RID: 657 RVA: 0x0000F784 File Offset: 0x0000D984
	public bool CanAcceptItem(string name)
	{
		Item2 item2ByName = DebugItemManager.main.GetItem2ByName(name);
		return this.CanAcceptItem(item2ByName);
	}

	// Token: 0x06000292 RID: 658 RVA: 0x0000F7A4 File Offset: 0x0000D9A4
	public bool CanAcceptItem(Item2 item)
	{
		return this.originOfItems != null && (this.originOfItems.Count < this.totalMax || this.totalMax <= 0) && item && (this.itemTypesAllowed.Count == 0 || Item2.ShareItemTypes(item.itemType, this.itemTypesAllowed)) && (item.oneOfAKindType != Item2.OneOfAKindType.OneTotal || this.canAcceptOneOfAKindItems);
	}

	// Token: 0x06000293 RID: 659 RVA: 0x0000F81C File Offset: 0x0000DA1C
	public void AddItemButton(Overworld_InventoryItemButton itemButton)
	{
		int num = 0;
		int num2 = 0;
		while (num2 < this.itemsParent.childCount && (itemButton.transform.position.y < this.itemsParent.GetChild(num2).transform.position.y - 1.5f || itemButton.transform.position.x > this.itemsParent.GetChild(num2).transform.position.x))
		{
			num++;
			num2++;
		}
		itemButton.transform.SetParent(this.itemsParent);
		itemButton.transform.SetSiblingIndex(num);
		itemButton.startingInventory = this;
		this.AddItem(itemButton.GetSiblingIndex(), itemButton.item.name);
	}

	// Token: 0x06000294 RID: 660 RVA: 0x0000F8E0 File Offset: 0x0000DAE0
	public void Close()
	{
		Object.FindAnyObjectByType<Overworld_InventoryManager>().ToggleInventory(Overworld_InventoryManager.ClickAction.NONE);
	}

	// Token: 0x0400019E RID: 414
	public bool isFilteredInventory;

	// Token: 0x0400019F RID: 415
	[SerializeField]
	private InputHandler inputHandlerForSortingMenu;

	// Token: 0x040001A0 RID: 416
	[SerializeField]
	private GameObject blankSelectablePrefab;

	// Token: 0x040001A1 RID: 417
	[SerializeField]
	private bool canAcceptOneOfAKindItems;

	// Token: 0x040001A2 RID: 418
	[SerializeField]
	private GameObject previousPageButton;

	// Token: 0x040001A3 RID: 419
	[SerializeField]
	private GameObject nextPageButton;

	// Token: 0x040001A4 RID: 420
	[SerializeField]
	private Image topSprite;

	// Token: 0x040001A5 RID: 421
	[SerializeField]
	private Transform itemsParent;

	// Token: 0x040001A6 RID: 422
	[SerializeField]
	private GameObject inventoryItemPrefab;

	// Token: 0x040001A7 RID: 423
	[SerializeField]
	private List<Item2.ItemType> itemTypesAllowed = new List<Item2.ItemType>();

	// Token: 0x040001A8 RID: 424
	private Overworld_BuildingInterface buildingInterface;

	// Token: 0x040001A9 RID: 425
	private Overworld_InventoryManager.ClickAction clickAction = Overworld_InventoryManager.ClickAction.NONE;

	// Token: 0x040001AA RID: 426
	private List<string> originOfItems;

	// Token: 0x040001AB RID: 427
	[SerializeField]
	private TMP_Dropdown sortDropdown;

	// Token: 0x040001AC RID: 428
	private int currentPage;

	// Token: 0x040001AD RID: 429
	private int maxPage;

	// Token: 0x040001AE RID: 430
	public int totalMax;

	// Token: 0x040001AF RID: 431
	private const int ITEMS_PER_PAGE = 50;

	// Token: 0x040001B0 RID: 432
	private GridLayoutGroup gridLayoutGroup;
}
