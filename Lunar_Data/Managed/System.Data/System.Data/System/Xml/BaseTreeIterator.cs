using System;

namespace System.Xml
{
	// Token: 0x02000027 RID: 39
	internal abstract class BaseTreeIterator
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00005761 File Offset: 0x00003961
		internal BaseTreeIterator(DataSetMapper mapper)
		{
			this.mapper = mapper;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000E5 RID: 229
		internal abstract XmlNode CurrentNode { get; }

		// Token: 0x060000E6 RID: 230
		internal abstract bool Next();

		// Token: 0x060000E7 RID: 231
		internal abstract bool NextRight();

		// Token: 0x060000E8 RID: 232 RVA: 0x00005770 File Offset: 0x00003970
		internal bool NextRowElement()
		{
			while (this.Next())
			{
				if (this.OnRowElement())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005787 File Offset: 0x00003987
		internal bool NextRightRowElement()
		{
			return this.NextRight() && (this.OnRowElement() || this.NextRowElement());
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000057A4 File Offset: 0x000039A4
		internal bool OnRowElement()
		{
			XmlBoundElement xmlBoundElement = this.CurrentNode as XmlBoundElement;
			return xmlBoundElement != null && xmlBoundElement.Row != null;
		}

		// Token: 0x04000429 RID: 1065
		protected DataSetMapper mapper;
	}
}
