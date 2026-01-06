using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200004B RID: 75
public class HordeRemainingDisplay : MonoBehaviour
{
	// Token: 0x0600021F RID: 543 RVA: 0x0000B03B File Offset: 0x0000923B
	private void OnEnable()
	{
		HordeRemainingDisplay.instance = this;
	}

	// Token: 0x06000220 RID: 544 RVA: 0x0000B043 File Offset: 0x00009243
	private void OnDisable()
	{
		if (HordeRemainingDisplay.instance == this)
		{
			HordeRemainingDisplay.instance = null;
		}
	}

	// Token: 0x06000221 RID: 545 RVA: 0x0000B058 File Offset: 0x00009258
	private void Start()
	{
	}

	// Token: 0x06000222 RID: 546 RVA: 0x0000B05C File Offset: 0x0000925C
	private void Update()
	{
		if (!Player.instance || Player.instance.isDead)
		{
			this.DisableText();
			this.hordeDisplay.SetActive(false);
			return;
		}
		if (RoomManager.instance.currentRoom && !RoomManager.instance.currentRoom.enemyWave && !OperatorPanel.instance)
		{
			this.DisableText();
			this.hordeDisplay.SetActive(false);
		}
		if (this.timeRemaining <= 0f && !Enemy.AnyEnemiesAliveThatMustBeDefeated() && !this.wonLevel && RoomManager.instance.currentRoom.enemyWave)
		{
			this.wonLevel = true;
			EnemyBullet.DestroyAllBullets();
			Player.instance.Win();
			GameManager.instance.WinLevel();
		}
		this.UpdateText();
		this.UpdateTime();
	}

	// Token: 0x06000223 RID: 547 RVA: 0x0000B137 File Offset: 0x00009337
	public void ShowTimer(bool show)
	{
		this.hordeDisplay.SetActive(show);
	}

	// Token: 0x06000224 RID: 548 RVA: 0x0000B145 File Offset: 0x00009345
	public void StartTutorialTimer()
	{
		this.hordeDisplay.SetActive(true);
		this.timeRemaining = 7f;
		this.totalTime = 7f;
	}

	// Token: 0x06000225 RID: 549 RVA: 0x0000B169 File Offset: 0x00009369
	private void DisableText()
	{
		this.objectiveText.gameObject.SetActive(false);
	}

	// Token: 0x06000226 RID: 550 RVA: 0x0000B17C File Offset: 0x0000937C
	private void UpdateText()
	{
		if (!RoomManager.instance.currentRoom)
		{
			return;
		}
		Room.SpecialObjective specialObjective = RoomManager.instance.currentRoom.specialObjective;
		if (specialObjective != Room.SpecialObjective.None)
		{
			if (specialObjective == Room.SpecialObjective.ChargeComputers)
			{
				this.objectiveText.gameObject.SetActive(true);
				this.objectiveText.SetKey("Hack Terminals");
				return;
			}
		}
		else
		{
			this.objectiveText.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000227 RID: 551 RVA: 0x0000B1E8 File Offset: 0x000093E8
	private void UpdateTime()
	{
		if (!RoomManager.instance.currentRoom)
		{
			return;
		}
		if (RoomManager.instance.currentRoom.specialObjective != Room.SpecialObjective.None)
		{
			this.hordeFill.fillAmount = 1f;
			return;
		}
		this.timeRemaining -= Time.deltaTime * TimeManager.instance.currentTimeScale;
		this.timeRemaining = Mathf.Clamp(this.timeRemaining, 0f, this.totalTime);
		this.setFill = this.timeRemaining / this.totalTime;
		this.hordeFill.fillAmount = Mathf.Lerp(this.hordeFill.fillAmount, this.setFill, Time.deltaTime * 5f);
	}

	// Token: 0x06000228 RID: 552 RVA: 0x0000B2A1 File Offset: 0x000094A1
	public void SetTimeToSurvive(float time)
	{
		this.wonLevel = false;
		time *= RunTypeManager.instance.GetRunTypeModifierPercentage(RunType.RunProperty.RunPropertyType.ExtraTimeToSurvive);
		this.timeRemaining = time;
		this.totalTime = time;
		this.ToggleDisplay(true);
	}

	// Token: 0x06000229 RID: 553 RVA: 0x0000B2CE File Offset: 0x000094CE
	public void ToggleDisplay(bool value)
	{
		this.hordeDisplay.SetActive(value);
		this.setFill = 1f;
		this.hordeFill.fillAmount = 1f;
	}

	// Token: 0x0400019C RID: 412
	public bool wonLevel;

	// Token: 0x0400019D RID: 413
	public static HordeRemainingDisplay instance;

	// Token: 0x0400019E RID: 414
	[SerializeField]
	private GameObject hordeDisplay;

	// Token: 0x0400019F RID: 415
	[SerializeField]
	private Image hordeFill;

	// Token: 0x040001A0 RID: 416
	[SerializeField]
	private ReplacementText objectiveText;

	// Token: 0x040001A1 RID: 417
	public float timeRemaining = 60f;

	// Token: 0x040001A2 RID: 418
	public float totalTime = 60f;

	// Token: 0x040001A3 RID: 419
	private float setFill = 1f;
}
