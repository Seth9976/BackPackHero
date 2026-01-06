using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003F RID: 63
public class Explosion : MonoBehaviour
{
	// Token: 0x060001CD RID: 461 RVA: 0x00009D0F File Offset: 0x00007F0F
	private void OnEnable()
	{
		this.SetParticles();
	}

	// Token: 0x060001CE RID: 462 RVA: 0x00009D18 File Offset: 0x00007F18
	private void Start()
	{
		foreach (Enemy enemy in Enemy.GetEnemiesInRadius(base.transform.position, this.GetRangeOfEffect()))
		{
			this.damageDealer.ApplyEffects(enemy.gameObject);
		}
		this.CreateOtherPrefabsInRadius();
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060001CF RID: 463 RVA: 0x00009D9C File Offset: 0x00007F9C
	private float GetRangeOfEffect()
	{
		float num = this.explosionRadius;
		if (this.modifierTypes.Contains(Modifier.ModifierEffect.Type.TossablesIncreasedArea))
		{
			num += ModifierManager.instance.GetModifierValue(Modifier.ModifierEffect.Type.TossablesIncreasedArea);
		}
		return num;
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x00009DD0 File Offset: 0x00007FD0
	private void CreateOtherPrefabsInRadius()
	{
		foreach (Explosion.PrefabsToSpawnInArea prefabsToSpawnInArea in this.prefabsToSpawnInArea)
		{
			for (float num = -this.explosionRadius; num < this.explosionRadius; num += prefabsToSpawnInArea.radiusToSpawn)
			{
				for (float num2 = -this.explosionRadius; num2 < this.explosionRadius; num2 += prefabsToSpawnInArea.radiusToSpawn)
				{
					Vector2 vector = new Vector2(base.transform.position.x + num, base.transform.position.y + num2);
					if (Vector2.Distance(vector, base.transform.position) < this.explosionRadius)
					{
						Object.Instantiate<GameObject>(prefabsToSpawnInArea.prefab, vector, Quaternion.identity);
					}
				}
			}
		}
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x00009EC4 File Offset: 0x000080C4
	private void SetParticles()
	{
		this.explosionParticles.Clear();
		if (this.setParticleSize)
		{
			this.explosionParticles.shape.radius = this.GetRangeOfEffect();
			ParticleSystem.EmissionModule emission = this.explosionParticles.emission;
			emission.SetBurst(0, new ParticleSystem.Burst(0f, (short)(this.GetRangeOfEffect() * emission.GetBurst(0).count.constant * 1f)));
		}
		this.explosionParticles.transform.SetParent(null);
		this.explosionParticles.Stop();
		this.explosionParticles.Play();
	}

	// Token: 0x04000165 RID: 357
	[SerializeField]
	private List<Explosion.PrefabsToSpawnInArea> prefabsToSpawnInArea = new List<Explosion.PrefabsToSpawnInArea>();

	// Token: 0x04000166 RID: 358
	[SerializeField]
	private DamageDealer damageDealer;

	// Token: 0x04000167 RID: 359
	[SerializeField]
	private float explosionRadius = 5f;

	// Token: 0x04000168 RID: 360
	[SerializeField]
	private ParticleSystem explosionParticles;

	// Token: 0x04000169 RID: 361
	[SerializeField]
	private bool setParticleSize;

	// Token: 0x0400016A RID: 362
	[SerializeField]
	private List<Modifier.ModifierEffect.Type> modifierTypes = new List<Modifier.ModifierEffect.Type>();

	// Token: 0x020000DB RID: 219
	[Serializable]
	private class PrefabsToSpawnInArea
	{
		// Token: 0x04000436 RID: 1078
		public GameObject prefab;

		// Token: 0x04000437 RID: 1079
		public float radiusToSpawn;
	}
}
