using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x020001F1 RID: 497
	internal class MatchSparse : Match
	{
		// Token: 0x06000D33 RID: 3379 RVA: 0x000361B6 File Offset: 0x000343B6
		internal MatchSparse(Regex regex, Hashtable caps, int capcount, string text, int begpos, int len, int startpos)
			: base(regex, capcount, text, begpos, len, startpos)
		{
			this._caps = caps;
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x000361CF File Offset: 0x000343CF
		public override GroupCollection Groups
		{
			get
			{
				if (this._groupcoll == null)
				{
					this._groupcoll = new GroupCollection(this, this._caps);
				}
				return this._groupcoll;
			}
		}

		// Token: 0x040007F4 RID: 2036
		internal new readonly Hashtable _caps;
	}
}
