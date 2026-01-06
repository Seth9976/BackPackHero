using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F0 RID: 240
	[NullableContext(2)]
	[Nullable(0)]
	internal class XmlNodeWrapper : IXmlNode
	{
		// Token: 0x06000C9A RID: 3226 RVA: 0x00032238 File Offset: 0x00030438
		[NullableContext(1)]
		public XmlNodeWrapper(XmlNode node)
		{
			this._node = node;
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x00032247 File Offset: 0x00030447
		public object WrappedNode
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0003224F File Offset: 0x0003044F
		public XmlNodeType NodeType
		{
			get
			{
				return this._node.NodeType;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0003225C File Offset: 0x0003045C
		public virtual string LocalName
		{
			get
			{
				return this._node.LocalName;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x0003226C File Offset: 0x0003046C
		[Nullable(1)]
		public List<IXmlNode> ChildNodes
		{
			[NullableContext(1)]
			get
			{
				if (this._childNodes == null)
				{
					if (!this._node.HasChildNodes)
					{
						this._childNodes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._childNodes = new List<IXmlNode>(this._node.ChildNodes.Count);
						foreach (object obj in this._node.ChildNodes)
						{
							XmlNode xmlNode = (XmlNode)obj;
							this._childNodes.Add(XmlNodeWrapper.WrapNode(xmlNode));
						}
					}
				}
				return this._childNodes;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0003231C File Offset: 0x0003051C
		protected virtual bool HasChildNodes
		{
			get
			{
				return this._node.HasChildNodes;
			}
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0003232C File Offset: 0x0003052C
		[NullableContext(1)]
		internal static IXmlNode WrapNode(XmlNode node)
		{
			XmlNodeType nodeType = node.NodeType;
			if (nodeType == 1)
			{
				return new XmlElementWrapper((XmlElement)node);
			}
			if (nodeType == 10)
			{
				return new XmlDocumentTypeWrapper((XmlDocumentType)node);
			}
			if (nodeType != 17)
			{
				return new XmlNodeWrapper(node);
			}
			return new XmlDeclarationWrapper((XmlDeclaration)node);
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0003237C File Offset: 0x0003057C
		[Nullable(1)]
		public List<IXmlNode> Attributes
		{
			[NullableContext(1)]
			get
			{
				if (this._attributes == null)
				{
					if (!this.HasAttributes)
					{
						this._attributes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._attributes = new List<IXmlNode>(this._node.Attributes.Count);
						foreach (object obj in this._node.Attributes)
						{
							XmlAttribute xmlAttribute = (XmlAttribute)obj;
							this._attributes.Add(XmlNodeWrapper.WrapNode(xmlAttribute));
						}
					}
				}
				return this._attributes;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x00032424 File Offset: 0x00030624
		private bool HasAttributes
		{
			get
			{
				XmlElement xmlElement = this._node as XmlElement;
				if (xmlElement != null)
				{
					return xmlElement.HasAttributes;
				}
				XmlAttributeCollection attributes = this._node.Attributes;
				return attributes != null && attributes.Count > 0;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x00032460 File Offset: 0x00030660
		public IXmlNode ParentNode
		{
			get
			{
				XmlAttribute xmlAttribute = this._node as XmlAttribute;
				XmlNode xmlNode = ((xmlAttribute != null) ? xmlAttribute.OwnerElement : this._node.ParentNode);
				if (xmlNode == null)
				{
					return null;
				}
				return XmlNodeWrapper.WrapNode(xmlNode);
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0003249B File Offset: 0x0003069B
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x000324A8 File Offset: 0x000306A8
		public string Value
		{
			get
			{
				return this._node.Value;
			}
			set
			{
				this._node.Value = value;
			}
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x000324B8 File Offset: 0x000306B8
		[NullableContext(1)]
		public IXmlNode AppendChild(IXmlNode newChild)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)newChild;
			this._node.AppendChild(xmlNodeWrapper._node);
			this._childNodes = null;
			this._attributes = null;
			return newChild;
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x000324ED File Offset: 0x000306ED
		public string NamespaceUri
		{
			get
			{
				return this._node.NamespaceURI;
			}
		}

		// Token: 0x04000402 RID: 1026
		[Nullable(1)]
		private readonly XmlNode _node;

		// Token: 0x04000403 RID: 1027
		[Nullable(new byte[] { 2, 1 })]
		private List<IXmlNode> _childNodes;

		// Token: 0x04000404 RID: 1028
		[Nullable(new byte[] { 2, 1 })]
		private List<IXmlNode> _attributes;
	}
}
