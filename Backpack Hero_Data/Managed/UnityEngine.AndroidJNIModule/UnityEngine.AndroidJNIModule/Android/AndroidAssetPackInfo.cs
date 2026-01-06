using System;

namespace UnityEngine.Android
{
	// Token: 0x02000011 RID: 17
	public class AndroidAssetPackInfo
	{
		// Token: 0x06000189 RID: 393 RVA: 0x000079D8 File Offset: 0x00005BD8
		internal AndroidAssetPackInfo(string name, AndroidAssetPackStatus status, ulong size, ulong bytesDownloaded, float transferProgress, AndroidAssetPackError error)
		{
			this.name = name;
			this.status = status;
			this.size = size;
			this.bytesDownloaded = bytesDownloaded;
			this.transferProgress = transferProgress;
			this.error = error;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00007A0F File Offset: 0x00005C0F
		public string name { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007A17 File Offset: 0x00005C17
		public AndroidAssetPackStatus status { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00007A1F File Offset: 0x00005C1F
		public ulong size { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00007A27 File Offset: 0x00005C27
		public ulong bytesDownloaded { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00007A2F File Offset: 0x00005C2F
		public float transferProgress { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00007A37 File Offset: 0x00005C37
		public AndroidAssetPackError error { get; }
	}
}
