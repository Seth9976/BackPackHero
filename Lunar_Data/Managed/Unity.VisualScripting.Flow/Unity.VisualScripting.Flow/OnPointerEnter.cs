using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200006D RID: 109
	[UnitCategory("Events/GUI")]
	[UnitOrder(14)]
	public sealed class OnPointerEnter : PointerEventUnit
	{
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000910A File Offset: 0x0000730A
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnPointerEnterMessageListener);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00009116 File Offset: 0x00007316
		protected override string hookName
		{
			get
			{
				return "OnPointerEnter";
			}
		}
	}
}
