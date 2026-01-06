using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Token: 0x02000143 RID: 323
public class Overworld_Generator : MonoBehaviour
{
	// Token: 0x06000C69 RID: 3177 RVA: 0x0007FC5C File Offset: 0x0007DE5C
	private Tile GetRandomTile()
	{
		int num = 0;
		foreach (Overworld_Generator.TileAndFrequency tileAndFrequency in this.tilesAndFrequencies)
		{
			num += tileAndFrequency.frequency;
		}
		int num2 = Random.Range(0, num);
		int num3 = 0;
		foreach (Overworld_Generator.TileAndFrequency tileAndFrequency2 in this.tilesAndFrequencies)
		{
			num3 += tileAndFrequency2.frequency;
			if (num2 < num3)
			{
				return tileAndFrequency2.tile;
			}
		}
		return null;
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x0007FD18 File Offset: 0x0007DF18
	private void Start()
	{
		foreach (Vector3Int vector3Int in this.backgroundTilemap.cellBounds.allPositionsWithin)
		{
			TileBase tile = this.backgroundTilemap.GetTile(vector3Int);
			if (!(tile == null) && !(tile != this.replacementTile))
			{
				this.grassyTilemap.SetTile(vector3Int, this.GetRandomTile());
			}
		}
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x0007FDB0 File Offset: 0x0007DFB0
	private void Update()
	{
	}

	// Token: 0x04000A0B RID: 2571
	[SerializeField]
	private List<Overworld_Generator.TileAndFrequency> tilesAndFrequencies = new List<Overworld_Generator.TileAndFrequency>();

	// Token: 0x04000A0C RID: 2572
	[SerializeField]
	private Tilemap backgroundTilemap;

	// Token: 0x04000A0D RID: 2573
	[SerializeField]
	private Tilemap grassyTilemap;

	// Token: 0x04000A0E RID: 2574
	[SerializeField]
	private RuleTile replacementTile;

	// Token: 0x020003F5 RID: 1013
	[Serializable]
	public class TileAndFrequency
	{
		// Token: 0x04001768 RID: 5992
		public Tile tile;

		// Token: 0x04001769 RID: 5993
		public int frequency;
	}
}
