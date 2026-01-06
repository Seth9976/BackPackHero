using System;

namespace Steamworks
{
	// Token: 0x0200001B RID: 27
	public static class SteamNetworkingMessages
	{
		// Token: 0x06000326 RID: 806 RVA: 0x00008B12 File Offset: 0x00006D12
		public static EResult SendMessageToUser(ref SteamNetworkingIdentity identityRemote, IntPtr pubData, uint cubData, int nSendFlags, int nRemoteChannel)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingMessages_SendMessageToUser(CSteamAPIContext.GetSteamNetworkingMessages(), ref identityRemote, pubData, cubData, nSendFlags, nRemoteChannel);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00008B29 File Offset: 0x00006D29
		public static int ReceiveMessagesOnChannel(int nLocalChannel, IntPtr[] ppOutMessages, int nMaxMessages)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingMessages_ReceiveMessagesOnChannel(CSteamAPIContext.GetSteamNetworkingMessages(), nLocalChannel, ppOutMessages, nMaxMessages);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00008B3D File Offset: 0x00006D3D
		public static bool AcceptSessionWithUser(ref SteamNetworkingIdentity identityRemote)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingMessages_AcceptSessionWithUser(CSteamAPIContext.GetSteamNetworkingMessages(), ref identityRemote);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00008B4F File Offset: 0x00006D4F
		public static bool CloseSessionWithUser(ref SteamNetworkingIdentity identityRemote)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingMessages_CloseSessionWithUser(CSteamAPIContext.GetSteamNetworkingMessages(), ref identityRemote);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00008B61 File Offset: 0x00006D61
		public static bool CloseChannelWithUser(ref SteamNetworkingIdentity identityRemote, int nLocalChannel)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingMessages_CloseChannelWithUser(CSteamAPIContext.GetSteamNetworkingMessages(), ref identityRemote, nLocalChannel);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00008B74 File Offset: 0x00006D74
		public static ESteamNetworkingConnectionState GetSessionConnectionInfo(ref SteamNetworkingIdentity identityRemote, out SteamNetConnectionInfo_t pConnectionInfo, out SteamNetConnectionRealTimeStatus_t pQuickStatus)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingMessages_GetSessionConnectionInfo(CSteamAPIContext.GetSteamNetworkingMessages(), ref identityRemote, out pConnectionInfo, out pQuickStatus);
		}
	}
}
