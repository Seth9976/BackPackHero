using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001DE RID: 478
	public class BlurEvent : FocusEventBase<BlurEvent>
	{
		// Token: 0x06000EF6 RID: 3830 RVA: 0x0003C2B8 File Offset: 0x0003A4B8
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool flag = base.relatedTarget == null;
			if (flag)
			{
				base.focusController.DoFocusChange(null);
			}
		}
	}
}
