using System;

namespace Unity.Collections
{
	// Token: 0x020000A2 RID: 162
	[BurstCompatible]
	public static class FixedString
	{
		// Token: 0x060004EF RID: 1263 RVA: 0x0000D380 File Offset: 0x0000B580
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, int arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000D3EC File Offset: 0x0000B5EC
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, int arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000D45C File Offset: 0x0000B65C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, int arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000D4C8 File Offset: 0x0000B6C8
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, int arg1, int arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000D524 File Offset: 0x0000B724
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, int arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000D594 File Offset: 0x0000B794
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, int arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000D604 File Offset: 0x0000B804
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, int arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000D674 File Offset: 0x0000B874
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, float arg1, int arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, int arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000D740 File Offset: 0x0000B940
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, int arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000D7B0 File Offset: 0x0000B9B0
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, int arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000D81C File Offset: 0x0000BA1C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, string arg1, int arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000D878 File Offset: 0x0000BA78
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, T1 arg1, int arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000D8D4 File Offset: 0x0000BAD4
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, T1 arg1, int arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000D934 File Offset: 0x0000BB34
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, T1 arg1, int arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000D990 File Offset: 0x0000BB90
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, int arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg2);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in arg1, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000D9DC File Offset: 0x0000BBDC
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, float arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000DA4C File Offset: 0x0000BC4C
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, float arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000DABC File Offset: 0x0000BCBC
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, float arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000DB2C File Offset: 0x0000BD2C
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, int arg1, float arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000DB8C File Offset: 0x0000BD8C
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, float arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000DBFC File Offset: 0x0000BDFC
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, float arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000DC70 File Offset: 0x0000BE70
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, float arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000DCE0 File Offset: 0x0000BEE0
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, float arg1, float arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000DD40 File Offset: 0x0000BF40
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, float arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, float arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000DE20 File Offset: 0x0000C020
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, float arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000DE90 File Offset: 0x0000C090
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, string arg1, float arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000DEF0 File Offset: 0x0000C0F0
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, T1 arg1, float arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000DF50 File Offset: 0x0000C150
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, T1 arg1, float arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000DFB0 File Offset: 0x0000C1B0
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, T1 arg1, float arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000E010 File Offset: 0x0000C210
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, float arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in arg1, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000E05C File Offset: 0x0000C25C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, string arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000E0C8 File Offset: 0x0000C2C8
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, string arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000E138 File Offset: 0x0000C338
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, string arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000E1A4 File Offset: 0x0000C3A4
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, int arg1, string arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000E200 File Offset: 0x0000C400
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, string arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000E270 File Offset: 0x0000C470
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, string arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000E2E0 File Offset: 0x0000C4E0
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, string arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000E350 File Offset: 0x0000C550
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, float arg1, string arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000E3B0 File Offset: 0x0000C5B0
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, string arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000E41C File Offset: 0x0000C61C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, string arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000E48C File Offset: 0x0000C68C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, string arg2, int arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000E4F8 File Offset: 0x0000C6F8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, string arg1, string arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000E554 File Offset: 0x0000C754
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, T1 arg1, string arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000E5B0 File Offset: 0x0000C7B0
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, T1 arg1, string arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000E610 File Offset: 0x0000C810
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, T1 arg1, string arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000E66C File Offset: 0x0000C86C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, string arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg2);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in arg1, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000E6B8 File Offset: 0x0000C8B8
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, int arg1, T1 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000E714 File Offset: 0x0000C914
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, int arg1, T1 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000E774 File Offset: 0x0000C974
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, int arg1, T1 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000E7D0 File Offset: 0x0000C9D0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, int arg1, T2 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in arg2, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000E81C File Offset: 0x0000CA1C
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, float arg1, T1 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000E87C File Offset: 0x0000CA7C
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, float arg1, T1 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000E8DC File Offset: 0x0000CADC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, float arg1, T1 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000E93C File Offset: 0x0000CB3C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, float arg1, T2 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in arg2, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000E988 File Offset: 0x0000CB88
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, string arg1, T1 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, string arg1, T1 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000EA44 File Offset: 0x0000CC44
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, string arg1, T1 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000EAA0 File Offset: 0x0000CCA0
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, string arg1, T2 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in arg2, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000EAEC File Offset: 0x0000CCEC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, int arg0, T1 arg1, T2 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in arg2, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000EB38 File Offset: 0x0000CD38
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, float arg0, T1 arg1, T2 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in arg2, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000EB84 File Offset: 0x0000CD84
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, string arg0, T1 arg1, T2 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in arg2, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, T1 arg0, T2 arg1, T3 arg2, int arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in arg1, in arg2, in fixedString32Bytes);
			return fixedString512Bytes;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000EC0C File Offset: 0x0000CE0C
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, int arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000EC7C File Offset: 0x0000CE7C
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, int arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, int arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000ED5C File Offset: 0x0000CF5C
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, int arg1, int arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000EDBC File Offset: 0x0000CFBC
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, int arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000EE2C File Offset: 0x0000D02C
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, int arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000EEA0 File Offset: 0x0000D0A0
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, int arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000EF10 File Offset: 0x0000D110
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, float arg1, int arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0000EF70 File Offset: 0x0000D170
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, int arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000EFE0 File Offset: 0x0000D1E0
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, int arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0000F050 File Offset: 0x0000D250
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, int arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000F0C0 File Offset: 0x0000D2C0
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, string arg1, int arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000F120 File Offset: 0x0000D320
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, T1 arg1, int arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000F180 File Offset: 0x0000D380
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, T1 arg1, int arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000F1E0 File Offset: 0x0000D3E0
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, T1 arg1, int arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000F240 File Offset: 0x0000D440
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, int arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg2);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in arg1, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000F28C File Offset: 0x0000D48C
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, float arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000F2FC File Offset: 0x0000D4FC
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, float arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0000F370 File Offset: 0x0000D570
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, float arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0000F3E0 File Offset: 0x0000D5E0
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, int arg1, float arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0000F440 File Offset: 0x0000D640
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, float arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0000F4B4 File Offset: 0x0000D6B4
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, float arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0000F528 File Offset: 0x0000D728
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, float arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0000F59C File Offset: 0x0000D79C
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, float arg1, float arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0000F600 File Offset: 0x0000D800
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, float arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000F670 File Offset: 0x0000D870
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, float arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0000F6E4 File Offset: 0x0000D8E4
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, float arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0000F754 File Offset: 0x0000D954
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, string arg1, float arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0000F7B4 File Offset: 0x0000D9B4
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, T1 arg1, float arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000F814 File Offset: 0x0000DA14
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, T1 arg1, float arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000F878 File Offset: 0x0000DA78
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, T1 arg1, float arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000F8D8 File Offset: 0x0000DAD8
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, float arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in arg1, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0000F928 File Offset: 0x0000DB28
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, string arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000F998 File Offset: 0x0000DB98
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, string arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000FA08 File Offset: 0x0000DC08
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, string arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000FA78 File Offset: 0x0000DC78
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, int arg1, string arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, string arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0000FB48 File Offset: 0x0000DD48
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, string arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000FBBC File Offset: 0x0000DDBC
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, string arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, float arg1, string arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0000FC8C File Offset: 0x0000DE8C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, string arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0000FCFC File Offset: 0x0000DEFC
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, string arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0000FD6C File Offset: 0x0000DF6C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, string arg2, float arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0000FDDC File Offset: 0x0000DFDC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, string arg1, string arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0000FE3C File Offset: 0x0000E03C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, T1 arg1, string arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000FE9C File Offset: 0x0000E09C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, T1 arg1, string arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000FEFC File Offset: 0x0000E0FC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, T1 arg1, string arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0000FF5C File Offset: 0x0000E15C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, string arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg2);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in arg1, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0000FFA8 File Offset: 0x0000E1A8
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, int arg1, T1 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00010008 File Offset: 0x0000E208
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, int arg1, T1 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00010068 File Offset: 0x0000E268
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, int arg1, T1 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000100C8 File Offset: 0x0000E2C8
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, int arg1, T2 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in arg2, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00010114 File Offset: 0x0000E314
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, float arg1, T1 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00010174 File Offset: 0x0000E374
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, float arg1, T1 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000101D8 File Offset: 0x0000E3D8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, float arg1, T1 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00010238 File Offset: 0x0000E438
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, float arg1, T2 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in arg2, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00010288 File Offset: 0x0000E488
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, string arg1, T1 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x000102E8 File Offset: 0x0000E4E8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, string arg1, T1 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00010348 File Offset: 0x0000E548
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, string arg1, T1 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000103A8 File Offset: 0x0000E5A8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, string arg1, T2 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in arg2, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000103F4 File Offset: 0x0000E5F4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, int arg0, T1 arg1, T2 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in arg2, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00010440 File Offset: 0x0000E640
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, float arg0, T1 arg1, T2 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in arg2, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00010490 File Offset: 0x0000E690
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, string arg0, T1 arg1, T2 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in arg2, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000104DC File Offset: 0x0000E6DC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, T1 arg0, T2 arg1, T3 arg2, float arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg3, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in arg1, in arg2, in fixedString32Bytes);
			return fixedString512Bytes;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00010518 File Offset: 0x0000E718
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, int arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00010584 File Offset: 0x0000E784
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, int arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000105F4 File Offset: 0x0000E7F4
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, int arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00010660 File Offset: 0x0000E860
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, int arg1, int arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000106BC File Offset: 0x0000E8BC
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, int arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0001072C File Offset: 0x0000E92C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, int arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001079C File Offset: 0x0000E99C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, int arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001080C File Offset: 0x0000EA0C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, float arg1, int arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001086C File Offset: 0x0000EA6C
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, int arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000108D8 File Offset: 0x0000EAD8
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, int arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00010948 File Offset: 0x0000EB48
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, int arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000109B4 File Offset: 0x0000EBB4
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, string arg1, int arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00010A10 File Offset: 0x0000EC10
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, T1 arg1, int arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00010A6C File Offset: 0x0000EC6C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, T1 arg1, int arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00010ACC File Offset: 0x0000ECCC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, T1 arg1, int arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00010B28 File Offset: 0x0000ED28
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, int arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg2);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in arg1, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00010B74 File Offset: 0x0000ED74
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, float arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00010BE4 File Offset: 0x0000EDE4
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, float arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00010C54 File Offset: 0x0000EE54
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, float arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00010CC4 File Offset: 0x0000EEC4
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, int arg1, float arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00010D24 File Offset: 0x0000EF24
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, float arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00010D94 File Offset: 0x0000EF94
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, float arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00010E08 File Offset: 0x0000F008
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, float arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00010E78 File Offset: 0x0000F078
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, float arg1, float arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00010ED8 File Offset: 0x0000F0D8
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, float arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00010F48 File Offset: 0x0000F148
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, float arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00010FB8 File Offset: 0x0000F1B8
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, float arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00011028 File Offset: 0x0000F228
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, string arg1, float arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00011088 File Offset: 0x0000F288
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, T1 arg1, float arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x000110E8 File Offset: 0x0000F2E8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, T1 arg1, float arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00011148 File Offset: 0x0000F348
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, T1 arg1, float arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000111A8 File Offset: 0x0000F3A8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, float arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg2, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in arg1, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000111F4 File Offset: 0x0000F3F4
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, int arg1, string arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00011260 File Offset: 0x0000F460
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, int arg1, string arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000112D0 File Offset: 0x0000F4D0
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, int arg1, string arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001133C File Offset: 0x0000F53C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, int arg1, string arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00011398 File Offset: 0x0000F598
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, float arg1, string arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00011408 File Offset: 0x0000F608
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, float arg1, string arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00011478 File Offset: 0x0000F678
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, float arg1, string arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x000114E8 File Offset: 0x0000F6E8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, float arg1, string arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00011548 File Offset: 0x0000F748
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, int arg0, string arg1, string arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000115B4 File Offset: 0x0000F7B4
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, float arg0, string arg1, string arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00011624 File Offset: 0x0000F824
		[NotBurstCompatible]
		public static FixedString512Bytes Format(FixedString512Bytes formatString, string arg0, string arg1, string arg2, string arg3)
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			FixedString32Bytes fixedString32Bytes4 = default(FixedString32Bytes);
			(ref fixedString32Bytes4).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in fixedString32Bytes4);
			return fixedString512Bytes;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00011690 File Offset: 0x0000F890
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, T1 arg0, string arg1, string arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x000116EC File Offset: 0x0000F8EC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, T1 arg1, string arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00011748 File Offset: 0x0000F948
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, T1 arg1, string arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000117A8 File Offset: 0x0000F9A8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, T1 arg1, string arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00011804 File Offset: 0x0000FA04
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, T2 arg1, string arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg2);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in arg1, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00011850 File Offset: 0x0000FA50
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, int arg1, T1 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x000118AC File Offset: 0x0000FAAC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, int arg1, T1 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001190C File Offset: 0x0000FB0C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, int arg1, T1 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00011968 File Offset: 0x0000FB68
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, int arg1, T2 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in arg2, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x000119B4 File Offset: 0x0000FBB4
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, float arg1, T1 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00011A14 File Offset: 0x0000FC14
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, float arg1, T1 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00011A74 File Offset: 0x0000FC74
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, float arg1, T1 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00011AD4 File Offset: 0x0000FCD4
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, float arg1, T2 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in arg2, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00011B20 File Offset: 0x0000FD20
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, string arg1, T1 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00011B7C File Offset: 0x0000FD7C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, string arg1, T1 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00011BDC File Offset: 0x0000FDDC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, string arg1, T1 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in fixedString32Bytes3);
			return fixedString512Bytes;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00011C38 File Offset: 0x0000FE38
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, string arg1, T2 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in arg2, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00011C84 File Offset: 0x0000FE84
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, int arg0, T1 arg1, T2 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in arg2, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00011CD0 File Offset: 0x0000FED0
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, float arg0, T1 arg1, T2 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in arg2, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00011D1C File Offset: 0x0000FF1C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, string arg0, T1 arg1, T2 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in arg2, in fixedString32Bytes2);
			return fixedString512Bytes;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00011D68 File Offset: 0x0000FF68
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, T1 arg0, T2 arg1, T3 arg2, string arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg3);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in arg1, in arg2, in fixedString32Bytes);
			return fixedString512Bytes;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00011DA4 File Offset: 0x0000FFA4
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, int arg1, int arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00011E00 File Offset: 0x00010000
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, int arg1, int arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00011E5C File Offset: 0x0001005C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, int arg1, int arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00011EB8 File Offset: 0x000100B8
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, int arg1, int arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00011F04 File Offset: 0x00010104
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, float arg1, int arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00011F60 File Offset: 0x00010160
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, float arg1, int arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00011FC0 File Offset: 0x000101C0
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, float arg1, int arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0001201C File Offset: 0x0001021C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, float arg1, int arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00012068 File Offset: 0x00010268
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, string arg1, int arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000120C4 File Offset: 0x000102C4
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, string arg1, int arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00012120 File Offset: 0x00010320
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, string arg1, int arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001217C File Offset: 0x0001037C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, string arg1, int arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000121C8 File Offset: 0x000103C8
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, int arg0, T1 arg1, int arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00012214 File Offset: 0x00010414
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, float arg0, T1 arg1, int arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00012260 File Offset: 0x00010460
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, string arg0, T1 arg1, int arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000122AC File Offset: 0x000104AC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, T1 arg0, T2 arg1, int arg2, T3 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in arg1, in fixedString32Bytes, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000122E4 File Offset: 0x000104E4
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, int arg1, float arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00012340 File Offset: 0x00010540
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, int arg1, float arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000123A0 File Offset: 0x000105A0
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, int arg1, float arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000123FC File Offset: 0x000105FC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, int arg1, float arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00012448 File Offset: 0x00010648
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, float arg1, float arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000124A8 File Offset: 0x000106A8
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, float arg1, float arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00012508 File Offset: 0x00010708
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, float arg1, float arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00012568 File Offset: 0x00010768
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, float arg1, float arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000125B8 File Offset: 0x000107B8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, string arg1, float arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00012614 File Offset: 0x00010814
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, string arg1, float arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00012674 File Offset: 0x00010874
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, string arg1, float arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000126D0 File Offset: 0x000108D0
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, string arg1, float arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0001271C File Offset: 0x0001091C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, int arg0, T1 arg1, float arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00012768 File Offset: 0x00010968
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, float arg0, T1 arg1, float arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000127B8 File Offset: 0x000109B8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, string arg0, T1 arg1, float arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00012804 File Offset: 0x00010A04
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, T1 arg0, T2 arg1, float arg2, T3 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg2, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in arg1, in fixedString32Bytes, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00012840 File Offset: 0x00010A40
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, int arg1, string arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001289C File Offset: 0x00010A9C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, int arg1, string arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000128F8 File Offset: 0x00010AF8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, int arg1, string arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00012954 File Offset: 0x00010B54
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, int arg1, string arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x000129A0 File Offset: 0x00010BA0
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, float arg1, string arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000129FC File Offset: 0x00010BFC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, float arg1, string arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00012A5C File Offset: 0x00010C5C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, float arg1, string arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00012AB8 File Offset: 0x00010CB8
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, float arg1, string arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00012B04 File Offset: 0x00010D04
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, int arg0, string arg1, string arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00012B60 File Offset: 0x00010D60
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, float arg0, string arg1, string arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00012BBC File Offset: 0x00010DBC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1>(FixedString512Bytes formatString, string arg0, string arg1, string arg2, T1 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00012C18 File Offset: 0x00010E18
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, T1 arg0, string arg1, string arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00012C64 File Offset: 0x00010E64
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, int arg0, T1 arg1, string arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00012CB0 File Offset: 0x00010EB0
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, float arg0, T1 arg1, string arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00012CFC File Offset: 0x00010EFC
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, string arg0, T1 arg1, string arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00012D48 File Offset: 0x00010F48
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, T1 arg0, T2 arg1, string arg2, T3 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg2);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in arg1, in fixedString32Bytes, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00012D80 File Offset: 0x00010F80
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, int arg0, int arg1, T1 arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00012DCC File Offset: 0x00010FCC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, float arg0, int arg1, T1 arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00012E18 File Offset: 0x00011018
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, string arg0, int arg1, T1 arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00012E64 File Offset: 0x00011064
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, T1 arg0, int arg1, T2 arg2, T3 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in arg2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00012E9C File Offset: 0x0001109C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, int arg0, float arg1, T1 arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00012EE8 File Offset: 0x000110E8
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, float arg0, float arg1, T1 arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00012F38 File Offset: 0x00011138
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, string arg0, float arg1, T1 arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00012F84 File Offset: 0x00011184
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, T1 arg0, float arg1, T2 arg2, T3 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in arg2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00012FC0 File Offset: 0x000111C0
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, int arg0, string arg1, T1 arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001300C File Offset: 0x0001120C
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, float arg0, string arg1, T1 arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00013058 File Offset: 0x00011258
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2>(FixedString512Bytes formatString, string arg0, string arg1, T1 arg2, T2 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000130A4 File Offset: 0x000112A4
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, T1 arg0, string arg1, T2 arg2, T3 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in arg2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x000130DC File Offset: 0x000112DC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, int arg0, T1 arg1, T2 arg2, T3 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in arg2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00013114 File Offset: 0x00011314
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, float arg0, T1 arg1, T2 arg2, T3 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in arg2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00013150 File Offset: 0x00011350
		[NotBurstCompatible]
		public static FixedString512Bytes Format<T1, T2, T3>(FixedString512Bytes formatString, string arg0, T1 arg1, T2 arg2, T3 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			(ref fixedString512Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in arg2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00013188 File Offset: 0x00011388
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString512Bytes Format<T1, T2, T3, T4>(FixedString512Bytes formatString, T1 arg0, T2 arg1, T3 arg2, T4 arg3) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes where T4 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			(ref fixedString512Bytes).AppendFormat(in formatString, in arg0, in arg1, in arg2, in arg3);
			return fixedString512Bytes;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000131B0 File Offset: 0x000113B0
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, int arg1, int arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00013208 File Offset: 0x00011408
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, int arg1, int arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00013264 File Offset: 0x00011464
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, int arg1, int arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000132BC File Offset: 0x000114BC
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, int arg1, int arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00013304 File Offset: 0x00011504
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, float arg1, int arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00013360 File Offset: 0x00011560
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, float arg1, int arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x000133BC File Offset: 0x000115BC
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, float arg1, int arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00013418 File Offset: 0x00011618
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, float arg1, int arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00013464 File Offset: 0x00011664
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, string arg1, int arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x000134BC File Offset: 0x000116BC
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, string arg1, int arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00013518 File Offset: 0x00011718
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, string arg1, int arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00013570 File Offset: 0x00011770
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, string arg1, int arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000135B8 File Offset: 0x000117B8
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, int arg0, T1 arg1, int arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00013600 File Offset: 0x00011800
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, float arg0, T1 arg1, int arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001364C File Offset: 0x0001184C
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, string arg0, T1 arg1, int arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00013694 File Offset: 0x00011894
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1, T2>(FixedString128Bytes formatString, T1 arg0, T2 arg1, int arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0, in arg1, in fixedString32Bytes);
			return fixedString128Bytes;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000136CC File Offset: 0x000118CC
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, int arg1, float arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00013728 File Offset: 0x00011928
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, int arg1, float arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00013784 File Offset: 0x00011984
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, int arg1, float arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x000137E0 File Offset: 0x000119E0
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, int arg1, float arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001382C File Offset: 0x00011A2C
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, float arg1, float arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00013888 File Offset: 0x00011A88
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, float arg1, float arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000138E8 File Offset: 0x00011AE8
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, float arg1, float arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00013944 File Offset: 0x00011B44
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, float arg1, float arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00013990 File Offset: 0x00011B90
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, string arg1, float arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x000139EC File Offset: 0x00011BEC
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, string arg1, float arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00013A48 File Offset: 0x00011C48
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, string arg1, float arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00013AA4 File Offset: 0x00011CA4
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, string arg1, float arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00013AF0 File Offset: 0x00011CF0
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, int arg0, T1 arg1, float arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00013B3C File Offset: 0x00011D3C
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, float arg0, T1 arg1, float arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00013B88 File Offset: 0x00011D88
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, string arg0, T1 arg1, float arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00013BD4 File Offset: 0x00011DD4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1, T2>(FixedString128Bytes formatString, T1 arg0, T2 arg1, float arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg2, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0, in arg1, in fixedString32Bytes);
			return fixedString128Bytes;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00013C0C File Offset: 0x00011E0C
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, int arg1, string arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00013C64 File Offset: 0x00011E64
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, int arg1, string arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00013CC0 File Offset: 0x00011EC0
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, int arg1, string arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00013D18 File Offset: 0x00011F18
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, int arg1, string arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00013D60 File Offset: 0x00011F60
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, float arg1, string arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00013DBC File Offset: 0x00011FBC
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, float arg1, string arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00013E18 File Offset: 0x00012018
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, float arg1, string arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00013E74 File Offset: 0x00012074
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, float arg1, string arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00013EC0 File Offset: 0x000120C0
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, string arg1, string arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00013F18 File Offset: 0x00012118
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, string arg1, string arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00013F74 File Offset: 0x00012174
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, string arg1, string arg2)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			FixedString32Bytes fixedString32Bytes3 = default(FixedString32Bytes);
			(ref fixedString32Bytes3).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in fixedString32Bytes3);
			return fixedString128Bytes;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00013FCC File Offset: 0x000121CC
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, string arg1, string arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00014014 File Offset: 0x00012214
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, int arg0, T1 arg1, string arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001405C File Offset: 0x0001225C
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, float arg0, T1 arg1, string arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000140A8 File Offset: 0x000122A8
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, string arg0, T1 arg1, string arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000140F0 File Offset: 0x000122F0
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1, T2>(FixedString128Bytes formatString, T1 arg0, T2 arg1, string arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg2);
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0, in arg1, in fixedString32Bytes);
			return fixedString128Bytes;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00014128 File Offset: 0x00012328
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, int arg0, int arg1, T1 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2);
			return fixedString128Bytes;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00014170 File Offset: 0x00012370
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, float arg0, int arg1, T1 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2);
			return fixedString128Bytes;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x000141BC File Offset: 0x000123BC
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, string arg0, int arg1, T1 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2);
			return fixedString128Bytes;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00014204 File Offset: 0x00012404
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1, T2>(FixedString128Bytes formatString, T1 arg0, int arg1, T2 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in arg2);
			return fixedString128Bytes;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001423C File Offset: 0x0001243C
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, int arg0, float arg1, T1 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2);
			return fixedString128Bytes;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00014288 File Offset: 0x00012488
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, float arg0, float arg1, T1 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2);
			return fixedString128Bytes;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x000142D4 File Offset: 0x000124D4
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, string arg0, float arg1, T1 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2);
			return fixedString128Bytes;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00014320 File Offset: 0x00012520
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1, T2>(FixedString128Bytes formatString, T1 arg0, float arg1, T2 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in arg2);
			return fixedString128Bytes;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00014358 File Offset: 0x00012558
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, int arg0, string arg1, T1 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2);
			return fixedString128Bytes;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x000143A0 File Offset: 0x000125A0
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, float arg0, string arg1, T1 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2);
			return fixedString128Bytes;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000143EC File Offset: 0x000125EC
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, string arg0, string arg1, T1 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2, in arg2);
			return fixedString128Bytes;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00014434 File Offset: 0x00012634
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1, T2>(FixedString128Bytes formatString, T1 arg0, string arg1, T2 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes, in arg2);
			return fixedString128Bytes;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001446C File Offset: 0x0001266C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1, T2>(FixedString128Bytes formatString, int arg0, T1 arg1, T2 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in arg2);
			return fixedString128Bytes;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x000144A4 File Offset: 0x000126A4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1, T2>(FixedString128Bytes formatString, float arg0, T1 arg1, T2 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in arg2);
			return fixedString128Bytes;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000144DC File Offset: 0x000126DC
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1, T2>(FixedString128Bytes formatString, string arg0, T1 arg1, T2 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1, in arg2);
			return fixedString128Bytes;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00014514 File Offset: 0x00012714
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1, T2, T3>(FixedString128Bytes formatString, T1 arg0, T2 arg1, T3 arg2) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0, in arg1, in arg2);
			return fixedString128Bytes;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001453C File Offset: 0x0001273C
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, int arg1)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00014584 File Offset: 0x00012784
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, int arg1)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000145CC File Offset: 0x000127CC
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, int arg1)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00014614 File Offset: 0x00012814
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, int arg1) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes);
			return fixedString128Bytes;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00014648 File Offset: 0x00012848
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, float arg1)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00014690 File Offset: 0x00012890
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, float arg1)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x000146DC File Offset: 0x000128DC
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, float arg1)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00014724 File Offset: 0x00012924
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, float arg1) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes);
			return fixedString128Bytes;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001475C File Offset: 0x0001295C
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0, string arg1)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x000147A4 File Offset: 0x000129A4
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0, string arg1)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x000147EC File Offset: 0x000129EC
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0, string arg1)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			FixedString32Bytes fixedString32Bytes2 = default(FixedString32Bytes);
			(ref fixedString32Bytes2).Append(arg1);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in fixedString32Bytes2);
			return fixedString128Bytes;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00014834 File Offset: 0x00012A34
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0, string arg1) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg1);
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0, in fixedString32Bytes);
			return fixedString128Bytes;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00014868 File Offset: 0x00012A68
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, int arg0, T1 arg1) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1);
			return fixedString128Bytes;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001489C File Offset: 0x00012A9C
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, float arg0, T1 arg1) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1);
			return fixedString128Bytes;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x000148D4 File Offset: 0x00012AD4
		[NotBurstCompatible]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, string arg0, T1 arg1) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes, in arg1);
			return fixedString128Bytes;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00014908 File Offset: 0x00012B08
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString32Bytes),
			typeof(FixedString32Bytes)
		})]
		public static FixedString128Bytes Format<T1, T2>(FixedString128Bytes formatString, T1 arg0, T2 arg1) where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0, in arg1);
			return fixedString128Bytes;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001492C File Offset: 0x00012B2C
		public static FixedString128Bytes Format(FixedString128Bytes formatString, int arg0)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes);
			return fixedString128Bytes;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00014960 File Offset: 0x00012B60
		public static FixedString128Bytes Format(FixedString128Bytes formatString, float arg0)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0, '.');
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes);
			return fixedString128Bytes;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00014994 File Offset: 0x00012B94
		[NotBurstCompatible]
		public static FixedString128Bytes Format(FixedString128Bytes formatString, string arg0)
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			(ref fixedString32Bytes).Append(arg0);
			(ref fixedString128Bytes).AppendFormat(in formatString, in fixedString32Bytes);
			return fixedString128Bytes;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x000149C8 File Offset: 0x00012BC8
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString32Bytes) })]
		public static FixedString128Bytes Format<T1>(FixedString128Bytes formatString, T1 arg0) where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedString128Bytes fixedString128Bytes = default(FixedString128Bytes);
			(ref fixedString128Bytes).AppendFormat(in formatString, in arg0);
			return fixedString128Bytes;
		}
	}
}
