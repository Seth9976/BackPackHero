using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000124 RID: 292
public class ItemMovementManager : MonoBehaviour
{
	// Token: 0x06000AE2 RID: 2786 RVA: 0x0006F0F1 File Offset: 0x0006D2F1
	private void OnEnable()
	{
		if (ItemMovementManager.main == null)
		{
			ItemMovementManager.main = this;
			return;
		}
		Object.Destroy(this);
	}

	// Token: 0x06000AE3 RID: 2787 RVA: 0x0006F10D File Offset: 0x0006D30D
	private void OnDisable()
	{
		if (ItemMovementManager.main == this)
		{
			ItemMovementManager.main = null;
		}
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x0006F122 File Offset: 0x0006D322
	private void Start()
	{
		this.gameManager = GameManager.main;
		this.gameFlowManager = GameFlowManager.main;
	}

	// Token: 0x06000AE5 RID: 2789 RVA: 0x0006F13A File Offset: 0x0006D33A
	private void Update()
	{
	}

	// Token: 0x06000AE6 RID: 2790 RVA: 0x0006F13C File Offset: 0x0006D33C
	public void CheckForMovementPublic()
	{
		if (this.isRunning)
		{
			return;
		}
		base.StartCoroutine(this.CheckForMovement());
	}

	// Token: 0x06000AE7 RID: 2791 RVA: 0x0006F154 File Offset: 0x0006D354
	private IEnumerator CheckForMovement()
	{
		this.isRunning = true;
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		List<Item2> allItemsInGrid = Item2.GetAllItemsInGrid();
		bool didMove = false;
		foreach (Item2 item in allItemsInGrid)
		{
			if (item && (!item.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBeMovedInCombatButReturnsToOriginalPosition) || this.gameManager.inventoryPhase != GameManager.InventoryPhase.inCombatMove))
			{
				ItemMovement component = item.GetComponent<ItemMovement>();
				if (component && component.inGrid)
				{
					PetMaster petMasterFromInventory = PetMaster.GetPetMasterFromInventory(item.parentInventoryGrid);
					if (!petMasterFromInventory || petMasterFromInventory.showingInventory)
					{
						Item2 itemWithStatusEffect = Item2.GetItemWithStatusEffect(Item2.ItemStatusEffect.Type.effectsGravity, null, false);
						Vector2 vector = new Vector2(0f, -1f);
						if (itemWithStatusEffect)
						{
							vector = itemWithStatusEffect.transform.up * -1f;
						}
						if (item.CheckForStatusEffect(Item2.ItemStatusEffect.Type.heavy))
						{
							bool flag = false;
							component.MoveItem(vector, 0, item.transform.position, out flag, false);
							if (flag)
							{
								didMove = true;
								yield return new WaitForFixedUpdate();
							}
						}
						else if (item.CheckForStatusEffect(Item2.ItemStatusEffect.Type.buoyant))
						{
							bool flag2 = false;
							Vector2 vector2 = vector * -1f;
							component.MoveItem(vector2, 0, item.transform.position, out flag2, false);
							if (flag2)
							{
								didMove = true;
								yield return new WaitForFixedUpdate();
							}
						}
					}
				}
			}
		}
		List<Item2>.Enumerator enumerator = default(List<Item2>.Enumerator);
		if (didMove)
		{
			yield return this.CheckForMovement();
		}
		else
		{
			this.gameFlowManager.CheckConstants();
		}
		this.isRunning = false;
		yield break;
		yield break;
	}

