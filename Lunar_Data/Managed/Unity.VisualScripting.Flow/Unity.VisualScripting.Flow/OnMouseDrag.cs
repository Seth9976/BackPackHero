using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000080 RID: 128
	[UnitCategory("Events/Input")]
	public sealed class OnMouseDrag : GameObjectEventUnit<EmptyEventArgs>, IMouseEventUnit
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000950D File Offset: 0x0000770D
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnMouseDragMessageListener);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00009519 File Offset: 0x00007719
		protected override string hookName
		{
			get
			{
				return "OnMouseDrag";
			}
		}
	}
}
