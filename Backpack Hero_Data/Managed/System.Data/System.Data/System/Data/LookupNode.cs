using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x020000A5 RID: 165
	internal sealed class LookupNode : ExpressionNode
	{
		// Token: 0x06000AA1 RID: 2721 RVA: 0x00031407 File Offset: 0x0002F607
		internal LookupNode(DataTable table, string columnName, string relationName)
			: base(table)
		{
			this._relationName = relationName;
			this._columnName = columnName;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00031420 File Offset: 0x0002F620
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			this._column = null;
			this._relation = null;
			if (table == null)
			{
				throw ExprException.ExpressionUnbound(this.ToString());
			}
			DataRelationCollection parentRelations = table.ParentRelations;
			if (this._relationName == null)
			{
				if (parentRelations.Count > 1)
				{
					throw ExprException.UnresolvedRelation(table.TableName, this.ToString());
				}
				this._relation = parentRelations[0];
			}
			else
			{
				this._relation = parentRelations[this._relationName];
			}
			if (this._relation == null)
			{
				throw ExprException.BindFailure(this._relationName);
			}
			DataTable parentTable = this._relation.ParentTable;
			this._column = parentTable.Columns[this._columnName];
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

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0003152A File Offset: 0x0002F72A
		internal override object Eval()
		{
			throw ExprException.EvalNoContext();
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00031534 File Offset: 0x0002F734
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			if (this._column == null || this._relation == null)
			{
				throw ExprException.ExpressionUnbound(this.ToString());
			}
			DataRow parentRow = row.GetParentRow(this._relation, version);
			if (parentRow == null)
			{
				return DBNull.Value;
			}
			return parentRow[this._column, parentRow.HasVersion(version) ? version : DataRowVersion.Current];
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0003053E File Offset: 0x0002E73E
		internal override object Eval(int[] recordNos)
		{
			throw ExprException.ComputeNotAggregate(this.ToString());
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00005AE9 File Offset: 0x00003CE9
		internal override bool IsConstant()
		{
			return false;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00005AE9 File Offset: 0x00003CE9
		internal override bool IsTableConstant()
		{
			return false;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00005AE9 File Offset: 0x00003CE9
		internal override bool HasLocalAggregate()
		{
			return false;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00005AE9 File Offset: 0x00003CE9
		internal override bool HasRemoteAggregate()
		{
			return false;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00031591 File Offset: 0x0002F791
		internal override bool DependsOn(DataColumn column)
		{
			return this._column == column;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0000565A File Offset: 0x0000385A
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x040006FE RID: 1790
		private readonly string _relationName;

		// Token: 0x040006FF RID: 1791
		private readonly string _columnName;

		// Token: 0x04000700 RID: 1792
		private DataColumn _column;

		// Token: 0x04000701 RID: 1793
		private DataRelation _relation;
	}
}
