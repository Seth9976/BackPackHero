using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000094 RID: 148
	public sealed class OnCollisionStay : CollisionEventUnit
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00009936 File Offset: 0x00007B36
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnCollisionStayMessageListener);
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00009942 File Offset: 0x00007B42
		protected override string hookName
		{
			get
			{
				return "OnCollisionStay";
			}
		}
	}
}
