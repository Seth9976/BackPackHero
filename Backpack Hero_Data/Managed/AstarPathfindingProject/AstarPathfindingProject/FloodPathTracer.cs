using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200008D RID: 141
	public class FloodPathTracer : ABPath
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x000290FA File Offset: 0x000272FA
		protected override bool hasEndPoint
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00029105 File Offset: 0x00027305
		public static FloodPathTracer Construct(Vector3 start, FloodPath flood, OnPathDelegate callback = null)
		{
			FloodPathTracer path = PathPool.GetPath<FloodPathTracer>();
			path.Setup(start, flood, callback);
			return path;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00029115 File Offset: 0x00027315
		protected void Setup(Vector3 start, FloodPath flood, OnPathDelegate callback)
		{
			this.flood = flood;
			if (flood == null || flood.PipelineState < PathState.Returned)
			{
				throw new ArgumentException("You must supply a calculated FloodPath to the 'flood' argument");
			}
			base.Setup(start, flood.originalStartPoint, callback);
			this.nnConstraint = new FloodPathConstraint(flood);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0002914F File Offset: 0x0002734F
		protected override void Reset()
		{
			base.Reset();
			this.flood = null;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0002915E File Offset: 0x0002735E
		protected override void Initialize()
		{
			if (this.startNode != null && this.flood.HasPathTo(this.startNode))
			{
				this.Trace(this.startNode);
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
			base.FailWithError("Could not find valid start node");
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0002919A File Offset: 0x0002739A
		protected override void CalculateStep(long targetTick)
		{
			if (base.CompleteState != PathCompleteState.Complete)
			{
				throw new Exception("Something went wrong. At this point the path should be completed");
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000291B0 File Offset: 0x000273B0
		public void Trace(GraphNode from)
		{
			GraphNode graphNode = from;
			int num = 0;
			while (graphNode != null)
			{
				this.path.Add(graphNode);
				this.vectorPath.Add((Vector3)graphNode.position);
				graphNode = this.flood.GetParent(graphNode);
				num++;
				if (num > 1024)
				{
					Debug.LogWarning("Inifinity loop? >1024 node path. Remove this message if you really have that long paths (FloodPathTracer.cs, Trace function)");
					return;
				}
			}
		}

		// Token: 0x040003F3 RID: 1011
		protected FloodPath flood;
	}
}
