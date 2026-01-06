using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000EC RID: 236
	public class DebugUIHandlerButton : DebugUIHandlerWidget
	{
		// Token: 0x060006ED RID: 1773 RVA: 0x0001EFD8 File Offset: 0x0001D1D8
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Button>();
			this.nameLabel.text = this.m_Field.displayName;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001F003 File Offset: 0x0001D203
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001F017 File Offset: 0x0001D217
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001F02A File Offset: 0x0001D22A
		public override void OnAction()
		{
			if (this.m_Field.action != null)
			{
				this.m_Field.action();
			}
		}

		// Token: 0x040003D3 RID: 979
		public Text nameLabel;

		// Token: 0x040003D4 RID: 980
		private DebugUI.Button m_Field;
	}
}
