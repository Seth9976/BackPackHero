using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000122 RID: 290
	[NativeHeader("Runtime/Graphics/CustomRenderTextureManager.h")]
	public static class CustomRenderTextureManager
	{
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060007FA RID: 2042 RVA: 0x0000BEA8 File Offset: 0x0000A0A8
		// (remove) Token: 0x060007FB RID: 2043 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		[field: DebuggerBrowsable(0)]
		public static event Action<CustomRenderTexture> textureLoaded;

		// Token: 0x060007FC RID: 2044 RVA: 0x0000BF0F File Offset: 0x0000A10F
		[RequiredByNativeCode]
		private static void InvokeOnTextureLoaded_Internal(CustomRenderTexture source)
		{
			Action<CustomRenderTexture> action = CustomRenderTextureManager.textureLoaded;
			if (action != null)
			{
				action.Invoke(source);
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060007FD RID: 2045 RVA: 0x0000BF24 File Offset: 0x0000A124
		// (remove) Token: 0x060007FE RID: 2046 RVA: 0x0000BF58 File Offset: 0x0000A158
		[field: DebuggerBrowsable(0)]
		public static event Action<CustomRenderTexture> textureUnloaded;

		// Token: 0x060007FF RID: 2047 RVA: 0x0000BF8B File Offset: 0x0000A18B
		[RequiredByNativeCode]
		private static void InvokeOnTextureUnloaded_Internal(CustomRenderTexture source)
		{
			Action<CustomRenderTexture> action = CustomRenderTextureManager.textureUnloaded;
			if (action != null)
			{
				action.Invoke(source);
			}
		}

		// Token: 0x06000800 RID: 2048
		[FreeFunction(Name = "CustomRenderTextureManagerScripting::GetAllCustomRenderTextures", HasExplicitThis = false)]
		[MethodImpl(4096)]
		public static extern void GetAllCustomRenderTextures(List<CustomRenderTexture> currentCustomRenderTextures);

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000801 RID: 2049 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		// (remove) Token: 0x06000802 RID: 2050 RVA: 0x0000BFD4 File Offset: 0x0000A1D4
		[field: DebuggerBrowsable(0)]
		public static event Action<CustomRenderTexture, int> updateTriggered;

		// Token: 0x06000803 RID: 2051 RVA: 0x0000C007 File Offset: 0x0000A207
		internal static void InvokeTriggerUpdate(CustomRenderTexture crt, int updateCount)
		{
			Action<CustomRenderTexture, int> action = CustomRenderTextureManager.updateTriggered;
			if (action != null)
			{
				action.Invoke(crt, updateCount);
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000804 RID: 2052 RVA: 0x0000C01C File Offset: 0x0000A21C
		// (remove) Token: 0x06000805 RID: 2053 RVA: 0x0000C050 File Offset: 0x0000A250
		[field: DebuggerBrowsable(0)]
		public static event Action<CustomRenderTexture> initializeTriggered;

		// Token: 0x06000806 RID: 2054 RVA: 0x0000C083 File Offset: 0x0000A283
		internal static void InvokeTriggerInitialize(CustomRenderTexture crt)
		{
			Action<CustomRenderTexture> action = CustomRenderTextureManager.initializeTriggered;
			if (action != null)
			{
				action.Invoke(crt);
			}
		}
	}
}
