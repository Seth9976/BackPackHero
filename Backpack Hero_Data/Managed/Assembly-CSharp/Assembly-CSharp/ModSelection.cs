using System;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000189 RID: 393
public class ModSelection : MonoBehaviour
{
	// Token: 0x06000FC8 RID: 4040 RVA: 0x000998A0 File Offset: 0x00097AA0
	private void Start()
	{
		this.mask.enabled = true;
		GameObject gameObject = GameObject.FindGameObjectWithTag("PopUpSpriteMask");
		if (gameObject)
		{
			gameObject.GetComponent<SpriteMask>().enabled = false;
		}
		this.LoadData();
		if (SteamManager.enabled)
		{
			this.GetModsButton.SetActive(true);
		}
	}

	// Token: 0x06000FC9 RID: 4041 RVA: 0x000998F4 File Offset: 0x00097AF4
	public void LoadData()
	{
		while (this.modListParent.childCount > 0)
		{
			Object.DestroyImmediate(this.modListParent.GetChild(0).gameObject);
		}
		if (ModLoader.main != null)
		{
			foreach (ModLoader.ModpackInfo modpackInfo in ModLoader.main.modpacks)
			{
				Object.Instantiate<GameObject>(this.modpackInfoPrefab, this.modListParent).GetComponent<ModpackInfoDisplay>().SetModpack(modpackInfo);
			}
		}
	}

	// Token: 0x06000FCA RID: 4042 RVA: 0x00099994 File Offset: 0x00097B94
	public void SetSliderMenu(Transform transform)
	{
		if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		RectTransform component = transform.GetComponent<RectTransform>();
		RectTransform component2 = transform.parent.GetComponent<RectTransform>();
		RectTransform component3 = transform.parent.parent.parent.GetComponent<RectTransform>();
		component2.anchoredPosition = new Vector2(0f, component2.rect.height - (float)(transform.parent.childCount - transform.GetSiblingIndex()) * component.rect.height - component3.rect.height / 2f);
	}

	// Token: 0x06000FCB RID: 4043 RVA: 0x00099A2D File Offset: 0x00097C2D
	public void SetFont(Transform t)
	{
		LangaugeManager.main.SetFont(t);
	}

	// Token: 0x06000FCC RID: 4044 RVA: 0x00099A3C File Offset: 0x00097C3C
	private void Update()
	{
		if (this.timePassed < 0.5f)
		{
			this.timePassed += Time.deltaTime;
		}
		this.debuggingToggle.isOn = Singleton.Instance.modDebugging;
		if (!this.eventBoxAnimator.gameObject.activeInHierarchy)
		{
			Object.Destroy(base.gameObject);
		}
		if (ModLoader.main.dataReady)
		{
			this.LoadData();
			ModLoader.main.dataReady = false;
		}
	}

	// Token: 0x06000FCD RID: 4045 RVA: 0x00099AB7 File Offset: 0x00097CB7
	public void ChangeToggles()
	{
		if (this.timePassed < 0.5f)
		{
			return;
		}
		ItemMovement.ConsiderChangingAllShaders();
	}

	// Token: 0x06000FCE RID: 4046 RVA: 0x00099ACC File Offset: 0x00097CCC
	public void OpenLink(string link)
	{
		Application.OpenURL(link);
	}

	// Token: 0x06000FCF RID: 4047 RVA: 0x00099AD4 File Offset: 0x00097CD4
	public void ToggleDebugging(Toggle toggle)
	{
		if (toggle.isOn == Singleton.Instance.modDebugging)
		{
			return;
		}
		if (toggle.isOn)
		{
			ConfirmationManager.CreateConfirmation(LangaugeManager.main.GetTextByKey("modDebugWarning"), new Func<Action>(this.EnableDebuggingInternal));
			return;
		}
		Singleton.Instance.modDebugging = false;
		ModMetaSave.SaveModData();
	}

	// Token: 0x06000FD0 RID: 4048 RVA: 0x00099B2D File Offset: 0x00097D2D
	private Action EnableDebuggingInternal()
	{
		Singleton.Instance.modDebugging = true;
		ModMetaSave.SaveModData();
		return null;
	}

	// Token: 0x06000FD1 RID: 4049 RVA: 0x00099B40 File Offset: 0x00097D40
	public void ReloadModpacks()
	{
		ModLoader.main.ReloadModpacks();
		ModMetaSave.SaveModData();
	}

	// Token: 0x06000FD2 RID: 4050 RVA: 0x00099B54 File Offset: 0x00097D54
	private void PopUp(string text, Vector2 position, float time)
	{
		position = base.GetComponentInParent<Canvas>().transform.InverseTransformPoint(position);
		MenuManager menuManager = Object.FindObjectOfType<MenuManager>();
		if (menuManager)
		{
			menuManager.CreatePopUp(text, position, time);
			return;
		}
		GameManager main = GameManager.main;
		if (main)
		{
			main.CreatePopUp(text, position, time);
		}
	}

	// Token: 0x06000FD3 RID: 4051 RVA: 0x00099BAD File Offset: 0x00097DAD
	public void OpenWorkshop()
	{
		SteamFriends.ActivateGameOverlayToWebPage("steam://url/SteamWorkshopPage/1970580", EActivateGameOverlayToWebPageMode.k_EActivateGameOverlayToWebPageMode_Default);
	}

	// Token: 0x06000FD4 RID: 4052 RVA: 0x00099BBA File Offset: 0x00097DBA
	public void OpenLinkConfirm(string url)
	{
		this.tempURL = url;
		ConfirmationManager.CreateConfirmation(LangaugeManager.main.GetTextByKey("modURLConfirm"), url, new Func<Action>(this.OpenLinkInternal));
	}

	// Token: 0x06000FD5 RID: 4053 RVA: 0x00099BE4 File Offset: 0x00097DE4
	private Action OpenLinkInternal()
	{
		if (this.tempURL == null)
		{
			return null;
		}
		string text = this.tempURL;
		this.tempURL = null;
		Application.OpenURL(text);
		return null;
	}

	// Token: 0x06000FD6 RID: 4054 RVA: 0x00099C04 File Offset: 0x00097E04
	public void EndEvent()
	{
		if (this.finished)
		{
			return;
		}
		SoundManager.main.PlaySFX("menuBlip");
		this.finished = true;
		this.eventBoxAnimator.Play("Out");
		Singleton.Instance.showingOptions = false;
		GameManager main = GameManager.main;
		if (main)
		{
			main.SetAllItemColliders(true);
			main.viewingEvent = false;
		}
		MenuManager menuManager = Object.FindObjectOfType<MenuManager>();
		if (menuManager)
		{
			menuManager.ShowButtons();
		}
	}

	// Token: 0x04000CF6 RID: 3318
	[SerializeField]
	private Mask mask;

	// Token: 0x04000CF7 RID: 3319
	private bool finished;

	// Token: 0x04000CF8 RID: 3320
	[SerializeField]
	private Animator eventBoxAnimator;

	// Token: 0x04000CF9 RID: 3321
	[SerializeField]
	private Transform modListParent;

	// Token: 0x04000CFA RID: 3322
	[SerializeField]
	private GameObject modpackInfoPrefab;

	// Token: 0x04000CFB RID: 3323
	[SerializeField]
	private Toggle debuggingToggle;

	// Token: 0x04000CFC RID: 3324
	[SerializeField]
	private GameObject GetModsButton;

	// Token: 0x04000CFD RID: 3325
	private string tempURL;

	// Token: 0x04000CFE RID: 3326
	private float timePassed;
}
