using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020000F9 RID: 249
	public class ClampProcessor : InputProcessor<float>
	{
		// Token: 0x06000E90 RID: 3728 RVA: 0x00047493 File Offset: 0x00045693
		public override float Process(float value, InputControl control)
		{
			return Mathf.Clamp(value, this.min, this.max);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x000474A7 File Offset: 0x000456A7
		public override string ToString()
		{
			return string.Format("Clamp(min={0},max={1})", this.min, this.max);
		}

		// Token: 0x040005FD RID: 1533
		public float min;

		// Token: 0x040005FE RID: 1534
		public float max;
	}
}
