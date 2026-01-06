using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000B5 RID: 181
	internal class PathReturnQueue
	{
		// Token: 0x060005BE RID: 1470 RVA: 0x0001B8A0 File Offset: 0x00019AA0
		public PathReturnQueue(object pathsClaimedSilentlyBy, Action OnReturnedPaths)
		{
			this.pathsClaimedSilentlyBy = pathsClaimedSilentlyBy;
			this.OnReturnedPaths = OnReturnedPaths;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001B8C4 File Offset: 0x00019AC4
		public void Enqueue(Path path)
		{
			Queue<Path> queue = this.pathReturnQueue;
			lock (queue)
			{
				this.pathReturnQueue.Enqueue(path);
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001B90C File Offset: 0x00019B0C
		public void ReturnPaths(bool timeSlice)
		{
			long num = (timeSlice ? (DateTime.UtcNow.Ticks + 10000L) : 0L);
			int num2 = 0;
			int num3 = 0;
			for (;;)
			{
				Queue<Path> queue = this.pathReturnQueue;
				Path path;
				lock (queue)
				{
					if (this.pathReturnQueue.Count == 0)
					{
						break;
					}
					path = this.pathReturnQueue.Dequeue();
				}
				((IPathInternals)path).AdvanceState(PathState.Returning);
				try
				{
					((IPathInternals)path).ReturnPath();
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
				((IPathInternals)path).AdvanceState(PathState.Returned);
				path.Release(this.pathsClaimedSilentlyBy, true);
				num2++;
				num3++;
				if (num2 > 5 && timeSlice)
				{
					num2 = 0;
					if (DateTime.UtcNow.Ticks >= num)
					{
						break;
					}
				}
			}
			if (num3 > 0)
			{
				this.OnReturnedPaths();
			}
		}

		// Token: 0x040003D4 RID: 980
		private readonly Queue<Path> pathReturnQueue = new Queue<Path>();

		// Token: 0x040003D5 RID: 981
		private readonly object pathsClaimedSilentlyBy;

		// Token: 0x040003D6 RID: 982
		private readonly Action OnReturnedPaths;
	}
}
