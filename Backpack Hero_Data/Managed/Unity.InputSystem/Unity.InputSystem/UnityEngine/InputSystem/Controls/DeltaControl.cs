using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.Scripting;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000110 RID: 272
	[Preserve]
	public class DeltaControl : Vector2Control
	{
		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x0004BB6E File Offset: 0x00049D6E
		// (set) Token: 0x06000F99 RID: 3993 RVA: 0x0004BB76 File Offset: 0x00049D76
		[InputControl(useStateFrom = "y", parameters = "clamp=1,clampMin=0,clampMax=3.402823E+38", synthetic = true, displayName = "Up")]
		[Preserve]
		public AxisControl up { get; set; }

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x0004BB7F File Offset: 0x00049D7F
		// (set) Token: 0x06000F9B RID: 3995 RVA: 0x0004BB87 File Offset: 0x00049D87
		[InputControl(useStateFrom = "y", parameters = "clamp=1,clampMin=-3.402823E+38,clampMax=0,invert", synthetic = true, displayName = "Down")]
		[Preserve]
		public AxisControl down { get; set; }

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x0004BB90 File Offset: 0x00049D90
		// (set) Token: 0x06000F9D RID: 3997 RVA: 0x0004BB98 File Offset: 0x00049D98
		[InputControl(useStateFrom = "x", parameters = "clamp=1,clampMin=-3.402823E+38,clampMax=0,invert", synthetic = true, displayName = "Left")]
		[Preserve]
		public AxisControl left { get; set; }

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x0004BBA1 File Offset: 0x00049DA1
		// (set) Token: 0x06000F9F RID: 3999 RVA: 0x0004BBA9 File Offset: 0x00049DA9
		[InputControl(useStateFrom = "x", parameters = "clamp=1,clampMin=0,clampMax=3.402823E+38", synthetic = true, displayName = "Right")]
		[Preserve]
		public AxisControl right { get; set; }

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0004BBB4 File Offset: 0x00049DB4
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.up = base.GetChildControl<AxisControl>("up");
			this.down = base.GetChildControl<AxisControl>("down");
			this.left = base.GetChildControl<AxisControl>("left");
			this.right = base.GetChildControl<AxisControl>("right");
		}
	}
}
