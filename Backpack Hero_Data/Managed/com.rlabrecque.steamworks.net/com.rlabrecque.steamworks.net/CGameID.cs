using System;

namespace Steamworks
{
	// Token: 0x0200018E RID: 398
	[Serializable]
	public struct CGameID : IEquatable<CGameID>, IComparable<CGameID>
	{
		// Token: 0x0600093E RID: 2366 RVA: 0x0000E38B File Offset: 0x0000C58B
		public CGameID(ulong GameID)
		{
			this.m_GameID = GameID;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0000E394 File Offset: 0x0000C594
		public CGameID(AppId_t nAppID)
		{
			this.m_GameID = 0UL;
			this.SetAppID(nAppID);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0000E3A5 File Offset: 0x0000C5A5
		public CGameID(AppId_t nAppID, uint nModID)
		{
			this.m_GameID = 0UL;
			this.SetAppID(nAppID);
			this.SetType(CGameID.EGameIDType.k_EGameIDTypeGameMod);
			this.SetModID(nModID);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0000E3C4 File Offset: 0x0000C5C4
		public bool IsSteamApp()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeApp;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0000E3CF File Offset: 0x0000C5CF
		public bool IsMod()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeGameMod;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0000E3DA File Offset: 0x0000C5DA
		public bool IsShortcut()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeShortcut;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0000E3E5 File Offset: 0x0000C5E5
		public bool IsP2PFile()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeP2P;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0000E3F0 File Offset: 0x0000C5F0
		public AppId_t AppID()
		{
			return new AppId_t((uint)(this.m_GameID & 16777215UL));
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0000E405 File Offset: 0x0000C605
		public CGameID.EGameIDType Type()
		{
			return (CGameID.EGameIDType)((this.m_GameID >> 24) & 255UL);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0000E418 File Offset: 0x0000C618
		public uint ModID()
		{
			return (uint)((this.m_GameID >> 32) & (ulong)(-1));
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0000E428 File Offset: 0x0000C628
		public bool IsValid()
		{
			switch (this.Type())
			{
			case CGameID.EGameIDType.k_EGameIDTypeApp:
				return this.AppID() != AppId_t.Invalid;
			case CGameID.EGameIDType.k_EGameIDTypeGameMod:
				return this.AppID() != AppId_t.Invalid && (this.ModID() & 2147483648U) > 0U;
			case CGameID.EGameIDType.k_EGameIDTypeShortcut:
				return (this.ModID() & 2147483648U) > 0U;
			case CGameID.EGameIDType.k_EGameIDTypeP2P:
				return this.AppID() == AppId_t.Invalid && (this.ModID() & 2147483648U) > 0U;
			default:
				return false;
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0000E4BE File Offset: 0x0000C6BE
		public void Reset()
		{
			this.m_GameID = 0UL;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0000E4C8 File Offset: 0x0000C6C8
		public void Set(ulong GameID)
		{
			this.m_GameID = GameID;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0000E4D1 File Offset: 0x0000C6D1
		private void SetAppID(AppId_t other)
		{
			this.m_GameID = (this.m_GameID & 18446744073692774400UL) | ((ulong)(uint)other & 16777215UL);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0000E4F5 File Offset: 0x0000C6F5
		private void SetType(CGameID.EGameIDType other)
		{
			this.m_GameID = (this.m_GameID & 18446744069431361535UL) | (ulong)((ulong)((long)other & 255L) << 24);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0000E51A File Offset: 0x0000C71A
		private void SetModID(uint other)
		{
			this.m_GameID = (this.m_GameID & (ulong)(-1)) | (((ulong)other & (ulong)(-1)) << 32);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0000E534 File Offset: 0x0000C734
		public override string ToString()
		{
			return this.m_GameID.ToString();
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0000E541 File Offset: 0x0000C741
		public override bool Equals(object other)
		{
			return other is CGameID && this == (CGameID)other;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0000E55E File Offset: 0x0000C75E
		public override int GetHashCode()
		{
			return this.m_GameID.GetHashCode();
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0000E56B File Offset: 0x0000C76B
		public static bool operator ==(CGameID x, CGameID y)
		{
			return x.m_GameID == y.m_GameID;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0000E57B File Offset: 0x0000C77B
		public static bool operator !=(CGameID x, CGameID y)
		{
			return !(x == y);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0000E587 File Offset: 0x0000C787
		public static explicit operator CGameID(ulong value)
		{
			return new CGameID(value);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0000E58F File Offset: 0x0000C78F
		public static explicit operator ulong(CGameID that)
		{
			return that.m_GameID;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0000E597 File Offset: 0x0000C797
		public bool Equals(CGameID other)
		{
			return this.m_GameID == other.m_GameID;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0000E5A7 File Offset: 0x0000C7A7
		public int CompareTo(CGameID other)
		{
			return this.m_GameID.CompareTo(other.m_GameID);
		}

		// Token: 0x04000A47 RID: 2631
		public ulong m_GameID;

		// Token: 0x020001E5 RID: 485
		public enum EGameIDType
		{
			// Token: 0x04000AD3 RID: 2771
			k_EGameIDTypeApp,
			// Token: 0x04000AD4 RID: 2772
			k_EGameIDTypeGameMod,
			// Token: 0x04000AD5 RID: 2773
			k_EGameIDTypeShortcut,
			// Token: 0x04000AD6 RID: 2774
			k_EGameIDTypeP2P
		}
	}
}
