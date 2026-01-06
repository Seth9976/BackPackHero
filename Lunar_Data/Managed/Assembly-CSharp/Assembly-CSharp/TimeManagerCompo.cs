using System;
using UnityEngine;

// Token: 0x0200009B RID: 155
[Serializable]
public class TimeManagerCompo
{
	// Token: 0x0600042E RID: 1070 RVA: 0x00014E32 File Offset: 0x00013032
	public void Update(TimeManagerCompo.TimeScaleType timeScaleType)
	{
		if (this.timeType != TimeManagerCompo.TimeType.temporaryUnscaled)
		{
			return;
		}
		if (timeScaleType == TimeManagerCompo.TimeScaleType.deltaTime)
		{
			this.UpdateDeltaTime();
			return;
		}
		if (timeScaleType == TimeManagerCompo.TimeScaleType.fixedDeltaTime)
		{
			this.UpdateFixedDeltaTime();
		}
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x00014E54 File Offset: 0x00013054
	private void UpdateDeltaTime()
	{
		this.currentTimeLerp = Mathf.Lerp(this.currentTimeLerp, TimeManager.instance.currentTimeScale, Time.deltaTime * 10f);
		if (Mathf.Abs(this.currentTimeLerp - TimeManager.instance.currentTimeScale) < 0.01f)
		{
			this.timeType = TimeManagerCompo.TimeType.TimeManagerScaled;
		}
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x00014EAC File Offset: 0x000130AC
	private void UpdateFixedDeltaTime()
	{
		this.currentTimeLerp = Mathf.Lerp(this.currentTimeLerp, TimeManager.instance.currentTimeScale, Time.fixedDeltaTime * 10f);
		if (Mathf.Abs(this.currentTimeLerp - TimeManager.instance.currentTimeScale) < 0.01f)
		{
			this.timeType = TimeManagerCompo.TimeType.TimeManagerScaled;
		}
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x00014F04 File Offset: 0x00013104
	public float GetTimeScale()
	{
		switch (this.timeType)
		{
		case TimeManagerCompo.TimeType.TimeManagerScaled:
			return TimeManager.instance.currentTimeScale;
		case TimeManagerCompo.TimeType.TimeManagerUnscaled:
			return 1f;
		case TimeManagerCompo.TimeType.temporaryUnscaled:
			return this.currentTimeLerp;
		default:
			return 1f;
		}
	}

	// Token: 0x04000336 RID: 822
	[SerializeField]
	private TimeManagerCompo.TimeType timeType;

	// Token: 0x04000337 RID: 823
	private float currentTimeLerp = 1f;

	// Token: 0x0200011E RID: 286
	public enum TimeType
	{
		// Token: 0x04000513 RID: 1299
		TimeManagerScaled,
		// Token: 0x04000514 RID: 1300
		TimeManagerUnscaled,
		// Token: 0x04000515 RID: 1301
		temporaryUnscaled
	}

	// Token: 0x0200011F RID: 287
	public enum TimeScaleType
	{
		// Token: 0x04000517 RID: 1303
		deltaTime,
		// Token: 0x04000518 RID: 1304
		fixedDeltaTime
	}
}
