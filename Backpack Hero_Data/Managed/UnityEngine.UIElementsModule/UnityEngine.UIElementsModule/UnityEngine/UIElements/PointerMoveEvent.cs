using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000218 RID: 536
	public sealed class PointerMoveEvent : PointerEventBase<PointerMoveEvent>
	{
		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x0003FB3C File Offset: 0x0003DD3C
		// (set) Token: 0x06001050 RID: 4176 RVA: 0x0003FB44 File Offset: 0x0003DD44
		internal bool isHandledByDraggable { get; set; }

		// Token: 0x06001051 RID: 4177 RVA: 0x0003FB4D File Offset: 0x0003DD4D
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0003FB5E File Offset: 0x0003DD5E
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable;
			((IPointerEventInternal)this).triggeredByOS = true;
			((IPointerEventInternal)this).recomputeTopElementUnderPointer = true;
			this.isHandledByDraggable = false;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0003FB81 File Offset: 0x0003DD81
		public PointerMoveEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0003FB94 File Offset: 0x0003DD94
		protected internal override void PostDispatch(IPanel panel)
		{
			bool flag = panel.ShouldSendCompatibilityMouseEvents(this);
			if (flag)
			{
				bool flag2 = base.imguiEvent != null && base.imguiEvent.rawType == EventType.MouseDown;
				if (flag2)
				{
					using (MouseDownEvent pooled = MouseDownEvent.GetPooled(this))
					{
						pooled.target = base.target;
						pooled.target.SendEvent(pooled);
					}
				}
				else
				{
					bool flag3 = base.imguiEvent != null && base.imguiEvent.rawType == EventType.MouseUp;
					if (flag3)
					{
						using (MouseUpEvent pooled2 = MouseUpEvent.GetPooled(this))
						{
							pooled2.target = base.target;
							pooled2.target.SendEvent(pooled2);
						}
					}
					else
					{
						using (MouseMoveEvent pooled3 = MouseMoveEvent.GetPooled(this))
						{
							pooled3.target = base.target;
							pooled3.target.SendEvent(pooled3);
						}
					}
				}
			}
			base.PostDispatch(panel);
		}
	}
}
