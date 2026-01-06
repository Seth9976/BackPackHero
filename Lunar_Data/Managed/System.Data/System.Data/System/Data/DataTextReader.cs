using System;
using System.Xml;

namespace System.Data
{
	// Token: 0x020000FE RID: 254
	internal sealed class DataTextReader : XmlReader
	{
		// Token: 0x06000DDB RID: 3547 RVA: 0x0004945B File Offset: 0x0004765B
		internal static XmlReader CreateReader(XmlReader xr)
		{
			return new DataTextReader(xr);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x00049463 File Offset: 0x00047663
		private DataTextReader(XmlReader input)
		{
			this._xmlreader = input;
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x00049472 File Offset: 0x00047672
		public override XmlReaderSettings Settings
		{
			get
			{
				return this._xmlreader.Settings;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x0004947F File Offset: 0x0004767F
		public override XmlNodeType NodeType
		{
			get
			{
				return this._xmlreader.NodeType;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x0004948C File Offset: 0x0004768C
		public override string Name
		{
			get
			{
				return this._xmlreader.Name;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x00049499 File Offset: 0x00047699
		public override string LocalName
		{
			get
			{
				return this._xmlreader.LocalName;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x000494A6 File Offset: 0x000476A6
		public override string NamespaceURI
		{
			get
			{
				return this._xmlreader.NamespaceURI;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x000494B3 File Offset: 0x000476B3
		public override string Prefix
		{
			get
			{
				return this._xmlreader.Prefix;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x000494C0 File Offset: 0x000476C0
		public override bool HasValue
		{
			get
			{
				return this._xmlreader.HasValue;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x000494CD File Offset: 0x000476CD
		public override string Value
		{
			get
			{
				return this._xmlreader.Value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x000494DA File Offset: 0x000476DA
		public override int Depth
		{
			get
			{
				return this._xmlreader.Depth;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x000494E7 File Offset: 0x000476E7
		public override string BaseURI
		{
			get
			{
				return this._xmlreader.BaseURI;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x000494F4 File Offset: 0x000476F4
		public override bool IsEmptyElement
		{
			get
			{
				return this._xmlreader.IsEmptyElement;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00049501 File Offset: 0x00047701
		public override bool IsDefault
		{
			get
			{
				return this._xmlreader.IsDefault;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x0004950E File Offset: 0x0004770E
		public override char QuoteChar
		{
			get
			{
				return this._xmlreader.QuoteChar;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0004951B File Offset: 0x0004771B
		public override XmlSpace XmlSpace
		{
			get
			{
				return this._xmlreader.XmlSpace;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00049528 File Offset: 0x00047728
		public override string XmlLang
		{
			get
			{
				return this._xmlreader.XmlLang;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x00049535 File Offset: 0x00047735
		public override int AttributeCount
		{
			get
			{
				return this._xmlreader.AttributeCount;
			}
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x00049542 File Offset: 0x00047742
		public override string GetAttribute(string name)
		{
			return this._xmlreader.GetAttribute(name);
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00049550 File Offset: 0x00047750
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this._xmlreader.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0004955F File Offset: 0x0004775F
		public override string GetAttribute(int i)
		{
			return this._xmlreader.GetAttribute(i);
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0004956D File Offset: 0x0004776D
		public override bool MoveToAttribute(string name)
		{
			return this._xmlreader.MoveToAttribute(name);
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0004957B File Offset: 0x0004777B
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this._xmlreader.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0004958A File Offset: 0x0004778A
		public override void MoveToAttribute(int i)
		{
			this._xmlreader.MoveToAttribute(i);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00049598 File Offset: 0x00047798
		public override bool MoveToFirstAttribute()
		{
			return this._xmlreader.MoveToFirstAttribute();
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x000495A5 File Offset: 0x000477A5
		public override bool MoveToNextAttribute()
		{
			return this._xmlreader.MoveToNextAttribute();
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x000495B2 File Offset: 0x000477B2
		public override bool MoveToElement()
		{
			return this._xmlreader.MoveToElement();
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x000495BF File Offset: 0x000477BF
		public override bool ReadAttributeValue()
		{
			return this._xmlreader.ReadAttributeValue();
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x000495CC File Offset: 0x000477CC
		public override bool Read()
		{
			return this._xmlreader.Read();
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x000495D9 File Offset: 0x000477D9
		public override bool EOF
		{
			get
			{
				return this._xmlreader.EOF;
			}
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x000495E6 File Offset: 0x000477E6
		public override void Close()
		{
			this._xmlreader.Close();
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x000495F3 File Offset: 0x000477F3
		public override ReadState ReadState
		{
			get
			{
				return this._xmlreader.ReadState;
			}
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00049600 File Offset: 0x00047800
		public override void Skip()
		{
			this._xmlreader.Skip();
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x0004960D File Offset: 0x0004780D
		public override XmlNameTable NameTable
		{
			get
			{
				return this._xmlreader.NameTable;
			}
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0004961A File Offset: 0x0004781A
		public override string LookupNamespace(string prefix)
		{
			return this._xmlreader.LookupNamespace(prefix);
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x00049628 File Offset: 0x00047828
		public override bool CanResolveEntity
		{
			get
			{
				return this._xmlreader.CanResolveEntity;
			}
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00049635 File Offset: 0x00047835
		public override void ResolveEntity()
		{
			this._xmlreader.ResolveEntity();
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x00049642 File Offset: 0x00047842
		public override bool CanReadBinaryContent
		{
			get
			{
				return this._xmlreader.CanReadBinaryContent;
			}
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0004964F File Offset: 0x0004784F
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0004965F File Offset: 0x0004785F
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0004966F File Offset: 0x0004786F
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0004967F File Offset: 0x0004787F
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x0004968F File Offset: 0x0004788F
		public override bool CanReadValueChunk
		{
			get
			{
				return this._xmlreader.CanReadValueChunk;
			}
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0004969C File Offset: 0x0004789C
		public override string ReadString()
		{
			return this._xmlreader.ReadString();
		}

		// Token: 0x04000981 RID: 2433
		private XmlReader _xmlreader;
	}
}
