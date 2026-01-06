using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000075 RID: 117
	public class DocumentationInfo
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00011A58 File Offset: 0x0000FC58
		public static string version
		{
			get
			{
				return "12.1";
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00011A5F File Offset: 0x0000FC5F
		public static string GetPageLink(string packageName, string pageName)
		{
			return string.Format("https://docs.unity3d.com/Packages/{0}@{1}/manual/{2}.html", packageName, DocumentationInfo.version, pageName);
		}

		// Token: 0x0400024F RID: 591
		private const string fallbackVersion = "12.1";

		// Token: 0x04000250 RID: 592
		private const string url = "https://docs.unity3d.com/Packages/{0}@{1}/manual/{2}.html";
	}
}
