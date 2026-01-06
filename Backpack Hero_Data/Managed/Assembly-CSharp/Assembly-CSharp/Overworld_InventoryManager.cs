using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000148 RID: 328
public class Overworld_InventoryManager : MonoBehaviour
{
	// Token: 0x06000CAC RID: 3244 RVA: 0x0008154B File Offset: 0x0007F74B
	private void Awake()
	{
		Overworld_InventoryManager.main = this;
	}

	// Token: 0x06000CAD RID: 3245 RVA: 0x00081553 File Offset: 0x0007F753
	private void OnDestroy()
	{
		if (Overworld_InventoryManager.main == this)
		{
			Overworld_InventoryManager.main = null;
		}
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x06000CAE RID: 3246 RVA: 0x00081568 File Offset: 0x0007F768
	// (set) Token: 0x06000CAF RID: 3247 RVA: 0x00081570 File Offset: 0x0007F770
	public Item2 chosenItem { get; private set; }

	// Token: 0x06000CB0 RID: 3248 RVA: 0x00081579 File Offset: 0x0007F779
	private void Start()
	{
	}

	// Token: 0x06000CB1 RID: 3249 RVA: 0x0008157B File Offset: 0x0007F77B
	private void Update()
	{
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x0008157D File Offset: 0x0007F77D
	public void ToggleSwapButtons(bool active)
	{
		if (active)
		{
			this.inventoryLayoutGroup.spacing = 220f;
		}
		else
		{
			this.inventoryLayoutGroup.spacing = 20f;
		}
		this.swapButtons.SetActive(active);
	}

	// Token: 0x06000CB3 RID: 3251 RVA: 0x000815B0 File Offset: 0x0007F7B0
	public void ToggleSwapButton1(bool active)
	{
		this.swapButton1.interactable = active;
	}

	// Token: 0x06000CB4 RID: 3252 RVA: 0x000815BE File Offset: 0x0007F7BE
	public void ToggleSwapButton2(bool active)
	{
		this.swapButton2.interactable = active;
	}

	// Token: 0x06000CB5 RID: 3253 RVA: 0x000815CC File Offset: 0x0007F7CC
	public void RemoveItem()
	{
		if (this.chosenItem == null)
		{
			return;
		}
		if (this.chosenItemNumber >= MetaProgressSaveManager.main.storedItems.Count)
		{
			return;
		}
		MetaProgressSaveManager.main.storedItems.RemoveAt(this.chosenItemNumber);
	}

	// Token: 0x06000CB6 RID: 3254 RVA: 0x0008160C File Offset: 0x0007F80C
	public GameObject UnlockLore()
	{
		if (this.chosenItem == null)
		{
			return null;
		}
		GameObject itemByName = DebugItemManager.main.GetItemByName(Item2.GetDisplayName(this.chosenItem.name));
		if (!itemByName)
		{
			return null;
		}
		LoreUnlocker component = itemByName.GetComponent<LoreUnlocker>();
		if (!component)
		{
			return null;
		}
		string loreNameKey = component.loreNameKey;
		return this.AddLore(loreNameKey);
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x0008166D File Offset: 0x0007F86D
	public GameObject AddLore(string x)
	{
		x = Item2.GetDisplayName(x);
		if (MetaProgressSaveManager.main.loresUnlocked.Contains(x))
		{
			return null;
		}
		MetaProgressSaveManager.main.loresUnlocked.Add(x);
		return Overworld_Manager.main.OpenNewLoreWindow(x);
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x000816A6 File Offset: 0x0007F8A6
	public void DestroyItem()
	{
		if (this.chosenItem == null)
		{
			return;
		}
		MetaProgressSaveManager.main.storedItems.RemoveAt(this.chosenItemNumber);
		MetaProgressSaveManager.main.DeleteOneOfAKindItem(this.chosenItem);
	}

	// Token: 0x06000CB9 RID: 3257 RVA: 0x000816DC File Offset: 0x0007F8DC
	public void ClearItem()
	{
		this.chosenItem = null;
	}

	// Token: 0x06000CBA RID: 3258 RVA: 0x000816E5 File Offset: 0x0007F8E5
	public void SelectItem(Overworld_InventoryItemButton itemButton)
	{
		if (itemButton.itemNumber == -1)
		{
			this.chosenItemNumber = itemButton.GetSiblingIndex();
		}
		else
		{
			this.chosenItemNumber = itemButton.itemNumber;
		}
		this.chosenItem = itemButton.item;
		this.HideInventory();
	}

	// Token: 0x06000CBB RID: 3259 RVA: 0x0008171C File Offset: 0x0007F91C
	private OverworldInventory SetupItems(List<string> originOfItems, Sprite sprite = null, Overworld_InventoryManager.ClickAction clickAction = Overworld_InventoryManager.ClickAction.NONE, string x = "")
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.inventoryPrefab, this.inventoryInterface.transform);
		gameObject.transform.SetAsLastSibling();
		OverworldInventory component = gameObject.GetComponent<OverworldInventory>();
		this.currentInventoryScreen = component;
		component.Setup(originOfItems, sprite, clickAction);
		if (originOfItems == MetaProgressSaveManager.main.storedItems)
		{
			DigitalCursor.main.SelectFirstSelectableInElement(gameObject.transform);
		}
		if (x != "")
		{
			ReplacementText componentInChildren = gameObject.GetComponentInChildren<ReplacementText>();
			if (componentInChildren)
			{
				componentInChildren.key = x;
			}
		}
		return component;
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x000817A8 File Offset: 0x0007F9A8
	public void ShowPlayerInventoryWithType(List<Item2.ItemType> types, List<Item2.Rarity> rarities)
	{
		this.ToggleSwapButtons(false);
		Overworld_InventoryManager.ClickAction clickAction = Overworld_InventoryManager.ClickAction.SELECTITEM;
		this.ShowInventory(clickAction).SetupItemsOfTypeOrRarity(types, rarities);
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x000817CC File Offset: 0x0007F9CC
	public void ShowPlayerInventoryWithItem(string itemName)
	{
		this.ToggleSwapButtons(false);
		Overworld_InventoryManager.ClickAction clickAction = Overworld_InventoryManager.ClickAction.SELECTITEM;
		this.ShowInventory(clickAction).SetupItemsOfSpecificItemType(itemName);
	}

	// Token: 0x06000CBE RID: 3262 RVA: 0x000817EF File Offset: 0x0007F9EF
	public void ToggleInventory(Overworld_InventoryManager.ClickAction clickAction = Overworld_InventoryManager.ClickAction.NONE)
	{
		if (this.inventoryInterface.activeInHierarchy)
		{
			this.HideInventory();
			this.inventoryInterface.SetActive(false);
		}
		else
		{
			this.ShowInventory(clickAction);
		}
		OverworldInventory.UpdateAllPages();
	}

	// Token: 0x06000CBF RID: 3263 RVA: 0x0008181F File Offset: 0x0007FA1F
	public void OpenChest(List<string> originOfItems, Sprite sprite)
	{
		this.ShowInventory(Overworld_InventoryManager.ClickAction.NONE);
		this.SetupItems(originOfItems, sprite, Overworld_InventoryManager.ClickAction.NONE, "gm74");
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x00081838 File Offset: 0x0007FA38
	private OverworldInventory ShowInventory(Overworld_InventoryManager.ClickAction clickAction = Overworld_InventoryManager.ClickAction.NONE)
	{
		SoundManager.main.PlaySFX("menuBlip");
		DigitalCursor.main.Show();
		this.RemoveAllInventories();
		this.ClearItem();
		this.inventoryInterface.SetActive(true);
		return this.SetupItems(MetaProgressSaveManager.main.storedItems, null, clickAction, "");
	}

	// Token: 0x06000CC1 RID: 3265 RVA: 0x00081890 File Offset: 0x0007FA90
	public void RemoveAllInventories()
	{
		for (int i = 0; i < this.inventoryInterface.transform.childCount; i++)
		{
			Transform child = this.inventoryInterface.transform.GetChild(i);
			if (!child.gameObject.CompareTag("Ignorable"))
			{
				child.SetParent(null);
				Object.Destroy(child.gameObject);
				i--;
			}
		}
	}

	// Token: 0x06000CC2 RID: 3266 RVA: 0x000818F2 File Offset: 0x0007FAF2
	public void HideInventory()
	{
		this.inventoryInterface.GetComponent<SingleUI>();
		this.RemoveAllInventories();
		this.inventoryInterface.SetActive(false);
	}

	// Token: 0x04000A3A RID: 2618
	[SerializeField]
	public HorizontalLayoutGroup inventoryLayoutGroup;

	// Token: 0x04000A3B RID: 2619
	public static Overworld_InventoryManager main;

	// Token: 0x04000A3C RID: 2620
	[SerializeField]
	public GameObject inventoryInterface;

	// Token: 0x04000A3D RID: 2621
	[SerializeField]
	private GameObject inventoryPrefab;

	// Token: 0x04000A3F RID: 2623
	public int chosenItemNumber;

	// Token: 0x04000A40 RID: 2624
	public static int currentInventories;

	// Token: 0x04000A41 RID: 2625
	[SerializeField]
	private GameObject swapButtons;

	// Token: 0x04000A42 RID: 2626
	[SerializeField]
	private Button swapButton1;

	// Token: 0x04000A43 RID: 2627
	[SerializeField]
	private Button swapButton2;

	// Token: 0x04000A44 RID: 2628
	public OverworldInventory currentInventoryScreen;

	// Token: 0x020003F9 RID: 1017
	public enum ClickAction
	{
		// Token: 0x04001774 RID: 6004
		SELECTITEM,
		// Token: 0x04001775 RID: 6005
		USEITEM,
		// Token: 0x04001776 RID: 6006
		GIVEITEM,
		// Token: 0x04001777 RID: 6007
		NONE,
		// Token: 0x04001778 RID: 6008
		SELECTITEMtoTake
	}
}
