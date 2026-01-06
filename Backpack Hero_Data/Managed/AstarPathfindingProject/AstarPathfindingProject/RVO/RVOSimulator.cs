using System;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020000D9 RID: 217
	[ExecuteInEditMode]
	[AddComponentMenu("Pathfinding/Local Avoidance/RVO Simulator")]
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_r_v_o_1_1_r_v_o_simulator.php")]
	public class RVOSimulator : VersionedMonoBehaviour
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x0003D5F6 File Offset: 0x0003B7F6
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x0003D5FD File Offset: 0x0003B7FD
		public static RVOSimulator active { get; private set; }

		// Token: 0x06000958 RID: 2392 RVA: 0x0003D605 File Offset: 0x0003B805
		public Simulator GetSimulator()
		{
			if (this.simulator == null)
			{
				this.Awake();
			}
			return this.simulator;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0003D61B File Offset: 0x0003B81B
		private void OnEnable()
		{
			RVOSimulator.active = this;
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0003D624 File Offset: 0x0003B824
		protected override void Awake()
		{
			base.Awake();
			RVOSimulator.active = this;
			if (this.simulator == null && Application.isPlaying)
			{
				int num = AstarPath.CalculateThreadCount(this.workerThreads);
				this.simulator = new Simulator(num, this.doubleBuffering, this.movementPlane);
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0003D670 File Offset: 0x0003B870
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
			Simulator simulator = this.GetSimulator();
			simulator.DesiredDeltaTime = 1f / (float)this.desiredSimulationFPS;
			simulator.symmetryBreakingBias = this.symmetryBreakingBias;
			simulator.Update();
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0003D6BF File Offset: 0x0003B8BF
		private void OnDestroy()
		{
			RVOSimulator.active = null;
			if (this.simulator != null)
			{
				this.simulator.OnDestroy();
			}
		}

		// Token: 0x04000577 RID: 1399
		[Tooltip("Desired FPS for rvo simulation. It is usually not necessary to run a crowd simulation at a very high fps.\nUsually 10-30 fps is enough, but can be increased for better quality.\nThe rvo simulation will never run at a higher fps than the game")]
		public int desiredSimulationFPS = 20;

		// Token: 0x04000578 RID: 1400
		[Tooltip("Number of RVO worker threads. If set to None, no multithreading will be used.")]
		public ThreadCount workerThreads = ThreadCount.Two;

		// Token: 0x04000579 RID: 1401
		[Tooltip("Calculate local avoidance in between frames.\nThis can increase jitter in the agents' movement so use it only if you really need the performance boost. It will also reduce the responsiveness of the agents to the commands you send to them.")]
		public bool doubleBuffering;

		// Token: 0x0400057A RID: 1402
		[Tooltip("Bias agents to pass each other on the right side.\nIf the desired velocity of an agent puts it on a collision course with another agent or an obstacle its desired velocity will be rotated this number of radians (1 radian is approximately 57°) to the right. This helps to break up symmetries and makes it possible to resolve some situations much faster.\n\nWhen many agents have the same goal this can however have the side effect that the group clustered around the target point may as a whole start to spin around the target point.")]
		[Range(0f, 0.2f)]
		public float symmetryBreakingBias = 0.1f;

		// Token: 0x0400057B RID: 1403
		[Tooltip("Determines if the XY (2D) or XZ (3D) plane is used for movement")]
		public MovementPlane movementPlane;

		// Token: 0x0400057C RID: 1404
		public bool drawObstacles;

		// Token: 0x0400057D RID: 1405
		private Simulator simulator;
	}
}
