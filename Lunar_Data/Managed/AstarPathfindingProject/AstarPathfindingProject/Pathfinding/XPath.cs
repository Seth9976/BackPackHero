using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000133 RID: 307
	[Obsolete("Use an ABPath with the ABPath.endingCondition field instead")]
	public class XPath : ABPath
	{
		// Token: 0x06000952 RID: 2386 RVA: 0x000336CD File Offset: 0x000318CD
		[Obsolete("Use ABPath.Construct instead")]
		public new static ABPath Construct(Vector3 start, Vector3 end, OnPathDelegate callback = null)
		{
			return ABPath.Construct(start, end, callback);
		}
	}
}
