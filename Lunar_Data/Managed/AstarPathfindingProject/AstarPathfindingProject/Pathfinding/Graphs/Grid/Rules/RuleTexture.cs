using System;
using Pathfinding.Jobs;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Graphs.Grid.Rules
{
	// Token: 0x02000214 RID: 532
	[Preserve]
	public class RuleTexture : GridGraphRule
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0005014E File Offset: 0x0004E34E
		public override int Hash
		{
			get
			{
				return base.Hash ^ (int)((this.texture != null) ? this.texture.updateCount : 0U);
			}
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00050174 File Offset: 0x0004E374
		public override void Register(GridGraphRules rules)
		{
			if (this.texture == null)
			{
				return;
			}
			if (!this.texture.isReadable)
			{
				Debug.LogError("Texture for the texture rule on a grid graph is not marked as readable.", this.texture);
				return;
			}
			if (this.colors.IsCreated)
			{
				this.colors.Dispose();
			}
			this.colors = new NativeArray<Color32>(this.texture.GetPixels32(), Allocator.Persistent).Reinterpret<int>();
			int2 textureSize = new int2(this.texture.width, this.texture.height);
			float4 channelPenaltiesCombined = float4.zero;
			bool4 channelDeterminesWalkability = false;
			float4 channelPositionScalesCombined = float4.zero;
			for (int i = 0; i < 4; i++)
			{
				channelPenaltiesCombined[i] = ((this.channels[i] == RuleTexture.ChannelUse.Penalty || this.channels[i] == RuleTexture.ChannelUse.WalkablePenalty) ? this.channelScales[i] : 0f);
				channelDeterminesWalkability[i] = this.channels[i] == RuleTexture.ChannelUse.Walkable || this.channels[i] == RuleTexture.ChannelUse.WalkablePenalty;
				channelPositionScalesCombined[i] = ((this.channels[i] == RuleTexture.ChannelUse.Position) ? this.channelScales[i] : 0f);
			}
			channelPositionScalesCombined /= 255f;
			channelPenaltiesCombined /= 255f;
			if (math.any(channelPositionScalesCombined))
			{
				rules.AddJobSystemPass(GridGraphRule.Pass.BeforeCollision, delegate(GridGraphRules.Context context)
				{
					new RuleTexture.JobTexturePosition
					{
						colorData = this.colors,
						nodePositions = context.data.nodes.positions,
						nodeNormals = context.data.nodes.normals,
						bounds = context.data.nodes.bounds,
						colorDataSize = textureSize,
						scale = ((this.scalingMode == RuleTexture.ScalingMode.FixedScale) ? (1f / math.max(0.01f, this.nodesPerPixel)) : (textureSize / new float2((float)context.graph.width, (float)context.graph.depth))),
						channelPositionScale = channelPositionScalesCombined,
						graphToWorld = context.data.transform.matrix
					}.Schedule(context.tracker);
				});
			}
			rules.AddJobSystemPass(GridGraphRule.Pass.BeforeConnections, delegate(GridGraphRules.Context context)
			{
				new RuleTexture.JobTexturePenalty
				{
					colorData = this.colors,
					penalty = context.data.nodes.penalties,
					walkable = context.data.nodes.walkable,
					nodeNormals = context.data.nodes.normals,
					bounds = context.data.nodes.bounds,
					colorDataSize = textureSize,
					scale = ((this.scalingMode == RuleTexture.ScalingMode.FixedScale) ? (1f / math.max(0.01f, this.nodesPerPixel)) : (textureSize / new float2((float)context.graph.width, (float)context.graph.depth))),
					channelPenalties = channelPenaltiesCombined,
					channelDeterminesWalkability = channelDeterminesWalkability
				}.Schedule(context.tracker);
			});
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00050322 File Offset: 0x0004E522
		public override void DisposeUnmanagedData()
		{
			if (this.colors.IsCreated)
			{
				this.colors.Dispose();
			}
		}

		// Token: 0x040009A5 RID: 2469
		public Texture2D texture;

		// Token: 0x040009A6 RID: 2470
		public RuleTexture.ChannelUse[] channels = new RuleTexture.ChannelUse[4];

		// Token: 0x040009A7 RID: 2471
		public float[] channelScales = new float[] { 1000f, 1000f, 1000f, 1000f };

		// Token: 0x040009A8 RID: 2472
		public RuleTexture.ScalingMode scalingMode = RuleTexture.ScalingMode.StretchToFitGraph;

		// Token: 0x040009A9 RID: 2473
		public float nodesPerPixel = 1f;

		// Token: 0x040009AA RID: 2474
		private NativeArray<int> colors;

		// Token: 0x02000215 RID: 533
		public enum ScalingMode
		{
			// Token: 0x040009AC RID: 2476
			FixedScale,
			// Token: 0x040009AD RID: 2477
			StretchToFitGraph
		}

		// Token: 0x02000216 RID: 534
		public enum ChannelUse
		{
			// Token: 0x040009AF RID: 2479
			None,
			// Token: 0x040009B0 RID: 2480
			Penalty,
			// Token: 0x040009B1 RID: 2481
			Position,
			// Token: 0x040009B2 RID: 2482
			WalkablePenalty,
			// Token: 0x040009B3 RID: 2483
			Walkable
		}

		// Token: 0x02000217 RID: 535
		[BurstCompile]
		public struct JobTexturePosition : IJob, GridIterationUtilities.INodeModifier
		{
			// Token: 0x06000CD8 RID: 3288 RVA: 0x0005037C File Offset: 0x0004E57C
			public void ModifyNode(int dataIndex, int dataX, int dataLayer, int dataZ)
			{
				int2 xz = this.bounds.min.xz;
				int2 @int = math.clamp((int2)math.round((new float2((float)dataX, (float)dataZ) + xz) * this.scale), int2.zero, this.colorDataSize - new int2(1, 1));
				int num = @int.y * this.colorDataSize.x + @int.x;
				int4 int2 = new int4(this.colorData[num] & 255, (this.colorData[num] >> 8) & 255, (this.colorData[num] >> 16) & 255, (this.colorData[num] >> 24) & 255);
				float num2 = math.dot(this.channelPositionScale, int2);
				this.nodePositions[dataIndex] = this.graphToWorld.MultiplyPoint3x4(new Vector3((float)(this.bounds.min.x + dataX) + 0.5f, num2, (float)(this.bounds.min.z + dataZ) + 0.5f));
			}

			// Token: 0x06000CD9 RID: 3289 RVA: 0x000504B6 File Offset: 0x0004E6B6
			public void Execute()
			{
				GridIterationUtilities.ForEachNode<RuleTexture.JobTexturePosition>(this.bounds.size, this.nodeNormals, ref this);
			}

			// Token: 0x040009B4 RID: 2484
			[ReadOnly]
			public NativeArray<int> colorData;

			// Token: 0x040009B5 RID: 2485
			[WriteOnly]
			public NativeArray<Vector3> nodePositions;

			// Token: 0x040009B6 RID: 2486
			[ReadOnly]
			public NativeArray<float4> nodeNormals;

			// Token: 0x040009B7 RID: 2487
			public Matrix4x4 graphToWorld;

			// Token: 0x040009B8 RID: 2488
			public IntBounds bounds;

			// Token: 0x040009B9 RID: 2489
			public int2 colorDataSize;

			// Token: 0x040009BA RID: 2490
			public float2 scale;

			// Token: 0x040009BB RID: 2491
			public float4 channelPositionScale;
		}

		// Token: 0x02000218 RID: 536
		[BurstCompile]
		public struct JobTexturePenalty : IJob, GridIterationUtilities.INodeModifier
		{
			// Token: 0x06000CDA RID: 3290 RVA: 0x000504D0 File Offset: 0x0004E6D0
			public void ModifyNode(int dataIndex, int dataX, int dataLayer, int dataZ)
			{
				int2 xz = this.bounds.min.xz;
				int2 @int = math.clamp((int2)math.round((new float2((float)dataX, (float)dataZ) + xz) * this.scale), int2.zero, this.colorDataSize - new int2(1, 1));
				int num = @int.y * this.colorDataSize.x + @int.x;
				int4 int2 = new int4(this.colorData[num] & 255, (this.colorData[num] >> 8) & 255, (this.colorData[num] >> 16) & 255, (this.colorData[num] >> 24) & 255);
				ref NativeArray<uint> ptr = ref this.penalty;
				ptr[dataIndex] += (uint)math.dot(this.channelPenalties, int2);
				this.walkable[dataIndex] = this.walkable[dataIndex] & !math.any(this.channelDeterminesWalkability & (int2 == 0));
			}

			// Token: 0x06000CDB RID: 3291 RVA: 0x00050609 File Offset: 0x0004E809
			public void Execute()
			{
				GridIterationUtilities.ForEachNode<RuleTexture.JobTexturePenalty>(this.bounds.size, this.nodeNormals, ref this);
			}

			// Token: 0x040009BC RID: 2492
			[ReadOnly]
			public NativeArray<int> colorData;

			// Token: 0x040009BD RID: 2493
			public NativeArray<uint> penalty;

			// Token: 0x040009BE RID: 2494
			public NativeArray<bool> walkable;

			// Token: 0x040009BF RID: 2495
			[ReadOnly]
			public NativeArray<float4> nodeNormals;

			// Token: 0x040009C0 RID: 2496
			public IntBounds bounds;

			// Token: 0x040009C1 RID: 2497
			public int2 colorDataSize;

			// Token: 0x040009C2 RID: 2498
			public float2 scale;

			// Token: 0x040009C3 RID: 2499
			public float4 channelPenalties;

			// Token: 0x040009C4 RID: 2500
			public bool4 channelDeterminesWalkability;
		}
	}
}
