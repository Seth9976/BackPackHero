using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001FB RID: 507
	internal static class ClassLibraryInitializer
	{
		// Token: 0x06001668 RID: 5736 RVA: 0x00023D24 File Offset: 0x00021F24
		[RequiredByNativeCode]
		private static void Init()
		{
			UnityLogWriter.Init();
		}
	}
}
