using System;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020000CF RID: 207
	public class ObstacleVertex
	{
		// Token: 0x0400051A RID: 1306
		public bool ignore;

		// Token: 0x0400051B RID: 1307
		public Vector3 position;

		// Token: 0x0400051C RID: 1308
		public Vector2 dir;

		// Token: 0x0400051D RID: 1309
		public float height;

		// Token: 0x0400051E RID: 1310
		public RVOLayer layer = RVOLayer.DefaultObstacle;

		// Token: 0x0400051F RID: 1311
		public ObstacleVertex next;

		// Token: 0x04000520 RID: 1312
		public ObstacleVertex prev;
	}
}
