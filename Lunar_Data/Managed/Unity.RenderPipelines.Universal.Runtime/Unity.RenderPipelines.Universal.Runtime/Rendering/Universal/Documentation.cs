using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200007D RID: 125
	internal class Documentation : DocumentationInfo
	{
		// Token: 0x060004A7 RID: 1191 RVA: 0x0001B9FE File Offset: 0x00019BFE
		public static string GetPageLink(string pageName)
		{
			return DocumentationInfo.GetPageLink("com.unity.render-pipelines.universal", pageName);
		}

		// Token: 0x04000349 RID: 841
		public const string packageName = "com.unity.render-pipelines.universal";
	}
}
