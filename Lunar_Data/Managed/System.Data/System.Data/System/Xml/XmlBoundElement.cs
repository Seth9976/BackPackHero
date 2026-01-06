using System;
using System.Data;
using System.Threading;

namespace System.Xml
{
	// Token: 0x02000031 RID: 49
	internal sealed class XmlBoundElement : XmlElement
	{
		// Token: 0x06000194 RID: 404 RVA: 0x00008E9F File Offset: 0x0000709F
		internal XmlBoundElement(string prefix, string localName, string namespaceURI, XmlDocument doc)
			: base(prefix, localName, namespaceURI, doc)
		{
			this._state = ElementState.None;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00008EB3 File Offset: 0x000070B3
		public override XmlAttributeCollection Attributes
		{
			get
			{
				this.AutoFoliate();
				return base.Attributes;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00008EC1 File Offset: 0x000070C1
		public override bool HasAttributes
		{
			get
			{
				return this.Attributes.Count > 0;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00008ED1 File Offset: 0x000070D1
		public override XmlNode FirstChild
		{
			get
			{
				this.AutoFoliate();
				return base.FirstChild;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00008EDF File Offset: 0x000070DF
		internal XmlNode SafeFirstChild
		{
			get
			{
				return base.FirstChild;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00008EE7 File Offset: 0x000070E7
		public override XmlNode LastChild
		{
			get
			{
				this.AutoFoliate();
				return base.LastChild;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00008EF8 File Offset: 0x000070F8
		public override XmlNode PreviousSibling
		{
			get
			{
				XmlNode previousSibling = base.PreviousSibling;
				if (previousSibling == null)
				{
					XmlBoundElement xmlBoundElement = this.ParentNode as XmlBoundElement;
					if (xmlBoundElement != null)
					{
						xmlBoundElement.AutoFoliate();
						return base.PreviousSibling;
					}
				}
				return previousSibling;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00008F2C File Offset: 0x0000712C
		internal XmlNode SafePreviousSibling
		{
			get
			{
				return base.PreviousSibling;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00008F34 File Offset: 0x00007134
		public override XmlNode NextSibling
		{
			get
			{
				XmlNode nextSibling = base.NextSibling;
				if (nextSibling == null)
				{
					XmlBoundElement xmlBoundElement = this.ParentNode as XmlBoundElement;
					if (xmlBoundElement != null)
					{
						xmlBoundElement.AutoFoliate();
						return base.NextSibling;
					}
				}
				return nextSibling;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00008F68 File Offset: 0x00007168
		internal XmlNode SafeNextSibling
		{
			get
			{
				return base.NextSibling;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00008F70 File Offset: 0x00007170
		public override bool HasChildNodes
		{
			get
			{
				this.AutoFoliate();
				return base.HasChildNodes;
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00008F7E File Offset: 0x0000717E
		public override XmlNode InsertBefore(XmlNode newChild, XmlNode refChild)
		{
			this.AutoFoliate();
			return base.InsertBefore(newChild, refChild);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008F8E File Offset: 0x0000718E
		public override XmlNode InsertAfter(XmlNode newChild, XmlNode refChild)
		{
			this.AutoFoliate();
			return base.InsertAfter(newChild, refChild);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008F9E File Offset: 0x0000719E
		public override XmlNode ReplaceChild(XmlNode newChild, XmlNode oldChild)
		{
			this.AutoFoliate();
			return base.ReplaceChild(newChild, oldChild);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00008FAE File Offset: 0x000071AE
		public override XmlNode AppendChild(XmlNode newChild)
		{
			this.AutoFoliate();
			return base.AppendChild(newChild);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008FC0 File Offset: 0x000071C0
		internal void RemoveAllChildren()
		{
			XmlNode nextSibling;
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = nextSibling)
			{
				nextSibling = xmlNode.NextSibling;
				this.RemoveChild(xmlNode);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00008FE8 File Offset: 0x000071E8
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00008FF0 File Offset: 0x000071F0
		public override string InnerXml
		{
			get
			{
				return base.InnerXml;
			}
			set
			{
				this.RemoveAllChildren();
				XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
				bool ignoreXmlEvents = xmlDataDocument.IgnoreXmlEvents;
				bool ignoreDataSetEvents = xmlDataDocument.IgnoreDataSetEvents;
				xmlDataDocument.IgnoreXmlEvents = true;
				xmlDataDocument.IgnoreDataSetEvents = true;
				base.InnerXml = value;
				xmlDataDocument.SyncTree(this);
				xmlDataDocument.IgnoreDataSetEvents = ignoreDataSetEvents;
				xmlDataDocument.IgnoreXmlEvents = ignoreXmlEvents;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00009045 File Offset: 0x00007245
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000904D File Offset: 0x0000724D
		internal DataRow Row
		{
			get
			{
				return this._row;
			}
			set
			{
				this._row = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00009056 File Offset: 0x00007256
		internal bool IsFoliated
		{
			get
			{
				while (this._state == ElementState.Foliating || this._state == ElementState.Defoliating)
				{
					Thread.Sleep(0);
				}
				return this._state != ElementState.Defoliated;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000907E File Offset: 0x0000727E
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00009086 File Offset: 0x00007286
		internal ElementState ElementState
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00009090 File Offset: 0x00007290
		internal void Foliate(ElementState newState)
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
			if (xmlDataDocument != null)
			{
				xmlDataDocument.Foliate(this, newState);
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000090B4 File Offset: 0x000072B4
		private void AutoFoliate()
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
			if (xmlDataDocument != null)
			{
				xmlDataDocument.Foliate(this, xmlDataDocument.AutoFoliationState);
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000090E0 File Offset: 0x000072E0
		public override XmlNode CloneNode(bool deep)
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
			ElementState autoFoliationState = xmlDataDocument.AutoFoliationState;
			xmlDataDocument.AutoFoliationState = ElementState.WeakFoliation;
			XmlElement xmlElement;
			try
			{
				this.Foliate(ElementState.WeakFoliation);
				xmlElement = (XmlElement)base.CloneNode(deep);
			}
			finally
			{
				xmlDataDocument.AutoFoliationState = autoFoliationState;
			}
			return xmlElement;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00009138 File Offset: 0x00007338
		public override void WriteContentTo(XmlWriter w)
		{
			DataPointer dataPointer = new DataPointer((XmlDataDocument)this.OwnerDocument, this);
			try
			{
				dataPointer.AddPointer();
				XmlBoundElement.WriteBoundElementContentTo(dataPointer, w);
			}
			finally
			{
				dataPointer.SetNoLongerUse();
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00009180 File Offset: 0x00007380
		public override void WriteTo(XmlWriter w)
		{
			DataPointer dataPointer = new DataPointer((XmlDataDocument)this.OwnerDocument, this);
			try
			{
				dataPointer.AddPointer();
				this.WriteRootBoundElementTo(dataPointer, w);
			}
			finally
			{
				dataPointer.SetNoLongerUse();
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000091C8 File Offset: 0x000073C8
		private void WriteRootBoundElementTo(DataPointer dp, XmlWriter w)
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
			w.WriteStartElement(dp.Prefix, dp.LocalName, dp.NamespaceURI);
			int attributeCount = dp.AttributeCount;
			bool flag = false;
			if (attributeCount > 0)
			{
				for (int i = 0; i < attributeCount; i++)
				{
					dp.MoveToAttribute(i);
					if (dp.Prefix == "xmlns" && dp.LocalName == "xsi")
					{
						flag = true;
					}
					XmlBoundElement.WriteTo(dp, w);
					dp.MoveToOwnerElement();
				}
			}
			if (!flag && xmlDataDocument._bLoadFromDataSet && xmlDataDocument._bHasXSINIL)
			{
				w.WriteAttributeString("xmlns", "xsi", "http://www.w3.org/2000/xmlns/", "http://www.w3.org/2001/XMLSchema-instance");
			}
			XmlBoundElement.WriteBoundElementContentTo(dp, w);
			if (dp.IsEmptyElement)
			{
				w.WriteEndElement();
				return;
			}
			w.WriteFullEndElement();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00009298 File Offset: 0x00007498
		private static void WriteBoundElementTo(DataPointer dp, XmlWriter w)
		{
			w.WriteStartElement(dp.Prefix, dp.LocalName, dp.NamespaceURI);
			int attributeCount = dp.AttributeCount;
			if (attributeCount > 0)
			{
				for (int i = 0; i < attributeCount; i++)
				{
					dp.MoveToAttribute(i);
					XmlBoundElement.WriteTo(dp, w);
					dp.MoveToOwnerElement();
				}
			}
			XmlBoundElement.WriteBoundElementContentTo(dp, w);
			if (dp.IsEmptyElement)
			{
				w.WriteEndElement();
				return;
			}
			w.WriteFullEndElement();
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00009306 File Offset: 0x00007506
		private static void WriteBoundElementContentTo(DataPointer dp, XmlWriter w)
		{
			if (!dp.IsEmptyElement && dp.MoveToFirstChild())
			{
				do
				{
					XmlBoundElement.WriteTo(dp, w);
				}
				while (dp.MoveToNextSibling());
				dp.MoveToParent();
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00009330 File Offset: 0x00007530
		private static void WriteTo(DataPointer dp, XmlWriter w)
		{
			switch (dp.NodeType)
			{
			case XmlNodeType.Element:
				XmlBoundElement.WriteBoundElementTo(dp, w);
				return;
			case XmlNodeType.Attribute:
				if (!dp.IsDefault)
				{
					w.WriteStartAttribute(dp.Prefix, dp.LocalName, dp.NamespaceURI);
					if (dp.MoveToFirstChild())
					{
						do
						{
							XmlBoundElement.WriteTo(dp, w);
						}
						while (dp.MoveToNextSibling());
						dp.MoveToParent();
					}
					w.WriteEndAttribute();
					return;
				}
				break;
			case XmlNodeType.Text:
				w.WriteString(dp.Value);
				return;
			default:
				if (dp.GetNode() != null)
				{
					dp.GetNode().WriteTo(w);
				}
				break;
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000093C8 File Offset: 0x000075C8
		public override XmlNodeList GetElementsByTagName(string name)
		{
			XmlNodeList elementsByTagName = base.GetElementsByTagName(name);
			int count = elementsByTagName.Count;
			return elementsByTagName;
		}

		// Token: 0x0400044E RID: 1102
		private DataRow _row;

		// Token: 0x0400044F RID: 1103
		private ElementState _state;
	}
}
