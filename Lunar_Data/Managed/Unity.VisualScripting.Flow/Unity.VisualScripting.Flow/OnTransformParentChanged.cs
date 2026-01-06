using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000079 RID: 121
	[UnitCategory("Events/Hierarchy")]
	public sealed class OnTransformParentChanged : GameObjectEventUnit<EmptyEventArgs>
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000936C File Offset: 0x0000756C
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnTransformParentChangedMessageListener);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00009378 File Offset: 0x00007578
		protected override string hookName
		{
			get
			{
				return "OnTransformParentChanged";
			}
		}
	}
}
