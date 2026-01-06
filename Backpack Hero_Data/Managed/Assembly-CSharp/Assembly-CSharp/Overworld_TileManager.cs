using System;
using UnityEngine;
using UnityEngine.Tilemaps;

// Token: 0x0200015A RID: 346
public class Overworld_TileManager : MonoBehaviour
{
	// Token: 0x06000DE5 RID: 3557 RVA: 0x0008AAD3 File Offset: 0x00088CD3
	private void Awake()
	{
		Overworld_TileManager.main = this;
	}

	// Token: 0x06000DE6 RID: 3558 RVA: 0x0008AADB File Offset: 0x00088CDB
	private void OnDestroy()
	{
		Overworld_TileManager.main = null;
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x0008AAE4 File Offset: 0x00088CE4
	public void RemoveTilesInArea(Vector2 center, Vector2 area)
	{
		area = new Vector2(Mathf.Floor(area.x), Mathf.Floor(area.y));
		center = new Vector2(Mathf.Ceil(center.x), Mathf.Ceil(center.y));
		Vector3Int vector3Int = this.tilemap.WorldToCell(center - area / 2f);
		Vector3Int vector3Int2 = this.tilemap.WorldToCell(center + area / 2f);
		string text = "Center: ";
		Vector2 vector = center;
		string text2 = vector.ToString();
		string text3 = " Area: ";
		vector = area;
		Debug.Log(text + text2 + text3 + vector.ToString());
		string text4 = "Min: ";
		Vector3Int vector3Int3 = vector3Int;
		string text5 = vector3Int3.ToString();
		string text6 = " Max: ";
		vector3Int3 = vector3Int2;
		Debug.Log(text4 + text5 + text6 + vector3Int3.ToString());
		for (float num = (float)vector3Int.x; num <= (float)vector3Int2.x; num += 1f)
		{
			for (float num2 = (float)vector3Int.y; num2 <= (float)vector3Int2.y; num2 += 1f)
			{
				Debug.Log("Removing tile at " + new Vector3(num, num2, 0f).ToString());
				this.RemoveTileAtPosition(new Vector3(num, num2, 0f));
			}
		}
		this.tilemap.RefreshAllTiles();
	}

	// Token: 0x06000DE8 RID: 3560 RVA: 0x0008AC64 File Offset: 0x00088E64
	public TileBase GetTileAtPosition(Vector3 position, out Sprite sprite)
	{
		TileBase tile = this.tilemap.GetTile(this.tilemap.WorldToCell(position));
		Sprite sprite2 = this.tilemap.GetSprite(this.tilemap.WorldToCell(position));
		sprite = sprite2;
		return tile;
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x0008ACA3 File Offset: 0x00088EA3
	public void RemoveTileAtPosition(Vector3 position)
	{
		this.tilemap.SetTile(this.tilemap.WorldToCell(position), null);
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x0008ACBD File Offset: 0x00088EBD
	public void AddTileAtPosition(Vector3 position, Tile tile)
	{
		this.tilemap.SetTile(this.tilemap.WorldToCell(position), tile);
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x0008ACD7 File Offset: 0x00088ED7
	public void AddTileAtPosition(Vector3 position, RuleTile tile)
	{
		this.tilemap.SetTile(this.tilemap.WorldToCell(position), tile);
	}

	// Token: 0x04000B49 RID: 2889
	public static Overworld_TileManager main;

	// Token: 0x04000B4A RID: 2890
	[SerializeField]
	private Tilemap tilemap;
}
