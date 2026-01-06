using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F1 RID: 497
	public abstract class MouseEventBase<T> : EventBase<T>, IMouseEvent, IMouseEventInternal where T : MouseEventBase<T>, new()
	{
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x0003D0D1 File Offset: 0x0003B2D1
		// (set) Token: 0x06000F54 RID: 3924 RVA: 0x0003D0D9 File Offset: 0x0003B2D9
		public EventModifiers modifiers { get; protected set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0003D0E2 File Offset: 0x0003B2E2
		// (set) Token: 0x06000F56 RID: 3926 RVA: 0x0003D0EA File Offset: 0x0003B2EA
		public Vector2 mousePosition { get; protected set; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0003D0F3 File Offset: 0x0003B2F3
		// (set) Token: 0x06000F58 RID: 3928 RVA: 0x0003D0FB File Offset: 0x0003B2FB
		public Vector2 localMousePosition { get; internal set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0003D104 File Offset: 0x0003B304
		// (set) Token: 0x06000F5A RID: 3930 RVA: 0x0003D10C File Offset: 0x0003B30C
		public Vector2 mouseDelta { get; protected set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x0003D115 File Offset: 0x0003B315
		// (set) Token: 0x06000F5C RID: 3932 RVA: 0x0003D11D File Offset: 0x0003B31D
		public int clickCount { get; protected set; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x0003D126 File Offset: 0x0003B326
		// (set) Token: 0x06000F5E RID: 3934 RVA: 0x0003D12E File Offset: 0x0003B32E
		public int button { get; protected set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x0003D137 File Offset: 0x0003B337
		// (set) Token: 0x06000F60 RID: 3936 RVA: 0x0003D13F File Offset: 0x0003B33F
		public int pressedButtons { get; protected set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x0003D148 File Offset: 0x0003B348
		public bool shiftKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x0003D168 File Offset: 0x0003B368
		public bool ctrlKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x0003D188 File Offset: 0x0003B388
		public bool commandKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0003D1A8 File Offset: 0x0003B3A8
		public bool altKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0003D1C8 File Offset: 0x0003B3C8
		public bool actionKey
		{
			get
			{
				bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
				bool flag2;
				if (flag)
				{
					flag2 = this.commandKey;
				}
				else
				{
					flag2 = this.ctrlKey;
				}
				return flag2;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x0003D201 File Offset: 0x0003B401
		// (set) Token: 0x06000F67 RID: 3943 RVA: 0x0003D209 File Offset: 0x0003B409
		bool IMouseEventInternal.triggeredByOS { get; set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0003D212 File Offset: 0x0003B412
		// (set) Token: 0x06000F69 RID: 3945 RVA: 0x0003D21A File Offset: 0x0003B41A
		bool IMouseEventInternal.recomputeTopElementUnderMouse { get; set; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x0003D223 File Offset: 0x0003B423
		// (set) Token: 0x06000F6B RID: 3947 RVA: 0x0003D22B File Offset: 0x0003B42B
		IPointerEvent IMouseEventInternal.sourcePointerEvent { get; set; }

		// Token: 0x06000F6C RID: 3948 RVA: 0x0003D234 File Offset: 0x0003B434
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0003D248 File Offset: 0x0003B448
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable;
			this.modifiers = EventModifiers.None;
			this.mousePosition = Vector2.zero;
			this.localMousePosition = Vector2.zero;
			this.mouseDelta = Vector2.zero;
			this.clickCount = 0;
			this.button = 0;
			this.pressedButtons = 0;
			((IMouseEventInternal)this).triggeredByOS = false;
			((IMouseEventInternal)this).recomputeTopElementUnderMouse = true;
			((IMouseEventInternal)this).sourcePointerEvent = null;
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0003D2BC File Offset: 0x0003B4BC
		// (set) Token: 0x06000F6F RID: 3951 RVA: 0x0003D2D4 File Offset: 0x0003B4D4
		public override IEventHandler currentTarget
		{
			get
			{
				return base.currentTarget;
			}
			internal set
			{
				base.currentTarget = value;
				VisualElement visualElement = this.currentTarget as VisualElement;
				bool flag = visualElement != null;
				if (flag)
				{
					this.localMousePosition = visualElement.WorldToLocal(this.mousePosition);
				}
				else
				{
					this.localMousePosition = this.mousePosition;
				}
			}
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0003D324 File Offset: 0x0003B524
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool triggeredByOS = ((IMouseEventInternal)this).triggeredByOS;
			if (triggeredByOS)
			{
				PointerDeviceState.SavePointerPosition(PointerId.mousePointerId, this.mousePosition, panel, panel.contextType);
			}
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0003D360 File Offset: 0x0003B560
		protected internal override void PostDispatch(IPanel panel)
		{
			EventBase eventBase = ((IMouseEventInternal)this).sourcePointerEvent as EventBase;
			bool flag = eventBase != null;
			if (flag)
			{
				Debug.Assert(eventBase.processed);
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					baseVisualElementPanel.CommitElementUnderPointers();
				}
				bool isPropagationStopped = base.isPropagationStopped;
				if (isPropagationStopped)
				{
					eventBase.StopPropagation();
				}
				bool isImmediatePropagationStopped = base.isImmediatePropagationStopped;
				if (isImmediatePropagationStopped)
				{
					eventBase.StopImmediatePropagation();
				}
				bool isDefaultPrevented = base.isDefaultPrevented;
				if (isDefaultPrevented)
				{
					eventBase.PreventDefault();
				}
				eventBase.processedByFocusController |= base.processedByFocusController;
			}
			base.PostDispatch(panel);
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0003D3FC File Offset: 0x0003B5FC
		public static T GetPooled(Event systemEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.imguiEvent = systemEvent;
			bool flag = systemEvent != null;
			if (flag)
			{
				pooled.modifiers = systemEvent.modifiers;
				pooled.mousePosition = systemEvent.mousePosition;
				pooled.localMousePosition = systemEvent.mousePosition;
				pooled.mouseDelta = systemEvent.delta;
				pooled.button = systemEvent.button;
				pooled.pressedButtons = PointerDeviceState.GetPressedButtons(PointerId.mousePointerId);
				pooled.clickCount = systemEvent.clickCount;
				pooled.triggeredByOS = true;
				pooled.recomputeTopElementUnderMouse = true;
			}
			return pooled;
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0003D4CC File Offset: 0x0003B6CC
		public static T GetPooled(Vector2 position, int button, int clickCount, Vector2 delta, EventModifiers modifiers = EventModifiers.None)
		{
			return MouseEventBase<T>.GetPooled(position, button, clickCount, delta, modifiers, false);
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0003D4EC File Offset: 0x0003B6EC
		internal static T GetPooled(Vector2 position, int button, int clickCount, Vector2 delta, EventModifiers modifiers, bool fromOS)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.modifiers = modifiers;
			pooled.mousePosition = position;
			pooled.localMousePosition = position;
			pooled.mouseDelta = delta;
			pooled.button = button;
			pooled.pressedButtons = PointerDeviceState.GetPressedButtons(PointerId.mousePointerId);
			pooled.clickCount = clickCount;
			pooled.triggeredByOS = fromOS;
			pooled.recomputeTopElementUnderMouse = true;
			return pooled;
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0003D588 File Offset: 0x0003B788
		internal static T GetPooled(IMouseEvent triggerEvent, Vector2 mousePosition, bool recomputeTopElementUnderMouse)
		{
			bool flag = triggerEvent != null;
			T t;
			if (flag)
			{
				t = MouseEventBase<T>.GetPooled(triggerEvent);
			}
			else
			{
				T pooled = EventBase<T>.GetPooled();
				pooled.mousePosition = mousePosition;
				pooled.localMousePosition = mousePosition;
				pooled.recomputeTopElementUnderMouse = recomputeTopElementUnderMouse;
				t = pooled;
			}
			return t;
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0003D5DC File Offset: 0x0003B7DC
		public static T GetPooled(IMouseEvent triggerEvent)
		{
			T pooled = EventBase<T>.GetPooled(triggerEvent as EventBase);
			bool flag = triggerEvent != null;
			if (flag)
			{
				pooled.modifiers = triggerEvent.modifiers;
				pooled.mousePosition = triggerEvent.mousePosition;
				pooled.localMousePosition = triggerEvent.mousePosition;
				pooled.mouseDelta = triggerEvent.mouseDelta;
				pooled.button = triggerEvent.button;
				pooled.pressedButtons = triggerEvent.pressedButtons;
				pooled.clickCount = triggerEvent.clickCount;
				IMouseEventInternal mouseEventInternal = triggerEvent as IMouseEventInternal;
				bool flag2 = mouseEventInternal != null;
				if (flag2)
				{
					pooled.triggeredByOS = mouseEventInternal.triggeredByOS;
					pooled.recomputeTopElementUnderMouse = false;
				}
			}
			return pooled;
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0003D6B8 File Offset: 0x0003B8B8
		protected static T GetPooled(IPointerEvent pointerEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			EventBase eventBase = pooled;
			EventBase eventBase2 = pointerEvent as EventBase;
			eventBase.target = ((eventBase2 != null) ? eventBase2.target : null);
			EventBase eventBase3 = pooled;
			EventBase eventBase4 = pointerEvent as EventBase;
			eventBase3.imguiEvent = ((eventBase4 != null) ? eventBase4.imguiEvent : null);
			EventBase eventBase5 = pointerEvent as EventBase;
			bool flag = ((eventBase5 != null) ? eventBase5.path : null) != null;
			if (flag)
			{
				pooled.path = (pointerEvent as EventBase).path;
			}
			pooled.modifiers = pointerEvent.modifiers;
			pooled.mousePosition = pointerEvent.position;
			pooled.localMousePosition = pointerEvent.position;
			pooled.mouseDelta = pointerEvent.deltaPosition;
			pooled.button = ((pointerEvent.button == -1) ? 0 : pointerEvent.button);
			pooled.pressedButtons = pointerEvent.pressedButtons;
			pooled.clickCount = pointerEvent.clickCount;
			IPointerEventInternal pointerEventInternal = pointerEvent as IPointerEventInternal;
			bool flag2 = pointerEventInternal != null;
			if (flag2)
			{
				pooled.triggeredByOS = pointerEventInternal.triggeredByOS;
				pooled.recomputeTopElementUnderMouse = true;
				pooled.sourcePointerEvent = pointerEvent;
			}
			return pooled;
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0003D815 File Offset: 0x0003BA15
		protected MouseEventBase()
		{
			this.LocalInit();
		}
	}
}
