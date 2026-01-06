using System;
using Pathfinding.Graphs.Grid.Jobs;
using Pathfinding.Jobs;
using Pathfinding.Util;
using Unity.Collections;
using UnityEngine;

namespace Pathfinding.Graphs.Grid
{
	// Token: 0x020001F6 RID: 502
	[Serializable]
	public class GraphCollision
	{
		// Token: 0x06000C6C RID: 3180 RVA: 0x0004D6FC File Offset: 0x0004B8FC
		public void Initialize(GraphTransform transform, float scale)
		{
			this.up = (transform.Transform(Vector3.up) - transform.Transform(Vector3.zero)).normalized;
			this.upheight = this.up * this.height;
			this.finalRadius = this.diameter * scale * 0.5f;
			this.finalRaycastRadius = this.thickRaycastDiameter * scale * 0.5f;
			this.contactFilter = new ContactFilter2D
			{
				layerMask = this.mask,
				useDepth = false,
				useLayerMask = true,
				useNormalAngle = false,
				useTriggers = false
			};
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0004D7B0 File Offset: 0x0004B9B0
		public bool Check(Vector3 position)
		{
			if (!this.collisionCheck)
			{
				return true;
			}
			if (this.use2D)
			{
				ColliderType colliderType = this.type;
				if (colliderType <= ColliderType.Capsule)
				{
					return Physics2D.OverlapCircle(position, this.finalRadius, this.contactFilter, GraphCollision.dummyArray) == 0;
				}
				return Physics2D.OverlapPoint(position, this.contactFilter, GraphCollision.dummyArray) == 0;
			}
			else
			{
				position += this.up * this.collisionOffset;
				ColliderType colliderType = this.type;
				if (colliderType == ColliderType.Sphere)
				{
					return !Physics.CheckSphere(position, this.finalRadius, this.mask, QueryTriggerInteraction.Ignore);
				}
				if (colliderType == ColliderType.Capsule)
				{
					return !Physics.CheckCapsule(position, position + this.upheight, this.finalRadius, this.mask, QueryTriggerInteraction.Ignore);
				}
				return !Physics.Raycast(position, this.up, this.height, this.mask, QueryTriggerInteraction.Ignore) && !Physics.Raycast(position + this.upheight, -this.up, this.height, this.mask, QueryTriggerInteraction.Ignore);
			}
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0004D8D4 File Offset: 0x0004BAD4
		public Vector3 CheckHeight(Vector3 position)
		{
			RaycastHit raycastHit;
			bool flag;
			return this.CheckHeight(position, out raycastHit, out flag);
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0004D8EC File Offset: 0x0004BAEC
		public Vector3 CheckHeight(Vector3 position, out RaycastHit hit, out bool walkable)
		{
			walkable = true;
			if (!this.heightCheck || this.use2D)
			{
				hit = default(RaycastHit);
				return position;
			}
			if (this.thickRaycast)
			{
				Ray ray = new Ray(position + this.up * this.fromHeight, -this.up);
				if (Physics.SphereCast(ray, this.finalRaycastRadius, out hit, this.fromHeight + 0.005f, this.heightMask, QueryTriggerInteraction.Ignore))
				{
					return VectorMath.ClosestPointOnLine(ray.origin, ray.origin + ray.direction, hit.point);
				}
				walkable &= !this.unwalkableWhenNoGround;
			}
			else
			{
				if (Physics.Raycast(position + this.up * this.fromHeight, -this.up, out hit, this.fromHeight + 0.005f, this.heightMask, QueryTriggerInteraction.Ignore))
				{
					return hit.point;
				}
				walkable &= !this.unwalkableWhenNoGround;
			}
			return position;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0004DA00 File Offset: 0x0004BC00
		public RaycastHit[] CheckHeightAll(Vector3 position, out int numHits)
		{
			if (!this.heightCheck || this.use2D)
			{
				this.hitBuffer[0] = new RaycastHit
				{
					point = position,
					distance = 0f
				};
				numHits = 1;
				return this.hitBuffer;
			}
			numHits = Physics.RaycastNonAlloc(position + this.up * this.fromHeight, -this.up, this.hitBuffer, this.fromHeight + 0.005f, this.heightMask, QueryTriggerInteraction.Ignore);
			if (numHits == this.hitBuffer.Length)
			{
				this.hitBuffer = new RaycastHit[this.hitBuffer.Length * 2];
				return this.CheckHeightAll(position, out numHits);
			}
			return this.hitBuffer;
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0004DAC8 File Offset: 0x0004BCC8
		public void JobCollisionRay(NativeArray<Vector3> nodePositions, NativeArray<bool> collisionCheckResult, Vector3 up, Allocator allocationMethod, JobDependencyTracker dependencyTracker)
		{
			NativeArray<RaycastCommand> nativeArray = dependencyTracker.NewNativeArray<RaycastCommand>(nodePositions.Length, allocationMethod, NativeArrayOptions.ClearMemory);
			NativeArray<RaycastCommand> nativeArray2 = dependencyTracker.NewNativeArray<RaycastCommand>(nodePositions.Length, allocationMethod, NativeArrayOptions.ClearMemory);
			NativeArray<RaycastHit> nativeArray3 = dependencyTracker.NewNativeArray<RaycastHit>(nodePositions.Length, allocationMethod, NativeArrayOptions.ClearMemory);
			NativeArray<RaycastHit> nativeArray4 = dependencyTracker.NewNativeArray<RaycastHit>(nodePositions.Length, allocationMethod, NativeArrayOptions.ClearMemory);
			new JobPrepareRaycasts
			{
				origins = nodePositions,
				originOffset = up * (this.height + this.collisionOffset),
				direction = -up,
				distance = this.height,
				mask = this.mask,
				physicsScene = Physics.defaultPhysicsScene,
				raycastCommands = nativeArray
			}.Schedule(dependencyTracker);
			new JobPrepareRaycasts
			{
				origins = nodePositions,
				originOffset = up * this.collisionOffset,
				direction = up,
				distance = this.height,
				mask = this.mask,
				physicsScene = Physics.defaultPhysicsScene,
				raycastCommands = nativeArray2
			}.Schedule(dependencyTracker);
			dependencyTracker.ScheduleBatch(nativeArray, nativeArray3, 2048);
			dependencyTracker.ScheduleBatch(nativeArray2, nativeArray4, 2048);
			new JobMergeRaycastCollisionHits
			{
				hit1 = nativeArray3,
				hit2 = nativeArray4,
				result = collisionCheckResult
			}.Schedule(dependencyTracker);
		}

		// Token: 0x04000942 RID: 2370
		public ColliderType type = ColliderType.Capsule;

		// Token: 0x04000943 RID: 2371
		public float diameter = 1f;

		// Token: 0x04000944 RID: 2372
		public float height = 2f;

		// Token: 0x04000945 RID: 2373
		public float collisionOffset;

		// Token: 0x04000946 RID: 2374
		[Obsolete("Only the Both mode is supported now")]
		public RayDirection rayDirection = RayDirection.Both;

		// Token: 0x04000947 RID: 2375
		public LayerMask mask;

		// Token: 0x04000948 RID: 2376
		public LayerMask heightMask = -1;

		// Token: 0x04000949 RID: 2377
		public float fromHeight = 100f;

		// Token: 0x0400094A RID: 2378
		public bool thickRaycast;

		// Token: 0x0400094B RID: 2379
		public float thickRaycastDiameter = 1f;

		// Token: 0x0400094C RID: 2380
		public bool unwalkableWhenNoGround = true;

		// Token: 0x0400094D RID: 2381
		public bool use2D;

		// Token: 0x0400094E RID: 2382
		public bool collisionCheck = true;

		// Token: 0x0400094F RID: 2383
		public bool heightCheck = true;

		// Token: 0x04000950 RID: 2384
		public Vector3 up;

		// Token: 0x04000951 RID: 2385
		private Vector3 upheight;

		// Token: 0x04000952 RID: 2386
		private ContactFilter2D contactFilter;

		// Token: 0x04000953 RID: 2387
		private static Collider2D[] dummyArray = new Collider2D[1];

		// Token: 0x04000954 RID: 2388
		private float finalRadius;

		// Token: 0x04000955 RID: 2389
		private float finalRaycastRadius;

		// Token: 0x04000956 RID: 2390
		public const float RaycastErrorMargin = 0.005f;

		// Token: 0x04000957 RID: 2391
		private RaycastHit[] hitBuffer = new RaycastHit[8];
	}
}
