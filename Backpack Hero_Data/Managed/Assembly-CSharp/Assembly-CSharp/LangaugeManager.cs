using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x02000108 RID: 264
public class LangaugeManager : MonoBehaviour
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06000905 RID: 2309 RVA: 0x0005DFB4 File Offset: 0x0005C1B4
	// (remove) Token: 0x06000906 RID: 2310 RVA: 0x0005DFE8 File Offset: 0x0005C1E8
	public static event LangaugeManager.LanguageChanged OnLanguageChanged;

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000907 RID: 2311 RVA: 0x0005E01B File Offset: 0x0005C21B
	public static LangaugeManager main
	{
		get
		{
			return LangaugeManager._instance;
		}
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x0005E022 File Offset: 0x0005C222
	public Dictionary<string, string> GetTerms()
	{
		return this.languageTerms;
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x0005E02A File Offset: 0x0005C22A
	private void Awake()
	{
		if (LangaugeManager._instance != null && LangaugeManager._instance != this)
		{
			Object.Destroy(base.gameObject);
		}
		else
		{
			LangaugeManager._instance = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}
		this.LoadFilesFromStreaming();
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x0005E06A File Offset: 0x0005C26A
	private void Update()
	{
		if (Keyboard.current != null && Keyboard.current[Key.Semicolon].isPressed)
		{
			this.LoadLanguageFile();
		}
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x0005E08C File Offset: 0x0005C28C
	private void LoadFilesFromPath(string path)
	{
		if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
			TextAsset[] array = Resources.LoadAll<TextAsset>(path);
			for (int i = 0; i < array.Length; i++)
			{
				string name = array[i].name;
				this.discoveredLanguages.Add(name);
			}
			return;
		}
		foreach (FileInfo fileInfo in new DirectoryInfo(Application.streamingAssetsPath).GetFiles(path + "*.*"))
		{
			if (fileInfo.Extension == ".csv" && !fileInfo.Name.EndsWith("-fixed.csv"))
			{
				string text = fileInfo.Name.Substring(0, fileInfo.Name.IndexOf("."));
				this.discoveredLanguages.Add(text);
			}
		}
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x0005E14E File Offset: 0x0005C34E
	private void LoadFilesFromStreaming()
	{
		this.discoveredLanguages = new List<string>();
		this.LoadFilesFromPath("Language/");
		this.LoadFilesFromPath("Language/translations/");
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x0005E171 File Offset: 0x0005C371
	public string GetLanguageName()
	{
		return this.discoveredLanguages[this.chosenLanguageNum];
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x0005E184 File Offset: 0x0005C384
	public void SetLanguage(string name)
	{
		Debug.Log("Language: " + name);
		int num = 0;
		using (List<string>.Enumerator enumerator = this.discoveredLanguages.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == name)
				{
					this.ChooseLanague(num);
					return;
				}
				num++;
			}
		}
		if (this.discoveredLanguages.Count >= 1)
		{
			this.ChooseLanague(0);
		}
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x0005E20C File Offset: 0x0005C40C
	public void ChooseLanague(int num)
	{
		Debug.Log("Language" + this.discoveredLanguages[this.chosenLanguageNum]);
		this.chosenLanguageNum = num;
		this.chosenFont = this.unicodeFont;
		foreach (LangaugeManager.LanguageAndFont languageAndFont in this.languageAndFonts)
		{
			if (languageAndFont.name.ToLower() == this.discoveredLanguages[this.chosenLanguageNum].ToLower())
			{
				Debug.Log("Found font for language: " + languageAndFont.name + " - " + languageAndFont.font.name);
				this.chosenFont = languageAndFont.font;
			}
		}
		this.LoadLanguageFile();
		LangaugeManager.LanguageChanged onLanguageChanged = LangaugeManager.OnLanguageChanged;
		if (onLanguageChanged == null)
		{
			return;
		}
		onLanguageChanged();
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x0005E2FC File Offset: 0x0005C4FC
	private string SearchForLanguageFileAtPath(string path)
	{
		string text = "";
		path = path.Replace(".csv", "-fixed.csv");
		if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
			foreach (TextAsset textAsset in Resources.LoadAll<TextAsset>(path))
			{
				if (textAsset.name == this.discoveredLanguages[this.chosenLanguageNum])
				{
					text = textAsset.text;
					return text;
				}
			}
		}
		else
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			Debug.Log("Start csv load");
			FileInfo[] files = new DirectoryInfo(Application.streamingAssetsPath).GetFiles(path + "*.*");
			Debug.Log(string.Concat(new string[]
			{
				"Found ",
				files.Length.ToString(),
				" files. ",
				stopwatch.ElapsedMilliseconds.ToString(),
				"ms"
			}));
			int num = 0;
			foreach (FileInfo fileInfo in files)
			{
				Debug.Log(string.Concat(new string[]
				{
					string.Format("File {0} -> ", num),
					fileInfo.Name,
					" ",
					stopwatch.ElapsedMilliseconds.ToString(),
					"ms"
				}));
				if (fileInfo.Extension == ".csv")
				{
					string text2 = fileInfo.Name.Replace("-fixed", "");
					if (fileInfo.Name.Substring(0, text2.IndexOf(".")) == this.discoveredLanguages[this.chosenLanguageNum])
					{
						Debug.Log("Read file " + stopwatch.ElapsedMilliseconds.ToString() + "ms");
						using (FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
						{
							using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
							{
								text = streamReader.ReadToEnd();
							}
						}
						Debug.Log("Read file done " + stopwatch.ElapsedMilliseconds.ToString() + "ms");
						return text;
					}
				}
			}
		}
		return "";
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x0005E568 File Offset: 0x0005C768
	public void LoadLanguageFile()
	{
		Stopwatch stopwatch = new Stopwatch();
		this.termsLoaded = false;
		this.languageTerms = new Dictionary<string, string>();
		string text = this.SearchForLanguageFileAtPath("Language/");
		if (text == "")
		{
			text = this.SearchForLanguageFileAtPath("Language/translations/");
		}
		string[] array = text.Split(new char[] { ',', '\n' });
		int num = 5;
		while (num + 5 < array.Length)
		{
			string text2 = array[num + 1].ToLower().Trim();
			text2 = text2.Replace("\"", "");
			string text3 = array[num + 2].Trim();
			string text4 = array[num + 3].Trim();
			if (text4.Contains("WARNING:"))
			{
				text4 = text3;
			}
			try
			{
				if (text2.Length > 0 && text4.Length > 0)
				{
					text4 = text4.Replace("^", ",");
					if (text4.Substring(0, 1) == "\"")
					{
						text4 = text4.Remove(0, 1);
					}
					if (text4.Substring(text4.Length - 1, 1) == "\"")
					{
						text4 = text4.Remove(text4.Length - 1, 1);
					}
					if (this.languageTerms.ContainsKey(text2))
					{
						Debug.Log("Already contains key" + text2);
						Debug.Log("At row " + (num / 5).ToString());
					}
					else
					{
						this.languageTerms.Add(text2, text4);
					}
				}
			}
			catch
			{
				Debug.Log("Error occured with key " + text2);
				Debug.Log("At row " + (num / 5).ToString());
			}
			num += 5;
		}
		this.ReplaceAllText();
		CardManager cardManager = Object.FindObjectOfType<CardManager>();
		if (cardManager)
		{
			cardManager.SetUpDescriptions();
		}
		this.loadedLanguage = true;
		Debug.Log("Loaded Language File: " + stopwatch.ElapsedMilliseconds.ToString() + "ms");
		this.termsLoaded = true;
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x0005E788 File Offset: 0x0005C988
	private void ReplaceAllText()
	{
		Player main = Player.main;
		if (main)
		{
			main.AddExperience(0, main.transform.position);
		}
		Options options = Object.FindObjectOfType<Options>();
		if (options)
		{
			options.SetOptionLanguage();
		}
		ReplacementText[] array = Object.FindObjectsOfType<ReplacementText>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].ReplaceText();
		}
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x0005E7E8 File Offset: 0x0005C9E8
	public bool KeyExists(string key)
	{
		key = key.ToLower().Trim();
		bool flag;
		try
		{
			flag = this.languageTerms.ContainsKey(key) || (ModLoader.main && ModLoader.main.languageTerms[this.GetLanguageName().ToLower()].ContainsKey(key)) || (ModLoader.main && ModLoader.main.languageTerms["english"].ContainsKey(key));
		}
		catch (KeyNotFoundException)
		{
			flag = false;
		}
		return flag;
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x0005E884 File Offset: 0x0005CA84
	public bool TranslationFileLoaded()
	{
		return this.languageTerms != null && this.termsLoaded;
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x0005E898 File Offset: 0x0005CA98
	public string GetTextByKey(string key)
	{
		if (this.languageTerms == null && (ModLoader.main == null || ModLoader.main.languageTerms == null))
		{
			return "No translation file loaded";
		}
		if (key == null)
		{
			return "";
		}
		key = key.ToLower().Trim();
		try
		{
			return this.languageTerms[key];
		}
		catch (Exception ex)
		{
			if (!(ex is KeyNotFoundException) && !(ex is NullReferenceException))
			{
				throw;
			}
			try
			{
				if (ModLoader.main)
				{
					return ModLoader.main.languageTerms[this.GetLanguageName().ToLower()][key];
				}
			}
			catch (KeyNotFoundException)
			{
				try
				{
					if (ModLoader.main)
					{
						return ModLoader.main.languageTerms["english"][key];
					}
				}
				catch (KeyNotFoundException)
				{
					return "Translation not found for key " + key;
				}
			}
		}
		return "Translation not found for key " + key;
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x0005E9B0 File Offset: 0x0005CBB0
	public void SetFont(Transform t)
	{
		foreach (TextMeshProUGUI textMeshProUGUI in t.GetComponentsInChildren<TextMeshProUGUI>())
		{
			if (!textMeshProUGUI.CompareTag("NumericOnlyText") && (!textMeshProUGUI.CompareTag("EnglishOverrideText") || !(this.chosenFont == this.languageAndFonts[0].font)))
			{
				textMeshProUGUI.font = this.chosenFont;
				if (this.chosenLanguageNum == 0)
				{
					if (textMeshProUGUI.fontStyle != FontStyles.Italic)
					{
						textMeshProUGUI.fontStyle = FontStyles.Normal;
					}
					else if (textMeshProUGUI.fontStyle == FontStyles.Italic)
					{
						textMeshProUGUI.fontStyle = FontStyles.Italic;
					}
				}
				else
				{
					textMeshProUGUI.fontStyle = FontStyles.Bold;
				}
				textMeshProUGUI.lineSpacing = -40f;
			}
		}
		foreach (TextMeshPro textMeshPro in t.GetComponentsInChildren<TextMeshPro>())
		{
			if (!textMeshPro.CompareTag("NumericOnlyText") && (!textMeshPro.CompareTag("EnglishOverrideText") || !(this.chosenFont == this.languageAndFonts[0].font)))
			{
				textMeshPro.font = this.chosenFont;
				if (this.chosenLanguageNum == 0)
				{
					if (textMeshPro.fontStyle != FontStyles.Italic)
					{
						textMeshPro.fontStyle = FontStyles.Normal;
					}
					else if (textMeshPro.fontStyle == FontStyles.Italic)
					{
						textMeshPro.fontStyle = FontStyles.Italic;
					}
				}
				else
				{
					textMeshPro.fontStyle = FontStyles.Bold;
				}
				textMeshPro.lineSpacing = -40f;
			}
		}
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x0005EB0C File Offset: 0x0005CD0C
	public string GetRandomTextFromMasterKey(string masterKey, int length = 10)
	{
		List<string> list = new List<string>();
		for (int i = 0; i < 10; i++)
		{
			string text = masterKey + i.ToString();
			if (LangaugeManager.main.KeyExists(text))
			{
				list.Add(LangaugeManager.main.GetTextByKey(text));
			}
		}
		if (list.Count == 0)
		{
			return "Translation not found for masterkey " + masterKey;
		}
		int num = Random.Range(0, list.Count);
		return list[num];
	}

	// Token: 0x04000723 RID: 1827
	private static LangaugeManager _instance;

	// Token: 0x04000724 RID: 1828
	[SerializeField]
	public List<LangaugeManager.LanguageAndFont> languageAndFonts;

	// Token: 0x04000725 RID: 1829
	public List<string> discoveredLanguages;

	// Token: 0x04000726 RID: 1830
	public TMP_FontAsset unicodeFont;

	// Token: 0x04000727 RID: 1831
	public int chosenLanguageNum;

	// Token: 0x04000728 RID: 1832
	public TMP_FontAsset chosenFont;

	// Token: 0x04000729 RID: 1833
	[SerializeField]
	private Dictionary<string, string> languageTerms;

	// Token: 0x0400072A RID: 1834
	public bool loadedLanguage;

	// Token: 0x0400072B RID: 1835
	private bool termsLoaded;

	// Token: 0x0400072C RID: 1836
	private TMP_FontAsset tMP_FontAsset;

	// Token: 0x02000380 RID: 896
	// (Invoke) Token: 0x06001717 RID: 5911
	public delegate void LanguageChanged();

	// Token: 0x02000381 RID: 897
	[Serializable]
	public class LanguageAndFont
	{
		// Token: 0x04001533 RID: 5427
		public string name;

		// Token: 0x04001534 RID: 5428
		public TMP_FontAsset font;
	}
}
