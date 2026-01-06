using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;

namespace System.Data.SqlTypes
{
	// Token: 0x020002D1 RID: 721
	internal static class SqlTypeWorkarounds
	{
		// Token: 0x06002225 RID: 8741 RVA: 0x0009E3CC File Offset: 0x0009C5CC
		internal static XmlReader SqlXmlCreateSqlXmlReader(Stream stream, bool closeInput = false, bool async = false)
		{
			XmlReaderSettings xmlReaderSettings = (closeInput ? (async ? SqlTypeWorkarounds.s_defaultXmlReaderSettingsAsyncCloseInput : SqlTypeWorkarounds.s_defaultXmlReaderSettingsCloseInput) : SqlTypeWorkarounds.s_defaultXmlReaderSettings);
			return XmlReader.Create(stream, xmlReaderSettings);
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x0009E3FC File Offset: 0x0009C5FC
		internal static DateTime SqlDateTimeToDateTime(int daypart, int timepart)
		{
			if (daypart < -53690 || daypart > 2958463 || timepart < 0 || timepart > 25919999)
			{
				throw new OverflowException(SQLResource.DateTimeOverflowMessage);
			}
			long ticks = new DateTime(1900, 1, 1).Ticks;
			long num = (long)daypart * 864000000000L;
			long num2 = (long)((double)timepart / 0.3 + 0.5) * 10000L;
			return new DateTime(ticks + num + num2);
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x0009E47C File Offset: 0x0009C67C
		internal static SqlMoney SqlMoneyCtor(long value, int ignored)
		{
			SqlTypeWorkarounds.SqlMoneyCaster sqlMoneyCaster = default(SqlTypeWorkarounds.SqlMoneyCaster);
			sqlMoneyCaster.Fake._fNotNull = true;
			sqlMoneyCaster.Fake._value = value;
			return sqlMoneyCaster.Real;
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x0009E4B4 File Offset: 0x0009C6B4
		internal static long SqlMoneyToSqlInternalRepresentation(SqlMoney money)
		{
			SqlTypeWorkarounds.SqlMoneyCaster sqlMoneyCaster = default(SqlTypeWorkarounds.SqlMoneyCaster);
			sqlMoneyCaster.Real = money;
			if (money.IsNull)
			{
				throw new SqlNullValueException();
			}
			return sqlMoneyCaster.Fake._value;
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x0009E4EC File Offset: 0x0009C6EC
		internal static void SqlDecimalExtractData(SqlDecimal d, out uint data1, out uint data2, out uint data3, out uint data4)
		{
			SqlTypeWorkarounds.SqlDecimalCaster sqlDecimalCaster = new SqlTypeWorkarounds.SqlDecimalCaster
			{
				Real = d
			};
			data1 = sqlDecimalCaster.Fake._data1;
			data2 = sqlDecimalCaster.Fake._data2;
			data3 = sqlDecimalCaster.Fake._data3;
			data4 = sqlDecimalCaster.Fake._data4;
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x0009E540 File Offset: 0x0009C740
		internal static SqlBinary SqlBinaryCtor(byte[] value, bool ignored)
		{
			SqlTypeWorkarounds.SqlBinaryCaster sqlBinaryCaster = default(SqlTypeWorkarounds.SqlBinaryCaster);
			sqlBinaryCaster.Fake._value = value;
			return sqlBinaryCaster.Real;
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x0009E568 File Offset: 0x0009C768
		internal static SqlGuid SqlGuidCtor(byte[] value, bool ignored)
		{
			SqlTypeWorkarounds.SqlGuidCaster sqlGuidCaster = default(SqlTypeWorkarounds.SqlGuidCaster);
			sqlGuidCaster.Fake._value = value;
			return sqlGuidCaster.Real;
		}

		// Token: 0x040016F9 RID: 5881
		private static readonly XmlReaderSettings s_defaultXmlReaderSettings = new XmlReaderSettings
		{
			ConformanceLevel = ConformanceLevel.Fragment
		};

		// Token: 0x040016FA RID: 5882
		private static readonly XmlReaderSettings s_defaultXmlReaderSettingsCloseInput = new XmlReaderSettings
		{
			ConformanceLevel = ConformanceLevel.Fragment,
			CloseInput = true
		};

		// Token: 0x040016FB RID: 5883
		private static readonly XmlReaderSettings s_defaultXmlReaderSettingsAsyncCloseInput = new XmlReaderSettings
		{
			Async = true,
			ConformanceLevel = ConformanceLevel.Fragment,
			CloseInput = true
		};

		// Token: 0x040016FC RID: 5884
		internal const SqlCompareOptions SqlStringValidSqlCompareOptionMask = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreNonSpace | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth | SqlCompareOptions.BinarySort | SqlCompareOptions.BinarySort2;

		// Token: 0x020002D2 RID: 722
		private struct SqlMoneyLookalike
		{
			// Token: 0x040016FD RID: 5885
			internal bool _fNotNull;

			// Token: 0x040016FE RID: 5886
			internal long _value;
		}

		// Token: 0x020002D3 RID: 723
		[StructLayout(LayoutKind.Explicit)]
		private struct SqlMoneyCaster
		{
			// Token: 0x040016FF RID: 5887
			[FieldOffset(0)]
			internal SqlMoney Real;

			// Token: 0x04001700 RID: 5888
			[FieldOffset(0)]
			internal SqlTypeWorkarounds.SqlMoneyLookalike Fake;
		}

		// Token: 0x020002D4 RID: 724
		private struct SqlDecimalLookalike
		{
			// Token: 0x04001701 RID: 5889
			internal byte _bStatus;

			// Token: 0x04001702 RID: 5890
			internal byte _bLen;

			// Token: 0x04001703 RID: 5891
			internal byte _bPrec;

			// Token: 0x04001704 RID: 5892
			internal byte _bScale;

			// Token: 0x04001705 RID: 5893
			internal uint _data1;

			// Token: 0x04001706 RID: 5894
			internal uint _data2;

			// Token: 0x04001707 RID: 5895
			internal uint _data3;

			// Token: 0x04001708 RID: 5896
			internal uint _data4;
		}

		// Token: 0x020002D5 RID: 725
		[StructLayout(LayoutKind.Explicit)]
		private struct SqlDecimalCaster
		{
			// Token: 0x04001709 RID: 5897
			[FieldOffset(0)]
			internal SqlDecimal Real;

			// Token: 0x0400170A RID: 5898
			[FieldOffset(0)]
			internal SqlTypeWorkarounds.SqlDecimalLookalike Fake;
		}

		// Token: 0x020002D6 RID: 726
		private struct SqlBinaryLookalike
		{
			// Token: 0x0400170B RID: 5899
			internal byte[] _value;
		}

		// Token: 0x020002D7 RID: 727
		[StructLayout(LayoutKind.Explicit)]
		private struct SqlBinaryCaster
		{
			// Token: 0x0400170C RID: 5900
			[FieldOffset(0)]
			internal SqlBinary Real;

			// Token: 0x0400170D RID: 5901
			[FieldOffset(0)]
			internal SqlTypeWorkarounds.SqlBinaryLookalike Fake;
		}

		// Token: 0x020002D8 RID: 728
		private struct SqlGuidLookalike
		{
			// Token: 0x0400170E RID: 5902
			internal byte[] _value;
		}

		// Token: 0x020002D9 RID: 729
		[StructLayout(LayoutKind.Explicit)]
		private struct SqlGuidCaster
		{
			// Token: 0x0400170F RID: 5903
			[FieldOffset(0)]
			internal SqlGuid Real;

			// Token: 0x04001710 RID: 5904
			[FieldOffset(0)]
			internal SqlTypeWorkarounds.SqlGuidLookalike Fake;
		}
	}
}
