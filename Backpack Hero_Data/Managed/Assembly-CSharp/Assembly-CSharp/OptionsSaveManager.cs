using System;
using System.Collections.Generic;
using DevPunksSaveGame;
using UnityEngine;

// Token: 0x02000166 RID: 358
public class OptionsSaveManager : MonoBehaviour
{
	// Token: 0x17000022 RID: 34
	// (get) Token: 0x06000E95 RID: 3733 RVA: 0x0009188E File Offset: 0x0008FA8E
	// (set) Token: 0x06000E96 RID: 3734 RVA: 0x00091895 File Offset: 0x0008FA95
	public static bool IsLoaded { get; private set; }

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x06000E97 RID: 3735 RVA: 0x0009189D File Offset: 0x0008FA9D
	private ES3Settings settings
	{
		get
		{
			if (this._settings == null)
			{
				this._settings = new ES3Settings(Application.persistentDataPath + "/bphSettings.sav", null);
			}
			return this._settings;
		}
	}

	// Token: 0x06000E98 RID: 3736 RVA: 0x000918C8 File Offset: 0x0008FAC8
	private void Start()
	{
		this._dirtyFiles = new HashSet<string>();
		this._loadedFiles = new List<string>();
		this.gameManager = GameManager.main;
		this.DoLoad();
	}

	// Token: 0x06000E99 RID: 3737 RVA: 0x000918F1 File Offset: 0x0008FAF1
	public void DoLoad()
	{
		this.LoadOptions();
		this.ApplySettings();
		Debug.Log("Done loading options!");
		Console.WriteLine("Done loading options!!!");
	}

	// Token: 0x06000E9A RID: 3738 RVA: 0x00091914 File Offset: 0x0008FB14
	private void Update()
	{
		if (!this.setup)
		{
			this.setup = true;
			this.DoLoad();
		}
		if (this._dirtyFiles.Count > 0)
		{
			Debug.Log("Has dirty files!");
			ES3Settings es3Settings = new ES3Settings(null, null);
			es3Settings.encryptionType = ES3.EncryptionType.None;
			es3Settings.location = ES3.Location.Cache;
			foreach (string text in this._dirtyFiles)
			{
				es3Settings.path = text;
				byte[] array = ES3.LoadRawBytes(es3Settings);
				ConsoleWrapper.Instance.SaveFileAsync("slot0", es3Settings.path, array);
			}
			this._dirtyFiles.Clear();
		}
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x000919D4 File Offset: 0x0008FBD4
	public void SaveOptions()
	{
		Debug.Log("Saving options!");
		try
		{
			ES3.DeleteFile(this.settings.path);
			ES3.Save<float>("sfxVolume", SoundManager.main.sfxVolume, this.settings);
			ES3.Save<float>("musicVolume", SoundManager.main.musicVolume, this.settings);
			ES3.Save<float>("brightness", Singleton.Instance.brightness, this.settings);
			ES3.Save<float>("itemAnimationSpeed", Singleton.Instance.itemAnimationSpeed, this.settings);
			ES3.Save<int>("fps", Singleton.Instance.fps, this.settings);
			ES3.Save<int>("resolutionX", Singleton.Instance.resolutionX, this.settings);
			ES3.Save<bool>("emojis", Singleton.Instance.showEmojis, this.settings);
			ES3.Save<bool>("playerAnimations", Singleton.Instance.playerAnimations, this.settings);
			ES3.Save<bool>("autoShowMap", Singleton.Instance.autoCloseMap, this.settings);
			ES3.Save<string>("language", LangaugeManager.main.GetLanguageName(), this.settings);
			ES3.Save<float>("cursorSpeed", Singleton.Instance.cursorSpeed, this.settings);
			ES3.Save<bool>("showOutlineOnCarvings", Singleton.Instance.showOutlineOnCarvings, this.settings);
			ES3.Save<int>("controllerIcons", Singleton.Instance.chosenControllerIcons, this.settings);
			ES3.Save<bool>("allowHolidayEvents", Singleton.Instance.allowHolidayEvents, this.settings);
			Debug.Log("Finished saving options!");
		}
		catch
		{
			Debug.Log("Error saving options");
		}
	}

	// Token: 0x06000E9C RID: 3740 RVA: 0x00091B9C File Offset: 0x0008FD9C
	public void LoadOptions()
	{
		Debug.Log("Loading options!");
		string text = "english";
		if (SoundManager.main)
		{
			SoundManager.main.sfxVolume = ES3.Load<float>("sfxVolume", 0.5f, this.settings);
			SoundManager.main.musicVolume = ES3.Load<float>("musicVolume", 0.5f, this.settings);
			SoundManager.main.ChangeAllVolume();
		}
		Singleton.Instance.brightness = ES3.Load<float>("brightness", 0.5f, this.settings);
		Singleton.Instance.itemAnimationSpeed = ES3.Load<float>("itemAnimationSpeed", 0.25f, this.settings);
		Singleton.Instance.fps = ES3.Load<int>("fps", 60, this.settings);
		QualitySettings.SetQualityLevel(ES3.Load<int>("quality", 2, this.settings));
		Singleton.Instance.resolutionX = ES3.Load<int>("resolutionX", 1280, this.settings);
		Singleton.Instance.showEmojis = ES3.Load<bool>("emojis", true, this.settings);
		Singleton.Instance.playerAnimations = ES3.Load<bool>("playerAnimations", true, this.settings);
		Singleton.Instance.autoCloseMap = ES3.Load<bool>("autoShowMap", false, this.settings);
		Singleton.Instance.cursorSpeed = ES3.Load<float>("cursorSpeed", 0.5f, this.settings);
		Singleton.Instance.showOutlineOnCarvings = ES3.Load<bool>("showOutlineOnCarvings", true, this.settings);
		Singleton.Instance.chosenControllerIcons = ES3.Load<int>("controllerIcons", -1, this.settings);
		Singleton.Instance.allowHolidayEvents = ES3.Load<bool>("allowHolidayEvents", true, this.settings);
		if (LangaugeManager.main)
		{
			text = ES3.Load<string>("language", null, "english", this.settings);
		}
		if (DigitalCursor.main)
		{
			DigitalCursor.main.SetControlIcons();
		}
		Singleton.Instance.chosenControllerIcons = ES3.Load<int>("controllerIcons", -1, this.settings);
		Debug.Log("Loaded all options");
		SoundManager.main.ChangeAllVolume();
		LangaugeManager.main.SetLanguage(text);
		DigitalCursor.main.SetControlIcons();
		Debug.Log("Loaded options!");
		OptionsSaveManager.IsLoaded = true;
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x00091DEA File Offset: 0x0008FFEA
	public void ApplySettings()
	{
		Application.targetFrameRate = Singleton.Instance.fps;
		Options.SetPixelPerfect();
	}

	// Token: 0x04000BC5 RID: 3013
	private GameManager gameManager;

	// Token: 0x04000BC6 RID: 3014
	private bool setup;

	// Token: 0x04000BC7 RID: 3015
	private List<string> _loadedFiles;

	// Token: 0x04000BC8 RID: 3016
	private HashSet<string> _dirtyFiles;

	// Token: 0x04000BCA RID: 3018
	private ES3Settings _settings;
}
