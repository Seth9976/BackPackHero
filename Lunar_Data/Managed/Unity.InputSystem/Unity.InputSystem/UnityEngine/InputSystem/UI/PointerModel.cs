using System;
using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x0200008A RID: 138
	internal struct PointerModel
	{
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0003B8E5 File Offset: 0x00039AE5
		public UIPointerType pointerType
		{
			get
			{
				return this.eventData.pointerType;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0003B8F2 File Offset: 0x00039AF2
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x0003B8FA File Offset: 0x00039AFA
		public Vector2 screenPosition
		{
			get
			{
				return this.m_ScreenPosition;
			}
			set
			{
				if (this.m_ScreenPosition != value)
				{
					this.m_ScreenPosition = value;
					this.changedThisFrame = true;
				}
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0003B918 File Offset: 0x00039B18
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x0003B920 File Offset: 0x00039B20
		public Vector3 worldPosition
		{
			get
			{
				return this.m_WorldPosition;
			}
			set
			{
				if (this.m_WorldPosition != value)
				{
					this.m_WorldPosition = value;
					this.changedThisFrame = true;
				}
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0003B93E File Offset: 0x00039B3E
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x0003B946 File Offset: 0x00039B46
		public Quaternion worldOrientation
		{
			get
			{
				return this.m_WorldOrientation;
			}
			set
			{
				if (this.m_WorldOrientation != value)
				{
					this.m_WorldOrientation = value;
					this.changedThisFrame = true;
				}
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0003B964 File Offset: 0x00039B64
		// (set) Token: 0x06000B24 RID: 2852 RVA: 0x0003B96C File Offset: 0x00039B6C
		public Vector2 scrollDelta
		{
			get
			{
				return this.m_ScrollDelta;
			}
			set
			{
				if (this.m_ScrollDelta != value)
				{
					this.changedThisFrame = true;
					this.m_ScrollDelta = value;
				}
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x0003B98A File Offset: 0x00039B8A
		// (set) Token: 0x06000B26 RID: 2854 RVA: 0x0003B992 File Offset: 0x00039B92
		public float pressure
		{
			get
			{
				return this.m_Pressure;
			}
			set
			{
				if (this.m_Pressure != value)
				{
					this.changedThisFrame = true;
					this.m_Pressure = value;
				}
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0003B9AB File Offset: 0x00039BAB
		// (set) Token: 0x06000B28 RID: 2856 RVA: 0x0003B9B3 File Offset: 0x00039BB3
		public float azimuthAngle
		{
			get
			{
				return this.m_AzimuthAngle;
			}
			set
			{
				if (this.m_AzimuthAngle != value)
				{
					this.changedThisFrame = true;
					this.m_AzimuthAngle = value;
				}
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0003B9CC File Offset: 0x00039BCC
		// (set) Token: 0x06000B2A RID: 2858 RVA: 0x0003B9D4 File Offset: 0x00039BD4
		public float altitudeAngle
		{
			get
			{
				return this.m_AltitudeAngle;
			}
			set
			{
				if (this.m_AltitudeAngle != value)
				{
					this.changedThisFrame = true;
					this.m_AltitudeAngle = value;
				}
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0003B9ED File Offset: 0x00039BED
		// (set) Token: 0x06000B2C RID: 2860 RVA: 0x0003B9F5 File Offset: 0x00039BF5
		public float twist
		{
			get
			{
				return this.m_Twist;
			}
			set
			{
				if (this.m_Twist != value)
				{
					this.changedThisFrame = true;
					this.m_Twist = value;
				}
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0003BA0E File Offset: 0x00039C0E
		// (set) Token: 0x06000B2E RID: 2862 RVA: 0x0003BA16 File Offset: 0x00039C16
		public Vector2 radius
		{
			get
			{
				return this.m_Radius;
			}
			set
			{
				if (this.m_Radius != value)
				{
					this.changedThisFrame = true;
					this.m_Radius = value;
				}
			}
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0003BA34 File Offset: 0x00039C34
		public PointerModel(ExtendedPointerEventData eventData)
		{
			this.eventData = eventData;
			this.changedThisFrame = false;
			this.leftButton = default(PointerModel.ButtonState);
			this.leftButton.OnEndFrame();
			this.rightButton = default(PointerModel.ButtonState);
			this.rightButton.OnEndFrame();
			this.middleButton = default(PointerModel.ButtonState);
			this.middleButton.OnEndFrame();
			this.m_ScreenPosition = default(Vector2);
			this.m_ScrollDelta = default(Vector2);
			this.m_WorldOrientation = default(Quaternion);
			this.m_WorldPosition = default(Vector3);
			this.m_Pressure = 0f;
			this.m_AzimuthAngle = 0f;
			this.m_AltitudeAngle = 0f;
			this.m_Twist = 0f;
			this.m_Radius = default(Vector2);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0003BAFC File Offset: 0x00039CFC
		public void OnFrameFinished()
		{
			this.changedThisFrame = false;
			this.scrollDelta = default(Vector2);
			this.leftButton.OnEndFrame();
			this.rightButton.OnEndFrame();
			this.middleButton.OnEndFrame();
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0003BB40 File Offset: 0x00039D40
		public void CopyTouchOrPenStateFrom(PointerEventData eventData)
		{
			this.pressure = eventData.pressure;
			this.azimuthAngle = eventData.azimuthAngle;
			this.altitudeAngle = eventData.altitudeAngle;
			this.twist = eventData.twist;
			this.radius = eventData.radius;
		}

		// Token: 0x040003E3 RID: 995
		public bool changedThisFrame;

		// Token: 0x040003E4 RID: 996
		public PointerModel.ButtonState leftButton;

		// Token: 0x040003E5 RID: 997
		public PointerModel.ButtonState rightButton;

		// Token: 0x040003E6 RID: 998
		public PointerModel.ButtonState middleButton;

		// Token: 0x040003E7 RID: 999
		public ExtendedPointerEventData eventData;

		// Token: 0x040003E8 RID: 1000
		private Vector2 m_ScreenPosition;

		// Token: 0x040003E9 RID: 1001
		private Vector2 m_ScrollDelta;

		// Token: 0x040003EA RID: 1002
		private Vector3 m_WorldPosition;

		// Token: 0x040003EB RID: 1003
		private Quaternion m_WorldOrientation;

		// Token: 0x040003EC RID: 1004
		private float m_Pressure;

		// Token: 0x040003ED RID: 1005
		private float m_AzimuthAngle;

		// Token: 0x040003EE RID: 1006
		private float m_AltitudeAngle;

		// Token: 0x040003EF RID: 1007
		private float m_Twist;

		// Token: 0x040003F0 RID: 1008
		private Vector2 m_Radius;

		// Token: 0x020001CB RID: 459
		public struct ButtonState
		{
			// Token: 0x17000561 RID: 1377
			// (get) Token: 0x06001404 RID: 5124 RVA: 0x0005C666 File Offset: 0x0005A866
			// (set) Token: 0x06001405 RID: 5125 RVA: 0x0005C670 File Offset: 0x0005A870
			public bool isPressed
			{
				get
				{
					return this.m_IsPressed;
				}
				set
				{
					if (this.m_IsPressed != value)
					{
						this.m_IsPressed = value;
						if (this.m_FramePressState == PointerEventData.FramePressState.NotChanged && value)
						{
							this.m_FramePressState = PointerEventData.FramePressState.Pressed;
							return;
						}
						if (this.m_FramePressState == PointerEventData.FramePressState.NotChanged && !value)
						{
							this.m_FramePressState = PointerEventData.FramePressState.Released;
							return;
						}
						if (this.m_FramePressState == PointerEventData.FramePressState.Pressed && !value)
						{
							this.m_FramePressState = PointerEventData.FramePressState.PressedAndReleased;
						}
					}
				}
			}

			// Token: 0x17000562 RID: 1378
			// (get) Token: 0x06001406 RID: 5126 RVA: 0x0005C6C8 File Offset: 0x0005A8C8
			// (set) Token: 0x06001407 RID: 5127 RVA: 0x0005C6D0 File Offset: 0x0005A8D0
			public bool ignoreNextClick
			{
				get
				{
					return this.m_IgnoreNextClick;
				}
				set
				{
					this.m_IgnoreNextClick = value;
				}
			}

			// Token: 0x17000563 RID: 1379
			// (get) Token: 0x06001408 RID: 5128 RVA: 0x0005C6D9 File Offset: 0x0005A8D9
			// (set) Token: 0x06001409 RID: 5129 RVA: 0x0005C6E1 File Offset: 0x0005A8E1
			public float pressTime
			{
				get
				{
					return this.m_PressTime;
				}
				set
				{
					this.m_PressTime = value;
				}
			}

			// Token: 0x17000564 RID: 1380
			// (get) Token: 0x0600140A RID: 5130 RVA: 0x0005C6EA File Offset: 0x0005A8EA
			// (set) Token: 0x0600140B RID: 5131 RVA: 0x0005C6F2 File Offset: 0x0005A8F2
			public bool clickedOnSameGameObject
			{
				get
				{
					return this.m_ClickedOnSameGameObject;
				}
				set
				{
					this.m_ClickedOnSameGameObject = value;
				}
			}

			// Token: 0x17000565 RID: 1381
			// (get) Token: 0x0600140C RID: 5132 RVA: 0x0005C6FB File Offset: 0x0005A8FB
			public bool wasPressedThisFrame
			{
				get
				{
					return this.m_FramePressState == PointerEventData.FramePressState.Pressed || this.m_FramePressState == PointerEventData.FramePressState.PressedAndReleased;
				}
			}

			// Token: 0x17000566 RID: 1382
			// (get) Token: 0x0600140D RID: 5133 RVA: 0x0005C710 File Offset: 0x0005A910
			public bool wasReleasedThisFrame
			{
				get
				{
					return this.m_FramePressState == PointerEventData.FramePressState.Released || this.m_FramePressState == PointerEventData.FramePressState.PressedAndReleased;
				}
			}

			// Token: 0x0600140E RID: 5134 RVA: 0x0005C728 File Offset: 0x0005A928
			public void CopyPressStateTo(PointerEventData eventData)
			{
				eventData.pointerPressRaycast = this.m_PressRaycast;
				eventData.pressPosition = this.m_PressPosition;
				eventData.clickCount = this.m_ClickCount;
				eventData.clickTime = this.m_ClickTime;
				eventData.pointerPress = this.m_LastPressObject;
				eventData.pointerPress = this.m_PressObject;
				eventData.rawPointerPress = this.m_RawPressObject;
				eventData.pointerDrag = this.m_DragObject;
				eventData.dragging = this.m_Dragging;
				if (this.ignoreNextClick)
				{
					eventData.eligibleForClick = false;
				}
			}

			// Token: 0x0600140F RID: 5135 RVA: 0x0005C7B0 File Offset: 0x0005A9B0
			public void CopyPressStateFrom(PointerEventData eventData)
			{
				this.m_PressRaycast = eventData.pointerPressRaycast;
				this.m_PressObject = eventData.pointerPress;
				this.m_RawPressObject = eventData.rawPointerPress;
				this.m_LastPressObject = eventData.lastPress;
				this.m_PressPosition = eventData.pressPosition;
				this.m_ClickTime = eventData.clickTime;
				this.m_ClickCount = eventData.clickCount;
				this.m_DragObject = eventData.pointerDrag;
				this.m_Dragging = eventData.dragging;
			}

			// Token: 0x06001410 RID: 5136 RVA: 0x0005C829 File Offset: 0x0005AA29
			public void OnEndFrame()
			{
				this.m_FramePressState = PointerEventData.FramePressState.NotChanged;
			}

			// Token: 0x04000947 RID: 2375
			private bool m_IsPressed;

			// Token: 0x04000948 RID: 2376
			private PointerEventData.FramePressState m_FramePressState;

			// Token: 0x04000949 RID: 2377
			private float m_PressTime;

			// Token: 0x0400094A RID: 2378
			private RaycastResult m_PressRaycast;

			// Token: 0x0400094B RID: 2379
			private GameObject m_PressObject;

			// Token: 0x0400094C RID: 2380
			private GameObject m_RawPressObject;

			// Token: 0x0400094D RID: 2381
			private GameObject m_LastPressObject;

			// Token: 0x0400094E RID: 2382
			private GameObject m_DragObject;

			// Token: 0x0400094F RID: 2383
			private Vector2 m_PressPosition;

			// Token: 0x04000950 RID: 2384
			private float m_ClickTime;

			// Token: 0x04000951 RID: 2385
			private int m_ClickCount;

			// Token: 0x04000952 RID: 2386
			private bool m_Dragging;

			// Token: 0x04000953 RID: 2387
			private bool m_ClickedOnSameGameObject;

			// Token: 0x04000954 RID: 2388
			private bool m_IgnoreNextClick;
		}
	}
}
