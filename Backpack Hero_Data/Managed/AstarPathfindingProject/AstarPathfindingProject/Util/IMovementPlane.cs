using System;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020000C4 RID: 196
	public interface IMovementPlane
	{
		// Token: 0x06000862 RID: 2146
		Vector2 ToPlane(Vector3 p);

		// Token: 0x06000863 RID: 2147
		Vector2 ToPlane(Vector3 p, out float elevation);

		// Token: 0x06000864 RID: 2148
		Vector3 ToWorld(Vector2 p, float elevation = 0f);
	}
}
