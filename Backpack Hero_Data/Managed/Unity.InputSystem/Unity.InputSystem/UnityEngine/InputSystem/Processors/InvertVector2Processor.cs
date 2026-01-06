using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020000FD RID: 253
	public class InvertVector2Processor : InputProcessor<Vector2>
	{
		// Token: 0x06000EA3 RID: 3747 RVA: 0x00047688 File Offset: 0x00045888
		public override Vector2 Process(Vector2 value, InputControl control)
		{
			if (this.invertX)
			{
				value.x *= -1f;
			}
			if (this.invertY)
			{
				value.y *= -1f;
			}
			return value;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x000476BB File Offset: 0x000458BB
		public override string ToString()
		{
			return string.Format("InvertVector2(invertX={0},invertY={1})", this.invertX, this.invertY);
		}

		// Token: 0x04000600 RID: 1536
		public bool invertX = true;

		// Token: 0x04000601 RID: 1537
		public bool invertY = true;
	}
}
