using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000022 RID: 34
public class DamageDealer : MonoBehaviour
{
	// Token: 0x0600010A RID: 266 RVA: 0x000068A1 File Offset: 0x00004AA1
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!this.callTriggerEnter)
		{
			return;
		}
		this.ApplyEffects(other.gameObject);
	}

	// Token: 0x0600010B RID: 267 RVA: 0x000068B8 File Offset: 0x00004AB8
	public float SetDamage(float damage)
	{
		this.damage = damage;
		return this.damage;
	}

	// Token: 0x0600010C RID: 268 RVA: 0x000068C8 File Offset: 0x00004AC8
	public float GetDamage(Status status = null)
	{
		float num = this.damage + Random.Range(-1.5f, 1.5f);
		if (this.damage > 0f && num <= 1f)
		{
			num = 1f;
		}
		num *= ModifierManager.instance.GetModifierPercentage(Modifier.ModifierEffect.Type.BulletDamage);
		if (status && status.GetStatusEffect(Status.StatusEffect.Type.Freeze))
		{
			num *= ModifierManager.instance.GetModifierPercentage(Modifier.ModifierEffect.Type.FrozenEnemiesTakeExtraDamagePercent);
		}
		return num;
	}

	// Token: 0x0600010D RID: 269 RVA: 0x0000693C File Offset: 0x00004B3C
	public void ApplyEffects(GameObject obj)
	{
		if (this.forceApplier)
		{
			Enemy component = obj.GetComponent<Enemy>();
			this.forceApplier.ApplyForce(component);
		}
		Status component2 = obj.GetComponent<Status>();
		if (component2)
		{
			foreach (Status.StatusEffect statusEffect in this.statusEffectsToApply)
			{
				component2.ApplyStatusEffect(statusEffect);
			}
			if (this.modifierTypes.Contains(Modifier.ModifierEffect.Type.FreezingLightning) && ModifierManager.instance.GetModifierRandomRoll(Modifier.ModifierEffect.Type.FreezingLightning))
			{
				component2.ApplyStatusEffect(Status.StatusEffect.Type.Freeze, 1f);
			}
		}
		float num = this.GetDamage(component2);
		Destructible component3 = obj.GetComponent<Destructible>();
		if (component3 && !component3.isDestroyed && num > 0f)
		{
			component3.TakeDamage(num, default(Vector2), true);
			if (component3.isDestroyed)
			{
				this.ApplyKillEffects(component3.transform.position);
			}
		}
		if (this.modifierTypes.Contains(Modifier.ModifierEffect.Type.SalemStakes) && ModifierManager.instance.GetModifierRandomRoll(Modifier.ModifierEffect.Type.SalemStakes))
		{
			component2.ApplyStatusEffect(Status.StatusEffect.Type.Burn, 1f);
		}
	}

	// Token: 0x0600010E RID: 270 RVA: 0x00006A6C File Offset: 0x00004C6C
	private void ApplyKillEffects(Vector2 position)
	{
		if (this.modifierTypes.Contains(Modifier.ModifierEffect.Type.StakesCreateSplinters) && ModifierManager.instance.GetModifierRandomRoll(Modifier.ModifierEffect.Type.StakesCreateSplinters))
		{
			this.CreateSplinters(position);
		}
	}

	// Token: 0x0600010F RID: 271 RVA: 0x00006A90 File Offset: 0x00004C90
	private void CreateSplinters(Vector2 position)
	{
		for (int i = 0; i < 8; i++)
		{
			float num = (float)i * 3.1415927f / 4f;
			Vector2 vector = new Vector2(Mathf.Cos(num), Mathf.Sin(num));
			this.CreateSplinter(position, vector);
		}
	}

	// Token: 0x06000110 RID: 272 RVA: 0x00006AD3 File Offset: 0x00004CD3
	private void CreateSplinter(Vector2 position, Vector2 direction)
	{
		Object.Instantiate<GameObject>(this.splinterPrefab, position, Quaternion.identity).GetComponent<Bullet>().SetVelocity(direction);
	}

	// Token: 0x040000C9 RID: 201
	[SerializeField]
	private List<Status.StatusEffect> statusEffectsToApply;

	// Token: 0x040000CA RID: 202
	public List<Modifier.ModifierEffect.Type> modifierTypes = new List<Modifier.ModifierEffect.Type>();

	// Token: 0x040000CB RID: 203
	[SerializeField]
	public float damage = 3f;

	// Token: 0x040000CC RID: 204
	[SerializeField]
	private GameObject splinterPrefab;

	// Token: 0x040000CD RID: 205
	[SerializeField]
	private bool callTriggerEnter = true;

	// Token: 0x040000CE RID: 206
	[SerializeField]
	private ForceApplier forceApplier;
}
