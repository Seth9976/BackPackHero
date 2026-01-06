using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000FE RID: 254
	[NullableContext(2)]
	[Nullable(0)]
	internal class XAttributeWrapper : XObjectWrapper
	{
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00032A28 File Offset: 0x00030C28
		[Nullable(1)]
		private XAttribute Attribute
		{
			[NullableContext(1)]
			get
			{
				return (XAttribute)base.WrappedNode;
			}
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00032A35 File Offset: 0x00030C35
		[NullableContext(1)]
		public XAttributeWrapper(XAttribute attribute)
			: base(attribute)
		{
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x00032A3E File Offset: 0x00030C3E
		// (set) Token: 0x06000D0F RID: 3343 RVA: 0x00032A4B File Offset: 0x00030C4B
		public override string Value
		{
			get
			{
				return this.Attribute.Value;
			}
			set
			{
				this.Attribute.Value = value ?? string.Empty;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x00032A62 File Offset: 0x00030C62
		public override string LocalName
		{
			get
			{
				return this.Attribute.Name.LocalName;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x00032A74 File Offset: 0x00030C74
		public override string NamespaceUri
		{
			get
			{
				return this.Attribute.Name.NamespaceName;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00032A86 File Offset: 0x00030C86
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Attribute.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Attribute.Parent);
			}
		}
	}
}
