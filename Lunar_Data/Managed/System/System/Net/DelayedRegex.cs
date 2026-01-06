using System;
using System.Text.RegularExpressions;

namespace System.Net
{
	// Token: 0x02000421 RID: 1057
	[Serializable]
	internal class DelayedRegex
	{
		// Token: 0x06002197 RID: 8599 RVA: 0x0007AEBC File Offset: 0x000790BC
		internal DelayedRegex(string regexString)
		{
			if (regexString == null)
			{
				throw new ArgumentNullException("regexString");
			}
			this._AsString = regexString;
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x0007AED9 File Offset: 0x000790D9
		internal DelayedRegex(Regex regex)
		{
			if (regex == null)
			{
				throw new ArgumentNullException("regex");
			}
			this._AsRegex = regex;
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06002199 RID: 8601 RVA: 0x0007AEF6 File Offset: 0x000790F6
		internal Regex AsRegex
		{
			get
			{
				if (this._AsRegex == null)
				{
					this._AsRegex = new Regex(this._AsString + "[/]?", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);
				}
				return this._AsRegex;
			}
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x0007AF28 File Offset: 0x00079128
		public override string ToString()
		{
			if (this._AsString == null)
			{
				return this._AsString = this._AsRegex.ToString();
			}
			return this._AsString;
		}

		// Token: 0x04001367 RID: 4967
		private Regex _AsRegex;

		// Token: 0x04001368 RID: 4968
		private string _AsString;
	}
}
