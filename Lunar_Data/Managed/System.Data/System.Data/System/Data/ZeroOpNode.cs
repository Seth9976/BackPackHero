using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x020000A9 RID: 169
	internal sealed class ZeroOpNode : ExpressionNode
	{
		// Token: 0x06000ACD RID: 2765 RVA: 0x00031D9D File Offset: 0x0002FF9D
		internal ZeroOpNode(int op)
			: base(null)
		{
			this._op = op;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x000094D4 File Offset: 0x000076D4
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00031DB0 File Offset: 0x0002FFB0
		internal override object Eval()
		{
			switch (this._op)
			{
			case 32:
				return DBNull.Value;
			case 33:
				return true;
			case 34:
				return false;
			default:
				return DBNull.Value;
			}
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0002DBF2 File Offset: 0x0002BDF2
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			return this.Eval();
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002DBF2 File Offset: 0x0002BDF2
		internal override object Eval(int[] recordNos)
		{
			return this.Eval();
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0000CD07 File Offset: 0x0000AF07
		internal override bool IsConstant()
		{
			return true;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0000CD07 File Offset: 0x0000AF07
		internal override bool IsTableConstant()
		{
			return true;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00005AE9 File Offset: 0x00003CE9
		internal override bool HasLocalAggregate()
		{
			return false;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00005AE9 File Offset: 0x00003CE9
		internal override bool HasRemoteAggregate()
		{
			return false;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0000565A File Offset: 0x0000385A
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x0400074B RID: 1867
		internal readonly int _op;

		// Token: 0x0400074C RID: 1868
		internal const int zop_True = 1;

		// Token: 0x0400074D RID: 1869
		internal const int zop_False = 0;

		// Token: 0x0400074E RID: 1870
		internal const int zop_Null = -1;
	}
}
