using System;
using System.Collections.Generic;
using UnityEngine.Internal;

namespace UnityEngine.Device
{
	// Token: 0x0200044F RID: 1103
	public static class Screen
	{
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002722 RID: 10018 RVA: 0x00040FAE File Offset: 0x0003F1AE
		// (set) Token: 0x06002723 RID: 10019 RVA: 0x00040FB5 File Offset: 0x0003F1B5
		public static float brightness
		{
			get
			{
				return Screen.brightness;
			}
			set
			{
				Screen.brightness = value;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06002724 RID: 10020 RVA: 0x00040FBE File Offset: 0x0003F1BE
		// (set) Token: 0x06002725 RID: 10021 RVA: 0x00040FC5 File Offset: 0x0003F1C5
		public static bool autorotateToLandscapeLeft
		{
			get
			{
				return Screen.autorotateToLandscapeLeft;
			}
			set
			{
				Screen.autorotateToLandscapeLeft = value;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06002726 RID: 10022 RVA: 0x00040FCE File Offset: 0x0003F1CE
		// (set) Token: 0x06002727 RID: 10023 RVA: 0x00040FD5 File Offset: 0x0003F1D5
		public static bool autorotateToLandscapeRight
		{
			get
			{
				return Screen.autorotateToLandscapeRight;
			}
			set
			{
				Screen.autorotateToLandscapeRight = value;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06002728 RID: 10024 RVA: 0x00040FDE File Offset: 0x0003F1DE
		// (set) Token: 0x06002729 RID: 10025 RVA: 0x00040FE5 File Offset: 0x0003F1E5
		public static bool autorotateToPortrait
		{
			get
			{
				return Screen.autorotateToPortrait;
			}
			set
			{
				Screen.autorotateToPortrait = value;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x0600272A RID: 10026 RVA: 0x00040FEE File Offset: 0x0003F1EE
		// (set) Token: 0x0600272B RID: 10027 RVA: 0x00040FF5 File Offset: 0x0003F1F5
		public static bool autorotateToPortraitUpsideDown
		{
			get
			{
				return Screen.autorotateToPortraitUpsideDown;
			}
			set
			{
				Screen.autorotateToPortraitUpsideDown = value;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x0600272C RID: 10028 RVA: 0x00040FFE File Offset: 0x0003F1FE
		public static Resolution currentResolution
		{
			get
			{
				return Screen.currentResolution;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x0600272D RID: 10029 RVA: 0x00041005 File Offset: 0x0003F205
		public static Rect[] cutouts
		{
			get
			{
				return Screen.cutouts;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x0600272E RID: 10030 RVA: 0x0004100C File Offset: 0x0003F20C
		public static float dpi
		{
			get
			{
				return Screen.dpi;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x0600272F RID: 10031 RVA: 0x00041013 File Offset: 0x0003F213
		// (set) Token: 0x06002730 RID: 10032 RVA: 0x0004101A File Offset: 0x0003F21A
		public static bool fullScreen
		{
			get
			{
				return Screen.fullScreen;
			}
			set
			{
				Screen.fullScreen = value;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06002731 RID: 10033 RVA: 0x00041023 File Offset: 0x0003F223
		// (set) Token: 0x06002732 RID: 10034 RVA: 0x0004102A File Offset: 0x0003F22A
		public static FullScreenMode fullScreenMode
		{
			get
			{
				return Screen.fullScreenMode;
			}
			set
			{
				Screen.fullScreenMode = value;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06002733 RID: 10035 RVA: 0x00041033 File Offset: 0x0003F233
		public static int height
		{
			get
			{
				return Screen.height;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06002734 RID: 10036 RVA: 0x0004103A File Offset: 0x0003F23A
		public static int width
		{
			get
			{
				return Screen.width;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x00041041 File Offset: 0x0003F241
		// (set) Token: 0x06002736 RID: 10038 RVA: 0x00041048 File Offset: 0x0003F248
		public static ScreenOrientation orientation
		{
			get
			{
				return Screen.orientation;
			}
			set
			{
				Screen.orientation = value;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002737 RID: 10039 RVA: 0x00041051 File Offset: 0x0003F251
		public static Resolution[] resolutions
		{
			get
			{
				return Screen.resolutions;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002738 RID: 10040 RVA: 0x00041058 File Offset: 0x0003F258
		public static Rect safeArea
		{
			get
			{
				return Screen.safeArea;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06002739 RID: 10041 RVA: 0x0004105F File Offset: 0x0003F25F
		// (set) Token: 0x0600273A RID: 10042 RVA: 0x00041066 File Offset: 0x0003F266
		public static int sleepTimeout
		{
			get
			{
				return Screen.sleepTimeout;
			}
			set
			{
				Screen.sleepTimeout = value;
			}
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x0004106F File Offset: 0x0003F26F
		public static void SetResolution(int width, int height, FullScreenMode fullscreenMode, [DefaultValue("0")] int preferredRefreshRate)
		{
			Screen.SetResolution(width, height, fullscreenMode, preferredRefreshRate);
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x0004107C File Offset: 0x0003F27C
		public static void SetResolution(int width, int height, FullScreenMode fullscreenMode)
		{
			Screen.SetResolution(width, height, fullscreenMode, 0);
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x00041089 File Offset: 0x0003F289
		public static void SetResolution(int width, int height, bool fullscreen, [DefaultValue("0")] int preferredRefreshRate)
		{
			Screen.SetResolution(width, height, fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed, preferredRefreshRate);
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x0004109C File Offset: 0x0003F29C
		public static void SetResolution(int width, int height, bool fullscreen)
		{
			Screen.SetResolution(width, height, fullscreen, 0);
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600273F RID: 10047 RVA: 0x000410A9 File Offset: 0x0003F2A9
		public static Vector2Int mainWindowPosition
		{
			get
			{
				return Screen.mainWindowPosition;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06002740 RID: 10048 RVA: 0x000410B0 File Offset: 0x0003F2B0
		public static DisplayInfo mainWindowDisplayInfo
		{
			get
			{
				return Screen.mainWindowDisplayInfo;
			}
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x000410B7 File Offset: 0x0003F2B7
		public static void GetDisplayLayout(List<DisplayInfo> displayLayout)
		{
			Screen.GetDisplayLayout(displayLayout);
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x000410C0 File Offset: 0x0003F2C0
		public static AsyncOperation MoveMainWindowTo(in DisplayInfo display, Vector2Int position)
		{
			return Screen.MoveMainWindowTo(in display, position);
		}
	}
}
