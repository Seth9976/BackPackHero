using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000099 RID: 153
	public sealed class OnTriggerExit : TriggerEventUnit
	{
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00009C32 File Offset: 0x00007E32
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnTriggerExitMessageListener);
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00009C3E File Offset: 0x00007E3E
		protected override string hookName
		{
			get
			{
				return "OnTriggerExit";
			}
		}
	}
}
