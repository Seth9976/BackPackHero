using System;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace System.Data.SqlClient
{
	// Token: 0x020001E7 RID: 487
	internal static class SQL
	{
		// Token: 0x0600171E RID: 5918 RVA: 0x00070D0B File Offset: 0x0006EF0B
		internal static Exception CannotGetDTCAddress()
		{
			return ADP.InvalidOperation(SR.GetString("Unable to get the address of the distributed transaction coordinator for the server, from the server.  Is DTC enabled on the server?"));
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x00070D1C File Offset: 0x0006EF1C
		internal static Exception InvalidInternalPacketSize(string str)
		{
			return ADP.ArgumentOutOfRange(str);
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x00070D24 File Offset: 0x0006EF24
		internal static Exception InvalidPacketSize()
		{
			return ADP.ArgumentOutOfRange(SR.GetString("Invalid Packet Size."));
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x00070D35 File Offset: 0x0006EF35
		internal static Exception InvalidPacketSizeValue()
		{
			return ADP.Argument(SR.GetString("Invalid 'Packet Size'.  The value must be an integer >= 512 and <= 32768."));
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00070D46 File Offset: 0x0006EF46
		internal static Exception InvalidSSPIPacketSize()
		{
			return ADP.Argument(SR.GetString("Invalid SSPI packet size."));
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x00070D57 File Offset: 0x0006EF57
		internal static Exception NullEmptyTransactionName()
		{
			return ADP.Argument(SR.GetString("Invalid transaction or invalid name for a point at which to save within the transaction."));
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x00070D68 File Offset: 0x0006EF68
		internal static Exception UserInstanceFailoverNotCompatible()
		{
			return ADP.Argument(SR.GetString("User Instance and Failover are not compatible options.  Please choose only one of the two in the connection string."));
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x00070D7C File Offset: 0x0006EF7C
		internal static Exception ParsingErrorLibraryType(ParsingErrorState state, int libraryType)
		{
			string text = "Internal connection fatal error. Error state: {0}, Authentication Library Type: {1}.";
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = libraryType;
			return ADP.InvalidOperation(SR.GetString(text, array));
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x00070DB8 File Offset: 0x0006EFB8
		internal static Exception InvalidSQLServerVersionUnknown()
		{
			return ADP.DataAdapter(SR.GetString("Unsupported SQL Server version.  The .Net Framework SqlClient Data Provider can only be used with SQL Server versions 7.0 and later."));
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00070DC9 File Offset: 0x0006EFC9
		internal static Exception SynchronousCallMayNotPend()
		{
			return new Exception(SR.GetString("Internal Error"));
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x00070DDA File Offset: 0x0006EFDA
		internal static Exception ConnectionLockedForBcpEvent()
		{
			return ADP.InvalidOperation(SR.GetString("The connection cannot be used because there is an ongoing operation that must be finished."));
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x00070DEB File Offset: 0x0006EFEB
		internal static Exception InstanceFailure()
		{
			return ADP.InvalidOperation(SR.GetString("Instance failure."));
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x00070DFC File Offset: 0x0006EFFC
		internal static Exception ChangePasswordArgumentMissing(string argumentName)
		{
			return ADP.ArgumentNull(SR.GetString("The '{0}' argument must not be null or empty.", new object[] { argumentName }));
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00070E17 File Offset: 0x0006F017
		internal static Exception ChangePasswordConflictsWithSSPI()
		{
			return ADP.Argument(SR.GetString("ChangePassword can only be used with SQL authentication, not with integrated security."));
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00070E28 File Offset: 0x0006F028
		internal static Exception ChangePasswordRequiresYukon()
		{
			return ADP.InvalidOperation(SR.GetString("ChangePassword requires SQL Server 9.0 or later."));
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00070E39 File Offset: 0x0006F039
		internal static Exception ChangePasswordUseOfUnallowedKey(string key)
		{
			return ADP.InvalidOperation(SR.GetString("The keyword '{0}' must not be specified in the connectionString argument to ChangePassword.", new object[] { key }));
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00070E54 File Offset: 0x0006F054
		internal static Exception GlobalTransactionsNotEnabled()
		{
			return ADP.InvalidOperation(SR.GetString("Global Transactions are not enabled for this Azure SQL Database. Please contact Azure SQL Database support for assistance."));
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x00070E65 File Offset: 0x0006F065
		internal static Exception UnknownSysTxIsolationLevel(IsolationLevel isolationLevel)
		{
			return ADP.InvalidOperation(SR.GetString("Unrecognized System.Transactions.IsolationLevel enumeration value: {0}.", new object[] { isolationLevel.ToString() }));
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x00070E8C File Offset: 0x0006F08C
		internal static Exception InvalidPartnerConfiguration(string server, string database)
		{
			return ADP.InvalidOperation(SR.GetString("Server {0}, database {1} is not configured for database mirroring.", new object[] { server, database }));
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x00070EAB File Offset: 0x0006F0AB
		internal static Exception MARSUnspportedOnConnection()
		{
			return ADP.InvalidOperation(SR.GetString("The connection does not support MultipleActiveResultSets."));
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00070EBC File Offset: 0x0006F0BC
		internal static Exception CannotModifyPropertyAsyncOperationInProgress([CallerMemberName] string property = "")
		{
			return ADP.InvalidOperation(SR.GetString("{0} cannot be changed while async operation is in progress.", new object[] { property }));
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00070ED7 File Offset: 0x0006F0D7
		internal static Exception NonLocalSSEInstance()
		{
			return ADP.NotSupported(SR.GetString("SSE Instance re-direction is not supported for non-local user instances."));
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00070EE8 File Offset: 0x0006F0E8
		internal static ArgumentOutOfRangeException NotSupportedEnumerationValue(Type type, int value)
		{
			return ADP.ArgumentOutOfRange(SR.GetString("The {0} enumeration value, {1}, is not supported by the .Net Framework SqlClient Data Provider.", new object[]
			{
				type.Name,
				value.ToString(CultureInfo.InvariantCulture)
			}), type.Name);
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x00070F1D File Offset: 0x0006F11D
		internal static ArgumentOutOfRangeException NotSupportedCommandType(CommandType value)
		{
			return SQL.NotSupportedEnumerationValue(typeof(CommandType), (int)value);
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x00070F2F File Offset: 0x0006F12F
		internal static ArgumentOutOfRangeException NotSupportedIsolationLevel(IsolationLevel value)
		{
			return SQL.NotSupportedEnumerationValue(typeof(IsolationLevel), (int)value);
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x00070F41 File Offset: 0x0006F141
		internal static Exception OperationCancelled()
		{
			return ADP.InvalidOperation(SR.GetString("Operation cancelled by user."));
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x00070F52 File Offset: 0x0006F152
		internal static Exception PendingBeginXXXExists()
		{
			return ADP.InvalidOperation(SR.GetString("The command execution cannot proceed due to a pending asynchronous operation already in progress."));
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x00070F63 File Offset: 0x0006F163
		internal static ArgumentOutOfRangeException InvalidSqlDependencyTimeout(string param)
		{
			return ADP.ArgumentOutOfRange(SR.GetString("Timeout specified is invalid. Timeout cannot be < 0."), param);
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x00070F75 File Offset: 0x0006F175
		internal static Exception NonXmlResult()
		{
			return ADP.InvalidOperation(SR.GetString("Invalid command sent to ExecuteXmlReader.  The command must return an Xml result."));
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x00070F86 File Offset: 0x0006F186
		internal static Exception InvalidUdt3PartNameFormat()
		{
			return ADP.Argument(SR.GetString("Invalid 3 part name format for UdtTypeName."));
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00070F97 File Offset: 0x0006F197
		internal static Exception InvalidParameterTypeNameFormat()
		{
			return ADP.Argument(SR.GetString("Invalid 3 part name format for TypeName."));
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00070FA8 File Offset: 0x0006F1A8
		internal static Exception InvalidParameterNameLength(string value)
		{
			return ADP.Argument(SR.GetString("The length of the parameter '{0}' exceeds the limit of 128 characters.", new object[] { value }));
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x00070FC3 File Offset: 0x0006F1C3
		internal static Exception PrecisionValueOutOfRange(byte precision)
		{
			return ADP.Argument(SR.GetString("Precision value '{0}' is either less than 0 or greater than the maximum allowed precision of 38.", new object[] { precision.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00070FE9 File Offset: 0x0006F1E9
		internal static Exception ScaleValueOutOfRange(byte scale)
		{
			return ADP.Argument(SR.GetString("Scale value '{0}' is either less than 0 or greater than the maximum allowed scale of 38.", new object[] { scale.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x0007100F File Offset: 0x0006F20F
		internal static Exception TimeScaleValueOutOfRange(byte scale)
		{
			return ADP.Argument(SR.GetString("Scale value '{0}' is either less than 0 or greater than the maximum allowed scale of 7.", new object[] { scale.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00071035 File Offset: 0x0006F235
		internal static Exception InvalidSqlDbType(SqlDbType value)
		{
			return ADP.InvalidEnumerationValue(typeof(SqlDbType), (int)value);
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00071047 File Offset: 0x0006F247
		internal static Exception UnsupportedTVPOutputParameter(ParameterDirection direction, string paramName)
		{
			return ADP.NotSupported(SR.GetString("ParameterDirection '{0}' specified for parameter '{1}' is not supported. Table-valued parameters only support ParameterDirection.Input.", new object[]
			{
				direction.ToString(),
				paramName
			}));
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x00071072 File Offset: 0x0006F272
		internal static Exception DBNullNotSupportedForTVPValues(string paramName)
		{
			return ADP.NotSupported(SR.GetString("DBNull value for parameter '{0}' is not supported. Table-valued parameters cannot be DBNull.", new object[] { paramName }));
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x0007108D File Offset: 0x0006F28D
		internal static Exception UnexpectedTypeNameForNonStructParams(string paramName)
		{
			return ADP.NotSupported(SR.GetString("TypeName specified for parameter '{0}'.  TypeName must only be set for Structured parameters.", new object[] { paramName }));
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x000710A8 File Offset: 0x0006F2A8
		internal static Exception ParameterInvalidVariant(string paramName)
		{
			return ADP.InvalidOperation(SR.GetString("Parameter '{0}' exceeds the size limit for the sql_variant datatype.", new object[] { paramName }));
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x000710C3 File Offset: 0x0006F2C3
		internal static Exception MustSetTypeNameForParam(string paramType, string paramName)
		{
			return ADP.Argument(SR.GetString("The {0} type parameter '{1}' must have a valid type name.", new object[] { paramType, paramName }));
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x000710E2 File Offset: 0x0006F2E2
		internal static Exception NullSchemaTableDataTypeNotSupported(string columnName)
		{
			return ADP.Argument(SR.GetString("DateType column for field '{0}' in schema table is null.  DataType must be non-null.", new object[] { columnName }));
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x000710FD File Offset: 0x0006F2FD
		internal static Exception InvalidSchemaTableOrdinals()
		{
			return ADP.Argument(SR.GetString("Invalid column ordinals in schema table.  ColumnOrdinals, if present, must not have duplicates or gaps."));
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0007110E File Offset: 0x0006F30E
		internal static Exception EnumeratedRecordMetaDataChanged(string fieldName, int recordNumber)
		{
			return ADP.Argument(SR.GetString("Metadata for field '{0}' of record '{1}' did not match the original record's metadata.", new object[] { fieldName, recordNumber }));
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00071132 File Offset: 0x0006F332
		internal static Exception EnumeratedRecordFieldCountChanged(int recordNumber)
		{
			return ADP.Argument(SR.GetString("Number of fields in record '{0}' does not match the number in the original record.", new object[] { recordNumber }));
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x00071152 File Offset: 0x0006F352
		internal static Exception InvalidTDSVersion()
		{
			return ADP.InvalidOperation(SR.GetString("The SQL Server instance returned an invalid or unsupported protocol version during login negotiation."));
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00071163 File Offset: 0x0006F363
		internal static Exception ParsingError()
		{
			return ADP.InvalidOperation(SR.GetString("Internal connection fatal error."));
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00071174 File Offset: 0x0006F374
		internal static Exception ParsingError(ParsingErrorState state)
		{
			string text = "Internal connection fatal error. Error state: {0}.";
			object[] array = new object[1];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			return ADP.InvalidOperation(SR.GetString(text, array));
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x000711A8 File Offset: 0x0006F3A8
		internal static Exception ParsingErrorValue(ParsingErrorState state, int value)
		{
			string text = "Internal connection fatal error. Error state: {0}, Value: {1}.";
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = value;
			return ADP.InvalidOperation(SR.GetString(text, array));
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x000711E4 File Offset: 0x0006F3E4
		internal static Exception ParsingErrorFeatureId(ParsingErrorState state, int featureId)
		{
			string text = "Internal connection fatal error. Error state: {0}, Feature Id: {1}.";
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = featureId;
			return ADP.InvalidOperation(SR.GetString(text, array));
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x00071220 File Offset: 0x0006F420
		internal static Exception MoneyOverflow(string moneyValue)
		{
			return ADP.Overflow(SR.GetString("SqlDbType.SmallMoney overflow.  Value '{0}' is out of range.  Must be between -214,748.3648 and 214,748.3647.", new object[] { moneyValue }));
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0007123B File Offset: 0x0006F43B
		internal static Exception SmallDateTimeOverflow(string datetime)
		{
			return ADP.Overflow(SR.GetString("SqlDbType.SmallDateTime overflow.  Value '{0}' is out of range.  Must be between 1/1/1900 12:00:00 AM and 6/6/2079 11:59:59 PM.", new object[] { datetime }));
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x00071256 File Offset: 0x0006F456
		internal static Exception SNIPacketAllocationFailure()
		{
			return ADP.InvalidOperation(SR.GetString("Memory allocation for internal connection failed."));
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x00071267 File Offset: 0x0006F467
		internal static Exception TimeOverflow(string time)
		{
			return ADP.Overflow(SR.GetString("SqlDbType.Time overflow.  Value '{0}' is out of range.  Must be between 00:00:00.0000000 and 23:59:59.9999999.", new object[] { time }));
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x00071282 File Offset: 0x0006F482
		internal static Exception InvalidRead()
		{
			return ADP.InvalidOperation(SR.GetString("Invalid attempt to read when no data is present."));
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x00071293 File Offset: 0x0006F493
		internal static Exception NonBlobColumn(string columnName)
		{
			return ADP.InvalidCast(SR.GetString("Invalid attempt to GetBytes on column '{0}'.  The GetBytes function can only be used on columns of type Text, NText, or Image.", new object[] { columnName }));
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x000712AE File Offset: 0x0006F4AE
		internal static Exception NonCharColumn(string columnName)
		{
			return ADP.InvalidCast(SR.GetString("Invalid attempt to GetChars on column '{0}'.  The GetChars function can only be used on columns of type Text, NText, Xml, VarChar or NVarChar.", new object[] { columnName }));
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x000712C9 File Offset: 0x0006F4C9
		internal static Exception StreamNotSupportOnColumnType(string columnName)
		{
			return ADP.InvalidCast(SR.GetString("Invalid attempt to GetStream on column '{0}'. The GetStream function can only be used on columns of type Binary, Image, Udt or VarBinary.", new object[] { columnName }));
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x000712E4 File Offset: 0x0006F4E4
		internal static Exception TextReaderNotSupportOnColumnType(string columnName)
		{
			return ADP.InvalidCast(SR.GetString("Invalid attempt to GetTextReader on column '{0}'. The GetTextReader function can only be used on columns of type Char, NChar, NText, NVarChar, Text or VarChar.", new object[] { columnName }));
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x000712FF File Offset: 0x0006F4FF
		internal static Exception XmlReaderNotSupportOnColumnType(string columnName)
		{
			return ADP.InvalidCast(SR.GetString("Invalid attempt to GetXmlReader on column '{0}'. The GetXmlReader function can only be used on columns of type Xml.", new object[] { columnName }));
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0007131A File Offset: 0x0006F51A
		internal static Exception UDTUnexpectedResult(string exceptionText)
		{
			return ADP.TypeLoad(SR.GetString("unexpected error encountered in SqlClient data provider. {0}", new object[] { exceptionText }));
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x00071335 File Offset: 0x0006F535
		internal static Exception SqlCommandHasExistingSqlNotificationRequest()
		{
			return ADP.InvalidOperation(SR.GetString("This SqlCommand object is already associated with another SqlDependency object."));
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00071346 File Offset: 0x0006F546
		internal static Exception SqlDepDefaultOptionsButNoStart()
		{
			return ADP.InvalidOperation(SR.GetString("When using SqlDependency without providing an options value, SqlDependency.Start() must be called prior to execution of a command added to the SqlDependency instance."));
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x00071357 File Offset: 0x0006F557
		internal static Exception SqlDependencyDatabaseBrokerDisabled()
		{
			return ADP.InvalidOperation(SR.GetString("The SQL Server Service Broker for the current database is not enabled, and as a result query notifications are not supported.  Please enable the Service Broker for this database if you wish to use notifications."));
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x00071368 File Offset: 0x0006F568
		internal static Exception SqlDependencyEventNoDuplicate()
		{
			return ADP.InvalidOperation(SR.GetString("SqlDependency.OnChange does not support multiple event registrations for the same delegate."));
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x00071379 File Offset: 0x0006F579
		internal static Exception SqlDependencyDuplicateStart()
		{
			return ADP.InvalidOperation(SR.GetString("SqlDependency does not support calling Start() with different connection strings having the same server, user, and database in the same app domain."));
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0007138A File Offset: 0x0006F58A
		internal static Exception SqlDependencyIdMismatch()
		{
			return ADP.InvalidOperation(SR.GetString("No SqlDependency exists for the key."));
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0007139B File Offset: 0x0006F59B
		internal static Exception SqlDependencyNoMatchingServerStart()
		{
			return ADP.InvalidOperation(SR.GetString("When using SqlDependency without providing an options value, SqlDependency.Start() must be called for each server that is being executed against."));
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x000713AC File Offset: 0x0006F5AC
		internal static Exception SqlDependencyNoMatchingServerDatabaseStart()
		{
			return ADP.InvalidOperation(SR.GetString("SqlDependency.Start has been called for the server the command is executing against more than once, but there is no matching server/user/database Start() call for current command."));
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x000713BD File Offset: 0x0006F5BD
		internal static TransactionPromotionException PromotionFailed(Exception inner)
		{
			TransactionPromotionException ex = new TransactionPromotionException(SR.GetString("Failure while attempting to promote transaction."), inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x000713D5 File Offset: 0x0006F5D5
		internal static Exception UnexpectedUdtTypeNameForNonUdtParams()
		{
			return ADP.Argument(SR.GetString("UdtTypeName property must be set only for UDT parameters."));
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x000713E6 File Offset: 0x0006F5E6
		internal static Exception MustSetUdtTypeNameForUdtParams()
		{
			return ADP.Argument(SR.GetString("UdtTypeName property must be set for UDT parameters."));
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x000713F7 File Offset: 0x0006F5F7
		internal static Exception UDTInvalidSqlType(string typeName)
		{
			return ADP.Argument(SR.GetString("Specified type is not registered on the target server. {0}.", new object[] { typeName }));
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x00071412 File Offset: 0x0006F612
		internal static Exception InvalidSqlDbTypeForConstructor(SqlDbType type)
		{
			return ADP.Argument(SR.GetString("The dbType {0} is invalid for this constructor.", new object[] { type.ToString() }));
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00071439 File Offset: 0x0006F639
		internal static Exception NameTooLong(string parameterName)
		{
			return ADP.Argument(SR.GetString("The name is too long."), parameterName);
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0007144B File Offset: 0x0006F64B
		internal static Exception InvalidSortOrder(SortOrder order)
		{
			return ADP.InvalidEnumerationValue(typeof(SortOrder), (int)order);
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0007145D File Offset: 0x0006F65D
		internal static Exception MustSpecifyBothSortOrderAndOrdinal(SortOrder order, int ordinal)
		{
			return ADP.InvalidOperation(SR.GetString("The sort order and ordinal must either both be specified, or neither should be specified (SortOrder.Unspecified and -1).  The values given were: order = {0}, ordinal = {1}.", new object[]
			{
				order.ToString(),
				ordinal
			}));
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0007148D File Offset: 0x0006F68D
		internal static Exception UnsupportedColumnTypeForSqlProvider(string columnName, string typeName)
		{
			return ADP.Argument(SR.GetString("The type of column '{0}' is not supported.  The type is '{1}'", new object[] { columnName, typeName }));
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x000714AC File Offset: 0x0006F6AC
		internal static Exception InvalidColumnMaxLength(string columnName, long maxLength)
		{
			return ADP.Argument(SR.GetString("The size of column '{0}' is not supported. The size is {1}.", new object[] { columnName, maxLength }));
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x000714D0 File Offset: 0x0006F6D0
		internal static Exception InvalidColumnPrecScale()
		{
			return ADP.Argument(SR.GetString("Invalid numeric precision/scale."));
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x000714E1 File Offset: 0x0006F6E1
		internal static Exception NotEnoughColumnsInStructuredType()
		{
			return ADP.Argument(SR.GetString("There are not enough fields in the Structured type.  Structured types must have at least one field."));
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x000714F2 File Offset: 0x0006F6F2
		internal static Exception DuplicateSortOrdinal(int sortOrdinal)
		{
			return ADP.InvalidOperation(SR.GetString("The sort ordinal {0} was specified twice.", new object[] { sortOrdinal }));
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x00071512 File Offset: 0x0006F712
		internal static Exception MissingSortOrdinal(int sortOrdinal)
		{
			return ADP.InvalidOperation(SR.GetString("The sort ordinal {0} was not specified.", new object[] { sortOrdinal }));
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00071532 File Offset: 0x0006F732
		internal static Exception SortOrdinalGreaterThanFieldCount(int columnOrdinal, int sortOrdinal)
		{
			return ADP.InvalidOperation(SR.GetString("The sort ordinal {0} on field {1} exceeds the total number of fields.", new object[] { sortOrdinal, columnOrdinal }));
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0007155B File Offset: 0x0006F75B
		internal static Exception IEnumerableOfSqlDataRecordHasNoRows()
		{
			return ADP.Argument(SR.GetString("There are no records in the SqlDataRecord enumeration. To send a table-valued parameter with no rows, use a null reference for the value instead."));
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0007156C File Offset: 0x0006F76C
		internal static Exception BulkLoadMappingInaccessible()
		{
			return ADP.InvalidOperation(SR.GetString("The mapped collection is in use and cannot be accessed at this time;"));
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x0007157D File Offset: 0x0006F77D
		internal static Exception BulkLoadMappingsNamesOrOrdinalsOnly()
		{
			return ADP.InvalidOperation(SR.GetString("Mappings must be either all name or all ordinal based."));
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x0007158E File Offset: 0x0006F78E
		internal static Exception BulkLoadCannotConvertValue(Type sourcetype, MetaType metatype, Exception e)
		{
			return ADP.InvalidOperation(SR.GetString("The given value of type {0} from the data source cannot be converted to type {1} of the specified target column.", new object[] { sourcetype.Name, metatype.TypeName }), e);
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x000715B8 File Offset: 0x0006F7B8
		internal static Exception BulkLoadNonMatchingColumnMapping()
		{
			return ADP.InvalidOperation(SR.GetString("The given ColumnMapping does not match up with any column in the source or destination."));
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x000715C9 File Offset: 0x0006F7C9
		internal static Exception BulkLoadNonMatchingColumnName(string columnName)
		{
			return SQL.BulkLoadNonMatchingColumnName(columnName, null);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x000715D2 File Offset: 0x0006F7D2
		internal static Exception BulkLoadNonMatchingColumnName(string columnName, Exception e)
		{
			return ADP.InvalidOperation(SR.GetString("The given ColumnName '{0}' does not match up with any column in data source.", new object[] { columnName }), e);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x000715EE File Offset: 0x0006F7EE
		internal static Exception BulkLoadStringTooLong()
		{
			return ADP.InvalidOperation(SR.GetString("String or binary data would be truncated."));
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x000715FF File Offset: 0x0006F7FF
		internal static Exception BulkLoadInvalidVariantValue()
		{
			return ADP.InvalidOperation(SR.GetString("Value cannot be converted to SqlVariant."));
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00071610 File Offset: 0x0006F810
		internal static Exception BulkLoadInvalidTimeout(int timeout)
		{
			return ADP.Argument(SR.GetString("Timeout Value '{0}' is less than 0.", new object[] { timeout.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00071636 File Offset: 0x0006F836
		internal static Exception BulkLoadExistingTransaction()
		{
			return ADP.InvalidOperation(SR.GetString("Unexpected existing transaction."));
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00071647 File Offset: 0x0006F847
		internal static Exception BulkLoadNoCollation()
		{
			return ADP.InvalidOperation(SR.GetString("Failed to obtain column collation information for the destination table. If the table is not in the current database the name must be qualified using the database name (e.g. [mydb]..[mytable](e.g. [mydb]..[mytable]); this also applies to temporary-tables (e.g. #mytable would be specified as tempdb..#mytable)."));
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00071658 File Offset: 0x0006F858
		internal static Exception BulkLoadConflictingTransactionOption()
		{
			return ADP.Argument(SR.GetString("Must not specify SqlBulkCopyOption.UseInternalTransaction and pass an external Transaction at the same time."));
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00071669 File Offset: 0x0006F869
		internal static Exception BulkLoadLcidMismatch(int sourceLcid, string sourceColumnName, int destinationLcid, string destinationColumnName)
		{
			return ADP.InvalidOperation(SR.GetString("The locale id '{0}' of the source column '{1}' and the locale id '{2}' of the destination column '{3}' do not match.", new object[] { sourceLcid, sourceColumnName, destinationLcid, destinationColumnName }));
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x0007169A File Offset: 0x0006F89A
		internal static Exception InvalidOperationInsideEvent()
		{
			return ADP.InvalidOperation(SR.GetString("Function must not be called during event."));
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x000716AB File Offset: 0x0006F8AB
		internal static Exception BulkLoadMissingDestinationTable()
		{
			return ADP.InvalidOperation(SR.GetString("The DestinationTableName property must be set before calling this method."));
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x000716BC File Offset: 0x0006F8BC
		internal static Exception BulkLoadInvalidDestinationTable(string tableName, Exception inner)
		{
			return ADP.InvalidOperation(SR.GetString("Cannot access destination table '{0}'.", new object[] { tableName }), inner);
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x000716D8 File Offset: 0x0006F8D8
		internal static Exception BulkLoadBulkLoadNotAllowDBNull(string columnName)
		{
			return ADP.InvalidOperation(SR.GetString("Column '{0}' does not allow DBNull.Value.", new object[] { columnName }));
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x000716F3 File Offset: 0x0006F8F3
		internal static Exception BulkLoadPendingOperation()
		{
			return ADP.InvalidOperation(SR.GetString("Attempt to invoke bulk copy on an object that has a pending operation."));
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00071704 File Offset: 0x0006F904
		internal static Exception InvalidTableDerivedPrecisionForTvp(string columnName, byte precision)
		{
			return ADP.InvalidOperation(SR.GetString("Precision '{0}' required to send all values in column '{1}' exceeds the maximum supported precision '{2}'. The values must all fit in a single precision.", new object[]
			{
				precision,
				columnName,
				SqlDecimal.MaxPrecision
			}));
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x00071735 File Offset: 0x0006F935
		internal static Exception ConnectionDoomed()
		{
			return ADP.InvalidOperation(SR.GetString("The requested operation cannot be completed because the connection has been broken."));
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00071746 File Offset: 0x0006F946
		internal static Exception OpenResultCountExceeded()
		{
			return ADP.InvalidOperation(SR.GetString("Open result count exceeded."));
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00071757 File Offset: 0x0006F957
		internal static Exception UnsupportedSysTxForGlobalTransactions()
		{
			return ADP.InvalidOperation(SR.GetString("The currently loaded System.Transactions.dll does not support Global Transactions."));
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00071768 File Offset: 0x0006F968
		internal static Exception MultiSubnetFailoverWithFailoverPartner(bool serverProvidedFailoverPartner, SqlInternalConnectionTds internalConnection)
		{
			string @string = SR.GetString("Connecting to a mirrored SQL Server instance using the MultiSubnetFailover connection option is not supported.");
			if (serverProvidedFailoverPartner)
			{
				SqlException ex = SqlException.CreateException(new SqlErrorCollection
				{
					new SqlError(0, 0, 20, null, @string, "", 0, null)
				}, null, internalConnection, null);
				ex._doNotReconnect = true;
				return ex;
			}
			return ADP.Argument(@string);
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x000717B6 File Offset: 0x0006F9B6
		internal static Exception MultiSubnetFailoverWithMoreThan64IPs()
		{
			return ADP.InvalidOperation(SQL.GetSNIErrorMessage(47));
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x000717C4 File Offset: 0x0006F9C4
		internal static Exception MultiSubnetFailoverWithInstanceSpecified()
		{
			return ADP.Argument(SQL.GetSNIErrorMessage(48));
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x000717D2 File Offset: 0x0006F9D2
		internal static Exception MultiSubnetFailoverWithNonTcpProtocol()
		{
			return ADP.Argument(SQL.GetSNIErrorMessage(49));
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x000717E0 File Offset: 0x0006F9E0
		internal static Exception ROR_FailoverNotSupportedConnString()
		{
			return ADP.Argument(SR.GetString("Connecting to a mirrored SQL Server instance using the ApplicationIntent ReadOnly connection option is not supported."));
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x000717F4 File Offset: 0x0006F9F4
		internal static Exception ROR_FailoverNotSupportedServer(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("Connecting to a mirrored SQL Server instance using the ApplicationIntent ReadOnly connection option is not supported."), "", 0, null)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00071838 File Offset: 0x0006FA38
		internal static Exception ROR_RecursiveRoutingNotSupported(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("Two or more redirections have occurred. Only one redirection per login is allowed."), "", 0, null)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x0007187C File Offset: 0x0006FA7C
		internal static Exception ROR_UnexpectedRoutingInfo(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("Unexpected routing information received."), "", 0, null)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x000718C0 File Offset: 0x0006FAC0
		internal static Exception ROR_InvalidRoutingInfo(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("Invalid routing information received."), "", 0, null)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00071904 File Offset: 0x0006FB04
		internal static Exception ROR_TimeoutAfterRoutingInfo(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("Server provided routing information, but timeout already expired."), "", 0, null)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00071948 File Offset: 0x0006FB48
		internal static SqlException CR_ReconnectTimeout()
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(-2, 0, 11, null, SQLMessage.Timeout(), "", 0, 258U, null)
			}, "");
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00071988 File Offset: 0x0006FB88
		internal static SqlException CR_ReconnectionCancelled()
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 11, null, SQLMessage.OperationCancelled(), "", 0, null)
			}, "");
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x000719C0 File Offset: 0x0006FBC0
		internal static Exception CR_NextAttemptWillExceedQueryTimeout(SqlException innerException, Guid connectionId)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 11, null, SR.GetString("Next reconnection attempt will exceed query timeout. Reconnection was terminated."), "", 0, null)
			}, "", connectionId, innerException);
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00071A00 File Offset: 0x0006FC00
		internal static Exception CR_EncryptionChanged(SqlInternalConnectionTds internalConnection)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("The server did not preserve SSL encryption during a recovery attempt, connection recovery is not possible."), "", 0, null)
			}, "", internalConnection, null);
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00071A40 File Offset: 0x0006FC40
		internal static SqlException CR_AllAttemptsFailed(SqlException innerException, Guid connectionId)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 11, null, SR.GetString("The connection is broken and recovery is not possible.  The client driver attempted to recover the connection one or more times and all attempts failed.  Increase the value of ConnectRetryCount to increase the number of recovery attempts."), "", 0, null)
			}, "", connectionId, innerException);
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x00071A80 File Offset: 0x0006FC80
		internal static SqlException CR_NoCRAckAtReconnection(SqlInternalConnectionTds internalConnection)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("The server did not acknowledge a recovery attempt, connection recovery is not possible."), "", 0, null)
			}, "", internalConnection, null);
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x00071AC0 File Offset: 0x0006FCC0
		internal static SqlException CR_TDSVersionNotPreserved(SqlInternalConnectionTds internalConnection)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("The server did not preserve the exact client TDS version requested during a recovery attempt, connection recovery is not possible."), "", 0, null)
			}, "", internalConnection, null);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x00071B00 File Offset: 0x0006FD00
		internal static SqlException CR_UnrecoverableServer(Guid connectionId)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("The connection is broken and recovery is not possible.  The connection is marked by the server as unrecoverable.  No attempt was made to restore the connection."), "", 0, null)
			}, "", connectionId, null);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00071B40 File Offset: 0x0006FD40
		internal static SqlException CR_UnrecoverableClient(Guid connectionId)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("The connection is broken and recovery is not possible.  The connection is marked by the client driver as unrecoverable.  No attempt was made to restore the connection."), "", 0, null)
			}, "", connectionId, null);
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00071B7F File Offset: 0x0006FD7F
		internal static Exception StreamWriteNotSupported()
		{
			return ADP.NotSupported(SR.GetString("The Stream does not support writing."));
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x00071B90 File Offset: 0x0006FD90
		internal static Exception StreamReadNotSupported()
		{
			return ADP.NotSupported(SR.GetString("The Stream does not support reading."));
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00071BA1 File Offset: 0x0006FDA1
		internal static Exception StreamSeekNotSupported()
		{
			return ADP.NotSupported(SR.GetString("The Stream does not support seeking."));
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x00071BB2 File Offset: 0x0006FDB2
		internal static SqlNullValueException SqlNullValue()
		{
			return new SqlNullValueException();
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x00071BB9 File Offset: 0x0006FDB9
		internal static Exception SubclassMustOverride()
		{
			return ADP.InvalidOperation(SR.GetString("Subclass did not override a required method."));
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00071BCA File Offset: 0x0006FDCA
		internal static Exception UnsupportedKeyword(string keyword)
		{
			return ADP.NotSupported(SR.GetString("The keyword '{0}' is not supported on this platform.", new object[] { keyword }));
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00071BE5 File Offset: 0x0006FDE5
		internal static Exception NetworkLibraryKeywordNotSupported()
		{
			return ADP.NotSupported(SR.GetString("The keyword 'Network Library' is not supported on this platform, prefix the 'Data Source' with the protocol desired instead ('tcp:' for a TCP connection, or 'np:' for a Named Pipe connection)."));
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x00071BF8 File Offset: 0x0006FDF8
		internal static Exception UnsupportedFeatureAndToken(SqlInternalConnectionTds internalConnection, string token)
		{
			NotSupportedException ex = ADP.NotSupported(SR.GetString("Received an unsupported token '{0}' while reading data from the server.", new object[] { token }));
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("The server is attempting to use a feature that is not supported on this platform."), "", 0, null)
			}, "", internalConnection, ex);
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00071C51 File Offset: 0x0006FE51
		internal static Exception BatchedUpdatesNotAvailableOnContextConnection()
		{
			return ADP.InvalidOperation(SR.GetString("Batching updates is not supported on the context connection."));
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00071C62 File Offset: 0x0006FE62
		internal static string GetSNIErrorMessage(int sniError)
		{
			string text = string.Format(null, "SNI_ERROR_{0}", sniError);
			return SR.GetResourceString(text, text);
		}

		// Token: 0x04000F49 RID: 3913
		internal static readonly byte[] AttentionHeader = new byte[] { 6, 1, 0, 8, 0, 0, 0, 0 };

		// Token: 0x04000F4A RID: 3914
		internal const int SqlDependencyTimeoutDefault = 0;

		// Token: 0x04000F4B RID: 3915
		internal const int SqlDependencyServerTimeout = 432000;

		// Token: 0x04000F4C RID: 3916
		internal const string SqlNotificationServiceDefault = "SqlQueryNotificationService";

		// Token: 0x04000F4D RID: 3917
		internal const string SqlNotificationStoredProcedureDefault = "SqlQueryNotificationStoredProcedure";
	}
}
