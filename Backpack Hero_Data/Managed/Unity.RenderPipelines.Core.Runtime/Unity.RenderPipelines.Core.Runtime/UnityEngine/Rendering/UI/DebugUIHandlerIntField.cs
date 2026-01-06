using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000F9 RID: 249
	public class DebugUIHandlerIntField : DebugUIHandlerWidget
	{
		// Token: 0x06000754 RID: 1876 RVA: 0x00020A92 File Offset: 0x0001EC92
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.IntField>();
			this.nameLabel.text = this.m_Field.displayName;
			this.UpdateValueLabel();
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00020AC3 File Offset: 0x0001ECC3
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00020AE8 File Offset: 0x0001ECE8
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00020B0C File Offset: 0x0001ED0C
		public override void OnIncrement(bool fast)
		{
			this.ChangeValue(fast, 1);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00020B16 File Offset: 0x0001ED16
		public override void OnDecrement(bool fast)
		{
			this.ChangeValue(fast, -1);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00020B20 File Offset: 0x0001ED20
		private void ChangeValue(bool fast, int multiplier)
		{
			int num = this.m_Field.GetValue();
			num += this.m_Field.incStep * (fast ? this.m_Field.intStepMult : 1) * multiplier;
			this.m_Field.SetValue(num);
			this.UpdateValueLabel();
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00020B70 File Offset: 0x0001ED70
		private void UpdateValueLabel()
		{
			if (this.valueLabel != null)
			{
				this.valueLabel.text = this.m_Field.GetValue().ToString("N0");
			}
		}

		// Token: 0x0400040B RID: 1035
		public Text nameLabel;

		// Token: 0x0400040C RID: 1036
		public Text valueLabel;

		// Token: 0x0400040D RID: 1037
		private DebugUI.IntField m_Field;
	}
}
