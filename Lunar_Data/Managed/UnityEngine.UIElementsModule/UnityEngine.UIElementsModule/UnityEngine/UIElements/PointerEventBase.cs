using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000216 RID: 534
	public abstract class PointerEventBase<T> : EventBase<T>, IPointerEvent, IPointerEventInternal where T : PointerEventBase<T>, new()
	{
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x0003EE82 File Offset: 0x0003D082
		// (set) Token: 0x06001013 RID: 4115 RVA: 0x0003EE8A File Offset: 0x0003D08A
		public int pointerId { get; protected set; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x0003EE93 File Offset: 0x0003D093
		// (set) Token: 0x06001015 RID: 4117 RVA: 0x0003EE9B File Offset: 0x0003D09B
		public string pointerType { get; protected set; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x0003EEA4 File Offset: 0x0003D0A4
		// (set) Token: 0x06001017 RID: 4119 RVA: 0x0003EEAC File Offset: 0x0003D0AC
		public bool isPrimary { get; protected set; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0003EEB5 File Offset: 0x0003D0B5
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x0003EEBD File Offset: 0x0003D0BD
		public int button { get; protected set; }

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x0003EEC6 File Offset: 0x0003D0C6
		// (set) Token: 0x0600101B RID: 4123 RVA: 0x0003EECE File Offset: 0x0003D0CE
		public int pressedButtons { get; protected set; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x0003EED7 File Offset: 0x0003D0D7
		// (set) Token: 0x0600101D RID: 4125 RVA: 0x0003EEDF File Offset: 0x0003D0DF
		public Vector3 position { get; protected set; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x0003EEE8 File Offset: 0x0003D0E8
		// (set) Token: 0x0600101F RID: 4127 RVA: 0x0003EEF0 File Offset: 0x0003D0F0
		public Vector3 localPosition { get; protected set; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x0003EEF9 File Offset: 0x0003D0F9
		// (set) Token: 0x06001021 RID: 4129 RVA: 0x0003EF01 File Offset: 0x0003D101
		public Vector3 deltaPosition { get; protected set; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x0003EF0A File Offset: 0x0003D10A
		// (set) Token: 0x06001023 RID: 4131 RVA: 0x0003EF12 File Offset: 0x0003D112
		public float deltaTime { get; protected set; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0003EF1B File Offset: 0x0003D11B
		// (set) Token: 0x06001025 RID: 4133 RVA: 0x0003EF23 File Offset: 0x0003D123
		public int clickCount { get; protected set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x0003EF2C File Offset: 0x0003D12C
		// (set) Token: 0x06001027 RID: 4135 RVA: 0x0003EF34 File Offset: 0x0003D134
		public float pressure { get; protected set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x0003EF3D File Offset: 0x0003D13D
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x0003EF45 File Offset: 0x0003D145
		public float tangentialPressure { get; protected set; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0003EF4E File Offset: 0x0003D14E
		// (set) Token: 0x0600102B RID: 4139 RVA: 0x0003EF56 File Offset: 0x0003D156
		public float altitudeAngle { get; protected set; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x0003EF5F File Offset: 0x0003D15F
		// (set) Token: 0x0600102D RID: 4141 RVA: 0x0003EF67 File Offset: 0x0003D167
		public float azimuthAngle { get; protected set; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0003EF70 File Offset: 0x0003D170
		// (set) Token: 0x0600102F RID: 4143 RVA: 0x0003EF78 File Offset: 0x0003D178
		public float twist { get; protected set; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0003EF81 File Offset: 0x0003D181
		// (set) Token: 0x06001031 RID: 4145 RVA: 0x0003EF89 File Offset: 0x0003D189
		public Vector2 radius { get; protected set; }

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x0003EF92 File Offset: 0x0003D192
		// (set) Token: 0x06001033 RID: 4147 RVA: 0x0003EF9A File Offset: 0x0003D19A
		public Vector2 radiusVariance { get; protected set; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x0003EFA3 File Offset: 0x0003D1A3
		// (set) Token: 0x06001035 RID: 4149 RVA: 0x0003EFAB File Offset: 0x0003D1AB
		public EventModifiers modifiers { get; protected set; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x0003EFB4 File Offset: 0x0003D1B4
		public bool shiftKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x0003EFD4 File Offset: 0x0003D1D4
		public bool ctrlKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x0003EFF4 File Offset: 0x0003D1F4
		public bool commandKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x0003F014 File Offset: 0x0003D214
		public bool altKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x0003F034 File Offset: 0x0003D234
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

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x0003F06D File Offset: 0x0003D26D
		// (set) Token: 0x0600103C RID: 4156 RVA: 0x0003F075 File Offset: 0x0003D275
		bool IPointerEventInternal.triggeredByOS { get; set; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x0003F07E File Offset: 0x0003D27E
		// (set) Token: 0x0600103E RID: 4158 RVA: 0x0003F086 File Offset: 0x0003D286
		bool IPointerEventInternal.recomputeTopElementUnderPointer { get; set; }

		// Token: 0x0600103F RID: 4159 RVA: 0x0003F08F File Offset: 0x0003D28F
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0003F0A0 File Offset: 0x0003D2A0
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable;
			base.propagateToIMGUI = false;
			this.pointerId = 0;
			this.pointerType = PointerType.unknown;
			this.isPrimary = false;
			this.button = -1;
			this.pressedButtons = 0;
			this.position = Vector3.zero;
			this.localPosition = Vector3.zero;
			this.deltaPosition = Vector3.zero;
			this.deltaTime = 0f;
			this.clickCount = 0;
			this.pressure = 0f;
			this.tangentialPressure = 0f;
			this.altitudeAngle = 0f;
			this.azimuthAngle = 0f;
			this.twist = 0f;
			this.radius = Vector2.zero;
			this.radiusVariance = Vector2.zero;
			this.modifiers = EventModifiers.None;
			((IPointerEventInternal)this).triggeredByOS = false;
			((IPointerEventInternal)this).recomputeTopElementUnderPointer = false;
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x0003F190 File Offset: 0x0003D390
		// (set) Token: 0x06001042 RID: 4162 RVA: 0x0003F1A8 File Offset: 0x0003D3A8
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
					this.localPosition = visualElement.WorldToLocal(this.position);
				}
				else
				{
					this.localPosition = this.position;
				}
			}
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0003F204 File Offset: 0x0003D404
		private static bool IsMouse(Event systemEvent)
		{
			EventType rawType = systemEvent.rawType;
			return rawType == EventType.MouseMove || rawType == EventType.MouseDown || rawType == EventType.MouseUp || rawType == EventType.MouseDrag || rawType == EventType.ContextClick || rawType == EventType.MouseEnterWindow || rawType == EventType.MouseLeaveWindow;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0003F240 File Offset: 0x0003D440
		public static T GetPooled(Event systemEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			bool flag = !PointerEventBase<T>.IsMouse(systemEvent) && systemEvent.rawType != EventType.DragUpdated;
			if (flag)
			{
				Debug.Assert(false, string.Concat(new string[]
				{
					"Unexpected event type: ",
					systemEvent.rawType.ToString(),
					" (",
					systemEvent.type.ToString(),
					")"
				}));
			}
			PointerType pointerType = systemEvent.pointerType;
			PointerType pointerType2 = pointerType;
			if (pointerType2 != PointerType.Touch)
			{
				if (pointerType2 != PointerType.Pen)
				{
					pooled.pointerType = PointerType.mouse;
					pooled.pointerId = PointerId.mousePointerId;
				}
				else
				{
					pooled.pointerType = PointerType.pen;
					pooled.pointerId = PointerId.penPointerIdBase;
				}
			}
			else
			{
				pooled.pointerType = PointerType.touch;
				pooled.pointerId = PointerId.touchPointerIdBase;
			}
			pooled.isPrimary = true;
			pooled.altitudeAngle = 0f;
			pooled.azimuthAngle = 0f;
			pooled.twist = 0f;
			pooled.radius = Vector2.zero;
			pooled.radiusVariance = Vector2.zero;
			pooled.imguiEvent = systemEvent;
			bool flag2 = systemEvent.rawType == EventType.MouseDown;
			if (flag2)
			{
				PointerDeviceState.PressButton(PointerId.mousePointerId, systemEvent.button);
				pooled.button = systemEvent.button;
			}
			else
			{
				bool flag3 = systemEvent.rawType == EventType.MouseUp;
				if (flag3)
				{
					PointerDeviceState.ReleaseButton(PointerId.mousePointerId, systemEvent.button);
					pooled.button = systemEvent.button;
				}
				else
				{
					bool flag4 = systemEvent.rawType == EventType.MouseMove;
					if (flag4)
					{
						pooled.button = -1;
					}
				}
			}
			pooled.pressedButtons = PointerDeviceState.GetPressedButtons(pooled.pointerId);
			pooled.position = systemEvent.mousePosition;
			pooled.localPosition = systemEvent.mousePosition;
			pooled.deltaPosition = systemEvent.delta;
			pooled.clickCount = systemEvent.clickCount;
			pooled.modifiers = systemEvent.modifiers;
			PointerType pointerType3 = systemEvent.pointerType;
			PointerType pointerType4 = pointerType3;
			if (pointerType4 - PointerType.Touch > 1)
			{
				pooled.pressure = ((pooled.pressedButtons == 0) ? 0f : 0.5f);
			}
			else
			{
				pooled.pressure = systemEvent.pressure;
			}
			pooled.tangentialPressure = 0f;
			pooled.triggeredByOS = true;
			return pooled;
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0003F540 File Offset: 0x0003D740
		public static T GetPooled(Touch touch, EventModifiers modifiers = EventModifiers.None)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.pointerId = touch.fingerId + PointerId.touchPointerIdBase;
			pooled.pointerType = PointerType.touch;
			bool flag = false;
			for (int i = PointerId.touchPointerIdBase; i < PointerId.touchPointerIdBase + PointerId.touchPointerCount; i++)
			{
				bool flag2 = i != pooled.pointerId && PointerDeviceState.GetPressedButtons(i) != 0;
				if (flag2)
				{
					flag = true;
					break;
				}
			}
			pooled.isPrimary = !flag;
			bool flag3 = touch.phase == TouchPhase.Began;
			if (flag3)
			{
				PointerDeviceState.PressButton(pooled.pointerId, 0);
				pooled.button = 0;
			}
			else
			{
				bool flag4 = touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled;
				if (flag4)
				{
					PointerDeviceState.ReleaseButton(pooled.pointerId, 0);
					pooled.button = 0;
				}
				else
				{
					pooled.button = -1;
				}
			}
			pooled.pressedButtons = PointerDeviceState.GetPressedButtons(pooled.pointerId);
			pooled.position = touch.position;
			pooled.localPosition = touch.position;
			pooled.deltaPosition = touch.deltaPosition;
			pooled.deltaTime = touch.deltaTime;
			pooled.clickCount = touch.tapCount;
			pooled.pressure = ((Mathf.Abs(touch.maximumPossiblePressure) > 1E-30f) ? (touch.pressure / touch.maximumPossiblePressure) : 1f);
			pooled.tangentialPressure = 0f;
			pooled.altitudeAngle = touch.altitudeAngle;
			pooled.azimuthAngle = touch.azimuthAngle;
			pooled.twist = 0f;
			pooled.radius = new Vector2(touch.radius, touch.radius);
			pooled.radiusVariance = new Vector2(touch.radiusVariance, touch.radiusVariance);
			pooled.modifiers = modifiers;
			pooled.triggeredByOS = true;
			return pooled;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0003F7C0 File Offset: 0x0003D9C0
		internal static T GetPooled(IPointerEvent triggerEvent, Vector2 position, int pointerId)
		{
			bool flag = triggerEvent != null;
			T t;
			if (flag)
			{
				t = PointerEventBase<T>.GetPooled(triggerEvent);
			}
			else
			{
				T pooled = EventBase<T>.GetPooled();
				pooled.position = position;
				pooled.localPosition = position;
				pooled.pointerId = pointerId;
				pooled.pointerType = PointerType.GetPointerType(pointerId);
				t = pooled;
			}
			return t;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0003F830 File Offset: 0x0003DA30
		public static T GetPooled(IPointerEvent triggerEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			bool flag = triggerEvent != null;
			if (flag)
			{
				pooled.pointerId = triggerEvent.pointerId;
				pooled.pointerType = triggerEvent.pointerType;
				pooled.isPrimary = triggerEvent.isPrimary;
				pooled.button = triggerEvent.button;
				pooled.pressedButtons = triggerEvent.pressedButtons;
				pooled.position = triggerEvent.position;
				pooled.localPosition = triggerEvent.localPosition;
				pooled.deltaPosition = triggerEvent.deltaPosition;
				pooled.deltaTime = triggerEvent.deltaTime;
				pooled.clickCount = triggerEvent.clickCount;
				pooled.pressure = triggerEvent.pressure;
				pooled.tangentialPressure = triggerEvent.tangentialPressure;
				pooled.altitudeAngle = triggerEvent.altitudeAngle;
				pooled.azimuthAngle = triggerEvent.azimuthAngle;
				pooled.twist = triggerEvent.twist;
				pooled.radius = triggerEvent.radius;
				pooled.radiusVariance = triggerEvent.radiusVariance;
				pooled.modifiers = triggerEvent.modifiers;
				IPointerEventInternal pointerEventInternal = triggerEvent as IPointerEventInternal;
				bool flag2 = pointerEventInternal != null;
				if (flag2)
				{
					pooled.triggeredByOS |= pointerEventInternal.triggeredByOS;
				}
			}
			return pooled;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0003F9C8 File Offset: 0x0003DBC8
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool triggeredByOS = ((IPointerEventInternal)this).triggeredByOS;
			if (triggeredByOS)
			{
				PointerDeviceState.SavePointerPosition(this.pointerId, this.position, panel, panel.contextType);
			}
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0003FA08 File Offset: 0x0003DC08
		protected internal override void PostDispatch(IPanel panel)
		{
			for (int i = 0; i < PointerId.maxPointers; i++)
			{
				panel.ProcessPointerCapture(i);
			}
			bool flag = !panel.ShouldSendCompatibilityMouseEvents(this) && ((IPointerEventInternal)this).triggeredByOS;
			if (flag)
			{
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					baseVisualElementPanel.CommitElementUnderPointers();
				}
			}
			base.PostDispatch(panel);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0003FA66 File Offset: 0x0003DC66
		protected PointerEventBase()
		{
			this.LocalInit();
		}
	}
}
