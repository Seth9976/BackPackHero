using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Unity.Burst.Intrinsics
{
	// Token: 0x0200001F RID: 31
	[DebuggerTypeProxy(typeof(V128DebugView))]
	[StructLayout(LayoutKind.Explicit)]
	public struct v128
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00006578 File Offset: 0x00004778
		public v128(byte b)
		{
			this = default(v128);
			this.Byte15 = b;
			this.Byte14 = b;
			this.Byte13 = b;
			this.Byte12 = b;
			this.Byte11 = b;
			this.Byte10 = b;
			this.Byte9 = b;
			this.Byte8 = b;
			this.Byte7 = b;
			this.Byte6 = b;
			this.Byte5 = b;
			this.Byte4 = b;
			this.Byte3 = b;
			this.Byte2 = b;
			this.Byte1 = b;
			this.Byte0 = b;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000661C File Offset: 0x0000481C
		public v128(byte a, byte b, byte c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k, byte l, byte m, byte n, byte o, byte p)
		{
			this = default(v128);
			this.Byte0 = a;
			this.Byte1 = b;
			this.Byte2 = c;
			this.Byte3 = d;
			this.Byte4 = e;
			this.Byte5 = f;
			this.Byte6 = g;
			this.Byte7 = h;
			this.Byte8 = i;
			this.Byte9 = j;
			this.Byte10 = k;
			this.Byte11 = l;
			this.Byte12 = m;
			this.Byte13 = n;
			this.Byte14 = o;
			this.Byte15 = p;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000066B0 File Offset: 0x000048B0
		public v128(sbyte b)
		{
			this = default(v128);
			this.SByte15 = b;
			this.SByte14 = b;
			this.SByte13 = b;
			this.SByte12 = b;
			this.SByte11 = b;
			this.SByte10 = b;
			this.SByte9 = b;
			this.SByte8 = b;
			this.SByte7 = b;
			this.SByte6 = b;
			this.SByte5 = b;
			this.SByte4 = b;
			this.SByte3 = b;
			this.SByte2 = b;
			this.SByte1 = b;
			this.SByte0 = b;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006754 File Offset: 0x00004954
		public v128(sbyte a, sbyte b, sbyte c, sbyte d, sbyte e, sbyte f, sbyte g, sbyte h, sbyte i, sbyte j, sbyte k, sbyte l, sbyte m, sbyte n, sbyte o, sbyte p)
		{
			this = default(v128);
			this.SByte0 = a;
			this.SByte1 = b;
			this.SByte2 = c;
			this.SByte3 = d;
			this.SByte4 = e;
			this.SByte5 = f;
			this.SByte6 = g;
			this.SByte7 = h;
			this.SByte8 = i;
			this.SByte9 = j;
			this.SByte10 = k;
			this.SByte11 = l;
			this.SByte12 = m;
			this.SByte13 = n;
			this.SByte14 = o;
			this.SByte15 = p;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000067E8 File Offset: 0x000049E8
		public v128(short v)
		{
			this = default(v128);
			this.SShort7 = v;
			this.SShort6 = v;
			this.SShort5 = v;
			this.SShort4 = v;
			this.SShort3 = v;
			this.SShort2 = v;
			this.SShort1 = v;
			this.SShort0 = v;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00006844 File Offset: 0x00004A44
		public v128(short a, short b, short c, short d, short e, short f, short g, short h)
		{
			this = default(v128);
			this.SShort0 = a;
			this.SShort1 = b;
			this.SShort2 = c;
			this.SShort3 = d;
			this.SShort4 = e;
			this.SShort5 = f;
			this.SShort6 = g;
			this.SShort7 = h;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006898 File Offset: 0x00004A98
		public v128(ushort v)
		{
			this = default(v128);
			this.UShort7 = v;
			this.UShort6 = v;
			this.UShort5 = v;
			this.UShort4 = v;
			this.UShort3 = v;
			this.UShort2 = v;
			this.UShort1 = v;
			this.UShort0 = v;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000068F4 File Offset: 0x00004AF4
		public v128(ushort a, ushort b, ushort c, ushort d, ushort e, ushort f, ushort g, ushort h)
		{
			this = default(v128);
			this.UShort0 = a;
			this.UShort1 = b;
			this.UShort2 = c;
			this.UShort3 = d;
			this.UShort4 = e;
			this.UShort5 = f;
			this.UShort6 = g;
			this.UShort7 = h;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006948 File Offset: 0x00004B48
		public v128(int v)
		{
			this = default(v128);
			this.SInt3 = v;
			this.SInt2 = v;
			this.SInt1 = v;
			this.SInt0 = v;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000697E File Offset: 0x00004B7E
		public v128(int a, int b, int c, int d)
		{
			this = default(v128);
			this.SInt0 = a;
			this.SInt1 = b;
			this.SInt2 = c;
			this.SInt3 = d;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000069A4 File Offset: 0x00004BA4
		public v128(uint v)
		{
			this = default(v128);
			this.UInt3 = v;
			this.UInt2 = v;
			this.UInt1 = v;
			this.UInt0 = v;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000069DA File Offset: 0x00004BDA
		public v128(uint a, uint b, uint c, uint d)
		{
			this = default(v128);
			this.UInt0 = a;
			this.UInt1 = b;
			this.UInt2 = c;
			this.UInt3 = d;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006A00 File Offset: 0x00004C00
		public v128(float f)
		{
			this = default(v128);
			this.Float3 = f;
			this.Float2 = f;
			this.Float1 = f;
			this.Float0 = f;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00006A36 File Offset: 0x00004C36
		public v128(float a, float b, float c, float d)
		{
			this = default(v128);
			this.Float0 = a;
			this.Float1 = b;
			this.Float2 = c;
			this.Float3 = d;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006A5C File Offset: 0x00004C5C
		public v128(double f)
		{
			this = default(v128);
			this.Double1 = f;
			this.Double0 = f;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006A80 File Offset: 0x00004C80
		public v128(double a, double b)
		{
			this = default(v128);
			this.Double0 = a;
			this.Double1 = b;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006A98 File Offset: 0x00004C98
		public v128(long f)
		{
			this = default(v128);
			this.SLong1 = f;
			this.SLong0 = f;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006ABC File Offset: 0x00004CBC
		public v128(long a, long b)
		{
			this = default(v128);
			this.SLong0 = a;
			this.SLong1 = b;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006AD4 File Offset: 0x00004CD4
		public v128(ulong f)
		{
			this = default(v128);
			this.ULong1 = f;
			this.ULong0 = f;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00006AF8 File Offset: 0x00004CF8
		public v128(ulong a, ulong b)
		{
			this = default(v128);
			this.ULong0 = a;
			this.ULong1 = b;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00006B0F File Offset: 0x00004D0F
		public v128(v64 lo, v64 hi)
		{
			this = default(v128);
			this.Lo64 = lo;
			this.Hi64 = hi;
		}

		// Token: 0x04000145 RID: 325
		[FieldOffset(0)]
		public byte Byte0;

		// Token: 0x04000146 RID: 326
		[FieldOffset(1)]
		public byte Byte1;

		// Token: 0x04000147 RID: 327
		[FieldOffset(2)]
		public byte Byte2;

		// Token: 0x04000148 RID: 328
		[FieldOffset(3)]
		public byte Byte3;

		// Token: 0x04000149 RID: 329
		[FieldOffset(4)]
		public byte Byte4;

		// Token: 0x0400014A RID: 330
		[FieldOffset(5)]
		public byte Byte5;

		// Token: 0x0400014B RID: 331
		[FieldOffset(6)]
		public byte Byte6;

		// Token: 0x0400014C RID: 332
		[FieldOffset(7)]
		public byte Byte7;

		// Token: 0x0400014D RID: 333
		[FieldOffset(8)]
		public byte Byte8;

		// Token: 0x0400014E RID: 334
		[FieldOffset(9)]
		public byte Byte9;

		// Token: 0x0400014F RID: 335
		[FieldOffset(10)]
		public byte Byte10;

		// Token: 0x04000150 RID: 336
		[FieldOffset(11)]
		public byte Byte11;

		// Token: 0x04000151 RID: 337
		[FieldOffset(12)]
		public byte Byte12;

		// Token: 0x04000152 RID: 338
		[FieldOffset(13)]
		public byte Byte13;

		// Token: 0x04000153 RID: 339
		[FieldOffset(14)]
		public byte Byte14;

		// Token: 0x04000154 RID: 340
		[FieldOffset(15)]
		public byte Byte15;

		// Token: 0x04000155 RID: 341
		[FieldOffset(0)]
		public sbyte SByte0;

		// Token: 0x04000156 RID: 342
		[FieldOffset(1)]
		public sbyte SByte1;

		// Token: 0x04000157 RID: 343
		[FieldOffset(2)]
		public sbyte SByte2;

		// Token: 0x04000158 RID: 344
		[FieldOffset(3)]
		public sbyte SByte3;

		// Token: 0x04000159 RID: 345
		[FieldOffset(4)]
		public sbyte SByte4;

		// Token: 0x0400015A RID: 346
		[FieldOffset(5)]
		public sbyte SByte5;

		// Token: 0x0400015B RID: 347
		[FieldOffset(6)]
		public sbyte SByte6;

		// Token: 0x0400015C RID: 348
		[FieldOffset(7)]
		public sbyte SByte7;

		// Token: 0x0400015D RID: 349
		[FieldOffset(8)]
		public sbyte SByte8;

		// Token: 0x0400015E RID: 350
		[FieldOffset(9)]
		public sbyte SByte9;

		// Token: 0x0400015F RID: 351
		[FieldOffset(10)]
		public sbyte SByte10;

		// Token: 0x04000160 RID: 352
		[FieldOffset(11)]
		public sbyte SByte11;

		// Token: 0x04000161 RID: 353
		[FieldOffset(12)]
		public sbyte SByte12;

		// Token: 0x04000162 RID: 354
		[FieldOffset(13)]
		public sbyte SByte13;

		// Token: 0x04000163 RID: 355
		[FieldOffset(14)]
		public sbyte SByte14;

		// Token: 0x04000164 RID: 356
		[FieldOffset(15)]
		public sbyte SByte15;

		// Token: 0x04000165 RID: 357
		[FieldOffset(0)]
		public ushort UShort0;

		// Token: 0x04000166 RID: 358
		[FieldOffset(2)]
		public ushort UShort1;

		// Token: 0x04000167 RID: 359
		[FieldOffset(4)]
		public ushort UShort2;

		// Token: 0x04000168 RID: 360
		[FieldOffset(6)]
		public ushort UShort3;

		// Token: 0x04000169 RID: 361
		[FieldOffset(8)]
		public ushort UShort4;

		// Token: 0x0400016A RID: 362
		[FieldOffset(10)]
		public ushort UShort5;

		// Token: 0x0400016B RID: 363
		[FieldOffset(12)]
		public ushort UShort6;

		// Token: 0x0400016C RID: 364
		[FieldOffset(14)]
		public ushort UShort7;

		// Token: 0x0400016D RID: 365
		[FieldOffset(0)]
		public short SShort0;

		// Token: 0x0400016E RID: 366
		[FieldOffset(2)]
		public short SShort1;

		// Token: 0x0400016F RID: 367
		[FieldOffset(4)]
		public short SShort2;

		// Token: 0x04000170 RID: 368
		[FieldOffset(6)]
		public short SShort3;

		// Token: 0x04000171 RID: 369
		[FieldOffset(8)]
		public short SShort4;

		// Token: 0x04000172 RID: 370
		[FieldOffset(10)]
		public short SShort5;

		// Token: 0x04000173 RID: 371
		[FieldOffset(12)]
		public short SShort6;

		// Token: 0x04000174 RID: 372
		[FieldOffset(14)]
		public short SShort7;

		// Token: 0x04000175 RID: 373
		[FieldOffset(0)]
		public uint UInt0;

		// Token: 0x04000176 RID: 374
		[FieldOffset(4)]
		public uint UInt1;

		// Token: 0x04000177 RID: 375
		[FieldOffset(8)]
		public uint UInt2;

		// Token: 0x04000178 RID: 376
		[FieldOffset(12)]
		public uint UInt3;

		// Token: 0x04000179 RID: 377
		[FieldOffset(0)]
		public int SInt0;

		// Token: 0x0400017A RID: 378
		[FieldOffset(4)]
		public int SInt1;

		// Token: 0x0400017B RID: 379
		[FieldOffset(8)]
		public int SInt2;

		// Token: 0x0400017C RID: 380
		[FieldOffset(12)]
		public int SInt3;

		// Token: 0x0400017D RID: 381
		[FieldOffset(0)]
		public ulong ULong0;

		// Token: 0x0400017E RID: 382
		[FieldOffset(8)]
		public ulong ULong1;

		// Token: 0x0400017F RID: 383
		[FieldOffset(0)]
		public long SLong0;

		// Token: 0x04000180 RID: 384
		[FieldOffset(8)]
		public long SLong1;

		// Token: 0x04000181 RID: 385
		[FieldOffset(0)]
		public float Float0;

		// Token: 0x04000182 RID: 386
		[FieldOffset(4)]
		public float Float1;

		// Token: 0x04000183 RID: 387
		[FieldOffset(8)]
		public float Float2;

		// Token: 0x04000184 RID: 388
		[FieldOffset(12)]
		public float Float3;

		// Token: 0x04000185 RID: 389
		[FieldOffset(0)]
		public double Double0;

		// Token: 0x04000186 RID: 390
		[FieldOffset(8)]
		public double Double1;

		// Token: 0x04000187 RID: 391
		[FieldOffset(0)]
		public v64 Lo64;

		// Token: 0x04000188 RID: 392
		[FieldOffset(8)]
		public v64 Hi64;
	}
}
