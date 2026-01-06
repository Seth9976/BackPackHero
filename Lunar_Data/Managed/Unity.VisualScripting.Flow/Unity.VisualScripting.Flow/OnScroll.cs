using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000070 RID: 112
	[UnitCategory("Events/GUI")]
	[UnitOrder(20)]
	public sealed class OnScroll : PointerEventUnit
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000915B File Offset: 0x0000735B
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnScrollMessageListener);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00009167 File Offset: 0x00007367
		protected override string hookName
		{
			get
			{
				return "OnScroll";
			}
		}
	}
}
