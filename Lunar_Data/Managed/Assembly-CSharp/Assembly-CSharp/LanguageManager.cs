using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x0200004E RID: 78
public class LanguageManager : MonoBehaviour
{
	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000247 RID: 583 RVA: 0x0000BFCB File Offset: 0x0000A1CB
	public static LanguageManager main
	{
		get
		{
			return LanguageManager._instance;
		}
	}

	// Token: 0x06000248 RID: 584 RVA: 0x0000BFD2 File Offset: 0x0000A1D2
	public Dictionary<string, string> GetTerms()
	{
		return this.languageTerms;
	}

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06000249 RID: 585 RVA: 0x0000BFDC File Offset: 0x0000A1DC
	// (remove) Token: 0x0600024A RID: 586 RVA: 0x0000C010 File Offset: 0x0000A210
	public static event LanguageManager.ReplaceAllTextAction OnReplaceText;

	// Token: 0x0600024B RID: 587 RVA: 0x0000C044 File Offset: 0x0000A244
	private void OnEnable()
	{
		if (LanguageManager._instance != null && LanguageManager._instance != this)
		{
			Object.Destroy(base.gameObject);
		}
		else
		{
			LanguageManager._instance = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}
		this.LoadFilesFromStreaming();
		this.ChooseLanguage(0);
	}

	// Token: 0x0600024C RID: 588 RVA: 0x0000C096 File Offset: 0x0000A296
	private void OnDisable()
	{
		if (LanguageManager._instance == this)
		{
			LanguageManager._instance = null;
		}
	}

	// Token: 0x0600024D RID: 589 RVA: 0x0000C0AB File Offset: 0x0000A2AB
	private void Update()
	{
		if (Keyboard.current != null && Keyboard.current[Key.Semicolon].isPressed)
		{
			this.LoadLanguageFile();
		}
	}

	// Token: 0x0600024E RID: 590 RVA: 0x0000C0CC File Offset: 0x0000A2CC
	private void LoadFilesFromPath(string path)
	{
		if (Application.platform == RuntimePlatform.WebGLPlayer && this.languageLocation == LanguageManager.LanguageLocation.StreamingAssets)
		{
			Debug.LogError("Cannot load files from StreamingAssets on WebGL");
			return;
		}
		if (Application.platform == RuntimePlatform.WebGLPlayer || this.languageLocation == LanguageManager.LanguageLocation.Resources)
		{
			TextAsset[] array = Resources.LoadAll<TextAsset>(path);
			for (int i = 0; i < array.Length; i++)
			{
				string name = array[i].name;
				this.discoveredLanguages.Add(name);
			}
		}
		else
		{
			foreach (FileInfo fileInfo in new DirectoryInfo(Application.streamingAssetsPath).GetFiles(path + "*.*"))
			{
				if (fileInfo.Extension == ".csv")
				{
					string text = fileInfo.Name.Substring(0, fileInfo.Name.IndexOf("."));
					this.discoveredLanguages.Add(text);
				}
			}
		}
		if (this.discoveredLanguages.Count >= 1)
		{
			this.discoveredLanguages.Remove("English");
			this.discoveredLanguages.Insert(0, "English");
		}
	}

	// Token: 0x0600024F RID: 591 RVA: 0x0000C1D1 File Offset: 0x0000A3D1
	private void LoadFilesFromStreaming()
	{
		this.discoveredLanguages = new List<string>();
		this.LoadFilesFromPath("Language/");
		this.LoadFilesFromPath("Language/translations/");
	}

	// Token: 0x06000250 RID: 592 RVA: 0x0000C1F4 File Offset: 0x0000A3F4
	public string GetLanguageName()
	{
		return this.discoveredLanguages[this.chosenLanguageNum];
	}

	// Token: 0x06000251 RID: 593 RVA: 0x0000C208 File Offset: 0x0000A408
	public string GetAutoDetectLanguage()
	{
		foreach (LanguageManager.LanguageAndFont languageAndFont in this.languageAndFonts)
		{
			if (Application.systemLanguage == languageAndFont.systemLanguage)
			{
				return languageAndFont.name;
			}
		}
		if (Application.systemLanguage == SystemLanguage.Chinese)
		{
			return "chinese simplified";
		}
		return "english";
	}

