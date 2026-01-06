using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200009F RID: 159
	public sealed class OnCollisionStay2D : CollisionEvent2DUnit
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00009E12 File Offset: 0x00008012
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnCollisionStay2DMessageListener);
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00009E1E File Offset: 0x0000801E
		protected override string hookName
		{
			get
			{
				return "OnCollisionStay2D";
			}
		}
	}
}
