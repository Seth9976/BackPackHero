using System;
using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x02000089 RID: 137
	internal struct NavigationModel
	{
		// Token: 0x06000B1B RID: 2843 RVA: 0x0003B8D8 File Offset: 0x00039AD8
		public void Reset()
		{
			this.move = Vector2.zero;
		}

		// Token: 0x040003DE RID: 990
		public Vector2 move;

		// Token: 0x040003DF RID: 991
		public int consecutiveMoveCount;

		// Token: 0x040003E0 RID: 992
		public MoveDirection lastMoveDirection;

		// Token: 0x040003E1 RID: 993
		public float lastMoveTime;

		// Token: 0x040003E2 RID: 994
		public AxisEventData eventData;
	}
}
