using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000033 RID: 51
	[AddComponentMenu("UI/Scroll Rect", 37)]
	[SelectionBase]
	[ExecuteAlways]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(RectTransform))]
	public class ScrollRect : UIBehaviour, IInitializePotentialDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IScrollHandler, ICanvasElement, ILayoutElement, ILayoutGroup, ILayoutController
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00011392 File Offset: 0x0000F592
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0001139A File Offset: 0x0000F59A
		public RectTransform content
		{
			get
			{
				return this.m_Content;
			}
			set
			{
				this.m_Content = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600035D RID: 861 RVA: 0x000113A3 File Offset: 0x0000F5A3
		// (set) Token: 0x0600035E RID: 862 RVA: 0x000113AB File Offset: 0x0000F5AB
		public bool horizontal
		{
			get
			{
				return this.m_Horizontal;
			}
			set
			{
				this.m_Horizontal = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600035F RID: 863 RVA: 0x000113B4 File Offset: 0x0000F5B4
		// (set) Token: 0x06000360 RID: 864 RVA: 0x000113BC File Offset: 0x0000F5BC
		public bool vertical
		{
			get
			{
				return this.m_Vertical;
			}
			set
			{
				this.m_Vertical = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000361 RID: 865 RVA: 0x000113C5 File Offset: 0x0000F5C5
		// (set) Token: 0x06000362 RID: 866 RVA: 0x000113CD File Offset: 0x0000F5CD
		public ScrollRect.MovementType movementType
		{
			get
			{
				return this.m_MovementType;
			}
			set
			{
				this.m_MovementType = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000363 RID: 867 RVA: 0x000113D6 File Offset: 0x0000F5D6
		// (set) Token: 0x06000364 RID: 868 RVA: 0x000113DE File Offset: 0x0000F5DE
		public float elasticity
		{
			get
			{
				return this.m_Elasticity;
			}
			set
			{
				this.m_Elasticity = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000365 RID: 869 RVA: 0x000113E7 File Offset: 0x0000F5E7
		// (set) Token: 0x06000366 RID: 870 RVA: 0x000113EF File Offset: 0x0000F5EF
		public bool inertia
		{
			get
			{
				return this.m_Inertia;
			}
			set
			{
				this.m_Inertia = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000367 RID: 871 RVA: 0x000113F8 File Offset: 0x0000F5F8
		// (set) Token: 0x06000368 RID: 872 RVA: 0x00011400 File Offset: 0x0000F600
		public float decelerationRate
		{
			get
			{
				return this.m_DecelerationRate;
			}
			set
			{
				this.m_DecelerationRate = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00011409 File Offset: 0x0000F609
		// (set) Token: 0x0600036A RID: 874 RVA: 0x00011411 File Offset: 0x0000F611
		public float scrollSensitivity
		{
			get
			{
				return this.m_ScrollSensitivity;
			}
			set
			{
				this.m_ScrollSensitivity = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0001141A File Offset: 0x0000F61A
		// (set) Token: 0x0600036C RID: 876 RVA: 0x00011422 File Offset: 0x0000F622
		public RectTransform viewport
		{
			get
			{
				return this.m_Viewport;
			}
			set
			{
				this.m_Viewport = value;
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00011431 File Offset: 0x0000F631
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0001143C File Offset: 0x0000F63C
		public Scrollbar horizontalScrollbar
		{
			get
			{
				return this.m_HorizontalScrollbar;
			}
			set
			{
				if (this.m_HorizontalScrollbar)
				{
					this.m_HorizontalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
				}
				this.m_HorizontalScrollbar = value;
				if (this.m_HorizontalScrollbar)
				{
					this.m_HorizontalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
				}
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600036F RID: 879 RVA: 0x000114A8 File Offset: 0x0000F6A8
		// (set) Token: 0x06000370 RID: 880 RVA: 0x000114B0 File Offset: 0x0000F6B0
		public Scrollbar verticalScrollbar
		{
			get
			{
				return this.m_VerticalScrollbar;
			}
			set
			{
				if (this.m_VerticalScrollbar)
				{
					this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
				}
				this.m_VerticalScrollbar = value;
				if (this.m_VerticalScrollbar)
				{
					this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
				}
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0001151C File Offset: 0x0000F71C
		// (set) Token: 0x06000372 RID: 882 RVA: 0x00011524 File Offset: 0x0000F724
		public ScrollRect.ScrollbarVisibility horizontalScrollbarVisibility
		{
			get
			{
				return this.m_HorizontalScrollbarVisibility;
			}
			set
			{
				this.m_HorizontalScrollbarVisibility = value;
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00011533 File Offset: 0x0000F733
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0001153B File Offset: 0x0000F73B
		public ScrollRect.ScrollbarVisibility verticalScrollbarVisibility
		{
			get
			{
				return this.m_VerticalScrollbarVisibility;
			}
			set
			{
				this.m_VerticalScrollbarVisibility = value;
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0001154A File Offset: 0x0000F74A
		// (set) Token: 0x06000376 RID: 886 RVA: 0x00011552 File Offset: 0x0000F752
		public float horizontalScrollbarSpacing
		{
			get
			{
				return this.m_HorizontalScrollbarSpacing;
			}
			set
			{
				this.m_HorizontalScrollbarSpacing = value;
				this.SetDirty();
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00011561 File Offset: 0x0000F761
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00011569 File Offset: 0x0000F769
		public float verticalScrollbarSpacing
		{
			get
			{
				return this.m_VerticalScrollbarSpacing;
			}
			set
			{
				this.m_VerticalScrollbarSpacing = value;
				this.SetDirty();
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00011578 File Offset: 0x0000F778
		// (set) Token: 0x0600037A RID: 890 RVA: 0x00011580 File Offset: 0x0000F780
		public ScrollRect.ScrollRectEvent onValueChanged
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

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0001158C File Offset: 0x0000F78C
		protected RectTransform viewRect
		{
			get
			{
				if (this.m_ViewRect == null)
				{
					this.m_ViewRect = this.m_Viewport;
				}
				if (this.m_ViewRect == null)
				{
					this.m_ViewRect = (RectTransform)base.transform;
				}
				return this.m_ViewRect;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600037C RID: 892 RVA: 0x000115D8 File Offset: 0x0000F7D8
		// (set) Token: 0x0600037D RID: 893 RVA: 0x000115E0 File Offset: 0x0000F7E0
		public Vector2 velocity
		{
			get
			{
				return this.m_Velocity;
			}
			set
			{
				this.m_Velocity = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600037E RID: 894 RVA: 0x000115E9 File Offset: 0x0000F7E9
		private RectTransform rectTransform
		{
			get
			{
				if (this.m_Rect == null)
				{
					this.m_Rect = base.GetComponent<RectTransform>();
				}
				return this.m_Rect;
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001160C File Offset: 0x0000F80C
		protected ScrollRect()
		{
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00011694 File Offset: 0x0000F894
		public virtual void Rebuild(CanvasUpdate executing)
		{
			if (executing == CanvasUpdate.Prelayout)
			{
				this.UpdateCachedData();
			}
			if (executing == CanvasUpdate.PostLayout)
			{
				this.UpdateBounds();
				this.UpdateScrollbars(Vector2.zero);
				this.UpdatePrevData();
				this.m_HasRebuiltLayout = true;
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000116C1 File Offset: 0x0000F8C1
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000116C3 File Offset: 0x0000F8C3
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x06000383 RID: 899 RVA: 0x000116C8 File Offset: 0x0000F8C8
		private void UpdateCachedData()
		{
			Transform transform = base.transform;
			this.m_HorizontalScrollbarRect = ((this.m_HorizontalScrollbar == null) ? null : (this.m_HorizontalScrollbar.transform as RectTransform));
			this.m_VerticalScrollbarRect = ((this.m_VerticalScrollbar == null) ? null : (this.m_VerticalScrollbar.transform as RectTransform));
			bool flag = this.viewRect.parent == transform;
			bool flag2 = !this.m_HorizontalScrollbarRect || this.m_HorizontalScrollbarRect.parent == transform;
			bool flag3 = !this.m_VerticalScrollbarRect || this.m_VerticalScrollbarRect.parent == transform;
			bool flag4 = flag && flag2 && flag3;
			this.m_HSliderExpand = flag4 && this.m_HorizontalScrollbarRect && this.horizontalScrollbarVisibility == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
			this.m_VSliderExpand = flag4 && this.m_VerticalScrollbarRect && this.verticalScrollbarVisibility == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
			this.m_HSliderHeight = ((this.m_HorizontalScrollbarRect == null) ? 0f : this.m_HorizontalScrollbarRect.rect.height);
			this.m_VSliderWidth = ((this.m_VerticalScrollbarRect == null) ? 0f : this.m_VerticalScrollbarRect.rect.width);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00011828 File Offset: 0x0000FA28
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
			}
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
			}
			CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
			this.SetDirty();
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001189C File Offset: 0x0000FA9C
		protected override void OnDisable()
		{
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
			}
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
			}
			this.m_Dragging = false;
			this.m_Scrolling = false;
			this.m_HasRebuiltLayout = false;
			this.m_Tracker.Clear();
			this.m_Velocity = Vector2.zero;
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001193D File Offset: 0x0000FB3D
		public override bool IsActive()
		{
			return base.IsActive() && this.m_Content != null;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00011955 File Offset: 0x0000FB55
		private void EnsureLayoutHasRebuilt()
		{
			if (!this.m_HasRebuiltLayout && !CanvasUpdateRegistry.IsRebuildingLayout())
			{
				Canvas.ForceUpdateCanvases();
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001196B File Offset: 0x0000FB6B
		public virtual void StopMovement()
		{
			this.m_Velocity = Vector2.zero;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00011978 File Offset: 0x0000FB78
		public virtual void OnScroll(PointerEventData data)
		{
			if (!this.IsActive())
			{
				return;
			}
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			Vector2 scrollDelta = data.scrollDelta;
			scrollDelta.y *= -1f;
			if (this.vertical && !this.horizontal)
			{
				if (Mathf.Abs(scrollDelta.x) > Mathf.Abs(scrollDelta.y))
				{
					scrollDelta.y = scrollDelta.x;
				}
				scrollDelta.x = 0f;
			}
			if (this.horizontal && !this.vertical)
			{
				if (Mathf.Abs(scrollDelta.y) > Mathf.Abs(scrollDelta.x))
				{
					scrollDelta.x = scrollDelta.y;
				}
				scrollDelta.y = 0f;
			}
			if (data.IsScrolling())
			{
				this.m_Scrolling = true;
			}
			Vector2 vector = this.m_Content.anchoredPosition;
			vector += scrollDelta * this.m_ScrollSensitivity;
			if (this.m_MovementType == ScrollRect.MovementType.Clamped)
			{
				vector += this.CalculateOffset(vector - this.m_Content.anchoredPosition);
			}
			this.SetContentAnchoredPosition(vector);
			this.UpdateBounds();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00011A95 File Offset: 0x0000FC95
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.m_Velocity = Vector2.zero;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00011AAC File Offset: 0x0000FCAC
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateBounds();
			this.m_PointerStartLocalCursor = Vector2.zero;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewRect, eventData.position, eventData.pressEventCamera, out this.m_PointerStartLocalCursor);
			this.m_ContentStartPosition = this.m_Content.anchoredPosition;
			this.m_Dragging = true;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00011B12 File Offset: 0x0000FD12
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.m_Dragging = false;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00011B24 File Offset: 0x0000FD24
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.m_Dragging)
			{
				return;
			}
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (!this.IsActive())
			{
				return;
			}
			Vector2 vector;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewRect, eventData.position, eventData.pressEventCamera, out vector))
			{
				return;
			}
			this.UpdateBounds();
			Vector2 vector2 = vector - this.m_PointerStartLocalCursor;
			Vector2 vector3 = this.m_ContentStartPosition + vector2;
			Vector2 vector4 = this.CalculateOffset(vector3 - this.m_Content.anchoredPosition);
			vector3 += vector4;
			if (this.m_MovementType == ScrollRect.MovementType.Elastic)
			{
				if (vector4.x != 0f)
				{
					vector3.x -= ScrollRect.RubberDelta(vector4.x, this.m_ViewBounds.size.x);
				}
				if (vector4.y != 0f)
				{
					vector3.y -= ScrollRect.RubberDelta(vector4.y, this.m_ViewBounds.size.y);
				}
			}
			this.SetContentAnchoredPosition(vector3);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00011C24 File Offset: 0x0000FE24
		protected virtual void SetContentAnchoredPosition(Vector2 position)
		{
			if (!this.m_Horizontal)
			{
				position.x = this.m_Content.anchoredPosition.x;
			}
			if (!this.m_Vertical)
			{
				position.y = this.m_Content.anchoredPosition.y;
			}
			if (position != this.m_Content.anchoredPosition)
			{
				this.m_Content.anchoredPosition = position;
				this.UpdateBounds();
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00011C94 File Offset: 0x0000FE94
		protected virtual void LateUpdate()
		{
			if (!this.m_Content)
			{
				return;
			}
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			float unscaledDeltaTime = Time.unscaledDeltaTime;
			Vector2 vector = this.CalculateOffset(Vector2.zero);
			if (unscaledDeltaTime > 0f)
			{
				if (!this.m_Dragging && (vector != Vector2.zero || this.m_Velocity != Vector2.zero))
				{
					Vector2 vector2 = this.m_Content.anchoredPosition;
					for (int i = 0; i < 2; i++)
					{
						if (this.m_MovementType == ScrollRect.MovementType.Elastic && vector[i] != 0f)
						{
							float num = this.m_Velocity[i];
							float num2 = this.m_Elasticity;
							if (this.m_Scrolling)
							{
								num2 *= 3f;
							}
							vector2[i] = Mathf.SmoothDamp(this.m_Content.anchoredPosition[i], this.m_Content.anchoredPosition[i] + vector[i], ref num, num2, float.PositiveInfinity, unscaledDeltaTime);
							if (Mathf.Abs(num) < 1f)
							{
								num = 0f;
							}
							this.m_Velocity[i] = num;
						}
						else if (this.m_Inertia)
						{
							ref Vector2 ptr = ref this.m_Velocity;
							int num3 = i;
							ptr[num3] *= Mathf.Pow(this.m_DecelerationRate, unscaledDeltaTime);
							if (Mathf.Abs(this.m_Velocity[i]) < 1f)
							{
								this.m_Velocity[i] = 0f;
							}
							ptr = ref vector2;
							num3 = i;
							ptr[num3] += this.m_Velocity[i] * unscaledDeltaTime;
						}
						else
						{
							this.m_Velocity[i] = 0f;
						}
					}
					if (this.m_MovementType == ScrollRect.MovementType.Clamped)
					{
						vector = this.CalculateOffset(vector2 - this.m_Content.anchoredPosition);
						vector2 += vector;
					}
					this.SetContentAnchoredPosition(vector2);
				}
				if (this.m_Dragging && this.m_Inertia)
				{
					Vector3 vector3 = (this.m_Content.anchoredPosition - this.m_PrevPosition) / unscaledDeltaTime;
					this.m_Velocity = Vector3.Lerp(this.m_Velocity, vector3, unscaledDeltaTime * 10f);
				}
			}
			if (this.m_ViewBounds != this.m_PrevViewBounds || this.m_ContentBounds != this.m_PrevContentBounds || this.m_Content.anchoredPosition != this.m_PrevPosition)
			{
				this.UpdateScrollbars(vector);
				UISystemProfilerApi.AddMarker("ScrollRect.value", this);
				this.m_OnValueChanged.Invoke(this.normalizedPosition);
				this.UpdatePrevData();
			}
			this.UpdateScrollbarVisibility();
			this.m_Scrolling = false;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00011F6C File Offset: 0x0001016C
		protected void UpdatePrevData()
		{
			if (this.m_Content == null)
			{
				this.m_PrevPosition = Vector2.zero;
			}
			else
			{
				this.m_PrevPosition = this.m_Content.anchoredPosition;
			}
			this.m_PrevViewBounds = this.m_ViewBounds;
			this.m_PrevContentBounds = this.m_ContentBounds;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00011FC0 File Offset: 0x000101C0
		private void UpdateScrollbars(Vector2 offset)
		{
			if (this.m_HorizontalScrollbar)
			{
				if (this.m_ContentBounds.size.x > 0f)
				{
					this.m_HorizontalScrollbar.size = Mathf.Clamp01((this.m_ViewBounds.size.x - Mathf.Abs(offset.x)) / this.m_ContentBounds.size.x);
				}
				else
				{
					this.m_HorizontalScrollbar.size = 1f;
				}
				this.m_HorizontalScrollbar.value = this.horizontalNormalizedPosition;
			}
			if (this.m_VerticalScrollbar)
			{
				if (this.m_ContentBounds.size.y > 0f)
				{
					this.m_VerticalScrollbar.size = Mathf.Clamp01((this.m_ViewBounds.size.y - Mathf.Abs(offset.y)) / this.m_ContentBounds.size.y);
				}
				else
				{
					this.m_VerticalScrollbar.size = 1f;
				}
				this.m_VerticalScrollbar.value = this.verticalNormalizedPosition;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000392 RID: 914 RVA: 0x000120D5 File Offset: 0x000102D5
		// (set) Token: 0x06000393 RID: 915 RVA: 0x000120E8 File Offset: 0x000102E8
		public Vector2 normalizedPosition
		{
			get
			{
				return new Vector2(this.horizontalNormalizedPosition, this.verticalNormalizedPosition);
			}
			set
			{
				this.SetNormalizedPosition(value.x, 0);
				this.SetNormalizedPosition(value.y, 1);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00012104 File Offset: 0x00010304
		// (set) Token: 0x06000395 RID: 917 RVA: 0x000121CB File Offset: 0x000103CB
		public float horizontalNormalizedPosition
		{
			get
			{
				this.UpdateBounds();
				if (this.m_ContentBounds.size.x <= this.m_ViewBounds.size.x || Mathf.Approximately(this.m_ContentBounds.size.x, this.m_ViewBounds.size.x))
				{
					return (float)((this.m_ViewBounds.min.x > this.m_ContentBounds.min.x) ? 1 : 0);
				}
				return (this.m_ViewBounds.min.x - this.m_ContentBounds.min.x) / (this.m_ContentBounds.size.x - this.m_ViewBounds.size.x);
			}
			set
			{
				this.SetNormalizedPosition(value, 0);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000396 RID: 918 RVA: 0x000121D8 File Offset: 0x000103D8
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0001229F File Offset: 0x0001049F
		public float verticalNormalizedPosition
		{
			get
			{
				this.UpdateBounds();
				if (this.m_ContentBounds.size.y <= this.m_ViewBounds.size.y || Mathf.Approximately(this.m_ContentBounds.size.y, this.m_ViewBounds.size.y))
				{
					return (float)((this.m_ViewBounds.min.y > this.m_ContentBounds.min.y) ? 1 : 0);
				}
				return (this.m_ViewBounds.min.y - this.m_ContentBounds.min.y) / (this.m_ContentBounds.size.y - this.m_ViewBounds.size.y);
			}
			set
			{
				this.SetNormalizedPosition(value, 1);
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000122A9 File Offset: 0x000104A9
		private void SetHorizontalNormalizedPosition(float value)
		{
			this.SetNormalizedPosition(value, 0);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000122B3 File Offset: 0x000104B3
		private void SetVerticalNormalizedPosition(float value)
		{
			this.SetNormalizedPosition(value, 1);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000122C0 File Offset: 0x000104C0
		protected virtual void SetNormalizedPosition(float value, int axis)
		{
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			float num = this.m_ContentBounds.size[axis] - this.m_ViewBounds.size[axis];
			float num2 = this.m_ViewBounds.min[axis] - value * num;
			float num3 = this.m_Content.anchoredPosition[axis] + num2 - this.m_ContentBounds.min[axis];
			Vector3 vector = this.m_Content.anchoredPosition;
			if (Mathf.Abs(vector[axis] - num3) > 0.01f)
			{
				vector[axis] = num3;
				this.m_Content.anchoredPosition = vector;
				this.m_Velocity[axis] = 0f;
				this.UpdateBounds();
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000123A5 File Offset: 0x000105A5
		private static float RubberDelta(float overStretching, float viewSize)
		{
			return (1f - 1f / (Mathf.Abs(overStretching) * 0.55f / viewSize + 1f)) * viewSize * Mathf.Sign(overStretching);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000123D0 File Offset: 0x000105D0
		protected override void OnRectTransformDimensionsChange()
		{
			this.SetDirty();
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600039D RID: 925 RVA: 0x000123D8 File Offset: 0x000105D8
		private bool hScrollingNeeded
		{
			get
			{
				return !Application.isPlaying || this.m_ContentBounds.size.x > this.m_ViewBounds.size.x + 0.01f;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0001240B File Offset: 0x0001060B
		private bool vScrollingNeeded
		{
			get
			{
				return !Application.isPlaying || this.m_ContentBounds.size.y > this.m_ViewBounds.size.y + 0.01f;
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001243E File Offset: 0x0001063E
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00012440 File Offset: 0x00010640
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00012442 File Offset: 0x00010642
		public virtual float minWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00012449 File Offset: 0x00010649
		public virtual float preferredWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00012450 File Offset: 0x00010650
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00012457 File Offset: 0x00010657
		public virtual float minHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0001245E File Offset: 0x0001065E
		public virtual float preferredHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00012465 File Offset: 0x00010665
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0001246C File Offset: 0x0001066C
		public virtual int layoutPriority
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00012470 File Offset: 0x00010670
		public virtual void SetLayoutHorizontal()
		{
			this.m_Tracker.Clear();
			this.UpdateCachedData();
			if (this.m_HSliderExpand || this.m_VSliderExpand)
			{
				this.m_Tracker.Add(this, this.viewRect, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
				this.viewRect.anchorMin = Vector2.zero;
				this.viewRect.anchorMax = Vector2.one;
				this.viewRect.sizeDelta = Vector2.zero;
				this.viewRect.anchoredPosition = Vector2.zero;
				LayoutRebuilder.ForceRebuildLayoutImmediate(this.content);
				this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
				this.m_ContentBounds = this.GetBounds();
			}
			if (this.m_VSliderExpand && this.vScrollingNeeded)
			{
				this.viewRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.viewRect.sizeDelta.y);
				LayoutRebuilder.ForceRebuildLayoutImmediate(this.content);
				this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
				this.m_ContentBounds = this.GetBounds();
			}
			if (this.m_HSliderExpand && this.hScrollingNeeded)
			{
				this.viewRect.sizeDelta = new Vector2(this.viewRect.sizeDelta.x, -(this.m_HSliderHeight + this.m_HorizontalScrollbarSpacing));
				this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
				this.m_ContentBounds = this.GetBounds();
			}
			if (this.m_VSliderExpand && this.vScrollingNeeded && this.viewRect.sizeDelta.x == 0f && this.viewRect.sizeDelta.y < 0f)
			{
				this.viewRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.viewRect.sizeDelta.y);
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000126D4 File Offset: 0x000108D4
		public virtual void SetLayoutVertical()
		{
			this.UpdateScrollbarLayout();
			this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
			this.m_ContentBounds = this.GetBounds();
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001272E File Offset: 0x0001092E
		private void UpdateScrollbarVisibility()
		{
			ScrollRect.UpdateOneScrollbarVisibility(this.vScrollingNeeded, this.m_Vertical, this.m_VerticalScrollbarVisibility, this.m_VerticalScrollbar);
			ScrollRect.UpdateOneScrollbarVisibility(this.hScrollingNeeded, this.m_Horizontal, this.m_HorizontalScrollbarVisibility, this.m_HorizontalScrollbar);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001276C File Offset: 0x0001096C
		private static void UpdateOneScrollbarVisibility(bool xScrollingNeeded, bool xAxisEnabled, ScrollRect.ScrollbarVisibility scrollbarVisibility, Scrollbar scrollbar)
		{
			if (scrollbar)
			{
				if (scrollbarVisibility == ScrollRect.ScrollbarVisibility.Permanent)
				{
					if (scrollbar.gameObject.activeSelf != xAxisEnabled)
					{
						scrollbar.gameObject.SetActive(xAxisEnabled);
						return;
					}
				}
				else if (scrollbar.gameObject.activeSelf != xScrollingNeeded)
				{
					scrollbar.gameObject.SetActive(xScrollingNeeded);
				}
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x000127BC File Offset: 0x000109BC
		private void UpdateScrollbarLayout()
		{
			if (this.m_VSliderExpand && this.m_HorizontalScrollbar)
			{
				this.m_Tracker.Add(this, this.m_HorizontalScrollbarRect, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.SizeDeltaX);
				this.m_HorizontalScrollbarRect.anchorMin = new Vector2(0f, this.m_HorizontalScrollbarRect.anchorMin.y);
				this.m_HorizontalScrollbarRect.anchorMax = new Vector2(1f, this.m_HorizontalScrollbarRect.anchorMax.y);
				this.m_HorizontalScrollbarRect.anchoredPosition = new Vector2(0f, this.m_HorizontalScrollbarRect.anchoredPosition.y);
				if (this.vScrollingNeeded)
				{
					this.m_HorizontalScrollbarRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.m_HorizontalScrollbarRect.sizeDelta.y);
				}
				else
				{
					this.m_HorizontalScrollbarRect.sizeDelta = new Vector2(0f, this.m_HorizontalScrollbarRect.sizeDelta.y);
				}
			}
			if (this.m_HSliderExpand && this.m_VerticalScrollbar)
			{
				this.m_Tracker.Add(this, this.m_VerticalScrollbarRect, DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaY);
				this.m_VerticalScrollbarRect.anchorMin = new Vector2(this.m_VerticalScrollbarRect.anchorMin.x, 0f);
				this.m_VerticalScrollbarRect.anchorMax = new Vector2(this.m_VerticalScrollbarRect.anchorMax.x, 1f);
				this.m_VerticalScrollbarRect.anchoredPosition = new Vector2(this.m_VerticalScrollbarRect.anchoredPosition.x, 0f);
				if (this.hScrollingNeeded)
				{
					this.m_VerticalScrollbarRect.sizeDelta = new Vector2(this.m_VerticalScrollbarRect.sizeDelta.x, -(this.m_HSliderHeight + this.m_HorizontalScrollbarSpacing));
					return;
				}
				this.m_VerticalScrollbarRect.sizeDelta = new Vector2(this.m_VerticalScrollbarRect.sizeDelta.x, 0f);
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x000129C4 File Offset: 0x00010BC4
		protected void UpdateBounds()
		{
			this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
			this.m_ContentBounds = this.GetBounds();
			if (this.m_Content == null)
			{
				return;
			}
			Vector3 size = this.m_ContentBounds.size;
			Vector3 vector = this.m_ContentBounds.center;
			Vector2 pivot = this.m_Content.pivot;
			ScrollRect.AdjustBounds(ref this.m_ViewBounds, ref pivot, ref size, ref vector);
			this.m_ContentBounds.size = size;
			this.m_ContentBounds.center = vector;
			if (this.movementType == ScrollRect.MovementType.Clamped)
			{
				Vector2 zero = Vector2.zero;
				if (this.m_ViewBounds.max.x > this.m_ContentBounds.max.x)
				{
					zero.x = Math.Min(this.m_ViewBounds.min.x - this.m_ContentBounds.min.x, this.m_ViewBounds.max.x - this.m_ContentBounds.max.x);
				}
				else if (this.m_ViewBounds.min.x < this.m_ContentBounds.min.x)
				{
					zero.x = Math.Max(this.m_ViewBounds.min.x - this.m_ContentBounds.min.x, this.m_ViewBounds.max.x - this.m_ContentBounds.max.x);
				}
				if (this.m_ViewBounds.min.y < this.m_ContentBounds.min.y)
				{
					zero.y = Math.Max(this.m_ViewBounds.min.y - this.m_ContentBounds.min.y, this.m_ViewBounds.max.y - this.m_ContentBounds.max.y);
				}
				else if (this.m_ViewBounds.max.y > this.m_ContentBounds.max.y)
				{
					zero.y = Math.Min(this.m_ViewBounds.min.y - this.m_ContentBounds.min.y, this.m_ViewBounds.max.y - this.m_ContentBounds.max.y);
				}
				if (zero.sqrMagnitude > 1E-45f)
				{
					vector = this.m_Content.anchoredPosition + zero;
					if (!this.m_Horizontal)
					{
						vector.x = this.m_Content.anchoredPosition.x;
					}
					if (!this.m_Vertical)
					{
						vector.y = this.m_Content.anchoredPosition.y;
					}
					ScrollRect.AdjustBounds(ref this.m_ViewBounds, ref pivot, ref size, ref vector);
				}
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00012CC0 File Offset: 0x00010EC0
		internal static void AdjustBounds(ref Bounds viewBounds, ref Vector2 contentPivot, ref Vector3 contentSize, ref Vector3 contentPos)
		{
			Vector3 vector = viewBounds.size - contentSize;
			if (vector.x > 0f)
			{
				contentPos.x -= vector.x * (contentPivot.x - 0.5f);
				contentSize.x = viewBounds.size.x;
			}
			if (vector.y > 0f)
			{
				contentPos.y -= vector.y * (contentPivot.y - 0.5f);
				contentSize.y = viewBounds.size.y;
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00012D58 File Offset: 0x00010F58
		private Bounds GetBounds()
		{
			if (this.m_Content == null)
			{
				return default(Bounds);
			}
			this.m_Content.GetWorldCorners(this.m_Corners);
			Matrix4x4 worldToLocalMatrix = this.viewRect.worldToLocalMatrix;
			return ScrollRect.InternalGetBounds(this.m_Corners, ref worldToLocalMatrix);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00012DA8 File Offset: 0x00010FA8
		internal static Bounds InternalGetBounds(Vector3[] corners, ref Matrix4x4 viewWorldToLocalMatrix)
		{
			Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			Vector3 vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
			for (int i = 0; i < 4; i++)
			{
				Vector3 vector3 = viewWorldToLocalMatrix.MultiplyPoint3x4(corners[i]);
				vector = Vector3.Min(vector3, vector);
				vector2 = Vector3.Max(vector3, vector2);
			}
			Bounds bounds = new Bounds(vector, Vector3.zero);
			bounds.Encapsulate(vector2);
			return bounds;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00012E1F File Offset: 0x0001101F
		private Vector2 CalculateOffset(Vector2 delta)
		{
			return ScrollRect.InternalCalculateOffset(ref this.m_ViewBounds, ref this.m_ContentBounds, this.m_Horizontal, this.m_Vertical, this.m_MovementType, ref delta);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00012E48 File Offset: 0x00011048
		internal static Vector2 InternalCalculateOffset(ref Bounds viewBounds, ref Bounds contentBounds, bool horizontal, bool vertical, ScrollRect.MovementType movementType, ref Vector2 delta)
		{
			Vector2 zero = Vector2.zero;
			if (movementType == ScrollRect.MovementType.Unrestricted)
			{
				return zero;
			}
			Vector2 vector = contentBounds.min;
			Vector2 vector2 = contentBounds.max;
			if (horizontal)
			{
				vector.x += delta.x;
				vector2.x += delta.x;
				float num = viewBounds.max.x - vector2.x;
				float num2 = viewBounds.min.x - vector.x;
				if (num2 < -0.001f)
				{
					zero.x = num2;
				}
				else if (num > 0.001f)
				{
					zero.x = num;
				}
			}
			if (vertical)
			{
				vector.y += delta.y;
				vector2.y += delta.y;
				float num3 = viewBounds.max.y - vector2.y;
				float num4 = viewBounds.min.y - vector.y;
				if (num3 > 0.001f)
				{
					zero.y = num3;
				}
				else if (num4 < -0.001f)
				{
					zero.y = num4;
				}
			}
			return zero;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00012F61 File Offset: 0x00011161
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00012F77 File Offset: 0x00011177
		protected void SetDirtyCaching()
		{
			if (!this.IsActive())
			{
				return;
			}
			CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			this.m_ViewRect = null;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00012F9A File Offset: 0x0001119A
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x04000125 RID: 293
		[SerializeField]
		private RectTransform m_Content;

		// Token: 0x04000126 RID: 294
		[SerializeField]
		private bool m_Horizontal = true;

		// Token: 0x04000127 RID: 295
		[SerializeField]
		private bool m_Vertical = true;

		// Token: 0x04000128 RID: 296
		[SerializeField]
		private ScrollRect.MovementType m_MovementType = ScrollRect.MovementType.Elastic;

		// Token: 0x04000129 RID: 297
		[SerializeField]
		private float m_Elasticity = 0.1f;

		// Token: 0x0400012A RID: 298
		[SerializeField]
		private bool m_Inertia = true;

		// Token: 0x0400012B RID: 299
		[SerializeField]
		private float m_DecelerationRate = 0.135f;

		// Token: 0x0400012C RID: 300
		[SerializeField]
		private float m_ScrollSensitivity = 1f;

		// Token: 0x0400012D RID: 301
		[SerializeField]
		private RectTransform m_Viewport;

		// Token: 0x0400012E RID: 302
		[SerializeField]
		private Scrollbar m_HorizontalScrollbar;

		// Token: 0x0400012F RID: 303
		[SerializeField]
		private Scrollbar m_VerticalScrollbar;

		// Token: 0x04000130 RID: 304
		[SerializeField]
		private ScrollRect.ScrollbarVisibility m_HorizontalScrollbarVisibility;

		// Token: 0x04000131 RID: 305
		[SerializeField]
		private ScrollRect.ScrollbarVisibility m_VerticalScrollbarVisibility;

		// Token: 0x04000132 RID: 306
		[SerializeField]
		private float m_HorizontalScrollbarSpacing;

		// Token: 0x04000133 RID: 307
		[SerializeField]
		private float m_VerticalScrollbarSpacing;

		// Token: 0x04000134 RID: 308
		[SerializeField]
		private ScrollRect.ScrollRectEvent m_OnValueChanged = new ScrollRect.ScrollRectEvent();

		// Token: 0x04000135 RID: 309
		private Vector2 m_PointerStartLocalCursor = Vector2.zero;

		// Token: 0x04000136 RID: 310
		protected Vector2 m_ContentStartPosition = Vector2.zero;

		// Token: 0x04000137 RID: 311
		private RectTransform m_ViewRect;

		// Token: 0x04000138 RID: 312
		protected Bounds m_ContentBounds;

		// Token: 0x04000139 RID: 313
		private Bounds m_ViewBounds;

		// Token: 0x0400013A RID: 314
		private Vector2 m_Velocity;

		// Token: 0x0400013B RID: 315
		private bool m_Dragging;

		// Token: 0x0400013C RID: 316
		private bool m_Scrolling;

		// Token: 0x0400013D RID: 317
		private Vector2 m_PrevPosition = Vector2.zero;

		// Token: 0x0400013E RID: 318
		private Bounds m_PrevContentBounds;

		// Token: 0x0400013F RID: 319
		private Bounds m_PrevViewBounds;

		// Token: 0x04000140 RID: 320
		[NonSerialized]
		private bool m_HasRebuiltLayout;

		// Token: 0x04000141 RID: 321
		private bool m_HSliderExpand;

		// Token: 0x04000142 RID: 322
		private bool m_VSliderExpand;

		// Token: 0x04000143 RID: 323
		private float m_HSliderHeight;

		// Token: 0x04000144 RID: 324
		private float m_VSliderWidth;

		// Token: 0x04000145 RID: 325
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x04000146 RID: 326
		private RectTransform m_HorizontalScrollbarRect;

		// Token: 0x04000147 RID: 327
		private RectTransform m_VerticalScrollbarRect;

		// Token: 0x04000148 RID: 328
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x04000149 RID: 329
		private readonly Vector3[] m_Corners = new Vector3[4];

		// Token: 0x020000A7 RID: 167
		public enum MovementType
		{
			// Token: 0x040002EE RID: 750
			Unrestricted,
			// Token: 0x040002EF RID: 751
			Elastic,
			// Token: 0x040002F0 RID: 752
			Clamped
		}

		// Token: 0x020000A8 RID: 168
		public enum ScrollbarVisibility
		{
			// Token: 0x040002F2 RID: 754
			Permanent,
			// Token: 0x040002F3 RID: 755
			AutoHide,
			// Token: 0x040002F4 RID: 756
			AutoHideAndExpandViewport
		}

		// Token: 0x020000A9 RID: 169
		[Serializable]
		public class ScrollRectEvent : UnityEvent<Vector2>
		{
		}
	}
}
