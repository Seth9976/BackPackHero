using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004E RID: 78
	internal class ObjectPool<T> where T : new()
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00008A18 File Offset: 0x00006C18
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00008A30 File Offset: 0x00006C30
		public int maxSize
		{
			get
			{
				return this.m_MaxSize;
			}
			set
			{
				this.m_MaxSize = Math.Max(0, value);
				while (this.Size() > this.m_MaxSize)
				{
					this.Get();
				}
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008A68 File Offset: 0x00006C68
		public ObjectPool(int maxSize = 100)
		{
			this.maxSize = maxSize;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00008A88 File Offset: 0x00006C88
		public int Size()
		{
			return this.m_Stack.Count;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00008AA5 File Offset: 0x00006CA5
		public void Clear()
		{
			this.m_Stack.Clear();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00008AB4 File Offset: 0x00006CB4
		public T Get()
		{
			return (this.m_Stack.Count == 0) ? new T() : this.m_Stack.Pop();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008AE8 File Offset: 0x00006CE8
		public void Release(T element)
		{
			bool flag = this.m_Stack.Count > 0 && this.m_Stack.Peek() == element;
			if (flag)
			{
				Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
			}
			bool flag2 = this.m_Stack.Count < this.maxSize;
			if (flag2)
			{
				this.m_Stack.Push(element);
			}
		}

		// Token: 0x040000D8 RID: 216
		private readonly Stack<T> m_Stack = new Stack<T>();

		// Token: 0x040000D9 RID: 217
		private int m_MaxSize;
	}
}
