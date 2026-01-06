using System;

namespace Steamworks
{
	// Token: 0x02000196 RID: 406
	[Serializable]
	public struct HTTPRequestHandle : IEquatable<HTTPRequestHandle>, IComparable<HTTPRequestHandle>
	{
		// Token: 0x060009AC RID: 2476 RVA: 0x0000EC4F File Offset: 0x0000CE4F
		public HTTPRequestHandle(uint value)
		{
			this.m_HTTPRequestHandle = value;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0000EC58 File Offset: 0x0000CE58
		public override string ToString()
		{
			return this.m_HTTPRequestHandle.ToString();
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0000EC65 File Offset: 0x0000CE65
		public override bool Equals(object other)
		{
			return other is HTTPRequestHandle && this == (HTTPRequestHandle)other;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0000EC82 File Offset: 0x0000CE82
		public override int GetHashCode()
		{
			return this.m_HTTPRequestHandle.GetHashCode();
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0000EC8F File Offset: 0x0000CE8F
		public static bool operator ==(HTTPRequestHandle x, HTTPRequestHandle y)
		{
			return x.m_HTTPRequestHandle == y.m_HTTPRequestHandle;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0000EC9F File Offset: 0x0000CE9F
		public static bool operator !=(HTTPRequestHandle x, HTTPRequestHandle y)
		{
			return !(x == y);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0000ECAB File Offset: 0x0000CEAB
		public static explicit operator HTTPRequestHandle(uint value)
		{
			return new HTTPRequestHandle(value);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0000ECB3 File Offset: 0x0000CEB3
		public static explicit operator uint(HTTPRequestHandle that)
		{
			return that.m_HTTPRequestHandle;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0000ECBB File Offset: 0x0000CEBB
		public bool Equals(HTTPRequestHandle other)
		{
			return this.m_HTTPRequestHandle == other.m_HTTPRequestHandle;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0000ECCB File Offset: 0x0000CECB
		public int CompareTo(HTTPRequestHandle other)
		{
			return this.m_HTTPRequestHandle.CompareTo(other.m_HTTPRequestHandle);
		}

		// Token: 0x04000A62 RID: 2658
		public static readonly HTTPRequestHandle Invalid = new HTTPRequestHandle(0U);

		// Token: 0x04000A63 RID: 2659
		public uint m_HTTPRequestHandle;
	}
}
