using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020000B3 RID: 179
public class Enemy : CustomInputHandler
{
	// Token: 0x0600049F RID: 1183 RVA: 0x0002C26C File Offset: 0x0002A46C
	public static void AllEnemiesNextTurnStarts()
	{
		foreach (Enemy enemy in Enemy.allEnemies)
		{
			if (enemy && enemy.gameObject.activeSelf && enemy.stats && !enemy.dead)
			{
				enemy.NextTurnStarts();
			}
		}
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x0002C2E8 File Offset: 0x0002A4E8
	public void NextTurnStarts()
	{
		if (!base.gameObject || !this.stats || this.dead)
		{
			return;
		}
		foreach (GameObject gameObject in this.statusEffectsToGainEachTurnPrefabs)
		{
			this.stats.AddStatusEffectFromPrefab(gameObject, false, 0);
		}
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x0002C368 File Offset: 0x0002A568
	public static bool EnemiesAlive()
	{
		foreach (Enemy enemy in Enemy.allEnemies)
		{
			if (enemy.gameObject.activeSelf && !enemy.dead)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x0002C3D0 File Offset: 0x0002A5D0
	public static int EnemiesAliveCount()
	{
		int num = 0;
		foreach (Enemy enemy in Enemy.allEnemies)
		{
			if (enemy.gameObject.activeSelf && !enemy.dead)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x0002C438 File Offset: 0x0002A638
	public static List<Enemy> GetEnemiesAlive()
	{
		List<Enemy> list = new List<Enemy>();
		foreach (Enemy enemy in Enemy.allEnemies)
		{
			if (enemy.gameObject.activeSelf && !enemy.dead)
			{
				list.Add(enemy);
			}
		}
		return list;
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x0002C4A8 File Offset: 0x0002A6A8
	public void ChangeAnimator(RuntimeAnimatorController animator)
	{
		if (!this.spriteAnimator)
		{
			return;
		}
		this.spriteAnimator.runtimeAnimatorController = animator;
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x0002C4C4 File Offset: 0x0002A6C4
	public bool ConsiderSwap(Enemy.EnemySwap.Condition condition)
	{
		foreach (Enemy.EnemySwap enemySwap in this.enemySwaps)
		{
			if (enemySwap.condition == condition)
			{
				this.spriteAnimator.runtimeAnimatorController = enemySwap.newEnemy.spriteAnimator.runtimeAnimatorController;
				this.possibleAttacks = enemySwap.newEnemy.possibleAttacks;
				this.actionNum = 0;
				this.boxCollider2D.size = enemySwap.newEnemy.GetComponent<BoxCollider2D>().size;
				this.boxCollider2D.offset = enemySwap.newEnemy.GetComponent<BoxCollider2D>().offset;
				base.transform.position = new Vector3(base.transform.position.x, -4.8f + this.boxCollider2D.size.y / 2f, base.transform.position.z);
				if (GameManager.main.targetedEnemy == this)
				{
					GameManager.main.SelectEnemy(this);
				}
				if (enemySwap.statusMigrationStyle == Enemy.EnemySwap.StatusMigrationStyle.resetHealthKeepStatusConditions)
				{
					this.stats.health = 1;
					this.stats.SetMaxHP(enemySwap.newEnemy.health);
					this.stats.SetHealth(enemySwap.newEnemy.health);
				}
				else if (enemySwap.statusMigrationStyle == Enemy.EnemySwap.StatusMigrationStyle.resetHealthKeepStatusConditionsButNotPowers)
				{
					this.stats.health = 1;
					this.stats.SetMaxHP(enemySwap.newEnemy.health);
					this.stats.SetHealth(enemySwap.newEnemy.health);
					this.stats.RemoveAllPowers();
				}
				else if (enemySwap.statusMigrationStyle == Enemy.EnemySwap.StatusMigrationStyle.keepHealthPercentageKeepStatusConditions)
				{
					float num = (float)(this.stats.health / this.stats.maxHealth);
					this.stats.health = 1;
					this.stats.SetMaxHP(enemySwap.newEnemy.health);
					this.stats.SetHealth(Mathf.RoundToInt((float)this.stats.maxHealth * num));
				}
				if (enemySwap.doParticles)
				{
					this.transformParticles.SetActive(true);
				}
				foreach (Enemy.PossibleAttack possibleAttack in this.possibleAttacks)
				{
					possibleAttack.currentCoolDown = 0;
				}
				base.StartCoroutine(this.OnDeathEffects());
				base.StartCoroutine(this.SetIntention());
				foreach (GameObject gameObject in enemySwap.newEnemy.statusEffectsToStartWithPrefabs)
				{
					this.stats.AddStatusEffectFromPrefab(gameObject, false, 0);
				}
				this.statusEffectsToGainEachTurnPrefabs = enemySwap.newEnemy.statusEffectsToGainEachTurnPrefabs;
				this.onDeathEffects = enemySwap.newEnemy.onDeathEffects;
				this.enemySwaps = enemySwap.newEnemy.enemySwaps;
				return true;
			}
		}
		return false;
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x0002C808 File Offset: 0x0002AA08
	private void ApplyAlternateOutfit(Enemy.AlternateOutfitAnimator a)
	{
		this.spriteAnimator.runtimeAnimatorController = a.animator;
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x0002C81C File Offset: 0x0002AA1C
	public bool CheckForProperty(Enemy.EnemyProperty.Type type)
	{
		if (type == Enemy.EnemyProperty.Type.cowardly && this.stats.GetStatusEffectOfType(StatusEffect.Type.Cowardly))
		{
			return true;
		}
		if (type == Enemy.EnemyProperty.Type.blocking && this.stats.GetStatusEffectOfType(StatusEffect.Type.Defender))
		{
			return true;
		}
		using (List<Enemy.EnemyProperty>.Enumerator enumerator = this.enemyProperties.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.type == type)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x0002C8AC File Offset: 0x0002AAAC
	public void AddProperty(Enemy.EnemyProperty.Type type)
	{
		using (List<Enemy.EnemyProperty>.Enumerator enumerator = this.enemyProperties.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.type == type)
				{
					return;
				}
			}
		}
		Enemy.EnemyProperty enemyProperty = new Enemy.EnemyProperty();
		enemyProperty.type = type;
		this.enemyProperties.Add(enemyProperty);
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x0002C91C File Offset: 0x0002AB1C
	public static string GetRealName(string name)
	{
		string text;
		if (name.IndexOf("(") == -1)
		{
			text = name;
		}
		else
		{
			text = name.Substring(0, name.IndexOf("("));
		}
		if (text.Contains("variant") || text.Contains("Variant"))
		{
			text = text.Substring(0, Mathf.Max(text.IndexOf("variant"), text.IndexOf("Variant")));
		}
		return text;
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x0002C98C File Offset: 0x0002AB8C
	public static Enemy GetFrontMostEnemy()
	{
		if (Enemy.allEnemies.Count == 0)
		{
			return null;
		}
		Enemy enemy = Enemy.allEnemies[0];
		foreach (Enemy enemy2 in Enemy.allEnemies)
		{
			if (enemy2.transform.position.x < enemy.transform.position.x)
			{
				enemy = enemy2;
			}
		}
		return enemy;
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x0002CA18 File Offset: 0x0002AC18
	public static Enemy GetBackmostEnemy()
	{
		if (Enemy.allEnemies.Count == 0)
		{
			return null;
		}
		Enemy enemy = Enemy.allEnemies[0];
		foreach (Enemy enemy2 in Enemy.allEnemies)
		{
			if (enemy2.transform.position.x > enemy.transform.position.x)
			{
				enemy = enemy2;
			}
		}
		return enemy;
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x0002CAA4 File Offset: 0x0002ACA4
	private void OnEnable()
	{
		if (!Enemy.allEnemies.Contains(this))
		{
			Enemy.allEnemies.Add(this);
		}
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x0002CABE File Offset: 0x0002ACBE
	private void OnDisable()
	{
		Enemy.allEnemies.Remove(this);
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x0002CACC File Offset: 0x0002ACCC
	private void OnDestroy()
	{
		if (this.mySpacerLocation)
		{
			Object.Destroy(this.mySpacerLocation.gameObject);
		}
		Enemy.allEnemies.Remove(this);
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x0002CAF8 File Offset: 0x0002ACF8
	private void Start()
	{
		if (MetaProgressSaveManager.ConditionsMet(this.conditionsForCreateEncounter) && this.objectToCreateOnEncounter)
		{
			Object.Instantiate<GameObject>(this.objectToCreateOnEncounter, base.transform.position, Quaternion.identity);
		}
		MetaProgressSaveManager.main.AddMetaProgressMarker(this.metaProgressMarkerOnMeeting);
		if (GameFlowManager.main && this.metaProgressMarkerOnDefeating != MetaProgressSaveManager.MetaProgressMarker.none)
		{
			GameFlowManager.main.AddMarker(this.metaProgressMarkerOnDefeating);
		}
		this.possibleAttacksStart = new List<Enemy.PossibleAttack>(this.possibleAttacks);
		this.combatPet = base.GetComponent<CombatPet>();
		if (!Enemy.allEnemies.Contains(this))
		{
			Enemy.allEnemies.Add(this);
		}
		foreach (Enemy.AlternateOutfitAnimator alternateOutfitAnimator in this.alternateOutfitAnimators)
		{
			if (alternateOutfitAnimator.type == Enemy.AlternateOutfitAnimator.Type.randomChance && Random.Range(0f, 100f) <= alternateOutfitAnimator.value)
			{
				this.ApplyAlternateOutfit(alternateOutfitAnimator);
			}
		}
		this.gameManager = GameManager.main;
		this.gameFlowManager = GameFlowManager.main;
		this.actionNum = 0;
		if (!this.isPet)
		{
			this.mySpacerLocation = Object.Instantiate<GameObject>(this.enemySpacerPrefab, base.transform.position, Quaternion.identity).transform;
			this.mySpacerLocation.SetParent(GameObject.FindGameObjectWithTag("EnemySpacerParent").transform);
			this.mySpacerLocation.SetAsFirstSibling();
			if (this.isSummon)
			{
				this.mySpacerLocation.GetComponent<UICarvingSpacer>().CreateWidth(250f);
			}
			base.transform.position = new Vector3(GameObject.FindGameObjectWithTag("EnemySpacerParent").transform.position.x, base.transform.position.y, base.transform.position.z);
			if (!this.ignoreOrderAndAlwaysDoActionsInOrder)
			{
				int num = 0;
				using (List<Enemy>.Enumerator enumerator2 = Enemy.allEnemies.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Enemy enemy = enumerator2.Current;
						if (enemy.transform.position.x < base.transform.position.x && enemy != this)
						{
							num++;
						}
					}
					goto IL_0267;
				}
				IL_023B:
				this.actionNum++;
				num--;
				if (this.actionNum >= this.possibleAttacks.Count)
				{
					this.actionNum = 0;
				}
				IL_0267:
				if (num > 0)
				{
					goto IL_023B;
				}
			}
		}
		this.player = Player.main;
		this.MakeReferences();
		this.startFlipped = this.spriteRenderer.flipX;
		if (this.gameManager.floor > this.gameManager.floorsPerLevel * 3 && this.gameManager.dungeonLevel.zone != DungeonLevel.Zone.Chaos)
		{
			this.health = Mathf.RoundToInt((float)this.health * ((float)(this.gameManager.floor - this.gameManager.floorsPerLevel * 3 + 1) * 2f));
		}
		RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.increaseEnemyHealth);
		if (runProperty != null)
		{
			this.health = Mathf.RoundToInt((float)this.health * ((100f + (float)runProperty.value) / 100f));
		}
		runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.increaseXP);
		if (runProperty != null)
		{
			this.xp = Mathf.RoundToInt((float)this.xp * ((100f + (float)runProperty.value) / 100f));
		}
		if (this.setHealthToStats)
		{
			this.stats.health = this.health;
			this.stats.maxHealth = this.health;
		}
		this.boxCollider2D = base.GetComponent<BoxCollider2D>();
		this.enemyIntentParent.gameObject.SetActive(true);
		this.enemyIntentParent.SetParent(GameObject.FindGameObjectWithTag("EnemyIntentParent").transform);
		this.enemyIntentParent.localScale = Vector3.one;
		foreach (Item2.CombattEffect combattEffect in this.combattEffects)
		{
			if (combattEffect.trigger.types.Count == 0)
			{
				combattEffect.trigger.types = new List<Item2.ItemType> { Item2.ItemType.Any };
			}
			if (combattEffect.trigger.areas.Count == 0)
			{
				combattEffect.trigger.areas = new List<Item2.Area> { Item2.Area.self };
			}
		}
		foreach (GameObject gameObject in this.statusEffectsToStartWithPrefabs)
		{
			this.stats.AddStatusEffectFromPrefab(gameObject, false, 0);
		}
		base.StartCoroutine(this.RandomizeAnimation());
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x0002CFD0 File Offset: 0x0002B1D0
	public void PlayAnimation(string x)
	{
		if (!this.positionAnimator)
		{
			return;
		}
		this.positionAnimator.Play(x, 0, 0f);
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x0002CFF4 File Offset: 0x0002B1F4
	public bool HasCurrentIntentType(Enemy.Attack.Type type)
	{
		if (this.myNextAttack == null)
		{
			return false;
		}
		using (List<Enemy.Attack>.Enumerator enumerator = this.myNextAttack.attacks.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.type == type)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x0002D060 File Offset: 0x0002B260
	private void SwitchToAlternate()
	{
		this.actionNum = 0;
		if (this.alternateAnimator && this.spriteAnimator)
		{
			this.positionAnimator.Play("enemyPos_buff", 0, 0f);
			this.spriteAnimator.runtimeAnimatorController = this.alternateAnimator;
		}
		this.isPerformingAlternateAttacks = true;
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x0002D0BC File Offset: 0x0002B2BC
	public void ConsiderIntentSwapFeedbackMicrophone()
	{
		if (this.myOverrideIntention == null)
		{
			this.myOverrideIntention = this.myNextAttack.Clone();
		}
		foreach (Enemy.Attack attack in this.myOverrideIntention.attacks)
		{
			if (attack.type == Enemy.Attack.Type.attack && attack.target == Item2.Effect.Target.player)
			{
				attack.sprite = this.blockSprite;
				attack.type = Enemy.Attack.Type.block;
				attack.target = Item2.Effect.Target.enemy;
				attack.attackAnimation = Enemy.Attack.AttackAnimation.buff;
			}
			else if (attack.type == Enemy.Attack.Type.block && (attack.target == Item2.Effect.Target.enemy || attack.target == Item2.Effect.Target.allEnemies))
			{
				attack.sprite = this.attackSprite;
				attack.type = Enemy.Attack.Type.attack;
				attack.target = Item2.Effect.Target.player;
				attack.attackAnimation = Enemy.Attack.AttackAnimation.attack;
			}
		}
		this.CreateIntentionObject();
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x0002D1A0 File Offset: 0x0002B3A0
	public IEnumerator ConsiderActionResponse(Item2.Trigger.ActionTrigger trigger, List<Status> statsEffected, List<Item2.ItemType> itemTypes)
	{
		if (this.dead || !this.stats)
		{
			yield break;
		}
		if ((trigger == Item2.Trigger.ActionTrigger.whenAttacked || trigger == Item2.Trigger.ActionTrigger.onTakeDamage) && (statsEffected == null || statsEffected.Count == 0 || !statsEffected.Contains(this.stats)))
		{
			yield break;
		}
		int statusEffectValue = this.stats.GetStatusEffectValue(StatusEffect.Type.Angry);
		if (trigger == Item2.Trigger.ActionTrigger.onTakeDamage && statsEffected.Contains(this.stats) && statusEffectValue > -1)
		{
			this.stats.AddStatusEffect(StatusEffect.Type.rage, (float)statusEffectValue, Item2.Effect.MathematicalType.summative);
		}
		int statusEffectValue2 = this.stats.GetStatusEffectValue(StatusEffect.Type.copiesPlayerX);
		if (statusEffectValue2 > -1 && !this.HasCurrentIntentType(Enemy.Attack.Type.attack) && ((trigger == Item2.Trigger.ActionTrigger.onUse && itemTypes.Contains(Item2.ItemType.Weapon)) || trigger == Item2.Trigger.ActionTrigger.onScratch))
		{
			this.ChangeIntentionPerm(Enemy.Attack.Type.attack, Enemy.Attack.AttackAnimation.attack, new Vector2((float)(statusEffectValue2 - 2), (float)(statusEffectValue2 + 2)));
		}
		else if (trigger == Item2.Trigger.ActionTrigger.onUse && statusEffectValue2 > -1 && itemTypes.Contains(Item2.ItemType.Shield) && !this.HasCurrentIntentType(Enemy.Attack.Type.block))
		{
			this.ChangeIntentionPerm(Enemy.Attack.Type.block, Enemy.Attack.AttackAnimation.buff, new Vector2((float)(statusEffectValue2 - 2), (float)(statusEffectValue2 + 2)));
		}
		int num = this.stats.GetStatusEffectValue(StatusEffect.Type.Shift);
		float num2 = (float)this.stats.health / (float)this.stats.maxHealth;
		if (num > -1 && num2 <= (float)num / 100f)
		{
			this.SwitchToAlternate();
			this.stats.AddStatusEffect(StatusEffect.Type.Shift, -999f, Item2.Effect.MathematicalType.summative);
		}
		num = this.stats.GetStatusEffectValue(StatusEffect.Type.Lonely);
		if (num > -1 && Enemy.EnemiesAliveCount() == 1)
		{
			this.SwitchToAlternate();
			this.stats.AddStatusEffect(StatusEffect.Type.Lonely, -999f, Item2.Effect.MathematicalType.summative);
		}
		foreach (Item2.CombattEffect combattEffect in this.combattEffects)
		{
			if (combattEffect.trigger.trigger == trigger && (Item2.ShareItemTypes(combattEffect.trigger.types, itemTypes) || combattEffect.trigger.types.Count == 0 || combattEffect.trigger.types.Contains(Item2.ItemType.Any)))
			{
				yield return this.ApplyEffect(combattEffect);
			}
		}
		List<Item2.CombattEffect>.Enumerator enumerator = default(List<Item2.CombattEffect>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x0002D1C4 File Offset: 0x0002B3C4
	private IEnumerator OnDeathEffects()
	{
		if (!this.stats)
		{
			yield break;
		}
		int num = this.stats.GetStatusEffectValue(StatusEffect.Type.Wealthy);
		if (num > -1)
		{
			this.gameManager.ChangeGold(num);
		}
		num = this.stats.GetStatusEffectValue(StatusEffect.Type.Windfall);
		if (num > -1)
		{
			this.player.uncommonLuckFromItems += (float)num * 2f;
			this.player.rareLuckFromItems += (float)num;
			this.player.legendaryLuckFromItems += (float)num / 2f;
		}
		foreach (Enemy enemy in Enemy.allEnemies)
		{
			if (enemy && enemy.stats && !enemy.dead && !(enemy == this))
			{
				StatusEffect statusEffectOfType = enemy.stats.GetStatusEffectOfType(StatusEffect.Type.Dependent);
				if (statusEffectOfType && this.IsMatchingAny(statusEffectOfType.enemyScriptsRelated, base.gameObject))
				{
					enemy.SwitchToAlternate();
					enemy.stats.AddStatusEffect(StatusEffect.Type.Dependent, -999f, Item2.Effect.MathematicalType.summative);
				}
			}
		}
		StatusEffect statusEffectOfType2 = this.stats.GetStatusEffectOfType(StatusEffect.Type.Stack);
		if (statusEffectOfType2)
		{
			List<Enemy> list = new List<Enemy>();
			foreach (GameObject gameObject in statusEffectOfType2.enemyScriptsRelated)
			{
				Enemy component = Object.Instantiate<GameObject>(gameObject, Vector3.zero, Quaternion.identity, this.player.transform.parent).GetComponent<Enemy>();
				list.Add(component);
				Enemy.SetEnemyPosition(component);
			}
			if (this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.enemyTurn)
			{
				foreach (Enemy enemyScript in list)
				{
					yield return new WaitForFixedUpdate();
					enemyScript.StartCoroutine(enemyScript.SetIntention());
					enemyScript = null;
				}
				List<Enemy>.Enumerator enumerator3 = default(List<Enemy>.Enumerator);
			}
		}
		yield break;
		yield break;
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x0002D1D3 File Offset: 0x0002B3D3
	private void MakeReferences()
	{
		this.displayName = Enemy.GetRealName(base.gameObject.name);
		this.spriteRenderer = base.GetComponentInChildren<SpriteRenderer>();
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x0002D1F7 File Offset: 0x0002B3F7
	private IEnumerator RandomizeAnimation()
	{
		if (this.isShadow)
		{
			this.spriteAnimator.runtimeAnimatorController = Player.main.GetComponentInChildren<Animator>().runtimeAnimatorController;
		}
		yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
		this.spriteAnimator.enabled = true;
		yield break;
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x0002D208 File Offset: 0x0002B408
	private void Update()
	{
		if (!this.stats || this.dead)
		{
			return;
		}
		if (!this.isPet)
		{
			if (!this.stats.IsCharmed())
			{
				base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, 1f + base.transform.localPosition.x / 10f);
			}
			else
			{
				base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, 1f - base.transform.localPosition.x / 10f);
			}
			if (this.mySpacerLocation)
			{
				Vector3 vector = new Vector3(this.mySpacerLocation.transform.position.x, base.transform.position.y, base.transform.position.z);
				base.transform.position = Vector3.MoveTowards(base.transform.position, vector, 10f * Time.deltaTime * Vector3.Distance(base.transform.position, vector));
			}
		}
		if (this.stats && this.spriteRenderer && !this.isPet)
		{
			if (this.stats.IsCharmed())
			{
				if (base.transform.localScale.x != -1f)
				{
					base.transform.localScale = new Vector3(-1f, 1f, 1f);
					this.mySpacerLocation.SetParent(GameObject.FindGameObjectWithTag("PlayerSpacerParent").transform);
					this.mySpacerLocation.GetComponent<UICarvingSpacer>().CreateWidth(250f);
					this.mySpacerLocation.SetAsLastSibling();
					if (this.gameManager && this.gameManager.targetedEnemy && this.gameManager.targetedEnemy == this)
					{
						this.gameManager.TargetNextEnemy(true);
					}
				}
			}
			else if (base.transform.localScale.x != 1f)
			{
				base.transform.localScale = new Vector3(1f, 1f, 1f);
				this.mySpacerLocation.SetParent(GameObject.FindGameObjectWithTag("EnemySpacerParent").transform);
				this.mySpacerLocation.GetComponent<UICarvingSpacer>().CreateWidth(250f);
				this.mySpacerLocation.SetAsFirstSibling();
			}
		}
		if (this.gameFlowManager && !this.gameFlowManager.isCheckingEffects)
		{
			this.isTargetable = true;
		}
		if (this.isSummon && this.stats.GetStatusEffectOfType(StatusEffect.Type.Cowardly) == null)
		{
			this.stats.AddStatusEffect(StatusEffect.Type.Cowardly, 1f, Item2.Effect.MathematicalType.summative);
		}
		this.enemyIntentParent.transform.position = base.transform.position + Vector3.up * (this.boxCollider2D.size.y / 2f + 0.5f);
		this.enemyIntentParent.transform.position = new Vector3(this.enemyIntentParent.transform.position.x, Mathf.Min(this.enemyIntentParent.transform.position.y, -1f), this.enemyIntentParent.transform.position.z);
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x0002D5B8 File Offset: 0x0002B7B8
	private void SetPos()
	{
		this.enemyIntentParent.transform.position = base.transform.position + Vector3.up * (this.boxCollider2D.size.y / 2f + 0.5f);
		this.enemyIntentParent.transform.position = new Vector3(this.enemyIntentParent.transform.position.x, Mathf.Min(this.enemyIntentParent.transform.position.y, -1f), this.enemyIntentParent.transform.position.z);
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x0002D669 File Offset: 0x0002B869
	public void HurtAnimation()
	{
		this.positionAnimator.Play("enemyPos_hurt", 0, 0f);
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x0002D681 File Offset: 0x0002B881
	public void DodgeAnimation()
	{
		this.positionAnimator.Play("enemyPos_dodge", 0, 0f);
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x0002D69C File Offset: 0x0002B89C
	public static List<Status> GetAllEnemyStats()
	{
		List<Status> list = new List<Status>();
		foreach (Enemy enemy in Enemy.allEnemies)
		{
			if (!enemy.dead)
			{
				list.Add(enemy.stats);
			}
		}
		return list;
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x0002D704 File Offset: 0x0002B904
	public static Enemy GetSelectedEnemy()
	{
		GameManager main = GameManager.main;
		if (!main || !main.targetedEnemy || main.targetedEnemy.dead)
		{
			return null;
		}
		return main.targetedEnemy;
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x0002D744 File Offset: 0x0002B944
	public static Status GetSelectedEnemyStatus()
	{
		GameManager main = GameManager.main;
		if (!main || !main.targetedEnemy || main.targetedEnemy.dead)
		{
			return null;
		}
		return main.targetedEnemy.stats;
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x0002D788 File Offset: 0x0002B988
	public static float GetClipLength(Animator animator, string name)
	{
		RuntimeAnimatorController runtimeAnimatorController = animator.runtimeAnimatorController;
		for (int i = 0; i < runtimeAnimatorController.animationClips.Length; i++)
		{
			if (runtimeAnimatorController.animationClips[i].name == name)
			{
				return runtimeAnimatorController.animationClips[i].length;
			}
		}
		return -1f;
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x0002D7D7 File Offset: 0x0002B9D7
	public IEnumerator ApplyEffect(Item2.CombattEffect combattEffect)
	{
		if (this.dead)
		{
			yield break;
		}
		List<Status> list = new List<Status>();
		if (combattEffect.effect.target == Item2.Effect.Target.player)
		{
			List<Status> list2 = new List<Status>();
			list2.Add(Player.main.stats);
			list = list2;
			list = list2;
		}
		else if (combattEffect.effect.target == Item2.Effect.Target.enemy)
		{
			list = new List<Status> { this.stats };
		}
		else if (combattEffect.effect.target == Item2.Effect.Target.allEnemies)
		{
			foreach (Enemy enemy in Enemy.allEnemies)
			{
				if (!enemy.dead)
				{
					list.Add(enemy.stats);
				}
			}
		}
		List<Enemy> list3 = new List<Enemy>(Enemy.allEnemies);
		List<Enemy> list4 = new List<Enemy>();
		for (int i = 0; i < list3.Count; i++)
		{
			Enemy enemy2 = list3[i];
			if (!enemy2 || enemy2.dead || !enemy2.stats || enemy2.stats.IsCharmed())
			{
				list4.Add(list3[i]);
				list3.RemoveAt(i);
				i--;
			}
		}
		if (this.stats.IsCharmed())
		{
			if (combattEffect.effect.target == Item2.Effect.Target.player)
			{
				if (list3.Count > 0)
				{
					int num = Random.Range(0, list3.Count);
					list = new List<Status> { list3[num].stats };
				}
			}
			else if (combattEffect.effect.target == Item2.Effect.Target.allEnemies)
			{
				list = new List<Status> { this.player.stats };
				foreach (Enemy enemy3 in list4)
				{
					list.Add(enemy3.stats);
				}
			}
		}
		foreach (Status status in list)
		{
			if (combattEffect.effect.type == Item2.Effect.Type.Damage)
			{
				int num2 = Mathf.RoundToInt(combattEffect.effect.value) * -1;
				status.Attack(this.player.stats, num2, null, false, false, false);
			}
			else if (combattEffect.effect.type == Item2.Effect.Type.Block)
			{
				int num3 = Mathf.RoundToInt(combattEffect.effect.value);
				status.ChangeArmor((float)Mathf.RoundToInt((float)num3), Item2.Effect.MathematicalType.summative);
			}
			else if (combattEffect.effect.type == Item2.Effect.Type.HP)
			{
				status.ChangeHealth(Mathf.RoundToInt(combattEffect.effect.value), null, false);
			}
			else if (combattEffect.effect.type == Item2.Effect.Type.AP)
			{
				this.player.ChangeAP(Mathf.RoundToInt(combattEffect.effect.value));
			}
			else if (combattEffect.effect.type == Item2.Effect.Type.Poison)
			{
				status.AddStatusEffect(StatusEffect.Type.poison, (float)Mathf.RoundToInt(combattEffect.effect.value), combattEffect.effect.mathematicalType);
			}
			else if (combattEffect.effect.type == Item2.Effect.Type.Regen)
			{
				status.AddStatusEffect(StatusEffect.Type.regen, (float)Mathf.RoundToInt(combattEffect.effect.value), Item2.Effect.MathematicalType.summative);
			}
			else if (combattEffect.effect.type == Item2.Effect.Type.Spikes)
			{
				status.AddStatusEffect(StatusEffect.Type.spikes, (float)Mathf.RoundToInt(combattEffect.effect.value), combattEffect.effect.mathematicalType);
			}
			else if (combattEffect.effect.type == Item2.Effect.Type.Haste)
			{
				status.AddStatusEffect(StatusEffect.Type.haste, (float)Mathf.RoundToInt(combattEffect.effect.value), Item2.Effect.MathematicalType.summative);
			}
			else if (combattEffect.effect.type == Item2.Effect.Type.Slow)
			{
				status.AddStatusEffect(StatusEffect.Type.slow, (float)Mathf.RoundToInt(combattEffect.effect.value), Item2.Effect.MathematicalType.summative);
			}
			else if (combattEffect.effect.type == Item2.Effect.Type.Rage)
			{
				status.AddStatusEffect(StatusEffect.Type.rage, (float)Mathf.RoundToInt(combattEffect.effect.value), Item2.Effect.MathematicalType.summative);
			}
			else if (combattEffect.effect.type == Item2.Effect.Type.Weak)
			{
				status.AddStatusEffect(StatusEffect.Type.weak, (float)Mathf.RoundToInt(combattEffect.effect.value), Item2.Effect.MathematicalType.summative);
			}
			else if (combattEffect.effect.type == Item2.Effect.Type.Dodge)
			{
				status.AddStatusEffect(StatusEffect.Type.dodge, (float)Mathf.RoundToInt(combattEffect.effect.value), Item2.Effect.MathematicalType.summative);
			}
			else if (combattEffect.effect.type == Item2.Effect.Type.Dodge)
			{
				status.AddStatusEffect(StatusEffect.Type.dodge, (float)Mathf.RoundToInt(combattEffect.effect.value), Item2.Effect.MathematicalType.summative);
			}
			else if (combattEffect.effect.type == Item2.Effect.Type.Freeze)
			{
				status.AddStatusEffect(StatusEffect.Type.freeze, (float)Mathf.RoundToInt(combattEffect.effect.value), Item2.Effect.MathematicalType.summative);
			}
			else if (combattEffect.effect.type == Item2.Effect.Type.Burn)
			{
				status.AddStatusEffect(StatusEffect.Type.fire, (float)Mathf.RoundToInt(combattEffect.effect.value), Item2.Effect.MathematicalType.summative);
			}
			else if (combattEffect.effect.type == Item2.Effect.Type.ToughHide)
			{
				status.AddStatusEffect(StatusEffect.Type.toughHide, (float)Mathf.RoundToInt(combattEffect.effect.value), Item2.Effect.MathematicalType.summative);
			}
		}
		this.positionAnimator.Play("enemyPos_buff");
		yield return new WaitForSeconds(0.1f);
		yield break;
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x0002D7F0 File Offset: 0x0002B9F0
	private bool IsMatchingAny(List<GameObject> enemies, GameObject enemy)
	{
		foreach (GameObject gameObject in enemies)
		{
			if (this.IsMatching(gameObject, enemy))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x0002D848 File Offset: 0x0002BA48
	private bool IsMatching(GameObject a, GameObject b)
	{
		string text = Item2.GetDisplayName(a.name);
		string text2 = Item2.GetDisplayName(b.name);
		return text == text2;
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x0002D878 File Offset: 0x0002BA78
	public static void SetAllEnemyIntentionsBackup()
	{
		foreach (Enemy enemy in Enemy.allEnemies)
		{
			if (enemy.myNextAttack == null)
			{
				enemy.StartCoroutine(enemy.SetIntention());
			}
		}
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x0002D8D8 File Offset: 0x0002BAD8
	public void EndCombatAbruptly()
	{
		foreach (Enemy enemy in new List<Enemy>(Enemy.allEnemies))
		{
			Object.Destroy(enemy.gameObject);
		}
		GameFlowManager.main.EndCombatAbruptly();
		Player.main.animator.Play("Player_Idle");
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x0002D950 File Offset: 0x0002BB50
	public IEnumerator Die()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		BoxCollider2D component = base.GetComponent<BoxCollider2D>();
		if (component)
		{
			component.enabled = false;
		}
		Object.FindObjectOfType<MetaProgressSaveManager>().FindNewItem(base.gameObject);
		yield return this.OnDeathEffects();
		foreach (GameObject gameObject in this.rewardItems)
		{
			this.gameManager.rewardItems.Add(gameObject);
		}
		if (this.xp > 0)
		{
			this.player.AddExperience(this.xp, base.transform.position);
		}
		this.positionAnimator.Play("enemyPos_die");
		SoundManager.main.PlaySFXPitched("enemy_die", Random.Range(0.9f, 1.1f), false);
		Object.Destroy(this.enemyIntentParent.gameObject);
		Object.Destroy(this.stats.gameObject);
		if (this.mySpacerLocation)
		{
			this.mySpacerLocation.GetComponent<UICarvingSpacer>().Remove();
		}
		yield return new WaitForEndOfFrame();
		if (!this.gameFlowManager.IsLivingNonCoward() && !this.isPet)
		{
			foreach (Enemy enemy in Enemy.allEnemies)
			{
				if (enemy && !enemy.dead)
				{
					enemy.ChangeIntention(Enemy.Attack.Type.runAway);
				}
			}
		}
		yield return new WaitForEndOfFrame();
		yield return new WaitForSeconds(Enemy.GetClipLength(this.positionAnimator, "enemyPos_die"));
		UnityEvent unityEvent = this.onDeath;
		if (unityEvent != null)
		{
			unityEvent.Invoke();
		}
		DigitalCursor.main.UpdateContextControls();
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x0002D95F File Offset: 0x0002BB5F
	public IEnumerator RunAway()
	{
		base.transform.localScale = Vector3.one;
		if (this.stats.IsCharmed())
		{
			if (this.xp > 0)
			{
				this.player.AddExperience(this.xp, base.transform.position);
			}
			if (this.goldHeld > 0)
			{
				this.gameManager.ChangeGold(this.goldHeld);
			}
			int num = this.stats.GetStatusEffectValue(StatusEffect.Type.Wealthy);
			if (num > -1)
			{
				this.gameManager.ChangeGold(num);
			}
			num = this.stats.GetStatusEffectValue(StatusEffect.Type.Windfall);
			if (num > -1)
			{
				this.player.uncommonLuckFromItems += (float)num * 2f;
				this.player.rareLuckFromItems += (float)num;
				this.player.legendaryLuckFromItems += (float)num / 2f;
			}
		}
		this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.whenEnemyRuns, null, new List<Status> { this.stats }, true, false);
		this.dead = true;
		this.positionAnimator.Play("enemyPos_runAway");
		SoundManager.main.PlaySFX("enemy_die");
		Object.Destroy(this.enemyIntentParent.gameObject);
		Object.Destroy(this.stats.gameObject);
		if (this.mySpacerLocation)
		{
			this.mySpacerLocation.GetComponent<UICarvingSpacer>().Remove();
		}
		yield return new WaitForEndOfFrame();
		yield return new WaitForSeconds(Enemy.GetClipLength(this.positionAnimator, "enemyPos_runAway") / 0.3f);
		if (!this || !base.gameObject)
		{
			yield break;
		}
		if (base.gameObject)
		{
			Object.Destroy(base.gameObject);
		}
		yield break;
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x0002D970 File Offset: 0x0002BB70
	public void UpdateIntentionRageAndWeak()
	{
		if (this.myNextAttack == null)
		{
			return;
		}
		int num = this.stats.GetStatusEffectValue(StatusEffect.Type.rage);
		int num2 = this.stats.GetStatusEffectValue(StatusEffect.Type.weak);
		num = Mathf.Max(num, 0);
		num2 = Mathf.Max(num2, 0);
		int num3 = this.stats.GetStatusEffectValue(StatusEffect.Type.haste);
		int num4 = this.stats.GetStatusEffectValue(StatusEffect.Type.slow);
		num3 = Mathf.Max(num3, 0);
		num4 = Mathf.Max(num4, 0);
		foreach (Enemy.Attack attack in this.myNextAttack.attacks)
		{
			if (attack.type == Enemy.Attack.Type.attack || attack.type == Enemy.Attack.Type.selfDestruct)
			{
				int num5 = Mathf.Max(attack.currentPower + num + num2 * -1, 0);
				foreach (EnemyActionPreview enemyActionPreview in Object.FindObjectsOfType<EnemyActionPreview>())
				{
					if (enemyActionPreview.myAttackReference == attack)
					{
						enemyActionPreview.GetComponentInChildren<TextMeshProUGUI>().text = num5.ToString() ?? "";
					}
				}
			}
			else if (attack.type == Enemy.Attack.Type.block)
			{
				int num6 = Mathf.Max(attack.currentPower + num3 + num4 * -1, 0);
				foreach (EnemyActionPreview enemyActionPreview2 in Object.FindObjectsOfType<EnemyActionPreview>())
				{
					if (enemyActionPreview2.myAttackReference == attack)
					{
						enemyActionPreview2.GetComponentInChildren<TextMeshProUGUI>().text = num6.ToString() ?? "";
					}
				}
			}
		}
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x0002DB00 File Offset: 0x0002BD00
	public static List<Enemy> SortEnemiesLeftToRight(List<Enemy> objects)
	{
		return (from x in objects
			where x.mySpacerLocation
			orderby x.mySpacerLocation.transform.position.x
			select x).ToList<Enemy>();
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x0002DB5C File Offset: 0x0002BD5C
	public static void AllEnemiesConsiderActionOverride()
	{
		foreach (Enemy enemy in Enemy.allEnemies)
		{
			enemy.ConsiderActionOverride();
		}
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x0002DBAC File Offset: 0x0002BDAC
	public void ConsiderActionOverride()
	{
		if (!this.stats || this.dead)
		{
			return;
		}
		if (this.stats.GetStatusEffectValue(StatusEffect.Type.poison) >= this.stats.health)
		{
			this.ChangeIntention(Enemy.Attack.Type.poisonSickness);
			return;
		}
		if (this.stats.GetStatusEffectValue(StatusEffect.Type.sleep) >= 0)
		{
			this.ChangeIntention(Enemy.Attack.Type.pass);
			return;
		}
		if (this.gameFlowManager && !this.gameFlowManager.IsLivingNonCoward() && !this.isPet)
		{
			this.ChangeIntention(Enemy.Attack.Type.runAway);
			return;
		}
		this.RevertIntentionBack();
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x0002DC3C File Offset: 0x0002BE3C
	public void ChangeIntentionPerm(Enemy.PossibleAttack possibleAttack)
	{
		if (!this.gameManager || !this.gameFlowManager)
		{
			this.gameManager = GameManager.main;
			this.gameFlowManager = GameFlowManager.main;
		}
		if (this.myOverrideIntention != null && this.myOverrideIntention.attacks != null && this.myOverrideIntention.attacks.Count > 0)
		{
			return;
		}
		this.myNextAttack = possibleAttack;
		this.CreateIntentionObject();
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x0002DCAF File Offset: 0x0002BEAF
	public void ChangeIntentionPerm(List<Enemy.PossibleAttack> possibleAttacks)
	{
		if (possibleAttacks.Count == 0)
		{
			return;
		}
		this.ChangeIntentionPerm(possibleAttacks[0]);
		this.actionNum = 1;
		this.storedAttacks = this.possibleAttacks;
		this.possibleAttacks = possibleAttacks;
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x0002DCE4 File Offset: 0x0002BEE4
	public void ChangeIntentionPerm(Enemy.Attack.Type type, Enemy.Attack.AttackAnimation attackAnimation, Vector2 powerRange)
	{
		if (!this.gameManager || !this.gameFlowManager)
		{
			this.gameManager = GameManager.main;
			this.gameFlowManager = GameFlowManager.main;
		}
		if (this.myOverrideIntention != null && this.myOverrideIntention.attacks != null && this.myOverrideIntention.attacks.Count > 0 && this.myOverrideIntention.attacks[0].type == type)
		{
			return;
		}
		Enemy.PossibleAttack possibleAttack = new Enemy.PossibleAttack();
		possibleAttack.attacks = new List<Enemy.Attack>();
		Enemy.Attack attack = new Enemy.Attack();
		attack.type = type;
		attack.powerRange = powerRange;
		Enemy.SetCurrentPower(attack);
		attack.attackAnimation = attackAnimation;
		possibleAttack.attacks.Add(attack);
		this.myNextAttack = possibleAttack;
		this.CreateIntentionObject();
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x0002DDB0 File Offset: 0x0002BFB0
	public void ChangeIntention(Enemy.Attack.Type type)
	{
		if (!this.gameManager || !this.gameFlowManager)
		{
			this.gameManager = GameManager.main;
			this.gameFlowManager = GameFlowManager.main;
		}
		if (this.myOverrideIntention != null && this.myOverrideIntention.attacks != null && this.myOverrideIntention.attacks.Count > 0 && this.myOverrideIntention.attacks[0].type == type)
		{
			return;
		}
		this.myOverrideIntention = new Enemy.PossibleAttack();
		this.myOverrideIntention.attacks = new List<Enemy.Attack>();
		Enemy.Attack attack = new Enemy.Attack();
		attack.type = type;
		this.myOverrideIntention.attacks.Add(attack);
		this.myNextAttack = this.myOverrideIntention;
		if (type == Enemy.Attack.Type.poisonSickness)
		{
			attack.attackAnimation = Enemy.Attack.AttackAnimation.none;
		}
		this.CreateIntentionObject();
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x0002DE86 File Offset: 0x0002C086
	public void RevertIntentionBack()
	{
		if (this.myOverrideIntention == null)
		{
			return;
		}
		this.myOverrideIntention = null;
		this.CreateIntentionObject();
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x0002DEA0 File Offset: 0x0002C0A0
	public List<StatusEffect> GetAllPowers()
	{
		List<StatusEffect> list = new List<StatusEffect>();
		foreach (GameObject gameObject in this.statusEffectsToStartWithPrefabs)
		{
			StatusEffect component = gameObject.GetComponent<StatusEffect>();
			if (component)
			{
				list.Add(component);
			}
		}
		foreach (GameObject gameObject2 in this.statusEffectsToGainEachTurnPrefabs)
		{
			StatusEffect component2 = gameObject2.GetComponent<StatusEffect>();
			if (component2)
			{
				list.Add(component2);
			}
		}
		foreach (Enemy.PossibleAttack possibleAttack in this.possibleAttacks)
		{
			foreach (Enemy.Attack attack in possibleAttack.attacks)
			{
				foreach (GameObject gameObject3 in attack.prefabs)
				{
					StatusEffect component3 = gameObject3.GetComponent<StatusEffect>();
					if (component3)
					{
						list.Add(component3);
					}
				}
			}
		}
		return list;
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x0002E020 File Offset: 0x0002C220
	public IEnumerator SetIntention()
	{
		if (!this.gameManager || !this.gameFlowManager)
		{
			this.gameManager = GameManager.main;
			this.gameFlowManager = GameFlowManager.main;
		}
		if (this.dead || !this.stats || !this.gameManager || !this.gameFlowManager)
		{
			yield break;
		}
		this.RevertIntentionBack();
		if (this.storedAttacks != null && this.storedAttacks.Count > 0 && this.actionNum >= this.possibleAttacks.Count)
		{
			if (this.spriteAnimator && this.defaultAnimator)
			{
				this.spriteAnimator.runtimeAnimatorController = this.defaultAnimator;
				this.positionAnimator.Play("enemyPos_buff", 0, 0f);
			}
			this.possibleAttacks = this.storedAttacks;
			this.storedAttacks = null;
			this.actionNum = 0;
		}
		bool flag = false;
		List<Enemy.PossibleAttack> list = new List<Enemy.PossibleAttack>();
		List<Enemy.PossibleAttack> list2 = new List<Enemy.PossibleAttack>(this.possibleAttacks);
		if (this.isPerformingAlternateAttacks)
		{
			list2 = new List<Enemy.PossibleAttack>(this.alternatePossibleAttacks);
		}
		for (int i = 0; i < list2.Count; i++)
		{
			Enemy.PossibleAttack possibleAttack = list2[i];
			if (possibleAttack.skipIfEnemyExists.Count > 0)
			{
				bool flag2 = false;
				foreach (Enemy enemy in Enemy.allEnemies)
				{
					if (this.IsMatchingAny(possibleAttack.skipIfEnemyExists, enemy.gameObject))
					{
						flag2 = true;
					}
				}
				if (flag2)
				{
					list2.RemoveAt(i);
					i--;
				}
			}
		}
		foreach (Enemy.PossibleAttack possibleAttack2 in list2)
		{
			if (possibleAttack2.currentCoolDown > 0)
			{
				possibleAttack2.currentCoolDown--;
			}
			else
			{
				bool flag3 = false;
				foreach (Enemy.Attack attack in possibleAttack2.attacks)
				{
					if (attack.type == Enemy.Attack.Type.heal && this.stats.health > this.stats.maxHealth - this.stats.maxHealth / 4 && attack.target == Item2.Effect.Target.enemy)
					{
						flag3 = true;
					}
				}
				if (!flag3)
				{
					list.Add(possibleAttack2);
				}
				if ((possibleAttack2.coolDown == 999 && possibleAttack2.currentCoolDown == 0) || possibleAttack2.alwaysConsiderFirst)
				{
					flag = true;
					this.myNextAttack = possibleAttack2;
					break;
				}
			}
		}
		if (list != null && list.Count > 0 && Player.main.stats.GetStatusEffectValue(StatusEffect.Type.curse) > 0)
		{
			list.RemoveAll((Enemy.PossibleAttack x) => x.attacks.Any((Enemy.Attack y) => y.type == Enemy.Attack.Type.curseStatus));
		}
		if (!flag)
		{
			if (list.Count == 0)
			{
				list = list2;
			}
			if (this.actionNum >= list.Count || this.actionNum < 0)
			{
				this.actionNum = 0;
			}
			this.myNextAttack = list[this.actionNum];
			this.actionNum++;
		}
		if (this.myNextAttack.coolDown != -999)
		{
			this.myNextAttack.currentCoolDown = this.myNextAttack.coolDown;
		}
		this.ConsiderActionOverride();
		this.CreateIntentionObject();
		yield break;
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x0002E030 File Offset: 0x0002C230
	public static void SetCurrentPower(Enemy.Attack attack1)
	{
		attack1.powerIsSet = true;
		attack1.currentPower = Mathf.RoundToInt(Random.Range(attack1.powerRange.x, attack1.powerRange.y));
		if (attack1.type == Enemy.Attack.Type.attack)
		{
			RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.enemyAttacksMultipler);
			if (runProperty != null)
			{
				attack1.currentPower = Mathf.RoundToInt((float)attack1.currentPower * ((100f + (float)runProperty.value) / 100f));
			}
		}
		if (GameManager.main && !attack1.ignoreScaling && attack1.type != Enemy.Attack.Type.dodge && attack1.type != Enemy.Attack.Type.curseStatus && attack1.type != Enemy.Attack.Type.hazard)
		{
			attack1.currentPower += Mathf.Max(0, (GameFlowManager.main.turnNumber - 7) * 2);
			if (GameManager.main.floor > GameManager.main.floorsPerLevel * 3)
			{
				attack1.currentPower += (GameManager.main.floor - GameManager.main.floorsPerLevel * 3 + 1) * 2;
			}
		}
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x0002E13C File Offset: 0x0002C33C
	public void CreateIntentionObject()
	{
		foreach (EnemyActionPreview enemyActionPreview in Object.FindObjectsOfType<EnemyActionPreview>())
		{
			if (enemyActionPreview.enemy && enemyActionPreview.enemy == this)
			{
				Object.Destroy(enemyActionPreview.gameObject);
			}
		}
		Enemy.PossibleAttack possibleAttack = this.myNextAttack;
		if (this.myOverrideIntention != null)
		{
			possibleAttack = this.myOverrideIntention;
		}
		if (possibleAttack == null || possibleAttack.attacks == null || possibleAttack.attacks.Count <= 0)
		{
			return;
		}
		int num = 0;
		foreach (Enemy.Attack attack in possibleAttack.attacks)
		{
			if ((attack.type == Enemy.Attack.Type.hazard && Singleton.Instance.storyMode && !MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedHazards)) || (attack.type == Enemy.Attack.Type.curseStatus && Singleton.Instance.storyMode && !MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedCurses)))
			{
				attack.type = Enemy.Attack.Type.pass;
				attack.attackAnimation = Enemy.Attack.AttackAnimation.buff;
				if (num > 0)
				{
					attack.hideIntention = true;
				}
			}
			if (!attack.hideIntention)
			{
				num++;
				attack.description = attack.startingDescription;
				if (attack.reactionary)
				{
					Object.FindObjectOfType<ItemMovement>().CreateHighlight(Color.red, null);
				}
				GameObject gameObject = Object.Instantiate<GameObject>(this.enemyIntentPrefab, Vector3.zero, Quaternion.identity, this.enemyIntentParent);
				TextMeshProUGUI componentInChildren = gameObject.GetComponentInChildren<TextMeshProUGUI>();
				Image componentInChildren2 = gameObject.GetComponentInChildren<Image>();
				gameObject.GetComponent<EnemyActionPreview>().myAttackReference = attack;
				gameObject.GetComponent<EnemyActionPreview>().enemy = this;
				Enemy.SetCurrentPower(attack);
				if (attack.type == Enemy.Attack.Type.poison)
				{
					if (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.enemiesWontPoison) != null)
					{
						attack.currentPower = Mathf.RoundToInt((float)attack.currentPower * 1.5f);
						attack.type = Enemy.Attack.Type.attack;
						attack.target = Item2.Effect.Target.player;
						attack.attackAnimation = Enemy.Attack.AttackAnimation.attack;
					}
				}
				else if ((attack.type == Enemy.Attack.Type.hazard || attack.type == Enemy.Attack.Type.curseStatus) && RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.enemiesWontCurse) != null)
				{
					attack.currentPower = Mathf.RoundToInt(Random.Range(7f + (float)this.gameManager.floor * 1.5f, 12f + (float)this.gameManager.floor * 1.5f));
					attack.type = Enemy.Attack.Type.attack;
					attack.target = Item2.Effect.Target.player;
					attack.attackAnimation = Enemy.Attack.AttackAnimation.attack;
				}
				componentInChildren.text = attack.currentPower.ToString() ?? "";
				if (attack.type == Enemy.Attack.Type.attack)
				{
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[0];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.player;
					}
					this.UpdateIntentionRageAndWeak();
				}
				else if (attack.type == Enemy.Attack.Type.selfDestruct)
				{
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[21];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.player;
					}
					this.UpdateIntentionRageAndWeak();
				}
				else if (attack.type == Enemy.Attack.Type.block)
				{
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[1];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.enemy;
					}
					this.UpdateIntentionRageAndWeak();
				}
				else if (attack.type == Enemy.Attack.Type.heal)
				{
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[2];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.enemy;
					}
				}
				else if (attack.type == Enemy.Attack.Type.pass)
				{
					componentInChildren.text = "";
					if (attack.sprite == null && this.stats && this.stats.GetStatusEffectValue(StatusEffect.Type.sleep) > 0)
					{
						attack.sprite = this.intentionSprites[22];
					}
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[4];
					}
				}
				else if (attack.type == Enemy.Attack.Type.poison)
				{
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[5];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.player;
					}
				}
				else if (attack.type == Enemy.Attack.Type.burn)
				{
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[13];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.player;
					}
				}
				else if (attack.type == Enemy.Attack.Type.regen)
				{
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[6];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.enemy;
					}
				}
				else if (attack.type == Enemy.Attack.Type.spikes)
				{
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[8];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.enemy;
					}
				}
				else if (attack.type == Enemy.Attack.Type.dodge)
				{
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[14];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.enemy;
					}
				}
				else if (attack.type == Enemy.Attack.Type.slow)
				{
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[15];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.player;
					}
				}
				else if (attack.type == Enemy.Attack.Type.weak)
				{
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[16];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.player;
					}
				}
				else if (attack.type == Enemy.Attack.Type.rage)
				{
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[17];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.enemy;
					}
				}
				else if (attack.type == Enemy.Attack.Type.haste)
				{
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[18];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.enemy;
					}
				}
				else if (attack.type == Enemy.Attack.Type.summon)
				{
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[9];
					}
					componentInChildren.text = "";
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.enemy;
					}
				}
				else if (attack.type == Enemy.Attack.Type.vampire)
				{
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[10];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.player;
					}
				}
				else if (attack.type == Enemy.Attack.Type.hazard)
				{
					if (attack.currentPower == 1)
					{
						componentInChildren.text = "";
					}
					attack.sprite = null;
					if (attack.prefabs.Count > 0)
					{
						GameObject gameObject2 = attack.prefabs[0];
						if (gameObject2)
						{
							SpriteRenderer component = gameObject2.GetComponent<SpriteRenderer>();
							if (component)
							{
								attack.sprite = component.sprite;
							}
						}
					}
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[12];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.player;
					}
				}
				else if (attack.type == Enemy.Attack.Type.curseStatus)
				{
					componentInChildren.text = "";
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[25];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.player;
					}
				}
				else if (attack.type == Enemy.Attack.Type.freeze)
				{
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[24];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.player;
					}
				}
				else if (attack.type == Enemy.Attack.Type.attack)
				{
					componentInChildren.text = "";
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[12];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.player;
					}
				}
				else if (attack.type == Enemy.Attack.Type.poisonSickness)
				{
					componentInChildren.text = "";
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[19];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.enemy;
					}
				}
				else if (attack.type == Enemy.Attack.Type.runAway)
				{
					componentInChildren.text = "";
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[20];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.enemy;
					}
				}
				else if (attack.type == Enemy.Attack.Type.steal)
				{
					if (attack.sprite == null)
					{
						attack.sprite = this.intentionSprites[23];
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.player;
					}
				}
				else if (attack.type == Enemy.Attack.Type.addStatusEffect)
				{
					StatusEffect component2 = attack.prefabs[0].GetComponent<StatusEffect>();
					if (component2)
					{
						attack.sprite = component2.mainSpriteRenderer.sprite;
					}
					if (attack.target == Item2.Effect.Target.unspecified)
					{
						attack.target = Item2.Effect.Target.enemy;
					}
					if (attack.isNumeric)
					{
						componentInChildren.text = attack.currentPower.ToString() ?? "";
					}
					else
					{
						componentInChildren.text = "";
					}
				}
				componentInChildren2.enabled = true;
				componentInChildren2.sprite = attack.sprite;
				if (attack.target == Item2.Effect.Target.allEnemies)
				{
					Object.Instantiate<GameObject>(componentInChildren2.gameObject, componentInChildren2.transform.position + Vector3.up * 0.1f + Vector3.right * 0.1f, Quaternion.identity, componentInChildren2.transform.parent).transform.SetAsFirstSibling();
				}
			}
		}
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x0002EBA4 File Offset: 0x0002CDA4
	public IEnumerator Turn()
	{
		Enemy.PossibleAttack currentAttack = this.myNextAttack;
		this.myNextAttack = null;
		if (!this || !base.gameObject || !this.stats || !this.gameManager || this.gameManager.dead)
		{
			yield break;
		}
		if (this.dead)
		{
			yield break;
		}
		if ((!this.isPet && this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.petTurn) || (this.isPet && this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.enemyTurn))
		{
			yield break;
		}
		this.stats.GetStatusEffectValue(StatusEffect.Type.rage);
		this.stats.GetStatusEffectValue(StatusEffect.Type.weak);
		this.stats.GetStatusEffectValue(StatusEffect.Type.haste);
		this.stats.GetStatusEffectValue(StatusEffect.Type.slow);
		yield return this.stats.NextTurn();
		if (this.stats.health <= 0)
		{
			yield break;
		}
		List<Enemy> enemiesThatAreNotCharmed = new List<Enemy>(Enemy.allEnemies);
		List<Enemy> enemiesThatAreCharmed = new List<Enemy>();
		for (int i = 0; i < enemiesThatAreNotCharmed.Count; i++)
		{
			Enemy enemy = enemiesThatAreNotCharmed[i];
			if (enemy && !enemy.dead && enemy.stats && enemy.stats.IsCharmed())
			{
				enemiesThatAreCharmed.Add(enemiesThatAreNotCharmed[i]);
				enemiesThatAreNotCharmed.RemoveAt(i);
				i--;
			}
		}
		Enemy.AllEnemiesConsiderActionOverride();
		if (this.myOverrideIntention != null)
		{
			currentAttack = this.myOverrideIntention;
		}
		if (currentAttack == null || currentAttack.attacks == null || currentAttack.attacks.Count <= 0)
		{
			yield break;
		}
		foreach (Enemy.Attack attack in currentAttack.attacks)
		{
			while (GameFlowManager.main.isCheckingEffects)
			{
				yield return null;
			}
			if (this.dead)
			{
				yield break;
			}
			if (Singleton.Instance.playerAnimations && !attack.hideIntention)
			{
				if (attack.attackAnimation == Enemy.Attack.AttackAnimation.attack)
				{
					this.positionAnimator.Play("enemyPos_attack");
				}
				else if (attack.attackAnimation == Enemy.Attack.AttackAnimation.buff)
				{
					this.positionAnimator.Play("enemyPos_buff");
				}
			}
			List<Status> list = new List<Status>();
			if (attack.target == Item2.Effect.Target.player)
			{
				list.Add(PetMaster.GetRightMostCombatPetStats());
			}
			else if (attack.target == Item2.Effect.Target.enemy)
			{
				if (this.isPet)
				{
					list.Add(this.gameManager.targetedEnemy.stats);
				}
				else
				{
					list.Add(this.stats);
				}
			}
			else if (attack.target == Item2.Effect.Target.allEnemies)
			{
				foreach (Enemy enemy2 in Enemy.allEnemies)
				{
					list.Add(enemy2.stats);
				}
			}
			if (attack.target == Item2.Effect.Target.unspecified)
			{
				list.Add(this.player.stats);
			}
			if (this.stats.IsCharmed() && !this.isPet)
			{
				if (attack.target == Item2.Effect.Target.player)
				{
					if (enemiesThatAreNotCharmed.Count > 0)
					{
						int num = Random.Range(0, enemiesThatAreNotCharmed.Count);
						list = new List<Status> { enemiesThatAreNotCharmed[num].stats };
					}
				}
				else if (attack.target == Item2.Effect.Target.allEnemies)
				{
					list = new List<Status> { this.player.stats };
					foreach (Enemy enemy3 in enemiesThatAreCharmed)
					{
						list.Add(enemy3.stats);
					}
				}
			}
			if (list.Count == 1 && list.Contains(this.player.stats))
			{
				float x = this.player.playerSpritePositionTransform.transform.position.x;
				Status status = this.player.stats;
				foreach (Enemy enemy4 in enemiesThatAreCharmed)
				{
					if (enemy4 && !enemy4.dead && enemy4.stats && enemy4.transform.position.x > x && enemy4.CheckForProperty(Enemy.EnemyProperty.Type.blocking))
					{
						status = enemy4.stats;
					}
				}
				list = new List<Status> { status };
			}
			if (attack.type == Enemy.Attack.Type.curseStatus || attack.type == Enemy.Attack.Type.hazard)
			{
				list = new List<Status> { this.player.stats };
			}
			if (this.stats.IsCharmed() && (attack.type == Enemy.Attack.Type.curseStatus || attack.type == Enemy.Attack.Type.hazard))
			{
				list = new List<Status>();
			}
			foreach (Status stats in list)
			{
				if (stats)
				{
					if (attack.type == Enemy.Attack.Type.attack)
					{
						if (Singleton.Instance.playerAnimations)
						{
							yield return new WaitForSeconds(Enemy.GetClipLength(this.positionAnimator, "enemyPos_attack"));
						}
						stats.Attack(this.stats, -attack.currentPower, null, false, false, false);
					}
					else if (attack.type == Enemy.Attack.Type.selfDestruct)
					{
						if (Singleton.Instance.playerAnimations)
						{
							yield return new WaitForSeconds(Enemy.GetClipLength(this.positionAnimator, "enemyPos_attack"));
						}
						stats.Attack(this.stats, -attack.currentPower, null, false, false, false);
						this.stats.Attack(this.stats, -999, null, false, false, false);
					}
					else if (attack.type == Enemy.Attack.Type.block)
					{
						if (Singleton.Instance.playerAnimations)
						{
							yield return new WaitForSeconds(Enemy.GetClipLength(this.positionAnimator, "enemyPos_attack"));
						}
						stats.ChangeArmor((float)attack.currentPower, Item2.Effect.MathematicalType.summative);
					}
					else if (attack.type == Enemy.Attack.Type.heal)
					{
						if (Singleton.Instance.playerAnimations)
						{
							yield return new WaitForSeconds(Enemy.GetClipLength(this.positionAnimator, "enemyPos_attack"));
						}
						stats.ChangeHealth(attack.currentPower, null, false);
					}
					else if (attack.type == Enemy.Attack.Type.poison)
					{
						if (Singleton.Instance.playerAnimations)
						{
							yield return new WaitForSeconds(Enemy.GetClipLength(this.positionAnimator, "enemyPos_attack"));
						}
						stats.AddStatusEffect(StatusEffect.Type.poison, (float)attack.currentPower, Item2.Effect.MathematicalType.summative);
					}
					else if (attack.type == Enemy.Attack.Type.burn)
					{
						if (Singleton.Instance.playerAnimations)
						{
							yield return new WaitForSeconds(Enemy.GetClipLength(this.positionAnimator, "enemyPos_attack"));
						}
						stats.AddStatusEffect(StatusEffect.Type.fire, (float)attack.currentPower, Item2.Effect.MathematicalType.summative);
					}
					else if (attack.type == Enemy.Attack.Type.spikes)
					{
						if (Singleton.Instance.playerAnimations)
						{
							yield return new WaitForSeconds(Enemy.GetClipLength(this.positionAnimator, "enemyPos_attack"));
						}
						stats.AddStatusEffect(StatusEffect.Type.spikes, (float)attack.currentPower, Item2.Effect.MathematicalType.summative);
					}
					else if (attack.type == Enemy.Attack.Type.regen)
					{
						if (Singleton.Instance.playerAnimations)
						{
							yield return new WaitForSeconds(Enemy.GetClipLength(this.positionAnimator, "enemyPos_attack"));
						}
						stats.AddStatusEffect(StatusEffect.Type.regen, (float)attack.currentPower, Item2.Effect.MathematicalType.summative);
					}
					else if (attack.type == Enemy.Attack.Type.dodge)
					{
						if (Singleton.Instance.playerAnimations)
						{
							yield return new WaitForSeconds(Enemy.GetClipLength(this.positionAnimator, "enemyPos_attack"));
						}
						stats.AddStatusEffect(StatusEffect.Type.dodge, (float)attack.currentPower, Item2.Effect.MathematicalType.summative);
					}
					else if (attack.type == Enemy.Attack.Type.slow)
					{
						if (Singleton.Instance.playerAnimations)
						{
							yield return new WaitForSeconds(Enemy.GetClipLength(this.positionAnimator, "enemyPos_attack"));
						}
						stats.AddStatusEffect(StatusEffect.Type.slow, (float)attack.currentPower, Item2.Effect.MathematicalType.summative);
					}
					else if (attack.type == Enemy.Attack.Type.weak)
					{
						if (Singleton.Instance.playerAnimations)
						{
							yield return new WaitForSeconds(Enemy.GetClipLength(this.positionAnimator, "enemyPos_attack"));
						}
						stats.AddStatusEffect(StatusEffect.Type.weak, (float)attack.currentPower, Item2.Effect.MathematicalType.summative);
					}
					else if (attack.type == Enemy.Attack.Type.rage)
					{
						if (Singleton.Instance.playerAnimations)
						{
							yield return new WaitForSeconds(Enemy.GetClipLength(this.positionAnimator, "enemyPos_attack"));
						}
						stats.AddStatusEffect(StatusEffect.Type.rage, (float)attack.currentPower, Item2.Effect.MathematicalType.summative);
					}
					else if (attack.type == Enemy.Attack.Type.haste)
					{
						if (Singleton.Instance.playerAnimations)
						{
							yield return new WaitForSeconds(Enemy.GetClipLength(this.positionAnimator, "enemyPos_attack"));
						}
						stats.AddStatusEffect(StatusEffect.Type.haste, (float)attack.currentPower, Item2.Effect.MathematicalType.summative);
					}
					else if (attack.type == Enemy.Attack.Type.freeze)
					{
						if (Singleton.Instance.playerAnimations)
						{
							yield return new WaitForSeconds(Enemy.GetClipLength(this.positionAnimator, "enemyPos_attack"));
						}
						stats.AddStatusEffect(StatusEffect.Type.freeze, (float)attack.currentPower, Item2.Effect.MathematicalType.summative);
					}
					else if (attack.type == Enemy.Attack.Type.curseStatus)
					{
						if (Singleton.Instance.playerAnimations)
						{
							yield return new WaitForSeconds(Enemy.GetClipLength(this.positionAnimator, "enemyPos_attack"));
						}
						stats.AddStatusEffect(StatusEffect.Type.curse, (float)attack.currentPower, Item2.Effect.MathematicalType.summative);
					}
					else if (attack.type == Enemy.Attack.Type.summon)
					{
						if (Item2.GetDisplayName(base.name) == Item2.GetDisplayName("Satchel"))
						{
							SoundManager.main.PlaySFX("instr_flute_" + Random.Range(1, 5).ToString());
							this.spriteAnimator.Play("UseItem", 0, 0f);
						}
						else
						{
							SoundManager.main.PlaySFX("generic");
						}
						if (Singleton.Instance.playerAnimations)
						{
							yield return new WaitForSeconds(Enemy.GetClipLength(this.positionAnimator, "enemyPos_attack"));
						}
						foreach (GameObject prefab in attack.prefabs)
						{
							yield return new WaitForEndOfFrame();
							yield return new WaitForFixedUpdate();
							if (Enemy.allEnemies.Count < 6)
							{
								Enemy component = Object.Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity, base.transform.parent).GetComponent<Enemy>();
								component.xp = 0;
								component.isSummon = true;
								Enemy.SetEnemyPosition(component);
							}
							prefab = null;
						}
						List<GameObject>.Enumerator enumerator4 = default(List<GameObject>.Enumerator);
						GameManager.main.ReselectEnemyForDefenders();
					}
					else if (attack.type == Enemy.Attack.Type.pass)
					{
						SoundManager.main.PlaySFX("blipUp");
					}
					else
					{
						if (attack.type == Enemy.Attack.Type.runAway)
						{
							yield return this.RunAway();
							yield break;
						}
						if (attack.type == Enemy.Attack.Type.vampire)
						{
							if (Singleton.Instance.playerAnimations)
							{
								yield return new WaitForSeconds(Enemy.GetClipLength(this.positionAnimator, "enemyPos_attack"));
							}
							stats.Vampire(this.stats, attack.currentPower * -1, null);
						}
						else if (attack.type == Enemy.Attack.Type.steal)
						{
							if (Singleton.Instance.playerAnimations)
							{
								yield return new WaitForSeconds(Enemy.GetClipLength(this.positionAnimator, "enemyPos_attack"));
							}
							if (!this.stats.IsCharmed())
							{
								stats.Attack(this.stats, 0, null, false, false, false);
								int num2 = attack.currentPower;
								num2 = Mathf.Min(num2, this.gameManager.goldAmount);
								this.gameManager.ChangeGold(num2 * -1);
								this.stats.AddStatusEffect(StatusEffect.Type.Wealthy, (float)num2, Item2.Effect.MathematicalType.summative);
								SoundManager.main.PlaySFX("negative");
								this.player.Hurt();
							}
						}
						else if (attack.type == Enemy.Attack.Type.addStatusEffect)
						{
							Enemy.SetCurrentPower(attack);
							if (attack.prefabs.Count > 0)
							{
								stats.AddStatusEffectFromPrefab(attack.prefabs[0], attack.isNumeric, attack.currentPower);
							}
						}
						else if (attack.type == Enemy.Attack.Type.changeAnimator)
						{
							this.spriteAnimator.runtimeAnimatorController = attack.animator;
							this.positionAnimator.Play("enemyPos_buff", 0, 0f);
						}
						else if (attack.type == Enemy.Attack.Type.hazard)
						{
							if (this.gameManager.dead)
							{
								yield break;
							}
							if (!this.stats.IsCharmed())
							{
								List<GameObject> list2 = new List<GameObject>();
								Vector2 vector = base.transform.position;
								vector += new Vector2(-1.5f, -1.5f) * (float)attack.currentPower / 2f;
								for (int j = 0; j < attack.currentPower; j++)
								{
									GameObject gameObject = this.gameManager.CreateCursePublic(attack.prefabs);
									gameObject.transform.position = vector + new Vector2((float)j * 1.5f, (float)j * 1.5f);
									list2.Add(gameObject);
								}
								SoundManager.main.PlaySFX("hazard");
								yield return this.gameManager.CreateCurseReorg(list2, false);
							}
							while (this.gameManager.inventoryPhase == GameManager.InventoryPhase.specialReorganization)
							{
								yield return null;
							}
						}
					}
					stats = null;
				}
			}
			List<Status>.Enumerator enumerator3 = default(List<Status>.Enumerator);
			EnemyActionPreview[] array = Object.FindObjectsOfType<EnemyActionPreview>();
			GameObject intentToDestroy = null;
			foreach (EnemyActionPreview enemyActionPreview in array)
			{
				if (enemyActionPreview.myAttackReference == attack)
				{
					enemyActionPreview.GetComponent<Animator>().Play("Out");
					intentToDestroy = enemyActionPreview.gameObject;
				}
			}
			yield return new WaitForSeconds(0.25f);
			if (intentToDestroy)
			{
				Object.Destroy(intentToDestroy);
			}
			intentToDestroy = null;
			attack = null;
		}
		List<Enemy.Attack>.Enumerator enumerator = default(List<Enemy.Attack>.Enumerator);
		yield return this.stats.TurnEnd();
		this.myNextAttack = null;
		yield break;
		yield break;
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x0002EBB4 File Offset: 0x0002CDB4
	public static void SetEnemyPosition(Enemy newEnemy)
	{
		BoxCollider2D component = newEnemy.GetComponent<BoxCollider2D>();
		newEnemy.transform.position = new Vector3(GameManager.main.spawnPosition.position.x - component.size.x / 2f, -4.8f + newEnemy.GetComponent<BoxCollider2D>().size.y / 2f, 1f);
		bool flag = true;
		while (flag)
		{
			flag = false;
			newEnemy.transform.position += Vector3.left / 2f;
			foreach (RaycastHit2D raycastHit2D in Physics2D.BoxCastAll(newEnemy.transform.position, component.size * 1.2f, 0f, Vector3.zero))
			{
				if (raycastHit2D.collider.gameObject.CompareTag("InteractiveVisual") && raycastHit2D.collider.gameObject != newEnemy)
				{
					flag = true;
					break;
				}
			}
		}
		newEnemy.transform.position = new Vector3(newEnemy.transform.position.x, -4.8f + newEnemy.GetComponent<BoxCollider2D>().size.y / 2f, newEnemy.transform.position.z);
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x0002ED1B File Offset: 0x0002CF1B
	public void OnMouseDown()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.SelectEnemy();
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x0002ED30 File Offset: 0x0002CF30
	public override void OnPressStart(string keyName, bool overrideKeyName)
	{
		if (keyName != "confirm" && !overrideKeyName)
		{
			return;
		}
		this.SelectEnemy();
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x0002ED49 File Offset: 0x0002CF49
	private void SelectEnemy()
	{
		if (this.isPet)
		{
			return;
		}
		if (this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.playerTurn && !this.gameManager.viewingEvent)
		{
			this.gameManager.SelectEnemy(this);
		}
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x0002ED7C File Offset: 0x0002CF7C
	public override void OnCursorHold()
	{
		if (this.isPet)
		{
			return;
		}
		if (Input.GetMouseButton(0) || (this.gameManager && this.gameManager.viewingEvent))
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
			this.previewCard = Object.Instantiate<GameObject>(this.cardPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").GetComponent<Canvas>().transform);
			this.ApplyToCard(this.previewCard);
		}
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x0002EE44 File Offset: 0x0002D044
	public void ApplyToCard(GameObject previewCard)
	{
		this.MakeReferences();
		Card component = previewCard.GetComponent<Card>();
		component.GetDescriptions(this, base.gameObject);
		component.iconSprite.sprite = this.spriteRenderer.sprite;
		component.iconSprite.color = this.spriteRenderer.color;
		component.iconSprite.SetNativeSize();
		component.iconSprite.rectTransform.sizeDelta = component.iconSprite.rectTransform.sizeDelta.normalized * 170f;
		if (this.spriteRenderer.flipX)
		{
			component.iconSprite.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x0002EF05 File Offset: 0x0002D105
	public override void OnCursorEnd()
	{
		this.RemoveCard();
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x0002EF0D File Offset: 0x0002D10D
	private void RemoveCard()
	{
		if (this.previewCard)
		{
			Object.Destroy(this.previewCard);
		}
		this.timeToDisplayCard = 0f;
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x0002EF32 File Offset: 0x0002D132
	public void SetStatsHealth()
	{
		this.stats.SetHealth(this.health);
		this.stats.SetMaxHP(this.health);
	}

	// Token: 0x04000369 RID: 873
	[SerializeField]
	private Sprite attackSprite;

	// Token: 0x0400036A RID: 874
	[SerializeField]
	private Sprite blockSprite;

	// Token: 0x0400036B RID: 875
	[SerializeField]
	private UnityEvent onDeath;

	// Token: 0x0400036C RID: 876
	[SerializeField]
	private bool ignoreOrderAndAlwaysDoActionsInOrder;

	// Token: 0x0400036D RID: 877
	[SerializeField]
	private List<GameObject> statusEffectsToStartWithPrefabs;

	// Token: 0x0400036E RID: 878
	[SerializeField]
	private List<GameObject> statusEffectsToGainEachTurnPrefabs;

	// Token: 0x0400036F RID: 879
	[SerializeField]
	public List<MetaProgressSaveManager.MetaProgressCondition> conditionsForCreateEncounter = new List<MetaProgressSaveManager.MetaProgressCondition>();

	// Token: 0x04000370 RID: 880
	[SerializeField]
	private GameObject objectToCreateOnEncounter;

	// Token: 0x04000371 RID: 881
	[SerializeField]
	public List<MetaProgressSaveManager.MetaProgressCondition> conditionsForDefeatEncounter = new List<MetaProgressSaveManager.MetaProgressCondition>();

	// Token: 0x04000372 RID: 882
	[SerializeField]
	private GameObject objectToCreateOnDefeatEncounter;

	// Token: 0x04000373 RID: 883
	[SerializeField]
	private GameObject objectToCreateOnDefeatPrada;

	// Token: 0x04000374 RID: 884
	[SerializeField]
	private MetaProgressSaveManager.MetaProgressMarker metaProgressMarkerOnMeeting;

	// Token: 0x04000375 RID: 885
	[SerializeField]
	private MetaProgressSaveManager.MetaProgressMarker metaProgressMarkerOnDefeating;

	// Token: 0x04000376 RID: 886
	[SerializeField]
	public bool isPet;

	// Token: 0x04000377 RID: 887
	[SerializeField]
	public PetItem2 petItem2;

	// Token: 0x04000378 RID: 888
	public static List<Enemy> allEnemies = new List<Enemy>();

	// Token: 0x04000379 RID: 889
	[SerializeField]
	public List<Character.CharacterName> validForCharacters;

	// Token: 0x0400037A RID: 890
	[SerializeField]
	private Transform enemyIntentParent;

	// Token: 0x0400037B RID: 891
	[SerializeField]
	private GameObject enemyIntentPrefab;

	// Token: 0x0400037C RID: 892
	private GameManager gameManager;

	// Token: 0x0400037D RID: 893
	private GameFlowManager gameFlowManager;

	// Token: 0x0400037E RID: 894
	[SerializeField]
	private Animator positionAnimator;

	// Token: 0x0400037F RID: 895
	[SerializeField]
	private Animator spriteAnimator;

	// Token: 0x04000380 RID: 896
	[SerializeField]
	private bool isShadow;

	// Token: 0x04000381 RID: 897
	[SerializeField]
	private GameObject transformParticles;

	// Token: 0x04000382 RID: 898
	private CombatPet combatPet;

	// Token: 0x04000383 RID: 899
	[SerializeField]
	private List<Enemy.EnemySwap> enemySwaps = new List<Enemy.EnemySwap>();

	// Token: 0x04000384 RID: 900
	[SerializeField]
	private List<Enemy.AlternateOutfitAnimator> alternateOutfitAnimators = new List<Enemy.AlternateOutfitAnimator>();

	// Token: 0x04000385 RID: 901
	[SerializeField]
	public Status stats;

	// Token: 0x04000386 RID: 902
	private float timeToDisplayCard;

	// Token: 0x04000387 RID: 903
	[SerializeField]
	private GameObject cardPrefab;

	// Token: 0x04000388 RID: 904
	private GameObject previewCard;

	// Token: 0x04000389 RID: 905
	public bool isTargetable;

	// Token: 0x0400038A RID: 906
	public int goldHeld;

	// Token: 0x0400038B RID: 907
	[SerializeField]
	private StatusEffect statusEffectPrefab;

	// Token: 0x0400038C RID: 908
	[SerializeField]
	public List<Item2.CombattEffect> combattEffects;

	// Token: 0x0400038D RID: 909
	[SerializeField]
	public int health = 3;

	// Token: 0x0400038E RID: 910
	[SerializeField]
	public int xp = 3;

	// Token: 0x0400038F RID: 911
	[SerializeField]
	private List<Enemy.OnDeathEffect> onDeathEffects = new List<Enemy.OnDeathEffect>();

	// Token: 0x04000390 RID: 912
	[SerializeField]
	public List<Enemy.PossibleAttack> possibleAttacks;

	// Token: 0x04000391 RID: 913
	[Header("------------------------Alternates for Shift change------------------------")]
	[SerializeField]
	public string alternateName;

	// Token: 0x04000392 RID: 914
	[SerializeField]
	public RuntimeAnimatorController defaultAnimator;

	// Token: 0x04000393 RID: 915
	[SerializeField]
	public RuntimeAnimatorController alternateAnimator;

	// Token: 0x04000394 RID: 916
	[SerializeField]
	public List<Enemy.PossibleAttack> alternatePossibleAttacks;

	// Token: 0x04000395 RID: 917
	public bool isPerformingAlternateAttacks;

	// Token: 0x04000396 RID: 918
	[Header("------------------------Alternates for Shift change------------------------")]
	[SerializeField]
	public List<Enemy.PossibleAttack> possibleAttacksStart;

	// Token: 0x04000397 RID: 919
	[NonSerialized]
	public Enemy.PossibleAttack myNextAttack;

	// Token: 0x04000398 RID: 920
	[HideInInspector]
	public Enemy.PossibleAttack myOverrideIntention;

	// Token: 0x04000399 RID: 921
	private BoxCollider2D boxCollider2D;

	// Token: 0x0400039A RID: 922
	[HideInInspector]
	public bool dead;

	// Token: 0x0400039B RID: 923
	[HideInInspector]
	public string displayName;

	// Token: 0x0400039C RID: 924
	[SerializeField]
	public List<GameObject> rewardItems;

	// Token: 0x0400039D RID: 925
	[SerializeField]
	public List<string> displayDesc;

	// Token: 0x0400039E RID: 926
	[SerializeField]
	private Sprite[] intentionSprites;

	// Token: 0x0400039F RID: 927
	[NonSerialized]
	public bool isSummon;

	// Token: 0x040003A0 RID: 928
	[SerializeField]
	private GameObject enemySpacerPrefab;

	// Token: 0x040003A1 RID: 929
	[SerializeField]
	public Transform mySpacerLocation;

	// Token: 0x040003A2 RID: 930
	[SerializeField]
	private List<Enemy.EnemyProperty> enemyProperties = new List<Enemy.EnemyProperty>();

	// Token: 0x040003A3 RID: 931
	private int actionNum;

	// Token: 0x040003A4 RID: 932
	private SpriteRenderer spriteRenderer;

	// Token: 0x040003A5 RID: 933
	private Player player;

	// Token: 0x040003A6 RID: 934
	private bool setHealthToStats = true;

	// Token: 0x040003A7 RID: 935
	private bool startFlipped;

	// Token: 0x040003A8 RID: 936
	public bool deathAbruptlyEndsCombat;

	// Token: 0x040003A9 RID: 937
	public List<Enemy.PossibleAttack> storedAttacks = new List<Enemy.PossibleAttack>();

	// Token: 0x020002D0 RID: 720
	[Serializable]
	private class AlternateOutfitAnimator
	{
		// Token: 0x040010D9 RID: 4313
		public Enemy.AlternateOutfitAnimator.Type type;

		// Token: 0x040010DA RID: 4314
		public float value;

		// Token: 0x040010DB RID: 4315
		public RuntimeAnimatorController animator;

		// Token: 0x0200048E RID: 1166
		public enum Type
		{
			// Token: 0x04001A92 RID: 6802
			randomChance,
			// Token: 0x04001A93 RID: 6803
			transition,
			// Token: 0x04001A94 RID: 6804
			transitionOnDeath
		}
	}

	// Token: 0x020002D1 RID: 721
	[Serializable]
	public class EnemySwap
	{
		// Token: 0x040010DC RID: 4316
		public Enemy.EnemySwap.StatusMigrationStyle statusMigrationStyle;

		// Token: 0x040010DD RID: 4317
		public Enemy.EnemySwap.Condition condition;

		// Token: 0x040010DE RID: 4318
		public Enemy newEnemy;

		// Token: 0x040010DF RID: 4319
		public bool doParticles;

		// Token: 0x0200048F RID: 1167
		public enum Condition
		{
			// Token: 0x04001A96 RID: 6806
			onDeath
		}

		// Token: 0x02000490 RID: 1168
		public enum StatusMigrationStyle
		{
			// Token: 0x04001A98 RID: 6808
			resetHealthKeepStatusConditions,
			// Token: 0x04001A99 RID: 6809
			keepHealthPercentageKeepStatusConditions,
			// Token: 0x04001A9A RID: 6810
			resetHealthKeepStatusConditionsButNotPowers
		}
	}

	// Token: 0x020002D2 RID: 722
	[Serializable]
	public class Attack
	{
		// Token: 0x060014AE RID: 5294 RVA: 0x000B4B9D File Offset: 0x000B2D9D
		public Enemy.Attack Clone()
		{
			return (Enemy.Attack)base.MemberwiseClone();
		}

		// Token: 0x040010E0 RID: 4320
		public Item2.Effect.Target target;

		// Token: 0x040010E1 RID: 4321
		public EventButton eventButton;

		// Token: 0x040010E2 RID: 4322
		public Enemy.Attack.Type type;

		// Token: 0x040010E3 RID: 4323
		public string startingDescription;

		// Token: 0x040010E4 RID: 4324
		public bool reactionary;

		// Token: 0x040010E5 RID: 4325
		[NonSerialized]
		public string description;

		// Token: 0x040010E6 RID: 4326
		public Sprite sprite;

		// Token: 0x040010E7 RID: 4327
		public Vector2 powerRange = new Vector2(-999f, -999f);

		// Token: 0x040010E8 RID: 4328
		[HideInInspector]
		public int currentPower;

		// Token: 0x040010E9 RID: 4329
		[HideInInspector]
		public bool powerIsSet;

		// Token: 0x040010EA RID: 4330
		[SerializeField]
		public RuntimeAnimatorController animator;

		// Token: 0x040010EB RID: 4331
		[SerializeField]
		public List<GameObject> prefabs;

		// Token: 0x040010EC RID: 4332
		[SerializeField]
		public bool isNumeric;

		// Token: 0x040010ED RID: 4333
		[SerializeField]
		public bool hideIntention;

		// Token: 0x040010EE RID: 4334
		[SerializeField]
		public bool ignoreScaling;

		// Token: 0x040010EF RID: 4335
		[SerializeField]
		public Enemy.Attack.AttackAnimation attackAnimation;

		// Token: 0x02000491 RID: 1169
		public enum Type
		{
			// Token: 0x04001A9C RID: 6812
			attack,
			// Token: 0x04001A9D RID: 6813
			block,
			// Token: 0x04001A9E RID: 6814
			poison,
			// Token: 0x04001A9F RID: 6815
			spikes,
			// Token: 0x04001AA0 RID: 6816
			regen,
			// Token: 0x04001AA1 RID: 6817
			summon,
			// Token: 0x04001AA2 RID: 6818
			pass,
			// Token: 0x04001AA3 RID: 6819
			heal,
			// Token: 0x04001AA4 RID: 6820
			vampire,
			// Token: 0x04001AA5 RID: 6821
			hazard,
			// Token: 0x04001AA6 RID: 6822
			burn,
			// Token: 0x04001AA7 RID: 6823
			dodge,
			// Token: 0x04001AA8 RID: 6824
			slow,
			// Token: 0x04001AA9 RID: 6825
			weak,
			// Token: 0x04001AAA RID: 6826
			rage,
			// Token: 0x04001AAB RID: 6827
			haste,
			// Token: 0x04001AAC RID: 6828
			selfDestruct,
			// Token: 0x04001AAD RID: 6829
			poisonSickness,
			// Token: 0x04001AAE RID: 6830
			runAway,
			// Token: 0x04001AAF RID: 6831
			steal,
			// Token: 0x04001AB0 RID: 6832
			freeze,
			// Token: 0x04001AB1 RID: 6833
			curseStatus,
			// Token: 0x04001AB2 RID: 6834
			addStatusEffect,
			// Token: 0x04001AB3 RID: 6835
			changeAnimator
		}

		// Token: 0x02000492 RID: 1170
		public enum AttackAnimation
		{
			// Token: 0x04001AB5 RID: 6837
			attack,
			// Token: 0x04001AB6 RID: 6838
			buff,
			// Token: 0x04001AB7 RID: 6839
			none
		}
	}

	// Token: 0x020002D3 RID: 723
	[Serializable]
	public class PossibleAttack
	{
		// Token: 0x060014B0 RID: 5296 RVA: 0x000B4BC7 File Offset: 0x000B2DC7
		public Enemy.PossibleAttack Clone()
		{
			return (Enemy.PossibleAttack)base.MemberwiseClone();
		}

		// Token: 0x040010F0 RID: 4336
		public bool alwaysConsiderFirst;

		// Token: 0x040010F1 RID: 4337
		public List<GameObject> skipIfEnemyExists;

		// Token: 0x040010F2 RID: 4338
		public List<Enemy.Attack> attacks;

		// Token: 0x040010F3 RID: 4339
		public int coolDown = -999;

		// Token: 0x040010F4 RID: 4340
		[HideInInspector]
		public int currentCoolDown;
	}

	// Token: 0x020002D4 RID: 724
	[Serializable]
	public class OnDeathEffect
	{
		// Token: 0x040010F5 RID: 4341
		public Enemy.OnDeathEffect.Type type;

		// Token: 0x040010F6 RID: 4342
		public List<GameObject> objs = new List<GameObject>();

		// Token: 0x02000493 RID: 1171
		public enum Type
		{
			// Token: 0x04001AB9 RID: 6841
			summonOnKill,
			// Token: 0x04001ABA RID: 6842
			changeBadger
		}
	}

	// Token: 0x020002D5 RID: 725
	[Serializable]
	public class EnemyProperty
	{
		// Token: 0x040010F7 RID: 4343
		public Enemy.EnemyProperty.Type type;

		// Token: 0x040010F8 RID: 4344
		public int value;

		// Token: 0x02000494 RID: 1172
		public enum Type
		{
			// Token: 0x04001ABC RID: 6844
			cowardly,
			// Token: 0x04001ABD RID: 6845
			blocking
		}
	}
}
