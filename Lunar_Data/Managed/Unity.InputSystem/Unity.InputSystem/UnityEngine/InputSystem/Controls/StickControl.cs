using System;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000117 RID: 279
	public class StickControl : Vector2Control
	{
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x0004C4AF File Offset: 0x0004A6AF
		// (set) Token: 0x06000FC9 RID: 4041 RVA: 0x0004C4B7 File Offset: 0x0004A6B7
		[InputControl(useStateFrom = "y", processors = "axisDeadzone", parameters = "clamp=2,clampMin=0,clampMax=1", synthetic = true, displayName = "Up")]
		[InputControl(name = "x", minValue = -1f, maxValue = 1f, layout = "Axis", processors = "axisDeadzone")]
		[InputControl(name = "y", minValue = -1f, maxValue = 1f, layout = "Axis", processors = "axisDeadzone")]
		public ButtonControl up { get; set; }

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x0004C4C0 File Offset: 0x0004A6C0
		// (set) Token: 0x06000FCB RID: 4043 RVA: 0x0004C4C8 File Offset: 0x0004A6C8
		[InputControl(useStateFrom = "y", processors = "axisDeadzone", parameters = "clamp=2,clampMin=-1,clampMax=0,invert", synthetic = true, displayName = "Down")]
		public ButtonControl down { get; set; }

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x0004C4D1 File Offset: 0x0004A6D1
		// (set) Token: 0x06000FCD RID: 4045 RVA: 0x0004C4D9 File Offset: 0x0004A6D9
		[InputControl(useStateFrom = "x", processors = "axisDeadzone", parameters = "clamp=2,clampMin=-1,clampMax=0,invert", synthetic = true, displayName = "Left")]
		public ButtonControl left { get; set; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x0004C4E2 File Offset: 0x0004A6E2
		// (set) Token: 0x06000FCF RID: 4047 RVA: 0x0004C4EA File Offset: 0x0004A6EA
		[InputControl(useStateFrom = "x", processors = "axisDeadzone", parameters = "clamp=2,clampMin=0,clampMax=1", synthetic = true, displayName = "Right")]
		public ButtonControl right { get; set; }

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0004C4F4 File Offset: 0x0004A6F4
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
