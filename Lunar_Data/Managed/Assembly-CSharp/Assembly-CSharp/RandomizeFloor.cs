using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

// Token: 0x02000079 RID: 121
public class RandomizeFloor : MonoBehaviour
{
	// Token: 0x0600035F RID: 863 RVA: 0x00010D9A File Offset: 0x0000EF9A
	private void Start()
	{
		this.Setup();
	}

	// Token: 0x06000360 RID: 864 RVA: 0x00010DA4 File Offset: 0x0000EFA4
	private void Setup()
	{
		this.wallTilemap.CompressBounds();
		this.floorTilemap = base.gameObject.AddComponent<Tilemap>();
		base.gameObject.AddComponent<TilemapRenderer>();
		this.floorTilemap.origin = this.wallTilemap.origin;
		this.floorTilemap.size = this.wallTilemap.size;
		this.floorTilemap.ResizeBounds();
		for (int i = this.floorTilemap.origin.x; i < this.floorTilemap.origin.x + this.floorTilemap.size.x; i++)
		{
			for (int j = this.floorTilemap.origin.y; j < this.floorTilemap.origin.y + this.floorTilemap.size.y; j++)
			{
				TileBase tileBase = this.GetTile();
				if (tileBase == null)
				{
					tileBase = this.floorTiles[0].tile;
				}
				this.floorTilemap.SetTile(new Vector3Int(i, j, 0), tileBase);
			}
		}
	}

	// Token: 0x06000361 RID: 865 RVA: 0x00010ED4 File Offset: 0x0000F0D4
	private TileBase GetTile()
	{
		float num = this.floorTiles.Sum((RandomizeFloor.TileAndWeight x) => x.percent);
		float num2 = Random.Range(0f, num);
		float num3 = 0f;
		foreach (RandomizeFloor.TileAndWeight tileAndWeight in this.floorTiles)
		{
			num3 += tileAndWeight.percent;
			if (num2 < num3)
			{
				return tileAndWeight.tile;
			}
		}
		return null;
	}

	// Token: 0x04000290 RID: 656
	[SerializeField]
	private List<RandomizeFloor.TileAndWeight> floorTiles = new List<RandomizeFloor.TileAndWeight>();

	// Token: 0x04000291 RID: 657
	[SerializeField]
	private Tilemap wallTilemap;

	// Token: 0x04000292 RID: 658
	private Tilemap floorTilemap;

	// Token: 0x02000102 RID: 258
	[Serializable]
	private class TileAndWeight
	{
		// Token: 0x040004BE RID: 1214
		public TileBase tile;

		// Token: 0x040004BF RID: 1215
		public float percent;
	}
}
