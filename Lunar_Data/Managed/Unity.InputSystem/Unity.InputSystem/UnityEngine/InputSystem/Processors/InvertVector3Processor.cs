using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020000FE RID: 254
	public class InvertVector3Processor : InputProcessor<Vector3>
	{
		// Token: 0x06000EA1 RID: 3745 RVA: 0x000476A8 File Offset: 0x000458A8
		public override Vector3 Process(Vector3 value, InputControl control)
		{
			if (this.invertX)
			{
				value.x *= -1f;
			}
			if (this.invertY)
			{
				value.y *= -1f;
			}
			if (this.invertZ)
			{
				value.z *= -1f;
			}
			return value;
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x000476FE File Offset: 0x000458FE
		public override string ToString()
		{
			return string.Format("InvertVector3(invertX={0},invertY={1},invertZ={2})", this.invertX, this.invertY, this.invertZ);
		}

		// Token: 0x04000601 RID: 1537
		public bool invertX = true;

		// Token: 0x04000602 RID: 1538
		public bool invertY = true;

		// Token: 0x04000603 RID: 1539
		public bool invertZ = true;
	}
}
