using System;

namespace System.Data
{
	// Token: 0x02000046 RID: 70
	internal sealed class ParentForeignKeyConstraintEnumerator : ForeignKeyConstraintEnumerator
	{
		// Token: 0x060002BD RID: 701 RVA: 0x0000E52E File Offset: 0x0000C72E
		public ParentForeignKeyConstraintEnumerator(DataSet dataSet, DataTable inTable)
			: base(dataSet)
		{
			this._table = inTable;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000E53E File Offset: 0x0000C73E
		protected override bool IsValidCandidate(Constraint constraint)
		{
			return constraint is ForeignKeyConstraint && ((ForeignKeyConstraint)constraint).RelatedTable == this._table;
		}

		// Token: 0x040004A6 RID: 1190
		private readonly DataTable _table;
	}
}
