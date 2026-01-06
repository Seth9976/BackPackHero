using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	// Token: 0x02000096 RID: 150
	internal abstract class ExpressionNode
	{
		// Token: 0x06000A15 RID: 2581 RVA: 0x0002E2D8 File Offset: 0x0002C4D8
		protected ExpressionNode(DataTable table)
		{
			this._table = table;
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0002E2E8 File Offset: 0x0002C4E8
		internal IFormatProvider FormatProvider
		{
			get
			{
				if (this._table == null)
				{
					return CultureInfo.CurrentCulture;
				}
				return this._table.FormatProvider;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x00005AE9 File Offset: 0x00003CE9
		internal virtual bool IsSqlColumn
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0002E310 File Offset: 0x0002C510
		protected DataTable table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0002E318 File Offset: 0x0002C518
		protected void BindTable(DataTable table)
		{
			this._table = table;
		}

		// Token: 0x06000A1A RID: 2586
		internal abstract void Bind(DataTable table, List<DataColumn> list);

		// Token: 0x06000A1B RID: 2587
		internal abstract object Eval();

		// Token: 0x06000A1C RID: 2588
		internal abstract object Eval(DataRow row, DataRowVersion version);

		// Token: 0x06000A1D RID: 2589
		internal abstract object Eval(int[] recordNos);

		// Token: 0x06000A1E RID: 2590
		internal abstract bool IsConstant();

		// Token: 0x06000A1F RID: 2591
		internal abstract bool IsTableConstant();

		// Token: 0x06000A20 RID: 2592
		internal abstract bool HasLocalAggregate();

		// Token: 0x06000A21 RID: 2593
		internal abstract bool HasRemoteAggregate();

		// Token: 0x06000A22 RID: 2594
		internal abstract ExpressionNode Optimize();

		// Token: 0x06000A23 RID: 2595 RVA: 0x00005AE9 File Offset: 0x00003CE9
		internal virtual bool DependsOn(DataColumn column)
		{
			return false;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0002E321 File Offset: 0x0002C521
		internal static bool IsInteger(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.SByte || type == StorageType.Byte;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0002E349 File Offset: 0x0002C549
		internal static bool IsIntegerSql(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.SByte || type == StorageType.Byte || type == StorageType.SqlInt64 || type == StorageType.SqlInt32 || type == StorageType.SqlInt16 || type == StorageType.SqlByte;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002E385 File Offset: 0x0002C585
		internal static bool IsSigned(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.SByte || ExpressionNode.IsFloat(type);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0002E3A1 File Offset: 0x0002C5A1
		internal static bool IsSignedSql(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.SByte || type == StorageType.SqlInt64 || type == StorageType.SqlInt32 || type == StorageType.SqlInt16 || ExpressionNode.IsFloatSql(type);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0002E3CC File Offset: 0x0002C5CC
		internal static bool IsUnsigned(StorageType type)
		{
			return type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.Byte;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002E3E2 File Offset: 0x0002C5E2
		internal static bool IsUnsignedSql(StorageType type)
		{
			return type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.SqlByte || type == StorageType.Byte;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0002E3FD File Offset: 0x0002C5FD
		internal static bool IsNumeric(StorageType type)
		{
			return ExpressionNode.IsFloat(type) || ExpressionNode.IsInteger(type);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0002E40F File Offset: 0x0002C60F
		internal static bool IsNumericSql(StorageType type)
		{
			return ExpressionNode.IsFloatSql(type) || ExpressionNode.IsIntegerSql(type);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0002E421 File Offset: 0x0002C621
		internal static bool IsFloat(StorageType type)
		{
			return type == StorageType.Single || type == StorageType.Double || type == StorageType.Decimal;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0002E434 File Offset: 0x0002C634
		internal static bool IsFloatSql(StorageType type)
		{
			return type == StorageType.Single || type == StorageType.Double || type == StorageType.Decimal || type == StorageType.SqlDouble || type == StorageType.SqlDecimal || type == StorageType.SqlMoney || type == StorageType.SqlSingle;
		}

		// Token: 0x04000681 RID: 1665
		private DataTable _table;
	}
}
