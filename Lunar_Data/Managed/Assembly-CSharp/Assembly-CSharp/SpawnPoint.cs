using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000095 RID: 149
public class SpawnPoint : MonoBehaviour
{
	// Token: 0x06000404 RID: 1028 RVA: 0x00013FEF File Offset: 0x000121EF
	private void OnEnable()
	{
		SpawnPoint.spawnPoints.Add(this);
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x00013FFC File Offset: 0x000121FC
	private void OnDisable()
	{
		SpawnPoint.spawnPoints.Remove(this);
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x0001400C File Offset: 0x0001220C
	public static SpawnPoint GetClosestSpawnPoint(Vector2 pos)
	{
		return SpawnPoint.spawnPoints.OrderBy((SpawnPoint x) => Vector2.Distance(x.transform.position, pos)).First<SpawnPoint>();
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x00014044 File Offset: 0x00012244
	public static Vector2 GetRandomSpawnPoint(SpawnPoint.SpawnType type, Vector2 origin, float minimumDistance)
	{
		List<SpawnPoint> list = SpawnPoint.spawnPoints.Where((SpawnPoint x) => x.spawnType == type).ToList<SpawnPoint>();
		if (list.Count == 0)
		{
			type = SpawnPoint.SpawnType.Door;
			list = SpawnPoint.spawnPoints.Where((SpawnPoint x) => x.spawnType == type).ToList<SpawnPoint>();
		}
		list = list.OrderBy((SpawnPoint x) => Random.value).ToList<SpawnPoint>();
		foreach (SpawnPoint spawnPoint in list)
		{
			if (Vector2.Distance(spawnPoint.transform.position, origin) > minimumDistance)
			{
				return spawnPoint.transform.position + Random.insideUnitSphere * spawnPoint.radius;
			}
		}
		return Vector2.zero;
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x00014154 File Offset: 0x00012354
	public static void RemoveAllSpawnPoints()
	{
		SpawnPoint.spawnPoints.Clear();
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x00014160 File Offset: 0x00012360
	public static Vector2 GetRandomSpawnPoint(SpawnPoint.SpawnType type)
	{
		List<SpawnPoint> list = SpawnPoint.spawnPoints.Where((SpawnPoint x) => x.spawnType == type).ToList<SpawnPoint>();
		if (list.Count == 0)
		{
			type = SpawnPoint.SpawnType.Door;
			list = SpawnPoint.spawnPoints.Where((SpawnPoint x) => x.spawnType == type).ToList<SpawnPoint>();
		}
		if (list.Count == 0)
		{
			return Vector2.zero;
		}
		SpawnPoint spawnPoint = list[Random.Range(0, list.Count)];
		return spawnPoint.transform.position + Random.insideUnitSphere * spawnPoint.radius;
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x00014207 File Offset: 0x00012407
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(base.transform.position, this.radius);
	}

	// Token: 0x04000311 RID: 785
	public SpawnPoint.SpawnType spawnType;

	// Token: 0x04000312 RID: 786
	public static List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

	// Token: 0x04000313 RID: 787
	[SerializeField]
	private float radius = 1.5f;

	// Token: 0x02000118 RID: 280
	public enum SpawnType
	{
		// Token: 0x04000507 RID: 1287
		Door,
		// Token: 0x04000508 RID: 1288
		inRoom
	}
}
