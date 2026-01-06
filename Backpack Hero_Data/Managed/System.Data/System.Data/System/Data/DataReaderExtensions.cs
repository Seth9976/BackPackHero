using System;
using System.ComponentModel;
using System.Data.Common;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data
{
	// Token: 0x02000108 RID: 264
	public static class DataReaderExtensions
	{
		// Token: 0x06000E7A RID: 3706 RVA: 0x0004EC62 File Offset: 0x0004CE62
		public static bool GetBoolean(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetBoolean(reader.GetOrdinal(name));
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0004EC77 File Offset: 0x0004CE77
		public static byte GetByte(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetByte(reader.GetOrdinal(name));
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0004EC8C File Offset: 0x0004CE8C
		public static long GetBytes(this DbDataReader reader, string name, long dataOffset, byte[] buffer, int bufferOffset, int length)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetBytes(reader.GetOrdinal(name), dataOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0004ECA7 File Offset: 0x0004CEA7
		public static char GetChar(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetChar(reader.GetOrdinal(name));
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0004ECBC File Offset: 0x0004CEBC
		public static long GetChars(this DbDataReader reader, string name, long dataOffset, char[] buffer, int bufferOffset, int length)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetChars(reader.GetOrdinal(name), dataOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0004ECD7 File Offset: 0x0004CED7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static DbDataReader GetData(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetData(reader.GetOrdinal(name));
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0004ECEC File Offset: 0x0004CEEC
		public static string GetDataTypeName(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetDataTypeName(reader.GetOrdinal(name));
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0004ED01 File Offset: 0x0004CF01
		public static DateTime GetDateTime(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetDateTime(reader.GetOrdinal(name));
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0004ED16 File Offset: 0x0004CF16
		public static decimal GetDecimal(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetDecimal(reader.GetOrdinal(name));
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0004ED2B File Offset: 0x0004CF2B
		public static double GetDouble(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetDouble(reader.GetOrdinal(name));
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0004ED40 File Offset: 0x0004CF40
		public static Type GetFieldType(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetFieldType(reader.GetOrdinal(name));
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0004ED55 File Offset: 0x0004CF55
		public static T GetFieldValue<T>(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetFieldValue<T>(reader.GetOrdinal(name));
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0004ED6A File Offset: 0x0004CF6A
		public static Task<T> GetFieldValueAsync<T>(this DbDataReader reader, string name, CancellationToken cancellationToken = default(CancellationToken))
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetFieldValueAsync<T>(reader.GetOrdinal(name), cancellationToken);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0004ED80 File Offset: 0x0004CF80
		public static float GetFloat(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetFloat(reader.GetOrdinal(name));
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0004ED95 File Offset: 0x0004CF95
		public static Guid GetGuid(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetGuid(reader.GetOrdinal(name));
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0004EDAA File Offset: 0x0004CFAA
		public static short GetInt16(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetInt16(reader.GetOrdinal(name));
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0004EDBF File Offset: 0x0004CFBF
		public static int GetInt32(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetInt32(reader.GetOrdinal(name));
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0004EDD4 File Offset: 0x0004CFD4
		public static long GetInt64(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetInt64(reader.GetOrdinal(name));
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0004EDE9 File Offset: 0x0004CFE9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static Type GetProviderSpecificFieldType(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetProviderSpecificFieldType(reader.GetOrdinal(name));
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0004EDFE File Offset: 0x0004CFFE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static object GetProviderSpecificValue(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetProviderSpecificValue(reader.GetOrdinal(name));
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0004EE13 File Offset: 0x0004D013
		public static Stream GetStream(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetStream(reader.GetOrdinal(name));
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0004EE28 File Offset: 0x0004D028
		public static string GetString(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetString(reader.GetOrdinal(name));
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0004EE3D File Offset: 0x0004D03D
		public static TextReader GetTextReader(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetTextReader(reader.GetOrdinal(name));
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0004EE52 File Offset: 0x0004D052
		public static object GetValue(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetValue(reader.GetOrdinal(name));
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0004EE67 File Offset: 0x0004D067
		public static bool IsDBNull(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.IsDBNull(reader.GetOrdinal(name));
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0004EE7C File Offset: 0x0004D07C
		public static Task<bool> IsDBNullAsync(this DbDataReader reader, string name, CancellationToken cancellationToken = default(CancellationToken))
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.IsDBNullAsync(reader.GetOrdinal(name), cancellationToken);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0004EE92 File Offset: 0x0004D092
		private static void AssertNotNull(DbDataReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
		}
	}
}
