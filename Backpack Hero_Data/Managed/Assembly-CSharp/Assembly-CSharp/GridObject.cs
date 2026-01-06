using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000E4 RID: 228
public class GridObject : MonoBehaviour
{
	// Token: 0x06000705 RID: 1797 RVA: 0x00043E3E File Offset: 0x0004203E
	public void SetCollider()
	{
		this.SetColliders(base.GetComponent<BoxCollider2D>());
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x00043E4C File Offset: 0x0004204C
	public void SetColliders(BoxCollider2D collider)
	{
		this.SetColliders(new List<BoxCollider2D> { collider });
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x00043E60 File Offset: 0x00042060
	public void SetColliders(List<BoxCollider2D> colliders)
	{
		for (int i = 0; i < colliders.Count; i++)
		{
			if (!colliders[i])
			{
				colliders.RemoveAt(i);
				i--;
			}
		}
		this.colliders = colliders;
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x00043E9E File Offset: 0x0004209E
	private void OnEnable()
	{
		if (!GridObject.gridObjects.Contains(this))
		{
			GridObject.gridObjects.Add(this);
		}
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x00043EB8 File Offset: 0x000420B8
	private void OnDestroy()
	{
		this.ClearGridPositions();
		GridObject.gridObjects.Remove(this);
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x00043ECC File Offset: 0x000420CC
	private void OnDisable()
	{
		this.ClearGridPositions();
		GridObject.gridObjects.Remove(this);
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x00043EE0 File Offset: 0x000420E0
	private void Start()
	{
		if (!this.parentTransform || this.parentTransform == null)
		{
			this.parentTransform = base.transform;
		}
		if (CR8Manager.instance && CR8Manager.instance.isTesting)
		{
			return;
		}
		if (this.colliders.Count == 0)
		{
			this.colliders = base.GetComponentsInChildren<BoxCollider2D>().ToList<BoxCollider2D>();
		}
		if (!GridObject.grid || !GridObject.grid.gameObject.activeInHierarchy)
		{
			GridObject.grid = Object.FindObjectOfType<Grid>();
		}
		this.SnapToGrid();
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x00043F78 File Offset: 0x00042178
	private void GetColliders()
	{
		this.colliders = base.GetComponentsInChildren<BoxCollider2D>().ToList<BoxCollider2D>();
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x00043F8C File Offset: 0x0004218C
	public void ClearGridPositions()
	{
		foreach (Vector2Int vector2Int in this.gridPositions)
		{
			GridObject.gridObjectsArray[vector2Int.x + 300][vector2Int.y + 300].Remove(this);
		}
		this.gridPositions.Clear();
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x00044014 File Offset: 0x00042214
	public static void SnapAllToGrid()
	{
		foreach (GridObject gridObject in GridObject.gridObjects)
		{
			gridObject.SnapToGrid();
		}
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x00044064 File Offset: 0x00042264
	private IEnumerator SnapToGridCoroutine()
	{
		while (!this.ignoreScale && base.transform.lossyScale != Vector3.one && base.transform.lossyScale != new Vector3(-1f, 1f, 1f))
		{
			yield return null;
		}
		this.SnapToGrid();
		this.snapToGridCoroutine = null;
		yield break;
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x00044074 File Offset: 0x00042274
	public void SnapToGrid()
	{
		if (!this.parentTransform)
		{
			this.parentTransform = base.transform;
		}
		if (this.parentTransform.transform.position.x < -250f || this.parentTransform.transform.position.x > 1000f || this.parentTransform.transform.position.y < -250f || this.parentTransform.transform.position.y > 1000f)
		{
			return;
		}
		if (!GridObject.grid)
		{
			return;
		}
		if (this.colliders.Count == 0)
		{
			this.GetColliders();
			if (this.colliders.Count == 0)
			{
				return;
			}
		}
		if (!this.ignoreScale && base.transform.lossyScale != Vector3.one && base.transform.lossyScale != new Vector3(-1f, 1f, 1f))
		{
			if (this.snapToGridCoroutine == null)
			{
				this.snapToGridCoroutine = base.StartCoroutine(this.SnapToGridCoroutine());
			}
			return;
		}
		Vector2 vector = this.CalculateDifferenceToGrid(default(Vector3));
		this.UpdateGridPositions(vector);
		this.parentTransform.position += vector;
		this.parentTransform.localPosition = new Vector3(Mathf.Round(this.parentTransform.localPosition.x * 2f) / 2f, Mathf.Round(this.parentTransform.localPosition.y * 2f) / 2f, this.parentTransform.localPosition.z);
		foreach (GridObject gridObject in GridObject.GetItemsAtPosition(this.gridPositions))
		{
			gridObject == this;
		}
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x00044278 File Offset: 0x00042478
	private Vector2 GetColliderSize(Vector2 colliderSize)
	{
		if (base.transform.rotation.eulerAngles.z % 180f == 90f)
		{
			colliderSize = new Vector2(colliderSize.y, colliderSize.x);
		}
		return colliderSize;
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x000442C0 File Offset: 0x000424C0
	private Vector2 GetColliderOffset(Vector2 offset)
	{
		Vector2 vector = offset;
		vector = Quaternion.Euler(0f, 0f, base.transform.rotation.eulerAngles.z) * vector;
		if (base.transform.localScale == new Vector3(-1f, 1f, 1f))
		{
			vector.x = -vector.x;
		}
		return vector;
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x0004433C File Offset: 0x0004253C
	public Vector2 CalculateDifferenceToGrid(Vector3 offset = default(Vector3))
	{
		if (this.colliders.Count == 0 || !this.colliders[0])
		{
			this.GetColliders();
			if (this.colliders.Count == 0)
			{
				Debug.LogError("No colliders found. You need colliders so the game knows what to snap to");
				return Vector2.zero;
			}
		}
		Vector2 vector = this.colliders[0].transform.position + offset + this.GetColliderOffset(this.colliders[0].offset);
		Vector3Int vector3Int = GridObject.grid.WorldToCell(vector);
		Vector2 vector2 = GridObject.grid.GetCellCenterWorld(vector3Int) - vector;
		Vector2 colliderSize = this.GetColliderSize(this.colliders[0].size);
		if (colliderSize.x % 2f == 0f)
		{
			if (vector2.x > 0f)
			{
				vector2.x -= 0.5f;
			}
			else
			{
				vector2.x += 0.5f;
			}
		}
		if (colliderSize.y % 2f == 0f)
		{
			if (vector2.y > 0f)
			{
				vector2.y -= 0.5f;
			}
			else
			{
				vector2.y += 0.5f;
			}
		}
		return vector2;
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x0004449E File Offset: 0x0004269E
	public static Vector2Int WorldToCell(Vector2 worldPosition)
	{
		return (Vector2Int)GridObject.grid.WorldToCell(worldPosition);
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x000444B8 File Offset: 0x000426B8
	public static List<Vector2> CellToWorld(List<Vector2> cellPositions)
	{
		List<Vector2> list = new List<Vector2>();
		foreach (Vector2 vector in cellPositions)
		{
			list.Add(GridObject.CellToWorld(Vector2Int.RoundToInt(vector)));
		}
		return list;
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x00044518 File Offset: 0x00042718
	public static List<Vector2> CellToWorld(List<Vector2Int> cellPositions)
	{
		List<Vector2> list = new List<Vector2>();
		foreach (Vector2Int vector2Int in cellPositions)
		{
			list.Add(GridObject.CellToWorld(vector2Int));
		}
		return list;
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x00044574 File Offset: 0x00042774
	public static Vector2 CellToWorld(Vector2Int cellPosition)
	{
		return GridObject.grid.GetCellCenterWorld((Vector3Int)cellPosition);
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x0004458C File Offset: 0x0004278C
	public List<Vector2Int> GetGridPositionsAtPosition(Vector2 position)
	{
		Vector2 vector = base.transform.position - position;
		Vector2 vector2 = this.CalculateDifferenceToGrid(vector);
		List<Vector2Int> list = new List<Vector2Int>();
		foreach (BoxCollider2D boxCollider2D in this.colliders)
		{
			Vector2Int vector2Int = (Vector2Int)GridObject.grid.WorldToCell(boxCollider2D.transform.position - vector + this.GetColliderOffset(boxCollider2D.offset) + new Vector3(0.1f, 0.1f, 0f) + vector2 - this.GetColliderSize(boxCollider2D.size) / 2f);
			Vector2Int vector2Int2 = (Vector2Int)GridObject.grid.WorldToCell(boxCollider2D.transform.position - vector + this.GetColliderOffset(boxCollider2D.offset) - new Vector3(0.1f, 0.1f, 0f) + vector2 + this.GetColliderSize(boxCollider2D.size) / 2f);
			for (int i = vector2Int.x; i <= vector2Int2.x; i++)
			{
				for (int j = vector2Int.y; j <= vector2Int2.y; j++)
				{
					Vector2Int vector2Int3 = new Vector2Int(i, j);
					if (!list.Contains(vector2Int3))
					{
						list.Add(vector2Int3);
					}
				}
			}
		}
		return list;
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x00044774 File Offset: 0x00042974
	private void UpdateGridPositions(Vector2 difference)
	{
		this.ClearGridPositions();
		foreach (BoxCollider2D boxCollider2D in this.colliders)
		{
			Vector2Int vector2Int = (Vector2Int)GridObject.grid.WorldToCell(boxCollider2D.transform.position + this.GetColliderOffset(boxCollider2D.offset) + new Vector3(0.1f, 0.1f, 0f) + difference - this.GetColliderSize(boxCollider2D.size) / 2f);
			Vector2Int vector2Int2 = (Vector2Int)GridObject.grid.WorldToCell(boxCollider2D.transform.position + this.GetColliderOffset(boxCollider2D.offset) - new Vector3(0.1f, 0.1f, 0f) + difference + this.GetColliderSize(boxCollider2D.size) / 2f);
			for (int i = vector2Int.x; i <= vector2Int2.x; i++)
			{
				for (int j = vector2Int.y; j <= vector2Int2.y; j++)
				{
					Vector2Int vector2Int3 = new Vector2Int(i, j);
					if (!this.gridPositions.Contains(vector2Int3))
					{
						this.gridPositions.Add(vector2Int3);
						while (GridObject.gridObjectsArray.Count <= i + 300)
						{
							GridObject.gridObjectsArray.Add(new List<List<GridObject>>());
						}
						while (GridObject.gridObjectsArray[i + 300].Count <= j + 300)
						{
							GridObject.gridObjectsArray[i + 300].Add(new List<GridObject>());
						}
						GridObject.gridObjectsArray[i + 300][j + 300].Add(this);
					}
				}
			}
		}
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x000449BC File Offset: 0x00042BBC
	private void OnDrawGizmosSelected()
	{
		if (!GridObject.grid)
		{
			GridObject.grid = Object.FindObjectOfType<Grid>();
		}
		foreach (BoxCollider2D boxCollider2D in this.colliders)
		{
			Vector2Int vector2Int = (Vector2Int)GridObject.grid.WorldToCell(boxCollider2D.transform.position + this.GetColliderOffset(boxCollider2D.offset) + new Vector3(0.1f, 0.1f, 0f) - this.GetColliderSize(boxCollider2D.size) / 2f);
			Vector2Int vector2Int2 = (Vector2Int)GridObject.grid.WorldToCell(boxCollider2D.transform.position + this.GetColliderOffset(boxCollider2D.offset) - new Vector3(0.1f, 0.1f, 0f) + this.GetColliderSize(boxCollider2D.size) / 2f);
			for (int i = vector2Int.x; i <= vector2Int2.x; i++)
			{
				for (int j = vector2Int.y; j <= vector2Int2.y; j++)
				{
					Vector2Int vector2Int3 = new Vector2Int(i, j);
					if (this.type == GridObject.Type.item)
					{
						Gizmos.color = Color.magenta;
						Gizmos.DrawWireCube(GridObject.grid.GetCellCenterWorld((Vector3Int)vector2Int3), Vector3.one * 0.5f);
					}
					if (this.type == GridObject.Type.grid)
					{
						Gizmos.color = Color.yellow;
						Gizmos.DrawWireSphere(GridObject.grid.GetCellCenterWorld((Vector3Int)vector2Int3), 0.2f);
					}
				}
			}
		}
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x00044BB8 File Offset: 0x00042DB8
	public static List<Vector2Int> GetGridPositionsFromMultiple(List<GridObject> gridObjects)
	{
		List<Vector2Int> list = new List<Vector2Int>();
		foreach (GridObject gridObject in gridObjects)
		{
			list.AddRange(gridObject.GetGridPositions());
		}
		return list;
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x00044C14 File Offset: 0x00042E14
	public List<Vector2> GetWorldPositions()
	{
		return this.gridPositions.Select((Vector2Int x) => GridObject.grid.GetCellCenterWorld((Vector3Int)x)).ToList<Vector2>();
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x00044C45 File Offset: 0x00042E45
	public List<Vector2Int> GetGridPositions()
	{
		return this.gridPositions;
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x00044C50 File Offset: 0x00042E50
	public List<Vector2> GetWorldPositionsInLine(Vector2 worldStartPosition, Vector2Int direction, int length)
	{
		List<Vector2> list = new List<Vector2>();
		Vector3Int vector3Int = GridObject.grid.WorldToCell(worldStartPosition);
		for (int i = 0; i < length; i++)
		{
			Vector3Int vector3Int2 = vector3Int + (Vector3Int)direction * i;
			if (this.gridPositions.Contains((Vector2Int)vector3Int2))
			{
				list.Add(GridObject.grid.GetCellCenterWorld(vector3Int2));
			}
		}
		return list;
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x00044CC0 File Offset: 0x00042EC0
	public static List<Vector2Int> AdjacentVecs(List<Vector2Int> positions, int extendWithPath, List<GridObject.Tag> tagsForExpansion, int naturallyExpand)
	{
		List<Vector2Int> list = new List<Vector2Int>();
		foreach (Vector2Int vector2Int in positions)
		{
			list.Add(vector2Int + Vector2Int.up);
			list.Add(vector2Int + Vector2Int.down);
			list.Add(vector2Int + Vector2Int.left);
			list.Add(vector2Int + Vector2Int.right);
		}
		List<Vector2Int> list2 = new List<Vector2Int>(positions);
		for (int i = 0; i < naturallyExpand; i++)
		{
			foreach (Vector2Int vector2Int2 in new List<Vector2Int>(list))
			{
				list.Add(vector2Int2 + Vector2Int.up);
				list.Add(vector2Int2 + Vector2Int.down);
				list.Add(vector2Int2 + Vector2Int.left);
				list.Add(vector2Int2 + Vector2Int.right);
			}
		}
		list = list.Distinct<Vector2Int>().ToList<Vector2Int>();
		list = list.Except(positions).ToList<Vector2Int>();
		for (int j = 0; j < extendWithPath; j++)
		{
			foreach (Vector2Int vector2Int3 in new List<Vector2Int>(list))
			{
				if (!list2.Contains(vector2Int3))
				{
					list2.Add(vector2Int3);
					List<GridObject> itemsAtPosition = GridObject.GetItemsAtPosition(vector2Int3);
					List<GridObject> list3 = GridObject.FilterByType(itemsAtPosition, GridObject.Type.grid);
					if (list3.Count > 0 && itemsAtPosition.Count <= list3.Count && GridObject.HasAppropriateTags(list3, tagsForExpansion))
					{
						list.Add(vector2Int3 + Vector2Int.up);
						list.Add(vector2Int3 + Vector2Int.down);
						list.Add(vector2Int3 + Vector2Int.left);
						list.Add(vector2Int3 + Vector2Int.right);
					}
				}
			}
		}
		list = list.Distinct<Vector2Int>().ToList<Vector2Int>();
		list = list.Except(positions).ToList<Vector2Int>();
		return list;
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x00044F10 File Offset: 0x00043110
	public static List<GridObject> GetAdjacentItems(List<Vector2Int> positions, int extendWithPath, List<GridObject.Tag> tagsForExpansion, int naturallyExpand)
	{
		new List<GridObject>();
		return GridObject.GetItemsAtPosition(GridObject.AdjacentVecs(positions, extendWithPath, tagsForExpansion, naturallyExpand)).Distinct<GridObject>().ToList<GridObject>();
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x00044F30 File Offset: 0x00043130
	public static List<GridObject> GetGridObjectsAtCollider(Collider2D collider2D, Vector3 worldPosition)
	{
		List<Vector2Int> list = new List<Vector2Int>();
		Vector2Int vector2Int = (Vector2Int)GridObject.grid.WorldToCell(worldPosition + collider2D.offset + new Vector3(0.1f, 0.1f, 0f) - collider2D.bounds.size / 2f);
		Vector2Int vector2Int2 = (Vector2Int)GridObject.grid.WorldToCell(worldPosition + collider2D.offset - new Vector3(0.1f, 0.1f, 0f) + collider2D.bounds.size / 2f);
		for (int i = vector2Int.x; i <= vector2Int2.x; i++)
		{
			for (int j = vector2Int.y; j <= vector2Int2.y; j++)
			{
				Vector2Int vector2Int3 = new Vector2Int(i, j);
				list.Add(vector2Int3);
			}
		}
		return GridObject.GetItemsAtPosition(list);
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x00045044 File Offset: 0x00043244
	public static List<GridObject> GetItemsInLine(Vector2 worldStartPosition, Vector2Int direction, int length)
	{
		List<GridObject> list = new List<GridObject>();
		Vector3Int vector3Int = GridObject.grid.WorldToCell(worldStartPosition);
		for (int i = 0; i < length; i++)
		{
			List<GridObject> itemsAtPosition = GridObject.GetItemsAtPosition((Vector2Int)(vector3Int + (Vector3Int)direction * i));
			list.AddRange(itemsAtPosition);
		}
		return list.Distinct<GridObject>().ToList<GridObject>();
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x000450A8 File Offset: 0x000432A8
	public static List<GridObject> GetItemsAtPosition(List<Vector2Int> positions)
	{
		List<GridObject> list = new List<GridObject>();
		foreach (Vector2Int vector2Int in positions)
		{
			list.AddRange(GridObject.GetItemsAtPosition(vector2Int));
		}
		list = list.Distinct<GridObject>().ToList<GridObject>();
		return list;
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x00045110 File Offset: 0x00043310
	public static List<GridObject> GetItemsAtPosition(Vector2 worldPosition)
	{
		return GridObject.GetItemsAtPosition((Vector2Int)GridObject.grid.WorldToCell(worldPosition));
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x0004512C File Offset: 0x0004332C
	public static List<GridObject> GetItemsAtPosition(Vector2Int position)
	{
		if (position.x + 300 < 0 || position.y + 300 < 0)
		{
			return new List<GridObject>();
		}
		if (GridObject.gridObjectsArray.Count <= position.x + 300 || GridObject.gridObjectsArray[position.x + 300].Count <= position.y + 300)
		{
			return new List<GridObject>();
		}
		return new List<GridObject>(GridObject.gridObjectsArray[position.x + 300][position.y + 300]);
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x000451D8 File Offset: 0x000433D8
	public static List<GridObject> FilterByType(List<GridObject> objects, GridObject.Type type)
	{
		return objects.Where((GridObject x) => x.type == type).ToList<GridObject>();
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x0004520C File Offset: 0x0004340C
	public static bool HasAppropriateTags(List<GridObject> objects, List<GridObject.Tag> tagsForExpansion)
	{
		foreach (GridObject gridObject in objects)
		{
			if (gridObject.tagType != GridObject.Tag.none && tagsForExpansion.Contains(gridObject.tagType))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x040005A9 RID: 1449
	private static List<GridObject> gridObjects = new List<GridObject>();

	// Token: 0x040005AA RID: 1450
	[SerializeField]
	private Transform parentTransform;

	// Token: 0x040005AB RID: 1451
	[SerializeField]
	private List<BoxCollider2D> colliders = new List<BoxCollider2D>();

	// Token: 0x040005AC RID: 1452
	[SerializeField]
	private static Grid grid;

	// Token: 0x040005AD RID: 1453
	[SerializeField]
	private bool ignoreScale;

	// Token: 0x040005AE RID: 1454
	public GridObject.Type type;

	// Token: 0x040005AF RID: 1455
	public GridObject.Tag tagType;

	// Token: 0x040005B0 RID: 1456
	public List<Vector2Int> gridPositions = new List<Vector2Int>();

	// Token: 0x040005B1 RID: 1457
	public List<Vector2Int> lastGridPositions = new List<Vector2Int>();

	// Token: 0x040005B2 RID: 1458
	private const int NUM_TO_PREVENT_NEG_INDEX = 300;

	// Token: 0x040005B3 RID: 1459
	private static List<List<List<GridObject>>> gridObjectsArray = new List<List<List<GridObject>>>();

	// Token: 0x040005B4 RID: 1460
	private Coroutine snapToGridCoroutine;

	// Token: 0x02000323 RID: 803
	public enum Type
	{
		// Token: 0x04001283 RID: 4739
		item,
		// Token: 0x04001284 RID: 4740
		grid,
		// Token: 0x04001285 RID: 4741
		neither
	}

	// Token: 0x02000324 RID: 804
	public enum Tag
	{
		// Token: 0x04001287 RID: 4743
		none,
		// Token: 0x04001288 RID: 4744
		any,
		// Token: 0x04001289 RID: 4745
		path,
		// Token: 0x0400128A RID: 4746
		brick,
		// Token: 0x0400128B RID: 4747
		stone,
		// Token: 0x0400128C RID: 4748
		farm,
		// Token: 0x0400128D RID: 4749
		water,
		// Token: 0x0400128E RID: 4750
		entrance,
		// Token: 0x0400128F RID: 4751
		blocksPath,
		// Token: 0x04001290 RID: 4752
		Boundry,
		// Token: 0x04001291 RID: 4753
		decoration,
		// Token: 0x04001292 RID: 4754
		bridge
	}
}
