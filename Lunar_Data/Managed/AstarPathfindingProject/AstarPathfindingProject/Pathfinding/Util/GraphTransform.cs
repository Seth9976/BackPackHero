using System;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x0200026E RID: 622
	public class GraphTransform : IMovementPlane, ITransform
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x0005B1C2 File Offset: 0x000593C2
		public bool identity
		{
			get
			{
				return this.isIdentity;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x0005B1CA File Offset: 0x000593CA
		public bool onlyTranslational
		{
			get
			{
				return this.isOnlyTranslational;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x0005B1D2 File Offset: 0x000593D2
		// (set) Token: 0x06000EBE RID: 3774 RVA: 0x0005B1DA File Offset: 0x000593DA
		public Matrix4x4 matrix { get; private set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x0005B1E3 File Offset: 0x000593E3
		// (set) Token: 0x06000EC0 RID: 3776 RVA: 0x0005B1EB File Offset: 0x000593EB
		public Matrix4x4 inverseMatrix { get; private set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x0005B1F4 File Offset: 0x000593F4
		// (set) Token: 0x06000EC2 RID: 3778 RVA: 0x0005B1FC File Offset: 0x000593FC
		public Quaternion rotation { get; private set; }

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0005B205 File Offset: 0x00059405
		public GraphTransform(Matrix4x4 matrix)
		{
			this.Set(matrix);
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0005B214 File Offset: 0x00059414
		protected void Set(Matrix4x4 matrix)
		{
			this.matrix = matrix;
			this.inverseMatrix = matrix.inverse;
			this.isIdentity = matrix.isIdentity;
			this.isOnlyTranslational = GraphTransform.MatrixIsTranslational(matrix);
			this.up = matrix.MultiplyVector(Vector3.up).normalized;
			this.translation = matrix.MultiplyPoint3x4(Vector3.zero);
			this.i3translation = (Int3)this.translation;
			this.rotation = Quaternion.LookRotation(this.TransformVector(Vector3.forward), this.TransformVector(Vector3.up));
			this.inverseRotation = Quaternion.Inverse(this.rotation);
			this.isXY = this.rotation == Quaternion.Euler(-90f, 0f, 0f);
			this.isXZ = this.rotation == Quaternion.Euler(0f, 0f, 0f);
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0005B307 File Offset: 0x00059507
		public Vector3 WorldUpAtGraphPosition(Vector3 point)
		{
			return this.up;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0005B310 File Offset: 0x00059510
		private static bool MatrixIsTranslational(Matrix4x4 matrix)
		{
			return matrix.GetColumn(0) == new Vector4(1f, 0f, 0f, 0f) && matrix.GetColumn(1) == new Vector4(0f, 1f, 0f, 0f) && matrix.GetColumn(2) == new Vector4(0f, 0f, 1f, 0f) && matrix.m33 == 1f;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0005B3A4 File Offset: 0x000595A4
		public Vector3 Transform(Vector3 point)
		{
			if (this.onlyTranslational)
			{
				return point + this.translation;
			}
			return this.matrix.MultiplyPoint3x4(point);
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0005B3D8 File Offset: 0x000595D8
		public Vector3 TransformVector(Vector3 dir)
		{
			if (this.onlyTranslational)
			{
				return dir;
			}
			return this.matrix.MultiplyVector(dir);
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0005B400 File Offset: 0x00059600
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

		// Token: 0x06000ECA RID: 3786 RVA: 0x0005B47C File Offset: 0x0005967C
		public unsafe void Transform(UnsafeSpan<Int3> arr)
		{
			if (this.onlyTranslational)
			{
				for (int i = arr.Length - 1; i >= 0; i--)
				{
					*arr[i] += this.i3translation;
				}
				return;
			}
			for (int j = arr.Length - 1; j >= 0; j--)
			{
				*arr[j] = (Int3)this.matrix.MultiplyPoint3x4((Vector3)(*arr[j]));
			}
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0005B50C File Offset: 0x0005970C
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

		// Token: 0x06000ECC RID: 3788 RVA: 0x0005B57C File Offset: 0x0005977C
		public Vector3 InverseTransform(Vector3 point)
		{
			if (this.onlyTranslational)
			{
				return point - this.translation;
			}
			return this.inverseMatrix.MultiplyPoint3x4(point);
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0005B5B0 File Offset: 0x000597B0
		public Vector3 InverseTransformVector(Vector3 dir)
		{
			if (this.onlyTranslational)
			{
				return dir;
			}
			return this.inverseMatrix.MultiplyVector(dir);
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0005B5D8 File Offset: 0x000597D8
		public Int3 InverseTransform(Int3 point)
		{
			if (this.onlyTranslational)
			{
				return point - this.i3translation;
			}
			return (Int3)this.inverseMatrix.MultiplyPoint3x4((Vector3)point);
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0005B614 File Offset: 0x00059814
		public void InverseTransform(Int3[] arr)
		{
			for (int i = arr.Length - 1; i >= 0; i--)
			{
				arr[i] = (Int3)this.inverseMatrix.MultiplyPoint3x4((Vector3)arr[i]);
			}
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0005B658 File Offset: 0x00059858
		public unsafe void InverseTransform(UnsafeSpan<Int3> arr)
		{
			for (int i = arr.Length - 1; i >= 0; i--)
			{
				*arr[i] = (Int3)this.inverseMatrix.MultiplyPoint3x4((Vector3)(*arr[i]));
			}
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0005B6AB File Offset: 0x000598AB
		public static GraphTransform operator *(GraphTransform lhs, Matrix4x4 rhs)
		{
			return new GraphTransform(lhs.matrix * rhs);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0005B6BE File Offset: 0x000598BE
		public static GraphTransform operator *(Matrix4x4 lhs, GraphTransform rhs)
		{
			return new GraphTransform(lhs * rhs.matrix);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0005B6D4 File Offset: 0x000598D4
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

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0005B908 File Offset: 0x00059B08
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

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0005BB3C File Offset: 0x00059D3C
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

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0005BB8A File Offset: 0x00059D8A
		Vector2 IMovementPlane.ToPlane(Vector3 point, out float elevation)
		{
			if (!this.isXZ)
			{
				point = this.inverseRotation * point;
			}
			elevation = point.y;
			return new Vector2(point.x, point.z);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0005BBBB File Offset: 0x00059DBB
		Vector3 IMovementPlane.ToWorld(Vector2 point, float elevation)
		{
			return this.rotation * new Vector3(point.x, elevation, point.y);
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0005BBDA File Offset: 0x00059DDA
		public SimpleMovementPlane ToSimpleMovementPlane()
		{
			return new SimpleMovementPlane(this.rotation);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0005BBE8 File Offset: 0x00059DE8
		public void CopyTo(MutableGraphTransform graphTransform)
		{
			graphTransform.isXY = this.isXY;
			graphTransform.isXZ = this.isXZ;
			graphTransform.isOnlyTranslational = this.isOnlyTranslational;
			graphTransform.isIdentity = this.isIdentity;
			graphTransform.matrix = this.matrix;
			graphTransform.inverseMatrix = this.inverseMatrix;
			graphTransform.up = this.up;
			graphTransform.translation = this.translation;
			graphTransform.i3translation = this.i3translation;
			graphTransform.rotation = this.rotation;
			graphTransform.inverseRotation = this.inverseRotation;
		}

		// Token: 0x04000B05 RID: 2821
		private bool isXY;

		// Token: 0x04000B06 RID: 2822
		private bool isXZ;

		// Token: 0x04000B07 RID: 2823
		private bool isOnlyTranslational;

		// Token: 0x04000B08 RID: 2824
		private bool isIdentity;

		// Token: 0x04000B0B RID: 2827
		private Vector3 up;

		// Token: 0x04000B0C RID: 2828
		private Vector3 translation;

		// Token: 0x04000B0D RID: 2829
		private Int3 i3translation;

		// Token: 0x04000B0F RID: 2831
		private Quaternion inverseRotation;

		// Token: 0x04000B10 RID: 2832
		public static readonly GraphTransform identityTransform = new GraphTransform(Matrix4x4.identity);

		// Token: 0x04000B11 RID: 2833
		public static readonly GraphTransform xyPlane = new GraphTransform(Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(-90f, 0f, 0f), Vector3.one));

		// Token: 0x04000B12 RID: 2834
		public static readonly GraphTransform xzPlane = new GraphTransform(Matrix4x4.identity);
	}
}
