using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020000F9 RID: 249
	public class ClampProcessor : InputProcessor<float>
	{
		// Token: 0x06000E95 RID: 3733 RVA: 0x000474DF File Offset: 0x000456DF
		public override float Process(float value, InputControl control)
		{
			return Mathf.Clamp(value, this.min, this.max);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x000474F3 File Offset: 0x000456F3
		public override string ToString()
		{
			return string.Format("Clamp(min={0},max={1})", this.min, this.max);
		}

		// Token: 0x040005FE RID: 1534
		public float min;

		// Token: 0x040005FF RID: 1535
		public float max;
	}
}
