using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200013C RID: 316
	public class GroupBox : BindableElement, IGroupBox
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x00028995 File Offset: 0x00026B95
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x000289AC File Offset: 0x00026BAC
		public string text
		{
			get
			{
				Label titleLabel = this.m_TitleLabel;
				return (titleLabel != null) ? titleLabel.text : null;
			}
			set
			{
				bool flag = !string.IsNullOrEmpty(value);
				if (flag)
				{
					bool flag2 = this.m_TitleLabel == null;
					if (flag2)
					{
						this.m_TitleLabel = new Label(value);
						this.m_TitleLabel.AddToClassList(GroupBox.labelUssClassName);
						base.Insert(0, this.m_TitleLabel);
					}
					this.m_TitleLabel.text = value;
				}
				else
				{
					bool flag3 = this.m_TitleLabel != null;
					if (flag3)
					{
						this.m_TitleLabel.RemoveFromHierarchy();
						this.m_TitleLabel = null;
					}
				}
			}
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00028A34 File Offset: 0x00026C34
		public GroupBox()
			: this(null)
		{
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00028A3F File Offset: 0x00026C3F
		public GroupBox(string text)
		{
			base.AddToClassList(GroupBox.ussClassName);
			this.text = text;
		}

		// Token: 0x0400046B RID: 1131
		public static readonly string ussClassName = "unity-group-box";

		// Token: 0x0400046C RID: 1132
		public static readonly string labelUssClassName = GroupBox.ussClassName + "__label";

		// Token: 0x0400046D RID: 1133
		private Label m_TitleLabel;

		// Token: 0x0200013D RID: 317
		public new class UxmlFactory : UxmlFactory<GroupBox, GroupBox.UxmlTraits>
		{
		}

		// Token: 0x0200013E RID: 318
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x06000A52 RID: 2642 RVA: 0x00028A86 File Offset: 0x00026C86
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((GroupBox)ve).text = this.m_Text.GetValueFromBag(bag, cc);
			}

			// Token: 0x0400046E RID: 1134
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};
		}
	}
}
