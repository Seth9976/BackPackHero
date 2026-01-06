using System;

namespace UnityEngine
{
	// Token: 0x020000DA RID: 218
	public enum RuntimePlatform
	{
		// Token: 0x04000276 RID: 630
		OSXEditor,
		// Token: 0x04000277 RID: 631
		OSXPlayer,
		// Token: 0x04000278 RID: 632
		WindowsPlayer,
		// Token: 0x04000279 RID: 633
		[Obsolete("WebPlayer export is no longer supported in Unity 5.4+.", true)]
		OSXWebPlayer,
		// Token: 0x0400027A RID: 634
		[Obsolete("Dashboard widget on Mac OS X export is no longer supported in Unity 5.4+.", true)]
		OSXDashboardPlayer,
		// Token: 0x0400027B RID: 635
		[Obsolete("WebPlayer export is no longer supported in Unity 5.4+.", true)]
		WindowsWebPlayer,
		// Token: 0x0400027C RID: 636
		WindowsEditor = 7,
		// Token: 0x0400027D RID: 637
		IPhonePlayer,
		// Token: 0x0400027E RID: 638
		[Obsolete("Xbox360 export is no longer supported in Unity 5.5+.")]
		XBOX360 = 10,
		// Token: 0x0400027F RID: 639
		[Obsolete("PS3 export is no longer supported in Unity >=5.5.")]
		PS3 = 9,
		// Token: 0x04000280 RID: 640
		Android = 11,
		// Token: 0x04000281 RID: 641
		[Obsolete("NaCl export is no longer supported in Unity 5.0+.")]
		NaCl,
		// Token: 0x04000282 RID: 642
		[Obsolete("FlashPlayer export is no longer supported in Unity 5.0+.")]
		FlashPlayer = 15,
		// Token: 0x04000283 RID: 643
		LinuxPlayer = 13,
		// Token: 0x04000284 RID: 644
		LinuxEditor = 16,
		// Token: 0x04000285 RID: 645
		WebGLPlayer,
		// Token: 0x04000286 RID: 646
		[Obsolete("Use WSAPlayerX86 instead")]
		MetroPlayerX86,
		// Token: 0x04000287 RID: 647
		WSAPlayerX86 = 18,
		// Token: 0x04000288 RID: 648
		[Obsolete("Use WSAPlayerX64 instead")]
		MetroPlayerX64,
		// Token: 0x04000289 RID: 649
		WSAPlayerX64 = 19,
		// Token: 0x0400028A RID: 650
		[Obsolete("Use WSAPlayerARM instead")]
		MetroPlayerARM,
		// Token: 0x0400028B RID: 651
		WSAPlayerARM = 20,
		// Token: 0x0400028C RID: 652
		[Obsolete("Windows Phone 8 was removed in 5.3")]
		WP8Player,
		// Token: 0x0400028D RID: 653
		[Obsolete("BlackBerryPlayer export is no longer supported in Unity 5.4+.")]
		BlackBerryPlayer,
		// Token: 0x0400028E RID: 654
		[Obsolete("TizenPlayer export is no longer supported in Unity 2017.3+.")]
		TizenPlayer,
		// Token: 0x0400028F RID: 655
		[Obsolete("PSP2 is no longer supported as of Unity 2018.3")]
		PSP2,
		// Token: 0x04000290 RID: 656
		PS4,
		// Token: 0x04000291 RID: 657
		[Obsolete("PSM export is no longer supported in Unity >= 5.3")]
		PSM,
		// Token: 0x04000292 RID: 658
		XboxOne,
		// Token: 0x04000293 RID: 659
		[Obsolete("SamsungTVPlayer export is no longer supported in Unity 2017.3+.")]
		SamsungTVPlayer,
		// Token: 0x04000294 RID: 660
		[Obsolete("Wii U is no longer supported in Unity 2018.1+.")]
		WiiU = 30,
		// Token: 0x04000295 RID: 661
		tvOS,
		// Token: 0x04000296 RID: 662
		Switch,
		// Token: 0x04000297 RID: 663
		Lumin,
		// Token: 0x04000298 RID: 664
		Stadia,
		// Token: 0x04000299 RID: 665
		CloudRendering,
		// Token: 0x0400029A RID: 666
		[Obsolete("GameCoreScarlett is deprecated, please use GameCoreXboxSeries (UnityUpgradable) -> GameCoreXboxSeries", false)]
		GameCoreScarlett = -1,
		// Token: 0x0400029B RID: 667
		GameCoreXboxSeries = 36,
		// Token: 0x0400029C RID: 668
		GameCoreXboxOne,
		// Token: 0x0400029D RID: 669
		PS5,
		// Token: 0x0400029E RID: 670
		EmbeddedLinuxArm64,
		// Token: 0x0400029F RID: 671
		EmbeddedLinuxArm32,
		// Token: 0x040002A0 RID: 672
		EmbeddedLinuxX64,
		// Token: 0x040002A1 RID: 673
		EmbeddedLinuxX86,
		// Token: 0x040002A2 RID: 674
		LinuxServer,
		// Token: 0x040002A3 RID: 675
		WindowsServer,
		// Token: 0x040002A4 RID: 676
		OSXServer
	}
}
