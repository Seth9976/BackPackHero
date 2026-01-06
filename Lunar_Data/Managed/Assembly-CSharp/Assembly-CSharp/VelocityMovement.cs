using System;
using UnityEngine;

// Token: 0x020000AA RID: 170
public class VelocityMovement : MonoBehaviour
{
	// Token: 0x06000486 RID: 1158 RVA: 0x00016426 File Offset: 0x00014626
	private void Start()
	{
		if (this.dustParticles)
		{
			this.dustEmission = this.dustParticles.emission;
		}
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x00016448 File Offset: 0x00014648
	private void Update()
	{
		if (this.dustParticles)
		{
			this.dustEmission.rateOverDistance = Mathf.Max(0f, this.storedVelocity.magnitude);
		}
		float num = 1f;
		if (this.timeType == VelocityMovement.TimeType.TimeManagerScaled)
		{
			num = TimeManager.instance.currentTimeScale;
		}
		else if (this.timeType == VelocityMovement.TimeType.TimeManagerLerp)
		{
			num = this.currentTimeLerp;
			this.currentTimeLerp = Mathf.Lerp(this.currentTimeLerp, TimeManager.instance.currentTimeScale, Time.deltaTime * 10f);
			if (Mathf.Abs(this.currentTimeLerp - TimeManager.instance.currentTimeScale) < 0.01f)
			{
				this.timeType = VelocityMovement.TimeType.TimeManagerScaled;
			}
		}
		else if (this.timeType == VelocityMovement.TimeType.Unscaled)
		{
			num = 1f;
		}
		this.storedVelocity = Vector2.MoveTowards(this.storedVelocity, Vector2.zero, Time.deltaTime * 10f * num);
		this.rb.velocity = this.movementVelocity + this.storedVelocity * num;
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x00016552 File Offset: 0x00014752
	public void AddStoredVelocity(Vector2 velocity)
	{
		this.currentTimeLerp = 1f;
		this.timeType = VelocityMovement.TimeType.TimeManagerLerp;
		this.dustScaler.SetLerp();
		this.storedVelocity += velocity;
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x00016583 File Offset: 0x00014783
	public void SetMovementVelocity(Vector2 velocity)
	{
		this.movementVelocity = velocity;
	}

	// Token: 0x0400037F RID: 895
	[SerializeField]
	private VelocityMovement.TimeType timeType;

	// Token: 0x04000380 RID: 896
	private float currentTimeLerp = 1f;

	// Token: 0x04000381 RID: 897
	[SerializeField]
	private ParticleSystem dustParticles;

	// Token: 0x04000382 RID: 898
	[SerializeField]
	private ParticleSystemTimeScaler dustScaler;

	// Token: 0x04000383 RID: 899
	private ParticleSystem.EmissionModule dustEmission;

	// Token: 0x04000384 RID: 900
	[SerializeField]
	private Rigidbody2D rb;

	// Token: 0x04000385 RID: 901
	[SerializeField]
	private Vector2 movementVelocity;

	// Token: 0x04000386 RID: 902
	[SerializeField]
	private Vector2 storedVelocity;

	// Token: 0x02000124 RID: 292
	[SerializeField]
	private enum TimeType
	{
		// Token: 0x04000527 RID: 1319
		TimeManagerScaled,
		// Token: 0x04000528 RID: 1320
		Unscaled,
		// Token: 0x04000529 RID: 1321
		TimeManagerLerp
	}
}
