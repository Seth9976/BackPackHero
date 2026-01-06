using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200008A RID: 138
	public class FleePath : RandomPath
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x00028D2E File Offset: 0x00026F2E
		public static FleePath Construct(Vector3 start, Vector3 avoid, int searchLength, OnPathDelegate callback = null)
		{
			FleePath path = PathPool.GetPath<FleePath>();
			path.Setup(start, avoid, searchLength, callback);
			return path;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00028D3F File Offset: 0x00026F3F
		protected void Setup(Vector3 start, Vector3 avoid, int searchLength, OnPathDelegate callback)
		{
			base.Setup(start, searchLength, callback);
			this.aim = start - (avoid - start) * 10f;
		}
	}
}
