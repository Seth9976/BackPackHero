using System;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x02000057 RID: 87
	public class TreeRuleReturnScope : RuleReturnScope
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00009CA6 File Offset: 0x00008CA6
		// (set) Token: 0x06000342 RID: 834 RVA: 0x00009CAE File Offset: 0x00008CAE
		public override object Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
			}
		}

		// Token: 0x040000EE RID: 238
		private object start;
	}
}
