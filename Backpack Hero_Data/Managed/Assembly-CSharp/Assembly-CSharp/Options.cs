using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x0200018A RID: 394
public class Options : MonoBehaviour
{
	// Token: 0x06000FD8 RID: 4056 RVA: 0x00099C84 File Offset: 0x00097E84
	private void Start()
	{
		this.mask.enabled = true;
		EventSystem.current.SetSelectedGameObject(this.musicSlider.gameObject);
		this.deleteAllButton.SetActive(false);
		this.deleteAllSavesButton.SetActive(false);
		this.confirmDeleteAllButton.SetActive(false);
		GameObject gameObject = GameObject.FindGameObjectWithTag("PopUpSpriteMask");
		if (gameObject)
		{
			gameObject.GetComponent<SpriteMask>().enabled = false;
		}
		this.sfxSlider.value = SoundManager.main.sfxVolume;
		this.musicSlider.value = SoundManager.main.musicVolume;
		this.itemAnimationSpeedSlider.value = Singleton.Instance.itemAnimationSpeed;
		this.brightnessSlider.value = Singleton.Instance.brightness;
		this.cursorSpeedSlider.value = Singleton.Instance.cursorSpeed;
		this.controllerDropDown.value = Singleton.Instance.chosenControllerIcons;
		this.rotateButtonToggle.isOn = Singleton.Instance.rotateButton;
		this.emojiToggle.isOn = Singleton.Instance.showEmojis;
		this.autoCloseMapToggle.isOn = Singleton.Instance.autoCloseMap;
		this.playerAnimationsToggle.isOn = Singleton.Instance.playerAnimations;
		this.showCarvingOutlineToggle.isOn = Singleton.Instance.showOutlineOnCarvings;
		this.clickToMoveToggle.isOn = Singleton.Instance.clickOnceToPickupAndAgainToDrop;
		this.snapToBuildingGridToggle.isOn = Singleton.Instance.snapToGrid;
		this.reverseAandBToggle.isOn = Singleton.Instance.reverseAandB;
		this.reverseXandYToggle.isOn = Singleton.Instance.reverseXandY;
		this.allowHolidayEventsToggle.isOn = Singleton.Instance.allowHolidayEvents;
		if (Singleton.Instance.fps == 15)
		{
			this.fpsDropDown.value = 0;
		}
		else if (Singleton.Instance.fps == 30)
		{
			this.fpsDropDown.value = 1;
		}
		else if (Singleton.Instance.fps == 45)
		{
			this.fpsDropDown.value = 2;
		}
		else if (Singleton.Instance.fps == 60)
		{
			this.fpsDropDown.value = 3;
		}
		else if (Singleton.Instance.fps == 99)
		{
			this.fpsDropDown.value = 4;
		}
		if (Singleton.Instance.resolutionX == 640)
		{
			this.resolutionDropDown.value = 0;
		}
		else if (Singleton.Instance.resolutionX == 1280)
		{
			this.resolutionDropDown.value = 1;
		}
		else if (Singleton.Instance.resolutionX == 1366)
		{
			this.resolutionDropDown.value = 2;
		}
		else if (Singleton.Instance.resolutionX == 1920)
		{
			this.resolutionDropDown.value = 3;
		}
		else if (Singleton.Instance.resolutionX == 2560)
		{
			this.resolutionDropDown.value = 4;
		}
		else if (Singleton.Instance.resolutionX == 3200)
		{
			this.resolutionDropDown.value = 5;
		}
		else if (Singleton.Instance.resolutionX == 3840)
		{
			this.resolutionDropDown.value = 6;
		}
		if (Screen.fullScreen)
		{
			this.screenSize.value = 1;
		}
		else
		{
			this.screenSize.value = 0;
		}
		this.quality.value = QualitySettings.GetQualityLevel();
		Object main = GameManager.main;
		Overworld_Manager overworld_Manager = Object.FindObjectOfType<Overworld_Manager>();
		if (!main)
		{
			this.deleteAllButton.SetActive(true);
			this.deleteAllSavesButton.SetActive(true);
			this.returnToMenuButton.SetActive(false);
			this.resourceTotalParent.SetActive(false);
		}
		else
		{
			this.deleteAllButton.SetActive(false);
			this.returnToMenuButton.SetActive(true);
			if (Singleton.Instance.storyMode)
			{
				this.resourceTotalParent.SetActive(true);
				this.resourceDisplayPanel.SetupResources(MetaProgressSaveManager.main.resources);
			}
		}
		if (overworld_Manager)
		{
			this.returnToMenuButton.SetActive(true);
		}
		this.confirmDeleteAllButton.SetActive(false);
		if (Singleton.Instance.storyMode && !overworld_Manager)
		{
			this.returnToOrderiaButton.SetActive(true);
		}
		else
		{
			this.returnToOrderiaButton.SetActive(false);
		}
		if (SceneManager.GetActiveScene().name != "MainMenu")
		{
			this.RemoveLanguageDropdown();
		}
		this.languageDropDown.ClearOptions();
		List<string> list = new List<string>();
		foreach (string text in LangaugeManager.main.discoveredLanguages)
		{
			list.Add(text);
		}
		this.languageDropDown.AddOptions(list);
		this.languageDropDown.value = LangaugeManager.main.chosenLanguageNum;
		this.SetOptionLanguage();
	}

