using System;

namespace Steamworks
{
	// Token: 0x020001A1 RID: 417
	[Serializable]
	public struct HServerListRequest : IEquatable<HServerListRequest>
	{
		// Token: 0x06000A0E RID: 2574 RVA: 0x0000F18C File Offset: 0x0000D38C
		public HServerListRequest(IntPtr value)
		{
			this.m_HServerListRequest = value;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0000F195 File Offset: 0x0000D395
		public override string ToString()
		{
			return this.m_HServerListRequest.ToString();
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0000F1A2 File Offset: 0x0000D3A2
		public override bool Equals(object other)
		{
			return other is HServerListRequest && this == (HServerListRequest)other;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0000F1BF File Offset: 0x0000D3BF
		public override int GetHashCode()
		{
			return this.m_HServerListRequest.GetHashCode();
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0000F1CC File Offset: 0x0000D3CC
		public static bool operator ==(HServerListRequest x, HServerListRequest y)
		{
			return x.m_HServerListRequest == y.m_HServerListRequest;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0000F1DF File Offset: 0x0000D3DF
		public static bool operator !=(HServerListRequest x, HServerListRequest y)
		{
			return !(x == y);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0000F1EB File Offset: 0x0000D3EB
		public static explicit operator HServerListRequest(IntPtr value)
		{
			return new HServerListRequest(value);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0000F1F3 File Offset: 0x0000D3F3
		public static explicit operator IntPtr(HServerListRequest that)
		{
			return that.m_HServerListRequest;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0000F1FB File Offset: 0x0000D3FB
		public bool Equals(HServerListRequest other)
		{
			return this.m_HServerListRequest == other.m_HServerListRequest;
		}

		// Token: 0x04000A72 RID: 2674
		public static readonly HServerListRequest Invalid = new HServerListRequest(IntPtr.Zero);

		// Token: 0x04000A73 RID: 2675
		public IntPtr m_HServerListRequest;
	}
}
