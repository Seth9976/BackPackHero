using System;
using Pathfinding.Util;
using Unity.Mathematics;

namespace Pathfinding.PID
{
	// Token: 0x02000243 RID: 579
	public struct AnglePIDControlOutput
	{
		// Token: 0x06000D81 RID: 3457 RVA: 0x0005679E File Offset: 0x0005499E
		public AnglePIDControlOutput(NativeMovementPlane movementPlane, AnglePIDControlOutput2D control2D)
		{
			this.rotationDelta = movementPlane.ToWorldRotationDelta(-control2D.rotationDelta);
			this.positionDelta = movementPlane.ToWorld(control2D.positionDelta, 0f);
			this.maxDesiredWallDistance = 0f;
		}

		// Token: 0x04000AAB RID: 2731
		public quaternion rotationDelta;

		// Token: 0x04000AAC RID: 2732
		public float3 positionDelta;

		// Token: 0x04000AAD RID: 2733
		public float maxDesiredWallDistance;
	}
}
