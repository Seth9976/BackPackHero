using System;

namespace Pathfinding.RVO
{
	// Token: 0x02000290 RID: 656
	[Flags]
	public enum AgentDebugFlags : byte
	{
		// Token: 0x04000BA5 RID: 2981
		Nothing = 0,
		// Token: 0x04000BA6 RID: 2982
		ObstacleVOs = 1,
		// Token: 0x04000BA7 RID: 2983
		AgentVOs = 2,
		// Token: 0x04000BA8 RID: 2984
		ReachedState = 4,
		// Token: 0x04000BA9 RID: 2985
		DesiredVelocity = 8,
		// Token: 0x04000BAA RID: 2986
		ChosenVelocity = 16,
		// Token: 0x04000BAB RID: 2987
		Obstacles = 32,
		// Token: 0x04000BAC RID: 2988
		ForwardClearance = 64
	}
}
