using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000FD RID: 253
	[NullableContext(2)]
	[Nullable(0)]
	internal class XObjectWrapper : IXmlNode
	{
		// Token: 0x06000D01 RID: 3329 RVA: 0x000329D6 File Offset: 0x00030BD6
		public XObjectWrapper(XObject xmlObject)
		{
			this._xmlObject = xmlObject;
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x000329E5 File Offset: 0x00030BE5
		public object WrappedNode
		{
			get
			{
				return this._xmlObject;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x000329ED File Offset: 0x00030BED
		public virtual XmlNodeType NodeType
		{
			get
			{
				XObject xmlObject = this._xmlObject;
				if (xmlObject == null)
				{
					return 0;
				}
				return xmlObject.NodeType;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00032A00 File Offset: 0x00030C00
		public virtual string LocalName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00032A03 File Offset: 0x00030C03
		[Nullable(1)]
		public virtual List<IXmlNode> ChildNodes
		{
			[NullableContext(1)]
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00032A0A File Offset: 0x00030C0A
		[Nullable(1)]
		public virtual List<IXmlNode> Attributes
		{
			[NullableContext(1)]
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00032A11 File Offset: 0x00030C11
		public virtual IXmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00032A14 File Offset: 0x00030C14
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x00032A17 File Offset: 0x00030C17
		public virtual string Value
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00032A1E File Offset: 0x00030C1E
		[NullableContext(1)]
		public virtual IXmlNode AppendChild(IXmlNode newChild)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x00032A25 File Offset: 0x00030C25
		public virtual string NamespaceUri
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000408 RID: 1032
		private readonly XObject _xmlObject;
	}
}
