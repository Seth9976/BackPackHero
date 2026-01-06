using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000247 RID: 583
	internal static class RuntimeEventDispatcher
	{
		// Token: 0x06001181 RID: 4481 RVA: 0x00043798 File Offset: 0x00041998
		public static EventDispatcher Create()
		{
			List<IEventDispatchingStrategy> list = new List<IEventDispatchingStrategy>();
			list.Add(new NavigationEventDispatchingStrategy());
			list.Add(new PointerCaptureDispatchingStrategy());
			list.Add(new KeyboardEventDispatchingStrategy());
			list.Add(new PointerEventDispatchingStrategy());
			list.Add(new MouseEventDispatchingStrategy());
			list.Add(new DefaultDispatchingStrategy());
			return EventDispatcher.CreateForRuntime(list);
		}
	}
}
