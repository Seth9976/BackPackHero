using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000036 RID: 54
	[AddComponentMenu("UI/Slider", 34)]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class Slider : Selectable, IDragHandler, IEventSystemHandler, IInitializePotentialDragHandler, ICanvasElement
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00013D21 File Offset: 0x00011F21
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x00013D29 File Offset: 0x00011F29
		public RectTransform fillRect
		{
			get
			{
				return this.m_FillRect;
			}
			set
			{
				if (SetPropertyUtility.SetClass<RectTransform>(ref this.m_FillRect, value))
				{
					this.UpdateCachedReferences();
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00013D45 File Offset: 0x00011F45
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x00013D4D File Offset: 0x00011F4D
		public RectTransform handleRect
		{
			get
			{
				return this.m_HandleRect;
			}
			set
			{
				if (SetPropertyUtility.SetClass<RectTransform>(ref this.m_HandleRect, value))
				{
					this.UpdateCachedReferences();
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00013D69 File Offset: 0x00011F69
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x00013D71 File Offset: 0x00011F71
		public Slider.Direction direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Slider.Direction>(ref this.m_Direction, value))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00013D87 File Offset: 0x00011F87
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x00013D8F File Offset: 0x00011F8F
		public float minValue
		{
			get
			{
				return this.m_MinValue;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MinValue, value))
				{
					this.Set(this.m_Value, true);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00013DB2 File Offset: 0x00011FB2
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x00013DBA File Offset: 0x00011FBA
		public float maxValue
		{
			get
			{
				return this.m_MaxValue;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MaxValue, value))
				{
					this.Set(this.m_Value, true);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00013DDD File Offset: 0x00011FDD
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x00013DE5 File Offset: 0x00011FE5
		public bool wholeNumbers
		{
			get
			{
				return this.m_WholeNumbers;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_WholeNumbers, value))
				{
					this.Set(this.m_Value, true);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00013E08 File Offset: 0x00012008
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x00013E24 File Offset: 0x00012024
		public virtual float value
		{
			get
			{
				if (!this.wholeNumbers)
				{
					return this.m_Value;
				}
				return Mathf.Round(this.m_Value);
			}
			set
			{
				this.Set(value, true);
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00013E2E File Offset: 0x0001202E
		public virtual void SetValueWithoutNotify(float input)
		{
			this.Set(input, false);
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00013E38 File Offset: 0x00012038
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x00013E6A File Offset: 0x0001206A
		public float normalizedValue
		{
			get
			{
				if (Mathf.Approximately(this.minValue, this.maxValue))
				{
					return 0f;
				}
				return Mathf.InverseLerp(this.minValue, this.maxValue, this.value);
			}
			set
			{
				this.value = Mathf.Lerp(this.minValue, this.maxValue, value);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00013E84 File Offset: 0x00012084
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x00013E8C File Offset: 0x0001208C
		public Slider.SliderEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				this.m_OnValueChanged = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00013E95 File Offset: 0x00012095
		private float stepSize
		{
			get
			{
				if (!this.wholeNumbers)
				{
					return (this.maxValue - this.minValue) * 0.1f;
				}
				return 1f;
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00013EB8 File Offset: 0x000120B8
		protected Slider()
		{
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00013EE1 File Offset: 0x000120E1
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00013EE3 File Offset: 0x000120E3
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00013EE5 File Offset: 0x000120E5
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00013EE7 File Offset: 0x000120E7
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateCachedReferences();
			this.Set(this.m_Value, false);
			this.UpdateVisuals();
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00013F08 File Offset: 0x00012108
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			base.OnDisable();
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00013F1B File Offset: 0x0001211B
		protected virtual void Update()
		{
			if (this.m_DelayedUpdateVisuals)
			{
				this.m_DelayedUpdateVisuals = false;
				this.Set(this.m_Value, false);
				this.UpdateVisuals();
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00013F40 File Offset: 0x00012140
		protected override void OnDidApplyAnimationProperties()
		{
			this.m_Value = this.ClampValue(this.m_Value);
			float num = this.normalizedValue;
			if (this.m_FillContainerRect != null)
			{
				if (this.m_FillImage != null && this.m_FillImage.type == Image.Type.Filled)
				{
					num = this.m_FillImage.fillAmount;
				}
				else
				{
					num = (this.reverseValue ? (1f - this.m_FillRect.anchorMin[(int)this.axis]) : this.m_FillRect.anchorMax[(int)this.axis]);
				}
			}
			else if (this.m_HandleContainerRect != null)
			{
				num = (this.reverseValue ? (1f - this.m_HandleRect.anchorMin[(int)this.axis]) : this.m_HandleRect.anchorMin[(int)this.axis]);
			}
			this.UpdateVisuals();
			if (num != this.normalizedValue)
			{
				UISystemProfilerApi.AddMarker("Slider.value", this);
				this.onValueChanged.Invoke(this.m_Value);
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00014064 File Offset: 0x00012264
		private void UpdateCachedReferences()
		{
			if (this.m_FillRect && this.m_FillRect != (RectTransform)base.transform)
			{
				this.m_FillTransform = this.m_FillRect.transform;
				this.m_FillImage = this.m_FillRect.GetComponent<Image>();
				if (this.m_FillTransform.parent != null)
				{
					this.m_FillContainerRect = this.m_FillTransform.parent.GetComponent<RectTransform>();
				}
			}
			else
			{
				this.m_FillRect = null;
				this.m_FillContainerRect = null;
				this.m_FillImage = null;
			}
			if (this.m_HandleRect && this.m_HandleRect != (RectTransform)base.transform)
			{
				this.m_HandleTransform = this.m_HandleRect.transform;
				if (this.m_HandleTransform.parent != null)
				{
					this.m_HandleContainerRect = this.m_HandleTransform.parent.GetComponent<RectTransform>();
					return;
				}
			}
			else
			{
				this.m_HandleRect = null;
				this.m_HandleContainerRect = null;
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00014168 File Offset: 0x00012368
		private float ClampValue(float input)
		{
			float num = Mathf.Clamp(input, this.minValue, this.maxValue);
			if (this.wholeNumbers)
			{
				num = Mathf.Round(num);
			}
			return num;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00014198 File Offset: 0x00012398
		protected virtual void Set(float input, bool sendCallback = true)
		{
			float num = this.ClampValue(input);
			if (this.m_Value == num)
			{
				return;
			}
			this.m_Value = num;
			this.UpdateVisuals();
			if (sendCallback)
			{
				UISystemProfilerApi.AddMarker("Slider.value", this);
				this.m_OnValueChanged.Invoke(num);
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000141DE File Offset: 0x000123DE
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateVisuals();
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x000141F5 File Offset: 0x000123F5
		private Slider.Axis axis
		{
			get
			{
				if (this.m_Direction != Slider.Direction.LeftToRight && this.m_Direction != Slider.Direction.RightToLeft)
				{
					return Slider.Axis.Vertical;
				}
				return Slider.Axis.Horizontal;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0001420B File Offset: 0x0001240B
		private bool reverseValue
		{
			get
			{
				return this.m_Direction == Slider.Direction.RightToLeft || this.m_Direction == Slider.Direction.TopToBottom;
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00014224 File Offset: 0x00012424
		private void UpdateVisuals()
		{
			this.m_Tracker.Clear();
			if (this.m_FillContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_FillRect, DrivenTransformProperties.Anchors);
				Vector2 zero = Vector2.zero;
				Vector2 one = Vector2.one;
				if (this.m_FillImage != null && this.m_FillImage.type == Image.Type.Filled)
				{
					this.m_FillImage.fillAmount = this.normalizedValue;
				}
				else if (this.reverseValue)
				{
					zero[(int)this.axis] = 1f - this.normalizedValue;
				}
				else
				{
					one[(int)this.axis] = this.normalizedValue;
				}
				this.m_FillRect.anchorMin = zero;
				this.m_FillRect.anchorMax = one;
			}
			if (this.m_HandleContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_HandleRect, DrivenTransformProperties.Anchors);
				Vector2 zero2 = Vector2.zero;
				Vector2 one2 = Vector2.one;
				zero2[(int)this.axis] = (one2[(int)this.axis] = (this.reverseValue ? (1f - this.normalizedValue) : this.normalizedValue));
				this.m_HandleRect.anchorMin = zero2;
				this.m_HandleRect.anchorMax = one2;
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00014374 File Offset: 0x00012574
		private void UpdateDrag(PointerEventData eventData, Camera cam)
		{
			RectTransform rectTransform = this.m_HandleContainerRect ?? this.m_FillContainerRect;
			if (rectTransform != null && rectTransform.rect.size[(int)this.axis] > 0f)
			{
				Vector2 zero = Vector2.zero;
				if (!MultipleDisplayUtilities.GetRelativeMousePositionForDrag(eventData, ref zero))
				{
					return;
				}
				Vector2 vector;
				if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, zero, cam, out vector))
				{
					return;
				}
				vector -= rectTransform.rect.position;
				float num = Mathf.Clamp01((vector - this.m_Offset)[(int)this.axis] / rectTransform.rect.size[(int)this.axis]);
				this.normalizedValue = (this.reverseValue ? (1f - num) : num);
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00014452 File Offset: 0x00012652
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00014470 File Offset: 0x00012670
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			base.OnPointerDown(eventData);
			this.m_Offset = Vector2.zero;
			if (this.m_HandleContainerRect != null && RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera))
			{
				Vector2 vector;
				if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.pressEventCamera, out vector))
				{
					this.m_Offset = vector;
					return;
				}
			}
			else
			{
				this.UpdateDrag(eventData, eventData.pressEventCamera);
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000144FA File Offset: 0x000126FA
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.UpdateDrag(eventData, eventData.pressEventCamera);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00014514 File Offset: 0x00012714
		public override void OnMove(AxisEventData eventData)
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				base.OnMove(eventData);
				return;
			}
			switch (eventData.moveDir)
			{
			case MoveDirection.Left:
				if (this.axis == Slider.Axis.Horizontal && this.FindSelectableOnLeft() == null)
				{
					this.Set(this.reverseValue ? (this.value + this.stepSize) : (this.value - this.stepSize), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Up:
				if (this.axis == Slider.Axis.Vertical && this.FindSelectableOnUp() == null)
				{
					this.Set(this.reverseValue ? (this.value - this.stepSize) : (this.value + this.stepSize), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Right:
				if (this.axis == Slider.Axis.Horizontal && this.FindSelectableOnRight() == null)
				{
					this.Set(this.reverseValue ? (this.value - this.stepSize) : (this.value + this.stepSize), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Down:
				if (this.axis == Slider.Axis.Vertical && this.FindSelectableOnDown() == null)
				{
					this.Set(this.reverseValue ? (this.value + this.stepSize) : (this.value - this.stepSize), true);
					return;
				}
				base.OnMove(eventData);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00014680 File Offset: 0x00012880
		public override Selectable FindSelectableOnLeft()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000146B0 File Offset: 0x000128B0
		public override Selectable FindSelectableOnRight()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000146E0 File Offset: 0x000128E0
		public override Selectable FindSelectableOnUp()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00014710 File Offset: 0x00012910
		public override Selectable FindSelectableOnDown()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001473F File Offset: 0x0001293F
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00014748 File Offset: 0x00012948
		public void SetDirection(Slider.Direction direction, bool includeRectLayouts)
		{
			Slider.Axis axis = this.axis;
			bool reverseValue = this.reverseValue;
			this.direction = direction;
			if (!includeRectLayouts)
			{
				return;
			}
			if (this.axis != axis)
			{
				RectTransformUtility.FlipLayoutAxes(base.transform as RectTransform, true, true);
			}
			if (this.reverseValue != reverseValue)
			{
				RectTransformUtility.FlipLayoutOnAxis(base.transform as RectTransform, (int)this.axis, true, true);
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000147AA File Offset: 0x000129AA
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x0400015A RID: 346
		[SerializeField]
		private RectTransform m_FillRect;

		// Token: 0x0400015B RID: 347
		[SerializeField]
		private RectTransform m_HandleRect;

		// Token: 0x0400015C RID: 348
		[Space]
		[SerializeField]
		private Slider.Direction m_Direction;

		// Token: 0x0400015D RID: 349
		[SerializeField]
		private float m_MinValue;

		// Token: 0x0400015E RID: 350
		[SerializeField]
		private float m_MaxValue = 1f;

		// Token: 0x0400015F RID: 351
		[SerializeField]
		private bool m_WholeNumbers;

		// Token: 0x04000160 RID: 352
		[SerializeField]
		protected float m_Value;

		// Token: 0x04000161 RID: 353
		[Space]
		[SerializeField]
		private Slider.SliderEvent m_OnValueChanged = new Slider.SliderEvent();

		// Token: 0x04000162 RID: 354
		private Image m_FillImage;

		// Token: 0x04000163 RID: 355
		private Transform m_FillTransform;

		// Token: 0x04000164 RID: 356
		private RectTransform m_FillContainerRect;

		// Token: 0x04000165 RID: 357
		private Transform m_HandleTransform;

		// Token: 0x04000166 RID: 358
		private RectTransform m_HandleContainerRect;

		// Token: 0x04000167 RID: 359
		private Vector2 m_Offset = Vector2.zero;

		// Token: 0x04000168 RID: 360
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x04000169 RID: 361
		private bool m_DelayedUpdateVisuals;

		// Token: 0x020000AC RID: 172
		public enum Direction
		{
			// Token: 0x04000301 RID: 769
			LeftToRight,
			// Token: 0x04000302 RID: 770
			RightToLeft,
			// Token: 0x04000303 RID: 771
			BottomToTop,
			// Token: 0x04000304 RID: 772
			TopToBottom
		}

		// Token: 0x020000AD RID: 173
		[Serializable]
		public class SliderEvent : UnityEvent<float>
		{
		}

		// Token: 0x020000AE RID: 174
		private enum Axis
		{
			// Token: 0x04000306 RID: 774
			Horizontal,
			// Token: 0x04000307 RID: 775
			Vertical
		}
	}
}
