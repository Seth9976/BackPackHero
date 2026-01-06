using System;
using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x02000083 RID: 131
	internal class ExtendedAxisEventData : AxisEventData
	{
		// Token: 0x06000A97 RID: 2711 RVA: 0x00038E06 File Offset: 0x00037006
		public ExtendedAxisEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00038E0F File Offset: 0x0003700F
		public override string ToString()
		{
			return string.Format("MoveDir: {0}\nMoveVector: {1}", base.moveDir, base.moveVector);
		}
	}
}
