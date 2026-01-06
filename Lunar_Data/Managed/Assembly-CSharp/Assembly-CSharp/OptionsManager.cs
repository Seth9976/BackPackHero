using System;
using SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Token: 0x02000060 RID: 96
public class OptionsManager : MonoBehaviour
{
	// Token: 0x060002BC RID: 700 RVA: 0x0000E354 File Offset: 0x0000C554
	private void Start()
	{
		LanguageManager.OnReplaceText += this.ReplaceText;
		this.sfxVolumeSlider.value = SoundManager.instance.sfxVolume;
		this.musicVolumeSlider.value = SoundManager.instance.musicVolume;
		switch (Screen.fullScreenMode)
		{
		case FullScreenMode.ExclusiveFullScreen:
			this.screenSize.SetValueWithoutNotify(1);
			break;
		case FullScreenMode.FullScreenWindow:
			this.screenSize.SetValueWithoutNotify(2);
			break;
		case FullScreenMode.Windowed:
			this.screenSize.SetValueWithoutNotify(0);
			break;
		}
		if (Screen.currentResolution.width == 1366 && Screen.currentResolution.height == 768)
		{
			this.screenResolution.SetValueWithoutNotify(0);
		}
		else if (Screen.currentResolution.width == 1920 && Screen.currentResolution.height == 1080)
		{
			this.screenResolution.SetValueWithoutNotify(1);
		}
		else if (Screen.currentResolution.width == 2560 && Screen.currentResolution.height == 1440)
		{
			this.screenResolution.SetValueWithoutNotify(2);
		}
		if (ControllerSpriteManager.instance.autoDetect)
		{
			this.controllerSpriteSet.SetValueWithoutNotify(0);
		}
		else
		{
			switch (ControllerSpriteManager.instance.currentSpriteSet)
			{
			case ControllerSpriteManager.ControllerSpriteType.Xbox:
				this.controllerSpriteSet.SetValueWithoutNotify(1);
				break;
			case ControllerSpriteManager.ControllerSpriteType.Switch:
				this.controllerSpriteSet.SetValueWithoutNotify(2);
				break;
			case ControllerSpriteManager.ControllerSpriteType.PS:
				this.controllerSpriteSet.SetValueWithoutNotify(3);
				break;
			}
		}
		this.stretchToFill.isOn = Singleton.instance.stretchToFill;
		this.languageDropdown.ClearOptions();
		foreach (string text in LanguageManager.main.discoveredLanguages)
		{
			this.languageDropdown.options.Add(new TMP_Dropdown.OptionData(text));
		}
		this.languageDropdown.SetValueWithoutNotify(LanguageManager.main.chosenLanguageNum);
	}

	// Token: 0x060002BD RID: 701 RVA: 0x0000E574 File Offset: 0x0000C774
	public void ReplaceText()
	{
		if (ControllerSpriteManager.instance.autoDetect)
		{
			this.controllerSpriteSet.SetValueWithoutNotify(1);
			this.controllerSpriteSet.SetValueWithoutNotify(0);
		}
	}

	// Token: 0x060002BE RID: 702 RVA: 0x0000E59A File Offset: 0x0000C79A
	private void Update()
	{
	}

	// Token: 0x060002BF RID: 703 RVA: 0x0000E59C File Offset: 0x0000C79C
	public void PlaySFX()
	{
		SoundManager.instance.PlaySFX("blip", double.PositiveInfinity);
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x0000E5B6 File Offset: 0x0000C7B6
	public void OpenURL(string url)
	{
		Application.OpenURL(url);
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x0000E5BE File Offset: 0x0000C7BE
	public void UpdateSFXVolume()
	{
		SoundManager.instance.sfxVolume = this.sfxVolumeSlider.value;
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x0000E5D5 File Offset: 0x0000C7D5
	public void UpdateMusicVolume()
	{
		SoundManager.instance.musicVolume = this.musicVolumeSlider.value;
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x0000E5EC File Offset: 0x0000C7EC
	public void ToggleStretchToFill()
	{
		Singleton.instance.stretchToFill = this.stretchToFill.isOn;
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x0000E604 File Offset: 0x0000C804
	public void ChangeResolution()
	{
		switch (this.screenResolution.value)
		{
		case 0:
			Screen.SetResolution(1366, 768, Screen.fullScreenMode);
			return;
		case 1:
			Screen.SetResolution(1920, 1080, Screen.fullScreenMode);
			return;
		case 2:
			Screen.SetResolution(2560, 1440, Screen.fullScreenMode);
			return;
		default:
			return;
		}
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x0000E66E File Offset: 0x0000C86E
	public void ChangeLanguage()
	{
		LanguageManager.main.ChooseLanguageViaString(this.languageDropdown.options[this.languageDropdown.value].text);
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x0000E69C File Offset: 0x0000C89C
	public void ChangeScreenSize()
	{
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

	// Token: 0x060002C7 RID: 711 RVA: 0x0000E6F4 File Offset: 0x0000C8F4
	public void ChangeControllerSpriteSet()
	{
		switch (this.controllerSpriteSet.value)
		{
		case 0:
			ControllerSpriteManager.instance.autoDetect = true;
			ControllerSpriteManager.instance.AutoDetect(Gamepad.current);
			return;
		case 1:
			ControllerSpriteManager.instance.autoDetect = false;
			ControllerSpriteManager.instance.SwitchSpriteSet(ControllerSpriteManager.ControllerSpriteType.Xbox);
			return;
		case 2:
			ControllerSpriteManager.instance.autoDetect = false;
			ControllerSpriteManager.instance.SwitchSpriteSet(ControllerSpriteManager.ControllerSpriteType.Switch);
			return;
		case 3:
			ControllerSpriteManager.instance.autoDetect = false;
			ControllerSpriteManager.instance.SwitchSpriteSet(ControllerSpriteManager.ControllerSpriteType.PS);
			return;
		default:
			return;
		}
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x0000E783 File Offset: 0x0000C983
	public void CloseWindow()
	{
		SaveManager.SaveOptions();
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x0000E795 File Offset: 0x0000C995
	public void DeleteAllProgress()
	{
		ConfirmationBoxManager.instance.ShowConfirmationBox("sureDeleteAll", new ConfirmationBox.OnConfirm(this.DeleteAllProgressConfirmed), null);
	}

	// Token: 0x060002CA RID: 714 RVA: 0x0000E7B3 File Offset: 0x0000C9B3
	public void DeleteAllProgressConfirmed()
	{
		Singleton.instance.DeleteAllProgress();
		PlayerPrefs.DeleteAll();
		this.CloseWindow();
	}

	// Token: 0x0400020E RID: 526
	[SerializeField]
	private Slider sfxVolumeSlider;

	// Token: 0x0400020F RID: 527
	[SerializeField]
	private Slider musicVolumeSlider;

	// Token: 0x04000210 RID: 528
	[SerializeField]
	private TMP_Dropdown screenSize;

	// Token: 0x04000211 RID: 529
	[SerializeField]
	private TMP_Dropdown screenResolution;

	// Token: 0x04000212 RID: 530
	[SerializeField]
	private TMP_Dropdown controllerSpriteSet;

	// Token: 0x04000213 RID: 531
	[SerializeField]
	private TMP_Dropdown languageDropdown;

	// Token: 0x04000214 RID: 532
	[SerializeField]
	private Toggle stretchToFill;
}
