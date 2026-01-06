using System;
using System.Collections.Generic;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x0200011C RID: 284
	public abstract class BaseSlider<TValueType> : BaseField<TValueType> where TValueType : IComparable<TValueType>
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00023461 File Offset: 0x00021661
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x00023469 File Offset: 0x00021669
		internal VisualElement dragContainer { get; private set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00023472 File Offset: 0x00021672
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x0002347A File Offset: 0x0002167A
		internal VisualElement dragElement { get; private set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x00023483 File Offset: 0x00021683
		// (set) Token: 0x0600091D RID: 2333 RVA: 0x0002348B File Offset: 0x0002168B
		internal VisualElement dragBorderElement { get; private set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x00023494 File Offset: 0x00021694
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x0002349C File Offset: 0x0002169C
		internal TextField inputTextField { get; private set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x000234A8 File Offset: 0x000216A8
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x000234C0 File Offset: 0x000216C0
		public TValueType lowValue
		{
			get
			{
				return this.m_LowValue;
			}
			set
			{
				bool flag = !EqualityComparer<TValueType>.Default.Equals(this.m_LowValue, value);
				if (flag)
				{
					this.m_LowValue = value;
					this.ClampValue();
					this.UpdateDragElementPosition();
					base.SaveViewData();
				}
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x00023504 File Offset: 0x00021704
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x0002351C File Offset: 0x0002171C
		public TValueType highValue
		{
			get
			{
				return this.m_HighValue;
			}
			set
			{
				bool flag = !EqualityComparer<TValueType>.Default.Equals(this.m_HighValue, value);
				if (flag)
				{
					this.m_HighValue = value;
					this.ClampValue();
					this.UpdateDragElementPosition();
					base.SaveViewData();
				}
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00023560 File Offset: 0x00021760
		internal void SetHighValueWithoutNotify(TValueType newHighValue)
		{
			this.m_HighValue = newHighValue;
			TValueType tvalueType = (this.clamped ? this.GetClampedValue(this.value) : this.value);
			this.SetValueWithoutNotify(tvalueType);
			this.UpdateDragElementPosition();
			base.SaveViewData();
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x000235A8 File Offset: 0x000217A8
		public TValueType range
		{
			get
			{
				return this.SliderRange();
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x000235C0 File Offset: 0x000217C0
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x000235D8 File Offset: 0x000217D8
		public virtual float pageSize
		{
			get
			{
				return this.m_PageSize;
			}
			set
			{
				this.m_PageSize = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x000235E4 File Offset: 0x000217E4
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x000235FC File Offset: 0x000217FC
		public virtual bool showInputField
		{
			get
			{
				return this.m_ShowInputField;
			}
			set
			{
				bool flag = this.m_ShowInputField != value;
				if (flag)
				{
					this.m_ShowInputField = value;
					this.UpdateTextFieldVisibility();
				}
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0002362A File Offset: 0x0002182A
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x00023632 File Offset: 0x00021832
		internal bool clamped { get; set; } = true;

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0002363B File Offset: 0x0002183B
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x00023643 File Offset: 0x00021843
		internal ClampedDragger<TValueType> clampedDragger { get; private set; }

		// Token: 0x0600092E RID: 2350 RVA: 0x0002364C File Offset: 0x0002184C
		private TValueType Clamp(TValueType value, TValueType lowBound, TValueType highBound)
		{
			TValueType tvalueType = value;
			bool flag = lowBound.CompareTo(value) > 0;
			if (flag)
			{
				tvalueType = lowBound;
			}
			else
			{
				bool flag2 = highBound.CompareTo(value) < 0;
				if (flag2)
				{
					tvalueType = highBound;
				}
			}
			return tvalueType;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00023698 File Offset: 0x00021898
		private TValueType GetClampedValue(TValueType newValue)
		{
			TValueType tvalueType = this.lowValue;
			TValueType tvalueType2 = this.highValue;
			bool flag = tvalueType.CompareTo(tvalueType2) > 0;
			if (flag)
			{
				TValueType tvalueType3 = tvalueType;
				tvalueType = tvalueType2;
				tvalueType2 = tvalueType3;
			}
			return this.Clamp(newValue, tvalueType, tvalueType2);
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x000236E0 File Offset: 0x000218E0
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x000236F8 File Offset: 0x000218F8
		public override TValueType value
		{
			get
			{
				return base.value;
			}
			set
			{
				TValueType tvalueType = (this.clamped ? this.GetClampedValue(value) : value);
				base.value = tvalueType;
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00023724 File Offset: 0x00021924
		public override void SetValueWithoutNotify(TValueType newValue)
		{
			TValueType tvalueType = (this.clamped ? this.GetClampedValue(newValue) : newValue);
			base.SetValueWithoutNotify(tvalueType);
			this.UpdateDragElementPosition();
			this.UpdateTextFieldValue();
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x0002375C File Offset: 0x0002195C
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x00023774 File Offset: 0x00021974
		public SliderDirection direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				this.m_Direction = value;
				bool flag = this.m_Direction == SliderDirection.Horizontal;
				if (flag)
				{
					base.RemoveFromClassList(BaseSlider<TValueType>.verticalVariantUssClassName);
					base.AddToClassList(BaseSlider<TValueType>.horizontalVariantUssClassName);
				}
				else
				{
					base.RemoveFromClassList(BaseSlider<TValueType>.horizontalVariantUssClassName);
					base.AddToClassList(BaseSlider<TValueType>.verticalVariantUssClassName);
				}
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x000237CC File Offset: 0x000219CC
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x000237E4 File Offset: 0x000219E4
		public bool inverted
		{
			get
			{
				return this.m_Inverted;
			}
			set
			{
				bool flag = this.m_Inverted != value;
				if (flag)
				{
					this.m_Inverted = value;
					this.UpdateDragElementPosition();
				}
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00023814 File Offset: 0x00021A14
		internal BaseSlider(string label, TValueType start, TValueType end, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f)
			: base(label, null)
		{
			base.AddToClassList(BaseSlider<TValueType>.ussClassName);
			base.labelElement.AddToClassList(BaseSlider<TValueType>.labelUssClassName);
			base.visualInput.AddToClassList(BaseSlider<TValueType>.inputUssClassName);
			this.direction = direction;
			this.pageSize = pageSize;
			this.lowValue = start;
			this.highValue = end;
			base.pickingMode = PickingMode.Ignore;
			this.dragContainer = new VisualElement
			{
				name = "unity-drag-container"
			};
			this.dragContainer.AddToClassList(BaseSlider<TValueType>.dragContainerUssClassName);
			base.visualInput.Add(this.dragContainer);
			VisualElement visualElement = new VisualElement
			{
				name = "unity-tracker"
			};
			visualElement.AddToClassList(BaseSlider<TValueType>.trackerUssClassName);
			this.dragContainer.Add(visualElement);
			this.dragBorderElement = new VisualElement
			{
				name = "unity-dragger-border"
			};
			this.dragBorderElement.AddToClassList(BaseSlider<TValueType>.draggerBorderUssClassName);
			this.dragContainer.Add(this.dragBorderElement);
			this.dragElement = new VisualElement
			{
				name = "unity-dragger"
			};
			this.dragElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.UpdateDragElementPosition), TrickleDown.NoTrickleDown);
			this.dragElement.AddToClassList(BaseSlider<TValueType>.draggerUssClassName);
			this.dragContainer.Add(this.dragElement);
			this.clampedDragger = new ClampedDragger<TValueType>(this, new Action(this.SetSliderValueFromClick), new Action(this.SetSliderValueFromDrag));
			this.dragContainer.pickingMode = PickingMode.Position;
			this.dragContainer.AddManipulator(this.clampedDragger);
			base.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
			this.UpdateTextFieldVisibility();
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x000239EC File Offset: 0x00021BEC
		protected static float GetClosestPowerOfTen(float positiveNumber)
		{
			bool flag = positiveNumber <= 0f;
			float num;
			if (flag)
			{
				num = 1f;
			}
			else
			{
				num = Mathf.Pow(10f, (float)Mathf.RoundToInt(Mathf.Log10(positiveNumber)));
			}
			return num;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00023A2C File Offset: 0x00021C2C
		protected static float RoundToMultipleOf(float value, float roundingValue)
		{
			bool flag = roundingValue == 0f;
			float num;
			if (flag)
			{
				num = value;
			}
			else
			{
				num = Mathf.Round(value / roundingValue) * roundingValue;
			}
			return num;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00023A58 File Offset: 0x00021C58
		private void ClampValue()
		{
			this.value = base.rawValue;
		}

		// Token: 0x0600093B RID: 2363
		internal abstract TValueType SliderLerpUnclamped(TValueType a, TValueType b, float interpolant);

		// Token: 0x0600093C RID: 2364
		internal abstract float SliderNormalizeValue(TValueType currentValue, TValueType lowerValue, TValueType higherValue);

		// Token: 0x0600093D RID: 2365
		internal abstract TValueType SliderRange();

		// Token: 0x0600093E RID: 2366
		internal abstract TValueType ParseStringToValue(string stringValue);

		// Token: 0x0600093F RID: 2367
		internal abstract void ComputeValueFromKey(BaseSlider<TValueType>.SliderKey sliderKey, bool isShift);

		// Token: 0x06000940 RID: 2368 RVA: 0x00023A68 File Offset: 0x00021C68
		private TValueType SliderLerpDirectionalUnclamped(TValueType a, TValueType b, float positionInterpolant)
		{
			float num = ((this.direction == SliderDirection.Vertical) ? (1f - positionInterpolant) : positionInterpolant);
			bool inverted = this.inverted;
			TValueType tvalueType;
			if (inverted)
			{
				tvalueType = this.SliderLerpUnclamped(b, a, num);
			}
			else
			{
				tvalueType = this.SliderLerpUnclamped(a, b, num);
			}
			return tvalueType;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00023AB0 File Offset: 0x00021CB0
		private void SetSliderValueFromDrag()
		{
			bool flag = this.clampedDragger.dragDirection != ClampedDragger<TValueType>.DragDirection.Free;
			if (!flag)
			{
				Vector2 delta = this.clampedDragger.delta;
				bool flag2 = this.direction == SliderDirection.Horizontal;
				if (flag2)
				{
					this.ComputeValueAndDirectionFromDrag(this.dragContainer.resolvedStyle.width, this.dragElement.resolvedStyle.width, this.m_DragElementStartPos.x + delta.x);
				}
				else
				{
					this.ComputeValueAndDirectionFromDrag(this.dragContainer.resolvedStyle.height, this.dragElement.resolvedStyle.height, this.m_DragElementStartPos.y + delta.y);
				}
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00023B68 File Offset: 0x00021D68
		private void ComputeValueAndDirectionFromDrag(float sliderLength, float dragElementLength, float dragElementPos)
		{
			float num = sliderLength - dragElementLength;
			bool flag = Mathf.Abs(num) < 1E-30f;
			if (!flag)
			{
				float num2 = Mathf.Max(0f, Mathf.Min(dragElementPos, num)) / num;
				this.value = this.SliderLerpDirectionalUnclamped(this.lowValue, this.highValue, num2);
			}
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00023BBC File Offset: 0x00021DBC
		private void SetSliderValueFromClick()
		{
			bool flag = this.clampedDragger.dragDirection == ClampedDragger<TValueType>.DragDirection.Free;
			if (!flag)
			{
				bool flag2 = this.clampedDragger.dragDirection == ClampedDragger<TValueType>.DragDirection.None;
				if (flag2)
				{
					bool flag3 = Mathf.Approximately(this.pageSize, 0f);
					if (flag3)
					{
						float num = ((this.direction == SliderDirection.Horizontal) ? (this.clampedDragger.startMousePosition.x - this.dragElement.resolvedStyle.width / 2f) : this.dragElement.transform.position.x);
						float num2 = ((this.direction == SliderDirection.Horizontal) ? this.dragElement.transform.position.y : (this.clampedDragger.startMousePosition.y - this.dragElement.resolvedStyle.height / 2f));
						Vector3 vector = new Vector3(num, num2, 0f);
						this.dragElement.transform.position = vector;
						this.dragBorderElement.transform.position = vector;
						this.m_DragElementStartPos = new Rect(num, num2, this.dragElement.resolvedStyle.width, this.dragElement.resolvedStyle.height);
						this.clampedDragger.dragDirection = ClampedDragger<TValueType>.DragDirection.Free;
						bool flag4 = this.direction == SliderDirection.Horizontal;
						if (flag4)
						{
							this.ComputeValueAndDirectionFromDrag(this.dragContainer.resolvedStyle.width, this.dragElement.resolvedStyle.width, this.m_DragElementStartPos.x);
						}
						else
						{
							this.ComputeValueAndDirectionFromDrag(this.dragContainer.resolvedStyle.height, this.dragElement.resolvedStyle.height, this.m_DragElementStartPos.y);
						}
						return;
					}
					this.m_DragElementStartPos = new Rect(this.dragElement.transform.position.x, this.dragElement.transform.position.y, this.dragElement.resolvedStyle.width, this.dragElement.resolvedStyle.height);
				}
				bool flag5 = this.direction == SliderDirection.Horizontal;
				if (flag5)
				{
					this.ComputeValueAndDirectionFromClick(this.dragContainer.resolvedStyle.width, this.dragElement.resolvedStyle.width, this.dragElement.transform.position.x, this.clampedDragger.lastMousePosition.x);
				}
				else
				{
					this.ComputeValueAndDirectionFromClick(this.dragContainer.resolvedStyle.height, this.dragElement.resolvedStyle.height, this.dragElement.transform.position.y, this.clampedDragger.lastMousePosition.y);
				}
			}
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00023E88 File Offset: 0x00022088
		private void OnKeyDown(KeyDownEvent evt)
		{
			BaseSlider<TValueType>.SliderKey sliderKey = BaseSlider<TValueType>.SliderKey.None;
			bool flag = this.direction == SliderDirection.Horizontal;
			bool flag2 = (flag && evt.keyCode == KeyCode.Home) || (!flag && evt.keyCode == KeyCode.End);
			if (flag2)
			{
				sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.Highest : BaseSlider<TValueType>.SliderKey.Lowest);
			}
			else
			{
				bool flag3 = (flag && evt.keyCode == KeyCode.End) || (!flag && evt.keyCode == KeyCode.Home);
				if (flag3)
				{
					sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.Lowest : BaseSlider<TValueType>.SliderKey.Highest);
				}
				else
				{
					bool flag4 = (flag && evt.keyCode == KeyCode.PageUp) || (!flag && evt.keyCode == KeyCode.PageDown);
					if (flag4)
					{
						sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.HigherPage : BaseSlider<TValueType>.SliderKey.LowerPage);
					}
					else
					{
						bool flag5 = (flag && evt.keyCode == KeyCode.PageDown) || (!flag && evt.keyCode == KeyCode.PageUp);
						if (flag5)
						{
							sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.LowerPage : BaseSlider<TValueType>.SliderKey.HigherPage);
						}
						else
						{
							bool flag6 = (flag && evt.keyCode == KeyCode.LeftArrow) || (!flag && evt.keyCode == KeyCode.DownArrow);
							if (flag6)
							{
								sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.Higher : BaseSlider<TValueType>.SliderKey.Lower);
							}
							else
							{
								bool flag7 = (flag && evt.keyCode == KeyCode.RightArrow) || (!flag && evt.keyCode == KeyCode.UpArrow);
								if (flag7)
								{
									sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.Lower : BaseSlider<TValueType>.SliderKey.Higher);
								}
							}
						}
					}
				}
			}
			bool flag8 = sliderKey == BaseSlider<TValueType>.SliderKey.None;
			if (!flag8)
			{
				this.ComputeValueFromKey(sliderKey, evt.shiftKey);
				evt.StopPropagation();
			}
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00024028 File Offset: 0x00022228
		internal virtual void ComputeValueAndDirectionFromClick(float sliderLength, float dragElementLength, float dragElementPos, float dragElementLastPos)
		{
			float num = sliderLength - dragElementLength;
			bool flag = Mathf.Abs(num) < 1E-30f;
			if (!flag)
			{
				bool flag2 = dragElementLastPos < dragElementPos;
				bool flag3 = dragElementLastPos > dragElementPos + dragElementLength;
				bool flag4 = (this.inverted ? flag3 : flag2);
				bool flag5 = (this.inverted ? flag2 : flag3);
				float num2 = (this.inverted ? (-this.pageSize) : this.pageSize);
				bool flag6 = flag4 && this.clampedDragger.dragDirection != ClampedDragger<TValueType>.DragDirection.LowToHigh;
				if (flag6)
				{
					this.clampedDragger.dragDirection = ClampedDragger<TValueType>.DragDirection.HighToLow;
					float num3 = Mathf.Max(0f, Mathf.Min(dragElementPos - num2, num)) / num;
					this.value = this.SliderLerpDirectionalUnclamped(this.lowValue, this.highValue, num3);
				}
				else
				{
					bool flag7 = flag5 && this.clampedDragger.dragDirection != ClampedDragger<TValueType>.DragDirection.HighToLow;
					if (flag7)
					{
						this.clampedDragger.dragDirection = ClampedDragger<TValueType>.DragDirection.LowToHigh;
						float num4 = Mathf.Max(0f, Mathf.Min(dragElementPos + num2, num)) / num;
						this.value = this.SliderLerpDirectionalUnclamped(this.lowValue, this.highValue, num4);
					}
				}
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00024158 File Offset: 0x00022358
		public void AdjustDragElement(float factor)
		{
			bool flag = factor < 1f;
			this.dragElement.visible = flag;
			bool flag2 = flag;
			if (flag2)
			{
				IStyle style = this.dragElement.style;
				this.dragElement.style.visibility = StyleKeyword.Null;
				bool flag3 = this.direction == SliderDirection.Horizontal;
				if (flag3)
				{
					float num = ((base.resolvedStyle.minWidth == StyleKeyword.Auto) ? 0f : base.resolvedStyle.minWidth.value);
					style.width = Mathf.Round(Mathf.Max(this.dragContainer.layout.width * factor, num));
				}
				else
				{
					float num2 = ((base.resolvedStyle.minHeight == StyleKeyword.Auto) ? 0f : base.resolvedStyle.minHeight.value);
					style.height = Mathf.Round(Mathf.Max(this.dragContainer.layout.height * factor, num2));
				}
			}
			this.dragBorderElement.visible = this.dragElement.visible;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0002429C File Offset: 0x0002249C
		private void UpdateDragElementPosition(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				this.UpdateDragElementPosition();
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x000242D9 File Offset: 0x000224D9
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			this.UpdateDragElementPosition();
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x000242EC File Offset: 0x000224EC
		private bool SameValues(float a, float b, float epsilon)
		{
			return Mathf.Abs(b - a) < epsilon;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0002430C File Offset: 0x0002250C
		private void UpdateDragElementPosition()
		{
			bool flag = base.panel == null;
			if (!flag)
			{
				float num = this.SliderNormalizeValue(this.value, this.lowValue, this.highValue);
				float num2 = (this.inverted ? (1f - num) : num);
				float num3 = base.scaledPixelsPerPoint * 0.5f;
				bool flag2 = this.direction == SliderDirection.Horizontal;
				if (flag2)
				{
					float width = this.dragElement.resolvedStyle.width;
					float num4 = -this.dragElement.resolvedStyle.marginLeft - this.dragElement.resolvedStyle.marginRight;
					float num5 = this.dragContainer.layout.width - width + num4;
					float num6 = num2 * num5;
					bool flag3 = float.IsNaN(num6);
					if (!flag3)
					{
						float x = this.dragElement.transform.position.x;
						bool flag4 = !this.SameValues(x, num6, num3);
						if (flag4)
						{
							Vector3 vector = new Vector3(num6, 0f, 0f);
							this.dragElement.transform.position = vector;
							this.dragBorderElement.transform.position = vector;
						}
					}
				}
				else
				{
					float height = this.dragElement.resolvedStyle.height;
					float num7 = this.dragContainer.resolvedStyle.height - height;
					float num8 = (1f - num2) * num7;
					bool flag5 = float.IsNaN(num8);
					if (!flag5)
					{
						float y = this.dragElement.transform.position.y;
						bool flag6 = !this.SameValues(y, num8, num3);
						if (flag6)
						{
							Vector3 vector2 = new Vector3(0f, num8, 0f);
							this.dragElement.transform.position = vector2;
							this.dragBorderElement.transform.position = vector2;
						}
					}
				}
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x000244F8 File Offset: 0x000226F8
		protected override void ExecuteDefaultAction(EventBase evt)
		{
			base.ExecuteDefaultAction(evt);
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = evt.eventTypeId == EventBase<GeometryChangedEvent>.TypeId();
				if (flag2)
				{
					this.UpdateDragElementPosition((GeometryChangedEvent)evt);
				}
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0002453C File Offset: 0x0002273C
		private void UpdateTextFieldVisibility()
		{
			bool showInputField = this.showInputField;
			if (showInputField)
			{
				bool flag = this.inputTextField == null;
				if (flag)
				{
					this.inputTextField = new TextField
					{
						name = "unity-text-field"
					};
					this.inputTextField.AddToClassList(BaseSlider<TValueType>.textFieldClassName);
					this.inputTextField.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnTextFieldValueChange));
					this.inputTextField.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnTextFieldFocusOut), TrickleDown.NoTrickleDown);
					base.visualInput.Add(this.inputTextField);
					this.UpdateTextFieldValue();
				}
			}
			else
			{
				bool flag2 = this.inputTextField != null && this.inputTextField.panel != null;
				if (flag2)
				{
					bool flag3 = this.inputTextField.panel != null;
					if (flag3)
					{
						this.inputTextField.RemoveFromHierarchy();
					}
					this.inputTextField.UnregisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnTextFieldValueChange));
					this.inputTextField.UnregisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnTextFieldFocusOut), TrickleDown.NoTrickleDown);
					this.inputTextField = null;
				}
			}
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00024654 File Offset: 0x00022854
		private void UpdateTextFieldValue()
		{
			bool flag = this.inputTextField == null;
			if (!flag)
			{
				this.inputTextField.SetValueWithoutNotify(string.Format(CultureInfo.InvariantCulture, "{0:g7}", new object[] { this.value }));
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x000246A0 File Offset: 0x000228A0
		private void OnTextFieldFocusOut(FocusOutEvent evt)
		{
			this.UpdateTextFieldValue();
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x000246AC File Offset: 0x000228AC
		private void OnTextFieldValueChange(ChangeEvent<string> evt)
		{
			TValueType clampedValue = this.GetClampedValue(this.ParseStringToValue(evt.newValue));
			bool flag = !EqualityComparer<TValueType>.Default.Equals(clampedValue, this.value);
			if (flag)
			{
				this.value = clampedValue;
				evt.StopPropagation();
				bool flag2 = base.elementPanel != null;
				if (flag2)
				{
					this.OnViewDataReady();
				}
			}
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0002470C File Offset: 0x0002290C
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				VisualElement dragElement = this.dragElement;
				if (dragElement != null)
				{
					dragElement.RemoveFromHierarchy();
				}
			}
			else
			{
				this.dragContainer.Add(this.dragElement);
			}
		}

		// Token: 0x040003D2 RID: 978
		[SerializeField]
		private TValueType m_LowValue;

		// Token: 0x040003D3 RID: 979
		[SerializeField]
		private TValueType m_HighValue;

		// Token: 0x040003D4 RID: 980
		private float m_PageSize;

		// Token: 0x040003D5 RID: 981
		private bool m_ShowInputField = false;

		// Token: 0x040003D8 RID: 984
		private Rect m_DragElementStartPos;

		// Token: 0x040003D9 RID: 985
		private SliderDirection m_Direction;

		// Token: 0x040003DA RID: 986
		private bool m_Inverted = false;

		// Token: 0x040003DB RID: 987
		internal const float kDefaultPageSize = 0f;

		// Token: 0x040003DC RID: 988
		internal const bool kDefaultShowInputField = false;

		// Token: 0x040003DD RID: 989
		internal const bool kDefaultInverted = false;

		// Token: 0x040003DE RID: 990
		public new static readonly string ussClassName = "unity-base-slider";

		// Token: 0x040003DF RID: 991
		public new static readonly string labelUssClassName = BaseSlider<TValueType>.ussClassName + "__label";

		// Token: 0x040003E0 RID: 992
		public new static readonly string inputUssClassName = BaseSlider<TValueType>.ussClassName + "__input";

		// Token: 0x040003E1 RID: 993
		public static readonly string horizontalVariantUssClassName = BaseSlider<TValueType>.ussClassName + "--horizontal";

		// Token: 0x040003E2 RID: 994
		public static readonly string verticalVariantUssClassName = BaseSlider<TValueType>.ussClassName + "--vertical";

		// Token: 0x040003E3 RID: 995
		public static readonly string dragContainerUssClassName = BaseSlider<TValueType>.ussClassName + "__drag-container";

		// Token: 0x040003E4 RID: 996
		public static readonly string trackerUssClassName = BaseSlider<TValueType>.ussClassName + "__tracker";

		// Token: 0x040003E5 RID: 997
		public static readonly string draggerUssClassName = BaseSlider<TValueType>.ussClassName + "__dragger";

		// Token: 0x040003E6 RID: 998
		public static readonly string draggerBorderUssClassName = BaseSlider<TValueType>.ussClassName + "__dragger-border";

		// Token: 0x040003E7 RID: 999
		public static readonly string textFieldClassName = BaseSlider<TValueType>.ussClassName + "__text-field";

		// Token: 0x0200011D RID: 285
		internal enum SliderKey
		{
			// Token: 0x040003E9 RID: 1001
			None,
			// Token: 0x040003EA RID: 1002
			Lowest,
			// Token: 0x040003EB RID: 1003
			LowerPage,
			// Token: 0x040003EC RID: 1004
			Lower,
			// Token: 0x040003ED RID: 1005
			Higher,
			// Token: 0x040003EE RID: 1006
			HigherPage,
			// Token: 0x040003EF RID: 1007
			Highest
		}
	}
}
