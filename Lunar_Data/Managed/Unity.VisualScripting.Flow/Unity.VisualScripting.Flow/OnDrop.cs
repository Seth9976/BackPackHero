using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000064 RID: 100
	[UnitCategory("Events/GUI")]
	[TypeIcon(typeof(OnDrag))]
	[UnitOrder(19)]
	public sealed class OnDrop : PointerEventUnit
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00008EF0 File Offset: 0x000070F0
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnDropMessageListener);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00008EFC File Offset: 0x000070FC
		protected override string hookName
		{
			get
			{
				return "OnDrop";
			}
		}
	}
}
