using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000071 RID: 113
	public class RecastBBTreeBox
	{
		// Token: 0x06000618 RID: 1560 RVA: 0x00023FF8 File Offset: 0x000221F8
		public RecastBBTreeBox(RecastMeshObj mesh)
		{
			this.mesh = mesh;
			Vector3 min = mesh.bounds.min;
			Vector3 max = mesh.bounds.max;
			this.rect = Rect.MinMaxRect(min.x, min.z, max.x, max.z);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0002404D File Offset: 0x0002224D
		public bool Contains(Vector3 p)
		{
			return this.rect.Contains(p);
		}

		// Token: 0x0400036C RID: 876
		public Rect rect;

		// Token: 0x0400036D RID: 877
		public RecastMeshObj mesh;

		// Token: 0x0400036E RID: 878
		public RecastBBTreeBox c1;

		// Token: 0x0400036F RID: 879
		public RecastBBTreeBox c2;
	}
}
