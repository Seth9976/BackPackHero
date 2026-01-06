using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000075 RID: 117
	[UnitCategory("Events/GUI")]
	[UnitOrder(24)]
	public sealed class OnSubmit : GenericGuiEventUnit
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000929C File Offset: 0x0000749C
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnSubmitMessageListener);
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x000092A8 File Offset: 0x000074A8
		protected override string hookName
		{
			get
			{
				return "OnSubmit";
			}
		}
	}
}
