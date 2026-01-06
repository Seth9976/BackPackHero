using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200006E RID: 110
	[UnitCategory("Events/GUI")]
	[UnitOrder(15)]
	public sealed class OnPointerExit : PointerEventUnit
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00009125 File Offset: 0x00007325
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnPointerExitMessageListener);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060003CB RID: 971 RVA: 0x00009131 File Offset: 0x00007331
		protected override string hookName
		{
			get
			{
				return "OnPointerExit";
			}
		}
	}
}
