using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000066 RID: 102
	[UnitCategory("Events/GUI")]
	[TypeIcon(typeof(OnDrag))]
	[UnitOrder(18)]
	public sealed class OnEndDrag : PointerEventUnit
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00008FAE File Offset: 0x000071AE
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnEndDragMessageListener);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00008FBA File Offset: 0x000071BA
		protected override string hookName
		{
			get
			{
				return "OnEndDrag";
			}
		}
	}
}
