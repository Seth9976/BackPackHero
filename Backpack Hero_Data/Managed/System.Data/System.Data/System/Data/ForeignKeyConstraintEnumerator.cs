using System;

namespace System.Data
{
	// Token: 0x02000044 RID: 68
	internal class ForeignKeyConstraintEnumerator : ConstraintEnumerator
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x0000E4DE File Offset: 0x0000C6DE
		public ForeignKeyConstraintEnumerator(DataSet dataSet)
			: base(dataSet)
		{
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000E4E7 File Offset: 0x0000C6E7
		protected override bool IsValidCandidate(Constraint constraint)
		{
			return constraint is ForeignKeyConstraint;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000E4F2 File Offset: 0x0000C6F2
		public ForeignKeyConstraint GetForeignKeyConstraint()
		{
			return (ForeignKeyConstraint)base.CurrentObject;
		}
	}
}
