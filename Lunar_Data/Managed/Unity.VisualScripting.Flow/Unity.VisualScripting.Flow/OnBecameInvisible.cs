using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000A5 RID: 165
	[UnitCategory("Events/Rendering")]
	public sealed class OnBecameInvisible : GameObjectEventUnit<EmptyEventArgs>
	{
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000A046 File Offset: 0x00008246
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnBecameInvisibleMessageListener);
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0000A052 File Offset: 0x00008252
		protected override string hookName
		{
			get
			{
				return "OnBecameInvisible";
			}
		}
	}
}
