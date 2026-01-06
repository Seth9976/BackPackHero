using System;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding.RVO
{
	// Token: 0x020000D6 RID: 214
	[AddComponentMenu("Pathfinding/Local Avoidance/RVO Controller")]
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_r_v_o_1_1_r_v_o_controller.php")]
	public class RVOController : VersionedMonoBehaviour
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0003C5A1 File Offset: 0x0003A7A1
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x0003C5BD File Offset: 0x0003A7BD
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

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0003C5DA File Offset: 0x0003A7DA
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x0003C5F6 File Offset: 0x0003A7F6
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

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0003C613 File Offset: 0x0003A813
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x0003C635 File Offset: 0x0003A835
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

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0003C63E File Offset: 0x0003A83E
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x0003C646 File Offset: 0x0003A846
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

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0003C648 File Offset: 0x0003A848
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x0003C64B File Offset: 0x0003A84B
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

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0003C64D File Offset: 0x0003A84D
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x0003C654 File Offset: 0x0003A854
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

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x0003C656 File Offset: 0x0003A856
		// (set) Token: 0x06000920 RID: 2336 RVA: 0x0003C65D File Offset: 0x0003A85D
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

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0003C65F File Offset: 0x0003A85F
		public MovementPlane movementPlane
		{
			get
			{
				if (this.simulator != null)
				{
					return this.simulator.movementPlane;
				}
				if (RVOSimulator.active)
				{
					return RVOSimulator.active.movementPlane;
				}
				return MovementPlane.XZ;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0003C68D File Offset: 0x0003A88D
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x0003C695 File Offset: 0x0003A895
		public IAgent rvoAgent { get; private set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0003C69E File Offset: 0x0003A89E
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x0003C6A6 File Offset: 0x0003A8A6
		public Simulator simulator { get; private set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0003C6AF File Offset: 0x0003A8AF
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x0003C6D1 File Offset: 0x0003A8D1
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

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0003C6DA File Offset: 0x0003A8DA
		public Vector3 position
		{
			get
			{
				return this.To3D(this.rvoAgent.Position, this.rvoAgent.ElevationCoordinate);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0003C6F8 File Offset: 0x0003A8F8
		// (set) Token: 0x0600092A RID: 2346 RVA: 0x0003C72B File Offset: 0x0003A92B
		public Vector3 velocity
		{
			get
			{
				float num = ((Time.deltaTime > 0.0001f) ? Time.deltaTime : 0.02f);
				return this.CalculateMovementDelta(num) / num;
			}
			set
			{
				this.rvoAgent.ForceSetVelocity(this.To2D(value));
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0003C740 File Offset: 0x0003A940
		public Vector3 CalculateMovementDelta(float deltaTime)
		{
			if (this.rvoAgent == null)
			{
				return Vector3.zero;
			}
			return this.To3D(Vector2.ClampMagnitude(this.rvoAgent.CalculatedTargetPoint - this.To2D((this.ai != null) ? this.ai.position : this.tr.position), this.rvoAgent.CalculatedSpeed * deltaTime), 0f);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0003C7AE File Offset: 0x0003A9AE
		public Vector3 CalculateMovementDelta(Vector3 position, float deltaTime)
		{
			return this.To3D(Vector2.ClampMagnitude(this.rvoAgent.CalculatedTargetPoint - this.To2D(position), this.rvoAgent.CalculatedSpeed * deltaTime), 0f);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0003C7E4 File Offset: 0x0003A9E4
		public void SetCollisionNormal(Vector3 normal)
		{
			this.rvoAgent.SetCollisionNormal(this.To2D(normal));
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0003C7F8 File Offset: 0x0003A9F8
		[Obsolete("Set the 'velocity' property instead")]
		public void ForceSetVelocity(Vector3 velocity)
		{
			this.velocity = velocity;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0003C804 File Offset: 0x0003AA04
		public Vector2 To2D(Vector3 p)
		{
			float num;
			return this.To2D(p, out num);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0003C81A File Offset: 0x0003AA1A
		public Vector2 To2D(Vector3 p, out float elevation)
		{
			if (this.movementPlane == MovementPlane.XY)
			{
				elevation = -p.z;
				return new Vector2(p.x, p.y);
			}
			elevation = p.y;
			return new Vector2(p.x, p.z);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0003C859 File Offset: 0x0003AA59
		public Vector3 To3D(Vector2 p, float elevationCoordinate)
		{
			if (this.movementPlane == MovementPlane.XY)
			{
				return new Vector3(p.x, p.y, -elevationCoordinate);
			}
			return new Vector3(p.x, elevationCoordinate, p.y);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0003C88A File Offset: 0x0003AA8A
		private void OnDisable()
		{
			if (this.simulator == null)
			{
				return;
			}
			this.simulator.RemoveAgent(this.rvoAgent);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0003C8A8 File Offset: 0x0003AAA8
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
			if (this.rvoAgent != null)
			{
				this.simulator.AddAgent(this.rvoAgent);
				return;
			}
			this.rvoAgent = this.simulator.AddAgent(Vector2.zero, 0f);
			this.rvoAgent.PreCalculationCallback = new Action(this.UpdateAgentProperties);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0003C964 File Offset: 0x0003AB64
		protected void UpdateAgentProperties()
		{
			Vector3 localScale = this.tr.localScale;
			this.rvoAgent.Radius = Mathf.Max(0.001f, this.radius * localScale.x);
			this.rvoAgent.AgentTimeHorizon = this.agentTimeHorizon;
			this.rvoAgent.ObstacleTimeHorizon = this.obstacleTimeHorizon;
			this.rvoAgent.Locked = this.locked;
			this.rvoAgent.MaxNeighbours = this.maxNeighbours;
			this.rvoAgent.DebugDraw = this.debug;
			this.rvoAgent.Layer = this.layer;
			this.rvoAgent.CollidesWith = this.collidesWith;
			this.rvoAgent.Priority = this.priority;
			float num;
			this.rvoAgent.Position = this.To2D((this.ai != null) ? this.ai.position : this.tr.position, out num);
			if (this.movementPlane == MovementPlane.XZ)
			{
				this.rvoAgent.Height = this.height * localScale.y;
				this.rvoAgent.ElevationCoordinate = num + (this.center - 0.5f * this.height) * localScale.y;
				return;
			}
			this.rvoAgent.Height = 1f;
			this.rvoAgent.ElevationCoordinate = 0f;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0003CAC2 File Offset: 0x0003ACC2
		public void SetTarget(Vector3 pos, float speed, float maxSpeed)
		{
			if (this.simulator == null)
			{
				return;
			}
			this.rvoAgent.SetTarget(this.To2D(pos), speed, maxSpeed);
			if (this.lockWhenNotMoving)
			{
				this.locked = speed < 0.001f;
			}
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0003CAF8 File Offset: 0x0003ACF8
		public void Move(Vector3 vel)
		{
			if (this.simulator == null)
			{
				return;
			}
			Vector2 vector = this.To2D(vel);
			float magnitude = vector.magnitude;
			this.rvoAgent.SetTarget(this.To2D((this.ai != null) ? this.ai.position : this.tr.position) + vector, magnitude, magnitude);
			if (this.lockWhenNotMoving)
			{
				this.locked = magnitude < 0.001f;
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0003CB6D File Offset: 0x0003AD6D
		[Obsolete("Use transform.position instead, the RVOController can now handle that without any issues.")]
		public void Teleport(Vector3 pos)
		{
			this.tr.position = pos;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0003CB7C File Offset: 0x0003AD7C
		private void OnDrawGizmos()
		{
			this.tr = base.transform;
			if (this.ai == null)
			{
				Color color = AIBase.ShapeGizmoColor * (this.locked ? 0.5f : 1f);
				Vector3 position = base.transform.position;
				Vector3 localScale = this.tr.localScale;
				if (this.movementPlane == MovementPlane.XY)
				{
					Draw.Gizmos.Cylinder(position, Vector3.forward, 0f, this.radius * localScale.x, color);
					return;
				}
				Draw.Gizmos.Cylinder(position + this.To3D(Vector2.zero, this.center - this.height * 0.5f) * localScale.y, this.To3D(Vector2.zero, 1f), this.height * localScale.y, this.radius * localScale.x, color);
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0003CC68 File Offset: 0x0003AE68
		protected override int OnUpgradeSerializedData(int version, bool unityThread)
		{
			if (version <= 1)
			{
				if (!unityThread)
				{
					return -1;
				}
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
			return 2;
		}

		// Token: 0x04000558 RID: 1368
		[SerializeField]
		[FormerlySerializedAs("radius")]
		internal float radiusBackingField = 0.5f;

		// Token: 0x04000559 RID: 1369
		[SerializeField]
		[FormerlySerializedAs("height")]
		private float heightBackingField = 2f;

		// Token: 0x0400055A RID: 1370
		[SerializeField]
		[FormerlySerializedAs("center")]
		private float centerBackingField = 1f;

		// Token: 0x0400055B RID: 1371
		[Tooltip("A locked unit cannot move. Other units will still avoid it. But avoidance quality is not the best")]
		public bool locked;

		// Token: 0x0400055C RID: 1372
		[Tooltip("Automatically set #locked to true when desired velocity is approximately zero")]
		public bool lockWhenNotMoving;

		// Token: 0x0400055D RID: 1373
		[Tooltip("How far into the future to look for collisions with other agents (in seconds)")]
		public float agentTimeHorizon = 2f;

		// Token: 0x0400055E RID: 1374
		[Tooltip("How far into the future to look for collisions with obstacles (in seconds)")]
		public float obstacleTimeHorizon = 2f;

		// Token: 0x0400055F RID: 1375
		[Tooltip("Max number of other agents to take into account.\nA smaller value can reduce CPU load, a higher value can lead to better local avoidance quality.")]
		public int maxNeighbours = 10;

		// Token: 0x04000560 RID: 1376
		public RVOLayer layer = RVOLayer.DefaultAgent;

		// Token: 0x04000561 RID: 1377
		[EnumFlag]
		public RVOLayer collidesWith = (RVOLayer)(-1);

		// Token: 0x04000562 RID: 1378
		[HideInInspector]
		[Obsolete]
		public float wallAvoidForce = 1f;

		// Token: 0x04000563 RID: 1379
		[HideInInspector]
		[Obsolete]
		public float wallAvoidFalloff = 1f;

		// Token: 0x04000564 RID: 1380
		[Tooltip("How strongly other agents will avoid this agent")]
		[Range(0f, 1f)]
		public float priority = 0.5f;

		// Token: 0x04000567 RID: 1383
		protected Transform tr;

		// Token: 0x04000568 RID: 1384
		[SerializeField]
		[FormerlySerializedAs("ai")]
		private IAstarAI aiBackingField;

		// Token: 0x04000569 RID: 1385
		public bool debug;
	}
}
