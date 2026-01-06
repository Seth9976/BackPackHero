using System;
using Pathfinding.Drawing;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding.RVO
{
	// Token: 0x020002A7 RID: 679
	[AddComponentMenu("Pathfinding/Local Avoidance/RVO Controller")]
	[UniqueComponent(tag = "rvo")]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/rvocontroller.html")]
	public class RVOController : VersionedMonoBehaviour
	{
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x00065AA5 File Offset: 0x00063CA5
		// (set) Token: 0x06001015 RID: 4117 RVA: 0x00065AC1 File Offset: 0x00063CC1
		public float radius
		{
			get
			{
				if (this.ai != null)
				{
					return this.ai.radius;
				}
				return this.radiusBackingField;
			}
			set
			{
				if (this.ai != null)
				{
					this.ai.radius = value;
				}
				this.radiusBackingField = value;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x00065ADE File Offset: 0x00063CDE
		// (set) Token: 0x06001017 RID: 4119 RVA: 0x00065AFA File Offset: 0x00063CFA
		public float height
		{
			get
			{
				if (this.ai != null)
				{
					return this.ai.height;
				}
				return this.heightBackingField;
			}
			set
			{
				if (this.ai != null)
				{
					this.ai.height = value;
				}
				this.heightBackingField = value;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x00065B17 File Offset: 0x00063D17
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x00065B39 File Offset: 0x00063D39
		public float center
		{
			get
			{
				if (this.ai != null)
				{
					return this.ai.height / 2f;
				}
				return this.centerBackingField;
			}
			set
			{
				this.centerBackingField = value;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x00065B42 File Offset: 0x00063D42
		// (set) Token: 0x0600101B RID: 4123 RVA: 0x000033F6 File Offset: 0x000015F6
		[Obsolete("This field is obsolete in version 4.0 and will not affect anything. Use the LegacyRVOController if you need the old behaviour")]
		public LayerMask mask
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x00018013 File Offset: 0x00016213
		// (set) Token: 0x0600101D RID: 4125 RVA: 0x000033F6 File Offset: 0x000015F6
		[Obsolete("This field is obsolete in version 4.0 and will not affect anything. Use the LegacyRVOController if you need the old behaviour")]
		public bool enableRotation
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x000057A6 File Offset: 0x000039A6
		// (set) Token: 0x0600101F RID: 4127 RVA: 0x000033F6 File Offset: 0x000015F6
		[Obsolete("This field is obsolete in version 4.0 and will not affect anything. Use the LegacyRVOController if you need the old behaviour")]
		public float rotationSpeed
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x000057A6 File Offset: 0x000039A6
		// (set) Token: 0x06001021 RID: 4129 RVA: 0x000033F6 File Offset: 0x000015F6
		[Obsolete("This field is obsolete in version 4.0 and will not affect anything. Use the LegacyRVOController if you need the old behaviour")]
		public float maxSpeed
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x00065B4A File Offset: 0x00063D4A
		public MovementPlane movementPlaneMode
		{
			get
			{
				SimulatorBurst simulator = this.simulator;
				if (simulator != null)
				{
					return simulator.MovementPlane;
				}
				RVOSimulator active = RVOSimulator.active;
				if (active == null)
				{
					return MovementPlane.XZ;
				}
				return active.movementPlane;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x00065B6C File Offset: 0x00063D6C
		// (set) Token: 0x06001024 RID: 4132 RVA: 0x00065BE0 File Offset: 0x00063DE0
		public SimpleMovementPlane movementPlane
		{
			get
			{
				SimulatorBurst simulator = this.simulator;
				MovementPlane? movementPlane;
				if (simulator == null)
				{
					RVOSimulator active = RVOSimulator.active;
					movementPlane = ((active != null) ? new MovementPlane?(active.movementPlane) : null);
				}
				else
				{
					movementPlane = new MovementPlane?(simulator.MovementPlane);
				}
				MovementPlane? movementPlane2 = movementPlane;
				if (movementPlane2 != null)
				{
					if (movementPlane2.Value == MovementPlane.Arbitrary)
					{
						return this.movementPlaneBackingField;
					}
					if (movementPlane2.Value == MovementPlane.XY)
					{
						return SimpleMovementPlane.XYPlane;
					}
				}
				return SimpleMovementPlane.XZPlane;
			}
			set
			{
				SimulatorBurst simulator = this.simulator;
				MovementPlane? movementPlane;
				if (simulator == null)
				{
					RVOSimulator active = RVOSimulator.active;
					movementPlane = ((active != null) ? new MovementPlane?(active.movementPlane) : null);
				}
				else
				{
					movementPlane = new MovementPlane?(simulator.MovementPlane);
				}
				MovementPlane? movementPlane2 = movementPlane;
				if (movementPlane2 != null && movementPlane2.Value != MovementPlane.Arbitrary)
				{
					throw new InvalidOperationException("Cannot set the movement plane unless the RVOSimulator's movement plane setting is set to Arbitrary.");
				}
				this.movementPlaneBackingField = value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x00065C47 File Offset: 0x00063E47
		// (set) Token: 0x06001026 RID: 4134 RVA: 0x00065C4F File Offset: 0x00063E4F
		public IAgent rvoAgent { get; private set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x00065C58 File Offset: 0x00063E58
		// (set) Token: 0x06001028 RID: 4136 RVA: 0x00065C60 File Offset: 0x00063E60
		private SimulatorBurst simulator { get; set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x00065C69 File Offset: 0x00063E69
		// (set) Token: 0x0600102A RID: 4138 RVA: 0x00065C8B File Offset: 0x00063E8B
		protected IAstarAI ai
		{
			get
			{
				if (this.aiBackingField as MonoBehaviour == null)
				{
					this.aiBackingField = null;
				}
				return this.aiBackingField;
			}
			set
			{
				this.aiBackingField = value;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x00065C94 File Offset: 0x00063E94
		public Vector3 position
		{
			get
			{
				this.simulator.BlockUntilSimulationStepDone();
				return this.rvoAgent.Position;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x00065CAC File Offset: 0x00063EAC
		// (set) Token: 0x0600102D RID: 4141 RVA: 0x00065CDF File Offset: 0x00063EDF
		public Vector3 velocity
		{
			get
			{
				float num = ((Time.deltaTime > 0.0001f) ? Time.deltaTime : 0.02f);
				return this.CalculateMovementDelta(num) / num;
			}
			set
			{
				this.simulator.BlockUntilSimulationStepDone();
				this.rvoAgent.ForceSetVelocity(value);
			}
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00065CF8 File Offset: 0x00063EF8
		public Vector3 CalculateMovementDelta(float deltaTime)
		{
			return this.CalculateMovementDelta((this.ai != null) ? this.ai.position : this.tr.position, deltaTime);
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00065D24 File Offset: 0x00063F24
		public Vector3 CalculateMovementDelta(Vector3 position, float deltaTime)
		{
			if (this.rvoAgent == null)
			{
				return Vector3.zero;
			}
			Vector2 vector = this.movementPlane.ToPlane(this.rvoAgent.CalculatedTargetPoint - position);
			return this.movementPlane.ToWorld(Vector2.ClampMagnitude(vector, this.rvoAgent.CalculatedSpeed * deltaTime), 0f);
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x00065D85 File Offset: 0x00063F85
		public bool AvoidingAnyAgents
		{
			get
			{
				return this.rvoAgent != null && this.rvoAgent.AvoidingAnyAgents;
			}
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00065D9C File Offset: 0x00063F9C
		public void SetCollisionNormal(Vector3 normal)
		{
			this.simulator.BlockUntilSimulationStepDone();
			this.rvoAgent.SetCollisionNormal(normal);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00065DB5 File Offset: 0x00063FB5
		public void SetObstacleQuery(GraphNode sourceNode)
		{
			this.obstacleQuery = sourceNode;
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00065DBE File Offset: 0x00063FBE
		[Obsolete("Set the 'velocity' property instead")]
		public void ForceSetVelocity(Vector3 velocity)
		{
			this.velocity = velocity;
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00065DC8 File Offset: 0x00063FC8
		public Vector2 To2D(Vector3 p)
		{
			return this.movementPlane.ToPlane(p);
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x00065DE4 File Offset: 0x00063FE4
		public Vector2 To2D(Vector3 p, out float elevation)
		{
			return this.movementPlane.ToPlane(p, out elevation);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00065E04 File Offset: 0x00064004
		public Vector3 To3D(Vector2 p, float elevationCoordinate)
		{
			return this.movementPlane.ToWorld(p, elevationCoordinate);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00065E21 File Offset: 0x00064021
		private void OnDisable()
		{
			if (this.simulator == null)
			{
				return;
			}
			this.simulator.RemoveAgent(this.rvoAgent);
			this.simulator = null;
			this.rvoAgent = null;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00065E4C File Offset: 0x0006404C
		private void OnEnable()
		{
			this.tr = base.transform;
			this.ai = base.GetComponent<IAstarAI>();
			AIBase aibase = this.ai as AIBase;
			if (aibase != null)
			{
				aibase.FindComponents();
			}
			if (RVOSimulator.active == null)
			{
				Debug.LogError("No RVOSimulator component found in the scene. Please add one.");
				base.enabled = false;
				return;
			}
			this.simulator = RVOSimulator.active.GetSimulator();
			this.rvoAgent = this.simulator.AddAgent(Vector3.zero);
			this.rvoAgent.PreCalculationCallback = new Action(this.UpdateAgentProperties);
			this.rvoAgent.DestroyedCallback = new Action(this.OnAgentDestroyed);
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x00065EF9 File Offset: 0x000640F9
		private void OnAgentDestroyed()
		{
			if (base.gameObject.activeInHierarchy)
			{
				this.simulator = null;
				this.rvoAgent = null;
				base.enabled = false;
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00065F20 File Offset: 0x00064120
		protected void UpdateAgentProperties()
		{
			Vector3 localScale = this.tr.localScale;
			this.rvoAgent.Radius = Mathf.Max(0.001f, this.radius * Mathf.Abs(localScale.x));
			this.rvoAgent.AgentTimeHorizon = this.agentTimeHorizon;
			this.rvoAgent.ObstacleTimeHorizon = this.obstacleTimeHorizon;
			this.rvoAgent.Locked = this.locked;
			this.rvoAgent.MaxNeighbours = this.maxNeighbours;
			this.rvoAgent.DebugFlags = this.debug;
			this.rvoAgent.Layer = this.layer;
			this.rvoAgent.CollidesWith = this.collidesWith;
			SimpleMovementPlane movementPlane = this.movementPlane;
			this.rvoAgent.MovementPlane = movementPlane;
			float num;
			Vector2 vector = movementPlane.ToPlane((this.ai != null) ? this.ai.position : this.tr.position, out num);
			if (this.movementPlaneMode == MovementPlane.XY)
			{
				this.rvoAgent.Height = 1f;
				this.rvoAgent.Position = movementPlane.ToWorld(vector, 0f);
			}
			else
			{
				this.rvoAgent.Height = this.height * localScale.y;
				this.rvoAgent.Position = movementPlane.ToWorld(vector, num + (this.center - 0.5f * this.height) * localScale.y);
			}
			ReachedEndOfPath calculatedEffectivelyReachedDestination = this.rvoAgent.CalculatedEffectivelyReachedDestination;
			float num2 = this.priority * this.priorityMultiplier;
			float num3 = this.flowFollowingStrength;
			if (calculatedEffectivelyReachedDestination == ReachedEndOfPath.Reached)
			{
				num3 = 1f;
				num2 *= 0.3f;
			}
			else if (calculatedEffectivelyReachedDestination == ReachedEndOfPath.ReachedSoon)
			{
				num3 = 1f;
				num2 *= 0.45f;
			}
			this.rvoAgent.Priority = num2;
			this.rvoAgent.FlowFollowingStrength = num3;
			this.rvoAgent.SetObstacleQuery(this.obstacleQuery);
			this.obstacleQuery = null;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00066112 File Offset: 0x00064312
		public void SetTarget(Vector3 pos, float speed, float maxSpeed, Vector3 endOfPath)
		{
			if (this.rvoAgent == null)
			{
				return;
			}
			this.simulator.BlockUntilSimulationStepDone();
			this.rvoAgent.SetTarget(pos, speed, maxSpeed, endOfPath);
			if (this.lockWhenNotMoving)
			{
				this.locked = speed < 0.001f;
			}
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00066150 File Offset: 0x00064350
		public void Move(Vector3 velocity)
		{
			if (this.rvoAgent == null)
			{
				return;
			}
			this.simulator.BlockUntilSimulationStepDone();
			float magnitude = this.movementPlane.ToPlane(velocity).magnitude;
			this.rvoAgent.SetTarget(((this.ai != null) ? this.ai.position : this.tr.position) + velocity, magnitude, magnitude, new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity));
			if (this.lockWhenNotMoving)
			{
				this.locked = magnitude < 0.001f;
			}
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000661E6 File Offset: 0x000643E6
		[Obsolete("Use transform.position instead, the RVOController can now handle that without any issues.")]
		public void Teleport(Vector3 pos)
		{
			this.tr.position = pos;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x000661F4 File Offset: 0x000643F4
		public override void DrawGizmos()
		{
			this.tr = base.transform;
			if (this.ai == null)
			{
				Color color = AIBase.ShapeGizmoColor * (this.locked ? 0.5f : 1f);
				Vector3 position = base.transform.position;
				Vector3 localScale = this.tr.localScale;
				if (this.movementPlaneMode == MovementPlane.XY)
				{
					Draw.WireCylinder(position, Vector3.forward, 0f, this.radius * localScale.x, color);
					return;
				}
				Draw.WireCylinder(position + this.To3D(Vector2.zero, this.center - this.height * 0.5f) * localScale.y, this.To3D(Vector2.zero, 1f), this.height * localScale.y, this.radius * localScale.x, color);
			}
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x000662EC File Offset: 0x000644EC
		protected override void OnUpgradeSerializedData(ref Migrations migrations, bool unityThread)
		{
			int num;
			if (migrations.TryMigrateFromLegacyFormat(out num) && num > 1)
			{
				migrations.MarkMigrationFinished(0);
			}
			if (migrations.AddAndMaybeRunMigration(0, unityThread))
			{
				if (base.transform.localScale.y != 0f)
				{
					this.centerBackingField /= Mathf.Abs(base.transform.localScale.y);
				}
				if (base.transform.localScale.y != 0f)
				{
					this.heightBackingField /= Mathf.Abs(base.transform.localScale.y);
				}
				if (base.transform.localScale.x != 0f)
				{
					this.radiusBackingField /= Mathf.Abs(base.transform.localScale.x);
				}
			}
		}

		// Token: 0x04000C4E RID: 3150
		[SerializeField]
		[FormerlySerializedAs("radius")]
		internal float radiusBackingField = 0.5f;

		// Token: 0x04000C4F RID: 3151
		[SerializeField]
		[FormerlySerializedAs("height")]
		private float heightBackingField = 2f;

		// Token: 0x04000C50 RID: 3152
		[SerializeField]
		[FormerlySerializedAs("center")]
		private float centerBackingField = 1f;

		// Token: 0x04000C51 RID: 3153
		[Tooltip("A locked unit cannot move. Other units will still avoid it. But avoidance quality is not the best")]
		public bool locked;

		// Token: 0x04000C52 RID: 3154
		[Tooltip("Automatically set #locked to true when desired velocity is approximately zero")]
		public bool lockWhenNotMoving;

		// Token: 0x04000C53 RID: 3155
		[Tooltip("How far into the future to look for collisions with other agents (in seconds)")]
		public float agentTimeHorizon = 2f;

		// Token: 0x04000C54 RID: 3156
		[Tooltip("How far into the future to look for collisions with obstacles (in seconds)")]
		public float obstacleTimeHorizon = 0.5f;

		// Token: 0x04000C55 RID: 3157
		[Tooltip("Max number of other agents to take into account.\nA smaller value can reduce CPU load, a higher value can lead to better local avoidance quality.")]
		public int maxNeighbours = 10;

		// Token: 0x04000C56 RID: 3158
		public RVOLayer layer = RVOLayer.DefaultAgent;

		// Token: 0x04000C57 RID: 3159
		[EnumFlag]
		public RVOLayer collidesWith = (RVOLayer)(-1);

		// Token: 0x04000C58 RID: 3160
		[HideInInspector]
		[Obsolete]
		public float wallAvoidForce = 1f;

		// Token: 0x04000C59 RID: 3161
		[HideInInspector]
		[Obsolete]
		public float wallAvoidFalloff = 1f;

		// Token: 0x04000C5A RID: 3162
		[Tooltip("How strongly other agents will avoid this agent")]
		[Range(0f, 1f)]
		public float priority = 0.5f;

		// Token: 0x04000C5B RID: 3163
		[NonSerialized]
		public float priorityMultiplier = 1f;

		// Token: 0x04000C5C RID: 3164
		[NonSerialized]
		public float flowFollowingStrength;

		// Token: 0x04000C5D RID: 3165
		private GraphNode obstacleQuery;

		// Token: 0x04000C60 RID: 3168
		protected Transform tr;

		// Token: 0x04000C61 RID: 3169
		[SerializeField]
		[FormerlySerializedAs("ai")]
		private IAstarAI aiBackingField;

		// Token: 0x04000C62 RID: 3170
		internal SimpleMovementPlane movementPlaneBackingField = GraphTransform.xzPlane.ToSimpleMovementPlane();

		// Token: 0x04000C63 RID: 3171
		public AgentDebugFlags debug;

		// Token: 0x020002A8 RID: 680
		[Flags]
		private enum RVOControllerMigrations
		{
			// Token: 0x04000C65 RID: 3173
			MigrateScale = 0
		}
	}
}
