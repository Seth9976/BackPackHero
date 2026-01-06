using System;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000188 RID: 392
public class ModpackInfoDisplay : MonoBehaviour
{
	// Token: 0x06000FBC RID: 4028 RVA: 0x00099337 File Offset: 0x00097537
	private void Start()
	{
	}

	// Token: 0x06000FBD RID: 4029 RVA: 0x00099339 File Offset: 0x00097539
	public void SetModpack(ModLoader.ModpackInfo modpack)
	{
		this.modpack = modpack;
		this.RefreshUI();
	}

	// Token: 0x06000FBE RID: 4030 RVA: 0x00099348 File Offset: 0x00097548
	public void RefreshUI()
	{
		this.modNameUI.text = LangaugeManager.main.GetTextByKey(this.modpack.displayName);
		this.modAuthorUI.text = LangaugeManager.main.GetTextByKey(this.modpack.author);
		this.modDescriptionUI.text = LangaugeManager.main.GetTextByKey(this.modpack.description);
		this.modVersionUI.text = "v" + this.modpack.modVersion;
		this.modIconUI.sprite = this.modpack.icon;
		switch (this.modpack.origin)
		{
		case ModLoader.ModpackInfo.Origin.SteamWorkshop:
			this.modOriginIconUI.sprite = this.OriginSteam;
			break;
		case ModLoader.ModpackInfo.Origin.Zip:
			this.modOriginIconUI.sprite = this.OriginZip;
			break;
		case ModLoader.ModpackInfo.Origin.Folder:
			this.modOriginIconUI.sprite = this.OriginFolder;
			break;
		}
		this.modEnabledUI.isOn = this.modpack.loaded;
		if (this.modpack.website == null)
		{
			this.modWebsiteBtn.gameObject.SetActive(false);
		}
		if (this.modpack.modCount.ContainsKey(ModLoader.ModpackInfo.ModType.AddItem))
		{
			this.itemsAddedUI.GetComponentInChildren<TextMeshProUGUI>().text = this.modpack.modCount[ModLoader.ModpackInfo.ModType.AddItem].ToString();
		}
		else
		{
			this.itemsAddedUI.GetComponentInChildren<TextMeshProUGUI>().text = "0";
		}
		if (this.modpack.modCount.ContainsKey(ModLoader.ModpackInfo.ModType.AddEnemy))
		{
			this.enemiesAddedUI.GetComponentInChildren<TextMeshProUGUI>().text = this.modpack.modCount[ModLoader.ModpackInfo.ModType.AddEnemy].ToString();
		}
		else
		{
			this.enemiesAddedUI.GetComponentInChildren<TextMeshProUGUI>().text = "0";
		}
		if (this.modpack.modCount.ContainsKey(ModLoader.ModpackInfo.ModType.ReplaceAsset))
		{
			this.assetsReplacedUI.GetComponentInChildren<TextMeshProUGUI>().text = this.modpack.modCount[ModLoader.ModpackInfo.ModType.ReplaceAsset].ToString();
		}
		else
		{
			this.assetsReplacedUI.GetComponentInChildren<TextMeshProUGUI>().text = "0";
		}
		this.modErrorBtn.SetActive(!this.modpack.loadable);
	}

	// Token: 0x06000FBF RID: 4031 RVA: 0x00099588 File Offset: 0x00097788
	public void OnErrorButton()
	{
		WorkshopWizard component = Object.Instantiate<GameObject>(this.messageBoxPrefab, GameObject.FindGameObjectWithTag("UI Canvas").transform).GetComponent<WorkshopWizard>();
		component.wizardMode = false;
		component.SetMessage(this.modpack.errorCause, WorkshopWizard.Action.Dismiss);
		foreach (TMP_Text tmp_Text in component.gameObject.GetComponentsInChildren<TMP_Text>())
		{
			if (tmp_Text.gameObject.name == "Title")
			{
				tmp_Text.text = LangaugeManager.main.GetTextByKey("modLoadError");
			}
		}
	}

	// Token: 0x06000FC0 RID: 4032 RVA: 0x00099618 File Offset: 0x00097818
	public void onToggle(Toggle change)
	{
		if (!this.modpack.loadable)
		{
			change.isOn = false;
			return;
		}
		if (this.modpack != null && !this.modpack.loaded && change.isOn)
		{
			ModLoader.main.LoadModpack(this.modpack);
		}
		if (this.modpack != null && this.modpack.loaded && !change.isOn)
		{
			if (Object.FindAnyObjectByType<GameManager>() != null)
			{
				string textByKey = LangaugeManager.main.GetTextByKey("modUnloadWarning2");
				ConfirmationManager.CreateConfirmation(LangaugeManager.main.GetTextByKey("modUnloadWarning"), textByKey, new Func<Action>(this.UnloadInternal));
			}
			else
			{
				this.UnloadInternal();
			}
		}
		this.RefreshUI();
	}

