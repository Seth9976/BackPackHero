using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000086 RID: 134
	[UnitCategory("Events/Input")]
	public sealed class OnMouseUpAsButton : GameObjectEventUnit<EmptyEventArgs>, IMouseEventUnit
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00009648 File Offset: 0x00007848
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnMouseUpAsButtonMessageListener);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00009654 File Offset: 0x00007854
		protected override string hookName
		{
			get
			{
				return "OnMouseUpAsButton";
			}
		}
	}
}
