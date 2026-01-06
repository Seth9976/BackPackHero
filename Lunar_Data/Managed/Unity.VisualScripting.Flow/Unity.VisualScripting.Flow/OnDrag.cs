using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000063 RID: 99
	[UnitCategory("Events/GUI")]
	[UnitOrder(17)]
	public sealed class OnDrag : PointerEventUnit
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00008ED5 File Offset: 0x000070D5
		protected override string hookName
		{
			get
			{
				return "OnDrag";
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00008EDC File Offset: 0x000070DC
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnDragMessageListener);
			}
		}
	}
}
