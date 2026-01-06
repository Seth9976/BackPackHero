using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x0200014D RID: 333
	[DisplayStringFormat("{up}/{left}/{down}/{right}")]
	[DisplayName("Up/Down/Left/Right Composite")]
	public class Vector2Composite : InputBindingComposite<Vector2>
	{
		// Token: 0x060011E9 RID: 4585 RVA: 0x0005454C File Offset: 0x0005274C
		public override Vector2 ReadValue(ref InputBindingCompositeContext context)
		{
			Vector2Composite.Mode mode = this.mode;
			if (mode == Vector2Composite.Mode.Analog)
			{
				float num = context.ReadValue<float>(this.up);
				float num2 = context.ReadValue<float>(this.down);
				float num3 = context.ReadValue<float>(this.left);
				float num4 = context.ReadValue<float>(this.right);
				return DpadControl.MakeDpadVector(num, num2, num3, num4);
			}
			bool flag = context.ReadValueAsButton(this.up);
			bool flag2 = context.ReadValueAsButton(this.down);
			bool flag3 = context.ReadValueAsButton(this.left);
			bool flag4 = context.ReadValueAsButton(this.right);
			if (!this.normalize)
			{
				mode = Vector2Composite.Mode.Digital;
			}
			return DpadControl.MakeDpadVector(flag, flag2, flag3, flag4, mode == Vector2Composite.Mode.DigitalNormalized);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x000545F0 File Offset: 0x000527F0
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			return this.ReadValue(ref context).magnitude;
		}

		// Token: 0x04000706 RID: 1798
		[InputControl(layout = "Axis")]
		public int up;

		// Token: 0x04000707 RID: 1799
		[InputControl(layout = "Axis")]
		public int down;

		// Token: 0x04000708 RID: 1800
		[InputControl(layout = "Axis")]
		public int left;

		// Token: 0x04000709 RID: 1801
		[InputControl(layout = "Axis")]
		public int right;

		// Token: 0x0400070A RID: 1802
		[Obsolete("Use Mode.DigitalNormalized with 'mode' instead")]
		public bool normalize = true;

		// Token: 0x0400070B RID: 1803
		public Vector2Composite.Mode mode;

		// Token: 0x0200024B RID: 587
		public enum Mode
		{
			// Token: 0x04000C41 RID: 3137
			Analog = 2,
			// Token: 0x04000C42 RID: 3138
			DigitalNormalized = 0,
			// Token: 0x04000C43 RID: 3139
			Digital
		}
	}
}
