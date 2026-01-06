using System;
using System.Diagnostics;

namespace UnityEngine.UIElements
{
	// Token: 0x0200001D RID: 29
	internal class DisposeHelper
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00004E70 File Offset: 0x00003070
		[Conditional("UNITY_UIELEMENTS_DEBUG_DISPOSE")]
		public static void NotifyMissingDispose(IDisposable disposable)
		{
			bool flag = disposable == null;
			if (!flag)
			{
				Debug.LogError("An IDisposable instance of type '" + disposable.GetType().FullName + "' has not been disposed.");
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004EA8 File Offset: 0x000030A8
		public static void NotifyDisposedUsed(IDisposable disposable)
		{
			Debug.LogError("An instance of type '" + disposable.GetType().FullName + "' is being used although it has been disposed.");
		}
	}
}
