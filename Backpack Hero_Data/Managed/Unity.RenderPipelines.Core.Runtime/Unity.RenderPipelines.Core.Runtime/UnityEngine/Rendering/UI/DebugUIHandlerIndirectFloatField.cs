using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000F7 RID: 247
	public class DebugUIHandlerIndirectFloatField : DebugUIHandlerWidget
	{
		// Token: 0x06000745 RID: 1861 RVA: 0x00020883 File Offset: 0x0001EA83
		public void Init()
		{
			this.UpdateValueLabel();
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0002088B File Offset: 0x0001EA8B
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x000208B0 File Offset: 0x0001EAB0
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000208D4 File Offset: 0x0001EAD4
		public override void OnIncrement(bool fast)
		{
			this.ChangeValue(fast, 1f);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x000208E2 File Offset: 0x0001EAE2
		public override void OnDecrement(bool fast)
		{
			this.ChangeValue(fast, -1f);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x000208F0 File Offset: 0x0001EAF0
		private void ChangeValue(bool fast, float multiplier)
		{
			float num = this.getter();
			num += this.incStepGetter() * (fast ? this.incStepMultGetter() : 1f) * multiplier;
			this.setter(num);
			this.UpdateValueLabel();
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00020944 File Offset: 0x0001EB44
		private void UpdateValueLabel()
		{
			if (this.valueLabel != null)
			{
				this.valueLabel.text = this.getter().ToString("N" + this.decimalsGetter().ToString());
			}
		}

		// Token: 0x040003FE RID: 1022
		public Text nameLabel;

		// Token: 0x040003FF RID: 1023
		public Text valueLabel;

		// Token: 0x04000400 RID: 1024
		public Func<float> getter;

		// Token: 0x04000401 RID: 1025
		public Action<float> setter;

		// Token: 0x04000402 RID: 1026
		public Func<float> incStepGetter;

		// Token: 0x04000403 RID: 1027
		public Func<float> incStepMultGetter;

		// Token: 0x04000404 RID: 1028
		public Func<float> decimalsGetter;
	}
}
