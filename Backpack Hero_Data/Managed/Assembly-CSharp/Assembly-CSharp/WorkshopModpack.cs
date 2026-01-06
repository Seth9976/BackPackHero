using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using Steamworks;
using UnityEngine;

// Token: 0x02000136 RID: 310
[Serializable]
public class WorkshopModpack
{
	// Token: 0x06000BB1 RID: 2993 RVA: 0x0007AF88 File Offset: 0x00079188
	private void OnItemCreate(CreateItemResult_t pCallback, bool bIOFailure)
	{
		if (pCallback.m_eResult != EResult.k_EResultOK)
		{
			Debug.LogError("Could not create item." + pCallback.m_eResult.ToString());
			WorkshopWizard workshopWizard = this.wizard;
			if (workshopWizard != null)
			{
				workshopWizard.SetMessage("Could not publish modpack. \n\n Error message: Item could not be created (" + pCallback.m_eResult.ToString() + ")", WorkshopWizard.Action.Dismiss);
			}
			this.error = true;
			return;
		}
		if (pCallback.m_bUserNeedsToAcceptWorkshopLegalAgreement)
		{
			WorkshopWizard workshopWizard2 = this.wizard;
			if (workshopWizard2 != null)
			{
				workshopWizard2.SetMessage("Before publishing this modpack, you need to agree to the Workshop Legal Agreement.\n\n The link to the agreement will open in your Steam Overlay when you click okay.", WorkshopWizard.Action.OpenToS);
			}
			this.error = true;
			return;
		}
		string text = "Got File ID ";
		PublishedFileId_t nPublishedFileId = pCallback.m_nPublishedFileId;
		Debug.Log(text + nPublishedFileId.ToString());
		this.fileId = pCallback.m_nPublishedFileId;
	}

	// Token: 0x06000BB2 RID: 2994 RVA: 0x0007B050 File Offset: 0x00079250
	public void InitItem()
	{
		Debug.Log("Creating new workshop item");
		this.ItemCreateCallback = CallResult<CreateItemResult_t>.Create(new CallResult<CreateItemResult_t>.APIDispatchDelegate(this.OnItemCreate));
		this.ItemCreateCallback.Set(SteamUGC.CreateItem(SteamUtils.GetAppID(), EWorkshopFileType.k_EWorkshopFileTypeFirst), null);
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x0007B08C File Offset: 0x0007928C
	public UGCUpdateHandle_t PublishItem(bool createNew)
	{
		Debug.Log(string.Concat(new string[]
		{
			"Publishing modpack ",
			this.modpack.displayName,
			" ",
			this.modpack.internalName,
			" ",
			this.modpack.baseDir
		}));
		UGCUpdateHandle_t ugcupdateHandle_t = SteamUGC.StartItemUpdate(SteamUtils.GetAppID(), this.fileId);
		if (createNew)
		{
			SteamUGC.SetItemTitle(ugcupdateHandle_t, LangaugeManager.main.GetTextByKey(this.modpack.displayName));
			SteamUGC.SetItemDescription(ugcupdateHandle_t, LangaugeManager.main.GetTextByKey(this.modpack.description));
			SteamUGC.SetItemVisibility(ugcupdateHandle_t, ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPublic);
		}
		SteamUGC.SetItemContent(ugcupdateHandle_t, this.modpack.baseDir);
		SteamUGC.SetItemTags(ugcupdateHandle_t, this.tags);
		if (File.Exists(this.modpack.baseDir + "/workshop_thumbnail.png"))
		{
			SteamUGC.SetItemPreview(ugcupdateHandle_t, this.modpack.baseDir + "/workshop_thumbnail.png");
		}
		if (this.notes == "")
		{
			this.notes = null;
		}
		SteamUGC.SubmitItemUpdate(ugcupdateHandle_t, this.notes);
		return ugcupdateHandle_t;
	}

	// Token: 0x06000BB4 RID: 2996 RVA: 0x0007B1BD File Offset: 0x000793BD
	public IEnumerator Publish(WorkshopWizard thiswizard = null)
	{
		this.wizard = thiswizard;
		WorkshopWizard workshopWizard = this.wizard;
		if (workshopWizard != null)
		{
			workshopWizard.SetProgress(0f, "Creating Item");
		}
		JObject jobject = JObject.Parse(File.ReadAllText(this.modpack.baseDir + ModLoader.FILENAME_META));
		jobject["steam_workshop_tags"] = new JArray(this.tags);
		File.WriteAllText(this.modpack.baseDir + ModLoader.FILENAME_META, jobject.ToString());
		bool createNew = false;
		if (this.fileId == PublishedFileId_t.Invalid)
		{
			createNew = true;
			this.InitItem();
		}
		while (this.fileId == PublishedFileId_t.Invalid && !this.error)
		{
			yield return null;
		}
		if (this.fileId != PublishedFileId_t.Invalid)
		{
			JObject jobject2 = JObject.Parse(File.ReadAllText(this.modpack.baseDir + ModLoader.FILENAME_META));
			jobject2["steam_workshop_id"] = (ulong)this.fileId;
			File.WriteAllText(this.modpack.baseDir + ModLoader.FILENAME_META, jobject2.ToString());
		}
		if (this.error)
		{
			yield break;
		}
		WorkshopWizard workshopWizard2 = this.wizard;
		if (workshopWizard2 != null)
		{
			workshopWizard2.SetProgress(50f, "Publishing Item");
		}
		UGCUpdateHandle_t handle = this.PublishItem(createNew);
		ulong num;
		ulong num2;
		while (SteamUGC.GetItemUpdateProgress(handle, out num, out num2) != EItemUpdateStatus.k_EItemUpdateStatusInvalid)
		{
			if (num2 == 0UL)
			{
				num2 = 1UL;
			}
			WorkshopWizard workshopWizard3 = this.wizard;
			if (workshopWizard3 != null)
			{
				ulong num3;
				ulong num4;
				workshopWizard3.SetProgress(num / num2, SteamUGC.GetItemUpdateProgress(handle, out num3, out num4).ToString());
			}
			yield return null;
		}
		WorkshopWizard workshopWizard4 = this.wizard;
		if (workshopWizard4 != null)
		{
			string text = "Your Modpack has been successfully published at ID ";
			PublishedFileId_t publishedFileId_t = this.fileId;
			workshopWizard4.SetMessage(text + publishedFileId_t.ToString() + ". \n\n You may now visit your workshop item's page to edit title, description, add screenshots and change the visibility of your modpack. Press the Steam button in the Mod Selection Menu to get redirect to your page.", WorkshopWizard.Action.Dismiss);
		}
		Debug.Log("Function concluded without error");
		yield break;
	}

	// Token: 0x04000982 RID: 2434
	public string title = "";

	// Token: 0x04000983 RID: 2435
	public string description = "";

	// Token: 0x04000984 RID: 2436
	public PublishedFileId_t fileId = PublishedFileId_t.Invalid;

	// Token: 0x04000985 RID: 2437
	public ModLoader.ModpackInfo modpack;

	// Token: 0x04000986 RID: 2438
	public List<string> tags;

	// Token: 0x04000987 RID: 2439
	public string notes;

	// Token: 0x04000988 RID: 2440
	public bool isOwner;

	// Token: 0x04000989 RID: 2441
	private bool error;

	// Token: 0x0400098A RID: 2442
	private WorkshopWizard wizard;

	// Token: 0x0400098B RID: 2443
	public bool local;

	// Token: 0x0400098C RID: 2444
	private CallResult<CreateItemResult_t> ItemCreateCallback;
}
