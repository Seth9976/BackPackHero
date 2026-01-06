using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000127 RID: 295
	public class Box : VisualElement
	{
		// Token: 0x060009D9 RID: 2521 RVA: 0x000268CD File Offset: 0x00024ACD
		public Box()
		{
			base.AddToClassList(Box.ussClassName);
		}

		// Token: 0x04000427 RID: 1063
		public static readonly string ussClassName = "unity-box";

		// Token: 0x02000128 RID: 296
		public new class UxmlFactory : UxmlFactory<Box>
		{
		}
	}
}
