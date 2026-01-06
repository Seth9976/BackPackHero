using System;
using System.Collections;
using SaveSystem.States;
using UnityEngine;

// Token: 0x0200005A RID: 90
public class MenuManager : MonoBehaviour
{
	// Token: 0x06000298 RID: 664 RVA: 0x0000D736 File Offset: 0x0000B936
	private void OnEnable()
	{
		if (MenuManager.instance == null)
		{
			MenuManager.instance = this;
			return;
		}
		if (MenuManager.instance != this)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000299 RID: 665 RVA: 0x0000D764 File Offset: 0x0000B964
	private void OnDisable()
	{
		if (MenuManager.instance == this)
		{
			MenuManager.instance = null;
		}
	}

	// Token: 0x0600029A RID: 666 RVA: 0x0000D779 File Offset: 0x0000B979
	private void Start()
	{
		SoundManager.instance.PlayOrContinueSong("mus_title_piano");
		SoundManager.instance.musicEffect = false;
	}

	// Token: 0x0600029B RID: 667 RVA: 0x0000D795 File Offset: 0x0000B995
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.H))
		{
			Singleton.instance.AddAccomplishment(ProgressState.Accomplishment.TutorialCompleted);
		}
	}

	// Token: 0x0600029C RID: 668 RVA: 0x0000D7AB File Offset: 0x0000B9AB
	public void OpenOptions()
	{
		Object.Instantiate<GameObject>(this.optionsPanelPrefab, CanvasManager.instance.masterContentScaler);
	}

	// Token: 0x0600029D RID: 669 RVA: 0x0000D7C3 File Offset: 0x0000B9C3
	public void OpenRunPanel()
	{
		Singleton.instance.selectedCharacter = this.nunCharacter;
		if (!Singleton.instance.CheckAccomplishment(ProgressState.Accomplishment.TutorialCompleted))
		{
			this.LoadGame();
			return;
		}
		Object.Instantiate<GameObject>(this.runPanelPrefab, CanvasManager.instance.masterContentScaler);
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0000D800 File Offset: 0x0000BA00
	public void LoadGame()
	{
		Singleton.instance.UpdateStartingRunAccomplishments();
		SoundManager.instance.FadeOutAllSongs();
		SoundManager.instance.PlaySFX("startGame", double.PositiveInfinity);
		Object.Destroy(this.mainMenuButtons);
		this.spaceShip.SetActive(true);
		base.StartCoroutine(this.LoadAfterDelay());
	}

	// Token: 0x0600029F RID: 671 RVA: 0x0000D85D File Offset: 0x0000BA5D
	private IEnumerator LoadAfterDelay()
	{
		yield return new WaitForSeconds(1.4f);
		if (!Singleton.instance.CheckAccomplishment(ProgressState.Accomplishment.TutorialCompleted))
		{
			Singleton.instance.selectedRun = this.tutorialRunType;
		}
		LoadingManager.instance.LoadScene("Game");
		yield break;
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x0000D86C File Offset: 0x0000BA6C
	public void LoadUnlocksScene()
	{
		LoadingManager.instance.LoadScene("Unlocks");
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x0000D87D File Offset: 0x0000BA7D
	public void QuitApplication()
	{
		Application.Quit();
	}

	// Token: 0x040001EA RID: 490
	[SerializeField]
	private PlayableCharacter nunCharacter;

	// Token: 0x040001EB RID: 491
	[SerializeField]
	private RunType tutorialRunType;

	// Token: 0x040001EC RID: 492
	[SerializeField]
	private GameObject runPanelPrefab;

	// Token: 0x040001ED RID: 493
	[SerializeField]
	private GameObject optionsPanelPrefab;

	// Token: 0x040001EE RID: 494
	[SerializeField]
	private Animator moonLightAnimator;

	// Token: 0x040001EF RID: 495
	[SerializeField]
	private GameObject unlocksButton;

	// Token: 0x040001F0 RID: 496
	[SerializeField]
	private GameObject spaceShip;

	// Token: 0x040001F1 RID: 497
	[SerializeField]
	private GameObject mainMenuButtons;

	// Token: 0x040001F2 RID: 498
	public static MenuManager instance;
}
