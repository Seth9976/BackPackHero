using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnityEngine.UIElements
{
	// Token: 0x02000168 RID: 360
	public class Scroller : VisualElement
	{
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000B39 RID: 2873 RVA: 0x0002D16C File Offset: 0x0002B36C
		// (remove) Token: 0x06000B3A RID: 2874 RVA: 0x0002D1A4 File Offset: 0x0002B3A4
		[field: DebuggerBrowsable(0)]
		public event Action<float> valueChanged;

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0002D1D9 File Offset: 0x0002B3D9
		// (set) Token: 0x06000B3C RID: 2876 RVA: 0x0002D1E1 File Offset: 0x0002B3E1
		public Slider slider { get; private set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0002D1EA File Offset: 0x0002B3EA
		// (set) Token: 0x06000B3E RID: 2878 RVA: 0x0002D1F2 File Offset: 0x0002B3F2
		public RepeatButton lowButton { get; private set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0002D1FB File Offset: 0x0002B3FB
		// (set) Token: 0x06000B40 RID: 2880 RVA: 0x0002D203 File Offset: 0x0002B403
		public RepeatButton highButton { get; private set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0002D20C File Offset: 0x0002B40C
		// (set) Token: 0x06000B42 RID: 2882 RVA: 0x0002D229 File Offset: 0x0002B429
		public float value
		{
			get
			{
				return this.slider.value;
			}
			set
			{
				this.slider.value = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x0002D23C File Offset: 0x0002B43C
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x0002D259 File Offset: 0x0002B459
		public float lowValue
		{
			get
			{
				return this.slider.lowValue;
			}
			set
			{
				this.slider.lowValue = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0002D26C File Offset: 0x0002B46C
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x0002D289 File Offset: 0x0002B489
		public float highValue
		{
			get
			{
				return this.slider.highValue;
			}
			set
			{
				this.slider.highValue = value;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x0002D29C File Offset: 0x0002B49C
		// (set) Token: 0x06000B48 RID: 2888 RVA: 0x0002D2C0 File Offset: 0x0002B4C0
		public SliderDirection direction
		{
			get
			{
				return (base.resolvedStyle.flexDirection == FlexDirection.Row) ? SliderDirection.Horizontal : SliderDirection.Vertical;
			}
			set
			{
				this.slider.direction = value;
				bool flag = value == SliderDirection.Horizontal;
				if (flag)
				{
					base.style.flexDirection = FlexDirection.Row;
					base.AddToClassList(Scroller.horizontalVariantUssClassName);
					base.RemoveFromClassList(Scroller.verticalVariantUssClassName);
				}
				else
				{
					base.style.flexDirection = FlexDirection.Column;
					base.AddToClassList(Scroller.verticalVariantUssClassName);
					base.RemoveFromClassList(Scroller.horizontalVariantUssClassName);
				}
			}
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0002D33D File Offset: 0x0002B53D
		public Scroller()
			: this(0f, 0f, null, SliderDirection.Vertical)
		{
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0002D354 File Offset: 0x0002B554
		public Scroller(float lowValue, float highValue, Action<float> valueChanged, SliderDirection direction = SliderDirection.Vertical)
		{
			base.AddToClassList(Scroller.ussClassName);
			this.slider = new Slider(lowValue, highValue, direction, 20f)
			{
				name = "unity-slider",
				viewDataKey = "Slider"
			};
			this.slider.AddToClassList(Scroller.sliderUssClassName);
			this.slider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnSliderValueChange));
			this.slider.inverted = direction == SliderDirection.Vertical;
			this.lowButton = new RepeatButton(new Action(this.ScrollPageUp), 250L, 30L)
			{
				name = "unity-low-button"
			};
			this.lowButton.AddToClassList(Scroller.lowButtonUssClassName);
			base.Add(this.lowButton);
			this.highButton = new RepeatButton(new Action(this.ScrollPageDown), 250L, 30L)
			{
				name = "unity-high-button"
			};
			this.highButton.AddToClassList(Scroller.highButtonUssClassName);
			base.Add(this.highButton);
			base.Add(this.slider);
			this.direction = direction;
			this.valueChanged = valueChanged;
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0002D48F File Offset: 0x0002B68F
		public void Adjust(float factor)
		{
			base.SetEnabled(factor < 1f);
			this.slider.AdjustDragElement(factor);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0002D4AE File Offset: 0x0002B6AE
		private void OnSliderValueChange(ChangeEvent<float> evt)
		{
			this.value = evt.newValue;
			Action<float> action = this.valueChanged;
			if (action != null)
			{
				action.Invoke(this.slider.value);
			}
			base.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0002D4E7 File Offset: 0x0002B6E7
		public void ScrollPageUp()
		{
			this.ScrollPageUp(1f);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0002D4F6 File Offset: 0x0002B6F6
		public void ScrollPageDown()
		{
			this.ScrollPageDown(1f);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002D508 File Offset: 0x0002B708
		public void ScrollPageUp(float factor)
		{
			this.value -= factor * (this.slider.pageSize * ((this.slider.lowValue < this.slider.highValue) ? 1f : (-1f)));
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0002D558 File Offset: 0x0002B758
		public void ScrollPageDown(float factor)
		{
			this.value += factor * (this.slider.pageSize * ((this.slider.lowValue < this.slider.highValue) ? 1f : (-1f)));
		}

		// Token: 0x04000519 RID: 1305
		internal const float kDefaultPageSize = 20f;

		// Token: 0x0400051A RID: 1306
		public static readonly string ussClassName = "unity-scroller";

		// Token: 0x0400051B RID: 1307
		public static readonly string horizontalVariantUssClassName = Scroller.ussClassName + "--horizontal";

		// Token: 0x0400051C RID: 1308
		public static readonly string verticalVariantUssClassName = Scroller.ussClassName + "--vertical";

		// Token: 0x0400051D RID: 1309
		public static readonly string sliderUssClassName = Scroller.ussClassName + "__slider";

		// Token: 0x0400051E RID: 1310
		public static readonly string lowButtonUssClassName = Scroller.ussClassName + "__low-button";

		// Token: 0x0400051F RID: 1311
		public static readonly string highButtonUssClassName = Scroller.ussClassName + "__high-button";

		// Token: 0x02000169 RID: 361
		public new class UxmlFactory : UxmlFactory<Scroller, Scroller.UxmlTraits>
		{
		}

		// Token: 0x0200016A RID: 362
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x17000233 RID: 563
			// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0002D62C File Offset: 0x0002B82C
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x06000B54 RID: 2900 RVA: 0x0002D64C File Offset: 0x0002B84C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Scroller scroller = (Scroller)ve;
				scroller.slider.lowValue = this.m_LowValue.GetValueFromBag(bag, cc);
				scroller.slider.highValue = this.m_HighValue.GetValueFromBag(bag, cc);
				scroller.direction = this.m_Direction.GetValueFromBag(bag, cc);
				scroller.value = this.m_Value.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000520 RID: 1312
			private UxmlFloatAttributeDescription m_LowValue = new UxmlFloatAttributeDescription
			{
				name = "low-value",
				obsoleteNames = new string[] { "lowValue" }
			};

			// Token: 0x04000521 RID: 1313
			private UxmlFloatAttributeDescription m_HighValue = new UxmlFloatAttributeDescription
			{
				name = "high-value",
				obsoleteNames = new string[] { "highValue" }
			};

			// Token: 0x04000522 RID: 1314
			private UxmlEnumAttributeDescription<SliderDirection> m_Direction = new UxmlEnumAttributeDescription<SliderDirection>
			{
				name = "direction",
				defaultValue = SliderDirection.Vertical
			};

			// Token: 0x04000523 RID: 1315
			private UxmlFloatAttributeDescription m_Value = new UxmlFloatAttributeDescription
			{
				name = "value"
			};
		}
	}
}
