using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x02000014 RID: 20
	[AddComponentMenu("Pathfinding/AI/AIPath (2D,3D)")]
	[UniqueComponent(tag = "ai")]
	public class AIPath : AIBase, IAstarAI
	{
		// Token: 0x06000105 RID: 261 RVA: 0x0000626F File Offset: 0x0000446F
		public override void Teleport(Vector3 newPosition, bool clearPath = true)
		{
			this.reachedEndOfPath = false;
			base.Teleport(newPosition, clearPath);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00006280 File Offset: 0x00004480
		public float remainingDistance
		{
			get
			{
				if (!this.interpolator.valid)
				{
					return float.PositiveInfinity;
				}
				return this.interpolator.remainingDistance + this.movementPlane.ToPlane(this.interpolator.position - base.position).magnitude;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000062D8 File Offset: 0x000044D8
		public override bool reachedDestination
		{
			get
			{
				if (!this.reachedEndOfPath)
				{
					return false;
				}
				if (!this.interpolator.valid || this.remainingDistance + this.movementPlane.ToPlane(base.destination - this.interpolator.endPoint).magnitude > this.endReachedDistance)
				{
					return false;
				}
				if (this.orientation != OrientationMode.YAxisForward)
				{
					float num;
					this.movementPlane.ToPlane(base.destination - base.position, out num);
					float num2 = this.tr.localScale.y * this.height;
					if (num > num2 || (double)num < (double)(-(double)num2) * 0.5)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000638E File Offset: 0x0000458E
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00006396 File Offset: 0x00004596
		public bool reachedEndOfPath { get; protected set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000639F File Offset: 0x0000459F
		public bool hasPath
		{
			get
			{
				return this.interpolator.valid;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000063AC File Offset: 0x000045AC
		public bool pathPending
		{
			get
			{
				return this.waitingForPathCalculation;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000063B4 File Offset: 0x000045B4
		public Vector3 steeringTarget
		{
			get
			{
				if (!this.interpolator.valid)
				{
					return base.position;
				}
				return this.interpolator.position;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000063D5 File Offset: 0x000045D5
		public override Vector3 endOfPath
		{
			get
			{
				if (this.interpolator.valid)
				{
					return this.interpolator.endPoint;
				}
				if (float.IsFinite(base.destination.x))
				{
					return base.destination;
				}
				return base.position;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000640F File Offset: 0x0000460F
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00006417 File Offset: 0x00004617
		float IAstarAI.radius
		{
			get
			{
				return this.radius;
			}
			set
			{
				this.radius = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00006420 File Offset: 0x00004620
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00006428 File Offset: 0x00004628
		float IAstarAI.height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00006431 File Offset: 0x00004631
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00006439 File Offset: 0x00004639
		float IAstarAI.maxSpeed
		{
			get
			{
				return this.maxSpeed;
			}
			set
			{
				this.maxSpeed = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00006442 File Offset: 0x00004642
		// (set) Token: 0x06000115 RID: 277 RVA: 0x0000644A File Offset: 0x0000464A
		bool IAstarAI.canSearch
		{
			get
			{
				return base.canSearch;
			}
			set
			{
				base.canSearch = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00006453 File Offset: 0x00004653
		// (set) Token: 0x06000117 RID: 279 RVA: 0x0000645B File Offset: 0x0000465B
		bool IAstarAI.canMove
		{
			get
			{
				return this.canMove;
			}
			set
			{
				this.canMove = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00006464 File Offset: 0x00004664
		NativeMovementPlane IAstarAI.movementPlane
		{
			get
			{
				return new NativeMovementPlane(this.movementPlane);
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006471 File Offset: 0x00004671
		public void GetRemainingPath(List<Vector3> buffer, out bool stale)
		{
			buffer.Clear();
			buffer.Add(base.position);
			if (!this.interpolator.valid)
			{
				stale = true;
				return;
			}
			stale = false;
			this.interpolator.GetRemainingPath(buffer);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000064A8 File Offset: 0x000046A8
		public void GetRemainingPath(List<Vector3> buffer, List<PathPartWithLinkInfo> partsBuffer, out bool stale)
		{
			this.GetRemainingPath(buffer, out stale);
			if (partsBuffer != null)
			{
				partsBuffer.Clear();
				partsBuffer.Add(new PathPartWithLinkInfo
				{
					startIndex = 0,
					endIndex = buffer.Count - 1
				});
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000064EC File Offset: 0x000046EC
		protected override void OnDisable()
		{
			base.OnDisable();
			this.rotationFilterState = Vector2.zero;
			this.rotationFilterState2 = Vector2.zero;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000033F6 File Offset: 0x000015F6
		public virtual void OnTargetReached()
		{
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000650C File Offset: 0x0000470C
		protected virtual void UpdateMovementPlane()
		{
			if (this.path.path == null || this.path.path.Count == 0)
			{
				return;
			}
			ITransformedGraph transformedGraph = AstarData.GetGraph(this.path.path[0]) as ITransformedGraph;
			IMovementPlane movementPlane = ((transformedGraph != null) ? transformedGraph.transform : ((this.orientation == OrientationMode.YAxisForward) ? new GraphTransform(Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(-90f, 270f, 90f), Vector3.one)) : GraphTransform.identityTransform));
			this.movementPlane = movementPlane.ToSimpleMovementPlane();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000065A8 File Offset: 0x000047A8
		protected override void OnPathComplete(Path newPath)
		{
			ABPath abpath = newPath as ABPath;
			if (abpath == null)
			{
				throw new Exception("This function only handles ABPaths, do not use special path types");
			}
			this.waitingForPathCalculation = false;
			abpath.Claim(this);
			if (abpath.error)
			{
				abpath.Release(this, false);
				base.SetPath(null, true);
				return;
			}
			if (this.path != null)
			{
				this.path.Release(this, false);
			}
			this.path = abpath;
			if (!abpath.endPointKnownBeforeCalculation)
			{
				base.destination = abpath.originalEndPoint;
			}
			if (this.path.vectorPath.Count == 1)
			{
				this.path.vectorPath.Add(this.path.vectorPath[0]);
			}
			this.interpolatorPath.SetPath(this.path.vectorPath);
			this.interpolator = this.interpolatorPath.start;
			this.UpdateMovementPlane();
			this.reachedEndOfPath = false;
			this.interpolator.MoveToLocallyClosestPoint((this.GetFeetPosition() + abpath.originalStartPoint) * 0.5f, true, true);
			this.interpolator.MoveToLocallyClosestPoint(this.GetFeetPosition(), true, true);
			this.interpolator.MoveToCircleIntersection2D<SimpleMovementPlane>(base.position, this.pickNextWaypointDist, this.movementPlane);
			if (this.remainingDistance <= this.endReachedDistance)
			{
				this.reachedEndOfPath = true;
				this.OnTargetReached();
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000066FC File Offset: 0x000048FC
		protected override void ClearPath()
		{
			base.CancelCurrentPathRequest();
			if (this.path != null)
			{
				this.path.Release(this, false);
			}
			this.path = null;
			this.interpolatorPath.SetPath(null);
			this.reachedEndOfPath = false;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006734 File Offset: 0x00004934
		protected override void MovementUpdateInternal(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			float num = this.maxAcceleration;
			if (num < 0f)
			{
				num *= -this.maxSpeed;
			}
			if (this.updatePosition)
			{
				this.simulatedPosition = this.tr.position;
			}
			if (this.updateRotation)
			{
				this.simulatedRotation = this.tr.rotation;
			}
			Vector3 simulatedPosition = this.simulatedPosition;
			Vector2 vector = this.movementPlane.ToPlane(this.simulatedRotation * ((this.orientation == OrientationMode.YAxisForward) ? Vector3.up : Vector3.forward));
			bool flag = base.isStopped || (this.reachedDestination && this.whenCloseToDestination == CloseToDestinationMode.Stop);
			if (this.rvoController != null)
			{
				this.rvoDensityBehavior.Update(this.rvoController.enabled, this.reachedDestination, ref flag, ref this.rvoController.priorityMultiplier, ref this.rvoController.flowFollowingStrength, simulatedPosition);
			}
			float num2 = 0f;
			float num3;
			if (this.interpolator.valid)
			{
				this.interpolator.MoveToCircleIntersection2D<SimpleMovementPlane>(simulatedPosition, this.pickNextWaypointDist, this.movementPlane);
				Vector2 vector2 = this.movementPlane.ToPlane(this.steeringTarget - simulatedPosition);
				num3 = vector2.magnitude + Mathf.Max(0f, this.interpolator.remainingDistance);
				bool reachedEndOfPath = this.reachedEndOfPath;
				this.reachedEndOfPath = num3 <= this.endReachedDistance;
				if (!reachedEndOfPath && this.reachedEndOfPath)
				{
					this.OnTargetReached();
				}
				if (!flag)
				{
					num2 = ((num3 < this.slowdownDistance) ? Mathf.Sqrt(num3 / this.slowdownDistance) : 1f);
					this.velocity2D += MovementUtilities.CalculateAccelerationToReachPoint(vector2, vector2.normalized * this.maxSpeed, this.velocity2D, num, this.rotationSpeed, this.maxSpeed, vector) * deltaTime;
				}
			}
			else
			{
				this.reachedEndOfPath = false;
				num3 = float.PositiveInfinity;
			}
			if (!this.interpolator.valid || flag)
			{
				this.velocity2D -= Vector2.ClampMagnitude(this.velocity2D, num * deltaTime);
				num2 = 1f;
			}
			this.velocity2D = MovementUtilities.ClampVelocity(this.velocity2D, this.maxSpeed, num2, this.slowWhenNotFacingTarget && this.enableRotation, this.preventMovingBackwards, vector);
			this.ApplyGravity(deltaTime);
			bool flag2 = false;
			if (this.rvoController != null && this.rvoController.enabled)
			{
				Vector3 vector3 = simulatedPosition + this.movementPlane.ToWorld(Vector2.ClampMagnitude(this.velocity2D, num3), 0f);
				this.rvoController.SetTarget(vector3, this.velocity2D.magnitude, this.maxSpeed, this.endOfPath);
				flag2 = this.rvoController.AvoidingAnyAgents;
			}
			Vector2 vector4 = (this.lastDeltaPosition = base.CalculateDeltaToMoveThisFrame(simulatedPosition, num3, deltaTime));
			nextPosition = simulatedPosition + this.movementPlane.ToWorld(vector4, this.verticalVelocity * deltaTime);
			this.CalculateNextRotation(num2, flag2, out nextRotation);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006A54 File Offset: 0x00004C54
		protected virtual void CalculateNextRotation(float slowdown, bool avoidingOtherAgents, out Quaternion nextRotation)
		{
			if (this.lastDeltaTime > 1E-05f && this.enableRotation)
			{
				float num = this.radius * this.tr.localScale.x * 0.2f;
				float num2 = MovementUtilities.FilterRotationDirection(ref this.rotationFilterState, ref this.rotationFilterState2, this.lastDeltaPosition, num, this.lastDeltaTime, avoidingOtherAgents);
				nextRotation = base.SimulateRotationTowards(this.rotationFilterState, this.rotationSpeed * this.lastDeltaTime * num2, this.rotationSpeed * this.lastDeltaTime);
				return;
			}
			nextRotation = this.rotation;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006AF0 File Offset: 0x00004CF0
		protected override Vector3 ClampToNavmesh(Vector3 position, out bool positionChanged)
		{
			if (this.constrainInsideGraph)
			{
				AIPath.cachedNNConstraint.tags = this.seeker.traversableTags;
				AIPath.cachedNNConstraint.graphMask = this.seeker.graphMask;
				AIPath.cachedNNConstraint.distanceMetric = DistanceMetric.ClosestAsSeenFromAboveSoft();
				NNInfo nearest = AstarPath.active.GetNearest(position, AIPath.cachedNNConstraint);
				if (nearest.node == null)
				{
					positionChanged = false;
					return position;
				}
				Vector3 position2 = nearest.position;
				if (this.rvoController != null && this.rvoController.enabled)
				{
					this.rvoController.SetObstacleQuery(nearest.node);
				}
				Vector2 vector = this.movementPlane.ToPlane(position2 - position);
				float sqrMagnitude = vector.sqrMagnitude;
				if (sqrMagnitude > 1.0000001E-06f)
				{
					this.velocity2D -= vector * Vector2.Dot(vector, this.velocity2D) / sqrMagnitude;
					positionChanged = true;
					return position + this.movementPlane.ToWorld(vector, 0f);
				}
			}
			positionChanged = false;
			return position;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006BFD File Offset: 0x00004DFD
		protected override void OnUpgradeSerializedData(ref Migrations migrations, bool unityThread)
		{
			if (migrations.IsLegacyFormat && migrations.LegacyVersion < 1)
			{
				this.rotationSpeed *= 90f;
			}
			base.OnUpgradeSerializedData(ref migrations, unityThread);
		}

		// Token: 0x040000C1 RID: 193
		public float maxAcceleration = -2.5f;

		// Token: 0x040000C2 RID: 194
		[FormerlySerializedAs("turningSpeed")]
		public float rotationSpeed = 360f;

		// Token: 0x040000C3 RID: 195
		public float slowdownDistance = 0.6f;

		// Token: 0x040000C4 RID: 196
		public float pickNextWaypointDist = 2f;

		// Token: 0x040000C5 RID: 197
		public bool alwaysDrawGizmos;

		// Token: 0x040000C6 RID: 198
		public bool slowWhenNotFacingTarget = true;

		// Token: 0x040000C7 RID: 199
		public bool preventMovingBackwards;

		// Token: 0x040000C8 RID: 200
		public bool constrainInsideGraph;

		// Token: 0x040000C9 RID: 201
		protected Path path;

		// Token: 0x040000CA RID: 202
		protected PathInterpolator.Cursor interpolator;

		// Token: 0x040000CB RID: 203
		protected PathInterpolator interpolatorPath = new PathInterpolator();

		// Token: 0x040000CD RID: 205
		private Vector2 rotationFilterState;

		// Token: 0x040000CE RID: 206
		private Vector2 rotationFilterState2;

		// Token: 0x040000CF RID: 207
		private static NNConstraint cachedNNConstraint = NNConstraint.Walkable;
	}
}
