using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000183 RID: 387
	public class ISteamMatchmakingRulesResponse
	{
		// Token: 0x060008CE RID: 2254 RVA: 0x0000D2F0 File Offset: 0x0000B4F0
		public ISteamMatchmakingRulesResponse(ISteamMatchmakingRulesResponse.RulesResponded onRulesResponded, ISteamMatchmakingRulesResponse.RulesFailedToRespond onRulesFailedToRespond, ISteamMatchmakingRulesResponse.RulesRefreshComplete onRulesRefreshComplete)
		{
			if (onRulesResponded == null || onRulesFailedToRespond == null || onRulesRefreshComplete == null)
			{
				throw new ArgumentNullException();
			}
			this.m_RulesResponded = onRulesResponded;
			this.m_RulesFailedToRespond = onRulesFailedToRespond;
			this.m_RulesRefreshComplete = onRulesRefreshComplete;
			this.m_VTable = new ISteamMatchmakingRulesResponse.VTable
			{
				m_VTRulesResponded = new ISteamMatchmakingRulesResponse.InternalRulesResponded(this.InternalOnRulesResponded),
				m_VTRulesFailedToRespond = new ISteamMatchmakingRulesResponse.InternalRulesFailedToRespond(this.InternalOnRulesFailedToRespond),
				m_VTRulesRefreshComplete = new ISteamMatchmakingRulesResponse.InternalRulesRefreshComplete(this.InternalOnRulesRefreshComplete)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingRulesResponse.VTable)));
			Marshal.StructureToPtr<ISteamMatchmakingRulesResponse.VTable>(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, GCHandleType.Pinned);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0000D3AC File Offset: 0x0000B5AC
		~ISteamMatchmakingRulesResponse()
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

		// Token: 0x060008D0 RID: 2256 RVA: 0x0000D408 File Offset: 0x0000B608
		private void InternalOnRulesResponded(IntPtr thisptr, IntPtr pchRule, IntPtr pchValue)
		{
			this.m_RulesResponded(InteropHelp.PtrToStringUTF8(pchRule), InteropHelp.PtrToStringUTF8(pchValue));
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0000D421 File Offset: 0x0000B621
		private void InternalOnRulesFailedToRespond(IntPtr thisptr)
		{
			this.m_RulesFailedToRespond();
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0000D42E File Offset: 0x0000B62E
		private void InternalOnRulesRefreshComplete(IntPtr thisptr)
		{
			this.m_RulesRefreshComplete();
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0000D43B File Offset: 0x0000B63B
		public static explicit operator IntPtr(ISteamMatchmakingRulesResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x04000A04 RID: 2564
		private ISteamMatchmakingRulesResponse.VTable m_VTable;

		// Token: 0x04000A05 RID: 2565
		private IntPtr m_pVTable;

		// Token: 0x04000A06 RID: 2566
		private GCHandle m_pGCHandle;

		// Token: 0x04000A07 RID: 2567
		private ISteamMatchmakingRulesResponse.RulesResponded m_RulesResponded;

		// Token: 0x04000A08 RID: 2568
		private ISteamMatchmakingRulesResponse.RulesFailedToRespond m_RulesFailedToRespond;

		// Token: 0x04000A09 RID: 2569
		private ISteamMatchmakingRulesResponse.RulesRefreshComplete m_RulesRefreshComplete;

		// Token: 0x020001DD RID: 477
		// (Invoke) Token: 0x06000BAE RID: 2990
		public delegate void RulesResponded(string pchRule, string pchValue);

		// Token: 0x020001DE RID: 478
		// (Invoke) Token: 0x06000BB2 RID: 2994
		public delegate void RulesFailedToRespond();

		// Token: 0x020001DF RID: 479
		// (Invoke) Token: 0x06000BB6 RID: 2998
		public delegate void RulesRefreshComplete();

		// Token: 0x020001E0 RID: 480
		// (Invoke) Token: 0x06000BBA RID: 3002
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		public delegate void InternalRulesResponded(IntPtr thisptr, IntPtr pchRule, IntPtr pchValue);

		// Token: 0x020001E1 RID: 481
		// (Invoke) Token: 0x06000BBE RID: 3006
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		public delegate void InternalRulesFailedToRespond(IntPtr thisptr);

		// Token: 0x020001E2 RID: 482
		// (Invoke) Token: 0x06000BC2 RID: 3010
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		public delegate void InternalRulesRefreshComplete(IntPtr thisptr);

		// Token: 0x020001E3 RID: 483
		[StructLayout(LayoutKind.Sequential)]
		private class VTable
		{
			// Token: 0x04000ACB RID: 2763
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingRulesResponse.InternalRulesResponded m_VTRulesResponded;

			// Token: 0x04000ACC RID: 2764
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingRulesResponse.InternalRulesFailedToRespond m_VTRulesFailedToRespond;

			// Token: 0x04000ACD RID: 2765
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingRulesResponse.InternalRulesRefreshComplete m_VTRulesRefreshComplete;
		}
	}
}
