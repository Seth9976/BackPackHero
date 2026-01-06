using System;
using System.IO;
using UnityEngine;

// Token: 0x020001A4 RID: 420
public class TooManySavesBox : MonoBehaviour
{
	// Token: 0x17000033 RID: 51
	// (get) Token: 0x060010AC RID: 4268 RVA: 0x0009E5D2 File Offset: 0x0009C7D2
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

	// Token: 0x060010AD RID: 4269 RVA: 0x0009E5F0 File Offset: 0x0009C7F0
	private DateTime GetSaveDate(int num)
	{
		string text = Application.persistentDataPath + "/";
		this.settings.path = text + "bphRun" + num.ToString() + ".sav";
		DateTime timestamp = ES3.GetTimestamp(text + "bphRun" + num.ToString() + ".sav");
		timestamp.ToLocalTime();
		return timestamp;
	}

	// Token: 0x060010AE RID: 4270 RVA: 0x0009E658 File Offset: 0x0009C858
	public void DeleteOldestSave()
	{
		int num = 999999;
		DateTime dateTime = new DateTime(2099, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		string text = Application.persistentDataPath + "/";
		foreach (string text2 in Directory.GetFiles(text, "bphRun*.sav"))
		{
			Debug.Log("File found!" + text2);
			int num2 = int.Parse(text2.Replace(text + "bphRun", "").Replace(".sav", ""));
			DateTime saveDate = this.GetSaveDate(num2);
			if (saveDate < dateTime)
			{
				dateTime = saveDate;
				num = num2;
			}
		}
		Debug.Log("Oldest save is " + num.ToString() + " from " + dateTime.ToString());
		if (num != 999999)
		{
			SaveManager.DeleteSave(num);
		}
		MenuManager.main.StartGameButton();
		this.GoBack();
	}

	// Token: 0x060010AF RID: 4271 RVA: 0x0009E749 File Offset: 0x0009C949
	public void GoBack()
	{
		Object.Destroy(this.box.gameObject);
	}

	// Token: 0x04000D8E RID: 3470
	private ES3Settings _settings;

	// Token: 0x04000D8F RID: 3471
	[SerializeField]
	private GameObject box;
}
