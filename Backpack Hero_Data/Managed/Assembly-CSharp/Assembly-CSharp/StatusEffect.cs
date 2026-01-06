using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200011F RID: 287
public class StatusEffect : CustomInputHandler
{
	// Token: 0x060009C8 RID: 2504 RVA: 0x00063B51 File Offset: 0x00061D51
	private void OnEnable()
	{
		if (!StatusEffect.allStatusEffects.Contains(this))
		{
			StatusEffect.allStatusEffects.Add(this);
		}
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x00063B6B File Offset: 0x00061D6B
	private void OnDisable()
	{
		StatusEffect.allStatusEffects.Remove(this);
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x00063B7C File Offset: 0x00061D7C
	private void Start()
	{
		foreach (Enemy.PossibleAttack possibleAttack in this.possibleAttacks)
		{
			foreach (Enemy.Attack attack in possibleAttack.attacks)
			{
				for (int i = 0; i < attack.prefabs.Count; i++)
				{
					if (attack.prefabs[i] == base.gameObject)
					{
						Debug.Log("Status effect references itself!");
						GameObject gameObject = Resources.Load<GameObject>("Status Effects/" + base.name.Replace("(Clone)", "").Trim());
						if (gameObject)
						{
							string text = "changing to prefab ";
							GameObject gameObject2 = gameObject;
							Debug.Log(text + ((gameObject2 != null) ? gameObject2.ToString() : null));
							attack.prefabs[i] = gameObject;
						}
						else
						{
							Debug.LogError("Couldn't find status effect prefab for " + base.name);
							Debug.LogError("It's a reference to its own prefab so it can't be instantiated once this is deleted");
						}
					}
				}
			}
		}
		this.status = base.GetComponentInParent<Status>();
		this.animator = base.GetComponent<Animator>();
		this.mainSpriteRenderer.sprite = this.GetSpriteFromType(this.type);
		if (this.type == StatusEffect.Type.poison)
		{
			this.decreasesTimeType = StatusEffect.DecreasesTime.endOfTurn;
		}
		else if (this.type == StatusEffect.Type.regen)
		{
			this.decreasesTimeType = StatusEffect.DecreasesTime.startOfTurn;
		}
		else if (this.type == StatusEffect.Type.spikes)
		{
			this.decreasesTimeType = StatusEffect.DecreasesTime.startOfTurn;
		}
		else if (this.type == StatusEffect.Type.rage)
		{
			this.decreasesTimeType = StatusEffect.DecreasesTime.endOfTurn;
		}
		else if (this.type == StatusEffect.Type.weak)
		{
			this.decreasesTimeType = StatusEffect.DecreasesTime.endOfTurn;
		}
		else if (this.type == StatusEffect.Type.haste)
		{
			this.decreasesTimeType = StatusEffect.DecreasesTime.endOfTurn;
		}
		else if (this.type == StatusEffect.Type.slow)
		{
			this.decreasesTimeType = StatusEffect.DecreasesTime.endOfTurn;
		}
		else if (this.type == StatusEffect.Type.freeze)
		{
			this.decreasesTimeType = StatusEffect.DecreasesTime.endOfTurn;
		}
		else if (this.type == StatusEffect.Type.fire)
		{
			this.decreasesTimeType = StatusEffect.DecreasesTime.startOfTurn;
		}
		else if (this.type == StatusEffect.Type.dodge)
		{
			this.decreasesTimeType = StatusEffect.DecreasesTime.startOfTurn;
		}
		else if (this.type == StatusEffect.Type.toughHide)
		{
			this.isNumeric = false;
			this.decreasesTimeType = StatusEffect.DecreasesTime.never;
		}
		else if (this.type == StatusEffect.Type.charm)
		{
			this.decreasesTimeType = StatusEffect.DecreasesTime.endOfTurn;
		}
		else if (this.type == StatusEffect.Type.sleep)
		{
			this.decreasesTimeType = StatusEffect.DecreasesTime.endOfTurn;
		}
		else if (this.type == StatusEffect.Type.zombie)
		{
			this.isNumeric = false;
			this.decreasesTimeType = StatusEffect.DecreasesTime.never;
		}
		else if (this.type == StatusEffect.Type.curse)
		{
			this.isNumeric = false;
			this.decreasesTimeType = StatusEffect.DecreasesTime.remainsEvenAfterCombat;
		}
		else if (this.type == StatusEffect.Type.exhaust)
		{
			this.isNumeric = false;
			this.decreasesTimeType = StatusEffect.DecreasesTime.never;
		}
		else if (this.type == StatusEffect.Type.reactive)
		{
			this.isNumeric = true;
			this.decreasesTimeType = StatusEffect.DecreasesTime.clearsFullyAtStartOfTurn;
		}
		else if (this.type == StatusEffect.Type.Shift)
		{
			this.isNumeric = false;
			this.decreasesTimeType = StatusEffect.DecreasesTime.never;
		}
		else if (this.type == StatusEffect.Type.Lonely)
		{
			this.isNumeric = false;
			this.decreasesTimeType = StatusEffect.DecreasesTime.never;
		}
		else if (this.type == StatusEffect.Type.Dependent)
		{
			this.isNumeric = false;
			this.decreasesTimeType = StatusEffect.DecreasesTime.never;
		}
		else if (this.type == StatusEffect.Type.Wealthy)
		{
			this.isNumeric = true;
			this.decreasesTimeType = StatusEffect.DecreasesTime.never;
		}
		else if (this.type == StatusEffect.Type.Windfall)
		{
			this.isNumeric = false;
			this.decreasesTimeType = StatusEffect.DecreasesTime.never;
		}
		else if (this.type == StatusEffect.Type.Stack)
		{
			this.isNumeric = false;
			this.decreasesTimeType = StatusEffect.DecreasesTime.never;
		}
		else if (this.type == StatusEffect.Type.Cowardly)
		{
			this.isNumeric = false;
			this.decreasesTimeType = StatusEffect.DecreasesTime.never;
		}
		else if (this.type == StatusEffect.Type.Defender)
		{
			this.isNumeric = false;
			this.decreasesTimeType = StatusEffect.DecreasesTime.never;
		}
		else if (this.type == StatusEffect.Type.RemoveAllSpikes)
		{
			this.isNumeric = false;
			this.decreasesTimeType = StatusEffect.DecreasesTime.never;
		}
		else if (this.type == StatusEffect.Type.Wary)
		{
			this.isNumeric = true;
			this.decreasesTimeType = StatusEffect.DecreasesTime.never;
		}
		else if (this.type == StatusEffect.Type.AutoShield)
		{
			this.isNumeric = true;
			this.decreasesTimeType = StatusEffect.DecreasesTime.never;
		}
		else if (this.type == StatusEffect.Type.Sturdy)
		{
			this.isNumeric = true;
			this.decreasesTimeType = StatusEffect.DecreasesTime.clearsFullyAtStartOfTurn;
		}
		this.UpdateValue();
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x00063FF8 File Offset: 0x000621F8
	public static bool CanBeCleansed(StatusEffect.Type type)
	{
		return type - StatusEffect.Type.reactive > 14;
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x00064005 File Offset: 0x00062205
	public void RemoveChangeIntentionXWithoutEffect()
	{
		if (!this.status)
		{
			return;
		}
		if (this.type == StatusEffect.Type.reactive || this.type == StatusEffect.Type.Sturdy)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		this.RemoveStatusEffect();
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x0006403C File Offset: 0x0006223C
	public void RemoveStatusEffect()
	{
		if (!this.status)
		{
			return;
		}
		if (this.type == StatusEffect.Type.reactive && this.status.enemyScript)
		{
			if (this.animatorControllerToApplyToEnemyIfReacted)
			{
				this.status.enemyScript.ChangeAnimator(this.animatorControllerToApplyToEnemyIfReacted);
			}
			if (this.possibleAttacks.Count > 0)
			{
				this.status.enemyScript.ChangeIntentionPerm(this.possibleAttacks);
			}
			else
			{
				this.status.enemyScript.ChangeIntentionPerm(this.possibleAttack);
			}
		}
		if (this.type == StatusEffect.Type.Wary && this.status.enemyScript && this.status.health > 0)
		{
			this.status.ChangeArmor((float)this.secondaryValue, Item2.Effect.MathematicalType.summative);
			if (this.animatorControllerToApplyToEnemyIfReacted)
			{
				this.status.enemyScript.ChangeAnimator(this.animatorControllerToApplyToEnemyIfReacted);
			}
			if (this.possibleAttacks.Count > 0)
			{
				this.status.enemyScript.ChangeIntentionPerm(this.possibleAttacks);
			}
			else
			{
				this.status.enemyScript.ChangeIntentionPerm(this.possibleAttack);
			}
		}
		if (this.type == StatusEffect.Type.zombie && this.status.playerScript)
		{
			Item2.RemoveAllModifiers(new List<Item2.Modifier.Length> { Item2.Modifier.Length.untilUnzombied }, -1);
		}
		if (this.type == StatusEffect.Type.Sturdy)
		{
			this.status.ChangeArmor((float)this.secondaryValue, Item2.Effect.MathematicalType.summative);
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x000641CB File Offset: 0x000623CB
	private IEnumerator AddStatusEffect(Status status, StatusEffect.Type type, int num)
	{
		yield return new WaitForSeconds(0.1f);
		while (GameFlowManager.main.isCheckingEffects)
		{
			yield return null;
		}
		if (!status)
		{
			yield break;
		}
		status.AddStatusEffect(type, (float)num, Item2.Effect.MathematicalType.summative);
		yield break;
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x000641E8 File Offset: 0x000623E8
	public void SetValue(int num)
	{
		if (num < this.value && StatusEffect.CanBeCleansed(this.type) && Item2.GetItemStatusEffectBool(Item2.ItemStatusEffect.Type.statusEffectsAreConvertedToBurn) && this.type != StatusEffect.Type.fire && this.type != StatusEffect.Type.zombie && this.type != StatusEffect.Type.curse)
		{
			this.status.StartCoroutine(this.AddStatusEffect(this.status, StatusEffect.Type.fire, this.value - num));
		}
		this.value = num;
		this.UpdateValue();
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x00064260 File Offset: 0x00062460
	public void ChangeValue(int num)
	{
		if (num < 0 && StatusEffect.CanBeCleansed(this.type) && Item2.GetItemStatusEffectBool(Item2.ItemStatusEffect.Type.statusEffectsAreConvertedToBurn) && this.type != StatusEffect.Type.fire && this.type != StatusEffect.Type.zombie && this.type != StatusEffect.Type.curse)
		{
			this.status.StartCoroutine(this.AddStatusEffect(this.status, StatusEffect.Type.fire, -num));
		}
		this.value += num;
		this.UpdateValue();
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x000642D4 File Offset: 0x000624D4
	public void UpdateValue()
	{
		if (!this.isNumeric)
		{
			TextMeshProUGUI componentInChildren = base.GetComponentInChildren<TextMeshProUGUI>();
			if (componentInChildren)
			{
				componentInChildren.text = "";
			}
			if (this.animator)
			{
				this.animator.Play("AP_PulseUp", 0, 0f);
			}
			return;
		}
		if (StatusEffect.CanBeCleansed(this.type))
		{
			this.border.gameObject.SetActive(false);
		}
		else
		{
			this.border.gameObject.SetActive(true);
		}
		if (this.type == StatusEffect.Type.curse)
		{
			while (CurseManager.Instance.CursesStored() > this.value)
			{
				CurseManager.Instance.RemoveACurse();
			}
		}
		if (this.type == StatusEffect.Type.poison && this.value >= 50)
		{
			AchievementAbstractor.instance.ConsiderAchievement("PoisonStack");
		}
		base.GetComponentInChildren<TextMeshProUGUI>().text = this.value.ToString() ?? "";
		if (this.animator)
		{
			this.animator.Play("AP_PulseUp", 0, 0f);
		}
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x000643E3 File Offset: 0x000625E3
	public override void OnCursorHold()
	{
		this.ShowCard();
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x000643EB File Offset: 0x000625EB
	private void OnMouseOver()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.ShowCard();
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x00064400 File Offset: 0x00062600
	private void ShowCard()
	{
		if (Input.GetMouseButton(0))
		{
			this.RemoveCard();
			return;
		}
		this.timeToDisplayCard += Time.deltaTime;
		if (this.timeToDisplayCard > 0.3f && !this.previewCard)
		{
			this.previewCard = Object.Instantiate<GameObject>(this.cardPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("PrimaryCanvas").GetComponent<Canvas>().transform);
			Card component = this.previewCard.GetComponent<Card>();
			string text = "";
			if (!StatusEffect.CanBeCleansed(this.type))
			{
				text = "--- " + LangaugeManager.main.GetTextByKey("sePower") + " ---<br>";
			}
			text += this.GetStatusEffectDescription();
			if (text.Length > 0)
			{
				component.GetDescriptionsSimple(new List<string> { text }, base.gameObject);
			}
		}
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x000644EC File Offset: 0x000626EC
	public string GetStatusEffectDescription()
	{
		string text = "";
		if (this.type == StatusEffect.Type.toughHide)
		{
			text = text + LangaugeManager.main.GetTextByKey(StatusEffect.GetNameKeyFromType(this.type)) + ". " + LangaugeManager.main.GetTextByKey(StatusEffect.GetDescriptionKeyFromType(this.type)).Replace("/x", 50.ToString() ?? "");
		}
		else if (this.type == StatusEffect.Type.Dependent)
		{
			string text2 = "";
			foreach (GameObject gameObject in this.enemyScriptsRelated)
			{
				if (gameObject)
				{
					text2 += LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(gameObject.name));
					if (this.enemyScriptsRelated.IndexOf(gameObject) < this.enemyScriptsRelated.Count - 1)
					{
						text2 += ", ";
					}
				}
			}
			if (text2.Length > 1)
			{
				text2 = "<u>" + text2 + "</u>";
			}
			text = text + LangaugeManager.main.GetTextByKey(StatusEffect.GetNameKeyFromType(this.type)) + ". ";
			text += LangaugeManager.main.GetTextByKey(StatusEffect.GetDescriptionKeyFromType(this.type)).Replace("/x", text2 ?? "");
		}
		else if (this.type == StatusEffect.Type.reactive)
		{
			text = text + LangaugeManager.main.GetTextByKey(StatusEffect.GetNameKeyFromType(this.type)) + ". " + LangaugeManager.main.GetTextByKey(StatusEffect.GetDescriptionKeyFromType(this.type)).Replace("/x", this.value.ToString() ?? "");
			if (this.possibleAttacks.Count > 0)
			{
				text = text.Replace("/y", "<br><br>" + EnemyActionPreview.GetAttackDescription(this.possibleAttacks[0].attacks[0], this.status));
			}
			else
			{
				text = text.Replace("/y", "<br><br>" + EnemyActionPreview.GetAttackDescription(this.possibleAttack.attacks[0], this.status));
			}
		}
		else if (this.type == StatusEffect.Type.Wary)
		{
			text = text + LangaugeManager.main.GetTextByKey(StatusEffect.GetNameKeyFromType(this.type)) + ". " + LangaugeManager.main.GetTextByKey(StatusEffect.GetDescriptionKeyFromType(this.type)).Replace("/x", this.value.ToString() ?? "");
			text = text.Replace("/y", this.secondaryValue.ToString() ?? "");
			if (this.possibleAttacks.Count > 0)
			{
				text = text.Replace("/z", "<br><br>" + EnemyActionPreview.GetAttackDescription(this.possibleAttacks[0].attacks[0], this.status));
			}
			else
			{
				text = text.Replace("/z", "<br><br>" + EnemyActionPreview.GetAttackDescription(this.possibleAttack.attacks[0], this.status));
			}
		}
		else
		{
			text = text + LangaugeManager.main.GetTextByKey(StatusEffect.GetNameKeyFromType(this.type)) + ". " + LangaugeManager.main.GetTextByKey(StatusEffect.GetDescriptionKeyFromType(this.type)).Replace("/x", this.value.ToString() ?? "").Replace("/y", this.secondaryValue.ToString() ?? "");
		}
		return text;
	}

	// Token: 0x060009D6 RID: 2518 RVA: 0x000648C8 File Offset: 0x00062AC8
	public Sprite GetSpriteFromType(StatusEffect.Type type)
	{
		if (this.overrideSprite)
		{
			return this.overrideSprite;
		}
		switch (type)
		{
		case StatusEffect.Type.poison:
			return this.sprites[0];
		case StatusEffect.Type.regen:
			return this.sprites[1];
		case StatusEffect.Type.spikes:
			return this.sprites[2];
		case StatusEffect.Type.rage:
			return this.sprites[3];
		case StatusEffect.Type.weak:
			return this.sprites[4];
		case StatusEffect.Type.haste:
			return this.sprites[5];
		case StatusEffect.Type.slow:
			return this.sprites[6];
		case StatusEffect.Type.freeze:
			return this.sprites[7];
		case StatusEffect.Type.fire:
			return this.sprites[8];
		case StatusEffect.Type.dodge:
			return this.sprites[9];
		case StatusEffect.Type.toughHide:
			return this.sprites[10];
		case StatusEffect.Type.charm:
			return this.sprites[11];
		case StatusEffect.Type.sleep:
			return this.sprites[12];
		case StatusEffect.Type.zombie:
			return this.sprites[13];
		case StatusEffect.Type.curse:
			return this.sprites[14];
		case StatusEffect.Type.exhaust:
			return this.sprites[15];
		case StatusEffect.Type.reactive:
			return this.sprites[16];
		case StatusEffect.Type.Shift:
			return this.sprites[17];
		case StatusEffect.Type.Lonely:
			return this.sprites[18];
		case StatusEffect.Type.Dependent:
			return this.sprites[19];
		case StatusEffect.Type.Angry:
			return this.sprites[20];
		case StatusEffect.Type.Windfall:
			return this.sprites[21];
		case StatusEffect.Type.Wealthy:
			return this.sprites[22];
		case StatusEffect.Type.Stack:
			return this.sprites[23];
		case StatusEffect.Type.Cowardly:
			return this.sprites[24];
		case StatusEffect.Type.Defender:
			return this.sprites[25];
		case StatusEffect.Type.RemoveAllSpikes:
			return this.sprites[26];
		case StatusEffect.Type.Wary:
			return this.sprites[27];
		case StatusEffect.Type.AutoShield:
		case StatusEffect.Type.Sturdy:
			return this.sprites[28];
		}
		return null;
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x00064A8C File Offset: 0x00062C8C
	public static string GetNameKeyFromType(StatusEffect.Type type)
	{
		switch (type)
		{
		case StatusEffect.Type.poison:
			return "se1";
		case StatusEffect.Type.regen:
			return "se2";
		case StatusEffect.Type.spikes:
			return "se3";
		case StatusEffect.Type.rage:
			return "se6";
		case StatusEffect.Type.weak:
			return "se7";
		case StatusEffect.Type.haste:
			return "se4";
		case StatusEffect.Type.slow:
			return "se5";
		case StatusEffect.Type.freeze:
			return "se9";
		case StatusEffect.Type.fire:
			return "se10";
		case StatusEffect.Type.dodge:
			return "se8";
		case StatusEffect.Type.toughHide:
			return "se11";
		case StatusEffect.Type.charm:
			return "se12";
		case StatusEffect.Type.sleep:
			return "se13";
		case StatusEffect.Type.zombie:
			return "se14";
		case StatusEffect.Type.curse:
			return "se16";
		case StatusEffect.Type.exhaust:
			return "se15";
		case StatusEffect.Type.reactive:
			return "se17";
		case StatusEffect.Type.copiesPlayerX:
			return "se18";
		case StatusEffect.Type.Shift:
			return "se19";
		case StatusEffect.Type.Lonely:
			return "se20";
		case StatusEffect.Type.Dependent:
			return "se21";
		case StatusEffect.Type.Angry:
			return "se22";
		case StatusEffect.Type.Windfall:
			return "se23";
		case StatusEffect.Type.Wealthy:
			return "se24";
		case StatusEffect.Type.Stack:
			return "se25";
		case StatusEffect.Type.Cowardly:
			return "enemyC";
		case StatusEffect.Type.Defender:
			return "enemyB";
		case StatusEffect.Type.RemoveAllSpikes:
			return "se26";
		case StatusEffect.Type.Wary:
			return "se27";
		case StatusEffect.Type.AutoShield:
			return "se28";
		case StatusEffect.Type.Sturdy:
			return "se29";
		default:
			return "";
		}
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x00064BDF File Offset: 0x00062DDF
	public static string GetDescriptionKeyFromType(StatusEffect.Type type)
	{
		return StatusEffect.GetNameKeyFromType(type) + "d";
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x00064BF1 File Offset: 0x00062DF1
	public override void OnCursorEnd()
	{
		this.RemoveCard();
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x00064BF9 File Offset: 0x00062DF9
	private void OnMouseExit()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.RemoveCard();
	}

	// Token: 0x060009DB RID: 2523 RVA: 0x00064C0E File Offset: 0x00062E0E
	private void RemoveCard()
	{
		if (this.previewCard)
		{
			Object.Destroy(this.previewCard);
		}
		this.timeToDisplayCard = 0f;
	}

	// Token: 0x04000800 RID: 2048
	public static List<StatusEffect> allStatusEffects = new List<StatusEffect>();

	// Token: 0x04000801 RID: 2049
	[SerializeField]
	public RuntimeAnimatorController animatorControllerToApplyToEnemyIfReacted;

	// Token: 0x04000802 RID: 2050
	[SerializeField]
	private GameObject border;

	// Token: 0x04000803 RID: 2051
	[SerializeField]
	public Image mainSpriteRenderer;

	// Token: 0x04000804 RID: 2052
	[SerializeField]
	private Sprite overrideSprite;

	// Token: 0x04000805 RID: 2053
	[SerializeField]
	private Sprite[] sprites;

	// Token: 0x04000806 RID: 2054
	public StatusEffect.Type type;

	// Token: 0x04000807 RID: 2055
	public int value;

	// Token: 0x04000808 RID: 2056
	public int secondaryValue;

	// Token: 0x04000809 RID: 2057
	[SerializeField]
	private GameObject cardPrefab;

	// Token: 0x0400080A RID: 2058
	private GameObject previewCard;

	// Token: 0x0400080B RID: 2059
	private float timeToDisplayCard;

	// Token: 0x0400080C RID: 2060
	public string displayName = "Attack";

	// Token: 0x0400080D RID: 2061
	[HideInInspector]
	public Animator animator;

	// Token: 0x0400080E RID: 2062
	public bool isNumeric = true;

	// Token: 0x0400080F RID: 2063
	public Enemy.PossibleAttack possibleAttack;

	// Token: 0x04000810 RID: 2064
	public List<Enemy.PossibleAttack> possibleAttacks = new List<Enemy.PossibleAttack>();

	// Token: 0x04000811 RID: 2065
	[SerializeField]
	public List<GameObject> enemyScriptsRelated;

	// Token: 0x04000812 RID: 2066
	public StatusEffect.DecreasesTime decreasesTimeType = StatusEffect.DecreasesTime.endOfTurn;

	// Token: 0x04000813 RID: 2067
	private Status status;

	// Token: 0x04000814 RID: 2068
	public StatusEffect.OnRemoveStack onRemoveStack;

	// Token: 0x02000392 RID: 914
	[Serializable]
	public class StatusEffectToAdd
	{
		// Token: 0x06001758 RID: 5976 RVA: 0x000C5028 File Offset: 0x000C3228
		public StatusEffectToAdd(StatusEffect.StatusEffectToAdd.Target _target, StatusEffect.Type _statusEffectType, int _value)
		{
			this.target = _target;
			this.statusEffectType = _statusEffectType;
			this.value = _value;
			this.startingValue = this.value;
		}

		// Token: 0x0400156C RID: 5484
		public StatusEffect.StatusEffectToAdd.Target target;

		// Token: 0x0400156D RID: 5485
		public StatusEffect.Type statusEffectType;

		// Token: 0x0400156E RID: 5486
		public int value;

		// Token: 0x0400156F RID: 5487
		public StatusEffect.StatusEffectToAdd.Useage useage;

		// Token: 0x04001570 RID: 5488
		[HideInInspector]
		public int startingValue;

		// Token: 0x020004B4 RID: 1204
		public enum Target
		{
			// Token: 0x04001C3B RID: 7227
			player,
			// Token: 0x04001C3C RID: 7228
			enemy,
			// Token: 0x04001C3D RID: 7229
			allEnemies
		}

		// Token: 0x020004B5 RID: 1205
		public enum Useage
		{
			// Token: 0x04001C3F RID: 7231
			active,
			// Token: 0x04001C40 RID: 7232
			passive
		}
	}

	// Token: 0x02000393 RID: 915
	public enum Type
	{
		// Token: 0x04001572 RID: 5490
		poison,
		// Token: 0x04001573 RID: 5491
		regen,
		// Token: 0x04001574 RID: 5492
		spikes,
		// Token: 0x04001575 RID: 5493
		rage,
		// Token: 0x04001576 RID: 5494
		weak,
		// Token: 0x04001577 RID: 5495
		haste,
		// Token: 0x04001578 RID: 5496
		slow,
		// Token: 0x04001579 RID: 5497
		freeze,
		// Token: 0x0400157A RID: 5498
		fire,
		// Token: 0x0400157B RID: 5499
		dodge,
		// Token: 0x0400157C RID: 5500
		toughHide,
		// Token: 0x0400157D RID: 5501
		charm,
		// Token: 0x0400157E RID: 5502
		sleep,
		// Token: 0x0400157F RID: 5503
		zombie,
		// Token: 0x04001580 RID: 5504
		curse,
		// Token: 0x04001581 RID: 5505
		exhaust,
		// Token: 0x04001582 RID: 5506
		reactive,
		// Token: 0x04001583 RID: 5507
		copiesPlayerX,
		// Token: 0x04001584 RID: 5508
		Shift,
		// Token: 0x04001585 RID: 5509
		Lonely,
		// Token: 0x04001586 RID: 5510
		Dependent,
		// Token: 0x04001587 RID: 5511
		Angry,
		// Token: 0x04001588 RID: 5512
		Windfall,
		// Token: 0x04001589 RID: 5513
		Wealthy,
		// Token: 0x0400158A RID: 5514
		Stack,
		// Token: 0x0400158B RID: 5515
		Cowardly,
		// Token: 0x0400158C RID: 5516
		Defender,
		// Token: 0x0400158D RID: 5517
		RemoveAllSpikes,
		// Token: 0x0400158E RID: 5518
		Wary,
		// Token: 0x0400158F RID: 5519
		AutoShield,
		// Token: 0x04001590 RID: 5520
		Sturdy
	}

	// Token: 0x02000394 RID: 916
	public enum DecreasesTime
	{
		// Token: 0x04001592 RID: 5522
		startOfTurn,
		// Token: 0x04001593 RID: 5523
		endOfTurn,
		// Token: 0x04001594 RID: 5524
		never,
		// Token: 0x04001595 RID: 5525
		remainsEvenAfterCombat,
		// Token: 0x04001596 RID: 5526
		clearsFullyAtEndOfTurn,
		// Token: 0x04001597 RID: 5527
		clearsFullyAtStartOfTurn
	}

	// Token: 0x02000395 RID: 917
	// (Invoke) Token: 0x0600175A RID: 5978
	public delegate void OnRemoveStack();
}
