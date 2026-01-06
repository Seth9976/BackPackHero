using System;
using System.Data.Common;

namespace System.Data.ProviderBase
{
	// Token: 0x02000304 RID: 772
	internal abstract class DataReaderContainer
	{
		// Token: 0x06002309 RID: 8969 RVA: 0x000A1068 File Offset: 0x0009F268
		internal static DataReaderContainer Create(IDataReader dataReader, bool returnProviderSpecificTypes)
		{
			if (returnProviderSpecificTypes)
			{
				DbDataReader dbDataReader = dataReader as DbDataReader;
				if (dbDataReader != null)
				{
					return new DataReaderContainer.ProviderSpecificDataReader(dataReader, dbDataReader);
				}
			}
			return new DataReaderContainer.CommonLanguageSubsetDataReader(dataReader);
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x000A1090 File Offset: 0x0009F290
		protected DataReaderContainer(IDataReader dataReader)
		{
			this._dataReader = dataReader;
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x0600230B RID: 8971 RVA: 0x000A109F File Offset: 0x0009F29F
		internal int FieldCount
		{
			get
			{
				return this._fieldCount;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x0600230C RID: 8972
		internal abstract bool ReturnProviderSpecificTypes { get; }

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x0600230D RID: 8973
		protected abstract int VisibleFieldCount { get; }

		// Token: 0x0600230E RID: 8974
		internal abstract Type GetFieldType(int ordinal);

		// Token: 0x0600230F RID: 8975
		internal abstract object GetValue(int ordinal);

		// Token: 0x06002310 RID: 8976
		internal abstract int GetValues(object[] values);

		// Token: 0x06002311 RID: 8977 RVA: 0x000A10A8 File Offset: 0x0009F2A8
		internal string GetName(int ordinal)
		{
			string name = this._dataReader.GetName(ordinal);
			if (name == null)
			{
				return "";
			}
			return name;
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000A10CC File Offset: 0x0009F2CC
		internal DataTable GetSchemaTable()
		{
			return this._dataReader.GetSchemaTable();
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x000A10D9 File Offset: 0x0009F2D9
		internal bool NextResult()
		{
			this._fieldCount = 0;
			if (this._dataReader.NextResult())
			{
				this._fieldCount = this.VisibleFieldCount;
				return true;
			}
			return false;
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x000A10FE File Offset: 0x0009F2FE
		internal bool Read()
		{
			return this._dataReader.Read();
		}

		// Token: 0x0400176A RID: 5994
		protected readonly IDataReader _dataReader;

		// Token: 0x0400176B RID: 5995
		protected int _fieldCount;

		// Token: 0x02000305 RID: 773
		private sealed class ProviderSpecificDataReader : DataReaderContainer
		{
			// Token: 0x06002315 RID: 8981 RVA: 0x000A110B File Offset: 0x0009F30B
			internal ProviderSpecificDataReader(IDataReader dataReader, DbDataReader dbDataReader)
				: base(dataReader)
			{
				this._providerSpecificDataReader = dbDataReader;
				this._fieldCount = this.VisibleFieldCount;
			}

			// Token: 0x17000601 RID: 1537
			// (get) Token: 0x06002316 RID: 8982 RVA: 0x0000CD07 File Offset: 0x0000AF07
			internal override bool ReturnProviderSpecificTypes
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000602 RID: 1538
			// (get) Token: 0x06002317 RID: 8983 RVA: 0x000A1128 File Offset: 0x0009F328
			protected override int VisibleFieldCount
			{
				get
				{
					int visibleFieldCount = this._providerSpecificDataReader.VisibleFieldCount;
					if (0 > visibleFieldCount)
					{
						return 0;
					}
					return visibleFieldCount;
				}
			}

			// Token: 0x06002318 RID: 8984 RVA: 0x000A1148 File Offset: 0x0009F348
			internal override Type GetFieldType(int ordinal)
			{
				return this._providerSpecificDataReader.GetProviderSpecificFieldType(ordinal);
			}

			// Token: 0x06002319 RID: 8985 RVA: 0x000A1156 File Offset: 0x0009F356
			internal override object GetValue(int ordinal)
			{
				return this._providerSpecificDataReader.GetProviderSpecificValue(ordinal);
			}

			// Token: 0x0600231A RID: 8986 RVA: 0x000A1164 File Offset: 0x0009F364
			internal override int GetValues(object[] values)
			{
				return this._providerSpecificDataReader.GetProviderSpecificValues(values);
			}

			// Token: 0x0400176C RID: 5996
			private DbDataReader _providerSpecificDataReader;
		}

		// Token: 0x02000306 RID: 774
		private sealed class CommonLanguageSubsetDataReader : DataReaderContainer
		{
			// Token: 0x0600231B RID: 8987 RVA: 0x000A1172 File Offset: 0x0009F372
			internal CommonLanguageSubsetDataReader(IDataReader dataReader)
				: base(dataReader)
			{
				this._fieldCount = this.VisibleFieldCount;
			}

			// Token: 0x17000603 RID: 1539
			// (get) Token: 0x0600231C RID: 8988 RVA: 0x00005AE9 File Offset: 0x00003CE9
			internal override bool ReturnProviderSpecificTypes
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000604 RID: 1540
			// (get) Token: 0x0600231D RID: 8989 RVA: 0x000A1188 File Offset: 0x0009F388
			protected override int VisibleFieldCount
			{
				get
				{
					int fieldCount = this._dataReader.FieldCount;
					if (0 > fieldCount)
					{
						return 0;
					}
					return fieldCount;
				}
			}

			// Token: 0x0600231E RID: 8990 RVA: 0x000A11A8 File Offset: 0x0009F3A8
			internal override Type GetFieldType(int ordinal)
			{
				return this._dataReader.GetFieldType(ordinal);
			}

			// Token: 0x0600231F RID: 8991 RVA: 0x000A11B6 File Offset: 0x0009F3B6
			internal override object GetValue(int ordinal)
			{
				return this._dataReader.GetValue(ordinal);
			}

			// Token: 0x06002320 RID: 8992 RVA: 0x000A11C4 File Offset: 0x0009F3C4
			internal override int GetValues(object[] values)
			{
				return this._dataReader.GetValues(values);
			}
		}
	}
}
