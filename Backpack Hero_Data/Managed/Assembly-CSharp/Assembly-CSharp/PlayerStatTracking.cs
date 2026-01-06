using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200018D RID: 397
public class PlayerStatTracking : MonoBehaviour
{
	// Token: 0x17000032 RID: 50
	// (get) Token: 0x0600100A RID: 4106 RVA: 0x0009B46B File Offset: 0x0009966B
	public static PlayerStatTracking main
	{
		get
		{
			return PlayerStatTracking._instance;
		}
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x0009B472 File Offset: 0x00099672
	private void Awake()
	{
		if (PlayerStatTracking._instance != null && PlayerStatTracking._instance != this)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		PlayerStatTracking._instance = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x0009B4AB File Offset: 0x000996AB
	private void Start()
	{
		this.player = Player.main;
		this.gameManager = GameManager.main;
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x0009B4C4 File Offset: 0x000996C4
	private void Update()
	{
		DateTime.Now.ToString();
	}

	// Token: 0x0600100E RID: 4110 RVA: 0x0009B4E0 File Offset: 0x000996E0
	public void SetStat(string x, int y)
	{
		foreach (PlayerStatTracking.Stat stat in this.stats)
		{
			if (stat.name.ToLower() == x.ToLower())
			{
				stat.value = y;
				return;
			}
		}
		PlayerStatTracking.Stat stat2 = new PlayerStatTracking.Stat();
		stat2.name = x;
		stat2.value = y;
		this.stats.Add(stat2);
	}

	// Token: 0x0600100F RID: 4111 RVA: 0x0009B570 File Offset: 0x00099770
	public void AddStat(string x, int y)
	{
		foreach (PlayerStatTracking.Stat stat in this.stats)
		{
			if (stat.name.ToLower() == x.ToLower())
			{
				stat.value += y;
				return;
			}
		}
		PlayerStatTracking.Stat stat2 = new PlayerStatTracking.Stat();
		stat2.name = x;
		stat2.value = y;
		this.stats.Add(stat2);
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x0009B604 File Offset: 0x00099804
	public void SaveAllStats()
	{
	}

	// Token: 0x06001011 RID: 4113 RVA: 0x0009B606 File Offset: 0x00099806
	public void LoadAllStats()
	{
	}

	// Token: 0x04000D2C RID: 3372
	public List<string> gamesSavedandLoaded = new List<string>();

	// Token: 0x04000D2D RID: 3373
	public List<PlayerStatTracking.Stat> stats = new List<PlayerStatTracking.Stat>();

	// Token: 0x04000D2E RID: 3374
	private GameManager gameManager;

	// Token: 0x04000D2F RID: 3375
	private Player player;

	// Token: 0x04000D30 RID: 3376
	private static PlayerStatTracking _instance;

	// Token: 0x02000468 RID: 1128
	[Serializable]
	public class Stat
	{
		// Token: 0x04001A19 RID: 6681
		public string name;

		// Token: 0x04001A1A RID: 6682
		public int value;
	}
}
