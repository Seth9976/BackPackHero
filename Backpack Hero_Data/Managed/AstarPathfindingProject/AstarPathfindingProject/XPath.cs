using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000090 RID: 144
	public class XPath : ABPath
	{
		// Token: 0x060006FE RID: 1790 RVA: 0x0002A680 File Offset: 0x00028880
		public new static XPath Construct(Vector3 start, Vector3 end, OnPathDelegate callback = null)
		{
			XPath path = PathPool.GetPath<XPath>();
			path.Setup(start, end, callback);
			path.endingCondition = new ABPathEndingCondition(path);
			return path;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0002A69C File Offset: 0x0002889C
		protected override void Reset()
		{
			base.Reset();
			this.endingCondition = null;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0002A6AB File Offset: 0x000288AB
		protected override bool EndPointGridGraphSpecialCase(GraphNode endNode)
		{
			return false;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0002A6B0 File Offset: 0x000288B0
		protected override void CompletePathIfStartIsValidTarget()
		{
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			if (this.endingCondition.TargetFound(pathNode))
			{
				this.ChangeEndNode(this.startNode);
				this.Trace(pathNode);
				base.CompleteState = PathCompleteState.Complete;
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0002A6F8 File Offset: 0x000288F8
		private void ChangeEndNode(GraphNode target)
		{
			if (this.endNode != null && this.endNode != this.startNode)
			{
				PathNode pathNode = this.pathHandler.GetPathNode(this.endNode);
				pathNode.flag1 = (pathNode.flag2 = false);
			}
			this.endNode = target;
			this.endPoint = (Vector3)target.position;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0002A754 File Offset: 0x00028954
		protected override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				int searchedNodes = base.searchedNodes;
				base.searchedNodes = searchedNodes + 1;
				if (this.endingCondition.TargetFound(this.currentR))
				{
					base.CompleteState = PathCompleteState.Complete;
					break;
				}
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
				if (this.pathHandler.heap.isEmpty)
				{
					base.FailWithError("Searched whole area but could not find target");
					return;
				}
				this.currentR = this.pathHandler.heap.Remove();
				if (num > 500)
				{
					if (DateTime.UtcNow.Ticks >= targetTick)
					{
						return;
					}
					num = 0;
					if (base.searchedNodes > 1000000)
					{
						throw new Exception("Probable infinite loop. Over 1,000,000 nodes searched");
					}
				}
				num++;
			}
			if (base.CompleteState == PathCompleteState.Complete)
			{
				this.ChangeEndNode(this.currentR.node);
				this.Trace(this.currentR);
			}
		}

		// Token: 0x0400040A RID: 1034
		public PathEndingCondition endingCondition;
	}
}
