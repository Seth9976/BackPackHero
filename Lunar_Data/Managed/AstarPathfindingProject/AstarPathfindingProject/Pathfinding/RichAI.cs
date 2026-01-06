using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Drawing;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000017 RID: 23
	[AddComponentMenu("Pathfinding/AI/RichAI (3D, for navmesh)")]
	[UniqueComponent(tag = "ai")]
	public class RichAI : AIBase, IAstarAI
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00006C9D File Offset: 0x00004E9D
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00006CA5 File Offset: 0x00004EA5
		public bool traversingOffMeshLink { get; protected set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00006CAE File Offset: 0x00004EAE
		public float remainingDistance
		{
			get
			{
				return this.distanceToSteeringTarget + Vector3.Distance(this.steeringTarget, this.richPath.Endpoint);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00006CCD File Offset: 0x00004ECD
		public bool reachedEndOfPath
		{
			get
			{
				return this.approachingPathEndpoint && this.distanceToSteeringTarget < this.endReachedDistance;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00006CE8 File Offset: 0x00004EE8
		public override bool reachedDestination
		{
			get
			{
				if (!this.reachedEndOfPath)
				{
					return false;
				}
				if (this.distanceToSteeringTarget + this.movementPlane.ToPlane(this.steeringTarget - this.richPath.Endpoint).magnitude + this.movementPlane.ToPlane(base.destination - this.richPath.Endpoint).magnitude > this.endReachedDistance)
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

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00006DBB File Offset: 0x00004FBB
		public bool hasPath
		{
			get
			{
				return this.richPath.GetCurrentPart() != null;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00006DCB File Offset: 0x00004FCB
		public bool pathPending
		{
			get
			{
				return this.waitingForPathCalculation || this.delayUpdatePath;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00006DDD File Offset: 0x00004FDD
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00006DE5 File Offset: 0x00004FE5
		public Vector3 steeringTarget { get; protected set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000640F File Offset: 0x0000460F
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00006417 File Offset: 0x00004617
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

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00006420 File Offset: 0x00004620
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00006428 File Offset: 0x00004628
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

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00006431 File Offset: 0x00004631
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00006439 File Offset: 0x00004639
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00006442 File Offset: 0x00004642
		// (set) Token: 0x06000160 RID: 352 RVA: 0x0000644A File Offset: 0x0000464A
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

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00006453 File Offset: 0x00004653
		// (set) Token: 0x06000162 RID: 354 RVA: 0x0000645B File Offset: 0x0000465B
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

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00006464 File Offset: 0x00004664
		NativeMovementPlane IAstarAI.movementPlane
		{
			get
			{
				return new NativeMovementPlane(this.movementPlane);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00006DEE File Offset: 0x00004FEE
		public bool approachingPartEndpoint
		{
			get
			{
				return this.lastCorner && this.nextCorners.Count == 1;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00006E08 File Offset: 0x00005008
		public bool approachingPathEndpoint
		{
			get
			{
				return this.approachingPartEndpoint && this.richPath.IsLastPart;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00006E1F File Offset: 0x0000501F
		public override Vector3 endOfPath
		{
			get
			{
				if (this.hasPath)
				{
					return this.richPath.Endpoint;
				}
				if (float.IsFinite(base.destination.x))
				{
					return base.destination;
				}
				return base.position;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00006E54 File Offset: 0x00005054
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00006E5C File Offset: 0x0000505C
		public override Quaternion rotation
		{
			get
			{
				return base.rotation;
			}
			set
			{
				base.rotation = value;
				this.rotationFilterState = Vector2.zero;
				this.rotationFilterState2 = Vector2.zero;
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006E7B File Offset: 0x0000507B
		public override void Teleport(Vector3 newPosition, bool clearPath = true)
		{
			base.Teleport(this.ClampPositionToGraph(newPosition), clearPath);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006E8C File Offset: 0x0000508C
		protected virtual Vector3 ClampPositionToGraph(Vector3 newPosition)
		{
			NNInfo nninfo = ((AstarPath.active != null) ? AstarPath.active.GetNearest(newPosition) : default(NNInfo));
			float num;
			this.movementPlane.ToPlane(newPosition, out num);
			return this.movementPlane.ToWorld(this.movementPlane.ToPlane((nninfo.node != null) ? nninfo.position : newPosition), num);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006EF4 File Offset: 0x000050F4
		protected override void OnDisable()
		{
			base.OnDisable();
			this.traversingOffMeshLink = false;
			base.StopAllCoroutines();
			this.rotationFilterState = Vector2.zero;
			this.rotationFilterState2 = Vector2.zero;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00006F1F File Offset: 0x0000511F
		protected override bool shouldRecalculatePath
		{
			get
			{
				return base.shouldRecalculatePath && !this.traversingOffMeshLink;
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006F34 File Offset: 0x00005134
		public override void SearchPath()
		{
			if (this.traversingOffMeshLink)
			{
				this.delayUpdatePath = true;
				return;
			}
			base.SearchPath();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006F4C File Offset: 0x0000514C
		protected override void OnPathComplete(Path p)
		{
			this.waitingForPathCalculation = false;
			p.Claim(this);
			if (p.error)
			{
				p.Release(this, false);
				return;
			}
			if (this.traversingOffMeshLink)
			{
				this.delayUpdatePath = true;
			}
			else
			{
				ABPath abpath = p as ABPath;
				if (abpath != null && !abpath.endPointKnownBeforeCalculation)
				{
					base.destination = abpath.originalEndPoint;
				}
				this.richPath.Initialize(this.seeker, p, true, this.funnelSimplification);
				RichFunnel richFunnel = this.richPath.GetCurrentPart() as RichFunnel;
				if (richFunnel != null)
				{
					if (this.updatePosition)
					{
						this.simulatedPosition = this.tr.position;
					}
					Vector2 vector = this.movementPlane.ToPlane(this.UpdateTarget(richFunnel));
					this.steeringTarget = this.nextCorners[0];
					Vector2 vector2 = this.movementPlane.ToPlane(this.steeringTarget);
					this.distanceToSteeringTarget = (vector2 - vector).magnitude;
					if (this.lastCorner && this.nextCorners.Count == 1 && this.distanceToSteeringTarget <= this.endReachedDistance)
					{
						this.NextPart();
					}
				}
			}
			p.Release(this, false);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00007072 File Offset: 0x00005272
		protected override void ClearPath()
		{
			base.CancelCurrentPathRequest();
			this.richPath.Clear();
			this.lastCorner = false;
			this.delayUpdatePath = false;
			this.distanceToSteeringTarget = float.PositiveInfinity;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000070A0 File Offset: 0x000052A0
		protected void NextPart()
		{
			if (!this.richPath.CompletedAllParts)
			{
				if (!this.richPath.IsLastPart)
				{
					this.lastCorner = false;
				}
				this.richPath.NextPart();
				if (this.richPath.CompletedAllParts)
				{
					this.OnTargetReached();
				}
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000070EC File Offset: 0x000052EC
		public void GetRemainingPath(List<Vector3> buffer, out bool stale)
		{
			this.richPath.GetRemainingPath(buffer, null, this.simulatedPosition, out stale);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00007102 File Offset: 0x00005302
		public void GetRemainingPath(List<Vector3> buffer, List<PathPartWithLinkInfo> partsBuffer, out bool stale)
		{
			this.richPath.GetRemainingPath(buffer, partsBuffer, this.simulatedPosition, out stale);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000033F6 File Offset: 0x000015F6
		protected virtual void OnTargetReached()
		{
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007118 File Offset: 0x00005318
		protected virtual Vector3 UpdateTarget(RichFunnel fn)
		{
			this.nextCorners.Clear();
			bool flag;
			Vector3 vector = fn.Update(this.simulatedPosition, this.nextCorners, 2, out this.lastCorner, out flag);
			if (flag && !this.waitingForPathCalculation && base.canSearch)
			{
				this.SearchPath();
			}
			return vector;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007164 File Offset: 0x00005364
		protected override void MovementUpdateInternal(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			if (this.updatePosition)
			{
				this.simulatedPosition = this.tr.position;
			}
			if (this.updateRotation)
			{
				this.simulatedRotation = this.tr.rotation;
			}
			RichPathPart currentPart = this.richPath.GetCurrentPart();
			if (currentPart is RichSpecial)
			{
				if (!this.traversingOffMeshLink && !this.richPath.CompletedAllParts)
				{
					base.StartCoroutine(this.TraverseSpecial(currentPart as RichSpecial));
				}
				nextPosition = (this.steeringTarget = this.simulatedPosition);
				nextRotation = this.rotation;
				return;
			}
			RichFunnel richFunnel = currentPart as RichFunnel;
			bool flag = base.isStopped || (this.reachedDestination && this.whenCloseToDestination == CloseToDestinationMode.Stop);
			if (this.rvoController != null)
			{
				this.rvoDensityBehavior.Update(this.rvoController.enabled, this.reachedDestination, ref flag, ref this.rvoController.priorityMultiplier, ref this.rvoController.flowFollowingStrength, this.simulatedPosition);
			}
			if (richFunnel != null && !flag)
			{
				this.TraverseFunnel(richFunnel, deltaTime, out nextPosition, out nextRotation);
				return;
			}
			this.velocity2D -= Vector2.ClampMagnitude(this.velocity2D, this.acceleration * deltaTime);
			this.FinalMovement(this.simulatedPosition, deltaTime, float.PositiveInfinity, 1f, out nextPosition, out nextRotation);
			if (richFunnel == null || base.isStopped)
			{
				this.steeringTarget = this.simulatedPosition;
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000072D8 File Offset: 0x000054D8
		private void TraverseFunnel(RichFunnel fn, float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			Vector3 vector = this.UpdateTarget(fn);
			float num;
			Vector2 vector2 = this.movementPlane.ToPlane(vector, out num);
			if (Time.frameCount % 5 == 0 && this.wallForce > 0f && this.wallDist > 0f)
			{
				this.wallBuffer.Clear();
				fn.FindWalls(this.wallBuffer, this.wallDist);
			}
			this.steeringTarget = this.nextCorners[0];
			Vector2 vector3 = this.movementPlane.ToPlane(this.steeringTarget);
			Vector2 vector4 = vector3 - vector2;
			Vector2 vector5 = VectorMath.Normalize(vector4, out this.distanceToSteeringTarget);
			Vector2 vector6 = this.CalculateWallForce(vector2, num, vector5);
			Vector2 vector7;
			if (this.approachingPartEndpoint)
			{
				vector7 = ((this.slowdownTime > 0f) ? Vector2.zero : (vector5 * this.maxSpeed));
				vector6 *= Math.Min(this.distanceToSteeringTarget / 0.5f, 1f);
				if (this.distanceToSteeringTarget <= this.endReachedDistance)
				{
					this.NextPart();
				}
			}
			else
			{
				vector7 = (((this.nextCorners.Count > 1) ? this.movementPlane.ToPlane(this.nextCorners[1]) : (vector2 + 2f * vector4)) - vector3).normalized * this.maxSpeed;
			}
			Vector2 vector8 = this.movementPlane.ToPlane(this.simulatedRotation * ((this.orientation == OrientationMode.YAxisForward) ? Vector3.up : Vector3.forward));
			Vector2 vector9 = MovementUtilities.CalculateAccelerationToReachPoint(vector3 - vector2, vector7, this.velocity2D, this.acceleration, this.rotationSpeed, this.maxSpeed, vector8);
			this.velocity2D += (vector9 + vector6 * this.wallForce) * deltaTime;
			float num2 = this.distanceToSteeringTarget + Vector3.Distance(this.steeringTarget, fn.exactEnd);
			float num3 = ((num2 < this.maxSpeed * this.slowdownTime) ? Mathf.Sqrt(num2 / (this.maxSpeed * this.slowdownTime)) : 1f);
			this.FinalMovement(vector, deltaTime, num2, num3, out nextPosition, out nextRotation);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007514 File Offset: 0x00005714
		private void FinalMovement(Vector3 position3D, float deltaTime, float distanceToEndOfPath, float speedLimitFactor, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			Vector2 vector = this.movementPlane.ToPlane(this.simulatedRotation * ((this.orientation == OrientationMode.YAxisForward) ? Vector3.up : Vector3.forward));
			this.ApplyGravity(deltaTime);
			this.velocity2D = MovementUtilities.ClampVelocity(this.velocity2D, this.maxSpeed, speedLimitFactor, this.slowWhenNotFacingTarget && this.enableRotation, this.preventMovingBackwards, vector);
			bool flag = false;
			if (this.rvoController != null && this.rvoController.enabled)
			{
				Vector3 vector2 = position3D + this.movementPlane.ToWorld(Vector2.ClampMagnitude(this.velocity2D, distanceToEndOfPath), 0f);
				this.rvoController.SetTarget(vector2, this.velocity2D.magnitude, this.maxSpeed, this.endOfPath);
				flag = this.rvoController.AvoidingAnyAgents;
			}
			Vector2 vector3 = (this.lastDeltaPosition = base.CalculateDeltaToMoveThisFrame(position3D, distanceToEndOfPath, deltaTime));
			if (this.enableRotation)
			{
				float num = this.radius * this.tr.localScale.x * 0.2f;
				float num2 = MovementUtilities.FilterRotationDirection(ref this.rotationFilterState, ref this.rotationFilterState2, vector3, num, deltaTime, flag);
				nextRotation = base.SimulateRotationTowards(this.rotationFilterState, this.rotationSpeed * deltaTime * num2, this.rotationSpeed * deltaTime);
			}
			else
			{
				nextRotation = this.simulatedRotation;
			}
			nextPosition = position3D + this.movementPlane.ToWorld(vector3, this.verticalVelocity * deltaTime);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000076A0 File Offset: 0x000058A0
		protected override Vector3 ClampToNavmesh(Vector3 position, out bool positionChanged)
		{
			if (this.richPath != null)
			{
				RichFunnel richFunnel = this.richPath.GetCurrentPart() as RichFunnel;
				if (richFunnel != null)
				{
					Vector3 vector = richFunnel.ClampToNavmesh(position);
					if (this.rvoController != null && this.rvoController.enabled)
					{
						this.rvoController.SetObstacleQuery(richFunnel.CurrentNode);
					}
					Vector2 vector2 = this.movementPlane.ToPlane(vector - position);
					float sqrMagnitude = vector2.sqrMagnitude;
					if (sqrMagnitude > 1.0000001E-06f)
					{
						this.velocity2D -= vector2 * Vector2.Dot(vector2, this.velocity2D) / sqrMagnitude;
						positionChanged = true;
						return position + this.movementPlane.ToWorld(vector2, 0f);
					}
				}
			}
			positionChanged = false;
			return position;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007770 File Offset: 0x00005970
		private Vector2 CalculateWallForce(Vector2 position, float elevation, Vector2 directionToTarget)
		{
			if (this.wallForce <= 0f || this.wallDist <= 0f)
			{
				return Vector2.zero;
			}
			float num = 0f;
			float num2 = 0f;
			Vector3 vector = this.movementPlane.ToWorld(position, elevation);
			for (int i = 0; i < this.wallBuffer.Count; i += 2)
			{
				float sqrMagnitude = (VectorMath.ClosestPointOnSegment(this.wallBuffer[i], this.wallBuffer[i + 1], vector) - vector).sqrMagnitude;
				if (sqrMagnitude <= this.wallDist * this.wallDist)
				{
					Vector2 normalized = this.movementPlane.ToPlane(this.wallBuffer[i + 1] - this.wallBuffer[i]).normalized;
					float num3 = Vector2.Dot(directionToTarget, normalized);
					float num4 = 1f - Math.Max(0f, 2f * (sqrMagnitude / (this.wallDist * this.wallDist)) - 1f);
					if (num3 > 0f)
					{
						num2 = Math.Max(num2, num3 * num4);
					}
					else
					{
						num = Math.Max(num, -num3 * num4);
					}
				}
			}
			return new Vector2(directionToTarget.y, -directionToTarget.x) * (num2 - num);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000078C3 File Offset: 0x00005AC3
		protected virtual IEnumerator TraverseSpecial(RichSpecial link)
		{
			this.traversingOffMeshLink = true;
			this.velocity2D = Vector3.zero;
			IEnumerator enumerator = ((this.onTraverseOffMeshLink != null) ? this.onTraverseOffMeshLink(link) : this.TraverseOffMeshLinkFallback(link));
			yield return base.StartCoroutine(enumerator);
			this.traversingOffMeshLink = false;
			this.NextPart();
			if (this.delayUpdatePath)
			{
				this.delayUpdatePath = false;
				if (base.canSearch)
				{
					this.SearchPath();
				}
			}
			yield break;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000078D9 File Offset: 0x00005AD9
		protected IEnumerator TraverseOffMeshLinkFallback(RichSpecial link)
		{
			float duration = ((this.maxSpeed > 0f) ? (Vector3.Distance(link.second.position, link.first.position) / this.maxSpeed) : 1f);
			float startTime = Time.time;
			for (;;)
			{
				Vector3 vector = Vector3.Lerp(link.first.position, link.second.position, Mathf.InverseLerp(startTime, startTime + duration, Time.time));
				if (this.updatePosition)
				{
					this.tr.position = vector;
				}
				else
				{
					this.simulatedPosition = vector;
				}
				if (Time.time >= startTime + duration)
				{
					break;
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000078F0 File Offset: 0x00005AF0
		public override void DrawGizmos()
		{
			base.DrawGizmos();
			if (this.tr != null)
			{
				Vector3 vector = base.position;
				for (int i = 0; i < this.nextCorners.Count; i++)
				{
					Draw.Line(vector, this.nextCorners[i], RichAI.GizmoColorPath);
					vector = this.nextCorners[i];
				}
			}
		}

		// Token: 0x040000D0 RID: 208
		public float acceleration = 5f;

		// Token: 0x040000D1 RID: 209
		public float rotationSpeed = 360f;

		// Token: 0x040000D2 RID: 210
		public float slowdownTime = 0.5f;

		// Token: 0x040000D3 RID: 211
		public float wallForce = 3f;

		// Token: 0x040000D4 RID: 212
		public float wallDist = 1f;

		// Token: 0x040000D5 RID: 213
		public bool funnelSimplification;

		// Token: 0x040000D6 RID: 214
		public bool slowWhenNotFacingTarget = true;

		// Token: 0x040000D7 RID: 215
		public bool preventMovingBackwards;

		// Token: 0x040000D8 RID: 216
		public Func<RichSpecial, IEnumerator> onTraverseOffMeshLink;

		// Token: 0x040000D9 RID: 217
		protected readonly RichPath richPath = new RichPath();

		// Token: 0x040000DA RID: 218
		protected bool delayUpdatePath;

		// Token: 0x040000DB RID: 219
		protected bool lastCorner;

		// Token: 0x040000DC RID: 220
		private Vector2 rotationFilterState;

		// Token: 0x040000DD RID: 221
		private Vector2 rotationFilterState2;

		// Token: 0x040000DE RID: 222
		protected float distanceToSteeringTarget = float.PositiveInfinity;

		// Token: 0x040000DF RID: 223
		protected readonly List<Vector3> nextCorners = new List<Vector3>();

		// Token: 0x040000E0 RID: 224
		protected readonly List<Vector3> wallBuffer = new List<Vector3>();

		// Token: 0x040000E3 RID: 227
		protected static readonly Color GizmoColorPath = new Color(0.03137255f, 0.30588236f, 0.7607843f);
	}
}
