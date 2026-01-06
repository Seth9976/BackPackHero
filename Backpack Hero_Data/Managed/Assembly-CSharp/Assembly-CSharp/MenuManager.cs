using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000125 RID: 293
public class MenuManager : MonoBehaviour
{
	// Token: 0x06000AF0 RID: 2800 RVA: 0x0006F816 File Offset: 0x0006DA16
	private void OnEnable()
	{
		MenuManager.main = this;
	}

	// Token: 0x06000AF1 RID: 2801 RVA: 0x0006F81E File Offset: 0x0006DA1E
	private void OnDisable()
	{
		if (MenuManager.main == this)
		{
			MenuManager.main = null;
		}
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x0006F833 File Offset: 0x0006DA33
	private void Awake()
	{
	}

	// Token: 0x06000AF3 RID: 2803 RVA: 0x0006F838 File Offset: 0x0006DA38
	private void Start()
	{
		Singleton.Instance.townSaveBackup = null;
		DigitalCursor.main.Hide();
		base.StartCoroutine(this.SetStartText());
		SoundManager.main.StopAllSongs();
		string text = "bph_game_title";
		if (EventManager.instance != null)
		{
			switch (EventManager.instance.eventType)
			{
			case EventManager.EventType.Winter:
				text = "bph_game_title_winter";
				goto IL_0082;
			case EventManager.EventType.Halloween:
				text = "bph_game_title_halloween";
				goto IL_0082;
			case EventManager.EventType.Summer:
				text = "bph_game_title_summer";
				goto IL_0082;
			}
			text = "bph_game_title";
		}
		IL_0082:
		SoundManager.main.PlaySongSudden(text, 0f, 0f, true);
		SoundManager.main.ChangeAllVolume();
		this.characterAnimator.Play("Player_Win");
		Object.FindObjectOfType<OptionsSaveManager>().ApplySettings();
		if (Singleton.Instance.allowOptions)
		{
			this.optionsButton.SetActive(true);
		}
		else
		{
			this.optionsButton.SetActive(false);
		}
		if (Singleton.Instance.showQRCode)
		{
			this.qrCode.SetActive(true);
			return;
		}
		this.qrCode.SetActive(false);
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x0006F94C File Offset: 0x0006DB4C
	public void UpdateStartText()
	{
		base.StartCoroutine(this.SetStartText());
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x0006F95B File Offset: 0x0006DB5B
	private IEnumerator SetStartText()
	{
		while (!LangaugeManager.main.TranslationFileLoaded())
		{
			yield return null;
		}
		string text = LangaugeManager.main.GetTextByKey("pressToStart");
		if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.cursor)
		{
			text = text.Replace("/x", DigitalCursor.main.GetSpriteAtlasForKey("confirm"));
		}
		else if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.controller)
		{
			text = text.Replace("/x", DigitalCursor.main.GetSpriteAtlasForKey("confirm"));
		}
		this.startText.text = text;
		LangaugeManager.main.SetFont(this.startText.transform);
		yield break;
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x0006F96C File Offset: 0x0006DB6C
	public void CreatePopUp(string text, Vector2 localPoint, float speed)
	{
		Canvas canvas = Object.FindObjectOfType<Canvas>();
		GameObject gameObject = Object.Instantiate<GameObject>(this.popUpPrefab, Vector3.zero, Quaternion.identity, canvas.transform);
		gameObject.GetComponentInChildren<TextMeshProUGUI>().text = text;
		gameObject.GetComponent<RectTransform>().anchoredPosition = localPoint;
		gameObject.GetComponentInChildren<Animator>().speed = speed;
		gameObject.transform.SetAsLastSibling();
		LangaugeManager.main.SetFont(gameObject.transform);
	}

	// Token: 0x06000AF7 RID: 2807 RVA: 0x0006F9DC File Offset: 0x0006DBDC
	private void Update()
	{
		if (Input.GetKey("space") || Input.GetMouseButton(0) || DigitalCursor.main.GetInputDown("confirm") || DigitalCursor.main.GetInputDown("cancel"))
		{
			if (!this.started)
			{
				Object.FindObjectOfType<MainMenuPhysics>().started = true;
				this.started = true;
			}
			this.mainAnimator.SetTrigger("continue");
			DigitalCursor.main.Show();
		}
	}

	// Token: 0x06000AF8 RID: 2808 RVA: 0x0006FA53 File Offset: 0x0006DC53
	public void HideButtons()
	{
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x0006FA55 File Offset: 0x0006DC55
	public void ShowButtons()
	{
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x0006FA57 File Offset: 0x0006DC57
	public void LoadDataSwitch()
	{
		Object.FindObjectOfType<OptionsSaveManager>().DoLoad();
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x0006FA64 File Offset: 0x0006DC64
	public void StartGameButton()
	{
		Singleton.Instance.storyMode = false;
		MetaProgressSaveManager.main.SetupMetaData(-1);
		Singleton.Instance.doTutorial = false;
		Singleton.Instance.completedTutorial = true;
		Singleton.Instance.loadSave = false;
		int lowestSave = Singleton.Instance.GetLowestSave();
		Singleton.Instance.saveNumber = lowestSave;
		Object.Instantiate<GameObject>(this.characterSelectorPrefab, Vector3.zero, Quaternion.identity, Object.FindObjectOfType<Canvas>().transform);
	}

	// Token: 0x06000AFC RID: 2812 RVA: 0x0006FAE0 File Offset: 0x0006DCE0
	public void OpenSaveMenuManager()
	{
		Singleton.Instance.storyMode = false;
		MetaProgressSaveManager.main.SetupMetaData(-1);
		this.HideButtons();
		SoundManager.main.PlaySFX("menuBlip");
		Object.Instantiate<GameObject>(this.saveMenuManagerPrefab, Vector3.zero, Quaternion.identity, Object.FindObjectOfType<Canvas>().transform);
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x0006FB38 File Offset: 0x0006DD38
	public void GoAdventuring()
	{
		RunType runType = Singleton.Instance.runType;
		Singleton.Instance.storyMode = false;
		Singleton.Instance.mission = null;
		if (RunTypeManager.CheckForRunPropertyInRunType(RunType.RunProperty.Type.doTutorial, runType) != null)
		{
			Singleton.Instance.doTutorial = true;
			Singleton.Instance.completedTutorial = false;
		}
		this.freezeMenu = true;
		SceneLoader.main.LoadScene("Game", LoadSceneMode.Single, null, null);
	}

	// Token: 0x06000AFE RID: 2814 RVA: 0x0006FBA0 File Offset: 0x0006DDA0
	public void LoadGameAndContinueSave()
	{
		this.freezeMenu = true;
		SoundManager.main.PlaySFX("menuBlip");
		Singleton.Instance.mission = null;
		Singleton.Instance.storyMode = false;
		Singleton.Instance.doTutorial = false;
		Singleton.Instance.completedTutorial = true;
		Singleton.Instance.loadSave = true;
		Singleton.Instance.runType = null;
		Singleton.Instance.StartGameScene();
		SceneLoader.main.LoadScene("Game", LoadSceneMode.Single, null, null);
	}

	// Token: 0x06000AFF RID: 2815 RVA: 0x0006FC21 File Offset: 0x0006DE21
	public void QuitGame()
	{
		Application.Quit();
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x0006FC28 File Offset: 0x0006DE28
	public void OpenOptions()
	{
		this.HideButtons();
		SoundManager.main.PlaySFX("menuBlip");
		GameObject gameObject = Object.Instantiate<GameObject>(this.optionsPrefab, Vector3.zero, Quaternion.identity, Object.FindObjectOfType<Canvas>().transform);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
	}

	// Token: 0x06000B01 RID: 2817 RVA: 0x0006FC88 File Offset: 0x0006DE88
	public void OpenMods()
	{
		this.HideButtons();
		SoundManager.main.PlaySFX("menuBlip");
		GameObject gameObject = Object.Instantiate<GameObject>(this.modSelectionPrefab, Vector3.zero, Quaternion.identity, Object.FindObjectOfType<Canvas>().transform);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
	}

	// Token: 0x06000B02 RID: 2818 RVA: 0x0006FCE8 File Offset: 0x0006DEE8
	public void OpenCredits()
	{
		this.HideButtons();
		SoundManager.main.PlaySFX("menuBlip");
		GameObject gameObject = GameObject.FindGameObjectWithTag("UI Canvas");
		if (!gameObject)
		{
			gameObject = Object.FindObjectOfType<Canvas>().gameObject;
		}
		GameObject gameObject2 = Object.Instantiate<GameObject>(this.creditsPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
		gameObject2.transform.localPosition = Vector3.zero;
		gameObject2.transform.localScale = Vector3.one;
	}

	// Token: 0x06000B03 RID: 2819 RVA: 0x0006FD62 File Offset: 0x0006DF62
	public void OpenSteam()
	{
		Application.OpenURL("https://store.steampowered.com/app/1970580/Backpack_Hero/");
	}

	// Token: 0x040008DC RID: 2268
	public static MenuManager main;

	// Token: 0x040008DD RID: 2269
	public bool freezeMenu;

	// Token: 0x040008DE RID: 2270
	[SerializeField]
	private RunType tutorialRunType;

	// Token: 0x040008DF RID: 2271
	[SerializeField]
	private Animator mainAnimator;

	// Token: 0x040008E0 RID: 2272
	[SerializeField]
	private GameObject allButtons;

	// Token: 0x040008E1 RID: 2273
	[SerializeField]
	private GameObject optionsButton;

	// Token: 0x040008E2 RID: 2274
	[SerializeField]
	private GameObject startGameSimpleButton;

	// Token: 0x040008E3 RID: 2275
	[SerializeField]
	private GameObject startGameButton;

	// Token: 0x040008E4 RID: 2276
	[SerializeField]
	private GameObject matthewButton;

	// Token: 0x040008E5 RID: 2277
	[SerializeField]
	private GameObject continueButton;

	// Token: 0x040008E6 RID: 2278
	[SerializeField]
	private GameObject optionsPrefab;

	// Token: 0x040008E7 RID: 2279
	[SerializeField]
	private GameObject modSelectionPrefab;

	// Token: 0x040008E8 RID: 2280
	[SerializeField]
	private GameObject creditsPrefab;

	// Token: 0x040008E9 RID: 2281
	[SerializeField]
	private CostumeSelector costumeSelector;

	// Token: 0x040008EA RID: 2282
	[SerializeField]
	private GameObject characterSelectorPrefab;

	// Token: 0x040008EB RID: 2283
	[SerializeField]
	private GameObject tooManySavesPrefab;

	// Token: 0x040008EC RID: 2284
	[SerializeField]
	private GameObject popUpPrefab;

	// Token: 0x040008ED RID: 2285
	[SerializeField]
	private GameObject saveMenuManagerPrefab;

	// Token: 0x040008EE RID: 2286
	[SerializeField]
	private Animator characterAnimator;

	// Token: 0x040008EF RID: 2287
	[SerializeField]
	private TextMeshProUGUI startText;

	// Token: 0x040008F0 RID: 2288
	[SerializeField]
	private GameObject qrCode;

	// Token: 0x040008F1 RID: 2289
	private bool started;

	// Token: 0x040008F2 RID: 2290
	private int buttonNum;
}
