using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000093 RID: 147
	public sealed class OnCollisionExit : CollisionEventUnit
	{
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000991B File Offset: 0x00007B1B
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnCollisionExitMessageListener);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00009927 File Offset: 0x00007B27
		protected override string hookName
		{
			get
			{
				return "OnCollisionExit";
			}
		}
	}
}
