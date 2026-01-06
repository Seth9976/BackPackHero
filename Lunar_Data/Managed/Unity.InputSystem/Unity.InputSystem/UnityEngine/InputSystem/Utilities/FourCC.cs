using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200012C RID: 300
	public struct FourCC : IEquatable<FourCC>
	{
		// Token: 0x06001087 RID: 4231 RVA: 0x0004EC29 File Offset: 0x0004CE29
		public FourCC(int code)
		{
			this.m_Code = code;
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0004EC32 File Offset: 0x0004CE32
		public FourCC(char a, char b = ' ', char c = ' ', char d = ' ')
		{
			this.m_Code = (int)(((int)a << 24) | ((int)b << 16) | ((int)c << 8) | d);
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0004EC4C File Offset: 0x0004CE4C
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

		// Token: 0x0600108A RID: 4234 RVA: 0x0004ECDC File Offset: 0x0004CEDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int(FourCC fourCC)
		{
			return fourCC.m_Code;
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0004ECE4 File Offset: 0x0004CEE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator FourCC(int i)
		{
			return new FourCC(i);
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x0004ECEC File Offset: 0x0004CEEC
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

		// Token: 0x0600108D RID: 4237 RVA: 0x0004ED5F File Offset: 0x0004CF5F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(FourCC other)
		{
			return this.m_Code == other.m_Code;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0004ED70 File Offset: 0x0004CF70
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

		// Token: 0x0600108F RID: 4239 RVA: 0x0004ED9A File Offset: 0x0004CF9A
		public override int GetHashCode()
		{
			return this.m_Code;
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0004EDA2 File Offset: 0x0004CFA2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(FourCC left, FourCC right)
		{
			return left.m_Code == right.m_Code;
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0004EDB2 File Offset: 0x0004CFB2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(FourCC left, FourCC right)
		{
			return left.m_Code != right.m_Code;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0004EDC5 File Offset: 0x0004CFC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static FourCC FromInt32(int i)
		{
			return i;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0004EDCD File Offset: 0x0004CFCD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ToInt32(FourCC fourCC)
		{
			return fourCC.m_Code;
		}

		// Token: 0x040006B6 RID: 1718
		private int m_Code;
	}
}
