using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200006C RID: 108
	[UnitCategory("Events/GUI")]
	[UnitOrder(12)]
	public sealed class OnPointerDown : PointerEventUnit
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x000090EF File Offset: 0x000072EF
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnPointerDownMessageListener);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x000090FB File Offset: 0x000072FB
		protected override string hookName
		{
			get
			{
				return "OnPointerDown";
			}
		}
	}
}
