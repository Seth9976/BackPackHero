using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000199 RID: 409
public class specialItemPochette : SpecialItem
{
	// Token: 0x0600106C RID: 4204 RVA: 0x0009D138 File Offset: 0x0009B338
	public override IEnumerator ApplySpecialEffect(List<Enemy> effectedEnemies)
	{
		foreach (CombatPet combatPet in CombatPet.GetLivePets())
		{
			Item2 item = combatPet.petItem2.myItem;
			Item2.EffectTotal effectTotal = new Item2.EffectTotal();
			effectTotal.effect = this.effect.Clone();
			effectTotal.effectPieces = new List<Item2.EffectTotal.EffectPiece>();
			effectTotal.trigger = this.trigger;
			effectTotal.multiplier = 1f;
			foreach (Item2.Modifier modifier in item.appliedModifiers)
			{
				foreach (Item2.Effect effect in modifier.effects)
				{
					item.ApplyToEffectTotal(effectTotal, effect, modifier, Item2.Effect.MathematicalType.summative);
				}
			}
			foreach (Item2.Modifier modifier2 in item.appliedModifiers)
			{
				foreach (Item2.Effect effect2 in modifier2.effects)
				{
					item.ApplyToEffectTotal(effectTotal, effect2, modifier2, Item2.Effect.MathematicalType.multiplicative);
				}
			}
			item.CalculateEffectTotal(effectTotal);
			GameFlowManager.main.ConsiderPetAnimation(item);
			yield return item.ApplyEffect(effectTotal, effectedEnemies, null);
			item.RemoveModifiers(new List<Item2.Modifier.Length> { Item2.Modifier.Length.untilUse }, -1);
			yield return new WaitForSeconds(0.25f);
			item = null;
		}
		List<CombatPet>.Enumerator enumerator = default(List<CombatPet>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x04000D60 RID: 3424
	[SerializeField]
	private Item2.Effect effect;
}
