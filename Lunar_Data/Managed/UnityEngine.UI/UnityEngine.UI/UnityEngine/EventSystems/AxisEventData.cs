using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200004C RID: 76
	public class AxisEventData : BaseEventData
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00017CBB File Offset: 0x00015EBB
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x00017CC3 File Offset: 0x00015EC3
		public Vector2 moveVector { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00017CCC File Offset: 0x00015ECC
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x00017CD4 File Offset: 0x00015ED4
		public MoveDirection moveDir { get; set; }

		// Token: 0x06000529 RID: 1321 RVA: 0x00017CDD File Offset: 0x00015EDD
		public AxisEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
			this.moveVector = Vector2.zero;
			this.moveDir = MoveDirection.None;
		}
	}
}