	// Token: 0x06000FD9 RID: 4057 RVA: 0x0009A158 File Offset: 0x00098358
	private void RemoveLanguageDropdown()
	{
		this.languageHeader.SetActive(false);
		this.languageDropDown.transform.parent.gameObject.SetActive(false);
		Navigation navigation = this.languageDropDown.navigation.selectOnUp.navigation;
		Navigation navigation2 = new Navigation
		{
			mode = Navigation.Mode.Explicit,
			selectOnDown = this.languageDropDown.navigation.selectOnDown,
			selectOnUp = navigation.selectOnUp,
			selectOnLeft = navigation.selectOnLeft,
			selectOnRight = navigation.selectOnRight
		};
		this.languageDropDown.navigation.selectOnUp.navigation = navigation2;
		Navigation navigation3 = this.languageDropDown.navigation.selectOnDown.navigation;
		Navigation navigation4 = new Navigation
		{
			mode = Navigation.Mode.Explicit,
			selectOnDown = navigation3.selectOnDown,
			selectOnUp = this.languageDropDown.navigation.selectOnUp,
			selectOnLeft = navigation3.selectOnLeft,
			selectOnRight = navigation3.selectOnRight
		};
		this.languageDropDown.navigation.selectOnDown.navigation = navigation4;
	}

	// Token: 0x06000FDA RID: 4058 RVA: 0x0009A2A4 File Offset: 0x000984A4
	public void SetController()
	{
		Singleton.Instance.chosenControllerIcons = this.controllerDropDown.value;
	}

	// Token: 0x06000FDB RID: 4059 RVA: 0x0009A2BC File Offset: 0x000984BC
	public void SetOptionLanguage()
	{
		this.screenSize.options[0].text = LangaugeManager.main.GetTextByKey("windowed");
		this.screenSize.options[1].text = LangaugeManager.main.GetTextByKey("full screen");
		this.screenSize.options[2].text = LangaugeManager.main.GetTextByKey("borderless");
		this.fpsDropDown.options[4].text = LangaugeManager.main.GetTextByKey("unlimited");
		this.quality.options[0].text = LangaugeManager.main.GetTextByKey("low");
		this.quality.options[1].text = LangaugeManager.main.GetTextByKey("medium");
		this.quality.options[2].text = LangaugeManager.main.GetTextByKey("high");
		this.ResetButtonText(this.screenSize);
		this.ResetButtonText(this.fpsDropDown);
		this.ResetButtonText(this.quality);
	}

	// Token: 0x06000FDC RID: 4060 RVA: 0x0009A3F0 File Offset: 0x000985F0
	public void SetSliderMenu(Transform transform)
	{
		if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		RectTransform component = transform.GetComponent<RectTransform>();
		RectTransform component2 = transform.parent.GetComponent<RectTransform>();
		RectTransform component3 = transform.parent.parent.parent.GetComponent<RectTransform>();
		component2.anchoredPosition = new Vector2(0f, component2.rect.height - (float)(transform.parent.childCount - transform.GetSiblingIndex()) * component.rect.height - component3.rect.height / 2f);
	}

	// Token: 0x06000FDD RID: 4061 RVA: 0x0009A489 File Offset: 0x00098689
	public void SetFont(Transform t)
	{
		LangaugeManager.main.SetFont(t);
	}

