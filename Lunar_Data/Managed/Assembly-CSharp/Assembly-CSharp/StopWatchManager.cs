using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000098 RID: 152
public class StopWatchManager : MonoBehaviour
{
	// Token: 0x0600041E RID: 1054 RVA: 0x00014BA1 File Offset: 0x00012DA1
	private void Start()
	{
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x00014BA3 File Offset: 0x00012DA3
	private void ResetStopWatch()
	{
		this.currentFill = 0f;
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x00014BB0 File Offset: 0x00012DB0
	private void Update()
	{
		this.currentFill += Time.deltaTime * TimeManager.instance.currentTimeScale;
		this.stopWatchForeground.fillAmount = this.currentFill / this.necessaryFill;
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x00014BE8 File Offset: 0x00012DE8
	public void StartStopWatch()
	{
		if (this.currentFill < this.necessaryFill)
		{
			SoundManager.instance.PlaySFX("noBlip", double.PositiveInfinity);
			return;
		}
		this.ResetStopWatch();
		PassiveManager.instance.CreatePassiveEffectFromPrefab(this.stopWatchPassiveEffect, base.GetComponentInChildren<Image>().sprite, "Stopwatch", 3f, null, null);
	}

	// Token: 0x0400032C RID: 812
	[SerializeField]
	private GameObject stopWatchPassiveEffect;

	// Token: 0x0400032D RID: 813
	[SerializeField]
	private Image stopWatchForeground;

	// Token: 0x0400032E RID: 814
	[SerializeField]
	private float necessaryFill = 3f;

	// Token: 0x0400032F RID: 815
	[SerializeField]
	private float currentFill;
}
