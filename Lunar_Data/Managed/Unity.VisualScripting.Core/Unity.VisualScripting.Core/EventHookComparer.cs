using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x02000051 RID: 81
	public class EventHookComparer : IEqualityComparer<EventHook>
	{
		// Token: 0x06000265 RID: 613 RVA: 0x0000619F File Offset: 0x0000439F
		public bool Equals(EventHook x, EventHook y)
		{
			return x.Equals(y);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000061A9 File Offset: 0x000043A9
		public int GetHashCode(EventHook obj)
		{
			return obj.GetHashCode();
		}
	}
}
