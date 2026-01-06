using System;
using Pathfinding.Serialization;
using Pathfinding.Util;
using Unity.Jobs;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000C7 RID: 199
	[Preserve]
	public class LayerGridGraph : GridGraph, IUpdatableGraph
	{
		// Token: 0x06000648 RID: 1608 RVA: 0x00021546 File Offset: 0x0001F746
		protected override void DisposeUnmanagedData()
		{
			base.DisposeUnmanagedData();
			LevelGridNode.ClearGridGraph((int)this.graphIndex, this);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0002155A File Offset: 0x0001F75A
		public LayerGridGraph()
		{
			this.newGridNodeDelegate = () => new LevelGridNode();
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00021594 File Offset: 0x0001F794
		protected override GridNodeBase[] AllocateNodesJob(int size, out JobHandle dependency)
		{
			LevelGridNode[] array = new LevelGridNode[size];
			AstarPath active = this.active;
			GridNodeBase[] array2 = array;
			dependency = active.AllocateNodes<GridNodeBase>(array2, size, this.newGridNodeDelegate, 1U);
			return array;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x000215C7 File Offset: 0x0001F7C7
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x000215CF File Offset: 0x0001F7CF
		public override int LayerCount
		{
			get
			{
				return this.layerCount;
			}
			protected set
			{
				this.layerCount = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x000215D8 File Offset: 0x0001F7D8
		public override int MaxLayers
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000215DC File Offset: 0x0001F7DC
		public override int CountNodes()
		{
			if (this.nodes == null)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if (this.nodes[i] != null)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00021618 File Offset: 0x0001F818
		public override void GetNodes(Action<GraphNode> action)
		{
			if (this.nodes == null)
			{
				return;
			}
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if (this.nodes[i] != null)
				{
					action(this.nodes[i]);
				}
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0002165C File Offset: 0x0001F85C
		public override int GetNodesInRegion(IntRect rect, GridNodeBase[] buffer)
		{
			IntRect intRect = new IntRect(0, 0, this.width - 1, this.depth - 1);
			rect = IntRect.Intersection(rect, intRect);
			if (this.nodes == null || !rect.IsValid() || this.nodes.Length != this.width * this.depth * this.layerCount)
			{
				return 0;
			}
			int num = 0;
			try
			{
				for (int i = 0; i < this.layerCount; i++)
				{
					int num2 = i * base.Width * base.Depth;
					for (int j = rect.ymin; j <= rect.ymax; j++)
					{
						int num3 = num2 + j * base.Width;
						for (int k = rect.xmin; k <= rect.xmax; k++)
						{
							GridNodeBase gridNodeBase = this.nodes[num3 + k];
							if (gridNodeBase != null)
							{
								buffer[num] = gridNodeBase;
								num++;
							}
						}
					}
				}
			}
			catch (IndexOutOfRangeException)
			{
				throw new ArgumentException("Buffer is too small");
			}
			return num;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0002175C File Offset: 0x0001F95C
		public GridNodeBase GetNode(int x, int z, int layer)
		{
			if (x < 0 || z < 0 || x >= this.width || z >= this.depth || layer < 0 || layer >= this.layerCount)
			{
				return null;
			}
			return this.nodes[x + z * this.width + layer * this.width * this.depth];
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x000217B3 File Offset: 0x0001F9B3
		protected override IGraphUpdatePromise ScanInternal(bool async)
		{
			LevelGridNode.SetGridGraph((int)this.graphIndex, this);
			this.layerCount = 0;
			this.lastScannedWidth = this.width;
			this.lastScannedDepth = this.depth;
			return base.ScanInternal(async);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000217E8 File Offset: 0x0001F9E8
		protected override GridNodeBase GetNearestFromGraphSpace(Vector3 positionGraphSpace)
		{
			if (this.nodes == null || this.depth * this.width * this.layerCount != this.nodes.Length)
			{
				return null;
			}
			int x = (int)positionGraphSpace.x;
			float z = positionGraphSpace.z;
			int num = Mathf.Clamp(x, 0, this.width - 1);
			int num2 = Mathf.Clamp((int)z, 0, this.depth - 1);
			Vector3 vector = base.transform.Transform(positionGraphSpace);
			return this.GetNearestNode(vector, num, num2, null);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00021864 File Offset: 0x0001FA64
		private GridNodeBase GetNearestNode(Vector3 position, int x, int z, NNConstraint constraint)
		{
			int num = this.width * z + x;
			float num2 = float.PositiveInfinity;
			GridNodeBase gridNodeBase = null;
			for (int i = 0; i < this.layerCount; i++)
			{
				GridNodeBase gridNodeBase2 = this.nodes[num + this.width * this.depth * i];
				if (gridNodeBase2 != null)
				{
					float sqrMagnitude = ((Vector3)gridNodeBase2.position - position).sqrMagnitude;
					if (sqrMagnitude < num2 && (constraint == null || constraint.Suitable(gridNodeBase2)))
					{
						num2 = sqrMagnitude;
						gridNodeBase = gridNodeBase2;
					}
				}
			}
			return gridNodeBase;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000218EC File Offset: 0x0001FAEC
		protected override void SerializeExtraInfo(GraphSerializationContext ctx)
		{
			if (this.nodes == null)
			{
				ctx.writer.Write(-1);
				return;
			}
			ctx.writer.Write(this.nodes.Length);
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if (this.nodes[i] == null)
				{
					ctx.writer.Write(-1);
				}
				else
				{
					ctx.writer.Write(0);
					this.nodes[i].SerializeNode(ctx);
				}
			}
			base.SerializeNodeSurfaceNormals(ctx);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00021970 File Offset: 0x0001FB70
		protected override void DeserializeExtraInfo(GraphSerializationContext ctx)
		{
			int num = ctx.reader.ReadInt32();
			if (num == -1)
			{
				this.nodes = null;
				return;
			}
			GridNodeBase[] array = new LevelGridNode[num];
			this.nodes = array;
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if (ctx.reader.ReadInt32() != -1)
				{
					this.nodes[i] = this.newGridNodeDelegate();
					this.active.InitializeNode(this.nodes[i]);
					this.nodes[i].DeserializeNode(ctx);
				}
				else
				{
					this.nodes[i] = null;
				}
			}
			base.DeserializeNativeData(ctx, ctx.meta.version >= AstarSerializer.V4_3_37);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00021A20 File Offset: 0x0001FC20
		protected override void PostDeserialization(GraphSerializationContext ctx)
		{
			base.UpdateTransform();
			this.lastScannedWidth = this.width;
			this.lastScannedDepth = this.depth;
			this.SetUpOffsetsAndCosts();
			LevelGridNode.SetGridGraph((int)this.graphIndex, this);
			if (this.nodes == null || this.nodes.Length == 0)
			{
				return;
			}
			if (this.width * this.depth * this.layerCount != this.nodes.Length)
			{
				Debug.LogError("Node data did not match with bounds data. Probably a change to the bounds/width/depth data was made after scanning the graph, just prior to saving it. Nodes will be discarded");
				this.nodes = new GridNodeBase[0];
				return;
			}
			for (int i = 0; i < this.layerCount; i++)
			{
				for (int j = 0; j < this.depth; j++)
				{
					for (int k = 0; k < this.width; k++)
					{
						LevelGridNode levelGridNode = this.nodes[j * this.width + k + this.width * this.depth * i] as LevelGridNode;
						if (levelGridNode != null)
						{
							levelGridNode.NodeInGridIndex = j * this.width + k;
							levelGridNode.LayerCoordinateInGrid = i;
						}
					}
				}
			}
		}

		// Token: 0x04000452 RID: 1106
		[JsonMember]
		internal int layerCount;

		// Token: 0x04000453 RID: 1107
		[JsonMember]
		public float characterHeight = 0.4f;

		// Token: 0x04000454 RID: 1108
		internal int lastScannedWidth;

		// Token: 0x04000455 RID: 1109
		internal int lastScannedDepth;
	}
}
