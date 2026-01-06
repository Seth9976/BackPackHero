using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000AB RID: 171
public class WaveManager : MonoBehaviour
{
	// Token: 0x0600048B RID: 1163 RVA: 0x0001659F File Offset: 0x0001479F
	private void OnEnable()
	{
		if (WaveManager.instance == null)
		{
			WaveManager.instance = this;
		}
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x000165B4 File Offset: 0x000147B4
	private void OnDisable()
	{
		if (WaveManager.instance == this)
		{
			WaveManager.instance = null;
		}
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x000165C9 File Offset: 0x000147C9
	private void Start()
	{
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x000165CC File Offset: 0x000147CC
	private void Update()
	{
		if (!Player.instance)
		{
			return;
		}
		if (Player.instance.isDead || HordeRemainingDisplay.instance.wonLevel || Player.instance.isWinning || OperatorPanel.instance)
		{
			return;
		}
		this.totalTime += Time.deltaTime * TimeManager.instance.currentTimeScale * RunTypeManager.instance.GetRunTypeModifierPercentage(RunType.RunProperty.RunPropertyType.FasterWaveManagerTimer);
		if (!this.currentWave)
		{
			return;
		}
		if (this.totalTime > this.GetDelayOfWave(this.currentWave.enemyWaveSpawns[this.currentWaveIndex]))
		{
			this.SpawnEnemyWave(this.currentWave.enemyWaveSpawns[this.currentWaveIndex], SpawnPoint.SpawnType.Door);
			if (this.currentWaveIndex + 1 < this.currentWave.enemyWaveSpawns.Count<EnemyWave.WavesSpawn>())
			{
				this.currentWaveIndex++;
				return;
			}
			this.totalTime -= this.currentWave.enemyWaveSpawns[this.currentWaveIndex].delay;
		}
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x000166E0 File Offset: 0x000148E0
	public float GetDelayOfWave(EnemyWave.WavesSpawn wave)
	{
		float num = 0.1f;
		foreach (EnemyWave.WavesSpawn wavesSpawn in this.currentWave.enemyWaveSpawns)
		{
			num += wavesSpawn.delay;
			if (wavesSpawn == wave)
			{
				break;
			}
		}
		return num;
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x00016748 File Offset: 0x00014948
	private void SpawnEnemyWave(EnemyWave.WavesSpawn wave, SpawnPoint.SpawnType spawnType = SpawnPoint.SpawnType.Door)
	{
		foreach (EnemyWave.EnemySpawn enemySpawn in wave.enemies)
		{
			for (int i = 0; i < enemySpawn.numberToSpawn; i++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(enemySpawn.enemyPrefab, SpawnPoint.GetRandomSpawnPoint(spawnType), Quaternion.identity);
				gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1f);
			}
		}
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x000167F8 File Offset: 0x000149F8
	public void SetCurrentWave(EnemyWave wave)
	{
		this.isRunning = false;
		this.currentWave = wave;
		this.currentWaveIndex = 0;
		this.totalTime = 0f;
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x0001681A File Offset: 0x00014A1A
	public void StartWave()
	{
		if (this.isRunning)
		{
			return;
		}
		this.isRunning = true;
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x0001682C File Offset: 0x00014A2C
	public void EndWave()
	{
		this.isRunning = false;
	}

	// Token: 0x04000387 RID: 903
	public static WaveManager instance;

	// Token: 0x04000388 RID: 904
	[SerializeField]
	public EnemyWave currentWave;

	// Token: 0x04000389 RID: 905
	private int currentWaveIndex;

	// Token: 0x0400038A RID: 906
	private float totalTime;

	// Token: 0x0400038B RID: 907
	private bool isRunning;

	// Token: 0x02000125 RID: 293
	[Serializable]
	private class WavesForLevel
	{
		// Token: 0x0400052A RID: 1322
		public int floorNumber;

		// Token: 0x0400052B RID: 1323
		public List<EnemyWave> enemyWaves;
	}
}
