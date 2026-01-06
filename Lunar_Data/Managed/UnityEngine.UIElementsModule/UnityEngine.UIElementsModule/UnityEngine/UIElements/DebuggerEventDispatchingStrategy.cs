using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001C7 RID: 455
	internal class DebuggerEventDispatchingStrategy : IEventDispatchingStrategy
	{
		// Token: 0x06000E36 RID: 3638 RVA: 0x0003A1B0 File Offset: 0x000383B0
		public bool CanDispatchEvent(EventBase evt)
		{
			return false;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x000020E6 File Offset: 0x000002E6
		public void DispatchEvent(EventBase evt, IPanel panel)
		{
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000020E6 File Offset: 0x000002E6
		public void PostDispatch(EventBase evt, IPanel panel)
		{
		}
	}
}
