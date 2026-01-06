using System;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000117 RID: 279
	public class StickControl : Vector2Control
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x0004C4FB File Offset: 0x0004A6FB
		// (set) Token: 0x06000FCE RID: 4046 RVA: 0x0004C503 File Offset: 0x0004A703
		[InputControl(useStateFrom = "y", processors = "axisDeadzone", parameters = "clamp=2,clampMin=0,clampMax=1", synthetic = true, displayName = "Up")]
		[InputControl(name = "x", minValue = -1f, maxValue = 1f, layout = "Axis", processors = "axisDeadzone")]
		[InputControl(name = "y", minValue = -1f, maxValue = 1f, layout = "Axis", processors = "axisDeadzone")]
		public ButtonControl up { get; set; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0004C50C File Offset: 0x0004A70C
		// (set) Token: 0x06000FD0 RID: 4048 RVA: 0x0004C514 File Offset: 0x0004A714
		[InputControl(useStateFrom = "y", processors = "axisDeadzone", parameters = "clamp=2,clampMin=-1,clampMax=0,invert", synthetic = true, displayName = "Down")]
		public ButtonControl down { get; set; }

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x0004C51D File Offset: 0x0004A71D
		// (set) Token: 0x06000FD2 RID: 4050 RVA: 0x0004C525 File Offset: 0x0004A725
		[InputControl(useStateFrom = "x", processors = "axisDeadzone", parameters = "clamp=2,clampMin=-1,clampMax=0,invert", synthetic = true, displayName = "Left")]
		public ButtonControl left { get; set; }

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x0004C52E File Offset: 0x0004A72E
		// (set) Token: 0x06000FD4 RID: 4052 RVA: 0x0004C536 File Offset: 0x0004A736
		[InputControl(useStateFrom = "x", processors = "axisDeadzone", parameters = "clamp=2,clampMin=0,clampMax=1", synthetic = true, displayName = "Right")]
		public ButtonControl right { get; set; }

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0004C540 File Offset: 0x0004A740
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.up = base.GetChildControl<ButtonControl>("up");
			this.down = base.GetChildControl<ButtonControl>("down");
			this.left = base.GetChildControl<ButtonControl>("left");
			this.right = base.GetChildControl<ButtonControl>("right");
		}
	}
}
