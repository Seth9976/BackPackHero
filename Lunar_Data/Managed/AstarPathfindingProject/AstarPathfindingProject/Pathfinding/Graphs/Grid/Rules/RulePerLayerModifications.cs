using System;
using Pathfinding.Jobs;
using Pathfinding.Util;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Graphs.Grid.Rules
{
	// Token: 0x02000210 RID: 528
	[Preserve]
	public class RulePerLayerModifications : GridGraphRule
	{
		// Token: 0x06000CD0 RID: 3280 RVA: 0x0004FF48 File Offset: 0x0004E148
		public override void Register(GridGraphRules rules)
		{
			int[] layerToTag = new int[32];
			bool[] layerToUnwalkable = new bool[32];
			for (int i = 0; i < this.layerRules.Length; i++)
			{
				RulePerLayerModifications.PerLayerRule perLayerRule = this.layerRules[i];
				if (perLayerRule.action == RulePerLayerModifications.RuleAction.SetTag)
				{
					layerToTag[perLayerRule.layer] = 1073741824 | perLayerRule.tag;
				}
				else
				{
					layerToUnwalkable[perLayerRule.layer] = true;
				}
			}
			rules.AddMainThreadPass(GridGraphRule.Pass.BeforeConnections, delegate(GridGraphRules.Context context)
			{
				NativeArray<RaycastHit> heightHits = context.data.heightHits;
				NativeArray<bool> walkable = context.data.nodes.walkable;
				NativeArray<int> tags = context.data.nodes.tags;
				Slice3D slice3D = new Slice3D(context.data.nodes.bounds, context.data.heightHitsBounds);
				int3 size = slice3D.slice.size;
				for (int j = 0; j < size.y; j++)
				{
					for (int k = 0; k < size.z; k++)
					{
						int num = j * size.x * size.z + k * size.x;
						for (int l = 0; l < size.x; l++)
						{
							int num2 = num + l;
							int num3 = slice3D.InnerCoordinateToOuterIndex(l, j, k);
							Collider collider = heightHits[num2].collider;
							if (collider != null)
							{
								int layer = collider.gameObject.layer;
								if (layerToUnwalkable[layer])
								{
									walkable[num3] = false;
								}
								int num4 = layerToTag[layer];
								if ((num4 & 1073741824) != 0)
								{
									tags[num3] = num4 & 255;
								}
							}
						}
					}
				}
			});
		}

		// Token: 0x0400099B RID: 2459
		public RulePerLayerModifications.PerLayerRule[] layerRules = new RulePerLayerModifications.PerLayerRule[0];

		// Token: 0x0400099C RID: 2460
		private const int SetTagBit = 1073741824;

		// Token: 0x02000211 RID: 529
		public struct PerLayerRule
		{
			// Token: 0x0400099D RID: 2461
			public int layer;

			// Token: 0x0400099E RID: 2462
			public RulePerLayerModifications.RuleAction action;

			// Token: 0x0400099F RID: 2463
			public int tag;
		}

		// Token: 0x02000212 RID: 530
		public enum RuleAction
		{
			// Token: 0x040009A1 RID: 2465
			SetTag,
			// Token: 0x040009A2 RID: 2466
			MakeUnwalkable
		}
	}
}
