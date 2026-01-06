using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200005E RID: 94
public class ModifierManager : MonoBehaviour
{
	// Token: 0x060002AE RID: 686 RVA: 0x0000DAF1 File Offset: 0x0000BCF1
	private void OnEnable()
	{
		if (ModifierManager.instance == null)
		{
			ModifierManager.instance = this;
		}
	}

	// Token: 0x060002AF RID: 687 RVA: 0x0000DB06 File Offset: 0x0000BD06
	private void OnDisable()
	{
		if (ModifierManager.instance == this)
		{
			ModifierManager.instance = null;
		}
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x0000DB1C File Offset: 0x0000BD1C
	public float GetAsymptoticModifierValue(Modifier.ModifierEffect.Type modifierEffectType)
	{
		float num = 0f;
		int num2 = 0;
		foreach (Modifier modifier in this.modifiers)
		{
			using (List<Modifier.ModifierEffect>.Enumerator enumerator2 = modifier.effects.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.type == modifierEffectType)
					{
						num2++;
					}
				}
			}
		}
		for (int i = 0; i < num2; i++)
		{
			num += Mathf.Pow(0.5f, (float)(i + 1));
		}
		return num;
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
	public bool GetModifierRandomRoll(Modifier.ModifierEffect.Type modifierEffectType)
	{
		foreach (Modifier modifier in this.modifiers)
		{
			foreach (Modifier.ModifierEffect modifierEffect in modifier.effects)
			{
				if (modifierEffect.type == modifierEffectType && Random.value < modifierEffect.value)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x0000DC7C File Offset: 0x0000BE7C
	public float GetModifierPercentage(Modifier.ModifierEffect.Type modifierEffectType)
	{
		float num = 1f;
		foreach (Modifier modifier in this.modifiers)
		{
			foreach (Modifier.ModifierEffect modifierEffect in modifier.effects)
			{
				if (modifierEffect.type == modifierEffectType)
				{
					num += modifierEffect.value;
				}
			}
		}
		return num;
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x0000DD1C File Offset: 0x0000BF1C
	public float GetModifierValue(Modifier.ModifierEffect.Type modifierEffectType)
	{
		float num = 0f;
		foreach (Modifier modifier in this.modifiers)
		{
			foreach (Modifier.ModifierEffect modifierEffect in modifier.effects)
			{
				if (modifierEffect.type == modifierEffectType)
				{
					num += modifierEffect.value;
				}
			}
		}
		return num;
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x0000DDBC File Offset: 0x0000BFBC
	public bool GetModifierExists(Modifier.ModifierEffect.Type modifierEffectType)
	{
		foreach (Modifier modifier in this.modifiers)
		{
			using (List<Modifier.ModifierEffect>.Enumerator enumerator2 = modifier.effects.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.type == modifierEffectType)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x040001FB RID: 507
	public static ModifierManager instance;

	// Token: 0x040001FC RID: 508
	[SerializeField]
	public List<Modifier> modifiers = new List<Modifier>();
}
