using System;
using System.Diagnostics;

namespace UnityEngine.UIElements
{
	// Token: 0x0200000D RID: 13
	internal class ClampedDragger<T> : Clickable where T : IComparable<T>
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600003C RID: 60 RVA: 0x00002B4C File Offset: 0x00000D4C
		// (remove) Token: 0x0600003D RID: 61 RVA: 0x00002B84 File Offset: 0x00000D84
		[field: DebuggerBrowsable(0)]
		public event Action dragging;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002BB9 File Offset: 0x00000DB9
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002BC1 File Offset: 0x00000DC1
		public ClampedDragger<T>.DragDirection dragDirection { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002BCA File Offset: 0x00000DCA
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002BD2 File Offset: 0x00000DD2
		private BaseSlider<T> slider { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002BDB File Offset: 0x00000DDB
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002BE3 File Offset: 0x00000DE3
		public Vector2 startMousePosition { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002BEC File Offset: 0x00000DEC
		public Vector2 delta
		{
			get
			{
				return base.lastMousePosition - this.startMousePosition;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002BFF File Offset: 0x00000DFF
		public ClampedDragger(BaseSlider<T> slider, Action clickHandler, Action dragHandler)
			: base(clickHandler, 250L, 30L)
		{
			this.dragDirection = ClampedDragger<T>.DragDirection.None;
			this.slider = slider;
			this.dragging += dragHandler;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002C2B File Offset: 0x00000E2B
		protected override void ProcessDownEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.startMousePosition = localPosition;
			this.dragDirection = ClampedDragger<T>.DragDirection.None;
			base.ProcessDownEvent(evt, localPosition, pointerId);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002C48 File Offset: 0x00000E48
		protected override void ProcessMoveEvent(EventBase evt, Vector2 localPosition)
		{
			base.ProcessMoveEvent(evt, localPosition);
			bool flag = this.dragDirection == ClampedDragger<T>.DragDirection.None;
			if (flag)
			{
				this.dragDirection = ClampedDragger<T>.DragDirection.Free;
			}
			bool flag2 = this.dragDirection == ClampedDragger<T>.DragDirection.Free;
			if (flag2)
			{
				bool flag3 = evt.eventTypeId == EventBase<PointerMoveEvent>.TypeId();
				if (flag3)
				{
					PointerMoveEvent pointerMoveEvent = (PointerMoveEvent)evt;
					bool flag4 = pointerMoveEvent.pointerId != PointerId.mousePointerId;
					if (flag4)
					{
						pointerMoveEvent.isHandledByDraggable = true;
					}
				}
				Action action = this.dragging;
				if (action != null)
				{
					action.Invoke();
				}
			}
		}

		// Token: 0x0200000E RID: 14
		[Flags]
		public enum DragDirection
		{
			// Token: 0x04000020 RID: 32
			None = 0,
			// Token: 0x04000021 RID: 33
			LowToHigh = 1,
			// Token: 0x04000022 RID: 34
			HighToLow = 2,
			// Token: 0x04000023 RID: 35
			Free = 4
		}
	}
}
