using System;
using UnityEngine;
using UnityEngine.Tilemaps;

// Token: 0x02000081 RID: 129
public class Room : MonoBehaviour
{
	// Token: 0x06000380 RID: 896 RVA: 0x000117A4 File Offset: 0x0000F9A4
	private void Start()
	{
		if (!this.enemyWave)
		{
			HordeRemainingDisplay.instance.ShowTimer(false);
		}
		Room.SpecialObjective specialObjective = this.specialObjective;
		if (this.wallTilemap)
		{
			this.wallTilemap.CompressBounds();
			this.bounds = default(Bounds);
			this.bounds.center = this.wallTilemap.localBounds.center;
			this.bounds.extents = new Vector3(this.wallTilemap.localBounds.extents.x, this.wallTilemap.localBounds.extents.y, 999f);
		}
		WaveManager.instance.SetCurrentWave(this.enemyWave);
		WaveManager.instance.StartWave();
		HordeRemainingDisplay.instance.SetTimeToSurvive(this.timeToSurvive);
	}

	// Token: 0x06000381 RID: 897 RVA: 0x00011883 File Offset: 0x0000FA83
	public Vector2 GetCenter()
	{
		return base.transform.position + this.bounds.center;
	}

	// Token: 0x06000382 RID: 898 RVA: 0x000118A8 File Offset: 0x0000FAA8
	public Vector2 GetRandomPointComfortablyInsideRoom()
	{
		Bounds bounds = this.bounds;
		bounds.extents = new Vector3(bounds.extents.x - 1.5f, bounds.extents.y - 1.5f, 1000f);
		return new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
	}

	// Token: 0x06000383 RID: 899 RVA: 0x00011930 File Offset: 0x0000FB30
	public bool ComfortablyInsideRoom(Vector2 point)
	{
		Bounds bounds = this.bounds;
		bounds.extents = new Vector3(bounds.extents.x - 1.5f, bounds.extents.y - 1.5f, 1000f);
		bounds.center = this.bounds.center + base.transform.position;
		return bounds.Contains(point);
	}

	// Token: 0x06000384 RID: 900 RVA: 0x000119A8 File Offset: 0x0000FBA8
	public Vector2 ClampPositionToComfortablyInSideRoom(Vector2 point)
	{
		Bounds bounds = this.bounds;
		bounds.extents = new Vector3(bounds.extents.x - 1.5f, bounds.extents.y - 1.5f, 1000f);
		bounds.center = this.bounds.center + base.transform.position;
		Vector2 vector = point;
		if (point.x < bounds.min.x)
		{
			vector.x = bounds.min.x;
		}
		if (point.x > bounds.max.x)
		{
			vector.x = bounds.max.x;
		}
		if (point.y < bounds.min.y)
		{
			vector.y = bounds.min.y;
		}
		if (point.y > bounds.max.y)
		{
			vector.y = bounds.max.y;
		}
		return vector;
	}

	// Token: 0x06000385 RID: 901 RVA: 0x00011AB4 File Offset: 0x0000FCB4
	public Vector2 GetRandomPosition()
	{
		Bounds bounds = this.bounds;
		bounds.extents = new Vector3(bounds.extents.x - 1f, bounds.extents.y - 1f, 1000f);
		bounds.center = this.bounds.center + base.transform.position;
		return new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
	}

	// Token: 0x06000386 RID: 902 RVA: 0x00011B5E File Offset: 0x0000FD5E
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(new Vector3(0f, 1f, 0f), new Vector2(20f, 10f));
	}

	// Token: 0x040002A9 RID: 681
	[SerializeField]
	public Bounds bounds;

	// Token: 0x040002AA RID: 682
	[SerializeField]
	private Tilemap wallTilemap;

	// Token: 0x040002AB RID: 683
	[SerializeField]
	private GameObject floorTilemapPrefab;

	// Token: 0x040002AC RID: 684
	[SerializeField]
	public EnemyWave enemyWave;

	// Token: 0x040002AD RID: 685
	[SerializeField]
	public Room.SpecialObjective specialObjective;

	// Token: 0x040002AE RID: 686
	[SerializeField]
	public float specialObjectiveValue;

	// Token: 0x040002AF RID: 687
	[SerializeField]
	private float timeToSurvive = 30f;

	// Token: 0x040002B0 RID: 688
	[SerializeField]
	public int validForLevel = 1;

	// Token: 0x040002B1 RID: 689
	[SerializeField]
	public Transform playerSpawnPosition;

	// Token: 0x02000107 RID: 263
	public enum SpecialObjective
	{
		// Token: 0x040004D0 RID: 1232
		None,
		// Token: 0x040004D1 RID: 1233
		ChargeComputers
	}
}
