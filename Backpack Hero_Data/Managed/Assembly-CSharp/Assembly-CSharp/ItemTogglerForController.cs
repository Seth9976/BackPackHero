using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000F6 RID: 246
public class ItemTogglerForController : MonoBehaviour
{
	// Token: 0x06000892 RID: 2194 RVA: 0x0005AB35 File Offset: 0x00058D35
	private void Start()
	{
		if (ItemTogglerForController.main && ItemTogglerForController.main != this)
		{
			Object.Destroy(this);
			return;
		}
		ItemTogglerForController.main = this;
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x0005AB5D File Offset: 0x00058D5D
	private void OnDestroy()
	{
		if (ItemTogglerForController.main == this)
		{
			ItemTogglerForController.main = null;
		}
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x0005AB74 File Offset: 0x00058D74
	private void Update()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.controller || GameManager.main.draggingItem)
		{
			this.buttonPromptLeft.gameObject.SetActive(false);
			this.buttonPromptRight.gameObject.SetActive(false);
			return;
		}
		if ((this.followItem && this.followItem.destroyed) || (this.followItemLeft && (this.followItemLeft.destroyed || !this.followItemLeft.itemMovement || this.followItemLeft.itemMovement.inGrid)) || (this.followItemRight && (this.followItemRight.destroyed || !this.followItemRight.itemMovement || this.followItemRight.itemMovement.inGrid)) || GameManager.main.viewingEvent)
		{
			this.ResetItem();
		}
		if (!this.followItemRight && !GameManager.main.draggingItem && !GameManager.main.viewingEvent)
		{
			this.currentTimeOut -= Time.deltaTime;
			if (this.currentTimeOut < 0f)
			{
				this.SetItem(null);
			}
		}
		else
		{
			this.currentTimeOut = this.timeOut;
		}
		if (this.followItemRight)
		{
			this.buttonPromptRight.gameObject.SetActive(true);
			this.buttonPromptRight.transform.position = this.followItemRight.transform.position + (Vector3.up + Vector3.right + Vector3.back) * 0.25f;
		}
		else
		{
			this.buttonPromptRight.gameObject.SetActive(false);
		}
		if (this.followItemLeft)
		{
			this.buttonPromptLeft.gameObject.SetActive(true);
			this.buttonPromptLeft.transform.position = this.followItemLeft.transform.position + (Vector3.up + Vector3.right + Vector3.back) * 0.25f;
			return;
		}
		this.buttonPromptLeft.gameObject.SetActive(false);
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x0005ADBC File Offset: 0x00058FBC
	public void ResetItem()
	{
		this.followItem = null;
		this.followItemLeft = null;
		this.followItemRight = null;
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x0005ADD3 File Offset: 0x00058FD3
	public void SetItemIfUnset(Item2 item)
	{
		if (!this.followItem)
		{
			this.SetItem(item);
		}
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x0005ADEC File Offset: 0x00058FEC
	public void SetItem(Item2 item)
	{
		List<Item2> list = Item2.allItems;
		list = Item2.RemoveItemsInGrid(list);
		list = Item2.SortItemsByPositionSimple(list);
		if (list.Count == 0)
		{
			this.ResetItem();
			return;
		}
		int num = list.IndexOf(item);
		if (num != -1)
		{
			this.followItem = list[num];
		}
		this.followItemLeft = null;
		this.followItemRight = null;
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x0005AE44 File Offset: 0x00059044
	public static GameObject GetClosestItemOrGridInDirection(Vector2 startingPosition, Vector2 dir, List<GameObject> ignoreItems)
	{
		List<GameObject> list = new List<GameObject>();
		foreach (Item2 item in Item2.allItems)
		{
			if (!item.destroyed)
			{
				list.Add(item.gameObject);
			}
		}
		foreach (GridSquare gridSquare in GridSquare.allGrids)
		{
			list.Add(gridSquare.gameObject);
		}
		if (GameManager.main && !GameManager.main.dead)
		{
			foreach (GridExtension gridExtension in GridExtension.gridExtensions)
			{
				list.Add(gridExtension.gameObject);
			}
			foreach (Enemy enemy in Enemy.allEnemies)
			{
				list.Add(enemy.gameObject);
			}
			foreach (EnemyActionPreview enemyActionPreview in EnemyActionPreview.enemyActionPreviews)
			{
				list.Add(enemyActionPreview.gameObject);
			}
			foreach (StatusEffect statusEffect in StatusEffect.allStatusEffects)
			{
				list.Add(statusEffect.gameObject);
			}
			foreach (DungeonRoom dungeonRoom in DungeonRoom.GetAllVisibleRooms())
			{
				list.Add(dungeonRoom.gameObject);
			}
			if (Player.main.characterName == Character.CharacterName.Pochette)
			{
				foreach (DraggableCombat draggableCombat in DraggableCombat.all)
				{
					if (!draggableCombat.isDragging)
					{
						list.Add(draggableCombat.gameObject);
					}
				}
			}
		}
		list = list.Except(ignoreItems).ToList<GameObject>();
		if (GameManager.main && GameManager.main.draggingItem)
		{
			list.Remove(GameManager.main.draggingItem.gameObject);
		}
		dir = dir.normalized;
		if (Mathf.Abs(dir.y) < 0.1f)
		{
			dir = new Vector2(dir.x, 0f);
		}
		if (Mathf.Abs(dir.x) < 0.1f)
		{
			dir = new Vector2(0f, dir.y);
		}
		dir = dir.normalized;
		Vector2 pos = startingPosition + dir * 0.5f;
		if (DigitalCursor.main.IsHoveringOnStatusEffect())
		{
			pos = startingPosition + dir * 0.05f;
		}
		List<GameObject> list2 = new List<GameObject>();
		foreach (GameObject gameObject in list)
		{
			if (gameObject.transform.position.x >= -10f && gameObject.transform.position.x <= 10f && gameObject.transform.position.y >= -5.5f && gameObject.transform.position.y <= 5.5f && Vector2.Distance(startingPosition, gameObject.transform.position) >= Vector2.Distance(pos, gameObject.transform.position) && (dir.x <= 0.5f || Mathf.Abs(dir.x) <= Mathf.Abs(dir.y) || Mathf.Abs(gameObject.transform.position.y - pos.y) <= Mathf.Abs(gameObject.transform.position.x - pos.x)) && (dir.x >= -0.5f || Mathf.Abs(dir.x) <= Mathf.Abs(dir.y) || Mathf.Abs(gameObject.transform.position.y - pos.y) <= Mathf.Abs(gameObject.transform.position.x - pos.x)) && (dir.y <= 0.5f || Mathf.Abs(dir.y) <= Mathf.Abs(dir.x) || Mathf.Abs(gameObject.transform.position.x - pos.x) <= Mathf.Abs(gameObject.transform.position.y - pos.y)) && (dir.y >= -0.5f || Mathf.Abs(dir.y) <= Mathf.Abs(dir.x) || Mathf.Abs(gameObject.transform.position.x - pos.x) <= Mathf.Abs(gameObject.transform.position.y - pos.y)))
			{
				list2.Add(gameObject);
			}
		}
		if (list2.Count == 0)
		{
			return null;
		}
		list2 = list2.OrderBy((GameObject x) => Vector2.Distance(pos, x.transform.position)).ToList<GameObject>();
		return list2[0];
	}

	// Token: 0x040006C0 RID: 1728
	public static ItemTogglerForController main;

	// Token: 0x040006C1 RID: 1729
	[SerializeField]
	private Item2 followItemRight;

	// Token: 0x040006C2 RID: 1730
	[SerializeField]
	private Item2 followItem;

	// Token: 0x040006C3 RID: 1731
	[SerializeField]
	private Item2 followItemLeft;

	// Token: 0x040006C4 RID: 1732
	[SerializeField]
	private Transform buttonPromptLeft;

	// Token: 0x040006C5 RID: 1733
	[SerializeField]
	private Transform buttonPromptRight;

	// Token: 0x040006C6 RID: 1734
	private float timeOut = 0.1f;

	// Token: 0x040006C7 RID: 1735
	private float currentTimeOut;
}
