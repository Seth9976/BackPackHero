using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000043 RID: 67
public class ForceApplier : MonoBehaviour
{
	// Token: 0x060001DF RID: 479 RVA: 0x0000A239 File Offset: 0x00008439
	public void Start()
	{
		if (this.whenToApply.Contains(ForceApplier.WhenToApply.OnStart))
		{
			this.CreateForce();
		}
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x0000A250 File Offset: 0x00008450
	public void CreateForce()
	{
		foreach (Enemy enemy in Enemy.GetEnemiesInRadius(base.transform.position, this.GetRangeOfEffect()))
		{
			this.ApplyForce(enemy);
		}
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x0000A2B8 File Offset: 0x000084B8
	private float GetRangeOfEffect()
	{
		float num = this.rangeOfEffect;
		if (this.modifierTypes.Contains(Modifier.ModifierEffect.Type.TossablesIncreasedArea))
		{
			num += ModifierManager.instance.GetModifierValue(Modifier.ModifierEffect.Type.TossablesIncreasedArea);
		}
		return num;
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x0000A2EC File Offset: 0x000084EC
	public void ApplyForce(Enemy enemy)
	{
		if (!enemy)
		{
			return;
		}
		VelocityMovement component = enemy.GetComponent<VelocityMovement>();
		if (!component)
		{
			return;
		}
		if (this.forceType == ForceApplier.ForceType.PushFromSelf)
		{
			Vector2 vector = (enemy.transform.position - base.transform.position).normalized * this.power;
			component.AddStoredVelocity(vector);
			return;
		}
		if (this.forceType == ForceApplier.ForceType.PushFromPlayer)
		{
			component.AddStoredVelocity((enemy.transform.position - Player.instance.transform.position).normalized * this.power);
		}
	}

	// Token: 0x04000172 RID: 370
	[SerializeField]
	private List<ForceApplier.WhenToApply> whenToApply;

	// Token: 0x04000173 RID: 371
	[SerializeField]
	public ForceApplier.ForceType forceType;

	// Token: 0x04000174 RID: 372
	[SerializeField]
	public float rangeOfEffect = 1f;

	// Token: 0x04000175 RID: 373
	[SerializeField]
	public float power = 10f;

	// Token: 0x04000176 RID: 374
	[SerializeField]
	public List<Modifier.ModifierEffect.Type> modifierTypes = new List<Modifier.ModifierEffect.Type>();

	// Token: 0x020000DC RID: 220
	public enum WhenToApply
	{
		// Token: 0x04000439 RID: 1081
		OnStart
	}

	// Token: 0x020000DD RID: 221
	public enum ForceType
	{
		// Token: 0x0400043B RID: 1083
		PushFromSelf,
		// Token: 0x0400043C RID: 1084
		PushFromPlayer
	}
}
