using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000015 RID: 21
	public static class SteamMatchmakingServers
	{
		// Token: 0x060002BC RID: 700 RVA: 0x00007FF5 File Offset: 0x000061F5
		public static HServerListRequest RequestInternetServerList(AppId_t iApp, MatchMakingKeyValuePair_t[] ppchFilters, uint nFilters, ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerListRequest)NativeMethods.ISteamMatchmakingServers_RequestInternetServerList(CSteamAPIContext.GetSteamMatchmakingServers(), iApp, new MMKVPMarshaller(ppchFilters), nFilters, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000801E File Offset: 0x0000621E
		public static HServerListRequest RequestLANServerList(AppId_t iApp, ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerListRequest)NativeMethods.ISteamMatchmakingServers_RequestLANServerList(CSteamAPIContext.GetSteamMatchmakingServers(), iApp, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000803B File Offset: 0x0000623B
		public static HServerListRequest RequestFriendsServerList(AppId_t iApp, MatchMakingKeyValuePair_t[] ppchFilters, uint nFilters, ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerListRequest)NativeMethods.ISteamMatchmakingServers_RequestFriendsServerList(CSteamAPIContext.GetSteamMatchmakingServers(), iApp, new MMKVPMarshaller(ppchFilters), nFilters, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00008064 File Offset: 0x00006264
		public static HServerListRequest RequestFavoritesServerList(AppId_t iApp, MatchMakingKeyValuePair_t[] ppchFilters, uint nFilters, ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerListRequest)NativeMethods.ISteamMatchmakingServers_RequestFavoritesServerList(CSteamAPIContext.GetSteamMatchmakingServers(), iApp, new MMKVPMarshaller(ppchFilters), nFilters, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000808D File Offset: 0x0000628D
		public static HServerListRequest RequestHistoryServerList(AppId_t iApp, MatchMakingKeyValuePair_t[] ppchFilters, uint nFilters, ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerListRequest)NativeMethods.ISteamMatchmakingServers_RequestHistoryServerList(CSteamAPIContext.GetSteamMatchmakingServers(), iApp, new MMKVPMarshaller(ppchFilters), nFilters, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x000080B6 File Offset: 0x000062B6
		public static HServerListRequest RequestSpectatorServerList(AppId_t iApp, MatchMakingKeyValuePair_t[] ppchFilters, uint nFilters, ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerListRequest)NativeMethods.ISteamMatchmakingServers_RequestSpectatorServerList(CSteamAPIContext.GetSteamMatchmakingServers(), iApp, new MMKVPMarshaller(ppchFilters), nFilters, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x000080DF File Offset: 0x000062DF
		public static void ReleaseRequest(HServerListRequest hServerListRequest)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmakingServers_ReleaseRequest(CSteamAPIContext.GetSteamMatchmakingServers(), hServerListRequest);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000080F1 File Offset: 0x000062F1
		public static gameserveritem_t GetServerDetails(HServerListRequest hRequest, int iServer)
		{
			InteropHelp.TestIfAvailableClient();
			return (gameserveritem_t)Marshal.PtrToStructure(NativeMethods.ISteamMatchmakingServers_GetServerDetails(CSteamAPIContext.GetSteamMatchmakingServers(), hRequest, iServer), typeof(gameserveritem_t));
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00008118 File Offset: 0x00006318
		public static void CancelQuery(HServerListRequest hRequest)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmakingServers_CancelQuery(CSteamAPIContext.GetSteamMatchmakingServers(), hRequest);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000812A File Offset: 0x0000632A
		public static void RefreshQuery(HServerListRequest hRequest)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmakingServers_RefreshQuery(CSteamAPIContext.GetSteamMatchmakingServers(), hRequest);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000813C File Offset: 0x0000633C
		public static bool IsRefreshing(HServerListRequest hRequest)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmakingServers_IsRefreshing(CSteamAPIContext.GetSteamMatchmakingServers(), hRequest);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000814E File Offset: 0x0000634E
		public static int GetServerCount(HServerListRequest hRequest)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmakingServers_GetServerCount(CSteamAPIContext.GetSteamMatchmakingServers(), hRequest);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00008160 File Offset: 0x00006360
		public static void RefreshServer(HServerListRequest hRequest, int iServer)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmakingServers_RefreshServer(CSteamAPIContext.GetSteamMatchmakingServers(), hRequest, iServer);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00008173 File Offset: 0x00006373
		public static HServerQuery PingServer(uint unIP, ushort usPort, ISteamMatchmakingPingResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerQuery)NativeMethods.ISteamMatchmakingServers_PingServer(CSteamAPIContext.GetSteamMatchmakingServers(), unIP, usPort, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00008191 File Offset: 0x00006391
		public static HServerQuery PlayerDetails(uint unIP, ushort usPort, ISteamMatchmakingPlayersResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerQuery)NativeMethods.ISteamMatchmakingServers_PlayerDetails(CSteamAPIContext.GetSteamMatchmakingServers(), unIP, usPort, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000081AF File Offset: 0x000063AF
		public static HServerQuery ServerRules(uint unIP, ushort usPort, ISteamMatchmakingRulesResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerQuery)NativeMethods.ISteamMatchmakingServers_ServerRules(CSteamAPIContext.GetSteamMatchmakingServers(), unIP, usPort, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000081CD File Offset: 0x000063CD
		public static void CancelServerQuery(HServerQuery hServerQuery)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmakingServers_CancelServerQuery(CSteamAPIContext.GetSteamMatchmakingServers(), hServerQuery);
		}
	}
}
