using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;

namespace System.Data
{
	// Token: 0x020000A8 RID: 168
	internal sealed class UnaryNode : ExpressionNode
	{
		// Token: 0x06000AC1 RID: 2753 RVA: 0x00031A73 File Offset: 0x0002FC73
		internal UnaryNode(DataTable table, int op, ExpressionNode right)
			: base(table)
		{
			this._op = op;
			this._right = right;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00031A8A File Offset: 0x0002FC8A
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			this._right.Bind(table, list);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002B1D2 File Offset: 0x000293D2
		internal override object Eval()
		{
			return this.Eval(null, DataRowVersion.Default);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00031AA0 File Offset: 0x0002FCA0
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			return this.EvalUnaryOp(this._op, this._right.Eval(row, version));
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00031ABB File Offset: 0x0002FCBB
		internal override object Eval(int[] recordNos)
		{
			return this._right.Eval(recordNos);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00031ACC File Offset: 0x0002FCCC
		private object EvalUnaryOp(int op, object vl)
		{
			object value = DBNull.Value;
			if (DataExpression.IsUnknown(vl))
			{
				return DBNull.Value;
			}
			switch (op)
			{
			case 0:
				return vl;
			case 1:
			{
				StorageType storageType = DataStorage.GetStorageType(vl.GetType());
				if (ExpressionNode.IsNumericSql(storageType))
				{
					switch (storageType)
					{
					case StorageType.Byte:
						return (int)(-(int)((byte)vl));
					case StorageType.Int16:
						return (int)(-(int)((short)vl));
					case StorageType.UInt16:
					case StorageType.UInt32:
					case StorageType.UInt64:
						break;
					case StorageType.Int32:
						return -(int)vl;
					case StorageType.Int64:
						return -(long)vl;
					case StorageType.Single:
						return -(float)vl;
					case StorageType.Double:
						return -(double)vl;
					case StorageType.Decimal:
						return -(decimal)vl;
					default:
						switch (storageType)
						{
						case StorageType.SqlDecimal:
							return -(SqlDecimal)vl;
						case StorageType.SqlDouble:
							return -(SqlDouble)vl;
						case StorageType.SqlInt16:
							return -(SqlInt16)vl;
						case StorageType.SqlInt32:
							return -(SqlInt32)vl;
						case StorageType.SqlInt64:
							return -(SqlInt64)vl;
						case StorageType.SqlMoney:
							return -(SqlMoney)vl;
						case StorageType.SqlSingle:
							return -(SqlSingle)vl;
						}
						break;
					}
					return DBNull.Value;
				}
				throw ExprException.TypeMismatch(this.ToString());
			}
			case 2:
			{
				StorageType storageType = DataStorage.GetStorageType(vl.GetType());
				if (ExpressionNode.IsNumericSql(storageType))
				{
					return vl;
				}
				throw ExprException.TypeMismatch(this.ToString());
			}
			case 3:
				if (vl is SqlBoolean)
				{
					if (((SqlBoolean)vl).IsFalse)
					{
						return SqlBoolean.True;
					}
					if (((SqlBoolean)vl).IsTrue)
					{
						return SqlBoolean.False;
					}
					throw ExprException.UnsupportedOperator(op);
				}
				else
				{
					if (DataExpression.ToBoolean(vl))
					{
						return false;
					}
					return true;
				}
				break;
			default:
				throw ExprException.UnsupportedOperator(op);
			}
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00031D1D File Offset: 0x0002FF1D
		internal override bool IsConstant()
		{
			return this._right.IsConstant();
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00031D2A File Offset: 0x0002FF2A
		internal override bool IsTableConstant()
		{
			return this._right.IsTableConstant();
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00031D37 File Offset: 0x0002FF37
		internal override bool HasLocalAggregate()
		{
			return this._right.HasLocalAggregate();
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00031D44 File Offset: 0x0002FF44
		internal override bool HasRemoteAggregate()
		{
			return this._right.HasRemoteAggregate();
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00031D51 File Offset: 0x0002FF51
		internal override bool DependsOn(DataColumn column)
		{
			return this._right.DependsOn(column);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00031D60 File Offset: 0x0002FF60
		internal override ExpressionNode Optimize()
		{
			this._right = this._right.Optimize();
			if (this.IsConstant())
			{
				object obj = this.Eval();
				return new ConstNode(base.table, ValueType.Object, obj, false);
			}
			return this;
		}

		// Token: 0x04000749 RID: 1865
		internal readonly int _op;

		// Token: 0x0400074A RID: 1866
		internal ExpressionNode _right;
	}
}
