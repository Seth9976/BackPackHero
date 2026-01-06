using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DevPunksSaveGame;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000157 RID: 343
public class Overworld_SaveManager : MonoBehaviour
{
	// Token: 0x17000020 RID: 32
	// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00087AF2 File Offset: 0x00085CF2
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

	// Token: 0x06000D91 RID: 3473 RVA: 0x00087B10 File Offset: 0x00085D10
	private void Start()
	{
		string text = Application.persistentDataPath + "/";
		this.settings.path = text + "bphStoryModeOverworld" + Singleton.Instance.storyModeSlot.ToString() + ".sav";
		this.LoadOverworldIncrementing();
	}

	// Token: 0x06000D92 RID: 3474 RVA: 0x00087B60 File Offset: 0x00085D60
	public void LoadOverworldIncrementing()
	{
		Debug.Log("---Loading overworld incrementing---");
		try
		{
			List<string> list = SaveIncrementer.GetSaveFilesForSlot("bphStoryModeOverworld" + Singleton.Instance.storyModeSlot.ToString(), ".sav", false);
			try
			{
				if (Singleton.Instance.townSaveBackup != null && Singleton.Instance.townSaveBackup.Length != 0)
				{
					Debug.Log("Found town save in memory, length " + Singleton.Instance.townSaveBackup.Length.ToString());
					this.settings.location = ES3.Location.Cache;
					ES3.SaveRaw(Singleton.Instance.townSaveBackup, this.settings);
					list = list.Prepend("memory").ToList<string>();
				}
				else
				{
					Debug.Log("No town save in memory, loading from files.");
				}
			}
			catch (Exception ex)
			{
				string text = "Could not load overworld from memory copy, trying files.";
				Exception ex2 = ex;
				Debug.Log(text + ((ex2 != null) ? ex2.ToString() : null));
				this.settings.location = ES3.Location.File;
			}
			Debug.Log("SaveList");
			foreach (string text2 in list)
			{
				Debug.Log(text2);
			}
			if (list.Count == 0)
			{
				string text3 = Application.persistentDataPath + "/";
				this.settings.path = text3 + SaveIncrementer.GetFilenameForSlot(list, "bphStoryModeOverworld" + Singleton.Instance.storyModeSlot.ToString(), ".sav", true);
				return;
			}
			foreach (string text4 in list)
			{
				try
				{
					if (text4 != "memory")
					{
						this.settings.path = Application.persistentDataPath + "/" + text4;
						Application.persistentDataPath + "/";
					}
					if (ES3.KeyExists("buildingsStoredAsStrings", this.settings))
					{
						if (ES3.Load<string>("buildingsStoredAsStrings", this.settings) == ":RUBBLE E+(2.00, 22.50, 21.60)::RUBBLE B+(21.00, 0.50, 0.00)::RUBBLE A+(16.50, -9.50, -10.99)::RUBBLE B+(16.50, 4.50, 4.01)::RUBBLE A+(21.50, 14.50, 13.01)::RUBBLE A+(-13.50, 15.50, 14.01)::RUBBLE C+(19.00, 11.50, 11.00)::RUBBLE C+(6.00, -12.50, -13.00)::RUBBLE C+(14.00, 1.50, 1.00)::RUBBLE C+(-16.00, 12.50, 12.00)::RUBBLE C+(-11.00, 13.50, 13.00)::RUBBLE C+(-21.00, 20.50, 20.00)::RUBBLE A+(2.50, -10.50, -11.99)::RUBBLE C+(-12.00, -14.50, -15.00)::RUBBLE E+(-9.00, -14.50, -15.40)::RUBBLE C+(-9.00, -12.50, -13.00)::RUBBLE A+(6.50, 11.50, 10.01)::RUBBLE A+(34.50, 11.50, 10.01)::RUBBLE C+(-14.00, 3.50, 3.00)::RUBBLE C+(-20.00, 4.50, 4.00)::RUBBLE C+(4.00, 9.50, 9.00)::RUBBLE A+(20.50, -19.50, -20.99)::RUBBLE A+(30.50, 4.50, 3.01)::RUBBLE E+(1.00, -16.50, -17.40)::RUBBLE A+(6.50, -21.50, -22.99)::RUBBLE E+(34.00, -5.50, -6.40)::RUBBLE A+(-17.50, -5.50, -6.99)::RUBBLE D+(17.00, 22.50, 21.60)::SHOP RUBBLE+(-2.50, 7.50, 6.06)::RUBBLE A+(-9.50, -7.50, -8.99)::RUBBLE E+(0.00, 15.50, 14.60)::RUBBLE B+(-8.50, 20.50, 20.01)::RUBBLE D+(-13.00, -4.50, -5.40)::RUBBLE D+(-9.00, -16.50, -17.40)::RUBBLE D+(5.00, -17.50, -18.40):")
						{
							Debug.LogWarning("Default structures at " + text4);
							if (text4 != list.Last<string>())
							{
								continue;
							}
						}
						Debug.Log("Attempting to load " + text4);
						base.StartCoroutine(this.Load());
						return;
					}
					Debug.LogWarning("No buildings in save " + text4);
				}
				catch (Exception ex3)
				{
					string text5 = "Could not load overworld save ";
					string text6 = text4;
					string text7 = " --- ";
					Exception ex4 = ex3;
					Debug.LogError(text5 + text6 + text7 + ((ex4 != null) ? ex4.ToString() : null));
				}
			}
		}
		catch (Exception ex5)
		{
			string text8 = "Error while loading the overworld ";
			Exception ex6 = ex5;
			Debug.LogError(text8 + ((ex6 != null) ? ex6.ToString() : null));
		}
		Singleton.Instance.errorMessage = "Could not load your town save file. Please restart the game and try again. <br> If the issue persists, please contact us.";
		SceneManager.LoadScene("MainMenu");
	}

