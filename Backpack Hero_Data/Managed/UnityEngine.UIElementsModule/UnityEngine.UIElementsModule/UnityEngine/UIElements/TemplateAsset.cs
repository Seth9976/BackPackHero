using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x020002C8 RID: 712
	[Serializable]
	internal class TemplateAsset : VisualElementAsset
	{
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060017CD RID: 6093 RVA: 0x00060310 File Offset: 0x0005E510
		// (set) Token: 0x060017CE RID: 6094 RVA: 0x00060328 File Offset: 0x0005E528
		public string templateAlias
		{
			get
			{
				return this.m_TemplateAlias;
			}
			set
			{
				this.m_TemplateAlias = value;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060017CF RID: 6095 RVA: 0x00060334 File Offset: 0x0005E534
		// (set) Token: 0x060017D0 RID: 6096 RVA: 0x00060364 File Offset: 0x0005E564
		public List<TemplateAsset.AttributeOverride> attributeOverrides
		{
			get
			{
				return (this.m_AttributeOverrides == null) ? (this.m_AttributeOverrides = new List<TemplateAsset.AttributeOverride>()) : this.m_AttributeOverrides;
			}
			set
			{
				this.m_AttributeOverrides = value;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060017D1 RID: 6097 RVA: 0x00060370 File Offset: 0x0005E570
		// (set) Token: 0x060017D2 RID: 6098 RVA: 0x00060388 File Offset: 0x0005E588
		internal List<VisualTreeAsset.SlotUsageEntry> slotUsages
		{
			get
			{
				return this.m_SlotUsages;
			}
			set
			{
				this.m_SlotUsages = value;
			}
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x00060392 File Offset: 0x0005E592
		public TemplateAsset(string templateAlias, string fullTypeName)
			: base(fullTypeName)
		{
			Assert.IsFalse(string.IsNullOrEmpty(templateAlias), "Template alias must not be null or empty");
			this.m_TemplateAlias = templateAlias;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x000603B8 File Offset: 0x0005E5B8
		public void AddSlotUsage(string slotName, int resId)
		{
			bool flag = this.m_SlotUsages == null;
			if (flag)
			{
				this.m_SlotUsages = new List<VisualTreeAsset.SlotUsageEntry>();
			}
			this.m_SlotUsages.Add(new VisualTreeAsset.SlotUsageEntry(slotName, resId));
		}

		// Token: 0x04000A24 RID: 2596
		[SerializeField]
		private string m_TemplateAlias;

		// Token: 0x04000A25 RID: 2597
		[SerializeField]
		private List<TemplateAsset.AttributeOverride> m_AttributeOverrides;

		// Token: 0x04000A26 RID: 2598
		[SerializeField]
		private List<VisualTreeAsset.SlotUsageEntry> m_SlotUsages;

		// Token: 0x020002C9 RID: 713
		[Serializable]
		public struct AttributeOverride
		{
			// Token: 0x04000A27 RID: 2599
			public string m_ElementName;

			// Token: 0x04000A28 RID: 2600
			public string m_AttributeName;

			// Token: 0x04000A29 RID: 2601
			public string m_Value;
		}
	}
}
