using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000A3 RID: 163
	public sealed class OnTriggerStay2D : TriggerEvent2DUnit
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00009FEA File Offset: 0x000081EA
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnTriggerStay2DMessageListener);
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00009FF6 File Offset: 0x000081F6
		protected override string hookName
		{
			get
			{
				return "OnTriggerStay2D";
			}
		}
	}
}
