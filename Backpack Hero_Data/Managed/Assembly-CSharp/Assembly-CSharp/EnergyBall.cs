using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x020000AB RID: 171
public class EnergyBall : MonoBehaviour
{
	// Token: 0x17000009 RID: 9
	// (get) Token: 0x060003FE RID: 1022 RVA: 0x00027AE5 File Offset: 0x00025CE5
	// (set) Token: 0x060003FF RID: 1023 RVA: 0x00027AED File Offset: 0x00025CED
	public Vector2 currentDirection { get; private set; }

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000400 RID: 1024 RVA: 0x00027AF6 File Offset: 0x00025CF6
	// (set) Token: 0x06000401 RID: 1025 RVA: 0x00027AFD File Offset: 0x00025CFD
	public static Vector2 publicCurrentDirection { get; private set; }

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000402 RID: 1026 RVA: 0x00027B05 File Offset: 0x00025D05
	// (set) Token: 0x06000403 RID: 1027 RVA: 0x00027B0D File Offset: 0x00025D0D
	public Quaternion currentRotation { get; private set; }

	// Token: 0x06000404 RID: 1028 RVA: 0x00027B18 File Offset: 0x00025D18
	public void AddSpecialMovement(Vector2 movement, Item2 item)
	{
		if (this.itemEffectingMovements)
		{
			float num = Vector2.Distance(this.itemEffectingMovements.transform.position, base.transform.position);
			if (Vector2.Distance(item.transform.position, base.transform.position) >= num)
			{
				return;
			}
		}
		this.itemEffectingMovements = item;
		this.specialMovementsToPerform.Clear();
		this.specialMovementsToPerform.Add(movement);
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x00027BA4 File Offset: 0x00025DA4
	public void ClearSpecialMovement()
	{
		this.itemEffectingMovements = null;
		this.specialMovementsToPerform.Clear();
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x00027BB8 File Offset: 0x00025DB8
	private void OnEnable()
	{
		if (EnergyBall.energyBalls.Count == 0)
		{
			EnergyBall.ResetAllHeat();
		}
		if (!EnergyBall.energyBalls.Contains(this))
		{
			EnergyBall.energyBalls.Add(this);
		}
		this.energyBallNum = CR8Manager.instance.energyBallNum;
		CR8Manager.instance.energyBallNum++;
		EnergyBall.energyBalls.Sort((EnergyBall x, EnergyBall y) => x.energyBallNum.CompareTo(y.energyBallNum));
		EnergyBall.energyBalls.Reverse();
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x00027C44 File Offset: 0x00025E44
	private void OnDisable()
	{
		EnergyBall.energyBalls.Remove(this);
		EnergyBall.energyBalls.Sort((EnergyBall x, EnergyBall y) => x.energyBallNum.CompareTo(y.energyBallNum));
		EnergyBall.energyBalls.Reverse();
		if (CR8Manager.instance && EnergyBall.energyBalls.Count == 0)
		{
			CR8Manager.instance.energyBallNum = 0;
		}
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x00027CB4 File Offset: 0x00025EB4
	public static void ResetAllHeat()
	{
		foreach (Item2 item in Item2.GetAllItemsInGrid())
		{
			EnergyEmitter component = item.GetComponent<EnergyEmitter>();
			if (component)
			{
				component.ResetHeat();
			}
		}
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x00027D14 File Offset: 0x00025F14
	public static void EndAllEnergyBalls()
	{
		while (EnergyBall.energyBalls.Count > 0)
		{
			if (EnergyBall.energyBalls[0])
			{
				EnergyBall.energyBalls[0].DestroyEnergy();
			}
			EnergyBall.energyBalls.RemoveAt(0);
		}
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x00027D54 File Offset: 0x00025F54
	public static EnergyBall GetCurrentEnergyBall()
	{
		if (EnergyBall.energyBalls.Count == 0)
		{
			return null;
		}
		for (int i = EnergyBall.energyBalls.Count - 1; i >= 0; i--)
		{
			EnergyBall energyBall = EnergyBall.energyBalls[i];
			if (energyBall && energyBall.gameObject && energyBall.gameObject.activeInHierarchy && !energyBall.isDestroying)
			{
				return energyBall;
			}
		}
		return null;
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x00027DC0 File Offset: 0x00025FC0
	public int CheckForEnergyEmitter(EnergyEmitter energyEmitter)
	{
		foreach (EnergyBall.EnergyEmitterUsed energyEmitterUsed in this.energyEmittersUsed)
		{
			if (energyEmitterUsed.energyEmitter == energyEmitter)
			{
				return energyEmitterUsed.timesUsed;
			}
		}
		return -1;
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x00027E28 File Offset: 0x00026028
	public void AddEnergyEmitter(EnergyEmitter energyEmitter)
	{
		foreach (EnergyBall.EnergyEmitterUsed energyEmitterUsed in this.energyEmittersUsed)
		{
			if (energyEmitterUsed.energyEmitter == energyEmitter)
			{
				energyEmitterUsed.timesUsed++;
				return;
			}
		}
		EnergyBall.EnergyEmitterUsed energyEmitterUsed2 = new EnergyBall.EnergyEmitterUsed();
		energyEmitterUsed2.energyEmitter = energyEmitter;
		energyEmitterUsed2.timesUsed = 1;
		this.energyEmittersUsed.Add(energyEmitterUsed2);
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x00027EB4 File Offset: 0x000260B4
	private void Start()
	{
		this.timer = 0f;
		this.MakeAssociations();
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x00027EC8 File Offset: 0x000260C8
	private void MakeAssociations()
	{
		if (this.madeAssociations)
		{
			return;
		}
		this.madeAssociations = true;
		this.DisplayEnergy();
		this.filteredItemTypes = new List<Item2.ItemType>();
		this.isDestroying = false;
		this.isSpinning = true;
		this.particleEffectParent.transform.rotation = Quaternion.Euler(0f, 0f, (float)Random.Range(0, 360));
		if (!this.gameManager)
		{
			this.gameManager = GameManager.main;
		}
		if (!this.gameFlowManager)
		{
			this.gameFlowManager = GameFlowManager.main;
		}
		if (!this.player)
		{
			this.player = Player.main;
		}
		if (!this.cR8Manager)
		{
			this.cR8Manager = Object.FindObjectOfType<CR8Manager>();
		}
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x00027F90 File Offset: 0x00026190
	private void Update()
	{
		if (this.isSpinning)
		{
			this.spinningTime += Time.deltaTime;
			if (this.spinningTime > 0.2f)
			{
				this.particleEffect.transform.localPosition = Vector3.MoveTowards(this.particleEffect.transform.localPosition, new Vector3(0.35f, 0f, 0f), Time.deltaTime * this.speed / 2f);
				this.particleEffectParent.transform.Rotate(new Vector3(0f, 0f, 90f) * Time.deltaTime * this.speed);
				return;
			}
		}
		else
		{
			this.spinningTime = 0f;
			this.particleEffect.transform.localPosition = Vector3.MoveTowards(this.particleEffect.transform.localPosition, Vector3.zero, Time.deltaTime * this.speed / 4f);
		}
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x00028096 File Offset: 0x00026296
	public IEnumerator MoveEnergyBall(bool test = false)
	{
		this.MakeAssociations();
		List<Item2> items = new List<Item2>();
		List<GridSquare> grids = new List<GridSquare>();
		bool forceReplay = false;
		for (;;)
		{
			EnergyBall.currentGrid = null;
			forceReplay = false;
			if (!this || !base.gameObject || this.isDestroying)
			{
				break;
			}
			if (this.energyValue <= 0)
			{
				goto Block_4;
			}
			SoundManager.main.PlaySFXPitched("elecHum", Random.Range(0.95f, 1.05f), false);
			Vector3 destination = base.transform.localPosition + base.transform.up;
			this.currentDirection = base.transform.up;
			this.currentRotation = base.transform.rotation;
			if (this.specialMovementsToPerform.Count != 0)
			{
				destination = base.transform.localPosition + this.specialMovementsToPerform[0];
				this.currentDirection = this.specialMovementsToPerform[0];
				Vector3 vector = destination - base.transform.position;
				float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
				this.currentRotation = Quaternion.Euler(0f, 0f, num);
				this.specialMovementsToPerform.RemoveAt(0);
			}
			while (Vector3.Distance(base.transform.localPosition, destination) > 0.1f)
			{
				this.isSpinning = false;
				base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, destination, Time.deltaTime * this.speed * Mathf.Max(Singleton.Instance.itemAnimationSpeed, 0.1f));
				yield return null;
				if (!this || !base.gameObject || this.isDestroying)
				{
					goto IL_0236;
				}
				if (this.particleEffect && this.particleEffect.gameObject && !this.particleEffect.gameObject.activeInHierarchy)
				{
					this.particleEffect.gameObject.SetActive(true);
				}
			}
			this.spacesTraveled++;
			this.isSpinning = true;
			base.transform.localPosition = Vector3Int.RoundToInt(destination);
			foreach (GridObject gridObject in GridObject.GetItemsAtPosition(base.transform.position))
			{
				GridSquare component = gridObject.GetComponent<GridSquare>();
				if (component)
				{
					EnergyBall.currentGrid = component;
					break;
				}
			}
			this.ClearSpecialMovement();
			items = new List<Item2>();
			grids = new List<GridSquare>();
			yield return new WaitForFixedUpdate();
			if (!this || !base.gameObject || this.isDestroying)
			{
				goto IL_037F;
			}
			while (Singleton.Instance.showingOptions)
			{
				yield return null;
			}
			if (!this || !base.gameObject || this.isDestroying)
			{
				goto IL_03CB;
			}
			while (!this.cR8Manager.isTesting && Enemy.allEnemies.Count >= 1 && (!this.gameManager.targetedEnemy || this.gameManager.targetedEnemy.dead))
			{
				yield return null;
			}
			if (!this || !base.gameObject || this.isDestroying)
			{
				goto IL_0449;
			}
			EnergyBall.publicCurrentDirection = this.currentDirection;
			EnergyBall.startedByEnergyBall = true;
			GameFlowManager.main.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onEnergyMove, null, null, true, false);
			while (GameFlowManager.main.isCheckingEffects)
			{
				yield return null;
			}
			if (!this || !base.gameObject || this.isDestroying)
			{
				goto IL_04B8;
			}
			Item2.TestAtVectorPublic(items, grids, base.transform.position);
			foreach (Item2 item in items)
			{
				if (item.itemMovement.inGrid)
				{
					this.gameFlowManager.battlePhase = GameFlowManager.BattlePhase.playerTurn;
					bool flag = item.CanBeUsedActive(false, item.costs, true, false, null, false);
					if (flag && this.filteredItemTypes.Count > 0 && !Item2.ShareItemTypes(this.filteredItemTypes, item.itemType))
					{
						flag = false;
					}
					EnergyEmitter ee = item.GetComponent<EnergyEmitter>();
					if (flag)
					{
						if (ee && ee.acceptableEntraces.Count >= 1 && !EnergyEmitter.CorrectDirection(this.currentDirection, item.transform, ee.acceptableEntraces))
						{
							continue;
						}
						int currentCost = Item2.GetCurrentCost(Item2.Cost.Type.energy, item.costs);
						if (this.energyValue < currentCost)
						{
							this.gameManager.CreatePopUpWorld(LangaugeManager.main.GetTextByKey("gm15"), base.transform.position);
							this.energyValue = 0;
							break;
						}
						if (this.energyValue >= 0)
						{
							List<Item2.CombattEffect> combattEffects = new List<Item2.CombattEffect>();
							if (item.CheckForStatusEffect(Item2.ItemStatusEffect.Type.strengthBasedOnDistance))
							{
								foreach (Item2.CombattEffect combattEffect in item.combatEffects)
								{
									if (combattEffect.effect.value == 0f)
									{
										combattEffect.effect.value += (float)(this.spacesTraveled * item.GetStatusEffectValue(Item2.ItemStatusEffect.Type.strengthBasedOnDistance));
									}
									combattEffects.Add(combattEffect);
								}
							}
							if (!ee || (ee.ConsiderChargeUseLimit(this) && ee.playable))
							{
								yield return this.gameFlowManager.UseItem(item, false, false, Item2.PlayerAnimation.UseDefault, true, test);
							}
							if (item.CheckForStatusEffect(Item2.ItemStatusEffect.Type.strengthBasedOnDistance))
							{
								foreach (Item2.CombattEffect combattEffect2 in combattEffects)
								{
									combattEffect2.effect.value = 0f;
								}
							}
							combattEffects = null;
						}
						if (!this || !base.gameObject)
						{
							yield break;
						}
						if (ee)
						{
							SoundManager.main.PlaySFX("elecActivateComponent");
							ee.Action(this, this.currentDirection);
						}
						if (this.energyValue <= 0)
						{
							this.DestroyEnergy();
							yield break;
						}
						if (!this || !base.gameObject || this.isDestroying)
						{
							yield break;
						}
					}
					ee = null;
					item = null;
				}
			}
			List<Item2>.Enumerator enumerator2 = default(List<Item2>.Enumerator);
			if (this.cR8Manager.exit)
			{
				goto Block_27;
			}
			while (this.gameManager.inSpecialReorg)
			{
				yield return null;
			}
			if (grids.Count == 0)
			{
				Item2 itemWithStatusEffect = Item2.GetItemWithStatusEffect(Item2.ItemStatusEffect.Type.CR8ChargesReverseWhenOffGrid, null, false);
				if (itemWithStatusEffect)
				{
					base.transform.position = itemWithStatusEffect.transform.position;
					base.transform.rotation = itemWithStatusEffect.transform.rotation;
					if (this.energyValue > 0)
					{
						forceReplay = true;
					}
				}
			}
			destination = default(Vector3);
			if (grids.Count == 0 && !forceReplay)
			{
				goto Block_32;
			}
		}
		yield break;
		Block_4:
		this.DestroyEnergy();
		yield break;
		IL_0236:
		yield break;
		IL_037F:
		yield break;
		IL_03CB:
		yield break;
		IL_0449:
		yield break;
		IL_04B8:
		yield break;
		Block_27:
		this.DestroyEnergy();
		yield break;
		Block_32:
		this.DestroyEnergy();
		yield break;
		yield break;
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x000280AC File Offset: 0x000262AC
	public void ChangeEnergy(int num)
	{
		this.energyValue += num;
		this.DisplayEnergy();
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x000280C4 File Offset: 0x000262C4
	public void DisplayEnergy()
	{
		if (this.energyValue > 0)
		{
			Object.Instantiate<GameObject>(this.textPrefab, base.transform.position + Vector3.back, Quaternion.identity, base.transform.parent).GetComponentInChildren<TextMeshPro>().text = this.energyValue.ToString() ?? "";
		}
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x00028128 File Offset: 0x00026328
	public void DestroyEnergy()
	{
		if (!this.isDestroying)
		{
			this.isDestroying = true;
			base.StartCoroutine(this.DestroyDelay());
		}
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x00028146 File Offset: 0x00026346
	private IEnumerator DestroyDelay()
	{
		if (this.text)
		{
			this.text.gameObject.SetActive(false);
		}
		SoundManager.main.PlaySFX("elecDissipate");
		this.isSpinning = false;
		Object.Instantiate<GameObject>(this.particleEffectPuffPrefab, base.transform.position, Quaternion.identity, base.transform.parent);
		yield return new WaitForSeconds(0.5f);
		while (GameFlowManager.main.isCheckingEffects)
		{
			yield return null;
		}
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x040002F9 RID: 761
	public static List<EnergyBall> energyBalls = new List<EnergyBall>();

	// Token: 0x040002FA RID: 762
	[SerializeField]
	private float speed;

	// Token: 0x040002FB RID: 763
	[SerializeField]
	private ParticleSystem particleEffect;

	// Token: 0x040002FC RID: 764
	[SerializeField]
	private ParticleSystem particleEffect2;

	// Token: 0x040002FD RID: 765
	[SerializeField]
	private GameObject particleEffectParent;

	// Token: 0x040002FE RID: 766
	[SerializeField]
	private GameObject particleEffectPuffPrefab;

	// Token: 0x040002FF RID: 767
	[SerializeField]
	private TextMeshPro text;

	// Token: 0x04000300 RID: 768
	private bool isSpinning = true;

	// Token: 0x04000301 RID: 769
	private GameManager gameManager;

	// Token: 0x04000302 RID: 770
	private GameFlowManager gameFlowManager;

	// Token: 0x04000303 RID: 771
	private CR8Manager cR8Manager;

	// Token: 0x04000304 RID: 772
	public int energyValue = 3;

	// Token: 0x04000305 RID: 773
	[SerializeField]
	private GameObject textPrefab;

	// Token: 0x04000306 RID: 774
	public bool isDestroying;

	// Token: 0x04000307 RID: 775
	public List<Item2.ItemType> filteredItemTypes;

	// Token: 0x04000308 RID: 776
	private bool madeAssociations;

	// Token: 0x04000309 RID: 777
	public int spacesTraveled;

	// Token: 0x0400030A RID: 778
	private float timer;

	// Token: 0x0400030B RID: 779
	private Player player;

	// Token: 0x0400030C RID: 780
	private float maxTime = 30f;

	// Token: 0x0400030D RID: 781
	public static GridSquare currentGrid;

	// Token: 0x04000310 RID: 784
	public static bool startedByEnergyBall = false;

	// Token: 0x04000311 RID: 785
	private List<Vector2> specialMovementsToPerform = new List<Vector2>();

	// Token: 0x04000313 RID: 787
	private Item2 itemEffectingMovements;

	// Token: 0x04000314 RID: 788
	public List<EnergyBall.EnergyEmitterUsed> energyEmittersUsed = new List<EnergyBall.EnergyEmitterUsed>();

	// Token: 0x04000315 RID: 789
	public int energyBallNum;

	// Token: 0x04000316 RID: 790
	private float spinningTime;

	// Token: 0x020002BE RID: 702
	public class EnergyEmitterUsed
	{
		// Token: 0x04001082 RID: 4226
		public EnergyEmitter energyEmitter;

		// Token: 0x04001083 RID: 4227
		public int timesUsed;
	}
}
