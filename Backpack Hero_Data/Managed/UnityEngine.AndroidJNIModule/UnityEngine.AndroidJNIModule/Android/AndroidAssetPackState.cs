using System;

namespace UnityEngine.Android
{
	// Token: 0x02000012 RID: 18
	public class AndroidAssetPackState
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00007A3F File Offset: 0x00005C3F
		internal AndroidAssetPackState(string name, AndroidAssetPackStatus status, AndroidAssetPackError error)
		{
			this.name = name;
			this.status = status;
			this.error = error;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00007A5E File Offset: 0x00005C5E
		public string name { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00007A66 File Offset: 0x00005C66
		public AndroidAssetPackStatus status { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00007A6E File Offset: 0x00005C6E
		public AndroidAssetPackError error { get; }
	}
}
