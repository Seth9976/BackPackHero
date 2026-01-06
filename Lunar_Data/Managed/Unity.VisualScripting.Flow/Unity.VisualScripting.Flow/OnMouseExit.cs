using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000082 RID: 130
	[UnitCategory("Events/Input")]
	public sealed class OnMouseExit : GameObjectEventUnit<EmptyEventArgs>, IMouseEventUnit
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00009543 File Offset: 0x00007743
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnMouseExitMessageListener);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000954F File Offset: 0x0000774F
		protected override string hookName
		{
			get
			{
				return "OnMouseExit";
			}
		}
	}
}
