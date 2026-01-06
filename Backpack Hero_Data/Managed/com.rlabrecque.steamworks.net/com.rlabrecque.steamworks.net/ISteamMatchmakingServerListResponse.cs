using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000180 RID: 384
	public class ISteamMatchmakingServerListResponse
	{
		// Token: 0x060008BD RID: 2237 RVA: 0x0000CEB0 File Offset: 0x0000B0B0
		public ISteamMatchmakingServerListResponse(ISteamMatchmakingServerListResponse.ServerResponded onServerResponded, ISteamMatchmakingServerListResponse.ServerFailedToRespond onServerFailedToRespond, ISteamMatchmakingServerListResponse.RefreshComplete onRefreshComplete)
		{
			if (onServerResponded == null || onServerFailedToRespond == null || onRefreshComplete == null)
			{
				throw new ArgumentNullException();
			}
			this.m_ServerResponded = onServerResponded;
			this.m_ServerFailedToRespond = onServerFailedToRespond;
			this.m_RefreshComplete = onRefreshComplete;
			this.m_VTable = new ISteamMatchmakingServerListResponse.VTable
			{
				m_VTServerResponded = new ISteamMatchmakingServerListResponse.InternalServerResponded(this.InternalOnServerResponded),
				m_VTServerFailedToRespond = new ISteamMatchmakingServerListResponse.InternalServerFailedToRespond(this.InternalOnServerFailedToRespond),
				m_VTRefreshComplete = new ISteamMatchmakingServerListResponse.InternalRefreshComplete(this.InternalOnRefreshComplete)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingServerListResponse.VTable)));
			Marshal.StructureToPtr<ISteamMatchmakingServerListResponse.VTable>(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, GCHandleType.Pinned);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0000CF6C File Offset: 0x0000B16C
		~ISteamMatchmakingServerListResponse()
		{
			if (this.m_pVTable != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pVTable);
			}
			if (this.m_pGCHandle.IsAllocated)
			{
				this.m_pGCHandle.Free();
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0000CFC8 File Offset: 0x0000B1C8
		private void InternalOnServerResponded(IntPtr thisptr, HServerListRequest hRequest, int iServer)
		{
			try
			{
				this.m_ServerResponded(hRequest, iServer);
			}
			catch (Exception ex)
			{
				CallbackDispatcher.ExceptionHandler(ex);
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0000CFFC File Offset: 0x0000B1FC
		private void InternalOnServerFailedToRespond(IntPtr thisptr, HServerListRequest hRequest, int iServer)
		{
			try
			{
				this.m_ServerFailedToRespond(hRequest, iServer);
			}
			catch (Exception ex)
			{
				CallbackDispatcher.ExceptionHandler(ex);
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0000D030 File Offset: 0x0000B230
		private void InternalOnRefreshComplete(IntPtr thisptr, HServerListRequest hRequest, EMatchMakingServerResponse response)
		{
			try
			{
				this.m_RefreshComplete(hRequest, response);
			}
			catch (Exception ex)
			{
				CallbackDispatcher.ExceptionHandler(ex);
			}
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0000D064 File Offset: 0x0000B264
		public static explicit operator IntPtr(ISteamMatchmakingServerListResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x040009F3 RID: 2547
		private ISteamMatchmakingServerListResponse.VTable m_VTable;

		// Token: 0x040009F4 RID: 2548
		private IntPtr m_pVTable;

		// Token: 0x040009F5 RID: 2549
		private GCHandle m_pGCHandle;

		// Token: 0x040009F6 RID: 2550
		private ISteamMatchmakingServerListResponse.ServerResponded m_ServerResponded;

		// Token: 0x040009F7 RID: 2551
		private ISteamMatchmakingServerListResponse.ServerFailedToRespond m_ServerFailedToRespond;

		// Token: 0x040009F8 RID: 2552
		private ISteamMatchmakingServerListResponse.RefreshComplete m_RefreshComplete;

		// Token: 0x020001CA RID: 458
		// (Invoke) Token: 0x06000B6B RID: 2923
		public delegate void ServerResponded(HServerListRequest hRequest, int iServer);

		// Token: 0x020001CB RID: 459
		// (Invoke) Token: 0x06000B6F RID: 2927
		public delegate void ServerFailedToRespond(HServerListRequest hRequest, int iServer);

		// Token: 0x020001CC RID: 460
		// (Invoke) Token: 0x06000B73 RID: 2931
		public delegate void RefreshComplete(HServerListRequest hRequest, EMatchMakingServerResponse response);

		// Token: 0x020001CD RID: 461
		// (Invoke) Token: 0x06000B77 RID: 2935
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		private delegate void InternalServerResponded(IntPtr thisptr, HServerListRequest hRequest, int iServer);

		// Token: 0x020001CE RID: 462
		// (Invoke) Token: 0x06000B7B RID: 2939
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		private delegate void InternalServerFailedToRespond(IntPtr thisptr, HServerListRequest hRequest, int iServer);

		// Token: 0x020001CF RID: 463
		// (Invoke) Token: 0x06000B7F RID: 2943
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		private delegate void InternalRefreshComplete(IntPtr thisptr, HServerListRequest hRequest, EMatchMakingServerResponse response);

		// Token: 0x020001D0 RID: 464
		[StructLayout(LayoutKind.Sequential)]
		private class VTable
		{
			// Token: 0x04000AC3 RID: 2755
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingServerListResponse.InternalServerResponded m_VTServerResponded;

			// Token: 0x04000AC4 RID: 2756
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingServerListResponse.InternalServerFailedToRespond m_VTServerFailedToRespond;

			// Token: 0x04000AC5 RID: 2757
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingServerListResponse.InternalRefreshComplete m_VTRefreshComplete;
		}
	}
}
