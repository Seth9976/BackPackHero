using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002C0 RID: 704
	public class UxmlStyleTraits : UxmlTraits
	{
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x0005FF70 File Offset: 0x0005E170
		public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x04000A0E RID: 2574
		private UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
		{
			name = "name"
		};

		// Token: 0x04000A0F RID: 2575
		private UxmlStringAttributeDescription m_Path = new UxmlStringAttributeDescription
		{
			name = "path"
		};

		// Token: 0x04000A10 RID: 2576
		private UxmlStringAttributeDescription m_Src = new UxmlStringAttributeDescription
		{
			name = "src"
		};
	}
}
