using System;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000203 RID: 515
	internal sealed class RegexFC
	{
		// Token: 0x06000E67 RID: 3687 RVA: 0x0003E44F File Offset: 0x0003C64F
		public RegexFC(bool nullable)
		{
			this._cc = new RegexCharClass();
			this._nullable = nullable;
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0003E46C File Offset: 0x0003C66C
		public RegexFC(char ch, bool not, bool nullable, bool caseInsensitive)
		{
			this._cc = new RegexCharClass();
			if (not)
			{
				if (ch > '\0')
				{
					this._cc.AddRange('\0', ch - '\u0001');
				}
				if (ch < '\uffff')
				{
					this._cc.AddRange(ch + '\u0001', char.MaxValue);
				}
			}
			else
			{
				this._cc.AddRange(ch, ch);
			}
			this.CaseInsensitive = caseInsensitive;
			this._nullable = nullable;
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0003E4DB File Offset: 0x0003C6DB
		public RegexFC(string charClass, bool nullable, bool caseInsensitive)
		{
			this._cc = RegexCharClass.Parse(charClass);
			this._nullable = nullable;
			this.CaseInsensitive = caseInsensitive;
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0003E500 File Offset: 0x0003C700
		public bool AddFC(RegexFC fc, bool concatenate)
		{
			if (!this._cc.CanMerge || !fc._cc.CanMerge)
			{
				return false;
			}
			if (concatenate)
			{
				if (!this._nullable)
				{
					return true;
				}
				if (!fc._nullable)
				{
					this._nullable = false;
				}
			}
			else if (fc._nullable)
			{
				this._nullable = true;
			}
			this.CaseInsensitive |= fc.CaseInsensitive;
			this._cc.AddCharClass(fc._cc);
			return true;
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x0003E57B File Offset: 0x0003C77B
		// (set) Token: 0x06000E6C RID: 3692 RVA: 0x0003E583 File Offset: 0x0003C783
		public bool CaseInsensitive { get; private set; }

		// Token: 0x06000E6D RID: 3693 RVA: 0x0003E58C File Offset: 0x0003C78C
		public string GetFirstChars(CultureInfo culture)
		{
			if (this.CaseInsensitive)
			{
				this._cc.AddLowercase(culture);
			}
			return this._cc.ToStringClass();
		}

		// Token: 0x040008FE RID: 2302
		private RegexCharClass _cc;

		// Token: 0x040008FF RID: 2303
		public bool _nullable;
	}
}
