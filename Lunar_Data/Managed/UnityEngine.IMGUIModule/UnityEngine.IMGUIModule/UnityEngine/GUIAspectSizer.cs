using System;

namespace UnityEngine
{
	// Token: 0x02000035 RID: 53
	internal sealed class GUIAspectSizer : GUILayoutEntry
	{
		// Token: 0x060003E9 RID: 1001 RVA: 0x0000CF70 File Offset: 0x0000B170
		public GUIAspectSizer(float aspect, GUILayoutOption[] options)
			: base(0f, 0f, 0f, 0f, GUIStyle.none)
		{
			this.aspect = aspect;
			this.ApplyOptions(options);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
		public override void CalcHeight()
		{
			this.minHeight = (this.maxHeight = this.rect.width / this.aspect);
		}

		// Token: 0x040000FA RID: 250
		private float aspect;
	}
}
