using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000A1 RID: 161
	public sealed class OnTriggerEnter2D : TriggerEvent2DUnit
	{
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00009FB4 File Offset: 0x000081B4
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnTriggerEnter2DMessageListener);
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00009FC0 File Offset: 0x000081C0
		protected override string hookName
		{
			get
			{
				return "OnTriggerEnter2D";
			}
		}
	}
}
