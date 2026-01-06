using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000085 RID: 133
	[UnitCategory("Events/Input")]
	public sealed class OnMouseUp : GameObjectEventUnit<EmptyEventArgs>, IMouseEventUnit
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0000962D File Offset: 0x0000782D
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnMouseUpMessageListener);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00009639 File Offset: 0x00007839
		protected override string hookName
		{
			get
			{
				return "OnMouseUp";
			}
		}
	}
}
