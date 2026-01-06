using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200012D RID: 301
	public class FleePath : RandomPath
	{
		// Token: 0x0600091A RID: 2330 RVA: 0x0003228C File Offset: 0x0003048C
		public static FleePath Construct(Vector3 start, Vector3 avoid, int searchLength, OnPathDelegate callback = null)
		{
			FleePath path = PathPool.GetPath<FleePath>();
			path.Setup(start, avoid, searchLength, callback);
			return path;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0003229D File Offset: 0x0003049D
		protected void Setup(Vector3 start, Vector3 avoid, int searchLength, OnPathDelegate callback)
		{
			base.Setup(start, searchLength, callback);
			this.aim = start - (avoid - start) * 10f;
		}
	}
}
