using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200007F RID: 127
	[UnitCategory("Events/Input")]
	public sealed class OnMouseDown : GameObjectEventUnit<EmptyEventArgs>, IMouseEventUnit
	{
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x000094F2 File Offset: 0x000076F2
		protected override string hookName
		{
			get
			{
				return "OnMouseDown";
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x000094F9 File Offset: 0x000076F9
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnMouseDownMessageListener);
			}
		}
	}
}
