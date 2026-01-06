using System;

// Token: 0x020000EC RID: 236
public class SpecialItemDoublePoison : SpecialItem
{
	// Token: 0x0600085A RID: 2138 RVA: 0x00057348 File Offset: 0x00055548
	public override void UseSpecialEffect(Status stat)
	{
		int statusEffectValue = stat.GetStatusEffectValue(StatusEffect.Type.poison);
		if (statusEffectValue > 0)
		{
			stat.AddStatusEffect(StatusEffect.Type.poison, (float)statusEffectValue, Item2.Effect.MathematicalType.summative);
		}
	}
}
