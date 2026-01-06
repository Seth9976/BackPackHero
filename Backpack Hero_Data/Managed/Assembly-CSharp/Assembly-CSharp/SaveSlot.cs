using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000117 RID: 279
public class SaveSlot : MonoBehaviour
{
	// Token: 0x17000015 RID: 21
	// (get) Token: 0x06000979 RID: 2425 RVA: 0x00061175 File Offset: 0x0005F375
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

	// Token: 0x0600097A RID: 2426 RVA: 0x00061194 File Offset: 0x0005F394
	public void SetStuff(Sprite sprite, int num)
	{
		Debug.Log("Loading slot bphRun" + num.ToString() + ".sav");
		this.image.sprite = sprite;
		this.saveNumber = num;
		string text = Application.persistentDataPath + "/";
		this.settings.path = text + "bphRun" + num.ToString() + ".sav";
		this.dateTime = ES3.GetTimestamp(text + "bphRun" + this.saveNumber.ToString() + ".sav");
		this.dateTime.ToLocalTime();
		this.dateTypeText.text = this.dateTime.ToString();
		if (ES3.Load<bool>("debugMode", false, this.settings))
		{
			this.runTypeText.text = "Debugging Mode";
		}
		else
		{
			this.runTypeText.text = LangaugeManager.main.GetTextByKey(ES3.Load<string>("saveCharName", this.settings)) + " - " + LangaugeManager.main.GetTextByKey(ES3.Load<string>("saveRunName", this.settings));
		}
		try
		{
			this.levelText.text = LangaugeManager.main.GetTextByKey("h1") + ": " + Mathf.Max(ES3.Load<int>("floor", 0, this.settings), 1).ToString();
			this.floorText.text = LangaugeManager.main.GetTextByKey("h4") + ": " + Mathf.Max(ES3.Load<int>("level", 0, this.settings), 1).ToString();
		}
		catch (Exception)
		{
			Debug.LogError("save file was corrupted");
			this.errorPopUp.SetActive(true);
			base.gameObject.GetComponent<Button>().interactable = false;
		}
		this.ironManText.gameObject.SetActive(ES3.Load<bool>("ironMan", false, this.settings));
		LangaugeManager.main.SetFont(base.transform);
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x000613AC File Offset: 0x0005F5AC
	public void LoadThisGame()
	{
		MenuManager menuManager = Object.FindObjectOfType<MenuManager>();
		if (menuManager.freezeMenu)
		{
			return;
		}
		SoundManager.main.PlaySFX("menuBlip");
		Singleton.Instance.saveNumber = this.saveNumber;
		menuManager.LoadGameAndContinueSave();
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x000613F0 File Offset: 0x0005F5F0
	public void ConsiderDelete()
	{
		if (SceneLoader.main.IsLoading())
		{
			return;
		}
		Debug.Log(this.dateTime.ToString() ?? "");
		Object.FindObjectOfType<SaveMenuManager>(true).nextSaveSlot = base.gameObject.transform.GetSiblingIndex();
		ConfirmationManager.CreateConfirmation(LangaugeManager.main.GetTextByKey("gm69"), LangaugeManager.main.GetTextByKey("gm70"), new Func<Action>(this.DeleteSave));
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x0006146D File Offset: 0x0005F66D
	private Action DeleteSave()
	{
		this.DeleteSaveFiles();
		return null;
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x00061478 File Offset: 0x0005F678
	public void DeleteSaveFiles()
	{
		SaveManager.DeleteSave(this.saveNumber);
		Transform parent = base.gameObject.transform.parent;
		int num = 9999;
		int siblingIndex = base.gameObject.transform.GetSiblingIndex();
		if (siblingIndex < parent.childCount - 1 && parent.GetChild(siblingIndex + 1) != null)
		{
			num = siblingIndex;
		}
		if (num == 9999 && siblingIndex > 0 && parent.GetChild(siblingIndex - 1) != null)
		{
			num = siblingIndex - 1;
		}
		if (num != 9999)
		{
			Object.FindObjectOfType<SaveMenuManager>(true).nextSaveSlot = num;
		}
		else
		{
			Object.FindObjectOfType<SaveMenuManager>(true).nextSaveSlot = 0;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0400078A RID: 1930
	[SerializeField]
	private int saveNumber;

	// Token: 0x0400078B RID: 1931
	public DateTime dateTime;

	// Token: 0x0400078C RID: 1932
	[SerializeField]
	private Image image;

	// Token: 0x0400078D RID: 1933
	[SerializeField]
	private TextMeshProUGUI dateTypeText;

	// Token: 0x0400078E RID: 1934
	[SerializeField]
	private TextMeshProUGUI runTypeText;

	// Token: 0x0400078F RID: 1935
	[SerializeField]
	private TextMeshProUGUI levelText;

	// Token: 0x04000790 RID: 1936
	[SerializeField]
	private TextMeshProUGUI floorText;

	// Token: 0x04000791 RID: 1937
	[SerializeField]
	private TextMeshProUGUI ironManText;

	// Token: 0x04000792 RID: 1938
	[SerializeField]
	private GameObject errorPopUp;

	// Token: 0x04000793 RID: 1939
	private ES3Settings _settings;
}
