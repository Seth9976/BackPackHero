using System;
using Unity.Mathematics;

namespace Pathfinding.PID
{
	// Token: 0x02000242 RID: 578
	public struct AnglePIDControlOutput2D
	{
		// Token: 0x06000D7F RID: 3455 RVA: 0x00056714 File Offset: 0x00054914
		public AnglePIDControlOutput2D(float currentRotation, float targetRotation, float rotationDelta, float moveDistance)
		{
			float num;
			float num2;
			math.sincos(currentRotation + rotationDelta * 0.5f, out num, out num2);
			this.rotationDelta = rotationDelta;
			this.positionDelta = new float2(num2, num) * moveDistance;
			this.targetRotation = targetRotation;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00056758 File Offset: 0x00054958
		public static AnglePIDControlOutput2D WithMovementAtEnd(float currentRotation, float targetRotation, float rotationDelta, float moveDistance)
		{
			float num;
			float num2;
			math.sincos(currentRotation + rotationDelta, out num, out num2);
			return new AnglePIDControlOutput2D
			{
				rotationDelta = rotationDelta,
				targetRotation = targetRotation,
				positionDelta = new float2(num2, num) * moveDistance
			};
		}

		// Token: 0x04000AA8 RID: 2728
		public float rotationDelta;

		// Token: 0x04000AA9 RID: 2729
		public float targetRotation;

		// Token: 0x04000AAA RID: 2730
		public float2 positionDelta;
	}
}
