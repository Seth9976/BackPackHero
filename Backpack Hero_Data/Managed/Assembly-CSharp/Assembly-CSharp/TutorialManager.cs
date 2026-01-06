using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x02000127 RID: 295
public class TutorialManager : MonoBehaviour
{
	// Token: 0x06000B0D RID: 2829 RVA: 0x0006FF19 File Offset: 0x0006E119
	private void Awake()
	{
		if (TutorialManager.main == null)
		{
			TutorialManager.main = this;
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x0006FF3A File Offset: 0x0006E13A
	private void OnDestroy()
	{
		if (TutorialManager.main == this)
		{
			TutorialManager.main = null;
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0006FF4F File Offset: 0x0006E14F
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

	// Token: 0x06000B10 RID: 2832 RVA: 0x0006FF6C File Offset: 0x0006E16C
	public void ConsiderTutorial(string name)
	{
		name = name.Trim();
		if (this.completedTutorials.Contains(name))
		{
			return;
		}
		TutorialPopUpManager.main.DisplayTutorial(name);
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x0006FF90 File Offset: 0x0006E190
	private void Start()
	{
		this.completedTutorials = Object.FindObjectOfType<MetaProgressSaveManager>().LoadCompletedTutorials();
		this.gameManager = GameManager.main;
		this.gameFlowManager = GameFlowManager.main;
		if (Singleton.Instance.doTutorial)
		{
			if (Singleton.Instance.character == Character.CharacterName.Purse)
			{
				this.playType = TutorialManager.PlayType.tutorial;
			}
			else if (Singleton.Instance.character == Character.CharacterName.CR8)
			{
				this.playType = TutorialManager.PlayType.cr8Tutorial;
			}
			else
			{
				this.playType = TutorialManager.PlayType.release;
			}
		}
		else if (Singleton.Instance.loadSave)
		{
			this.playType = TutorialManager.PlayType.loadGame;
		}
		else if (Singleton.Instance.completedTutorial)
		{
			this.playType = TutorialManager.PlayType.release;
		}
		else
		{
			Singleton.Instance.runType = this.testingRunType;
			Singleton.Instance.mission = this.testingMission;
			Singleton.Instance.character = this.characterName;
			Singleton.Instance.SetNumberFromCharacter(this.characterName);
		}
		base.StartCoroutine(this.StartGame());
	}

	// Token: 0x06000B12 RID: 2834 RVA: 0x0007007E File Offset: 0x0006E27E
	public void HideClickMe()
	{
		this.clickMe.gameObject.SetActive(false);
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x00070091 File Offset: 0x0006E291
	public void ShowClickMe(Vector3 position)
	{
		this.clickMe.gameObject.SetActive(true);
		this.clickMe.transform.position = position;
	}

	// Token: 0x06000B14 RID: 2836 RVA: 0x000700B5 File Offset: 0x0006E2B5
	private IEnumerator StartGame()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		if (TwitchManager.isRunningPolls())
		{
			TwitchManager.Instance.pollManager.Init();
		}
		Singleton.Instance.showingOptions = false;
		Item2[] array = Object.FindObjectsOfType<Item2>();
		this.gameManager = GameManager.main;
		RunTypeManager runTypeManager = Object.FindObjectOfType<RunTypeManager>();
		runTypeManager.AssignRunTypeFromSingleton();
		runTypeManager.StartNewRun();
		if (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.doTutorial) && !Singleton.Instance.loadSave)
		{
			if (Singleton.Instance.character == Character.CharacterName.Purse)
			{
				this.tutorialSequence = TutorialManager.TutorialSequence.openingMessage;
				this.playType = TutorialManager.PlayType.tutorial;
			}
			else if (Singleton.Instance.character == Character.CharacterName.CR8)
			{
				this.playType = TutorialManager.PlayType.cr8Tutorial;
			}
			else if (Singleton.Instance.character == Character.CharacterName.Tote)
			{
				this.playType = TutorialManager.PlayType.totetorial;
			}
		}
		if (this.playType == TutorialManager.PlayType.tutorial && Singleton.Instance.character == Character.CharacterName.Tote)
		{
			this.playType = TutorialManager.PlayType.totetorial;
		}
		if (this.playType == TutorialManager.PlayType.tutorial)
		{
			MetaProgressSaveManager.main.ResetLastRun();
			if (Singleton.Instance.storyMode && !MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.backpackCollected))
			{
				Object.Instantiate<GameObject>(this.introCinematic, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("UI Canvas").transform);
				this.gameManager.MoveInventoryUpTop();
				RunTypeManager.Instance.CreateNPC(RunType.RunProperty.Type.doTutorial);
				SoundManager.main.StartCoroutine(SoundManager.main.PlaySongSudden2("story_mode_intro", 0f, false, 0f));
			}
			else
			{
				this.gameManager.ShowMap(true);
			}
			this.gameManager.dungeonLevel.currentFloor = DungeonLevel.Floor.intro;
			Item2[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				Object.Destroy(array2[i].gameObject);
			}
			if (!Singleton.Instance.storyMode)
			{
				this.tutorialEvent.SetActive(true);
			}
			this.tutorialRooms.SetActive(true);
			Object.Destroy(this.testingItems);
			Object.Destroy(this.testingRooms);
			Object.Destroy(this.cr8TutorialRooms);
			Object.Destroy(this.toteTutorialRooms);
			Object.Destroy(this.matthewRooms);
			DungeonRoom[] array3 = Object.FindObjectsOfType<DungeonRoom>();
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i].ChooseSprite();
			}
			Object.FindObjectOfType<DungeonSpawner>().SetAllEncounters();
			this.tutorialSequence = TutorialManager.TutorialSequence.openingMessage;
			Player.main.ChoseCharacter();
			Object.FindObjectOfType<Tote>().SpawnTote();
		}
		else if (this.playType == TutorialManager.PlayType.totetorial)
		{
			MetaProgressSaveManager.main.ResetLastRun();
			this.gameManager.dungeonLevel.currentFloor = DungeonLevel.Floor.intro;
			Item2[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				Object.Destroy(array2[i].gameObject);
			}
			this.toteTutorialRooms.SetActive(true);
			Object.Destroy(this.testingItems);
			Object.Destroy(this.tutorialRooms);
			Object.Destroy(this.testingRooms);
			Object.Destroy(this.matthewRooms);
			Object.Destroy(this.cr8TutorialRooms);
			DungeonRoom[] array3 = Object.FindObjectsOfType<DungeonRoom>();
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i].ChooseSprite();
			}
			this.tutorialSequence = TutorialManager.TutorialSequence.toteStart;
			Player player = Player.main;
			player.ChoseCharacter();
			player.SetBagSize();
			Object.FindObjectOfType<Tote>().SpawnTote();
		}
		else if (this.playType == TutorialManager.PlayType.cr8Tutorial)
		{
			MetaProgressSaveManager.main.ResetLastRun();
			this.gameManager.dungeonLevel.currentFloor = DungeonLevel.Floor.intro;
			Item2[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				Object.Destroy(array2[i].gameObject);
			}
			this.cr8TutorialRooms.SetActive(true);
			Object.Destroy(this.testingItems);
			Object.Destroy(this.tutorialRooms);
			Object.Destroy(this.testingRooms);
			Object.Destroy(this.matthewRooms);
			Object.Destroy(this.toteTutorialRooms);
			DungeonRoom[] array3 = Object.FindObjectsOfType<DungeonRoom>();
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i].ChooseSprite();
			}
			this.tutorialSequence = TutorialManager.TutorialSequence.cr8start;
			Player player2 = Player.main;
			Object.Instantiate<GameObject>(this.cr8CorePrefab, Vector3.zero, Quaternion.identity);
			player2.ChoseCharacter();
			player2.SetBagSize();
			Object.FindObjectOfType<Tote>().SpawnTote();
		}
		else if (this.playType == TutorialManager.PlayType.testing)
		{
			MetaProgressSaveManager.main.ResetLastRun();
			GameManager.main.dungeonLevel.currentFloor = DungeonLevel.Floor.intro;
			Object.Destroy(this.tutorialRooms);
			Object.Destroy(this.tutorialEvent);
			Object.Destroy(this.matthewRooms);
			Object.Destroy(this.cr8TutorialRooms);
			Object.Destroy(this.toteTutorialRooms);
			if (!this.testingRunType)
			{
				this.testingRunType = this.standardRunType;
			}
			Singleton.Instance.runType = this.testingRunType;
			Singleton.Instance.character = this.characterName;
			Singleton.Instance.SetNumberFromCharacter(this.characterName);
			DungeonSpawner dungeonSpawner = Object.FindObjectOfType<DungeonSpawner>();
			if (!dungeonSpawner.ConsiderSpecialRooms())
			{
				this.testingRooms.SetActive(true);
			}
			else
			{
				Object.Destroy(this.testingRooms);
			}
			DungeonRoom[] array3 = Object.FindObjectsOfType<DungeonRoom>();
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i].ChooseSprite();
			}
			yield return dungeonSpawner.SetAllRoomSpritesAndEncounters();
			this.tutorialSequence = TutorialManager.TutorialSequence.trulyDone;
			this.testingItems.SetActive(true);
			if (this.testingItems)
			{
				StackableItem componentInChildren = this.testingItems.GetComponentInChildren<StackableItem>();
				if (componentInChildren)
				{
					componentInChildren.amount = this.goldAmount;
				}
			}
			if (this.expandInventory)
			{
				Transform parent = Object.FindObjectOfType<GridSquare>().transform.parent;
				Object.Instantiate<GameObject>(this.gridPrefab, Vector3.zero, Quaternion.identity, parent).transform.localPosition = new Vector3(-2f, 2f, 0f);
				Object.Instantiate<GameObject>(this.gridPrefab, Vector3.zero, Quaternion.identity, parent).transform.localPosition = new Vector3(-1f, 2f, 0f);
				Object.Instantiate<GameObject>(this.gridPrefab, Vector3.zero, Quaternion.identity, parent).transform.localPosition = new Vector3(0f, 2f, 0f);
				Object.Instantiate<GameObject>(this.gridPrefab, Vector3.zero, Quaternion.identity, parent).transform.localPosition = new Vector3(1f, 2f, 0f);
				Object.Instantiate<GameObject>(this.gridPrefab, Vector3.zero, Quaternion.identity, parent).transform.localPosition = new Vector3(2f, 2f, 0f);
				Object.Instantiate<GameObject>(this.gridPrefab, Vector3.zero, Quaternion.identity, parent).transform.localPosition = new Vector3(-2f, 1f, 0f);
				Object.Instantiate<GameObject>(this.gridPrefab, Vector3.zero, Quaternion.identity, parent).transform.localPosition = new Vector3(-2f, 0f, 0f);
				Object.Instantiate<GameObject>(this.gridPrefab, Vector3.zero, Quaternion.identity, parent).transform.localPosition = new Vector3(-2f, -1f, 0f);
				Object.Instantiate<GameObject>(this.gridPrefab, Vector3.zero, Quaternion.identity, parent).transform.localPosition = new Vector3(-2f, -2f, 0f);
				Object.FindObjectOfType<LevelUpManager>().ResizeAllBackpacks();
			}
			Player player3 = Player.main;
			player3.ChoseCharacter();
			player3.SetBagSize();
			player3.SpawnObjects();
			Object.FindObjectOfType<Tote>().SpawnTote();
			this.gameManager.SetLevelFromBeginning();
			base.StartCoroutine(this.MoveCoreToCenterForTesting());
		}
		else if (this.playType == TutorialManager.PlayType.loadGame)
		{
			Object.Destroy(this.testingItems);
			this.tutorialSequence = TutorialManager.TutorialSequence.trulyDone;
			if (Singleton.Instance.loadSave)
			{
				this.gameManager.StartCoroutine(this.gameManager.ShowFadeOut());
				yield return new WaitForSeconds(1f);
				SaveManager saveManager = Object.FindObjectOfType<SaveManager>();
				saveManager.StartCoroutine(saveManager.Load(Singleton.Instance.saveNumber));
			}
		}
		else if (Singleton.Instance.storyMode)
		{
			MetaProgressSaveManager.main.ResetLastRun();
			if (!MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedBramble) || Singleton.Instance.IsStoryModeLevels())
			{
				GameManager.main.SetFirstLevel();
				this.gameManager.floor = 1;
				Item2[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					Object.Destroy(array2[i].gameObject);
				}
				Object.Destroy(this.testingItems);
				Object.Destroy(this.tutorialRooms);
				Object.Destroy(this.tutorialEvent);
				Object.Destroy(this.testingRooms);
				Object.Destroy(this.cr8TutorialRooms);
				Object.Destroy(this.toteTutorialRooms);
				this.gameManager.dungeonLevel.currentFloor = DungeonLevel.Floor.first;
				DungeonSpawner dungeonSpawner2 = Object.FindObjectOfType<DungeonSpawner>();
				dungeonSpawner2.StartCoroutine(dungeonSpawner2.SpawnChambers());
			}
			else
			{
				this.gameManager.dungeonLevel.currentFloor = DungeonLevel.Floor.intro;
				this.gameManager.floor = 0;
				Item2[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					Object.Destroy(array2[i].gameObject);
				}
				Object.Destroy(this.testingItems);
				Object.Destroy(this.tutorialRooms);
				Object.Destroy(this.tutorialEvent);
				Object.Destroy(this.testingRooms);
				Object.Destroy(this.cr8TutorialRooms);
				Object.Destroy(this.toteTutorialRooms);
				this.matthewRooms.SetActive(true);
				DungeonSpawner dungeonSpawner3 = Object.FindObjectOfType<DungeonSpawner>();
				if (!dungeonSpawner3.ConsiderSpecialRooms())
				{
					this.matthewRooms.SetActive(true);
				}
				yield return dungeonSpawner3.SetAllRoomSpritesAndEncounters();
				Object.FindObjectOfType<DungeonSpawner>().SpawnPlayer();
			}
			this.tutorialSequence = TutorialManager.TutorialSequence.trulyDone;
			this.gameManager.MoveAllItems();
			Player player4 = Player.main;
			player4.SpawnObjects();
			player4.ChoseCharacter();
			player4.SetBagSize();
			Object.FindObjectOfType<Tote>().SpawnTote();
			if (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.startFromMatt))
			{
				Object.Instantiate<GameObject>(this.matthew, Vector3.zero, Quaternion.identity, player4.transform.parent).transform.position = new Vector3(this.gameManager.spawnPosition.position.x - 1f, this.matthew.transform.position.y, -2.5f);
			}
			else if (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.classicMatthew))
			{
				Object.Instantiate<GameObject>(this.classicMatthew, Vector3.zero, Quaternion.identity, player4.transform.parent).transform.position = new Vector3(this.gameManager.spawnPosition.position.x - 1f, this.matthew.transform.position.y, -2.5f);
			}
		}
		else
		{
			MetaProgressSaveManager.main.ResetLastRun();
			this.gameManager.dungeonLevel.currentFloor = DungeonLevel.Floor.intro;
			try
			{
				ES3.Save<bool>("playedBefore", true, this.settings);
			}
			catch
			{
				try
				{
					Debug.Log("Save was corrupted - replacing");
					ES3.DeleteFile();
					ES3.Save<bool>("playedBefore", true, this.settings);
				}
				catch
				{
					Debug.Log("Save was corrupted - could not replace");
				}
			}
			Item2[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				Object.Destroy(array2[i].gameObject);
			}
			Object.Destroy(this.testingItems);
			Object.Destroy(this.tutorialRooms);
			Object.Destroy(this.tutorialEvent);
			Object.Destroy(this.testingRooms);
			Object.Destroy(this.cr8TutorialRooms);
			Object.Destroy(this.toteTutorialRooms);
			DungeonSpawner dungeonSpawner4 = Object.FindObjectOfType<DungeonSpawner>();
			if (!dungeonSpawner4.ConsiderSpecialRooms())
			{
				this.matthewRooms.SetActive(true);
			}
			yield return dungeonSpawner4.SetAllRoomSpritesAndEncounters();
			Object.FindObjectOfType<DungeonSpawner>().SpawnPlayer();
			this.tutorialSequence = TutorialManager.TutorialSequence.trulyDone;
			this.gameManager.MoveAllItems();
			Player player5 = Player.main;
			player5.SpawnObjects();
			player5.ChoseCharacter();
			player5.SetBagSize();
			Object.FindObjectOfType<Tote>().SpawnTote();
			if (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.startFromMatt))
			{
				Object.Instantiate<GameObject>(this.matthew, Vector3.zero, Quaternion.identity, player5.transform.parent).transform.position = new Vector3(this.gameManager.spawnPosition.position.x - 1f, this.matthew.transform.position.y, -2.5f);
			}
			else if (RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.classicMatthew))
			{
				Object.Instantiate<GameObject>(this.classicMatthew, Vector3.zero, Quaternion.identity, player5.transform.parent).transform.position = new Vector3(this.gameManager.spawnPosition.position.x - 1f, this.matthew.transform.position.y, -2.5f);
			}
			this.gameManager.SetLevelFromBeginning();
		}
		yield break;
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x000700C4 File Offset: 0x0006E2C4
	private IEnumerator MoveCoreToCenterForTesting()
	{
		yield return null;
		yield return new WaitForSeconds(0.1f);
		List<Item2> itemOfType = Item2.GetItemOfType(Item2.ItemType.Core, Item2.GetAllItemsOutsideGrid());
		if (itemOfType.Count > 0)
		{
			itemOfType[0].transform.localPosition = new Vector3(0f, -1f, 0f);
			itemOfType[0].GetComponent<ItemMovement>().AddToGrid(false);
		}
		yield break;
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x000700CC File Offset: 0x0006E2CC
	private void Update()
	{
		if (this.playType == TutorialManager.PlayType.testing)
		{
			Time.timeScale = this.timeScale;
		}
		if (this.playType != TutorialManager.PlayType.tutorial)
		{
			return;
		}
		if (this.tutorialStartEventMaster && !this.tutorialStartEventMaster.transform.GetChild(0).gameObject.activeInHierarchy && this.tutorialSequence == TutorialManager.TutorialSequence.done)
		{
			Object.Destroy(this.tutorialStartEventMaster);
		}
		if ((!this.tutorialStartEventMaster || !this.tutorialStartEventMaster.transform.GetChild(0).gameObject.activeInHierarchy) && this.tutorialSequence == TutorialManager.TutorialSequence.openingMessage)
		{
			DungeonEvent.FindDungeonEventOfType(DungeonEvent.DungeonEventType.Chest).gameObject.AddComponent<PulseImage>();
			this.tutorialSequence = TutorialManager.TutorialSequence.moveToChest;
			if (this.moveHereText)
			{
				this.moveHereText.SetActive(true);
				return;
			}
		}
		else
		{
			if (this.tutorialSequence == TutorialManager.TutorialSequence.moveToChest && this.gameManager.travelling)
			{
				if (this.moveHereText)
				{
					Object.Destroy(this.moveHereText);
				}
				this.HideClickMe();
				this.tutorialSequence = TutorialManager.TutorialSequence.organizeChest;
				return;
			}
			if (this.tutorialSequence == TutorialManager.TutorialSequence.moveToCombat && this.gameManager.travelling)
			{
				this.HideClickMe();
				return;
			}
			if (this.tutorialSequence == TutorialManager.TutorialSequence.openMap)
			{
				GameObject.FindGameObjectWithTag("MapButton").AddComponent<PulseImage>();
				this.tutorialSequence = TutorialManager.TutorialSequence.purseAfterOpenMap;
				return;
			}
			if (this.tutorialSequence == TutorialManager.TutorialSequence.moveToCombat && !this.gameManager.travelling)
			{
				DungeonEvent.FindDungeonEventOfType(DungeonEvent.DungeonEventType.Enemy).gameObject.AddComponent<PulseImage>();
				this.tutorialSequence = TutorialManager.TutorialSequence.done;
			}
		}
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x00070248 File Offset: 0x0006E448
	public void CombatStart()
	{
		if (this.tutorialSequence == TutorialManager.TutorialSequence.cr8start)
		{
			this.tutorialSequence = TutorialManager.TutorialSequence.cr8battle1;
			this.gameManager.StartCoroutine(this.gameManager.ShowPromptText(this.gameManager.tutorialText, 425f));
			this.gameManager.tutorialText.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("tutCR81");
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle1)
		{
			this.tutorialSequence = TutorialManager.TutorialSequence.cr8battle2;
			this.gameManager.StartCoroutine(this.gameManager.ShowPromptText(this.gameManager.tutorialText, 425f));
			this.gameManager.tutorialText.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("tutCR82");
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle2)
		{
			this.tutorialSequence = TutorialManager.TutorialSequence.cr8battle3;
			this.gameManager.StartCoroutine(this.gameManager.ShowPromptText(this.gameManager.tutorialText, 425f));
			this.gameManager.tutorialText.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("tutCR83");
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle3)
		{
			this.tutorialSequence = TutorialManager.TutorialSequence.cr8battle4;
			this.gameManager.StartCoroutine(this.gameManager.ShowPromptText(this.gameManager.tutorialText, 425f));
			this.gameManager.tutorialText.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("tutCR84");
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle4)
		{
			this.tutorialSequence = TutorialManager.TutorialSequence.cr8battle5;
			this.gameManager.StartCoroutine(this.gameManager.ShowPromptText(this.gameManager.tutorialText, 425f));
			this.gameManager.tutorialText.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("tutCR85");
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle5)
		{
			this.tutorialSequence = TutorialManager.TutorialSequence.cr8battle6;
			this.gameManager.StartCoroutine(this.gameManager.ShowPromptText(this.gameManager.tutorialText, 425f));
			this.gameManager.tutorialText.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("tutCR86");
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle6)
		{
			this.tutorialSequence = TutorialManager.TutorialSequence.cr8battle7;
			this.gameManager.StartCoroutine(this.gameManager.ShowPromptText(this.gameManager.tutorialText, 425f));
			this.gameManager.tutorialText.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("tutCR87");
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle7)
		{
			this.tutorialSequence = TutorialManager.TutorialSequence.cr8battle8;
			this.gameManager.StartCoroutine(this.gameManager.ShowPromptText(this.gameManager.tutorialText, 425f));
			this.gameManager.tutorialText.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("tutCR88");
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle8)
		{
			this.tutorialSequence = TutorialManager.TutorialSequence.cr8battle9;
			this.gameManager.StartCoroutine(this.gameManager.ShowPromptText(this.gameManager.tutorialText, 425f));
			this.gameManager.tutorialText.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("tutCR89");
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.toteStart)
		{
			this.tutorialSequence = TutorialManager.TutorialSequence.toteBattle1;
			this.gameManager.StartCoroutine(this.gameManager.ShowPromptText(this.gameManager.tutorialText, 425f));
			this.gameManager.tutorialText.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("tutTote1");
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.toteBattle1)
		{
			this.tutorialSequence = TutorialManager.TutorialSequence.toteBattle2;
			this.gameManager.StartCoroutine(this.gameManager.ShowPromptText(this.gameManager.tutorialText, 425f));
			this.gameManager.tutorialText.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("tutTote2");
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.toteBattle2)
		{
			this.tutorialSequence = TutorialManager.TutorialSequence.toteBattle3;
			this.gameManager.StartCoroutine(this.gameManager.ShowPromptText(this.gameManager.tutorialText, 425f));
			this.gameManager.tutorialText.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("tutTote3");
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.toteBattle3)
		{
			this.tutorialSequence = TutorialManager.TutorialSequence.toteBattle4;
			this.gameManager.StartCoroutine(this.gameManager.ShowPromptText(this.gameManager.tutorialText, 425f));
			this.gameManager.tutorialText.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("tutTote4");
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.toteChest)
		{
			this.tutorialSequence = TutorialManager.TutorialSequence.toteChestOpen;
			this.gameManager.StartCoroutine(this.gameManager.ShowPromptText(this.gameManager.tutorialText, 425f));
			this.gameManager.tutorialText.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey("tutTote5");
		}
	}

	// Token: 0x06000B18 RID: 2840 RVA: 0x00070778 File Offset: 0x0006E978
	public void CombatEnd()
	{
		this.gameManager.StartCoroutine(this.gameManager.HidePromptText(this.gameManager.tutorialText, -220f));
		if (this.tutorialSequence == TutorialManager.TutorialSequence.toteBattle4)
		{
			this.tutorialSequence = TutorialManager.TutorialSequence.toteChest;
		}
	}

	// Token: 0x06000B19 RID: 2841 RVA: 0x000707B4 File Offset: 0x0006E9B4
	public void TutorialTurnStart()
	{
		if (this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle1)
		{
			base.StartCoroutine(this.battle1(0));
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle2)
		{
			base.StartCoroutine(this.battle1(1));
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle3)
		{
			base.StartCoroutine(this.battle1(2));
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle4)
		{
			base.StartCoroutine(this.battle1(3));
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle5)
		{
			base.StartCoroutine(this.battle1(4));
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle6)
		{
			base.StartCoroutine(this.battle1(5));
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle7)
		{
			base.StartCoroutine(this.battle1(6));
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle8)
		{
			base.StartCoroutine(this.battle1(7));
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle9)
		{
			base.StartCoroutine(this.battle1(8));
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.toteBattle1)
		{
			base.StartCoroutine(this.SpawnToteItems(0));
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.toteBattle2)
		{
			base.StartCoroutine(this.SpawnToteItems(1));
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.toteBattle3)
		{
			base.StartCoroutine(this.SpawnToteItems(2));
			return;
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.toteBattle4)
		{
			base.StartCoroutine(this.SpawnToteItems(3));
		}
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x00070908 File Offset: 0x0006EB08
	public void TutorialTurnEnd()
	{
		if (this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle1 || this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle2 || this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle3 || this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle4 || this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle5 || this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle6 || this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle7 || this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle8 || this.tutorialSequence == TutorialManager.TutorialSequence.cr8battle9)
		{
			foreach (Item2 item in Item2.GetAllItems())
			{
				if (item && item.itemMovement && !item.destroyed && !item.itemType.Contains(Item2.ItemType.Core))
				{
					item.itemMovement.DelayDestroy();
				}
			}
		}
		if (this.tutorialSequence == TutorialManager.TutorialSequence.toteBattle1 || this.tutorialSequence == TutorialManager.TutorialSequence.toteBattle2 || this.tutorialSequence == TutorialManager.TutorialSequence.toteBattle3 || this.tutorialSequence == TutorialManager.TutorialSequence.toteBattle4)
		{
			Object.FindObjectOfType<Tote>().DestroyAllCarvings();
		}
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x00070A18 File Offset: 0x0006EC18
	public IEnumerator SpawnToteItems(int num)
	{
		Transform transform = GameObject.FindGameObjectWithTag("ItemParent").transform;
		Tote tote = Object.FindObjectOfType<Tote>();
		tote.DestroyAllCarvings();
		if (num == 0)
		{
			Object.Instantiate<GameObject>(this.toteTutorialItems[0], Vector3.one * -999f, Quaternion.identity, transform);
			Object.Instantiate<GameObject>(this.toteTutorialItems[0], Vector3.one * -999f, Quaternion.identity, transform);
			Object.Instantiate<GameObject>(this.toteTutorialItems[0], Vector3.one * -999f, Quaternion.identity, transform);
		}
		else if (num == 1)
		{
			Object.Instantiate<GameObject>(this.toteTutorialItems[1], Vector3.one * -999f, Quaternion.identity, transform);
		}
		else if (num == 2)
		{
			Object.Instantiate<GameObject>(this.toteTutorialItems[2], Vector3.one * -999f, Quaternion.identity, transform);
			Object.Instantiate<GameObject>(this.toteTutorialItems[3], Vector3.one * -999f, Quaternion.identity, transform);
			Object.Instantiate<GameObject>(this.toteTutorialItems[4], Vector3.one * -999f, Quaternion.identity, transform);
			Object.Instantiate<GameObject>(this.toteTutorialItems[4], Vector3.one * -999f, Quaternion.identity, transform);
			Object.Instantiate<GameObject>(this.toteTutorialItems[4], Vector3.one * -999f, Quaternion.identity, transform);
		}
		else if (num == 3)
		{
			Object.Instantiate<GameObject>(this.toteTutorialItems[0], Vector3.one * -999f, Quaternion.identity, transform);
			Object.Instantiate<GameObject>(this.toteTutorialItems[5], Vector3.one * -999f, Quaternion.identity, transform);
		}
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		tote.AssignAllCardsAfterLoad();
		yield break;
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x00070A2E File Offset: 0x0006EC2E
	private IEnumerator battle1(int num)
	{
		if (num == 0)
		{
			Object.Instantiate<GameObject>(this.cr8TutorialItems[0], Vector3.zero, Quaternion.identity);
		}
		else if (num == 1)
		{
			Object.Instantiate<GameObject>(this.cr8TutorialItems[1], Vector3.zero, Quaternion.identity);
			Object.Instantiate<GameObject>(this.cr8TutorialItems[1], Vector3.zero, Quaternion.identity);
		}
		else if (num == 2)
		{
			Object.Instantiate<GameObject>(this.cr8TutorialItems[2], Vector3.zero, Quaternion.identity);
		}
		else if (num == 3)
		{
			Object.Instantiate<GameObject>(this.cr8TutorialItems[3], Vector3.zero, Quaternion.identity);
			Object.Instantiate<GameObject>(this.cr8TutorialItems[4], Vector3.zero, Quaternion.identity);
		}
		else if (num == 4)
		{
			Object.Instantiate<GameObject>(this.cr8TutorialItems[7], Vector3.zero, Quaternion.identity);
			Object.Instantiate<GameObject>(this.cr8TutorialItems[7], Vector3.zero, Quaternion.identity);
			Object.Instantiate<GameObject>(this.cr8TutorialItems[5], Vector3.zero, Quaternion.identity);
		}
		else if (num == 5)
		{
			Object.Instantiate<GameObject>(this.cr8TutorialItems[8], Vector3.zero, Quaternion.identity);
			Object.Instantiate<GameObject>(this.cr8TutorialItems[6], Vector3.zero, Quaternion.identity);
		}
		else if (num == 6)
		{
			Object.Instantiate<GameObject>(this.cr8TutorialItems[2], Vector3.zero, Quaternion.identity);
			Object.Instantiate<GameObject>(this.cr8TutorialItems[1], Vector3.zero, Quaternion.identity);
		}
		else if (num == 7)
		{
			Object.Instantiate<GameObject>(this.cr8TutorialItems[7], Vector3.zero, Quaternion.identity);
			Object.Instantiate<GameObject>(this.cr8TutorialItems[7], Vector3.zero, Quaternion.identity);
			Object.Instantiate<GameObject>(this.cr8TutorialItems[9], Vector3.zero, Quaternion.identity);
		}
		else if (num == 8)
		{
			Object.Instantiate<GameObject>(this.cr8TutorialItems[4], Vector3.zero, Quaternion.identity);
			Object.Instantiate<GameObject>(this.cr8TutorialItems[4], Vector3.zero, Quaternion.identity);
			Object.Instantiate<GameObject>(this.cr8TutorialItems[4], Vector3.zero, Quaternion.identity);
			Object.Instantiate<GameObject>(this.cr8TutorialItems[10], Vector3.zero, Quaternion.identity);
			Object.Instantiate<GameObject>(this.cr8TutorialItems[3], Vector3.zero, Quaternion.identity);
		}
		this.gameManager.MoveAllItems();
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		this.gameManager.StartReorganization();
		yield break;
	}

	// Token: 0x040008F5 RID: 2293
	public static TutorialManager main;

	// Token: 0x040008F6 RID: 2294
	[SerializeField]
	private GameObject tutorialPopUpPrefab;

	// Token: 0x040008F7 RID: 2295
	[SerializeField]
	private TextMeshProUGUI versionText;

	// Token: 0x040008F8 RID: 2296
	[SerializeField]
	public string versionTextString;

	// Token: 0x040008F9 RID: 2297
	public TutorialManager.TutorialSequence tutorialSequence;

	// Token: 0x040008FA RID: 2298
	public List<string> completedTutorials;

	// Token: 0x040008FB RID: 2299
	[SerializeField]
	private GameObject introCinematic;

	// Token: 0x040008FC RID: 2300
	[Header("--------Testing Properties----------------------------------------")]
	[SerializeField]
	public TutorialManager.PlayType playType;

	// Token: 0x040008FD RID: 2301
	[SerializeField]
	public Character.CharacterName characterName;

	// Token: 0x040008FE RID: 2302
	[SerializeField]
	public RunType testingRunType;

	// Token: 0x040008FF RID: 2303
	[SerializeField]
	public Missions testingMission;

	// Token: 0x04000900 RID: 2304
	[SerializeField]
	public GameObject itemOnly;

	// Token: 0x04000901 RID: 2305
	[SerializeField]
	public DungeonLevel.EnemyEncounter2 enemyEncounter;

	// Token: 0x04000902 RID: 2306
	[SerializeField]
	public DungeonLevel.EventEncounter2 eventEncounter;

	// Token: 0x04000903 RID: 2307
	[Header("-------------------------------------------------------------------")]
	[SerializeField]
	private GameObject tutorialEvent;

	// Token: 0x04000904 RID: 2308
	[SerializeField]
	private GameObject tutorialRooms;

	// Token: 0x04000905 RID: 2309
	[SerializeField]
	private GameObject cr8TutorialRooms;

	// Token: 0x04000906 RID: 2310
	[SerializeField]
	private GameObject toteTutorialRooms;

	// Token: 0x04000907 RID: 2311
	[SerializeField]
	private GameObject moveHereText;

	// Token: 0x04000908 RID: 2312
	[SerializeField]
	private GameObject cr8CorePrefab;

	// Token: 0x04000909 RID: 2313
	[SerializeField]
	private List<GameObject> cr8TutorialItems;

	// Token: 0x0400090A RID: 2314
	[SerializeField]
	private List<GameObject> toteTutorialItems;

	// Token: 0x0400090B RID: 2315
	[SerializeField]
	private GameObject testingRooms;

	// Token: 0x0400090C RID: 2316
	[SerializeField]
	private GameObject matthewRooms;

	// Token: 0x0400090D RID: 2317
	[SerializeField]
	private GameObject[] startingObjects;

	// Token: 0x0400090E RID: 2318
	[SerializeField]
	private GameObject testingItems;

	// Token: 0x0400090F RID: 2319
	[SerializeField]
	private int goldAmount;

	// Token: 0x04000910 RID: 2320
	[SerializeField]
	public GameObject toteItem;

	// Token: 0x04000911 RID: 2321
	private GameManager gameManager;

	// Token: 0x04000912 RID: 2322
	private GameFlowManager gameFlowManager;

	// Token: 0x04000913 RID: 2323
	[SerializeField]
	private Transform clickMe;

	// Token: 0x04000914 RID: 2324
	[SerializeField]
	private GameObject tutorialStartEventMaster;

	// Token: 0x04000915 RID: 2325
	[SerializeField]
	private GameObject gridPrefab;

	// Token: 0x04000916 RID: 2326
	[Header("-------------------------------Testing Options------------------------------------")]
	[Header("-------------------------------------------------------------------")]
	[SerializeField]
	private bool expandInventory;

	// Token: 0x04000917 RID: 2327
	[SerializeField]
	public bool alwaysGenerateReorganizeItem;

	// Token: 0x04000918 RID: 2328
	[SerializeField]
	public bool allowCursesToBeMoved;

	// Token: 0x04000919 RID: 2329
	[SerializeField]
	public bool disableLevelingUp;

	// Token: 0x0400091A RID: 2330
	[SerializeField]
	public bool getRelics;

	// Token: 0x0400091B RID: 2331
	[SerializeField]
	public bool moveThroughBlockedItems;

	// Token: 0x0400091C RID: 2332
	[SerializeField]
	public GameObject reorganizeItem;

	// Token: 0x0400091D RID: 2333
	[SerializeField]
	public bool unlockAllAtlas;

	// Token: 0x0400091E RID: 2334
	[SerializeField]
	public bool showAllEventOptions;

	// Token: 0x0400091F RID: 2335
	[Header("-------------------------------------------------------------------")]
	[SerializeField]
	private bool exportText;

	// Token: 0x04000920 RID: 2336
	[SerializeField]
	private float timeScale;

	// Token: 0x04000921 RID: 2337
	[SerializeField]
	private RunType standardRunType;

	// Token: 0x04000922 RID: 2338
	private ES3Settings _settings;

	// Token: 0x04000923 RID: 2339
	[SerializeField]
	private GameObject matthew;

	// Token: 0x04000924 RID: 2340
	[SerializeField]
	private GameObject classicMatthew;

	// Token: 0x04000925 RID: 2341
	[SerializeField]
	private Transform combatEvent;

	// Token: 0x020003C7 RID: 967
	public enum PlayType
	{
		// Token: 0x040016A5 RID: 5797
		tutorial,
		// Token: 0x040016A6 RID: 5798
		testing,
		// Token: 0x040016A7 RID: 5799
		release,
		// Token: 0x040016A8 RID: 5800
		loadGame,
		// Token: 0x040016A9 RID: 5801
		cr8Tutorial,
		// Token: 0x040016AA RID: 5802
		totetorial
	}

	// Token: 0x020003C8 RID: 968
	public enum TutorialSequence
	{
		// Token: 0x040016AC RID: 5804
		start,
		// Token: 0x040016AD RID: 5805
		openingMessage,
		// Token: 0x040016AE RID: 5806
		moveToChest,
		// Token: 0x040016AF RID: 5807
		organizeChest,
		// Token: 0x040016B0 RID: 5808
		openMap,
		// Token: 0x040016B1 RID: 5809
		moveToCombat,
		// Token: 0x040016B2 RID: 5810
		firstCombat,
		// Token: 0x040016B3 RID: 5811
		done,
		// Token: 0x040016B4 RID: 5812
		trulyDone,
		// Token: 0x040016B5 RID: 5813
		cr8start,
		// Token: 0x040016B6 RID: 5814
		cr8battle1,
		// Token: 0x040016B7 RID: 5815
		cr8battle2,
		// Token: 0x040016B8 RID: 5816
		cr8battle3,
		// Token: 0x040016B9 RID: 5817
		cr8battle4,
		// Token: 0x040016BA RID: 5818
		cr8battle5,
		// Token: 0x040016BB RID: 5819
		cr8battle6,
		// Token: 0x040016BC RID: 5820
		cr8battle7,
		// Token: 0x040016BD RID: 5821
		cr8battle8,
		// Token: 0x040016BE RID: 5822
		toteStart,
		// Token: 0x040016BF RID: 5823
		toteBattle1,
		// Token: 0x040016C0 RID: 5824
		toteBattle2,
		// Token: 0x040016C1 RID: 5825
		toteBattle3,
		// Token: 0x040016C2 RID: 5826
		toteBattle4,
		// Token: 0x040016C3 RID: 5827
		toteChest,
		// Token: 0x040016C4 RID: 5828
		toteChestOpen,
		// Token: 0x040016C5 RID: 5829
		purseAfterOpenMap,
		// Token: 0x040016C6 RID: 5830
		cr8battle9
	}
}
