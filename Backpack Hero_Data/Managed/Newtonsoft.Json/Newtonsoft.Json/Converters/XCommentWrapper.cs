using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000FA RID: 250
	[NullableContext(2)]
	[Nullable(0)]
	internal class XCommentWrapper : XObjectWrapper
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0003279C File Offset: 0x0003099C
		[Nullable(1)]
		private XComment Text
		{
			[NullableContext(1)]
			get
			{
				return (XComment)base.WrappedNode;
			}
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x000327A9 File Offset: 0x000309A9
		[NullableContext(1)]
		public XCommentWrapper(XComment text)
			: base(text)
		{
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x000327B2 File Offset: 0x000309B2
		// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x000327BF File Offset: 0x000309BF
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

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x000327D6 File Offset: 0x000309D6
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
