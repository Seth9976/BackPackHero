using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002C3 RID: 707
	public class UxmlTemplateTraits : UxmlTraits
	{
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x000600C8 File Offset: 0x0005E2C8
		public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x04000A16 RID: 2582
		private UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
		{
			name = "name",
			use = UxmlAttributeDescription.Use.Required
		};

		// Token: 0x04000A17 RID: 2583
		private UxmlStringAttributeDescription m_Path = new UxmlStringAttributeDescription
		{
			name = "path"
		};

		// Token: 0x04000A18 RID: 2584
		private UxmlStringAttributeDescription m_Src = new UxmlStringAttributeDescription
		{
			name = "src"
		};
	}
}
