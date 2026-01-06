using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020000B2 RID: 178
	public class TemplateContainer : BindableElement
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x000163D8 File Offset: 0x000145D8
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x000163E0 File Offset: 0x000145E0
		public string templateId { get; private set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x000163E9 File Offset: 0x000145E9
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x000163F1 File Offset: 0x000145F1
		public VisualTreeAsset templateSource
		{
			get
			{
				return this.m_TemplateSource;
			}
			internal set
			{
				this.m_TemplateSource = value;
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x000163FA File Offset: 0x000145FA
		public TemplateContainer()
			: this(null)
		{
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00016405 File Offset: 0x00014605
		public TemplateContainer(string templateId)
		{
			this.templateId = templateId;
			this.m_ContentContainer = this;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00016420 File Offset: 0x00014620
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00016438 File Offset: 0x00014638
		internal void SetContentContainer(VisualElement content)
		{
			this.m_ContentContainer = content;
		}

		// Token: 0x0400025C RID: 604
		private VisualElement m_ContentContainer;

		// Token: 0x0400025D RID: 605
		private VisualTreeAsset m_TemplateSource;

		// Token: 0x020000B3 RID: 179
		public new class UxmlFactory : UxmlFactory<TemplateContainer, TemplateContainer.UxmlTraits>
		{
			// Token: 0x17000161 RID: 353
			// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00016442 File Offset: 0x00014642
			public override string uxmlName
			{
				get
				{
					return "Instance";
				}
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x060005FA RID: 1530 RVA: 0x00016449 File Offset: 0x00014649
			public override string uxmlQualifiedName
			{
				get
				{
					return this.uxmlNamespace + "." + this.uxmlName;
				}
			}

			// Token: 0x0400025E RID: 606
			internal const string k_ElementName = "Instance";
		}

		// Token: 0x020000B4 RID: 180
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x17000163 RID: 355
			// (get) Token: 0x060005FC RID: 1532 RVA: 0x0001646C File Offset: 0x0001466C
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x060005FD RID: 1533 RVA: 0x0001648C File Offset: 0x0001468C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				TemplateContainer templateContainer = (TemplateContainer)ve;
				templateContainer.templateId = this.m_Template.GetValueFromBag(bag, cc);
				VisualTreeAsset visualTreeAsset = cc.visualTreeAsset;
				VisualTreeAsset visualTreeAsset2 = ((visualTreeAsset != null) ? visualTreeAsset.ResolveTemplate(templateContainer.templateId) : null);
				bool flag = visualTreeAsset2 == null;
				if (flag)
				{
					templateContainer.Add(new Label(string.Format("Unknown Template: '{0}'", templateContainer.templateId)));
				}
				else
				{
					TemplateAsset templateAsset = bag as TemplateAsset;
					List<TemplateAsset.AttributeOverride> list = ((templateAsset != null) ? templateAsset.attributeOverrides : null);
					List<TemplateAsset.AttributeOverride> attributeOverrides = cc.attributeOverrides;
					List<TemplateAsset.AttributeOverride> list2 = null;
					bool flag2 = list != null || attributeOverrides != null;
					if (flag2)
					{
						list2 = new List<TemplateAsset.AttributeOverride>();
						bool flag3 = attributeOverrides != null;
						if (flag3)
						{
							list2.AddRange(attributeOverrides);
						}
						bool flag4 = list != null;
						if (flag4)
						{
							list2.AddRange(list);
						}
					}
					visualTreeAsset2.CloneTree(ve, cc.slotInsertionPoints, list2);
				}
				bool flag5 = visualTreeAsset2 == null;
				if (flag5)
				{
					Debug.LogErrorFormat("Could not resolve template with name '{0}'", new object[] { templateContainer.templateId });
				}
			}

			// Token: 0x0400025F RID: 607
			internal const string k_TemplateAttributeName = "template";

			// Token: 0x04000260 RID: 608
			private UxmlStringAttributeDescription m_Template = new UxmlStringAttributeDescription
			{
				name = "template",
				use = UxmlAttributeDescription.Use.Required
			};
		}
	}
}
