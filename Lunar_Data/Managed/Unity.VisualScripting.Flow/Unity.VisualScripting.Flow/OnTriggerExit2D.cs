using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000A2 RID: 162
	public sealed class OnTriggerExit2D : TriggerEvent2DUnit
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00009FCF File Offset: 0x000081CF
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnTriggerExit2DMessageListener);
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00009FDB File Offset: 0x000081DB
		protected override string hookName
		{
			get
			{
				return "OnTriggerExit2D";
			}
		}
	}
}
