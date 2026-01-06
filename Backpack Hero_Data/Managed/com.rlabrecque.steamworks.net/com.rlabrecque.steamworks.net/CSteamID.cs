using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200018F RID: 399
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct CSteamID : IEquatable<CSteamID>, IComparable<CSteamID>
	{
		// Token: 0x06000957 RID: 2391 RVA: 0x0000E5BA File Offset: 0x0000C7BA
		public CSteamID(AccountID_t unAccountID, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.m_SteamID = 0UL;
			this.Set(unAccountID, eUniverse, eAccountType);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0000E5CD File Offset: 0x0000C7CD
		public CSteamID(AccountID_t unAccountID, uint unAccountInstance, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.m_SteamID = 0UL;
			this.InstancedSet(unAccountID, unAccountInstance, eUniverse, eAccountType);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0000E5E2 File Offset: 0x0000C7E2
		public CSteamID(ulong ulSteamID)
		{
			this.m_SteamID = ulSteamID;
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0000E5EB File Offset: 0x0000C7EB
		public void Set(AccountID_t unAccountID, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.SetAccountID(unAccountID);
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(eAccountType);
			if (eAccountType == EAccountType.k_EAccountTypeClan || eAccountType == EAccountType.k_EAccountTypeGameServer)
			{
				this.SetAccountInstance(0U);
				return;
			}
			this.SetAccountInstance(1U);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0000E619 File Offset: 0x0000C819
		public void InstancedSet(AccountID_t unAccountID, uint unInstance, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.SetAccountID(unAccountID);
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(eAccountType);
			this.SetAccountInstance(unInstance);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0000E638 File Offset: 0x0000C838
		public void Clear()
		{
			this.m_SteamID = 0UL;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0000E642 File Offset: 0x0000C842
		public void CreateBlankAnonLogon(EUniverse eUniverse)
		{
			this.SetAccountID(new AccountID_t(0U));
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(EAccountType.k_EAccountTypeAnonGameServer);
			this.SetAccountInstance(0U);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0000E665 File Offset: 0x0000C865
		public void CreateBlankAnonUserLogon(EUniverse eUniverse)
		{
			this.SetAccountID(new AccountID_t(0U));
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(EAccountType.k_EAccountTypeAnonUser);
			this.SetAccountInstance(0U);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0000E689 File Offset: 0x0000C889
		public bool BBlankAnonAccount()
		{
			return this.GetAccountID() == new AccountID_t(0U) && this.BAnonAccount() && this.GetUnAccountInstance() == 0U;
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0000E6B1 File Offset: 0x0000C8B1
		public bool BGameServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeGameServer || this.GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0000E6C7 File Offset: 0x0000C8C7
		public bool BPersistentGameServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeGameServer;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0000E6D2 File Offset: 0x0000C8D2
		public bool BAnonGameServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0000E6DD File Offset: 0x0000C8DD
		public bool BContentServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeContentServer;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0000E6E8 File Offset: 0x0000C8E8
		public bool BClanAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeClan;
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0000E6F3 File Offset: 0x0000C8F3
		public bool BChatAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeChat;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0000E6FE File Offset: 0x0000C8FE
		public bool IsLobby()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeChat && (this.GetUnAccountInstance() & 262144U) > 0U;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0000E71A File Offset: 0x0000C91A
		public bool BIndividualAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeIndividual || this.GetEAccountType() == EAccountType.k_EAccountTypeConsoleUser;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0000E731 File Offset: 0x0000C931
		public bool BAnonAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeAnonUser || this.GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0000E748 File Offset: 0x0000C948
		public bool BAnonUserAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeAnonUser;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0000E754 File Offset: 0x0000C954
		public bool BConsoleUserAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeConsoleUser;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0000E760 File Offset: 0x0000C960
		public void SetAccountID(AccountID_t other)
		{
			this.m_SteamID = (this.m_SteamID & 18446744069414584320UL) | ((ulong)(uint)other & (ulong)(-1));
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0000E783 File Offset: 0x0000C983
		public void SetAccountInstance(uint other)
		{
			this.m_SteamID = (this.m_SteamID & 18442240478377148415UL) | (((ulong)other & 1048575UL) << 32);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0000E7A8 File Offset: 0x0000C9A8
		public void SetEAccountType(EAccountType other)
		{
			this.m_SteamID = (this.m_SteamID & 18379190079298994175UL) | (ulong)((ulong)((long)other & 15L) << 52);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0000E7CA File Offset: 0x0000C9CA
		public void SetEUniverse(EUniverse other)
		{
			this.m_SteamID = (this.m_SteamID & 72057594037927935UL) | (ulong)((ulong)((long)other & 255L) << 56);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0000E7EF File Offset: 0x0000C9EF
		public AccountID_t GetAccountID()
		{
			return new AccountID_t((uint)(this.m_SteamID & (ulong)(-1)));
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0000E800 File Offset: 0x0000CA00
		public uint GetUnAccountInstance()
		{
			return (uint)((this.m_SteamID >> 32) & 1048575UL);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0000E813 File Offset: 0x0000CA13
		public EAccountType GetEAccountType()
		{
			return (EAccountType)((this.m_SteamID >> 52) & 15UL);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0000E823 File Offset: 0x0000CA23
		public EUniverse GetEUniverse()
		{
			return (EUniverse)((this.m_SteamID >> 56) & 255UL);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0000E838 File Offset: 0x0000CA38
		public bool IsValid()
		{
			return this.GetEAccountType() > EAccountType.k_EAccountTypeInvalid && this.GetEAccountType() < EAccountType.k_EAccountTypeMax && this.GetEUniverse() > EUniverse.k_EUniverseInvalid && this.GetEUniverse() < EUniverse.k_EUniverseMax && (this.GetEAccountType() != EAccountType.k_EAccountTypeIndividual || (!(this.GetAccountID() == new AccountID_t(0U)) && this.GetUnAccountInstance() <= 1U)) && (this.GetEAccountType() != EAccountType.k_EAccountTypeClan || (!(this.GetAccountID() == new AccountID_t(0U)) && this.GetUnAccountInstance() == 0U)) && (this.GetEAccountType() != EAccountType.k_EAccountTypeGameServer || !(this.GetAccountID() == new AccountID_t(0U)));
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0000E8DA File Offset: 0x0000CADA
		public override string ToString()
		{
			return this.m_SteamID.ToString();
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0000E8E7 File Offset: 0x0000CAE7
		public override bool Equals(object other)
		{
			return other is CSteamID && this == (CSteamID)other;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0000E904 File Offset: 0x0000CB04
		public override int GetHashCode()
		{
			return this.m_SteamID.GetHashCode();
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0000E911 File Offset: 0x0000CB11
		public static bool operator ==(CSteamID x, CSteamID y)
		{
			return x.m_SteamID == y.m_SteamID;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0000E921 File Offset: 0x0000CB21
		public static bool operator !=(CSteamID x, CSteamID y)
		{
			return !(x == y);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0000E92D File Offset: 0x0000CB2D
		public static explicit operator CSteamID(ulong value)
		{
			return new CSteamID(value);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0000E935 File Offset: 0x0000CB35
		public static explicit operator ulong(CSteamID that)
		{
			return that.m_SteamID;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0000E93D File Offset: 0x0000CB3D
		public bool Equals(CSteamID other)
		{
			return this.m_SteamID == other.m_SteamID;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0000E94D File Offset: 0x0000CB4D
		public int CompareTo(CSteamID other)
		{
			return this.m_SteamID.CompareTo(other.m_SteamID);
		}

		// Token: 0x04000A48 RID: 2632
		public static readonly CSteamID Nil = default(CSteamID);

		// Token: 0x04000A49 RID: 2633
		public static readonly CSteamID OutofDateGS = new CSteamID(new AccountID_t(0U), 0U, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000A4A RID: 2634
		public static readonly CSteamID LanModeGS = new CSteamID(new AccountID_t(0U), 0U, EUniverse.k_EUniversePublic, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000A4B RID: 2635
		public static readonly CSteamID NotInitYetGS = new CSteamID(new AccountID_t(1U), 0U, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000A4C RID: 2636
		public static readonly CSteamID NonSteamGS = new CSteamID(new AccountID_t(2U), 0U, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000A4D RID: 2637
		public ulong m_SteamID;
	}
}
