using System;

namespace System.Data
{
	// Token: 0x02000045 RID: 69
	internal sealed class ChildForeignKeyConstraintEnumerator : ForeignKeyConstraintEnumerator
	{
		// Token: 0x060002BB RID: 699 RVA: 0x0000E4FF File Offset: 0x0000C6FF
		public ChildForeignKeyConstraintEnumerator(DataSet dataSet, DataTable inTable)
			: base(dataSet)
		{
			this._table = inTable;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000E50F File Offset: 0x0000C70F
		protected override bool IsValidCandidate(Constraint constraint)
		{
			return constraint is ForeignKeyConstraint && ((ForeignKeyConstraint)constraint).Table == this._table;
		}

		// Token: 0x040004A5 RID: 1189
		private readonly DataTable _table;
	}
}
