using System;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x0200004D RID: 77
	public class TreeVisitor
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x00008A37 File Offset: 0x00007A37
		public TreeVisitor(ITreeAdaptor adaptor)
		{
			this.adaptor = adaptor;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00008A46 File Offset: 0x00007A46
		public TreeVisitor()
			: this(new CommonTreeAdaptor())
		{
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00008A54 File Offset: 0x00007A54
		public object Visit(object t, ITreeVisitorAction action)
		{
			bool flag = this.adaptor.IsNil(t);
			if (action != null && !flag)
			{
				t = action.Pre(t);
			}
			int childCount = this.adaptor.GetChildCount(t);
			for (int i = 0; i < childCount; i++)
			{
				object child = this.adaptor.GetChild(t, i);
				object obj = this.Visit(child, action);
				object child2 = this.adaptor.GetChild(t, i);
				if (obj != child2)
				{
					this.adaptor.SetChild(t, i, obj);
				}
			}
			if (action != null && !flag)
			{
				t = action.Post(t);
			}
			return t;
		}

		// Token: 0x040000DB RID: 219
		protected ITreeAdaptor adaptor;
	}
}
