using System;
using System.Diagnostics;

namespace UnityEngine.UIElements
{
	// Token: 0x0200000F RID: 15
	public class Clickable : PointerManipulator
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000048 RID: 72 RVA: 0x00002CD0 File Offset: 0x00000ED0
		// (remove) Token: 0x06000049 RID: 73 RVA: 0x00002D08 File Offset: 0x00000F08
		[field: DebuggerBrowsable(0)]
		public event Action<EventBase> clickedWithEventInfo;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600004A RID: 74 RVA: 0x00002D40 File Offset: 0x00000F40
		// (remove) Token: 0x0600004B RID: 75 RVA: 0x00002D78 File Offset: 0x00000F78
		[field: DebuggerBrowsable(0)]
		public event Action clicked;

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002DAD File Offset: 0x00000FAD
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002DB5 File Offset: 0x00000FB5
		protected bool active { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002DBE File Offset: 0x00000FBE
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002DC6 File Offset: 0x00000FC6
		public Vector2 lastMousePosition { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002DCF File Offset: 0x00000FCF
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002DD8 File Offset: 0x00000FD8
		internal bool acceptClicksIfDisabled
		{
			get
			{
				return this.m_AcceptClicksIfDisabled;
			}
			set
			{
				bool flag = this.m_AcceptClicksIfDisabled == value;
				if (!flag)
				{
					this.UnregisterCallbacksFromTarget();
					this.m_AcceptClicksIfDisabled = value;
					this.RegisterCallbacksOnTarget();
				}
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002E0A File Offset: 0x0000100A
		private InvokePolicy invokePolicy
		{
			get
			{
				return this.acceptClicksIfDisabled ? InvokePolicy.IncludeDisabled : InvokePolicy.Default;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002E18 File Offset: 0x00001018
		public Clickable(Action handler, long delay, long interval)
			: this(handler)
		{
			this.m_Delay = delay;
			this.m_Interval = interval;
			this.active = false;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002E3C File Offset: 0x0000103C
		public Clickable(Action<EventBase> handler)
		{
			this.m_ActivePointerId = -1;
			base..ctor();
			this.clickedWithEventInfo = handler;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002E80 File Offset: 0x00001080
		public Clickable(Action handler)
		{
			this.m_ActivePointerId = -1;
			base..ctor();
			this.clicked = handler;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
			this.active = false;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002ECC File Offset: 0x000010CC
		private void OnTimer(TimerState timerState)
		{
			bool flag = (this.clicked != null || this.clickedWithEventInfo != null) && this.IsRepeatable();
			if (flag)
			{
				bool flag2 = this.ContainsPointer(this.m_ActivePointerId);
				if (flag2)
				{
					this.Invoke(null);
					base.target.pseudoStates |= PseudoStates.Active;
				}
				else
				{
					base.target.pseudoStates &= ~PseudoStates.Active;
				}
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002F40 File Offset: 0x00001140
		private bool IsRepeatable()
		{
			return this.m_Delay > 0L || this.m_Interval > 0L;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002F6C File Offset: 0x0000116C
		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<MouseDownEvent>(new EventCallback<MouseDownEvent>(this.OnMouseDown), this.invokePolicy, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<MouseMoveEvent>(new EventCallback<MouseMoveEvent>(this.OnMouseMove), this.invokePolicy, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnMouseUp), InvokePolicy.IncludeDisabled, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<MouseCaptureOutEvent>(new EventCallback<MouseCaptureOutEvent>(this.OnMouseCaptureOut), InvokePolicy.IncludeDisabled, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), this.invokePolicy, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), this.invokePolicy, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), InvokePolicy.IncludeDisabled, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), InvokePolicy.IncludeDisabled, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), InvokePolicy.IncludeDisabled, TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003078 File Offset: 0x00001278
		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<MouseDownEvent>(new EventCallback<MouseDownEvent>(this.OnMouseDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<MouseMoveEvent>(new EventCallback<MouseMoveEvent>(this.OnMouseMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnMouseUp), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<MouseCaptureOutEvent>(new EventCallback<MouseCaptureOutEvent>(this.OnMouseCaptureOut), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), TrickleDown.NoTrickleDown);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003168 File Offset: 0x00001368
		protected void OnMouseDown(MouseDownEvent evt)
		{
			bool flag = base.CanStartManipulation(evt);
			if (flag)
			{
				this.ProcessDownEvent(evt, evt.localMousePosition, PointerId.mousePointerId);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003194 File Offset: 0x00001394
		protected void OnMouseMove(MouseMoveEvent evt)
		{
			bool active = this.active;
			if (active)
			{
				this.ProcessMoveEvent(evt, evt.localMousePosition);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000031BC File Offset: 0x000013BC
		protected void OnMouseUp(MouseUpEvent evt)
		{
			bool flag = this.active && base.CanStopManipulation(evt);
			if (flag)
			{
				this.ProcessUpEvent(evt, evt.localMousePosition, PointerId.mousePointerId);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000031F4 File Offset: 0x000013F4
		private void OnMouseCaptureOut(MouseCaptureOutEvent evt)
		{
			bool active = this.active;
			if (active)
			{
				this.ProcessCancelEvent(evt, PointerId.mousePointerId);
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000321C File Offset: 0x0000141C
		private void OnPointerDown(PointerDownEvent evt)
		{
			bool flag = !base.CanStartManipulation(evt);
			if (!flag)
			{
				bool flag2 = evt.pointerId != PointerId.mousePointerId;
				if (flag2)
				{
					this.ProcessDownEvent(evt, evt.localPosition, evt.pointerId);
					base.target.panel.PreventCompatibilityMouseEvents(evt.pointerId);
				}
				else
				{
					evt.StopImmediatePropagation();
				}
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000328C File Offset: 0x0000148C
		private void OnPointerMove(PointerMoveEvent evt)
		{
			bool flag = !this.active;
			if (!flag)
			{
				bool flag2 = evt.pointerId != PointerId.mousePointerId;
				if (flag2)
				{
					this.ProcessMoveEvent(evt, evt.localPosition);
					base.target.panel.PreventCompatibilityMouseEvents(evt.pointerId);
				}
				else
				{
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000032F4 File Offset: 0x000014F4
		private void OnPointerUp(PointerUpEvent evt)
		{
			bool flag = !this.active || !base.CanStopManipulation(evt);
			if (!flag)
			{
				bool flag2 = evt.pointerId != PointerId.mousePointerId;
				if (flag2)
				{
					this.ProcessUpEvent(evt, evt.localPosition, evt.pointerId);
					base.target.panel.PreventCompatibilityMouseEvents(evt.pointerId);
				}
				else
				{
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003370 File Offset: 0x00001570
		private void OnPointerCancel(PointerCancelEvent evt)
		{
			bool flag = !this.active || !base.CanStopManipulation(evt);
			if (!flag)
			{
				bool flag2 = Clickable.IsNotMouseEvent(evt.pointerId);
				if (flag2)
				{
					this.ProcessCancelEvent(evt, evt.pointerId);
				}
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000033B8 File Offset: 0x000015B8
		private void OnPointerCaptureOut(PointerCaptureOutEvent evt)
		{
			bool flag = !this.active;
			if (!flag)
			{
				bool flag2 = Clickable.IsNotMouseEvent(evt.pointerId);
				if (flag2)
				{
					this.ProcessCancelEvent(evt, evt.pointerId);
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000033F4 File Offset: 0x000015F4
		private bool ContainsPointer(int pointerId)
		{
			VisualElement topElementUnderPointer = base.target.elementPanel.GetTopElementUnderPointer(pointerId);
			return base.target == topElementUnderPointer || base.target.Contains(topElementUnderPointer);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003430 File Offset: 0x00001630
		private static bool IsNotMouseEvent(int pointerId)
		{
			return pointerId != PointerId.mousePointerId;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000344D File Offset: 0x0000164D
		protected void Invoke(EventBase evt)
		{
			Action action = this.clicked;
			if (action != null)
			{
				action.Invoke();
			}
			Action<EventBase> action2 = this.clickedWithEventInfo;
			if (action2 != null)
			{
				action2.Invoke(evt);
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003478 File Offset: 0x00001678
		internal void SimulateSingleClick(EventBase evt, int delayMs = 100)
		{
			base.target.pseudoStates |= PseudoStates.Active;
			base.target.schedule.Execute(delegate
			{
				base.target.pseudoStates &= ~PseudoStates.Active;
			}).ExecuteLater((long)delayMs);
			this.Invoke(evt);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000034C8 File Offset: 0x000016C8
		protected virtual void ProcessDownEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.active = true;
			this.m_ActivePointerId = pointerId;
			base.target.CapturePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(pointerId);
			}
			this.lastMousePosition = localPosition;
			bool flag2 = this.IsRepeatable();
			if (flag2)
			{
				bool flag3 = this.ContainsPointer(pointerId);
				if (flag3)
				{
					this.Invoke(evt);
				}
				bool flag4 = this.m_Repeater == null;
				if (flag4)
				{
					this.m_Repeater = base.target.schedule.Execute(new Action<TimerState>(this.OnTimer)).Every(this.m_Interval).StartingIn(this.m_Delay);
				}
				else
				{
					this.m_Repeater.ExecuteLater(this.m_Delay);
				}
			}
			base.target.pseudoStates |= PseudoStates.Active;
			evt.StopImmediatePropagation();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000035B8 File Offset: 0x000017B8
		protected virtual void ProcessMoveEvent(EventBase evt, Vector2 localPosition)
		{
			this.lastMousePosition = localPosition;
			bool flag = this.ContainsPointer(this.m_ActivePointerId);
			if (flag)
			{
				base.target.pseudoStates |= PseudoStates.Active;
			}
			else
			{
				base.target.pseudoStates &= ~PseudoStates.Active;
			}
			evt.StopPropagation();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003614 File Offset: 0x00001814
		protected virtual void ProcessUpEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.active = false;
			this.m_ActivePointerId = -1;
			base.target.ReleasePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(pointerId);
			}
			base.target.pseudoStates &= ~PseudoStates.Active;
			bool flag2 = this.IsRepeatable();
			if (flag2)
			{
				IVisualElementScheduledItem repeater = this.m_Repeater;
				if (repeater != null)
				{
					repeater.Pause();
				}
			}
			else
			{
				bool flag3 = this.ContainsPointer(pointerId) && base.target.enabledInHierarchy;
				if (flag3)
				{
					this.Invoke(evt);
				}
			}
			evt.StopPropagation();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000036C4 File Offset: 0x000018C4
		protected virtual void ProcessCancelEvent(EventBase evt, int pointerId)
		{
			this.active = false;
			this.m_ActivePointerId = -1;
			base.target.ReleasePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(pointerId);
			}
			base.target.pseudoStates &= ~PseudoStates.Active;
			bool flag2 = this.IsRepeatable();
			if (flag2)
			{
				IVisualElementScheduledItem repeater = this.m_Repeater;
				if (repeater != null)
				{
					repeater.Pause();
				}
			}
			evt.StopPropagation();
		}

		// Token: 0x04000026 RID: 38
		private readonly long m_Delay;

		// Token: 0x04000027 RID: 39
		private readonly long m_Interval;

		// Token: 0x0400002A RID: 42
		private int m_ActivePointerId;

		// Token: 0x0400002B RID: 43
		private bool m_AcceptClicksIfDisabled;

		// Token: 0x0400002C RID: 44
		private IVisualElementScheduledItem m_Repeater;
	}
}