	// Token: 0x06000FC1 RID: 4033 RVA: 0x000996D4 File Offset: 0x000978D4
	public void OpenLinkConfirm()
	{
		string text = LangaugeManager.main.GetTextByKey("modURLWarning");
		text = text.Replace("/x", this.modpack.website);
		ConfirmationManager.CreateConfirmation(LangaugeManager.main.GetTextByKey("modURLConfirm"), text, new Func<Action>(this.OpenLinkInternal));
	}

	// Token: 0x06000FC2 RID: 4034 RVA: 0x00099729 File Offset: 0x00097929
	private Action OpenLinkInternal()
	{
		Application.OpenURL(this.modpack.website);
		return null;
	}

	// Token: 0x06000FC3 RID: 4035 RVA: 0x0009973C File Offset: 0x0009793C
	public void PublishButton()
	{
		if (SteamManager.enabled)
		{
			Object.Instantiate<GameObject>(this.modPublishWizardPrefab, GameObject.FindGameObjectWithTag("UI Canvas").transform).GetComponent<WorkshopWizard>().modpack = this.modpack;
		}
	}

	// Token: 0x06000FC4 RID: 4036 RVA: 0x00099770 File Offset: 0x00097970
	public void OriginButton()
	{
		if (this.modpack.origin == ModLoader.ModpackInfo.Origin.SteamWorkshop && SteamManager.enabled)
		{
			string text = "steam://url/CommunityFilePage/";
			PublishedFileId_t fileId = this.modpack.workshop.fileId;
			SteamFriends.ActivateGameOverlayToWebPage(text + fileId.ToString(), EActivateGameOverlayToWebPageMode.k_EActivateGameOverlayToWebPageMode_Default);
		}
		if (this.modpack.origin == ModLoader.ModpackInfo.Origin.Folder)
		{
			Application.OpenURL(this.modpack.baseDir);
		}
	}

	// Token: 0x06000FC5 RID: 4037 RVA: 0x000997DD File Offset: 0x000979DD
	public Action UnloadInternal()
	{
		if (this.modpack != null && this.modpack.loaded)
		{
			ModLoader.main.UnloadModpack(this.modpack, false, false);
		}
		this.RefreshUI();
		return null;
	}

	// Token: 0x06000FC6 RID: 4038 RVA: 0x00099810 File Offset: 0x00097A10
	private void Update()
	{
		if (ModLoader.main.dataReady)
		{
			this.RefreshUI();
			ModLoader.main.dataReady = false;
		}
		this.modPublishBtn.SetActive(SteamManager.enabled && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && (this.modpack.origin == ModLoader.ModpackInfo.Origin.Folder || (this.modpack.origin == ModLoader.ModpackInfo.Origin.SteamWorkshop && this.modpack.workshop.local)));
	}

	// Token: 0x04000CE3 RID: 3299
	private ModLoader.ModpackInfo modpack;

	// Token: 0x04000CE4 RID: 3300
	[SerializeField]
	private TextMeshProUGUI modNameUI;

	// Token: 0x04000CE5 RID: 3301
	[SerializeField]
	private TextMeshProUGUI modAuthorUI;

	// Token: 0x04000CE6 RID: 3302
	[SerializeField]
	private TextMeshProUGUI modDescriptionUI;

	// Token: 0x04000CE7 RID: 3303
	[SerializeField]
	private TextMeshProUGUI modVersionUI;

	// Token: 0x04000CE8 RID: 3304
	[SerializeField]
	private Image modIconUI;

	// Token: 0x04000CE9 RID: 3305
	[SerializeField]
	private Image modOriginIconUI;

	// Token: 0x04000CEA RID: 3306
	[SerializeField]
	private Toggle modEnabledUI;

	// Token: 0x04000CEB RID: 3307
	[SerializeField]
	private Button modWebsiteBtn;

	// Token: 0x04000CEC RID: 3308
	[SerializeField]
	private GameObject modPublishBtn;

	// Token: 0x04000CED RID: 3309
	[SerializeField]
	private GameObject modPublishWizardPrefab;

	// Token: 0x04000CEE RID: 3310
	[SerializeField]
	private GameObject messageBoxPrefab;

	// Token: 0x04000CEF RID: 3311
	[SerializeField]
	private GameObject modErrorBtn;

	// Token: 0x04000CF0 RID: 3312
	[SerializeField]
	private Transform itemsAddedUI;

	// Token: 0x04000CF1 RID: 3313
	[SerializeField]
	private Transform enemiesAddedUI;

	// Token: 0x04000CF2 RID: 3314
	[SerializeField]
	private Transform assetsReplacedUI;

	// Token: 0x04000CF3 RID: 3315
	[SerializeField]
	private Sprite OriginSteam;

	// Token: 0x04000CF4 RID: 3316
	[SerializeField]
	private Sprite OriginFolder;

	// Token: 0x04000CF5 RID: 3317
	[SerializeField]
	private Sprite OriginZip;
}
