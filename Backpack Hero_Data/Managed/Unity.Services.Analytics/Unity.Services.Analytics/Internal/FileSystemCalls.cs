using System;
using System.IO;
using UnityEngine;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x02000033 RID: 51
	internal class FileSystemCalls : IFileSystemCalls
	{
		// Token: 0x0600010A RID: 266 RVA: 0x000047F2 File Offset: 0x000029F2
		public bool CanAccessFileSystem()
		{
			return Application.platform != RuntimePlatform.Switch && Application.platform != RuntimePlatform.GameCoreXboxOne && Application.platform != RuntimePlatform.GameCoreXboxSeries && Application.platform != RuntimePlatform.PS5 && Application.platform != RuntimePlatform.PS4 && !string.IsNullOrEmpty(Application.persistentDataPath);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004830 File Offset: 0x00002A30
		public bool FileExists(string path)
		{
			return File.Exists(path);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004838 File Offset: 0x00002A38
		public void DeleteFile(string path)
		{
			File.Delete(path);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004840 File Offset: 0x00002A40
		public Stream OpenFileForWriting(string path)
		{
			return new FileStream(path, FileMode.Create);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004849 File Offset: 0x00002A49
		public Stream OpenFileForReading(string path)
		{
			return new FileStream(path, FileMode.Open);
		}
	}
}
