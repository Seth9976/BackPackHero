using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Configuration
{
	// Token: 0x0200019E RID: 414
	internal class ConfigNameValueCollection : NameValueCollection
	{
		// Token: 0x06000ADC RID: 2780 RVA: 0x0002ED4C File Offset: 0x0002CF4C
		public ConfigNameValueCollection()
		{
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0002ED54 File Offset: 0x0002CF54
		public ConfigNameValueCollection(ConfigNameValueCollection col)
			: base(col.Count, col)
		{
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0002ED63 File Offset: 0x0002CF63
		public ConfigNameValueCollection(IHashCodeProvider hashProvider, IComparer comparer)
			: base(hashProvider, comparer)
		{
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002ED6D File Offset: 0x0002CF6D
		public void ResetModified()
		{
			this.modified = false;
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0002ED76 File Offset: 0x0002CF76
		public bool IsModified
		{
			get
			{
				return this.modified;
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002ED7E File Offset: 0x0002CF7E
		public override void Set(string name, string value)
		{
			base.Set(name, value);
			this.modified = true;
		}

		// Token: 0x04000731 RID: 1841
		private bool modified;
	}
}
