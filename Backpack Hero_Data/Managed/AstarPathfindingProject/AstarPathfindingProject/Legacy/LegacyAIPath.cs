using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Legacy
{
	// Token: 0x0200009E RID: 158
	[RequireComponent(typeof(Seeker))]
	[AddComponentMenu("Pathfinding/Legacy/AI/Legacy AIPath (3D)")]
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_legacy_1_1_legacy_a_i_path.php")]
	public class LegacyAIPath : AIPath
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x0002CEDD File Offset: 0x0002B0DD
		protected override void Awake()
		{
			base.Awake();
			if (this.rvoController != null)
			{
				if (this.rvoController is LegacyRVOController)
				{
					(this.rvoController as LegacyRVOController).enableRotation = false;
					return;
				}
				Debug.LogError("The LegacyAIPath component only works with the legacy RVOController, not the latest one. Please upgrade this component", this);
			}
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0002CF20 File Offset: 0x0002B120
		protected override void OnPathComplete(Path _p)
		{
			ABPath abpath = _p as ABPath;
			if (abpath == null)
			{
				throw new Exception("This function only handles ABPaths, do not use special path types");
			}
			this.waitingForPathCalculation = false;
			abpath.Claim(this);
			if (abpath.error)
			{
				abpath.Release(this, false);
				return;
			}
			if (this.path != null)
			{
				this.path.Release(this, false);
			}
			this.path = abpath;
			this.currentWaypointIndex = 0;
			base.reachedEndOfPath = false;
			if (this.closestOnPathCheck)
			{
				Vector3 vector = ((Time.time - this.lastFoundWaypointTime < 0.3f) ? this.lastFoundWaypointPosition : abpath.originalStartPoint);
				Vector3 vector2 = this.GetFeetPosition() - vector;
				float magnitude = vector2.magnitude;
				vector2 /= magnitude;
				int num = (int)(magnitude / this.pickNextWaypointDist);
				for (int i = 0; i <= num; i++)
				{
					this.CalculateVelocity(vector);
					vector += vector2;
				}
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0002D000 File Offset: 0x0002B200
		protected override void Update()
		{
			if (!this.canMove)
			{
				return;
			}
			Vector3 vector = this.CalculateVelocity(this.GetFeetPosition());
			this.RotateTowards(this.targetDirection);
			if (this.rvoController != null)
			{
				this.rvoController.Move(vector);
				return;
			}
			if (this.controller != null)
			{
				this.controller.SimpleMove(vector);
				return;
			}
			if (this.rigid != null)
			{
				this.rigid.AddForce(vector);
				return;
			}
			this.tr.Translate(vector * Time.deltaTime, Space.World);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0002D098 File Offset: 0x0002B298
		protected float XZSqrMagnitude(Vector3 a, Vector3 b)
		{
			float num = b.x - a.x;
			float num2 = b.z - a.z;
			return num * num + num2 * num2;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0002D0C8 File Offset: 0x0002B2C8
		protected new Vector3 CalculateVelocity(Vector3 currentPosition)
		{
			if (this.path == null || this.path.vectorPath == null || this.path.vectorPath.Count == 0)
			{
				return Vector3.zero;
			}
			List<Vector3> vectorPath = this.path.vectorPath;
			if (vectorPath.Count == 1)
			{
				vectorPath.Insert(0, currentPosition);
			}
			if (this.currentWaypointIndex >= vectorPath.Count)
			{
				this.currentWaypointIndex = vectorPath.Count - 1;
			}
			if (this.currentWaypointIndex <= 1)
			{
				this.currentWaypointIndex = 1;
			}
			while (this.currentWaypointIndex < vectorPath.Count - 1 && this.XZSqrMagnitude(vectorPath[this.currentWaypointIndex], currentPosition) < this.pickNextWaypointDist * this.pickNextWaypointDist)
			{
				this.lastFoundWaypointPosition = currentPosition;
				this.lastFoundWaypointTime = Time.time;
				this.currentWaypointIndex++;
			}
			Vector3 vector = vectorPath[this.currentWaypointIndex] - vectorPath[this.currentWaypointIndex - 1];
			vector = this.CalculateTargetPoint(currentPosition, vectorPath[this.currentWaypointIndex - 1], vectorPath[this.currentWaypointIndex]) - currentPosition;
			vector.y = 0f;
			float magnitude = vector.magnitude;
			float num = Mathf.Clamp01(magnitude / this.slowdownDistance);
			this.targetDirection = vector;
			if (this.currentWaypointIndex == vectorPath.Count - 1 && magnitude <= this.endReachedDistance)
			{
				if (!base.reachedEndOfPath)
				{
					base.reachedEndOfPath = true;
					this.OnTargetReached();
				}
				return Vector3.zero;
			}
			Vector3 forward = this.tr.forward;
			float num2 = Vector3.Dot(vector.normalized, forward);
			float num3 = this.maxSpeed * Mathf.Max(num2, this.minMoveScale) * num;
			if (Time.deltaTime > 0f)
			{
				num3 = Mathf.Clamp(num3, 0f, magnitude / (Time.deltaTime * 2f));
			}
			return forward * num3;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0002D2AC File Offset: 0x0002B4AC
		protected void RotateTowards(Vector3 dir)
		{
			if (dir == Vector3.zero)
			{
				return;
			}
			Quaternion quaternion = this.tr.rotation;
			Quaternion quaternion2 = Quaternion.LookRotation(dir);
			Vector3 eulerAngles = Quaternion.Slerp(quaternion, quaternion2, base.turningSpeed * Time.deltaTime).eulerAngles;
			eulerAngles.z = 0f;
			eulerAngles.x = 0f;
			quaternion = Quaternion.Euler(eulerAngles);
			this.tr.rotation = quaternion;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0002D324 File Offset: 0x0002B524
		protected Vector3 CalculateTargetPoint(Vector3 p, Vector3 a, Vector3 b)
		{
			a.y = p.y;
			b.y = p.y;
			float magnitude = (a - b).magnitude;
			if (magnitude == 0f)
			{
				return a;
			}
			float num = Mathf.Clamp01(VectorMath.ClosestPointOnLineFactor(a, b, p));
			float magnitude2 = ((b - a) * num + a - p).magnitude;
			float num2 = Mathf.Clamp(this.forwardLook - magnitude2, 0f, this.forwardLook) / magnitude;
			num2 = Mathf.Clamp(num2 + num, 0f, 1f);
			return (b - a) * num2 + a;
		}

		// Token: 0x04000430 RID: 1072
		public float forwardLook = 1f;

		// Token: 0x04000431 RID: 1073
		public bool closestOnPathCheck = true;

		// Token: 0x04000432 RID: 1074
		protected float minMoveScale = 0.05f;

		// Token: 0x04000433 RID: 1075
		protected int currentWaypointIndex;

		// Token: 0x04000434 RID: 1076
		protected Vector3 lastFoundWaypointPosition;

		// Token: 0x04000435 RID: 1077
		protected float lastFoundWaypointTime = -9999f;

		// Token: 0x04000436 RID: 1078
		protected new Vector3 targetDirection;
	}
}
