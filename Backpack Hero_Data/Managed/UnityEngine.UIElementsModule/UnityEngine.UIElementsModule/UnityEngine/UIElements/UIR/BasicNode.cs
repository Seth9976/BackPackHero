using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000301 RID: 769
	internal class BasicNode<T> : LinkedPoolItem<BasicNode<T>>
	{
		// Token: 0x0600192C RID: 6444 RVA: 0x0006489C File Offset: 0x00062A9C
		public void AppendTo(ref BasicNode<T> first)
		{
			bool flag = first == null;
			if (flag)
			{
				first = this;
			}
			else
			{
				BasicNode<T> basicNode = first;
				while (basicNode.next != null)
				{
					basicNode = basicNode.next;
				}
				basicNode.next = this;
			}
		}

		// Token: 0x04000AD2 RID: 2770
		public BasicNode<T> next;

		// Token: 0x04000AD3 RID: 2771
		public T data;
	}
}
