using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000205 RID: 517
	internal class NavigationTabEvent : NavigationEventBase<NavigationTabEvent>
	{
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x0003E631 File Offset: 0x0003C831
		// (set) Token: 0x06000FC3 RID: 4035 RVA: 0x0003E639 File Offset: 0x0003C839
		public NavigationTabEvent.Direction direction { get; private set; }

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0003E644 File Offset: 0x0003C844
		internal static NavigationTabEvent.Direction DetermineMoveDirection(int moveValue)
		{
			return (moveValue > 0) ? NavigationTabEvent.Direction.Next : ((moveValue < 0) ? NavigationTabEvent.Direction.Previous : NavigationTabEvent.Direction.None);
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0003E668 File Offset: 0x0003C868
		public static NavigationTabEvent GetPooled(int moveValue)
		{
			NavigationTabEvent pooled = EventBase<NavigationTabEvent>.GetPooled();
			pooled.direction = NavigationTabEvent.DetermineMoveDirection(moveValue);
			return pooled;
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0003E68E File Offset: 0x0003C88E
		protected override void Init()
		{
			base.Init();
			this.direction = NavigationTabEvent.Direction.None;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0003E6A0 File Offset: 0x0003C8A0
		public NavigationTabEvent()
		{
			this.Init();
		}

		// Token: 0x02000206 RID: 518
		public enum Direction
		{
			// Token: 0x040006F3 RID: 1779
			None,
			// Token: 0x040006F4 RID: 1780
			Next,
			// Token: 0x040006F5 RID: 1781
			Previous
		}
	}
}
