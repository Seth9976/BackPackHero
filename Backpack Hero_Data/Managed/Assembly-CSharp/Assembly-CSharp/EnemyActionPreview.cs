using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B4 RID: 180
public class EnemyActionPreview : CustomInputHandler
{
	// Token: 0x060004E0 RID: 1248 RVA: 0x0002EFD9 File Offset: 0x0002D1D9
	private void Awake()
	{
		if (EnemyActionPreview.enemyActionPreviews == null)
		{
			EnemyActionPreview.enemyActionPreviews = new List<EnemyActionPreview>();
		}
		EnemyActionPreview.enemyActionPreviews.Add(this);
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x0002EFF7 File Offset: 0x0002D1F7
	private void OnDestroy()
	{
		this.RemoveCard();
		EnemyActionPreview.enemyActionPreviews.Remove(this);
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x0002F00B File Offset: 0x0002D20B
	private void Start()
	{
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x0002F010 File Offset: 0x0002D210
	private void Update()
	{
		if (!this.enemy)
		{
			Object.Destroy(base.gameObject);
		}
		if (this.enemy && this.imageTrans && this.myAttackReference != null && this.myAttackReference.type == Enemy.Attack.Type.attack)
		{
			this.imageTrans.transform.localScale = new Vector3(this.enemy.transform.localScale.x, 1f, 1f);
			return;
		}
		this.imageTrans.transform.localScale = Vector3.one;
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x0002F0AE File Offset: 0x0002D2AE
	private void OnMouseOver()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.ShowCard();
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x0002F0C3 File Offset: 0x0002D2C3
	public override void OnCursorHold()
	{
		this.ShowCard();
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x0002F0CB File Offset: 0x0002D2CB
	private void OnMouseExit()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.RemoveCard();
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x0002F0E0 File Offset: 0x0002D2E0
	public override void OnCursorEnd()
	{
		this.RemoveCard();
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x0002F0E8 File Offset: 0x0002D2E8
	private void ShowCard()
	{
		if (Input.GetMouseButton(0))
		{
			this.RemoveCard();
			return;
		}
		if (SingleUI.IsViewingPopUp() && !base.transform.IsChildOf(SingleUI.GetSingleUI().transform))
		{
			return;
		}
		this.timeToDisplayCard += Time.deltaTime;
		if (this.timeToDisplayCard > 0.3f && !this.previewCard)
		{
			if (this.myAttackReference.type == Enemy.Attack.Type.hazard && this.myAttackReference.prefabs.Count > 0)
			{
				this.previewCard = Object.Instantiate<GameObject>(this.cardForHazards, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").GetComponent<Canvas>().transform);
				Card component = this.previewCard.GetComponent<Card>();
				ItemMovement component2 = this.myAttackReference.prefabs[0].GetComponent<ItemMovement>();
				component2.ApplyCardToItem(this.previewCard, component2.GetComponent<Item2>(), null, false);
				component.deleteOnDeactivate = false;
				return;
			}
			this.previewCard = Object.Instantiate<GameObject>(this.cardPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").GetComponent<Canvas>().transform);
			this.previewCard.GetComponent<Card>().GetDescriptions(this, base.gameObject);
		}
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x0002F229 File Offset: 0x0002D429
	private void RemoveCard()
	{
		if (this.previewCard)
		{
			Object.Destroy(this.previewCard);
		}
		this.timeToDisplayCard = 0f;
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x0002F250 File Offset: 0x0002D450
	public static string GetAttackPower(Enemy.Attack attack, bool doOverride = false, int valueOverride = 0)
	{
		if (attack == null)
		{
			return "?";
		}
		if (attack.powerIsSet && doOverride)
		{
			return valueOverride.ToString() ?? "";
		}
		if (attack.powerIsSet)
		{
			return attack.currentPower.ToString() ?? "";
		}
		if (attack.powerRange.x == attack.powerRange.y)
		{
			return attack.powerRange.x.ToString() ?? "";
		}
		return attack.powerRange.x.ToString() + " - " + attack.powerRange.y.ToString();
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x0002F2FC File Offset: 0x0002D4FC
	public static string GetAttackDescription(Enemy.Attack attack, Status stats)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		if (stats != null)
		{
			num = stats.GetStatusEffectValue(StatusEffect.Type.rage);
			num2 = stats.GetStatusEffectValue(StatusEffect.Type.weak);
			num = Mathf.Max(num, 0);
			num2 = Mathf.Max(num2, 0);
			num3 = stats.GetStatusEffectValue(StatusEffect.Type.haste);
			num4 = stats.GetStatusEffectValue(StatusEffect.Type.slow);
			num3 = Mathf.Max(num3, 0);
			num4 = Mathf.Max(num4, 0);
		}
		if (attack != null)
		{
			int num5 = attack.currentPower;
		}
		string text = EnemyActionPreview.GetAttackPower(attack, false, 0);
		string text2 = "";
		if (attack.type == Enemy.Attack.Type.attack)
		{
			int num5 = Mathf.Max(attack.currentPower + num + num2 * -1, 0);
			text = EnemyActionPreview.GetAttackPower(attack, true, num5);
			if (attack.target == Item2.Effect.Target.allEnemies)
			{
				text2 = LangaugeManager.main.GetTextByKey("ea1a").Replace("/x", text ?? "");
			}
			else
			{
				text2 = LangaugeManager.main.GetTextByKey("ea1").Replace("/x", text ?? "");
			}
		}
		else if (attack.type == Enemy.Attack.Type.block)
		{
			int num5 = Mathf.Max(attack.currentPower + num3 + num4 * -1, 0);
			text = EnemyActionPreview.GetAttackPower(attack, true, num5);
			if (attack.target == Item2.Effect.Target.allEnemies)
			{
				text2 = LangaugeManager.main.GetTextByKey("ea2a").Replace("/x", text ?? "");
			}
			else
			{
				text2 = LangaugeManager.main.GetTextByKey("ea2").Replace("/x", text ?? "");
			}
		}
		else if (attack.type == Enemy.Attack.Type.heal)
		{
			if (attack.target == Item2.Effect.Target.allEnemies)
			{
				text2 = LangaugeManager.main.GetTextByKey("ea3a").Replace("/x", attack.currentPower.ToString() ?? "");
			}
			else
			{
				text2 = LangaugeManager.main.GetTextByKey("ea3").Replace("/x", attack.currentPower.ToString() ?? "");
			}
		}
		else if (attack.type == Enemy.Attack.Type.pass)
		{
			if (attack.target == Item2.Effect.Target.allEnemies)
			{
				text2 = LangaugeManager.main.GetTextByKey("ea4a").Replace("/x", attack.currentPower.ToString() ?? "");
			}
			else
			{
				text2 = LangaugeManager.main.GetTextByKey("ea4").Replace("/x", attack.currentPower.ToString() ?? "");
			}
		}
		else if (attack.type == Enemy.Attack.Type.poison)
		{
			if (attack.target == Item2.Effect.Target.allEnemies)
			{
				text2 = LangaugeManager.main.GetTextByKey("ea5a").Replace("/x", attack.currentPower.ToString() ?? "");
			}
			else
			{
				text2 = LangaugeManager.main.GetTextByKey("ea5").Replace("/x", attack.currentPower.ToString() ?? "");
			}
		}
		else if (attack.type == Enemy.Attack.Type.burn)
		{
			if (attack.target == Item2.Effect.Target.allEnemies)
			{
				text2 = LangaugeManager.main.GetTextByKey("ea6a").Replace("/x", attack.currentPower.ToString() ?? "");
			}
			else
			{
				text2 = LangaugeManager.main.GetTextByKey("ea6").Replace("/x", attack.currentPower.ToString() ?? "");
			}
		}
		else if (attack.type == Enemy.Attack.Type.regen)
		{
			if (attack.target == Item2.Effect.Target.allEnemies)
			{
				text2 = LangaugeManager.main.GetTextByKey("ea7a").Replace("/x", attack.currentPower.ToString() ?? "");
			}
			else
			{
				text2 = LangaugeManager.main.GetTextByKey("ea7").Replace("/x", attack.currentPower.ToString() ?? "");
			}
		}
		else if (attack.type == Enemy.Attack.Type.spikes)
		{
			if (attack.target == Item2.Effect.Target.allEnemies)
			{
				text2 = LangaugeManager.main.GetTextByKey("ea8a").Replace("/x", attack.currentPower.ToString() ?? "");
			}
			else
			{
				text2 = LangaugeManager.main.GetTextByKey("ea8").Replace("/x", attack.currentPower.ToString() ?? "");
			}
		}
		else if (attack.type == Enemy.Attack.Type.dodge)
		{
			if (attack.target == Item2.Effect.Target.allEnemies)
			{
				text2 = LangaugeManager.main.GetTextByKey("ea9a").Replace("/x", text ?? "");
			}
			else
			{
				text2 = LangaugeManager.main.GetTextByKey("ea9").Replace("/x", text ?? "");
			}
		}
		else if (attack.type == Enemy.Attack.Type.slow)
		{
			if (attack.target == Item2.Effect.Target.allEnemies)
			{
				text2 = LangaugeManager.main.GetTextByKey("ea10a").Replace("/x", attack.currentPower.ToString() ?? "");
			}
			else
			{
				text2 = LangaugeManager.main.GetTextByKey("ea10").Replace("/x", attack.currentPower.ToString() ?? "");
			}
		}
		else if (attack.type == Enemy.Attack.Type.weak)
		{
			if (attack.target == Item2.Effect.Target.allEnemies)
			{
				text2 = LangaugeManager.main.GetTextByKey("ea11a").Replace("/x", attack.currentPower.ToString() ?? "");
			}
			else
			{
				text2 = LangaugeManager.main.GetTextByKey("ea11").Replace("/x", attack.currentPower.ToString() ?? "");
			}
		}
		else if (attack.type == Enemy.Attack.Type.haste)
		{
			if (attack.target == Item2.Effect.Target.allEnemies)
			{
				text2 = LangaugeManager.main.GetTextByKey("ea16a").Replace("/x", attack.currentPower.ToString() ?? "");
			}
			else
			{
				text2 = LangaugeManager.main.GetTextByKey("ea16").Replace("/x", attack.currentPower.ToString() ?? "");
			}
		}
		else if (attack.type == Enemy.Attack.Type.rage)
		{
			if (attack.target == Item2.Effect.Target.allEnemies)
			{
				text2 = LangaugeManager.main.GetTextByKey("ea17a").Replace("/x", attack.currentPower.ToString() ?? "");
			}
			else
			{
				text2 = LangaugeManager.main.GetTextByKey("ea17").Replace("/x", attack.currentPower.ToString() ?? "");
			}
		}
		else if (attack.type == Enemy.Attack.Type.summon)
		{
			text2 = LangaugeManager.main.GetTextByKey("ea12");
		}
		else if (attack.type == Enemy.Attack.Type.vampire)
		{
			if (attack.target == Item2.Effect.Target.allEnemies)
			{
				text2 = LangaugeManager.main.GetTextByKey("ea13a").Replace("/x", attack.currentPower.ToString() ?? "");
			}
			else
			{
				text2 = LangaugeManager.main.GetTextByKey("ea13").Replace("/x", attack.currentPower.ToString() ?? "");
			}
		}
		else if (attack.type == Enemy.Attack.Type.selfDestruct)
		{
			text2 = LangaugeManager.main.GetTextByKey("ea15").Replace("/x", attack.currentPower.ToString() ?? "");
		}
		else if (attack.type == Enemy.Attack.Type.poisonSickness)
		{
			text2 = LangaugeManager.main.GetTextByKey("ea18") + " " + LangaugeManager.main.GetTextByKey("ea4");
		}
		else if (attack.type == Enemy.Attack.Type.runAway)
		{
			text2 = LangaugeManager.main.GetTextByKey("ea19");
		}
		else if (attack.type == Enemy.Attack.Type.steal)
		{
			text2 = LangaugeManager.main.GetTextByKey("ea20").Replace("/x", attack.currentPower.ToString() ?? "");
		}
		else if (attack.type != Enemy.Attack.Type.hazard)
		{
			if (attack.type == Enemy.Attack.Type.freeze)
			{
				text2 = LangaugeManager.main.GetTextByKey("ea21").Replace("/x", attack.currentPower.ToString() ?? "");
			}
			else if (attack.type == Enemy.Attack.Type.curseStatus)
			{
				text2 = LangaugeManager.main.GetTextByKey("ea14").Replace("/x", attack.currentPower.ToString() ?? "");
			}
			else if (attack.type == Enemy.Attack.Type.addStatusEffect)
			{
				if (attack.target == Item2.Effect.Target.allEnemies)
				{
					text2 = LangaugeManager.main.GetTextByKey("eaStatusEffecta");
				}
				else
				{
					text2 = LangaugeManager.main.GetTextByKey("eaStatusEffect");
				}
				if (attack.isNumeric)
				{
					text2 = text2.Replace("/x", attack.currentPower.ToString() ?? "");
				}
				else
				{
					text2 = text2.Replace(" /x", "");
				}
				text2 += "<br><br>";
				if (attack.prefabs.Count > 0)
				{
					StatusEffect component = attack.prefabs[0].GetComponent<StatusEffect>();
					text2 += LangaugeManager.main.GetTextByKey(StatusEffect.GetNameKeyFromType(component.type));
					text2 = text2 + ". " + LangaugeManager.main.GetTextByKey(StatusEffect.GetDescriptionKeyFromType(component.type));
					text2 = text2.Replace("/y", "<br><br>" + EnemyActionPreview.GetAttackDescription(component.possibleAttack.attacks[0], stats));
				}
			}
		}
		return text2;
	}

	// Token: 0x040003AA RID: 938
	public static List<EnemyActionPreview> enemyActionPreviews = new List<EnemyActionPreview>();

	// Token: 0x040003AB RID: 939
	[SerializeField]
	public Enemy enemy;

	// Token: 0x040003AC RID: 940
	private float timeToDisplayCard;

	// Token: 0x040003AD RID: 941
	[SerializeField]
	private GameObject cardPrefab;

	// Token: 0x040003AE RID: 942
	[SerializeField]
	private GameObject cardForHazards;

	// Token: 0x040003AF RID: 943
	[SerializeField]
	private Transform imageTrans;

	// Token: 0x040003B0 RID: 944
	private GameObject previewCard;

	// Token: 0x040003B1 RID: 945
	public string displayName = "";

	// Token: 0x040003B2 RID: 946
	public Enemy.Attack myAttackReference;
}
