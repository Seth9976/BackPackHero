using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000118 RID: 280
	public abstract class BaseField<TValueType> : BindableElement, INotifyValueChanged<TValueType>, IMixedValueSupport, IPrefixLabel
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00022AA0 File Offset: 0x00020CA0
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x00022AB8 File Offset: 0x00020CB8
		internal VisualElement visualInput
		{
			get
			{
				return this.m_VisualInput;
			}
			set
			{
				bool flag = this.m_VisualInput != null;
				if (flag)
				{
					bool flag2 = this.m_VisualInput.parent == this;
					if (flag2)
					{
						this.m_VisualInput.RemoveFromHierarchy();
					}
					this.m_VisualInput = null;
				}
				bool flag3 = value != null;
				if (flag3)
				{
					this.m_VisualInput = value;
				}
				else
				{
					this.m_VisualInput = new VisualElement
					{
						pickingMode = PickingMode.Ignore
					};
				}
				this.m_VisualInput.focusable = true;
				this.m_VisualInput.AddToClassList(BaseField<TValueType>.inputUssClassName);
				base.Add(this.m_VisualInput);
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00022B50 File Offset: 0x00020D50
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x00022B68 File Offset: 0x00020D68
		protected TValueType rawValue
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00022B74 File Offset: 0x00020D74
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x00022B8C File Offset: 0x00020D8C
		public virtual TValueType value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				bool flag = !EqualityComparer<TValueType>.Default.Equals(this.m_Value, value);
				if (flag)
				{
					bool flag2 = base.panel != null;
					if (flag2)
					{
						using (ChangeEvent<TValueType> pooled = ChangeEvent<TValueType>.GetPooled(this.m_Value, value))
						{
							pooled.target = this;
							this.SetValueWithoutNotify(value);
							this.SendEvent(pooled);
						}
					}
					else
					{
						this.SetValueWithoutNotify(value);
					}
				}
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00022C14 File Offset: 0x00020E14
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x00022C1C File Offset: 0x00020E1C
		public Label labelElement { get; private set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00022C28 File Offset: 0x00020E28
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x00022C48 File Offset: 0x00020E48
		public string label
		{
			get
			{
				return this.labelElement.text;
			}
			set
			{
				bool flag = this.labelElement.text != value;
				if (flag)
				{
					this.labelElement.text = value;
					bool flag2 = string.IsNullOrEmpty(this.labelElement.text);
					if (flag2)
					{
						base.AddToClassList(BaseField<TValueType>.noLabelVariantUssClassName);
						this.labelElement.RemoveFromHierarchy();
					}
					else
					{
						bool flag3 = !base.Contains(this.labelElement);
						if (flag3)
						{
							base.Insert(0, this.labelElement);
							base.RemoveFromClassList(BaseField<TValueType>.noLabelVariantUssClassName);
						}
					}
				}
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00022CDB File Offset: 0x00020EDB
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x00022CE4 File Offset: 0x00020EE4
		public bool showMixedValue
		{
			get
			{
				return this.m_ShowMixedValue;
			}
			set
			{
				bool flag = value == this.m_ShowMixedValue;
				if (!flag)
				{
					this.m_ShowMixedValue = value;
					this.UpdateMixedValueContent();
				}
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x00022D10 File Offset: 0x00020F10
		protected Label mixedValueLabel
		{
			get
			{
				bool flag = this.m_MixedValueLabel == null;
				if (flag)
				{
					this.m_MixedValueLabel = new Label(BaseField<TValueType>.mixedValueString)
					{
						focusable = true,
						tabIndex = -1
					};
					this.m_MixedValueLabel.AddToClassList(BaseField<TValueType>.labelUssClassName);
					this.m_MixedValueLabel.AddToClassList(BaseField<TValueType>.mixedValueLabelUssClassName);
				}
				return this.m_MixedValueLabel;
			}
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00022D7C File Offset: 0x00020F7C
		internal BaseField(string label)
		{
			base.isCompositeRoot = true;
			base.focusable = true;
			base.tabIndex = 0;
			base.excludeFromFocusRing = true;
			base.delegatesFocus = true;
			base.AddToClassList(BaseField<TValueType>.ussClassName);
			this.labelElement = new Label
			{
				focusable = true,
				tabIndex = -1
			};
			this.labelElement.AddToClassList(BaseField<TValueType>.labelUssClassName);
			bool flag = label != null;
			if (flag)
			{
				this.label = label;
			}
			else
			{
				base.AddToClassList(BaseField<TValueType>.noLabelVariantUssClassName);
			}
			base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			this.m_VisualInput = null;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00022E2F File Offset: 0x0002102F
		protected BaseField(string label, VisualElement visualInput)
			: this(label)
		{
			this.visualInput = visualInput;
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00022E44 File Offset: 0x00021044
		private void OnAttachToPanel(AttachToPanelEvent e)
		{
			for (VisualElement visualElement = base.parent; visualElement != null; visualElement = visualElement.parent)
			{
				bool flag = visualElement.ClassListContains("unity-inspector-element");
				if (flag)
				{
					this.m_LabelWidthRatio = 0.45f;
					this.m_LabelExtraPadding = 2f;
					this.m_LabelBaseMinWidth = 120f;
					base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
					base.AddToClassList(BaseField<TValueType>.inspectorFieldUssClassName);
					this.m_CachedInspectorElement = visualElement;
					this.m_CachedListAndFoldoutDepth = this.GetListAndFoldoutDepth();
					base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnInspectorFieldGeometryChanged), TrickleDown.NoTrickleDown);
					break;
				}
			}
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00022EEC File Offset: 0x000210EC
		private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
		{
			float num;
			bool flag = evt.customStyle.TryGetValue(BaseField<TValueType>.s_LabelWidthRatioProperty, out num);
			if (flag)
			{
				this.m_LabelWidthRatio = num;
			}
			float num2;
			bool flag2 = evt.customStyle.TryGetValue(BaseField<TValueType>.s_LabelExtraPaddingProperty, out num2);
			if (flag2)
			{
				this.m_LabelExtraPadding = num2;
			}
			float num3;
			bool flag3 = evt.customStyle.TryGetValue(BaseField<TValueType>.s_LabelBaseMinWidthProperty, out num3);
			if (flag3)
			{
				this.m_LabelBaseMinWidth = num3;
			}
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00022F5B File Offset: 0x0002115B
		private void OnInspectorFieldGeometryChanged(GeometryChangedEvent e)
		{
			this.AlignLabel();
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00022F68 File Offset: 0x00021168
		private void AlignLabel()
		{
			bool flag = !base.ClassListContains(BaseField<TValueType>.alignedFieldUssClassName);
			if (!flag)
			{
				int num = 15 * this.m_CachedListAndFoldoutDepth;
				float num2 = base.resolvedStyle.paddingLeft + base.resolvedStyle.paddingRight + base.resolvedStyle.marginLeft + base.resolvedStyle.marginRight;
				num2 += this.m_CachedInspectorElement.resolvedStyle.paddingLeft + this.m_CachedInspectorElement.resolvedStyle.paddingRight + this.m_CachedInspectorElement.resolvedStyle.marginLeft + this.m_CachedInspectorElement.resolvedStyle.marginRight;
				num2 += this.labelElement.resolvedStyle.paddingLeft + this.labelElement.resolvedStyle.paddingRight + this.labelElement.resolvedStyle.marginLeft + this.labelElement.resolvedStyle.marginRight;
				num2 += base.resolvedStyle.paddingLeft + base.resolvedStyle.paddingRight + base.resolvedStyle.marginLeft + base.resolvedStyle.marginRight;
				num2 += this.m_LabelExtraPadding;
				num2 += (float)num;
				this.labelElement.style.minWidth = Mathf.Max(this.m_LabelBaseMinWidth - (float)num, 0f);
				float num3 = this.m_CachedInspectorElement.resolvedStyle.width * this.m_LabelWidthRatio - num2;
				bool flag2 = Mathf.Abs(this.labelElement.resolvedStyle.width - num3) > 1E-30f;
				if (flag2)
				{
					this.labelElement.style.width = Mathf.Max(0f, num3);
				}
			}
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0002311E File Offset: 0x0002131E
		protected virtual void UpdateMixedValueContent()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00023128 File Offset: 0x00021328
		public virtual void SetValueWithoutNotify(TValueType newValue)
		{
			this.m_Value = newValue;
			bool flag = !string.IsNullOrEmpty(base.viewDataKey);
			if (flag)
			{
				base.SaveViewData();
			}
			base.MarkDirtyRepaint();
			bool showMixedValue = this.showMixedValue;
			if (showMixedValue)
			{
				this.UpdateMixedValueContent();
			}
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00023170 File Offset: 0x00021370
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			bool flag = this.m_VisualInput != null;
			if (flag)
			{
				string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
				TValueType value = this.m_Value;
				base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
				bool flag2 = !EqualityComparer<TValueType>.Default.Equals(value, this.m_Value);
				if (flag2)
				{
					using (ChangeEvent<TValueType> pooled = ChangeEvent<TValueType>.GetPooled(value, this.m_Value))
					{
						pooled.target = this;
						this.SetValueWithoutNotify(this.m_Value);
						this.SendEvent(pooled);
					}
				}
			}
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00023214 File Offset: 0x00021414
		internal override Rect GetTooltipRect()
		{
			return (!string.IsNullOrEmpty(this.label)) ? this.labelElement.worldBound : base.worldBound;
		}

		// Token: 0x040003B1 RID: 945
		public static readonly string ussClassName = "unity-base-field";

		// Token: 0x040003B2 RID: 946
		public static readonly string labelUssClassName = BaseField<TValueType>.ussClassName + "__label";

		// Token: 0x040003B3 RID: 947
		public static readonly string inputUssClassName = BaseField<TValueType>.ussClassName + "__input";

		// Token: 0x040003B4 RID: 948
		public static readonly string noLabelVariantUssClassName = BaseField<TValueType>.ussClassName + "--no-label";

		// Token: 0x040003B5 RID: 949
		public static readonly string labelDraggerVariantUssClassName = BaseField<TValueType>.labelUssClassName + "--with-dragger";

		// Token: 0x040003B6 RID: 950
		public static readonly string mixedValueLabelUssClassName = BaseField<TValueType>.labelUssClassName + "--mixed-value";

		// Token: 0x040003B7 RID: 951
		public static readonly string alignedFieldUssClassName = BaseField<TValueType>.ussClassName + "__aligned";

		// Token: 0x040003B8 RID: 952
		private static readonly string inspectorFieldUssClassName = BaseField<TValueType>.ussClassName + "__inspector-field";

		// Token: 0x040003B9 RID: 953
		private const int kIndentPerLevel = 15;

		// Token: 0x040003BA RID: 954
		protected static readonly string mixedValueString = "—";

		// Token: 0x040003BB RID: 955
		protected internal static readonly PropertyName serializedPropertyCopyName = "SerializedPropertyCopyName";

		// Token: 0x040003BC RID: 956
		private static CustomStyleProperty<float> s_LabelWidthRatioProperty = new CustomStyleProperty<float>("--unity-property-field-label-width-ratio");

		// Token: 0x040003BD RID: 957
		private static CustomStyleProperty<float> s_LabelExtraPaddingProperty = new CustomStyleProperty<float>("--unity-property-field-label-extra-padding");

		// Token: 0x040003BE RID: 958
		private static CustomStyleProperty<float> s_LabelBaseMinWidthProperty = new CustomStyleProperty<float>("--unity-property-field-label-base-min-width");

		// Token: 0x040003BF RID: 959
		private float m_LabelWidthRatio;

		// Token: 0x040003C0 RID: 960
		private float m_LabelExtraPadding;

		// Token: 0x040003C1 RID: 961
		private float m_LabelBaseMinWidth;

		// Token: 0x040003C2 RID: 962
		private VisualElement m_VisualInput;

		// Token: 0x040003C3 RID: 963
		[SerializeField]
		private TValueType m_Value;

		// Token: 0x040003C5 RID: 965
		private bool m_ShowMixedValue;

		// Token: 0x040003C6 RID: 966
		private Label m_MixedValueLabel;

		// Token: 0x040003C7 RID: 967
		private VisualElement m_CachedInspectorElement;

		// Token: 0x040003C8 RID: 968
		private int m_CachedListAndFoldoutDepth;

		// Token: 0x02000119 RID: 281
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x06000913 RID: 2323 RVA: 0x00023331 File Offset: 0x00021531
			public UxmlTraits()
			{
				base.focusIndex.defaultValue = 0;
				base.focusable.defaultValue = true;
			}

			// Token: 0x06000914 RID: 2324 RVA: 0x0002336C File Offset: 0x0002156C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((BaseField<TValueType>)ve).label = this.m_Label.GetValueFromBag(bag, cc);
			}

			// Token: 0x06000915 RID: 2325 RVA: 0x00023394 File Offset: 0x00021594
			internal static List<string> ParseChoiceList(string choicesFromBag)
			{
				bool flag = string.IsNullOrEmpty(choicesFromBag.Trim());
				List<string> list;
				if (flag)
				{
					list = null;
				}
				else
				{
					string[] array = choicesFromBag.Split(new char[] { ',' });
					bool flag2 = array.Length != 0;
					if (flag2)
					{
						List<string> list2 = new List<string>();
						foreach (string text in array)
						{
							list2.Add(text.Trim());
						}
						list = list2;
					}
					else
					{
						list = null;
					}
				}
				return list;
			}

			// Token: 0x040003C9 RID: 969
			private UxmlStringAttributeDescription m_Label = new UxmlStringAttributeDescription
			{
				name = "label"
			};
		}
	}
}
