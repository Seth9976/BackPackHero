using System;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x0200026C RID: 620
	public interface ITransform
	{
		// Token: 0x06000EB7 RID: 3767
		Vector3 Transform(Vector3 position);

		// Token: 0x06000EB8 RID: 3768
		Vector3 InverseTransform(Vector3 position);
	}
}
