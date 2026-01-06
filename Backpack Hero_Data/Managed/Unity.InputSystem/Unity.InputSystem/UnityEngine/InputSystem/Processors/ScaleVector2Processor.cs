using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000103 RID: 259
	public class ScaleVector2Processor : InputProcessor<Vector2>
	{
		// Token: 0x06000EB7 RID: 3767 RVA: 0x000478C3 File Offset: 0x00045AC3
		public override Vector2 Process(Vector2 value, InputControl control)
		{
			return new Vector2(value.x * this.x, value.y * this.y);
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x000478E4 File Offset: 0x00045AE4
		public override string ToString()
		{
			return string.Format("ScaleVector2(x={0},y={1})", this.x, this.y);
		}

		// Token: 0x04000609 RID: 1545
		[Tooltip("Scale factor to multiply the incoming Vector2's X component by.")]
		public float x = 1f;

		// Token: 0x0400060A RID: 1546
		[Tooltip("Scale factor to multiply the incoming Vector2's Y component by.")]
		public float y = 1f;
	}
}
