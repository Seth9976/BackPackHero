using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	// Token: 0x0200005D RID: 93
	internal static class ExceptionBuilder
	{
		// Token: 0x060003F5 RID: 1013 RVA: 0x0001245E File Offset: 0x0001065E
		private static void TraceException(string trace, Exception e)
		{
			if (e != null)
			{
				DataCommonEventSource.Log.Trace<Exception>(trace, e);
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001246F File Offset: 0x0001066F
		internal static Exception TraceExceptionAsReturnValue(Exception e)
		{
			ExceptionBuilder.TraceException("<comm.ADP.TraceException|ERR|THROW> '{0}'", e);
			return e;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001247D File Offset: 0x0001067D
		internal static Exception TraceExceptionForCapture(Exception e)
		{
			ExceptionBuilder.TraceException("<comm.ADP.TraceException|ERR|CATCH> '{0}'", e);
			return e;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001247D File Offset: 0x0001067D
		internal static Exception TraceExceptionWithoutRethrow(Exception e)
		{
			ExceptionBuilder.TraceException("<comm.ADP.TraceException|ERR|CATCH> '{0}'", e);
			return e;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001248B File Offset: 0x0001068B
		internal static Exception _Argument(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new ArgumentException(error));
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00012498 File Offset: 0x00010698
		internal static Exception _Argument(string paramName, string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new ArgumentException(error));
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000124A5 File Offset: 0x000106A5
		internal static Exception _Argument(string error, Exception innerException)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new ArgumentException(error, innerException));
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000124B3 File Offset: 0x000106B3
		private static Exception _ArgumentNull(string paramName, string msg)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new ArgumentNullException(paramName, msg));
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000124C1 File Offset: 0x000106C1
		internal static Exception _ArgumentOutOfRange(string paramName, string msg)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new ArgumentOutOfRangeException(paramName, msg));
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000124CF File Offset: 0x000106CF
		private static Exception _IndexOutOfRange(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new IndexOutOfRangeException(error));
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000124DC File Offset: 0x000106DC
		private static Exception _InvalidOperation(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new InvalidOperationException(error));
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000124E9 File Offset: 0x000106E9
		private static Exception _InvalidEnumArgumentException(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new InvalidEnumArgumentException(error));
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000124F6 File Offset: 0x000106F6
		private static Exception _InvalidEnumArgumentException<T>(T value)
		{
			return ExceptionBuilder._InvalidEnumArgumentException(SR.Format("The {0} enumeration value, {1}, is invalid.", typeof(T).Name, value.ToString()));
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00012523 File Offset: 0x00010723
		private static void ThrowDataException(string error, Exception innerException)
		{
			throw ExceptionBuilder.TraceExceptionAsReturnValue(new DataException(error, innerException));
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00012531 File Offset: 0x00010731
		private static Exception _Data(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new DataException(error));
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001253E File Offset: 0x0001073E
		private static Exception _Constraint(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new ConstraintException(error));
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001254B File Offset: 0x0001074B
		private static Exception _InvalidConstraint(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new InvalidConstraintException(error));
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00012558 File Offset: 0x00010758
		private static Exception _DeletedRowInaccessible(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new DeletedRowInaccessibleException(error));
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00012565 File Offset: 0x00010765
		private static Exception _DuplicateName(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new DuplicateNameException(error));
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00012572 File Offset: 0x00010772
		private static Exception _InRowChangingEvent(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new InRowChangingEventException(error));
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001257F File Offset: 0x0001077F
		private static Exception _MissingPrimaryKey(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new MissingPrimaryKeyException(error));
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001258C File Offset: 0x0001078C
		private static Exception _NoNullAllowed(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new NoNullAllowedException(error));
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00012599 File Offset: 0x00010799
		private static Exception _ReadOnly(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new ReadOnlyException(error));
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000125A6 File Offset: 0x000107A6
		private static Exception _RowNotInTable(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new RowNotInTableException(error));
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000125B3 File Offset: 0x000107B3
		private static Exception _VersionNotFound(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new VersionNotFoundException(error));
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000125C0 File Offset: 0x000107C0
		public static Exception ArgumentNull(string paramName)
		{
			return ExceptionBuilder._ArgumentNull(paramName, SR.Format("'{0}' argument cannot be null.", paramName));
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000125D3 File Offset: 0x000107D3
		public static Exception ArgumentOutOfRange(string paramName)
		{
			return ExceptionBuilder._ArgumentOutOfRange(paramName, SR.Format("'{0}' argument is out of range.", paramName));
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000125E6 File Offset: 0x000107E6
		public static Exception BadObjectPropertyAccess(string error)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Property not accessible because '{0}'.", error));
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000125F8 File Offset: 0x000107F8
		public static Exception ArgumentContainsNull(string paramName)
		{
			return ExceptionBuilder._Argument(paramName, SR.Format("'{0}' argument contains null value.", paramName));
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001260B File Offset: 0x0001080B
		public static Exception CannotModifyCollection()
		{
			return ExceptionBuilder._Argument("Collection itself is not modifiable.");
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00012617 File Offset: 0x00010817
		public static Exception CaseInsensitiveNameConflict(string name)
		{
			return ExceptionBuilder._Argument(SR.Format("The given name '{0}' matches at least two names in the collection object with different cases, but does not match either of them with the same case.", name));
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00012629 File Offset: 0x00010829
		public static Exception NamespaceNameConflict(string name)
		{
			return ExceptionBuilder._Argument(SR.Format("The given name '{0}' matches at least two names in the collection object with different namespaces.", name));
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001263B File Offset: 0x0001083B
		public static Exception InvalidOffsetLength()
		{
			return ExceptionBuilder._Argument("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00012647 File Offset: 0x00010847
		public static Exception ColumnNotInTheTable(string column, string table)
		{
			return ExceptionBuilder._Argument(SR.Format("Column '{0}' does not belong to table {1}.", column, table));
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001265A File Offset: 0x0001085A
		public static Exception ColumnNotInAnyTable()
		{
			return ExceptionBuilder._Argument("Column must belong to a table.");
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00012666 File Offset: 0x00010866
		public static Exception ColumnOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Cannot find column {0}.", index.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00012683 File Offset: 0x00010883
		public static Exception ColumnOutOfRange(string column)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Cannot find column {0}.", column));
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00012695 File Offset: 0x00010895
		public static Exception CannotAddColumn1(string column)
		{
			return ExceptionBuilder._Argument(SR.Format("Column '{0}' already belongs to this DataTable.", column));
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000126A7 File Offset: 0x000108A7
		public static Exception CannotAddColumn2(string column)
		{
			return ExceptionBuilder._Argument(SR.Format("Column '{0}' already belongs to another DataTable.", column));
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000126B9 File Offset: 0x000108B9
		public static Exception CannotAddColumn3()
		{
			return ExceptionBuilder._Argument("Cannot have more than one SimpleContent columns in a DataTable.");
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000126C5 File Offset: 0x000108C5
		public static Exception CannotAddColumn4(string column)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot add a SimpleContent column to a table containing element columns or nested relations.", column));
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000126D7 File Offset: 0x000108D7
		public static Exception CannotAddDuplicate(string column)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("A column named '{0}' already belongs to this DataTable.", column));
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000126E9 File Offset: 0x000108E9
		public static Exception CannotAddDuplicate2(string table)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("Cannot add a column named '{0}': a nested table with the same name already belongs to this DataTable.", table));
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x000126FB File Offset: 0x000108FB
		public static Exception CannotAddDuplicate3(string table)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("A column named '{0}' already belongs to this DataTable: cannot set a nested table name to the same name.", table));
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001270D File Offset: 0x0001090D
		public static Exception CannotRemoveColumn()
		{
			return ExceptionBuilder._Argument("Cannot remove a column that doesn't belong to this table.");
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00012719 File Offset: 0x00010919
		public static Exception CannotRemovePrimaryKey()
		{
			return ExceptionBuilder._Argument("Cannot remove this column, because it's part of the primary key.");
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00012725 File Offset: 0x00010925
		public static Exception CannotRemoveChildKey(string relation)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot remove this column, because it is part of the parent key for relationship {0}.", relation));
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00012737 File Offset: 0x00010937
		public static Exception CannotRemoveConstraint(string constraint, string table)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot remove this column, because it is a part of the constraint {0} on the table {1}.", constraint, table));
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001274A File Offset: 0x0001094A
		public static Exception CannotRemoveExpression(string column, string expression)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot remove this column, because it is part of an expression: {0} = {1}.", column, expression));
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001275D File Offset: 0x0001095D
		public static Exception ColumnNotInTheUnderlyingTable(string column, string table)
		{
			return ExceptionBuilder._Argument(SR.Format("Column '{0}' does not belong to underlying table '{1}'.", column, table));
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00012770 File Offset: 0x00010970
		public static Exception InvalidOrdinal(string name, int ordinal)
		{
			return ExceptionBuilder._ArgumentOutOfRange(name, SR.Format("Ordinal '{0}' exceeds the maximum number.", ordinal.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001278E File Offset: 0x0001098E
		public static Exception AddPrimaryKeyConstraint()
		{
			return ExceptionBuilder._Argument("Cannot add primary key constraint since primary key is already set for the table.");
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001279A File Offset: 0x0001099A
		public static Exception NoConstraintName()
		{
			return ExceptionBuilder._Argument("Cannot change the name of a constraint to empty string when it is in the ConstraintCollection.");
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000127A6 File Offset: 0x000109A6
		public static Exception ConstraintViolation(string constraint)
		{
			return ExceptionBuilder._Constraint(SR.Format("Cannot enforce constraints on constraint {0}.", constraint));
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000127B8 File Offset: 0x000109B8
		public static Exception ConstraintNotInTheTable(string constraint)
		{
			return ExceptionBuilder._Argument(SR.Format("Constraint '{0}' does not belong to this DataTable.", constraint));
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000127CC File Offset: 0x000109CC
		public static string KeysToString(object[] keys)
		{
			string text = string.Empty;
			for (int i = 0; i < keys.Length; i++)
			{
				text = text + Convert.ToString(keys[i], null) + ((i < keys.Length - 1) ? ", " : string.Empty);
			}
			return text;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00012814 File Offset: 0x00010A14
		public static string UniqueConstraintViolationText(DataColumn[] columns, object[] values)
		{
			if (columns.Length > 1)
			{
				string text = string.Empty;
				for (int i = 0; i < columns.Length; i++)
				{
					text = text + columns[i].ColumnName + ((i < columns.Length - 1) ? ", " : "");
				}
				return SR.Format("Column '{0}' is constrained to be unique.  Value '{1}' is already present.", text, ExceptionBuilder.KeysToString(values));
			}
			return SR.Format("Column '{0}' is constrained to be unique.  Value '{1}' is already present.", columns[0].ColumnName, Convert.ToString(values[0], null));
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0001288B File Offset: 0x00010A8B
		public static Exception ConstraintViolation(DataColumn[] columns, object[] values)
		{
			return ExceptionBuilder._Constraint(ExceptionBuilder.UniqueConstraintViolationText(columns, values));
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00012899 File Offset: 0x00010A99
		public static Exception ConstraintOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Cannot find constraint {0}.", index.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000128B6 File Offset: 0x00010AB6
		public static Exception DuplicateConstraint(string constraint)
		{
			return ExceptionBuilder._Data(SR.Format("Constraint matches constraint named {0} already in collection.", constraint));
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000128C8 File Offset: 0x00010AC8
		public static Exception DuplicateConstraintName(string constraint)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("A Constraint named '{0}' already belongs to this DataTable.", constraint));
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000128DA File Offset: 0x00010ADA
		public static Exception NeededForForeignKeyConstraint(UniqueConstraint key, ForeignKeyConstraint fk)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot remove unique constraint '{0}'. Remove foreign key constraint '{1}' first.", key.ConstraintName, fk.ConstraintName));
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000128F7 File Offset: 0x00010AF7
		public static Exception UniqueConstraintViolation()
		{
			return ExceptionBuilder._Argument("These columns don't currently have unique values.");
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00012903 File Offset: 0x00010B03
		public static Exception ConstraintForeignTable()
		{
			return ExceptionBuilder._Argument("These columns don't point to this table.");
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001290F File Offset: 0x00010B0F
		public static Exception ConstraintParentValues()
		{
			return ExceptionBuilder._Argument("This constraint cannot be enabled as not all values have corresponding parent values.");
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001291B File Offset: 0x00010B1B
		public static Exception ConstraintAddFailed(DataTable table)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("This constraint cannot be added since ForeignKey doesn't belong to table {0}.", table.TableName));
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00012932 File Offset: 0x00010B32
		public static Exception ConstraintRemoveFailed()
		{
			return ExceptionBuilder._Argument("Cannot remove a constraint that doesn't belong to this table.");
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001293E File Offset: 0x00010B3E
		public static Exception FailedCascadeDelete(string constraint)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Cannot delete this row because constraints are enforced on relation {0}, and deleting this row will strand child rows.", constraint));
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00012950 File Offset: 0x00010B50
		public static Exception FailedCascadeUpdate(string constraint)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Cannot make this change because constraints are enforced on relation {0}, and changing this value will strand child rows.", constraint));
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00012962 File Offset: 0x00010B62
		public static Exception FailedClearParentTable(string table, string constraint, string childTable)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Cannot clear table {0} because ForeignKeyConstraint {1} enforces constraints and there are child rows in {2}.", table, constraint, childTable));
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00012976 File Offset: 0x00010B76
		public static Exception ForeignKeyViolation(string constraint, object[] keys)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("ForeignKeyConstraint {0} requires the child key values ({1}) to exist in the parent table.", constraint, ExceptionBuilder.KeysToString(keys)));
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001298E File Offset: 0x00010B8E
		public static Exception RemoveParentRow(ForeignKeyConstraint constraint)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Cannot remove this row because it has child rows, and constraints on relation {0} are enforced.", constraint.ConstraintName));
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x000129A5 File Offset: 0x00010BA5
		public static string MaxLengthViolationText(string columnName)
		{
			return SR.Format("Column '{0}' exceeds the MaxLength limit.", columnName);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x000129B2 File Offset: 0x00010BB2
		public static string NotAllowDBNullViolationText(string columnName)
		{
			return SR.Format("Column '{0}' does not allow DBNull.Value.", columnName);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000129BF File Offset: 0x00010BBF
		public static Exception CantAddConstraintToMultipleNestedTable(string tableName)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot add constraint to DataTable '{0}' which is a child table in two nested relations.", tableName));
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000129D1 File Offset: 0x00010BD1
		public static Exception AutoIncrementAndExpression()
		{
			return ExceptionBuilder._Argument("Cannot set AutoIncrement property for a computed column.");
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000129DD File Offset: 0x00010BDD
		public static Exception AutoIncrementAndDefaultValue()
		{
			return ExceptionBuilder._Argument("Cannot set AutoIncrement property for a column with DefaultValue set.");
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000129E9 File Offset: 0x00010BE9
		public static Exception AutoIncrementSeed()
		{
			return ExceptionBuilder._Argument("AutoIncrementStep must be a non-zero value.");
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000129F5 File Offset: 0x00010BF5
		public static Exception CantChangeDataType()
		{
			return ExceptionBuilder._Argument("Cannot change DataType of a column once it has data.");
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00012A01 File Offset: 0x00010C01
		public static Exception NullDataType()
		{
			return ExceptionBuilder._Argument("Column requires a valid DataType.");
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00012A0D File Offset: 0x00010C0D
		public static Exception ColumnNameRequired()
		{
			return ExceptionBuilder._Argument("ColumnName is required when it is part of a DataTable.");
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00012A19 File Offset: 0x00010C19
		public static Exception DefaultValueAndAutoIncrement()
		{
			return ExceptionBuilder._Argument("Cannot set a DefaultValue on an AutoIncrement column.");
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00012A28 File Offset: 0x00010C28
		public static Exception DefaultValueDataType(string column, Type defaultType, Type columnType, Exception inner)
		{
			if (column.Length != 0)
			{
				return ExceptionBuilder._Argument(SR.Format("The DefaultValue for column {0} is of type {1} and cannot be converted to {2}.", column, defaultType.FullName, columnType.FullName), inner);
			}
			return ExceptionBuilder._Argument(SR.Format("The DefaultValue for the column is of type {0} and cannot be converted to {1}.", defaultType.FullName, columnType.FullName), inner);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00012A77 File Offset: 0x00010C77
		public static Exception DefaultValueColumnDataType(string column, Type defaultType, Type columnType, Exception inner)
		{
			return ExceptionBuilder._Argument(SR.Format("The DefaultValue for column {0} is of type {1}, but the column is of type {2}.", column, defaultType.FullName, columnType.FullName), inner);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00012A96 File Offset: 0x00010C96
		public static Exception ExpressionAndUnique()
		{
			return ExceptionBuilder._Argument("Cannot create an expression on a column that has AutoIncrement or Unique.");
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00012AA2 File Offset: 0x00010CA2
		public static Exception ExpressionAndReadOnly()
		{
			return ExceptionBuilder._Argument("Cannot set expression because column cannot be made ReadOnly.");
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00012AAE File Offset: 0x00010CAE
		public static Exception ExpressionAndConstraint(DataColumn column, Constraint constraint)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set Expression property on column {0}, because it is a part of a constraint.", column.ColumnName, constraint.ConstraintName));
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00012ACB File Offset: 0x00010CCB
		public static Exception ExpressionInConstraint(DataColumn column)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot create a constraint based on Expression column {0}.", column.ColumnName));
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00012AE2 File Offset: 0x00010CE2
		public static Exception ExpressionCircular()
		{
			return ExceptionBuilder._Argument("Cannot set Expression property due to circular reference in the expression.");
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00012AEE File Offset: 0x00010CEE
		public static Exception NonUniqueValues(string column)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Column '{0}' contains non-unique values.", column));
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00012B00 File Offset: 0x00010D00
		public static Exception NullKeyValues(string column)
		{
			return ExceptionBuilder._Data(SR.Format("Column '{0}' has null values in it.", column));
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00012B12 File Offset: 0x00010D12
		public static Exception NullValues(string column)
		{
			return ExceptionBuilder._NoNullAllowed(SR.Format("Column '{0}' does not allow nulls.", column));
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00012B24 File Offset: 0x00010D24
		public static Exception ReadOnlyAndExpression()
		{
			return ExceptionBuilder._ReadOnly("Cannot change ReadOnly property for the expression column.");
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00012B30 File Offset: 0x00010D30
		public static Exception ReadOnly(string column)
		{
			return ExceptionBuilder._ReadOnly(SR.Format("Column '{0}' is read only.", column));
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00012B42 File Offset: 0x00010D42
		public static Exception UniqueAndExpression()
		{
			return ExceptionBuilder._Argument("Cannot change Unique property for the expression column.");
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00012B4E File Offset: 0x00010D4E
		public static Exception SetFailed(object value, DataColumn column, Type type, Exception innerException)
		{
			return ExceptionBuilder._Argument(innerException.Message + SR.Format("Couldn't store <{0}> in {1} Column.  Expected type is {2}.", value.ToString(), column.ColumnName, type.Name), innerException);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00012B7D File Offset: 0x00010D7D
		public static Exception CannotSetToNull(DataColumn column)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set Column '{0}' to be null. Please use DBNull instead.", column.ColumnName));
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00012B94 File Offset: 0x00010D94
		public static Exception LongerThanMaxLength(DataColumn column)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set column '{0}'. The value violates the MaxLength limit of this column.", column.ColumnName));
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00012BAB File Offset: 0x00010DAB
		public static Exception CannotSetMaxLength(DataColumn column, int value)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set Column '{0}' property MaxLength to '{1}'. There is at least one string in the table longer than the new limit.", column.ColumnName, value.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00012BCE File Offset: 0x00010DCE
		public static Exception CannotSetMaxLength2(DataColumn column)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set Column '{0}' property MaxLength. The Column is SimpleContent.", column.ColumnName));
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00012BE5 File Offset: 0x00010DE5
		public static Exception CannotSetSimpleContentType(string columnName, Type type)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set Column '{0}' property DataType to {1}. The Column is SimpleContent.", columnName, type));
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00012BF8 File Offset: 0x00010DF8
		public static Exception CannotSetSimpleContent(string columnName, Type type)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set Column '{0}' property MappingType to SimpleContent. The Column DataType is {1}.", columnName, type));
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00012C0B File Offset: 0x00010E0B
		public static Exception CannotChangeNamespace(string columnName)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot change the Column '{0}' property Namespace. The Column is SimpleContent.", columnName));
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00012C1D File Offset: 0x00010E1D
		public static Exception HasToBeStringType(DataColumn column)
		{
			return ExceptionBuilder._Argument(SR.Format("MaxLength applies to string data type only. You cannot set Column '{0}' property MaxLength to be non-negative number.", column.ColumnName));
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00012C34 File Offset: 0x00010E34
		public static Exception AutoIncrementCannotSetIfHasData(string typeName)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot change AutoIncrement of a DataColumn with type '{0}' once it has data.", typeName));
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00012C46 File Offset: 0x00010E46
		public static Exception INullableUDTwithoutStaticNull(string typeName)
		{
			return ExceptionBuilder._Argument(SR.Format("Type '{0}' does not contain static Null property or field.", typeName));
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00012C58 File Offset: 0x00010E58
		public static Exception IComparableNotImplemented(string typeName)
		{
			return ExceptionBuilder._Data(SR.Format(" Type '{0}' does not implement IComparable interface. Comparison cannot be done.", typeName));
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00012C6A File Offset: 0x00010E6A
		public static Exception UDTImplementsIChangeTrackingButnotIRevertible(string typeName)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Type '{0}' does not implement IRevertibleChangeTracking; therefore can not proceed with RejectChanges().", typeName));
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00012C7C File Offset: 0x00010E7C
		public static Exception SetAddedAndModifiedCalledOnnonUnchanged()
		{
			return ExceptionBuilder._InvalidOperation("SetAdded and SetModified can only be called on DataRows with Unchanged DataRowState.");
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00012C88 File Offset: 0x00010E88
		public static Exception InvalidDataColumnMapping(Type type)
		{
			return ExceptionBuilder._Argument(SR.Format("DataColumn with type '{0}' is a complexType. Can not serialize value of a complex type as Attribute", type.AssemblyQualifiedName));
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00012C9F File Offset: 0x00010E9F
		public static Exception CannotSetDateTimeModeForNonDateTimeColumns()
		{
			return ExceptionBuilder._InvalidOperation("The DateTimeMode can be set only on DataColumns of type DateTime.");
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00012CAB File Offset: 0x00010EAB
		public static Exception InvalidDateTimeMode(DataSetDateTime mode)
		{
			return ExceptionBuilder._InvalidEnumArgumentException<DataSetDateTime>(mode);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00012CB3 File Offset: 0x00010EB3
		public static Exception CantChangeDateTimeMode(DataSetDateTime oldValue, DataSetDateTime newValue)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Cannot change DateTimeMode from '{0}' to '{1}' once the table has data.", oldValue.ToString(), newValue.ToString()));
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00012CDE File Offset: 0x00010EDE
		public static Exception ColumnTypeNotSupported()
		{
			return ADP.NotSupported("DataSet does not support System.Nullable<>.");
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00012CEA File Offset: 0x00010EEA
		public static Exception SetFailed(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Cannot set {0}.", name));
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00012CFC File Offset: 0x00010EFC
		public static Exception SetDataSetFailed()
		{
			return ExceptionBuilder._Data("Cannot change DataSet on a DataViewManager that's already the default view for a DataSet.");
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00012D08 File Offset: 0x00010F08
		public static Exception SetRowStateFilter()
		{
			return ExceptionBuilder._Data("RowStateFilter cannot show ModifiedOriginals and ModifiedCurrents at the same time.");
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00012D14 File Offset: 0x00010F14
		public static Exception CanNotSetDataSet()
		{
			return ExceptionBuilder._Data("Cannot change DataSet property once it is set.");
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00012D20 File Offset: 0x00010F20
		public static Exception CanNotUseDataViewManager()
		{
			return ExceptionBuilder._Data("DataSet must be set prior to using DataViewManager.");
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00012D2C File Offset: 0x00010F2C
		public static Exception CanNotSetTable()
		{
			return ExceptionBuilder._Data("Cannot change Table property once it is set.");
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00012D38 File Offset: 0x00010F38
		public static Exception CanNotUse()
		{
			return ExceptionBuilder._Data("DataTable must be set prior to using DataView.");
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00012D44 File Offset: 0x00010F44
		public static Exception CanNotBindTable()
		{
			return ExceptionBuilder._Data("Cannot bind to DataTable with no name.");
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00012D50 File Offset: 0x00010F50
		public static Exception SetTable()
		{
			return ExceptionBuilder._Data("Cannot change Table property on a DefaultView or a DataView coming from a DataViewManager.");
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00012D5C File Offset: 0x00010F5C
		public static Exception SetIListObject()
		{
			return ExceptionBuilder._Argument("Cannot set an object into this list.");
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00012D68 File Offset: 0x00010F68
		public static Exception AddNewNotAllowNull()
		{
			return ExceptionBuilder._Data("Cannot call AddNew on a DataView where AllowNew is false.");
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00012D74 File Offset: 0x00010F74
		public static Exception NotOpen()
		{
			return ExceptionBuilder._Data("DataView is not open.");
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00012D80 File Offset: 0x00010F80
		public static Exception CreateChildView()
		{
			return ExceptionBuilder._Argument("The relation is not parented to the table to which this DataView points.");
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00012D8C File Offset: 0x00010F8C
		public static Exception CanNotDelete()
		{
			return ExceptionBuilder._Data("Cannot delete on a DataSource where AllowDelete is false.");
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00012D98 File Offset: 0x00010F98
		public static Exception CanNotEdit()
		{
			return ExceptionBuilder._Data("Cannot edit on a DataSource where AllowEdit is false.");
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00012DA4 File Offset: 0x00010FA4
		public static Exception GetElementIndex(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Index {0} is either negative or above rows count.", index.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00012DC1 File Offset: 0x00010FC1
		public static Exception AddExternalObject()
		{
			return ExceptionBuilder._Argument("Cannot add external objects to this list.");
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00012DCD File Offset: 0x00010FCD
		public static Exception CanNotClear()
		{
			return ExceptionBuilder._Argument("Cannot clear this list.");
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00012DD9 File Offset: 0x00010FD9
		public static Exception InsertExternalObject()
		{
			return ExceptionBuilder._Argument("Cannot insert external objects to this list.");
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00012DE5 File Offset: 0x00010FE5
		public static Exception RemoveExternalObject()
		{
			return ExceptionBuilder._Argument("Cannot remove objects not in the list.");
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00012DF1 File Offset: 0x00010FF1
		public static Exception PropertyNotFound(string property, string table)
		{
			return ExceptionBuilder._Argument(SR.Format("{0} is neither a DataColumn nor a DataRelation for table {1}.", property, table));
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00012E04 File Offset: 0x00011004
		public static Exception ColumnToSortIsOutOfRange(string column)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot find column {0}.", column));
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00012E16 File Offset: 0x00011016
		public static Exception KeyTableMismatch()
		{
			return ExceptionBuilder._InvalidConstraint("Cannot create a Key from Columns that belong to different tables.");
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00012E22 File Offset: 0x00011022
		public static Exception KeyNoColumns()
		{
			return ExceptionBuilder._InvalidConstraint("Cannot have 0 columns.");
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00012E2E File Offset: 0x0001102E
		public static Exception KeyTooManyColumns(int cols)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Cannot have more than {0} columns.", cols.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00012E4B File Offset: 0x0001104B
		public static Exception KeyDuplicateColumns(string columnName)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Cannot create a Key when the same column is listed more than once: '{0}'", columnName));
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00012E5D File Offset: 0x0001105D
		public static Exception RelationDataSetMismatch()
		{
			return ExceptionBuilder._InvalidConstraint("Cannot have a relationship between tables in different DataSets.");
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00012E69 File Offset: 0x00011069
		public static Exception NoRelationName()
		{
			return ExceptionBuilder._Argument("RelationName is required when it is part of a DataSet.");
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00012E75 File Offset: 0x00011075
		public static Exception ColumnsTypeMismatch()
		{
			return ExceptionBuilder._InvalidConstraint("Parent Columns and Child Columns don't have type-matching columns.");
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00012E81 File Offset: 0x00011081
		public static Exception KeyLengthMismatch()
		{
			return ExceptionBuilder._Argument("ParentColumns and ChildColumns should be the same length.");
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00012E8D File Offset: 0x0001108D
		public static Exception KeyLengthZero()
		{
			return ExceptionBuilder._Argument("ParentColumns and ChildColumns must not be zero length.");
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00012E99 File Offset: 0x00011099
		public static Exception ForeignRelation()
		{
			return ExceptionBuilder._Argument("This relation should connect two tables in this DataSet to be added to this DataSet.");
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00012EA5 File Offset: 0x000110A5
		public static Exception KeyColumnsIdentical()
		{
			return ExceptionBuilder._InvalidConstraint("ParentKey and ChildKey are identical.");
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00012EB1 File Offset: 0x000110B1
		public static Exception RelationForeignTable(string t1, string t2)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("GetChildRows requires a row whose Table is {0}, but the specified row's Table is {1}.", t1, t2));
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00012EC4 File Offset: 0x000110C4
		public static Exception GetParentRowTableMismatch(string t1, string t2)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("GetParentRow requires a row whose Table is {0}, but the specified row's Table is {1}.", t1, t2));
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00012ED7 File Offset: 0x000110D7
		public static Exception SetParentRowTableMismatch(string t1, string t2)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("SetParentRow requires a child row whose Table is {0}, but the specified row's Table is {1}.", t1, t2));
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00012EEA File Offset: 0x000110EA
		public static Exception RelationForeignRow()
		{
			return ExceptionBuilder._Argument("The row doesn't belong to the same DataSet as this relation.");
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00012EF6 File Offset: 0x000110F6
		public static Exception RelationNestedReadOnly()
		{
			return ExceptionBuilder._Argument("Cannot set the 'Nested' property to false for this relation.");
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00012F02 File Offset: 0x00011102
		public static Exception TableCantBeNestedInTwoTables(string tableName)
		{
			return ExceptionBuilder._Argument(SR.Format("The same table '{0}' cannot be the child table in two nested relations.", tableName));
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00012F14 File Offset: 0x00011114
		public static Exception LoopInNestedRelations(string tableName)
		{
			return ExceptionBuilder._Argument(SR.Format("The table ({0}) cannot be the child table to itself in nested relations.", tableName));
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00012F26 File Offset: 0x00011126
		public static Exception RelationDoesNotExist()
		{
			return ExceptionBuilder._Argument("This relation doesn't belong to this relation collection.");
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00012F32 File Offset: 0x00011132
		public static Exception ParentRowNotInTheDataSet()
		{
			return ExceptionBuilder._Argument("This relation and child row don't belong to same DataSet.");
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00012F3E File Offset: 0x0001113E
		public static Exception ParentOrChildColumnsDoNotHaveDataSet()
		{
			return ExceptionBuilder._InvalidConstraint("Cannot create a DataRelation if Parent or Child Columns are not in a DataSet.");
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00012F4A File Offset: 0x0001114A
		public static Exception InValidNestedRelation(string childTableName)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Nested table '{0}' which inherits its namespace cannot have multiple parent tables in different namespaces.", childTableName));
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00012F5C File Offset: 0x0001115C
		public static Exception InvalidParentNamespaceinNestedRelation(string childTableName)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Nested table '{0}' with empty namespace cannot have multiple parent tables in different namespaces.", childTableName));
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00012EEA File Offset: 0x000110EA
		public static Exception RowNotInTheDataSet()
		{
			return ExceptionBuilder._Argument("The row doesn't belong to the same DataSet as this relation.");
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00012F6E File Offset: 0x0001116E
		public static Exception RowNotInTheTable()
		{
			return ExceptionBuilder._RowNotInTable("Cannot perform this operation on a row not in the table.");
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00012F7A File Offset: 0x0001117A
		public static Exception EditInRowChanging()
		{
			return ExceptionBuilder._InRowChangingEvent("Cannot change a proposed value in the RowChanging event.");
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00012F86 File Offset: 0x00011186
		public static Exception EndEditInRowChanging()
		{
			return ExceptionBuilder._InRowChangingEvent("Cannot call EndEdit() inside an OnRowChanging event.");
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00012F92 File Offset: 0x00011192
		public static Exception BeginEditInRowChanging()
		{
			return ExceptionBuilder._InRowChangingEvent("Cannot call BeginEdit() inside the RowChanging event.");
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00012F9E File Offset: 0x0001119E
		public static Exception CancelEditInRowChanging()
		{
			return ExceptionBuilder._InRowChangingEvent("Cannot call CancelEdit() inside an OnRowChanging event.  Throw an exception to cancel this update.");
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00012FAA File Offset: 0x000111AA
		public static Exception DeleteInRowDeleting()
		{
			return ExceptionBuilder._InRowChangingEvent("Cannot call Delete inside an OnRowDeleting event.  Throw an exception to cancel this delete.");
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00012FB6 File Offset: 0x000111B6
		public static Exception ValueArrayLength()
		{
			return ExceptionBuilder._Argument("Input array is longer than the number of columns in this table.");
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00012FC2 File Offset: 0x000111C2
		public static Exception NoCurrentData()
		{
			return ExceptionBuilder._VersionNotFound("There is no Current data to access.");
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00012FCE File Offset: 0x000111CE
		public static Exception NoOriginalData()
		{
			return ExceptionBuilder._VersionNotFound("There is no Original data to access.");
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00012FDA File Offset: 0x000111DA
		public static Exception NoProposedData()
		{
			return ExceptionBuilder._VersionNotFound("There is no Proposed data to access.");
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00012FE6 File Offset: 0x000111E6
		public static Exception RowRemovedFromTheTable()
		{
			return ExceptionBuilder._RowNotInTable("This row has been removed from a table and does not have any data.  BeginEdit() will allow creation of new data in this row.");
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00012FF2 File Offset: 0x000111F2
		public static Exception DeletedRowInaccessible()
		{
			return ExceptionBuilder._DeletedRowInaccessible("Deleted row information cannot be accessed through the row.");
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00012FFE File Offset: 0x000111FE
		public static Exception RowAlreadyDeleted()
		{
			return ExceptionBuilder._DeletedRowInaccessible("Cannot delete this row since it's already deleted.");
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001300A File Offset: 0x0001120A
		public static Exception RowEmpty()
		{
			return ExceptionBuilder._Argument("This row is empty.");
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00013016 File Offset: 0x00011216
		public static Exception InvalidRowVersion()
		{
			return ExceptionBuilder._Data("Version must be Original, Current, or Proposed.");
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00013022 File Offset: 0x00011222
		public static Exception RowOutOfRange()
		{
			return ExceptionBuilder._IndexOutOfRange("The given DataRow is not in the current DataRowCollection.");
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001302E File Offset: 0x0001122E
		public static Exception RowOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("There is no row at position {0}.", index.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001304B File Offset: 0x0001124B
		public static Exception RowInsertOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("The row insert position {0} is invalid.", index.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00013068 File Offset: 0x00011268
		public static Exception RowInsertTwice(int index, string tableName)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("The rowOrder value={0} has been found twice for table named '{1}'.", index.ToString(CultureInfo.InvariantCulture), tableName));
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00013086 File Offset: 0x00011286
		public static Exception RowInsertMissing(string tableName)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Values are missing in the rowOrder sequence for table '{0}'.", tableName));
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00013098 File Offset: 0x00011298
		public static Exception RowAlreadyRemoved()
		{
			return ExceptionBuilder._Data("Cannot remove a row that's already been removed.");
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000130A4 File Offset: 0x000112A4
		public static Exception MultipleParents()
		{
			return ExceptionBuilder._Data("A child row has multiple parents.");
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000130B0 File Offset: 0x000112B0
		public static Exception InvalidRowState(DataRowState state)
		{
			return ExceptionBuilder._InvalidEnumArgumentException<DataRowState>(state);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000130B8 File Offset: 0x000112B8
		public static Exception InvalidRowBitPattern()
		{
			return ExceptionBuilder._Argument("Unrecognized row state bit pattern.");
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000130C4 File Offset: 0x000112C4
		internal static Exception SetDataSetNameToEmpty()
		{
			return ExceptionBuilder._Argument("Cannot change the name of the DataSet to an empty string.");
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000130D0 File Offset: 0x000112D0
		internal static Exception SetDataSetNameConflicting(string name)
		{
			return ExceptionBuilder._Argument(SR.Format("The name '{0}' is invalid. A DataSet cannot have the same name of the DataTable.", name));
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000130E2 File Offset: 0x000112E2
		public static Exception DataSetUnsupportedSchema(string ns)
		{
			return ExceptionBuilder._Argument(SR.Format("The schema namespace is invalid. Please use this one instead: {0}.", ns));
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000130F4 File Offset: 0x000112F4
		public static Exception MergeMissingDefinition(string obj)
		{
			return ExceptionBuilder._Argument(SR.Format("Target DataSet missing definition for {0}.", obj));
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00013106 File Offset: 0x00011306
		public static Exception TablesInDifferentSets()
		{
			return ExceptionBuilder._Argument("Cannot create a relation between tables in different DataSets.");
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00013112 File Offset: 0x00011312
		public static Exception RelationAlreadyExists()
		{
			return ExceptionBuilder._Argument("A relation already exists for these child columns.");
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001311E File Offset: 0x0001131E
		public static Exception RowAlreadyInOtherCollection()
		{
			return ExceptionBuilder._Argument("This row already belongs to another table.");
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001312A File Offset: 0x0001132A
		public static Exception RowAlreadyInTheCollection()
		{
			return ExceptionBuilder._Argument("This row already belongs to this table.");
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00013136 File Offset: 0x00011336
		public static Exception TableMissingPrimaryKey()
		{
			return ExceptionBuilder._MissingPrimaryKey("Table doesn't have a primary key.");
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00013142 File Offset: 0x00011342
		public static Exception RecordStateRange()
		{
			return ExceptionBuilder._Argument("The RowStates parameter must be set to a valid combination of values from the DataViewRowState enumeration.");
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001314E File Offset: 0x0001134E
		public static Exception IndexKeyLength(int length, int keyLength)
		{
			if (length != 0)
			{
				return ExceptionBuilder._Argument(SR.Format("Expecting {0} value(s) for the key being indexed, but received {1} value(s).", length.ToString(CultureInfo.InvariantCulture), keyLength.ToString(CultureInfo.InvariantCulture)));
			}
			return ExceptionBuilder._Argument("Find finds a row based on a Sort order, and no Sort order is specified.");
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00013185 File Offset: 0x00011385
		public static Exception RemovePrimaryKey(DataTable table)
		{
			if (table.TableName.Length != 0)
			{
				return ExceptionBuilder._Argument(SR.Format("Cannot remove unique constraint since it's the primary key of table {0}.", table.TableName));
			}
			return ExceptionBuilder._Argument("Cannot remove unique constraint since it's the primary key of a table.");
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000131B4 File Offset: 0x000113B4
		public static Exception RelationAlreadyInOtherDataSet()
		{
			return ExceptionBuilder._Argument("This relation already belongs to another DataSet.");
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000131C0 File Offset: 0x000113C0
		public static Exception RelationAlreadyInTheDataSet()
		{
			return ExceptionBuilder._Argument("This relation already belongs to this DataSet.");
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000131CC File Offset: 0x000113CC
		public static Exception RelationNotInTheDataSet(string relation)
		{
			return ExceptionBuilder._Argument(SR.Format("Relation {0} does not belong to this DataSet.", relation));
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000131DE File Offset: 0x000113DE
		public static Exception RelationOutOfRange(object index)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Cannot find relation {0}.", Convert.ToString(index, null)));
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x000131F6 File Offset: 0x000113F6
		public static Exception DuplicateRelation(string relation)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("A Relation named '{0}' already belongs to this DataSet.", relation));
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00013208 File Offset: 0x00011408
		public static Exception RelationTableNull()
		{
			return ExceptionBuilder._Argument("Cannot create a collection on a null table.");
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00013208 File Offset: 0x00011408
		public static Exception RelationDataSetNull()
		{
			return ExceptionBuilder._Argument("Cannot create a collection on a null table.");
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00013214 File Offset: 0x00011414
		public static Exception RelationTableWasRemoved()
		{
			return ExceptionBuilder._Argument("The table this collection displays relations for has been removed from its DataSet.");
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00013220 File Offset: 0x00011420
		public static Exception ParentTableMismatch()
		{
			return ExceptionBuilder._Argument("Cannot add a relation to this table's ChildRelation collection where this table isn't the parent table.");
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001322C File Offset: 0x0001142C
		public static Exception ChildTableMismatch()
		{
			return ExceptionBuilder._Argument("Cannot add a relation to this table's ParentRelation collection where this table isn't the child table.");
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00013238 File Offset: 0x00011438
		public static Exception EnforceConstraint()
		{
			return ExceptionBuilder._Constraint("Failed to enable constraints. One or more rows contain values violating non-null, unique, or foreign-key constraints.");
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00013244 File Offset: 0x00011444
		public static Exception CaseLocaleMismatch()
		{
			return ExceptionBuilder._Argument("Cannot add a DataRelation or Constraint that has different Locale or CaseSensitive settings between its parent and child tables.");
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00013250 File Offset: 0x00011450
		public static Exception CannotChangeCaseLocale()
		{
			return ExceptionBuilder.CannotChangeCaseLocale(null);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00013258 File Offset: 0x00011458
		public static Exception CannotChangeCaseLocale(Exception innerException)
		{
			return ExceptionBuilder._Argument("Cannot change CaseSensitive or Locale property. This change would lead to at least one DataRelation or Constraint to have different Locale or CaseSensitive settings between its related tables.", innerException);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00013265 File Offset: 0x00011465
		public static Exception CannotChangeSchemaSerializationMode()
		{
			return ExceptionBuilder._InvalidOperation("SchemaSerializationMode property can be set only if it is overridden by derived DataSet.");
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00013271 File Offset: 0x00011471
		public static Exception InvalidSchemaSerializationMode(Type enumType, string mode)
		{
			return ExceptionBuilder._InvalidEnumArgumentException(SR.Format("The {0} enumeration value, {1}, is invalid.", enumType.Name, mode));
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00013289 File Offset: 0x00011489
		public static Exception InvalidRemotingFormat(SerializationFormat mode)
		{
			return ExceptionBuilder._InvalidEnumArgumentException<SerializationFormat>(mode);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00013291 File Offset: 0x00011491
		public static Exception TableForeignPrimaryKey()
		{
			return ExceptionBuilder._Argument("PrimaryKey columns do not belong to this table.");
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001329D File Offset: 0x0001149D
		public static Exception TableCannotAddToSimpleContent()
		{
			return ExceptionBuilder._Argument("Cannot add a nested relation or an element column to a table containing a SimpleContent column.");
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x000132A9 File Offset: 0x000114A9
		public static Exception NoTableName()
		{
			return ExceptionBuilder._Argument("TableName is required when it is part of a DataSet.");
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000132B5 File Offset: 0x000114B5
		public static Exception MultipleTextOnlyColumns()
		{
			return ExceptionBuilder._Argument("DataTable already has a simple content column.");
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000132C1 File Offset: 0x000114C1
		public static Exception InvalidSortString(string sort)
		{
			return ExceptionBuilder._Argument(SR.Format(" {0} isn't a valid Sort string entry.", sort));
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000132D3 File Offset: 0x000114D3
		public static Exception DuplicateTableName(string table)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("A DataTable named '{0}' already belongs to this DataSet.", table));
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000132E5 File Offset: 0x000114E5
		public static Exception DuplicateTableName2(string table, string ns)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("A DataTable named '{0}' with the same Namespace '{1}' already belongs to this DataSet.", table, ns));
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x000132F8 File Offset: 0x000114F8
		public static Exception SelfnestedDatasetConflictingName(string table)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("The table ({0}) cannot be the child table to itself in a nested relation: the DataSet name conflicts with the table name.", table));
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001330A File Offset: 0x0001150A
		public static Exception DatasetConflictingName(string table)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("The name '{0}' is invalid. A DataTable cannot have the same name of the DataSet.", table));
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001331C File Offset: 0x0001151C
		public static Exception TableAlreadyInOtherDataSet()
		{
			return ExceptionBuilder._Argument("DataTable already belongs to another DataSet.");
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00013328 File Offset: 0x00011528
		public static Exception TableAlreadyInTheDataSet()
		{
			return ExceptionBuilder._Argument("DataTable already belongs to this DataSet.");
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00013334 File Offset: 0x00011534
		public static Exception TableOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Cannot find table {0}.", index.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00013351 File Offset: 0x00011551
		public static Exception TableNotInTheDataSet(string table)
		{
			return ExceptionBuilder._Argument(SR.Format("Table {0} does not belong to this DataSet.", table));
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00013363 File Offset: 0x00011563
		public static Exception TableInRelation()
		{
			return ExceptionBuilder._Argument("Cannot remove a table that has existing relations.  Remove relations first.");
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001336F File Offset: 0x0001156F
		public static Exception TableInConstraint(DataTable table, Constraint constraint)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot remove table {0}, because it referenced in ForeignKeyConstraint {1}.  Remove the constraint first.", table.TableName, constraint.ConstraintName));
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001338C File Offset: 0x0001158C
		public static Exception CanNotSerializeDataTableHierarchy()
		{
			return ExceptionBuilder._InvalidOperation("Cannot serialize the DataTable. A DataTable being used in one or more DataColumn expressions is not a descendant of current DataTable.");
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00013398 File Offset: 0x00011598
		public static Exception CanNotRemoteDataTable()
		{
			return ExceptionBuilder._InvalidOperation("This DataTable can only be remoted as part of DataSet. One or more Expression Columns has reference to other DataTable(s).");
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x000133A4 File Offset: 0x000115A4
		public static Exception CanNotSetRemotingFormat()
		{
			return ExceptionBuilder._Argument("Cannot have different remoting format property value for DataSet and DataTable.");
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000133B0 File Offset: 0x000115B0
		public static Exception CanNotSerializeDataTableWithEmptyName()
		{
			return ExceptionBuilder._InvalidOperation("Cannot serialize the DataTable. DataTable name is not set.");
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x000133BC File Offset: 0x000115BC
		public static Exception TableNotFound(string tableName)
		{
			return ExceptionBuilder._Argument(SR.Format("DataTable '{0}' does not match to any DataTable in source.", tableName));
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000133CE File Offset: 0x000115CE
		public static Exception AggregateException(AggregateType aggregateType, Type type)
		{
			return ExceptionBuilder._Data(SR.Format("Invalid usage of aggregate function {0}() and Type: {1}.", aggregateType.ToString(), type.Name));
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000133F2 File Offset: 0x000115F2
		public static Exception InvalidStorageType(TypeCode typecode)
		{
			return ExceptionBuilder._Data(SR.Format("Invalid storage type: {0}.", typecode.ToString()));
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00013410 File Offset: 0x00011610
		public static Exception RangeArgument(int min, int max)
		{
			return ExceptionBuilder._Argument(SR.Format("Min ({0}) must be less than or equal to max ({1}) in a Range object.", min.ToString(CultureInfo.InvariantCulture), max.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00013439 File Offset: 0x00011639
		public static Exception NullRange()
		{
			return ExceptionBuilder._Data("This is a null range.");
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00013445 File Offset: 0x00011645
		public static Exception NegativeMinimumCapacity()
		{
			return ExceptionBuilder._Argument("MinimumCapacity must be non-negative.");
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00013454 File Offset: 0x00011654
		public static Exception ProblematicChars(char charValue)
		{
			string text = "The DataSet Xml persistency does not support the value '{0}' as Char value, please use Byte storage instead.";
			string text2 = "0x";
			ushort num = (ushort)charValue;
			return ExceptionBuilder._Argument(SR.Format(text, text2 + num.ToString("X", CultureInfo.InvariantCulture)));
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001348D File Offset: 0x0001168D
		public static Exception StorageSetFailed()
		{
			return ExceptionBuilder._Argument("Type of value has a mismatch with column type");
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00013499 File Offset: 0x00011699
		public static Exception SimpleTypeNotSupported()
		{
			return ExceptionBuilder._Data("DataSet doesn't support 'union' or 'list' as simpleType.");
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000134A5 File Offset: 0x000116A5
		public static Exception MissingAttribute(string attribute)
		{
			return ExceptionBuilder.MissingAttribute(string.Empty, attribute);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x000134B2 File Offset: 0x000116B2
		public static Exception MissingAttribute(string element, string attribute)
		{
			return ExceptionBuilder._Data(SR.Format("Invalid {0} syntax: missing required '{1}' attribute.", element, attribute));
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000134C5 File Offset: 0x000116C5
		public static Exception InvalidAttributeValue(string name, string value)
		{
			return ExceptionBuilder._Data(SR.Format("Value '{1}' is invalid for attribute '{0}'.", name, value));
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000134D8 File Offset: 0x000116D8
		public static Exception AttributeValues(string name, string value1, string value2)
		{
			return ExceptionBuilder._Data(SR.Format("The value of attribute '{0}' should be '{1}' or '{2}'.", name, value1, value2));
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000134EC File Offset: 0x000116EC
		public static Exception ElementTypeNotFound(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Cannot find ElementType name='{0}'.", name));
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000134FE File Offset: 0x000116FE
		public static Exception RelationParentNameMissing(string rel)
		{
			return ExceptionBuilder._Data(SR.Format("Parent table name is missing in relation '{0}'.", rel));
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00013510 File Offset: 0x00011710
		public static Exception RelationChildNameMissing(string rel)
		{
			return ExceptionBuilder._Data(SR.Format("Child table name is missing in relation '{0}'.", rel));
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00013522 File Offset: 0x00011722
		public static Exception RelationTableKeyMissing(string rel)
		{
			return ExceptionBuilder._Data(SR.Format("Parent table key is missing in relation '{0}'.", rel));
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00013534 File Offset: 0x00011734
		public static Exception RelationChildKeyMissing(string rel)
		{
			return ExceptionBuilder._Data(SR.Format("Child table key is missing in relation '{0}'.", rel));
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00013546 File Offset: 0x00011746
		public static Exception UndefinedDatatype(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Undefined data type: '{0}'.", name));
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00013558 File Offset: 0x00011758
		public static Exception DatatypeNotDefined()
		{
			return ExceptionBuilder._Data("Data type not defined.");
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00013564 File Offset: 0x00011764
		public static Exception MismatchKeyLength()
		{
			return ExceptionBuilder._Data("Invalid Relation definition: different length keys.");
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00013570 File Offset: 0x00011770
		public static Exception InvalidField(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Invalid XPath selection inside field node. Cannot find: {0}.", name));
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00013582 File Offset: 0x00011782
		public static Exception InvalidSelector(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Invalid XPath selection inside selector node: {0}.", name));
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00013594 File Offset: 0x00011794
		public static Exception CircularComplexType(string name)
		{
			return ExceptionBuilder._Data(SR.Format("DataSet doesn't allow the circular reference in the ComplexType named '{0}'.", name));
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000135A6 File Offset: 0x000117A6
		public static Exception CannotInstantiateAbstract(string name)
		{
			return ExceptionBuilder._Data(SR.Format("DataSet cannot instantiate an abstract ComplexType for the node {0}.", name));
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000135B8 File Offset: 0x000117B8
		public static Exception InvalidKey(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Invalid 'Key' node inside constraint named: {0}.", name));
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000135CA File Offset: 0x000117CA
		public static Exception DiffgramMissingTable(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Cannot load diffGram. Table '{0}' is missing in the destination dataset.", name));
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x000135DC File Offset: 0x000117DC
		public static Exception DiffgramMissingSQL()
		{
			return ExceptionBuilder._Data("Cannot load diffGram. The 'sql' node is missing.");
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x000135E8 File Offset: 0x000117E8
		public static Exception DuplicateConstraintRead(string str)
		{
			return ExceptionBuilder._Data(SR.Format("The constraint name {0} is already used in the schema.", str));
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000135FA File Offset: 0x000117FA
		public static Exception ColumnTypeConflict(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Column name '{0}' is defined for different mapping types.", name));
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001360C File Offset: 0x0001180C
		public static Exception CannotConvert(string name, string type)
		{
			return ExceptionBuilder._Data(SR.Format(" Cannot convert '{0}' to type '{1}'.", name, type));
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001361F File Offset: 0x0001181F
		public static Exception MissingRefer(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Missing '{0}' part in '{1}' constraint named '{2}'.", "refer", "keyref", name));
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001363B File Offset: 0x0001183B
		public static Exception InvalidPrefix(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Prefix '{0}' is not valid, because it contains special characters.", name));
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001364D File Offset: 0x0001184D
		public static Exception CanNotDeserializeObjectType()
		{
			return ExceptionBuilder._InvalidOperation("Unable to proceed with deserialization. Data does not implement IXMLSerializable, therefore polymorphism is not supported.");
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00013659 File Offset: 0x00011859
		public static Exception IsDataSetAttributeMissingInSchema()
		{
			return ExceptionBuilder._Data("IsDataSet attribute is missing in input Schema.");
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00013665 File Offset: 0x00011865
		public static Exception TooManyIsDataSetAtributeInSchema()
		{
			return ExceptionBuilder._Data("Cannot determine the DataSet Element. IsDataSet attribute exist more than once.");
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00013671 File Offset: 0x00011871
		public static Exception NestedCircular(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Circular reference in self-nested table '{0}'.", name));
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00013683 File Offset: 0x00011883
		public static Exception MultipleParentRows(string tableQName)
		{
			return ExceptionBuilder._Data(SR.Format("Cannot proceed with serializing DataTable '{0}'. It contains a DataRow which has multiple parent rows on the same Foreign Key.", tableQName));
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00013695 File Offset: 0x00011895
		public static Exception PolymorphismNotSupported(string typeName)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Type '{0}' does not implement IXmlSerializable interface therefore can not proceed with serialization.", typeName));
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x000136A7 File Offset: 0x000118A7
		public static Exception DataTableInferenceNotSupported()
		{
			return ExceptionBuilder._InvalidOperation("DataTable does not support schema inference from Xml.");
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x000136B3 File Offset: 0x000118B3
		internal static void ThrowMultipleTargetConverter(Exception innerException)
		{
			ExceptionBuilder.ThrowDataException((innerException != null) ? "An error occurred with the multiple target converter while writing an Xml Schema.  See the inner exception for details." : "An error occurred with the multiple target converter while writing an Xml Schema.  A null or empty string was returned.", innerException);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000136CA File Offset: 0x000118CA
		public static Exception DuplicateDeclaration(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Duplicated declaration '{0}'.", name));
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000136DC File Offset: 0x000118DC
		public static Exception FoundEntity()
		{
			return ExceptionBuilder._Data("DataSet cannot expand entities. Use XmlValidatingReader and set the EntityHandling property accordingly.");
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000136E8 File Offset: 0x000118E8
		public static Exception MergeFailed(string name)
		{
			return ExceptionBuilder._Data(name);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000136F0 File Offset: 0x000118F0
		public static Exception ConvertFailed(Type type1, Type type2)
		{
			return ExceptionBuilder._Data(SR.Format(" Cannot convert object of type '{0}' to object of type '{1}'.", type1.FullName, type2.FullName));
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001370D File Offset: 0x0001190D
		public static Exception InvalidDataTableReader(string tableName)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("DataTableReader is invalid for current DataTable '{0}'.", tableName));
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001371F File Offset: 0x0001191F
		public static Exception DataTableReaderSchemaIsInvalid(string tableName)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Schema of current DataTable '{0}' in DataTableReader has changed, DataTableReader is invalid.", tableName));
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00013731 File Offset: 0x00011931
		public static Exception CannotCreateDataReaderOnEmptyDataSet()
		{
			return ExceptionBuilder._Argument("DataTableReader Cannot be created. There is no DataTable in DataSet.");
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001373D File Offset: 0x0001193D
		public static Exception DataTableReaderArgumentIsEmpty()
		{
			return ExceptionBuilder._Argument("Cannot create DataTableReader. Argument is Empty.");
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00013749 File Offset: 0x00011949
		public static Exception ArgumentContainsNullValue()
		{
			return ExceptionBuilder._Argument("Cannot create DataTableReader. Arguments contain null value.");
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00013755 File Offset: 0x00011955
		public static Exception InvalidCurrentRowInDataTableReader()
		{
			return ExceptionBuilder._DeletedRowInaccessible("Current DataRow is either in Deleted or Detached state.");
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00013761 File Offset: 0x00011961
		public static Exception EmptyDataTableReader(string tableName)
		{
			return ExceptionBuilder._DeletedRowInaccessible(SR.Format("Current DataTable '{0}' is empty. There is no DataRow in DataTable.", tableName));
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00013773 File Offset: 0x00011973
		internal static Exception InvalidDuplicateNamedSimpleTypeDelaration(string stName, string errorStr)
		{
			return ExceptionBuilder._Argument(SR.Format("Simple type '{0}' has already be declared with different '{1}'.", stName, errorStr));
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00013786 File Offset: 0x00011986
		internal static Exception InternalRBTreeError(RBTreeError internalError)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("DataTable internal index is corrupted: '{0}'.", (int)internalError));
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001379D File Offset: 0x0001199D
		public static Exception EnumeratorModified()
		{
			return ExceptionBuilder._InvalidOperation("Collection was modified; enumeration operation might not execute.");
		}
	}
}
