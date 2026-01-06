using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020002AF RID: 687
	[HelpURL("https://arongranberg.com/astar/documentation/stable/turnbasedai.html")]
	public class TurnBasedAI : VersionedMonoBehaviour
	{
		// Token: 0x0600105A RID: 4186 RVA: 0x00066772 File Offset: 0x00064972
		private void Start()
		{
			this.blocker.BlockAtCurrentPosition();
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0006677F File Offset: 0x0006497F
		protected override void Awake()
		{
			base.Awake();
			this.traversalProvider = new BlockManager.TraversalProvider(this.blockManager, BlockManager.BlockMode.AllExceptSelector, new List<SingleNodeBlocker> { this.blocker });
		}

		// Token: 0x04000C7A RID: 3194
		public int movementPoints = 2;

		// Token: 0x04000C7B RID: 3195
		public BlockManager blockManager;

		// Token: 0x04000C7C RID: 3196
		public SingleNodeBlocker blocker;

		// Token: 0x04000C7D RID: 3197
		public GraphNode targetNode;

		// Token: 0x04000C7E RID: 3198
		public BlockManager.TraversalProvider traversalProvider;
	}
}
