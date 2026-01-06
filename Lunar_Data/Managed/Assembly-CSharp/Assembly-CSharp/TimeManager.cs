using System;
using UnityEngine;

// Token: 0x0200009A RID: 154
public class TimeManager : MonoBehaviour
{
	// Token: 0x1700000E RID: 14
	// (get) Token: 0x06000426 RID: 1062 RVA: 0x00014C95 File Offset: 0x00012E95
	public float currentTimeScale
	{
		get
		{
			return this.GetTimeScale();
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x06000427 RID: 1063 RVA: 0x00014C9D File Offset: 0x00012E9D
	public float currentUnscaledTimeScale
	{
		get
		{
			return this.GetUnScaledTimeScale();
		}
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x00014CA5 File Offset: 0x00012EA5
	private void OnEnable()
	{
		if (TimeManager.instance == null)
		{
			TimeManager.instance = this;
		}
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x00014CBA File Offset: 0x00012EBA
	private void OnDisable()
	{
		if (TimeManager.instance == this)
		{
			TimeManager.instance = null;
		}
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x00014CCF File Offset: 0x00012ECF
	private float GetTimeScale()
	{
		return this.currentTimeScaleInternal * Mathf.Max(0.01f, this.currentModifierTimeScale);
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x00014CE8 File Offset: 0x00012EE8
	private float GetUnScaledTimeScale()
	{
		return this.currentTimeScaleInternal;
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x00014CF0 File Offset: 0x00012EF0
	private void Update()
	{
		if (!Player.instance)
		{
			this.currentTimeScaleInternal = 1f;
			this.setTimeScale = 1f;
			return;
		}
		this.currentModifierTimeScale = ModifierManager.instance.GetModifierPercentage(Modifier.ModifierEffect.Type.ChangeTimeManagerTimeRate);
		if (Player.instance.isDead || Player.instance.isWinning)
		{
			this.setTimeScale = 1f;
			this.currentModifierTimeScale = 1f;
		}
		if (this.currentTimeScaleInternal != this.setTimeScale)
		{
			this.currentTimeScaleInternal = Mathf.Lerp(this.currentTimeScaleInternal, this.setTimeScale, Time.deltaTime * 10f);
			if (Mathf.Abs(this.currentTimeScaleInternal - this.setTimeScale) < 0.01f)
			{
				this.currentTimeScaleInternal = this.setTimeScale;
			}
		}
		float num;
		if (!Player.instance || Player.instance.isDead || Player.instance.isWinning)
		{
			num = 0f;
		}
		else
		{
			num = Mathf.Min(1f, this.setTimeScale);
		}
		if (SoundManager.instance)
		{
			SoundManager.instance.SetVolumeExtra(num);
		}
	}

	// Token: 0x04000332 RID: 818
	public static TimeManager instance;

	// Token: 0x04000333 RID: 819
	private float currentTimeScaleInternal = 1f;

	// Token: 0x04000334 RID: 820
	public float setTimeScale = 1f;

	// Token: 0x04000335 RID: 821
	private float currentModifierTimeScale = 1f;
}
