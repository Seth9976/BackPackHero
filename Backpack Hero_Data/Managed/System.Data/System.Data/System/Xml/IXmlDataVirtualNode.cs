using System;
using System.Data;

namespace System.Xml
{
	// Token: 0x0200002B RID: 43
	internal interface IXmlDataVirtualNode
	{
		// Token: 0x0600014A RID: 330
		bool IsOnNode(XmlNode nodeToCheck);

		// Token: 0x0600014B RID: 331
		bool IsOnColumn(DataColumn col);

		// Token: 0x0600014C RID: 332
		bool IsInUse();

		// Token: 0x0600014D RID: 333
		void OnFoliated(XmlNode foliatedNode);
	}
}
