using System;
using UnityEngine;

namespace Unity.Services.Analytics.Platform
{
	// Token: 0x02000042 RID: 66
	public static class Runtime
	{
		// Token: 0x06000152 RID: 338 RVA: 0x00005054 File Offset: 0x00003254
		public static string Name()
		{
			return Runtime.GetPlatform().ToString();
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005074 File Offset: 0x00003274
		private static UA2PlatformCode GetPlatform()
		{
			RuntimePlatform platform = Application.platform;
			if (platform <= RuntimePlatform.PS4)
			{
				switch (platform)
				{
				case RuntimePlatform.OSXEditor:
				case RuntimePlatform.OSXPlayer:
					return UA2PlatformCode.MAC_CLIENT;
				case RuntimePlatform.WindowsPlayer:
				case RuntimePlatform.WindowsEditor:
				case RuntimePlatform.LinuxPlayer:
				case RuntimePlatform.LinuxEditor:
					return UA2PlatformCode.PC_CLIENT;
				case RuntimePlatform.OSXWebPlayer:
				case RuntimePlatform.OSXDashboardPlayer:
				case RuntimePlatform.WindowsWebPlayer:
				case (RuntimePlatform)6:
				case RuntimePlatform.PS3:
				case RuntimePlatform.XBOX360:
				case RuntimePlatform.NaCl:
				case (RuntimePlatform)14:
				case RuntimePlatform.FlashPlayer:
					break;
				case RuntimePlatform.IPhonePlayer:
					return UA2PlatformCode.IOS;
				case RuntimePlatform.Android:
					return UA2PlatformCode.ANDROID;
				case RuntimePlatform.WebGLPlayer:
					return UA2PlatformCode.WEB;
				case RuntimePlatform.MetroPlayerX86:
				case RuntimePlatform.MetroPlayerX64:
				case RuntimePlatform.MetroPlayerARM:
					if (SystemInfo.deviceType != DeviceType.Handheld)
					{
						return UA2PlatformCode.PC_CLIENT;
					}
					return UA2PlatformCode.WINDOWS_MOBILE;
				default:
					if (platform == RuntimePlatform.PS4)
					{
						return UA2PlatformCode.PS4;
					}
					break;
				}
			}
			else
			{
				if (platform == RuntimePlatform.XboxOne)
				{
					return UA2PlatformCode.XBOXONE;
				}
				if (platform == RuntimePlatform.tvOS)
				{
					return UA2PlatformCode.IOS_TV;
				}
				if (platform == RuntimePlatform.Switch)
				{
					return UA2PlatformCode.SWITCH;
				}
			}
			return UA2PlatformCode.UNKNOWN;
		}
	}
}
