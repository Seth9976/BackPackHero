using System;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="E:System.Data.Common.DataAdapter.FillError" /> event of a <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200008D RID: 141
	public class FillErrorEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.FillErrorEventArgs" /> class.</summary>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> being updated. </param>
		/// <param name="values">The values for the row being updated. </param>
		// Token: 0x060009C4 RID: 2500 RVA: 0x0002AF0B File Offset: 0x0002910B
		public FillErrorEventArgs(DataTable dataTable, object[] values)
		{
			this._dataTable = dataTable;
			this._values = values;
			if (this._values == null)
			{
				this._values = Array.Empty<object>();
			}
		}

		/// <summary>Gets or sets a value indicating whether to continue the fill operation despite the error.</summary>
		/// <returns>true if the fill operation should continue; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0002AF34 File Offset: 0x00029134
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x0002AF3C File Offset: 0x0002913C
		public bool Continue
		{
			get
			{
				return this._continueFlag;
			}
			set
			{
				this._continueFlag = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> being updated when the error occurred.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> being updated.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0002AF45 File Offset: 0x00029145
		public DataTable DataTable
		{
			get
			{
				return this._dataTable;
			}
		}

		/// <summary>Gets the errors being handled.</summary>
		/// <returns>The errors being handled.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0002AF4D File Offset: 0x0002914D
		// (set) Token: 0x060009C9 RID: 2505 RVA: 0x0002AF55 File Offset: 0x00029155
		public Exception Errors
		{
			get
			{
				return this._errors;
			}
			set
			{
				this._errors = value;
			}
		}

		/// <summary>Gets the values for the row being updated when the error occurred.</summary>
		/// <returns>The values for the row being updated.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x0002AF60 File Offset: 0x00029160
		public object[] Values
		{
			get
			{
				object[] array = new object[this._values.Length];
				for (int i = 0; i < this._values.Length; i++)
				{
					array[i] = this._values[i];
				}
				return array;
			}
		}

		// Token: 0x04000636 RID: 1590
		private bool _continueFlag;

		// Token: 0x04000637 RID: 1591
		private DataTable _dataTable;

		// Token: 0x04000638 RID: 1592
		private Exception _errors;

		// Token: 0x04000639 RID: 1593
		private object[] _values;
	}
}
