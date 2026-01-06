using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x02000174 RID: 372
	public class Slider : BaseSlider<float>
	{
		// Token: 0x06000BB0 RID: 2992 RVA: 0x00030028 File Offset: 0x0002E228
		public Slider()
			: this(null, 0f, 10f, SliderDirection.Horizontal, 0f)
		{
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00030043 File Offset: 0x0002E243
		public Slider(float start, float end, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f)
			: this(null, start, end, direction, pageSize)
		{
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00030053 File Offset: 0x0002E253
		public Slider(string label, float start = 0f, float end = 10f, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f)
			: base(label, start, end, direction, pageSize)
		{
			base.AddToClassList(Slider.ussClassName);
			base.labelElement.AddToClassList(Slider.labelUssClassName);
			base.visualInput.AddToClassList(Slider.inputUssClassName);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00030094 File Offset: 0x0002E294
		internal override float SliderLerpUnclamped(float a, float b, float interpolant)
		{
			float num = Mathf.LerpUnclamped(a, b, interpolant);
			float num2 = Mathf.Abs((base.highValue - base.lowValue) / (base.dragContainer.resolvedStyle.width - base.dragElement.resolvedStyle.width));
			int num3 = ((num2 == 0f) ? Mathf.Clamp((int)(5.0 - (double)Mathf.Log10(Mathf.Abs(num2))), 0, 15) : Mathf.Clamp(-Mathf.FloorToInt(Mathf.Log10(Mathf.Abs(num2))), 0, 15));
			return (float)Math.Round((double)num, num3, 1);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00030138 File Offset: 0x0002E338
		internal override float SliderNormalizeValue(float currentValue, float lowerValue, float higherValue)
		{
			return (currentValue - lowerValue) / (higherValue - lowerValue);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00030154 File Offset: 0x0002E354
		internal override float SliderRange()
		{
			return Math.Abs(base.highValue - base.lowValue);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00030178 File Offset: 0x0002E378
		internal override float ParseStringToValue(string stringValue)
		{
			float num;
			bool flag = float.TryParse(stringValue.Replace(",", "."), 167, CultureInfo.InvariantCulture, ref num);
			float num2;
			if (flag)
			{
				num2 = num;
			}
			else
			{
				num2 = 0f;
			}
			return num2;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000301BC File Offset: 0x0002E3BC
		internal override void ComputeValueFromKey(BaseSlider<float>.SliderKey sliderKey, bool isShift)
		{
			if (sliderKey != BaseSlider<float>.SliderKey.None)
			{
				if (sliderKey != BaseSlider<float>.SliderKey.Lowest)
				{
					if (sliderKey != BaseSlider<float>.SliderKey.Highest)
					{
						bool flag = sliderKey == BaseSlider<float>.SliderKey.LowerPage || sliderKey == BaseSlider<float>.SliderKey.HigherPage;
						float num = BaseSlider<float>.GetClosestPowerOfTen(Mathf.Abs((base.highValue - base.lowValue) * 0.01f));
						bool flag2 = flag;
						if (flag2)
						{
							num *= this.pageSize;
						}
						else if (isShift)
						{
							num *= 10f;
						}
						bool flag3 = sliderKey == BaseSlider<float>.SliderKey.Lower || sliderKey == BaseSlider<float>.SliderKey.LowerPage;
						if (flag3)
						{
							num = -num;
						}
						this.value = BaseSlider<float>.RoundToMultipleOf(this.value + num * 0.5001f, Mathf.Abs(num));
					}
					else
					{
						this.value = base.highValue;
					}
				}
				else
				{
					this.value = base.lowValue;
				}
			}
		}

		// Token: 0x0400057C RID: 1404
		internal const float kDefaultHighValue = 10f;

		// Token: 0x0400057D RID: 1405
		public new static readonly string ussClassName = "unity-slider";

		// Token: 0x0400057E RID: 1406
		public new static readonly string labelUssClassName = Slider.ussClassName + "__label";

		// Token: 0x0400057F RID: 1407
		public new static readonly string inputUssClassName = Slider.ussClassName + "__input";

		// Token: 0x02000175 RID: 373
		public new class UxmlFactory : UxmlFactory<Slider, Slider.UxmlTraits>
		{
		}

		// Token: 0x02000176 RID: 374
		public new class UxmlTraits : BaseFieldTraits<float, UxmlFloatAttributeDescription>
		{
			// Token: 0x06000BBA RID: 3002 RVA: 0x000302C4 File Offset: 0x0002E4C4
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				Slider slider = (Slider)ve;
				slider.lowValue = this.m_LowValue.GetValueFromBag(bag, cc);
				slider.highValue = this.m_HighValue.GetValueFromBag(bag, cc);
				slider.direction = this.m_Direction.GetValueFromBag(bag, cc);
				slider.pageSize = this.m_PageSize.GetValueFromBag(bag, cc);
				slider.showInputField = this.m_ShowInputField.GetValueFromBag(bag, cc);
				slider.inverted = this.m_Inverted.GetValueFromBag(bag, cc);
				base.Init(ve, bag, cc);
			}

			// Token: 0x04000580 RID: 1408
			private UxmlFloatAttributeDescription m_LowValue = new UxmlFloatAttributeDescription
			{
				name = "low-value"
			};

			// Token: 0x04000581 RID: 1409
			private UxmlFloatAttributeDescription m_HighValue = new UxmlFloatAttributeDescription
			{
				name = "high-value",
				defaultValue = 10f
			};

			// Token: 0x04000582 RID: 1410
			private UxmlFloatAttributeDescription m_PageSize = new UxmlFloatAttributeDescription
			{
				name = "page-size",
				defaultValue = 0f
			};

			// Token: 0x04000583 RID: 1411
			private UxmlBoolAttributeDescription m_ShowInputField = new UxmlBoolAttributeDescription
			{
				name = "show-input-field",
				defaultValue = false
			};

			// Token: 0x04000584 RID: 1412
			private UxmlEnumAttributeDescription<SliderDirection> m_Direction = new UxmlEnumAttributeDescription<SliderDirection>
			{
				name = "direction",
				defaultValue = SliderDirection.Horizontal
			};

			// Token: 0x04000585 RID: 1413
			private UxmlBoolAttributeDescription m_Inverted = new UxmlBoolAttributeDescription
			{
				name = "inverted",
				defaultValue = false
			};
		}
	}
}
