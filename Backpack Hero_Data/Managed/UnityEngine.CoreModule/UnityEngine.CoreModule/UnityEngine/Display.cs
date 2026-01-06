using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000123 RID: 291
	[NativeHeader("Runtime/Graphics/DisplayManager.h")]
	[UsedByNativeCode]
	public class Display
	{
		// Token: 0x06000807 RID: 2055 RVA: 0x0000C097 File Offset: 0x0000A297
		internal Display()
		{
			this.nativeDisplay = new IntPtr(0);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0000C0AD File Offset: 0x0000A2AD
		internal Display(IntPtr nativeDisplay)
		{
			this.nativeDisplay = nativeDisplay;
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x0000C0C0 File Offset: 0x0000A2C0
		public int renderingWidth
		{
			get
			{
				int num = 0;
				int num2 = 0;
				Display.GetRenderingExtImpl(this.nativeDisplay, out num, out num2);
				return num;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0000C0E8 File Offset: 0x0000A2E8
		public int renderingHeight
		{
			get
			{
				int num = 0;
				int num2 = 0;
				Display.GetRenderingExtImpl(this.nativeDisplay, out num, out num2);
				return num2;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x0000C110 File Offset: 0x0000A310
		public int systemWidth
		{
			get
			{
				int num = 0;
				int num2 = 0;
				Display.GetSystemExtImpl(this.nativeDisplay, out num, out num2);
				return num;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0000C138 File Offset: 0x0000A338
		public int systemHeight
		{
			get
			{
				int num = 0;
				int num2 = 0;
				Display.GetSystemExtImpl(this.nativeDisplay, out num, out num2);
				return num2;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x0000C160 File Offset: 0x0000A360
		public RenderBuffer colorBuffer
		{
			get
			{
				RenderBuffer renderBuffer;
				RenderBuffer renderBuffer2;
				Display.GetRenderingBuffersImpl(this.nativeDisplay, out renderBuffer, out renderBuffer2);
				return renderBuffer;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0000C184 File Offset: 0x0000A384
		public RenderBuffer depthBuffer
		{
			get
			{
				RenderBuffer renderBuffer;
				RenderBuffer renderBuffer2;
				Display.GetRenderingBuffersImpl(this.nativeDisplay, out renderBuffer, out renderBuffer2);
				return renderBuffer2;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x0000C1A8 File Offset: 0x0000A3A8
		public bool active
		{
			get
			{
				return Display.GetActiveImpl(this.nativeDisplay);
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0000C1C8 File Offset: 0x0000A3C8
		public bool requiresBlitToBackbuffer
		{
			get
			{
				int num = this.nativeDisplay.ToInt32();
				bool flag = num < HDROutputSettings.displays.Length;
				if (flag)
				{
					bool flag2 = HDROutputSettings.displays[num].available && HDROutputSettings.displays[num].active;
					bool flag3 = flag2;
					if (flag3)
					{
						return true;
					}
				}
				return Display.RequiresBlitToBackbufferImpl(this.nativeDisplay);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0000C22C File Offset: 0x0000A42C
		public bool requiresSrgbBlitToBackbuffer
		{
			get
			{
				return Display.RequiresSrgbBlitToBackbufferImpl(this.nativeDisplay);
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0000C249 File Offset: 0x0000A449
		public void Activate()
		{
			Display.ActivateDisplayImpl(this.nativeDisplay, 0, 0, 60);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0000C25C File Offset: 0x0000A45C
		public void Activate(int width, int height, int refreshRate)
		{
			Display.ActivateDisplayImpl(this.nativeDisplay, width, height, refreshRate);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0000C26E File Offset: 0x0000A46E
		public void SetParams(int width, int height, int x, int y)
		{
			Display.SetParamsImpl(this.nativeDisplay, width, height, x, y);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0000C282 File Offset: 0x0000A482
		public void SetRenderingResolution(int w, int h)
		{
			Display.SetRenderingResolutionImpl(this.nativeDisplay, w, h);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0000C294 File Offset: 0x0000A494
		[Obsolete("MultiDisplayLicense has been deprecated.", false)]
		public static bool MultiDisplayLicense()
		{
			return true;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0000C2A8 File Offset: 0x0000A4A8
		public static Vector3 RelativeMouseAt(Vector3 inputMouseCoordinates)
		{
			int num = 0;
			int num2 = 0;
			int num3 = (int)inputMouseCoordinates.x;
			int num4 = (int)inputMouseCoordinates.y;
			Vector3 vector;
			vector.z = (float)Display.RelativeMouseAtImpl(num3, num4, out num, out num2);
			vector.x = (float)num;
			vector.y = (float)num2;
			return vector;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x0000C2F8 File Offset: 0x0000A4F8
		public static Display main
		{
			get
			{
				return Display._mainDisplay;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x0000C310 File Offset: 0x0000A510
		// (set) Token: 0x0600081A RID: 2074 RVA: 0x0000C327 File Offset: 0x0000A527
		public static int activeEditorGameViewTarget
		{
			get
			{
				return Display.m_ActiveEditorGameViewTarget;
			}
			internal set
			{
				Display.m_ActiveEditorGameViewTarget = value;
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0000C330 File Offset: 0x0000A530
		[RequiredByNativeCode]
		private static void RecreateDisplayList(IntPtr[] nativeDisplay)
		{
			bool flag = nativeDisplay.Length == 0;
			if (!flag)
			{
				Display.displays = new Display[nativeDisplay.Length];
				for (int i = 0; i < nativeDisplay.Length; i++)
				{
					Display.displays[i] = new Display(nativeDisplay[i]);
				}
				Display._mainDisplay = Display.displays[0];
			}
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0000C384 File Offset: 0x0000A584
		[RequiredByNativeCode]
		private static void FireDisplaysUpdated()
		{
			bool flag = Display.onDisplaysUpdated != null;
			if (flag)
			{
				Display.onDisplaysUpdated();
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600081D RID: 2077 RVA: 0x0000C3AC File Offset: 0x0000A5AC
		// (remove) Token: 0x0600081E RID: 2078 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
		[field: DebuggerBrowsable(0)]
		public static event Display.DisplaysUpdatedDelegate onDisplaysUpdated;

		// Token: 0x0600081F RID: 2079
		[FreeFunction("UnityDisplayManager_DisplaySystemResolution")]
		[MethodImpl(4096)]
		private static extern void GetSystemExtImpl(IntPtr nativeDisplay, out int w, out int h);

		// Token: 0x06000820 RID: 2080
		[FreeFunction("UnityDisplayManager_DisplayRenderingResolution")]
		[MethodImpl(4096)]
		private static extern void GetRenderingExtImpl(IntPtr nativeDisplay, out int w, out int h);

		// Token: 0x06000821 RID: 2081
		[FreeFunction("UnityDisplayManager_GetRenderingBuffersWrapper")]
		[MethodImpl(4096)]
		private static extern void GetRenderingBuffersImpl(IntPtr nativeDisplay, out RenderBuffer color, out RenderBuffer depth);

		// Token: 0x06000822 RID: 2082
		[FreeFunction("UnityDisplayManager_SetRenderingResolution")]
		[MethodImpl(4096)]
		private static extern void SetRenderingResolutionImpl(IntPtr nativeDisplay, int w, int h);

		// Token: 0x06000823 RID: 2083
		[FreeFunction("UnityDisplayManager_ActivateDisplay")]
		[MethodImpl(4096)]
		private static extern void ActivateDisplayImpl(IntPtr nativeDisplay, int width, int height, int refreshRate);

		// Token: 0x06000824 RID: 2084
		[FreeFunction("UnityDisplayManager_SetDisplayParam")]
		[MethodImpl(4096)]
		private static extern void SetParamsImpl(IntPtr nativeDisplay, int width, int height, int x, int y);

		// Token: 0x06000825 RID: 2085
		[FreeFunction("UnityDisplayManager_RelativeMouseAt")]
		[MethodImpl(4096)]
		private static extern int RelativeMouseAtImpl(int x, int y, out int rx, out int ry);

		// Token: 0x06000826 RID: 2086
		[FreeFunction("UnityDisplayManager_DisplayActive")]
		[MethodImpl(4096)]
		private static extern bool GetActiveImpl(IntPtr nativeDisplay);

		// Token: 0x06000827 RID: 2087
		[FreeFunction("UnityDisplayManager_RequiresBlitToBackbuffer")]
		[MethodImpl(4096)]
		private static extern bool RequiresBlitToBackbufferImpl(IntPtr nativeDisplay);

		// Token: 0x06000828 RID: 2088
		[FreeFunction("UnityDisplayManager_RequiresSRGBBlitToBackbuffer")]
		[MethodImpl(4096)]
		private static extern bool RequiresSrgbBlitToBackbufferImpl(IntPtr nativeDisplay);

		// Token: 0x06000829 RID: 2089 RVA: 0x0000C413 File Offset: 0x0000A613
		// Note: this type is marked as 'beforefieldinit'.
		static Display()
		{
			Display.onDisplaysUpdated = null;
		}

		// Token: 0x040003A7 RID: 935
		internal IntPtr nativeDisplay;

		// Token: 0x040003A8 RID: 936
		public static Display[] displays = new Display[]
		{
			new Display()
		};

		// Token: 0x040003A9 RID: 937
		private static Display _mainDisplay = Display.displays[0];

		// Token: 0x040003AA RID: 938
		private static int m_ActiveEditorGameViewTarget = -1;

		// Token: 0x02000124 RID: 292
		// (Invoke) Token: 0x0600082B RID: 2091
		public delegate void DisplaysUpdatedDelegate();
	}
}
