using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E0 RID: 480
	public class FocusEvent : FocusEventBase<FocusEvent>
	{
		// Token: 0x06000EFB RID: 3835 RVA: 0x0003C315 File Offset: 0x0003A515
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			base.focusController.DoFocusChange(base.target as Focusable);
		}
	}
}
