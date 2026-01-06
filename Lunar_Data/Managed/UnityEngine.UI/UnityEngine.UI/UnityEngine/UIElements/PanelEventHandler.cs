using System;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004A RID: 74
	[AddComponentMenu("UI Toolkit/Panel Event Handler (UI Toolkit)")]
	public class PanelEventHandler : UIBehaviour, IPointerMoveHandler, IEventSystemHandler, IPointerUpHandler, IPointerDownHandler, ISubmitHandler, ICancelHandler, IMoveHandler, IScrollHandler, ISelectHandler, IDeselectHandler, IPointerExitHandler, IPointerEnterHandler, IRuntimePanelComponent
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x000171E9 File Offset: 0x000153E9
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x000171F4 File Offset: 0x000153F4
		public IPanel panel
		{
			get
			{
				return this.m_Panel;
			}
			set
			{
				BaseRuntimePanel baseRuntimePanel = (BaseRuntimePanel)value;
				if (this.m_Panel != baseRuntimePanel)
				{
					this.UnregisterCallbacks();
					this.m_Panel = baseRuntimePanel;
					this.RegisterCallbacks();
				}
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00017224 File Offset: 0x00015424
		private GameObject selectableGameObject
		{
			get
			{
				BaseRuntimePanel panel = this.m_Panel;
				if (panel == null)
				{
					return null;
				}
				return panel.selectableGameObject;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00017237 File Offset: 0x00015437
		private EventSystem eventSystem
		{
			get
			{
				return UIElementsRuntimeUtility.activeEventSystem as EventSystem;
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00017243 File Offset: 0x00015443
		protected override void OnEnable()
		{
			base.OnEnable();
			this.RegisterCallbacks();
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00017251 File Offset: 0x00015451
		protected override void OnDisable()
		{
			base.OnDisable();
			this.UnregisterCallbacks();
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00017260 File Offset: 0x00015460
		private void RegisterCallbacks()
		{
			if (this.m_Panel != null)
			{
				this.m_Panel.destroyed += this.OnPanelDestroyed;
				this.m_Panel.visualTree.RegisterCallback<FocusEvent>(new EventCallback<FocusEvent>(this.OnElementFocus), TrickleDown.TrickleDown);
				this.m_Panel.visualTree.RegisterCallback<BlurEvent>(new EventCallback<BlurEvent>(this.OnElementBlur), TrickleDown.TrickleDown);
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000172C8 File Offset: 0x000154C8
		private void UnregisterCallbacks()
		{
			if (this.m_Panel != null)
			{
				this.m_Panel.destroyed -= this.OnPanelDestroyed;
				this.m_Panel.visualTree.UnregisterCallback<FocusEvent>(new EventCallback<FocusEvent>(this.OnElementFocus), TrickleDown.TrickleDown);
				this.m_Panel.visualTree.UnregisterCallback<BlurEvent>(new EventCallback<BlurEvent>(this.OnElementBlur), TrickleDown.TrickleDown);
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001732E File Offset: 0x0001552E
		private void OnPanelDestroyed()
		{
			this.panel = null;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00017337 File Offset: 0x00015537
		private void OnElementFocus(FocusEvent e)
		{
			if (!this.m_Selecting && this.eventSystem != null)
			{
				this.eventSystem.SetSelectedGameObject(this.selectableGameObject);
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00017360 File Offset: 0x00015560
		private void OnElementBlur(BlurEvent e)
		{
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00017364 File Offset: 0x00015564
		public void OnSelect(BaseEventData eventData)
		{
			this.m_Selecting = true;
			try
			{
				BaseRuntimePanel panel = this.m_Panel;
				if (panel != null)
				{
					panel.Focus();
				}
			}
			finally
			{
				this.m_Selecting = false;
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000173A4 File Offset: 0x000155A4
		public void OnDeselect(BaseEventData eventData)
		{
			BaseRuntimePanel panel = this.m_Panel;
			if (panel == null)
			{
				return;
			}
			panel.Blur();
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x000173B8 File Offset: 0x000155B8
		public void OnPointerMove(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Default))
			{
				return;
			}
			using (PointerMoveEvent pooled = PointerEventBase<PointerMoveEvent>.GetPooled(this.m_PointerEvent))
			{
				this.SendEvent(pooled, eventData);
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00017410 File Offset: 0x00015610
		public void OnPointerUp(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Up))
			{
				return;
			}
			using (PointerUpEvent pooled = PointerEventBase<PointerUpEvent>.GetPooled(this.m_PointerEvent))
			{
				this.SendEvent(pooled, eventData);
				if (pooled.pressedButtons == 0)
				{
					PointerDeviceState.SetPlayerPanelWithSoftPointerCapture(pooled.pointerId, null);
				}
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001747C File Offset: 0x0001567C
		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Down))
			{
				return;
			}
			if (this.eventSystem != null)
			{
				this.eventSystem.SetSelectedGameObject(this.selectableGameObject);
			}
			using (PointerDownEvent pooled = PointerEventBase<PointerDownEvent>.GetPooled(this.m_PointerEvent))
			{
				this.SendEvent(pooled, eventData);
				PointerDeviceState.SetPlayerPanelWithSoftPointerCapture(pooled.pointerId, this.m_Panel);
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00017504 File Offset: 0x00015704
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Default))
			{
				return;
			}
			if (eventData.pointerCurrentRaycast.gameObject == base.gameObject && eventData.pointerPressRaycast.gameObject != base.gameObject && this.m_PointerEvent.pointerId != PointerId.mousePointerId)
			{
				using (PointerCancelEvent pooled = PointerEventBase<PointerCancelEvent>.GetPooled(this.m_PointerEvent))
				{
					this.SendEvent(pooled, eventData);
				}
			}
			this.m_Panel.PointerLeavesPanel(this.m_PointerEvent.pointerId, this.m_PointerEvent.position);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000175C8 File Offset: 0x000157C8
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Default))
			{
				return;
			}
			this.m_Panel.PointerEntersPanel(this.m_PointerEvent.pointerId, this.m_PointerEvent.position);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00017614 File Offset: 0x00015814
		public void OnSubmit(BaseEventData eventData)
		{
			if (this.m_Panel == null)
			{
				return;
			}
			using (NavigationSubmitEvent pooled = EventBase<NavigationSubmitEvent>.GetPooled())
			{
				this.SendEvent(pooled, eventData);
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00017654 File Offset: 0x00015854
		public void OnCancel(BaseEventData eventData)
		{
			if (this.m_Panel == null)
			{
				return;
			}
			using (NavigationCancelEvent pooled = EventBase<NavigationCancelEvent>.GetPooled())
			{
				this.SendEvent(pooled, eventData);
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00017694 File Offset: 0x00015894
		public void OnMove(AxisEventData eventData)
		{
			if (this.m_Panel == null)
			{
				return;
			}
			using (NavigationMoveEvent pooled = NavigationMoveEvent.GetPooled(eventData.moveVector))
			{
				this.SendEvent(pooled, eventData);
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x000176DC File Offset: 0x000158DC
		public void OnScroll(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Default))
			{
				return;
			}
			Vector2 vector = eventData.scrollDelta;
			vector.y = -vector.y;
			vector /= 20f;
			using (WheelEvent pooled = WheelEvent.GetPooled(vector, this.m_PointerEvent))
			{
				this.SendEvent(pooled, eventData);
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001775C File Offset: 0x0001595C
		private void SendEvent(EventBase e, BaseEventData sourceEventData)
		{
			this.m_Panel.SendEvent(e, DispatchMode.Default);
			if (e.isPropagationStopped)
			{
				sourceEventData.Use();
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00017779 File Offset: 0x00015979
		private void SendEvent(EventBase e, Event sourceEvent)
		{
			this.m_Panel.SendEvent(e, DispatchMode.Default);
			if (e.isPropagationStopped)
			{
				sourceEvent.Use();
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00017796 File Offset: 0x00015996
		private void Update()
		{
			if (this.m_Panel != null && this.eventSystem != null && this.eventSystem.currentSelectedGameObject == this.selectableGameObject)
			{
				this.ProcessImguiEvents(true);
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000177CD File Offset: 0x000159CD
		private void LateUpdate()
		{
			this.ProcessImguiEvents(false);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000177D8 File Offset: 0x000159D8
		private void ProcessImguiEvents(bool isSelected)
		{
			bool flag = true;
			while (Event.PopEvent(this.m_Event))
			{
				if (this.m_Event.type != EventType.Ignore && this.m_Event.type != EventType.Repaint && this.m_Event.type != EventType.Layout)
				{
					PanelEventHandler.s_Modifiers = (flag ? this.m_Event.modifiers : (PanelEventHandler.s_Modifiers | this.m_Event.modifiers));
					flag = false;
					if (isSelected)
					{
						this.ProcessKeyboardEvent(this.m_Event);
						if (this.m_Event.type != EventType.Used)
						{
							this.ProcessTabEvent(this.m_Event);
						}
					}
				}
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00017876 File Offset: 0x00015A76
		private void ProcessKeyboardEvent(Event e)
		{
			if (e.type == EventType.KeyUp)
			{
				this.SendKeyUpEvent(e);
				return;
			}
			if (e.type == EventType.KeyDown)
			{
				this.SendKeyDownEvent(e);
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00017899 File Offset: 0x00015A99
		private void ProcessTabEvent(Event e)
		{
			if (e.type == EventType.KeyDown && e.character == '\t')
			{
				this.SendTabEvent(e, e.shift ? (-1) : 1);
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x000178C4 File Offset: 0x00015AC4
		private void SendTabEvent(Event e, int direction)
		{
			using (NavigationTabEvent pooled = NavigationTabEvent.GetPooled(direction))
			{
				this.SendEvent(pooled, e);
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x000178FC File Offset: 0x00015AFC
		private void SendKeyUpEvent(Event e)
		{
			using (KeyUpEvent pooled = KeyboardEventBase<KeyUpEvent>.GetPooled('\0', e.keyCode, e.modifiers))
			{
				this.SendEvent(pooled, e);
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00017940 File Offset: 0x00015B40
		private void SendKeyDownEvent(Event e)
		{
			using (KeyDownEvent pooled = KeyboardEventBase<KeyDownEvent>.GetPooled(e.character, e.keyCode, e.modifiers))
			{
				this.SendEvent(pooled, e);
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001798C File Offset: 0x00015B8C
		private bool ReadPointerData(PanelEventHandler.PointerEvent pe, PointerEventData eventData, PanelEventHandler.PointerEventType eventType = PanelEventHandler.PointerEventType.Default)
		{
			if (this.eventSystem == null || this.eventSystem.currentInputModule == null)
			{
				return false;
			}
			pe.Read(this, eventData, eventType);
			Vector2 vector;
			Vector2 vector2;
			this.m_Panel.ScreenToPanel(pe.position, pe.deltaPosition, out vector, out vector2, true);
			pe.SetPosition(vector, vector2);
			return true;
		}

		// Token: 0x040001A5 RID: 421
		private BaseRuntimePanel m_Panel;

		// Token: 0x040001A6 RID: 422
		private readonly PanelEventHandler.PointerEvent m_PointerEvent = new PanelEventHandler.PointerEvent();

		// Token: 0x040001A7 RID: 423
		private bool m_Selecting;

		// Token: 0x040001A8 RID: 424
		private Event m_Event = new Event();

		// Token: 0x040001A9 RID: 425
		private static EventModifiers s_Modifiers;

		// Token: 0x020000BD RID: 189
		private enum PointerEventType
		{
			// Token: 0x04000321 RID: 801
			Default,
			// Token: 0x04000322 RID: 802
			Down,
			// Token: 0x04000323 RID: 803
			Up
		}

		// Token: 0x020000BE RID: 190
		private class PointerEvent : IPointerEvent
		{
			// Token: 0x170001DA RID: 474
			// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001C362 File Offset: 0x0001A562
			// (set) Token: 0x06000716 RID: 1814 RVA: 0x0001C36A File Offset: 0x0001A56A
			public int pointerId { get; private set; }

			// Token: 0x170001DB RID: 475
			// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001C373 File Offset: 0x0001A573
			// (set) Token: 0x06000718 RID: 1816 RVA: 0x0001C37B File Offset: 0x0001A57B
			public string pointerType { get; private set; }

			// Token: 0x170001DC RID: 476
			// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001C384 File Offset: 0x0001A584
			// (set) Token: 0x0600071A RID: 1818 RVA: 0x0001C38C File Offset: 0x0001A58C
			public bool isPrimary { get; private set; }

			// Token: 0x170001DD RID: 477
			// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001C395 File Offset: 0x0001A595
			// (set) Token: 0x0600071C RID: 1820 RVA: 0x0001C39D File Offset: 0x0001A59D
			public int button { get; private set; }

			// Token: 0x170001DE RID: 478
			// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001C3A6 File Offset: 0x0001A5A6
			// (set) Token: 0x0600071E RID: 1822 RVA: 0x0001C3AE File Offset: 0x0001A5AE
			public int pressedButtons { get; private set; }

			// Token: 0x170001DF RID: 479
			// (get) Token: 0x0600071F RID: 1823 RVA: 0x0001C3B7 File Offset: 0x0001A5B7
			// (set) Token: 0x06000720 RID: 1824 RVA: 0x0001C3BF File Offset: 0x0001A5BF
			public Vector3 position { get; private set; }

			// Token: 0x170001E0 RID: 480
			// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001C3C8 File Offset: 0x0001A5C8
			// (set) Token: 0x06000722 RID: 1826 RVA: 0x0001C3D0 File Offset: 0x0001A5D0
			public Vector3 localPosition { get; private set; }

			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001C3D9 File Offset: 0x0001A5D9
			// (set) Token: 0x06000724 RID: 1828 RVA: 0x0001C3E1 File Offset: 0x0001A5E1
			public Vector3 deltaPosition { get; private set; }

			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001C3EA File Offset: 0x0001A5EA
			// (set) Token: 0x06000726 RID: 1830 RVA: 0x0001C3F2 File Offset: 0x0001A5F2
			public float deltaTime { get; private set; }

			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x06000727 RID: 1831 RVA: 0x0001C3FB File Offset: 0x0001A5FB
			// (set) Token: 0x06000728 RID: 1832 RVA: 0x0001C403 File Offset: 0x0001A603
			public int clickCount { get; private set; }

			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001C40C File Offset: 0x0001A60C
			// (set) Token: 0x0600072A RID: 1834 RVA: 0x0001C414 File Offset: 0x0001A614
			public float pressure { get; private set; }

			// Token: 0x170001E5 RID: 485
			// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001C41D File Offset: 0x0001A61D
			// (set) Token: 0x0600072C RID: 1836 RVA: 0x0001C425 File Offset: 0x0001A625
			public float tangentialPressure { get; private set; }

			// Token: 0x170001E6 RID: 486
			// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001C42E File Offset: 0x0001A62E
			// (set) Token: 0x0600072E RID: 1838 RVA: 0x0001C436 File Offset: 0x0001A636
			public float altitudeAngle { get; private set; }

			// Token: 0x170001E7 RID: 487
			// (get) Token: 0x0600072F RID: 1839 RVA: 0x0001C43F File Offset: 0x0001A63F
			// (set) Token: 0x06000730 RID: 1840 RVA: 0x0001C447 File Offset: 0x0001A647
			public float azimuthAngle { get; private set; }

			// Token: 0x170001E8 RID: 488
			// (get) Token: 0x06000731 RID: 1841 RVA: 0x0001C450 File Offset: 0x0001A650
			// (set) Token: 0x06000732 RID: 1842 RVA: 0x0001C458 File Offset: 0x0001A658
			public float twist { get; private set; }

			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x06000733 RID: 1843 RVA: 0x0001C461 File Offset: 0x0001A661
			// (set) Token: 0x06000734 RID: 1844 RVA: 0x0001C469 File Offset: 0x0001A669
			public Vector2 radius { get; private set; }

			// Token: 0x170001EA RID: 490
			// (get) Token: 0x06000735 RID: 1845 RVA: 0x0001C472 File Offset: 0x0001A672
			// (set) Token: 0x06000736 RID: 1846 RVA: 0x0001C47A File Offset: 0x0001A67A
			public Vector2 radiusVariance { get; private set; }

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x06000737 RID: 1847 RVA: 0x0001C483 File Offset: 0x0001A683
			// (set) Token: 0x06000738 RID: 1848 RVA: 0x0001C48B File Offset: 0x0001A68B
			public EventModifiers modifiers { get; private set; }

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001C494 File Offset: 0x0001A694
			public bool shiftKey
			{
				get
				{
					return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
				}
			}

			// Token: 0x170001ED RID: 493
			// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001C4A1 File Offset: 0x0001A6A1
			public bool ctrlKey
			{
				get
				{
					return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
				}
			}

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x0600073B RID: 1851 RVA: 0x0001C4AE File Offset: 0x0001A6AE
			public bool commandKey
			{
				get
				{
					return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
				}
			}

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001C4BB File Offset: 0x0001A6BB
			public bool altKey
			{
				get
				{
					return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
				}
			}

			// Token: 0x170001F0 RID: 496
			// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001C4C8 File Offset: 0x0001A6C8
			public bool actionKey
			{
				get
				{
					if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.OSXPlayer)
					{
						return this.ctrlKey;
					}
					return this.commandKey;
				}
			}

			// Token: 0x0600073E RID: 1854 RVA: 0x0001C4E8 File Offset: 0x0001A6E8
			public void Read(PanelEventHandler self, PointerEventData eventData, PanelEventHandler.PointerEventType eventType)
			{
				this.pointerId = self.eventSystem.currentInputModule.ConvertUIToolkitPointerId(eventData);
				this.pointerType = (PanelEventHandler.PointerEvent.<Read>g__InRange|82_0(this.pointerId, PointerId.touchPointerIdBase, PointerId.touchPointerCount) ? PointerType.touch : (PanelEventHandler.PointerEvent.<Read>g__InRange|82_0(this.pointerId, PointerId.penPointerIdBase, PointerId.penPointerCount) ? PointerType.pen : PointerType.mouse));
				this.isPrimary = this.pointerId == PointerId.mousePointerId || this.pointerId == PointerId.touchPointerIdBase || this.pointerId == PointerId.penPointerIdBase;
				this.button = (int)eventData.button;
				this.clickCount = eventData.clickCount;
				int num = Screen.height;
				Vector3 vector = Display.RelativeMouseAt(eventData.position);
				if (vector != Vector3.zero)
				{
					int num2 = (int)vector.z;
					if (num2 > 0 && num2 < Display.displays.Length)
					{
						num = Display.displays[num2].systemHeight;
					}
				}
				else
				{
					vector = eventData.position;
				}
				Vector2 delta = eventData.delta;
				vector.y = (float)num - vector.y;
				delta.y = -delta.y;
				this.localPosition = (this.position = vector);
				this.deltaPosition = delta;
				this.deltaTime = 0f;
				this.pressure = eventData.pressure;
				this.tangentialPressure = eventData.tangentialPressure;
				this.altitudeAngle = eventData.altitudeAngle;
				this.azimuthAngle = eventData.azimuthAngle;
				this.twist = eventData.twist;
				this.radius = eventData.radius;
				this.radiusVariance = eventData.radiusVariance;
				this.modifiers = PanelEventHandler.s_Modifiers;
				if (eventType == PanelEventHandler.PointerEventType.Default)
				{
					this.button = -1;
					this.clickCount = 0;
				}
				else
				{
					this.button = ((this.button >= 0) ? this.button : 0);
					this.clickCount = Mathf.Max(1, this.clickCount);
					if (eventType == PanelEventHandler.PointerEventType.Down)
					{
						PointerDeviceState.PressButton(this.pointerId, this.button);
					}
					else if (eventType == PanelEventHandler.PointerEventType.Up)
					{
						PointerDeviceState.ReleaseButton(this.pointerId, this.button);
					}
				}
				this.pressedButtons = PointerDeviceState.GetPressedButtons(this.pointerId);
			}

			// Token: 0x0600073F RID: 1855 RVA: 0x0001C718 File Offset: 0x0001A918
			public void SetPosition(Vector3 positionOverride, Vector3 deltaOverride)
			{
				this.position = positionOverride;
				this.localPosition = positionOverride;
				this.deltaPosition = deltaOverride;
			}

			// Token: 0x06000741 RID: 1857 RVA: 0x0001C744 File Offset: 0x0001A944
			[CompilerGenerated]
			internal static bool <Read>g__InRange|82_0(int i, int start, int count)
			{
				return i >= start && i < start + count;
			}
		}
	}
}
