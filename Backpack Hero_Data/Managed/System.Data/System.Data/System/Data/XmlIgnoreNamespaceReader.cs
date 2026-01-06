using System;
using System.Collections.Generic;
using System.Xml;

namespace System.Data
{
	// Token: 0x020000EF RID: 239
	internal sealed class XmlIgnoreNamespaceReader : XmlNodeReader
	{
		// Token: 0x06000D36 RID: 3382 RVA: 0x00040DBA File Offset: 0x0003EFBA
		internal XmlIgnoreNamespaceReader(XmlDocument xdoc, string[] namespacesToIgnore)
			: base(xdoc)
		{
			this._namespacesToIgnore = new List<string>(namespacesToIgnore);
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00040DD0 File Offset: 0x0003EFD0
		public override bool MoveToFirstAttribute()
		{
			return base.MoveToFirstAttribute() && ((!this._namespacesToIgnore.Contains(this.NamespaceURI) && (!(this.NamespaceURI == "http://www.w3.org/XML/1998/namespace") || !(this.LocalName != "lang"))) || this.MoveToNextAttribute());
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00040E28 File Offset: 0x0003F028
		public override bool MoveToNextAttribute()
		{
			bool flag;
			bool flag2;
			do
			{
				flag = false;
				flag2 = false;
				if (base.MoveToNextAttribute())
				{
					flag = true;
					if (this._namespacesToIgnore.Contains(this.NamespaceURI) || (this.NamespaceURI == "http://www.w3.org/XML/1998/namespace" && this.LocalName != "lang"))
					{
						flag2 = true;
					}
				}
			}
			while (flag2);
			return flag;
		}

		// Token: 0x04000868 RID: 2152
		private List<string> _namespacesToIgnore;
	}
}
