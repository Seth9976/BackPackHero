using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000FA RID: 250
	public class DebugUIHandlerMessageBox : DebugUIHandlerWidget
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x00020BB8 File Offset: 0x0001EDB8
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.MessageBox>();
			this.nameLabel.text = this.m_Field.displayName;
			Image component = base.GetComponent<Image>();
			DebugUI.MessageBox.Style style = this.m_Field.style;
			if (style == DebugUI.MessageBox.Style.Warning)
			{
				component.color = DebugUIHandlerMessageBox.k_WarningBackgroundColor;
				return;
			}
			if (style != DebugUI.MessageBox.Style.Error)
			{
				return;
			}
			component.color = DebugUIHandlerMessageBox.k_ErrorBackgroundColor;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00020C2B File Offset: 0x0001EE2B
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			return false;
		}

		// Token: 0x0400040E RID: 1038
		public Text nameLabel;

		// Token: 0x0400040F RID: 1039
		private DebugUI.MessageBox m_Field;

		// Token: 0x04000410 RID: 1040
		private static Color32 k_WarningBackgroundColor = new Color32(231, 180, 3, 30);

		// Token: 0x04000411 RID: 1041
		private static Color32 k_WarningTextColor = new Color32(231, 180, 3, byte.MaxValue);

		// Token: 0x04000412 RID: 1042
		private static Color32 k_ErrorBackgroundColor = new Color32(231, 75, 3, 30);

		// Token: 0x04000413 RID: 1043
		private static Color32 k_ErrorTextColor = new Color32(231, 75, 3, byte.MaxValue);
	}
}
