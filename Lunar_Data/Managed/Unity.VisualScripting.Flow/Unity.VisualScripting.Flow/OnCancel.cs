using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000061 RID: 97
	[UnitCategory("Events/GUI")]
	[UnitOrder(25)]
	public sealed class OnCancel : GenericGuiEventUnit
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00008E9F File Offset: 0x0000709F
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnCancelMessageListener);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00008EAB File Offset: 0x000070AB
		protected override string hookName
		{
			get
			{
				return "OnCancel";
			}
		}
	}
}
