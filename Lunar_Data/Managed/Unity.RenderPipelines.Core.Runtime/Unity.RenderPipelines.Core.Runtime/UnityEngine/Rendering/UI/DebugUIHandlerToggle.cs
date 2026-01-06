using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000FE RID: 254
	public class DebugUIHandlerToggle : DebugUIHandlerWidget
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x00021160 File Offset: 0x0001F360
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.BoolField>();
			this.nameLabel.text = this.m_Field.displayName;
			this.UpdateValueLabel();
			this.valueToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleValueChanged));
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x000211B8 File Offset: 0x0001F3B8
		private void OnToggleValueChanged(bool value)
		{
			this.m_Field.SetValue(value);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000211C6 File Offset: 0x0001F3C6
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.checkmarkImage.color = this.colorSelected;
			return true;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x000211EB File Offset: 0x0001F3EB
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.checkmarkImage.color = this.colorDefault;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00021210 File Offset: 0x0001F410
		public override void OnAction()
		{
			bool flag = !this.m_Field.GetValue();
			this.m_Field.SetValue(flag);
			this.UpdateValueLabel();
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0002123E File Offset: 0x0001F43E
		protected internal virtual void UpdateValueLabel()
		{
			if (this.valueToggle != null)
			{
				this.valueToggle.isOn = this.m_Field.GetValue();
			}
		}

		// Token: 0x04000421 RID: 1057
		public Text nameLabel;

		// Token: 0x04000422 RID: 1058
		public Toggle valueToggle;

		// Token: 0x04000423 RID: 1059
		public Image checkmarkImage;

		// Token: 0x04000424 RID: 1060
		protected internal DebugUI.BoolField m_Field;
	}
}
