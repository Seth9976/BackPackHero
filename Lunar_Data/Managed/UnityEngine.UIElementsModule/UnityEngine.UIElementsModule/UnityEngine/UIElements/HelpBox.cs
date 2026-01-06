using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000140 RID: 320
	public class HelpBox : VisualElement
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x00028ACC File Offset: 0x00026CCC
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x00028AE9 File Offset: 0x00026CE9
		public string text
		{
			get
			{
				return this.m_Label.text;
			}
			set
			{
				this.m_Label.text = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00028AFC File Offset: 0x00026CFC
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x00028B14 File Offset: 0x00026D14
		public HelpBoxMessageType messageType
		{
			get
			{
				return this.m_HelpBoxMessageType;
			}
			set
			{
				bool flag = value != this.m_HelpBoxMessageType;
				if (flag)
				{
					this.m_HelpBoxMessageType = value;
					this.UpdateIcon(value);
				}
			}
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00028B43 File Offset: 0x00026D43
		public HelpBox()
			: this(string.Empty, HelpBoxMessageType.None)
		{
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00028B54 File Offset: 0x00026D54
		public HelpBox(string text, HelpBoxMessageType messageType)
		{
			base.AddToClassList(HelpBox.ussClassName);
			this.m_HelpBoxMessageType = messageType;
			this.m_Label = new Label(text);
			this.m_Label.AddToClassList(HelpBox.labelUssClassName);
			base.Add(this.m_Label);
			this.m_Icon = new VisualElement();
			this.m_Icon.AddToClassList(HelpBox.iconUssClassName);
			this.UpdateIcon(messageType);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00028BCC File Offset: 0x00026DCC
		private string GetIconClass(HelpBoxMessageType messageType)
		{
			string text;
			switch (messageType)
			{
			case HelpBoxMessageType.Info:
				text = HelpBox.iconInfoUssClassName;
				break;
			case HelpBoxMessageType.Warning:
				text = HelpBox.iconwarningUssClassName;
				break;
			case HelpBoxMessageType.Error:
				text = HelpBox.iconErrorUssClassName;
				break;
			default:
				text = null;
				break;
			}
			return text;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00028C14 File Offset: 0x00026E14
		private void UpdateIcon(HelpBoxMessageType messageType)
		{
			bool flag = !string.IsNullOrEmpty(this.m_IconClass);
			if (flag)
			{
				this.m_Icon.RemoveFromClassList(this.m_IconClass);
			}
			this.m_IconClass = this.GetIconClass(messageType);
			bool flag2 = this.m_IconClass == null;
			if (flag2)
			{
				this.m_Icon.RemoveFromHierarchy();
			}
			else
			{
				this.m_Icon.AddToClassList(this.m_IconClass);
				bool flag3 = this.m_Icon.parent == null;
				if (flag3)
				{
					base.Insert(0, this.m_Icon);
				}
			}
		}

		// Token: 0x04000474 RID: 1140
		public static readonly string ussClassName = "unity-help-box";

		// Token: 0x04000475 RID: 1141
		public static readonly string labelUssClassName = HelpBox.ussClassName + "__label";

		// Token: 0x04000476 RID: 1142
		public static readonly string iconUssClassName = HelpBox.ussClassName + "__icon";

		// Token: 0x04000477 RID: 1143
		public static readonly string iconInfoUssClassName = HelpBox.iconUssClassName + "--info";

		// Token: 0x04000478 RID: 1144
		public static readonly string iconwarningUssClassName = HelpBox.iconUssClassName + "--warning";

		// Token: 0x04000479 RID: 1145
		public static readonly string iconErrorUssClassName = HelpBox.iconUssClassName + "--error";

		// Token: 0x0400047A RID: 1146
		private HelpBoxMessageType m_HelpBoxMessageType;

		// Token: 0x0400047B RID: 1147
		private VisualElement m_Icon;

		// Token: 0x0400047C RID: 1148
		private string m_IconClass;

		// Token: 0x0400047D RID: 1149
		private Label m_Label;

		// Token: 0x02000141 RID: 321
		public new class UxmlFactory : UxmlFactory<HelpBox, HelpBox.UxmlTraits>
		{
		}

		// Token: 0x02000142 RID: 322
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x06000A5E RID: 2654 RVA: 0x00028D2C File Offset: 0x00026F2C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				HelpBox helpBox = ve as HelpBox;
				helpBox.text = this.m_Text.GetValueFromBag(bag, cc);
				helpBox.messageType = this.m_MessageType.GetValueFromBag(bag, cc);
			}

			// Token: 0x0400047E RID: 1150
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};

			// Token: 0x0400047F RID: 1151
			private UxmlEnumAttributeDescription<HelpBoxMessageType> m_MessageType = new UxmlEnumAttributeDescription<HelpBoxMessageType>
			{
				name = "message-type",
				defaultValue = HelpBoxMessageType.None
			};
		}
	}
}
