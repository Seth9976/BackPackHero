using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x0200015C RID: 348
	internal sealed class SqlCachedBuffer : INullable
	{
		// Token: 0x0600111A RID: 4378 RVA: 0x00003D55 File Offset: 0x00001F55
		private SqlCachedBuffer()
		{
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00054AC4 File Offset: 0x00052CC4
		private SqlCachedBuffer(List<byte[]> cachedBytes)
		{
			this._cachedBytes = cachedBytes;
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x00054AD3 File Offset: 0x00052CD3
		internal List<byte[]> CachedBytes
		{
			get
			{
				return this._cachedBytes;
			}
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00054ADC File Offset: 0x00052CDC
		internal static bool TryCreate(SqlMetaDataPriv metadata, TdsParser parser, TdsParserStateObject stateObj, out SqlCachedBuffer buffer)
		{
			int num = 0;
			List<byte[]> list = new List<byte[]>();
			buffer = null;
			ulong num2;
			if (!parser.TryPlpBytesLeft(stateObj, out num2))
			{
				return false;
			}
			while (num2 != 0UL)
			{
				do
				{
					num = ((num2 > 2048UL) ? 2048 : ((int)num2));
					byte[] array = new byte[num];
					if (!stateObj.TryReadPlpBytes(ref array, 0, num, out num))
					{
						return false;
					}
					if (list.Count == 0)
					{
						SqlCachedBuffer.AddByteOrderMark(array, list);
					}
					list.Add(array);
					num2 -= (ulong)((long)num);
				}
				while (num2 > 0UL);
				if (!parser.TryPlpBytesLeft(stateObj, out num2))
				{
					return false;
				}
				if (num2 <= 0UL)
				{
					break;
				}
				continue;
			}
			buffer = new SqlCachedBuffer(list);
			return true;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00054B69 File Offset: 0x00052D69
		private static void AddByteOrderMark(byte[] byteArr, List<byte[]> cachedBytes)
		{
			if (byteArr.Length < 2 || byteArr[0] != 223 || byteArr[1] != 255)
			{
				cachedBytes.Add(TdsEnums.XMLUNICODEBOMBYTES);
			}
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00054B90 File Offset: 0x00052D90
		internal Stream ToStream()
		{
			return new SqlCachedStream(this);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00054B98 File Offset: 0x00052D98
		public override string ToString()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			if (this._cachedBytes.Count == 0)
			{
				return string.Empty;
			}
			return new SqlXml(this.ToStream()).Value;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00054BCB File Offset: 0x00052DCB
		internal SqlString ToSqlString()
		{
			if (this.IsNull)
			{
				return SqlString.Null;
			}
			return new SqlString(this.ToString());
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00054BE6 File Offset: 0x00052DE6
		internal SqlXml ToSqlXml()
		{
			return new SqlXml(this.ToStream());
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00054BF3 File Offset: 0x00052DF3
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal XmlReader ToXmlReader()
		{
			return SqlTypeWorkarounds.SqlXmlCreateSqlXmlReader(this.ToStream(), false, false);
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x00054C02 File Offset: 0x00052E02
		public bool IsNull
		{
			get
			{
				return this._cachedBytes == null;
			}
		}

		// Token: 0x04000B5D RID: 2909
		public static readonly SqlCachedBuffer Null = new SqlCachedBuffer();

		// Token: 0x04000B5E RID: 2910
		private const int _maxChunkSize = 2048;

		// Token: 0x04000B5F RID: 2911
		private List<byte[]> _cachedBytes;
	}
}
