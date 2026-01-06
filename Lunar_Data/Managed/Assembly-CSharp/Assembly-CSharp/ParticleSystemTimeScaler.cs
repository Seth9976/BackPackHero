using System;
using UnityEngine;

// Token: 0x02000062 RID: 98
public class ParticleSystemTimeScaler : MonoBehaviour
{
	// Token: 0x060002CF RID: 719 RVA: 0x0000E927 File Offset: 0x0000CB27
	private void Start()
	{
		this.main = this.particles.main;
		this.startingSpeed = this.main.simulationSpeed;
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x0000E94C File Offset: 0x0000CB4C
	private void Update()
	{
		float num = 1f;
		if (this.timeType == ParticleSystemTimeScaler.TimeType.TimeManagerScaled)
		{
			num = TimeManager.instance.currentTimeScale;
		}
		else if (this.timeType == ParticleSystemTimeScaler.TimeType.TimeManagerLerp)
		{
			num = this.currentTimeLerp;
			this.currentTimeLerp = Mathf.Lerp(this.currentTimeLerp, TimeManager.instance.currentTimeScale, Time.deltaTime * this.speedOfLerp);
			if (Mathf.Abs(this.currentTimeLerp - TimeManager.instance.currentTimeScale) < 0.01f)
			{
				this.timeType = ParticleSystemTimeScaler.TimeType.TimeManagerScaled;
			}
		}
		else if (this.timeType == ParticleSystemTimeScaler.TimeType.Unscaled)
		{
			num = 1f;
		}
		this.main.simulationSpeed = num * this.startingSpeed;
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x0000E9F3 File Offset: 0x0000CBF3
	public void SetLerp()
	{
		this.currentTimeLerp = 1f;
		this.timeType = ParticleSystemTimeScaler.TimeType.TimeManagerLerp;
	}

	// Token: 0x04000219 RID: 537
	[SerializeField]
	public ParticleSystemTimeScaler.TimeType timeType = ParticleSystemTimeScaler.TimeType.TimeManagerLerp;

	// Token: 0x0400021A RID: 538
	[SerializeField]
	private ParticleSystem particles;

	// Token: 0x0400021B RID: 539
	private ParticleSystem.MainModule main;

	// Token: 0x0400021C RID: 540
	private float startingSpeed = 1f;

	// Token: 0x0400021D RID: 541
	private float currentTimeLerp = 1f;

	// Token: 0x0400021E RID: 542
	[SerializeField]
	private float speedOfLerp = 10f;

	// Token: 0x020000EE RID: 238
	[SerializeField]
	public enum TimeType
	{
		// Token: 0x04000476 RID: 1142
		TimeManagerScaled,
		// Token: 0x04000477 RID: 1143
		Unscaled,
		// Token: 0x04000478 RID: 1144
		TimeManagerLerp
	}
}
