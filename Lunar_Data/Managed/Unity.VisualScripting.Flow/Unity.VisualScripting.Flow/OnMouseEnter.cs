using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000081 RID: 129
	[UnitCategory("Events/Input")]
	public sealed class OnMouseEnter : GameObjectEventUnit<EmptyEventArgs>, IMouseEventUnit
	{
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00009528 File Offset: 0x00007728
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnMouseEnterMessageListener);
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00009534 File Offset: 0x00007734
		protected override string hookName
		{
			get
			{
				return "OnMouseEnter";
			}
		}
	}
}