	// Token: 0x06000FDE RID: 4062 RVA: 0x0009A498 File Offset: 0x00098698
	public static void SetPixelPerfect()
	{
		PixelPerfectCamera pixelPerfectCamera = Object.FindObjectOfType<PixelPerfectCamera>();
		if (pixelPerfectCamera && pixelPerfectCamera.GetComponent<PixelZoomer>())
		{
			return;
		}
		pixelPerfectCamera.refResolutionX = Mathf.Max(Singleton.Instance.resolutionX, 640);
		pixelPerfectCamera.refResolutionY = Mathf.RoundToInt((float)pixelPerfectCamera.refResolutionX * 0.5625f);
		pixelPerfectCamera.assetsPPU = pixelPerfectCamera.refResolutionX / 20;
		pixelPerfectCamera.gridSnapping = PixelPerfectCamera.GridSnapping.UpscaleRenderTexture;
	}

	// Token: 0x06000FDF RID: 4063 RVA: 0x0009A50C File Offset: 0x0009870C
	public void SetResolution()
	{
		if (this.resolutionDropDown.value == 0)
		{
			Singleton.Instance.resolutionX = 640;
		}
		else if (this.resolutionDropDown.value == 1)
		{
			Singleton.Instance.resolutionX = 1280;
		}
		else if (this.resolutionDropDown.value == 2)
		{
			Singleton.Instance.resolutionX = 1366;
		}
		else if (this.resolutionDropDown.value == 3)
		{
			Singleton.Instance.resolutionX = 1920;
		}
		else if (this.resolutionDropDown.value == 4)
		{
			Singleton.Instance.resolutionX = 2560;
		}
		else if (this.resolutionDropDown.value == 5)
		{
			Singleton.Instance.resolutionX = 3200;
		}
		else if (this.resolutionDropDown.value == 6)
		{
			Singleton.Instance.resolutionX = 3840;
		}
		Options.SetPixelPerfect();
	}

	// Token: 0x06000FE0 RID: 4064 RVA: 0x0009A5FC File Offset: 0x000987FC
	public void ChangeScreenResolution()
	{
		PixelPerfectCamera pixelPerfectCamera = Object.FindObjectOfType<PixelPerfectCamera>();
		Screen.SetResolution(pixelPerfectCamera.refResolutionX, pixelPerfectCamera.refResolutionY, Screen.fullScreenMode);
	}

	// Token: 0x06000FE1 RID: 4065 RVA: 0x0009A628 File Offset: 0x00098828
	private void ResetButtonText(TMP_Dropdown dropdown)
	{
		int value = dropdown.value;
		if (dropdown.value < dropdown.options.Count - 1)
		{
			dropdown.value++;
			dropdown.value--;
		}
		else
		{
			dropdown.value--;
			dropdown.value++;
		}
		dropdown.value = Mathf.Clamp(dropdown.value, 0, dropdown.options.Count - 1);
		LangaugeManager.main.SetFont(dropdown.transform);
	}

