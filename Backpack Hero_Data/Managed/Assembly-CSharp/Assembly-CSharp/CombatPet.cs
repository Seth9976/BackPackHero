using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

// Token: 0x020000B2 RID: 178
public class CombatPet : CustomInputHandler
{
	// Token: 0x06000488 RID: 1160 RVA: 0x0002BBEC File Offset: 0x00029DEC
	private void Start()
	{
		if (!CombatPet.combatPets.Contains(this))
		{
			CombatPet.combatPets.Add(this);
		}
		this.enemyScript = base.GetComponent<Enemy>();
		this.draggableCombat = base.GetComponent<DraggableCombat>();
		this.gameManager = GameManager.main;
		this.gameFlowManager = GameFlowManager.main;
		this.mySpacerLocation = Object.Instantiate<GameObject>(this.petSpacerPrefab, base.transform.position, Quaternion.identity).transform;
		this.mySpacerLocation.SetParent(GameObject.FindGameObjectWithTag("PlayerSpacerParent").transform);
		int siblingIndex = Player.main.mySpacerLocation.GetSiblingIndex();
		if (Item2.UseItemIndirectWithStatusEffect(Item2.ItemStatusEffect.Type.petsAreSummonedBehindPochette, null))
		{
			this.mySpacerLocation.SetSiblingIndex(siblingIndex);
		}
		else
		{
			this.mySpacerLocation.SetSiblingIndex(siblingIndex + 1);
		}
		this.APAnimator.transform.SetParent(GameObject.FindGameObjectWithTag("PrimaryCanvas").transform);
		this.APAnimator.Play("ApIntro");
		this.ShowAP();
		this.spriteRenderer.enabled = false;
		base.StartCoroutine(this.EnableSprite());
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x0002BD04 File Offset: 0x00029F04
	public static int GetDeadPets()
	{
		int num = 0;
		using (List<CombatPet>.Enumerator enumerator = CombatPet.combatPets.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.stats.health <= 0)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x0002BD64 File Offset: 0x00029F64
	private IEnumerator RandomizeAnimation()
	{
		yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
		this.spriteAnimator.enabled = true;
		yield break;
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x0002BD73 File Offset: 0x00029F73
	private IEnumerator EnableSprite()
	{
		yield return new WaitForSeconds(0.4f);
		this.spriteRenderer.enabled = true;
		yield return this.RandomizeAnimation();
		yield break;
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x0002BD84 File Offset: 0x00029F84
	private void OnDestroy()
	{
		CombatPet.combatPets.Remove(this);
		if (this.mySpacerLocation)
		{
			Object.Destroy(this.mySpacerLocation.gameObject);
		}
		if (this.stats)
		{
			Object.Destroy(this.stats.gameObject);
		}
		if (this.APAnimator)
		{
			Object.Destroy(this.APAnimator.gameObject);
		}
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x0002BDF4 File Offset: 0x00029FF4
	private void Update()
	{
		if (this.APAnimator)
		{
			this.APAnimator.transform.position = this.APPosition.position;
		}
		if (this.stats && this.stats.health > 0)
		{
			this.dead = false;
			this.spriteAnimator.Play("idle");
		}
		else
		{
			if (!this.dead)
			{
				Object.FindObjectOfType<TutorialManager>().ConsiderTutorial("deadPet");
				GameFlowManager.main.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onPetDies, null, new List<Status> { this.stats }, true, false);
			}
			this.dead = true;
			this.spriteAnimator.Play("die");
		}
		if (this.mySpacerLocation && !this.draggableCombat.isDragging)
		{
			Vector3 vector = new Vector3(this.mySpacerLocation.transform.position.x, base.transform.position.y, base.transform.position.z);
			base.transform.position = Vector3.MoveTowards(base.transform.position, vector, 10f * Time.deltaTime * Vector3.Distance(base.transform.position, vector));
		}
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x0002BF3D File Offset: 0x0002A13D
	public static List<CombatPet> GetPets()
	{
		return new List<CombatPet>(CombatPet.combatPets).OrderBy((CombatPet x) => x.transform.position.x).ToList<CombatPet>();
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x0002BF74 File Offset: 0x0002A174
	public static List<CombatPet> GetLivePets()
	{
		new List<CombatPet>(CombatPet.combatPets);
		return (from x in CombatPet.combatPets.Where((CombatPet x) => x.stats.health > 0).ToList<CombatPet>()
			orderby x.transform.position.x
			select x).ToList<CombatPet>();
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x0002BFE3 File Offset: 0x0002A1E3
	public static IEnumerator AllPetsNextTurn()
	{
		foreach (CombatPet combatPet in CombatPet.combatPets)
		{
			if (combatPet && combatPet.gameObject)
			{
				yield return combatPet.NextTurn();
			}
		}
		List<CombatPet>.Enumerator enumerator = default(List<CombatPet>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x0002BFEB File Offset: 0x0002A1EB
	private IEnumerator NextTurn()
	{
		yield return this.stats.NextTurn();
		this.stats.EndArmor();
		yield break;
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x0002BFFA File Offset: 0x0002A1FA
	public IEnumerator ApplyEffect(Item2.CombattEffect combattEffect)
	{
		if (!this.petItem2 || this.stats.health <= 0)
		{
			yield break;
		}
		Player main = Player.main;
		List<Status> myStats = Item2.GetMyStats(combattEffect.effect, this.stats, new List<Status>(), this.gameFlowManager, this.gameManager, main, new List<Enemy>(), null);
		Item2.ApplyMyEffect(combattEffect.effect, myStats, this.stats, null, main);
		this.positionAnimator.Play("enemyPos_buff");
		yield return new WaitForSeconds(0.1f);
		yield break;
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x0002C010 File Offset: 0x0002A210
	public void ChangeAP(int amount)
	{
		this.gameManager.endTurnButton.GetComponent<Animator>();
		if (amount < 0)
		{
			this.APAnimator.Play("AP_PulseDown", 0, 0f);
			PlayerStatTracking.main.AddStat("Energy used", amount);
		}
		else if (amount > 0)
		{
			this.APAnimator.Play("AP_PulseUp", 0, 0f);
			PlayerStatTracking.main.AddStat("Energy gained", amount);
			SoundManager.main.PlaySFX("energy");
		}
		this.APAnimator.GetComponentInChildren<TextMeshProUGUI>().text = this.petItem2.currentAP.ToString() ?? "";
		Item2.SetAllItemColors();
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x0002C0C4 File Offset: 0x0002A2C4
	public void ShowAP()
	{
		if (this.APAnimator)
		{
			if (!this.APAnimator.gameObject.activeInHierarchy)
			{
				this.APAnimator.gameObject.SetActive(true);
				this.APAnimator.Play("ApIntro", 0, 0f);
			}
			this.APAnimator.GetComponentInChildren<TextMeshProUGUI>().text = this.petItem2.currentAP.ToString() ?? "";
		}
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x0002C140 File Offset: 0x0002A340
	public static void HideAllAp()
	{
		CombatPet[] array = Object.FindObjectsOfType<CombatPet>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].HideAP();
		}
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x0002C16C File Offset: 0x0002A36C
	public void HideAP()
	{
		if (this.APAnimator && this.APAnimator.gameObject.activeInHierarchy)
		{
			this.APAnimator.gameObject.SetActive(true);
			this.APAnimator.Play("ApExit", 0, 0f);
		}
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x0002C1BF File Offset: 0x0002A3BF
	public void HurtAnimation()
	{
		this.positionAnimator.Play("enemyPos_hurt", 0, 0f);
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x0002C1D7 File Offset: 0x0002A3D7
	public override void OnCursorHold()
	{
		this.petItem2.itemMovement.OnCursorHold();
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x0002C1E9 File Offset: 0x0002A3E9
	public override void OnCursorEnd()
	{
		this.petItem2.itemMovement.RemoveCard();
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x0002C1FB File Offset: 0x0002A3FB
	public void OnMouseExit()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.petItem2.itemMovement.RemoveCard();
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x0002C21A File Offset: 0x0002A41A
	public override void OnPressStart(string keyName, bool overrideKeyName)
	{
		if (keyName == "confirm" || overrideKeyName)
		{
			this.petItem2;
		}
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x0002C237 File Offset: 0x0002A437
	public void OnMouseUpAsButton()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.petItem2;
	}

	// Token: 0x04000359 RID: 857
	public SpriteRenderer spriteRenderer;

	// Token: 0x0400035A RID: 858
	public Animator positionAnimator;

	// Token: 0x0400035B RID: 859
	public Animator spriteAnimator;

	// Token: 0x0400035C RID: 860
	public static List<CombatPet> combatPets = new List<CombatPet>();

	// Token: 0x0400035D RID: 861
	[SerializeField]
	private Transform APPosition;

	// Token: 0x0400035E RID: 862
	[SerializeField]
	private Animator APAnimator;

	// Token: 0x0400035F RID: 863
	private static bool petsAreInBag = false;

	// Token: 0x04000360 RID: 864
	[SerializeField]
	public Status stats;

	// Token: 0x04000361 RID: 865
	[SerializeField]
	private GameObject petSpacerPrefab;

	// Token: 0x04000362 RID: 866
	[SerializeField]
	public Transform mySpacerLocation;

	// Token: 0x04000363 RID: 867
	private DraggableCombat draggableCombat;

	// Token: 0x04000364 RID: 868
	public PetItem2 petItem2;

	// Token: 0x04000365 RID: 869
	private GameManager gameManager;

	// Token: 0x04000366 RID: 870
	private GameFlowManager gameFlowManager;

	// Token: 0x04000367 RID: 871
	private Enemy enemyScript;

	// Token: 0x04000368 RID: 872
	public bool dead;
}
