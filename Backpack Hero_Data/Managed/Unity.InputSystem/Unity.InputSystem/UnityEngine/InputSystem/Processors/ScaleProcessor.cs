using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000102 RID: 258
	public class ScaleProcessor : InputProcessor<float>
	{
		// Token: 0x06000EB4 RID: 3764 RVA: 0x0004788F File Offset: 0x00045A8F
		public override float Process(float value, InputControl control)
		{
			return value * this.factor;
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00047899 File Offset: 0x00045A99
		public override string ToString()
		{
			return string.Format("Scale(factor={0})", this.factor);
		}

		// Token: 0x04000608 RID: 1544
		[Tooltip("Scale factor to multiply incoming float values by.")]
		public float factor = 1f;
	}
}
