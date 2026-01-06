using System;

namespace Steamworks
{
	// Token: 0x0200001F RID: 31
	public static class SteamRemotePlay
	{
		// Token: 0x0600037C RID: 892 RVA: 0x00009372 File Offset: 0x00007572
		public static uint GetSessionCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemotePlay_GetSessionCount(CSteamAPIContext.GetSteamRemotePlay());
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00009383 File Offset: 0x00007583
		public static RemotePlaySessionID_t GetSessionID(int iSessionIndex)
		{
			InteropHelp.TestIfAvailableClient();
			return (RemotePlaySessionID_t)NativeMethods.ISteamRemotePlay_GetSessionID(CSteamAPIContext.GetSteamRemotePlay(), iSessionIndex);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000939A File Offset: 0x0000759A
		public static CSteamID GetSessionSteamID(RemotePlaySessionID_t unSessionID)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamRemotePlay_GetSessionSteamID(CSteamAPIContext.GetSteamRemotePlay(), unSessionID);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000093B1 File Offset: 0x000075B1
		public static string GetSessionClientName(RemotePlaySessionID_t unSessionID)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamRemotePlay_GetSessionClientName(CSteamAPIContext.GetSteamRemotePlay(), unSessionID));
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000093C8 File Offset: 0x000075C8
		public static ESteamDeviceFormFactor GetSessionClientFormFactor(RemotePlaySessionID_t unSessionID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemotePlay_GetSessionClientFormFactor(CSteamAPIContext.GetSteamRemotePlay(), unSessionID);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000093DA File Offset: 0x000075DA
		public static bool BGetSessionClientResolution(RemotePlaySessionID_t unSessionID, out int pnResolutionX, out int pnResolutionY)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemotePlay_BGetSessionClientResolution(CSteamAPIContext.GetSteamRemotePlay(), unSessionID, out pnResolutionX, out pnResolutionY);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000093EE File Offset: 0x000075EE
		public static bool BSendRemotePlayTogetherInvite(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemotePlay_BSendRemotePlayTogetherInvite(CSteamAPIContext.GetSteamRemotePlay(), steamIDFriend);
		}
	}
}
