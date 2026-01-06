using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
public class AnimatorTimeScaler : MonoBehaviour
{
	// Token: 0x06000018 RID: 24 RVA: 0x0000255E File Offset: 0x0000075E
	private void Start()
	{
		this.startingSpeed = this.animator.speed;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002574 File Offset: 0x00000774
	private void Update()
	{
		float num = 1f;
		if (this.timeType == AnimatorTimeScaler.TimeType.TimeManagerScaled)
		{
			num = TimeManager.instance.currentTimeScale;
		}
		else if (this.timeType == AnimatorTimeScaler.TimeType.TimeManagerLerp)
		{
			num = this.currentTimeLerp;
			this.currentTimeLerp = Mathf.Lerp(this.currentTimeLerp, TimeManager.instance.currentTimeScale, Time.deltaTime * this.speedOfLerp);
			if (Mathf.Abs(this.currentTimeLerp - TimeManager.instance.currentTimeScale) < 0.01f)
			{
				this.timeType = AnimatorTimeScaler.TimeType.TimeManagerScaled;
			}
		}
		else if (this.timeType == AnimatorTimeScaler.TimeType.Unscaled)
		{
			num = 1f;
		}
		this.animator.speed = num * this.startingSpeed;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000261B File Offset: 0x0000081B
	public void SetLerp()
	{
		this.currentTimeLerp = 1f;
		this.timeType = AnimatorTimeScaler.TimeType.TimeManagerLerp;
	}

	// Token: 0x0400000B RID: 11
	[SerializeField]
	public AnimatorTimeScaler.TimeType timeType = AnimatorTimeScaler.TimeType.TimeManagerLerp;

	// Token: 0x0400000C RID: 12
	[SerializeField]
	private Animator animator;

	// Token: 0x0400000D RID: 13
	private float startingSpeed = 1f;

	// Token: 0x0400000E RID: 14
	private float currentTimeLerp = 1f;

	// Token: 0x0400000F RID: 15
	[SerializeField]
	private float speedOfLerp = 10f;

	// Token: 0x020000B7 RID: 183
	[SerializeField]
	public enum TimeType
	{
		// Token: 0x040003B4 RID: 948
		TimeManagerScaled,
		// Token: 0x040003B5 RID: 949
		Unscaled,
		// Token: 0x040003B6 RID: 950
		TimeManagerLerp
	}
}
