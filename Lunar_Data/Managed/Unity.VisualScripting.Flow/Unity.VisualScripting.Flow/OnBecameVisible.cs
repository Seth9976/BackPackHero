using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000A6 RID: 166
	[UnitCategory("Events/Rendering")]
	public sealed class OnBecameVisible : GameObjectEventUnit<EmptyEventArgs>
	{
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0000A061 File Offset: 0x00008261
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnBecameVisibleMessageListener);
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000A06D File Offset: 0x0000826D
		protected override string hookName
		{
			get
			{
				return "OnBecameVisible";
			}
		}
	}
}
