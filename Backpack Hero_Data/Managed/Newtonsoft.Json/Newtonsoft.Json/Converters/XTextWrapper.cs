using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F9 RID: 249
	[NullableContext(2)]
	[Nullable(0)]
	internal class XTextWrapper : XObjectWrapper
	{
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x00032741 File Offset: 0x00030941
		[Nullable(1)]
		private XText Text
		{
			[NullableContext(1)]
			get
			{
				return (XText)base.WrappedNode;
			}
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0003274E File Offset: 0x0003094E
		[NullableContext(1)]
		public XTextWrapper(XText text)
			: base(text)
		{
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00032757 File Offset: 0x00030957
		// (set) Token: 0x06000CEE RID: 3310 RVA: 0x00032764 File Offset: 0x00030964
		public override string Value
		{
			get
			{
				return this.Text.Value;
			}
			set
			{
				this.Text.Value = value ?? string.Empty;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x0003277B File Offset: 0x0003097B
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Text.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Text.Parent);
			}
		}
	}
}