	// Token: 0x06000D93 RID: 3475 RVA: 0x00087EA4 File Offset: 0x000860A4
	private void Update()
	{
		if (this.save)
		{
			this.Save();
			this.save = false;
		}
		if (this.load)
		{
			this.LoadOverworldIncrementing();
			this.load = false;
		}
	}

	// Token: 0x06000D94 RID: 3476 RVA: 0x00087ED0 File Offset: 0x000860D0
	public void SaveCommand(bool canBeStopped = true)
	{
		if (canBeStopped && (!MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.talkedToZaarAndUnlockedMissionMenu) || Overworld_ConversationManager.main.InLockedConversation()))
		{
			PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm3"), DigitalCursor.main.transform.position);
			return;
		}
		this.Save();
	}

	// Token: 0x06000D95 RID: 3477 RVA: 0x00087F2E File Offset: 0x0008612E
	private IEnumerator SaveRestricted()
	{
		yield return new WaitForSeconds(1f);
		this.isSavingOrLoading = false;
		yield break;
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x00087F40 File Offset: 0x00086140
	private void Save()
	{
		if (this.isSavingOrLoading)
		{
			PopUpManager.main.CreatePopUp("Could not save your town - saving or loading already in progress");
			if (this.savingCoroutine != null)
			{
				base.StopCoroutine(this.savingCoroutine);
			}
			this.savingCoroutine = base.StartCoroutine(this.SaveRestricted());
			return;
		}
		this.isSavingOrLoading = true;
		if (this.savingCoroutine != null)
		{
			base.StopCoroutine(this.savingCoroutine);
		}
		this.savingCoroutine = base.StartCoroutine(this.SaveRestricted());
		this.settings.location = ES3.Location.File;
		List<string> saveFilesForSlot = SaveIncrementer.GetSaveFilesForSlot("bphStoryModeOverworld" + Singleton.Instance.storyModeSlot.ToString(), ".sav", false);
		string text = Application.persistentDataPath + "/";
		this.settings.path = text + SaveIncrementer.GetFilenameForSlot(saveFilesForSlot, "bphStoryModeOverworld" + Singleton.Instance.storyModeSlot.ToString(), ".sav", true);
		LoadStoryGame.DeleteStoryRunSave(Singleton.Instance.storyModeSlot);
		MetaProgressSaveManager.main.SetupSettings(Singleton.Instance.storyModeSlot, false);
		MetaProgressSaveManager.main.SaveAll();
		SaveManager.SaveImage(this.screenshotCamera, Singleton.Instance.storyModeSlot);
		Vector2Int vector2Int;
		Vector2Int vector2Int2;
		string text2 = this.tilemapCustomSaver.ConvertToString(out vector2Int, out vector2Int2);
		if (text2 == null)
		{
			Singleton.Instance.ShowErrorMessage("Could not convert tilemap to string");
			PopUpManager.main.CreatePopUp("Could not save your town - couldn't convert tiles");
			return;
		}
		int x = vector2Int.x;
		int y = vector2Int.y;
		int x2 = vector2Int2.x;
		int y2 = vector2Int2.y;
		if (x == 0 || y == 0)
		{
			Singleton.Instance.ShowErrorMessage(string.Format("Unusable tilemap size {0}x{1}", x, y));
			PopUpManager.main.CreatePopUp("Could not save your town - unusable tilemap size");
			return;
		}
		string text3 = this.tilemapCustomSaver.ConvertStructuresToString(this.buildingsLayer);
		if (text3 == null)
		{
			Singleton.Instance.ShowErrorMessage("Structure string evaluated to null");
			PopUpManager.main.CreatePopUp("Could not save your town - couldn't convert buildings");
			return;
		}
		try
		{
			ES3.Save<string>("buildingsStoredAsStrings", text3, this.settings);
			ES3.Save<string>("tilemapLayer", text2, this.settings);
			ES3.Save<int>("tilemapLayerX", x, this.settings);
			ES3.Save<int>("tilemapLayerY", y, this.settings);
			ES3.Save<int>("tilemapLayerXOrigin", x2, this.settings);
			ES3.Save<int>("tilemapLayerYOrigin", y2, this.settings);
		}
		catch (Exception ex)
		{
			Singleton.Instance.ShowErrorMessage(string.Format("An error occurred while saving: {0}", ex));
			PopUpManager.main.CreatePopUp("Could not save your town - an error occurred with Saving System");
		}
		Singleton.Instance.townSaveBackup = ES3.LoadRawBytes(this.settings);
		PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm41"), DigitalCursor.main.transform.position);
	}

	// Token: 0x06000D97 RID: 3479 RVA: 0x00088224 File Offset: 0x00086424
	private IEnumerator Load()
	{
		yield return new WaitUntil(() => !ConsoleWrapper.Instance.SaveInProgress);
		if (!MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.talkedToZaarAndUnlockedMissionMenu))
		{
			MetaProgressSaveManager.main.DisableIntroMetaProgressMarkers();
			yield break;
		}
		this.isSavingOrLoading = true;
		Overworld_FollowAStarPath.DisableAll();
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		Overworld_Structure.structures.Clear();
		string text = ES3.Load<string>("tilemapLayer", this.settings);
		int num = ES3.Load<int>("tilemapLayerX", this.settings);
		int num2 = ES3.Load<int>("tilemapLayerY", this.settings);
		int num3 = ES3.Load<int>("tilemapLayerXOrigin", this.settings);
		int num4 = ES3.Load<int>("tilemapLayerYOrigin", this.settings);
		string text2 = "";
		try
		{
			text2 = ES3.Load<string>("buildingsStoredAsStrings", this.settings);
		}
		catch
		{
		}
		this.tilemapCustomSaver.ConvertFromString(text, new Vector2Int(num, num2), new Vector2Int(num3, num4));
		this.tilemapCustomSaver.ConvertStringToBuildings(this.buildingsLayer, text2);
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		Overworld_FollowAStarPath.EnableAll();
		Overworld_Manager.main.MakeReferences();
		Overworld_BuildingManager.main.ResetBuildingButtons();
		DisableWaterTile.DisableAllWaterTiles();
		Overworld_Structure.AllStructuresApplyAllModifiers();
		Debug.Log("Loaded Overworld");
		yield return null;
		yield return null;
		this.RemoveOutOfBoundsBuildings();
		this.isSavingOrLoading = false;
		yield break;
	}

	// Token: 0x06000D98 RID: 3480 RVA: 0x00088234 File Offset: 0x00086434
	private void RemoveOutOfBoundsBuildings()
	{
		foreach (Overworld_Structure overworld_Structure in Overworld_Structure.structures)
		{
			if (overworld_Structure.IsOutOfBOunds())
			{
				overworld_Structure.NaturalDelete();
			}
		}
	}

	// Token: 0x04000B01 RID: 2817
	[SerializeField]
	private List<GameObject> objectsToSave;

	// Token: 0x04000B02 RID: 2818
	[SerializeField]
	private List<Missions> allMissions;

	// Token: 0x04000B03 RID: 2819
	[SerializeField]
	private Camera screenshotCamera;

	// Token: 0x04000B04 RID: 2820
	public bool save;

	// Token: 0x04000B05 RID: 2821
	public bool load;

	// Token: 0x04000B06 RID: 2822
	[SerializeField]
	private GameObject buildingsLayer;

	// Token: 0x04000B07 RID: 2823
	[SerializeField]
	private TilemapCustomSaver tilemapCustomSaver;

	// Token: 0x04000B08 RID: 2824
	public bool isSavingOrLoading;

	// Token: 0x04000B09 RID: 2825
	private ES3Settings _settings;

	// Token: 0x04000B0A RID: 2826
	public bool loadGame;

	// Token: 0x04000B0B RID: 2827
	private string storedArrangement;

	// Token: 0x04000B0C RID: 2828
	private Coroutine savingCoroutine;
}
