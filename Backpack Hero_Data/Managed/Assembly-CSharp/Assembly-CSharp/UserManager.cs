using System;
using System.IO;
using TMPro;
using UnityEngine;

// Token: 0x020001AB RID: 427
public class UserManager : MonoBehaviour
{
	// Token: 0x060010F7 RID: 4343 RVA: 0x000A0DC9 File Offset: 0x0009EFC9
	private void Awake()
	{
		if (UserManager.main)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		UserManager.main = this;
	}

	// Token: 0x060010F8 RID: 4344 RVA: 0x000A0DE9 File Offset: 0x0009EFE9
	private void OnDestroy()
	{
		if (UserManager.main == this)
		{
			UserManager.main = null;
		}
	}

	// Token: 0x060010F9 RID: 4345 RVA: 0x000A0DFE File Offset: 0x0009EFFE
	private void Start()
	{
		this.SetVersionNumber();
	}

	// Token: 0x060010FA RID: 4346 RVA: 0x000A0E08 File Offset: 0x0009F008
	private void SetVersionNumber()
	{
		string text = "0";
		if (File.Exists(Application.streamingAssetsPath + "/build.txt"))
		{
			text = File.ReadAllText(Application.streamingAssetsPath + "/build.txt");
		}
		this.versionText.text = "v" + text;
	}

	// Token: 0x060010FB RID: 4347 RVA: 0x000A0E5C File Offset: 0x0009F05C
	private void Update()
	{
		if (GameManager.main && GameManager.main.viewingEvent)
		{
			return;
		}
		if (DigitalCursor.main.GetInputDown("pause") || Input.GetKeyDown(KeyCode.Escape))
		{
			this.ShowPauseMenu();
		}
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x000A0E97 File Offset: 0x0009F097
	public void ShowPauseMenu()
	{
		if (SingleUI.IsViewingPopUp())
		{
			return;
		}
		Object.Instantiate<GameObject>(this.pauseMenu, GameObject.FindGameObjectWithTag("UI Canvas").transform).transform.localPosition = Vector3.zero;
	}

	// Token: 0x060010FD RID: 4349 RVA: 0x000A0ECA File Offset: 0x0009F0CA
	public void ShowOptionsMenu()
	{
		if (Overworld_ConversationManager.main.InLockedConversation())
		{
			return;
		}
		Object.Instantiate<GameObject>(this.optionsMenu, GameObject.FindGameObjectWithTag("UI Canvas").transform).transform.localPosition = Vector3.zero;
	}

	// Token: 0x060010FE RID: 4350 RVA: 0x000A0F02 File Offset: 0x0009F102
	public void ShowItemAtlas()
	{
		if (Overworld_ConversationManager.main.InLockedConversation())
		{
			return;
		}
		Object.Instantiate<GameObject>(this.itemAtlas, GameObject.FindGameObjectWithTag("UI Canvas").transform).transform.localPosition = Vector3.zero;
	}

	// Token: 0x04000DD3 RID: 3539
	public static UserManager main;

	// Token: 0x04000DD4 RID: 3540
	[SerializeField]
	private GameObject pauseMenu;

	// Token: 0x04000DD5 RID: 3541
	[SerializeField]
	private GameObject optionsMenu;

	// Token: 0x04000DD6 RID: 3542
	[SerializeField]
	private GameObject itemAtlas;

	// Token: 0x04000DD7 RID: 3543
	[SerializeField]
	private TextMeshProUGUI versionText;
}
