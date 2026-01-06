using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DevPunksSaveGame;
using UnityEngine;

// Token: 0x02000168 RID: 360
public class SaveManager : MonoBehaviour
{
	// Token: 0x06000EA0 RID: 3744 RVA: 0x00091E10 File Offset: 0x00090010
	public void Awake()
	{
		SaveManager.main = this;
	}

	// Token: 0x06000EA1 RID: 3745 RVA: 0x00091E18 File Offset: 0x00090018
	public void OnDestory()
	{
		SaveManager.main = null;
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x00091E20 File Offset: 0x00090020
	public static SaveManager GetSaveManager()
	{
		return SaveManager.main;
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x00091E27 File Offset: 0x00090027
	private ES3Settings settings
	{
		get
		{
			if (this._settings == null)
			{
				this._settings = new ES3Settings(null, null);
			}
			return this._settings;
		}
	}

	// Token: 0x06000EA4 RID: 3748 RVA: 0x00091E44 File Offset: 0x00090044
	private void Start()
	{
		this.player = Player.main;
		this.gameManager = GameManager.main;
	}

	// Token: 0x06000EA5 RID: 3749 RVA: 0x00091E5C File Offset: 0x0009005C
	private void MakeAssociations()
	{
		if (!this.playerStatTracker)
		{
			this.playerStatTracker = GameObject.FindGameObjectWithTag("PlayerStatTracker");
		}
		if (!this.dungeonRooms)
		{
			this.dungeonRooms = GameObject.FindGameObjectWithTag("RoomsParent");
		}
		if (!this.backpack)
		{
			this.backpack = GameObject.FindGameObjectWithTag("Inventory");
		}
		if (!this.dungeonPlayer)
		{
			this.dungeonPlayer = GameObject.FindGameObjectWithTag("DungeonPlayer");
		}
		if (!this.gameManager)
		{
			this.gameManager = GameManager.main;
		}
		if (!this.player)
		{
			this.player = Player.main;
		}
		if (!this.toteManager)
		{
			Tote tote = Object.FindObjectOfType<Tote>();
			if (tote)
			{
				this.toteManager = tote.gameObject;
			}
		}
		if (!this.levelUpManager)
		{
			this.levelUpManager = Object.FindObjectOfType<LevelUpManager>();
		}
		if (!this.runTypeManager)
		{
			this.runTypeManager = Object.FindObjectOfType<RunTypeManager>();
		}
		if (!this.dungeonSpawner)
		{
			this.dungeonSpawner = Object.FindObjectOfType<DungeonSpawner>();
		}
		if (!this.curseManager)
		{
			this.curseManager = Object.FindObjectOfType<CurseManager>().gameObject;
		}
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x00091F99 File Offset: 0x00090199
	public IEnumerator Save(DungeonEvent dungeonEvent, string text = "Game Saved")
	{
		if (this.isSavingOrLoading)
		{
			yield break;
		}
		MetaProgressSaveManager.main.SaveAll();
		SaveManager.SaveImage(this.screenshotCamera, Singleton.Instance.saveNumber);
		GameFlowManager gameFlowManager = GameFlowManager.main;
		if (gameFlowManager)
		{
			gameFlowManager.DoAllSavedDestroys();
		}
		Debug.Log("Started saving options!");
		Application.persistentDataPath + "/";
		if (!Singleton.Instance.storyMode)
		{
			this.settings.path = string.Format("{0}/bphRun{1}.sav", Application.persistentDataPath, Singleton.Instance.saveNumber);
			ES3.DeleteFile(this.settings.path);
		}
		else
		{
			this.settings.path = string.Format("{0}/bphStoryModeRun{1}.sav", Application.persistentDataPath, Singleton.Instance.storyModeSlot);
			ES3.DeleteFile(this.settings.path);
		}
		this.gameManager.StartCoroutine(this.gameManager.SaveCheck());
		this.isSavingOrLoading = true;
		this.isUnstableState = true;
		Tote tote = Object.FindObjectOfType<Tote>();
		if (tote)
		{
			tote.RemoveToteUI();
		}
		CR8Manager cr8Manager = Object.FindObjectOfType<CR8Manager>();
		if (cr8Manager)
		{
			cr8Manager.RemoveAllEnergies();
		}
		ItemPouch.CloseAllPouches();
		ItemPouch.SetAllItemsToPouchParent();
		DungeonEvent.RemoveAllParticlesInDungeonEvents();
		yield return new WaitForSeconds(0.5f);
		ValueChanger.ResetAllValueChangesForSaving();
		ValueChangerEx.ResetAllValueChangesForSaving();
		GameFlowManager.main.DoAllSavedDestroys();
		this.MakeAssociations();
		this.playerStatTracker.GetComponent<PlayerStatTracking>().gamesSavedandLoaded.Add("Saved " + DateTime.Now.ToString());
		ES3.Save<GameObject>("dungeon", this.dungeonRooms, this.settings);
		ES3.Save<GameObject>("dungeonPlayer", this.dungeonPlayer, this.settings);
		ES3.Save<GameObject>("backpack", this.backpack, this.settings);
		ES3.Save<GameObject>("curseManager", this.curseManager, this.settings);
		ES3.Save<GameObject>("playerStatTracker", this.playerStatTracker, this.settings);
		if (this.runTypeManager && this.runTypeManager.missions)
		{
			ES3.Save<Missions>("missionType", this.runTypeManager.missions, this.settings);
			ES3.Save<List<RunType.RunProperty>>("runTypeProperties", this.runTypeManager.runProperties, this.settings);
			ES3.Save<string>("saveRunObjectName", this.runTypeManager.missions.name, this.settings);
			ES3.Save<string>("saveRunName", this.runTypeManager.missions.runTypeLanguageKey, this.settings);
			ES3.Save<int>("saveRunNumber", this.runTypeManager.missions.runTypeNumber, this.settings);
			Debug.Log("Saved mission type: " + this.runTypeManager.missions.name);
		}
		else if (this.runTypeManager && this.runTypeManager.runType)
		{
			ES3.Save<RunType>("runType", this.runTypeManager.runType, this.settings);
			ES3.Save<List<RunType.RunProperty>>("runTypeProperties", this.runTypeManager.runProperties, this.settings);
			ES3.Save<string>("saveRunName", this.runTypeManager.runType.runTypeLanguageKey, this.settings);
			Debug.Log("Saved run type: " + this.runTypeManager.runType.name);
		}
		else
		{
			Debug.Log("Couldn't save run type");
		}
		string text2 = Player.main.characterName.ToString();
		if (text2 == "CR8")
		{
			text2 = "CR-8";
		}
		TutorialManager tutorialManager = Object.FindObjectOfType<TutorialManager>();
		if (tutorialManager && tutorialManager.playType == TutorialManager.PlayType.testing)
		{
			ES3.Save<bool>("debugMode", true, this.settings);
		}
		else
		{
			ES3.Save<bool>("debugMode", false, this.settings);
		}
		ES3.Save<string>("saveCharName", text2, this.settings);
		ES3.Save<int>("floor", this.gameManager.floor, this.settings);
		ES3.Save<int>("zoneNumber", this.gameManager.zoneNumber, this.settings);
		ES3.Save<string>("zone", this.gameManager.dungeonLevel.zone.ToString(), this.settings);
		ES3.Save<DungeonLevel.Floor>("dungeonLevelFloor", this.gameManager.dungeonLevel.currentFloor, this.settings);
		int num = 0;
		foreach (GameObject gameObject in this.levelUpManager.chosenBlockPrefabs)
		{
			ES3.Save<string>("spaces" + num.ToString(), gameObject.name, this.settings);
			num++;
		}
		ES3.Save<int>("levelUpSpacesSaved", this.levelUpManager.spacesSaved, this.settings);
		ES3.Save<int>("cursesSkipped", this.gameManager.cursesSkipped, this.settings);
		ES3.Save<int>("gold", this.gameManager.goldAmount, this.settings);
		ES3.Save<int>("maxHealth", this.player.stats.maxHealth, this.settings);
		ES3.Save<int>("health", this.player.stats.health, this.settings);
		ES3.Save<int>("experience", this.player.experience, this.settings);
		ES3.Save<int>("level", this.player.level, this.settings);
		ES3.Save<float>("uncommonLuck", this.player.uncommonLuck, this.settings);
		ES3.Save<float>("rareLuck", this.player.rareLuck, this.settings);
		ES3.Save<float>("legendaryLuck", this.player.legendaryLuck, this.settings);
		ES3.Save<float>("uncommonLuckFromItems", this.player.uncommonLuckFromItems, this.settings);
		ES3.Save<float>("rareLuckFromItems", this.player.rareLuckFromItems, this.settings);
		ES3.Save<float>("legendaryLuckFromItems", this.player.legendaryLuckFromItems, this.settings);
		ES3.Save<bool>("gameInProgress", true, this.settings);
		foreach (StatusEffect statusEffect in this.player.stats.GetStatusEffects())
		{
			ES3.Save<int>("statusEffect_" + statusEffect.type.ToString(), statusEffect.value, this.settings);
		}
		ES3.Save<bool>("analyticsSent", Singleton.Instance.analyticsSent, this.settings);
		AnalyticsManager analyticsManager = Object.FindObjectOfType<AnalyticsManager>();
		if (analyticsManager)
		{
			ES3.Save<AnalyticsManager.ParameterObject>("analyticsData", analyticsManager.parameterObject, this.settings);
		}
		ES3.Save<List<DungeonSpawner.DungeonProperty>>("dungeonSpawnerProperties", Object.FindObjectOfType<DungeonSpawner>().dungeonProperties, this.settings);
		ES3.Save<bool>("ironMan", Singleton.Instance.ironMan, this.settings);
		ES3.Save<int>("characterNumber", Singleton.Instance.characterNumber, this.settings);
		ES3.Save<int>("costumeNumber", Singleton.Instance.costumeNumber, this.settings);
		ES3.Save<Random.State>("randomState", Random.state, this.settings);
		if (tote && this.player && this.player.characterName == Character.CharacterName.Tote)
		{
			tote.SetupToteUI();
		}
		ES3.Save<DateTime>("lastSaveDate", DateTime.Now, this.settings);
		yield return new WaitForEndOfFrame();
		this.isUnstableState = false;
		yield return new WaitUntil(() => !ConsoleWrapper.Instance.SaveInProgress);
		this.isSavingOrLoading = false;
		this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm41"));
		DungeonEvent.BuildParticlesInDungeonEvents();
		ItemPouch.SetAllItemsToItemsParent();
		yield break;
	}

	// Token: 0x06000EA7 RID: 3751 RVA: 0x00091FA8 File Offset: 0x000901A8
	public void LoadMission(int num)
	{
		this.UpdateSettingsPath(num);
		if (!this.runTypeManager)
		{
			Debug.Log("Couldn't find runtype manager");
			return;
		}
		if (ES3.KeyExists("missionType", this.settings))
		{
			this.runTypeManager.missions = ES3.Load<Missions>("missionType", this.runTypeManager.standardMissions, this.settings);
			this.runTypeManager.runProperties = ES3.Load<List<RunType.RunProperty>>("runTypeProperties", new List<RunType.RunProperty>(), this.settings);
			Singleton.Instance.mission = this.runTypeManager.missions;
			Debug.Log("Loaded mission: " + this.runTypeManager.missions.name + " from saved mission object");
			return;
		}
		if (ES3.KeyExists("saveRunObjectName", this.settings))
		{
			string text = ES3.Load<string>("saveRunObjectName", "empty", this.settings);
			Missions missionFromName = this.missionMaster.GetMissionFromName(text);
			if (missionFromName)
			{
				this.runTypeManager.missions = missionFromName;
				this.runTypeManager.runProperties = ES3.Load<List<RunType.RunProperty>>("runTypeProperties", new List<RunType.RunProperty>(), this.settings);
				Singleton.Instance.mission = this.runTypeManager.missions;
				Debug.Log("Loaded mission: " + this.runTypeManager.missions.name + " from object name");
				return;
			}
			Debug.Log("Key exists, but couldn't find mission from object name of " + text);
		}
		if (Singleton.Instance.storyMode && ES3.KeyExists("saveRunName", this.settings) && ES3.KeyExists("saveRunNumber", this.settings))
		{
			Debug.Log("A: " + this.settings.path);
			string text2 = ES3.Load<string>("saveRunName", "empty", this.settings);
			string text3 = ES3.Load<string>("saveRunNumber", "empty", this.settings);
			Debug.Log("Loading save " + text2 + " with id " + text3);
			int num2 = int.Parse(text3);
			Missions missionFromLanguageKeyAndNum = this.missionMaster.GetMissionFromLanguageKeyAndNum(text2, num2);
			if (missionFromLanguageKeyAndNum)
			{
				this.runTypeManager.missions = missionFromLanguageKeyAndNum;
				this.runTypeManager.runProperties = ES3.Load<List<RunType.RunProperty>>("runTypeProperties", new List<RunType.RunProperty>(), this.settings);
				Singleton.Instance.runType = this.runTypeManager.runType;
				Debug.Log(string.Concat(new string[]
				{
					"Loaded mission: ",
					this.runTypeManager.missions.name,
					" from language key of ",
					text2,
					" and num of ",
					num2.ToString()
				}));
				return;
			}
		}
		if (Singleton.Instance.storyMode && ES3.KeyExists("runTypeProperties", this.settings))
		{
			this.runTypeManager.runProperties = ES3.Load<List<RunType.RunProperty>>("runTypeProperties", new List<RunType.RunProperty>(), this.settings);
			Missions missionFromListOfProperties = this.missionMaster.GetMissionFromListOfProperties(this.runTypeManager.runProperties);
			if (missionFromListOfProperties)
			{
				this.runTypeManager.missions = missionFromListOfProperties;
				Singleton.Instance.mission = this.runTypeManager.missions;
				Debug.Log("Loaded mission: " + this.runTypeManager.missions.name + " from list of properties");
				return;
			}
			Debug.Log("Key exists for properties and we are in story mode, but couldn't find mission from list of properties");
		}
		if (ES3.KeyExists("runType", this.settings))
		{
			this.runTypeManager.runType = ES3.Load<RunType>("runType", this.runTypeManager.standardRunType, this.settings);
			this.runTypeManager.runProperties = ES3.Load<List<RunType.RunProperty>>("runTypeProperties", new List<RunType.RunProperty>(), this.settings);
			Singleton.Instance.runType = this.runTypeManager.runType;
			Debug.Log("Loaded run type: " + this.runTypeManager.runType.name);
			return;
		}
		if (Singleton.Instance.storyMode)
		{
			Singleton.Instance.mission = this.runTypeManager.standardMissions;
			this.runTypeManager.missions = this.runTypeManager.standardMissions;
		}
		Debug.Log("Couldn't load run type or mission");
	}

	// Token: 0x06000EA8 RID: 3752 RVA: 0x000923E8 File Offset: 0x000905E8
	private void UpdateSettingsPath(int num)
	{
		string text = Application.persistentDataPath + "/";
		if (!Singleton.Instance.storyMode)
		{
			this.settings.path = text + "bphRun" + num.ToString() + ".sav";
			Singleton.Instance.saveNumber = num;
			Debug.Log("Loaded regular data");
			return;
		}
		num = Singleton.Instance.storyModeSlot;
		this.settings.path = text + "bphStoryModeRun" + num.ToString() + ".sav";
		Debug.Log("Loaded story data");
	}

	// Token: 0x06000EA9 RID: 3753 RVA: 0x00092481 File Offset: 0x00090681
	public IEnumerator Load(int num)
	{
		yield return new WaitUntil(() => !ConsoleWrapper.Instance.SaveInProgress);
		MetaProgressSaveManager.main.Load();
		if (Singleton.Instance.runType)
		{
			Debug.Log("Started Load - " + Singleton.Instance.runType.name);
		}
		else
		{
			Debug.Log("Started Load - No Run Type");
		}
		this.UpdateSettingsPath(num);
		this.MakeAssociations();
		this.isSavingOrLoading = true;
		this.isUnstableState = true;
		foreach (object obj in this.dungeonRooms.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		foreach (object obj2 in this.backpack.transform)
		{
			Object.Destroy(((Transform)obj2).gameObject);
		}
		Debug.Log("Removed dungeon rooms and backpack children");
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		this.levelUpManager.spacesSaved = ES3.Load<int>("levelUpSpacesSaved", 0, this.settings);
		this.gameManager.cursesSkipped = ES3.Load<int>("cursesSkipped", 0, this.settings);
		this.gameManager.goldAmount = ES3.Load<int>("gold", 0, this.settings);
		this.gameManager.ChangeGold(0);
		this.player.stats.maxHealth = ES3.Load<int>("maxHealth", 40, this.settings);
		this.player.stats.maxHealthBeforeItems = this.player.stats.maxHealth;
		this.player.stats.health = ES3.Load<int>("health", 40, this.settings);
		this.player.experience = ES3.Load<int>("experience", 0, this.settings);
		this.player.level = ES3.Load<int>("level", 0, this.settings);
		this.player.uncommonLuck = ES3.Load<float>("uncommonLuck", 0f, this.settings);
		this.player.rareLuck = ES3.Load<float>("rareLuck", 0f, this.settings);
		this.player.legendaryLuck = ES3.Load<float>("legendaryLuck", 0f, this.settings);
		this.player.uncommonLuckFromItems = ES3.Load<float>("uncommonLuckFromItems", 0f, this.settings);
		this.player.rareLuckFromItems = ES3.Load<float>("rareLuckFromItems", 0f, this.settings);
		this.player.legendaryLuckFromItems = ES3.Load<float>("legendaryLuckFromItems", 0f, this.settings);
		Debug.Log("Loaded player stats");
		Singleton.Instance.analyticsSent = ES3.Load<bool>("analyticsSent", false, this.settings);
		AnalyticsManager analyticsManager = Object.FindObjectOfType<AnalyticsManager>();
		if (analyticsManager)
		{
			analyticsManager.parameterObject = ES3.Load<AnalyticsManager.ParameterObject>("analyticsData", new AnalyticsManager.ParameterObject(), this.settings);
		}
		DungeonSpawner.main.dungeonProperties = ES3.Load<List<DungeonSpawner.DungeonProperty>>("dungeonSpawnerProperties", new List<DungeonSpawner.DungeonProperty>(), this.settings);
		Singleton.Instance.ironMan = ES3.Load<bool>("ironMan", false, this.settings);
		Singleton.Instance.characterNumber = ES3.Load<int>("characterNumber", 0, this.settings);
		Singleton.Instance.costumeNumber = ES3.Load<int>("costumeNumber", 0, this.settings);
		Singleton.Instance.SetCharacterFromNumber();
		Debug.Log("Loaded character properties");
		this.player.ChooseCharacterSimpleStart();
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		SoundManager.main.ChangeAllVolume();
		Object.Destroy(GameObject.FindGameObjectWithTag("RoomsParent"));
		Object.Destroy(GameObject.FindGameObjectWithTag("Inventory"));
		Object.Destroy(GameObject.FindGameObjectWithTag("DungeonPlayer"));
		Object.Destroy(GameObject.FindGameObjectWithTag("PetMasters"));
		Debug.Log("Destroyed RoomsParent, Inventory, DungeonPlayer and PetMasters");
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		this.dungeonRooms = ES3.Load<GameObject>("dungeon", this.dungeonRooms, this.settings);
		if (!this.dungeonRooms)
		{
			Debug.LogError("Could not load dungeonRooms");
		}
		this.backpack = ES3.Load<GameObject>("backpack", this.backpack, this.settings);
		if (!this.backpack)
		{
			Debug.LogError("Could not load backpack");
		}
		this.dungeonPlayer = ES3.Load<GameObject>("dungeonPlayer", this.dungeonPlayer, this.settings);
		if (!this.dungeonPlayer)
		{
			Debug.LogError("Could not load dungeonPlayer");
		}
		this.curseManager = ES3.Load<GameObject>("curseManager", this.curseManager, this.settings);
		if (!this.curseManager)
		{
			Debug.LogError("Could not load curseManager");
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag("DungeonParent");
		if (gameObject == null)
		{
			Debug.LogError("DungeonParent not found");
		}
		this.dungeonRooms.transform.SetParent(gameObject.transform, false);
		this.dungeonPlayer.transform.SetParent(gameObject.transform, false);
		ES3.Load<GameObject>("playerStatTracker", this.playerStatTracker, this.settings);
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		this.LoadMission(num);
		this.runTypeManager.SetRunText();
		this.playerStatTracker.GetComponent<PlayerStatTracking>().gamesSavedandLoaded.Add("Loaded " + DateTime.Now.ToString());
		this.gameManager.floor = ES3.Load<int>("floor", 0, this.settings);
		this.gameManager.zoneNumber = ES3.Load<int>("zoneNumber", 0, this.settings);
		string text = "dungeon";
		try
		{
			text = ES3.Load<string>("zone", this.settings);
		}
		catch
		{
		}
		DungeonLevel.Zone zone = (DungeonLevel.Zone)Enum.Parse(typeof(DungeonLevel.Zone), text);
		this.levelUpManager.chosenBlockPrefabs = new List<GameObject>();
		for (int i = 0; i < 10; i++)
		{
			string text2 = ES3.Load<string>("spaces" + i.ToString(), "empty", this.settings);
			if (text2 == "empty")
			{
				break;
			}
			foreach (GameObject gameObject2 in this.levelUpManager.gridBlockPrefabs)
			{
				if (gameObject2.name == text2)
				{
					this.levelUpManager.chosenBlockPrefabs.Add(gameObject2);
				}
			}
		}
		Random.state = ES3.Load<Random.State>("randomState", Random.state, this.settings);
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		this.gameManager.inventoryTransform = this.backpack.transform;
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		yield return new WaitForSeconds(0.6f);
		Object.FindObjectsOfType<DungeonEvent>();
		DungeonPlayer dungeonPlayer = DungeonPlayer.main;
		dungeonPlayer.FindReachableEvents();
		dungeonPlayer.footStepPrefab = this.footstepPrefab;
		Animator animator = this.dungeonPlayer.GetComponent<Animator>();
		if (!animator)
		{
			animator = this.dungeonPlayer.AddComponent<Animator>();
		}
		if (animator)
		{
			animator.runtimeAnimatorController = this.dungeonPlayerController;
		}
		if (this.player)
		{
			this.player.ChoseCharacter();
		}
		this.gameManager.SetLevel(zone);
		this.gameManager.dungeonLevel.currentFloor = ES3.Load<DungeonLevel.Floor>("dungeonLevelFloor", DungeonLevel.Floor.first, this.settings);
		Debug.Log("Loaded dungeon level floor: " + this.gameManager.dungeonLevel.currentFloor.ToString());
		this.levelUpManager.inventoryParent = this.backpack.transform;
		this.levelUpManager.gridParent = GameObject.FindGameObjectWithTag("GridParent").transform;
		GameManager.main.mainGridParent = GameObject.FindGameObjectWithTag("GridParent").transform;
		Transform transform = Object.FindObjectOfType<ModularBackpack>().transform;
		this.levelUpManager.topBackpackSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
		this.levelUpManager.leftBackpackSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
		this.levelUpManager.rightBackpackSprite = transform.GetChild(2).GetComponent<SpriteRenderer>();
		this.levelUpManager.bottomBackpackSprite = transform.GetChild(3).GetComponent<SpriteRenderer>();
		this.levelUpManager.backgroundBackpackSprite = transform.GetChild(4).GetComponent<SpriteRenderer>();
		yield return new WaitForEndOfFrame();
		DungeonSpawner dungeonSpawner = Object.FindObjectOfType<DungeonSpawner>();
		if (this.dungeonRooms.transform.childCount == 0)
		{
			DoomRoomBlock[] array = Object.FindObjectsOfType<DoomRoomBlock>();
			for (int j = 0; j < array.Length; j++)
			{
				Object.Destroy(array[j].gameObject);
			}
			GameObject gameObject3 = Object.Instantiate<GameObject>(this.dungeonRoomsPrefab, Vector3.zero, Quaternion.identity, this.dungeonRooms.transform.parent);
			gameObject3.transform.localPosition = new Vector3(0f, 0f, -2f);
			Object.Destroy(this.dungeonRooms.gameObject);
			this.dungeonRooms = gameObject3;
		}
		dungeonSpawner.roomsParent = this.dungeonRooms.transform.GetChild(0);
		dungeonSpawner.player = this.dungeonPlayer;
		Object.FindObjectOfType<BackgroundController>().UpdateAllSprites();
		this.player.stats.SetHealth(this.player.stats.health);
		GameFlowManager.main.CheckConstants();
		Tote tote = Object.FindObjectOfType<Tote>();
		if (tote)
		{
			tote.SpawnTote();
			tote.AssignAllCardsAfterLoad();
		}
		PetMaster.AddAllToPetInventorys();
		ItemPouch.SetAllItemsToItemsParent();
		if (PocketManager.main)
		{
			PocketManager.main.DeterminePockets();
		}
		GridObject.SnapAllToGrid();
		Object.FindObjectOfType<ItemMovementManager>().SetAllMaterials();
		if (ES3.Load<bool>("debugMode", false, this.settings))
		{
			TutorialManager tutorialManager = Object.FindObjectOfType<TutorialManager>();
			if (tutorialManager)
			{
				tutorialManager.playType = TutorialManager.PlayType.testing;
			}
		}
		foreach (object obj3 in Enum.GetValues(typeof(StatusEffect.Type)))
		{
			StatusEffect.Type type = (StatusEffect.Type)obj3;
			int num2 = ES3.Load<int>("statusEffect_" + type.ToString(), -1, this.settings);
			if (num2 != -1)
			{
				this.player.stats.AddStatusEffect(type, (float)num2, Item2.Effect.MathematicalType.summative);
			}
		}
		Debug.Log("Finished Load - " + Singleton.Instance.runType.name);
		this.isSavingOrLoading = false;
		this.isUnstableState = false;
		this.gameManager.ShowInventory();
		GameManager.main.ShowInventoryInstant();
		this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm40"));
		this.gameManager.itemsParent = GameObject.FindGameObjectWithTag("ItemParent").transform;
		this.gameManager.ChangeCurse(0);
		yield return new WaitForSeconds(0.25f);
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return null;
		yield return null;
		yield return null;
		this.gridFollowInv.FindFollowFromTag();
		yield return null;
		yield return null;
		yield return null;
		yield return null;
		yield return null;
		ItemMovement.AllGetGridObject();
		GridSquare.AllGetGridObject();
		ItemMovement.AddAllToGrid();
		GridSquare.AddAllToGrid();
		yield return new WaitForEndOfFrame();
		ConnectionManager.main.FindManaNetworks();
		GameFlowManager.main.CheckConstants();
		yield break;
	}

	// Token: 0x06000EAA RID: 3754 RVA: 0x00092498 File Offset: 0x00090698
	public static void DeleteSave(int num)
	{
		Debug.Log("Deleting Save " + num.ToString());
		string text = Application.persistentDataPath + "/";
		string text2 = text + "bphRun" + num.ToString() + ".sav";
		if (ES3.FileExists(text2))
		{
			ES3.DeleteFile(text2);
		}
		string text3 = text + "runScreenshot" + num.ToString() + ".png";
		if (File.Exists(text3))
		{
			File.Delete(text3);
		}
	}

	// Token: 0x06000EAB RID: 3755 RVA: 0x00092518 File Offset: 0x00090718
	public static void DeleteStoryModeSave(int num)
	{
		string text = Application.persistentDataPath + "/";
		if (ES3.FileExists(text + "bphStoryModeRun" + num.ToString() + ".sav"))
		{
			ES3.DeleteFile(text + "bphStoryModeRun" + num.ToString() + ".sav");
		}
		if (ES3.FileExists(text + "bphStoryModeMetaData" + num.ToString() + ".sav"))
		{
			ES3.DeleteFile(text + "bphStoryModeMetaData" + num.ToString() + ".sav");
		}
		if (ES3.FileExists(text + "bphStoryModeOverworld" + num.ToString() + ".sav"))
		{
			ES3.DeleteFile(text + "bphStoryModeOverworld" + num.ToString() + ".sav");
		}
		File.Delete(text + "bphStoryModeScreenshot" + num.ToString() + ".png");
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x000925FF File Offset: 0x000907FF
	public IEnumerator SaveImage(int num)
	{
		yield return null;
		SaveManager.SaveImage(this.screenshotCamera, num);
		yield break;
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x00092618 File Offset: 0x00090818
	public static void SaveImage(Camera camera, int num)
	{
		camera.gameObject.SetActive(true);
		int num2 = 1280;
		int num3 = 720;
		RenderTexture renderTexture = new RenderTexture(num2, num3, 24);
		camera.targetTexture = renderTexture;
		Texture2D texture2D = new Texture2D(num2, num3, TextureFormat.RGB24, false);
		camera.Render();
		RenderTexture.active = renderTexture;
		Debug.unityLogger.Log("read pixels");
		texture2D.ReadPixels(new Rect(0f, 0f, (float)num2, (float)num3), 0, 0);
		camera.targetTexture = null;
		RenderTexture.active = null;
		Object.Destroy(renderTexture);
		Debug.Log("Encoding");
		byte[] array = texture2D.EncodeToPNG();
		if (!Singleton.Instance.storyMode)
		{
			string text = Application.persistentDataPath + "/runScreenshot" + num.ToString() + ".png";
			Debug.Log("write all bytese for PC");
			File.WriteAllBytes(text, array);
			Debug.Log("Took screenshot to at " + text);
		}
		else
		{
			string text2 = Application.persistentDataPath + "/bphStoryModeScreenshot" + Singleton.Instance.storyModeSlot.ToString() + ".png";
			Debug.Log("write all bytese for PC");
			File.WriteAllBytes(text2, array);
			Debug.Log("Took screenshot to at " + text2);
		}
		camera.gameObject.SetActive(false);
	}

	// Token: 0x06000EAE RID: 3758 RVA: 0x00092754 File Offset: 0x00090954
	public static Sprite LoadImage(string path)
	{
		string text = Application.persistentDataPath + "/" + path;
		if (!File.Exists(text))
		{
			Debug.Log("Can't find image!");
			return null;
		}
		byte[] array = File.ReadAllBytes(text);
		Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGB24, false);
		texture2D.LoadImage(array);
		return Sprite.Create(texture2D, new Rect(100f, 50f, (float)(texture2D.width - 200), (float)(texture2D.height - 100)), Vector2.zero);
	}

	// Token: 0x06000EAF RID: 3759 RVA: 0x000927D0 File Offset: 0x000909D0
	public static Sprite LoadImage(int num)
	{
		string text = Application.persistentDataPath + "/" + string.Format("runScreenshot{0}.png", num);
		if (!File.Exists(text))
		{
			Debug.Log("Can't find image!");
			return null;
		}
		byte[] array = File.ReadAllBytes(text);
		Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGB24, false);
		texture2D.LoadImage(array);
		return Sprite.Create(texture2D, new Rect(100f, 50f, (float)(texture2D.width - 200), (float)(texture2D.height - 100)), Vector2.zero);
	}

	// Token: 0x04000BCB RID: 3019
	public static SaveManager main;

	// Token: 0x04000BCC RID: 3020
	[Header("Save References ---------------------")]
	[SerializeField]
	private RunTypeSelector missionMaster;

	// Token: 0x04000BCD RID: 3021
	[SerializeField]
	private List<Object> objectsToSaveForES3;

	// Token: 0x04000BCE RID: 3022
	[Header("Save References ---------------------")]
	[SerializeField]
	public GameObject dungeonRoomsPrefab;

	// Token: 0x04000BCF RID: 3023
	[SerializeField]
	public Transform mousePreviewToCopy;

	// Token: 0x04000BD0 RID: 3024
	[SerializeField]
	public Transform relicParticles;

	// Token: 0x04000BD1 RID: 3025
	[SerializeField]
	public Transform curseParticles;

	// Token: 0x04000BD2 RID: 3026
	[SerializeField]
	public Transform blessingParticles;

	// Token: 0x04000BD3 RID: 3027
	[SerializeField]
	public RuntimeAnimatorController itemController;

	// Token: 0x04000BD4 RID: 3028
	[SerializeField]
	private RuntimeAnimatorController roomController;

	// Token: 0x04000BD5 RID: 3029
	[SerializeField]
	private RuntimeAnimatorController dungeonPlayerController;

	// Token: 0x04000BD6 RID: 3030
	[SerializeField]
	private GameObject footstepPrefab;

	// Token: 0x04000BD7 RID: 3031
	[Header("Things to Save ---------------------")]
	[SerializeField]
	private GameObject dungeonRooms;

	// Token: 0x04000BD8 RID: 3032
	[SerializeField]
	private GameObject curseManager;

	// Token: 0x04000BD9 RID: 3033
	[SerializeField]
	private GameObject dungeonPlayer;

	// Token: 0x04000BDA RID: 3034
	[SerializeField]
	private DungeonSpawner dungeonSpawner;

	// Token: 0x04000BDB RID: 3035
	[SerializeField]
	private GameObject backpack;

	// Token: 0x04000BDC RID: 3036
	[SerializeField]
	private GameObject playerStatTracker;

	// Token: 0x04000BDD RID: 3037
	[SerializeField]
	private GameObject toteManager;

	// Token: 0x04000BDE RID: 3038
	[SerializeField]
	private RunTypeManager runTypeManager;

	// Token: 0x04000BDF RID: 3039
	public bool isSavingOrLoading;

	// Token: 0x04000BE0 RID: 3040
	public bool isUnstableState;

	// Token: 0x04000BE1 RID: 3041
	private GameManager gameManager;

	// Token: 0x04000BE2 RID: 3042
	private LevelUpManager levelUpManager;

	// Token: 0x04000BE3 RID: 3043
	private Player player;

	// Token: 0x04000BE4 RID: 3044
	private ES3Settings _settings;

	// Token: 0x04000BE5 RID: 3045
	[SerializeField]
	private Follow gridFollowInv;

	// Token: 0x04000BE6 RID: 3046
	[SerializeField]
	private Camera screenshotCamera;
}
