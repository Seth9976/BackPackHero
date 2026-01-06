using System;
using System.Data.Common;
using System.Globalization;
using System.IO;

namespace System.Data.ProviderBase
{
	// Token: 0x02000300 RID: 768
	internal class DbMetaDataFactory
	{
		// Token: 0x060022E1 RID: 8929 RVA: 0x000A014C File Offset: 0x0009E34C
		public DbMetaDataFactory(Stream xmlStream, string serverVersion, string normalizedServerVersion)
		{
			ADP.CheckArgumentNull(xmlStream, "xmlStream");
			ADP.CheckArgumentNull(serverVersion, "serverVersion");
			ADP.CheckArgumentNull(normalizedServerVersion, "normalizedServerVersion");
			this.LoadDataSetFromXml(xmlStream);
			this._serverVersionString = serverVersion;
			this._normalizedServerVersion = normalizedServerVersion;
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060022E2 RID: 8930 RVA: 0x000A018A File Offset: 0x0009E38A
		protected DataSet CollectionDataSet
		{
			get
			{
				return this._metaDataCollectionsDataSet;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x000A0192 File Offset: 0x0009E392
		protected string ServerVersion
		{
			get
			{
				return this._serverVersionString;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060022E4 RID: 8932 RVA: 0x000A019A File Offset: 0x0009E39A
		protected string ServerVersionNormalized
		{
			get
			{
				return this._normalizedServerVersion;
			}
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x000A01A4 File Offset: 0x0009E3A4
		protected DataTable CloneAndFilterCollection(string collectionName, string[] hiddenColumnNames)
		{
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[collectionName];
			if (dataTable == null || collectionName != dataTable.TableName)
			{
				throw ADP.DataTableDoesNotExist(collectionName);
			}
			DataTable dataTable2 = new DataTable(collectionName)
			{
				Locale = CultureInfo.InvariantCulture
			};
			DataColumnCollection columns = dataTable2.Columns;
			DataColumn[] array = this.FilterColumns(dataTable, hiddenColumnNames, columns);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (this.SupportedByCurrentVersion(dataRow))
				{
					DataRow dataRow2 = dataTable2.NewRow();
					for (int i = 0; i < columns.Count; i++)
					{
						dataRow2[columns[i]] = dataRow[array[i], DataRowVersion.Current];
					}
					dataTable2.Rows.Add(dataRow2);
					dataRow2.AcceptChanges();
				}
			}
			return dataTable2;
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x000A02A8 File Offset: 0x0009E4A8
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x000A02B1 File Offset: 0x0009E4B1
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._normalizedServerVersion = null;
				this._serverVersionString = null;
				this._metaDataCollectionsDataSet.Dispose();
			}
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x000A02D0 File Offset: 0x0009E4D0
		private DataTable ExecuteCommand(DataRow requestedCollectionRow, string[] restrictions, DbConnection connection)
		{
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[DbMetaDataCollectionNames.MetaDataCollections];
			DataColumn dataColumn = dataTable.Columns["PopulationString"];
			DataColumn dataColumn2 = dataTable.Columns["NumberOfRestrictions"];
			DataColumn dataColumn3 = dataTable.Columns["CollectionName"];
			DataTable dataTable2 = null;
			string text = requestedCollectionRow[dataColumn, DataRowVersion.Current] as string;
			int num = (int)requestedCollectionRow[dataColumn2, DataRowVersion.Current];
			string text2 = requestedCollectionRow[dataColumn3, DataRowVersion.Current] as string;
			if (restrictions != null && restrictions.Length > num)
			{
				throw ADP.TooManyRestrictions(text2);
			}
			DbCommand dbCommand = connection.CreateCommand();
			dbCommand.CommandText = text;
			dbCommand.CommandTimeout = Math.Max(dbCommand.CommandTimeout, 180);
			for (int i = 0; i < num; i++)
			{
				DbParameter dbParameter = dbCommand.CreateParameter();
				if (restrictions != null && restrictions.Length > i && restrictions[i] != null)
				{
					dbParameter.Value = restrictions[i];
				}
				else
				{
					dbParameter.Value = DBNull.Value;
				}
				dbParameter.ParameterName = this.GetParameterName(text2, i + 1);
				dbParameter.Direction = ParameterDirection.Input;
				dbCommand.Parameters.Add(dbParameter);
			}
			DbDataReader dbDataReader = null;
			try
			{
				try
				{
					dbDataReader = dbCommand.ExecuteReader();
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableExceptionType(ex))
					{
						throw;
					}
					throw ADP.QueryFailed(text2, ex);
				}
				dataTable2 = new DataTable(text2)
				{
					Locale = CultureInfo.InvariantCulture
				};
				foreach (object obj in dbDataReader.GetSchemaTable().Rows)
				{
					DataRow dataRow = (DataRow)obj;
					dataTable2.Columns.Add(dataRow["ColumnName"] as string, (Type)dataRow["DataType"]);
				}
				object[] array = new object[dataTable2.Columns.Count];
				while (dbDataReader.Read())
				{
					dbDataReader.GetValues(array);
					dataTable2.Rows.Add(array);
				}
			}
			finally
			{
				if (dbDataReader != null)
				{
					dbDataReader.Dispose();
					dbDataReader = null;
				}
			}
			return dataTable2;
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x000A0524 File Offset: 0x0009E724
		private DataColumn[] FilterColumns(DataTable sourceTable, string[] hiddenColumnNames, DataColumnCollection destinationColumns)
		{
			int num = 0;
			foreach (object obj in sourceTable.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (this.IncludeThisColumn(dataColumn, hiddenColumnNames))
				{
					num++;
				}
			}
			if (num == 0)
			{
				throw ADP.NoColumns();
			}
			int num2 = 0;
			DataColumn[] array = new DataColumn[num];
			foreach (object obj2 in sourceTable.Columns)
			{
				DataColumn dataColumn2 = (DataColumn)obj2;
				if (this.IncludeThisColumn(dataColumn2, hiddenColumnNames))
				{
					DataColumn dataColumn3 = new DataColumn(dataColumn2.ColumnName, dataColumn2.DataType);
					destinationColumns.Add(dataColumn3);
					array[num2] = dataColumn2;
					num2++;
				}
			}
			return array;
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x000A0614 File Offset: 0x0009E814
		internal DataRow FindMetaDataCollectionRow(string collectionName)
		{
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[DbMetaDataCollectionNames.MetaDataCollections];
			if (dataTable == null)
			{
				throw ADP.InvalidXml();
			}
			DataColumn dataColumn = dataTable.Columns[DbMetaDataColumnNames.CollectionName];
			if (dataColumn == null || typeof(string) != dataColumn.DataType)
			{
				throw ADP.InvalidXmlMissingColumn(DbMetaDataCollectionNames.MetaDataCollections, DbMetaDataColumnNames.CollectionName);
			}
			DataRow dataRow = null;
			string text = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow2 = (DataRow)obj;
				string text2 = dataRow2[dataColumn, DataRowVersion.Current] as string;
				if (string.IsNullOrEmpty(text2))
				{
					throw ADP.InvalidXmlInvalidValue(DbMetaDataCollectionNames.MetaDataCollections, DbMetaDataColumnNames.CollectionName);
				}
				if (ADP.CompareInsensitiveInvariant(text2, collectionName))
				{
					if (!this.SupportedByCurrentVersion(dataRow2))
					{
						flag = true;
					}
					else if (collectionName == text2)
					{
						if (flag2)
						{
							throw ADP.CollectionNameIsNotUnique(collectionName);
						}
						dataRow = dataRow2;
						text = text2;
						flag2 = true;
					}
					else
					{
						if (text != null)
						{
							flag3 = true;
						}
						dataRow = dataRow2;
						text = text2;
					}
				}
			}
			if (dataRow == null)
			{
				if (!flag)
				{
					throw ADP.UndefinedCollection(collectionName);
				}
				throw ADP.UnsupportedVersion(collectionName);
			}
			else
			{
				if (!flag2 && flag3)
				{
					throw ADP.AmbigousCollectionName(collectionName);
				}
				return dataRow;
			}
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x000A076C File Offset: 0x0009E96C
		private void FixUpVersion(DataTable dataSourceInfoTable)
		{
			DataColumn dataColumn = dataSourceInfoTable.Columns["DataSourceProductVersion"];
			DataColumn dataColumn2 = dataSourceInfoTable.Columns["DataSourceProductVersionNormalized"];
			if (dataColumn == null || dataColumn2 == null)
			{
				throw ADP.MissingDataSourceInformationColumn();
			}
			if (dataSourceInfoTable.Rows.Count != 1)
			{
				throw ADP.IncorrectNumberOfDataSourceInformationRows();
			}
			DataRow dataRow = dataSourceInfoTable.Rows[0];
			dataRow[dataColumn] = this._serverVersionString;
			dataRow[dataColumn2] = this._normalizedServerVersion;
			dataRow.AcceptChanges();
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x000A07E8 File Offset: 0x0009E9E8
		private string GetParameterName(string neededCollectionName, int neededRestrictionNumber)
		{
			DataColumn dataColumn = null;
			DataColumn dataColumn2 = null;
			DataColumn dataColumn3 = null;
			DataColumn dataColumn4 = null;
			string text = null;
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[DbMetaDataCollectionNames.Restrictions];
			if (dataTable != null)
			{
				DataColumnCollection columns = dataTable.Columns;
				if (columns != null)
				{
					dataColumn = columns["CollectionName"];
					dataColumn2 = columns["ParameterName"];
					dataColumn3 = columns["RestrictionName"];
					dataColumn4 = columns["RestrictionNumber"];
				}
			}
			if (dataColumn2 == null || dataColumn == null || dataColumn3 == null || dataColumn4 == null)
			{
				throw ADP.MissingRestrictionColumn();
			}
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if ((string)dataRow[dataColumn] == neededCollectionName && (int)dataRow[dataColumn4] == neededRestrictionNumber && this.SupportedByCurrentVersion(dataRow))
				{
					text = (string)dataRow[dataColumn2];
					break;
				}
			}
			if (text == null)
			{
				throw ADP.MissingRestrictionRow();
			}
			return text;
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x000A090C File Offset: 0x0009EB0C
		public virtual DataTable GetSchema(DbConnection connection, string collectionName, string[] restrictions)
		{
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[DbMetaDataCollectionNames.MetaDataCollections];
			DataColumn dataColumn = dataTable.Columns["PopulationMechanism"];
			DataColumn dataColumn2 = dataTable.Columns[DbMetaDataColumnNames.CollectionName];
			DataRow dataRow = this.FindMetaDataCollectionRow(collectionName);
			string text = dataRow[dataColumn2, DataRowVersion.Current] as string;
			if (!ADP.IsEmptyArray(restrictions))
			{
				for (int i = 0; i < restrictions.Length; i++)
				{
					if (restrictions[i] != null && restrictions[i].Length > 4096)
					{
						throw ADP.NotSupported();
					}
				}
			}
			string text2 = dataRow[dataColumn, DataRowVersion.Current] as string;
			DataTable dataTable2;
			if (!(text2 == "DataTable"))
			{
				if (!(text2 == "SQLCommand"))
				{
					if (!(text2 == "PrepareCollection"))
					{
						throw ADP.UndefinedPopulationMechanism(text2);
					}
					dataTable2 = this.PrepareCollection(text, restrictions, connection);
				}
				else
				{
					dataTable2 = this.ExecuteCommand(dataRow, restrictions, connection);
				}
			}
			else
			{
				string[] array;
				if (text == DbMetaDataCollectionNames.MetaDataCollections)
				{
					array = new string[] { "PopulationMechanism", "PopulationString" };
				}
				else
				{
					array = null;
				}
				if (!ADP.IsEmptyArray(restrictions))
				{
					throw ADP.TooManyRestrictions(text);
				}
				dataTable2 = this.CloneAndFilterCollection(text, array);
				if (text == DbMetaDataCollectionNames.DataSourceInformation)
				{
					this.FixUpVersion(dataTable2);
				}
			}
			return dataTable2;
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x000A0A68 File Offset: 0x0009EC68
		private bool IncludeThisColumn(DataColumn sourceColumn, string[] hiddenColumnNames)
		{
			bool flag = true;
			string columnName = sourceColumn.ColumnName;
			if (columnName == "MinimumVersion" || columnName == "MaximumVersion")
			{
				flag = false;
			}
			else if (hiddenColumnNames != null)
			{
				for (int i = 0; i < hiddenColumnNames.Length; i++)
				{
					if (hiddenColumnNames[i] == columnName)
					{
						flag = false;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x000A0ABD File Offset: 0x0009ECBD
		private void LoadDataSetFromXml(Stream XmlStream)
		{
			this._metaDataCollectionsDataSet = new DataSet();
			this._metaDataCollectionsDataSet.Locale = CultureInfo.InvariantCulture;
			this._metaDataCollectionsDataSet.ReadXml(XmlStream);
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x00060F32 File Offset: 0x0005F132
		protected virtual DataTable PrepareCollection(string collectionName, string[] restrictions, DbConnection connection)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000A0AE8 File Offset: 0x0009ECE8
		private bool SupportedByCurrentVersion(DataRow requestedCollectionRow)
		{
			bool flag = true;
			DataColumnCollection columns = requestedCollectionRow.Table.Columns;
			DataColumn dataColumn = columns["MinimumVersion"];
			if (dataColumn != null)
			{
				object obj = requestedCollectionRow[dataColumn];
				if (obj != null && obj != DBNull.Value && 0 > string.Compare(this._normalizedServerVersion, (string)obj, StringComparison.OrdinalIgnoreCase))
				{
					flag = false;
				}
			}
			if (flag)
			{
				dataColumn = columns["MaximumVersion"];
				if (dataColumn != null)
				{
					object obj = requestedCollectionRow[dataColumn];
					if (obj != null && obj != DBNull.Value && 0 < string.Compare(this._normalizedServerVersion, (string)obj, StringComparison.OrdinalIgnoreCase))
					{
						flag = false;
					}
				}
			}
			return flag;
		}

		// Token: 0x0400174C RID: 5964
		private DataSet _metaDataCollectionsDataSet;

		// Token: 0x0400174D RID: 5965
		private string _normalizedServerVersion;

		// Token: 0x0400174E RID: 5966
		private string _serverVersionString;

		// Token: 0x0400174F RID: 5967
		private const string _collectionName = "CollectionName";

		// Token: 0x04001750 RID: 5968
		private const string _populationMechanism = "PopulationMechanism";

		// Token: 0x04001751 RID: 5969
		private const string _populationString = "PopulationString";

		// Token: 0x04001752 RID: 5970
		private const string _maximumVersion = "MaximumVersion";

		// Token: 0x04001753 RID: 5971
		private const string _minimumVersion = "MinimumVersion";

		// Token: 0x04001754 RID: 5972
		private const string _dataSourceProductVersionNormalized = "DataSourceProductVersionNormalized";

		// Token: 0x04001755 RID: 5973
		private const string _dataSourceProductVersion = "DataSourceProductVersion";

		// Token: 0x04001756 RID: 5974
		private const string _restrictionDefault = "RestrictionDefault";

		// Token: 0x04001757 RID: 5975
		private const string _restrictionNumber = "RestrictionNumber";

		// Token: 0x04001758 RID: 5976
		private const string _numberOfRestrictions = "NumberOfRestrictions";

		// Token: 0x04001759 RID: 5977
		private const string _restrictionName = "RestrictionName";

		// Token: 0x0400175A RID: 5978
		private const string _parameterName = "ParameterName";

		// Token: 0x0400175B RID: 5979
		private const string _dataTable = "DataTable";

		// Token: 0x0400175C RID: 5980
		private const string _sqlCommand = "SQLCommand";

		// Token: 0x0400175D RID: 5981
		private const string _prepareCollection = "PrepareCollection";
	}
}
