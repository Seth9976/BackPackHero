using System;
using System.Collections.Generic;

namespace Unity.VisualScripting.Antlr3.Runtime.Collections
{
	// Token: 0x02000045 RID: 69
	public class StackList : List<object>
	{
		// Token: 0x06000291 RID: 657 RVA: 0x00007E40 File Offset: 0x00006E40
		public void Push(object item)
		{
			base.Add(item);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00007E4C File Offset: 0x00006E4C
		public object Pop()
		{
			object obj = base[base.Count - 1];
			base.RemoveAt(base.Count - 1);
			return obj;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00007E77 File Offset: 0x00006E77
		public object Peek()
		{
			return base[base.Count - 1];
		}
	}
}
