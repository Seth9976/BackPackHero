using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000104 RID: 260
	public class ScaleVector3Processor : InputProcessor<Vector3>
	{
		// Token: 0x06000EB5 RID: 3765 RVA: 0x000478D8 File Offset: 0x00045AD8
		public override Vector3 Process(Vector3 value, InputControl control)
		{
			return new Vector3(value.x * this.x, value.y * this.y, value.z * this.z);
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00047906 File Offset: 0x00045B06
		public override string ToString()
		{
			return string.Format("ScaleVector3(x={0},y={1},z={2})", this.x, this.y, this.z);
		}

		// Token: 0x0400060A RID: 1546
		[Tooltip("Scale factor to multiply the incoming Vector3's X component by.")]
		public float x = 1f;

		// Token: 0x0400060B RID: 1547
		[Tooltip("Scale factor to multiply the incoming Vector3's Y component by.")]
		public float y = 1f;

		// Token: 0x0400060C RID: 1548
		[Tooltip("Scale factor to multiply the incoming Vector3's Z component by.")]
		public float z = 1f;
	}
}
