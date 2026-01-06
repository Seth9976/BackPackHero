using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200009A RID: 154
	public sealed class OnTriggerStay : TriggerEventUnit
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00009C4D File Offset: 0x00007E4D
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnTriggerStayMessageListener);
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00009C59 File Offset: 0x00007E59
		protected override string hookName
		{
			get
			{
				return "OnTriggerStay";
			}
		}
	}
}
