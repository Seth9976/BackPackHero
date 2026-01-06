using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000FB RID: 251
	[NullableContext(2)]
	[Nullable(0)]
	internal class XProcessingInstructionWrapper : XObjectWrapper
	{
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x000327F7 File Offset: 0x000309F7
		[Nullable(1)]
		private XProcessingInstruction ProcessingInstruction
		{
			[NullableContext(1)]
			get
			{
				return (XProcessingInstruction)base.WrappedNode;
			}
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00032804 File Offset: 0x00030A04
		[NullableContext(1)]
		public XProcessingInstructionWrapper(XProcessingInstruction processingInstruction)
			: base(processingInstruction)
		{
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x0003280D File Offset: 0x00030A0D
		public override string LocalName
		{
			get
			{
				return this.ProcessingInstruction.Target;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0003281A File Offset: 0x00030A1A
		// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x00032827 File Offset: 0x00030A27
		public override string Value
		{
			get
			{
				return this.ProcessingInstruction.Data;
			}
			set
			{
				this.ProcessingInstruction.Data = value ?? string.Empty;
			}
		}
	}
}
