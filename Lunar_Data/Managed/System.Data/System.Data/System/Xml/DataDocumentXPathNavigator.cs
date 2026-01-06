using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000028 RID: 40
	internal sealed class DataDocumentXPathNavigator : XPathNavigator, IHasXmlNode
	{
		// Token: 0x060000EB RID: 235 RVA: 0x000057CB File Offset: 0x000039CB
		internal DataDocumentXPathNavigator(XmlDataDocument doc, XmlNode node)
		{
			this._curNode = new XPathNodePointer(this, doc, node);
			this._temp = new XPathNodePointer(this, doc, node);
			this._doc = doc;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000057F6 File Offset: 0x000039F6
		private DataDocumentXPathNavigator(DataDocumentXPathNavigator other)
		{
			this._curNode = other._curNode.Clone(this);
			this._temp = other._temp.Clone(this);
			this._doc = other._doc;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000582E File Offset: 0x00003A2E
		public override XPathNavigator Clone()
		{
			return new DataDocumentXPathNavigator(this);
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00005836 File Offset: 0x00003A36
		internal XPathNodePointer CurNode
		{
			get
			{
				return this._curNode;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000583E File Offset: 0x00003A3E
		internal XmlDataDocument Document
		{
			get
			{
				return this._doc;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00005846 File Offset: 0x00003A46
		public override XPathNodeType NodeType
		{
			get
			{
				return this._curNode.NodeType;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00005853 File Offset: 0x00003A53
		public override string LocalName
		{
			get
			{
				return this._curNode.LocalName;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005860 File Offset: 0x00003A60
		public override string NamespaceURI
		{
			get
			{
				return this._curNode.NamespaceURI;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000586D File Offset: 0x00003A6D
		public override string Name
		{
			get
			{
				return this._curNode.Name;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000587A File Offset: 0x00003A7A
		public override string Prefix
		{
			get
			{
				return this._curNode.Prefix;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005888 File Offset: 0x00003A88
		public override string Value
		{
			get
			{
				XPathNodeType nodeType = this._curNode.NodeType;
				if (nodeType != XPathNodeType.Element && nodeType != XPathNodeType.Root)
				{
					return this._curNode.Value;
				}
				return this._curNode.InnerText;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000058BF File Offset: 0x00003ABF
		public override string BaseURI
		{
			get
			{
				return this._curNode.BaseURI;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000058CC File Offset: 0x00003ACC
		public override string XmlLang
		{
			get
			{
				return this._curNode.XmlLang;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000058D9 File Offset: 0x00003AD9
		public override bool IsEmptyElement
		{
			get
			{
				return this._curNode.IsEmptyElement;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000058E6 File Offset: 0x00003AE6
		public override XmlNameTable NameTable
		{
			get
			{
				return this._doc.NameTable;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000058F3 File Offset: 0x00003AF3
		public override bool HasAttributes
		{
			get
			{
				return this._curNode.AttributeCount > 0;
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005904 File Offset: 0x00003B04
		public override string GetAttribute(string localName, string namespaceURI)
		{
			if (this._curNode.NodeType != XPathNodeType.Element)
			{
				return string.Empty;
			}
			this._temp.MoveTo(this._curNode);
			if (!this._temp.MoveToAttribute(localName, namespaceURI))
			{
				return string.Empty;
			}
			return this._temp.Value;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005957 File Offset: 0x00003B57
		public override string GetNamespace(string name)
		{
			return this._curNode.GetNamespace(name);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005965 File Offset: 0x00003B65
		public override bool MoveToNamespace(string name)
		{
			return this._curNode.NodeType == XPathNodeType.Element && this._curNode.MoveToNamespace(name);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005983 File Offset: 0x00003B83
		public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			return this._curNode.NodeType == XPathNodeType.Element && this._curNode.MoveToFirstNamespace(namespaceScope);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000059A1 File Offset: 0x00003BA1
		public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
		{
			return this._curNode.NodeType == XPathNodeType.Namespace && this._curNode.MoveToNextNamespace(namespaceScope);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000059BF File Offset: 0x00003BBF
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this._curNode.NodeType == XPathNodeType.Element && this._curNode.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000059DE File Offset: 0x00003BDE
		public override bool MoveToFirstAttribute()
		{
			return this._curNode.NodeType == XPathNodeType.Element && this._curNode.MoveToNextAttribute(true);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000059FC File Offset: 0x00003BFC
		public override bool MoveToNextAttribute()
		{
			return this._curNode.NodeType == XPathNodeType.Attribute && this._curNode.MoveToNextAttribute(false);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005A1A File Offset: 0x00003C1A
		public override bool MoveToNext()
		{
			return this._curNode.NodeType != XPathNodeType.Attribute && this._curNode.MoveToNextSibling();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005A37 File Offset: 0x00003C37
		public override bool MoveToPrevious()
		{
			return this._curNode.NodeType != XPathNodeType.Attribute && this._curNode.MoveToPreviousSibling();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005A54 File Offset: 0x00003C54
		public override bool MoveToFirst()
		{
			return this._curNode.NodeType != XPathNodeType.Attribute && this._curNode.MoveToFirst();
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00005A71 File Offset: 0x00003C71
		public override bool HasChildren
		{
			get
			{
				return this._curNode.HasChildren;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005A7E File Offset: 0x00003C7E
		public override bool MoveToFirstChild()
		{
			return this._curNode.MoveToFirstChild();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005A8B File Offset: 0x00003C8B
		public override bool MoveToParent()
		{
			return this._curNode.MoveToParent();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005A98 File Offset: 0x00003C98
		public override void MoveToRoot()
		{
			this._curNode.MoveToRoot();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005AA8 File Offset: 0x00003CA8
		public override bool MoveTo(XPathNavigator other)
		{
			if (other != null)
			{
				DataDocumentXPathNavigator dataDocumentXPathNavigator = other as DataDocumentXPathNavigator;
				if (dataDocumentXPathNavigator != null && this._curNode.MoveTo(dataDocumentXPathNavigator.CurNode))
				{
					this._doc = this._curNode.Document;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool MoveToId(string id)
		{
			return false;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005AEC File Offset: 0x00003CEC
		public override bool IsSamePosition(XPathNavigator other)
		{
			if (other != null)
			{
				DataDocumentXPathNavigator dataDocumentXPathNavigator = other as DataDocumentXPathNavigator;
				if (dataDocumentXPathNavigator != null && this._doc == dataDocumentXPathNavigator.Document && this._curNode.IsSamePosition(dataDocumentXPathNavigator.CurNode))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005B2A File Offset: 0x00003D2A
		XmlNode IHasXmlNode.GetNode()
		{
			return this._curNode.Node;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005B38 File Offset: 0x00003D38
		public override XmlNodeOrder ComparePosition(XPathNavigator other)
		{
			if (other == null)
			{
				return XmlNodeOrder.Unknown;
			}
			DataDocumentXPathNavigator dataDocumentXPathNavigator = other as DataDocumentXPathNavigator;
			if (dataDocumentXPathNavigator != null && dataDocumentXPathNavigator.Document == this._doc)
			{
				return this._curNode.ComparePosition(dataDocumentXPathNavigator.CurNode);
			}
			return XmlNodeOrder.Unknown;
		}

		// Token: 0x0400042A RID: 1066
		private readonly XPathNodePointer _curNode;

		// Token: 0x0400042B RID: 1067
		private XmlDataDocument _doc;

		// Token: 0x0400042C RID: 1068
		private readonly XPathNodePointer _temp;
	}
}
