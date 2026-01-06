using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020000FD RID: 253
	public class InvertVector2Processor : InputProcessor<Vector2>
	{
		// Token: 0x06000E9E RID: 3742 RVA: 0x0004763C File Offset: 0x0004583C
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

		// Token: 0x06000E9F RID: 3743 RVA: 0x0004766F File Offset: 0x0004586F
		public override string ToString()
		{
			return string.Format("InvertVector2(invertX={0},invertY={1})", this.invertX, this.invertY);
		}

		// Token: 0x040005FF RID: 1535
		public bool invertX = true;

		// Token: 0x04000600 RID: 1536
		public bool invertY = true;
	}
}
