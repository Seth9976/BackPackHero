using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200009E RID: 158
	public sealed class OnCollisionExit2D : CollisionEvent2DUnit
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00009DF7 File Offset: 0x00007FF7
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnCollisionExit2DMessageListener);
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00009E03 File Offset: 0x00008003
		protected override string hookName
		{
			get
			{
				return "OnCollisionExit2D";
			}
		}
	}
}
