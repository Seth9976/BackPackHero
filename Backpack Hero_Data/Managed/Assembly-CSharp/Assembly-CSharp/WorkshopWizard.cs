using System;
using System.Collections.Generic;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000190 RID: 400
public class WorkshopWizard : MonoBehaviour
{
	// Token: 0x0600101A RID: 4122 RVA: 0x0009B784 File Offset: 0x00099984
	private void Start()
	{
		if (!this.wizardMode)
		{
			return;
		}
		if (this.modpack.workshop == null)
		{
			this.modpack.workshop = new WorkshopModpack
			{
				modpack = this.modpack
			};
		}
		if (this.modpack.workshop.fileId == PublishedFileId_t.Invalid)
		{
			this.SetQuestion("Your modpack \"" + LangaugeManager.main.GetTextByKey(this.modpack.displayName) + "\" will be published to the Steam Workshop.\n\nAfter publishing, you will be able to change title, description, screenshots and visibility of your modpack using the Steam Workshop interface. Press the Steam icon in the Modpack Selector.\n\nBy submitting this modpack, you agree to the workshop terms of service.", WorkshopWizard.Action.TagsAndNotes, WorkshopWizard.Action.Dismiss);
			return;
		}
		string[] array = new string[5];
		array[0] = "Your modpack \"";
		array[1] = LangaugeManager.main.GetTextByKey(this.modpack.displayName);
		array[2] = "\" will be updated on the Steam Workshop at ID ";
		int num = 3;
		PublishedFileId_t fileId = this.modpack.workshop.fileId;
		array[num] = fileId.ToString();
		array[4] = ".\n\nBy submitting this modpack, you agree to the workshop terms of service.";
		this.SetQuestion(string.Concat(array), WorkshopWizard.Action.TagsAndNotes, WorkshopWizard.Action.Dismiss);
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x0009B870 File Offset: 0x00099A70
	private void PerformAction(WorkshopWizard.Action action)
	{
		switch (action)
		{
		case WorkshopWizard.Action.TagsAndNotes:
			this.OpenTagsAndNotes();
			return;
		case WorkshopWizard.Action.Publish:
			this.EndTagsAndNotes();
			base.StartCoroutine(this.modpack.workshop.Publish(this));
			return;
		case WorkshopWizard.Action.Dismiss:
			Object.Destroy(base.gameObject);
			return;
		case WorkshopWizard.Action.OpenToS:
			SteamFriends.ActivateGameOverlayToWebPage("http://steamcommunity.com/sharedfiles/workshoplegalagreement", EActivateGameOverlayToWebPageMode.k_EActivateGameOverlayToWebPageMode_Default);
			this.PerformAction(WorkshopWizard.Action.Dismiss);
			return;
		default:
			return;
		}
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x0009B8D8 File Offset: 0x00099AD8
	public void ButtonYes()
	{
		this.PerformAction(this.yesAction);
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x0009B8E6 File Offset: 0x00099AE6
	public void ButtonNo()
	{
		this.PerformAction(this.noAction);
	}

	// Token: 0x0600101E RID: 4126 RVA: 0x0009B8F4 File Offset: 0x00099AF4
	public void ButtonOk()
	{
		this.PerformAction(this.messageAction);
	}

	// Token: 0x0600101F RID: 4127 RVA: 0x0009B904 File Offset: 0x00099B04
	public void OpenTagsAndNotes()
	{
		this.progressUI.SetActive(false);
		this.questionUI.SetActive(false);
		this.messageUI.SetActive(false);
		this.tagsUI.SetActive(true);
		Toggle[] componentsInChildren = this.tagsUI.GetComponentsInChildren<Toggle>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Toggle toggle = componentsInChildren[i];
			toggle.isOn = this.modpack.workshop.tags != null && this.modpack.workshop.tags.Exists((string x) => x == toggle.gameObject.name);
		}
		if (this.modpack.workshop.notes != null)
		{
			this.notesField.text = this.modpack.workshop.notes;
		}
		else
		{
			this.notesField.text = "";
		}
		this.yesAction = WorkshopWizard.Action.Publish;
		this.noAction = WorkshopWizard.Action.Dismiss;
	}

	// Token: 0x06001020 RID: 4128 RVA: 0x0009B9F8 File Offset: 0x00099BF8
	public void EndTagsAndNotes()
	{
		this.modpack.workshop.tags = new List<string>();
		foreach (Toggle toggle in this.tagsUI.GetComponentsInChildren<Toggle>())
		{
			if (toggle.isOn)
			{
				this.modpack.workshop.tags.Add(toggle.gameObject.name);
			}
		}
		this.modpack.workshop.notes = this.notesField.text;
	}

	// Token: 0x06001021 RID: 4129 RVA: 0x0009BA7C File Offset: 0x00099C7C
	public void SetMessage(string message, WorkshopWizard.Action action = WorkshopWizard.Action.Dismiss)
	{
		this.progressUI.SetActive(false);
		this.questionUI.SetActive(false);
		this.messageUI.SetActive(true);
		this.tagsUI.SetActive(false);
		this.messageText.text = message;
		this.messageAction = action;
	}

	// Token: 0x06001022 RID: 4130 RVA: 0x0009BACC File Offset: 0x00099CCC
	public void SetQuestion(string question, WorkshopWizard.Action yes = WorkshopWizard.Action.Dismiss, WorkshopWizard.Action no = WorkshopWizard.Action.Dismiss)
	{
		this.progressUI.SetActive(false);
		this.questionUI.SetActive(true);
		this.messageUI.SetActive(false);
		this.tagsUI.SetActive(false);
		this.questionText.text = question;
		this.yesAction = yes;
		this.noAction = no;
	}

	// Token: 0x06001023 RID: 4131 RVA: 0x0009BB24 File Offset: 0x00099D24
	public void SetProgress(float progress, string progressText)
	{
		this.progressUI.SetActive(true);
		this.questionUI.SetActive(false);
		this.messageUI.SetActive(false);
		this.tagsUI.SetActive(false);
		this.progressText.text = progressText;
		this.progressFill.fillAmount = progress;
	}

	// Token: 0x06001024 RID: 4132 RVA: 0x0009BB79 File Offset: 0x00099D79
	private void OnDestroy()
	{
		ModLoader.main.ReloadModpacks();
		ModMetaSave.SaveModData();
	}

	// Token: 0x04000D36 RID: 3382
	[SerializeField]
	private GameObject messageUI;

	// Token: 0x04000D37 RID: 3383
	[SerializeField]
	private GameObject tagsUI;

	// Token: 0x04000D38 RID: 3384
	[SerializeField]
	private GameObject questionUI;

	// Token: 0x04000D39 RID: 3385
	[SerializeField]
	private GameObject progressUI;

	// Token: 0x04000D3A RID: 3386
	[SerializeField]
	private TMP_Text questionText;

	// Token: 0x04000D3B RID: 3387
	[SerializeField]
	private TMP_Text messageText;

	// Token: 0x04000D3C RID: 3388
	[SerializeField]
	private TMP_Text progressText;

	// Token: 0x04000D3D RID: 3389
	[SerializeField]
	private TMP_InputField notesField;

	// Token: 0x04000D3E RID: 3390
	[SerializeField]
	private Image progressFill;

	// Token: 0x04000D3F RID: 3391
	[SerializeField]
	public bool wizardMode = true;

	// Token: 0x04000D40 RID: 3392
	public ModLoader.ModpackInfo modpack;

	// Token: 0x04000D41 RID: 3393
	private WorkshopWizard.Action yesAction = WorkshopWizard.Action.Dismiss;

	// Token: 0x04000D42 RID: 3394
	private WorkshopWizard.Action noAction = WorkshopWizard.Action.Dismiss;

	// Token: 0x04000D43 RID: 3395
	private WorkshopWizard.Action messageAction = WorkshopWizard.Action.Dismiss;

	// Token: 0x0200046A RID: 1130
	public enum Action
	{
		// Token: 0x04001A21 RID: 6689
		TagsAndNotes,
		// Token: 0x04001A22 RID: 6690
		Publish,
		// Token: 0x04001A23 RID: 6691
		Dismiss,
		// Token: 0x04001A24 RID: 6692
		OpenToS
	}
}
