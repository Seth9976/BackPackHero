using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020000E0 RID: 224
	[RequireComponent(typeof(Seeker))]
	[Obsolete("This script has been replaced by Pathfinding.Examples.MineBotAnimation. Any uses of this script in the Unity editor will be automatically replaced by one AIPath component and one MineBotAnimation component.")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_mine_bot_a_i.php")]
	public class MineBotAI : AIPath
	{
		// Token: 0x040005CB RID: 1483
		public Animation anim;

		// Token: 0x040005CC RID: 1484
		public float sleepVelocity = 0.4f;

		// Token: 0x040005CD RID: 1485
		public float animationSpeed = 0.2f;

		// Token: 0x040005CE RID: 1486
		public GameObject endOfPathEffect;
	}
}
