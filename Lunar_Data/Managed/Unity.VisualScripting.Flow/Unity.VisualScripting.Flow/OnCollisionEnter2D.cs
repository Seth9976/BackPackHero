using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200009D RID: 157
	public sealed class OnCollisionEnter2D : CollisionEvent2DUnit
	{
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00009DDC File Offset: 0x00007FDC
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnCollisionEnter2DMessageListener);
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00009DE8 File Offset: 0x00007FE8
		protected override string hookName
		{
			get
			{
				return "OnCollisionEnter2D";
			}
		}
	}
}
