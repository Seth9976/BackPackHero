using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

// Token: 0x020000AC RID: 172
public class EnergyEmitter : MonoBehaviour
{
	// Token: 0x06000417 RID: 1047 RVA: 0x000281A0 File Offset: 0x000263A0
	public void HideHeatParticles()
	{
		if (this.heatParticles)
		{
			this.heatParticles.Stop();
			this.heatParticles.gameObject.SetActive(false);
		}
		if (this.heatParticlesOnOverheat)
		{
			this.heatParticlesOnOverheat.Stop();
			this.heatParticlesOnOverheat.gameObject.SetActive(false);
		}
		if (this.heatInformation)
		{
			this.heatInformation.SetActive(false);
		}
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x00028218 File Offset: 0x00026418
	private void Start()
	{
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.CompareTag("DestroyOnLoad"))
			{
				Object.Destroy(transform.gameObject);
			}
		}
		if (this.heatInformation)
		{
			Object.Destroy(this.heatInformation);
		}
		this.heatInformation = Object.Instantiate<GameObject>(GameManager.main.emitterHeatInformationGameObject, base.transform.position, Quaternion.identity, base.transform);
		this.heatInformation.SetActive(true);
		this.heatParticles = this.heatInformation.transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
		this.heatParticlesOnOverheat = this.heatInformation.transform.GetChild(1).GetComponentInChildren<ParticleSystem>();
		this.overheatTextAnimator = this.heatInformation.GetComponentInChildren<Animator>();
		this.overheatTextAnimator.gameObject.SetActive(false);
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		this.myItem = base.GetComponent<Item2>();
		this.cR8Manager = Object.FindObjectOfType<CR8Manager>();
		if (this.type == EnergyEmitter.Type.toggle || this.type == EnergyEmitter.Type.switcher)
		{
			this.TurnToggleOff();
		}
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x00028368 File Offset: 0x00026568
	private void Update()
	{
		if (this.sprites.Count > 0 && this.spriteRenderer && (this.type == EnergyEmitter.Type.switcher || this.type == EnergyEmitter.Type.toggle))
		{
			if (this.toggle == 0)
			{
				if (this.spriteRenderer.sprite != this.sprites[0])
				{
					this.spriteRenderer.sprite = this.sprites[0];
					this.TurnToggleOff();
					return;
				}
			}
			else if (this.spriteRenderer.sprite != this.sprites[1])
			{
				this.spriteRenderer.sprite = this.sprites[1];
				this.TurnToggleOn();
			}
		}
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x00028428 File Offset: 0x00026628
	public void ResetHeat()
	{
		this.currentHeat = 0;
		if (this.heatParticlesOnOverheat && this.heatParticlesOnOverheat.gameObject.activeInHierarchy)
		{
			this.heatParticlesOnOverheat.Stop();
		}
		GameFlowManager.main.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onHeatReset, this.myItem, null, true, false);
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x0002847C File Offset: 0x0002667C
	public void ChangeHeat(int num)
	{
		this.currentHeat += num;
		this.currentHeat = Mathf.Clamp(this.currentHeat, 0, this.maxHeat + 1);
		if (this.currentHeat == this.maxHeat && this.heatParticlesOnOverheat && this.heatParticlesOnOverheat.gameObject.activeInHierarchy)
		{
			this.heatParticlesOnOverheat.Play();
		}
		if (this.currentHeat < this.maxHeat && this.heatParticlesOnOverheat && this.heatParticlesOnOverheat.gameObject.activeInHierarchy)
		{
			this.heatParticlesOnOverheat.Stop();
		}
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x00028521 File Offset: 0x00026721
	public bool WillBeOverheated()
	{
		return this.currentHeat + 1 > this.maxHeat;
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x00028533 File Offset: 0x00026733
	public bool IsOverheated()
	{
		return this.currentHeat > this.maxHeat;
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x00028543 File Offset: 0x00026743
	public void AutoCreateEnergy()
	{
		if (this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.runsAutomaticallyOnCoreUse))
		{
			this.CreateEnergy();
		}
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x0002855C File Offset: 0x0002675C
	public void CreateEnergy()
	{
		if (!this.energyBallPrefab)
		{
			this.energyBallPrefab = this.cR8Manager.cr8energyFlowPrefabBackup;
		}
		if (this.type == EnergyEmitter.Type.creator || this.type == EnergyEmitter.Type.newCharge)
		{
			Item2 component = base.GetComponent<Item2>();
			if (component && component.itemType.Contains(Item2.ItemType.Core))
			{
				Player.main.HideAP();
			}
			GameObject gameObject = Object.Instantiate<GameObject>(this.energyBallPrefab, base.transform.position + Vector3.back, base.transform.rotation, base.transform.parent);
			if (this.energyValueType == EnergyEmitter.EnergyValueType.setValue)
			{
				gameObject.GetComponent<EnergyBall>().energyValue = this.value;
				return;
			}
			if (this.energyValueType == EnergyEmitter.EnergyValueType.playerAP)
			{
				gameObject.GetComponent<EnergyBall>().energyValue = Player.main.AP;
				if (!this.cR8Manager.isTesting)
				{
					Player.main.AP = 0;
					return;
				}
			}
		}
		else if (this.type == EnergyEmitter.Type.newChargeDouble)
		{
			Object.Instantiate<GameObject>(this.energyBallPrefab, base.transform.position + Vector3.back * 0.01f, base.transform.rotation, base.transform.parent).GetComponent<EnergyBall>().energyValue = this.value;
			Object.Instantiate<GameObject>(this.energyBallPrefab, base.transform.position + Vector3.back * 0.01f, base.transform.rotation * Quaternion.Euler(0f, 0f, 180f), base.transform.parent).GetComponent<EnergyBall>().energyValue = this.value;
		}
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x00028718 File Offset: 0x00026918
	public void StartCombat()
	{
		this.ClearCharge();
		if (this.type == EnergyEmitter.Type.toggle)
		{
			this.TurnToggleOff();
		}
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x00028730 File Offset: 0x00026930
	public static bool CorrectDirection(Vector2 entranceDirection, Transform t, List<Vector2> acceptableEntraces)
	{
		if (acceptableEntraces.Count == 0)
		{
			return true;
		}
		foreach (Vector2 vector in acceptableEntraces)
		{
			Vector2 vector2 = t.InverseTransformVector(entranceDirection);
			if (Vector2Int.RoundToInt(vector) == Vector2Int.RoundToInt(vector2))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x000287AC File Offset: 0x000269AC
	public bool ConsiderChargeUseLimit(EnergyBall energyBall)
	{
		if (this.usesPerCharge != -1)
		{
			if (energyBall.CheckForEnergyEmitter(this) >= this.usesPerCharge)
			{
				return false;
			}
			energyBall.AddEnergyEmitter(this);
		}
		return true;
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x000287D0 File Offset: 0x000269D0
	public void Action(EnergyBall energyBall, Vector2 entranceDirection)
	{
		int energyValue = energyBall.energyValue;
		if (this.acceptableEntraces.Count > 0 && !EnergyEmitter.CorrectDirection(entranceDirection, base.transform, this.acceptableEntraces))
		{
			return;
		}
		if (energyBall.spacesTraveled < this.requiredDistance)
		{
			return;
		}
		this.myItem = base.GetComponent<Item2>();
		if (!this.myItem)
		{
			return;
		}
		List<Vector2> list = new List<Vector2>(this.acceptableEntraces);
		if (list.Count == 0)
		{
			list = this.myItem.GetAllEntrances();
		}
		float num = (float)(this.currentHeat + 1) / (float)this.maxHeat;
		if (EnergyEmitter.CorrectDirection(entranceDirection, base.transform, list))
		{
			this.currentHeat++;
			this.currentHeat = Mathf.Clamp(this.currentHeat, 0, this.maxHeat + 1);
			if (this.heatParticles && this.maxHeat > 0)
			{
				ParticleSystem.MainModule main = this.heatParticles.main;
				ParticleSystem.ShapeModule shape = this.heatParticles.shape;
				ParticleSystem.EmissionModule emission = this.heatParticles.emission;
				shape.scale = new Vector3(0.6f, 0.6f, 1f);
				emission.rateOverTime = Mathf.Lerp(25f, 100f, num);
				main.startSpeed = 2f;
				this.heatParticles.Play();
			}
		}
		if (this.maxHeat != 0)
		{
			if (this.currentHeat <= this.maxHeat)
			{
				if (this.currentHeat == this.maxHeat)
				{
					this.overheatTextAnimator.gameObject.SetActive(true);
					this.overheatTextAnimator.transform.parent.rotation = Quaternion.identity;
					this.overheatTextAnimator.transform.rotation = Quaternion.identity;
					this.overheatTextAnimator.GetComponentInChildren<TextMeshPro>().text = LangaugeManager.main.GetTextByKey("Max Heat");
					this.overheatTextAnimator.Play("maxHeatAnimation", 0, 0f);
					GameFlowManager.main.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onOverheat, this.myItem, null, true, false);
					if (this.heatParticlesOnOverheat && this.heatParticlesOnOverheat.gameObject.activeInHierarchy)
					{
						this.heatParticlesOnOverheat.Play();
					}
				}
			}
			else if (this.currentHeat == this.maxHeat + 1)
			{
				if (this.heatParticles)
				{
					ParticleSystem.MainModule main2 = this.heatParticles.main;
					ParticleSystem.ShapeModule shape2 = this.heatParticles.shape;
					ParticleSystem.EmissionModule emission2 = this.heatParticles.emission;
					shape2.scale = new Vector3(0.8f, 0.8f, 1f);
					emission2.rateOverTime = 500f;
					main2.startSpeed = 2.75f;
					this.heatParticles.Play();
				}
				if (this.overheatTextAnimator)
				{
					this.overheatTextAnimator.gameObject.SetActive(true);
					this.overheatTextAnimator.transform.parent.rotation = Quaternion.identity;
					this.overheatTextAnimator.transform.rotation = Quaternion.identity;
					this.overheatTextAnimator.GetComponentInChildren<TextMeshPro>().text = LangaugeManager.main.GetTextByKey("overHeat");
					this.overheatTextAnimator.Play("overheatAnimation", 0, 0f);
				}
				SoundManager.main.PlaySFX("overheat");
			}
			else if (this.IsOverheated())
			{
				if (this.overheatTextAnimator)
				{
					this.overheatTextAnimator.gameObject.SetActive(true);
					this.overheatTextAnimator.transform.parent.rotation = Quaternion.identity;
					this.overheatTextAnimator.transform.rotation = Quaternion.identity;
					this.overheatTextAnimator.GetComponentInChildren<TextMeshPro>().text = LangaugeManager.main.GetTextByKey("overHeat");
					this.overheatTextAnimator.Play("alreadyOverheatAnimation", 0, 0f);
				}
				SoundManager.main.PlaySFX("alreadyOverheated");
			}
		}
		Vector2 vector = base.transform.InverseTransformVector(entranceDirection);
		if (this.type == EnergyEmitter.Type.rotator)
		{
			if (vector == Vector2.right)
			{
				energyBall.transform.rotation *= Quaternion.Euler(0f, 0f, -90f);
			}
			else if (vector == Vector2.up)
			{
				energyBall.transform.rotation *= Quaternion.Euler(0f, 0f, 90f);
			}
		}
		else if (this.type == EnergyEmitter.Type.reverserWithExitOnOverheat)
		{
			if (this.currentHeat <= this.maxHeat)
			{
				energyBall.transform.rotation *= Quaternion.Euler(0f, 0f, 180f);
			}
			else if (vector == Vector2.right)
			{
				energyBall.transform.rotation *= Quaternion.Euler(0f, 0f, -90f);
			}
			else if (vector == Vector2.up)
			{
				energyBall.transform.rotation *= Quaternion.Euler(0f, 0f, 90f);
			}
		}
		else if (this.type == EnergyEmitter.Type.repeater)
		{
			if (!this.IsOverheated())
			{
				while (energyBall.energyValue > 0)
				{
					energyBall.energyValue--;
					GameObject gameObject = Object.Instantiate<GameObject>(energyBall.gameObject, base.transform.position + Vector3.back * 0.01f, energyBall.transform.rotation, base.transform.parent);
					gameObject.GetComponent<EnergyBall>().energyValue = 1;
					gameObject.transform.SetAsFirstSibling();
				}
				energyBall.DestroyEnergy();
			}
		}
		else if (this.type == EnergyEmitter.Type.splitter)
		{
			if (vector == Vector2.up)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(energyBall.gameObject, base.transform.position + Vector3.back * 0.01f, base.transform.rotation * Quaternion.Euler(0f, 0f, 90f), base.transform.parent);
				gameObject2.GetComponent<EnergyBall>().energyValue = Mathf.CeilToInt((float)energyBall.energyValue / 2f);
				gameObject2.transform.SetAsFirstSibling();
				if (energyBall.energyValue > 1)
				{
					GameObject gameObject3 = Object.Instantiate<GameObject>(energyBall.gameObject, base.transform.position + Vector3.back * 0.01f, base.transform.rotation * Quaternion.Euler(0f, 0f, 270f), base.transform.parent);
					gameObject3.GetComponent<EnergyBall>().energyValue = Mathf.CeilToInt((float)energyBall.energyValue / 2f);
					gameObject3.transform.SetAsFirstSibling();
				}
			}
			else if (vector == Vector2.right)
			{
				GameObject gameObject4 = Object.Instantiate<GameObject>(energyBall.gameObject, base.transform.position + Vector3.back * 0.01f, base.transform.rotation * Quaternion.Euler(0f, 0f, 180f), base.transform.parent);
				gameObject4.GetComponent<EnergyBall>().energyValue = Mathf.CeilToInt((float)energyBall.energyValue / 2f);
				gameObject4.transform.SetAsFirstSibling();
				if (energyBall.energyValue > 1)
				{
					GameObject gameObject5 = Object.Instantiate<GameObject>(energyBall.gameObject, base.transform.position + Vector3.back * 0.01f, base.transform.rotation * Quaternion.Euler(0f, 0f, -90f), base.transform.parent);
					gameObject5.GetComponent<EnergyBall>().energyValue = Mathf.CeilToInt((float)energyBall.energyValue / 2f);
					gameObject5.transform.SetAsFirstSibling();
				}
			}
			else if (vector == Vector2.left)
			{
				GameObject gameObject6 = Object.Instantiate<GameObject>(energyBall.gameObject, base.transform.position + Vector3.back * 0.01f, base.transform.rotation * Quaternion.Euler(0f, 0f, 180f), base.transform.parent);
				gameObject6.GetComponent<EnergyBall>().energyValue = Mathf.CeilToInt((float)energyBall.energyValue / 2f);
				gameObject6.transform.SetAsFirstSibling();
				if (energyBall.energyValue > 1)
				{
					GameObject gameObject7 = Object.Instantiate<GameObject>(energyBall.gameObject, base.transform.position + Vector3.back * 0.01f, base.transform.rotation * Quaternion.Euler(0f, 0f, 90f), base.transform.parent);
					gameObject7.GetComponent<EnergyBall>().energyValue = Mathf.CeilToInt((float)energyBall.energyValue / 2f);
					gameObject7.transform.SetAsFirstSibling();
				}
			}
			energyBall.DestroyEnergy();
		}
		else if (this.type == EnergyEmitter.Type.filter)
		{
			energyBall.energyValue += this.value;
			energyBall.filteredItemTypes = this.filteredItemTypes;
		}
		else if (this.type == EnergyEmitter.Type.charger)
		{
			if (this.heldCharge == -1)
			{
				this.spriteRenderer.sprite = this.sprites[1];
				this.heldCharge = energyBall.energyValue;
				energyBall.DestroyEnergy();
			}
			else
			{
				if (!this.energyBallPrefab)
				{
					this.energyBallPrefab = this.cR8Manager.cr8energyFlowPrefabBackup;
				}
				Object.Instantiate<GameObject>(this.energyBallPrefab, base.transform.position + Vector3.back * 0.01f, base.transform.rotation, base.transform.parent).GetComponent<EnergyBall>().energyValue = this.heldCharge * 2;
				this.ClearCharge();
				energyBall.DestroyEnergy();
			}
		}
		else
		{
			if (this.type == EnergyEmitter.Type.grinder)
			{
				List<Item2> list2 = new List<Item2>();
				this.myItem.FindItemsAndGridsinArea(list2, new List<GridSquare>(), new List<Item2.Area> { Item2.Area.topRotational }, Item2.AreaDistance.adjacent, null, null, null, true, false, true);
				using (List<Item2>.Enumerator enumerator = list2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Item2 item = enumerator.Current;
						int num2 = item.FindGridSpaces().Count * Mathf.RoundToInt((float)this.value);
						if (!this.cR8Manager.isTesting)
						{
							item.itemMovement.DelayDestroy();
						}
						energyBall.energyValue += num2;
					}
					goto IL_0DE8;
				}
			}
			if (this.type == EnergyEmitter.Type.reverser)
			{
				energyBall.transform.rotation *= Quaternion.Euler(0f, 0f, 180f);
			}
			else if (this.type == EnergyEmitter.Type.amplifier)
			{
				if (this.cR8Manager.isTesting && !this.IsOverheated())
				{
					energyBall.energyValue += this.value;
				}
			}
			else if (this.type == EnergyEmitter.Type.booster)
			{
				if (this.cR8Manager.isTesting && !this.IsOverheated() && this.ConsiderChargeUseLimit(energyBall))
				{
					energyBall.energyValue += this.value;
				}
			}
			else if (this.type == EnergyEmitter.Type.toggle)
			{
				if (this.toggle == 1)
				{
					if (vector == Vector2.right)
					{
						energyBall.transform.rotation *= Quaternion.Euler(0f, 0f, -90f);
					}
					else if (vector == Vector2.up)
					{
						energyBall.transform.rotation *= Quaternion.Euler(0f, 0f, 90f);
					}
					this.TurnToggleOff();
				}
				else
				{
					if (vector == Vector2.left)
					{
						energyBall.transform.rotation *= Quaternion.Euler(0f, 0f, 90f);
					}
					else if (vector == Vector2.up)
					{
						energyBall.transform.rotation *= Quaternion.Euler(0f, 0f, -90f);
					}
					this.TurnToggleOn();
				}
			}
			else if (this.type == EnergyEmitter.Type.switcher)
			{
				if (this.toggle == 1)
				{
					if (vector == Vector2.right)
					{
						energyBall.transform.rotation *= Quaternion.Euler(0f, 0f, -90f);
					}
					else if (vector == Vector2.up)
					{
						energyBall.transform.rotation *= Quaternion.Euler(0f, 0f, 90f);
					}
				}
				else if (vector == Vector2.left)
				{
					energyBall.transform.rotation *= Quaternion.Euler(0f, 0f, 90f);
				}
				else if (vector == Vector2.up)
				{
					energyBall.transform.rotation *= Quaternion.Euler(0f, 0f, -90f);
				}
			}
			else if (this.type == EnergyEmitter.Type.newChargeDouble || this.type == EnergyEmitter.Type.newCharge)
			{
				this.CreateEnergy();
			}
		}
		IL_0DE8:
		if (!energyBall.isDestroying && energyBall.energyValue != energyValue)
		{
			energyBall.DisplayEnergy();
		}
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x000295EC File Offset: 0x000277EC
	public void ClearCharge()
	{
		if (this.sprites.Count >= 1)
		{
			this.spriteRenderer.sprite = this.sprites[0];
		}
		this.heldCharge = -1;
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x0002961A File Offset: 0x0002781A
	public void FlipToggle()
	{
		this.toggle = (this.toggle + 1) % 2;
	}

	// Token: 0x06000426 RID: 1062 RVA: 0x0002962C File Offset: 0x0002782C
	private void TurnToggleOff()
	{
		this.acceptableEntraces.Add(Vector2.left);
		this.acceptableEntraces.Remove(Vector2.right);
		this.acceptableEntraces = this.acceptableEntraces.Distinct<Vector2>().ToList<Vector2>();
		this.toggle = 0;
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x0002966C File Offset: 0x0002786C
	private void TurnToggleOn()
	{
		this.acceptableEntraces.Add(Vector2.right);
		this.acceptableEntraces.Remove(Vector2.left);
		this.acceptableEntraces = this.acceptableEntraces.Distinct<Vector2>().ToList<Vector2>();
		this.toggle = 1;
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x000296AC File Offset: 0x000278AC
	public void LaunchDuringAction()
	{
		MonoBehaviour main = GameFlowManager.main;
		this.CreateEnergy();
		main.StartCoroutine(this.cR8Manager.RunCharges(false));
	}

	// Token: 0x04000317 RID: 791
	public EnergyEmitter.Type type;

	// Token: 0x04000318 RID: 792
	[SerializeField]
	private GameObject heatInformation;

	// Token: 0x04000319 RID: 793
	[SerializeField]
	private ParticleSystem heatParticles;

	// Token: 0x0400031A RID: 794
	[SerializeField]
	private ParticleSystem heatParticlesOnOverheat;

	// Token: 0x0400031B RID: 795
	[SerializeField]
	private Animator overheatTextAnimator;

	// Token: 0x0400031C RID: 796
	[SerializeField]
	private int currentHeat;

	// Token: 0x0400031D RID: 797
	[SerializeField]
	public int maxHeat = 10;

	// Token: 0x0400031E RID: 798
	[SerializeField]
	private GameObject energyBallPrefab;

	// Token: 0x0400031F RID: 799
	[SerializeField]
	private List<Item2.ItemType> filteredItemTypes;

	// Token: 0x04000320 RID: 800
	[SerializeField]
	private int value;

	// Token: 0x04000321 RID: 801
	[SerializeField]
	public EnergyEmitter.EnergyValueType energyValueType;

	// Token: 0x04000322 RID: 802
	[SerializeField]
	public List<Vector2> acceptableEntraces;

	// Token: 0x04000323 RID: 803
	private float time;

	// Token: 0x04000324 RID: 804
	[SerializeField]
	private List<Sprite> sprites;

	// Token: 0x04000325 RID: 805
	private int spriteNumber;

	// Token: 0x04000326 RID: 806
	private SpriteRenderer spriteRenderer;

	// Token: 0x04000327 RID: 807
	private int heldCharge = -1;

	// Token: 0x04000328 RID: 808
	private Item2 myItem;

	// Token: 0x04000329 RID: 809
	private CR8Manager cR8Manager;

	// Token: 0x0400032A RID: 810
	public bool playable;

	// Token: 0x0400032B RID: 811
	public int toggle;

	// Token: 0x0400032C RID: 812
	[Header("Restrictions")]
	public int requiredDistance;

	// Token: 0x0400032D RID: 813
	public int usesPerCharge = -1;

	// Token: 0x0400032E RID: 814
	public bool ignoreCostsOnOverheat;

	// Token: 0x020002C2 RID: 706
	public enum Type
	{
		// Token: 0x04001097 RID: 4247
		creator,
		// Token: 0x04001098 RID: 4248
		rotator,
		// Token: 0x04001099 RID: 4249
		splitter,
		// Token: 0x0400109A RID: 4250
		repeater,
		// Token: 0x0400109B RID: 4251
		filter,
		// Token: 0x0400109C RID: 4252
		charger,
		// Token: 0x0400109D RID: 4253
		toggle,
		// Token: 0x0400109E RID: 4254
		grinder,
		// Token: 0x0400109F RID: 4255
		reverser,
		// Token: 0x040010A0 RID: 4256
		amplifier,
		// Token: 0x040010A1 RID: 4257
		switcher,
		// Token: 0x040010A2 RID: 4258
		newCharge,
		// Token: 0x040010A3 RID: 4259
		newChargeDouble,
		// Token: 0x040010A4 RID: 4260
		booster,
		// Token: 0x040010A5 RID: 4261
		blank,
		// Token: 0x040010A6 RID: 4262
		reverserWithExitOnOverheat
	}

	// Token: 0x020002C3 RID: 707
	public enum EnergyValueType
	{
		// Token: 0x040010A8 RID: 4264
		setValue,
		// Token: 0x040010A9 RID: 4265
		playerAP
	}
}
