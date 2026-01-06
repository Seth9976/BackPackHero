using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;

namespace System.Data
{
	// Token: 0x02000095 RID: 149
	internal sealed class DataExpression : IFilter
	{
		// Token: 0x06000A03 RID: 2563 RVA: 0x0002DEEC File Offset: 0x0002C0EC
		internal DataExpression(DataTable table, string expression)
			: this(table, expression, null)
		{
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0002DEF8 File Offset: 0x0002C0F8
		internal DataExpression(DataTable table, string expression, Type type)
		{
			ExpressionParser expressionParser = new ExpressionParser(table);
			expressionParser.LoadExpression(expression);
			this._originalExpression = expression;
			this._expr = null;
			if (expression != null)
			{
				this._storageType = DataStorage.GetStorageType(type);
				if (this._storageType == StorageType.BigInteger)
				{
					throw ExprException.UnsupportedDataType(type);
				}
				this._dataType = type;
				this._expr = expressionParser.Parse();
				this._parsed = true;
				if (this._expr != null && table != null)
				{
					this.Bind(table);
					return;
				}
				this._bound = false;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0002DF86 File Offset: 0x0002C186
		internal string Expression
		{
			get
			{
				if (this._originalExpression == null)
				{
					return "";
				}
				return this._originalExpression;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0002DF9C File Offset: 0x0002C19C
		internal ExpressionNode ExpressionNode
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0002DFA4 File Offset: 0x0002C1A4
		internal bool HasValue
		{
			get
			{
				return this._expr != null;
			}
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0002DFB0 File Offset: 0x0002C1B0
		internal void Bind(DataTable table)
		{
			this._table = table;
			if (table == null)
			{
				return;
			}
			if (this._expr != null)
			{
				List<DataColumn> list = new List<DataColumn>();
				this._expr.Bind(table, list);
				this._expr = this._expr.Optimize();
				this._table = table;
				this._bound = true;
				this._dependency = list.ToArray();
			}
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0002E00E File Offset: 0x0002C20E
		internal bool DependsOn(DataColumn column)
		{
			return this._expr != null && this._expr.DependsOn(column);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0002E026 File Offset: 0x0002C226
		internal object Evaluate()
		{
			return this.Evaluate(null, DataRowVersion.Default);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0002E034 File Offset: 0x0002C234
		internal object Evaluate(DataRow row, DataRowVersion version)
		{
			if (!this._bound)
			{
				this.Bind(this._table);
			}
			object obj;
			if (this._expr != null)
			{
				obj = this._expr.Eval(row, version);
				if (obj == DBNull.Value && StorageType.Uri >= this._storageType)
				{
					return obj;
				}
				try
				{
					if (StorageType.Object != this._storageType)
					{
						obj = SqlConvert.ChangeType2(obj, this._storageType, this._dataType, this._table.FormatProvider);
					}
					return obj;
				}
				catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
				{
					ExceptionBuilder.TraceExceptionForCapture(ex);
					throw ExprException.DatavalueConvertion(obj, this._dataType, ex);
				}
			}
			obj = null;
			return obj;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0002E0E8 File Offset: 0x0002C2E8
		internal object Evaluate(DataRow[] rows)
		{
			return this.Evaluate(rows, DataRowVersion.Default);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0002E0F8 File Offset: 0x0002C2F8
		internal object Evaluate(DataRow[] rows, DataRowVersion version)
		{
			if (!this._bound)
			{
				this.Bind(this._table);
			}
			if (this._expr != null)
			{
				List<int> list = new List<int>();
				foreach (DataRow dataRow in rows)
				{
					if (dataRow.RowState != DataRowState.Deleted && (version != DataRowVersion.Original || dataRow._oldRecord != -1))
					{
						list.Add(dataRow.GetRecordFromVersion(version));
					}
				}
				int[] array = list.ToArray();
				return this._expr.Eval(array);
			}
			return DBNull.Value;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0002E180 File Offset: 0x0002C380
		public bool Invoke(DataRow row, DataRowVersion version)
		{
			if (this._expr == null)
			{
				return true;
			}
			if (row == null)
			{
				throw ExprException.InvokeArgument();
			}
			object obj = this._expr.Eval(row, version);
			bool flag;
			try
			{
				flag = DataExpression.ToBoolean(obj);
			}
			catch (EvaluateException)
			{
				throw ExprException.FilterConvertion(this.Expression);
			}
			return flag;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0002E1D8 File Offset: 0x0002C3D8
		internal DataColumn[] GetDependency()
		{
			return this._dependency;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0002E1E0 File Offset: 0x0002C3E0
		internal bool IsTableAggregate()
		{
			return this._expr != null && this._expr.IsTableConstant();
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002E1F7 File Offset: 0x0002C3F7
		internal static bool IsUnknown(object value)
		{
			return DataStorage.IsObjectNull(value);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002E1FF File Offset: 0x0002C3FF
		internal bool HasLocalAggregate()
		{
			return this._expr != null && this._expr.HasLocalAggregate();
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0002E216 File Offset: 0x0002C416
		internal bool HasRemoteAggregate()
		{
			return this._expr != null && this._expr.HasRemoteAggregate();
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0002E230 File Offset: 0x0002C430
		internal static bool ToBoolean(object value)
		{
			if (DataExpression.IsUnknown(value))
			{
				return false;
			}
			if (value is bool)
			{
				return (bool)value;
			}
			if (value is SqlBoolean)
			{
				return ((SqlBoolean)value).IsTrue;
			}
			if (value is string)
			{
				try
				{
					return bool.Parse((string)value);
				}
				catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
				{
					ExceptionBuilder.TraceExceptionForCapture(ex);
					throw ExprException.DatavalueConvertion(value, typeof(bool), ex);
				}
			}
			throw ExprException.DatavalueConvertion(value, typeof(bool), null);
		}

		// Token: 0x04000679 RID: 1657
		internal string _originalExpression;

		// Token: 0x0400067A RID: 1658
		private bool _parsed;

		// Token: 0x0400067B RID: 1659
		private bool _bound;

		// Token: 0x0400067C RID: 1660
		private ExpressionNode _expr;

		// Token: 0x0400067D RID: 1661
		private DataTable _table;

		// Token: 0x0400067E RID: 1662
		private readonly StorageType _storageType;

		// Token: 0x0400067F RID: 1663
		private readonly Type _dataType;

		// Token: 0x04000680 RID: 1664
		private DataColumn[] _dependency = Array.Empty<DataColumn>();
	}
}
