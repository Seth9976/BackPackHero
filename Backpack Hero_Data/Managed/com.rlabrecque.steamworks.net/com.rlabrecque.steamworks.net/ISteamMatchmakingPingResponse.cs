using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000181 RID: 385
	public class ISteamMatchmakingPingResponse
	{
		// Token: 0x060008C3 RID: 2243 RVA: 0x0000D074 File Offset: 0x0000B274
		public ISteamMatchmakingPingResponse(ISteamMatchmakingPingResponse.ServerResponded onServerResponded, ISteamMatchmakingPingResponse.ServerFailedToRespond onServerFailedToRespond)
		{
			if (onServerResponded == null || onServerFailedToRespond == null)
			{
				throw new ArgumentNullException();
			}
			this.m_ServerResponded = onServerResponded;
			this.m_ServerFailedToRespond = onServerFailedToRespond;
			this.m_VTable = new ISteamMatchmakingPingResponse.VTable
			{
				m_VTServerResponded = new ISteamMatchmakingPingResponse.InternalServerResponded(this.InternalOnServerResponded),
				m_VTServerFailedToRespond = new ISteamMatchmakingPingResponse.InternalServerFailedToRespond(this.InternalOnServerFailedToRespond)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingPingResponse.VTable)));
			Marshal.StructureToPtr<ISteamMatchmakingPingResponse.VTable>(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, GCHandleType.Pinned);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0000D114 File Offset: 0x0000B314
		~ISteamMatchmakingPingResponse()
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

		// Token: 0x060008C5 RID: 2245 RVA: 0x0000D170 File Offset: 0x0000B370
		private void InternalOnServerResponded(IntPtr thisptr, gameserveritem_t server)
		{
			this.m_ServerResponded(server);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0000D17E File Offset: 0x0000B37E
		private void InternalOnServerFailedToRespond(IntPtr thisptr)
		{
			this.m_ServerFailedToRespond();
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0000D18B File Offset: 0x0000B38B
		public static explicit operator IntPtr(ISteamMatchmakingPingResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x040009F9 RID: 2553
		private ISteamMatchmakingPingResponse.VTable m_VTable;

		// Token: 0x040009FA RID: 2554
		private IntPtr m_pVTable;

		// Token: 0x040009FB RID: 2555
		private GCHandle m_pGCHandle;

		// Token: 0x040009FC RID: 2556
		private ISteamMatchmakingPingResponse.ServerResponded m_ServerResponded;

		// Token: 0x040009FD RID: 2557
		private ISteamMatchmakingPingResponse.ServerFailedToRespond m_ServerFailedToRespond;

		// Token: 0x020001D1 RID: 465
		// (Invoke) Token: 0x06000B84 RID: 2948
		public delegate void ServerResponded(gameserveritem_t server);

		// Token: 0x020001D2 RID: 466
		// (Invoke) Token: 0x06000B88 RID: 2952
		public delegate void ServerFailedToRespond();

		// Token: 0x020001D3 RID: 467
		// (Invoke) Token: 0x06000B8C RID: 2956
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		private delegate void InternalServerResponded(IntPtr thisptr, gameserveritem_t server);

		// Token: 0x020001D4 RID: 468
		// (Invoke) Token: 0x06000B90 RID: 2960
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		private delegate void InternalServerFailedToRespond(IntPtr thisptr);

		// Token: 0x020001D5 RID: 469
		[StructLayout(LayoutKind.Sequential)]
		private class VTable
		{
			// Token: 0x04000AC6 RID: 2758
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingPingResponse.InternalServerResponded m_VTServerResponded;

			// Token: 0x04000AC7 RID: 2759
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingPingResponse.InternalServerFailedToRespond m_VTServerFailedToRespond;
		}
	}
}
