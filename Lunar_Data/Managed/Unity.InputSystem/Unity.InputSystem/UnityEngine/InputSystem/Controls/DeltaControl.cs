using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.Scripting;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000110 RID: 272
	[Preserve]
	public class DeltaControl : Vector2Control
	{
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0004BB22 File Offset: 0x00049D22
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x0004BB2A File Offset: 0x00049D2A
		[InputControl(useStateFrom = "y", parameters = "clamp=1,clampMin=0,clampMax=3.402823E+38", synthetic = true, displayName = "Up")]
		[Preserve]
		public AxisControl up { get; set; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0004BB33 File Offset: 0x00049D33
		// (set) Token: 0x06000F96 RID: 3990 RVA: 0x0004BB3B File Offset: 0x00049D3B
		[InputControl(useStateFrom = "y", parameters = "clamp=1,clampMin=-3.402823E+38,clampMax=0,invert", synthetic = true, displayName = "Down")]
		[Preserve]
		public AxisControl down { get; set; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x0004BB44 File Offset: 0x00049D44
		// (set) Token: 0x06000F98 RID: 3992 RVA: 0x0004BB4C File Offset: 0x00049D4C
		[InputControl(useStateFrom = "x", parameters = "clamp=1,clampMin=-3.402823E+38,clampMax=0,invert", synthetic = true, displayName = "Left")]
		[Preserve]
		public AxisControl left { get; set; }

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x0004BB55 File Offset: 0x00049D55
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x0004BB5D File Offset: 0x00049D5D
		[InputControl(useStateFrom = "x", parameters = "clamp=1,clampMin=0,clampMax=3.402823E+38", synthetic = true, displayName = "Right")]
		[Preserve]
		public AxisControl right { get; set; }

		// Token: 0x06000F9B RID: 3995 RVA: 0x0004BB68 File Offset: 0x00049D68
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
