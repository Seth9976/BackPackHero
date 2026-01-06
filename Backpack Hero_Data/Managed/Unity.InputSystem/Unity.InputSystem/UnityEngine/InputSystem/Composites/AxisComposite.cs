using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Processors;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x02000148 RID: 328
	[DisplayStringFormat("{negative}/{positive}")]
	[DisplayName("Positive/Negative Binding")]
	public class AxisComposite : InputBindingComposite<float>
	{
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x00053FC9 File Offset: 0x000521C9
		public float midPoint
		{
			get
			{
				return (this.maxValue + this.minValue) / 2f;
			}
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00053FE0 File Offset: 0x000521E0
		public override float ReadValue(ref InputBindingCompositeContext context)
		{
			float num = Mathf.Abs(context.ReadValue<float>(this.negative));
			float num2 = Mathf.Abs(context.ReadValue<float>(this.positive));
			bool flag = num > Mathf.Epsilon;
			bool flag2 = num2 > Mathf.Epsilon;
			if (flag == flag2)
			{
				switch (this.whichSideWins)
				{
				case AxisComposite.WhichSideWins.Neither:
					return this.midPoint;
				case AxisComposite.WhichSideWins.Positive:
					flag = false;
					break;
				}
			}
			float midPoint = this.midPoint;
			if (flag)
			{
				return midPoint - (midPoint - this.minValue) * num;
			}
			return midPoint + (this.maxValue - midPoint) * num2;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0005407C File Offset: 0x0005227C
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			float num = this.ReadValue(ref context);
			if (num < this.midPoint)
			{
				num = Mathf.Abs(num - this.midPoint);
				return NormalizeProcessor.Normalize(num, 0f, Mathf.Abs(this.minValue), 0f);
			}
			num = Mathf.Abs(num - this.midPoint);
			return NormalizeProcessor.Normalize(num, 0f, Mathf.Abs(this.maxValue), 0f);
		}

		// Token: 0x040006ED RID: 1773
		[InputControl(layout = "Axis")]
		public int negative;

		// Token: 0x040006EE RID: 1774
		[InputControl(layout = "Axis")]
		public int positive;

		// Token: 0x040006EF RID: 1775
		[Tooltip("Value to return when the negative side is fully actuated.")]
		public float minValue = -1f;

		// Token: 0x040006F0 RID: 1776
		[Tooltip("Value to return when the positive side is fully actuated.")]
		public float maxValue = 1f;

		// Token: 0x040006F1 RID: 1777
		[Tooltip("If both the positive and negative side are actuated, decides what value to return. 'Neither' (default) means that the resulting value is the midpoint between min and max. 'Positive' means that max will be returned. 'Negative' means that min will be returned.")]
		public AxisComposite.WhichSideWins whichSideWins;

		// Token: 0x0200024A RID: 586
		public enum WhichSideWins
		{
			// Token: 0x04000C3D RID: 3133
			Neither,
			// Token: 0x04000C3E RID: 3134
			Positive,
			// Token: 0x04000C3F RID: 3135
			Negative
		}
	}
}
