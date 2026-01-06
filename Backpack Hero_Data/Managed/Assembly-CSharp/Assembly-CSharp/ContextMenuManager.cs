using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020000B8 RID: 184
public class ContextMenuManager : MonoBehaviour
{
	// Token: 0x1700000C RID: 12
	// (get) Token: 0x060004FD RID: 1277 RVA: 0x00030C7E File Offset: 0x0002EE7E
	public static ContextMenuManager main
	{
		get
		{
			return ContextMenuManager._instance;
		}
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x00030C85 File Offset: 0x0002EE85
	private void Awake()
	{
		if (ContextMenuManager._instance != null && ContextMenuManager._instance != this)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		ContextMenuManager._instance = this;
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x00030CB3 File Offset: 0x0002EEB3
	private void OnDestory()
	{
		if (ContextMenuManager._instance == this)
		{
			ContextMenuManager._instance = null;
		}
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x00030CC8 File Offset: 0x0002EEC8
	private void Start()
	{
		this.gameManager = GameManager.main;
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x00030CD8 File Offset: 0x0002EED8
	public void ClearState()
	{
		if (GameManager.main)
		{
			GameManager.main.viewingEvent = false;
		}
		this.currentState = ContextMenuManager.CurrentState.noMenu;
		this.selectedItem = null;
		this.parentObject = null;
		if (this.contextMenu)
		{
			Object.Destroy(this.contextMenu);
		}
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x00030D29 File Offset: 0x0002EF29
	public bool CanInteractOutsideMenu()
	{
		return this.currentState == ContextMenuManager.CurrentState.noMenu && this.timeWithoutMenu >= 0.1f;
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x00030D48 File Offset: 0x0002EF48
	private void Update()
	{
		if (!this.selectedItem || this.selectedItem.destroyed || !this.parentObject || !this.parentObject.activeInHierarchy)
		{
			this.ClearState();
		}
		if (this.currentState == ContextMenuManager.CurrentState.viewingCard)
		{
			return;
		}
		if (this.currentState == ContextMenuManager.CurrentState.noMenu && this.timeWithoutMenu < 1f)
		{
			this.timeWithoutMenu += Time.deltaTime;
		}
		if (this.currentState != ContextMenuManager.CurrentState.noMenu)
		{
			this.timeWithoutMenu = 0f;
			if (this.currentState == ContextMenuManager.CurrentState.menuClosing)
			{
				if (this.contextMenu)
				{
					Object.Destroy(this.contextMenu);
				}
				this.currentState = ContextMenuManager.CurrentState.noMenu;
				this.selectedItem = null;
				return;
			}
			if (DigitalCursor.main.GetInputDown("cancel") || Input.GetMouseButtonUp(0))
			{
				this.currentState = ContextMenuManager.CurrentState.menuClosing;
				return;
			}
		}
		else
		{
			if (Input.GetMouseButtonDown(1))
			{
				this.RightClick();
				return;
			}
			if (DigitalCursor.main.GetInputDown("contextMenu"))
			{
				this.GetItem();
				if (this.IsViewingCard())
				{
					this.RightClick();
				}
			}
		}
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x00030E5C File Offset: 0x0002F05C
	public bool IsViewingCard()
	{
		Item2 item = this.GetItem();
		if (item == null)
		{
			return false;
		}
		ItemMovement component = item.GetComponent<ItemMovement>();
		float? num = ((component != null) ? new float?(component.timeViewingCard) : null);
		float num2 = 0.1f;
		return (num.GetValueOrDefault() > num2) & (num != null);
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x00030EAC File Offset: 0x0002F0AC
	public void RightClick()
	{
		if (!GameManager.main)
		{
			return;
		}
		Card[] array = Object.FindObjectsOfType<Card>();
		for (int i = 0; i < array.Length; i++)
		{
			Object.Destroy(array[i].gameObject);
		}
		if (this.currentState == ContextMenuManager.CurrentState.noMenu)
		{
			Item2 item = this.GetItem();
			if (!item || this.gameManager.draggingItem != null)
			{
				if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.cursor)
				{
					this.gameManager.RotateAll(90f);
					return;
				}
				return;
			}
			else
			{
				this.ReceiveItem(item, item.gameObject);
			}
		}
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x00030F3C File Offset: 0x0002F13C
	public void ReceiveItem(Item2 item, GameObject parentObject)
	{
		if (this.contextMenu)
		{
			Object.Destroy(this.contextMenu);
		}
		if (!item)
		{
			return;
		}
		this.timeNotSelected = 0f;
		this.selectedItem = item;
		this.parentObject = parentObject;
		this.contextMenu = Object.Instantiate<GameObject>(this.contextMenuPrefab, DigitalCursor.main.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").transform);
		this.contextMenu.transform.position = new Vector3(this.contextMenu.transform.position.x, this.contextMenu.transform.position.y, 0f);
		this.currentState = ContextMenuManager.CurrentState.menuOpen;
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x00031004 File Offset: 0x0002F204
	private void FindGraphicsRaycaster()
	{
		this.graphicRaycasters = Object.FindObjectsOfType<GraphicRaycaster>().ToList<GraphicRaycaster>();
		this.graphicRaycasters.Sort((GraphicRaycaster x, GraphicRaycaster y) => x.GetComponent<Canvas>().sortingOrder.CompareTo(y.GetComponent<Canvas>().sortingOrder));
		this.graphicRaycasters.Reverse();
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x00031058 File Offset: 0x0002F258
	private Item2 GetItem()
	{
		if (this.graphicRaycasters.Count == 0)
		{
			this.FindGraphicsRaycaster();
		}
		Item2 item = null;
		foreach (BaseRaycaster baseRaycaster in this.graphicRaycasters)
		{
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
			pointerEventData.position = Camera.main.WorldToScreenPoint(DigitalCursor.main.transform.position);
			List<RaycastResult> list = new List<RaycastResult>();
			baseRaycaster.Raycast(pointerEventData, list);
			foreach (RaycastResult raycastResult in list)
			{
				UICarvingIndicator componentInParent = raycastResult.gameObject.GetComponentInParent<UICarvingIndicator>();
				if (componentInParent)
				{
					item = componentInParent.GetItem();
					if (item)
					{
						return item;
					}
				}
			}
		}
		if (GameManager.main && this.gameManager.viewingEvent && this.gameManager.inventoryPhase != GameManager.InventoryPhase.choose)
		{
			return null;
		}
		foreach (RaycastHit2D raycastHit2D in Physics2D.RaycastAll(DigitalCursor.main.currentPosition, Vector2.zero))
		{
			Item2 componentInParent2 = raycastHit2D.collider.gameObject.GetComponentInParent<Item2>();
			if (componentInParent2)
			{
				item = componentInParent2;
				break;
			}
		}
		return item;
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x000311E4 File Offset: 0x0002F3E4
	public void OpenResearch(Item2 selectedItem)
	{
		List<Overworld_BuildingInterface.Research> list;
		List<Overworld_BuildingInterface.Research> list2;
		DebugItemManager.main.FindResearch(selectedItem, out list, out list2);
		List<Overworld_BuildingInterface.Research> list3 = new List<Overworld_BuildingInterface.Research>();
		list3.AddRange(list);
		list3.AddRange(list2);
		list3.Sort((Overworld_BuildingInterface.Research x, Overworld_BuildingInterface.Research y) => y.isFavorite.CompareTo(x.isFavorite));
		if (list.Count == 0 && list2.Count == 0)
		{
			return;
		}
		Object.Instantiate<GameObject>(this.scrollCardUnlockBarsPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").GetComponent<Canvas>().transform).transform.localPosition = Vector3.zero;
		Transform transform = GameObject.FindGameObjectWithTag("Content").transform;
		foreach (Overworld_BuildingInterface.Research research in list3)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.unlockBarPrefab, transform);
			gameObject.transform.localScale = Vector3.one * 0.75f;
			gameObject.GetComponent<ResearchUnlockBar>().Setup(research, null);
		}
		this.currentState = ContextMenuManager.CurrentState.viewingCard;
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x00031308 File Offset: 0x0002F508
	public void OpenCard(Item2 selectedItem)
	{
		if (!selectedItem)
		{
			return;
		}
		ItemMovement itemMovement = selectedItem.itemMovement;
		if (!itemMovement)
		{
			itemMovement = selectedItem.GetComponent<ItemMovement>();
		}
		if (!itemMovement)
		{
			return;
		}
		if (this.currentState != ContextMenuManager.CurrentState.viewingCard)
		{
			this.previewCard = null;
			GameObject gameObject = Object.Instantiate<GameObject>(this.scrollCardPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").GetComponent<Canvas>().transform);
			gameObject.transform.localPosition = Vector3.zero;
			ScrollCard componentInChildren = gameObject.GetComponentInChildren<ScrollCard>();
			ItemSwitcher component = selectedItem.GetComponent<ItemSwitcher>();
			if (component)
			{
				using (List<ItemSwitcher.Item2Change>.Enumerator enumerator = component.GetAllItemChanges().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ItemSwitcher.Item2Change item2Change = enumerator.Current;
						if (!(item2Change.alternateItem == selectedItem) && item2Change != null)
						{
							Sprite sprite = item2Change.sprite;
							if (!sprite)
							{
								sprite = component.GetComponent<SpriteRenderer>().sprite;
							}
							GameObject gameObject2 = selectedItem.GetComponent<ItemMovement>().ShowCardDirect(item2Change.alternateItem, sprite);
							Card componentInChildren2 = gameObject2.GetComponentInChildren<Card>();
							componentInChildren.GetCard(componentInChildren2);
							Card component2 = gameObject2.GetComponent<Card>();
							component2.MakeStuck();
							component2.IgnoreShop();
							component2.deleteOnDeactivate = false;
						}
					}
					goto IL_0150;
				}
			}
			this.previewCard = itemMovement.ShowCard(base.gameObject);
			Card componentInChildren3 = this.previewCard.GetComponentInChildren<Card>();
			componentInChildren.GetCard(componentInChildren3);
			IL_0150:
			this.currentState = ContextMenuManager.CurrentState.viewingCard;
		}
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x0003147C File Offset: 0x0002F67C
	public void OpenCardSimple()
	{
		if (this.currentState != ContextMenuManager.CurrentState.viewingCard)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.cardPrefabSimple, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").GetComponent<Canvas>().transform);
			gameObject.GetComponentInChildren<Card>().SetParent(base.gameObject);
			RunTypeManager runTypeManager = Object.FindObjectOfType<RunTypeManager>();
			if (runTypeManager && runTypeManager.runType)
			{
				string text = RunTypeSelector.SetText(Object.FindObjectOfType<RunTypeManager>().runType, false, false, new List<RunType>());
				gameObject.GetComponentInChildren<Card>().GetDescriptionsSimple(new List<string> { text }, base.gameObject);
				LangaugeManager.main.SetFont(gameObject.transform);
				this.currentState = ContextMenuManager.CurrentState.viewingCard;
				Object.Destroy(this.contextMenu);
				return;
			}
			if (runTypeManager && runTypeManager.missions)
			{
				Card componentInChildren = gameObject.GetComponentInChildren<Card>();
				componentInChildren.GetDescriptionMission(runTypeManager.missions, base.gameObject);
				componentInChildren.MakeStuck();
				LangaugeManager.main.SetFont(gameObject.transform);
				this.currentState = ContextMenuManager.CurrentState.viewingCard;
				Object.Destroy(this.contextMenu);
			}
		}
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x00031598 File Offset: 0x0002F798
	public void Command(ContextMenuButton.Type type, List<Item2.Cost> costs, Item2.PlayerAnimation playerAnimation)
	{
		DigitalCursor.main.ClearUIElement();
		if (GameManager.main)
		{
			GameManager.main.ClearEvent();
		}
		this.currentState = ContextMenuManager.CurrentState.noMenu;
		if (this.contextMenu)
		{
			Object.Destroy(this.contextMenu);
		}
		if (type == ContextMenuButton.Type.useItem)
		{
			this.selectedItem.itemMovement.ClickItem(false);
			return;
		}
		if (type == ContextMenuButton.Type.useItemMax)
		{
			base.StartCoroutine(this.UseItemAsMuchAsPossible(this.selectedItem));
			return;
		}
		if (type == ContextMenuButton.Type.viewCard)
		{
			this.OpenCard(this.selectedItem);
			return;
		}
		if (type == ContextMenuButton.Type.viewResearch)
		{
			this.OpenResearch(this.selectedItem);
			return;
		}
		if (type != ContextMenuButton.Type.rerollItems)
		{
			if (type == ContextMenuButton.Type.createRoom)
			{
				this.selectedItem.GetComponent<ItemMovement>().DelayDestroy();
				Object.FindObjectOfType<MapManager>().mapMode = MapManager.MapMode.createRoom;
				this.gameManager.ShowMap(false);
				return;
			}
			if (type == ContextMenuButton.Type.startBattle)
			{
				this.selectedItem.GetComponent<ItemMovement>().DelayDestroy();
				DungeonLevel.EnemyEncounter2 enemyEncounter = Object.FindObjectOfType<DungeonSpawner>().GetEnemyEncounter(false);
				Player main = Player.main;
				for (int i = 0; i < enemyEncounter.enemiesInGroup.Count; i++)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(enemyEncounter.enemiesInGroup[i], Vector3.zero, Quaternion.identity, main.transform.parent);
					gameObject.transform.position = new Vector3(this.gameManager.spawnPosition.position.x - (float)(i * 2) + 1f - gameObject.GetComponent<BoxCollider2D>().size.x / 2f, -4.8f + gameObject.GetComponent<BoxCollider2D>().size.y / 2f, 1f);
				}
				GameFlowManager.main.StartCombat();
				DungeonPlayer dungeonPlayer = Object.FindObjectOfType<DungeonPlayer>();
				dungeonPlayer.StartCoroutine(dungeonPlayer.WalkIntoEvent());
				return;
			}
			if (type == ContextMenuButton.Type.duplicatePotion)
			{
				List<Item2> list = new List<Item2>();
				this.selectedItem.FindItemsAndGridsinArea(list, new List<GridSquare>(), new List<Item2.Area> { Item2.Area.top }, Item2.AreaDistance.adjacent, null, null, null, true, false, true);
				using (List<Item2>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Item2 item = enumerator.Current;
						if (item.itemType.Contains(Item2.ItemType.Potion))
						{
							GameObject gameObject2 = Object.Instantiate<GameObject>(item.gameObject, item.transform.position, item.transform.rotation);
							base.StartCoroutine(this.SpawnInSeconds(gameObject2));
						}
					}
					return;
				}
			}
			if (type == ContextMenuButton.Type.combineConsumables)
			{
				List<Item2> list2 = new List<Item2>();
				this.selectedItem.FindItemsAndGridsinArea(list2, new List<GridSquare>(), new List<Item2.Area> { Item2.Area.left }, Item2.AreaDistance.all, null, null, null, true, false, true);
				if (list2.Count <= 0)
				{
					return;
				}
				List<Item2> list3 = new List<Item2>();
				this.selectedItem.FindItemsAndGridsinArea(list3, new List<GridSquare>(), new List<Item2.Area> { Item2.Area.right }, Item2.AreaDistance.all, null, null, null, true, false, true);
				using (List<Item2>.Enumerator enumerator = list3.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Item2 item2 = enumerator.Current;
						if (item2.itemType.Contains(Item2.ItemType.Potion))
						{
							foreach (Item2 item3 in list2)
							{
								if (item3.itemType.Contains(Item2.ItemType.Potion))
								{
									foreach (Item2.CombattEffect combattEffect in item3.combatEffects)
									{
										item2.combatEffects.Add(combattEffect);
									}
									item3.GetComponent<ItemMovement>().DelayDestroy();
								}
							}
							item2.GetComponentInChildren<SpriteRenderer>().sprite = this.mixedPotionSprite;
							item2.displayName = "Mixed Potion";
							item2.name = "Mixed Potion";
							break;
						}
					}
					return;
				}
			}
			if (type == ContextMenuButton.Type.flipX)
			{
				EnergyEmitter component = this.selectedItem.GetComponent<EnergyEmitter>();
				if (component)
				{
					component.FlipToggle();
					return;
				}
			}
			else
			{
				if (type == ContextMenuButton.Type.test)
				{
					CR8Manager.instance.Test(this.selectedItem.gameObject);
					return;
				}
				if (type == ContextMenuButton.Type.returnCarving)
				{
					Object.FindObjectOfType<Tote>().RemoveCarvingFromLists(this.selectedItem.gameObject);
					this.selectedItem.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
					this.selectedItem.GetComponent<Carving>().moveToCardViewer = false;
					this.selectedItem.GetComponent<Carving>().RemoveUI();
					this.selectedItem.GetComponent<ItemMovement>().returnsToOutOfInventoryPosition = true;
					GameManager.main.SetItemsAllowToTake();
					if (Object.FindObjectOfType<Store>())
					{
						this.selectedItem.isForSale = true;
						this.gameManager.ChangeGold(this.selectedItem.cost);
						return;
					}
				}
				else if (type == ContextMenuButton.Type.sellCarving)
				{
					if (this.selectedItem.itemType.Contains(Item2.ItemType.Curse) && !this.selectedItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.Cleansed))
					{
						GameManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm58"));
						return;
					}
					this.selectedItem.GetComponent<ItemMovement>().DelayDestroy();
					Store store = Object.FindObjectOfType<Store>();
					if (store && !this.selectedItem.itemType.Contains(Item2.ItemType.Curse) && store.dungeonEvent)
					{
						store.dungeonEvent.AddEventProperty(DungeonEvent.EventProperty.Type.increaseCost, 5);
						return;
					}
				}
				else
				{
					if (type == ContextMenuButton.Type.openPouch || type == ContextMenuButton.Type.closePouch)
					{
						ItemPouch component2 = this.selectedItem.GetComponent<ItemPouch>();
						component2.Toggle();
						component2.OpenedViaClick();
						return;
					}
					if (type == ContextMenuButton.Type.viewInventory)
					{
						this.selectedItem.GetComponent<PetItem>().petMaster.OpenInventory();
						return;
					}
					if (type == ContextMenuButton.Type.recallPet)
					{
						this.selectedItem.GetComponent<PetItem2>().RemoveCombatPet();
						return;
					}
					if (type == ContextMenuButton.Type.sellItem)
					{
						Store store2 = Object.FindObjectOfType<Store>();
						if (store2)
						{
							store2.BuyPlayerItem(this.selectedItem);
							return;
						}
					}
					else if (type == ContextMenuButton.Type.unlock)
					{
						Chest chest = Object.FindObjectOfType<Chest>();
						if (chest)
						{
							chest.UnlockAndOpen(this.selectedItem);
							return;
						}
					}
					else if (type == ContextMenuButton.Type.transform)
					{
						ItemSwitcher component3 = this.selectedItem.GetComponent<ItemSwitcher>();
						if (component3)
						{
							component3.ForceChoose();
							return;
						}
					}
					else if (type == ContextMenuButton.Type.selectForComboUse)
					{
						if (!GameFlowManager.main.IsComplete())
						{
							return;
						}
						if (GameFlowManager.main.selectedItem == this.selectedItem)
						{
							GameManager.main.EndChooseItem();
							return;
						}
						if (!this.selectedItem || !this.selectedItem.itemMovement || !this.selectedItem.itemMovement.CanBeTakenFromLimitedItemGet())
						{
							PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm9"));
							return;
						}
						GameFlowManager.main.SelectItem(this.selectedItem, costs);
						return;
					}
					else if (type == ContextMenuButton.Type.alternateUse)
					{
						if (!this.selectedItem || !this.selectedItem.itemMovement || !this.selectedItem.itemMovement.CanBeTakenFromLimitedItemGet())
						{
							PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm9"));
							return;
						}
						GameFlowManager.main.StartCoroutine(GameFlowManager.main.UseItem(this.selectedItem, true, true, playerAnimation, false, false));
					}
				}
			}
		}
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x00031CE8 File Offset: 0x0002FEE8
	private IEnumerator SpawnInSeconds(GameObject x)
	{
		yield return new WaitForSeconds(0.1f);
		x.GetComponent<ItemMovement>().RemoveFromGrid();
		this.gameManager.MoveAllItems();
		yield break;
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x00031CFE File Offset: 0x0002FEFE
	private IEnumerator UseItemAsMuchAsPossible(Item2 item)
	{
		this.gameManager.ShowStopButton();
		GameFlowManager gameFlowManager = GameFlowManager.main;
		this.exit = false;
		bool itemActivated = false;
		bool dontShowNegative = false;
		do
		{
			itemActivated = false;
			item.itemMovement.ClickItem(dontShowNegative);
			dontShowNegative = true;
			yield return new WaitForEndOfFrame();
			while (gameFlowManager.battlePhase == GameFlowManager.BattlePhase.playerAction || !gameFlowManager.IsComplete())
			{
				itemActivated = true;
				yield return null;
			}
		}
		while (itemActivated && !this.exit);
		this.gameManager.StopRepeatActionButton();
		yield break;
	}

	// Token: 0x040003C1 RID: 961
	private static ContextMenuManager _instance;

	// Token: 0x040003C2 RID: 962
	public ContextMenuManager.CurrentState currentState;

	// Token: 0x040003C3 RID: 963
	[SerializeField]
	private GameObject contextMenuPrefab;

	// Token: 0x040003C4 RID: 964
	[SerializeField]
	private GameObject cardPrefab;

	// Token: 0x040003C5 RID: 965
	[SerializeField]
	private GameObject cardCarvingPrefab;

	// Token: 0x040003C6 RID: 966
	[SerializeField]
	private GameObject cardPrefabSimple;

	// Token: 0x040003C7 RID: 967
	[SerializeField]
	private GameObject cardPetPrefab;

	// Token: 0x040003C8 RID: 968
	[SerializeField]
	private GameObject cardTreatPrefab;

	// Token: 0x040003C9 RID: 969
	[SerializeField]
	private GameObject scrollCardPrefab;

	// Token: 0x040003CA RID: 970
	[SerializeField]
	private Sprite mixedPotionSprite;

	// Token: 0x040003CB RID: 971
	private GameObject contextMenu;

	// Token: 0x040003CC RID: 972
	private CanvasGroup canvasGroup;

	// Token: 0x040003CD RID: 973
	public Item2 selectedItem;

	// Token: 0x040003CE RID: 974
	private GameObject parentObject;

	// Token: 0x040003CF RID: 975
	public GameObject previewCard;

	// Token: 0x040003D0 RID: 976
	private GameManager gameManager;

	// Token: 0x040003D1 RID: 977
	public bool exit;

	// Token: 0x040003D2 RID: 978
	private float timeWithoutMenu;

	// Token: 0x040003D3 RID: 979
	private float timeNotSelected;

	// Token: 0x040003D4 RID: 980
	private List<GraphicRaycaster> graphicRaycasters = new List<GraphicRaycaster>();

	// Token: 0x040003D5 RID: 981
	[SerializeField]
	private GameObject scrollCardUnlockBarsPrefab;

	// Token: 0x040003D6 RID: 982
	[SerializeField]
	private GameObject unlockBarPrefab;

	// Token: 0x020002E2 RID: 738
	public enum CurrentState
	{
		// Token: 0x04001148 RID: 4424
		noMenu,
		// Token: 0x04001149 RID: 4425
		menuOpen,
		// Token: 0x0400114A RID: 4426
		menuClosing,
		// Token: 0x0400114B RID: 4427
		viewingCard
	}
}
