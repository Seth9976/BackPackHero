using System;
using System.Text;

namespace Pathfinding.Util
{
	// Token: 0x02000274 RID: 628
	public struct Guid
	{
		// Token: 0x06000EF2 RID: 3826 RVA: 0x0005C36C File Offset: 0x0005A56C
		public Guid(byte[] bytes)
		{
			ulong num = (ulong)bytes[0] | ((ulong)bytes[1] << 8) | ((ulong)bytes[2] << 16) | ((ulong)bytes[3] << 24) | ((ulong)bytes[4] << 32) | ((ulong)bytes[5] << 40) | ((ulong)bytes[6] << 48) | ((ulong)bytes[7] << 56);
			ulong num2 = (ulong)bytes[8] | ((ulong)bytes[9] << 8) | ((ulong)bytes[10] << 16) | ((ulong)bytes[11] << 24) | ((ulong)bytes[12] << 32) | ((ulong)bytes[13] << 40) | ((ulong)bytes[14] << 48) | ((ulong)bytes[15] << 56);
			this._a = (BitConverter.IsLittleEndian ? num : Guid.SwapEndianness(num));
			this._b = (BitConverter.IsLittleEndian ? num2 : Guid.SwapEndianness(num2));
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0005C424 File Offset: 0x0005A624
		public Guid(string str)
		{
			this._a = 0UL;
			this._b = 0UL;
			if (str.Length < 32)
			{
				throw new FormatException("Invalid Guid format");
			}
			int i = 0;
			int num = 0;
			int num2 = 60;
			while (i < 16)
			{
				if (num >= str.Length)
				{
					throw new FormatException("Invalid Guid format. String too short");
				}
				char c = str[num];
				if (c != '-')
				{
					int num3 = "0123456789ABCDEF".IndexOf(char.ToUpperInvariant(c));
					if (num3 == -1)
					{
						throw new FormatException("Invalid Guid format : " + c.ToString() + " is not a hexadecimal character");
					}
					this._a |= (ulong)((ulong)((long)num3) << num2);
					num2 -= 4;
					i++;
				}
				num++;
			}
			num2 = 60;
			while (i < 32)
			{
				if (num >= str.Length)
				{
					throw new FormatException("Invalid Guid format. String too short");
				}
				char c2 = str[num];
				if (c2 != '-')
				{
					int num4 = "0123456789ABCDEF".IndexOf(char.ToUpperInvariant(c2));
					if (num4 == -1)
					{
						throw new FormatException("Invalid Guid format : " + c2.ToString() + " is not a hexadecimal character");
					}
					this._b |= (ulong)((ulong)((long)num4) << num2);
					num2 -= 4;
					i++;
				}
				num++;
			}
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0005C55B File Offset: 0x0005A75B
		public static Guid Parse(string input)
		{
			return new Guid(input);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0005C564 File Offset: 0x0005A764
		private static ulong SwapEndianness(ulong value)
		{
			ulong num = value & 255UL;
			ulong num2 = (value >> 8) & 255UL;
			ulong num3 = (value >> 16) & 255UL;
			ulong num4 = (value >> 24) & 255UL;
			ulong num5 = (value >> 32) & 255UL;
			ulong num6 = (value >> 40) & 255UL;
			ulong num7 = (value >> 48) & 255UL;
			ulong num8 = (value >> 56) & 255UL;
			return (num << 56) | (num2 << 48) | (num3 << 40) | (num4 << 32) | (num5 << 24) | (num6 << 16) | (num7 << 8) | num8;
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0005C5F4 File Offset: 0x0005A7F4
		public static Guid NewGuid()
		{
			byte[] array = new byte[16];
			Guid.random.NextBytes(array);
			return new Guid(array);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0005C61A File Offset: 0x0005A81A
		public static bool operator ==(Guid lhs, Guid rhs)
		{
			return lhs._a == rhs._a && lhs._b == rhs._b;
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0005C63A File Offset: 0x0005A83A
		public static bool operator !=(Guid lhs, Guid rhs)
		{
			return lhs._a != rhs._a || lhs._b != rhs._b;
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0005C660 File Offset: 0x0005A860
		public override bool Equals(object _rhs)
		{
			if (!(_rhs is Guid))
			{
				return false;
			}
			Guid guid = (Guid)_rhs;
			return this._a == guid._a && this._b == guid._b;
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0005C69C File Offset: 0x0005A89C
		public override int GetHashCode()
		{
			ulong num = this._a ^ this._b;
			return (int)(num >> 32) ^ (int)num;
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0005C6C0 File Offset: 0x0005A8C0
		public override string ToString()
		{
			if (Guid.text == null)
			{
				Guid.text = new StringBuilder();
			}
			StringBuilder stringBuilder = Guid.text;
			string text;
			lock (stringBuilder)
			{
				Guid.text.Length = 0;
				Guid.text.Append(this._a.ToString("x16")).Append('-').Append(this._b.ToString("x16"));
				text = Guid.text.ToString();
			}
			return text;
		}

		// Token: 0x04000B2A RID: 2858
		private const string hex = "0123456789ABCDEF";

		// Token: 0x04000B2B RID: 2859
		public static readonly Guid zero = new Guid(new byte[16]);

		// Token: 0x04000B2C RID: 2860
		public static readonly string zeroString = new Guid(new byte[16]).ToString();

		// Token: 0x04000B2D RID: 2861
		private readonly ulong _a;

		// Token: 0x04000B2E RID: 2862
		private readonly ulong _b;

		// Token: 0x04000B2F RID: 2863
		private static Random random = new Random();

		// Token: 0x04000B30 RID: 2864
		private static StringBuilder text;
	}
}
