using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000023 RID: 35
	public struct GraphHitInfo
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x000098F8 File Offset: 0x00007AF8
		public readonly float distance
		{
			get
			{
				return (this.point - this.origin).magnitude;
			}
		}

		// Token: 0x0400012A RID: 298
		public Vector3 origin;

		// Token: 0x0400012B RID: 299
		public Vector3 point;

		// Token: 0x0400012C RID: 300
		public GraphNode node;

		// Token: 0x0400012D RID: 301
		public Vector3 tangentOrigin;

		// Token: 0x0400012E RID: 302
		public Vector3 tangent;
	}
}
