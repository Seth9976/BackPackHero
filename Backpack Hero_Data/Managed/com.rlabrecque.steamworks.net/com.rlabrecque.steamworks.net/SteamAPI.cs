using System;

namespace Steamworks
{
	// Token: 0x02000185 RID: 389
	public static class SteamAPI
	{
		// Token: 0x060008D5 RID: 2261 RVA: 0x0000D484 File Offset: 0x0000B684
		public static bool Init()
		{
			InteropHelp.TestIfPlatformSupported();
			bool flag = NativeMethods.SteamAPI_Init();
			if (flag)
			{
				flag = CSteamAPIContext.Init();
			}
			if (flag)
			{
				CallbackDispatcher.Initialize();
			}
			return flag;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0000D4AE File Offset: 0x0000B6AE
		public static void Shutdown()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamAPI_Shutdown();
			CSteamAPIContext.Clear();
			CallbackDispatcher.Shutdown();
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0000D4C4 File Offset: 0x0000B6C4
		public static bool RestartAppIfNecessary(AppId_t unOwnAppID)
		{
			InteropHelp.TestIfPlatformSupported();
			return NativeMethods.SteamAPI_RestartAppIfNecessary(unOwnAppID);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0000D4D1 File Offset: 0x0000B6D1
		public static void ReleaseCurrentThreadMemory()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamAPI_ReleaseCurrentThreadMemory();
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0000D4DD File Offset: 0x0000B6DD
		public static void RunCallbacks()
		{
			CallbackDispatcher.RunFrame(false);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0000D4E5 File Offset: 0x0000B6E5
		public static bool IsSteamRunning()
		{
			InteropHelp.TestIfPlatformSupported();
			return NativeMethods.SteamAPI_IsSteamRunning();
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0000D4F1 File Offset: 0x0000B6F1
		public static HSteamPipe GetHSteamPipe()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamPipe)NativeMethods.SteamAPI_GetHSteamPipe();
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0000D502 File Offset: 0x0000B702
		public static HSteamUser GetHSteamUser()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamUser)NativeMethods.SteamAPI_GetHSteamUser();
		}
	}
}
