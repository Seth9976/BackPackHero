using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200011E RID: 286
public class Status : MonoBehaviour
{
	// Token: 0x060009AB RID: 2475 RVA: 0x00062470 File Offset: 0x00060670
	public void RemoveAllPowers()
	{
		for (int i = 0; i < this.statusEffects.transform.childCount; i++)
		{
			StatusEffect component = this.statusEffects.transform.GetChild(i).GetComponent<StatusEffect>();
			if (component && !StatusEffect.CanBeCleansed(component.type))
			{
				component.RemoveStatusEffect();
			}
		}
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x000624CA File Offset: 0x000606CA
	public void ResetForNewConsiderationSequence()
	{
		this.hasReactedToOnTakeDamage = false;
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x000624D3 File Offset: 0x000606D3
	private void OnEnable()
	{
		if (Status.allStatuses.Contains(this))
		{
			return;
		}
		Status.allStatuses.Add(this);
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x000624EE File Offset: 0x000606EE
	private void OnDisable()
	{
		Status.allStatuses.Remove(this);
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x000624FC File Offset: 0x000606FC
	private void Start()
	{
		this.gameManager = GameManager.main;
		this.gameFlowManager = GameFlowManager.main;
		this.playerScript = this.parent.GetComponent<Player>();
		this.enemyScript = this.parent.GetComponent<Enemy>();
		this.combatPet = this.parent.GetComponent<CombatPet>();
		if (!this.enemyScript && !this.combatPet)
		{
			this.maxHealth = this.health;
			this.maxHealthBeforeItems = this.maxHealth;
		}
		base.transform.SetParent(GameObject.FindGameObjectWithTag("PrimaryCanvas").GetComponent<Canvas>().transform);
		base.transform.SetAsFirstSibling();
		base.transform.localScale = Vector3.one;
		this.SetHealth(this.health);
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x000625CC File Offset: 0x000607CC
	public void EndCombat()
	{
		foreach (object obj in this.statusEffects)
		{
			Transform transform = (Transform)obj;
			StatusEffect component = transform.GetComponent<StatusEffect>();
			if (component && component.decreasesTimeType != StatusEffect.DecreasesTime.remainsEvenAfterCombat)
			{
				Object.Destroy(transform.gameObject);
			}
		}
		if (this.armor > 0)
		{
			this.armor = 0;
			this.ChangeArmor(0f, Item2.Effect.MathematicalType.summative);
		}
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x00062660 File Offset: 0x00060860
	public IEnumerator ConsiderBurn()
	{
		if (!this.parent || this.health <= 0)
		{
			yield break;
		}
		int num2;
		for (int i = 0; i < this.statusEffects.childCount; i = num2 + 1)
		{
			if (!this || !this.parent || this.health <= 0)
			{
				yield break;
			}
			StatusEffect component = this.statusEffects.GetChild(i).GetComponent<StatusEffect>();
			if (component.type == StatusEffect.Type.fire)
			{
				int num = component.value * -1;
				if (this.playerScript && Item2.GetItemWithStatusEffect(Item2.ItemStatusEffect.Type.dontTakeDamageFromBurn, null, true))
				{
					num = 0;
				}
				this.Attack(null, num, null, false, false, false);
				EffectParticleSystem.Instance.CopySprite(this.parent.GetComponentInChildren<SpriteRenderer>(), EffectParticleSystem.ParticleType.fire);
				SoundManager.main.PlaySFXPitched("statusEffectBad", Random.Range(0.95f, 1.05f), false);
				yield return new WaitForSeconds(0.25f);
			}
			if (!this || !this.parent || this.health <= 0)
			{
				yield break;
			}
			num2 = i;
		}
		yield break;
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x0006266F File Offset: 0x0006086F
	public IEnumerator NextTurn()
	{
		if (!this.parent || this.health <= 0)
		{
			yield break;
		}
		int num;
		for (int i = 0; i < this.statusEffects.childCount; i = num + 1)
		{
			StatusEffect statusEffect = this.statusEffects.GetChild(i).GetComponent<StatusEffect>();
			if (statusEffect.type == StatusEffect.Type.sleep)
			{
				GameFlowManager.main.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onEnemyAsleep, null, new List<Status> { this }, true, false);
			}
			if (statusEffect.type != StatusEffect.Type.poison && statusEffect.type != StatusEffect.Type.charm && statusEffect.type != StatusEffect.Type.sleep)
			{
				StatusEffect.Type type = statusEffect.type;
				if (type != StatusEffect.Type.regen)
				{
					if (type == StatusEffect.Type.exhaust)
					{
						this.playerScript.ChangeAP(-1);
						SoundManager.main.PlaySFX("statusEffectBad");
						yield return new WaitForSeconds(0.25f);
					}
				}
				else
				{
					this.ChangeHealth(statusEffect.value, null, false);
					SoundManager.main.PlaySFX("statusEffectGood");
					yield return new WaitForSeconds(0.25f);
				}
				if (!statusEffect)
				{
					yield break;
				}
				if (this.GetStatusEffectOfType(StatusEffect.Type.RemoveAllSpikes))
				{
					this.AddStatusEffect(StatusEffect.Type.spikes, -999f, Item2.Effect.MathematicalType.summative);
				}
				if (statusEffect.isNumeric && statusEffect.decreasesTimeType == StatusEffect.DecreasesTime.startOfTurn)
				{
					statusEffect.ChangeValue(-1);
				}
				if (statusEffect.isNumeric && statusEffect.decreasesTimeType == StatusEffect.DecreasesTime.clearsFullyAtStartOfTurn)
				{
					statusEffect.ChangeValue(-999);
				}
				if (statusEffect.value <= 0)
				{
					statusEffect.RemoveChangeIntentionXWithoutEffect();
				}
				statusEffect = null;
			}
			num = i;
		}
		yield break;
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x0006267E File Offset: 0x0006087E
	public IEnumerator TurnEnd()
	{
		if (!this.parent || this.health <= 0)
		{
			yield break;
		}
		int num;
		for (int i = 0; i < this.statusEffects.childCount; i = num + 1)
		{
			StatusEffect statusEffect = this.statusEffects.GetChild(i).GetComponent<StatusEffect>();
			StatusEffect.Type type = statusEffect.type;
			if (type != StatusEffect.Type.poison)
			{
				if (type != StatusEffect.Type.charm)
				{
					if (type == StatusEffect.Type.sleep)
					{
						SoundManager.main.PlaySFXPitched("statusEffectBad", Random.Range(0.95f, 1.05f), false);
						yield return new WaitForSeconds(0.25f);
					}
				}
				else
				{
					SoundManager.main.PlaySFX("statusEffectGood");
					yield return new WaitForSeconds(0.25f);
				}
			}
			else
			{
				if (this.GetStatusEffectValue(StatusEffect.Type.zombie) != -999)
				{
					this.ChangeHealth(statusEffect.value, null, true);
				}
				else
				{
					this.ChangeHealth(statusEffect.value * -1, null, false);
				}
				EffectParticleSystem.Instance.CopySprite(this.parent.GetComponentInChildren<SpriteRenderer>(), EffectParticleSystem.ParticleType.poison);
				SoundManager.main.PlaySFXPitched("statusEffectBad", Random.Range(0.95f, 1.05f), false);
				yield return new WaitForSeconds(0.25f);
			}
			if (!statusEffect || !this.parent)
			{
				yield break;
			}
			if (statusEffect.isNumeric && statusEffect.decreasesTimeType == StatusEffect.DecreasesTime.endOfTurn)
			{
				statusEffect.ChangeValue(-1);
			}
			else if (statusEffect.decreasesTimeType == StatusEffect.DecreasesTime.clearsFullyAtEndOfTurn)
			{
				statusEffect.RemoveChangeIntentionXWithoutEffect();
			}
			if (statusEffect.value <= 0)
			{
				statusEffect.RemoveStatusEffect();
			}
			if (!statusEffect || !this.parent)
			{
				yield break;
			}
			statusEffect = null;
			num = i;
		}
		this.ConsiderEndCharm();
		yield break;
	}

	// Token: 0x060009B4 RID: 2484 RVA: 0x0006268D File Offset: 0x0006088D
	public void ConsiderEndCharm()
	{
		if (this.GetStatusEffectValue(StatusEffect.Type.charm) < this.health)
		{
			this.charmedThisTurn = false;
		}
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x000626A6 File Offset: 0x000608A6
	public bool IsCharmed()
	{
		if (this.GetStatusEffectValue(StatusEffect.Type.charm) >= this.health || this.combatPet)
		{
			this.charmedThisTurn = true;
			return true;
		}
		return this.charmedThisTurn;
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x000626D9 File Offset: 0x000608D9
	public void EndArmor()
	{
		if (this.armor > 0)
		{
			this.armor = 0;
			this.ChangeArmor(0f, Item2.Effect.MathematicalType.summative);
		}
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x000626F8 File Offset: 0x000608F8
	public void AddStatusEffectFromPrefab(GameObject statusEffectPrefab, bool setValue = false, int value = 0)
	{
		if (!this.statusEffects)
		{
			return;
		}
		StatusEffect component = statusEffectPrefab.GetComponent<StatusEffect>();
		using (List<StatusEffect>.Enumerator enumerator = this.GetStatusEffects().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.type == component.type)
				{
					if (setValue)
					{
						this.AddStatusEffect(component.type, (float)value, Item2.Effect.MathematicalType.summative);
						return;
					}
					this.AddStatusEffect(component.type, (float)component.value, Item2.Effect.MathematicalType.summative);
					return;
				}
			}
		}
		GameObject gameObject = Object.Instantiate<GameObject>(statusEffectPrefab, Vector3.zero, Quaternion.identity, this.statusEffects);
		StatusEffect component2 = gameObject.GetComponent<StatusEffect>();
		if (setValue)
		{
			component2.SetValue(value);
		}
		gameObject.transform.SetAsFirstSibling();
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x000627C0 File Offset: 0x000609C0
	public void AddStatusEffect(StatusEffect.Type statusType, float value, Item2.Effect.MathematicalType mathematicalType = Item2.Effect.MathematicalType.summative)
	{
		if (!this.statusEffects)
		{
			return;
		}
		if (statusType == StatusEffect.Type.zombie && this.GetStatusEffectValue(StatusEffect.Type.zombie) == -999)
		{
			GameFlowManager.main.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.whenZombied, null, new List<Status> { this }, true, false);
		}
		if (Mathf.RoundToInt(value) == 0 && mathematicalType == Item2.Effect.MathematicalType.summative && statusType != StatusEffect.Type.zombie && statusType != StatusEffect.Type.toughHide)
		{
			return;
		}
		if (statusType == StatusEffect.Type.poison || statusType == StatusEffect.Type.slow || statusType == StatusEffect.Type.weak)
		{
			SoundManager.main.PlaySFXPitched("statusEffectBad", Random.Range(0.95f, 1.05f), false);
		}
		else if (statusType == StatusEffect.Type.haste || statusType == StatusEffect.Type.rage || statusType == StatusEffect.Type.regen || (statusType == StatusEffect.Type.dodge && value > 0f))
		{
			SoundManager.main.PlaySFX("statusEffectGood");
		}
		else if (statusType == StatusEffect.Type.spikes)
		{
			SoundManager.main.PlaySFXPitched("statusEffectSpike", Random.Range(0.95f, 1.05f), false);
		}
		if (mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			int i = 0;
			while (i < this.statusEffects.childCount)
			{
				StatusEffect component = this.statusEffects.GetChild(i).GetComponent<StatusEffect>();
				if (component.type == statusType)
				{
					component.SetValue(component.value * Mathf.RoundToInt(1f + value));
					if (component.value <= 0)
					{
						component.RemoveStatusEffect();
					}
					if (this.enemyScript)
					{
						this.enemyScript.UpdateIntentionRageAndWeak();
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			return;
		}
		for (int j = 0; j < this.statusEffects.childCount; j++)
		{
			StatusEffect component2 = this.statusEffects.GetChild(j).GetComponent<StatusEffect>();
			if (component2.type == statusType)
			{
				if (mathematicalType == Item2.Effect.MathematicalType.summative)
				{
					component2.ChangeValue(Mathf.RoundToInt(value));
				}
				component2.UpdateValue();
				if (component2.value <= 0)
				{
					component2.RemoveStatusEffect();
				}
				if (this.enemyScript)
				{
					this.enemyScript.UpdateIntentionRageAndWeak();
				}
				return;
			}
		}
		if (value <= 0f)
		{
			return;
		}
		GameObject gameObject = Object.Instantiate<GameObject>(this.statusEffectPrefab, Vector3.zero, Quaternion.identity, this.statusEffects);
		StatusEffect component3 = gameObject.GetComponent<StatusEffect>();
		component3.type = statusType;
		component3.SetValue(Mathf.RoundToInt(value));
		if (this.playerScript && this.statusEffects.childCount >= 4)
		{
			AchievementAbstractor.instance.ConsiderAchievement("StatusMaster");
		}
		if (statusType == StatusEffect.Type.regen)
		{
			gameObject.transform.SetAsFirstSibling();
		}
		Enemy component4 = this.parent.GetComponent<Enemy>();
		if (component4)
		{
			component4.UpdateIntentionRageAndWeak();
		}
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x00062A1C File Offset: 0x00060C1C
	public List<StatusEffect> GetStatusEffects()
	{
		List<StatusEffect> list = new List<StatusEffect>();
		if (!this.statusEffects)
		{
			return list;
		}
		for (int i = 0; i < this.statusEffects.childCount; i++)
		{
			StatusEffect component = this.statusEffects.GetChild(i).GetComponent<StatusEffect>();
			list.Add(component);
		}
		return list;
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x00062A70 File Offset: 0x00060C70
	public StatusEffect GetStatusEffectOfType(StatusEffect.Type type)
	{
		if (!this.statusEffects)
		{
			return null;
		}
		for (int i = 0; i < this.statusEffects.childCount; i++)
		{
			StatusEffect component = this.statusEffects.GetChild(i).GetComponent<StatusEffect>();
			if (component.type == type)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x00062AC0 File Offset: 0x00060CC0
	public int GetStatusEffectValue(StatusEffect.Type statusType)
	{
		if (!this.statusEffects)
		{
			return -999;
		}
		for (int i = 0; i < this.statusEffects.childCount; i++)
		{
			StatusEffect component = this.statusEffects.GetChild(i).GetComponent<StatusEffect>();
			if (component.type == statusType)
			{
				return component.value;
			}
		}
		return -999;
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x00062B20 File Offset: 0x00060D20
	public void ChangeArmor(float amount, Item2.Effect.MathematicalType mathematicalType = Item2.Effect.MathematicalType.summative)
	{
		if (mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			amount *= (float)this.armor;
		}
		else if (amount > 0f)
		{
			int statusEffectValue = this.GetStatusEffectValue(StatusEffect.Type.haste);
			if (statusEffectValue != -999)
			{
				amount += (float)statusEffectValue;
			}
			int statusEffectValue2 = this.GetStatusEffectValue(StatusEffect.Type.slow);
			if (statusEffectValue2 != -999)
			{
				amount -= (float)statusEffectValue2;
			}
			amount = Mathf.Max(amount, 0f);
		}
		if (amount < 0f)
		{
			SoundManager.main.PlaySFXPitched("hitShield", Random.Range(0.9f, 1.1f), false);
			GameObject gameObject = Object.Instantiate<GameObject>(this.damageIndicator, this.armorText.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("PrimaryCanvas").GetComponent<Canvas>().transform);
			gameObject.GetComponent<TextMeshProUGUI>().text = Mathf.Abs(Mathf.Min(amount * -1f, (float)this.armor)).ToString() ?? "";
			Rigidbody2D component = gameObject.GetComponent<Rigidbody2D>();
			component.velocity = new Vector2(Random.Range(0.5f, 1.1f), Random.Range(1.4f, 1.8f));
			component.gravityScale = 0.5f;
			gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
			if (this.parent.GetComponent<Player>() && !this.gameManager.dead)
			{
				PlayerStatTracking.main.AddStat("Player armor lost", Mathf.CeilToInt(amount));
			}
		}
		else if (amount > 0f)
		{
			SoundManager.main.PlaySFXPitched("addShield", Random.Range(0.95f, 1.05f), false);
			GameObject gameObject2 = Object.Instantiate<GameObject>(this.damageIndicator, this.armorText.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("PrimaryCanvas").GetComponent<Canvas>().transform);
			gameObject2.GetComponent<TextMeshProUGUI>().text = "+" + Mathf.Abs(Mathf.CeilToInt(amount)).ToString();
			Rigidbody2D component2 = gameObject2.GetComponent<Rigidbody2D>();
			component2.velocity = new Vector2(0f, 1.6f);
			component2.gravityScale = 0.3f;
			gameObject2.GetComponent<TextMeshProUGUI>().color = new Color(0.39f, 0.66f, 1f);
			if (this.parent.GetComponent<Player>() && !this.gameManager.dead)
			{
				PlayerStatTracking.main.AddStat("Player armor gained", Mathf.CeilToInt(amount));
			}
		}
		this.armor += Mathf.CeilToInt(amount);
		this.armor = Mathf.Clamp(this.armor, 0, 9999);
		this.armorText.text = this.armor.ToString() ?? "";
		if (this.armor > 0)
		{
			this.armorText.gameObject.SetActive(true);
			this.iconAnimator.Play("beginShield", 0, 0f);
			return;
		}
		if (this.armorText && this.armorText.gameObject.activeInHierarchy)
		{
			this.iconAnimator.Play("endShield", 0, 0f);
			this.armorText.gameObject.SetActive(false);
		}
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x00062E58 File Offset: 0x00061058
	public void Vampire(Status attacker, int amount, Item2 attackingItem = null)
	{
		if (!this.statusEffects)
		{
			return;
		}
		int num = amount * -1 - this.armor;
		if (num > 0 && this.GetStatusEffectValue(StatusEffect.Type.dodge) <= 0)
		{
			attacker.ChangeHealth(num, null, false);
		}
		this.Attack(attacker, amount, attackingItem, false, false, false);
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x00062EA4 File Offset: 0x000610A4
	public void Attack(Status attacker, int amount, Item2 attackingItem = null, bool isProjectile = false, bool ignoreRoughHide = false, bool ignoreRageAndWeak = false)
	{
		if (!this.statusEffects)
		{
			return;
		}
		if (attacker && attacker != this && !ignoreRageAndWeak)
		{
			int statusEffectValue = attacker.GetStatusEffectValue(StatusEffect.Type.rage);
			if (statusEffectValue != -999)
			{
				amount -= statusEffectValue;
			}
			int statusEffectValue2 = attacker.GetStatusEffectValue(StatusEffect.Type.weak);
			if (statusEffectValue2 != -999)
			{
				amount += statusEffectValue2;
			}
		}
		if (!attacker || attacker != this)
		{
			int statusEffectValue3 = this.GetStatusEffectValue(StatusEffect.Type.freeze);
			if (statusEffectValue3 != -999)
			{
				amount -= statusEffectValue3;
			}
		}
		GameFlowManager.main.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.whenAttacked, null, new List<Status> { this, attacker }, true, false);
		for (int i = 0; i < this.statusEffects.childCount; i++)
		{
			if (this.statusEffects.GetChild(i).GetComponent<StatusEffect>().type == StatusEffect.Type.dodge)
			{
				SoundManager.main.PlaySFXPitched("dodge", Random.Range(0.9f, 1.1f), false);
				GameObject gameObject = Object.Instantiate<GameObject>(this.damageIndicator, new Vector3(base.transform.position.x, this.parent.transform.position.y, -6f), Quaternion.identity, GameObject.FindGameObjectWithTag("PrimaryCanvas").GetComponent<Canvas>().transform);
				TextMeshProUGUI component = gameObject.GetComponent<TextMeshProUGUI>();
				component.text = LangaugeManager.main.GetTextByKey("se8");
				component.color = Color.white;
				if (this.enemyScript)
				{
					this.enemyScript.DodgeAnimation();
				}
				gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(1f, 1.4f), Random.Range(4.5f, 6f));
				LangaugeManager.main.SetFont(gameObject.transform);
				this.AddStatusEffect(StatusEffect.Type.dodge, -1f, Item2.Effect.MathematicalType.summative);
				return;
			}
		}
		int j = 0;
		while (j < this.statusEffects.childCount)
		{
			StatusEffect component2 = this.statusEffects.GetChild(j).GetComponent<StatusEffect>();
			if (component2.type == StatusEffect.Type.spikes && !isProjectile && (!attackingItem || !attackingItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.projectile)))
			{
				if (attacker && attacker != this)
				{
					attacker.ChangeHealth(component2.value * -1, true, null);
					break;
				}
				break;
			}
			else
			{
				if (component2.type == StatusEffect.Type.toughHide && !ignoreRoughHide && (isProjectile || (attackingItem && attackingItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.projectile))))
				{
					amount = Mathf.CeilToInt((float)amount / 2f);
					break;
				}
				j++;
			}
		}
		if (this.enemyScript)
		{
			GameFlowManager.main.AddCombatStat(GameFlowManager.CombatStat.Type.damageDealt, amount * -1, null);
		}
		this.ChangeHealth(Mathf.Min(amount, 0), true, attackingItem);
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x00063158 File Offset: 0x00061358
	public void ChangeHealth(int amount, bool blockable, Item2 attackingItem = null)
	{
		if (attackingItem && attackingItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.piercing))
		{
			blockable = false;
		}
		int num = this.armor;
		if (blockable && this.armor > 0)
		{
			this.ChangeArmor((float)amount, Item2.Effect.MathematicalType.summative);
			amount += num;
			if (amount >= 0)
			{
				return;
			}
		}
		this.ChangeHealth(amount, attackingItem, false);
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x000631AC File Offset: 0x000613AC
	public void ChangeHealth(int amount, Item2 attackingItem = null, bool overrideZombie = false)
	{
		this.health = Mathf.Clamp(this.health, 0, this.maxHealth);
		if (amount > 0 && this.GetStatusEffectValue(StatusEffect.Type.zombie) != -999 && !overrideZombie)
		{
			amount = -amount;
		}
		if (attackingItem && amount < 0)
		{
			int num = Mathf.Min(-amount, this.health);
			if (num > 0 && attackingItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.vampiric))
			{
				Player.main.stats.ChangeHealth(num, null, false);
			}
		}
		if (amount < 0)
		{
			this.AddStatusEffect(StatusEffect.Type.reactive, (float)amount, Item2.Effect.MathematicalType.summative);
			this.AddStatusEffect(StatusEffect.Type.Wary, (float)amount, Item2.Effect.MathematicalType.summative);
			this.AddStatusEffect(StatusEffect.Type.Sturdy, (float)amount, Item2.Effect.MathematicalType.summative);
			if (this.GetStatusEffectOfType(StatusEffect.Type.AutoShield) && this.health + amount > 0)
			{
				this.ChangeArmor((float)this.GetStatusEffectValue(StatusEffect.Type.AutoShield), Item2.Effect.MathematicalType.summative);
			}
		}
		if (this.playerScript && amount < 0 && amount >= -5 && Item2.GetItemWithStatusEffect(Item2.ItemStatusEffect.Type.roundDamageTo1, null, false))
		{
			amount = -1;
		}
		Item2 itemWithStatusEffect = Item2.GetItemWithStatusEffect(Item2.ItemStatusEffect.Type.takingDamageDoesStartOfTurnEffects, null, false);
		if (amount == 0)
		{
			SoundManager.main.PlaySFXPitched("weakHit", Random.Range(0.9f, 1.1f), false);
			GameObject gameObject = Object.Instantiate<GameObject>(this.damageIndicator, new Vector3(base.transform.position.x, this.parent.transform.position.y, -6f), Quaternion.identity, GameObject.FindGameObjectWithTag("PrimaryCanvas").GetComponent<Canvas>().transform);
			gameObject.GetComponent<TextMeshProUGUI>().text = Mathf.Abs(amount).ToString() ?? "";
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(1f, 1.4f), Random.Range(4.5f, 6f));
		}
		else if (amount < 0)
		{
			SoundManager.main.PlaySFXPitched("playerHit", Random.Range(0.9f, 1.1f), false);
			GameObject gameObject2 = Object.Instantiate<GameObject>(this.damageIndicator, new Vector3(base.transform.position.x, this.parent.transform.position.y, -6f), Quaternion.identity, GameObject.FindGameObjectWithTag("PrimaryCanvas").GetComponent<Canvas>().transform);
			gameObject2.GetComponent<TextMeshProUGUI>().text = Mathf.Abs(amount).ToString() ?? "";
			gameObject2.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(1f, 1.4f), Random.Range(4.5f, 6f));
			if (this.enemyScript)
			{
				this.enemyScript.HurtAnimation();
				if (amount < 0)
				{
					PlayerStatTracking.main.AddStat("Damage dealt", amount);
				}
				if (this.health + amount <= 0)
				{
					this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.whenAnEnemyIsDefeated, null, new List<Status> { this }, true, false);
				}
				if (this.health + amount <= 0 && attackingItem)
				{
					this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onKillEnemy, attackingItem, new List<Status> { this }, true, false);
					if (!this.enemyScript.isSummon)
					{
						this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onKillNonSummonEnemy, attackingItem, new List<Status> { this }, true, false);
					}
				}
			}
			if (this.combatPet)
			{
				if (this.health + amount <= 0 && attackingItem)
				{
					this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onKillEnemy, attackingItem, new List<Status> { this }, true, false);
				}
				this.combatPet.HurtAnimation();
			}
			Player component = this.parent.GetComponent<Player>();
			if (component && !this.gameManager.dead)
			{
				if (Item2.GetItemWithStatusEffect(Item2.ItemStatusEffect.Type.invincible, null, false))
				{
					amount = 0;
					GameFlowManager.main.AddCombatStat(GameFlowManager.CombatStat.Type.invincibleHitsTaken, 1, null);
				}
				if (this.health + amount <= 0)
				{
					this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onDie, attackingItem, new List<Status> { this }, true, false);
				}
				component.Hurt();
				if (amount < 0)
				{
					PlayerStatTracking.main.AddStat("Player health lost", amount);
					this.gameFlowManager.AddCombatStat(GameFlowManager.CombatStat.Type.damageTaken, amount * -1, null);
				}
			}
			if (!this.hasReactedToOnTakeDamage)
			{
				this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onTakeDamage, null, new List<Status> { this }, true, false);
				if (this.playerScript && amount < 0 && itemWithStatusEffect)
				{
					GameFlowManager.main.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onTurnStart, null, null, true, false);
				}
				this.hasReactedToOnTakeDamage = true;
			}
		}
		else if (amount > 0)
		{
			SoundManager.main.PlaySFX("heal");
			GameObject gameObject3 = Object.Instantiate<GameObject>(this.damageIndicator, this.parent.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("PrimaryCanvas").GetComponent<Canvas>().transform);
			TextMeshProUGUI component2 = gameObject3.GetComponent<TextMeshProUGUI>();
			component2.text = "+" + Mathf.Abs(amount).ToString();
			component2.color = Color.green;
			Rigidbody2D component3 = gameObject3.GetComponent<Rigidbody2D>();
			component3.velocity = new Vector2(0f, Random.Range(2.5f, 3.5f));
			component3.gravityScale = 0f;
			if (this.parent.GetComponent<Player>() && !this.gameManager.dead)
			{
				PlayerStatTracking.main.AddStat("Player health healed", amount);
			}
		}
		if ((this.playerScript || this.combatPet) && this.health + amount <= 0)
		{
			Transform inventoryFromStats = PetMaster.GetInventoryFromStats(this);
			if (inventoryFromStats && Item2.UseItemIndirectWithStatusEffect(Item2.ItemStatusEffect.Type.effigy, inventoryFromStats))
			{
				return;
			}
		}
		this.health += amount;
		this.health = Mathf.Clamp(this.health, 0, this.maxHealth);
		Enemy component4 = this.parent.GetComponent<Enemy>();
		component4;
		if (this.health <= 0 && component4 && !component4.dead && !component4.ConsiderSwap(Enemy.EnemySwap.Condition.onDeath))
		{
			if (!component4.deathAbruptlyEndsCombat)
			{
				component4.dead = true;
			}
			component4.StartCoroutine(component4.Die());
			PlayerStatTracking.main.AddStat("Enemies defeated", 1);
		}
		this.healthText.text = this.health.ToString() + "/" + this.maxHealth.ToString();
		this.healthSlider.value = (float)this.health / (float)this.maxHealth;
		if (this.combatPet)
		{
			this.combatPet.petItem2.health = this.health;
			this.combatPet.petItem2.maxHealth = this.maxHealth;
		}
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x0006383C File Offset: 0x00061A3C
	public void SetHealth(int amount)
	{
		this.health = Mathf.Clamp(this.health, 0, this.maxHealth);
		this.health = amount;
		this.health = Mathf.Clamp(this.health, 0, this.maxHealth);
		this.healthText.text = this.health.ToString() + "/" + this.maxHealth.ToString();
		this.healthSlider.value = (float)this.health / (float)this.maxHealth;
		if (this.combatPet)
		{
			this.combatPet.petItem2.health = this.health;
			this.combatPet.petItem2.maxHealth = this.maxHealth;
		}
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x00063900 File Offset: 0x00061B00
	public void SetMaxHP(int amount)
	{
		if (this.maxHealth == amount)
		{
			return;
		}
		this.maxHealth = amount;
		this.maxHealth = Mathf.Clamp(this.maxHealth, 0, 999999);
		this.maxHealthBeforeItems = this.maxHealth;
		this.healthText.text = Mathf.Clamp(this.health, 0, this.maxHealth).ToString() + "/" + this.maxHealth.ToString();
		this.healthSlider.value = (float)this.health / (float)this.maxHealth;
		if (this.combatPet)
		{
			this.combatPet.petItem2.health = this.health;
			this.combatPet.petItem2.maxHealth = this.maxHealth;
		}
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x000639D0 File Offset: 0x00061BD0
	public void ClampHealth()
	{
		this.health = Mathf.Clamp(this.health, 0, this.maxHealth);
		this.healthText.text = this.health.ToString() + "/" + this.maxHealth.ToString();
		this.healthSlider.value = (float)this.health / (float)this.maxHealth;
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x00063A3C File Offset: 0x00061C3C
	private void Update()
	{
		if (!this.parent)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		if (this.followParent)
		{
			base.transform.position = new Vector3(this.followParent.transform.position.x, -4.8f, this.followParent.position.z);
			return;
		}
		if (this.parent)
		{
			base.transform.position = new Vector3(this.parent.transform.position.x, -4.8f, base.transform.position.z);
		}
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x00063AF1 File Offset: 0x00061CF1
	public void Show(bool show)
	{
		if (!this.canvasGroup)
		{
			return;
		}
		if (!show)
		{
			this.canvasGroup.alpha = 0f;
			return;
		}
		this.canvasGroup.alpha = 1f;
	}

	// Token: 0x040007E9 RID: 2025
	public static List<Status> allStatuses = new List<Status>();

	// Token: 0x040007EA RID: 2026
	public GameObject parent;

	// Token: 0x040007EB RID: 2027
	[SerializeField]
	private Transform followParent;

	// Token: 0x040007EC RID: 2028
	[Header("References")]
	[SerializeField]
	private CanvasGroup canvasGroup;

	// Token: 0x040007ED RID: 2029
	[SerializeField]
	private Image icon;

	// Token: 0x040007EE RID: 2030
	[SerializeField]
	private Animator iconAnimator;

	// Token: 0x040007EF RID: 2031
	[SerializeField]
	private TextMeshProUGUI healthText;

	// Token: 0x040007F0 RID: 2032
	[SerializeField]
	private TextMeshProUGUI armorText;

	// Token: 0x040007F1 RID: 2033
	[SerializeField]
	private Slider healthSlider;

	// Token: 0x040007F2 RID: 2034
	[SerializeField]
	private GameObject damageIndicator;

	// Token: 0x040007F3 RID: 2035
	[Header("Stats")]
	public int health = 10;

	// Token: 0x040007F4 RID: 2036
	public int maxHealth = 10;

	// Token: 0x040007F5 RID: 2037
	[HideInInspector]
	public int maxHealthBeforeItems = 10;

	// Token: 0x040007F6 RID: 2038
	[HideInInspector]
	public int armor;

	// Token: 0x040007F7 RID: 2039
	[SerializeField]
	private Transform statusEffects;

	// Token: 0x040007F8 RID: 2040
	[SerializeField]
	private GameObject statusEffectPrefab;

	// Token: 0x040007F9 RID: 2041
	[HideInInspector]
	private GameManager gameManager;

	// Token: 0x040007FA RID: 2042
	[HideInInspector]
	private GameFlowManager gameFlowManager;

	// Token: 0x040007FB RID: 2043
	private bool charmedThisTurn;

	// Token: 0x040007FC RID: 2044
	public Player playerScript;

	// Token: 0x040007FD RID: 2045
	public Enemy enemyScript;

	// Token: 0x040007FE RID: 2046
	public CombatPet combatPet;

	// Token: 0x040007FF RID: 2047
	private bool hasReactedToOnTakeDamage;
}
