using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000092 RID: 146
	public sealed class OnCollisionEnter : CollisionEventUnit
	{
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00009900 File Offset: 0x00007B00
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnCollisionEnterMessageListener);
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000990C File Offset: 0x00007B0C
		protected override string hookName
		{
			get
			{
				return "OnCollisionEnter";
			}
		}
	}
}
