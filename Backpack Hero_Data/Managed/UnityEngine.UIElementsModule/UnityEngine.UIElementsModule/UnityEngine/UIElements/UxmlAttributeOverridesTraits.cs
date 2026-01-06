using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002C6 RID: 710
	public class UxmlAttributeOverridesTraits : UxmlTraits
	{
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x00060228 File Offset: 0x0005E428
		public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x04000A1E RID: 2590
		internal const string k_ElementNameAttributeName = "element-name";

		// Token: 0x04000A1F RID: 2591
		private UxmlStringAttributeDescription m_ElementName = new UxmlStringAttributeDescription
		{
			name = "element-name",
			use = UxmlAttributeDescription.Use.Required
		};
	}
}