	// Token: 0x06000FE2 RID: 4066 RVA: 0x0009A6BC File Offset: 0x000988BC
	private void Update()
	{
		if (this.timePassed < 0.5f)
		{
			this.timePassed += Time.deltaTime;
		}
		if (!this.eventBoxAnimator.gameObject.activeInHierarchy)
		{
			Object.FindObjectOfType<OptionsSaveManager>().SaveOptions();
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000FE3 RID: 4067 RVA: 0x0009A710 File Offset: 0x00098910
	public void ChangeLanguage()
	{
		int value = this.languageDropDown.value;
		if (LangaugeManager.main.chosenLanguageNum != value)
		{
			LangaugeManager.main.ChooseLanague(value);
		}
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x0009A744 File Offset: 0x00098944
	public void ChangeVolume()
	{
		if (this.timePassed < 0.5f)
		{
			return;
		}
		SoundManager.main.sfxVolume = this.sfxSlider.value;
		SoundManager.main.musicVolume = this.musicSlider.value;
		Singleton.Instance.itemAnimationSpeed = Mathf.Max(this.itemAnimationSpeedSlider.value, 0.01f);
		Singleton.Instance.brightness = Mathf.Max(this.brightnessSlider.value, 0.01f);
		Singleton.Instance.cursorSpeed = Mathf.Max(this.cursorSpeedSlider.value, 0.01f);
		GameManager main = GameManager.main;
		if (main)
		{
			main.SetBrightness();
		}
		SoundManager.main.ChangeAllVolume();
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x0009A804 File Offset: 0x00098A04
	public void EndVolumeChange()
	{
		SoundManager.main.PlaySFX("volumeSet");
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x0009A818 File Offset: 0x00098A18
	public void ChangeScreenSize()
	{
		if (this.timePassed < 0.5f)
		{
			return;
		}
		if (this.screenSize.value == 0)
		{
			Screen.fullScreen = false;
			Screen.fullScreenMode = FullScreenMode.Windowed;
			return;
		}
		if (this.screenSize.value == 1)
		{
			Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
			Screen.fullScreen = true;
			return;
		}
		if (this.screenSize.value == 2)
		{
			Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
		}
	}

	// Token: 0x06000FE7 RID: 4071 RVA: 0x0009A87C File Offset: 0x00098A7C
	public void ChangeQuality()
	{
		if (this.timePassed < 0.5f)
		{
			return;
		}
		if (this.quality.value == 0)
		{
			QualitySettings.SetQualityLevel(0, true);
			return;
		}
		if (this.quality.value == 1)
		{
			QualitySettings.SetQualityLevel(1, true);
			return;
		}
		if (this.quality.value == 2)
		{
			QualitySettings.SetQualityLevel(2, true);
		}
	}

	// Token: 0x06000FE8 RID: 4072 RVA: 0x0009A8D8 File Offset: 0x00098AD8
	public void ChangeToggles()
	{
		if (this.timePassed < 0.5f)
		{
			return;
		}
		Singleton.Instance.rotateButton = this.rotateButtonToggle.isOn;
		Singleton.Instance.showEmojis = this.emojiToggle.isOn;
		Singleton.Instance.playerAnimations = this.playerAnimationsToggle.isOn;
		Singleton.Instance.autoCloseMap = this.autoCloseMapToggle.isOn;
		Singleton.Instance.showOutlineOnCarvings = this.showCarvingOutlineToggle.isOn;
		Singleton.Instance.clickOnceToPickupAndAgainToDrop = this.clickToMoveToggle.isOn;
		Singleton.Instance.snapToGrid = this.snapToBuildingGridToggle.isOn;
		if (Singleton.Instance.reverseAandB != this.reverseAandBToggle.isOn || Singleton.Instance.reverseXandY != this.reverseXandYToggle.isOn)
		{
			Singleton.Instance.reverseAandB = this.reverseAandBToggle.isOn;
			Singleton.Instance.reverseXandY = this.reverseXandYToggle.isOn;
			DigitalCursor.main.ResetButtons();
		}
		Singleton.Instance.allowHolidayEvents = this.allowHolidayEventsToggle.isOn;
		ItemMovement.ConsiderChangingAllShaders();
	}

	// Token: 0x06000FE9 RID: 4073 RVA: 0x0009AA04 File Offset: 0x00098C04
	public void ChangeFPS()
	{
		if (this.fpsDropDown.value == 0)
		{
			Singleton.Instance.fps = 15;
		}
		else if (this.fpsDropDown.value == 1)
		{
			Singleton.Instance.fps = 30;
		}
		else if (this.fpsDropDown.value == 2)
		{
			Singleton.Instance.fps = 45;
		}
		else if (this.fpsDropDown.value == 3)
		{
			Singleton.Instance.fps = 60;
		}
		else if (this.fpsDropDown.value == 4)
		{
			Singleton.Instance.fps = 99;
		}
		Object.FindObjectOfType<OptionsSaveManager>().ApplySettings();
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x0009AAA4 File Offset: 0x00098CA4
	public void EndEvent()
	{
		if (this.finished)
		{
			return;
		}
		SoundManager.main.PlaySFX("menuBlip");
		this.finished = true;
		this.eventBoxAnimator.Play("Out");
		Singleton.Instance.showingOptions = false;
		GameManager main = GameManager.main;
		if (main)
		{
			main.SetAllItemColliders(true);
			main.viewingEvent = false;
		}
		MenuManager menuManager = Object.FindObjectOfType<MenuManager>();
		if (menuManager)
		{
			menuManager.ShowButtons();
		}
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x0009AB1B File Offset: 0x00098D1B
	public void OpenLink(string link)
	{
		Application.OpenURL(link);
	}

	// Token: 0x06000FEC RID: 4076 RVA: 0x0009AB24 File Offset: 0x00098D24
	public void CopyLink(string link)
	{
		GUIUtility.systemCopyBuffer = link;
		MenuManager main = MenuManager.main;
		Vector2 vector = base.GetComponentInParent<Canvas>().transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		if (main)
		{
			main.CreatePopUp(LangaugeManager.main.GetTextByKey("optQQ").Replace("/x", link), vector, 0.3f);
			return;
		}
		GameManager main2 = GameManager.main;
		if (main2)
		{
			main2.CreatePopUp(LangaugeManager.main.GetTextByKey("optQQ").Replace("/x", link), vector, 0.3f);
		}
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x0009ABC8 File Offset: 0x00098DC8
	private void PopUp(string text, Vector2 position, float time)
	{
		position = base.GetComponentInParent<Canvas>().transform.InverseTransformPoint(position);
		MenuManager main = MenuManager.main;
		if (main)
		{
			main.CreatePopUp(text, position, time);
			return;
		}
		GameManager main2 = GameManager.main;
		if (main2)
		{
			main2.CreatePopUp(text, position, time);
		}
	}

	// Token: 0x06000FEE RID: 4078 RVA: 0x0009AC21 File Offset: 0x00098E21
	public void OpenTwitchMenu()
	{
		Object.Instantiate<GameObject>(this.twitchMenuPrefab, base.transform.parent);
	}

	// Token: 0x06000FEF RID: 4079 RVA: 0x0009AC3C File Offset: 0x00098E3C
	public void DeleteAllSaveButton(string _typeOfDelete)
	{
		SoundManager.main.PlaySFX("menuBlip");
		this.typeOfDelete = _typeOfDelete;
		string text = "";
		if (this.typeOfDelete == "all")
		{
			text = LangaugeManager.main.GetTextByKey("opt13");
		}
		else if (this.typeOfDelete == "tut")
		{
			text = LangaugeManager.main.GetTextByKey("opt13c");
		}
		else if (this.typeOfDelete == "games")
		{
			text = LangaugeManager.main.GetTextByKey("opt13b");
		}
		ConfirmationManager.CreateConfirmation(text, LangaugeManager.main.GetTextByKey("opt14"), new Func<Action>(this.ConfirmDeleteAllSaves));
	}

	// Token: 0x06000FF0 RID: 4080 RVA: 0x0009ACF0 File Offset: 0x00098EF0
	public Action ConfirmDeleteAllSaves()
	{
		SoundManager.main.PlaySFX("menuBlip");
		string persistentDataPath = Application.persistentDataPath;
		if (this.typeOfDelete == "all")
		{
			for (int i = -100; i < 200; i++)
			{
				if (ES3.FileExists(string.Format("{0}/bphRun{1}.sav", persistentDataPath, i)) || ES3.FileExists(string.Format("{0}/ bphRun{1}.sav", persistentDataPath, i)))
				{
					SaveManager.DeleteSave(i);
				}
			}
			ES3.DeleteFile(persistentDataPath + "/bph.sav");
			ES3.DeleteFile(persistentDataPath + "/bphMeta.sav");
			ES3.DeleteFile(persistentDataPath + "/bphSettings.sav");
			ES3.DeleteFile(persistentDataPath + "/ bph.sav");
			ES3.DeleteFile(persistentDataPath + "/ bphMeta.sav");
			ES3.DeleteFile(persistentDataPath + "/ bphSettings.sav");
			foreach (string text in Directory.GetFiles(persistentDataPath + "/"))
			{
			}
			Debug.Log("All save data deleted!");
		}
		else if (this.typeOfDelete == "tut")
		{
			Object.FindObjectOfType<MetaProgressSaveManager>().SaveCompletedTutorials(new List<string>());
			TutorialManager tutorialManager = Object.FindObjectOfType<TutorialManager>();
			if (tutorialManager)
			{
				tutorialManager.completedTutorials = new List<string>();
			}
			Debug.Log("All tutorials reset!");
		}
		else if (this.typeOfDelete == "games")
		{
			for (int k = -100; k < 200; k++)
			{
				if (ES3.FileExists(string.Format("{0}/bphRun{1}.sav", persistentDataPath, k)) || ES3.FileExists(string.Format("{0}/ bphRun{1}.sav", persistentDataPath, k)))
				{
					SaveManager.DeleteSave(k);
				}
			}
			foreach (string text2 in Directory.GetFiles(persistentDataPath + "/"))
			{
				if (text2.Contains("bphRun") || text2.Contains("runScreenshot"))
				{
					File.Delete(text2);
				}
			}
			Debug.Log("All run saves deleted!");
		}
		this.confirmDeleteAllButton.SetActive(false);
		return null;
	}

	// Token: 0x06000FF1 RID: 4081 RVA: 0x0009AF10 File Offset: 0x00099110
	public static void QuitGame(bool overrideIronMan = false)
	{
		Object @object = Object.FindObjectOfType<Overworld_SaveManager>();
		string textByKey = LangaugeManager.main.GetTextByKey("opt14");
		if (@object)
		{
			ConfirmationManager.CreateConfirmation(textByKey, new Func<Action>(Options.SaveOverworldAndQuitGameInternal));
			return;
		}
		if (!Singleton.Instance.storyMode || !GameManager.main)
		{
			ConfirmationManager.CreateConfirmation(textByKey, new Func<Action>(Options.QuitGameInternal));
			return;
		}
		if (GameManager.main.CanSave())
		{
			string textByKey2 = LangaugeManager.main.GetTextByKey("opt22");
			ConfirmationManager.CreateConfirmation(textByKey, textByKey2, new Func<Action>(Options.SaveAndQuitGameInternal));
			return;
		}
		string textByKey3 = LangaugeManager.main.GetTextByKey("opt23");
		ConfirmationManager.CreateConfirmation(textByKey, textByKey3, new Func<Action>(Options.QuitGameInternal));
	}

	// Token: 0x06000FF2 RID: 4082 RVA: 0x0009AFCA File Offset: 0x000991CA
	public static Action SaveAndQuitGameInternal()
	{
		SingleUI.DestroyAll();
		GameManager.main.StartCoroutine(Options.SaveAndQuitGameInternalRoutine());
		return null;
	}

	// Token: 0x06000FF3 RID: 4083 RVA: 0x0009AFE2 File Offset: 0x000991E2
	public static Action SaveOverworldAndQuitGameInternal()
	{
		MonoBehaviour monoBehaviour = Object.FindObjectOfType<Overworld_SaveManager>();
		SingleUI.DestroyAll();
		monoBehaviour.StartCoroutine(Options.SaveOverworldAndQuitGameInternalRoutine());
		return null;
	}

	// Token: 0x06000FF4 RID: 4084 RVA: 0x0009AFFA File Offset: 0x000991FA
	private static IEnumerator SaveOverworldAndQuitGameInternalRoutine()
	{
		Overworld_SaveManager saveManager = Object.FindObjectOfType<Overworld_SaveManager>();
		if (!saveManager)
		{
			Debug.LogError("No save manager found!");
			yield break;
		}
		saveManager.SaveCommand(false);
		yield return new WaitForSeconds(0.5f);
		while (saveManager.isSavingOrLoading)
		{
			yield return new WaitForSeconds(0.1f);
		}
		Options.QuitGameInternal();
		yield break;
	}

	// Token: 0x06000FF5 RID: 4085 RVA: 0x0009B002 File Offset: 0x00099202
	private static IEnumerator SaveAndQuitGameInternalRoutine()
	{
		SaveManager saveManager = Object.FindObjectOfType<SaveManager>();
		if (!saveManager)
		{
			Debug.LogError("No save manager found!");
			yield break;
		}
		yield return saveManager.Save(null, "Game Saved");
		Options.QuitGameInternal();
		yield break;
	}

	// Token: 0x06000FF6 RID: 4086 RVA: 0x0009B00A File Offset: 0x0009920A
	private static Action QuitGameInternal()
	{
		if (Singleton.Instance.storyMode && !LoadStoryGame.SaveExists(Singleton.Instance.storyModeSlot))
		{
			LoadStoryGame.DeleteStorySaveFile(Singleton.Instance.storyModeSlot);
		}
		GameManager.QuitGame(false);
		return null;
	}

	// Token: 0x06000FF7 RID: 4087 RVA: 0x0009B040 File Offset: 0x00099240
	public static void ReturnToOrderia(bool overrideIronMan = false)
	{
		string textByKey = LangaugeManager.main.GetTextByKey("opt21");
		string textByKey2 = LangaugeManager.main.GetTextByKey("opt21d");
		ConfirmationManager.CreateConfirmation(textByKey, textByKey2, new Func<Action>(Options.ReturnToOrderiaInternal));
	}

	// Token: 0x06000FF8 RID: 4088 RVA: 0x0009B080 File Offset: 0x00099280
	public static Action ReturnToOrderiaInternal()
	{
		MetaProgressSaveManager.main.SaveLastRun(MetaProgressSaveManager.LastRun.Result.quit);
		ES3.DeleteFile(Application.persistentDataPath + "/" + "bphStoryModeRun" + Singleton.Instance.storyModeSlot.ToString() + ".sav");
		SceneLoader.main.LoadScene("Overworld", LoadSceneMode.Single, null, null);
		return null;
	}

	// Token: 0x04000CFF RID: 3327
	[SerializeField]
	private GameObject resourceTotalParent;

	// Token: 0x04000D00 RID: 3328
	[SerializeField]
	private Overworld_ResourceDisplayPanel resourceDisplayPanel;

	// Token: 0x04000D01 RID: 3329
	[SerializeField]
	private Mask mask;

	// Token: 0x04000D02 RID: 3330
	[SerializeField]
	private GameObject returnToMenuButton;

	// Token: 0x04000D03 RID: 3331
	[SerializeField]
	private GameObject returnToOrderiaButton;

	// Token: 0x04000D04 RID: 3332
	[SerializeField]
	private Slider musicSlider;

	// Token: 0x04000D05 RID: 3333
	[SerializeField]
	private Slider sfxSlider;

	// Token: 0x04000D06 RID: 3334
	[SerializeField]
	private Slider itemAnimationSpeedSlider;

	// Token: 0x04000D07 RID: 3335
	[SerializeField]
	private Slider brightnessSlider;

	// Token: 0x04000D08 RID: 3336
	[SerializeField]
	private Slider cursorSpeedSlider;

	// Token: 0x04000D09 RID: 3337
	[SerializeField]
	private TMP_Dropdown screenSize;

	// Token: 0x04000D0A RID: 3338
	[SerializeField]
	private TMP_Dropdown quality;

	// Token: 0x04000D0B RID: 3339
	[SerializeField]
	private GameObject languageHeader;

	// Token: 0x04000D0C RID: 3340
	[SerializeField]
	private TMP_Dropdown languageDropDown;

	// Token: 0x04000D0D RID: 3341
	[SerializeField]
	private TMP_Dropdown fpsDropDown;

	// Token: 0x04000D0E RID: 3342
	[SerializeField]
	private TMP_Dropdown resolutionDropDown;

	// Token: 0x04000D0F RID: 3343
	[SerializeField]
	private TMP_Dropdown controllerDropDown;

	// Token: 0x04000D10 RID: 3344
	[SerializeField]
	private Toggle rotateButtonToggle;

	// Token: 0x04000D11 RID: 3345
	[SerializeField]
	private Toggle playerAnimationsToggle;

	// Token: 0x04000D12 RID: 3346
	[SerializeField]
	private Toggle emojiToggle;

	// Token: 0x04000D13 RID: 3347
	[SerializeField]
	private Toggle autoCloseMapToggle;

	// Token: 0x04000D14 RID: 3348
	[SerializeField]
	private Toggle showCarvingOutlineToggle;

	// Token: 0x04000D15 RID: 3349
	[SerializeField]
	private Toggle clickToMoveToggle;

	// Token: 0x04000D16 RID: 3350
	[SerializeField]
	private Toggle snapToBuildingGridToggle;

	// Token: 0x04000D17 RID: 3351
	[SerializeField]
	private Toggle reverseAandBToggle;

	// Token: 0x04000D18 RID: 3352
	[SerializeField]
	private Toggle reverseXandYToggle;

	// Token: 0x04000D19 RID: 3353
	[SerializeField]
	private Toggle allowHolidayEventsToggle;

	// Token: 0x04000D1A RID: 3354
	private bool finished;

	// Token: 0x04000D1B RID: 3355
	[SerializeField]
	private Animator eventBoxAnimator;

	// Token: 0x04000D1C RID: 3356
	[SerializeField]
	private GameObject deleteAllButton;

	// Token: 0x04000D1D RID: 3357
	[SerializeField]
	private GameObject deleteAllSavesButton;

	// Token: 0x04000D1E RID: 3358
	[SerializeField]
	private GameObject confirmDeleteAllButton;

	// Token: 0x04000D1F RID: 3359
	[SerializeField]
	private GameObject twitchMenuPrefab;

	// Token: 0x04000D20 RID: 3360
	private float timePassed;

	// Token: 0x04000D21 RID: 3361
	private string typeOfDelete = "";
}
