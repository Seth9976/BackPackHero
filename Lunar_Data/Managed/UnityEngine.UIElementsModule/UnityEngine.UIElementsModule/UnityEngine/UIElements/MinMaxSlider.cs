using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000153 RID: 339
	public class MinMaxSlider : BaseField<Vector2>
	{
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0002B52C File Offset: 0x0002972C
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x0002B534 File Offset: 0x00029734
		internal VisualElement dragElement { get; private set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0002B53D File Offset: 0x0002973D
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x0002B545 File Offset: 0x00029745
		internal VisualElement dragMinThumb { get; private set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0002B54E File Offset: 0x0002974E
		// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x0002B556 File Offset: 0x00029756
		internal VisualElement dragMaxThumb { get; private set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0002B55F File Offset: 0x0002975F
		// (set) Token: 0x06000AD9 RID: 2777 RVA: 0x0002B567 File Offset: 0x00029767
		internal ClampedDragger<float> clampedDragger { get; private set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0002B570 File Offset: 0x00029770
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x0002B58D File Offset: 0x0002978D
		public float minValue
		{
			get
			{
				return this.value.x;
			}
			set
			{
				base.value = this.ClampValues(new Vector2(value, base.rawValue.y));
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0002B5B0 File Offset: 0x000297B0
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x0002B5CD File Offset: 0x000297CD
		public float maxValue
		{
			get
			{
				return this.value.y;
			}
			set
			{
				base.value = this.ClampValues(new Vector2(base.rawValue.x, value));
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0002B5F0 File Offset: 0x000297F0
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x0002B608 File Offset: 0x00029808
		public override Vector2 value
		{
			get
			{
				return base.value;
			}
			set
			{
				base.value = this.ClampValues(value);
			}
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002B619 File Offset: 0x00029819
		public override void SetValueWithoutNotify(Vector2 newValue)
		{
			base.SetValueWithoutNotify(this.ClampValues(newValue));
			this.UpdateDragElementPosition();
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0002B634 File Offset: 0x00029834
		public float range
		{
			get
			{
				return Math.Abs(this.highLimit - this.lowLimit);
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0002B658 File Offset: 0x00029858
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x0002B670 File Offset: 0x00029870
		public float lowLimit
		{
			get
			{
				return this.m_MinLimit;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_MinLimit, value);
				if (flag)
				{
					bool flag2 = value > this.m_MaxLimit;
					if (flag2)
					{
						throw new ArgumentException("lowLimit is greater than highLimit");
					}
					this.m_MinLimit = value;
					this.value = base.rawValue;
					this.UpdateDragElementPosition();
					bool flag3 = !string.IsNullOrEmpty(base.viewDataKey);
					if (flag3)
					{
						base.SaveViewData();
					}
				}
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0002B6E0 File Offset: 0x000298E0
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x0002B6F8 File Offset: 0x000298F8
		public float highLimit
		{
			get
			{
				return this.m_MaxLimit;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_MaxLimit, value);
				if (flag)
				{
					bool flag2 = value < this.m_MinLimit;
					if (flag2)
					{
						throw new ArgumentException("highLimit is smaller than lowLimit");
					}
					this.m_MaxLimit = value;
					this.value = base.rawValue;
					this.UpdateDragElementPosition();
					bool flag3 = !string.IsNullOrEmpty(base.viewDataKey);
					if (flag3)
					{
						base.SaveViewData();
					}
				}
			}
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002B768 File Offset: 0x00029968
		public MinMaxSlider()
			: this(null, 0f, 10f, float.MinValue, float.MaxValue)
		{
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002B787 File Offset: 0x00029987
		public MinMaxSlider(float minValue, float maxValue, float minLimit, float maxLimit)
			: this(null, minValue, maxValue, minLimit, maxLimit)
		{
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002B798 File Offset: 0x00029998
		public MinMaxSlider(string label, float minValue = 0f, float maxValue = 10f, float minLimit = -3.4028235E+38f, float maxLimit = 3.4028235E+38f)
			: base(label, null)
		{
			this.lowLimit = minLimit;
			this.highLimit = maxLimit;
			this.minValue = minValue;
			this.maxValue = maxValue;
			base.AddToClassList(MinMaxSlider.ussClassName);
			base.labelElement.AddToClassList(MinMaxSlider.labelUssClassName);
			base.visualInput.AddToClassList(MinMaxSlider.inputUssClassName);
			base.pickingMode = PickingMode.Ignore;
			this.m_DragState = MinMaxSlider.DragState.NoThumb;
			base.visualInput.pickingMode = PickingMode.Position;
			VisualElement visualElement = new VisualElement
			{
				name = "unity-tracker"
			};
			visualElement.AddToClassList(MinMaxSlider.trackerUssClassName);
			base.visualInput.Add(visualElement);
			this.dragElement = new VisualElement
			{
				name = "unity-dragger"
			};
			this.dragElement.AddToClassList(MinMaxSlider.draggerUssClassName);
			this.dragElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.UpdateDragElementPosition), TrickleDown.NoTrickleDown);
			base.visualInput.Add(this.dragElement);
			this.dragMinThumb = new VisualElement
			{
				name = "unity-thumb-min"
			};
			this.dragMaxThumb = new VisualElement
			{
				name = "unity-thumb-max"
			};
			this.dragMinThumb.AddToClassList(MinMaxSlider.minThumbUssClassName);
			this.dragMaxThumb.AddToClassList(MinMaxSlider.maxThumbUssClassName);
			this.dragElement.Add(this.dragMinThumb);
			this.dragElement.Add(this.dragMaxThumb);
			this.clampedDragger = new ClampedDragger<float>(null, new Action(this.SetSliderValueFromClick), new Action(this.SetSliderValueFromDrag));
			base.visualInput.AddManipulator(this.clampedDragger);
			this.m_MinLimit = minLimit;
			this.m_MaxLimit = maxLimit;
			base.rawValue = this.ClampValues(new Vector2(minValue, maxValue));
			this.UpdateDragElementPosition();
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002B974 File Offset: 0x00029B74
		private Vector2 ClampValues(Vector2 valueToClamp)
		{
			bool flag = this.m_MinLimit > this.m_MaxLimit;
			if (flag)
			{
				this.m_MinLimit = this.m_MaxLimit;
			}
			Vector2 vector = default(Vector2);
			bool flag2 = valueToClamp.y > this.m_MaxLimit;
			if (flag2)
			{
				valueToClamp.y = this.m_MaxLimit;
			}
			vector.x = Mathf.Clamp(valueToClamp.x, this.m_MinLimit, valueToClamp.y);
			vector.y = Mathf.Clamp(valueToClamp.y, valueToClamp.x, this.m_MaxLimit);
			return vector;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0002BA0C File Offset: 0x00029C0C
		private void UpdateDragElementPosition(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				this.UpdateDragElementPosition();
			}
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0002BA4C File Offset: 0x00029C4C
		private void UpdateDragElementPosition()
		{
			bool flag = base.panel == null;
			if (!flag)
			{
				float num = -this.dragElement.resolvedStyle.marginLeft - this.dragElement.resolvedStyle.marginRight;
				int num2 = this.dragElement.resolvedStyle.unitySliceLeft + this.dragElement.resolvedStyle.unitySliceRight;
				float num3 = Mathf.Round(this.SliderLerpUnclamped((float)this.dragElement.resolvedStyle.unitySliceLeft, base.visualInput.layout.width + num - (float)this.dragElement.resolvedStyle.unitySliceRight, this.SliderNormalizeValue(this.minValue, this.lowLimit, this.highLimit)) - (float)this.dragElement.resolvedStyle.unitySliceLeft);
				float num4 = Mathf.Round(this.SliderLerpUnclamped((float)this.dragElement.resolvedStyle.unitySliceLeft, base.visualInput.layout.width + num - (float)this.dragElement.resolvedStyle.unitySliceRight, this.SliderNormalizeValue(this.maxValue, this.lowLimit, this.highLimit)) + (float)this.dragElement.resolvedStyle.unitySliceRight);
				this.dragElement.style.width = Mathf.Max((float)num2, num4 - num3);
				this.dragElement.style.left = num3;
				float left = this.dragElement.resolvedStyle.left;
				float num5 = this.dragElement.resolvedStyle.left + (this.dragElement.resolvedStyle.width - (float)this.dragElement.resolvedStyle.unitySliceRight);
				float num6 = this.dragElement.layout.yMin + this.dragMinThumb.resolvedStyle.marginTop;
				float num7 = this.dragElement.layout.yMin + this.dragMaxThumb.resolvedStyle.marginTop;
				float num8 = Mathf.Max(this.dragElement.resolvedStyle.height, this.dragMinThumb.resolvedStyle.height);
				float num9 = Mathf.Max(this.dragElement.resolvedStyle.height, this.dragMaxThumb.resolvedStyle.height);
				this.m_DragMinThumbRect = new Rect(left, num6, (float)this.dragElement.resolvedStyle.unitySliceLeft, num8);
				this.m_DragMaxThumbRect = new Rect(num5, num7, (float)this.dragElement.resolvedStyle.unitySliceRight, num9);
				this.dragMaxThumb.style.left = this.dragElement.resolvedStyle.width - (float)this.dragElement.resolvedStyle.unitySliceRight;
				this.dragMaxThumb.style.top = 0f;
				this.dragMinThumb.style.width = this.m_DragMinThumbRect.width;
				this.dragMinThumb.style.height = this.m_DragMinThumbRect.height;
				this.dragMinThumb.style.left = 0f;
				this.dragMinThumb.style.top = 0f;
				this.dragMaxThumb.style.width = this.m_DragMaxThumbRect.width;
				this.dragMaxThumb.style.height = this.m_DragMaxThumbRect.height;
			}
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0002BDFC File Offset: 0x00029FFC
		internal float SliderLerpUnclamped(float a, float b, float interpolant)
		{
			return Mathf.LerpUnclamped(a, b, interpolant);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002BE18 File Offset: 0x0002A018
		internal float SliderNormalizeValue(float currentValue, float lowerValue, float higherValue)
		{
			return (currentValue - lowerValue) / (higherValue - lowerValue);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0002BE34 File Offset: 0x0002A034
		private float ComputeValueFromPosition(float positionToConvert)
		{
			float num = this.SliderNormalizeValue(positionToConvert, (float)this.dragElement.resolvedStyle.unitySliceLeft, base.visualInput.layout.width - (float)this.dragElement.resolvedStyle.unitySliceRight);
			return this.SliderLerpUnclamped(this.lowLimit, this.highLimit, num);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0002BEA0 File Offset: 0x0002A0A0
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

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002BEE4 File Offset: 0x0002A0E4
		private void SetSliderValueFromDrag()
		{
			bool flag = this.clampedDragger.dragDirection != ClampedDragger<float>.DragDirection.Free;
			if (!flag)
			{
				float x = this.m_DragElementStartPos.x;
				float num = x + this.clampedDragger.delta.x;
				this.ComputeValueFromDraggingThumb(x, num);
			}
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002BF34 File Offset: 0x0002A134
		private void SetSliderValueFromClick()
		{
			bool flag = this.clampedDragger.dragDirection == ClampedDragger<float>.DragDirection.Free;
			if (!flag)
			{
				bool flag2 = this.m_DragMinThumbRect.Contains(this.clampedDragger.startMousePosition);
				if (flag2)
				{
					this.m_DragState = MinMaxSlider.DragState.MinThumb;
				}
				else
				{
					bool flag3 = this.m_DragMaxThumbRect.Contains(this.clampedDragger.startMousePosition);
					if (flag3)
					{
						this.m_DragState = MinMaxSlider.DragState.MaxThumb;
					}
					else
					{
						bool flag4 = this.dragElement.layout.Contains(this.clampedDragger.startMousePosition);
						if (flag4)
						{
							this.m_DragState = MinMaxSlider.DragState.MiddleThumb;
						}
						else
						{
							this.m_DragState = MinMaxSlider.DragState.NoThumb;
						}
					}
				}
				bool flag5 = this.m_DragState == MinMaxSlider.DragState.NoThumb;
				if (flag5)
				{
					this.m_DragElementStartPos = new Vector2(this.clampedDragger.startMousePosition.x, this.dragElement.resolvedStyle.top);
					this.clampedDragger.dragDirection = ClampedDragger<float>.DragDirection.Free;
					this.ComputeValueDragStateNoThumb((float)this.dragElement.resolvedStyle.unitySliceLeft, base.visualInput.layout.width - (float)this.dragElement.resolvedStyle.unitySliceRight, this.m_DragElementStartPos.x);
					this.m_DragState = MinMaxSlider.DragState.MiddleThumb;
					this.m_ValueStartPos = this.value;
				}
				else
				{
					this.m_ValueStartPos = this.value;
					this.clampedDragger.dragDirection = ClampedDragger<float>.DragDirection.Free;
					this.m_DragElementStartPos = this.clampedDragger.startMousePosition;
				}
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002C0B0 File Offset: 0x0002A2B0
		private void ComputeValueDragStateNoThumb(float lowLimitPosition, float highLimitPosition, float dragElementPos)
		{
			bool flag = dragElementPos < lowLimitPosition;
			float num;
			if (flag)
			{
				num = this.lowLimit;
			}
			else
			{
				bool flag2 = dragElementPos > highLimitPosition;
				if (flag2)
				{
					num = this.highLimit;
				}
				else
				{
					num = this.ComputeValueFromPosition(dragElementPos);
				}
			}
			float num2 = this.maxValue - this.minValue;
			float num3 = num - num2;
			float num4 = num;
			bool flag3 = num3 < this.lowLimit;
			if (flag3)
			{
				num3 = this.lowLimit;
				num4 = num3 + num2;
			}
			this.value = new Vector2(num3, num4);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002C130 File Offset: 0x0002A330
		private void ComputeValueFromDraggingThumb(float dragElementStartPos, float dragElementEndPos)
		{
			float num = this.ComputeValueFromPosition(dragElementStartPos);
			float num2 = this.ComputeValueFromPosition(dragElementEndPos);
			float num3 = num2 - num;
			switch (this.m_DragState)
			{
			case MinMaxSlider.DragState.MinThumb:
			{
				float num4 = this.m_ValueStartPos.x + num3;
				bool flag = num4 > this.maxValue;
				if (flag)
				{
					num4 = this.maxValue;
				}
				else
				{
					bool flag2 = num4 < this.lowLimit;
					if (flag2)
					{
						num4 = this.lowLimit;
					}
				}
				this.value = new Vector2(num4, this.maxValue);
				break;
			}
			case MinMaxSlider.DragState.MiddleThumb:
			{
				Vector2 value = this.value;
				value.x = this.m_ValueStartPos.x + num3;
				value.y = this.m_ValueStartPos.y + num3;
				float num5 = this.m_ValueStartPos.y - this.m_ValueStartPos.x;
				bool flag3 = value.x < this.lowLimit;
				if (flag3)
				{
					value.x = this.lowLimit;
					value.y = this.lowLimit + num5;
				}
				else
				{
					bool flag4 = value.y > this.highLimit;
					if (flag4)
					{
						value.y = this.highLimit;
						value.x = this.highLimit - num5;
					}
				}
				this.value = value;
				break;
			}
			case MinMaxSlider.DragState.MaxThumb:
			{
				float num6 = this.m_ValueStartPos.y + num3;
				bool flag5 = num6 < this.minValue;
				if (flag5)
				{
					num6 = this.minValue;
				}
				else
				{
					bool flag6 = num6 > this.highLimit;
					if (flag6)
					{
						num6 = this.highLimit;
					}
				}
				this.value = new Vector2(this.minValue, num6);
				break;
			}
			}
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x000020E6 File Offset: 0x000002E6
		protected override void UpdateMixedValueContent()
		{
		}

		// Token: 0x040004D4 RID: 1236
		private Vector2 m_DragElementStartPos;

		// Token: 0x040004D5 RID: 1237
		private Vector2 m_ValueStartPos;

		// Token: 0x040004D6 RID: 1238
		private Rect m_DragMinThumbRect;

		// Token: 0x040004D7 RID: 1239
		private Rect m_DragMaxThumbRect;

		// Token: 0x040004D8 RID: 1240
		private MinMaxSlider.DragState m_DragState;

		// Token: 0x040004D9 RID: 1241
		private float m_MinLimit;

		// Token: 0x040004DA RID: 1242
		private float m_MaxLimit;

		// Token: 0x040004DB RID: 1243
		internal const float kDefaultHighValue = 10f;

		// Token: 0x040004DC RID: 1244
		public new static readonly string ussClassName = "unity-min-max-slider";

		// Token: 0x040004DD RID: 1245
		public new static readonly string labelUssClassName = MinMaxSlider.ussClassName + "__label";

		// Token: 0x040004DE RID: 1246
		public new static readonly string inputUssClassName = MinMaxSlider.ussClassName + "__input";

		// Token: 0x040004DF RID: 1247
		public static readonly string trackerUssClassName = MinMaxSlider.ussClassName + "__tracker";

		// Token: 0x040004E0 RID: 1248
		public static readonly string draggerUssClassName = MinMaxSlider.ussClassName + "__dragger";

		// Token: 0x040004E1 RID: 1249
		public static readonly string minThumbUssClassName = MinMaxSlider.ussClassName + "__min-thumb";

		// Token: 0x040004E2 RID: 1250
		public static readonly string maxThumbUssClassName = MinMaxSlider.ussClassName + "__max-thumb";

		// Token: 0x02000154 RID: 340
		public new class UxmlFactory : UxmlFactory<MinMaxSlider, MinMaxSlider.UxmlTraits>
		{
		}

		// Token: 0x02000155 RID: 341
		public new class UxmlTraits : BaseField<Vector2>.UxmlTraits
		{
			// Token: 0x06000AF7 RID: 2807 RVA: 0x0002C388 File Offset: 0x0002A588
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				MinMaxSlider minMaxSlider = (MinMaxSlider)ve;
				minMaxSlider.minValue = this.m_MinValue.GetValueFromBag(bag, cc);
				minMaxSlider.maxValue = this.m_MaxValue.GetValueFromBag(bag, cc);
				minMaxSlider.lowLimit = this.m_LowLimit.GetValueFromBag(bag, cc);
				minMaxSlider.highLimit = this.m_HighLimit.GetValueFromBag(bag, cc);
			}

			// Token: 0x040004E3 RID: 1251
			private UxmlFloatAttributeDescription m_MinValue = new UxmlFloatAttributeDescription
			{
				name = "min-value",
				defaultValue = 0f
			};

			// Token: 0x040004E4 RID: 1252
			private UxmlFloatAttributeDescription m_MaxValue = new UxmlFloatAttributeDescription
			{
				name = "max-value",
				defaultValue = 10f
			};

			// Token: 0x040004E5 RID: 1253
			private UxmlFloatAttributeDescription m_LowLimit = new UxmlFloatAttributeDescription
			{
				name = "low-limit",
				defaultValue = float.MinValue
			};

			// Token: 0x040004E6 RID: 1254
			private UxmlFloatAttributeDescription m_HighLimit = new UxmlFloatAttributeDescription
			{
				name = "high-limit",
				defaultValue = float.MaxValue
			};
		}

		// Token: 0x02000156 RID: 342
		private enum DragState
		{
			// Token: 0x040004E8 RID: 1256
			NoThumb,
			// Token: 0x040004E9 RID: 1257
			MinThumb,
			// Token: 0x040004EA RID: 1258
			MiddleThumb,
			// Token: 0x040004EB RID: 1259
			MaxThumb
		}
	}
}
