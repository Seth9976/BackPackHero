using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x02000059 RID: 89
	public class RewriteRuleTokenStream : RewriteRuleElementStream<IToken>
	{
		// Token: 0x06000354 RID: 852 RVA: 0x00009F8F File Offset: 0x00008F8F
		public RewriteRuleTokenStream(ITreeAdaptor adaptor, string elementDescription)
			: base(adaptor, elementDescription)
		{
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00009F99 File Offset: 0x00008F99
		public RewriteRuleTokenStream(ITreeAdaptor adaptor, string elementDescription, IToken oneElement)
			: base(adaptor, elementDescription, oneElement)
		{
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00009FA4 File Offset: 0x00008FA4
		public RewriteRuleTokenStream(ITreeAdaptor adaptor, string elementDescription, IList<IToken> elements)
			: base(adaptor, elementDescription, elements)
		{
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00009FAF File Offset: 0x00008FAF
		[Obsolete("This constructor is for internal use only and might be phased out soon. Use instead the one with IList<T>.")]
		public RewriteRuleTokenStream(ITreeAdaptor adaptor, string elementDescription, IList elements)
			: base(adaptor, elementDescription, elements)
		{
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00009FBA File Offset: 0x00008FBA
		public object NextNode()
		{
			return this.adaptor.Create((IToken)base._Next());
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00009FD2 File Offset: 0x00008FD2
		public IToken NextToken()
		{
			return (IToken)base._Next();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00009FDF File Offset: 0x00008FDF
		protected override object ToTree(IToken el)
		{
			return el;
		}
	}
}
