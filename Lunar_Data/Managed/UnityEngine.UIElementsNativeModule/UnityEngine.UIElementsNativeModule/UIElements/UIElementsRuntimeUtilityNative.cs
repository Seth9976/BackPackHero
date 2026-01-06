using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.UIElements
{
	// Token: 0x02000025 RID: 37
	[NativeHeader("Modules/UIElementsNative/UIElementsRuntimeUtilityNative.h")]
	[VisibleToOtherModules(new string[] { "Unity.UIElements" })]
	internal static class UIElementsRuntimeUtilityNative
	{
		// Token: 0x0600016F RID: 367 RVA: 0x00003EE1 File Offset: 0x000020E1
		[RequiredByNativeCode]
		public static void RepaintOverlayPanels()
		{
			Action repaintOverlayPanelsCallback = UIElementsRuntimeUtilityNative.RepaintOverlayPanelsCallback;
			if (repaintOverlayPanelsCallback != null)
			{
				repaintOverlayPanelsCallback.Invoke();
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00003EF5 File Offset: 0x000020F5
		[RequiredByNativeCode]
		public static void UpdateRuntimePanels()
		{
			Action updateRuntimePanelsCallback = UIElementsRuntimeUtilityNative.UpdateRuntimePanelsCallback;
			if (updateRuntimePanelsCallback != null)
			{
				updateRuntimePanelsCallback.Invoke();
			}
		}

		// Token: 0x06000171 RID: 369
		[MethodImpl(4096)]
		public static extern void RegisterPlayerloopCallback();

		// Token: 0x06000172 RID: 370
		[MethodImpl(4096)]
		public static extern void UnregisterPlayerloopCallback();

		// Token: 0x06000173 RID: 371
		[MethodImpl(4096)]
		public static extern void VisualElementCreation();

		// Token: 0x0400006A RID: 106
		internal static Action RepaintOverlayPanelsCallback;

		// Token: 0x0400006B RID: 107
		internal static Action UpdateRuntimePanelsCallback;
	}
}
