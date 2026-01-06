using System;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000057 RID: 87
	[Serializable]
	public class GraphCollision
	{
		// Token: 0x06000448 RID: 1096 RVA: 0x0001506C File Offset: 0x0001326C
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

		// Token: 0x06000449 RID: 1097 RVA: 0x00015120 File Offset: 0x00013320
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
				RayDirection rayDirection = this.rayDirection;
				if (rayDirection == RayDirection.Up)
				{
					return !Physics.Raycast(position, this.up, this.height, this.mask, QueryTriggerInteraction.Ignore);
				}
				if (rayDirection == RayDirection.Both)
				{
					return !Physics.Raycast(position, this.up, this.height, this.mask, QueryTriggerInteraction.Ignore) && !Physics.Raycast(position + this.upheight, -this.up, this.height, this.mask, QueryTriggerInteraction.Ignore);
				}
				return !Physics.Raycast(position + this.upheight, -this.up, this.height, this.mask, QueryTriggerInteraction.Ignore);
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000152A4 File Offset: 0x000134A4
		public Vector3 CheckHeight(Vector3 position)
		{
			RaycastHit raycastHit;
			bool flag;
			return this.CheckHeight(position, out raycastHit, out flag);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x000152BC File Offset: 0x000134BC
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

		// Token: 0x0600044C RID: 1100 RVA: 0x000153D0 File Offset: 0x000135D0
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

		// Token: 0x0600044D RID: 1101 RVA: 0x00015498 File Offset: 0x00013698
		public void DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			this.type = (ColliderType)ctx.reader.ReadInt32();
			this.diameter = ctx.reader.ReadSingle();
			this.height = ctx.reader.ReadSingle();
			this.collisionOffset = ctx.reader.ReadSingle();
			this.rayDirection = (RayDirection)ctx.reader.ReadInt32();
			this.mask = ctx.reader.ReadInt32();
			this.heightMask = ctx.reader.ReadInt32();
			this.fromHeight = ctx.reader.ReadSingle();
			this.thickRaycast = ctx.reader.ReadBoolean();
			this.thickRaycastDiameter = ctx.reader.ReadSingle();
			this.unwalkableWhenNoGround = ctx.reader.ReadBoolean();
			this.use2D = ctx.reader.ReadBoolean();
			this.collisionCheck = ctx.reader.ReadBoolean();
			this.heightCheck = ctx.reader.ReadBoolean();
		}

		// Token: 0x04000286 RID: 646
		public ColliderType type = ColliderType.Capsule;

		// Token: 0x04000287 RID: 647
		public float diameter = 1f;

		// Token: 0x04000288 RID: 648
		public float height = 2f;

		// Token: 0x04000289 RID: 649
		public float collisionOffset;

		// Token: 0x0400028A RID: 650
		public RayDirection rayDirection = RayDirection.Both;

		// Token: 0x0400028B RID: 651
		public LayerMask mask;

		// Token: 0x0400028C RID: 652
		public LayerMask heightMask = -1;

		// Token: 0x0400028D RID: 653
		public float fromHeight = 100f;

		// Token: 0x0400028E RID: 654
		public bool thickRaycast;

		// Token: 0x0400028F RID: 655
		public float thickRaycastDiameter = 1f;

		// Token: 0x04000290 RID: 656
		public bool unwalkableWhenNoGround = true;

		// Token: 0x04000291 RID: 657
		public bool use2D;

		// Token: 0x04000292 RID: 658
		public bool collisionCheck = true;

		// Token: 0x04000293 RID: 659
		public bool heightCheck = true;

		// Token: 0x04000294 RID: 660
		public Vector3 up;

		// Token: 0x04000295 RID: 661
		private Vector3 upheight;

		// Token: 0x04000296 RID: 662
		private ContactFilter2D contactFilter;

		// Token: 0x04000297 RID: 663
		private static Collider2D[] dummyArray = new Collider2D[1];

		// Token: 0x04000298 RID: 664
		private float finalRadius;

		// Token: 0x04000299 RID: 665
		private float finalRaycastRadius;

		// Token: 0x0400029A RID: 666
		public const float RaycastErrorMargin = 0.005f;

		// Token: 0x0400029B RID: 667
		private RaycastHit[] hitBuffer = new RaycastHit[8];
	}
}
