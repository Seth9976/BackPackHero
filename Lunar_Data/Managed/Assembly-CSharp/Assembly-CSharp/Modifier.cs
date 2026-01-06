using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200005D RID: 93
public class Modifier : MonoBehaviour
{
	// Token: 0x060002AB RID: 683 RVA: 0x0000DAAC File Offset: 0x0000BCAC
	private void Start()
	{
		ModifierManager.instance.modifiers.Add(this);
	}

	// Token: 0x060002AC RID: 684 RVA: 0x0000DABE File Offset: 0x0000BCBE
	private void OnDestroy()
	{
		if (!ModifierManager.instance)
		{
			return;
		}
		ModifierManager.instance.modifiers.Remove(this);
	}

	// Token: 0x040001FA RID: 506
	[SerializeField]
	public List<Modifier.ModifierEffect> effects = new List<Modifier.ModifierEffect>();

	// Token: 0x020000EC RID: 236
	[Serializable]
	public class ModifierEffect
	{
		// Token: 0x0400046A RID: 1130
		public Modifier.ModifierEffect.Type type;

		// Token: 0x0400046B RID: 1131
		public float value;

		// Token: 0x0200012B RID: 299
		public enum Type
		{
			// Token: 0x0400054C RID: 1356
			BulletDamage,
			// Token: 0x0400054D RID: 1357
			Boomerang,
			// Token: 0x0400054E RID: 1358
			SpeedPercentBoostOnFire,
			// Token: 0x0400054F RID: 1359
			EnemiesThatDieByFireExplodePercent,
			// Token: 0x04000550 RID: 1360
			NumberOfStakes,
			// Token: 0x04000551 RID: 1361
			StakesCreateSplinters,
			// Token: 0x04000552 RID: 1362
			PenetrationAsymptote,
			// Token: 0x04000553 RID: 1363
			StakesBounce,
			// Token: 0x04000554 RID: 1364
			GarlicCosts0,
			// Token: 0x04000555 RID: 1365
			NextCardCosts0,
			// Token: 0x04000556 RID: 1366
			SalemStakes,
			// Token: 0x04000557 RID: 1367
			TossablesIncreasedArea,
			// Token: 0x04000558 RID: 1368
			PoisonEnemiesHaveAChanceToSpawnLocusts,
			// Token: 0x04000559 RID: 1369
			FroznEnemiesHaveAChanceToExplodeIntoIce,
			// Token: 0x0400055A RID: 1370
			FrozenEnemiesTakeExtraDamagePercent,
			// Token: 0x0400055B RID: 1371
			GainEnergyFasterWhenStill,
			// Token: 0x0400055C RID: 1372
			FreezingLightning,
			// Token: 0x0400055D RID: 1373
			ChangeTimeManagerTimeRate
		}
	}
}
