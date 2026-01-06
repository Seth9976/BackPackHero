using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000100 RID: 256
	public class DebugUIHandlerUIntField : DebugUIHandlerWidget
	{
		// Token: 0x0600077D RID: 1917 RVA: 0x00021413 File Offset: 0x0001F613
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.UIntField>();
			this.nameLabel.text = this.m_Field.displayName;
			this.UpdateValueLabel();
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00021444 File Offset: 0x0001F644
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00021469 File Offset: 0x0001F669
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0002148D File Offset: 0x0001F68D
		public override void OnIncrement(bool fast)
		{
			this.ChangeValue(fast, 1);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00021497 File Offset: 0x0001F697
		public override void OnDecrement(bool fast)
		{
			this.ChangeValue(fast, -1);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x000214A4 File Offset: 0x0001F6A4
		private void ChangeValue(bool fast, int multiplier)
		{
			long num = (long)((ulong)this.m_Field.GetValue());
			if (num == 0L && multiplier < 0)
			{
				return;
			}
			num += (long)((ulong)(this.m_Field.incStep * (fast ? this.m_Field.intStepMult : 1U)) * (ulong)((long)multiplier));
			this.m_Field.SetValue((uint)num);
			this.UpdateValueLabel();
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00021500 File Offset: 0x0001F700
		private void UpdateValueLabel()
		{
			if (this.valueLabel != null)
			{
				this.valueLabel.text = this.m_Field.GetValue().ToString("N0");
			}
		}

		// Token: 0x04000427 RID: 1063
		public Text nameLabel;

		// Token: 0x04000428 RID: 1064
		public Text valueLabel;

		// Token: 0x04000429 RID: 1065
		private DebugUI.UIntField m_Field;
	}
}
