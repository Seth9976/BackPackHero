using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001AD RID: 429
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct SteamNetworkingIdentity : IEquatable<SteamNetworkingIdentity>
	{
		// Token: 0x06000A60 RID: 2656 RVA: 0x0000F5D8 File Offset: 0x0000D7D8
		public void Clear()
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_Clear(ref this);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0000F5E0 File Offset: 0x0000D7E0
		public bool IsInvalid()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_IsInvalid(ref this);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0000F5E8 File Offset: 0x0000D7E8
		public void SetSteamID(CSteamID steamID)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetSteamID(ref this, (ulong)steamID);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0000F5F6 File Offset: 0x0000D7F6
		public CSteamID GetSteamID()
		{
			return (CSteamID)NativeMethods.SteamAPI_SteamNetworkingIdentity_GetSteamID(ref this);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0000F603 File Offset: 0x0000D803
		public void SetSteamID64(ulong steamID)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetSteamID64(ref this, steamID);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0000F60C File Offset: 0x0000D80C
		public ulong GetSteamID64()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetSteamID64(ref this);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0000F614 File Offset: 0x0000D814
		public void SetIPAddr(SteamNetworkingIPAddr addr)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetIPAddr(ref this, ref addr);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0000F61F File Offset: 0x0000D81F
		public SteamNetworkingIPAddr GetIPAddr()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0000F626 File Offset: 0x0000D826
		public void SetIPv4Addr(uint nIPv4, ushort nPort)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetIPv4Addr(ref this, nIPv4, nPort);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0000F630 File Offset: 0x0000D830
		public uint GetIPv4()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetIPv4(ref this);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0000F638 File Offset: 0x0000D838
		public ESteamNetworkingFakeIPType GetFakeIPType()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetFakeIPType(ref this);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0000F640 File Offset: 0x0000D840
		public bool IsFakeIP()
		{
			return this.GetFakeIPType() > ESteamNetworkingFakeIPType.k_ESteamNetworkingFakeIPType_NotFake;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0000F64B File Offset: 0x0000D84B
		public void SetLocalHost()
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetLocalHost(ref this);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0000F653 File Offset: 0x0000D853
		public bool IsLocalHost()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_IsLocalHost(ref this);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0000F65C File Offset: 0x0000D85C
		public bool SetGenericString(string pszString)
		{
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszString))
			{
				flag = NativeMethods.SteamAPI_SteamNetworkingIdentity_SetGenericString(ref this, utf8StringHandle);
			}
			return flag;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0000F698 File Offset: 0x0000D898
		public string GetGenericString()
		{
			return InteropHelp.PtrToStringUTF8(NativeMethods.SteamAPI_SteamNetworkingIdentity_GetGenericString(ref this));
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0000F6A5 File Offset: 0x0000D8A5
		public bool SetGenericBytes(byte[] data, uint cbLen)
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_SetGenericBytes(ref this, data, cbLen);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0000F6AF File Offset: 0x0000D8AF
		public byte[] GetGenericBytes(out int cbLen)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0000F6B6 File Offset: 0x0000D8B6
		public bool Equals(SteamNetworkingIdentity x)
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_IsEqualTo(ref this, ref x);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0000F6C0 File Offset: 0x0000D8C0
		public void ToString(out string buf)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(128);
			NativeMethods.SteamAPI_SteamNetworkingIdentity_ToString(ref this, intPtr, 128U);
			buf = InteropHelp.PtrToStringUTF8(intPtr);
			Marshal.FreeHGlobal(intPtr);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0000F6F4 File Offset: 0x0000D8F4
		public bool ParseString(string pszStr)
		{
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszStr))
			{
				flag = NativeMethods.SteamAPI_SteamNetworkingIdentity_ParseString(ref this, utf8StringHandle);
			}
			return flag;
		}

		// Token: 0x04000A82 RID: 2690
		public ESteamNetworkingIdentityType m_eType;

		// Token: 0x04000A83 RID: 2691
		private int m_cbSize;

		// Token: 0x04000A84 RID: 2692
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		private uint[] m_reserved;

		// Token: 0x04000A85 RID: 2693
		public const int k_cchMaxString = 128;

		// Token: 0x04000A86 RID: 2694
		public const int k_cchMaxGenericString = 32;

		// Token: 0x04000A87 RID: 2695
		public const int k_cbMaxGenericBytes = 32;
	}
}
