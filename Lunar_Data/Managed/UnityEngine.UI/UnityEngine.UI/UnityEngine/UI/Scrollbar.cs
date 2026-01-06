using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000032 RID: 50
	[AddComponentMenu("UI/Scrollbar", 36)]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class Scrollbar : Selectable, IBeginDragHandler, IEventSystemHandler, IDragHandler, IInitializePotentialDragHandler, ICanvasElement
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00010ADB File Offset: 0x0000ECDB
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00010AE3 File Offset: 0x0000ECE3
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

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00010AFF File Offset: 0x0000ECFF
		// (set) Token: 0x06000332 RID: 818 RVA: 0x00010B07 File Offset: 0x0000ED07
		public Scrollbar.Direction direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Scrollbar.Direction>(ref this.m_Direction, value))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00010B1D File Offset: 0x0000ED1D
		protected Scrollbar()
		{
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00010B48 File Offset: 0x0000ED48
		// (set) Token: 0x06000335 RID: 821 RVA: 0x00010B81 File Offset: 0x0000ED81
		public float value
		{
			get
			{
				float num = this.m_Value;
				if (this.m_NumberOfSteps > 1)
				{
					num = Mathf.Round(num * (float)(this.m_NumberOfSteps - 1)) / (float)(this.m_NumberOfSteps - 1);
				}
				return num;
			}
			set
			{
				this.Set(value, true);
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00010B8B File Offset: 0x0000ED8B
		public virtual void SetValueWithoutNotify(float input)
		{
			this.Set(input, false);
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00010B95 File Offset: 0x0000ED95
		// (set) Token: 0x06000338 RID: 824 RVA: 0x00010B9D File Offset: 0x0000ED9D
		public float size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_Size, Mathf.Clamp01(value)))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00010BB8 File Offset: 0x0000EDB8
		// (set) Token: 0x0600033A RID: 826 RVA: 0x00010BC0 File Offset: 0x0000EDC0
		public int numberOfSteps
		{
			get
			{
				return this.m_NumberOfSteps;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_NumberOfSteps, value))
				{
					this.Set(this.m_Value, true);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00010BE3 File Offset: 0x0000EDE3
		// (set) Token: 0x0600033C RID: 828 RVA: 0x00010BEB File Offset: 0x0000EDEB
		public Scrollbar.ScrollEvent onValueChanged
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

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00010BF4 File Offset: 0x0000EDF4
		private float stepSize
		{
			get
			{
				if (this.m_NumberOfSteps <= 1)
				{
					return 0.1f;
				}
				return 1f / (float)(this.m_NumberOfSteps - 1);
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00010C14 File Offset: 0x0000EE14
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00010C16 File Offset: 0x0000EE16
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00010C18 File Offset: 0x0000EE18
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00010C1A File Offset: 0x0000EE1A
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateCachedReferences();
			this.Set(this.m_Value, false);
			this.UpdateVisuals();
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00010C3B File Offset: 0x0000EE3B
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			base.OnDisable();
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00010C4E File Offset: 0x0000EE4E
		protected virtual void Update()
		{
			if (this.m_DelayedUpdateVisuals)
			{
				this.m_DelayedUpdateVisuals = false;
				this.UpdateVisuals();
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00010C65 File Offset: 0x0000EE65
		private void UpdateCachedReferences()
		{
			if (this.m_HandleRect && this.m_HandleRect.parent != null)
			{
				this.m_ContainerRect = this.m_HandleRect.parent.GetComponent<RectTransform>();
				return;
			}
			this.m_ContainerRect = null;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00010CA5 File Offset: 0x0000EEA5
		private void Set(float input, bool sendCallback = true)
		{
			float value = this.m_Value;
			this.m_Value = input;
			if (value == this.value)
			{
				return;
			}
			this.UpdateVisuals();
			if (sendCallback)
			{
				UISystemProfilerApi.AddMarker("Scrollbar.value", this);
				this.m_OnValueChanged.Invoke(this.value);
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00010CE2 File Offset: 0x0000EEE2
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateVisuals();
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00010CF9 File Offset: 0x0000EEF9
		private Scrollbar.Axis axis
		{
			get
			{
				if (this.m_Direction != Scrollbar.Direction.LeftToRight && this.m_Direction != Scrollbar.Direction.RightToLeft)
				{
					return Scrollbar.Axis.Vertical;
				}
				return Scrollbar.Axis.Horizontal;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00010D0F File Offset: 0x0000EF0F
		private bool reverseValue
		{
			get
			{
				return this.m_Direction == Scrollbar.Direction.RightToLeft || this.m_Direction == Scrollbar.Direction.TopToBottom;
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00010D28 File Offset: 0x0000EF28
		private void UpdateVisuals()
		{
			this.m_Tracker.Clear();
			if (this.m_ContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_HandleRect, DrivenTransformProperties.Anchors);
				Vector2 zero = Vector2.zero;
				Vector2 one = Vector2.one;
				float num = Mathf.Clamp01(this.value) * (1f - this.size);
				if (this.reverseValue)
				{
					zero[(int)this.axis] = 1f - num - this.size;
					one[(int)this.axis] = 1f - num;
				}
				else
				{
					zero[(int)this.axis] = num;
					one[(int)this.axis] = num + this.size;
				}
				this.m_HandleRect.anchorMin = zero;
				this.m_HandleRect.anchorMax = one;
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00010E04 File Offset: 0x0000F004
		private void UpdateDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (this.m_ContainerRect == null)
			{
				return;
			}
			Vector2 zero = Vector2.zero;
			if (!MultipleDisplayUtilities.GetRelativeMousePositionForDrag(eventData, ref zero))
			{
				return;
			}
			Vector2 vector;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_ContainerRect, zero, eventData.pressEventCamera, out vector))
			{
				return;
			}
			Vector2 vector2 = vector - this.m_Offset - this.m_ContainerRect.rect.position - (this.m_HandleRect.rect.size - this.m_HandleRect.sizeDelta) * 0.5f;
			float num = ((this.axis == Scrollbar.Axis.Horizontal) ? this.m_ContainerRect.rect.width : this.m_ContainerRect.rect.height) * (1f - this.size);
			if (num <= 0f)
			{
				return;
			}
			this.DoUpdateDrag(vector2, num);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00010EFC File Offset: 0x0000F0FC
		private void DoUpdateDrag(Vector2 handleCorner, float remainingSize)
		{
			switch (this.m_Direction)
			{
			case Scrollbar.Direction.LeftToRight:
				this.Set(Mathf.Clamp01(handleCorner.x / remainingSize), true);
				return;
			case Scrollbar.Direction.RightToLeft:
				this.Set(Mathf.Clamp01(1f - handleCorner.x / remainingSize), true);
				return;
			case Scrollbar.Direction.BottomToTop:
				this.Set(Mathf.Clamp01(handleCorner.y / remainingSize), true);
				return;
			case Scrollbar.Direction.TopToBottom:
				this.Set(Mathf.Clamp01(1f - handleCorner.y / remainingSize), true);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00010F86 File Offset: 0x0000F186
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00010FA4 File Offset: 0x0000F1A4
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			this.isPointerDownAndNotDragging = false;
			if (!this.MayDrag(eventData))
			{
				return;
			}
			if (this.m_ContainerRect == null)
			{
				return;
			}
			this.m_Offset = Vector2.zero;
			Vector2 vector;
			if (RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera) && RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.pressEventCamera, out vector))
			{
				this.m_Offset = vector - this.m_HandleRect.rect.center;
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00011039 File Offset: 0x0000F239
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			if (this.m_ContainerRect != null)
			{
				this.UpdateDrag(eventData);
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0001105A File Offset: 0x0000F25A
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			base.OnPointerDown(eventData);
			this.isPointerDownAndNotDragging = true;
			this.m_PointerDownRepeat = base.StartCoroutine(this.ClickRepeat(eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera));
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00011097 File Offset: 0x0000F297
		protected IEnumerator ClickRepeat(PointerEventData eventData)
		{
			return this.ClickRepeat(eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000110B0 File Offset: 0x0000F2B0
		protected IEnumerator ClickRepeat(Vector2 screenPosition, Camera camera)
		{
			while (this.isPointerDownAndNotDragging)
			{
				Vector2 vector;
				if (!RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, screenPosition, camera) && RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, screenPosition, camera, out vector))
				{
					float num = ((((this.axis == Scrollbar.Axis.Horizontal) ? vector.x : vector.y) < 0f) ? this.size : (-this.size));
					this.value += (this.reverseValue ? num : (-num));
					this.value = Mathf.Clamp01(this.value);
					this.value = Mathf.Round(this.value * 10000f) / 10000f;
				}
				yield return new WaitForEndOfFrame();
			}
			base.StopCoroutine(this.m_PointerDownRepeat);
			yield break;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000110CD File Offset: 0x0000F2CD
		public override void OnPointerUp(PointerEventData eventData)
		{
			base.OnPointerUp(eventData);
			this.isPointerDownAndNotDragging = false;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x000110E0 File Offset: 0x0000F2E0
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
				if (this.axis == Scrollbar.Axis.Horizontal && this.FindSelectableOnLeft() == null)
				{
					this.Set(Mathf.Clamp01(this.reverseValue ? (this.value + this.stepSize) : (this.value - this.stepSize)), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Up:
				if (this.axis == Scrollbar.Axis.Vertical && this.FindSelectableOnUp() == null)
				{
					this.Set(Mathf.Clamp01(this.reverseValue ? (this.value - this.stepSize) : (this.value + this.stepSize)), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Right:
				if (this.axis == Scrollbar.Axis.Horizontal && this.FindSelectableOnRight() == null)
				{
					this.Set(Mathf.Clamp01(this.reverseValue ? (this.value - this.stepSize) : (this.value + this.stepSize)), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Down:
				if (this.axis == Scrollbar.Axis.Vertical && this.FindSelectableOnDown() == null)
				{
					this.Set(Mathf.Clamp01(this.reverseValue ? (this.value + this.stepSize) : (this.value - this.stepSize)), true);
					return;
				}
				base.OnMove(eventData);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00011260 File Offset: 0x0000F460
		public override Selectable FindSelectableOnLeft()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00011290 File Offset: 0x0000F490
		public override Selectable FindSelectableOnRight()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000112C0 File Offset: 0x0000F4C0
		public override Selectable FindSelectableOnUp()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000112F0 File Offset: 0x0000F4F0
		public override Selectable FindSelectableOnDown()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0001131F File Offset: 0x0000F51F
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00011328 File Offset: 0x0000F528
		public void SetDirection(Scrollbar.Direction direction, bool includeRectLayouts)
		{
			Scrollbar.Axis axis = this.axis;
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

		// Token: 0x0600035A RID: 858 RVA: 0x0001138A File Offset: 0x0000F58A
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x04000119 RID: 281
		[SerializeField]
		private RectTransform m_HandleRect;

		// Token: 0x0400011A RID: 282
		[SerializeField]
		private Scrollbar.Direction m_Direction;

		// Token: 0x0400011B RID: 283
		[Range(0f, 1f)]
		[SerializeField]
		private float m_Value;

		// Token: 0x0400011C RID: 284
		[Range(0f, 1f)]
		[SerializeField]
		private float m_Size = 0.2f;

		// Token: 0x0400011D RID: 285
		[Range(0f, 11f)]
		[SerializeField]
		private int m_NumberOfSteps;

		// Token: 0x0400011E RID: 286
		[Space(6f)]
		[SerializeField]
		private Scrollbar.ScrollEvent m_OnValueChanged = new Scrollbar.ScrollEvent();

		// Token: 0x0400011F RID: 287
		private RectTransform m_ContainerRect;

		// Token: 0x04000120 RID: 288
		private Vector2 m_Offset = Vector2.zero;

		// Token: 0x04000121 RID: 289
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x04000122 RID: 290
		private Coroutine m_PointerDownRepeat;

		// Token: 0x04000123 RID: 291
		private bool isPointerDownAndNotDragging;

		// Token: 0x04000124 RID: 292
		private bool m_DelayedUpdateVisuals;

		// Token: 0x020000A3 RID: 163
		public enum Direction
		{
			// Token: 0x040002E1 RID: 737
			LeftToRight,
			// Token: 0x040002E2 RID: 738
			RightToLeft,
			// Token: 0x040002E3 RID: 739
			BottomToTop,
			// Token: 0x040002E4 RID: 740
			TopToBottom
		}

		// Token: 0x020000A4 RID: 164
		[Serializable]
		public class ScrollEvent : UnityEvent<float>
		{
		}

		// Token: 0x020000A5 RID: 165
		private enum Axis
		{
			// Token: 0x040002E6 RID: 742
			Horizontal,
			// Token: 0x040002E7 RID: 743
			Vertical
		}
	}
}