	// Token: 0x06000AE8 RID: 2792 RVA: 0x0006F164 File Offset: 0x0006D364
	public void SetAllMaterials()
	{
		RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.puzzleMode);
		foreach (Item2 item in Object.FindObjectsOfType<Item2>())
		{
			SpriteRenderer component = item.GetComponent<SpriteRenderer>();
			if (component.sharedMaterial == null)
			{
				component.sharedMaterial = this.gameManager.standardItemMaterial;
			}
			if (runProperty != null && runProperty.assignedGameObject.Contains(item.gameObject))
			{
				component.sharedMaterial = this.gameManager.outlineItemMaterial;
				component.sharedMaterial.color = Color.blue;
			}
		}
	}

	// Token: 0x06000AE9 RID: 2793 RVA: 0x0006F1F5 File Offset: 0x0006D3F5
	public void CheckAllForValidPlacementTote()
	{
	}

	// Token: 0x06000AEA RID: 2794 RVA: 0x0006F1F8 File Offset: 0x0006D3F8
	public void CheckAllForValidPlacementPublic()
	{
		RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.puzzleMode);
		if (runProperty)
		{
			this.CheckAllForValidPlacementProperty(runProperty);
		}
		if (Item2.GetItemWithStatusEffect(Item2.ItemStatusEffect.Type.cannotPlaceItemsOfSameTypeAdjacent, null, false))
		{
			foreach (Item2 item in Item2.GetAllItemsInGrid())
			{
				if (item)
				{
					List<Item2> list = new List<Item2>();
					item.FindItemsAndGridsinArea(list, new List<GridSquare>(), new List<Item2.Area> { Item2.Area.adjacent }, Item2.AreaDistance.all, null, null, null, true, false, true);
					foreach (Item2 item2 in list)
					{
						if (item2 && Item2.ShareItemTypes(item2.itemType, item.itemType))
						{
							PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("is28"));
							item.itemMovement.RemoveFromGrid();
							item.itemMovement.MoveOut(item.transform.position);
							break;
						}
					}
				}
			}
		}
		foreach (Item2 item3 in Item2.GetAllItemsInGrid())
		{
			if (item3.itemMovement && item3.itemMovement.inGrid)
			{
				if (item3.CheckForStatusEffect(Item2.ItemStatusEffect.Type.cannotBePlacedAtEdge) && item3.IsAtTheEdgeOfGrid())
				{
					PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("is34"), item3.transform.position);
					item3.itemMovement.RemoveFromGrid();
					item3.itemMovement.MoveOut(item3.transform.position);
				}
				if (item3.CheckForStatusEffect(Item2.ItemStatusEffect.Type.mustBePlacedAtEdge) && !item3.IsAtTheEdgeOfGrid())
				{
					PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("is33"), item3.transform.position);
					item3.itemMovement.RemoveFromGrid();
					item3.itemMovement.MoveOut(item3.transform.position);
				}
			}
		}
	}

	// Token: 0x06000AEB RID: 2795 RVA: 0x0006F490 File Offset: 0x0006D690
	public void CheckAllForValidPlacementProperty(RunType.RunProperty property)
	{
		List<Item2> allItemsInGrid = Item2.GetAllItemsInGrid();
		foreach (Item2 item in Object.FindObjectsOfType<Item2>())
		{
			if (!allItemsInGrid.Contains(item))
			{
				SpriteRenderer component = item.GetComponent<SpriteRenderer>();
				if (!property.assignedGameObject.Contains(item.gameObject))
				{
					component.sharedMaterial = this.gameManager.standardItemMaterial;
				}
			}
		}
		List<Item2> list = new List<Item2>();
		foreach (GameObject gameObject in property.assignedGameObject)
		{
			if (gameObject)
			{
				Item2 component2 = gameObject.GetComponent<Item2>();
				if (component2.itemMovement.inGrid)
				{
					list.Add(component2);
				}
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			Item2 item2 = list[j];
			if (item2)
			{
				this.FindAdjacentItems(item2, list);
			}
		}
		foreach (Item2 item3 in allItemsInGrid)
		{
			if (item3 && item3.gameObject)
			{
				SpriteRenderer component3 = item3.GetComponent<SpriteRenderer>();
				if (!list.Contains(item3))
				{
					component3.sharedMaterial = this.gameManager.outlineItemMaterial;
					component3.material.color = Color.red;
				}
				else if (component3.material.color == Color.red)
				{
					component3.sharedMaterial = this.gameManager.standardItemMaterial;
				}
			}
		}
	}

	// Token: 0x06000AEC RID: 2796 RVA: 0x0006F64C File Offset: 0x0006D84C
	public bool CheckAllForValidPlacementBool(RunType.RunProperty property)
	{
		List<Item2> allItemsInGrid = Item2.GetAllItemsInGrid();
		List<Item2> list = new List<Item2>();
		foreach (GameObject gameObject in property.assignedGameObject)
		{
			if (gameObject)
			{
				Item2 component = gameObject.GetComponent<Item2>();
				if (component.itemMovement.inGrid)
				{
					list.Add(component);
				}
			}
		}
		for (int i = 0; i < list.Count; i++)
		{
			Item2 item = list[i];
			if (item)
			{
				this.FindAdjacentItems(item, list);
			}
		}
		foreach (Item2 item2 in allItemsInGrid)
		{
			if (item2 && item2.gameObject && !list.Contains(item2))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x0006F760 File Offset: 0x0006D960
	private void FindAdjacentItems(Item2 item, List<Item2> validItems)
	{
		List<Item2> list = new List<Item2>();
		item.FindItemsAndGridsinArea(list, new List<GridSquare>(), new List<Item2.Area> { Item2.Area.adjacent }, Item2.AreaDistance.all, null, null, null, true, false, true);
		foreach (Item2 item2 in list)
		{
			if (!validItems.Contains(item2) && this.CheckForValidPlacement(item2, item))
			{
				validItems.Add(item2);
			}
		}
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x0006F7E8 File Offset: 0x0006D9E8
	private bool CheckForValidPlacement(Item2 adjItem, Item2 item)
	{
		return Item2.ShareItemTypes(item.itemType, adjItem.itemType) || item.rarity == adjItem.rarity;
	}

	// Token: 0x040008D6 RID: 2262
	public static ItemMovementManager main;

	// Token: 0x040008D7 RID: 2263
	public bool isRunning;

	// Token: 0x040008D8 RID: 2264
	private Coroutine checkingForMovement;

	// Token: 0x040008D9 RID: 2265
	private GameFlowManager gameFlowManager;

	// Token: 0x040008DA RID: 2266
	private GameManager gameManager;

	// Token: 0x040008DB RID: 2267
	public bool isCheckingForPuzzle;
}
