using System;
using System.Text;

namespace Mono.Net.Dns
{
	// Token: 0x020000B5 RID: 181
	internal class DnsHeader
	{
		// Token: 0x0600037C RID: 892 RVA: 0x0000A856 File Offset: 0x00008A56
		public DnsHeader(byte[] bytes)
			: this(bytes, 0)
		{
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000A860 File Offset: 0x00008A60
		public DnsHeader(byte[] bytes, int offset)
			: this(new ArraySegment<byte>(bytes, offset, 12))
		{
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000A871 File Offset: 0x00008A71
		public DnsHeader(ArraySegment<byte> segment)
		{
			if (segment.Count != 12)
			{
				throw new ArgumentException("Count must be 12", "segment");
			}
			this.bytes = segment;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000A89C File Offset: 0x00008A9C
		public void Clear()
		{
			for (int i = 0; i < 12; i++)
			{
				this.bytes.Array[i + this.bytes.Offset] = 0;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000A8D0 File Offset: 0x00008AD0
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0000A90C File Offset: 0x00008B0C
		public ushort ID
		{
			get
			{
				return (ushort)((int)this.bytes.Array[this.bytes.Offset] * 256 + (int)this.bytes.Array[this.bytes.Offset + 1]);
			}
			set
			{
				this.bytes.Array[this.bytes.Offset] = (byte)((value & 65280) >> 8);
				this.bytes.Array[this.bytes.Offset + 1] = (byte)(value & 255);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000A95B File Offset: 0x00008B5B
		// (set) Token: 0x06000383 RID: 899 RVA: 0x0000A980 File Offset: 0x00008B80
		public bool IsQuery
		{
			get
			{
				return (this.bytes.Array[2 + this.bytes.Offset] & 128) > 0;
			}
			set
			{
				if (!value)
				{
					byte[] array = this.bytes.Array;
					int num = 2 + this.bytes.Offset;
					array[num] |= 128;
					return;
				}
				byte[] array2 = this.bytes.Array;
				int num2 = 2 + this.bytes.Offset;
				array2[num2] &= 127;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000A9DC File Offset: 0x00008BDC
		// (set) Token: 0x06000385 RID: 901 RVA: 0x0000AA00 File Offset: 0x00008C00
		public DnsOpCode OpCode
		{
			get
			{
				return (DnsOpCode)((this.bytes.Array[2 + this.bytes.Offset] & 120) >> 3);
			}
			set
			{
				if (!Enum.IsDefined(typeof(DnsOpCode), value))
				{
					throw new ArgumentOutOfRangeException("value", "Invalid DnsOpCode value");
				}
				int num = (int)((int)value << 3);
				int num2 = (int)(this.bytes.Array[2 + this.bytes.Offset] & 135);
				num |= num2;
				this.bytes.Array[2 + this.bytes.Offset] = (byte)num;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000AA79 File Offset: 0x00008C79
		// (set) Token: 0x06000387 RID: 903 RVA: 0x0000AA9C File Offset: 0x00008C9C
		public bool AuthoritativeAnswer
		{
			get
			{
				return (this.bytes.Array[2 + this.bytes.Offset] & 4) > 0;
			}
			set
			{
				if (value)
				{
					byte[] array = this.bytes.Array;
					int num = 2 + this.bytes.Offset;
					array[num] |= 4;
					return;
				}
				byte[] array2 = this.bytes.Array;
				int num2 = 2 + this.bytes.Offset;
				array2[num2] &= 251;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000AAF7 File Offset: 0x00008CF7
		// (set) Token: 0x06000389 RID: 905 RVA: 0x0000AB18 File Offset: 0x00008D18
		public bool Truncation
		{
			get
			{
				return (this.bytes.Array[2 + this.bytes.Offset] & 2) > 0;
			}
			set
			{
				if (value)
				{
					byte[] array = this.bytes.Array;
					int num = 2 + this.bytes.Offset;
					array[num] |= 2;
					return;
				}
				byte[] array2 = this.bytes.Array;
				int num2 = 2 + this.bytes.Offset;
				array2[num2] &= 253;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000AB73 File Offset: 0x00008D73
		// (set) Token: 0x0600038B RID: 907 RVA: 0x0000AB94 File Offset: 0x00008D94
		public bool RecursionDesired
		{
			get
			{
				return (this.bytes.Array[2 + this.bytes.Offset] & 1) > 0;
			}
			set
			{
				if (value)
				{
					byte[] array = this.bytes.Array;
					int num = 2 + this.bytes.Offset;
					array[num] |= 1;
					return;
				}
				byte[] array2 = this.bytes.Array;
				int num2 = 2 + this.bytes.Offset;
				array2[num2] &= 254;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000ABEF File Offset: 0x00008DEF
		// (set) Token: 0x0600038D RID: 909 RVA: 0x0000AC14 File Offset: 0x00008E14
		public bool RecursionAvailable
		{
			get
			{
				return (this.bytes.Array[3 + this.bytes.Offset] & 128) > 0;
			}
			set
			{
				if (value)
				{
					byte[] array = this.bytes.Array;
					int num = 3 + this.bytes.Offset;
					array[num] |= 128;
					return;
				}
				byte[] array2 = this.bytes.Array;
				int num2 = 3 + this.bytes.Offset;
				array2[num2] &= 127;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000AC70 File Offset: 0x00008E70
		// (set) Token: 0x0600038F RID: 911 RVA: 0x0000AC90 File Offset: 0x00008E90
		public int ZReserved
		{
			get
			{
				return (this.bytes.Array[3 + this.bytes.Offset] & 112) >> 4;
			}
			set
			{
				if (value < 0 || value > 7)
				{
					throw new ArgumentOutOfRangeException("value", "Must be between 0 and 7");
				}
				byte[] array = this.bytes.Array;
				int num = 3 + this.bytes.Offset;
				array[num] &= 143;
				byte[] array2 = this.bytes.Array;
				int num2 = 3 + this.bytes.Offset;
				array2[num2] |= (byte)((value << 4) & 112);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000AD05 File Offset: 0x00008F05
		// (set) Token: 0x06000391 RID: 913 RVA: 0x0000AD24 File Offset: 0x00008F24
		public DnsRCode RCode
		{
			get
			{
				return (DnsRCode)(this.bytes.Array[3 + this.bytes.Offset] & 15);
			}
			set
			{
				if (value < DnsRCode.NoError || value > (DnsRCode)15)
				{
					throw new ArgumentOutOfRangeException("value", "Must be between 0 and 15");
				}
				byte[] array = this.bytes.Array;
				int num = 3 + this.bytes.Offset;
				array[num] &= 15;
				byte[] array2 = this.bytes.Array;
				int num2 = 3 + this.bytes.Offset;
				array2[num2] |= (byte)value;
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000AD94 File Offset: 0x00008F94
		private static ushort GetUInt16(byte[] bytes, int offset)
		{
			return (ushort)((int)bytes[offset] * 256 + (int)bytes[offset + 1]);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000ADA6 File Offset: 0x00008FA6
		private static void SetUInt16(byte[] bytes, int offset, ushort val)
		{
			bytes[offset] = (byte)((val & 65280) >> 8);
			bytes[offset + 1] = (byte)(val & 255);
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000ADC2 File Offset: 0x00008FC2
		// (set) Token: 0x06000395 RID: 917 RVA: 0x0000ADD5 File Offset: 0x00008FD5
		public ushort QuestionCount
		{
			get
			{
				return DnsHeader.GetUInt16(this.bytes.Array, 4);
			}
			set
			{
				DnsHeader.SetUInt16(this.bytes.Array, 4, value);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000ADE9 File Offset: 0x00008FE9
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0000ADFC File Offset: 0x00008FFC
		public ushort AnswerCount
		{
			get
			{
				return DnsHeader.GetUInt16(this.bytes.Array, 6);
			}
			set
			{
				DnsHeader.SetUInt16(this.bytes.Array, 6, value);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000AE10 File Offset: 0x00009010
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0000AE23 File Offset: 0x00009023
		public ushort AuthorityCount
		{
			get
			{
				return DnsHeader.GetUInt16(this.bytes.Array, 8);
			}
			set
			{
				DnsHeader.SetUInt16(this.bytes.Array, 8, value);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000AE37 File Offset: 0x00009037
		// (set) Token: 0x0600039B RID: 923 RVA: 0x0000AE4B File Offset: 0x0000904B
		public ushort AdditionalCount
		{
			get
			{
				return DnsHeader.GetUInt16(this.bytes.Array, 10);
			}
			set
			{
				DnsHeader.SetUInt16(this.bytes.Array, 10, value);
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000AE60 File Offset: 0x00009060
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("ID: {0} QR: {1} Opcode: {2} AA: {3} TC: {4} RD: {5} RA: {6} \r\nRCode: {7} ", new object[] { this.ID, this.IsQuery, this.OpCode, this.AuthoritativeAnswer, this.Truncation, this.RecursionDesired, this.RecursionAvailable, this.RCode });
			stringBuilder.AppendFormat("Q: {0} A: {1} NS: {2} AR: {3}\r\n", new object[] { this.QuestionCount, this.AnswerCount, this.AuthorityCount, this.AdditionalCount });
			return stringBuilder.ToString();
		}

		// Token: 0x040002A4 RID: 676
		public const int DnsHeaderLength = 12;

		// Token: 0x040002A5 RID: 677
		private ArraySegment<byte> bytes;
	}
}
