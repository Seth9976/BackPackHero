using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200005F RID: 95
	[UnitCategory("Events/GUI")]
	[TypeIcon(typeof(OnDrag))]
	[UnitOrder(16)]
	public sealed class OnBeginDrag : PointerEventUnit
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00008E69 File Offset: 0x00007069
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnBeginDragMessageListener);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00008E75 File Offset: 0x00007075
		protected override string hookName
		{
			get
			{
				return "OnBeginDrag";
			}
		}
	}
}
