using System;
using System.Text;

namespace Pathfinding.Util
{
	// Token: 0x020000CC RID: 204
	public struct Guid
	{
		// Token: 0x060008A5 RID: 2213 RVA: 0x0003A87C File Offset: 0x00038A7C
		public Guid(byte[] bytes)
		{
			ulong num = (ulong)bytes[0] | ((ulong)bytes[1] << 8) | ((ulong)bytes[2] << 16) | ((ulong)bytes[3] << 24) | ((ulong)bytes[4] << 32) | ((ulong)bytes[5] << 40) | ((ulong)bytes[6] << 48) | ((ulong)bytes[7] << 56);
			ulong num2 = (ulong)bytes[8] | ((ulong)bytes[9] << 8) | ((ulong)bytes[10] << 16) | ((ulong)bytes[11] << 24) | ((ulong)bytes[12] << 32) | ((ulong)bytes[13] << 40) | ((ulong)bytes[14] << 48) | ((ulong)bytes[15] << 56);
			this._a = (BitConverter.IsLittleEndian ? num : Guid.SwapEndianness(num));
			this._b = (BitConverter.IsLittleEndian ? num2 : Guid.SwapEndianness(num2));
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0003A934 File Offset: 0x00038B34
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

		// Token: 0x060008A7 RID: 2215 RVA: 0x0003AA6B File Offset: 0x00038C6B
		public static Guid Parse(string input)
		{
			return new Guid(input);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0003AA74 File Offset: 0x00038C74
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

		// Token: 0x060008A9 RID: 2217 RVA: 0x0003AB04 File Offset: 0x00038D04
		public byte[] ToByteArray()
		{
			byte[] array = new byte[16];
			byte[] bytes = BitConverter.GetBytes((!BitConverter.IsLittleEndian) ? Guid.SwapEndianness(this._a) : this._a);
			byte[] bytes2 = BitConverter.GetBytes((!BitConverter.IsLittleEndian) ? Guid.SwapEndianness(this._b) : this._b);
			for (int i = 0; i < 8; i++)
			{
				array[i] = bytes[i];
				array[i + 8] = bytes2[i];
			}
			return array;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0003AB74 File Offset: 0x00038D74
		public static Guid NewGuid()
		{
			byte[] array = new byte[16];
			Guid.random.NextBytes(array);
			return new Guid(array);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0003AB9A File Offset: 0x00038D9A
		public static bool operator ==(Guid lhs, Guid rhs)
		{
			return lhs._a == rhs._a && lhs._b == rhs._b;
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0003ABBA File Offset: 0x00038DBA
		public static bool operator !=(Guid lhs, Guid rhs)
		{
			return lhs._a != rhs._a || lhs._b != rhs._b;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0003ABE0 File Offset: 0x00038DE0
		public override bool Equals(object _rhs)
		{
			if (!(_rhs is Guid))
			{
				return false;
			}
			Guid guid = (Guid)_rhs;
			return this._a == guid._a && this._b == guid._b;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0003AC1C File Offset: 0x00038E1C
		public override int GetHashCode()
		{
			ulong num = this._a ^ this._b;
			return (int)(num >> 32) ^ (int)num;
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0003AC40 File Offset: 0x00038E40
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

		// Token: 0x04000501 RID: 1281
		private const string hex = "0123456789ABCDEF";

		// Token: 0x04000502 RID: 1282
		public static readonly Guid zero = new Guid(new byte[16]);

		// Token: 0x04000503 RID: 1283
		public static readonly string zeroString = new Guid(new byte[16]).ToString();

		// Token: 0x04000504 RID: 1284
		private readonly ulong _a;

		// Token: 0x04000505 RID: 1285
		private readonly ulong _b;

		// Token: 0x04000506 RID: 1286
		private static Random random = new Random();

		// Token: 0x04000507 RID: 1287
		private static StringBuilder text;
	}
}
