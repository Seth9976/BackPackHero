using System;
using UnityEngine;

// Token: 0x0200018B RID: 395
public class PauseMenu : MonoBehaviour
{
	// Token: 0x06000FFA RID: 4090 RVA: 0x0009B0F0 File Offset: 0x000992F0
	private void OnEnable()
	{
		PauseMenu.isClosing = false;
	}

	// Token: 0x06000FFB RID: 4091 RVA: 0x0009B0F8 File Offset: 0x000992F8
	private void Start()
	{
		if (GameManager.main)
		{
			GameManager.main.viewingEvent = true;
		}
		if (!GameManager.main)
		{
			this.statsButton.SetActive(false);
		}
		if (Singleton.Instance.storyMode && GameManager.main)
		{
			this.returnToOrderiaButton.SetActive(true);
		}
		else
		{
			this.returnToOrderiaButton.SetActive(false);
		}
		this.eventBoxAnimator = base.GetComponentInChildren<Animator>();
		DigitalCursor.main.SelectUIElement(this.startingSelection);
		DigitalCursor.main.Show();
		LevelUpManager levelUpManager = Object.FindObjectOfType<LevelUpManager>();
		if (levelUpManager && levelUpManager.CanLevelUp() && GameFlowManager.main.battlePhase == GameFlowManager.BattlePhase.outOfBattle)
		{
			this.levelUpOptionButton.SetActive(true);
		}
		else
		{
			this.levelUpOptionButton.SetActive(false);
		}
		if (!Singleton.Instance.allowOptions)
		{
			this.optionsButton.SetActive(false);
		}
	}

	// Token: 0x06000FFC RID: 4092 RVA: 0x0009B1E0 File Offset: 0x000993E0
	private void Update()
	{
		if (!base.transform.GetChild(0) || !base.transform.GetChild(0).gameObject.activeInHierarchy)
		{
			if (GameManager.main)
			{
				GameManager.main.viewingEvent = false;
			}
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000FFD RID: 4093 RVA: 0x0009B23C File Offset: 0x0009943C
	public static void SaveGame()
	{
		if (PauseMenu.isClosing)
		{
			return;
		}
		if (GameManager.main)
		{
			bool viewingEvent = GameManager.main.viewingEvent;
			GameManager.main.viewingEvent = false;
			GameManager.main.SaveGame();
			GameManager.main.viewingEvent = viewingEvent;
			return;
		}
		Object.FindObjectOfType<Overworld_SaveManager>().SaveCommand(true);
	}

	// Token: 0x06000FFE RID: 4094 RVA: 0x0009B294 File Offset: 0x00099494
	public void LevelUp()
	{
		if (PauseMenu.isClosing)
		{
			return;
		}
		GameManager.main.viewingEvent = false;
		this.CloseAndDestroy();
		Object.FindObjectOfType<LevelUpManager>().LevelUpButton();
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x0009B2BC File Offset: 0x000994BC
	public void OpenOptions()
	{
		if (PauseMenu.isClosing)
		{
			return;
		}
		Debug.Log("Options from pause menu");
		PauseMenu.isClosing = true;
		if (GameManager.main)
		{
			GameManager.main.viewingEvent = false;
		}
		this.Close();
		UserManager.main.ShowOptionsMenu();
	}

	// Token: 0x06001000 RID: 4096 RVA: 0x0009B308 File Offset: 0x00099508
	public void OpenAtlas()
	{
		if (PauseMenu.isClosing)
		{
			return;
		}
		PauseMenu.isClosing = true;
		Debug.Log("Atlas from pause menu");
		if (GameManager.main)
		{
			GameManager.main.viewingEvent = false;
		}
		this.Close();
		UserManager.main.ShowItemAtlas();
	}

	// Token: 0x06001001 RID: 4097 RVA: 0x0009B354 File Offset: 0x00099554
	public void OpenStats()
	{
		if (PauseMenu.isClosing)
		{
			return;
		}
		PauseMenu.isClosing = true;
		GameManager.main.viewingEvent = false;
		this.Close();
		GameManager.main.ShowStats();
	}

	// Token: 0x06001002 RID: 4098 RVA: 0x0009B37F File Offset: 0x0009957F
	public void OpenControls()
	{
		if (PauseMenu.isClosing)
		{
			return;
		}
		PauseMenu.isClosing = true;
		this.Close();
		Object.Instantiate<GameObject>(this.controlsInstructions, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").transform);
	}

	// Token: 0x06001003 RID: 4099 RVA: 0x0009B3BA File Offset: 0x000995BA
	public static void QuitGame(bool overrideIronMan = false)
	{
		Options.QuitGame(false);
	}

	// Token: 0x06001004 RID: 4100 RVA: 0x0009B3C2 File Offset: 0x000995C2
	public void Close()
	{
	}

	// Token: 0x06001005 RID: 4101 RVA: 0x0009B3C4 File Offset: 0x000995C4
	public void CloseAndDestroy()
	{
		PauseMenu.isClosing = true;
		this.singleUI.DestroyImmediate();
	}

	// Token: 0x04000D22 RID: 3362
	[SerializeField]
	private SingleUI singleUI;

	// Token: 0x04000D23 RID: 3363
	[SerializeField]
	private GameObject startingSelection;

	// Token: 0x04000D24 RID: 3364
	[SerializeField]
	private Animator eventBoxAnimator;

	// Token: 0x04000D25 RID: 3365
	[SerializeField]
	private GameObject levelUpOptionButton;

	// Token: 0x04000D26 RID: 3366
	[SerializeField]
	private GameObject statsButton;

	// Token: 0x04000D27 RID: 3367
	[SerializeField]
	private GameObject controlsInstructions;

	// Token: 0x04000D28 RID: 3368
	[SerializeField]
	private GameObject optionsButton;

	// Token: 0x04000D29 RID: 3369
	[SerializeField]
	private GameObject returnToOrderiaButton;

	// Token: 0x04000D2A RID: 3370
	private static bool isClosing;
}
