using System;
using System.Collections.Generic;

namespace DevPunksSaveGame.Interfaces
{
	// Token: 0x02000234 RID: 564
	public interface FileInterface
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06001277 RID: 4727
		bool IsReady { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06001278 RID: 4728
		bool UsesCriticalSection { get; }

		// Token: 0x06001279 RID: 4729
		void EnterCriticalSection();

		// Token: 0x0600127A RID: 4730
		void ExitCriticalSection();

		// Token: 0x0600127B RID: 4731
		void SetSaveIconPath(string path);

		// Token: 0x0600127C RID: 4732
		void SetSlotSizeInMB(ulong size);

		// Token: 0x0600127D RID: 4733
		void SetUseCompression(bool zip);

		// Token: 0x0600127E RID: 4734
		string GetSystemLanguage();

		// Token: 0x0600127F RID: 4735
		void SaveFile(SaveFileEntry fileEntry, Action<EFileResult> callback, string title = "", string subTitle = "", string detail = "");

		// Token: 0x06001280 RID: 4736
		void LoadFileAsync(SaveFileEntry fileEntry, Action<EFileResult, SaveFileEntry> callback);

		// Token: 0x06001281 RID: 4737
		ValueTuple<EFileResult, SaveFileEntry> LoadFileSync(string slot, string path);

		// Token: 0x06001282 RID: 4738
		void LoadFilesSync(string slot, List<string> files, Action<EFileResult, List<SaveFileEntry>> callback);

		// Token: 0x06001283 RID: 4739
		void DeleteSlotAndAllWithin(string slot);

		// Token: 0x06001284 RID: 4740
		void BatchSave(List<SaveFileEntry> saveFileEntries, Action<EFileResult> saveFileCallback, string v1, string v2, string v3);

		// Token: 0x06001285 RID: 4741
		string[] GetAllFilesInSlot(string slot);

		// Token: 0x06001286 RID: 4742
		string[] GetAllSlots();
	}
}
