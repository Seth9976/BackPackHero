using System;

namespace UnityEngine.Android
{
	// Token: 0x02000013 RID: 19
	public class AndroidAssetPackUseMobileDataRequestResult
	{
		// Token: 0x06000194 RID: 404 RVA: 0x00007A76 File Offset: 0x00005C76
		internal AndroidAssetPackUseMobileDataRequestResult(bool allowed)
		{
			this.allowed = allowed;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00007A87 File Offset: 0x00005C87
		public bool allowed { get; }
	}
}
