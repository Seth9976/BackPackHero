using System;
using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x02000083 RID: 131
	internal class ExtendedAxisEventData : AxisEventData
	{
		// Token: 0x06000A99 RID: 2713 RVA: 0x00038E42 File Offset: 0x00037042
		public ExtendedAxisEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00038E4B File Offset: 0x0003704B
		public override string ToString()
		{
			return string.Format("MoveDir: {0}\nMoveVector: {1}", base.moveDir, base.moveVector);
		}
	}
}
