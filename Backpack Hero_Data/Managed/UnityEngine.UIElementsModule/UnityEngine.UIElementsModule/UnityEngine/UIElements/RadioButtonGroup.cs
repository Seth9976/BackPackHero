using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000162 RID: 354
	public class RadioButtonGroup : BaseField<int>, IGroupBox
	{
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0002CDAA File Offset: 0x0002AFAA
		// (set) Token: 0x06000B28 RID: 2856 RVA: 0x0002CDB4 File Offset: 0x0002AFB4
		public IEnumerable<string> choices
		{
			get
			{
				return this.m_Choices;
			}
			set
			{
				this.m_Choices = value;
				foreach (RadioButton radioButton in this.m_RadioButtons)
				{
					radioButton.UnregisterValueChangedCallback(this.m_RadioButtonValueChangedCallback);
					radioButton.RemoveFromHierarchy();
				}
				this.m_RadioButtons.Clear();
				bool flag = this.m_Choices != null;
				if (flag)
				{
					foreach (string text in this.m_Choices)
					{
						RadioButton radioButton2 = new RadioButton
						{
							text = text
						};
						radioButton2.RegisterValueChangedCallback(this.m_RadioButtonValueChangedCallback);
						this.m_RadioButtons.Add(radioButton2);
						base.visualInput.Add(radioButton2);
					}
					this.UpdateRadioButtons();
				}
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002CEB8 File Offset: 0x0002B0B8
		public RadioButtonGroup()
			: this(null, null)
		{
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0002CEC4 File Offset: 0x0002B0C4
		public RadioButtonGroup(string label, List<string> radioButtonChoices = null)
			: base(label, null)
		{
			base.AddToClassList(RadioButtonGroup.ussClassName);
			this.m_RadioButtonValueChangedCallback = new EventCallback<ChangeEvent<bool>>(this.RadioButtonValueChangedCallback);
			this.choices = radioButtonChoices;
			this.value = -1;
			base.visualInput.focusable = false;
			base.delegatesFocus = true;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002CF2C File Offset: 0x0002B12C
		private void RadioButtonValueChangedCallback(ChangeEvent<bool> evt)
		{
			bool newValue = evt.newValue;
			if (newValue)
			{
				this.value = this.m_RadioButtons.IndexOf(evt.target as RadioButton);
				evt.StopPropagation();
			}
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0002CF6A File Offset: 0x0002B16A
		public override void SetValueWithoutNotify(int newValue)
		{
			base.SetValueWithoutNotify(newValue);
			this.UpdateRadioButtons();
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002CF7C File Offset: 0x0002B17C
		private void UpdateRadioButtons()
		{
			bool flag = this.value >= 0 && this.value < this.m_RadioButtons.Count;
			if (flag)
			{
				this.m_RadioButtons[this.value].value = true;
			}
			else
			{
				foreach (RadioButton radioButton in this.m_RadioButtons)
				{
					radioButton.value = false;
				}
			}
		}

		// Token: 0x0400050C RID: 1292
		public new static readonly string ussClassName = "unity-radio-button-group";

		// Token: 0x0400050D RID: 1293
		private IEnumerable<string> m_Choices;

		// Token: 0x0400050E RID: 1294
		private List<RadioButton> m_RadioButtons = new List<RadioButton>();

		// Token: 0x0400050F RID: 1295
		private EventCallback<ChangeEvent<bool>> m_RadioButtonValueChangedCallback;

		// Token: 0x02000163 RID: 355
		public new class UxmlFactory : UxmlFactory<RadioButtonGroup, RadioButtonGroup.UxmlTraits>
		{
		}

		// Token: 0x02000164 RID: 356
		public new class UxmlTraits : BaseFieldTraits<int, UxmlIntAttributeDescription>
		{
			// Token: 0x06000B30 RID: 2864 RVA: 0x0002D030 File Offset: 0x0002B230
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				RadioButtonGroup radioButtonGroup = (RadioButtonGroup)ve;
				radioButtonGroup.choices = BaseField<int>.UxmlTraits.ParseChoiceList(this.m_Choices.GetValueFromBag(bag, cc));
			}

			// Token: 0x04000510 RID: 1296
			private UxmlStringAttributeDescription m_Choices = new UxmlStringAttributeDescription
			{
				name = "choices"
			};
		}
	}
}
