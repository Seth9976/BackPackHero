using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002BD RID: 701
	public class UxmlRootElementTraits : UxmlTraits
	{
		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x0005FDD4 File Offset: 0x0005DFD4
		public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield return new UxmlChildElementDescription(typeof(VisualElement));
				yield break;
			}
		}

		// Token: 0x04000A07 RID: 2567
		protected UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
		{
			name = "name"
		};

		// Token: 0x04000A08 RID: 2568
		private UxmlStringAttributeDescription m_Class = new UxmlStringAttributeDescription
		{
			name = "class"
		};
	}
}
