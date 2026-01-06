using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200012C RID: 300
	internal class ButtonStripField : BaseField<int>
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x00026B5C File Offset: 0x00024D5C
		private List<Button> buttons
		{
			get
			{
				this.m_Buttons.Clear();
				this.Query(null, null).ToList(this.m_Buttons);
				return this.m_Buttons;
			}
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00026B98 File Offset: 0x00024D98
		public void AddButton(string text, string name = "")
		{
			Button button = this.CreateButton(name);
			button.text = text;
			base.Add(button);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00026BC0 File Offset: 0x00024DC0
		public void AddButton(Background icon, string name = "")
		{
			Button button = this.CreateButton(name);
			VisualElement visualElement = new VisualElement();
			visualElement.AddToClassList("unity-button-strip-field__button-icon");
			visualElement.style.backgroundImage = icon;
			button.Add(visualElement);
			base.Add(button);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00026C0C File Offset: 0x00024E0C
		private Button CreateButton(string name)
		{
			Button button = new Button
			{
				name = name
			};
			button.AddToClassList("unity-button-strip-field__button");
			button.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnButtonDetachFromPanel), TrickleDown.NoTrickleDown);
			button.clicked += delegate
			{
				this.value = this.buttons.IndexOf(button);
			};
			base.Add(button);
			this.RefreshButtonsStyling();
			return button;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00026C9C File Offset: 0x00024E9C
		private void OnButtonDetachFromPanel(DetachFromPanelEvent evt)
		{
			VisualElement visualElement = evt.currentTarget as VisualElement;
			ButtonStripField buttonStripField;
			bool flag;
			if (visualElement != null)
			{
				buttonStripField = visualElement.parent as ButtonStripField;
				flag = buttonStripField != null;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2)
			{
				buttonStripField.RefreshButtonsStyling();
				buttonStripField.EnsureValueIsValid();
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00026CE0 File Offset: 0x00024EE0
		private void RefreshButtonsStyling()
		{
			for (int i = 0; i < this.buttons.Count; i++)
			{
				Button button = this.m_Buttons[i];
				bool flag = this.m_Buttons.Count == 1;
				bool flag2 = i == 0;
				bool flag3 = i == this.m_Buttons.Count - 1;
				button.EnableInClassList("unity-button-strip-field__button--alone", flag);
				button.EnableInClassList("unity-button-strip-field__button--left", !flag && flag2);
				button.EnableInClassList("unity-button-strip-field__button--right", !flag && flag3);
				button.EnableInClassList("unity-button-strip-field__button--middle", !flag && !flag2 && !flag3);
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00026D91 File Offset: 0x00024F91
		public ButtonStripField()
			: base(null)
		{
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00026DA7 File Offset: 0x00024FA7
		public ButtonStripField(string label)
			: base(label)
		{
			base.AddToClassList("unity-button-strip-field");
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00026DC9 File Offset: 0x00024FC9
		public override void SetValueWithoutNotify(int newValue)
		{
			newValue = Mathf.Clamp(newValue, 0, this.buttons.Count - 1);
			base.SetValueWithoutNotify(newValue);
			this.RefreshButtonsState();
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00026DF1 File Offset: 0x00024FF1
		private void EnsureValueIsValid()
		{
			this.SetValueWithoutNotify(Mathf.Clamp(this.value, 0, this.buttons.Count - 1));
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00026E14 File Offset: 0x00025014
		private void RefreshButtonsState()
		{
			for (int i = 0; i < this.buttons.Count; i++)
			{
				bool flag = i == this.value;
				if (flag)
				{
					this.m_Buttons[i].pseudoStates |= PseudoStates.Checked;
				}
				else
				{
					this.m_Buttons[i].pseudoStates &= ~PseudoStates.Checked;
				}
			}
		}

		// Token: 0x0400042B RID: 1067
		public const string className = "unity-button-strip-field";

		// Token: 0x0400042C RID: 1068
		private const string k_ButtonClass = "unity-button-strip-field__button";

		// Token: 0x0400042D RID: 1069
		private const string k_IconClass = "unity-button-strip-field__button-icon";

		// Token: 0x0400042E RID: 1070
		private const string k_ButtonLeftClass = "unity-button-strip-field__button--left";

		// Token: 0x0400042F RID: 1071
		private const string k_ButtonMiddleClass = "unity-button-strip-field__button--middle";

		// Token: 0x04000430 RID: 1072
		private const string k_ButtonRightClass = "unity-button-strip-field__button--right";

		// Token: 0x04000431 RID: 1073
		private const string k_ButtonAloneClass = "unity-button-strip-field__button--alone";

		// Token: 0x04000432 RID: 1074
		private List<Button> m_Buttons = new List<Button>();

		// Token: 0x0200012D RID: 301
		public new class UxmlFactory : UxmlFactory<ButtonStripField, ButtonStripField.UxmlTraits>
		{
		}

		// Token: 0x0200012E RID: 302
		public new class UxmlTraits : BaseField<int>.UxmlTraits
		{
		}
	}
}
