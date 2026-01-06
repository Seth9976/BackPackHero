using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200006B RID: 107
	[UnitCategory("Events/GUI")]
	[UnitOrder(11)]
	public sealed class OnPointerClick : PointerEventUnit
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x000090D4 File Offset: 0x000072D4
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnPointerClickMessageListener);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x000090E0 File Offset: 0x000072E0
		protected override string hookName
		{
			get
			{
				return "OnPointerClick";
			}
		}
	}
}
