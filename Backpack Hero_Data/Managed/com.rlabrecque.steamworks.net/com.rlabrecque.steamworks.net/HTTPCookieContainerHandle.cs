using System;

namespace Steamworks
{
	// Token: 0x02000195 RID: 405
	[Serializable]
	public struct HTTPCookieContainerHandle : IEquatable<HTTPCookieContainerHandle>, IComparable<HTTPCookieContainerHandle>
	{
		// Token: 0x060009A1 RID: 2465 RVA: 0x0000EBB3 File Offset: 0x0000CDB3
		public HTTPCookieContainerHandle(uint value)
		{
			this.m_HTTPCookieContainerHandle = value;
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0000EBBC File Offset: 0x0000CDBC
		public override string ToString()
		{
			return this.m_HTTPCookieContainerHandle.ToString();
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0000EBC9 File Offset: 0x0000CDC9
		public override bool Equals(object other)
		{
			return other is HTTPCookieContainerHandle && this == (HTTPCookieContainerHandle)other;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0000EBE6 File Offset: 0x0000CDE6
		public override int GetHashCode()
		{
			return this.m_HTTPCookieContainerHandle.GetHashCode();
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0000EBF3 File Offset: 0x0000CDF3
		public static bool operator ==(HTTPCookieContainerHandle x, HTTPCookieContainerHandle y)
		{
			return x.m_HTTPCookieContainerHandle == y.m_HTTPCookieContainerHandle;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0000EC03 File Offset: 0x0000CE03
		public static bool operator !=(HTTPCookieContainerHandle x, HTTPCookieContainerHandle y)
		{
			return !(x == y);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0000EC0F File Offset: 0x0000CE0F
		public static explicit operator HTTPCookieContainerHandle(uint value)
		{
			return new HTTPCookieContainerHandle(value);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0000EC17 File Offset: 0x0000CE17
		public static explicit operator uint(HTTPCookieContainerHandle that)
		{
			return that.m_HTTPCookieContainerHandle;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0000EC1F File Offset: 0x0000CE1F
		public bool Equals(HTTPCookieContainerHandle other)
		{
			return this.m_HTTPCookieContainerHandle == other.m_HTTPCookieContainerHandle;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0000EC2F File Offset: 0x0000CE2F
		public int CompareTo(HTTPCookieContainerHandle other)
		{
			return this.m_HTTPCookieContainerHandle.CompareTo(other.m_HTTPCookieContainerHandle);
		}

		// Token: 0x04000A60 RID: 2656
		public static readonly HTTPCookieContainerHandle Invalid = new HTTPCookieContainerHandle(0U);

		// Token: 0x04000A61 RID: 2657
		public uint m_HTTPCookieContainerHandle;
	}
}
