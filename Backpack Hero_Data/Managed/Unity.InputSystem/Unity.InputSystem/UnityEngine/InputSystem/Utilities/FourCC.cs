using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200012C RID: 300
	public struct FourCC : IEquatable<FourCC>
	{
		// Token: 0x0600108E RID: 4238 RVA: 0x0004EDD1 File Offset: 0x0004CFD1
		public FourCC(int code)
		{
			this.m_Code = code;
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0004EDDA File Offset: 0x0004CFDA
		public FourCC(char a, char b = ' ', char c = ' ', char d = ' ')
		{
			this.m_Code = (int)(((int)a << 24) | ((int)b << 16) | ((int)c << 8) | d);
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0004EDF4 File Offset: 0x0004CFF4
		public FourCC(string str)
		{
			this = default(FourCC);
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int length = str.Length;
			if (length < 1 || length > 4)
			{
				throw new ArgumentException("FourCC string must be one to four characters long!", "str");
			}
			char c = str[0];
			char c2 = ((length > 1) ? str[1] : ' ');
			char c3 = ((length > 2) ? str[2] : ' ');
			char c4 = ((length > 3) ? str[3] : ' ');
			this.m_Code = (int)(((int)c << 24) | ((int)c2 << 16) | ((int)c3 << 8) | c4);
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0004EE84 File Offset: 0x0004D084
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int(FourCC fourCC)
		{
			return fourCC.m_Code;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0004EE8C File Offset: 0x0004D08C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator FourCC(int i)
		{
			return new FourCC(i);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0004EE94 File Offset: 0x0004D094
		public override string ToString()
		{
			return string.Format("{0}{1}{2}{3}", new object[]
			{
				(char)(this.m_Code >> 24),
				(char)((this.m_Code & 16711680) >> 16),
				(char)((this.m_Code & 65280) >> 8),
				(char)(this.m_Code & 255)
			});
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0004EF07 File Offset: 0x0004D107
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(FourCC other)
		{
			return this.m_Code == other.m_Code;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0004EF18 File Offset: 0x0004D118
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is FourCC)
			{
				FourCC fourCC = (FourCC)obj;
				return this.Equals(fourCC);
			}
			return false;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0004EF42 File Offset: 0x0004D142
		public override int GetHashCode()
		{
			return this.m_Code;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0004EF4A File Offset: 0x0004D14A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(FourCC left, FourCC right)
		{
			return left.m_Code == right.m_Code;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0004EF5A File Offset: 0x0004D15A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(FourCC left, FourCC right)
		{
			return left.m_Code != right.m_Code;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0004EF6D File Offset: 0x0004D16D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static FourCC FromInt32(int i)
		{
			return i;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0004EF75 File Offset: 0x0004D175
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ToInt32(FourCC fourCC)
		{
			return fourCC.m_Code;
		}

		// Token: 0x040006B7 RID: 1719
		private int m_Code;
	}
}
