using System;
using System.Collections;

namespace System.Data
{
	// Token: 0x02000043 RID: 67
	internal class ConstraintEnumerator
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x0000E419 File Offset: 0x0000C619
		public ConstraintEnumerator(DataSet dataSet)
		{
			this._tables = ((dataSet != null) ? dataSet.Tables.GetEnumerator() : null);
			this._currentObject = null;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000E440 File Offset: 0x0000C640
		public bool GetNext()
		{
			this._currentObject = null;
			while (this._tables != null)
			{
				if (this._constraints == null)
				{
					if (!this._tables.MoveNext())
					{
						this._tables = null;
						return false;
					}
					this._constraints = ((DataTable)this._tables.Current).Constraints.GetEnumerator();
				}
				if (!this._constraints.MoveNext())
				{
					this._constraints = null;
				}
				else
				{
					Constraint constraint = (Constraint)this._constraints.Current;
					if (this.IsValidCandidate(constraint))
					{
						this._currentObject = constraint;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000E4D6 File Offset: 0x0000C6D6
		public Constraint GetConstraint()
		{
			return this._currentObject;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000CD07 File Offset: 0x0000AF07
		protected virtual bool IsValidCandidate(Constraint constraint)
		{
			return true;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000E4D6 File Offset: 0x0000C6D6
		protected Constraint CurrentObject
		{
			get
			{
				return this._currentObject;
			}
		}

		// Token: 0x040004A2 RID: 1186
		private IEnumerator _tables;

		// Token: 0x040004A3 RID: 1187
		private IEnumerator _constraints;

		// Token: 0x040004A4 RID: 1188
		private Constraint _currentObject;
	}
}
