using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000302 RID: 770
	internal class BasicNodePool<T> : LinkedPool<BasicNode<T>>
	{
		// Token: 0x0600192E RID: 6446 RVA: 0x000648E2 File Offset: 0x00062AE2
		private static void Reset(BasicNode<T> node)
		{
			node.next = null;
			node.data = default(T);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x000648F8 File Offset: 0x00062AF8
		private static BasicNode<T> Create()
		{
			return new BasicNode<T>();
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x0006490F File Offset: 0x00062B0F
		public BasicNodePool()
			: base(new Func<BasicNode<T>>(BasicNodePool<T>.Create), new Action<BasicNode<T>>(BasicNodePool<T>.Reset), 10000)
		{
		}
	}
}
