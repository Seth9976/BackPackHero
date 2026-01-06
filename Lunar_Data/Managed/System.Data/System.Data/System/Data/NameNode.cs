using System;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data
{
	// Token: 0x020000A6 RID: 166
	internal sealed class NameNode : ExpressionNode
	{
		// Token: 0x06000AAC RID: 2732 RVA: 0x0003159F File Offset: 0x0002F79F
		internal NameNode(DataTable table, char[] text, int start, int pos)
			: base(table)
		{
			this._name = NameNode.ParseName(text, start, pos);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000315B7 File Offset: 0x0002F7B7
		internal NameNode(DataTable table, string name)
			: base(table)
		{
			this._name = name;
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x000315C7 File Offset: 0x0002F7C7
		internal override bool IsSqlColumn
		{
			get
			{
				return this._column.IsSqlType;
			}
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000315D4 File Offset: 0x0002F7D4
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			if (table == null)
			{
				throw ExprException.UnboundName(this._name);
			}
			try
			{
				this._column = table.Columns[this._name];
			}
			catch (Exception ex)
			{
				this._found = false;
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw ExprException.UnboundName(this._name);
			}
			if (this._column == null)
			{
				throw ExprException.UnboundName(this._name);
			}
			this._name = this._column.ColumnName;
			this._found = true;
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
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0003152A File Offset: 0x0002F72A
		internal override object Eval()
		{
			throw ExprException.EvalNoContext();
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000316A0 File Offset: 0x0002F8A0
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			if (!this._found)
			{
				throw ExprException.UnboundName(this._name);
			}
			if (row != null)
			{
				return this._column[row.GetRecordFromVersion(version)];
			}
			if (this.IsTableConstant())
			{
				return this._column.DataExpression.Evaluate();
			}
			throw ExprException.UnboundName(this._name);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0003053E File Offset: 0x0002E73E
		internal override object Eval(int[] records)
		{
			throw ExprException.ComputeNotAggregate(this.ToString());
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00005AE9 File Offset: 0x00003CE9
		internal override bool IsConstant()
		{
			return false;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x000316FB File Offset: 0x0002F8FB
		internal override bool IsTableConstant()
		{
			return this._column != null && this._column.Computed && this._column.DataExpression.IsTableAggregate();
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00031724 File Offset: 0x0002F924
		internal override bool HasLocalAggregate()
		{
			return this._column != null && this._column.Computed && this._column.DataExpression.HasLocalAggregate();
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0003174D File Offset: 0x0002F94D
		internal override bool HasRemoteAggregate()
		{
			return this._column != null && this._column.Computed && this._column.DataExpression.HasRemoteAggregate();
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00031776 File Offset: 0x0002F976
		internal override bool DependsOn(DataColumn column)
		{
			return this._column == column || (this._column.Computed && this._column.DataExpression.DependsOn(column));
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0000565A File Offset: 0x0000385A
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x000317A4 File Offset: 0x0002F9A4
		internal static string ParseName(char[] text, int start, int pos)
		{
			char c = '\0';
			string text2 = string.Empty;
			int num = start;
			int num2 = pos;
			checked
			{
				if (text[start] == '`')
				{
					start++;
					pos--;
					c = '\\';
					text2 = "`";
				}
				else if (text[start] == '[')
				{
					start++;
					pos--;
					c = '\\';
					text2 = "]\\";
				}
			}
			if (c != '\0')
			{
				int num3 = start;
				for (int i = start; i < pos; i++)
				{
					if (text[i] == c && i + 1 < pos && text2.IndexOf(text[i + 1]) >= 0)
					{
						i++;
					}
					text[num3] = text[i];
					num3++;
				}
				pos = num3;
			}
			if (pos == start)
			{
				throw ExprException.InvalidName(new string(text, num, num2 - num));
			}
			return new string(text, start, pos - start);
		}

		// Token: 0x04000702 RID: 1794
		internal char _open;

		// Token: 0x04000703 RID: 1795
		internal char _close;

		// Token: 0x04000704 RID: 1796
		internal string _name;

		// Token: 0x04000705 RID: 1797
		internal bool _found;

		// Token: 0x04000706 RID: 1798
		internal bool _type;

		// Token: 0x04000707 RID: 1799
		internal DataColumn _column;
	}
}
