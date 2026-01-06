using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000078 RID: 120
	[UnitCategory("Events/Hierarchy")]
	public sealed class OnTransformChildrenChanged : GameObjectEventUnit<EmptyEventArgs>
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00009351 File Offset: 0x00007551
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnTransformChildrenChangedMessageListener);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000935D File Offset: 0x0000755D
		protected override string hookName
		{
			get
			{
				return "OnTransformChildrenChanged";
			}
		}
	}
}
