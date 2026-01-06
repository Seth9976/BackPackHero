using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pathfinding.Drawing;
using Pathfinding.ECS.RVO;
using Pathfinding.Jobs;
using Pathfinding.Util;
using Unity.Burst.CompilerServices;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x02000298 RID: 664
	public class SimulatorBurst
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x00061515 File Offset: 0x0005F715
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x0006151D File Offset: 0x0005F71D
		public float DesiredDeltaTime
		{
			get
			{
				return this.desiredDeltaTime;
			}
			set
			{
				this.desiredDeltaTime = Math.Max(value, 0f);
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x00061530 File Offset: 0x0005F730
		// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x00061538 File Offset: 0x0005F738
		public float SymmetryBreakingBias { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00061541 File Offset: 0x0005F741
		// (set) Token: 0x06000FA6 RID: 4006 RVA: 0x00061549 File Offset: 0x0005F749
		public bool HardCollisions { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00061552 File Offset: 0x0005F752
		// (set) Token: 0x06000FA8 RID: 4008 RVA: 0x0006155A File Offset: 0x0005F75A
		public bool UseNavmeshAsObstacle { get; set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x00061564 File Offset: 0x0005F764
		public Rect AgentBounds
		{
			get
			{
				this.lastJob.Complete();
				return this.quadtree.bounds;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x0006158A File Offset: 0x0005F78A
		public int AgentCount
		{
			get
			{
				return this.numAgents;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x00061592 File Offset: 0x0005F792
		public MovementPlane MovementPlane
		{
			get
			{
				return this.movementPlane;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0006159A File Offset: 0x0005F79A
		// (set) Token: 0x06000FAD RID: 4013 RVA: 0x000615A2 File Offset: 0x0005F7A2
		public JobHandle lastJob { get; private set; }

		// Token: 0x06000FAE RID: 4014 RVA: 0x000615AC File Offset: 0x0005F7AC
		public void BlockUntilSimulationStepDone()
		{
			this.lastJob.Complete();
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x000615C8 File Offset: 0x0005F7C8
		public SimulatorBurst(MovementPlane movementPlane)
		{
			this.DesiredDeltaTime = 1f;
			this.movementPlane = movementPlane;
			this.obstacleData.Init(Allocator.Persistent);
			this.AllocateAgentSpace();
			this.quadtree.BuildJob(this.simulationData.position, this.simulationData.version, this.simulationData.desiredSpeed, this.simulationData.radius, 0, movementPlane).Run<RVOQuadtreeBurst.JobBuild>();
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0006166C File Offset: 0x0005F86C
		public void ClearAgents()
		{
			this.BlockUntilSimulationStepDone();
			for (int i = 0; i < this.agentDestroyCallbacks.Length; i++)
			{
				Action action = this.agentDestroyCallbacks[i];
				if (action != null)
				{
					action();
				}
			}
			this.numAgents = 0;
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x000616AC File Offset: 0x0005F8AC
		public void OnDestroy()
		{
			this.debugDrawingScope.Dispose();
			this.BlockUntilSimulationStepDone();
			this.ClearAgents();
			this.obstacleData.Dispose();
			this.simulationData.Dispose();
			this.temporaryAgentData.Dispose();
			this.outputData.Dispose();
			this.quadtree.Dispose();
			this.horizonAgentData.Dispose();
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00061714 File Offset: 0x0005F914
		private void AllocateAgentSpace()
		{
			if (this.numAgents > this.agentPreCalculationCallbacks.Length || this.agentPreCalculationCallbacks.Length == 0)
			{
				int length = this.simulationData.version.Length;
				int num = Mathf.Max(64, Mathf.Max(this.numAgents, this.agentPreCalculationCallbacks.Length * 2));
				this.simulationData.Realloc(num, Allocator.Persistent);
				this.temporaryAgentData.Realloc(num, Allocator.Persistent);
				this.outputData.Realloc(num, Allocator.Persistent);
				this.horizonAgentData.Realloc(num, Allocator.Persistent);
				Memory.Realloc<Action>(ref this.agentPreCalculationCallbacks, num);
				Memory.Realloc<Action>(ref this.agentDestroyCallbacks, num);
				for (int i = length; i < num; i++)
				{
					this.simulationData.version[i] = new AgentIndex(0, i);
				}
			}
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x000617D9 File Offset: 0x0005F9D9
		[Obsolete("Use AddAgent(Vector3) instead")]
		public IAgent AddAgent(Vector2 position, float elevationCoordinate)
		{
			if (this.movementPlane == MovementPlane.XY)
			{
				return this.AddAgent(new Vector3(position.x, position.y, elevationCoordinate));
			}
			return this.AddAgent(new Vector3(position.x, elevationCoordinate, position.y));
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x00061818 File Offset: 0x0005FA18
		public IAgent AddAgent(Vector3 position)
		{
			AgentIndex agentIndex = this.AddAgentBurst(position);
			return new SimulatorBurst.Agent
			{
				simulator = this,
				agentIndex = agentIndex
			};
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00061850 File Offset: 0x0005FA50
		public AgentIndex AddAgentBurst(float3 position)
		{
			this.BlockUntilSimulationStepDone();
			int num;
			if (this.freeAgentIndices.Count > 0)
			{
				num = this.freeAgentIndices.Pop();
			}
			else
			{
				int num2 = this.numAgents;
				this.numAgents = num2 + 1;
				num = num2;
				this.AllocateAgentSpace();
			}
			AgentIndex agentIndex = this.simulationData.version[num].WithIncrementedVersion();
			this.simulationData.version[num] = agentIndex;
			this.simulationData.radius[num] = 5f;
			this.simulationData.height[num] = 5f;
			this.simulationData.desiredSpeed[num] = 0f;
			this.simulationData.maxSpeed[num] = 1f;
			this.simulationData.agentTimeHorizon[num] = 2f;
			this.simulationData.obstacleTimeHorizon[num] = 2f;
			this.simulationData.locked[num] = false;
			this.simulationData.maxNeighbours[num] = 10;
			this.simulationData.layer[num] = RVOLayer.DefaultAgent;
			this.simulationData.collidesWith[num] = (RVOLayer)(-1);
			this.simulationData.flowFollowingStrength[num] = 0f;
			this.simulationData.position[num] = position;
			this.simulationData.collisionNormal[num] = float3.zero;
			this.simulationData.manuallyControlled[num] = false;
			this.simulationData.priority[num] = 0.5f;
			this.simulationData.debugFlags[num] = AgentDebugFlags.Nothing;
			this.simulationData.targetPoint[num] = position;
			this.simulationData.movementPlane[num] = new NativeMovementPlane(((this.movementPlane == MovementPlane.XY) ? SimpleMovementPlane.XYPlane : SimpleMovementPlane.XZPlane).rotation);
			this.simulationData.allowedVelocityDeviationAngles[num] = float2.zero;
			this.simulationData.endOfPath[num] = float3.zero;
			this.simulationData.agentObstacleMapping[num] = -1;
			this.simulationData.hierarchicalNodeIndex[num] = -1;
			this.outputData.speed[num] = 0f;
			this.outputData.numNeighbours[num] = 0;
			this.outputData.targetPoint[num] = position;
			this.outputData.blockedByAgents[num * 7] = -1;
			this.outputData.effectivelyReachedDestination[num] = ReachedEndOfPath.NotReached;
			this.horizonAgentData.horizonSide[num] = 0;
			this.agentPreCalculationCallbacks[num] = null;
			this.agentDestroyCallbacks[num] = null;
			return agentIndex;
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00061B24 File Offset: 0x0005FD24
		[Obsolete("Use AddAgent(Vector3) instead")]
		public IAgent AddAgent(IAgent agent)
		{
			throw new NotImplementedException("Use AddAgent(position) instead. Agents are not persistent after being removed.");
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00061B30 File Offset: 0x0005FD30
		public void RemoveAgent(IAgent agent)
		{
			if (agent == null)
			{
				throw new ArgumentNullException("agent");
			}
			SimulatorBurst.Agent agent2 = (SimulatorBurst.Agent)agent;
			this.RemoveAgent(agent2.agentIndex);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00061B60 File Offset: 0x0005FD60
		public bool AgentExists(AgentIndex agent)
		{
			this.BlockUntilSimulationStepDone();
			if (!this.simulationData.version.IsCreated)
			{
				return false;
			}
			int index = agent.Index;
			return index < this.simulationData.version.Length && agent.Version == this.simulationData.version[index].Version;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00061BCC File Offset: 0x0005FDCC
		public void RemoveAgent(AgentIndex agent)
		{
			this.BlockUntilSimulationStepDone();
			if (!this.AgentExists(agent))
			{
				throw new InvalidOperationException("Trying to remove agent which does not exist");
			}
			int index = agent.Index;
			this.simulationData.version[index] = this.simulationData.version[index].WithIncrementedVersion().WithDeleted();
			this.agentPreCalculationCallbacks[index] = null;
			try
			{
				if (this.agentDestroyCallbacks[index] != null)
				{
					this.agentDestroyCallbacks[index]();
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
			this.agentDestroyCallbacks[index] = null;
			this.freeAgentIndices.Push(index);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00061C7C File Offset: 0x0005FE7C
		private void PreCalculation(JobHandle dependency)
		{
			bool flag = false;
			for (int i = 0; i < this.numAgents; i++)
			{
				Action action = this.agentPreCalculationCallbacks[i];
				if (action != null)
				{
					if (!flag)
					{
						dependency.Complete();
						flag = true;
					}
					action();
				}
			}
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00061CBC File Offset: 0x0005FEBC
		public JobHandle Update(JobHandle dependency, float dt, bool drawGizmos, Allocator allocator)
		{
			if (false)
			{
				default(JobRVO<XYMovementPlane>).ScheduleBatch(0, 0, default(JobHandle));
				default(JobRVO<XZMovementPlane>).ScheduleBatch(0, 0, default(JobHandle));
				default(JobRVO<ArbitraryMovementPlane>).ScheduleBatch(0, 0, default(JobHandle));
				default(JobRVOCalculateNeighbours<XYMovementPlane>).ScheduleBatch(0, 0, default(JobHandle));
				default(JobRVOCalculateNeighbours<XZMovementPlane>).ScheduleBatch(0, 0, default(JobHandle));
				default(JobRVOCalculateNeighbours<ArbitraryMovementPlane>).ScheduleBatch(0, 0, default(JobHandle));
				default(JobHardCollisions<XYMovementPlane>).ScheduleBatch(0, 0, default(JobHandle));
				default(JobHardCollisions<XZMovementPlane>).ScheduleBatch(0, 0, default(JobHandle));
				default(JobHardCollisions<ArbitraryMovementPlane>).ScheduleBatch(0, 0, default(JobHandle));
				default(JobDestinationReached<XYMovementPlane>).Schedule(default(JobHandle));
				default(JobDestinationReached<XZMovementPlane>).Schedule(default(JobHandle));
				default(JobDestinationReached<ArbitraryMovementPlane>).Schedule(default(JobHandle));
			}
			if (this.movementPlane == MovementPlane.XY)
			{
				return this.UpdateInternal<XYMovementPlane>(dependency, dt, drawGizmos, allocator);
			}
			if (this.movementPlane == MovementPlane.XZ)
			{
				return this.UpdateInternal<XZMovementPlane>(dependency, dt, drawGizmos, allocator);
			}
			return this.UpdateInternal<ArbitraryMovementPlane>(dependency, dt, drawGizmos, allocator);
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00061E3E File Offset: 0x0006003E
		public void LockSimulationDataReadOnly(JobHandle dependencies)
		{
			this.lastJob = JobHandle.CombineDependencies(this.lastJob, dependencies);
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x00061E54 File Offset: 0x00060054
		private JobHandle UpdateInternal<T>(JobHandle dependency, float deltaTime, bool drawGizmos, Allocator allocator) where T : struct, IMovementPlaneWrapper
		{
			deltaTime = math.max(deltaTime, 0.0005f);
			this.BlockUntilSimulationStepDone();
			this.PreCalculation(dependency);
			JobHandle jobHandle = this.quadtree.BuildJob(this.simulationData.position, this.simulationData.version, this.outputData.speed, this.simulationData.radius, this.numAgents, this.movementPlane).Schedule(dependency);
			JobHandle jobHandle2 = new JobRVOPreprocess
			{
				agentData = this.simulationData,
				previousOutput = this.outputData,
				temporaryAgentData = this.temporaryAgentData,
				startIndex = 0,
				endIndex = this.numAgents
			}.Schedule(dependency);
			int num = math.max(this.numAgents / 64, 8);
			JobHandle jobHandle3 = new JobRVOCalculateNeighbours<T>
			{
				agentData = this.simulationData,
				quadtree = this.quadtree,
				outNeighbours = this.temporaryAgentData.neighbours,
				output = this.outputData
			}.ScheduleBatch(this.numAgents, num, JobHandle.CombineDependencies(jobHandle2, jobHandle));
			JobHandle.ScheduleBatchedJobs();
			JobHandle jobHandle4 = JobHandle.CombineDependencies(jobHandle2, jobHandle3);
			this.debugDrawingScope.Rewind();
			CommandBuilder builder = DrawingManager.GetBuilder(this.debugDrawingScope, false);
			JobHandle jobHandle5 = new JobHorizonAvoidancePhase1
			{
				agentData = this.simulationData,
				neighbours = this.temporaryAgentData.neighbours,
				desiredTargetPointInVelocitySpace = this.temporaryAgentData.desiredTargetPointInVelocitySpace,
				horizonAgentData = this.horizonAgentData,
				draw = builder
			}.ScheduleBatch(this.numAgents, num, jobHandle4);
			JobHandle jobHandle6 = new JobHorizonAvoidancePhase2
			{
				neighbours = this.temporaryAgentData.neighbours,
				versions = this.simulationData.version,
				desiredVelocity = this.temporaryAgentData.desiredVelocity,
				desiredTargetPointInVelocitySpace = this.temporaryAgentData.desiredTargetPointInVelocitySpace,
				horizonAgentData = this.horizonAgentData,
				movementPlane = this.simulationData.movementPlane
			}.ScheduleBatch(this.numAgents, num, jobHandle5);
			JobHandle jobHandle7 = new JobHardCollisions<T>
			{
				agentData = this.simulationData,
				neighbours = this.temporaryAgentData.neighbours,
				collisionVelocityOffsets = this.temporaryAgentData.collisionVelocityOffsets,
				deltaTime = deltaTime,
				enabled = this.HardCollisions
			}.ScheduleBatch(this.numAgents, num, jobHandle4);
			bool flag = AstarPath.active != null;
			RWLock.CombinedReadLockAsync combinedReadLockAsync;
			NavmeshEdges.NavmeshBorderData navmeshBorderData;
			if (flag)
			{
				navmeshBorderData = AstarPath.active.GetNavmeshBorderData(out combinedReadLockAsync);
			}
			else
			{
				navmeshBorderData = NavmeshEdges.NavmeshBorderData.CreateEmpty(allocator);
				combinedReadLockAsync = default(RWLock.CombinedReadLockAsync);
			}
			JobRVO<T> jobRVO = new JobRVO<T>
			{
				agentData = this.simulationData,
				temporaryAgentData = this.temporaryAgentData,
				navmeshEdgeData = navmeshBorderData,
				output = this.outputData,
				deltaTime = deltaTime,
				symmetryBreakingBias = Mathf.Max(0f, this.SymmetryBreakingBias),
				draw = builder,
				useNavmeshAsObstacle = this.UseNavmeshAsObstacle,
				priorityMultiplier = 1f
			};
			jobHandle4 = JobHandle.CombineDependencies(jobHandle6, jobHandle7, combinedReadLockAsync.dependency);
			JobHandle jobHandle8 = jobRVO.ScheduleBatch(this.numAgents, num, jobHandle4);
			if (flag)
			{
				combinedReadLockAsync.UnlockAfter(jobHandle8);
			}
			else
			{
				navmeshBorderData.DisposeEmpty(jobHandle8);
			}
			JobHandle jobHandle9 = new JobDestinationReached<T>
			{
				agentData = this.simulationData,
				obstacleData = this.obstacleData,
				temporaryAgentData = this.temporaryAgentData,
				output = this.outputData,
				draw = builder,
				numAgents = this.numAgents
			}.Schedule(jobHandle8);
			JobHandle jobHandle10 = this.simulationData.collisionNormal.MemSet(float3.zero).Schedule(jobHandle9);
			JobHandle jobHandle11 = this.simulationData.manuallyControlled.MemSet(false).Schedule(jobHandle9);
			JobHandle jobHandle12 = this.simulationData.hierarchicalNodeIndex.MemSet(-1).Schedule(jobHandle9);
			dependency = JobHandle.CombineDependencies(jobHandle9, jobHandle10, jobHandle11);
			dependency = JobHandle.CombineDependencies(dependency, jobHandle12);
			if (this.drawQuadtree && drawGizmos)
			{
				dependency = JobHandle.CombineDependencies(dependency, new RVOQuadtreeBurst.DebugDrawJob
				{
					draw = builder,
					quadtree = this.quadtree
				}.Schedule(jobHandle));
			}
			builder.DisposeAfter(dependency, AllowedDelay.EndOfFrame);
			this.lastJob = dependency;
			return dependency;
		}

		// Token: 0x04000BDE RID: 3038
		private float desiredDeltaTime = 0.05f;

		// Token: 0x04000BDF RID: 3039
		private int numAgents;

		// Token: 0x04000BE0 RID: 3040
		private RedrawScope debugDrawingScope;

		// Token: 0x04000BE1 RID: 3041
		public RVOQuadtreeBurst quadtree;

		// Token: 0x04000BE2 RID: 3042
		public bool drawQuadtree;

		// Token: 0x04000BE3 RID: 3043
		private Action[] agentPreCalculationCallbacks = new Action[0];

		// Token: 0x04000BE4 RID: 3044
		private Action[] agentDestroyCallbacks = new Action[0];

		// Token: 0x04000BE5 RID: 3045
		private Stack<int> freeAgentIndices = new Stack<int>();

		// Token: 0x04000BE6 RID: 3046
		private SimulatorBurst.TemporaryAgentData temporaryAgentData;

		// Token: 0x04000BE7 RID: 3047
		private SimulatorBurst.HorizonAgentData horizonAgentData;

		// Token: 0x04000BE8 RID: 3048
		public SimulatorBurst.ObstacleData obstacleData;

		// Token: 0x04000BE9 RID: 3049
		public SimulatorBurst.AgentData simulationData;

		// Token: 0x04000BEA RID: 3050
		public SimulatorBurst.AgentOutputData outputData;

		// Token: 0x04000BEB RID: 3051
		public const int MaxNeighbourCount = 50;

		// Token: 0x04000BEC RID: 3052
		public const int MaxBlockingAgentCount = 7;

		// Token: 0x04000BED RID: 3053
		public const int MaxObstacleVertices = 256;

		// Token: 0x04000BF1 RID: 3057
		public readonly MovementPlane movementPlane;

		// Token: 0x02000299 RID: 665
		private struct Agent : IAgent
		{
			// Token: 0x17000234 RID: 564
			// (get) Token: 0x06000FBE RID: 4030 RVA: 0x000622D3 File Offset: 0x000604D3
			public int AgentIndex
			{
				get
				{
					return this.agentIndex.Index;
				}
			}

			// Token: 0x17000235 RID: 565
			// (get) Token: 0x06000FBF RID: 4031 RVA: 0x000622E0 File Offset: 0x000604E0
			// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x00062302 File Offset: 0x00060502
			public Vector3 Position
			{
				get
				{
					return this.simulator.simulationData.position[this.AgentIndex];
				}
				set
				{
					this.simulator.simulationData.position[this.AgentIndex] = value;
				}
			}

			// Token: 0x17000236 RID: 566
			// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x00062325 File Offset: 0x00060525
			// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x00062342 File Offset: 0x00060542
			public bool Locked
			{
				get
				{
					return this.simulator.simulationData.locked[this.AgentIndex];
				}
				set
				{
					this.simulator.simulationData.locked[this.AgentIndex] = value;
				}
			}

			// Token: 0x17000237 RID: 567
			// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x00062360 File Offset: 0x00060560
			// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x0006237D File Offset: 0x0006057D
			public float Radius
			{
				get
				{
					return this.simulator.simulationData.radius[this.AgentIndex];
				}
				set
				{
					this.simulator.simulationData.radius[this.AgentIndex] = value;
				}
			}

			// Token: 0x17000238 RID: 568
			// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x0006239B File Offset: 0x0006059B
			// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x000623B8 File Offset: 0x000605B8
			public float Height
			{
				get
				{
					return this.simulator.simulationData.height[this.AgentIndex];
				}
				set
				{
					this.simulator.simulationData.height[this.AgentIndex] = value;
				}
			}

			// Token: 0x17000239 RID: 569
			// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x000623D6 File Offset: 0x000605D6
			// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x000623F3 File Offset: 0x000605F3
			public float AgentTimeHorizon
			{
				get
				{
					return this.simulator.simulationData.agentTimeHorizon[this.AgentIndex];
				}
				set
				{
					this.simulator.simulationData.agentTimeHorizon[this.AgentIndex] = value;
				}
			}

			// Token: 0x1700023A RID: 570
			// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x00062411 File Offset: 0x00060611
			// (set) Token: 0x06000FCA RID: 4042 RVA: 0x0006242E File Offset: 0x0006062E
			public float ObstacleTimeHorizon
			{
				get
				{
					return this.simulator.simulationData.obstacleTimeHorizon[this.AgentIndex];
				}
				set
				{
					this.simulator.simulationData.obstacleTimeHorizon[this.AgentIndex] = value;
				}
			}

			// Token: 0x1700023B RID: 571
			// (get) Token: 0x06000FCB RID: 4043 RVA: 0x0006244C File Offset: 0x0006064C
			// (set) Token: 0x06000FCC RID: 4044 RVA: 0x00062469 File Offset: 0x00060669
			public int MaxNeighbours
			{
				get
				{
					return this.simulator.simulationData.maxNeighbours[this.AgentIndex];
				}
				set
				{
					this.simulator.simulationData.maxNeighbours[this.AgentIndex] = value;
				}
			}

			// Token: 0x1700023C RID: 572
			// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00062487 File Offset: 0x00060687
			// (set) Token: 0x06000FCE RID: 4046 RVA: 0x000624A4 File Offset: 0x000606A4
			public RVOLayer Layer
			{
				get
				{
					return this.simulator.simulationData.layer[this.AgentIndex];
				}
				set
				{
					this.simulator.simulationData.layer[this.AgentIndex] = value;
				}
			}

			// Token: 0x1700023D RID: 573
			// (get) Token: 0x06000FCF RID: 4047 RVA: 0x000624C2 File Offset: 0x000606C2
			// (set) Token: 0x06000FD0 RID: 4048 RVA: 0x000624DF File Offset: 0x000606DF
			public RVOLayer CollidesWith
			{
				get
				{
					return this.simulator.simulationData.collidesWith[this.AgentIndex];
				}
				set
				{
					this.simulator.simulationData.collidesWith[this.AgentIndex] = value;
				}
			}

			// Token: 0x1700023E RID: 574
			// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x000624FD File Offset: 0x000606FD
			// (set) Token: 0x06000FD2 RID: 4050 RVA: 0x0006251A File Offset: 0x0006071A
			public float FlowFollowingStrength
			{
				get
				{
					return this.simulator.simulationData.flowFollowingStrength[this.AgentIndex];
				}
				set
				{
					this.simulator.simulationData.flowFollowingStrength[this.AgentIndex] = value;
				}
			}

			// Token: 0x1700023F RID: 575
			// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x00062538 File Offset: 0x00060738
			// (set) Token: 0x06000FD4 RID: 4052 RVA: 0x00062555 File Offset: 0x00060755
			public AgentDebugFlags DebugFlags
			{
				get
				{
					return this.simulator.simulationData.debugFlags[this.AgentIndex];
				}
				set
				{
					this.simulator.simulationData.debugFlags[this.AgentIndex] = value;
				}
			}

			// Token: 0x17000240 RID: 576
			// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00062573 File Offset: 0x00060773
			// (set) Token: 0x06000FD6 RID: 4054 RVA: 0x00062590 File Offset: 0x00060790
			public float Priority
			{
				get
				{
					return this.simulator.simulationData.priority[this.AgentIndex];
				}
				set
				{
					this.simulator.simulationData.priority[this.AgentIndex] = value;
				}
			}

			// Token: 0x17000241 RID: 577
			// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x000625AE File Offset: 0x000607AE
			// (set) Token: 0x06000FD8 RID: 4056 RVA: 0x000625CB File Offset: 0x000607CB
			public int HierarchicalNodeIndex
			{
				get
				{
					return this.simulator.simulationData.hierarchicalNodeIndex[this.AgentIndex];
				}
				set
				{
					this.simulator.simulationData.hierarchicalNodeIndex[this.AgentIndex] = value;
				}
			}

			// Token: 0x17000242 RID: 578
			// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x000625E9 File Offset: 0x000607E9
			// (set) Token: 0x06000FDA RID: 4058 RVA: 0x00062615 File Offset: 0x00060815
			public SimpleMovementPlane MovementPlane
			{
				get
				{
					return new SimpleMovementPlane(this.simulator.simulationData.movementPlane[this.AgentIndex].rotation);
				}
				set
				{
					this.simulator.simulationData.movementPlane[this.AgentIndex] = new NativeMovementPlane(value);
				}
			}

			// Token: 0x17000243 RID: 579
			// (set) Token: 0x06000FDB RID: 4059 RVA: 0x00062638 File Offset: 0x00060838
			public Action PreCalculationCallback
			{
				set
				{
					this.simulator.agentPreCalculationCallbacks[this.AgentIndex] = value;
				}
			}

			// Token: 0x17000244 RID: 580
			// (set) Token: 0x06000FDC RID: 4060 RVA: 0x0006264D File Offset: 0x0006084D
			public Action DestroyedCallback
			{
				set
				{
					this.simulator.agentDestroyCallbacks[this.AgentIndex] = value;
				}
			}

			// Token: 0x17000245 RID: 581
			// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00062662 File Offset: 0x00060862
			public Vector3 CalculatedTargetPoint
			{
				get
				{
					this.simulator.BlockUntilSimulationStepDone();
					return this.simulator.outputData.targetPoint[this.AgentIndex];
				}
			}

			// Token: 0x17000246 RID: 582
			// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0006268F File Offset: 0x0006088F
			public float CalculatedSpeed
			{
				get
				{
					this.simulator.BlockUntilSimulationStepDone();
					return this.simulator.outputData.speed[this.AgentIndex];
				}
			}

			// Token: 0x17000247 RID: 583
			// (get) Token: 0x06000FDF RID: 4063 RVA: 0x000626B7 File Offset: 0x000608B7
			public ReachedEndOfPath CalculatedEffectivelyReachedDestination
			{
				get
				{
					this.simulator.BlockUntilSimulationStepDone();
					return this.simulator.outputData.effectivelyReachedDestination[this.AgentIndex];
				}
			}

			// Token: 0x17000248 RID: 584
			// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x000626DF File Offset: 0x000608DF
			public int NeighbourCount
			{
				get
				{
					this.simulator.BlockUntilSimulationStepDone();
					return this.simulator.outputData.numNeighbours[this.AgentIndex];
				}
			}

			// Token: 0x17000249 RID: 585
			// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00062707 File Offset: 0x00060907
			public bool AvoidingAnyAgents
			{
				get
				{
					this.simulator.BlockUntilSimulationStepDone();
					return this.simulator.outputData.blockedByAgents[this.AgentIndex * 7] != -1;
				}
			}

			// Token: 0x06000FE2 RID: 4066 RVA: 0x00062737 File Offset: 0x00060937
			public void SetObstacleQuery(GraphNode sourceNode)
			{
				this.HierarchicalNodeIndex = ((sourceNode != null && !sourceNode.Destroyed && sourceNode.Walkable) ? sourceNode.HierarchicalNodeIndex : (-1));
			}

			// Token: 0x06000FE3 RID: 4067 RVA: 0x0006275B File Offset: 0x0006095B
			public void SetTarget(Vector3 targetPoint, float desiredSpeed, float maxSpeed, Vector3 endOfPath)
			{
				this.simulator.simulationData.SetTarget(this.AgentIndex, targetPoint, desiredSpeed, maxSpeed, endOfPath);
			}

			// Token: 0x06000FE4 RID: 4068 RVA: 0x00062782 File Offset: 0x00060982
			public void SetCollisionNormal(Vector3 normal)
			{
				this.simulator.simulationData.collisionNormal[this.AgentIndex] = normal;
			}

			// Token: 0x06000FE5 RID: 4069 RVA: 0x000627A8 File Offset: 0x000609A8
			public void ForceSetVelocity(Vector3 velocity)
			{
				this.simulator.simulationData.targetPoint[this.AgentIndex] = this.simulator.simulationData.position[this.AgentIndex] + velocity * 1000f;
				this.simulator.simulationData.desiredSpeed[this.AgentIndex] = velocity.magnitude;
				this.simulator.simulationData.allowedVelocityDeviationAngles[this.AgentIndex] = float2.zero;
				this.simulator.simulationData.manuallyControlled[this.AgentIndex] = true;
			}

			// Token: 0x04000BF3 RID: 3059
			public SimulatorBurst simulator;

			// Token: 0x04000BF4 RID: 3060
			public AgentIndex agentIndex;
		}

		// Token: 0x0200029A RID: 666
		public struct ObstacleData
		{
			// Token: 0x06000FE6 RID: 4070 RVA: 0x00062860 File Offset: 0x00060A60
			public void Init(Allocator allocator)
			{
				if (!this.obstacles.IsCreated)
				{
					this.obstacles = new NativeList<UnmanagedObstacle>(0, allocator);
				}
				if (!this.obstacleVertexGroups.IsCreated)
				{
					this.obstacleVertexGroups = new SlabAllocator<ObstacleVertexGroup>(4, allocator);
				}
				if (!this.obstacleVertices.IsCreated)
				{
					this.obstacleVertices = new SlabAllocator<float3>(16, allocator);
				}
			}

			// Token: 0x06000FE7 RID: 4071 RVA: 0x000628CB File Offset: 0x00060ACB
			public void Dispose()
			{
				if (this.obstacleVertexGroups.IsCreated)
				{
					this.obstacleVertexGroups.Dispose();
					this.obstacleVertices.Dispose();
					this.obstacles.Dispose();
				}
			}

			// Token: 0x04000BF5 RID: 3061
			public SlabAllocator<ObstacleVertexGroup> obstacleVertexGroups;

			// Token: 0x04000BF6 RID: 3062
			public SlabAllocator<float3> obstacleVertices;

			// Token: 0x04000BF7 RID: 3063
			public NativeList<UnmanagedObstacle> obstacles;
		}

		// Token: 0x0200029B RID: 667
		public struct AgentData
		{
			// Token: 0x06000FE8 RID: 4072 RVA: 0x000628FC File Offset: 0x00060AFC
			public void Realloc(int size, Allocator allocator)
			{
				Memory.Realloc<AgentIndex>(ref this.version, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<float>(ref this.radius, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<float>(ref this.height, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<float>(ref this.desiredSpeed, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<float>(ref this.maxSpeed, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<float>(ref this.agentTimeHorizon, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<float>(ref this.obstacleTimeHorizon, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<bool>(ref this.locked, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<int>(ref this.maxNeighbours, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<RVOLayer>(ref this.layer, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<RVOLayer>(ref this.collidesWith, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<float>(ref this.flowFollowingStrength, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<float3>(ref this.position, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<float3>(ref this.collisionNormal, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<bool>(ref this.manuallyControlled, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<float>(ref this.priority, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<AgentDebugFlags>(ref this.debugFlags, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<float3>(ref this.targetPoint, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<NativeMovementPlane>(ref this.movementPlane, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<float2>(ref this.allowedVelocityDeviationAngles, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<float3>(ref this.endOfPath, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<int>(ref this.agentObstacleMapping, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<int>(ref this.hierarchicalNodeIndex, size, allocator, NativeArrayOptions.UninitializedMemory);
			}

			// Token: 0x06000FE9 RID: 4073 RVA: 0x00062A4C File Offset: 0x00060C4C
			public void SetTarget(int agentIndex, float3 targetPoint, float desiredSpeed, float maxSpeed, float3 endOfPath)
			{
				maxSpeed = math.max(maxSpeed, 0f);
				desiredSpeed = math.clamp(desiredSpeed, 0f, maxSpeed);
				this.targetPoint[agentIndex] = targetPoint;
				this.desiredSpeed[agentIndex] = desiredSpeed;
				this.maxSpeed[agentIndex] = maxSpeed;
				this.endOfPath[agentIndex] = endOfPath;
			}

			// Token: 0x06000FEA RID: 4074 RVA: 0x00062AAC File Offset: 0x00060CAC
			[MethodImpl(256)]
			public bool HasDebugFlag(int agentIndex, AgentDebugFlags flag)
			{
				return Hint.Unlikely((this.debugFlags[agentIndex] & flag) > AgentDebugFlags.Nothing);
			}

			// Token: 0x06000FEB RID: 4075 RVA: 0x00062AC4 File Offset: 0x00060CC4
			public void Dispose()
			{
				this.version.Dispose();
				this.radius.Dispose();
				this.height.Dispose();
				this.desiredSpeed.Dispose();
				this.maxSpeed.Dispose();
				this.agentTimeHorizon.Dispose();
				this.obstacleTimeHorizon.Dispose();
				this.locked.Dispose();
				this.maxNeighbours.Dispose();
				this.layer.Dispose();
				this.collidesWith.Dispose();
				this.flowFollowingStrength.Dispose();
				this.position.Dispose();
				this.collisionNormal.Dispose();
				this.manuallyControlled.Dispose();
				this.priority.Dispose();
				this.debugFlags.Dispose();
				this.targetPoint.Dispose();
				this.movementPlane.Dispose();
				this.allowedVelocityDeviationAngles.Dispose();
				this.endOfPath.Dispose();
				this.agentObstacleMapping.Dispose();
				this.hierarchicalNodeIndex.Dispose();
			}

			// Token: 0x04000BF8 RID: 3064
			public NativeArray<AgentIndex> version;

			// Token: 0x04000BF9 RID: 3065
			public NativeArray<float> radius;

			// Token: 0x04000BFA RID: 3066
			public NativeArray<float> height;

			// Token: 0x04000BFB RID: 3067
			public NativeArray<float> desiredSpeed;

			// Token: 0x04000BFC RID: 3068
			public NativeArray<float> maxSpeed;

			// Token: 0x04000BFD RID: 3069
			public NativeArray<float> agentTimeHorizon;

			// Token: 0x04000BFE RID: 3070
			public NativeArray<float> obstacleTimeHorizon;

			// Token: 0x04000BFF RID: 3071
			public NativeArray<bool> locked;

			// Token: 0x04000C00 RID: 3072
			public NativeArray<int> maxNeighbours;

			// Token: 0x04000C01 RID: 3073
			public NativeArray<RVOLayer> layer;

			// Token: 0x04000C02 RID: 3074
			public NativeArray<RVOLayer> collidesWith;

			// Token: 0x04000C03 RID: 3075
			public NativeArray<float> flowFollowingStrength;

			// Token: 0x04000C04 RID: 3076
			public NativeArray<float3> position;

			// Token: 0x04000C05 RID: 3077
			public NativeArray<float3> collisionNormal;

			// Token: 0x04000C06 RID: 3078
			public NativeArray<bool> manuallyControlled;

			// Token: 0x04000C07 RID: 3079
			public NativeArray<float> priority;

			// Token: 0x04000C08 RID: 3080
			public NativeArray<AgentDebugFlags> debugFlags;

			// Token: 0x04000C09 RID: 3081
			public NativeArray<float3> targetPoint;

			// Token: 0x04000C0A RID: 3082
			public NativeArray<float2> allowedVelocityDeviationAngles;

			// Token: 0x04000C0B RID: 3083
			public NativeArray<NativeMovementPlane> movementPlane;

			// Token: 0x04000C0C RID: 3084
			public NativeArray<float3> endOfPath;

			// Token: 0x04000C0D RID: 3085
			public NativeArray<int> agentObstacleMapping;

			// Token: 0x04000C0E RID: 3086
			public NativeArray<int> hierarchicalNodeIndex;
		}

		// Token: 0x0200029C RID: 668
		public struct AgentOutputData
		{
			// Token: 0x06000FEC RID: 4076 RVA: 0x00062BD0 File Offset: 0x00060DD0
			public void Realloc(int size, Allocator allocator)
			{
				Memory.Realloc<float3>(ref this.targetPoint, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<float>(ref this.speed, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<int>(ref this.numNeighbours, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<int>(ref this.blockedByAgents, size * 7, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<ReachedEndOfPath>(ref this.effectivelyReachedDestination, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<float>(ref this.forwardClearance, size, allocator, NativeArrayOptions.UninitializedMemory);
			}

			// Token: 0x06000FED RID: 4077 RVA: 0x00062C34 File Offset: 0x00060E34
			public void Move(int fromIndex, int toIndex)
			{
				this.targetPoint[toIndex] = this.targetPoint[fromIndex];
				this.speed[toIndex] = this.speed[fromIndex];
				this.numNeighbours[toIndex] = this.numNeighbours[fromIndex];
				this.effectivelyReachedDestination[toIndex] = this.effectivelyReachedDestination[fromIndex];
				for (int i = 0; i < 7; i++)
				{
					this.blockedByAgents[toIndex * 7 + i] = this.blockedByAgents[fromIndex * 7 + i];
				}
				this.forwardClearance[toIndex] = this.forwardClearance[fromIndex];
			}

			// Token: 0x06000FEE RID: 4078 RVA: 0x00062CE8 File Offset: 0x00060EE8
			public void Dispose()
			{
				this.targetPoint.Dispose();
				this.speed.Dispose();
				this.numNeighbours.Dispose();
				this.blockedByAgents.Dispose();
				this.effectivelyReachedDestination.Dispose();
				this.forwardClearance.Dispose();
			}

			// Token: 0x04000C0F RID: 3087
			public NativeArray<float3> targetPoint;

			// Token: 0x04000C10 RID: 3088
			public NativeArray<float> speed;

			// Token: 0x04000C11 RID: 3089
			public NativeArray<int> numNeighbours;

			// Token: 0x04000C12 RID: 3090
			[NativeDisableParallelForRestriction]
			public NativeArray<int> blockedByAgents;

			// Token: 0x04000C13 RID: 3091
			public NativeArray<ReachedEndOfPath> effectivelyReachedDestination;

			// Token: 0x04000C14 RID: 3092
			public NativeArray<float> forwardClearance;
		}

		// Token: 0x0200029D RID: 669
		public struct HorizonAgentData
		{
			// Token: 0x06000FEF RID: 4079 RVA: 0x00062D37 File Offset: 0x00060F37
			public void Realloc(int size, Allocator allocator)
			{
				Memory.Realloc<int>(ref this.horizonSide, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<float>(ref this.horizonMinAngle, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<float>(ref this.horizonMaxAngle, size, allocator, NativeArrayOptions.UninitializedMemory);
			}

			// Token: 0x06000FF0 RID: 4080 RVA: 0x00062D63 File Offset: 0x00060F63
			public void Move(int fromIndex, int toIndex)
			{
				this.horizonSide[toIndex] = this.horizonSide[fromIndex];
			}

			// Token: 0x06000FF1 RID: 4081 RVA: 0x00062D7D File Offset: 0x00060F7D
			public void Dispose()
			{
				this.horizonSide.Dispose();
				this.horizonMinAngle.Dispose();
				this.horizonMaxAngle.Dispose();
			}

			// Token: 0x04000C15 RID: 3093
			public NativeArray<int> horizonSide;

			// Token: 0x04000C16 RID: 3094
			public NativeArray<float> horizonMinAngle;

			// Token: 0x04000C17 RID: 3095
			public NativeArray<float> horizonMaxAngle;
		}

		// Token: 0x0200029E RID: 670
		public struct TemporaryAgentData
		{
			// Token: 0x06000FF2 RID: 4082 RVA: 0x00062DA0 File Offset: 0x00060FA0
			public void Realloc(int size, Allocator allocator)
			{
				Memory.Realloc<float2>(ref this.desiredTargetPointInVelocitySpace, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<float3>(ref this.desiredVelocity, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<float3>(ref this.currentVelocity, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<float2>(ref this.collisionVelocityOffsets, size, allocator, NativeArrayOptions.UninitializedMemory);
				Memory.Realloc<int>(ref this.neighbours, size * 50, allocator, NativeArrayOptions.UninitializedMemory);
			}

			// Token: 0x06000FF3 RID: 4083 RVA: 0x00062DF6 File Offset: 0x00060FF6
			public void Dispose()
			{
				this.desiredTargetPointInVelocitySpace.Dispose();
				this.desiredVelocity.Dispose();
				this.currentVelocity.Dispose();
				this.neighbours.Dispose();
				this.collisionVelocityOffsets.Dispose();
			}

			// Token: 0x04000C18 RID: 3096
			public NativeArray<float2> desiredTargetPointInVelocitySpace;

			// Token: 0x04000C19 RID: 3097
			public NativeArray<float3> desiredVelocity;

			// Token: 0x04000C1A RID: 3098
			public NativeArray<float3> currentVelocity;

			// Token: 0x04000C1B RID: 3099
			public NativeArray<float2> collisionVelocityOffsets;

			// Token: 0x04000C1C RID: 3100
			public NativeArray<int> neighbours;
		}
	}
}
