using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020000D8 RID: 216
	public abstract class RVOObstacle : VersionedMonoBehaviour
	{
		// Token: 0x06000943 RID: 2371
		protected abstract void CreateObstacles();

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000944 RID: 2372
		protected abstract bool ExecuteInEditor { get; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000945 RID: 2373
		protected abstract bool LocalCoordinates { get; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000946 RID: 2374
		protected abstract bool StaticObstacle { get; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000947 RID: 2375
		protected abstract float Height { get; }

		// Token: 0x06000948 RID: 2376
		protected abstract bool AreGizmosDirty();

		// Token: 0x06000949 RID: 2377 RVA: 0x0003CFC9 File Offset: 0x0003B1C9
		public void OnDrawGizmos()
		{
			this.OnDrawGizmos(false);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0003CFD2 File Offset: 0x0003B1D2
		public void OnDrawGizmosSelected()
		{
			this.OnDrawGizmos(true);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0003CFDC File Offset: 0x0003B1DC
		public void OnDrawGizmos(bool selected)
		{
			this.gizmoDrawing = true;
			Gizmos.color = new Color(0.615f, 1f, 0.06f, selected ? 1f : 0.7f);
			MovementPlane movementPlane = ((RVOSimulator.active != null) ? RVOSimulator.active.movementPlane : MovementPlane.XZ);
			Vector3 vector = ((movementPlane == MovementPlane.XZ) ? Vector3.up : (-Vector3.forward));
			if (this.gizmoVerts == null || this.AreGizmosDirty() || this._obstacleMode != this.obstacleMode)
			{
				this._obstacleMode = this.obstacleMode;
				if (this.gizmoVerts == null)
				{
					this.gizmoVerts = new List<Vector3[]>();
				}
				else
				{
					this.gizmoVerts.Clear();
				}
				this.CreateObstacles();
			}
			Matrix4x4 matrix = this.GetMatrix();
			for (int i = 0; i < this.gizmoVerts.Count; i++)
			{
				Vector3[] array = this.gizmoVerts[i];
				int j = 0;
				int num = array.Length - 1;
				while (j < array.Length)
				{
					Gizmos.DrawLine(matrix.MultiplyPoint3x4(array[j]), matrix.MultiplyPoint3x4(array[num]));
					num = j++;
				}
				if (selected)
				{
					int k = 0;
					int num2 = array.Length - 1;
					while (k < array.Length)
					{
						Vector3 vector2 = matrix.MultiplyPoint3x4(array[num2]);
						Vector3 vector3 = matrix.MultiplyPoint3x4(array[k]);
						if (movementPlane != MovementPlane.XY)
						{
							Gizmos.DrawLine(vector2 + vector * this.Height, vector3 + vector * this.Height);
							Gizmos.DrawLine(vector2, vector2 + vector * this.Height);
						}
						Vector3 vector4 = (vector2 + vector3) * 0.5f;
						Vector3 normalized = (vector3 - vector2).normalized;
						if (!(normalized == Vector3.zero))
						{
							Vector3 vector5 = Vector3.Cross(vector, normalized);
							Gizmos.DrawLine(vector4, vector4 + vector5);
							Gizmos.DrawLine(vector4 + vector5, vector4 + vector5 * 0.5f + normalized * 0.5f);
							Gizmos.DrawLine(vector4 + vector5, vector4 + vector5 * 0.5f - normalized * 0.5f);
						}
						num2 = k++;
					}
				}
			}
			this.gizmoDrawing = false;
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0003D25F File Offset: 0x0003B45F
		protected virtual Matrix4x4 GetMatrix()
		{
			if (!this.LocalCoordinates)
			{
				return Matrix4x4.identity;
			}
			return base.transform.localToWorldMatrix;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0003D27C File Offset: 0x0003B47C
		public void OnDisable()
		{
			if (this.addedObstacles != null)
			{
				if (this.sim == null)
				{
					throw new Exception("This should not happen! Make sure you are not overriding the OnEnable function");
				}
				for (int i = 0; i < this.addedObstacles.Count; i++)
				{
					this.sim.RemoveObstacle(this.addedObstacles[i]);
				}
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0003D2D4 File Offset: 0x0003B4D4
		public void OnEnable()
		{
			if (this.addedObstacles != null)
			{
				if (this.sim == null)
				{
					throw new Exception("This should not happen! Make sure you are not overriding the OnDisable function");
				}
				for (int i = 0; i < this.addedObstacles.Count; i++)
				{
					ObstacleVertex obstacleVertex = this.addedObstacles[i];
					ObstacleVertex obstacleVertex2 = obstacleVertex;
					do
					{
						obstacleVertex.layer = this.layer;
						obstacleVertex = obstacleVertex.next;
					}
					while (obstacleVertex != obstacleVertex2);
					this.sim.AddObstacle(this.addedObstacles[i]);
				}
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0003D350 File Offset: 0x0003B550
		public void Start()
		{
			this.addedObstacles = new List<ObstacleVertex>();
			this.sourceObstacles = new List<Vector3[]>();
			this.prevUpdateMatrix = this.GetMatrix();
			this.CreateObstacles();
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0003D37C File Offset: 0x0003B57C
		public void Update()
		{
			Matrix4x4 matrix = this.GetMatrix();
			if (matrix != this.prevUpdateMatrix)
			{
				for (int i = 0; i < this.addedObstacles.Count; i++)
				{
					this.sim.UpdateObstacle(this.addedObstacles[i], this.sourceObstacles[i], matrix);
				}
				this.prevUpdateMatrix = matrix;
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0003D3DF File Offset: 0x0003B5DF
		protected void FindSimulator()
		{
			if (RVOSimulator.active == null)
			{
				throw new InvalidOperationException("No RVOSimulator could be found in the scene. Please add one to any GameObject");
			}
			this.sim = RVOSimulator.active.GetSimulator();
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0003D40C File Offset: 0x0003B60C
		protected void AddObstacle(Vector3[] vertices, float height)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("Vertices Must Not Be Null");
			}
			if (height < 0f)
			{
				throw new ArgumentOutOfRangeException("Height must be non-negative");
			}
			if (vertices.Length < 2)
			{
				throw new ArgumentException("An obstacle must have at least two vertices");
			}
			if (this.sim == null)
			{
				this.FindSimulator();
			}
			if (this.gizmoDrawing)
			{
				Vector3[] array = new Vector3[vertices.Length];
				this.WindCorrectly(vertices);
				Array.Copy(vertices, array, vertices.Length);
				this.gizmoVerts.Add(array);
				return;
			}
			if (vertices.Length == 2)
			{
				this.AddObstacleInternal(vertices, height);
				return;
			}
			this.WindCorrectly(vertices);
			this.AddObstacleInternal(vertices, height);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0003D4A6 File Offset: 0x0003B6A6
		private void AddObstacleInternal(Vector3[] vertices, float height)
		{
			this.addedObstacles.Add(this.sim.AddObstacle(vertices, height, this.GetMatrix(), this.layer, true));
			this.sourceObstacles.Add(vertices);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0003D4DC File Offset: 0x0003B6DC
		private void WindCorrectly(Vector3[] vertices)
		{
			int num = 0;
			float num2 = float.PositiveInfinity;
			Matrix4x4 matrix = this.GetMatrix();
			for (int i = 0; i < vertices.Length; i++)
			{
				float x = matrix.MultiplyPoint3x4(vertices[i]).x;
				if (x < num2)
				{
					num = i;
					num2 = x;
				}
			}
			Vector3 vector = matrix.MultiplyPoint3x4(vertices[(num - 1 + vertices.Length) % vertices.Length]);
			Vector3 vector2 = matrix.MultiplyPoint3x4(vertices[num]);
			Vector3 vector3 = matrix.MultiplyPoint3x4(vertices[(num + 1) % vertices.Length]);
			MovementPlane movementPlane;
			if (this.sim != null)
			{
				movementPlane = this.sim.movementPlane;
			}
			else if (RVOSimulator.active)
			{
				movementPlane = RVOSimulator.active.movementPlane;
			}
			else
			{
				movementPlane = MovementPlane.XZ;
			}
			if (movementPlane == MovementPlane.XY)
			{
				vector.z = vector.y;
				vector2.z = vector2.y;
				vector3.z = vector3.y;
			}
			if (VectorMath.IsClockwiseXZ(vector, vector2, vector3) != (this.obstacleMode == RVOObstacle.ObstacleVertexWinding.KeepIn))
			{
				Array.Reverse<Vector3>(vertices);
			}
		}

		// Token: 0x0400056D RID: 1389
		public RVOObstacle.ObstacleVertexWinding obstacleMode;

		// Token: 0x0400056E RID: 1390
		public RVOLayer layer = RVOLayer.DefaultObstacle;

		// Token: 0x0400056F RID: 1391
		protected Simulator sim;

		// Token: 0x04000570 RID: 1392
		private List<ObstacleVertex> addedObstacles;

		// Token: 0x04000571 RID: 1393
		private List<Vector3[]> sourceObstacles;

		// Token: 0x04000572 RID: 1394
		private bool gizmoDrawing;

		// Token: 0x04000573 RID: 1395
		private List<Vector3[]> gizmoVerts;

		// Token: 0x04000574 RID: 1396
		private RVOObstacle.ObstacleVertexWinding _obstacleMode;

		// Token: 0x04000575 RID: 1397
		private Matrix4x4 prevUpdateMatrix;

		// Token: 0x02000164 RID: 356
		public enum ObstacleVertexWinding
		{
			// Token: 0x040007EC RID: 2028
			KeepOut,
			// Token: 0x040007ED RID: 2029
			KeepIn
		}
	}
}
