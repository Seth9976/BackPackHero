using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200015F RID: 351
	public class RadioButton : BaseBoolField, IGroupBoxOption
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0002CAFA File Offset: 0x0002ACFA
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x0002CB04 File Offset: 0x0002AD04
		public override bool value
		{
			get
			{
				return base.value;
			}
			set
			{
				bool flag = base.value != value;
				if (flag)
				{
					base.value = value;
					this.UpdateCheckmark();
					if (value)
					{
						this.OnOptionSelected<RadioButton>();
					}
				}
			}
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0002CB41 File Offset: 0x0002AD41
		public RadioButton()
			: this(null)
		{
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002CB4C File Offset: 0x0002AD4C
		public RadioButton(string label)
			: base(label)
		{
			base.AddToClassList(RadioButton.ussClassName);
			base.visualInput.AddToClassList(RadioButton.inputUssClassName);
			base.labelElement.AddToClassList(RadioButton.labelUssClassName);
			this.m_CheckMark.RemoveFromHierarchy();
			this.m_CheckmarkBackground = new VisualElement
			{
				pickingMode = PickingMode.Ignore
			};
			this.m_CheckmarkBackground.Add(this.m_CheckMark);
			this.m_CheckmarkBackground.AddToClassList(RadioButton.checkmarkBackgroundUssClassName);
			this.m_CheckMark.AddToClassList(RadioButton.checkmarkUssClassName);
			base.visualInput.Add(this.m_CheckmarkBackground);
			this.UpdateCheckmark();
			this.RegisterGroupBoxOptionCallbacks<RadioButton>();
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0002CC03 File Offset: 0x0002AE03
		protected override void InitLabel()
		{
			base.InitLabel();
			this.m_Label.AddToClassList(RadioButton.textUssClassName);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0002CC20 File Offset: 0x0002AE20
		protected override void ToggleValue()
		{
			bool flag = !this.value;
			if (flag)
			{
				this.value = true;
			}
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0002CC45 File Offset: 0x0002AE45
		public void SetSelected(bool selected)
		{
			this.value = selected;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0002CC50 File Offset: 0x0002AE50
		public override void SetValueWithoutNotify(bool newValue)
		{
			base.SetValueWithoutNotify(newValue);
			this.UpdateCheckmark();
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0002CC62 File Offset: 0x0002AE62
		private void UpdateCheckmark()
		{
			this.m_CheckMark.style.display = (this.value ? DisplayStyle.Flex : DisplayStyle.None);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0002CC88 File Offset: 0x0002AE88
		protected override void UpdateMixedValueContent()
		{
			base.UpdateMixedValueContent();
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				this.m_CheckmarkBackground.RemoveFromHierarchy();
			}
			else
			{
				this.m_CheckmarkBackground.Add(this.m_CheckMark);
				base.visualInput.Add(this.m_CheckmarkBackground);
			}
		}

		// Token: 0x04000504 RID: 1284
		public new static readonly string ussClassName = "unity-radio-button";

		// Token: 0x04000505 RID: 1285
		public new static readonly string labelUssClassName = RadioButton.ussClassName + "__label";

		// Token: 0x04000506 RID: 1286
		public new static readonly string inputUssClassName = RadioButton.ussClassName + "__input";

		// Token: 0x04000507 RID: 1287
		public static readonly string checkmarkBackgroundUssClassName = RadioButton.ussClassName + "__checkmark-background";

		// Token: 0x04000508 RID: 1288
		public static readonly string checkmarkUssClassName = RadioButton.ussClassName + "__checkmark";

		// Token: 0x04000509 RID: 1289
		public static readonly string textUssClassName = RadioButton.ussClassName + "__text";

		// Token: 0x0400050A RID: 1290
		private VisualElement m_CheckmarkBackground;

		// Token: 0x02000160 RID: 352
		public new class UxmlFactory : UxmlFactory<RadioButton, RadioButton.UxmlTraits>
		{
		}

		// Token: 0x02000161 RID: 353
		public new class UxmlTraits : BaseFieldTraits<bool, UxmlBoolAttributeDescription>
		{
			// Token: 0x06000B25 RID: 2853 RVA: 0x0002CD64 File Offset: 0x0002AF64
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((RadioButton)ve).text = this.m_Text.GetValueFromBag(bag, cc);
			}

			// Token: 0x0400050B RID: 1291
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};
		}
	}
}
