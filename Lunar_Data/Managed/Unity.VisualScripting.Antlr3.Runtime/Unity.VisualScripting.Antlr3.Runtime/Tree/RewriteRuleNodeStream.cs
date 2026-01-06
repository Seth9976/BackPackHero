using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x0200001F RID: 31
	public class RewriteRuleNodeStream : RewriteRuleElementStream<object>
	{
		// Token: 0x06000177 RID: 375 RVA: 0x00004FD3 File Offset: 0x00003FD3
		public RewriteRuleNodeStream(ITreeAdaptor adaptor, string elementDescription)
			: base(adaptor, elementDescription)
		{
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00004FDD File Offset: 0x00003FDD
		public RewriteRuleNodeStream(ITreeAdaptor adaptor, string elementDescription, object oneElement)
			: base(adaptor, elementDescription, oneElement)
		{
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00004FE8 File Offset: 0x00003FE8
		public RewriteRuleNodeStream(ITreeAdaptor adaptor, string elementDescription, IList<object> elements)
			: base(adaptor, elementDescription, elements)
		{
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00004FF3 File Offset: 0x00003FF3
		[Obsolete("This constructor is for internal use only and might be phased out soon. Use instead the one with IList<T>.")]
		public RewriteRuleNodeStream(ITreeAdaptor adaptor, string elementDescription, IList elements)
			: base(adaptor, elementDescription, elements)
		{
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00004FFE File Offset: 0x00003FFE
		public object NextNode()
		{
			return base._Next();
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005006 File Offset: 0x00004006
		protected override object ToTree(object el)
		{
			return this.adaptor.DupNode(el);
		}
	}
}
