using System;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x02000267 RID: 615
	public interface IMovementPlane
	{
		// Token: 0x06000E8F RID: 3727
		Vector2 ToPlane(Vector3 p);

		// Token: 0x06000E90 RID: 3728
		Vector2 ToPlane(Vector3 p, out float elevation);

		// Token: 0x06000E91 RID: 3729
		Vector3 ToWorld(Vector2 p, float elevation = 0f);

		// Token: 0x06000E92 RID: 3730
		SimpleMovementPlane ToSimpleMovementPlane();
	}
}
