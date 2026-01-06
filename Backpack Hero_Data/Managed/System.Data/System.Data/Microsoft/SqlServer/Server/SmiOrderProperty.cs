using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200039E RID: 926
	internal class SmiOrderProperty : SmiMetaDataProperty
	{
		// Token: 0x06002C6A RID: 11370 RVA: 0x000C24ED File Offset: 0x000C06ED
		internal SmiOrderProperty(IList<SmiOrderProperty.SmiColumnOrder> columnOrders)
		{
			this._columns = new ReadOnlyCollection<SmiOrderProperty.SmiColumnOrder>(columnOrders);
		}

		// Token: 0x17000765 RID: 1893
		internal SmiOrderProperty.SmiColumnOrder this[int ordinal]
		{
			get
			{
				if (this._columns.Count <= ordinal)
				{
					return new SmiOrderProperty.SmiColumnOrder
					{
						Order = SortOrder.Unspecified,
						SortOrdinal = -1
					};
				}
				return this._columns[ordinal];
			}
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x000094D4 File Offset: 0x000076D4
		[Conditional("DEBUG")]
		internal void CheckCount(int countToMatch)
		{
		}

		// Token: 0x04001B28 RID: 6952
		private IList<SmiOrderProperty.SmiColumnOrder> _columns;

		// Token: 0x0200039F RID: 927
		internal struct SmiColumnOrder
		{
			// Token: 0x04001B29 RID: 6953
			internal int SortOrdinal;

			// Token: 0x04001B2A RID: 6954
			internal SortOrder Order;
		}
	}
}
