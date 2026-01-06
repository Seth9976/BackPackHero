using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000046 RID: 70
	internal class PathReturnQueue
	{
		// Token: 0x06000352 RID: 850 RVA: 0x00012ACE File Offset: 0x00010CCE
		public PathReturnQueue(object pathsClaimedSilentlyBy)
		{
			this.pathsClaimedSilentlyBy = pathsClaimedSilentlyBy;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00012AE8 File Offset: 0x00010CE8
		public void Enqueue(Path path)
		{
			Queue<Path> queue = this.pathReturnQueue;
			lock (queue)
			{
				this.pathReturnQueue.Enqueue(path);
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00012B30 File Offset: 0x00010D30
		public void ReturnPaths(bool timeSlice)
		{
			long num = (timeSlice ? (DateTime.UtcNow.Ticks + 10000L) : 0L);
			int num2 = 0;
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
				if (num2 > 5 && timeSlice)
				{
					num2 = 0;
					if (DateTime.UtcNow.Ticks >= num)
					{
						break;
					}
				}
			}
		}

		// Token: 0x04000219 RID: 537
		private Queue<Path> pathReturnQueue = new Queue<Path>();

		// Token: 0x0400021A RID: 538
		private object pathsClaimedSilentlyBy;
	}
}
