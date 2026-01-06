using System;
using System.Collections;
using System.Collections.Generic;
using SaveSystem;
using SaveSystem.States;
using UnityEngine;

// Token: 0x02000046 RID: 70
public class GameManager : MonoBehaviour
{
	// Token: 0x060001F5 RID: 501 RVA: 0x0000AAE0 File Offset: 0x00008CE0
	private void OnEnable()
	{
		GameManager.instance = this;
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x0000AAE8 File Offset: 0x00008CE8
	private void OnDisable()
	{
		GameManager.instance = null;
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x0000AAF0 File Offset: 0x00008CF0
	private void Start()
	{
		SoundManager.instance.PlaySongSudden(SoundManager.instance.GetSongClipByName("mus_track1"));
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x0000AB0B File Offset: 0x00008D0B
	private void Update()
	{
		SoundManager.instance.musicEffect = SingleUI.IsViewingPopUp() || FadeMaster.instance.fading;
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x0000AB2B File Offset: 0x00008D2B
	public void StartChooseCard()
	{
		Object.Instantiate<GameObject>(this.chooseCardPrefab, CanvasManager.instance.masterContentScaler);
	}

	// Token: 0x060001FA RID: 506 RVA: 0x0000AB43 File Offset: 0x00008D43
	public void StartChooseRelic()
	{
		Object.Instantiate<GameObject>(this.chooseRelicPrefab, CanvasManager.instance.masterContentScaler);
	}

	// Token: 0x060001FB RID: 507 RVA: 0x0000AB5B File Offset: 0x00008D5B
	public void WinLevel()
	{
		if (this.winningRoutine != null)
		{
			base.StopCoroutine(this.winningRoutine);
		}
		this.winningRoutine = base.StartCoroutine(this.WinningRoutine());
	}

	// Token: 0x060001FC RID: 508 RVA: 0x0000AB83 File Offset: 0x00008D83
	public void ShowDeck(Transform origin)
	{
		if (!InputManager.instance.IsGameInput())
		{
			return;
		}
		this.deckViewerReference = Object.Instantiate<GameObject>(this.deckViewerPrefab, CanvasManager.instance.masterContentScaler);
		this.deckViewerReference.GetComponentInChildren<DeckPanel>().ShowCards(origin);
	}

	// Token: 0x060001FD RID: 509 RVA: 0x0000ABBE File Offset: 0x00008DBE
	public void ToggleDeck(Transform origin)
	{
		if (this.deckViewerReference)
		{
			Object.Destroy(this.deckViewerReference);
			return;
		}
		this.ShowDeck(origin);
	}

	// Token: 0x060001FE RID: 510 RVA: 0x0000ABE0 File Offset: 0x00008DE0
	private IEnumerator WinningRoutine()
	{
		List<Enemy> enemies = new List<Enemy>(Enemy.enemies);
		foreach (Enemy enemy in enemies)
		{
			if (enemy)
			{
				Destructible component = enemy.GetComponent<Destructible>();
				if (!component)
				{
					Object.Destroy(enemy.gameObject);
				}
				component.SilentlyExplode();
				yield return new WaitForSeconds(1f / (float)enemies.Count);
			}
		}
		List<Enemy>.Enumerator enumerator = default(List<Enemy>.Enumerator);
		if (this.isTutorial)
		{
			Singleton.instance.AddAccomplishment(ProgressState.Accomplishment.TutorialCompleted);
			SaveManager.SaveProgress();
		}
		if (!this.isTutorial && RoomManager.instance.currentRoom.specialObjective != Room.SpecialObjective.ChargeComputers)
		{
			yield return new WaitForSeconds(0.25f);
			this.StartChooseRelic();
		}
		while (SingleUI.IsViewingPopUp())
		{
			yield return null;
		}
		yield return new WaitForSeconds(0.6f);
		FadeMaster.instance.SetFade(true);
		InputManager.instance.CloseDeck();
		CardManager.instance.isAllowedToDraw = false;
		CardManager.instance.CearAllExhausts();
		CardManager.instance.ReshuffleAllCards();
		yield return new WaitForSeconds(0.75f);
		if (this.isTutorial)
		{
			this.isTutorial = false;
			TutorialManager.instance.LoadStoryPanel();
			while (SingleUI.IsViewingPopUp())
			{
				yield return null;
			}
			RoomManager.instance.ClearRoomContents();
			PickUp.RemoveAllPickups();
			PassiveManager.instance.ClearAllPassiveEffects();
			Player.instance.StartNewLevel();
			CardManager.instance.isAllowedToDraw = true;
			FadeMaster.instance.SetFade(false);
			yield break;
		}
		if (RoomManager.instance.currentRoom.specialObjective == Room.SpecialObjective.ChargeComputers)
		{
			Singleton.instance.CompleteRun();
			UnlockManager.instance.ShowUnlocks();
			yield return new WaitForSeconds(0.1f);
			while (SingleUI.IsViewingPopUp())
			{
				yield return null;
			}
			while (SingleUI.IsViewingPopUp())
			{
				yield return null;
			}
			Object.Instantiate<GameObject>(this.endingPrefab, CanvasManager.instance.masterContentScaler);
			SaveManager.SaveProgress();
			yield break;
		}
		this.mapReference.gameObject.SetActive(true);
		while (SingleUI.IsViewingPopUp())
		{
			yield return null;
		}
		RoomManager.instance.ClearRoomContents();
		PickUp.RemoveAllPickups();
		PassiveManager.instance.ClearAllPassiveEffects();
		Singleton.instance.AddAccomplishment(ProgressState.Accomplishment.RoomsSurvived, 1);
		SaveManager.SaveProgress();
		RoomManager.instance.CreateChosenRoomPrefab();
		HealthBarMaster.instance.Heal(5f + RunTypeManager.instance.GetRunTypeModifierValue(RunType.RunProperty.RunPropertyType.ExtraHealthEachLevel));
		Player.instance.StartNewLevel();
		yield return new WaitForSeconds(0.5f);
		CardManager.instance.isAllowedToDraw = true;
		FadeMaster.instance.SetFade(false);
		yield break;
		yield break;
	}

	// Token: 0x04000180 RID: 384
	public static GameManager instance;

	// Token: 0x04000181 RID: 385
	[SerializeField]
	private GameObject chooseCardPrefab;

	// Token: 0x04000182 RID: 386
	[SerializeField]
	private GameObject chooseRelicPrefab;

	// Token: 0x04000183 RID: 387
	[SerializeField]
	private GameObject endingPrefab;

	// Token: 0x04000184 RID: 388
	[SerializeField]
	private GameObject mapReference;

	// Token: 0x04000185 RID: 389
	[SerializeField]
	private GameObject deckViewerPrefab;

	// Token: 0x04000186 RID: 390
	[SerializeField]
	private GameObject deckViewerReference;

	// Token: 0x04000187 RID: 391
	private Coroutine winningRoutine;

	// Token: 0x04000188 RID: 392
	public bool isTutorial;
}
