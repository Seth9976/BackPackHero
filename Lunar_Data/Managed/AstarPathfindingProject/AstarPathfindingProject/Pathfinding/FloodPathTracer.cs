using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000130 RID: 304
	public class FloodPathTracer : ABPath
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x00018013 File Offset: 0x00016213
		protected override bool hasEndPoint
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x000325C9 File Offset: 0x000307C9
		public static FloodPathTracer Construct(Vector3 start, FloodPath flood, OnPathDelegate callback = null)
		{
			FloodPathTracer path = PathPool.GetPath<FloodPathTracer>();
			path.Setup(start, flood, callback);
			return path;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000325D9 File Offset: 0x000307D9
		protected void Setup(Vector3 start, FloodPath flood, OnPathDelegate callback)
		{
			this.flood = flood;
			if (flood == null || flood.PipelineState < PathState.Returning)
			{
				throw new ArgumentException("You must supply a calculated FloodPath to the 'flood' argument");
			}
			base.Setup(start, flood.originalStartPoint, callback);
			this.nnConstraint = new FloodPathConstraint(flood);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00032613 File Offset: 0x00030813
		protected override void Reset()
		{
			base.Reset();
			this.flood = null;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00032624 File Offset: 0x00030824
		protected override void Prepare()
		{
			if (!this.flood.IsValid(this.pathHandler.nodeStorage))
			{
				base.FailWithError("The flood path is invalid because nodes have been destroyed since it was calculated. Please recalculate the flood path.");
				return;
			}
			base.Prepare();
			if (base.CompleteState == PathCompleteState.NotCalculated)
			{
				uint num = 0U;
				while ((ulong)num < (ulong)((long)this.pathHandler.numTemporaryNodes))
				{
					uint num2 = this.pathHandler.temporaryNodeStartIndex + num;
					ref TemporaryNode temporaryNode = ref this.pathHandler.GetTemporaryNode(num2);
					if (temporaryNode.type == TemporaryNodeType.Start)
					{
						GraphNode node = this.pathHandler.GetNode(temporaryNode.associatedNode);
						bool flag = false;
						uint num3 = 0U;
						while ((ulong)num3 < (ulong)((long)node.PathNodeVariants))
						{
							if (this.flood.GetParent(node.NodeIndex + num3) != 0U)
							{
								flag = true;
								base.CompleteState = PathCompleteState.Complete;
								this.Trace(node.NodeIndex + num3);
								break;
							}
							num3 += 1U;
						}
						if (!flag)
						{
							base.FailWithError("The flood path did not contain any information about the end node. Have you modified the path's nnConstraint to an instance which does not subclass FloodPathConstraint?");
						}
						return;
					}
					num += 1U;
				}
				base.FailWithError("Could not find a valid start node");
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0003271F File Offset: 0x0003091F
		protected override void CalculateStep(long targetTick)
		{
			if (base.CompleteState != PathCompleteState.Complete)
			{
				throw new Exception("Something went wrong. At this point the path should be completed");
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00032738 File Offset: 0x00030938
		protected override void Trace(uint fromPathNodeIndex)
		{
			uint num = fromPathNodeIndex;
			int num2 = 0;
			while (num != 0U)
			{
				if ((num & 2147483648U) != 0U)
				{
					num = this.flood.GetParent(num & 2147483647U);
				}
				else
				{
					GraphNode node = this.pathHandler.GetNode(num);
					if (node == null)
					{
						base.FailWithError("A node in the path has been destroyed. The FloodPath needs to be recalculated before you can use a FloodPathTracer.");
						return;
					}
					if (!base.CanTraverse(node))
					{
						base.FailWithError("A node in the path is no longer walkable. The FloodPath needs to be recalculated before you can use a FloodPathTracer.");
						return;
					}
					this.path.Add(node);
					this.vectorPath.Add((Vector3)node.position);
					uint parent = this.flood.GetParent(num);
					if (parent == num)
					{
						break;
					}
					num = parent;
				}
				num2++;
				if (num2 > 10000)
				{
					Debug.LogWarning("Infinite loop? >10000 node path. Remove this message if you really have that long paths");
					return;
				}
			}
		}

		// Token: 0x04000652 RID: 1618
		protected FloodPath flood;
	}
}
