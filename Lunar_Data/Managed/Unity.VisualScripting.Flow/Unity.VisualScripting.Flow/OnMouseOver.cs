using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000084 RID: 132
	[UnitCategory("Events/Input")]
	public sealed class OnMouseOver : GameObjectEventUnit<EmptyEventArgs>, IMouseEventUnit
	{
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00009612 File Offset: 0x00007812
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnMouseOverMessageListener);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000961E File Offset: 0x0000781E
		protected override string hookName
		{
			get
			{
				return "OnMouseOver";
			}
		}
	}
}
