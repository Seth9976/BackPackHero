using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x02000291 RID: 657
	public interface IAgent
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000F79 RID: 3961
		int AgentIndex { get; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000F7A RID: 3962
		// (set) Token: 0x06000F7B RID: 3963
		Vector3 Position { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000F7C RID: 3964
		Vector3 CalculatedTargetPoint { get; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000F7D RID: 3965
		bool AvoidingAnyAgents { get; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000F7E RID: 3966
		float CalculatedSpeed { get; }

		// Token: 0x06000F7F RID: 3967
		void SetTarget(Vector3 targetPoint, float desiredSpeed, float maxSpeed, Vector3 endOfPath);

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000F80 RID: 3968
		// (set) Token: 0x06000F81 RID: 3969
		SimpleMovementPlane MovementPlane { get; set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000F82 RID: 3970
		// (set) Token: 0x06000F83 RID: 3971
		bool Locked { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000F84 RID: 3972
		// (set) Token: 0x06000F85 RID: 3973
		float Radius { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000F86 RID: 3974
		// (set) Token: 0x06000F87 RID: 3975
		float Height { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000F88 RID: 3976
		// (set) Token: 0x06000F89 RID: 3977
		float AgentTimeHorizon { get; set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000F8A RID: 3978
		// (set) Token: 0x06000F8B RID: 3979
		float ObstacleTimeHorizon { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000F8C RID: 3980
		// (set) Token: 0x06000F8D RID: 3981
		int MaxNeighbours { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000F8E RID: 3982
		int NeighbourCount { get; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000F8F RID: 3983
		// (set) Token: 0x06000F90 RID: 3984
		RVOLayer Layer { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000F91 RID: 3985
		// (set) Token: 0x06000F92 RID: 3986
		RVOLayer CollidesWith { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000F93 RID: 3987
		// (set) Token: 0x06000F94 RID: 3988
		float FlowFollowingStrength { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000F95 RID: 3989
		// (set) Token: 0x06000F96 RID: 3990
		AgentDebugFlags DebugFlags { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000F97 RID: 3991
		// (set) Token: 0x06000F98 RID: 3992
		float Priority { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000F99 RID: 3993
		// (set) Token: 0x06000F9A RID: 3994
		int HierarchicalNodeIndex { get; set; }

		// Token: 0x17000229 RID: 553
		// (set) Token: 0x06000F9B RID: 3995
		Action PreCalculationCallback { set; }

		// Token: 0x1700022A RID: 554
		// (set) Token: 0x06000F9C RID: 3996
		Action DestroyedCallback { set; }

		// Token: 0x06000F9D RID: 3997
		void SetCollisionNormal(Vector3 normal);

		// Token: 0x06000F9E RID: 3998
		void ForceSetVelocity(Vector3 velocity);

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000F9F RID: 3999
		ReachedEndOfPath CalculatedEffectivelyReachedDestination { get; }

		// Token: 0x06000FA0 RID: 4000
		void SetObstacleQuery(GraphNode sourceNode);
	}
}
