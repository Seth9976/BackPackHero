using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000103 RID: 259
	public class ScaleVector2Processor : InputProcessor<Vector2>
	{
		// Token: 0x06000EB2 RID: 3762 RVA: 0x00047877 File Offset: 0x00045A77
		public override Vector2 Process(Vector2 value, InputControl control)
		{
			return new Vector2(value.x * this.x, value.y * this.y);
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00047898 File Offset: 0x00045A98
		public override string ToString()
		{
			return string.Format("ScaleVector2(x={0},y={1})", this.x, this.y);
		}

		// Token: 0x04000608 RID: 1544
		[Tooltip("Scale factor to multiply the incoming Vector2's X component by.")]
		public float x = 1f;

		// Token: 0x04000609 RID: 1545
		[Tooltip("Scale factor to multiply the incoming Vector2's Y component by.")]
		public float y = 1f;
	}
}
