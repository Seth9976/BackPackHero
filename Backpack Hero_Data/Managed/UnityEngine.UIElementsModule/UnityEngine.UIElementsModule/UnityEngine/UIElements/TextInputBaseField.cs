using System;
using System.Diagnostics;

namespace UnityEngine.UIElements
{
	// Token: 0x02000183 RID: 387
	public abstract class TextInputBaseField<TValueType> : BaseField<TValueType>
	{
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x000311D1 File Offset: 0x0002F3D1
		protected internal TextInputBaseField<TValueType>.TextInputBase textInputBase
		{
			get
			{
				return this.m_TextInputBase;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x000311DC File Offset: 0x0002F3DC
		internal TextHandle textHandle
		{
			get
			{
				return new TextHandle
				{
					textHandle = this.iTextHandle
				};
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x00031204 File Offset: 0x0002F404
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x0003120C File Offset: 0x0002F40C
		internal ITextHandle iTextHandle { get; private set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x00031218 File Offset: 0x0002F418
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x00031235 File Offset: 0x0002F435
		public string text
		{
			get
			{
				return this.m_TextInputBase.text;
			}
			protected set
			{
				this.m_TextInputBase.text = value;
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000C0E RID: 3086 RVA: 0x00031248 File Offset: 0x0002F448
		// (remove) Token: 0x06000C0F RID: 3087 RVA: 0x00031280 File Offset: 0x0002F480
		[field: DebuggerBrowsable(0)]
		protected event Action<bool> onIsReadOnlyChanged;

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x000312B8 File Offset: 0x0002F4B8
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x000312D5 File Offset: 0x0002F4D5
		public bool isReadOnly
		{
			get
			{
				return this.m_TextInputBase.isReadOnly;
			}
			set
			{
				this.m_TextInputBase.isReadOnly = value;
				Action<bool> action = this.onIsReadOnlyChanged;
				if (action != null)
				{
					action.Invoke(value);
				}
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x000312F8 File Offset: 0x0002F4F8
		// (set) Token: 0x06000C13 RID: 3091 RVA: 0x00031318 File Offset: 0x0002F518
		public bool isPasswordField
		{
			get
			{
				return this.m_TextInputBase.isPasswordField;
			}
			set
			{
				bool flag = this.m_TextInputBase.isPasswordField == value;
				if (!flag)
				{
					this.m_TextInputBase.isPasswordField = value;
					this.m_TextInputBase.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x00031358 File Offset: 0x0002F558
		public Color selectionColor
		{
			get
			{
				return this.m_TextInputBase.selectionColor;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x00031365 File Offset: 0x0002F565
		public Color cursorColor
		{
			get
			{
				return this.m_TextInputBase.cursorColor;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x00031372 File Offset: 0x0002F572
		public int cursorIndex
		{
			get
			{
				return this.m_TextInputBase.cursorIndex;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0003137F File Offset: 0x0002F57F
		public int selectIndex
		{
			get
			{
				return this.m_TextInputBase.selectIndex;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x0003138C File Offset: 0x0002F58C
		// (set) Token: 0x06000C19 RID: 3097 RVA: 0x000313A9 File Offset: 0x0002F5A9
		public int maxLength
		{
			get
			{
				return this.m_TextInputBase.maxLength;
			}
			set
			{
				this.m_TextInputBase.maxLength = value;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x000313BC File Offset: 0x0002F5BC
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x000313D9 File Offset: 0x0002F5D9
		public bool doubleClickSelectsWord
		{
			get
			{
				return this.m_TextInputBase.doubleClickSelectsWord;
			}
			set
			{
				this.m_TextInputBase.doubleClickSelectsWord = value;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x000313EC File Offset: 0x0002F5EC
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x00031409 File Offset: 0x0002F609
		public bool tripleClickSelectsLine
		{
			get
			{
				return this.m_TextInputBase.tripleClickSelectsLine;
			}
			set
			{
				this.m_TextInputBase.tripleClickSelectsLine = value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x0003141C File Offset: 0x0002F61C
		// (set) Token: 0x06000C1F RID: 3103 RVA: 0x00031439 File Offset: 0x0002F639
		public bool isDelayed
		{
			get
			{
				return this.m_TextInputBase.isDelayed;
			}
			set
			{
				this.m_TextInputBase.isDelayed = value;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0003144C File Offset: 0x0002F64C
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x00031469 File Offset: 0x0002F669
		public char maskChar
		{
			get
			{
				return this.m_TextInputBase.maskChar;
			}
			set
			{
				this.m_TextInputBase.maskChar = value;
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0003147C File Offset: 0x0002F67C
		public Vector2 MeasureTextSize(string textToMeasure, float width, VisualElement.MeasureMode widthMode, float height, VisualElement.MeasureMode heightMode)
		{
			return TextUtilities.MeasureVisualElementTextSize(this, textToMeasure, width, widthMode, height, heightMode, this.iTextHandle);
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x000314A1 File Offset: 0x0002F6A1
		internal TextEditorEventHandler editorEventHandler
		{
			get
			{
				return this.m_TextInputBase.editorEventHandler;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x000314AE File Offset: 0x0002F6AE
		internal TextEditorEngine editorEngine
		{
			get
			{
				return this.m_TextInputBase.editorEngine;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x000314BB File Offset: 0x0002F6BB
		internal bool hasFocus
		{
			get
			{
				return this.m_TextInputBase.hasFocus;
			}
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0002311E File Offset: 0x0002131E
		protected virtual string ValueToString(TValueType value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0002311E File Offset: 0x0002131E
		protected virtual TValueType StringToValue(string str)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x000314C8 File Offset: 0x0002F6C8
		public void SelectAll()
		{
			this.m_TextInputBase.SelectAll();
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x000314D7 File Offset: 0x0002F6D7
		internal void SyncTextEngine()
		{
			this.m_TextInputBase.SyncTextEngine();
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x000314E6 File Offset: 0x0002F6E6
		internal void DrawWithTextSelectionAndCursor(MeshGenerationContext mgc, string newText)
		{
			this.m_TextInputBase.DrawWithTextSelectionAndCursor(mgc, newText, base.scaledPixelsPerPoint);
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x000314FD File Offset: 0x0002F6FD
		protected TextInputBaseField(int maxLength, char maskChar, TextInputBaseField<TValueType>.TextInputBase textInputBase)
			: this(null, maxLength, maskChar, textInputBase)
		{
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0003150C File Offset: 0x0002F70C
		protected TextInputBaseField(string label, int maxLength, char maskChar, TextInputBaseField<TValueType>.TextInputBase textInputBase)
			: base(label, textInputBase)
		{
			base.tabIndex = 0;
			base.delegatesFocus = true;
			base.labelElement.tabIndex = -1;
			base.AddToClassList(TextInputBaseField<TValueType>.ussClassName);
			base.labelElement.AddToClassList(TextInputBaseField<TValueType>.labelUssClassName);
			base.visualInput.AddToClassList(TextInputBaseField<TValueType>.inputUssClassName);
			base.visualInput.AddToClassList(TextInputBaseField<TValueType>.singleLineInputUssClassName);
			this.m_TextInputBase = textInputBase;
			this.m_TextInputBase.maxLength = maxLength;
			this.m_TextInputBase.maskChar = maskChar;
			base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnFieldCustomStyleResolved), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x000315CA File Offset: 0x0002F7CA
		private void OnAttachToPanel(AttachToPanelEvent e)
		{
			this.iTextHandle = ((e.destinationPanel.contextType == ContextType.Editor) ? TextNativeHandle.New() : TextCoreHandle.New());
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x000315EE File Offset: 0x0002F7EE
		private void OnFieldCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			this.m_TextInputBase.OnInputCustomStyleResolved(e);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00031600 File Offset: 0x0002F800
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
					char? c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : default(char?));
					int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
					int num2 = 3;
					bool flag3;
					if (!((num.GetValueOrDefault() == num2) & (num != null)))
					{
						c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : default(char?));
						num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
						num2 = 10;
						flag3 = (num.GetValueOrDefault() == num2) & (num != null);
					}
					else
					{
						flag3 = true;
					}
					bool flag4 = flag3;
					if (flag4)
					{
						VisualElement visualInput = base.visualInput;
						if (visualInput != null)
						{
							visualInput.Focus();
						}
					}
				}
				else
				{
					bool flag5 = evt.eventTypeId == EventBase<FocusInEvent>.TypeId();
					if (flag5)
					{
						bool flag6 = evt.leafTarget == this || evt.leafTarget == base.labelElement;
						if (flag6)
						{
							this.m_VisualInputTabIndex = base.visualInput.tabIndex;
							base.visualInput.tabIndex = -1;
						}
					}
					else
					{
						bool flag7 = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
						if (flag7)
						{
							base.delegatesFocus = false;
						}
						else
						{
							bool flag8 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
							if (flag8)
							{
								base.delegatesFocus = true;
								bool flag9 = evt.leafTarget == this || evt.leafTarget == base.labelElement;
								if (flag9)
								{
									base.visualInput.tabIndex = this.m_VisualInputTabIndex;
								}
							}
							else
							{
								bool flag10 = evt.eventTypeId == EventBase<FocusInEvent>.TypeId();
								if (flag10)
								{
									bool showMixedValue = base.showMixedValue;
									if (showMixedValue)
									{
										this.m_TextInputBase.ResetValueAndText();
									}
									bool flag11 = evt.leafTarget == this || evt.leafTarget == base.labelElement;
									if (flag11)
									{
										this.m_VisualInputTabIndex = base.visualInput.tabIndex;
										base.visualInput.tabIndex = -1;
									}
								}
								else
								{
									bool flag12 = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
									if (flag12)
									{
										base.delegatesFocus = false;
									}
									else
									{
										bool flag13 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
										if (flag13)
										{
											base.delegatesFocus = true;
											bool flag14 = evt.leafTarget == this || evt.leafTarget == base.labelElement;
											if (flag14)
											{
												base.visualInput.tabIndex = this.m_VisualInputTabIndex;
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x000318C0 File Offset: 0x0002FAC0
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				this.text = BaseField<TValueType>.mixedValueString;
				base.AddToClassList(BaseField<TValueType>.mixedValueLabelUssClassName);
				VisualElement visualInput = base.visualInput;
				if (visualInput != null)
				{
					visualInput.AddToClassList(BaseField<TValueType>.mixedValueLabelUssClassName);
				}
			}
			else
			{
				VisualElement visualInput2 = base.visualInput;
				if (visualInput2 != null)
				{
					visualInput2.RemoveFromClassList(BaseField<TValueType>.mixedValueLabelUssClassName);
				}
				base.RemoveFromClassList(BaseField<TValueType>.mixedValueLabelUssClassName);
			}
		}

		// Token: 0x04000599 RID: 1433
		private static CustomStyleProperty<Color> s_SelectionColorProperty = new CustomStyleProperty<Color>("--unity-selection-color");

		// Token: 0x0400059A RID: 1434
		private static CustomStyleProperty<Color> s_CursorColorProperty = new CustomStyleProperty<Color>("--unity-cursor-color");

		// Token: 0x0400059B RID: 1435
		private int m_VisualInputTabIndex;

		// Token: 0x0400059C RID: 1436
		private TextInputBaseField<TValueType>.TextInputBase m_TextInputBase;

		// Token: 0x0400059D RID: 1437
		internal const int kMaxLengthNone = -1;

		// Token: 0x0400059E RID: 1438
		internal const char kMaskCharDefault = '*';

		// Token: 0x040005A0 RID: 1440
		public new static readonly string ussClassName = "unity-base-text-field";

		// Token: 0x040005A1 RID: 1441
		public new static readonly string labelUssClassName = TextInputBaseField<TValueType>.ussClassName + "__label";

		// Token: 0x040005A2 RID: 1442
		public new static readonly string inputUssClassName = TextInputBaseField<TValueType>.ussClassName + "__input";

		// Token: 0x040005A3 RID: 1443
		public static readonly string singleLineInputUssClassName = TextInputBaseField<TValueType>.inputUssClassName + "--single-line";

		// Token: 0x040005A4 RID: 1444
		public static readonly string multilineInputUssClassName = TextInputBaseField<TValueType>.inputUssClassName + "--multiline";

		// Token: 0x040005A5 RID: 1445
		public static readonly string textInputUssName = "unity-text-input";

		// Token: 0x02000184 RID: 388
		public new class UxmlTraits : BaseFieldTraits<string, UxmlStringAttributeDescription>
		{
			// Token: 0x06000C32 RID: 3122 RVA: 0x000319C0 File Offset: 0x0002FBC0
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				TextInputBaseField<TValueType> textInputBaseField = (TextInputBaseField<TValueType>)ve;
				textInputBaseField.maxLength = this.m_MaxLength.GetValueFromBag(bag, cc);
				textInputBaseField.isPasswordField = this.m_Password.GetValueFromBag(bag, cc);
				textInputBaseField.isReadOnly = this.m_IsReadOnly.GetValueFromBag(bag, cc);
				textInputBaseField.isDelayed = this.m_IsDelayed.GetValueFromBag(bag, cc);
				string valueFromBag = this.m_MaskCharacter.GetValueFromBag(bag, cc);
				bool flag = !string.IsNullOrEmpty(valueFromBag);
				if (flag)
				{
					textInputBaseField.maskChar = valueFromBag.get_Chars(0);
				}
				textInputBaseField.text = this.m_Text.GetValueFromBag(bag, cc);
			}

			// Token: 0x040005A7 RID: 1447
			private UxmlIntAttributeDescription m_MaxLength = new UxmlIntAttributeDescription
			{
				name = "max-length",
				obsoleteNames = new string[] { "maxLength" },
				defaultValue = -1
			};

			// Token: 0x040005A8 RID: 1448
			private UxmlBoolAttributeDescription m_Password = new UxmlBoolAttributeDescription
			{
				name = "password"
			};

			// Token: 0x040005A9 RID: 1449
			private UxmlStringAttributeDescription m_MaskCharacter = new UxmlStringAttributeDescription
			{
				name = "mask-character",
				obsoleteNames = new string[] { "maskCharacter" },
				defaultValue = '*'.ToString()
			};

			// Token: 0x040005AA RID: 1450
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};

			// Token: 0x040005AB RID: 1451
			private UxmlBoolAttributeDescription m_IsReadOnly = new UxmlBoolAttributeDescription
			{
				name = "readonly"
			};

			// Token: 0x040005AC RID: 1452
			private UxmlBoolAttributeDescription m_IsDelayed = new UxmlBoolAttributeDescription
			{
				name = "is-delayed"
			};
		}

		// Token: 0x02000185 RID: 389
		protected internal abstract class TextInputBase : VisualElement, ITextInputField, IEventHandler, ITextElement
		{
			// Token: 0x17000271 RID: 625
			// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00031B55 File Offset: 0x0002FD55
			internal string originalText
			{
				get
				{
					return this.m_OriginalText;
				}
			}

			// Token: 0x06000C35 RID: 3125 RVA: 0x00031B60 File Offset: 0x0002FD60
			public void ResetValueAndText()
			{
				this.m_OriginalText = (this.text = null);
			}

			// Token: 0x06000C36 RID: 3126 RVA: 0x00031B7F File Offset: 0x0002FD7F
			private void SaveValueAndText()
			{
				this.m_OriginalText = this.text;
			}

			// Token: 0x06000C37 RID: 3127 RVA: 0x00031B8E File Offset: 0x0002FD8E
			private void RestoreValueAndText()
			{
				this.text = this.m_OriginalText;
			}

			// Token: 0x06000C38 RID: 3128 RVA: 0x00031B9E File Offset: 0x0002FD9E
			public void SelectAll()
			{
				TextEditorEngine editorEngine = this.editorEngine;
				if (editorEngine != null)
				{
					editorEngine.SelectAll();
				}
			}

			// Token: 0x06000C39 RID: 3129 RVA: 0x00031BB3 File Offset: 0x0002FDB3
			internal void SelectNone()
			{
				TextEditorEngine editorEngine = this.editorEngine;
				if (editorEngine != null)
				{
					editorEngine.SelectNone();
				}
			}

			// Token: 0x06000C3A RID: 3130 RVA: 0x00031BC8 File Offset: 0x0002FDC8
			private void UpdateText(string value)
			{
				bool flag = this.text != value;
				if (flag)
				{
					using (InputEvent pooled = InputEvent.GetPooled(this.text, value))
					{
						pooled.target = base.parent;
						this.text = value;
						VisualElement parent = base.parent;
						if (parent != null)
						{
							parent.SendEvent(pooled);
						}
					}
				}
			}

			// Token: 0x06000C3B RID: 3131 RVA: 0x00031C3C File Offset: 0x0002FE3C
			protected virtual TValueType StringToValue(string str)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000C3C RID: 3132 RVA: 0x00031C44 File Offset: 0x0002FE44
			internal void UpdateValueFromText()
			{
				TValueType tvalueType = this.StringToValue(this.text);
				TextInputBaseField<TValueType> textInputBaseField = (TextInputBaseField<TValueType>)base.parent;
				textInputBaseField.value = tvalueType;
			}

			// Token: 0x06000C3D RID: 3133 RVA: 0x00031C74 File Offset: 0x0002FE74
			internal void UpdateTextFromValue()
			{
				TextInputBaseField<TValueType> textInputBaseField = (TextInputBaseField<TValueType>)base.parent;
				this.text = textInputBaseField.ValueToString(textInputBaseField.rawValue);
			}

			// Token: 0x17000272 RID: 626
			// (get) Token: 0x06000C3E RID: 3134 RVA: 0x00031CA4 File Offset: 0x0002FEA4
			public int cursorIndex
			{
				get
				{
					return this.editorEngine.cursorIndex;
				}
			}

			// Token: 0x17000273 RID: 627
			// (get) Token: 0x06000C3F RID: 3135 RVA: 0x00031CC4 File Offset: 0x0002FEC4
			public int selectIndex
			{
				get
				{
					return this.editorEngine.selectIndex;
				}
			}

			// Token: 0x17000274 RID: 628
			// (get) Token: 0x06000C40 RID: 3136 RVA: 0x00031CE1 File Offset: 0x0002FEE1
			bool ITextInputField.isReadOnly
			{
				get
				{
					return this.isReadOnly || !base.enabledInHierarchy;
				}
			}

			// Token: 0x17000275 RID: 629
			// (get) Token: 0x06000C41 RID: 3137 RVA: 0x00031CF7 File Offset: 0x0002FEF7
			// (set) Token: 0x06000C42 RID: 3138 RVA: 0x00031CFF File Offset: 0x0002FEFF
			public bool isReadOnly { get; set; }

			// Token: 0x17000276 RID: 630
			// (get) Token: 0x06000C43 RID: 3139 RVA: 0x00031D08 File Offset: 0x0002FF08
			// (set) Token: 0x06000C44 RID: 3140 RVA: 0x00031D10 File Offset: 0x0002FF10
			public int maxLength { get; set; }

			// Token: 0x17000277 RID: 631
			// (get) Token: 0x06000C45 RID: 3141 RVA: 0x00031D19 File Offset: 0x0002FF19
			// (set) Token: 0x06000C46 RID: 3142 RVA: 0x00031D21 File Offset: 0x0002FF21
			public char maskChar { get; set; }

			// Token: 0x17000278 RID: 632
			// (get) Token: 0x06000C47 RID: 3143 RVA: 0x00031D2A File Offset: 0x0002FF2A
			// (set) Token: 0x06000C48 RID: 3144 RVA: 0x00031D32 File Offset: 0x0002FF32
			public virtual bool isPasswordField { get; set; }

			// Token: 0x17000279 RID: 633
			// (get) Token: 0x06000C49 RID: 3145 RVA: 0x00031D3B File Offset: 0x0002FF3B
			// (set) Token: 0x06000C4A RID: 3146 RVA: 0x00031D43 File Offset: 0x0002FF43
			public bool doubleClickSelectsWord { get; set; }

			// Token: 0x1700027A RID: 634
			// (get) Token: 0x06000C4B RID: 3147 RVA: 0x00031D4C File Offset: 0x0002FF4C
			// (set) Token: 0x06000C4C RID: 3148 RVA: 0x00031D54 File Offset: 0x0002FF54
			public bool tripleClickSelectsLine { get; set; }

			// Token: 0x1700027B RID: 635
			// (get) Token: 0x06000C4D RID: 3149 RVA: 0x00031D5D File Offset: 0x0002FF5D
			// (set) Token: 0x06000C4E RID: 3150 RVA: 0x00031D65 File Offset: 0x0002FF65
			internal bool isDelayed { get; set; }

			// Token: 0x1700027C RID: 636
			// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00031D6E File Offset: 0x0002FF6E
			// (set) Token: 0x06000C50 RID: 3152 RVA: 0x00031D76 File Offset: 0x0002FF76
			internal bool isDragging { get; set; }

			// Token: 0x1700027D RID: 637
			// (get) Token: 0x06000C51 RID: 3153 RVA: 0x00031D80 File Offset: 0x0002FF80
			private bool touchScreenTextField
			{
				get
				{
					return TouchScreenKeyboard.isSupported && !TouchScreenKeyboard.isInPlaceEditingAllowed;
				}
			}

			// Token: 0x1700027E RID: 638
			// (get) Token: 0x06000C52 RID: 3154 RVA: 0x00031DA4 File Offset: 0x0002FFA4
			private bool touchScreenTextFieldChanged
			{
				get
				{
					return this.m_TouchScreenTextFieldInitialized != this.touchScreenTextField;
				}
			}

			// Token: 0x1700027F RID: 639
			// (get) Token: 0x06000C53 RID: 3155 RVA: 0x00031DC7 File Offset: 0x0002FFC7
			public Color selectionColor
			{
				get
				{
					return this.m_SelectionColor;
				}
			}

			// Token: 0x17000280 RID: 640
			// (get) Token: 0x06000C54 RID: 3156 RVA: 0x00031DCF File Offset: 0x0002FFCF
			public Color cursorColor
			{
				get
				{
					return this.m_CursorColor;
				}
			}

			// Token: 0x17000281 RID: 641
			// (get) Token: 0x06000C55 RID: 3157 RVA: 0x00031DD8 File Offset: 0x0002FFD8
			internal bool hasFocus
			{
				get
				{
					return base.elementPanel != null && base.elementPanel.focusController.GetLeafFocusedElement() == this;
				}
			}

			// Token: 0x17000282 RID: 642
			// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00031E08 File Offset: 0x00030008
			// (set) Token: 0x06000C57 RID: 3159 RVA: 0x00031E10 File Offset: 0x00030010
			internal TextEditorEventHandler editorEventHandler { get; private set; }

			// Token: 0x17000283 RID: 643
			// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00031E19 File Offset: 0x00030019
			// (set) Token: 0x06000C59 RID: 3161 RVA: 0x00031E21 File Offset: 0x00030021
			internal TextEditorEngine editorEngine { get; private set; }

			// Token: 0x17000284 RID: 644
			// (get) Token: 0x06000C5A RID: 3162 RVA: 0x00031E2C File Offset: 0x0003002C
			// (set) Token: 0x06000C5B RID: 3163 RVA: 0x00031E44 File Offset: 0x00030044
			public string text
			{
				get
				{
					return this.m_Text;
				}
				set
				{
					bool flag = this.m_Text == value;
					if (!flag)
					{
						this.m_Text = value;
						this.editorEngine.text = value;
						base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
					}
				}
			}

			// Token: 0x06000C5C RID: 3164 RVA: 0x00031E84 File Offset: 0x00030084
			internal TextInputBase()
			{
				this.isReadOnly = false;
				base.focusable = true;
				base.AddToClassList(TextInputBaseField<TValueType>.inputUssClassName);
				base.AddToClassList(TextInputBaseField<TValueType>.singleLineInputUssClassName);
				this.m_Text = string.Empty;
				base.name = TextInputBaseField<string>.textInputUssName;
				base.requireMeasureFunction = true;
				this.editorEngine = new TextEditorEngine(new TextEditorEngine.OnDetectFocusChangeFunction(this.OnDetectFocusChange), new TextEditorEngine.OnIndexChangeFunction(this.OnCursorIndexChange));
				this.editorEngine.style.richText = false;
				this.InitTextEditorEventHandler();
				this.editorEngine.style = new GUIStyle(this.editorEngine.style);
				base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnInputCustomStyleResolved), TrickleDown.NoTrickleDown);
				base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
				base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
			}

			// Token: 0x06000C5D RID: 3165 RVA: 0x00031FA0 File Offset: 0x000301A0
			private void InitTextEditorEventHandler()
			{
				this.m_TouchScreenTextFieldInitialized = this.touchScreenTextField;
				bool touchScreenTextFieldInitialized = this.m_TouchScreenTextFieldInitialized;
				if (touchScreenTextFieldInitialized)
				{
					this.editorEventHandler = new TouchScreenTextEditorEventHandler(this.editorEngine, this);
				}
				else
				{
					this.doubleClickSelectsWord = true;
					this.tripleClickSelectsLine = true;
					this.editorEventHandler = new KeyboardTextEditorEventHandler(this.editorEngine, this);
				}
			}

			// Token: 0x06000C5E RID: 3166 RVA: 0x00032000 File Offset: 0x00030200
			private DropdownMenuAction.Status CutActionStatus(DropdownMenuAction a)
			{
				return (base.enabledInHierarchy && this.editorEngine.hasSelection && !this.isPasswordField) ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled;
			}

			// Token: 0x06000C5F RID: 3167 RVA: 0x00032034 File Offset: 0x00030234
			private DropdownMenuAction.Status CopyActionStatus(DropdownMenuAction a)
			{
				return ((!base.enabledInHierarchy || this.editorEngine.hasSelection) && !this.isPasswordField) ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled;
			}

			// Token: 0x06000C60 RID: 3168 RVA: 0x00032068 File Offset: 0x00030268
			private DropdownMenuAction.Status PasteActionStatus(DropdownMenuAction a)
			{
				return base.enabledInHierarchy ? (this.editorEngine.CanPaste() ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled) : DropdownMenuAction.Status.Hidden;
			}

			// Token: 0x06000C61 RID: 3169 RVA: 0x00032098 File Offset: 0x00030298
			private void ProcessMenuCommand(string command)
			{
				using (ExecuteCommandEvent pooled = CommandEventBase<ExecuteCommandEvent>.GetPooled(command))
				{
					pooled.target = this;
					this.SendEvent(pooled);
				}
			}

			// Token: 0x06000C62 RID: 3170 RVA: 0x000320DC File Offset: 0x000302DC
			private void Cut(DropdownMenuAction a)
			{
				this.ProcessMenuCommand("Cut");
			}

			// Token: 0x06000C63 RID: 3171 RVA: 0x000320EB File Offset: 0x000302EB
			private void Copy(DropdownMenuAction a)
			{
				this.ProcessMenuCommand("Copy");
			}

			// Token: 0x06000C64 RID: 3172 RVA: 0x000320FA File Offset: 0x000302FA
			private void Paste(DropdownMenuAction a)
			{
				this.ProcessMenuCommand("Paste");
			}

			// Token: 0x06000C65 RID: 3173 RVA: 0x0003210C File Offset: 0x0003030C
			internal void OnInputCustomStyleResolved(CustomStyleResolvedEvent e)
			{
				Color clear = Color.clear;
				Color clear2 = Color.clear;
				ICustomStyle customStyle = e.customStyle;
				bool flag = customStyle.TryGetValue(TextInputBaseField<TValueType>.s_SelectionColorProperty, out clear);
				if (flag)
				{
					this.m_SelectionColor = clear;
				}
				bool flag2 = customStyle.TryGetValue(TextInputBaseField<TValueType>.s_CursorColorProperty, out clear2);
				if (flag2)
				{
					this.m_CursorColor = clear2;
				}
				TextInputBaseField<TValueType>.TextInputBase.SyncGUIStyle(this, this.editorEngine.style);
			}

			// Token: 0x06000C66 RID: 3174 RVA: 0x00032171 File Offset: 0x00030371
			private void OnAttachToPanel(AttachToPanelEvent attachEvent)
			{
				this.m_TextHandle = ((attachEvent.destinationPanel.contextType == ContextType.Editor) ? TextNativeHandle.New() : TextCoreHandle.New());
			}

			// Token: 0x06000C67 RID: 3175 RVA: 0x00032194 File Offset: 0x00030394
			internal virtual void SyncTextEngine()
			{
				this.editorEngine.text = this.CullString(this.text);
				this.editorEngine.SaveBackup();
				this.editorEngine.position = base.layout;
				this.editorEngine.DetectFocusChange();
			}

			// Token: 0x06000C68 RID: 3176 RVA: 0x000321E4 File Offset: 0x000303E4
			internal string CullString(string s)
			{
				bool flag = this.maxLength >= 0 && s != null && s.Length > this.maxLength;
				string text;
				if (flag)
				{
					text = s.Substring(0, this.maxLength);
				}
				else
				{
					text = s;
				}
				return text;
			}

			// Token: 0x06000C69 RID: 3177 RVA: 0x00032228 File Offset: 0x00030428
			internal void OnGenerateVisualContent(MeshGenerationContext mgc)
			{
				string text = this.text;
				bool isPasswordField = this.isPasswordField;
				if (isPasswordField)
				{
					text = "".PadRight(this.text.Length, this.maskChar);
				}
				bool touchScreenTextFieldInitialized = this.m_TouchScreenTextFieldInitialized;
				if (touchScreenTextFieldInitialized)
				{
					TouchScreenTextEditorEventHandler touchScreenTextEditorEventHandler = this.editorEventHandler as TouchScreenTextEditorEventHandler;
					bool flag = touchScreenTextEditorEventHandler != null;
					if (flag)
					{
						mgc.Text(MeshGenerationContextUtils.TextParams.MakeStyleBased(this, text), this.m_TextHandle, base.scaledPixelsPerPoint);
					}
				}
				else
				{
					bool flag2 = !this.hasFocus;
					if (flag2)
					{
						mgc.Text(MeshGenerationContextUtils.TextParams.MakeStyleBased(this, text), this.m_TextHandle, base.scaledPixelsPerPoint);
					}
					else
					{
						this.DrawWithTextSelectionAndCursor(mgc, text, base.scaledPixelsPerPoint);
					}
				}
			}

			// Token: 0x06000C6A RID: 3178 RVA: 0x000322E8 File Offset: 0x000304E8
			internal void DrawWithTextSelectionAndCursor(MeshGenerationContext mgc, string newText, float pixelsPerPoint)
			{
				Color color = ((base.panel.contextType == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white);
				KeyboardTextEditorEventHandler keyboardTextEditorEventHandler = this.editorEventHandler as KeyboardTextEditorEventHandler;
				bool flag = keyboardTextEditorEventHandler == null;
				if (!flag)
				{
					keyboardTextEditorEventHandler.PreDrawCursor(newText);
					int cursorIndex = this.editorEngine.cursorIndex;
					int selectIndex = this.editorEngine.selectIndex;
					Vector2 scrollOffset = this.editorEngine.scrollOffset;
					float num = TextUtilities.ComputeTextScaling(base.worldTransform, pixelsPerPoint);
					MeshGenerationContextUtils.TextParams textParams = MeshGenerationContextUtils.TextParams.MakeStyleBased(this, " ");
					float num2 = this.m_TextHandle.GetLineHeight(0, textParams, num, pixelsPerPoint);
					float num3 = 0f;
					bool flag2 = this.editorEngine.multiline && base.resolvedStyle.whiteSpace == WhiteSpace.Normal;
					if (flag2)
					{
						num3 = base.contentRect.width;
					}
					Vector2 vector = this.editorEngine.graphicalCursorPos - scrollOffset;
					vector.y += num2;
					GUIUtility.compositionCursorPos = this.LocalToWorld(vector);
					int num4 = (string.IsNullOrEmpty(GUIUtility.compositionString) ? selectIndex : (cursorIndex + GUIUtility.compositionString.Length));
					bool flag3 = cursorIndex != num4 && !this.isDragging;
					if (flag3)
					{
						int num5 = ((cursorIndex < num4) ? cursorIndex : num4);
						int num6 = ((cursorIndex > num4) ? cursorIndex : num4);
						CursorPositionStylePainterParameters cursorPositionStylePainterParameters = CursorPositionStylePainterParameters.GetDefault(this, this.text);
						cursorPositionStylePainterParameters.text = this.editorEngine.text;
						cursorPositionStylePainterParameters.wordWrapWidth = num3;
						cursorPositionStylePainterParameters.cursorIndex = num5;
						Vector2 vector2 = this.m_TextHandle.GetCursorPosition(cursorPositionStylePainterParameters, num);
						cursorPositionStylePainterParameters.cursorIndex = num6;
						Vector2 vector3 = this.m_TextHandle.GetCursorPosition(cursorPositionStylePainterParameters, num);
						vector2 -= scrollOffset;
						vector3 -= scrollOffset;
						num2 = this.m_TextHandle.GetLineHeight(cursorIndex, textParams, num, pixelsPerPoint);
						bool flag4 = Mathf.Approximately(vector2.y, vector3.y);
						if (flag4)
						{
							mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
							{
								rect = new Rect(vector2.x, vector2.y, vector3.x - vector2.x, num2),
								color = this.selectionColor,
								playmodeTintColor = color
							});
						}
						else
						{
							mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
							{
								rect = new Rect(vector2.x, vector2.y, base.contentRect.xMax - vector2.x, num2),
								color = this.selectionColor,
								playmodeTintColor = color
							});
							float num7 = vector3.y - vector2.y - num2;
							bool flag5 = num7 > 0f;
							if (flag5)
							{
								mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
								{
									rect = new Rect(base.contentRect.xMin, vector2.y + num2, base.contentRect.width, num7),
									color = this.selectionColor,
									playmodeTintColor = color
								});
							}
							bool flag6 = vector3.x != base.contentRect.x;
							if (flag6)
							{
								mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
								{
									rect = new Rect(base.contentRect.xMin, vector3.y, vector3.x, num2),
									color = this.selectionColor,
									playmodeTintColor = color
								});
							}
						}
					}
					bool flag7 = !string.IsNullOrEmpty(this.editorEngine.text) && base.contentRect.width > 0f && base.contentRect.height > 0f;
					if (flag7)
					{
						textParams.rect = new Rect(base.contentRect.x - scrollOffset.x, base.contentRect.y - scrollOffset.y, base.contentRect.width + scrollOffset.x, base.contentRect.height + scrollOffset.y);
						textParams.text = this.editorEngine.text;
						mgc.Text(textParams, this.m_TextHandle, base.scaledPixelsPerPoint);
					}
					bool flag8 = !this.isReadOnly && !this.isDragging;
					if (flag8)
					{
						bool flag9 = cursorIndex == num4 && TextUtilities.IsFontAssigned(this);
						if (flag9)
						{
							CursorPositionStylePainterParameters cursorPositionStylePainterParameters = CursorPositionStylePainterParameters.GetDefault(this, this.text);
							cursorPositionStylePainterParameters.text = this.editorEngine.text;
							cursorPositionStylePainterParameters.wordWrapWidth = num3;
							cursorPositionStylePainterParameters.cursorIndex = cursorIndex;
							Vector2 vector4 = this.m_TextHandle.GetCursorPosition(cursorPositionStylePainterParameters, num);
							vector4 -= scrollOffset;
							mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
							{
								rect = new Rect(vector4.x, vector4.y, 1f, num2),
								color = this.cursorColor,
								playmodeTintColor = color
							});
						}
						bool flag10 = this.editorEngine.altCursorPosition != -1;
						if (flag10)
						{
							CursorPositionStylePainterParameters cursorPositionStylePainterParameters = CursorPositionStylePainterParameters.GetDefault(this, this.text);
							cursorPositionStylePainterParameters.text = this.editorEngine.text.Substring(0, this.editorEngine.altCursorPosition);
							cursorPositionStylePainterParameters.wordWrapWidth = num3;
							cursorPositionStylePainterParameters.cursorIndex = this.editorEngine.altCursorPosition;
							Vector2 vector5 = this.m_TextHandle.GetCursorPosition(cursorPositionStylePainterParameters, num);
							vector5 -= scrollOffset;
							mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
							{
								rect = new Rect(vector5.x, vector5.y, 1f, num2),
								color = this.cursorColor,
								playmodeTintColor = color
							});
						}
					}
					keyboardTextEditorEventHandler.PostDrawCursor();
				}
			}

			// Token: 0x06000C6B RID: 3179 RVA: 0x00032908 File Offset: 0x00030B08
			internal virtual bool AcceptCharacter(char c)
			{
				return !this.isReadOnly && base.enabledInHierarchy;
			}

			// Token: 0x06000C6C RID: 3180 RVA: 0x0003292C File Offset: 0x00030B2C
			protected virtual void BuildContextualMenu(ContextualMenuPopulateEvent evt)
			{
				bool flag = ((evt != null) ? evt.target : null) is TextInputBaseField<TValueType>.TextInputBase;
				if (flag)
				{
					bool flag2 = !this.isReadOnly;
					if (flag2)
					{
						evt.menu.AppendAction("Cut", new Action<DropdownMenuAction>(this.Cut), new Func<DropdownMenuAction, DropdownMenuAction.Status>(this.CutActionStatus), null);
					}
					evt.menu.AppendAction("Copy", new Action<DropdownMenuAction>(this.Copy), new Func<DropdownMenuAction, DropdownMenuAction.Status>(this.CopyActionStatus), null);
					bool flag3 = !this.isReadOnly;
					if (flag3)
					{
						evt.menu.AppendAction("Paste", new Action<DropdownMenuAction>(this.Paste), new Func<DropdownMenuAction, DropdownMenuAction.Status>(this.PasteActionStatus), null);
					}
				}
			}

			// Token: 0x06000C6D RID: 3181 RVA: 0x000329F4 File Offset: 0x00030BF4
			private void OnDetectFocusChange()
			{
				bool flag = this.editorEngine.m_HasFocus && !this.hasFocus;
				if (flag)
				{
					this.editorEngine.OnFocus();
				}
				bool flag2 = !this.editorEngine.m_HasFocus && this.hasFocus;
				if (flag2)
				{
					this.editorEngine.OnLostFocus();
				}
			}

			// Token: 0x06000C6E RID: 3182 RVA: 0x0000DDFB File Offset: 0x0000BFFB
			private void OnCursorIndexChange()
			{
				base.IncrementVersion(VersionChangeType.Repaint);
			}

			// Token: 0x06000C6F RID: 3183 RVA: 0x00032A58 File Offset: 0x00030C58
			protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
			{
				string text = this.m_Text;
				bool flag = string.IsNullOrEmpty(text);
				if (flag)
				{
					text = " ";
				}
				return TextUtilities.MeasureVisualElementTextSize(this, text, desiredWidth, widthMode, desiredHeight, heightMode, this.m_TextHandle);
			}

			// Token: 0x06000C70 RID: 3184 RVA: 0x00032A95 File Offset: 0x00030C95
			internal override void ExecuteDefaultActionDisabledAtTarget(EventBase evt)
			{
				base.ExecuteDefaultActionDisabledAtTarget(evt);
				this.ProcessEventAtTarget(evt);
			}

			// Token: 0x06000C71 RID: 3185 RVA: 0x00032AA8 File Offset: 0x00030CA8
			protected override void ExecuteDefaultActionAtTarget(EventBase evt)
			{
				base.ExecuteDefaultActionAtTarget(evt);
				this.ProcessEventAtTarget(evt);
			}

			// Token: 0x06000C72 RID: 3186 RVA: 0x00032ABC File Offset: 0x00030CBC
			private void ProcessEventAtTarget(EventBase evt)
			{
				BaseVisualElementPanel elementPanel = base.elementPanel;
				if (elementPanel != null)
				{
					ContextualMenuManager contextualMenuManager = elementPanel.contextualMenuManager;
					if (contextualMenuManager != null)
					{
						contextualMenuManager.DisplayMenuIfEventMatches(evt, this);
					}
				}
				long? num = ((evt != null) ? new long?(evt.eventTypeId) : default(long?));
				long num2 = EventBase<ContextualMenuPopulateEvent>.TypeId();
				bool flag = (num.GetValueOrDefault() == num2) & (num != null);
				if (flag)
				{
					ContextualMenuPopulateEvent contextualMenuPopulateEvent = evt as ContextualMenuPopulateEvent;
					int count = contextualMenuPopulateEvent.menu.MenuItems().Count;
					this.BuildContextualMenu(contextualMenuPopulateEvent);
					bool flag2 = count > 0 && contextualMenuPopulateEvent.menu.MenuItems().Count > count;
					if (flag2)
					{
						contextualMenuPopulateEvent.menu.InsertSeparator(null, count);
					}
				}
				else
				{
					bool flag3 = evt.eventTypeId == EventBase<FocusInEvent>.TypeId();
					if (flag3)
					{
						this.SaveValueAndText();
						bool touchScreenTextFieldChanged = this.touchScreenTextFieldChanged;
						if (touchScreenTextFieldChanged)
						{
							this.InitTextEditorEventHandler();
						}
						bool flag4 = this.m_HardwareKeyboardPoller == null;
						if (flag4)
						{
							this.m_HardwareKeyboardPoller = base.schedule.Execute(delegate
							{
								bool touchScreenTextFieldChanged2 = this.touchScreenTextFieldChanged;
								if (touchScreenTextFieldChanged2)
								{
									this.InitTextEditorEventHandler();
									this.Blur();
								}
							}).Every(250L);
						}
						else
						{
							this.m_HardwareKeyboardPoller.Resume();
						}
					}
					else
					{
						bool flag5 = evt.eventTypeId == EventBase<FocusOutEvent>.TypeId();
						if (flag5)
						{
							bool flag6 = this.m_HardwareKeyboardPoller != null;
							if (flag6)
							{
								this.m_HardwareKeyboardPoller.Pause();
							}
						}
						else
						{
							bool flag7 = evt.eventTypeId == EventBase<KeyDownEvent>.TypeId();
							if (flag7)
							{
								KeyDownEvent keyDownEvent = evt as KeyDownEvent;
								bool flag8 = keyDownEvent != null && keyDownEvent.keyCode == KeyCode.Escape;
								if (flag8)
								{
									this.RestoreValueAndText();
									base.parent.Focus();
								}
							}
						}
					}
				}
				this.editorEventHandler.ExecuteDefaultActionAtTarget(evt);
			}

			// Token: 0x06000C73 RID: 3187 RVA: 0x00032C82 File Offset: 0x00030E82
			protected override void ExecuteDefaultAction(EventBase evt)
			{
				base.ExecuteDefaultAction(evt);
				this.editorEventHandler.ExecuteDefaultAction(evt);
			}

			// Token: 0x17000285 RID: 645
			// (get) Token: 0x06000C74 RID: 3188 RVA: 0x00032C9A File Offset: 0x00030E9A
			bool ITextInputField.hasFocus
			{
				get
				{
					return this.hasFocus;
				}
			}

			// Token: 0x06000C75 RID: 3189 RVA: 0x00032CA2 File Offset: 0x00030EA2
			void ITextInputField.SyncTextEngine()
			{
				this.SyncTextEngine();
			}

			// Token: 0x06000C76 RID: 3190 RVA: 0x00032CAC File Offset: 0x00030EAC
			bool ITextInputField.AcceptCharacter(char c)
			{
				return this.AcceptCharacter(c);
			}

			// Token: 0x06000C77 RID: 3191 RVA: 0x00032CC8 File Offset: 0x00030EC8
			string ITextInputField.CullString(string s)
			{
				return this.CullString(s);
			}

			// Token: 0x06000C78 RID: 3192 RVA: 0x00032CE1 File Offset: 0x00030EE1
			void ITextInputField.UpdateText(string value)
			{
				this.UpdateText(value);
			}

			// Token: 0x17000286 RID: 646
			// (get) Token: 0x06000C79 RID: 3193 RVA: 0x00032CEC File Offset: 0x00030EEC
			TextEditorEngine ITextInputField.editorEngine
			{
				get
				{
					return this.editorEngine;
				}
			}

			// Token: 0x17000287 RID: 647
			// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00032CF4 File Offset: 0x00030EF4
			bool ITextInputField.isDelayed
			{
				get
				{
					return this.isDelayed;
				}
			}

			// Token: 0x06000C7B RID: 3195 RVA: 0x00032CFC File Offset: 0x00030EFC
			void ITextInputField.UpdateValueFromText()
			{
				this.UpdateValueFromText();
			}

			// Token: 0x06000C7C RID: 3196 RVA: 0x00032D06 File Offset: 0x00030F06
			private void DeferGUIStyleRectSync()
			{
				base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnPercentResolved), TrickleDown.NoTrickleDown);
			}

			// Token: 0x06000C7D RID: 3197 RVA: 0x00032D20 File Offset: 0x00030F20
			private void OnPercentResolved(GeometryChangedEvent evt)
			{
				base.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnPercentResolved), TrickleDown.NoTrickleDown);
				GUIStyle style = this.editorEngine.style;
				int num = (int)base.resolvedStyle.marginLeft;
				int num2 = (int)base.resolvedStyle.marginTop;
				int num3 = (int)base.resolvedStyle.marginRight;
				int num4 = (int)base.resolvedStyle.marginBottom;
				TextInputBaseField<TValueType>.TextInputBase.AssignRect(style.margin, num, num2, num3, num4);
				num = (int)base.resolvedStyle.paddingLeft;
				num2 = (int)base.resolvedStyle.paddingTop;
				num3 = (int)base.resolvedStyle.paddingRight;
				num4 = (int)base.resolvedStyle.paddingBottom;
				TextInputBaseField<TValueType>.TextInputBase.AssignRect(style.padding, num, num2, num3, num4);
			}

			// Token: 0x06000C7E RID: 3198 RVA: 0x00032DDC File Offset: 0x00030FDC
			private unsafe static void SyncGUIStyle(TextInputBaseField<TValueType>.TextInputBase textInput, GUIStyle style)
			{
				ComputedStyle computedStyle = *textInput.computedStyle;
				style.alignment = computedStyle.unityTextAlign;
				style.wordWrap = computedStyle.whiteSpace == WhiteSpace.Normal;
				style.clipping = ((computedStyle.overflow == OverflowInternal.Visible) ? TextClipping.Overflow : TextClipping.Clip);
				style.font = TextUtilities.GetFont(textInput);
				style.fontSize = (int)computedStyle.fontSize.value;
				style.fontStyle = computedStyle.unityFontStyleAndWeight;
				int num = computedStyle.unitySliceLeft;
				int num2 = computedStyle.unitySliceTop;
				int num3 = computedStyle.unitySliceRight;
				int num4 = computedStyle.unitySliceBottom;
				TextInputBaseField<TValueType>.TextInputBase.AssignRect(style.border, num, num2, num3, num4);
				bool flag = TextInputBaseField<TValueType>.TextInputBase.IsLayoutUsingPercent(textInput);
				if (flag)
				{
					textInput.DeferGUIStyleRectSync();
				}
				else
				{
					num = (int)computedStyle.marginLeft.value;
					num2 = (int)computedStyle.marginTop.value;
					num3 = (int)computedStyle.marginRight.value;
					num4 = (int)computedStyle.marginBottom.value;
					TextInputBaseField<TValueType>.TextInputBase.AssignRect(style.margin, num, num2, num3, num4);
					num = (int)computedStyle.paddingLeft.value;
					num2 = (int)computedStyle.paddingTop.value;
					num3 = (int)computedStyle.paddingRight.value;
					num4 = (int)computedStyle.paddingBottom.value;
					TextInputBaseField<TValueType>.TextInputBase.AssignRect(style.padding, num, num2, num3, num4);
				}
			}

			// Token: 0x06000C7F RID: 3199 RVA: 0x00032F6C File Offset: 0x0003116C
			private unsafe static bool IsLayoutUsingPercent(VisualElement ve)
			{
				ComputedStyle computedStyle = *ve.computedStyle;
				bool flag = computedStyle.marginLeft.unit == LengthUnit.Percent || computedStyle.marginTop.unit == LengthUnit.Percent || computedStyle.marginRight.unit == LengthUnit.Percent || computedStyle.marginBottom.unit == LengthUnit.Percent;
				bool flag2;
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					bool flag3 = computedStyle.paddingLeft.unit == LengthUnit.Percent || computedStyle.paddingTop.unit == LengthUnit.Percent || computedStyle.paddingRight.unit == LengthUnit.Percent || computedStyle.paddingBottom.unit == LengthUnit.Percent;
					flag2 = flag3;
				}
				return flag2;
			}

			// Token: 0x06000C80 RID: 3200 RVA: 0x00033033 File Offset: 0x00031233
			private static void AssignRect(RectOffset rect, int left, int top, int right, int bottom)
			{
				rect.left = left;
				rect.top = top;
				rect.right = right;
				rect.bottom = bottom;
			}

			// Token: 0x040005AD RID: 1453
			private string m_OriginalText;

			// Token: 0x040005B6 RID: 1462
			private bool m_TouchScreenTextFieldInitialized;

			// Token: 0x040005B7 RID: 1463
			private IVisualElementScheduledItem m_HardwareKeyboardPoller = null;

			// Token: 0x040005B8 RID: 1464
			private Color m_SelectionColor = Color.clear;

			// Token: 0x040005B9 RID: 1465
			private Color m_CursorColor = Color.grey;

			// Token: 0x040005BC RID: 1468
			private ITextHandle m_TextHandle;

			// Token: 0x040005BD RID: 1469
			private string m_Text;
		}
	}
}
