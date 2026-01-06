using System;

namespace Steamworks
{
	// Token: 0x0200018B RID: 395
	[Serializable]
	public struct servernetadr_t
	{
		// Token: 0x06000924 RID: 2340 RVA: 0x0000E167 File Offset: 0x0000C367
		public void Init(uint ip, ushort usQueryPort, ushort usConnectionPort)
		{
			this.m_unIP = ip;
			this.m_usQueryPort = usQueryPort;
			this.m_usConnectionPort = usConnectionPort;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0000E17E File Offset: 0x0000C37E
		public ushort GetQueryPort()
		{
			return this.m_usQueryPort;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0000E186 File Offset: 0x0000C386
		public void SetQueryPort(ushort usPort)
		{
			this.m_usQueryPort = usPort;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0000E18F File Offset: 0x0000C38F
		public ushort GetConnectionPort()
		{
			return this.m_usConnectionPort;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0000E197 File Offset: 0x0000C397
		public void SetConnectionPort(ushort usPort)
		{
			this.m_usConnectionPort = usPort;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		public uint GetIP()
		{
			return this.m_unIP;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0000E1A8 File Offset: 0x0000C3A8
		public void SetIP(uint unIP)
		{
			this.m_unIP = unIP;
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0000E1B1 File Offset: 0x0000C3B1
		public string GetConnectionAddressString()
		{
			return servernetadr_t.ToString(this.m_unIP, this.m_usConnectionPort);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0000E1C4 File Offset: 0x0000C3C4
		public string GetQueryAddressString()
		{
			return servernetadr_t.ToString(this.m_unIP, this.m_usQueryPort);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0000E1D8 File Offset: 0x0000C3D8
		public static string ToString(uint unIP, ushort usPort)
		{
			return string.Format("{0}.{1}.{2}.{3}:{4}", new object[]
			{
				(ulong)(unIP >> 24) & 255UL,
				(ulong)(unIP >> 16) & 255UL,
				(ulong)(unIP >> 8) & 255UL,
				(ulong)unIP & 255UL,
				usPort
			});
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0000E24A File Offset: 0x0000C44A
		public static bool operator <(servernetadr_t x, servernetadr_t y)
		{
			return x.m_unIP < y.m_unIP || (x.m_unIP == y.m_unIP && x.m_usQueryPort < y.m_usQueryPort);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0000E27A File Offset: 0x0000C47A
		public static bool operator >(servernetadr_t x, servernetadr_t y)
		{
			return x.m_unIP > y.m_unIP || (x.m_unIP == y.m_unIP && x.m_usQueryPort > y.m_usQueryPort);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0000E2AA File Offset: 0x0000C4AA
		public override bool Equals(object other)
		{
			return other is servernetadr_t && this == (servernetadr_t)other;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0000E2C7 File Offset: 0x0000C4C7
		public override int GetHashCode()
		{
			return this.m_unIP.GetHashCode() + this.m_usQueryPort.GetHashCode() + this.m_usConnectionPort.GetHashCode();
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0000E2EC File Offset: 0x0000C4EC
		public static bool operator ==(servernetadr_t x, servernetadr_t y)
		{
			return x.m_unIP == y.m_unIP && x.m_usQueryPort == y.m_usQueryPort && x.m_usConnectionPort == y.m_usConnectionPort;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0000E31A File Offset: 0x0000C51A
		public static bool operator !=(servernetadr_t x, servernetadr_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0000E326 File Offset: 0x0000C526
		public bool Equals(servernetadr_t other)
		{
			return this.m_unIP == other.m_unIP && this.m_usQueryPort == other.m_usQueryPort && this.m_usConnectionPort == other.m_usConnectionPort;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0000E354 File Offset: 0x0000C554
		public int CompareTo(servernetadr_t other)
		{
			return this.m_unIP.CompareTo(other.m_unIP) + this.m_usQueryPort.CompareTo(other.m_usQueryPort) + this.m_usConnectionPort.CompareTo(other.m_usConnectionPort);
		}

		// Token: 0x04000A44 RID: 2628
		private ushort m_usConnectionPort;

		// Token: 0x04000A45 RID: 2629
		private ushort m_usQueryPort;

		// Token: 0x04000A46 RID: 2630
		private uint m_unIP;
	}
}
