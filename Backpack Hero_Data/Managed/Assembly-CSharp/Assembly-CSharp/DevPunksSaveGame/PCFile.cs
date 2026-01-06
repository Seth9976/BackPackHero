using System;
using System.Collections.Generic;
using System.IO;
using DevPunksSaveGame.Interfaces;

namespace DevPunksSaveGame
{
	// Token: 0x0200022E RID: 558
	internal class PCFile : FileInterface
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x000AD4EC File Offset: 0x000AB6EC
		public bool IsReady
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x000AD4EF File Offset: 0x000AB6EF
		public bool UsesCriticalSection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x000AD4F2 File Offset: 0x000AB6F2
		bool FileInterface.IsReady
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x000AD4F9 File Offset: 0x000AB6F9
		bool FileInterface.UsesCriticalSection
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x000AD500 File Offset: 0x000AB700
		public void BatchSave(List<SaveFileEntry> saveFileEntries, Action<EFileResult> saveFileCallback, string v1, string v2, string v3)
		{
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x000AD502 File Offset: 0x000AB702
		public void EnterCriticalSection()
		{
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x000AD504 File Offset: 0x000AB704
		public void ExitCriticalSection()
		{
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x000AD506 File Offset: 0x000AB706
		public string[] GetAllSlots()
		{
			return new string[0];
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x000AD50E File Offset: 0x000AB70E
		public string[] GetAllFilesInSlot(string slot)
		{
			return new string[0];
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x000AD516 File Offset: 0x000AB716
		public void LoadFile(SaveFileEntry fileEntry, Action<EFileResult, SaveFileEntry> callback)
		{
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x000AD518 File Offset: 0x000AB718
		public void LoadFiles(List<string> files, Action<EFileResult, List<SaveFileEntry>> callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x000AD51F File Offset: 0x000AB71F
		public void SetUseCompression(bool zip)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x000AD526 File Offset: 0x000AB726
		public void SaveFile(SaveFileEntry fileEntry, Action<EFileResult> callback, string title = "", string subTitle = "", string detail = "")
		{
			Directory.CreateDirectory(fileEntry.Path);
			File.WriteAllBytes(fileEntry.Path, fileEntry.Value);
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x000AD545 File Offset: 0x000AB745
		public void LoadFileAsync(SaveFileEntry fileEntry, Action<EFileResult, SaveFileEntry> callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x000AD54C File Offset: 0x000AB74C
		public void SaveFiles(List<SaveFileEntry> files, Action<EFileResult> callback, string title = "", string subTitle = "", string detail = "")
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x000AD553 File Offset: 0x000AB753
		public void SetSaveIconPath(string path)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x000AD55A File Offset: 0x000AB75A
		public void SetSlot(string folder)
		{
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x000AD55C File Offset: 0x000AB75C
		public void SetSlotSizeInMB(ulong size)
		{
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x000AD55E File Offset: 0x000AB75E
		public void LoadFilesSync(string slot, List<string> files, Action<EFileResult, List<SaveFileEntry>> callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x000AD565 File Offset: 0x000AB765
		void FileInterface.DeleteSlotAndAllWithin(string slot)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x000AD56C File Offset: 0x000AB76C
		void FileInterface.EnterCriticalSection()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x000AD573 File Offset: 0x000AB773
		void FileInterface.ExitCriticalSection()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x000AD57A File Offset: 0x000AB77A
		ValueTuple<EFileResult, SaveFileEntry> FileInterface.LoadFileSync(string slot, string path)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x000AD581 File Offset: 0x000AB781
		void FileInterface.SaveFile(SaveFileEntry fileEntry, Action<EFileResult> callback, string title, string subTitle, string detail)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x000AD588 File Offset: 0x000AB788
		void FileInterface.SetSaveIconPath(string path)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x000AD58F File Offset: 0x000AB78F
		void FileInterface.SetSlotSizeInMB(ulong size)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x000AD596 File Offset: 0x000AB796
		public string GetSystemLanguage()
		{
			throw new NotImplementedException();
		}
	}
}
