using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000017 RID: 23
	internal class DefaultEventSystem
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000041B1 File Offset: 0x000023B1
		private bool isAppFocused
		{
			get
			{
				return Application.isFocused;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000041B8 File Offset: 0x000023B8
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000041DE File Offset: 0x000023DE
		internal DefaultEventSystem.IInput input
		{
			get
			{
				DefaultEventSystem.IInput input;
				if ((input = this.m_Input) == null)
				{
					input = (this.m_Input = this.GetDefaultInput());
				}
				return input;
			}
			set
			{
				this.m_Input = value;
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000041E8 File Offset: 0x000023E8
		private DefaultEventSystem.IInput GetDefaultInput()
		{
			DefaultEventSystem.IInput input = new DefaultEventSystem.Input();
			try
			{
				input.GetAxisRaw(this.m_HorizontalAxis);
			}
			catch (InvalidOperationException)
			{
				input = new DefaultEventSystem.NoInput();
				Debug.LogWarning("UI Toolkit is currently relying on legacy Input Manager for its active input source, but the legacy Input Manager is not available using your current Project Settings. Some UI Toolkit functionality might be missing or not working properly as a result. To fix this problem, you can enable \"Input Manager (old)\" or \"Both\" in the Active Input Source setting of the Player section. UI Toolkit is using its internal default event system to process input. Alternatively, you may activate new Input System support with UI Toolkit by adding an EventSystem component to your active scene.");
			}
			return input;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004238 File Offset: 0x00002438
		private bool ShouldIgnoreEventsOnAppNotFocused()
		{
			OperatingSystemFamily operatingSystemFamily = SystemInfo.operatingSystemFamily;
			OperatingSystemFamily operatingSystemFamily2 = operatingSystemFamily;
			return operatingSystemFamily2 - OperatingSystemFamily.MacOSX <= 2;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000425F File Offset: 0x0000245F
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00004268 File Offset: 0x00002468
		public BaseRuntimePanel focusedPanel
		{
			get
			{
				return this.m_FocusedPanel;
			}
			set
			{
				bool flag = this.m_FocusedPanel != value;
				if (flag)
				{
					BaseRuntimePanel focusedPanel = this.m_FocusedPanel;
					if (focusedPanel != null)
					{
						focusedPanel.Blur();
					}
					this.m_FocusedPanel = value;
					BaseRuntimePanel focusedPanel2 = this.m_FocusedPanel;
					if (focusedPanel2 != null)
					{
						focusedPanel2.Focus();
					}
				}
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000042B4 File Offset: 0x000024B4
		public void Update(DefaultEventSystem.UpdateMode updateMode = DefaultEventSystem.UpdateMode.Always)
		{
			bool flag = !this.isAppFocused && this.ShouldIgnoreEventsOnAppNotFocused() && updateMode == DefaultEventSystem.UpdateMode.IgnoreIfAppNotFocused;
			if (!flag)
			{
				this.m_SendingTouchEvents = this.ProcessTouchEvents();
				this.SendIMGUIEvents();
				this.SendInputEvents();
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000042FC File Offset: 0x000024FC
		private void SendIMGUIEvents()
		{
			while (Event.PopEvent(this.m_Event))
			{
				bool flag = this.m_Event.type == EventType.Repaint;
				if (!flag)
				{
					bool flag2 = this.m_Event.type == EventType.KeyUp || this.m_Event.type == EventType.KeyDown;
					if (flag2)
					{
						this.SendFocusBasedEvent<DefaultEventSystem>((DefaultEventSystem self) => UIElementsRuntimeUtility.CreateEvent(self.m_Event), this);
					}
					else
					{
						bool flag3 = !this.m_SendingTouchEvents && this.input.mousePresent;
						if (flag3)
						{
							int? num;
							Vector2 localScreenPosition = DefaultEventSystem.GetLocalScreenPosition(this.m_Event, out num);
							bool flag4 = this.m_Event.type == EventType.ScrollWheel;
							if (flag4)
							{
								this.SendPositionBasedEvent<DefaultEventSystem>(localScreenPosition, this.m_Event.delta, PointerId.mousePointerId, num, delegate(Vector3 panelPosition, Vector3 panelDelta, DefaultEventSystem self)
								{
									self.m_Event.mousePosition = panelPosition;
									return UIElementsRuntimeUtility.CreateEvent(self.m_Event);
								}, this, false);
							}
							else
							{
								this.SendPositionBasedEvent<DefaultEventSystem>(localScreenPosition, this.m_Event.delta, PointerId.mousePointerId, num, delegate(Vector3 panelPosition, Vector3 panelDelta, DefaultEventSystem self)
								{
									self.m_Event.mousePosition = panelPosition;
									self.m_Event.delta = panelDelta;
									return UIElementsRuntimeUtility.CreateEvent(self.m_Event);
								}, this, this.m_Event.type == EventType.MouseDown);
							}
						}
					}
				}
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000446C File Offset: 0x0000266C
		private void SendInputEvents()
		{
			bool flag = this.ShouldSendMoveFromInput();
			bool flag2 = flag;
			if (flag2)
			{
				this.SendFocusBasedEvent<DefaultEventSystem>((DefaultEventSystem self) => NavigationMoveEvent.GetPooled(self.GetRawMoveVector()), this);
			}
			bool buttonDown = this.input.GetButtonDown(this.m_SubmitButton);
			if (buttonDown)
			{
				this.SendFocusBasedEvent<DefaultEventSystem>((DefaultEventSystem self) => EventBase<NavigationSubmitEvent>.GetPooled(), this);
			}
			bool buttonDown2 = this.input.GetButtonDown(this.m_CancelButton);
			if (buttonDown2)
			{
				this.SendFocusBasedEvent<DefaultEventSystem>((DefaultEventSystem self) => EventBase<NavigationCancelEvent>.GetPooled(), this);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000452C File Offset: 0x0000272C
		internal void SendFocusBasedEvent<TArg>(Func<TArg, EventBase> evtFactory, TArg arg)
		{
			bool flag = this.focusedPanel != null;
			if (flag)
			{
				using (EventBase eventBase = evtFactory.Invoke(arg))
				{
					this.focusedPanel.visualTree.SendEvent(eventBase);
					this.UpdateFocusedPanel(this.focusedPanel);
					return;
				}
			}
			List<Panel> sortedPlayerPanels = UIElementsRuntimeUtility.GetSortedPlayerPanels();
			for (int i = sortedPlayerPanels.Count - 1; i >= 0; i--)
			{
				Panel panel = sortedPlayerPanels[i];
				BaseRuntimePanel baseRuntimePanel = panel as BaseRuntimePanel;
				bool flag2 = baseRuntimePanel != null;
				if (flag2)
				{
					using (EventBase eventBase2 = evtFactory.Invoke(arg))
					{
						baseRuntimePanel.visualTree.SendEvent(eventBase2);
						bool processedByFocusController = eventBase2.processedByFocusController;
						if (processedByFocusController)
						{
							this.UpdateFocusedPanel(baseRuntimePanel);
						}
						bool isPropagationStopped = eventBase2.isPropagationStopped;
						if (isPropagationStopped)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004634 File Offset: 0x00002834
		internal void SendPositionBasedEvent<TArg>(Vector3 mousePosition, Vector3 delta, int pointerId, Func<Vector3, Vector3, TArg, EventBase> evtFactory, TArg arg, bool deselectIfNoTarget = false)
		{
			this.SendPositionBasedEvent<TArg>(mousePosition, delta, pointerId, default(int?), evtFactory, arg, deselectIfNoTarget);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000465C File Offset: 0x0000285C
		private void SendPositionBasedEvent<TArg>(Vector3 mousePosition, Vector3 delta, int pointerId, int? targetDisplay, Func<Vector3, Vector3, TArg, EventBase> evtFactory, TArg arg, bool deselectIfNoTarget = false)
		{
			bool flag = this.focusedPanel != null;
			if (flag)
			{
				this.UpdateFocusedPanel(this.focusedPanel);
			}
			IPanel panel = PointerDeviceState.GetPlayerPanelWithSoftPointerCapture(pointerId);
			IEventHandler capturingElement = RuntimePanel.s_EventDispatcher.pointerState.GetCapturingElement(pointerId);
			VisualElement visualElement = capturingElement as VisualElement;
			bool flag2 = visualElement != null;
			if (flag2)
			{
				panel = visualElement.panel;
			}
			BaseRuntimePanel baseRuntimePanel = null;
			Vector2 zero = Vector2.zero;
			Vector2 zero2 = Vector2.zero;
			BaseRuntimePanel baseRuntimePanel2 = panel as BaseRuntimePanel;
			bool flag3 = baseRuntimePanel2 != null;
			if (flag3)
			{
				baseRuntimePanel = baseRuntimePanel2;
				baseRuntimePanel.ScreenToPanel(mousePosition, delta, out zero, out zero2, false);
			}
			else
			{
				List<Panel> sortedPlayerPanels = UIElementsRuntimeUtility.GetSortedPlayerPanels();
				for (int i = sortedPlayerPanels.Count - 1; i >= 0; i--)
				{
					BaseRuntimePanel baseRuntimePanel3 = sortedPlayerPanels[i] as BaseRuntimePanel;
					bool flag4;
					if (baseRuntimePanel3 != null)
					{
						if (targetDisplay != null)
						{
							int targetDisplay2 = baseRuntimePanel3.targetDisplay;
							int? num = targetDisplay;
							flag4 = (targetDisplay2 == num.GetValueOrDefault()) & (num != null);
						}
						else
						{
							flag4 = true;
						}
					}
					else
					{
						flag4 = false;
					}
					bool flag5 = flag4;
					if (flag5)
					{
						bool flag6 = baseRuntimePanel3.ScreenToPanel(mousePosition, delta, out zero, out zero2, false) && baseRuntimePanel3.Pick(zero) != null;
						if (flag6)
						{
							baseRuntimePanel = baseRuntimePanel3;
							break;
						}
					}
				}
			}
			BaseRuntimePanel baseRuntimePanel4 = PointerDeviceState.GetPanel(pointerId, ContextType.Player) as BaseRuntimePanel;
			bool flag7 = baseRuntimePanel4 != baseRuntimePanel;
			if (flag7)
			{
				if (baseRuntimePanel4 != null)
				{
					baseRuntimePanel4.PointerLeavesPanel(pointerId, baseRuntimePanel4.ScreenToPanel(mousePosition));
				}
				if (baseRuntimePanel != null)
				{
					baseRuntimePanel.PointerEntersPanel(pointerId, zero);
				}
			}
			bool flag8 = baseRuntimePanel != null;
			if (flag8)
			{
				using (EventBase eventBase = evtFactory.Invoke(zero, zero2, arg))
				{
					baseRuntimePanel.visualTree.SendEvent(eventBase);
					bool processedByFocusController = eventBase.processedByFocusController;
					if (processedByFocusController)
					{
						this.UpdateFocusedPanel(baseRuntimePanel);
					}
					bool flag9 = eventBase.eventTypeId == EventBase<PointerDownEvent>.TypeId();
					if (flag9)
					{
						PointerDeviceState.SetPlayerPanelWithSoftPointerCapture(pointerId, baseRuntimePanel);
					}
					else
					{
						bool flag10 = eventBase.eventTypeId == EventBase<PointerUpEvent>.TypeId() && ((PointerUpEvent)eventBase).pressedButtons == 0;
						if (flag10)
						{
							PointerDeviceState.SetPlayerPanelWithSoftPointerCapture(pointerId, null);
						}
					}
				}
			}
			else if (deselectIfNoTarget)
			{
				this.focusedPanel = null;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000048C8 File Offset: 0x00002AC8
		private void UpdateFocusedPanel(BaseRuntimePanel runtimePanel)
		{
			bool flag = runtimePanel.focusController.focusedElement != null;
			if (flag)
			{
				this.focusedPanel = runtimePanel;
			}
			else
			{
				bool flag2 = this.focusedPanel == runtimePanel;
				if (flag2)
				{
					this.focusedPanel = null;
				}
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000490C File Offset: 0x00002B0C
		private static EventBase MakeTouchEvent(Touch touch, EventModifiers modifiers)
		{
			EventBase eventBase;
			switch (touch.phase)
			{
			case TouchPhase.Began:
				eventBase = PointerEventBase<PointerDownEvent>.GetPooled(touch, modifiers);
				break;
			case TouchPhase.Moved:
				eventBase = PointerEventBase<PointerMoveEvent>.GetPooled(touch, modifiers);
				break;
			case TouchPhase.Stationary:
				eventBase = PointerEventBase<PointerStationaryEvent>.GetPooled(touch, modifiers);
				break;
			case TouchPhase.Ended:
				eventBase = PointerEventBase<PointerUpEvent>.GetPooled(touch, modifiers);
				break;
			case TouchPhase.Canceled:
				eventBase = PointerEventBase<PointerCancelEvent>.GetPooled(touch, modifiers);
				break;
			default:
				eventBase = null;
				break;
			}
			return eventBase;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004978 File Offset: 0x00002B78
		private bool ProcessTouchEvents()
		{
			for (int i = 0; i < this.input.touchCount; i++)
			{
				Touch touch = this.input.GetTouch(i);
				bool flag = touch.type == TouchType.Indirect;
				if (!flag)
				{
					int? num;
					touch.position = UIElementsRuntimeUtility.MultiDisplayBottomLeftToPanelPosition(touch.position, out num);
					int? num2;
					touch.rawPosition = UIElementsRuntimeUtility.MultiDisplayBottomLeftToPanelPosition(touch.rawPosition, out num2);
					touch.deltaPosition = UIElementsRuntimeUtility.ScreenBottomLeftToPanelDelta(touch.deltaPosition);
					this.SendPositionBasedEvent<Touch>(touch.position, touch.deltaPosition, PointerId.touchPointerIdBase + touch.fingerId, num, delegate(Vector3 panelPosition, Vector3 panelDelta, Touch _touch)
					{
						_touch.position = panelPosition;
						_touch.deltaPosition = panelDelta;
						return DefaultEventSystem.MakeTouchEvent(_touch, EventModifiers.None);
					}, touch, false);
				}
			}
			return this.input.touchCount > 0;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004A6C File Offset: 0x00002C6C
		private Vector2 GetRawMoveVector()
		{
			Vector2 zero = Vector2.zero;
			zero.x = this.input.GetAxisRaw(this.m_HorizontalAxis);
			zero.y = this.input.GetAxisRaw(this.m_VerticalAxis);
			bool buttonDown = this.input.GetButtonDown(this.m_HorizontalAxis);
			if (buttonDown)
			{
				bool flag = zero.x < 0f;
				if (flag)
				{
					zero.x = -1f;
				}
				bool flag2 = zero.x > 0f;
				if (flag2)
				{
					zero.x = 1f;
				}
			}
			bool buttonDown2 = this.input.GetButtonDown(this.m_VerticalAxis);
			if (buttonDown2)
			{
				bool flag3 = zero.y < 0f;
				if (flag3)
				{
					zero.y = -1f;
				}
				bool flag4 = zero.y > 0f;
				if (flag4)
				{
					zero.y = 1f;
				}
			}
			return zero;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004B60 File Offset: 0x00002D60
		private bool ShouldSendMoveFromInput()
		{
			float unscaledTime = Time.unscaledTime;
			Vector2 rawMoveVector = this.GetRawMoveVector();
			bool flag = Mathf.Approximately(rawMoveVector.x, 0f) && Mathf.Approximately(rawMoveVector.y, 0f);
			bool flag2;
			if (flag)
			{
				this.m_ConsecutiveMoveCount = 0;
				flag2 = false;
			}
			else
			{
				bool flag3 = this.input.GetButtonDown(this.m_HorizontalAxis) || this.input.GetButtonDown(this.m_VerticalAxis);
				bool flag4 = Vector2.Dot(rawMoveVector, this.m_LastMoveVector) > 0f;
				bool flag5 = !flag3;
				if (flag5)
				{
					bool flag6 = flag4 && this.m_ConsecutiveMoveCount == 1;
					if (flag6)
					{
						flag3 = unscaledTime > this.m_PrevActionTime + this.m_RepeatDelay;
					}
					else
					{
						flag3 = unscaledTime > this.m_PrevActionTime + 1f / this.m_InputActionsPerSecond;
					}
				}
				bool flag7 = !flag3;
				if (flag7)
				{
					flag2 = false;
				}
				else
				{
					NavigationMoveEvent.Direction direction = NavigationMoveEvent.DetermineMoveDirection(rawMoveVector.x, rawMoveVector.y, 0.6f);
					bool flag8 = direction > NavigationMoveEvent.Direction.None;
					if (flag8)
					{
						bool flag9 = !flag4;
						if (flag9)
						{
							this.m_ConsecutiveMoveCount = 0;
						}
						this.m_ConsecutiveMoveCount++;
						this.m_PrevActionTime = unscaledTime;
						this.m_LastMoveVector = rawMoveVector;
					}
					else
					{
						this.m_ConsecutiveMoveCount = 0;
					}
					flag2 = direction > NavigationMoveEvent.Direction.None;
				}
			}
			return flag2;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004CB4 File Offset: 0x00002EB4
		private static Vector2 GetLocalScreenPosition(Event evt, out int? targetDisplay)
		{
			targetDisplay = default(int?);
			return evt.mousePosition;
		}

		// Token: 0x04000039 RID: 57
		internal static Func<bool> IsEditorRemoteConnected = () => false;

		// Token: 0x0400003A RID: 58
		private DefaultEventSystem.IInput m_Input;

		// Token: 0x0400003B RID: 59
		private readonly string m_HorizontalAxis = "Horizontal";

		// Token: 0x0400003C RID: 60
		private readonly string m_VerticalAxis = "Vertical";

		// Token: 0x0400003D RID: 61
		private readonly string m_SubmitButton = "Submit";

		// Token: 0x0400003E RID: 62
		private readonly string m_CancelButton = "Cancel";

		// Token: 0x0400003F RID: 63
		private readonly float m_InputActionsPerSecond = 10f;

		// Token: 0x04000040 RID: 64
		private readonly float m_RepeatDelay = 0.5f;

		// Token: 0x04000041 RID: 65
		private bool m_SendingTouchEvents;

		// Token: 0x04000042 RID: 66
		private Event m_Event = new Event();

		// Token: 0x04000043 RID: 67
		private BaseRuntimePanel m_FocusedPanel;

		// Token: 0x04000044 RID: 68
		private int m_ConsecutiveMoveCount;

		// Token: 0x04000045 RID: 69
		private Vector2 m_LastMoveVector;

		// Token: 0x04000046 RID: 70
		private float m_PrevActionTime;

		// Token: 0x02000018 RID: 24
		public enum UpdateMode
		{
			// Token: 0x04000048 RID: 72
			Always,
			// Token: 0x04000049 RID: 73
			IgnoreIfAppNotFocused
		}

		// Token: 0x02000019 RID: 25
		internal interface IInput
		{
			// Token: 0x060000AF RID: 175
			bool GetButtonDown(string button);

			// Token: 0x060000B0 RID: 176
			float GetAxisRaw(string axis);

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x060000B1 RID: 177
			int touchCount { get; }

			// Token: 0x060000B2 RID: 178
			Touch GetTouch(int index);

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x060000B3 RID: 179
			bool mousePresent { get; }
		}

		// Token: 0x0200001A RID: 26
		private class Input : DefaultEventSystem.IInput
		{
			// Token: 0x060000B4 RID: 180 RVA: 0x00004D4C File Offset: 0x00002F4C
			public bool GetButtonDown(string button)
			{
				return UnityEngine.Input.GetButtonDown(button);
			}

			// Token: 0x060000B5 RID: 181 RVA: 0x00004D54 File Offset: 0x00002F54
			public float GetAxisRaw(string axis)
			{
				return UnityEngine.Input.GetAxis(axis);
			}

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004D5C File Offset: 0x00002F5C
			public int touchCount
			{
				get
				{
					return UnityEngine.Input.touchCount;
				}
			}

			// Token: 0x060000B7 RID: 183 RVA: 0x00004D63 File Offset: 0x00002F63
			public Touch GetTouch(int index)
			{
				return UnityEngine.Input.GetTouch(index);
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004D6B File Offset: 0x00002F6B
			public bool mousePresent
			{
				get
				{
					return UnityEngine.Input.mousePresent;
				}
			}
		}

		// Token: 0x0200001B RID: 27
		private class NoInput : DefaultEventSystem.IInput
		{
			// Token: 0x060000BA RID: 186 RVA: 0x00004D72 File Offset: 0x00002F72
			public bool GetButtonDown(string button)
			{
				return false;
			}

			// Token: 0x060000BB RID: 187 RVA: 0x00004D75 File Offset: 0x00002F75
			public float GetAxisRaw(string axis)
			{
				return 0f;
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x060000BC RID: 188 RVA: 0x00004D72 File Offset: 0x00002F72
			public int touchCount
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x060000BD RID: 189 RVA: 0x00004D7C File Offset: 0x00002F7C
			public Touch GetTouch(int index)
			{
				return default(Touch);
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x060000BE RID: 190 RVA: 0x00004D72 File Offset: 0x00002F72
			public bool mousePresent
			{
				get
				{
					return false;
				}
			}
		}
	}
}
