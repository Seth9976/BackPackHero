using System;

// Token: 0x020000EE RID: 238
public class SpecialItemRemoveStatusEffects : SpecialItem
{
	// Token: 0x06000860 RID: 2144 RVA: 0x0005742C File Offset: 0x0005562C
	public override void UseSpecialEffect(Status stat)
	{
		stat.AddStatusEffect(StatusEffect.Type.poison, -999f, Item2.Effect.MathematicalType.summative);
		stat.AddStatusEffect(StatusEffect.Type.freeze, -999f, Item2.Effect.MathematicalType.summative);
		stat.AddStatusEffect(StatusEffect.Type.regen, -999f, Item2.Effect.MathematicalType.summative);
		stat.AddStatusEffect(StatusEffect.Type.spikes, -999f, Item2.Effect.MathematicalType.summative);
		stat.AddStatusEffect(StatusEffect.Type.haste, -999f, Item2.Effect.MathematicalType.summative);
		stat.AddStatusEffect(StatusEffect.Type.slow, -999f, Item2.Effect.MathematicalType.summative);
		stat.AddStatusEffect(StatusEffect.Type.rage, -999f, Item2.Effect.MathematicalType.summative);
		stat.AddStatusEffect(StatusEffect.Type.weak, -999f, Item2.Effect.MathematicalType.summative);
		stat.AddStatusEffect(StatusEffect.Type.fire, -999f, Item2.Effect.MathematicalType.summative);
	}
}
