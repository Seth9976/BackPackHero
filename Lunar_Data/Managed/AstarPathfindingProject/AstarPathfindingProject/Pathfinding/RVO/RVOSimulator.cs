using System;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020002AC RID: 684
	[ExecuteInEditMode]
	[AddComponentMenu("Pathfinding/Local Avoidance/RVO Simulator")]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/rvosimulator.html")]
	public class RVOSimulator : VersionedMonoBehaviour
	{
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x00066486 File Offset: 0x00064686
		// (set) Token: 0x06001048 RID: 4168 RVA: 0x0006648D File Offset: 0x0006468D
		public static RVOSimulator active { get; private set; }

		// Token: 0x06001049 RID: 4169 RVA: 0x00066495 File Offset: 0x00064695
		public SimulatorBurst GetSimulator()
		{
			if (this.simulatorBurst == null && Application.isPlaying)
			{
				this.simulatorBurst = new SimulatorBurst(this.movementPlane);
			}
			return this.simulatorBurst;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x000664C0 File Offset: 0x000646C0
		private void OnEnable()
		{
			if (RVOSimulator.active != null)
			{
				if (RVOSimulator.active != this && Application.isPlaying)
				{
					if (base.enabled)
					{
						Debug.LogWarning("Another RVOSimulator component is already in the scene. More than one RVOSimulator component cannot be active at the same time. Disabling this one.", this);
					}
					base.enabled = false;
				}
				return;
			}
			RVOSimulator.active = this;
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x00066510 File Offset: 0x00064710
		private void Update()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (this.desiredSimulationFPS < 1)
			{
				this.desiredSimulationFPS = 1;
			}
			SimulatorBurst simulator = this.GetSimulator();
			simulator.DesiredDeltaTime = 1f / (float)this.desiredSimulationFPS;
			simulator.SymmetryBreakingBias = this.symmetryBreakingBias;
			simulator.HardCollisions = this.hardCollisions;
			simulator.drawQuadtree = this.drawQuadtree;
			simulator.UseNavmeshAsObstacle = this.useNavmeshAsObstacle;
			simulator.Update(default(JobHandle), Time.deltaTime, true, Allocator.TempJob).Complete();
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0006659B File Offset: 0x0006479B
		private void OnDisable()
		{
			if (RVOSimulator.active == this)
			{
				RVOSimulator.active = null;
			}
			if (this.simulatorBurst != null)
			{
				this.simulatorBurst.OnDestroy();
				this.simulatorBurst = null;
			}
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x000033F6 File Offset: 0x000015F6
		public override void DrawGizmos()
		{
		}

		// Token: 0x04000C6D RID: 3181
		[Tooltip("Desired FPS for rvo simulation. It is usually not necessary to run a crowd simulation at a very high fps.\nUsually 10-30 fps is enough, but can be increased for better quality.\nThe rvo simulation will never run at a higher fps than the game")]
		public int desiredSimulationFPS = 20;

		// Token: 0x04000C6E RID: 3182
		[Tooltip("Number of RVO worker threads. If set to None, no multithreading will be used.")]
		[Obsolete("The number of worker threads is now set by the unity job system", true)]
		public ThreadCount workerThreads = ThreadCount.Two;

		// Token: 0x04000C6F RID: 3183
		[Tooltip("Calculate local avoidance in between frames.\nThis can increase jitter in the agents' movement so use it only if you really need the performance boost. It will also reduce the responsiveness of the agents to the commands you send to them.")]
		[Obsolete("Double buffering has been removed")]
		public bool doubleBuffering;

		// Token: 0x04000C70 RID: 3184
		public bool hardCollisions = true;

		// Token: 0x04000C71 RID: 3185
		[Tooltip("Bias agents to pass each other on the right side.\nIf the desired velocity of an agent puts it on a collision course with another agent or an obstacle its desired velocity will be rotated this number of radians (1 radian is approximately 57°) to the right. This helps to break up symmetries and makes it possible to resolve some situations much faster.\n\nWhen many agents have the same goal this can however have the side effect that the group clustered around the target point may as a whole start to spin around the target point.")]
		[Range(0f, 0.2f)]
		public float symmetryBreakingBias = 0.1f;

		// Token: 0x04000C72 RID: 3186
		[Tooltip("Determines if the XY (2D) or XZ (3D) plane is used for movement")]
		public MovementPlane movementPlane;

		// Token: 0x04000C73 RID: 3187
		public bool useNavmeshAsObstacle;

		// Token: 0x04000C74 RID: 3188
		public bool drawQuadtree;

		// Token: 0x04000C75 RID: 3189
		private SimulatorBurst simulatorBurst;
	}
}
