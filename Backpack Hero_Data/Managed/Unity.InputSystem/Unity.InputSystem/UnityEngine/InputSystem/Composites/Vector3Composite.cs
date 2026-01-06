using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x0200014E RID: 334
	[DisplayStringFormat("{up}+{down}/{left}+{right}/{forward}+{backward}")]
	[DisplayName("Up/Down/Left/Right/Forward/Backward Composite")]
	public class Vector3Composite : InputBindingComposite<Vector3>
	{
		// Token: 0x060011EC RID: 4588 RVA: 0x0005461C File Offset: 0x0005281C
		public override Vector3 ReadValue(ref InputBindingCompositeContext context)
		{
			if (this.mode == Vector3Composite.Mode.Analog)
			{
				float num = context.ReadValue<float>(this.up);
				float num2 = context.ReadValue<float>(this.down);
				float num3 = context.ReadValue<float>(this.left);
				float num4 = context.ReadValue<float>(this.right);
				float num5 = context.ReadValue<float>(this.forward);
				float num6 = context.ReadValue<float>(this.backward);
				return new Vector3(num4 - num3, num - num2, num5 - num6);
			}
			float num7 = (context.ReadValueAsButton(this.up) ? 1f : 0f);
			float num8 = (context.ReadValueAsButton(this.down) ? (-1f) : 0f);
			float num9 = (context.ReadValueAsButton(this.left) ? (-1f) : 0f);
			float num10 = (context.ReadValueAsButton(this.right) ? 1f : 0f);
			float num11 = (context.ReadValueAsButton(this.forward) ? 1f : 0f);
			float num12 = (context.ReadValueAsButton(this.backward) ? (-1f) : 0f);
			Vector3 normalized = new Vector3(num9 + num10, num7 + num8, num11 + num12);
			if (this.mode == Vector3Composite.Mode.DigitalNormalized)
			{
				normalized = normalized.normalized;
			}
			return normalized;
		}

		// Token: 0x0400070C RID: 1804
		[InputControl(layout = "Axis")]
		public int up;

		// Token: 0x0400070D RID: 1805
		[InputControl(layout = "Axis")]
		public int down;

		// Token: 0x0400070E RID: 1806
		[InputControl(layout = "Axis")]
		public int left;

		// Token: 0x0400070F RID: 1807
		[InputControl(layout = "Axis")]
		public int right;

		// Token: 0x04000710 RID: 1808
		[InputControl(layout = "Axis")]
		public int forward;

		// Token: 0x04000711 RID: 1809
		[InputControl(layout = "Axis")]
		public int backward;

		// Token: 0x04000712 RID: 1810
		public Vector3Composite.Mode mode;

		// Token: 0x0200024C RID: 588
		public enum Mode
		{
			// Token: 0x04000C45 RID: 3141
			Analog,
			// Token: 0x04000C46 RID: 3142
			DigitalNormalized,
			// Token: 0x04000C47 RID: 3143
			Digital
		}
	}
}
