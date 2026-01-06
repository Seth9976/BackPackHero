using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E7 RID: 231
public class ItemPouch : MonoBehaviour
{
	// Token: 0x06000825 RID: 2085 RVA: 0x00055E1B File Offset: 0x0005401B
	public void OnDestroy()
	{
		ItemPouch.itemPouches.Remove(this);
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x00055E29 File Offset: 0x00054029
	public void Start()
	{
		this.wasToggledOpenWithPress = false;
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		this.petItem = base.GetComponent<PetItem2>();
		this.itemMovement = base.GetComponent<ItemMovement>();
		this.open = false;
		ItemPouch.itemPouches.Add(this);
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x00055E68 File Offset: 0x00054068
	public void Update()
	{
		if (this.itemMovement && this.itemMovement.isDragging && this.open)
		{
			this.ClosePouch();
		}
		if (this.isHoveringOver)
		{
			this.timeOver += Time.deltaTime;
			this.timeAway = 0f;
			if (this.timeOver > 0.2f)
			{
				this.wasEverHoveredOver = true;
			}
		}
		else if (!this.isHoveringOver && this.wasEverHoveredOver && !this.wasToggledOpenWithPress)
		{
			this.timeAway += Time.deltaTime;
			if (this.timeAway > 0.2f)
			{
				this.ClosePouch();
			}
		}
		if (this.open)
		{
			foreach (GameObject gameObject in this.itemsInside)
			{
				if (gameObject)
				{
					gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, -4.2f);
				}
			}
		}
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x00055F9C File Offset: 0x0005419C
	public void ReplaceInList(GameObject oldObj, GameObject newObj, ref List<GameObject> objs)
	{
		int num = objs.FindIndex((GameObject x) => x == oldObj);
		if (num != -1)
		{
			objs[num] = newObj;
		}
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x00055FD8 File Offset: 0x000541D8
	public static bool AnyPouchesHovered()
	{
		foreach (ItemPouch itemPouch in ItemPouch.itemPouches)
		{
			if (itemPouch && itemPouch.isHoveringOver)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x0005603C File Offset: 0x0005423C
	public bool IsHoveringOverThisPouch()
	{
		return this.isHoveringOver;
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x00056044 File Offset: 0x00054244
	public static List<Item2> GetAllItem2sFromPouches()
	{
		List<Item2> list = new List<Item2>();
		foreach (ItemPouch itemPouch in ItemPouch.itemPouches)
		{
			if (itemPouch)
			{
				foreach (GameObject gameObject in itemPouch.itemsInside)
				{
					Item2 component = gameObject.GetComponent<Item2>();
					list.Add(component);
				}
			}
		}
		return list;
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x000560E8 File Offset: 0x000542E8
	public static void AllPouchesMarkAllItemsAsOwned()
	{
		foreach (ItemPouch itemPouch in ItemPouch.itemPouches)
		{
			if (itemPouch)
			{
				itemPouch.MarkAllItemsAsOwned();
			}
		}
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x00056144 File Offset: 0x00054344
	public void MarkAllItemsAsOwned()
	{
		foreach (GameObject gameObject in this.itemsInside)
		{
			gameObject.SetActive(true);
			Item2 component = gameObject.GetComponent<Item2>();
			if (component)
			{
				component.cost = -999;
				component.isOwned = true;
				component.isForSale = false;
				if (component.itemMovement)
				{
					component.itemMovement.returnsToOutOfInventoryPosition = false;
					component.itemMovement.inPouch = true;
				}
			}
			gameObject.SetActive(false);
		}
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x000561EC File Offset: 0x000543EC
	public void Toggle()
	{
		if (this.open)
		{
			this.ClosePouch();
			return;
		}
		this.OpenPouch();
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x00056204 File Offset: 0x00054404
	public static List<GameObject> FindItemsInPouches(List<GameObject> items)
	{
		List<GameObject> list = new List<GameObject>();
		foreach (GameObject gameObject in items)
		{
			if (ItemPouch.FindPouchForItem(gameObject) != null)
			{
				list.Add(gameObject);
			}
		}
		return list;
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x00056268 File Offset: 0x00054468
	public static ItemPouch GetOpenPouch()
	{
		foreach (ItemPouch itemPouch in ItemPouch.itemPouches)
		{
			if (itemPouch && itemPouch.open)
			{
				return itemPouch;
			}
		}
		return null;
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x000562CC File Offset: 0x000544CC
	public static ItemPouch FindPouchForItem(GameObject item)
	{
		foreach (ItemPouch itemPouch in ItemPouch.itemPouches)
		{
			if (itemPouch && itemPouch.itemsInside.Contains(item))
			{
				return itemPouch;
			}
		}
		return null;
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x00056334 File Offset: 0x00054534
	public void AddItem(GameObject item)
	{
		if (!this.itemsInside.Contains(item))
		{
			this.itemsInside.Add(item);
			if (this.petItem)
			{
				this.petItem.SetupPetEffects();
			}
		}
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x00056368 File Offset: 0x00054568
	public static void CloseAllPouches()
	{
		foreach (ItemPouch itemPouch in ItemPouch.itemPouches)
		{
			if (itemPouch && itemPouch && itemPouch.open)
			{
				itemPouch.ClosePouch();
			}
		}
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x000563D4 File Offset: 0x000545D4
	public static void DeleteAllitemsRoll(out int numRemoved)
	{
		numRemoved = 0;
		foreach (ItemPouch itemPouch in ItemPouch.itemPouches)
		{
			if (itemPouch)
			{
				itemPouch.SetActiveAllPouchItems(true);
				for (int i = 0; i < itemPouch.itemsInside.Count; i++)
				{
					ItemMovement component = itemPouch.itemsInside[i].GetComponent<ItemMovement>();
					if (component.returnsToOutOfInventoryPosition)
					{
						numRemoved++;
						itemPouch.DeleteItem(component);
						i--;
					}
				}
				itemPouch.SetActiveAllPouchItems(false);
			}
		}
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x0005647C File Offset: 0x0005467C
	public void DeleteItem(ItemMovement x)
	{
		if (this.itemsInside.Contains(x.gameObject))
		{
			x.DelayDestroy();
			this.itemsInside.Remove(x.gameObject);
			Object.Destroy(x.gameObject);
		}
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x000564B4 File Offset: 0x000546B4
	public static void RemoveItemFromPouches(GameObject item, out bool wasIn)
	{
		wasIn = false;
		foreach (ItemPouch itemPouch in ItemPouch.itemPouches)
		{
			if (itemPouch && itemPouch.itemsInside.Contains(item))
			{
				itemPouch.itemsInside.Remove(item);
				if (itemPouch.petItem)
				{
					itemPouch.petItem.SetupPetEffects();
				}
				wasIn = true;
			}
		}
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x00056540 File Offset: 0x00054740
	public static void SetActiveAllPouchesAllItems(bool active)
	{
		foreach (ItemPouch itemPouch in ItemPouch.itemPouches)
		{
			if (itemPouch)
			{
				itemPouch.SetActiveAllPouchItems(active);
			}
		}
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x0005659C File Offset: 0x0005479C
	public void SetActiveAllPouchItems(bool active)
	{
		foreach (GameObject gameObject in this.itemsInside)
		{
			gameObject.SetActive(active);
			ItemMovement component = gameObject.GetComponent<ItemMovement>();
			if (component)
			{
				component.inPouch = true;
			}
			SpriteRenderer component2 = gameObject.GetComponent<SpriteRenderer>();
			if (component2 && this.spriteRenderer)
			{
				component2.sortingOrder = this.spriteRenderer.sortingOrder;
			}
		}
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x00056630 File Offset: 0x00054830
	public void MoveAllItems(Vector3 difference)
	{
		foreach (GameObject gameObject in this.itemsInside)
		{
			gameObject.transform.position += difference;
		}
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x00056694 File Offset: 0x00054894
	public static void SetAllItemsToPouchParent()
	{
		foreach (ItemPouch itemPouch in ItemPouch.itemPouches)
		{
			if (itemPouch)
			{
				itemPouch.SetAllItemsParent(itemPouch.transform);
			}
		}
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x000566F4 File Offset: 0x000548F4
	public static void SetAllItemsToItemsParent()
	{
		foreach (ItemPouch itemPouch in ItemPouch.itemPouches)
		{
			if (itemPouch)
			{
				itemPouch.SetAllItemsParent(GameObject.FindGameObjectWithTag("ItemParent").transform);
			}
		}
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x0005675C File Offset: 0x0005495C
	public void SetAllItemsParent(Transform parent)
	{
		foreach (GameObject gameObject in this.itemsInside)
		{
			gameObject.transform.SetParent(parent);
		}
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x000567B4 File Offset: 0x000549B4
	public void SetAllSprites()
	{
		Sprite standardGridSprite = Player.main.chosenCharacter.standardGridSprite;
		GridSquare[] componentsInChildren = this.pouchContents.GetComponentsInChildren<GridSquare>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].SetSprite(standardGridSprite);
		}
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x000567F4 File Offset: 0x000549F4
	public void OpenedViaClick()
	{
		if (!this.open && this.openingRoutine == null)
		{
			return;
		}
		this.wasToggledOpenWithPress = true;
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x0005680E File Offset: 0x00054A0E
	private IEnumerator OpenAfterDelay()
	{
		yield return new WaitForSeconds(0.25f);
		this.OpenPouch();
		this.openingRoutine = null;
		yield break;
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x00056820 File Offset: 0x00054A20
	public void OpenPouch()
	{
		if (this.open)
		{
			return;
		}
		if (this.openingRoutine != null)
		{
			base.StopCoroutine(this.openingRoutine);
		}
		if (GameManager.main.viewingTop != GameManager.ViewingTop.inventory)
		{
			this.openingRoutine = base.StartCoroutine(this.OpenAfterDelay());
			GameManager.main.ShowInventory();
			return;
		}
		ItemPouch.CloseAllPouches();
		GameFlowManager main = GameFlowManager.main;
		if (main.battlePhase != GameFlowManager.BattlePhase.outOfBattle && main.battlePhase != GameFlowManager.BattlePhase.playerTurn)
		{
			return;
		}
		this.wasEverHoveredOver = false;
		this.isHoveringOver = false;
		this.timeOver = 0f;
		this.timeAway = 0f;
		if (!this.pouchContents)
		{
			GameObject gameObject = this.pouchPrefabs[Random.Range(0, this.pouchPrefabs.Count)];
			this.pouchContents = Object.Instantiate<GameObject>(gameObject, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("Inventory").transform);
			this.pouchContents.transform.rotation = Quaternion.identity;
			foreach (SpriteRenderer spriteRenderer in this.pouchContents.GetComponentsInChildren<SpriteRenderer>())
			{
				spriteRenderer.sortingOrder = spriteRenderer.sortingOrder;
			}
			foreach (GridSquare gridSquare in this.pouchContents.GetComponentsInChildren<GridSquare>())
			{
				gridSquare.isPouch = true;
				gridSquare.itemPouch = this;
			}
		}
		if (this.spriteRenderer && this.openSprite)
		{
			this.spriteRenderer.sprite = this.openSprite;
		}
		this.open = true;
		this.pouchContents.SetActive(true);
		ItemPouchMaster componentInChildren = this.pouchContents.GetComponentInChildren<ItemPouchMaster>();
		if (componentInChildren)
		{
			componentInChildren.SetPouch(this);
		}
		this.SetActiveAllPouchItems(true);
		foreach (SpriteRenderer spriteRenderer2 in base.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer2.color = Color.white;
			if (spriteRenderer2 && this.spriteRenderer)
			{
				spriteRenderer2.sortingOrder = this.spriteRenderer.sortingOrder;
			}
		}
		foreach (GridSquare gridSquare2 in this.pouchContents.GetComponentsInChildren<GridSquare>())
		{
			if (gridSquare2.isPouch)
			{
				gridSquare2.itemPouch = this;
			}
		}
		this.SetAllSprites();
		this.pouchContents.transform.SetParent(GameObject.FindGameObjectWithTag("Inventory").transform);
		this.pouchContents.transform.rotation = Quaternion.identity;
		if (this.pouchPosition)
		{
			this.pouchContents.transform.position = this.pouchPosition.position;
			this.pouchContents.transform.localPosition = Vector3Int.RoundToInt(this.pouchContents.transform.localPosition);
		}
		else if (base.transform.localPosition.y > -1.3f)
		{
			this.pouchContents.transform.position = base.transform.position + Vector3.down * 4f + Vector3.back * 3f;
			this.pouchContents.transform.localPosition = Vector3Int.RoundToInt(this.pouchContents.transform.localPosition);
		}
		else
		{
			this.pouchContents.transform.position = base.transform.position + Vector3.up * 1.5f + Vector3.back * 3f;
			this.pouchContents.transform.localPosition = Vector3Int.RoundToInt(this.pouchContents.transform.localPosition);
		}
		Vector3 vector = this.pouchContents.transform.localPosition - this.lastOpeningPosition;
		this.lastOpeningPosition = this.pouchContents.transform.localPosition;
		this.MoveAllItems(vector);
		Store store = Object.FindObjectOfType<Store>();
		if (store)
		{
			store.SetPrices();
		}
		base.StartCoroutine(this.OpenPouchAnimation());
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x00056C4B File Offset: 0x00054E4B
	private IEnumerator OpenPouchAnimation()
	{
		float t = 0f;
		float totalTime = 0.1f;
		this.pouchContents.transform.localPosition = new Vector3(this.pouchContents.transform.localPosition.x, this.pouchContents.transform.localPosition.y, -7f);
		while (t < totalTime)
		{
			t += Time.deltaTime;
			this.pouchContents.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t / totalTime);
			foreach (GameObject gameObject in this.itemsInside)
			{
				gameObject.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t / totalTime);
			}
			yield return null;
		}
		this.pouchContents.transform.localScale = Vector3.one;
		foreach (GameObject gameObject2 in this.itemsInside)
		{
			gameObject2.transform.localScale = Vector3.one;
		}
		yield return new WaitForFixedUpdate();
		foreach (GridSquare gridSquare in this.pouchContents.GetComponentsInChildren<GridSquare>())
		{
			if (!gridSquare.TestForItem())
			{
				gridSquare.GetComponent<SpriteRenderer>().enabled = true;
			}
			GridObject component = gridSquare.GetComponent<GridObject>();
			if (component)
			{
				component.SnapToGrid();
			}
		}
		foreach (GameObject gameObject3 in this.itemsInside)
		{
			GridObject component2 = gameObject3.GetComponent<GridObject>();
			if (component2)
			{
				component2.SnapToGrid();
			}
		}
		this.pouchContents.transform.localPosition = new Vector3(this.pouchContents.transform.localPosition.x, this.pouchContents.transform.localPosition.y, -7f);
		yield break;
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x00056C5C File Offset: 0x00054E5C
	public void ClosePouch()
	{
		if (!this.open)
		{
			return;
		}
		SpriteRenderer component = base.GetComponent<SpriteRenderer>();
		if (component && this.closedSprite)
		{
			component.sprite = this.closedSprite;
		}
		this.pouchContents.transform.SetParent(base.transform);
		this.open = false;
		this.pouchContents.SetActive(false);
		this.SetActiveAllPouchItems(false);
		this.wasEverHoveredOver = false;
		this.isHoveringOver = false;
		this.wasToggledOpenWithPress = false;
		this.timeOver = 0f;
		this.timeAway = 0f;
	}

	// Token: 0x0400064E RID: 1614
	[SerializeField]
	public Sprite backgroundSprite;

	// Token: 0x0400064F RID: 1615
	[SerializeField]
	private List<GameObject> pouchPrefabs;

	// Token: 0x04000650 RID: 1616
	[SerializeField]
	public GameObject pouchContents;

	// Token: 0x04000651 RID: 1617
	public bool open;

	// Token: 0x04000652 RID: 1618
	private ItemMovement itemMovement;

	// Token: 0x04000653 RID: 1619
	private Vector3 lastOpeningPosition;

	// Token: 0x04000654 RID: 1620
	[SerializeField]
	public List<GameObject> itemsInside = new List<GameObject>();

	// Token: 0x04000655 RID: 1621
	[SerializeField]
	private Sprite openSprite;

	// Token: 0x04000656 RID: 1622
	[SerializeField]
	private Sprite closedSprite;

	// Token: 0x04000657 RID: 1623
	[SerializeField]
	public List<Item2.ItemType> allowedTypes;

	// Token: 0x04000658 RID: 1624
	private static List<ItemPouch> itemPouches = new List<ItemPouch>();

	// Token: 0x04000659 RID: 1625
	public PetItem2 petItem;

	// Token: 0x0400065A RID: 1626
	public bool isHoveringOver;

	// Token: 0x0400065B RID: 1627
	private bool wasEverHoveredOver;

	// Token: 0x0400065C RID: 1628
	[SerializeField]
	private bool wasToggledOpenWithPress;

	// Token: 0x0400065D RID: 1629
	public SpriteRenderer spriteRenderer;

	// Token: 0x0400065E RID: 1630
	private float timeOver;

	// Token: 0x0400065F RID: 1631
	private float timeAway;

	// Token: 0x04000660 RID: 1632
	[SerializeField]
	private Transform pouchPosition;

	// Token: 0x04000661 RID: 1633
	private Coroutine openingRoutine;
}
