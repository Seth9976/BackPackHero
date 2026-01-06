using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000144 RID: 324
	internal sealed class Row
	{
		// Token: 0x06001074 RID: 4212 RVA: 0x00050D48 File Offset: 0x0004EF48
		internal Row(int rowCount)
		{
			this._dataFields = new object[rowCount];
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x00050D5C File Offset: 0x0004EF5C
		internal object[] DataFields
		{
			get
			{
				return this._dataFields;
			}
		}

		// Token: 0x170002E5 RID: 741
		internal object this[int index]
		{
			get
			{
				return this._dataFields[index];
			}
		}

		// Token: 0x04000AD5 RID: 2773
		private object[] _dataFields;
	}
}
