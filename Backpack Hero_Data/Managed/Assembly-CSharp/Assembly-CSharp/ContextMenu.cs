using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B6 RID: 182
public class ContextMenu : MonoBehaviour
{
	// Token: 0x060004F2 RID: 1266 RVA: 0x0002FD37 File Offset: 0x0002DF37
	private void OnDestroy()
	{
		ContextMenuManager.main.ClearState();
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x0002FD44 File Offset: 0x0002DF44
	private void Start()
	{
		this.canvas = base.GetComponentInParent<Canvas>();
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.rectTransform = base.GetComponent<RectTransform>();
		this.gameFlowManager = GameFlowManager.main;
		this.gameManager = GameManager.main;
		this.contextMenuManager = Object.FindObjectOfType<ContextMenuManager>();
		this.cR8Manager = Object.FindObjectOfType<CR8Manager>();
		this.canvasGroup.alpha = 0f;
		if (GameManager.main)
		{
			this.previouslyViewingEvent = GameManager.main.viewingEvent;
			GameManager.main.viewingEvent = true;
		}
		if (ContextMenuManager.main.selectedItem)
		{
			List<ContextMenuButton.ContextMenuButtonAndCost> list = new List<ContextMenuButton.ContextMenuButtonAndCost>();
			list.Add(new ContextMenuButton.ContextMenuButtonAndCost
			{
				type = ContextMenuButton.Type.viewCard
			});
			list.Add(new ContextMenuButton.ContextMenuButtonAndCost
			{
				type = ContextMenuButton.Type.useItem
			});
			list.Add(new ContextMenuButton.ContextMenuButtonAndCost
			{
				type = ContextMenuButton.Type.useItemMax
			});
			list.Add(new ContextMenuButton.ContextMenuButtonAndCost
			{
				type = ContextMenuButton.Type.sellItem
			});
			if (Singleton.Instance.storyMode)
			{
				List<Overworld_BuildingInterface.Research> list2;
				List<Overworld_BuildingInterface.Research> list3;
				DebugItemManager.main.FindResearch(ContextMenuManager.main.selectedItem, out list2, out list3);
				if (list2.Count > 0 || list3.Count > 0)
				{
					list.Add(new ContextMenuButton.ContextMenuButtonAndCost
					{
						type = ContextMenuButton.Type.viewResearch
					});
				}
			}
			if (ContextMenuManager.main.selectedItem.itemType.Contains(Item2.ItemType.Key))
			{
				list.Add(new ContextMenuButton.ContextMenuButtonAndCost
				{
					type = ContextMenuButton.Type.unlock
				});
			}
			foreach (ContextMenuButton.ContextMenuButtonAndCost contextMenuButtonAndCost in ContextMenuManager.main.selectedItem.contextMenuOptions)
			{
				list.Add(contextMenuButtonAndCost);
			}
			this.CreateButtons(list);
			base.StartCoroutine(this.FindHeight());
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x0002FF30 File Offset: 0x0002E130
	private void Update()
	{
		if (!ContextMenuManager.main.selectedItem)
		{
			ContextMenuManager.main.ClearState();
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x0002FF58 File Offset: 0x0002E158
	public void CreateButtons(List<ContextMenuButton.ContextMenuButtonAndCost> contextMenuButtons)
	{
		Player main = Player.main;
		Store store = Object.FindObjectOfType<Store>();
		Chest chest = Object.FindObjectOfType<Chest>();
		foreach (ContextMenuButton.ContextMenuButtonAndCost contextMenuButtonAndCost in contextMenuButtons)
		{
			if ((GameManager.main || contextMenuButtonAndCost.type == ContextMenuButton.Type.viewCard || contextMenuButtonAndCost.type == ContextMenuButton.Type.viewResearch) && (!GameManager.main || !(ContextMenuManager.main.selectedItem.gameObject == GameManager.main.reorgnizeItem) || contextMenuButtonAndCost.type == ContextMenuButton.Type.viewCard) && (((!GameFlowManager.main || GameFlowManager.main.battlePhase == GameFlowManager.BattlePhase.outOfBattle || GameFlowManager.main.battlePhase == GameFlowManager.BattlePhase.playerTurn) && (!GameFlowManager.main || GameFlowManager.main.battlePhase != GameFlowManager.BattlePhase.playerTurn || GameManager.main.inventoryPhase != GameManager.InventoryPhase.open)) || contextMenuButtonAndCost.type == ContextMenuButton.Type.viewCard || contextMenuButtonAndCost.type == ContextMenuButton.Type.viewResearch) && ((ContextMenuManager.main.selectedItem.itemMovement && ContextMenuManager.main.selectedItem.itemMovement.inGrid) || contextMenuButtonAndCost.type == ContextMenuButton.Type.viewCard || contextMenuButtonAndCost.type == ContextMenuButton.Type.returnCarving || contextMenuButtonAndCost.type == ContextMenuButton.Type.sellCarving || contextMenuButtonAndCost.type == ContextMenuButton.Type.openPouch || contextMenuButtonAndCost.type == ContextMenuButton.Type.closePouch || contextMenuButtonAndCost.type == ContextMenuButton.Type.viewInventory || contextMenuButtonAndCost.type == ContextMenuButton.Type.transform || contextMenuButtonAndCost.type == ContextMenuButton.Type.selectForComboUse || contextMenuButtonAndCost.type == ContextMenuButton.Type.alternateUse || contextMenuButtonAndCost.type == ContextMenuButton.Type.viewResearch) && (contextMenuButtonAndCost.type != ContextMenuButton.Type.sellCarving || !ContextMenuManager.main.selectedItem || !ContextMenuManager.main.selectedItem.itemType.Contains(Item2.ItemType.Curse) || ContextMenuManager.main.selectedItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.Cleansed)) && (!this.cR8Manager || !this.cR8Manager.isRunning || contextMenuButtonAndCost.type == ContextMenuButton.Type.viewCard || contextMenuButtonAndCost.type == ContextMenuButton.Type.viewResearch) && (!GameFlowManager.main || !GameFlowManager.main.isCheckingEffects || contextMenuButtonAndCost.type == ContextMenuButton.Type.viewCard || contextMenuButtonAndCost.type == ContextMenuButton.Type.viewResearch) && (contextMenuButtonAndCost.type != ContextMenuButton.Type.unlock || (chest && chest.padlock)) && (contextMenuButtonAndCost.type != ContextMenuButton.Type.sellItem || (store && store.CorrectStoreType(ContextMenuManager.main.selectedItem) && ContextMenuManager.main.selectedItem.isOwned)))
			{
				bool flag = true;
				if ((contextMenuButtonAndCost.type != ContextMenuButton.Type.recallPet || this.contextMenuManager.selectedItem.GetComponent<PetItem2>().combatPet) && (!main || main.characterName != Character.CharacterName.CR8 || contextMenuButtonAndCost.type != ContextMenuButton.Type.useItemMax) && (contextMenuButtonAndCost.type != ContextMenuButton.Type.test || this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.outOfBattle) && ((Item2.GetCosts(ContextMenuManager.main.selectedItem.costs).Count > 0 && GameFlowManager.main && this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.outOfBattle && !ContextMenuManager.main.selectedItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBeMovedInCombat) && !ContextMenuManager.main.selectedItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBeMovedInCombatButReturnsToOriginalPosition) && (Singleton.Instance.character != Character.CharacterName.CR8 || this.contextMenuManager.selectedItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBeUsedByCR8Directly))) || (contextMenuButtonAndCost.type != ContextMenuButton.Type.useItem && contextMenuButtonAndCost.type != ContextMenuButton.Type.useItemMax)) && (!GameFlowManager.main || this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.outOfBattle || (contextMenuButtonAndCost.type != ContextMenuButton.Type.createRoom && contextMenuButtonAndCost.type != ContextMenuButton.Type.startBattle && contextMenuButtonAndCost.type != ContextMenuButton.Type.duplicatePotion && contextMenuButtonAndCost.type != ContextMenuButton.Type.combineConsumables)))
				{
					if (contextMenuButtonAndCost.type == ContextMenuButton.Type.combineConsumables)
					{
						flag = false;
						List<Item2> list = new List<Item2>();
						this.contextMenuManager.selectedItem.FindItemsAndGridsinArea(list, new List<GridSquare>(), new List<Item2.Area> { Item2.Area.left }, Item2.AreaDistance.adjacent, null, null, null, true, false, true);
						if (list.Count > 0 && list[0].itemType.Contains(Item2.ItemType.Potion))
						{
							list = new List<Item2>();
							this.contextMenuManager.selectedItem.FindItemsAndGridsinArea(list, new List<GridSquare>(), new List<Item2.Area> { Item2.Area.right }, Item2.AreaDistance.adjacent, null, null, null, true, false, true);
							if (list.Count > 0 && list[0].itemType.Contains(Item2.ItemType.Potion))
							{
								flag = true;
							}
						}
					}
					else if (contextMenuButtonAndCost.type == ContextMenuButton.Type.duplicatePotion)
					{
						flag = false;
						List<Item2> list2 = new List<Item2>();
						this.contextMenuManager.selectedItem.FindItemsAndGridsinArea(list2, new List<GridSquare>(), new List<Item2.Area> { Item2.Area.top }, Item2.AreaDistance.adjacent, null, null, null, true, false, true);
						if (list2.Count > 0 && list2[0].itemType.Contains(Item2.ItemType.Potion))
						{
							flag = true;
						}
					}
					else if (contextMenuButtonAndCost.type == ContextMenuButton.Type.flipX)
					{
						flag = (this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.outOfBattle || this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.playerTurn) && (!this.cR8Manager || (!this.cR8Manager.isRunning && !this.cR8Manager.isTesting));
					}
					else if (contextMenuButtonAndCost.type == ContextMenuButton.Type.returnCarving)
					{
						flag = false;
						Carving component = this.contextMenuManager.selectedItem.GetComponent<Carving>();
						if (component && component.moveToCardViewer && !this.contextMenuManager.selectedItem.isOwned)
						{
							flag = true;
						}
					}
					else if (contextMenuButtonAndCost.type == ContextMenuButton.Type.sellCarving)
					{
						flag = false;
						if (store && this.contextMenuManager.selectedItem.isOwned && store.carvingStore)
						{
							flag = true;
						}
					}
					else if (contextMenuButtonAndCost.type == ContextMenuButton.Type.closePouch)
					{
						flag = false;
						ItemPouch component2 = this.contextMenuManager.selectedItem.GetComponent<ItemPouch>();
						if (component2 && component2.open)
						{
							flag = true;
						}
					}
					else if (contextMenuButtonAndCost.type == ContextMenuButton.Type.openPouch)
					{
						flag = false;
						ItemPouch component3 = this.contextMenuManager.selectedItem.GetComponent<ItemPouch>();
						if (component3 && !component3.open)
						{
							flag = true;
						}
					}
					else if (contextMenuButtonAndCost.type == ContextMenuButton.Type.transform)
					{
						flag = true;
					}
					if (flag)
					{
						Object.Instantiate<GameObject>(this.contextMenuButtonPrefab, Vector3.zero, Quaternion.identity, this.buttonTransform).GetComponent<ContextMenuButton>().chosenContextMenuButton = contextMenuButtonAndCost;
					}
				}
			}
		}
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x00030644 File Offset: 0x0002E844
	private IEnumerator FindHeight()
	{
		Vector2 vector;
		if (this.canvas.renderMode == RenderMode.ScreenSpaceCamera)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvas.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(DigitalCursor.main.transform.position), this.canvas.worldCamera, out vector);
		}
		else
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvas.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(DigitalCursor.main.transform.position), null, out vector);
		}
		if (vector.x > 0f)
		{
			this.rectTransform.pivot = new Vector2(1f, this.rectTransform.pivot.y);
		}
		else
		{
			this.rectTransform.pivot = new Vector2(0f, this.rectTransform.pivot.y);
		}
		if (vector.y < 0f)
		{
			this.rectTransform.pivot = new Vector2(this.rectTransform.pivot.x, 0f);
		}
		else
		{
			this.rectTransform.pivot = new Vector2(this.rectTransform.pivot.x, 1f);
		}
		LangaugeManager.main.SetFont(base.transform);
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		yield return new WaitForEndOfFrame();
		if (this.canvas.renderMode == RenderMode.ScreenSpaceCamera)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvas.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(DigitalCursor.main.transform.position), this.canvas.worldCamera, out vector);
		}
		else
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvas.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(DigitalCursor.main.transform.position), null, out vector);
		}
		if (vector.x > 0f)
		{
			this.rectTransform.pivot = new Vector2(1f, this.rectTransform.pivot.y);
		}
		else
		{
			this.rectTransform.pivot = new Vector2(0f, this.rectTransform.pivot.y);
		}
		if (vector.y < 0f)
		{
			this.rectTransform.pivot = new Vector2(this.rectTransform.pivot.x, 0f);
		}
		else
		{
			this.rectTransform.pivot = new Vector2(this.rectTransform.pivot.x, 1f);
		}
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.canvasGroup.alpha = 1f;
		yield break;
	}

	// Token: 0x040003B3 RID: 947
	private Canvas canvas;

	// Token: 0x040003B4 RID: 948
	private RectTransform rectTransform;

	// Token: 0x040003B5 RID: 949
	private CanvasGroup canvasGroup;

	// Token: 0x040003B6 RID: 950
	[SerializeField]
	private GameObject contextMenuButtonPrefab;

	// Token: 0x040003B7 RID: 951
	[SerializeField]
	private Transform buttonTransform;

	// Token: 0x040003B8 RID: 952
	private GameFlowManager gameFlowManager;

	// Token: 0x040003B9 RID: 953
	private GameManager gameManager;

	// Token: 0x040003BA RID: 954
	private ContextMenuManager contextMenuManager;

	// Token: 0x040003BB RID: 955
	private CR8Manager cR8Manager;

	// Token: 0x040003BC RID: 956
	private bool previouslyViewingEvent;
}
