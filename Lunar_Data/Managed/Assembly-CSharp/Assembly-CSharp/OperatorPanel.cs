using System;
using Febucci.UI;
using UnityEngine;

// Token: 0x0200005F RID: 95
public class OperatorPanel : MonoBehaviour
{
	// Token: 0x060002B6 RID: 694 RVA: 0x0000DE63 File Offset: 0x0000C063
	private void OnEnable()
	{
		OperatorPanel.instance = this;
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x0000DE6B File Offset: 0x0000C06B
	private void OnDisable()
	{
		if (OperatorPanel.instance == this)
		{
			OperatorPanel.instance = null;
		}
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x0000DE80 File Offset: 0x0000C080
	private void Start()
	{
		this.energyTextHighlight.gameObject.SetActive(false);
		this.timerTextHighlight.gameObject.SetActive(false);
		this.cardsTextHighlight.gameObject.SetActive(false);
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x0000DEB8 File Offset: 0x0000C0B8
	private void Update()
	{
		this.loopNumber++;
		if (this.loopNumber <= 2)
		{
			return;
		}
		this.time += Time.deltaTime * TimeManager.instance.currentTimeScale;
		if (TimeManager.instance.currentTimeScale == 0f)
		{
			this.timeStandingStill += Time.deltaTime;
		}
		if (this.typewriterByCharacter.isShowingText)
		{
			this.operatorPortraitAnimator.PlayAnimation("talk");
		}
		else
		{
			this.operatorPortraitAnimator.PlayAnimation("idle");
		}
		this.text.typewriterStartsAutomatically = false;
		if (this.loopNumber == 3)
		{
			HordeRemainingDisplay.instance.ShowTimer(false);
			this.text.typewriterStartsAutomatically = true;
			this.tutorialText.SetKey("tut1");
			EnergyBarMaster.instance.RemoveAllEnergyCapsules();
		}
		switch (this.tutorialStage)
		{
		case OperatorPanel.TutorialStage.Move:
			if (this.time > 2f)
			{
				this.tutorialStage = OperatorPanel.TutorialStage.AvoidGhouls;
				this.SpawnGhouls();
				this.text.typewriterStartsAutomatically = true;
				this.tutorialText.SetKey("tut3");
				this.time = 0f;
				this.timeStandingStill = 0f;
				return;
			}
			break;
		case OperatorPanel.TutorialStage.Wait:
			break;
		case OperatorPanel.TutorialStage.AvoidGhouls:
			if (this.time > 5f)
			{
				this.tutorialStage = OperatorPanel.TutorialStage.ChargeEnergy;
				this.text.typewriterStartsAutomatically = true;
				this.tutorialText.SetKey("tut4");
				this.energyTextHighlight.gameObject.SetActive(true);
				this.time = 0f;
				this.timeStandingStill = 0f;
				for (int i = 0; i < 4; i++)
				{
					EnergyBarMaster.instance.CreateEmptyEnergyCapsule();
				}
				return;
			}
			break;
		case OperatorPanel.TutorialStage.ChargeEnergy:
			if (EnergyBarMaster.instance.GetEnergy() >= 4)
			{
				this.tutorialStage = OperatorPanel.TutorialStage.StandStill;
				this.text.typewriterStartsAutomatically = true;
				this.tutorialText.SetKey("tutStandStill");
				this.energyTextHighlight.gameObject.SetActive(false);
				this.time = 0f;
				this.timeStandingStill = 0f;
				return;
			}
			break;
		case OperatorPanel.TutorialStage.StandStill:
			if (this.timeStandingStill > 2.5f)
			{
				this.tutorialStage = OperatorPanel.TutorialStage.PlayCard;
				this.text.typewriterStartsAutomatically = true;
				this.tutorialText.SetKey("tut5");
				this.cardsTextHighlight.gameObject.SetActive(true);
				this.time = 0f;
				this.timeStandingStill = 0f;
				CardManager.instance.isAllowedToDraw = true;
				return;
			}
			break;
		case OperatorPanel.TutorialStage.PlayCard:
			if (this.hasPlayedCard)
			{
				this.tutorialStage = OperatorPanel.TutorialStage.Survive;
				this.text.typewriterStartsAutomatically = true;
				this.tutorialText.SetKey("tut6");
				this.cardsTextHighlight.gameObject.SetActive(false);
				this.timerTextHighlight.gameObject.SetActive(true);
				this.time = 0f;
				this.timeStandingStill = 0f;
				HordeRemainingDisplay.instance.StartTutorialTimer();
				return;
			}
			break;
		case OperatorPanel.TutorialStage.Survive:
			if (this.time > 3f)
			{
				this.time = 0f;
				this.SpawnGhouls();
			}
			if (HordeRemainingDisplay.instance.timeRemaining <= 0f)
			{
				EnemyBullet.DestroyAllBullets();
				Player.instance.Win();
				GameManager.instance.WinLevel();
				this.tutorialStage = OperatorPanel.TutorialStage.Finsh;
				Object.Destroy(base.gameObject);
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x060002BA RID: 698 RVA: 0x0000E20C File Offset: 0x0000C40C
	private void SpawnGhouls()
	{
		for (int i = 0; i < 2; i++)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.ghoul, this.spawnPointA.transform.position, Quaternion.identity);
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1f);
			gameObject.transform.position += new Vector3((float)Random.Range(-2, 2), (float)Random.Range(-1, 1), 0f);
		}
		for (int j = 0; j < 2; j++)
		{
			GameObject gameObject2 = Object.Instantiate<GameObject>(this.ghoul, this.spawnPointB.transform.position, Quaternion.identity);
			gameObject2.transform.position = new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y, -1f);
			gameObject2.transform.position += new Vector3((float)Random.Range(-2, 2), (float)Random.Range(-1, 1), 0f);
		}
	}

	// Token: 0x040001FD RID: 509
	public static OperatorPanel instance;

	// Token: 0x040001FE RID: 510
	public OperatorPanel.TutorialStage tutorialStage;

	// Token: 0x040001FF RID: 511
	[SerializeField]
	private ReplacementText tutorialText;

	// Token: 0x04000200 RID: 512
	[SerializeField]
	private TextAnimator_TMP text;

	// Token: 0x04000201 RID: 513
	[SerializeField]
	private TypewriterByCharacter typewriterByCharacter;

	// Token: 0x04000202 RID: 514
	[SerializeField]
	private SimpleAnimator operatorPortraitAnimator;

	// Token: 0x04000203 RID: 515
	[SerializeField]
	private GameObject ghoul;

	// Token: 0x04000204 RID: 516
	[SerializeField]
	public SpawnPoint spawnPointA;

	// Token: 0x04000205 RID: 517
	[SerializeField]
	public SpawnPoint spawnPointB;

	// Token: 0x04000206 RID: 518
	[SerializeField]
	private GameObject energyTextHighlight;

	// Token: 0x04000207 RID: 519
	[SerializeField]
	private GameObject timerTextHighlight;

	// Token: 0x04000208 RID: 520
	[SerializeField]
	private GameObject cardsTextHighlight;

	// Token: 0x04000209 RID: 521
	[SerializeField]
	private bool hasPlayed;

	// Token: 0x0400020A RID: 522
	[SerializeField]
	public bool hasPlayedCard;

	// Token: 0x0400020B RID: 523
	private float time;

	// Token: 0x0400020C RID: 524
	private float timeStandingStill;

	// Token: 0x0400020D RID: 525
	private int loopNumber;

	// Token: 0x020000ED RID: 237
	public enum TutorialStage
	{
		// Token: 0x0400046D RID: 1133
		Move,
		// Token: 0x0400046E RID: 1134
		Wait,
		// Token: 0x0400046F RID: 1135
		AvoidGhouls,
		// Token: 0x04000470 RID: 1136
		ChargeEnergy,
		// Token: 0x04000471 RID: 1137
		StandStill,
		// Token: 0x04000472 RID: 1138
		PlayCard,
		// Token: 0x04000473 RID: 1139
		Survive,
		// Token: 0x04000474 RID: 1140
		Finsh
	}
}
