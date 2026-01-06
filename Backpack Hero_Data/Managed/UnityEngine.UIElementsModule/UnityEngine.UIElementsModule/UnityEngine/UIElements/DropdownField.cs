using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000130 RID: 304
	public class DropdownField : BaseField<string>
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x00026EBC File Offset: 0x000250BC
		protected TextElement textElement
		{
			get
			{
				return this.m_TextElement;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x00026ED4 File Offset: 0x000250D4
		public string text
		{
			get
			{
				return this.m_TextElement.text;
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00026EF4 File Offset: 0x000250F4
		internal string GetValueToDisplay()
		{
			bool flag = this.m_FormatSelectedValueCallback != null;
			string text;
			if (flag)
			{
				text = this.m_FormatSelectedValueCallback.Invoke(this.value);
			}
			else
			{
				text = this.value ?? string.Empty;
			}
			return text;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00026F38 File Offset: 0x00025138
		internal string GetListItemToDisplay(string value)
		{
			bool flag = this.m_FormatListItemCallback != null;
			string text;
			if (flag)
			{
				text = this.m_FormatListItemCallback.Invoke(value);
			}
			else
			{
				text = ((value != null && this.m_Choices.Contains(value)) ? value : string.Empty);
			}
			return text;
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x00026F80 File Offset: 0x00025180
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x00026F98 File Offset: 0x00025198
		internal virtual Func<string, string> formatSelectedValueCallback
		{
			get
			{
				return this.m_FormatSelectedValueCallback;
			}
			set
			{
				this.m_FormatSelectedValueCallback = value;
				this.textElement.text = this.GetValueToDisplay();
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00026FB4 File Offset: 0x000251B4
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x00026FCC File Offset: 0x000251CC
		internal virtual Func<string, string> formatListItemCallback
		{
			get
			{
				return this.m_FormatListItemCallback;
			}
			set
			{
				this.m_FormatListItemCallback = value;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00026FD8 File Offset: 0x000251D8
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x00026FF0 File Offset: 0x000251F0
		public int index
		{
			get
			{
				return this.m_Index;
			}
			set
			{
				this.m_Index = value;
				bool flag = this.m_Choices == null || value >= this.m_Choices.Count || value < 0;
				if (flag)
				{
					this.value = null;
				}
				else
				{
					this.value = this.m_Choices[this.m_Index];
				}
			}
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00027048 File Offset: 0x00025248
		public DropdownField()
			: this(null)
		{
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00027054 File Offset: 0x00025254
		public DropdownField(string label)
			: base(label, null)
		{
			base.AddToClassList(DropdownField.ussClassNameBasePopupField);
			base.labelElement.AddToClassList(DropdownField.labelUssClassNameBasePopupField);
			this.m_TextElement = new DropdownField.PopupTextElement
			{
				pickingMode = PickingMode.Ignore
			};
			this.m_TextElement.AddToClassList(DropdownField.textUssClassNameBasePopupField);
			base.visualInput.AddToClassList(DropdownField.inputUssClassNameBasePopupField);
			base.visualInput.Add(this.m_TextElement);
			this.m_ArrowElement = new VisualElement();
			this.m_ArrowElement.AddToClassList(DropdownField.arrowUssClassNameBasePopupField);
			this.m_ArrowElement.pickingMode = PickingMode.Ignore;
			base.visualInput.Add(this.m_ArrowElement);
			this.choices = new List<string>();
			base.AddToClassList(DropdownField.ussClassNamePopupField);
			base.labelElement.AddToClassList(DropdownField.labelUssClassNamePopupField);
			base.visualInput.AddToClassList(DropdownField.inputUssClassNamePopupField);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00027152 File Offset: 0x00025352
		public DropdownField(List<string> choices, string defaultValue, Func<string, string> formatSelectedValueCallback = null, Func<string, string> formatListItemCallback = null)
			: this(null, choices, defaultValue, formatSelectedValueCallback, formatListItemCallback)
		{
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00027164 File Offset: 0x00025364
		public DropdownField(string label, List<string> choices, string defaultValue, Func<string, string> formatSelectedValueCallback = null, Func<string, string> formatListItemCallback = null)
			: this(label)
		{
			bool flag = defaultValue == null;
			if (flag)
			{
				throw new ArgumentNullException("defaultValue");
			}
			this.choices = choices;
			this.SetValueWithoutNotify(defaultValue);
			this.formatListItemCallback = formatListItemCallback;
			this.formatSelectedValueCallback = formatSelectedValueCallback;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x000271AF File Offset: 0x000253AF
		public DropdownField(List<string> choices, int defaultIndex, Func<string, string> formatSelectedValueCallback = null, Func<string, string> formatListItemCallback = null)
			: this(null, choices, defaultIndex, formatSelectedValueCallback, formatListItemCallback)
		{
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x000271BF File Offset: 0x000253BF
		public DropdownField(string label, List<string> choices, int defaultIndex, Func<string, string> formatSelectedValueCallback = null, Func<string, string> formatListItemCallback = null)
			: this(label)
		{
			this.choices = choices;
			this.index = defaultIndex;
			this.formatListItemCallback = formatListItemCallback;
			this.formatSelectedValueCallback = formatSelectedValueCallback;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x000271EC File Offset: 0x000253EC
		internal void AddMenuItems(IGenericMenu menu)
		{
			bool flag = menu == null;
			if (flag)
			{
				throw new ArgumentNullException("menu");
			}
			bool flag2 = this.m_Choices == null;
			if (!flag2)
			{
				using (List<string>.Enumerator enumerator = this.m_Choices.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string item = enumerator.Current;
						bool flag3 = item == this.value;
						menu.AddItem(this.GetListItemToDisplay(item), flag3, delegate
						{
							this.ChangeValueFromMenu(item);
						});
					}
				}
			}
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x000272A8 File Offset: 0x000254A8
		private void ChangeValueFromMenu(string menuItem)
		{
			this.value = menuItem;
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x000272B3 File Offset: 0x000254B3
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x000272BB File Offset: 0x000254BB
		public virtual List<string> choices
		{
			get
			{
				return this.m_Choices;
			}
			set
			{
				this.m_Choices = value;
				this.SetValueWithoutNotify(base.rawValue);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x000272D4 File Offset: 0x000254D4
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x000272EC File Offset: 0x000254EC
		public override string value
		{
			get
			{
				return base.value;
			}
			set
			{
				List<string> choices = this.m_Choices;
				this.m_Index = ((choices != null) ? choices.IndexOf(value) : (-1));
				base.value = value;
			}
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00027310 File Offset: 0x00025510
		public override void SetValueWithoutNotify(string newValue)
		{
			List<string> choices = this.m_Choices;
			this.m_Index = ((choices != null) ? choices.IndexOf(newValue) : (-1));
			base.SetValueWithoutNotify(newValue);
			((INotifyValueChanged<string>)this.m_TextElement).SetValueWithoutNotify(this.GetValueToDisplay());
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00027348 File Offset: 0x00025548
		protected override void ExecuteDefaultActionAtTarget(EventBase evt)
		{
			base.ExecuteDefaultActionAtTarget(evt);
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = false;
				KeyDownEvent keyDownEvent = evt as KeyDownEvent;
				bool flag3 = keyDownEvent != null;
				if (flag3)
				{
					bool flag4 = keyDownEvent.keyCode == KeyCode.Space || keyDownEvent.keyCode == KeyCode.KeypadEnter || keyDownEvent.keyCode == KeyCode.Return;
					if (flag4)
					{
						flag2 = true;
					}
				}
				else
				{
					MouseDownEvent mouseDownEvent = evt as MouseDownEvent;
					bool flag5 = mouseDownEvent != null && mouseDownEvent.button == 0;
					if (flag5)
					{
						MouseDownEvent mouseDownEvent2 = (MouseDownEvent)evt;
						bool flag6 = base.visualInput.ContainsPoint(base.visualInput.WorldToLocal(mouseDownEvent2.mousePosition));
						if (flag6)
						{
							flag2 = true;
						}
					}
				}
				bool flag7 = flag2;
				if (flag7)
				{
					this.ShowMenu();
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00027414 File Offset: 0x00025614
		private void ShowMenu()
		{
			bool flag = this.createMenuCallback != null;
			IGenericMenu genericMenu;
			if (flag)
			{
				genericMenu = this.createMenuCallback.Invoke();
			}
			else
			{
				BaseVisualElementPanel elementPanel = base.elementPanel;
				IGenericMenu genericMenu2;
				if (elementPanel == null || elementPanel.contextType != ContextType.Player)
				{
					genericMenu2 = DropdownUtility.CreateDropdown();
				}
				else
				{
					IGenericMenu genericMenu3 = new GenericDropdownMenu();
					genericMenu2 = genericMenu3;
				}
				genericMenu = genericMenu2;
			}
			this.AddMenuItems(genericMenu);
			genericMenu.DropDown(base.visualInput.worldBound, this, true);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00027484 File Offset: 0x00025684
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				this.textElement.text = BaseField<string>.mixedValueString;
			}
			this.textElement.EnableInClassList(BaseField<string>.mixedValueLabelUssClassName, base.showMixedValue);
		}

		// Token: 0x04000435 RID: 1077
		internal List<string> m_Choices;

		// Token: 0x04000436 RID: 1078
		private TextElement m_TextElement;

		// Token: 0x04000437 RID: 1079
		private VisualElement m_ArrowElement;

		// Token: 0x04000438 RID: 1080
		internal Func<string, string> m_FormatSelectedValueCallback;

		// Token: 0x04000439 RID: 1081
		internal Func<string, string> m_FormatListItemCallback;

		// Token: 0x0400043A RID: 1082
		internal Func<IGenericMenu> createMenuCallback = null;

		// Token: 0x0400043B RID: 1083
		private int m_Index = -1;

		// Token: 0x0400043C RID: 1084
		internal static readonly string ussClassNameBasePopupField = "unity-base-popup-field";

		// Token: 0x0400043D RID: 1085
		internal static readonly string textUssClassNameBasePopupField = DropdownField.ussClassNameBasePopupField + "__text";

		// Token: 0x0400043E RID: 1086
		internal static readonly string arrowUssClassNameBasePopupField = DropdownField.ussClassNameBasePopupField + "__arrow";

		// Token: 0x0400043F RID: 1087
		internal static readonly string labelUssClassNameBasePopupField = DropdownField.ussClassNameBasePopupField + "__label";

		// Token: 0x04000440 RID: 1088
		internal static readonly string inputUssClassNameBasePopupField = DropdownField.ussClassNameBasePopupField + "__input";

		// Token: 0x04000441 RID: 1089
		internal static readonly string ussClassNamePopupField = "unity-popup-field";

		// Token: 0x04000442 RID: 1090
		internal static readonly string labelUssClassNamePopupField = DropdownField.ussClassNamePopupField + "__label";

		// Token: 0x04000443 RID: 1091
		internal static readonly string inputUssClassNamePopupField = DropdownField.ussClassNamePopupField + "__input";

		// Token: 0x02000131 RID: 305
		public new class UxmlFactory : UxmlFactory<DropdownField, DropdownField.UxmlTraits>
		{
		}

		// Token: 0x02000132 RID: 306
		public new class UxmlTraits : BaseField<string>.UxmlTraits
		{
			// Token: 0x06000A15 RID: 2581 RVA: 0x0002756C File Offset: 0x0002576C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				DropdownField dropdownField = (DropdownField)ve;
				dropdownField.choices = BaseField<string>.UxmlTraits.ParseChoiceList(this.m_Choices.GetValueFromBag(bag, cc));
				dropdownField.index = this.m_Index.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000444 RID: 1092
			private UxmlIntAttributeDescription m_Index = new UxmlIntAttributeDescription
			{
				name = "index"
			};

			// Token: 0x04000445 RID: 1093
			private UxmlStringAttributeDescription m_Choices = new UxmlStringAttributeDescription
			{
				name = "choices"
			};
		}

		// Token: 0x02000133 RID: 307
		private class PopupTextElement : TextElement
		{
			// Token: 0x06000A17 RID: 2583 RVA: 0x000275F0 File Offset: 0x000257F0
			protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
			{
				string text = this.text;
				bool flag = string.IsNullOrEmpty(text);
				if (flag)
				{
					text = " ";
				}
				return base.MeasureTextSize(text, desiredWidth, widthMode, desiredHeight, heightMode);
			}
		}
	}
}
