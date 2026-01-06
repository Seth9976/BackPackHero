using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000050 RID: 80
	[UnitCategory("Events/Animation")]
	public sealed class OnAnimatorMove : GameObjectEventUnit<EmptyEventArgs>
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600033E RID: 830 RVA: 0x000087EA File Offset: 0x000069EA
		public override Type MessageListenerType
		{
			get
			{
				return typeof(AnimatorMessageListener);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600033F RID: 831 RVA: 0x000087F6 File Offset: 0x000069F6
		protected override string hookName
		{
			get
			{
				return "OnAnimatorMove";
			}
		}
	}
}