	// Token: 0x06000252 RID: 594 RVA: 0x0000C280 File Offset: 0x0000A480
	public void ChooseLanguage(string name)
	{
		int num = 0;
		using (List<string>.Enumerator enumerator = this.discoveredLanguages.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == name)
				{
					this.ChooseLanguage(num);
					return;
				}
				num++;
			}
		}
		if (this.discoveredLanguages.Count >= 1)
		{
			this.ChooseLanguage(0);
		}
	}

	// Token: 0x06000253 RID: 595 RVA: 0x0000C2F8 File Offset: 0x0000A4F8
	public void ChooseLanguageViaString(string name)
	{
		int num = 0;
		using (List<string>.Enumerator enumerator = this.discoveredLanguages.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.ToLower() == name.ToLower())
				{
					Debug.Log("Found language: " + name);
					this.ChooseLanguage(num);
					return;
				}
				num++;
			}
		}
		Debug.Log("Could not find language: " + name);
		if (this.discoveredLanguages.Count >= 1)
		{
			this.ChooseLanguage(0);
		}
	}

	// Token: 0x06000254 RID: 596 RVA: 0x0000C39C File Offset: 0x0000A59C
	public void ChooseLanguage(int num)
	{
		this.chosenLanguageNum = num;
		foreach (LanguageManager.LanguageAndFont languageAndFont in this.languageAndFonts)
		{
			if (languageAndFont.name.ToLower() == this.discoveredLanguages[this.chosenLanguageNum].ToLower())
			{
				Debug.Log("Found font for language: " + languageAndFont.name + " - " + languageAndFont.font.name);
				this.chosenFont = languageAndFont.font;
			}
		}
		this.LoadLanguageFile();
	}

	// Token: 0x06000255 RID: 597 RVA: 0x0000C450 File Offset: 0x0000A650
	private string SearchForLanguageFileAtPath(string path)
	{
		string text = "";
		if (Application.platform == RuntimePlatform.WebGLPlayer && this.languageLocation == LanguageManager.LanguageLocation.StreamingAssets)
		{
			Debug.LogError("Cannot load files from StreamingAssets on WebGL");
			return "";
		}
		if (Application.platform == RuntimePlatform.WebGLPlayer || this.languageLocation == LanguageManager.LanguageLocation.Resources)
		{
			foreach (TextAsset textAsset in Resources.LoadAll<TextAsset>(path))
			{
				if (textAsset.name == this.discoveredLanguages[this.chosenLanguageNum])
				{
					text = textAsset.text;
					break;
				}
			}
		}
		else
		{
			foreach (FileInfo fileInfo in new DirectoryInfo(Application.streamingAssetsPath).GetFiles(path + "*.*"))
			{
				if (fileInfo.Extension == ".csv" && fileInfo.Name.Substring(0, fileInfo.Name.IndexOf(".")) == this.discoveredLanguages[this.chosenLanguageNum])
				{
					using (FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
					{
						using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
						{
							text = streamReader.ReadToEnd();
							break;
						}
					}
				}
			}
		}
		return text;
	}

	// Token: 0x06000256 RID: 598 RVA: 0x0000C5BC File Offset: 0x0000A7BC
	public void LoadLanguageFile()
	{
		this.languageTerms = new Dictionary<string, string>();
		string text = this.SearchForLanguageFileAtPath("Language/");
		if (text == "")
		{
			text = this.SearchForLanguageFileAtPath("Language/translations/");
		}
		bool flag = false;
		text = text.Replace("\"\"", "*");
		for (int i = 0; i < text.Length; i++)
		{
			if (text.Substring(i, 1) == "\"")
			{
				flag = !flag;
			}
			else
			{
				if (flag && text.Substring(i, 1) == ",")
				{
					text = text.Remove(i, 1);
					text = text.Insert(i, ";");
				}
				if (flag && text.Substring(i, 1) == "\n")
				{
					text = text.Remove(i, 1);
					text = text.Insert(i, "<br>");
				}
			}
		}
		text = text.Replace("*", "\"");
		string[] array = text.Split(new char[] { ',', '\n' });
		for (int j = 0; j < array.Length; j++)
		{
			array[j] = array[j].Replace(";", ",");
		}
		int num = 0;
		while (num + 1 < array.Length)
		{
			string text2 = array[num].ToLower().Trim();
			text2 = text2.Replace("\"", "");
			string text3 = array[num + 1].Trim();
			text3 = text3.Replace("\"", "");
			try
			{
				if (text2.Length > 0 && text3.Length > 0)
				{
					if (this.languageTerms.ContainsKey(text2))
					{
						Debug.Log("Already contains key" + text2);
						Debug.Log("At row " + (num / 2).ToString());
					}
					else
					{
						if (this.debugLogTerms)
						{
							Debug.Log(string.Concat(new string[]
							{
								text2,
								" ",
								text3,
								"  --At row ",
								(num / 2).ToString()
							}));
						}
						this.languageTerms.Add(text2, text3);
					}
				}
			}
			catch
			{
				Debug.Log("Error occured with key " + text2);
				Debug.Log("At row " + (num / 5).ToString());
			}
			num += 2;
		}
		LanguageManager.ReplaceAllTextAction onReplaceText = LanguageManager.OnReplaceText;
		if (onReplaceText == null)
		{
			return;
		}
		onReplaceText();
	}

	// Token: 0x06000257 RID: 599 RVA: 0x0000C844 File Offset: 0x0000AA44
	public bool TranslationFileLoaded()
	{
		return this.languageTerms != null;
	}

	// Token: 0x06000258 RID: 600 RVA: 0x0000C84F File Offset: 0x0000AA4F
	public bool KeyExists(string key)
	{
		key = key.ToLower().Trim();
		return this.languageTerms.ContainsKey(key);
	}

	// Token: 0x06000259 RID: 601 RVA: 0x0000C86C File Offset: 0x0000AA6C
	public string GetText(string key)
	{
		if (this.languageTerms == null)
		{
			return "No translation file loaded";
		}
		if (key == null)
		{
			return "";
		}
		key = key.ToLower().Trim();
		string text;
		try
		{
			text = this.languageTerms[key];
		}
		catch (KeyNotFoundException)
		{
			text = "key: " + key;
		}
		return text;
	}

	// Token: 0x0600025A RID: 602 RVA: 0x0000C8CC File Offset: 0x0000AACC
	public void SetFont(Transform t)
	{
		TextMeshProUGUI[] componentsInChildren = t.GetComponentsInChildren<TextMeshProUGUI>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].font = this.chosenFont;
		}
		TextMeshPro[] componentsInChildren2 = t.GetComponentsInChildren<TextMeshPro>();
		for (int i = 0; i < componentsInChildren2.Length; i++)
		{
			componentsInChildren2[i].font = this.chosenFont;
		}
	}

	// Token: 0x040001AF RID: 431
	private static LanguageManager _instance;

	// Token: 0x040001B0 RID: 432
	[SerializeField]
	public LanguageManager.LanguageLocation languageLocation;

	// Token: 0x040001B1 RID: 433
	[SerializeField]
	private bool debugLogTerms;

	// Token: 0x040001B2 RID: 434
	[SerializeField]
	public List<LanguageManager.LanguageAndFont> languageAndFonts;

	// Token: 0x040001B3 RID: 435
	public List<string> discoveredLanguages;

	// Token: 0x040001B4 RID: 436
	public int chosenLanguageNum;

	// Token: 0x040001B5 RID: 437
	public TMP_FontAsset chosenFont;

	// Token: 0x040001B6 RID: 438
	[SerializeField]
	private Dictionary<string, string> languageTerms;

	// Token: 0x020000E6 RID: 230
	public enum LanguageLocation
	{
		// Token: 0x0400045C RID: 1116
		StreamingAssets,
		// Token: 0x0400045D RID: 1117
		Resources
	}

	// Token: 0x020000E7 RID: 231
	[Serializable]
	public class LanguageAndFont
	{
		// Token: 0x0400045E RID: 1118
		public string name;

		// Token: 0x0400045F RID: 1119
		public TMP_FontAsset font;

		// Token: 0x04000460 RID: 1120
		public SystemLanguage systemLanguage;
	}

	// Token: 0x020000E8 RID: 232
	// (Invoke) Token: 0x0600055F RID: 1375
	public delegate void ReplaceAllTextAction();
}
