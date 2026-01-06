using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020000DC RID: 220
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_examples_1_1_turn_based_a_i.php")]
	public class TurnBasedAI : VersionedMonoBehaviour
	{
		// Token: 0x0600099C RID: 2460 RVA: 0x0003E8BB File Offset: 0x0003CABB
		private void Start()
		{
			this.blocker.BlockAtCurrentPosition();
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0003E8C8 File Offset: 0x0003CAC8
		protected override void Awake()
		{
			base.Awake();
			this.traversalProvider = new BlockManager.TraversalProvider(this.blockManager, BlockManager.BlockMode.AllExceptSelector, new List<SingleNodeBlocker> { this.blocker });
		}

		// Token: 0x040005AF RID: 1455
		public int movementPoints = 2;

		// Token: 0x040005B0 RID: 1456
		public BlockManager blockManager;

		// Token: 0x040005B1 RID: 1457
		public SingleNodeBlocker blocker;

		// Token: 0x040005B2 RID: 1458
		public GraphNode targetNode;

		// Token: 0x040005B3 RID: 1459
		public BlockManager.TraversalProvider traversalProvider;
	}
}
