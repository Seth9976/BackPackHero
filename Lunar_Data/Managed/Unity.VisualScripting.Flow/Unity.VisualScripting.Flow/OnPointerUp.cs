using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200006F RID: 111
	[UnitCategory("Events/GUI")]
	[UnitOrder(13)]
	public sealed class OnPointerUp : PointerEventUnit
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00009140 File Offset: 0x00007340
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnPointerUpMessageListener);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000914C File Offset: 0x0000734C
		protected override string hookName
		{
			get
			{
				return "OnPointerUp";
			}
		}
	}
}
