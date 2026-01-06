using System;
using Pathfinding.Jobs;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200002C RID: 44
	public class GraphUpdateObject
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00009D48 File Offset: 0x00007F48
		public GraphUpdateStage stage
		{
			get
			{
				switch (this.internalStage)
				{
				case -3:
					return GraphUpdateStage.Aborted;
				case -1:
					return GraphUpdateStage.Created;
				case 0:
					return GraphUpdateStage.Applied;
				}
				return GraphUpdateStage.Pending;
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000033F6 File Offset: 0x000015F6
		public virtual void WillUpdateNode(GraphNode node)
		{
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000033F6 File Offset: 0x000015F6
		[Obsolete("Use AstarPath.Snapshot instead", true)]
		public virtual void RevertFromBackup()
		{
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00009D80 File Offset: 0x00007F80
		public virtual void Apply(GraphNode node)
		{
			if (this.shape == null || this.shape.Contains(node))
			{
				node.Penalty = (uint)((ulong)node.Penalty + (ulong)((long)this.addPenalty));
				if (this.modifyWalkability)
				{
					node.Walkable = this.setWalkability;
				}
				if (this.modifyTag)
				{
					node.Tag = this.setTag;
				}
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00009DE8 File Offset: 0x00007FE8
		public virtual void ApplyJob(GraphUpdateObject.GraphUpdateData data, JobDependencyTracker dependencyTracker)
		{
			if (this.addPenalty == 0 && !this.modifyWalkability && !this.modifyTag)
			{
				return;
			}
			new GraphUpdateObject.JobGraphUpdate
			{
				shape = ((this.shape != null) ? new GraphUpdateShape.BurstShape(this.shape, Allocator.Persistent) : GraphUpdateShape.BurstShape.Everything),
				data = data,
				bounds = this.bounds,
				penaltyDelta = this.addPenalty,
				modifyWalkability = this.modifyWalkability,
				walkabilityValue = this.setWalkability,
				modifyTag = this.modifyTag,
				tagValue = (int)this.setTag.value
			}.Schedule(dependencyTracker);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00009E9B File Offset: 0x0000809B
		public GraphUpdateObject()
		{
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00009ECA File Offset: 0x000080CA
		public GraphUpdateObject(Bounds b)
		{
			this.bounds = b;
		}

		// Token: 0x0400014E RID: 334
		public Bounds bounds;

		// Token: 0x0400014F RID: 335
		public bool updatePhysics = true;

		// Token: 0x04000150 RID: 336
		public bool resetPenaltyOnPhysics = true;

		// Token: 0x04000151 RID: 337
		public bool updateErosion = true;

		// Token: 0x04000152 RID: 338
		public NNConstraint nnConstraint = NNConstraint.None;

		// Token: 0x04000153 RID: 339
		public int addPenalty;

		// Token: 0x04000154 RID: 340
		public bool modifyWalkability;

		// Token: 0x04000155 RID: 341
		public bool setWalkability;

		// Token: 0x04000156 RID: 342
		public bool modifyTag;

		// Token: 0x04000157 RID: 343
		public PathfindingTag setTag;

		// Token: 0x04000158 RID: 344
		[Obsolete("This field does not do anything anymore. Use AstarPath.Snapshot instead.")]
		public bool trackChangedNodes;

		// Token: 0x04000159 RID: 345
		public GraphUpdateShape shape;

		// Token: 0x0400015A RID: 346
		internal int internalStage = -1;

		// Token: 0x0400015B RID: 347
		internal const int STAGE_CREATED = -1;

		// Token: 0x0400015C RID: 348
		internal const int STAGE_PENDING = -2;

		// Token: 0x0400015D RID: 349
		internal const int STAGE_ABORTED = -3;

		// Token: 0x0400015E RID: 350
		internal const int STAGE_APPLIED = 0;

		// Token: 0x0200002D RID: 45
		public struct GraphUpdateData
		{
			// Token: 0x0400015F RID: 351
			public NativeArray<Vector3> nodePositions;

			// Token: 0x04000160 RID: 352
			public NativeArray<uint> nodePenalties;

			// Token: 0x04000161 RID: 353
			public NativeArray<bool> nodeWalkable;

			// Token: 0x04000162 RID: 354
			public NativeArray<int> nodeTags;

			// Token: 0x04000163 RID: 355
			public NativeArray<int> nodeIndices;
		}

		// Token: 0x0200002E RID: 46
		[BurstCompile]
		public struct JobGraphUpdate : IJob
		{
			// Token: 0x060001F2 RID: 498 RVA: 0x00009F00 File Offset: 0x00008100
			public void Execute()
			{
				for (int i = 0; i < this.data.nodeIndices.Length; i++)
				{
					int num = this.data.nodeIndices[i];
					if (this.bounds.Contains(this.data.nodePositions[num]) && this.shape.Contains(this.data.nodePositions[num]))
					{
						ref NativeArray<uint> ptr = ref this.data.nodePenalties;
						int num2 = num;
						ptr[num2] += (uint)this.penaltyDelta;
						if (this.modifyWalkability)
						{
							this.data.nodeWalkable[num] = this.walkabilityValue;
						}
						if (this.modifyTag)
						{
							this.data.nodeTags[num] = this.tagValue;
						}
					}
				}
			}

			// Token: 0x04000164 RID: 356
			public GraphUpdateShape.BurstShape shape;

			// Token: 0x04000165 RID: 357
			public GraphUpdateObject.GraphUpdateData data;

			// Token: 0x04000166 RID: 358
			public Bounds bounds;

			// Token: 0x04000167 RID: 359
			public int penaltyDelta;

			// Token: 0x04000168 RID: 360
			public bool modifyWalkability;

			// Token: 0x04000169 RID: 361
			public bool walkabilityValue;

			// Token: 0x0400016A RID: 362
			public bool modifyTag;

			// Token: 0x0400016B RID: 363
			public int tagValue;
		}
	}
}
