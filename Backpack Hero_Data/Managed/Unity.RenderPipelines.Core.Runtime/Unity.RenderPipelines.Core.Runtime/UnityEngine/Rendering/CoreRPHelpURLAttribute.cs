using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000074 RID: 116
	[Conditional("UNITY_EDITOR")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, AllowMultiple = false)]
	public class CoreRPHelpURLAttribute : HelpURLAttribute
	{
		// Token: 0x060003B1 RID: 945 RVA: 0x00011A49 File Offset: 0x0000FC49
		public CoreRPHelpURLAttribute(string pageName, string packageName = "com.unity.render-pipelines.core")
			: base(DocumentationInfo.GetPageLink(packageName, pageName))
		{
		}
	}
}
