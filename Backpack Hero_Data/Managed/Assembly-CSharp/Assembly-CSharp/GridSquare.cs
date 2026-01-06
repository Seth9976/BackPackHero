using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D9 RID: 217
public class GridSquare : CustomInputHandler
{
	// Token: 0x0600064E RID: 1614 RVA: 0x0003DB3C File Offset: 0x0003BD3C
	public bool CountAsEmpty()
	{
		return this.itemCountsAsEmpty || !this.containsItem;
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x0003DB53 File Offset: 0x0003BD53
	private void OnEnable()
	{
		if (!GridSquare.allGrids.Contains(this))
		{
			GridSquare.allGrids.Add(this);
		}
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x0003DB6D File Offset: 0x0003BD6D
	private void OnDisable()
	{
		GridSquare.allGrids.Remove(this);
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x0003DB7B File Offset: 0x0003BD7B
	private void OnDestroy()
	{
		GridSquare.allGrids.Remove(this);
	}

	// Token: 0x06000652 RID: 1618 RVA: 0x0003DB8C File Offset: 0x0003BD8C
	private void Start()
	{
		this.gridBlock = base.GetComponentInParent<GridBlock>();
		if (!GridSquare.allGrids.Contains(this))
		{
			GridSquare.allGrids.Add(this);
		}
		if (!this.player)
		{
			this.player = Player.main;
		}
		if (!this.spriteRenderer)
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		}
		if (this.player && this.player.chosenCharacter)
		{
			this.SetSprite(this.player.chosenCharacter.standardGridSprite);
		}
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x0003DC24 File Offset: 0x0003BE24
	private void Update()
	{
		if (!this.gridObject && !this.gridBlock)
		{
			this.gridObject = base.GetComponent<GridObject>();
			if (!this.gridObject)
			{
				this.gridObject = base.gameObject.AddComponent<GridObject>();
			}
			this.gridObject.type = GridObject.Type.grid;
		}
		if (!this.player)
		{
			this.player = Player.main;
		}
		if (!this.containsItem)
		{
			this.itemCountsAsEmpty = false;
		}
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x0003DCA8 File Offset: 0x0003BEA8
	public static void AllGetGridObject()
	{
		foreach (GridSquare gridSquare in GridSquare.allGrids)
		{
			gridSquare.GetGridObject();
		}
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x0003DCF8 File Offset: 0x0003BEF8
	public void GetGridObject()
	{
		this.gridObject = base.GetComponent<GridObject>();
		if (!this.gridObject)
		{
			this.gridObject = base.gameObject.AddComponent<GridObject>();
		}
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x0003DD24 File Offset: 0x0003BF24
	public static void AddAllToGrid()
	{
		foreach (GridSquare gridSquare in GridSquare.allGrids)
		{
			gridSquare.AddToGrid();
		}
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x0003DD74 File Offset: 0x0003BF74
	private void AddToGrid()
	{
		this.gridObject.ClearGridPositions();
		this.gridObject.SnapToGrid();
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x0003DD8C File Offset: 0x0003BF8C
	public static bool BackpackIsFull()
	{
		using (List<GridSquare>.Enumerator enumerator = GridSquare.allGrids.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.containsItem)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x0003DDE4 File Offset: 0x0003BFE4
	public static List<GameObject> ConsiderIfHoveringInPouch(List<GameObject> gridSquares, out bool inPouch)
	{
		List<GameObject> list = new List<GameObject>();
		List<GameObject> list2 = new List<GameObject>();
		foreach (GameObject gameObject in gridSquares)
		{
			GridSquare component = gameObject.GetComponent<GridSquare>();
			if (component)
			{
				if (component.isPouch)
				{
					list.Add(gameObject);
				}
				else
				{
					list2.Add(gameObject);
				}
			}
		}
		inPouch = false;
		if (list.Count > 0)
		{
			inPouch = true;
			return list;
		}
		return list2;
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x0003DE74 File Offset: 0x0003C074
	public static void ChangeAllColliders(bool activate)
	{
		foreach (GridSquare gridSquare in GridSquare.allGrids)
		{
			gridSquare.GetComponent<BoxCollider2D>().enabled = activate;
		}
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x0003DECC File Offset: 0x0003C0CC
	public void SetColor()
	{
		if (!this.player)
		{
			this.player = Player.main;
		}
		if (!this.spriteRenderer)
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		}
		if (this.player.characterName == Character.CharacterName.Tote)
		{
			if (this.isRunic)
			{
				this.spriteRenderer.color = new Color(0.46f, 1f, 0.71f, this.spriteRenderer.color.a);
				return;
			}
			this.spriteRenderer.color = new Color(1f, 1f, 1f, this.spriteRenderer.color.a);
		}
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x0003DF7F File Offset: 0x0003C17F
	public void SetSprite(Sprite sprite)
	{
		if (!this.spriteRenderer)
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		}
		this.spriteRenderer.sprite = sprite;
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x0003DFA8 File Offset: 0x0003C1A8
	public bool TestForItem()
	{
		foreach (RaycastHit2D raycastHit2D in Physics2D.RaycastAll(base.transform.position, Vector2.zero))
		{
			if (raycastHit2D.collider.CompareTag("Item") && ItemPouch.FindPouchForItem(raycastHit2D.collider.gameObject))
			{
				ItemMovement component = raycastHit2D.collider.GetComponent<ItemMovement>();
				if (component && component.inGrid)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x0003E034 File Offset: 0x0003C234
	public static GridSquare GetClosestGridSquare(Vector2 position)
	{
		GridSquare gridSquare = null;
		float num = 999f;
		foreach (GridSquare gridSquare2 in GridSquare.allGrids)
		{
			float num2 = Vector2.Distance(position, gridSquare2.transform.position);
			if (num2 < num)
			{
				num = num2;
				gridSquare = gridSquare2;
			}
		}
		return gridSquare;
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x0003E0AC File Offset: 0x0003C2AC
	public static GridSquare GetClosestGridSquareInDirection(Vector2 position, Vector2 dir)
	{
		GridSquare gridSquare = null;
		float num = 999f;
		position += dir * 0.75f;
		foreach (GridSquare gridSquare2 in GridSquare.allGrids)
		{
			float num2 = Vector2.Distance(position, gridSquare2.transform.position);
			if (num2 < num && (dir.x <= 0.5f || Mathf.Abs(dir.x) <= Mathf.Abs(dir.y) || Mathf.Abs(gridSquare2.transform.position.y - position.y) <= Mathf.Abs(gridSquare2.transform.position.x - position.x)) && (dir.x >= -0.5f || Mathf.Abs(dir.x) <= Mathf.Abs(dir.y) || Mathf.Abs(gridSquare2.transform.position.y - position.y) <= Mathf.Abs(gridSquare2.transform.position.x - position.x)) && (dir.y <= 0.5f || Mathf.Abs(dir.y) <= Mathf.Abs(dir.x) || Mathf.Abs(gridSquare2.transform.position.x - position.x) <= Mathf.Abs(gridSquare2.transform.position.y - position.y)) && (dir.y >= -0.5f || Mathf.Abs(dir.y) <= Mathf.Abs(dir.x) || Mathf.Abs(gridSquare2.transform.position.x - position.x) <= Mathf.Abs(gridSquare2.transform.position.y - position.y)))
			{
				num = num2;
				gridSquare = gridSquare2;
			}
		}
		return gridSquare;
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x0003E2CC File Offset: 0x0003C4CC
	public static List<Vector2> GetAllGridVectors()
	{
		List<Vector2> list = new List<Vector2>();
		foreach (GridSquare gridSquare in GridSquare.allGrids)
		{
			list.Add(gridSquare.transform.position);
		}
		return list;
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x0003E334 File Offset: 0x0003C534
	public static GridSquare GetGridSquare(int x, int y)
	{
		foreach (GridSquare gridSquare in GridSquare.allGrids)
		{
			if (gridSquare.transform.localPosition.x == (float)x && gridSquare.transform.localPosition.y == (float)y)
			{
				return gridSquare;
			}
		}
		return null;
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x0003E3B0 File Offset: 0x0003C5B0
	public static List<GridSquare> GetAllOpenSquares()
	{
		List<GridSquare> list = new List<GridSquare>();
		foreach (GridSquare gridSquare in GridSquare.allGrids)
		{
			if (gridSquare.GetComponent<SpriteRenderer>().enabled)
			{
				list.Add(gridSquare);
			}
		}
		return list;
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x0003E418 File Offset: 0x0003C618
	public static int IsRunicSquareInList(List<GameObject> grids)
	{
		int num = 0;
		foreach (GameObject gameObject in grids)
		{
			GridSquare component = gameObject.GetComponent<GridSquare>();
			if (component && component.isRunic)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x0003E47C File Offset: 0x0003C67C
	public static int IsPouchSquareInList(List<GameObject> grids, out ItemPouch pouch)
	{
		pouch = null;
		int num = 0;
		foreach (GameObject gameObject in grids)
		{
			GridSquare component = gameObject.GetComponent<GridSquare>();
			if (component && component.isPouch)
			{
				num++;
				pouch = ItemPouch.GetOpenPouch();
			}
		}
		return num;
	}

	// Token: 0x04000517 RID: 1303
	public static List<GridSquare> allGrids = new List<GridSquare>();

	// Token: 0x04000518 RID: 1304
	private Player player;

	// Token: 0x04000519 RID: 1305
	private SpriteRenderer spriteRenderer;

	// Token: 0x0400051A RID: 1306
	public bool isRunic;

	// Token: 0x0400051B RID: 1307
	public bool isPouch;

	// Token: 0x0400051C RID: 1308
	public bool containsItem;

	// Token: 0x0400051D RID: 1309
	public bool itemCountsAsEmpty;

	// Token: 0x0400051E RID: 1310
	public ItemPouch itemPouch;

	// Token: 0x0400051F RID: 1311
	private GridObject gridObject;

	// Token: 0x04000520 RID: 1312
	private GridBlock gridBlock;
}
