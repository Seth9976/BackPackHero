using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000033 RID: 51
[CreateAssetMenu(fileName = "EnemyWaveSpawns", menuName = "ScriptableObjects/EnemyWave", order = 1)]
public class EnemyWave : ScriptableObject
{
	// Token: 0x06000192 RID: 402 RVA: 0x000090B8 File Offset: 0x000072B8
	public List<GameObject> GetAllDistinctEnemyTypes()
	{
		List<GameObject> list = new List<GameObject>();
		foreach (EnemyWave.WavesSpawn wavesSpawn in this.enemyWaveSpawns)
		{
			foreach (EnemyWave.EnemySpawn enemySpawn in wavesSpawn.enemies)
			{
				if (!list.Contains(enemySpawn.enemyPrefab))
				{
					list.Add(enemySpawn.enemyPrefab);
				}
			}
		}
		return list;
	}

	// Token: 0x04000142 RID: 322
	[SerializeField]
	public List<EnemyWave.WavesSpawn> enemyWaveSpawns = new List<EnemyWave.WavesSpawn>();

	// Token: 0x020000D7 RID: 215
	[Serializable]
	public class WavesSpawn
	{
		// Token: 0x04000429 RID: 1065
		public List<EnemyWave.EnemySpawn> enemies = new List<EnemyWave.EnemySpawn>();

		// Token: 0x0400042A RID: 1066
		[HideInInspector]
		public float delay;
	}

	// Token: 0x020000D8 RID: 216
	[Serializable]
	public class EnemySpawn
	{
		// Token: 0x0400042B RID: 1067
		public GameObject enemyPrefab;

		// Token: 0x0400042C RID: 1068
		public int numberToSpawn;
	}
}
