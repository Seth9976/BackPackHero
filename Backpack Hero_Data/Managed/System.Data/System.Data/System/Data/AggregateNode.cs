using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x02000090 RID: 144
	internal sealed class AggregateNode : ExpressionNode
	{
		// Token: 0x060009CF RID: 2511 RVA: 0x0002AF9A File Offset: 0x0002919A
		internal AggregateNode(DataTable table, FunctionId aggregateType, string columnName)
			: this(table, aggregateType, columnName, true, null)
		{
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0002AFA8 File Offset: 0x000291A8
		internal AggregateNode(DataTable table, FunctionId aggregateType, string columnName, bool local, string relationName)
			: base(table)
		{
			this._aggregate = (Aggregate)aggregateType;
			if (aggregateType == FunctionId.Sum)
			{
				this._type = AggregateType.Sum;
			}
			else if (aggregateType == FunctionId.Avg)
			{
				this._type = AggregateType.Mean;
			}
			else if (aggregateType == FunctionId.Min)
			{
				this._type = AggregateType.Min;
			}
			else if (aggregateType == FunctionId.Max)
			{
				this._type = AggregateType.Max;
			}
			else if (aggregateType == FunctionId.Count)
			{
				this._type = AggregateType.Count;
			}
			else if (aggregateType == FunctionId.Var)
			{
				this._type = AggregateType.Var;
			}
			else
			{
				if (aggregateType != FunctionId.StDev)
				{
					throw ExprException.UndefinedFunction(Function.s_functionName[(int)aggregateType]);
				}
				this._type = AggregateType.StDev;
			}
			this._local = local;
			this._relationName = relationName;
			this._columnName = columnName;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0002B04C File Offset: 0x0002924C
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			if (table == null)
			{
				throw ExprException.AggregateUnbound(this.ToString());
			}
			if (this._local)
			{
				this._relation = null;
			}
			else
			{
				DataRelationCollection childRelations = table.ChildRelations;
				if (this._relationName == null)
				{
					if (childRelations.Count > 1)
					{
						throw ExprException.UnresolvedRelation(table.TableName, this.ToString());
					}
					if (childRelations.Count != 1)
					{
						throw ExprException.AggregateUnbound(this.ToString());
					}
					this._relation = childRelations[0];
				}
				else
				{
					this._relation = childRelations[this._relationName];
				}
			}
			this._childTable = ((this._relation == null) ? table : this._relation.ChildTable);
			this._column = this._childTable.Columns[this._columnName];
			if (this._column == null)
			{
				throw ExprException.UnboundName(this._columnName);
			}
			int i;
			for (i = 0; i < list.Count; i++)
			{
				DataColumn dataColumn = list[i];
				if (this._column == dataColumn)
				{
					break;
				}
			}
			if (i >= list.Count)
			{
				list.Add(this._column);
			}
			AggregateNode.Bind(this._relation, list);
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0002B170 File Offset: 0x00029370
		internal static void Bind(DataRelation relation, List<DataColumn> list)
		{
			if (relation != null)
			{
				foreach (DataColumn dataColumn in relation.ChildColumnsReference)
				{
					if (!list.Contains(dataColumn))
					{
						list.Add(dataColumn);
					}
				}
				foreach (DataColumn dataColumn2 in relation.ParentColumnsReference)
				{
					if (!list.Contains(dataColumn2))
					{
						list.Add(dataColumn2);
					}
				}
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0002B1D2 File Offset: 0x000293D2
		internal override object Eval()
		{
			return this.Eval(null, DataRowVersion.Default);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0002B1E0 File Offset: 0x000293E0
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			if (this._childTable == null)
			{
				throw ExprException.AggregateUnbound(this.ToString());
			}
			DataRow[] array;
			if (this._local)
			{
				array = new DataRow[this._childTable.Rows.Count];
				this._childTable.Rows.CopyTo(array, 0);
			}
			else
			{
				if (row == null)
				{
					throw ExprException.EvalNoContext();
				}
				if (this._relation == null)
				{
					throw ExprException.AggregateUnbound(this.ToString());
				}
				array = row.GetChildRows(this._relation, version);
			}
			if (version == DataRowVersion.Proposed)
			{
				version = DataRowVersion.Default;
			}
			List<int> list = new List<int>();
			int i = 0;
			while (i < array.Length)
			{
				if (array[i].RowState == DataRowState.Deleted)
				{
					if (DataRowAction.Rollback == array[i]._action)
					{
						version = DataRowVersion.Original;
						goto IL_00BF;
					}
				}
				else if (DataRowAction.Rollback != array[i]._action || array[i].RowState != DataRowState.Added)
				{
					goto IL_00BF;
				}
				IL_00E1:
				i++;
				continue;
				IL_00BF:
				if (version != DataRowVersion.Original || array[i]._oldRecord != -1)
				{
					list.Add(array[i].GetRecordFromVersion(version));
					goto IL_00E1;
				}
				goto IL_00E1;
			}
			int[] array2 = list.ToArray();
			return this._column.GetAggregateValue(array2, this._type);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0002B2F1 File Offset: 0x000294F1
		internal override object Eval(int[] records)
		{
			if (this._childTable == null)
			{
				throw ExprException.AggregateUnbound(this.ToString());
			}
			if (!this._local)
			{
				throw ExprException.ComputeNotAggregate(this.ToString());
			}
			return this._column.GetAggregateValue(records, this._type);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00005AE9 File Offset: 0x00003CE9
		internal override bool IsConstant()
		{
			return false;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0002B32D File Offset: 0x0002952D
		internal override bool IsTableConstant()
		{
			return this._local;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0002B32D File Offset: 0x0002952D
		internal override bool HasLocalAggregate()
		{
			return this._local;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0002B335 File Offset: 0x00029535
		internal override bool HasRemoteAggregate()
		{
			return !this._local;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0002B340 File Offset: 0x00029540
		internal override bool DependsOn(DataColumn column)
		{
			return this._column == column || (this._column.Computed && this._column.DataExpression.DependsOn(column));
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0000565A File Offset: 0x0000385A
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x04000643 RID: 1603
		private readonly AggregateType _type;

		// Token: 0x04000644 RID: 1604
		private readonly Aggregate _aggregate;

		// Token: 0x04000645 RID: 1605
		private readonly bool _local;

		// Token: 0x04000646 RID: 1606
		private readonly string _relationName;

		// Token: 0x04000647 RID: 1607
		private readonly string _columnName;

		// Token: 0x04000648 RID: 1608
		private DataTable _childTable;

		// Token: 0x04000649 RID: 1609
		private DataColumn _column;

		// Token: 0x0400064A RID: 1610
		private DataRelation _relation;
	}
}
