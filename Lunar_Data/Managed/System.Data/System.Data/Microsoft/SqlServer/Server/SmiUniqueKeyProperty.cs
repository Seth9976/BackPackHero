using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200039D RID: 925
	internal class SmiUniqueKeyProperty : SmiMetaDataProperty
	{
		// Token: 0x06002C67 RID: 11367 RVA: 0x000C24BB File Offset: 0x000C06BB
		internal SmiUniqueKeyProperty(IList<bool> columnIsKey)
		{
			this._columns = new ReadOnlyCollection<bool>(columnIsKey);
		}

		// Token: 0x17000764 RID: 1892
		internal bool this[int ordinal]
		{
			get
			{
				return this._columns.Count > ordinal && this._columns[ordinal];
			}
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x000094D4 File Offset: 0x000076D4
		[Conditional("DEBUG")]
		internal void CheckCount(int countToMatch)
		{
		}

		// Token: 0x04001B27 RID: 6951
		private IList<bool> _columns;
	}
}
