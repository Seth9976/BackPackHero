using System;
using System.Collections.Generic;

namespace TwitchLib.Api.Core.RateLimiter
{
	// Token: 0x02000009 RID: 9
	public class LimitedSizeStack<T> : LinkedList<T>
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002E8F File Offset: 0x0000108F
		public LimitedSizeStack(int maxSize)
		{
			this._maxSize = maxSize;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002E9E File Offset: 0x0000109E
		public void Push(T item)
		{
			base.AddFirst(item);
			if (base.Count > this._maxSize)
			{
				base.RemoveLast();
			}
		}

		// Token: 0x0400001A RID: 26
		private readonly int _maxSize;
	}
}
