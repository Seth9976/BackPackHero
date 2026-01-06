using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000098 RID: 152
	public sealed class OnTriggerEnter : TriggerEventUnit
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00009C17 File Offset: 0x00007E17
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnTriggerEnterMessageListener);
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x00009C23 File Offset: 0x00007E23
		protected override string hookName
		{
			get
			{
				return "OnTriggerEnter";
			}
		}
	}
}
