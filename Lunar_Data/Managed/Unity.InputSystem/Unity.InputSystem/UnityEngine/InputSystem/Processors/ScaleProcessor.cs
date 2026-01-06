using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000102 RID: 258
	public class ScaleProcessor : InputProcessor<float>
	{
		// Token: 0x06000EAF RID: 3759 RVA: 0x00047843 File Offset: 0x00045A43
		public override float Process(float value, InputControl control)
		{
			return value * this.factor;
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0004784D File Offset: 0x00045A4D
		public override string ToString()
		{
			return string.Format("Scale(factor={0})", this.factor);
		}

		// Token: 0x04000607 RID: 1543
		[Tooltip("Scale factor to multiply incoming float values by.")]
		public float factor = 1f;
	}
}
