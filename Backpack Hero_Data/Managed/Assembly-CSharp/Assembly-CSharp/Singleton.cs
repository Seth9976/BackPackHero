using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200011B RID: 283
public class Singleton : MonoBehaviour
{
	// Token: 0x17000016 RID: 22
	// (get) Token: 0x06000995 RID: 2453 RVA: 0x00061F1C File Offset: 0x0006011C
	public static Singleton Instance
	{
		get
		{
			return Singleton._instance;
		}
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x00061F23 File Offset: 0x00060123
	public void SetCharacter(Character.CharacterName name)
	{
		this.character = name;
		this.SetNumberFromCharacter(name);
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x00061F33 File Offset: 0x00060133
	public bool IsStoryModeLevels()
	{
		return !(this.mission == null) && this.storyMode && this.mission.dungeonLevels != null && this.mission.dungeonLevels.Count != 0;
	}

	// Token: 0x06000998 RID: 2456 RVA: 0x00061F74 File Offset: 0x00060174
	public void SetCharacterFromNumber()
	{
		if (Singleton.Instance.characterNumber < 0 || Singleton.Instance.characterNumber >= 5)
		{
			Singleton.Instance.characterNumber = 0;
		}
		if (this.characterNumber == 0)
		{
			this.character = Character.CharacterName.Purse;
			return;
		}
		if (this.characterNumber == 1)
		{
			this.character = Character.CharacterName.Tote;
			return;
		}
		if (this.characterNumber == 2)
		{
			this.character = Character.CharacterName.CR8;
			return;
		}
		if (this.characterNumber == 3)
		{
			this.character = Character.CharacterName.Satchel;
			return;
		}
		if (this.characterNumber == 4)
		{
			this.character = Character.CharacterName.Pochette;
		}
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x00061FF9 File Offset: 0x000601F9
	public void SetNumberFromCharacter(Character.CharacterName name)
	{
		if (name == Character.CharacterName.Purse)
		{
			this.characterNumber = 0;
			return;
		}
		if (name == Character.CharacterName.Tote)
		{
			this.characterNumber = 1;
			return;
		}
		if (name == Character.CharacterName.CR8)
		{
			this.characterNumber = 2;
			return;
		}
		if (name == Character.CharacterName.Satchel)
		{
			this.characterNumber = 3;
			return;
		}
		if (name == Character.CharacterName.Pochette)
		{
			this.characterNumber = 4;
		}
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x00062038 File Offset: 0x00060238
	public int GetLowestSave()
	{
		string text = Application.persistentDataPath + "/";
		new ES3Settings(null, null).path = text;
		for (int i = 0; i < 9999; i++)
		{
			if (!ES3.FileExists(text + "bphRun" + i.ToString() + ".sav"))
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x00062094 File Offset: 0x00060294
	private void Awake()
	{
		if (Singleton._instance != null && Singleton._instance != this)
		{
			Object.Destroy(base.gameObject);
		}
		else
		{
			Singleton._instance = this;
			this.townSaveBackup = null;
			this.SetCharacterFromNumber();
			Object.DontDestroyOnLoad(base.gameObject);
			this.CheckCommandArgs();
		}
		this.SetCharacterFromNumber();
		SceneManager.sceneLoaded += this.OnSceneLoaded;
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x00062103 File Offset: 0x00060303
	private void Start()
	{
		if (Singleton._instance == this)
		{
			this._optionsSaveManager = base.GetComponent<OptionsSaveManager>();
		}
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x00062120 File Offset: 0x00060320
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == "MainMenu")
		{
			Debug.Log("Purging stale overworld save cache");
			this.townSaveBackup = null;
		}
		if (this.errorMessage != null && this.errorMessage != "")
		{
			WorkshopWizard component = Object.Instantiate<GameObject>(this.messageBox, GameObject.FindGameObjectWithTag("UI Canvas").transform).GetComponent<WorkshopWizard>();
			component.wizardMode = false;
			component.SetMessage(this.errorMessage, WorkshopWizard.Action.Dismiss);
			this.errorMessage = null;
			foreach (TMP_Text tmp_Text in component.gameObject.GetComponentsInChildren<TMP_Text>())
			{
				if (tmp_Text.gameObject.name == "Title")
				{
					tmp_Text.text = "ERROR";
				}
			}
		}
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x000621E7 File Offset: 0x000603E7
	private void Update()
	{
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x000621EC File Offset: 0x000603EC
	public void ShowErrorMessage(string message)
	{
		if (message == null)
		{
			throw new NullReferenceException("Error message cannot be null");
		}
		if (message == "")
		{
			throw new Exception("Error message cannot be an empty string");
		}
		WorkshopWizard component = Object.Instantiate<GameObject>(this.messageBox, GameObject.FindGameObjectWithTag("UI Canvas").transform).GetComponent<WorkshopWizard>();
		component.wizardMode = false;
		component.SetMessage(message, WorkshopWizard.Action.Dismiss);
		foreach (TMP_Text tmp_Text in component.gameObject.GetComponentsInChildren<TMP_Text>())
		{
			if (tmp_Text.gameObject.name == "Title")
			{
				tmp_Text.text = "ERROR";
			}
		}
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x0006228C File Offset: 0x0006048C
	public void CheckCommandArgs()
	{
		Debug.Log("---------------------------------------------------------");
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		for (int i = 0; i < commandLineArgs.Length; i++)
		{
			if (commandLineArgs[i].ToLower() == "-debug")
			{
				Debug.Log("Beep boop. I am in debug mode");
				Singleton.Instance.developmentMode = true;
			}
		}
		Debug.Log("---------------------------------------------------------");
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x000622EA File Offset: 0x000604EA
	public void ChangedActiveScene(Scene current, Scene next)
	{
		Debug.Log("Changing to Scene1");
		Options.SetPixelPerfect();
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x000622FB File Offset: 0x000604FB
	private void ChangeScene()
	{
		Debug.Log("Changing to Scene2");
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x00062307 File Offset: 0x00060507
	public void StartGameScene()
	{
		this.showingOptions = false;
	}

	// Token: 0x040007A7 RID: 1959
	[Header("Pax Demo Settings")]
	public float demoTime;

	// Token: 0x040007A8 RID: 1960
	[SerializeField]
	public bool isDemo;

	// Token: 0x040007A9 RID: 1961
	[SerializeField]
	public bool allowOtherCharacters;

	// Token: 0x040007AA RID: 1962
	[SerializeField]
	public bool allowOtherGameModes;

	// Token: 0x040007AB RID: 1963
	[SerializeField]
	public bool allowOptions;

	// Token: 0x040007AC RID: 1964
	[SerializeField]
	public bool enableTimeout;

	// Token: 0x040007AD RID: 1965
	[SerializeField]
	public bool endGameAfterBoss;

	// Token: 0x040007AE RID: 1966
	[SerializeField]
	public bool showQRCode;

	// Token: 0x040007AF RID: 1967
	[SerializeField]
	public List<GameObject> otherPrefabObjectsToSpawn = new List<GameObject>();

	// Token: 0x040007B0 RID: 1968
	[SerializeField]
	public bool reverseAandB;

	// Token: 0x040007B1 RID: 1969
	[SerializeField]
	public bool reverseXandY;

	// Token: 0x040007B2 RID: 1970
	private static Singleton _instance;

	// Token: 0x040007B3 RID: 1971
	[HideInInspector]
	public OptionsSaveManager _optionsSaveManager;

	// Token: 0x040007B4 RID: 1972
	[Header("------------------------------------------")]
	public int storyModeSlot;

	// Token: 0x040007B5 RID: 1973
	public Missions mission;

	// Token: 0x040007B6 RID: 1974
	public bool storyMode;

	// Token: 0x040007B7 RID: 1975
	[SerializeField]
	public Missions tutorialMission;

	// Token: 0x040007B8 RID: 1976
	public bool loadOverworld = true;

	// Token: 0x040007B9 RID: 1977
	public bool isTestingMode;

	// Token: 0x040007BA RID: 1978
	public string errorMessage;

	// Token: 0x040007BB RID: 1979
	[SerializeField]
	public GameObject messageBox;

	// Token: 0x040007BC RID: 1980
	[NonSerialized]
	public byte[] townSaveBackup;

	// Token: 0x040007BD RID: 1981
	[Header("------------------------------------------")]
	public bool completedTutorial;

	// Token: 0x040007BE RID: 1982
	public bool loadSave;

	// Token: 0x040007BF RID: 1983
	public bool doTutorial;

	// Token: 0x040007C0 RID: 1984
	public int characterNumber;

	// Token: 0x040007C1 RID: 1985
	public Character.CharacterName character = Character.CharacterName.Purse;

	// Token: 0x040007C2 RID: 1986
	public RunType runType;

	// Token: 0x040007C3 RID: 1987
	public int costumeNumber;

	// Token: 0x040007C4 RID: 1988
	public float speedRunTime;

	// Token: 0x040007C5 RID: 1989
	public float brightness = 0.5f;

	// Token: 0x040007C6 RID: 1990
	public float itemAnimationSpeed = 0.1f;

	// Token: 0x040007C7 RID: 1991
	public bool rotateButton;

	// Token: 0x040007C8 RID: 1992
	public bool modDebugging;

	// Token: 0x040007C9 RID: 1993
	public bool showEmojis = true;

	// Token: 0x040007CA RID: 1994
	public bool allowHolidayEvents = true;

	// Token: 0x040007CB RID: 1995
	public bool showingOptions;

	// Token: 0x040007CC RID: 1996
	public bool playerAnimations = true;

	// Token: 0x040007CD RID: 1997
	public bool snapToGrid = true;

	// Token: 0x040007CE RID: 1998
	public bool autoCloseMap;

	// Token: 0x040007CF RID: 1999
	public bool ironMan;

	// Token: 0x040007D0 RID: 2000
	public int fps;

	// Token: 0x040007D1 RID: 2001
	public int chosenControllerIcons = -1;

	// Token: 0x040007D2 RID: 2002
	public int resolutionX = 1280;

	// Token: 0x040007D3 RID: 2003
	public bool showOutlineOnCarvings = true;

	// Token: 0x040007D4 RID: 2004
	public int saveNumber;

	// Token: 0x040007D5 RID: 2005
	public bool analyticsActive;

	// Token: 0x040007D6 RID: 2006
	public bool analyticsSent;

	// Token: 0x040007D7 RID: 2007
	public float cursorSpeed = 1f;

	// Token: 0x040007D8 RID: 2008
	public bool clickOnceToPickupAndAgainToDrop;

	// Token: 0x040007D9 RID: 2009
	[SerializeField]
	public bool developmentMode;

	// Token: 0x040007DA RID: 2010
	[SerializeField]
	public bool unlockAllAtlas;

	// Token: 0x040007DB RID: 2011
	[Header("Twitch Integration Settings")]
	public bool twitchIntegrationEnabled = true;

	// Token: 0x040007DC RID: 2012
	public float twitchPollTimeout = 30f;

	// Token: 0x040007DD RID: 2013
	public bool twitchPollCheckDuplicate;

	// Token: 0x040007DE RID: 2014
	public bool twitchEnablePolls = true;

	// Token: 0x040007DF RID: 2015
	public bool twitchEnableDeathCounter = true;

	// Token: 0x040007E0 RID: 2016
	public bool twitchEnableEmoteEffect;
}
