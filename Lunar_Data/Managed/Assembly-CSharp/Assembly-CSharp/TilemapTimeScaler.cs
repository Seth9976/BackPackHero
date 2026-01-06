using System;
using UnityEngine;
using UnityEngine.Tilemaps;

// Token: 0x02000099 RID: 153
public class TilemapTimeScaler : MonoBehaviour
{
	// Token: 0x06000423 RID: 1059 RVA: 0x00014C5C File Offset: 0x00012E5C
	private void Start()
	{
		this.startingFrameRate = this.tilemap.animationFrameRate;
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x00014C6F File Offset: 0x00012E6F
	private void Update()
	{
		this.tilemap.animationFrameRate = this.startingFrameRate * TimeManager.instance.currentTimeScale;
	}

	// Token: 0x04000330 RID: 816
	[SerializeField]
	private Tilemap tilemap;

	// Token: 0x04000331 RID: 817
	private float startingFrameRate;
}
