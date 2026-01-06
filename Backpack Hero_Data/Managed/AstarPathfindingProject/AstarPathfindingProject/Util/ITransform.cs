using System;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020000C5 RID: 197
	public interface ITransform
	{
		// Token: 0x06000865 RID: 2149
		Vector3 Transform(Vector3 position);

		// Token: 0x06000866 RID: 2150
		Vector3 InverseTransform(Vector3 position);
	}
}
