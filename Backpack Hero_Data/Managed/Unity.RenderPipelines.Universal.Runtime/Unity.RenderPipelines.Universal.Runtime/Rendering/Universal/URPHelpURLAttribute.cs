using System;
using System.Diagnostics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200007C RID: 124
	[Conditional("UNITY_EDITOR")]
	internal class URPHelpURLAttribute : CoreRPHelpURLAttribute
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x0001B9F0 File Offset: 0x00019BF0
		public URPHelpURLAttribute(string pageName)
			: base(pageName, "com.unity.render-pipelines.universal")
		{
		}
	}
}
