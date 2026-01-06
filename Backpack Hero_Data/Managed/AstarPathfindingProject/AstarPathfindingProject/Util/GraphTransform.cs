using System;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020000C6 RID: 198
	public class GraphTransform : IMovementPlane, ITransform
	{
		// Token: 0x06000867 RID: 2151 RVA: 0x00037E28 File Offset: 0x00036028
		public GraphTransform(Matrix4x4 matrix)
		{
			this.matrix = matrix;
			this.inverseMatrix = matrix.inverse;
			this.identity = matrix.isIdentity;
			this.onlyTranslational = GraphTransform.MatrixIsTranslational(matrix);
			this.up = matrix.MultiplyVector(Vector3.up).normalized;
			this.translation = matrix.MultiplyPoint3x4(Vector3.zero);
			this.i3translation = (Int3)this.translation;
			this.rotation = Quaternion.LookRotation(this.TransformVector(Vector3.forward), this.TransformVector(Vector3.up));
			this.inverseRotation = Quaternion.Inverse(this.rotation);
			this.isXY = this.rotation == Quaternion.Euler(-90f, 0f, 0f);
			this.isXZ = this.rotation == Quaternion.Euler(0f, 0f, 0f);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00037F21 File Offset: 0x00036121
		public Vector3 WorldUpAtGraphPosition(Vector3 point)
		{
			return this.up;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00037F2C File Offset: 0x0003612C
		private static bool MatrixIsTranslational(Matrix4x4 matrix)
		{
			return matrix.GetColumn(0) == new Vector4(1f, 0f, 0f, 0f) && matrix.GetColumn(1) == new Vector4(0f, 1f, 0f, 0f) && matrix.GetColumn(2) == new Vector4(0f, 0f, 1f, 0f) && matrix.m33 == 1f;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00037FC0 File Offset: 0x000361C0
		public Vector3 Transform(Vector3 point)
		{
			if (this.onlyTranslational)
			{
				return point + this.translation;
			}
			return this.matrix.MultiplyPoint3x4(point);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00037FF4 File Offset: 0x000361F4
		public Vector3 TransformVector(Vector3 point)
		{
			if (this.onlyTranslational)
			{
				return point;
			}
			return this.matrix.MultiplyVector(point);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0003801C File Offset: 0x0003621C
		public void Transform(Int3[] arr)
		{
			if (this.onlyTranslational)
			{
				for (int i = arr.Length - 1; i >= 0; i--)
				{
					arr[i] += this.i3translation;
				}
				return;
			}
			for (int j = arr.Length - 1; j >= 0; j--)
			{
				arr[j] = (Int3)this.matrix.MultiplyPoint3x4((Vector3)arr[j]);
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00038098 File Offset: 0x00036298
		public void Transform(Vector3[] arr)
		{
			if (this.onlyTranslational)
			{
				for (int i = arr.Length - 1; i >= 0; i--)
				{
					arr[i] += this.translation;
				}
				return;
			}
			for (int j = arr.Length - 1; j >= 0; j--)
			{
				arr[j] = this.matrix.MultiplyPoint3x4(arr[j]);
			}
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00038108 File Offset: 0x00036308
		public Vector3 InverseTransform(Vector3 point)
		{
			if (this.onlyTranslational)
			{
				return point - this.translation;
			}
			return this.inverseMatrix.MultiplyPoint3x4(point);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0003813C File Offset: 0x0003633C
		public Int3 InverseTransform(Int3 point)
		{
			if (this.onlyTranslational)
			{
				return point - this.i3translation;
			}
			return (Int3)this.inverseMatrix.MultiplyPoint3x4((Vector3)point);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00038178 File Offset: 0x00036378
		public void InverseTransform(Int3[] arr)
		{
			for (int i = arr.Length - 1; i >= 0; i--)
			{
				arr[i] = (Int3)this.inverseMatrix.MultiplyPoint3x4((Vector3)arr[i]);
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000381BB File Offset: 0x000363BB
		public static GraphTransform operator *(GraphTransform lhs, Matrix4x4 rhs)
		{
			return new GraphTransform(lhs.matrix * rhs);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000381CE File Offset: 0x000363CE
		public static GraphTransform operator *(Matrix4x4 lhs, GraphTransform rhs)
		{
			return new GraphTransform(lhs * rhs.matrix);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x000381E4 File Offset: 0x000363E4
		public Bounds Transform(Bounds bounds)
		{
			if (this.onlyTranslational)
			{
				return new Bounds(bounds.center + this.translation, bounds.size);
			}
			Vector3[] array = ArrayPool<Vector3>.Claim(8);
			Vector3 extents = bounds.extents;
			array[0] = this.Transform(bounds.center + new Vector3(extents.x, extents.y, extents.z));
			array[1] = this.Transform(bounds.center + new Vector3(extents.x, extents.y, -extents.z));
			array[2] = this.Transform(bounds.center + new Vector3(extents.x, -extents.y, extents.z));
			array[3] = this.Transform(bounds.center + new Vector3(extents.x, -extents.y, -extents.z));
			array[4] = this.Transform(bounds.center + new Vector3(-extents.x, extents.y, extents.z));
			array[5] = this.Transform(bounds.center + new Vector3(-extents.x, extents.y, -extents.z));
			array[6] = this.Transform(bounds.center + new Vector3(-extents.x, -extents.y, extents.z));
			array[7] = this.Transform(bounds.center + new Vector3(-extents.x, -extents.y, -extents.z));
			Vector3 vector = array[0];
			Vector3 vector2 = array[0];
			for (int i = 1; i < 8; i++)
			{
				vector = Vector3.Min(vector, array[i]);
				vector2 = Vector3.Max(vector2, array[i]);
			}
			ArrayPool<Vector3>.Release(ref array, false);
			return new Bounds((vector + vector2) * 0.5f, vector2 - vector);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00038418 File Offset: 0x00036618
		public Bounds InverseTransform(Bounds bounds)
		{
			if (this.onlyTranslational)
			{
				return new Bounds(bounds.center - this.translation, bounds.size);
			}
			Vector3[] array = ArrayPool<Vector3>.Claim(8);
			Vector3 extents = bounds.extents;
			array[0] = this.InverseTransform(bounds.center + new Vector3(extents.x, extents.y, extents.z));
			array[1] = this.InverseTransform(bounds.center + new Vector3(extents.x, extents.y, -extents.z));
			array[2] = this.InverseTransform(bounds.center + new Vector3(extents.x, -extents.y, extents.z));
			array[3] = this.InverseTransform(bounds.center + new Vector3(extents.x, -extents.y, -extents.z));
			array[4] = this.InverseTransform(bounds.center + new Vector3(-extents.x, extents.y, extents.z));
			array[5] = this.InverseTransform(bounds.center + new Vector3(-extents.x, extents.y, -extents.z));
			array[6] = this.InverseTransform(bounds.center + new Vector3(-extents.x, -extents.y, extents.z));
			array[7] = this.InverseTransform(bounds.center + new Vector3(-extents.x, -extents.y, -extents.z));
			Vector3 vector = array[0];
			Vector3 vector2 = array[0];
			for (int i = 1; i < 8; i++)
			{
				vector = Vector3.Min(vector, array[i]);
				vector2 = Vector3.Max(vector2, array[i]);
			}
			ArrayPool<Vector3>.Release(ref array, false);
			return new Bounds((vector + vector2) * 0.5f, vector2 - vector);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0003864C File Offset: 0x0003684C
		Vector2 IMovementPlane.ToPlane(Vector3 point)
		{
			if (this.isXY)
			{
				return new Vector2(point.x, point.y);
			}
			if (!this.isXZ)
			{
				point = this.inverseRotation * point;
			}
			return new Vector2(point.x, point.z);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0003869A File Offset: 0x0003689A
		Vector2 IMovementPlane.ToPlane(Vector3 point, out float elevation)
		{
			if (!this.isXZ)
			{
				point = this.inverseRotation * point;
			}
			elevation = point.y;
			return new Vector2(point.x, point.z);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x000386CB File Offset: 0x000368CB
		Vector3 IMovementPlane.ToWorld(Vector2 point, float elevation)
		{
			return this.rotation * new Vector3(point.x, elevation, point.y);
		}

		// Token: 0x040004DE RID: 1246
		public readonly bool identity;

		// Token: 0x040004DF RID: 1247
		public readonly bool onlyTranslational;

		// Token: 0x040004E0 RID: 1248
		private readonly bool isXY;

		// Token: 0x040004E1 RID: 1249
		private readonly bool isXZ;

		// Token: 0x040004E2 RID: 1250
		private readonly Matrix4x4 matrix;

		// Token: 0x040004E3 RID: 1251
		private readonly Matrix4x4 inverseMatrix;

		// Token: 0x040004E4 RID: 1252
		private readonly Vector3 up;

		// Token: 0x040004E5 RID: 1253
		private readonly Vector3 translation;

		// Token: 0x040004E6 RID: 1254
		private readonly Int3 i3translation;

		// Token: 0x040004E7 RID: 1255
		private readonly Quaternion rotation;

		// Token: 0x040004E8 RID: 1256
		private readonly Quaternion inverseRotation;

		// Token: 0x040004E9 RID: 1257
		public static readonly GraphTransform identityTransform = new GraphTransform(Matrix4x4.identity);
	}
}
