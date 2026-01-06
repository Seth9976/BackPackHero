using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AD RID: 173
public class Satchel : MonoBehaviour
{
	// Token: 0x0600042A RID: 1066 RVA: 0x000296E9 File Offset: 0x000278E9
	private void Start()
	{
		this.objs = new List<GameObject>();
		this.gameManager = GameManager.main;
		this.gameFlowManager = GameFlowManager.main;
		Player main = Player.main;
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x00029712 File Offset: 0x00027912
	private void Update()
	{
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x00029714 File Offset: 0x00027914
	private Vector2 TranslateItemAreaToVector(Item2.Area area, Transform transformToUse)
	{
		Vector2 vector = default(Vector2);
		Item2.Area area2 = Item2.TranslateArea(area, transformToUse);
		if (area2 == Item2.Area.right)
		{
			vector = Vector2.right;
		}
		else if (area2 == Item2.Area.top)
		{
			vector = Vector2.up;
		}
		else if (area2 == Item2.Area.bottom)
		{
			vector = Vector2.down;
		}
		else if (area2 == Item2.Area.left)
		{
			vector = Vector2.left;
		}
		return vector;
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x00029760 File Offset: 0x00027960
	public void Cram(Item2 originItem, Item2.Area area, Item2.AreaDistance areaDistance, Item2.Area directionalOverride = Item2.Area.self)
	{
		Vector2 vector = this.TranslateItemAreaToVector(area, originItem.transform);
		area = Item2.TranslateArea(area, originItem.transform);
		List<ItemMovement> list = this.FindAdjacentItems(originItem, area, areaDistance);
		if (directionalOverride != Item2.Area.self)
		{
			area = directionalOverride;
			vector = this.TranslateItemAreaToVector(area, originItem.transform);
		}
		if (list.Count > 0)
		{
			List<ItemMovement> list2 = new List<ItemMovement>(list);
			while (list.Count > 0)
			{
				this.FindAdjacentItems(list2, list, out list, area, Item2.AreaDistance.adjacent);
			}
			foreach (ItemMovement itemMovement in list2)
			{
				itemMovement.SimpleMove(vector);
				itemMovement.ConsiderCramDestroy();
			}
		}
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x00029818 File Offset: 0x00027A18
	public void CramHightlight(Item2 originItem, Item2.Area area, Item2.AreaDistance areaDistance, Item2.Area directionalOverride = Item2.Area.self)
	{
		this.TranslateItemAreaToVector(area, originItem.transform);
		area = Item2.TranslateArea(area, originItem.transform);
		this.showingHighlight = true;
		List<ItemMovement> list = this.FindAdjacentItems(originItem, area, areaDistance);
		if (directionalOverride != Item2.Area.self)
		{
			area = directionalOverride;
			this.TranslateItemAreaToVector(area, originItem.transform);
		}
		if (list.Count > 0)
		{
			List<ItemMovement> list2 = new List<ItemMovement>(list);
			while (list.Count > 0)
			{
				this.FindAdjacentItems(list2, list, out list, area, Item2.AreaDistance.adjacent);
			}
			foreach (ItemMovement itemMovement in list2)
			{
				itemMovement.CreateHighlight(Color.white, null);
			}
		}
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x000298D4 File Offset: 0x00027AD4
	public void EndCramHighlights()
	{
		this.showingHighlight = false;
		ItemMovement.RemoveAllHighlights();
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x000298E2 File Offset: 0x00027AE2
	private IEnumerator ClearHighLightAfterFixedUpdate()
	{
		yield return new WaitForFixedUpdate();
		this.EndCramHighlights();
		yield break;
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x000298F4 File Offset: 0x00027AF4
	private List<ItemMovement> FindAdjacentItems(Item2 originItem, Item2.Area area, Item2.AreaDistance areaDistance)
	{
		List<Item2> list = new List<Item2>();
		originItem.FindItemsAndGridsinArea(list, new List<GridSquare>(), new List<Item2.Area> { area }, areaDistance, null, null, null, true, false, true);
		List<ItemMovement> list2 = new List<ItemMovement>();
		foreach (Item2 item in list)
		{
			list2.Add(item.GetComponent<ItemMovement>());
		}
		return list2;
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x00029974 File Offset: 0x00027B74
	private void FindAdjacentItems(List<ItemMovement> allItemsFoundSoFar, List<ItemMovement> itemsToSearchFrom, out List<ItemMovement> itemsFound, Item2.Area area, Item2.AreaDistance areaDistance)
	{
		itemsFound = new List<ItemMovement>();
		foreach (Component component in itemsToSearchFrom)
		{
			List<Item2> list = new List<Item2>();
			component.GetComponent<Item2>().FindItemsAndGridsinArea(list, new List<GridSquare>(), new List<Item2.Area> { area }, areaDistance, null, null, null, true, false, true);
			foreach (Item2 item in list)
			{
				ItemMovement component2 = item.GetComponent<ItemMovement>();
				if (component2 && component2.inGrid && !allItemsFoundSoFar.Contains(component2))
				{
					allItemsFoundSoFar.Add(component2);
					itemsFound.Add(component2);
				}
			}
		}
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x00029A58 File Offset: 0x00027C58
	public void DropAway(Vector3 pos, Quaternion rot, SpriteRenderer spriteRenderer)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.dropAwayPrefab, pos, rot);
		ItemAnimationProxy component = gameObject.GetComponent<ItemAnimationProxy>();
		component.CopySprite(spriteRenderer);
		component.FlyTo(pos, gameObject.transform);
	}

	// Token: 0x0400032F RID: 815
	private GameManager gameManager;

	// Token: 0x04000330 RID: 816
	private GameFlowManager gameFlowManager;

	// Token: 0x04000331 RID: 817
	[SerializeField]
	private GameObject dropAwayPrefab;

	// Token: 0x04000332 RID: 818
	private List<GameObject> objs;

	// Token: 0x04000333 RID: 819
	public bool showingHighlight;
}
