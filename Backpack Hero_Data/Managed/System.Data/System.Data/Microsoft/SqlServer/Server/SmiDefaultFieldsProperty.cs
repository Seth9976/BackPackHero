using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003A0 RID: 928
	internal class SmiDefaultFieldsProperty : SmiMetaDataProperty
	{
		// Token: 0x06002C6D RID: 11373 RVA: 0x000C2545 File Offset: 0x000C0745
		internal SmiDefaultFieldsProperty(IList<bool> defaultFields)
		{
			this._defaults = new ReadOnlyCollection<bool>(defaultFields);
		}

		// Token: 0x17000766 RID: 1894
		internal bool this[int ordinal]
		{
			get
			{
				return this._defaults.Count > ordinal && this._defaults[ordinal];
			}
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x000094D4 File Offset: 0x000076D4
		[Conditional("DEBUG")]
		internal void CheckCount(int countToMatch)
		{
		}

		// Token: 0x04001B2B RID: 6955
		private IList<bool> _defaults;
	}
}
