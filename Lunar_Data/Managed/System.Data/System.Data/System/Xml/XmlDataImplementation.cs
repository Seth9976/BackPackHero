using System;

namespace System.Xml
{
	// Token: 0x02000033 RID: 51
	internal sealed class XmlDataImplementation : XmlImplementation
	{
		// Token: 0x0600023D RID: 573 RVA: 0x0000C918 File Offset: 0x0000AB18
		public override XmlDocument CreateDocument()
		{
			return new XmlDataDocument(this);
		}
	}
}
