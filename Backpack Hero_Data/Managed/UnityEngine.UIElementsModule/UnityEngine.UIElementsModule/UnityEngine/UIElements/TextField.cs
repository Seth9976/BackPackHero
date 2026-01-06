using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200017E RID: 382
	public class TextField : TextInputBaseField<string>
	{
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x00030A47 File Offset: 0x0002EC47
		private TextField.TextInput textInput
		{
			get
			{
				return (TextField.TextInput)base.textInputBase;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x00030A54 File Offset: 0x0002EC54
		// (set) Token: 0x06000BE1 RID: 3041 RVA: 0x00030A71 File Offset: 0x0002EC71
		public bool multiline
		{
			get
			{
				return this.textInput.multiline;
			}
			set
			{
				this.textInput.multiline = value;
			}
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00030A81 File Offset: 0x0002EC81
		public void SelectRange(int rangeCursorIndex, int selectionIndex)
		{
			this.textInput.SelectRange(rangeCursorIndex, selectionIndex);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00030A92 File Offset: 0x0002EC92
		public TextField()
			: this(null)
		{
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00030A9D File Offset: 0x0002EC9D
		public TextField(int maxLength, bool multiline, bool isPasswordField, char maskChar)
			: this(null, maxLength, multiline, isPasswordField, maskChar)
		{
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00030AAD File Offset: 0x0002ECAD
		public TextField(string label)
			: this(label, -1, false, false, '*')
		{
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00030AC0 File Offset: 0x0002ECC0
		public TextField(string label, int maxLength, bool multiline, bool isPasswordField, char maskChar)
			: base(label, maxLength, maskChar, new TextField.TextInput())
		{
			base.AddToClassList(TextField.ussClassName);
			base.labelElement.AddToClassList(TextField.labelUssClassName);
			base.visualInput.AddToClassList(TextField.inputUssClassName);
			base.pickingMode = PickingMode.Ignore;
			this.SetValueWithoutNotify("");
			this.multiline = multiline;
			base.isPasswordField = isPasswordField;
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x00030B34 File Offset: 0x0002ED34
		// (set) Token: 0x06000BE8 RID: 3048 RVA: 0x00030B4C File Offset: 0x0002ED4C
		public override string value
		{
			get
			{
				return base.value;
			}
			set
			{
				base.value = value;
				base.text = base.rawValue;
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00030B64 File Offset: 0x0002ED64
		public override void SetValueWithoutNotify(string newValue)
		{
			base.SetValueWithoutNotify(newValue);
			base.text = base.rawValue;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00030B7C File Offset: 0x0002ED7C
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
			base.text = base.rawValue;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0000A3F1 File Offset: 0x000085F1
		protected override string ValueToString(string value)
		{
			return value;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0000A3F1 File Offset: 0x000085F1
		protected override string StringToValue(string str)
		{
			return str;
		}

		// Token: 0x04000594 RID: 1428
		public new static readonly string ussClassName = "unity-text-field";

		// Token: 0x04000595 RID: 1429
		public new static readonly string labelUssClassName = TextField.ussClassName + "__label";

		// Token: 0x04000596 RID: 1430
		public new static readonly string inputUssClassName = TextField.ussClassName + "__input";

		// Token: 0x0200017F RID: 383
		public new class UxmlFactory : UxmlFactory<TextField, TextField.UxmlTraits>
		{
		}

		// Token: 0x02000180 RID: 384
		public new class UxmlTraits : TextInputBaseField<string>.UxmlTraits
		{
			// Token: 0x06000BEF RID: 3055 RVA: 0x00030BEC File Offset: 0x0002EDEC
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				TextField textField = (TextField)ve;
				textField.multiline = this.m_Multiline.GetValueFromBag(bag, cc);
				base.Init(ve, bag, cc);
			}

			// Token: 0x04000597 RID: 1431
			private UxmlBoolAttributeDescription m_Multiline = new UxmlBoolAttributeDescription
			{
				name = "multiline"
			};
		}

		// Token: 0x02000181 RID: 385
		private class TextInput : TextInputBaseField<string>.TextInputBase
		{
			// Token: 0x17000255 RID: 597
			// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00030C3F File Offset: 0x0002EE3F
			private TextField parentTextField
			{
				get
				{
					return (TextField)base.parent;
				}
			}

			// Token: 0x17000256 RID: 598
			// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x00030C4C File Offset: 0x0002EE4C
			// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x00030C64 File Offset: 0x0002EE64
			public bool multiline
			{
				get
				{
					return this.m_Multiline;
				}
				set
				{
					this.m_Multiline = value;
					bool flag = !value;
					if (flag)
					{
						base.text = base.text.Replace("\n", "");
					}
					this.SetTextAlign();
				}
			}

			// Token: 0x06000BF4 RID: 3060 RVA: 0x00030CA4 File Offset: 0x0002EEA4
			private void SetTextAlign()
			{
				bool multiline = this.m_Multiline;
				if (multiline)
				{
					base.RemoveFromClassList(TextInputBaseField<string>.singleLineInputUssClassName);
					base.AddToClassList(TextInputBaseField<string>.multilineInputUssClassName);
				}
				else
				{
					base.RemoveFromClassList(TextInputBaseField<string>.multilineInputUssClassName);
					base.AddToClassList(TextInputBaseField<string>.singleLineInputUssClassName);
				}
			}

			// Token: 0x17000257 RID: 599
			// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x00030CF4 File Offset: 0x0002EEF4
			public override bool isPasswordField
			{
				set
				{
					base.isPasswordField = value;
					if (value)
					{
						this.multiline = false;
					}
				}
			}

			// Token: 0x06000BF6 RID: 3062 RVA: 0x00030D18 File Offset: 0x0002EF18
			protected override string StringToValue(string str)
			{
				return str;
			}

			// Token: 0x06000BF7 RID: 3063 RVA: 0x00030D2C File Offset: 0x0002EF2C
			public void SelectRange(int cursorIndex, int selectionIndex)
			{
				bool flag = base.editorEngine != null;
				if (flag)
				{
					base.editorEngine.cursorIndex = cursorIndex;
					base.editorEngine.selectIndex = selectionIndex;
				}
			}

			// Token: 0x06000BF8 RID: 3064 RVA: 0x00030D64 File Offset: 0x0002EF64
			internal override void SyncTextEngine()
			{
				bool flag = this.parentTextField != null;
				if (flag)
				{
					base.editorEngine.multiline = this.multiline;
					base.editorEngine.isPasswordField = this.isPasswordField;
				}
				base.SyncTextEngine();
			}

			// Token: 0x06000BF9 RID: 3065 RVA: 0x00030DAC File Offset: 0x0002EFAC
			protected override void ExecuteDefaultActionAtTarget(EventBase evt)
			{
				base.ExecuteDefaultActionAtTarget(evt);
				bool flag = evt == null;
				if (!flag)
				{
					bool flag2 = evt.eventTypeId == EventBase<KeyDownEvent>.TypeId();
					if (flag2)
					{
						KeyDownEvent keyDownEvent = evt as KeyDownEvent;
						bool flag3 = !this.parentTextField.isDelayed || (!this.multiline && ((keyDownEvent != null && keyDownEvent.keyCode == KeyCode.KeypadEnter) || (keyDownEvent != null && keyDownEvent.keyCode == KeyCode.Return)));
						if (flag3)
						{
							this.parentTextField.value = base.text;
						}
						bool multiline = this.multiline;
						if (multiline)
						{
							char? c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : default(char?));
							int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
							int num2 = 9;
							bool flag4 = ((num.GetValueOrDefault() == num2) & (num != null)) && keyDownEvent.modifiers == EventModifiers.None;
							if (flag4)
							{
								if (keyDownEvent != null)
								{
									keyDownEvent.StopPropagation();
								}
								if (keyDownEvent != null)
								{
									keyDownEvent.PreventDefault();
								}
							}
							else
							{
								c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : default(char?));
								num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
								num2 = 3;
								bool flag5;
								if (!((num.GetValueOrDefault() == num2) & (num != null)) || keyDownEvent == null || !keyDownEvent.shiftKey)
								{
									c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : default(char?));
									num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
									num2 = 10;
									flag5 = ((num.GetValueOrDefault() == num2) & (num != null)) && keyDownEvent != null && keyDownEvent.shiftKey;
								}
								else
								{
									flag5 = true;
								}
								bool flag6 = flag5;
								if (flag6)
								{
									base.parent.Focus();
									evt.StopPropagation();
									evt.PreventDefault();
								}
							}
						}
						else
						{
							char? c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : default(char?));
							int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
							int num2 = 3;
							bool flag7;
							if (!((num.GetValueOrDefault() == num2) & (num != null)))
							{
								c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : default(char?));
								num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
								num2 = 10;
								flag7 = (num.GetValueOrDefault() == num2) & (num != null);
							}
							else
							{
								flag7 = true;
							}
							bool flag8 = flag7;
							if (flag8)
							{
								base.parent.Focus();
								evt.StopPropagation();
								evt.PreventDefault();
							}
						}
					}
					else
					{
						bool flag9 = evt.eventTypeId == EventBase<ExecuteCommandEvent>.TypeId();
						if (flag9)
						{
							ExecuteCommandEvent executeCommandEvent = evt as ExecuteCommandEvent;
							string commandName = executeCommandEvent.commandName;
							bool flag10 = !this.parentTextField.isDelayed && (commandName == "Paste" || commandName == "Cut");
							if (flag10)
							{
								this.parentTextField.value = base.text;
							}
						}
						else
						{
							bool flag11 = evt.eventTypeId == EventBase<NavigationSubmitEvent>.TypeId() || evt.eventTypeId == EventBase<NavigationCancelEvent>.TypeId() || evt.eventTypeId == EventBase<NavigationMoveEvent>.TypeId();
							if (flag11)
							{
								evt.StopPropagation();
								evt.PreventDefault();
							}
						}
					}
				}
			}

			// Token: 0x06000BFA RID: 3066 RVA: 0x00031158 File Offset: 0x0002F358
			protected override void ExecuteDefaultAction(EventBase evt)
			{
				base.ExecuteDefaultAction(evt);
				bool flag;
				if (this.parentTextField.isDelayed)
				{
					long? num = ((evt != null) ? new long?(evt.eventTypeId) : default(long?));
					long num2 = EventBase<BlurEvent>.TypeId();
					flag = (num.GetValueOrDefault() == num2) & (num != null);
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				if (flag2)
				{
					this.parentTextField.value = base.text;
				}
			}

			// Token: 0x04000598 RID: 1432
			private bool m_Multiline;
		}
	}
}
