using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000F8 RID: 248
	public class DebugUIHandlerIndirectToggle : DebugUIHandlerWidget
	{
		// Token: 0x0600074D RID: 1869 RVA: 0x000209A2 File Offset: 0x0001EBA2
		public void Init()
		{
			this.UpdateValueLabel();
			this.valueToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleValueChanged));
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x000209C6 File Offset: 0x0001EBC6
		private void OnToggleValueChanged(bool value)
		{
			this.setter(this.index, value);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x000209DA File Offset: 0x0001EBDA
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.checkmarkImage.color = this.colorSelected;
			return true;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x000209FF File Offset: 0x0001EBFF
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.checkmarkImage.color = this.colorDefault;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00020A24 File Offset: 0x0001EC24
		public override void OnAction()
		{
			bool flag = !this.getter(this.index);
			this.setter(this.index, flag);
			this.UpdateValueLabel();
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00020A5E File Offset: 0x0001EC5E
		internal void UpdateValueLabel()
		{
			if (this.valueToggle != null)
			{
				this.valueToggle.isOn = this.getter(this.index);
			}
		}

		// Token: 0x04000405 RID: 1029
		public Text nameLabel;

		// Token: 0x04000406 RID: 1030
		public Toggle valueToggle;

		// Token: 0x04000407 RID: 1031
		public Image checkmarkImage;

		// Token: 0x04000408 RID: 1032
		public Func<int, bool> getter;

		// Token: 0x04000409 RID: 1033
		public Action<int, bool> setter;

		// Token: 0x0400040A RID: 1034
		internal int index;
	}
}
