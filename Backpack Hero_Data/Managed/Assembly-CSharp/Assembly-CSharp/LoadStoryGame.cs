using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x0200006C RID: 108
public class LoadStoryGame : MonoBehaviour
{
	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600021B RID: 539 RVA: 0x0000D165 File Offset: 0x0000B365
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

	// Token: 0x0600021C RID: 540 RVA: 0x0000D184 File Offset: 0x0000B384
	private void SetupAsNew()
	{
		this.all.SetActive(false);
		this.deleteButton.SetActive(false);
		this.newGame.SetActive(true);
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.GetComponent<RectTransform>());
		LangaugeManager.main.SetFont(base.transform);
	}

	// Token: 0x0600021D RID: 541 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	private void Start()
	{
		this.FixLastScreenshotDirectory();
		this.RemoveBrokenSaveDirectories();
		Singleton.Instance.storyMode = true;
		int siblingIndex = base.transform.parent.GetSiblingIndex();
		Singleton.Instance.storyModeSlot = siblingIndex;
		MetaProgressSaveManager.main.SetupMetaData(siblingIndex);
		if (MetaProgressSaveManager.main.AnyMetaData(siblingIndex))
		{
			this.all.SetActive(true);
			this.newGame.SetActive(false);
			Sprite sprite = SaveManager.LoadImage("bphStoryModeScreenshot" + siblingIndex.ToString() + ".png");
			if (sprite != null)
			{
				this.storyModeSlotScreenshot.sprite = sprite;
				this.storyModeSlotScreenshot.transform.localPosition = Vector3.zero;
				this.storyModeSlotScreenshot.GetComponent<RectTransform>().sizeDelta = new Vector2(600f, 338f);
			}
			this.title.text = LangaugeManager.main.GetTextByKey("storySave1") + " " + (siblingIndex + 1).ToString();
			this.settings.path = string.Format("{0}/bphStoryModeRun{1}.sav", Application.persistentDataPath, siblingIndex);
			if (!ES3.FileExists(this.settings))
			{
				this.description.text = LangaugeManager.main.GetTextByKey("storyExplanation") ?? "";
			}
			else
			{
				string text = ES3.Load<string>("saveCharName", this.settings);
				string text2 = ES3.Load<string>("saveRunName", this.settings);
				this.description.text = LangaugeManager.main.GetTextByKey(text) + " - " + LangaugeManager.main.GetTextByKey(text2);
			}
			DateTime dateTime = MetaProgressSaveManager.main.dateTime;
			dateTime = dateTime.ToLocalTime();
			this.time.text = dateTime.ToString("dd/MM/yyyy hh:mm tt") ?? "";
			this.itemsUnlocked.text = string.Concat(new string[]
			{
				LangaugeManager.main.GetTextByKey("storySave2"),
				": ",
				MetaProgressSaveManager.main.itemsDiscovered.Count.ToString(),
				" / ",
				DebugItemManager.main.item2s.Count.ToString()
			});
			this.missionsCompleted.text = LangaugeManager.main.GetTextByKey("storySave3") + ": " + MetaProgressSaveManager.main.missionsComplete.Count.ToString();
			if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedTote))
			{
				this.Tote.color = Color.white;
			}
			if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedCR8))
			{
				this.CR8.color = Color.white;
			}
			if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedSatchel))
			{
				this.Satchel.color = Color.white;
			}
			if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedPochette))
			{
				this.Pochette.color = Color.white;
			}
			LangaugeManager.main.SetFont(base.transform);
			return;
		}
		this.SetupAsNew();
	}

	// Token: 0x0600021E RID: 542 RVA: 0x0000D4E0 File Offset: 0x0000B6E0
	private void FixLastScreenshotDirectory()
	{
	}

	// Token: 0x0600021F RID: 543 RVA: 0x0000D4E2 File Offset: 0x0000B6E2
	private void RemoveBrokenSaveDirectories()
	{
	}

	// Token: 0x06000220 RID: 544 RVA: 0x0000D4E4 File Offset: 0x0000B6E4
	public void LoadStoryFromActivity(int slot)
	{
		this.slotNumber = slot;
		this.LoadStoryGameCommand();
	}

	// Token: 0x06000221 RID: 545 RVA: 0x0000D4F4 File Offset: 0x0000B6F4
	public void LoadStoryGameCommand()
	{
		if (SceneLoader.main.IsLoading())
		{
			return;
		}
		SoundManager.main.FadeOutAllSongs(1f);
		Singleton.Instance.saveNumber = -1;
		Singleton.Instance.storyMode = true;
		Singleton.Instance.storyModeSlot = this.slotNumber;
		MetaProgressSaveManager.main.SetupMetaData(this.slotNumber);
		string text = Application.persistentDataPath + "/";
		this.settings.path = text + "bphStoryModeRun" + Singleton.Instance.storyModeSlot.ToString() + ".sav";
		if (ES3.FileExists(this.settings))
		{
			Singleton.Instance.doTutorial = false;
			Singleton.Instance.completedTutorial = true;
			Singleton.Instance.loadSave = false;
			Singleton.Instance.loadSave = true;
			Singleton.Instance.loadOverworld = true;
			Debug.Log("Loading game");
			SceneLoader.main.LoadScene("Game", LoadSceneMode.Single, null, null);
			return;
		}
		if (SaveIncrementer.GetSaveFilesForSlot("bphStoryModeOverworld" + Singleton.Instance.storyModeSlot.ToString(), ".sav", false).Count > 0)
		{
			Singleton.Instance.loadOverworld = true;
			Debug.Log("Loading overworld");
			SceneLoader.main.LoadScene("Overworld", LoadSceneMode.Single, null, null);
			return;
		}
		Singleton.Instance.SetCharacter(Character.CharacterName.Purse);
		Singleton.Instance.doTutorial = true;
		Singleton.Instance.completedTutorial = false;
		Singleton.Instance.mission = Singleton.Instance.tutorialMission;
		Singleton.Instance.runType = null;
		Singleton.Instance.loadOverworld = true;
		SceneLoader.main.LoadScene("Game", LoadSceneMode.Single, null, null);
		MetaProgressSaveManager.main.missionsComplete = new List<string> { Missions.Stringify(Singleton.Instance.tutorialMission) };
		Debug.Log("No save file found");
	}

	// Token: 0x06000222 RID: 546 RVA: 0x0000D6CD File Offset: 0x0000B8CD
	public void ConsiderDelete()
	{
		if (SceneLoader.main.IsLoading())
		{
			return;
		}
		ConfirmationManager.CreateConfirmation(LangaugeManager.main.GetTextByKey("gm69"), LangaugeManager.main.GetTextByKey("gm70"), new Func<Action>(this.DeleteSave));
	}

	// Token: 0x06000223 RID: 547 RVA: 0x0000D70B File Offset: 0x0000B90B
	private Action DeleteSave()
	{
		this.DeleteSaveFiles();
		return null;
	}

	// Token: 0x06000224 RID: 548 RVA: 0x0000D714 File Offset: 0x0000B914
	private void DeleteSaveFiles()
	{
		LoadStoryGame.DeleteStorySaveFile(this.slotNumber);
		this.SetupAsNew();
	}

	// Token: 0x06000225 RID: 549 RVA: 0x0000D728 File Offset: 0x0000B928
	public static bool SaveExists(int slotNumber)
	{
		return SaveIncrementer.GetSaveFilesForSlot("bphStoryModeRun" + slotNumber.ToString(), ".sav", false).Count > 0 || SaveIncrementer.GetSaveFilesForSlot("bphStoryModeOverworld" + slotNumber.ToString(), ".sav", false).Count > 0;
	}

	// Token: 0x06000226 RID: 550 RVA: 0x0000D780 File Offset: 0x0000B980
	public static void DeleteStoryRunSave(int slotNumber)
	{
		string text = Application.persistentDataPath + "/" + "bphStoryModeRun" + slotNumber.ToString() + ".sav";
		if (ES3.FileExists(text))
		{
			ES3.DeleteFile(text);
		}
	}

	// Token: 0x06000227 RID: 551 RVA: 0x0000D7C4 File Offset: 0x0000B9C4
	public static void BackupStorySave()
	{
		string text = Application.persistentDataPath + "/";
		string text2 = text + "bphStoryModeOverworld" + Singleton.Instance.storyModeSlot.ToString() + ".sav";
		string text3 = text + "bphStoryModeMetaData" + Singleton.Instance.storyModeSlot.ToString() + ".sav";
		foreach (string text4 in new string[] { text2, text3 })
		{
			if (ES3.FileExists(text4))
			{
				string text5 = text4 + ".backup";
				if (ES3.FileExists(text5))
				{
					ES3.DeleteFile(text5);
				}
				ES3.CopyFile(text4, text5);
			}
		}
	}

	// Token: 0x06000228 RID: 552 RVA: 0x0000D874 File Offset: 0x0000BA74
	public static void RestoreFromBackup()
	{
		string text = Application.persistentDataPath + "/";
		string text2 = text + "bphStoryModeOverworld" + Singleton.Instance.storyModeSlot.ToString() + ".sav";
		string text3 = text + "bphStoryModeMetaData" + Singleton.Instance.storyModeSlot.ToString() + ".sav";
		foreach (string text4 in new string[] { text2, text3 })
		{
			if (!ES3.FileExists(text4))
			{
				string text5 = text4 + ".backup";
				if (ES3.FileExists(text5))
				{
					ES3.CopyFile(text5, text4);
				}
			}
		}
	}

	// Token: 0x06000229 RID: 553 RVA: 0x0000D91C File Offset: 0x0000BB1C
	public static void DeleteStorySaveFile(int slotNumber)
	{
		string text = Application.persistentDataPath + "/";
		string text2 = text + "bphStoryModeMetaData" + slotNumber.ToString() + ".sav";
		string text3 = text + "bphStoryModeRun" + slotNumber.ToString() + ".sav";
		string text4 = "bphStoryModeScreenshot" + slotNumber.ToString() + ".png";
		string[] array = new string[] { text2, text3, text4 };
		Debug.Log(array.Concat(SaveIncrementer.GetSaveFilesForSlot("bphStoryModeOverworld" + slotNumber.ToString(), ".sav", true)).Count<string>().ToString() + " files");
		foreach (string text5 in array.Concat(SaveIncrementer.GetSaveFilesForSlot("bphStoryModeOverworld" + slotNumber.ToString(), ".sav", true)))
		{
			if (ES3.FileExists(text5))
			{
				ES3.DeleteFile(text5);
			}
		}
		SaveIncrementer.DeleteAllSaveFiles("bphStoryModeMetaData" + slotNumber.ToString(), ".sav");
		if (ES3.FileExists(text4))
		{
			ES3.DeleteFile(text4);
		}
	}

	// Token: 0x04000162 RID: 354
	[Header("---------------Story Mode New-----------------")]
	[SerializeField]
	private GameObject newGame;

	// Token: 0x04000163 RID: 355
	[Header("---------------Story Mode Slot-----------------")]
	[SerializeField]
	private int slotNumber;

	// Token: 0x04000164 RID: 356
	[SerializeField]
	private GameObject all;

	// Token: 0x04000165 RID: 357
	[SerializeField]
	private GameObject deleteButton;

	// Token: 0x04000166 RID: 358
	[SerializeField]
	private TextMeshProUGUI title;

	// Token: 0x04000167 RID: 359
	[SerializeField]
	private TextMeshProUGUI description;

	// Token: 0x04000168 RID: 360
	[SerializeField]
	private TextMeshProUGUI time;

	// Token: 0x04000169 RID: 361
	[SerializeField]
	private Image storyModeSlotScreenshot;

	// Token: 0x0400016A RID: 362
	[SerializeField]
	private TextMeshProUGUI itemsUnlocked;

	// Token: 0x0400016B RID: 363
	[SerializeField]
	private TextMeshProUGUI missionsCompleted;

	// Token: 0x0400016C RID: 364
	[SerializeField]
	private Image Purse;

	// Token: 0x0400016D RID: 365
	[SerializeField]
	private Image Tote;

	// Token: 0x0400016E RID: 366
	[SerializeField]
	private Image CR8;

	// Token: 0x0400016F RID: 367
	[SerializeField]
	private Image Satchel;

	// Token: 0x04000170 RID: 368
	[SerializeField]
	private Image Pochette;

	// Token: 0x04000171 RID: 369
	private ES3Settings _settings;
}
