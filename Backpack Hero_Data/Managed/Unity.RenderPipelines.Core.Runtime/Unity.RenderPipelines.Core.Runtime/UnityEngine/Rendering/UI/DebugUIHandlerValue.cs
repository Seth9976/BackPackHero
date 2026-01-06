using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000101 RID: 257
	public class DebugUIHandlerValue : DebugUIHandlerWidget
	{
		// Token: 0x06000785 RID: 1925 RVA: 0x00021546 File Offset: 0x0001F746
		protected override void OnEnable()
		{
			this.m_Timer = 0f;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00021553 File Offset: 0x0001F753
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Value>();
			this.nameLabel.text = this.m_Field.displayName;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0002157E File Offset: 0x0001F77E
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x000215A3 File Offset: 0x0001F7A3
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x000215C8 File Offset: 0x0001F7C8
		private void Update()
		{
			if (this.m_Timer >= this.m_Field.refreshRate)
			{
				this.valueLabel.text = this.m_Field.GetValue().ToString();
				this.m_Timer -= this.m_Field.refreshRate;
			}
			this.m_Timer += Time.deltaTime;
		}

		// Token: 0x0400042A RID: 1066
		public Text nameLabel;

		// Token: 0x0400042B RID: 1067
		public Text valueLabel;

		// Token: 0x0400042C RID: 1068
		private DebugUI.Value m_Field;

		// Token: 0x0400042D RID: 1069
		private float m_Timer;
	}
}
