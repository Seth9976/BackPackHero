using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000F3 RID: 243
	public class DebugUIHandlerFloatField : DebugUIHandlerWidget
	{
		// Token: 0x0600072C RID: 1836 RVA: 0x0002030C File Offset: 0x0001E50C
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.FloatField>();
			this.nameLabel.text = this.m_Field.displayName;
			this.UpdateValueLabel();
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0002033D File Offset: 0x0001E53D
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00020362 File Offset: 0x0001E562
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00020386 File Offset: 0x0001E586
		public override void OnIncrement(bool fast)
		{
			this.ChangeValue(fast, 1f);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00020394 File Offset: 0x0001E594
		public override void OnDecrement(bool fast)
		{
			this.ChangeValue(fast, -1f);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x000203A4 File Offset: 0x0001E5A4
		private void ChangeValue(bool fast, float multiplier)
		{
			float num = this.m_Field.GetValue();
			num += this.m_Field.incStep * (fast ? this.m_Field.incStepMult : 1f) * multiplier;
			this.m_Field.SetValue(num);
			this.UpdateValueLabel();
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x000203F8 File Offset: 0x0001E5F8
		private void UpdateValueLabel()
		{
			this.valueLabel.text = this.m_Field.GetValue().ToString("N" + this.m_Field.decimals.ToString());
		}

		// Token: 0x040003F0 RID: 1008
		public Text nameLabel;

		// Token: 0x040003F1 RID: 1009
		public Text valueLabel;

		// Token: 0x040003F2 RID: 1010
		private DebugUI.FloatField m_Field;
	}
}
