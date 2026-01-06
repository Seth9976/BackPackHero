using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200012A RID: 298
	[NativeHeader("Runtime/Graphics/ScreenManager.h")]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[StaticAccessor("GetScreenManager()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Graphics/WindowLayout.h")]
	public sealed class Screen
	{
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000832 RID: 2098
		public static extern int width
		{
			[NativeMethod(Name = "GetWidth", IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000833 RID: 2099
		public static extern int height
		{
			[NativeMethod(Name = "GetHeight", IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000834 RID: 2100
		public static extern float dpi
		{
			[NativeName("GetDPI")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000835 RID: 2101
		[MethodImpl(4096)]
		private static extern void RequestOrientation(ScreenOrientation orient);

		// Token: 0x06000836 RID: 2102
		[MethodImpl(4096)]
		private static extern ScreenOrientation GetScreenOrientation();

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x0000C500 File Offset: 0x0000A700
		// (set) Token: 0x06000838 RID: 2104 RVA: 0x0000C518 File Offset: 0x0000A718
		public static ScreenOrientation orientation
		{
			get
			{
				return Screen.GetScreenOrientation();
			}
			set
			{
				bool flag = value == ScreenOrientation.Unknown;
				if (flag)
				{
					Debug.Log("ScreenOrientation.Unknown is deprecated. Please use ScreenOrientation.AutoRotation");
					value = ScreenOrientation.AutoRotation;
				}
				Screen.RequestOrientation(value);
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000839 RID: 2105
		// (set) Token: 0x0600083A RID: 2106
		[NativeProperty("ScreenTimeout")]
		public static extern int sleepTimeout
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600083B RID: 2107
		[NativeName("GetIsOrientationEnabled")]
		[MethodImpl(4096)]
		private static extern bool IsOrientationEnabled(EnabledOrientation orient);

		// Token: 0x0600083C RID: 2108
		[NativeName("SetIsOrientationEnabled")]
		[MethodImpl(4096)]
		private static extern void SetOrientationEnabled(EnabledOrientation orient, bool enabled);

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0000C548 File Offset: 0x0000A748
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x0000C560 File Offset: 0x0000A760
		public static bool autorotateToPortrait
		{
			get
			{
				return Screen.IsOrientationEnabled(EnabledOrientation.kAutorotateToPortrait);
			}
			set
			{
				Screen.SetOrientationEnabled(EnabledOrientation.kAutorotateToPortrait, value);
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x0000C56C File Offset: 0x0000A76C
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x0000C584 File Offset: 0x0000A784
		public static bool autorotateToPortraitUpsideDown
		{
			get
			{
				return Screen.IsOrientationEnabled(EnabledOrientation.kAutorotateToPortraitUpsideDown);
			}
			set
			{
				Screen.SetOrientationEnabled(EnabledOrientation.kAutorotateToPortraitUpsideDown, value);
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x0000C590 File Offset: 0x0000A790
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
		public static bool autorotateToLandscapeLeft
		{
			get
			{
				return Screen.IsOrientationEnabled(EnabledOrientation.kAutorotateToLandscapeLeft);
			}
			set
			{
				Screen.SetOrientationEnabled(EnabledOrientation.kAutorotateToLandscapeLeft, value);
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x0000C5B4 File Offset: 0x0000A7B4
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x0000C5CC File Offset: 0x0000A7CC
		public static bool autorotateToLandscapeRight
		{
			get
			{
				return Screen.IsOrientationEnabled(EnabledOrientation.kAutorotateToLandscapeRight);
			}
			set
			{
				Screen.SetOrientationEnabled(EnabledOrientation.kAutorotateToLandscapeRight, value);
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		public static Resolution currentResolution
		{
			get
			{
				Resolution resolution;
				Screen.get_currentResolution_Injected(out resolution);
				return resolution;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000846 RID: 2118
		// (set) Token: 0x06000847 RID: 2119
		public static extern bool fullScreen
		{
			[NativeName("IsFullscreen")]
			[MethodImpl(4096)]
			get;
			[NativeName("RequestSetFullscreenFromScript")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000848 RID: 2120
		// (set) Token: 0x06000849 RID: 2121
		public static extern FullScreenMode fullScreenMode
		{
			[NativeName("GetFullscreenMode")]
			[MethodImpl(4096)]
			get;
			[NativeName("RequestSetFullscreenModeFromScript")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x0000C5F0 File Offset: 0x0000A7F0
		public static Rect safeArea
		{
			get
			{
				Rect rect;
				Screen.get_safeArea_Injected(out rect);
				return rect;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600084B RID: 2123
		public static extern Rect[] cutouts
		{
			[FreeFunction("ScreenScripting::GetCutouts")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600084C RID: 2124
		[NativeName("RequestResolution")]
		[MethodImpl(4096)]
		public static extern void SetResolution(int width, int height, FullScreenMode fullscreenMode, [DefaultValue("0")] int preferredRefreshRate);

		// Token: 0x0600084D RID: 2125 RVA: 0x0000C605 File Offset: 0x0000A805
		public static void SetResolution(int width, int height, FullScreenMode fullscreenMode)
		{
			Screen.SetResolution(width, height, fullscreenMode, 0);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0000C612 File Offset: 0x0000A812
		public static void SetResolution(int width, int height, bool fullscreen, [DefaultValue("0")] int preferredRefreshRate)
		{
			Screen.SetResolution(width, height, fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed, preferredRefreshRate);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0000C625 File Offset: 0x0000A825
		public static void SetResolution(int width, int height, bool fullscreen)
		{
			Screen.SetResolution(width, height, fullscreen, 0);
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x0000C634 File Offset: 0x0000A834
		public static Vector2Int mainWindowPosition
		{
			get
			{
				return Screen.GetMainWindowPosition();
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x0000C64C File Offset: 0x0000A84C
		public static DisplayInfo mainWindowDisplayInfo
		{
			get
			{
				return Screen.GetMainWindowDisplayInfo();
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0000C664 File Offset: 0x0000A864
		public static void GetDisplayLayout(List<DisplayInfo> displayLayout)
		{
			bool flag = displayLayout == null;
			if (flag)
			{
				throw new ArgumentNullException();
			}
			Screen.GetDisplayLayoutImpl(displayLayout);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0000C688 File Offset: 0x0000A888
		public static AsyncOperation MoveMainWindowTo(in DisplayInfo display, Vector2Int position)
		{
			return Screen.MoveMainWindowImpl(in display, position);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0000C6A4 File Offset: 0x0000A8A4
		[FreeFunction("GetMainWindowPosition")]
		private static Vector2Int GetMainWindowPosition()
		{
			Vector2Int vector2Int;
			Screen.GetMainWindowPosition_Injected(out vector2Int);
			return vector2Int;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		[FreeFunction("GetMainWindowDisplayInfo")]
		private static DisplayInfo GetMainWindowDisplayInfo()
		{
			DisplayInfo displayInfo;
			Screen.GetMainWindowDisplayInfo_Injected(out displayInfo);
			return displayInfo;
		}

		// Token: 0x06000856 RID: 2134
		[FreeFunction("GetDisplayLayout")]
		[MethodImpl(4096)]
		private static extern void GetDisplayLayoutImpl(List<DisplayInfo> displayLayout);

		// Token: 0x06000857 RID: 2135 RVA: 0x0000C6D1 File Offset: 0x0000A8D1
		[FreeFunction("MoveMainWindow")]
		private static AsyncOperation MoveMainWindowImpl(in DisplayInfo display, Vector2Int position)
		{
			return Screen.MoveMainWindowImpl_Injected(in display, ref position);
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000858 RID: 2136
		public static extern Resolution[] resolutions
		{
			[FreeFunction("ScreenScripting::GetResolutions")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000859 RID: 2137
		// (set) Token: 0x0600085A RID: 2138
		public static extern float brightness
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0000C6DC File Offset: 0x0000A8DC
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x0000C6F8 File Offset: 0x0000A8F8
		[Obsolete("Use Cursor.lockState and Cursor.visible instead.", false)]
		[EditorBrowsable(1)]
		public static bool lockCursor
		{
			get
			{
				return CursorLockMode.Locked == Cursor.lockState;
			}
			set
			{
				if (value)
				{
					Cursor.visible = false;
					Cursor.lockState = CursorLockMode.Locked;
				}
				else
				{
					Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true;
				}
			}
		}

		// Token: 0x0600085E RID: 2142
		[MethodImpl(4096)]
		private static extern void get_currentResolution_Injected(out Resolution ret);

		// Token: 0x0600085F RID: 2143
		[MethodImpl(4096)]
		private static extern void get_safeArea_Injected(out Rect ret);

		// Token: 0x06000860 RID: 2144
		[MethodImpl(4096)]
		private static extern void GetMainWindowPosition_Injected(out Vector2Int ret);

		// Token: 0x06000861 RID: 2145
		[MethodImpl(4096)]
		private static extern void GetMainWindowDisplayInfo_Injected(out DisplayInfo ret);

		// Token: 0x06000862 RID: 2146
		[MethodImpl(4096)]
		private static extern AsyncOperation MoveMainWindowImpl_Injected(in DisplayInfo display, ref Vector2Int position);
	}
}
