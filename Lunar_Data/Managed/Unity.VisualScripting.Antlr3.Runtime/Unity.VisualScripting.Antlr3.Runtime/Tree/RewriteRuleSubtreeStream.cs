using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x02000055 RID: 85
	public class RewriteRuleSubtreeStream : RewriteRuleElementStream<object>
	{
		// Token: 0x06000332 RID: 818 RVA: 0x00009BE2 File Offset: 0x00008BE2
		public RewriteRuleSubtreeStream(ITreeAdaptor adaptor, string elementDescription)
			: base(adaptor, elementDescription)
		{
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00009BEC File Offset: 0x00008BEC
		public RewriteRuleSubtreeStream(ITreeAdaptor adaptor, string elementDescription, object oneElement)
			: base(adaptor, elementDescription, oneElement)
		{
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00009BF7 File Offset: 0x00008BF7
		public RewriteRuleSubtreeStream(ITreeAdaptor adaptor, string elementDescription, IList<object> elements)
			: base(adaptor, elementDescription, elements)
		{
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00009C02 File Offset: 0x00008C02
		[Obsolete("This constructor is for internal use only and might be phased out soon. Use instead the one with IList<T>.")]
		public RewriteRuleSubtreeStream(ITreeAdaptor adaptor, string elementDescription, IList elements)
			: base(adaptor, elementDescription, elements)
		{
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00009C0D File Offset: 0x00008C0D
		public object NextNode()
		{
			return this.FetchObject((object o) => this.adaptor.DupNode(o));
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00009C21 File Offset: 0x00008C21
		private object FetchObject(RewriteRuleSubtreeStream.ProcessHandler ph)
		{
			if (this.RequiresDuplication())
			{
				return ph(base._Next());
			}
			return base._Next();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00009C40 File Offset: 0x00008C40
		private bool RequiresDuplication()
		{
			int count = base.Count;
			return this.dirty || (this.cursor >= count && count == 1);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00009C6D File Offset: 0x00008C6D
		public override object NextTree()
		{
			return this.FetchObject((object o) => this.Dup(o));
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00009C81 File Offset: 0x00008C81
		private object Dup(object el)
		{
			return this.adaptor.DupTree(el);
		}

		// Token: 0x02000056 RID: 86
		// (Invoke) Token: 0x0600033E RID: 830
		private delegate object ProcessHandler(object o);
	}
}
