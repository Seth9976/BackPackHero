using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001C4 RID: 452
	[NativeType(Header = "Runtime/Math/Matrix4x4.h")]
	[Il2CppEagerStaticClassConstruction]
	[NativeClass("Matrix4x4f")]
	[NativeHeader("Runtime/Math/MathScripting.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct Matrix4x4 : IEquatable<Matrix4x4>, IFormattable
	{
		// Token: 0x060013BD RID: 5053 RVA: 0x0001C554 File Offset: 0x0001A754
		[ThreadSafe]
		private Quaternion GetRotation()
		{
			Quaternion quaternion;
			Matrix4x4.GetRotation_Injected(ref this, out quaternion);
			return quaternion;
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0001C56C File Offset: 0x0001A76C
		[ThreadSafe]
		private Vector3 GetLossyScale()
		{
			Vector3 vector;
			Matrix4x4.GetLossyScale_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0001C582 File Offset: 0x0001A782
		[ThreadSafe]
		private bool IsIdentity()
		{
			return Matrix4x4.IsIdentity_Injected(ref this);
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0001C58A File Offset: 0x0001A78A
		[ThreadSafe]
		private float GetDeterminant()
		{
			return Matrix4x4.GetDeterminant_Injected(ref this);
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0001C594 File Offset: 0x0001A794
		[ThreadSafe]
		private FrustumPlanes DecomposeProjection()
		{
			FrustumPlanes frustumPlanes;
			Matrix4x4.DecomposeProjection_Injected(ref this, out frustumPlanes);
			return frustumPlanes;
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x0001C5AC File Offset: 0x0001A7AC
		public Quaternion rotation
		{
			get
			{
				return this.GetRotation();
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x0001C5C4 File Offset: 0x0001A7C4
		public Vector3 lossyScale
		{
			get
			{
				return this.GetLossyScale();
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x0001C5DC File Offset: 0x0001A7DC
		public bool isIdentity
		{
			get
			{
				return this.IsIdentity();
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x0001C5F4 File Offset: 0x0001A7F4
		public float determinant
		{
			get
			{
				return this.GetDeterminant();
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x0001C60C File Offset: 0x0001A80C
		public FrustumPlanes decomposeProjection
		{
			get
			{
				return this.DecomposeProjection();
			}
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0001C624 File Offset: 0x0001A824
		[ThreadSafe]
		public bool ValidTRS()
		{
			return Matrix4x4.ValidTRS_Injected(ref this);
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0001C62C File Offset: 0x0001A82C
		public static float Determinant(Matrix4x4 m)
		{
			return m.determinant;
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0001C648 File Offset: 0x0001A848
		[FreeFunction("MatrixScripting::TRS", IsThreadSafe = true)]
		public static Matrix4x4 TRS(Vector3 pos, Quaternion q, Vector3 s)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.TRS_Injected(ref pos, ref q, ref s, out matrix4x);
			return matrix4x;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0001C663 File Offset: 0x0001A863
		public void SetTRS(Vector3 pos, Quaternion q, Vector3 s)
		{
			this = Matrix4x4.TRS(pos, q, s);
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0001C674 File Offset: 0x0001A874
		[FreeFunction("MatrixScripting::Inverse3DAffine", IsThreadSafe = true)]
		public static bool Inverse3DAffine(Matrix4x4 input, ref Matrix4x4 result)
		{
			return Matrix4x4.Inverse3DAffine_Injected(ref input, ref result);
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0001C680 File Offset: 0x0001A880
		[FreeFunction("MatrixScripting::Inverse", IsThreadSafe = true)]
		public static Matrix4x4 Inverse(Matrix4x4 m)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Inverse_Injected(ref m, out matrix4x);
			return matrix4x;
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x0001C698 File Offset: 0x0001A898
		public Matrix4x4 inverse
		{
			get
			{
				return Matrix4x4.Inverse(this);
			}
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0001C6B8 File Offset: 0x0001A8B8
		[FreeFunction("MatrixScripting::Transpose", IsThreadSafe = true)]
		public static Matrix4x4 Transpose(Matrix4x4 m)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Transpose_Injected(ref m, out matrix4x);
			return matrix4x;
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x0001C6D0 File Offset: 0x0001A8D0
		public Matrix4x4 transpose
		{
			get
			{
				return Matrix4x4.Transpose(this);
			}
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0001C6F0 File Offset: 0x0001A8F0
		[FreeFunction("MatrixScripting::Ortho", IsThreadSafe = true)]
		public static Matrix4x4 Ortho(float left, float right, float bottom, float top, float zNear, float zFar)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Ortho_Injected(left, right, bottom, top, zNear, zFar, out matrix4x);
			return matrix4x;
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0001C710 File Offset: 0x0001A910
		[FreeFunction("MatrixScripting::Perspective", IsThreadSafe = true)]
		public static Matrix4x4 Perspective(float fov, float aspect, float zNear, float zFar)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Perspective_Injected(fov, aspect, zNear, zFar, out matrix4x);
			return matrix4x;
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0001C72C File Offset: 0x0001A92C
		[FreeFunction("MatrixScripting::LookAt", IsThreadSafe = true)]
		public static Matrix4x4 LookAt(Vector3 from, Vector3 to, Vector3 up)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.LookAt_Injected(ref from, ref to, ref up, out matrix4x);
			return matrix4x;
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0001C748 File Offset: 0x0001A948
		[FreeFunction("MatrixScripting::Frustum", IsThreadSafe = true)]
		public static Matrix4x4 Frustum(float left, float right, float bottom, float top, float zNear, float zFar)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Frustum_Injected(left, right, bottom, top, zNear, zFar, out matrix4x);
			return matrix4x;
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0001C768 File Offset: 0x0001A968
		public static Matrix4x4 Frustum(FrustumPlanes fp)
		{
			return Matrix4x4.Frustum(fp.left, fp.right, fp.bottom, fp.top, fp.zNear, fp.zFar);
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0001C7A4 File Offset: 0x0001A9A4
		public Matrix4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
		{
			this.m00 = column0.x;
			this.m01 = column1.x;
			this.m02 = column2.x;
			this.m03 = column3.x;
			this.m10 = column0.y;
			this.m11 = column1.y;
			this.m12 = column2.y;
			this.m13 = column3.y;
			this.m20 = column0.z;
			this.m21 = column1.z;
			this.m22 = column2.z;
			this.m23 = column3.z;
			this.m30 = column0.w;
			this.m31 = column1.w;
			this.m32 = column2.w;
			this.m33 = column3.w;
		}

		// Token: 0x1700040D RID: 1037
		public float this[int row, int column]
		{
			[MethodImpl(256)]
			get
			{
				return this[row + column * 4];
			}
			[MethodImpl(256)]
			set
			{
				this[row + column * 4] = value;
			}
		}

		// Token: 0x1700040E RID: 1038
		public float this[int index]
		{
			get
			{
				float num;
				switch (index)
				{
				case 0:
					num = this.m00;
					break;
				case 1:
					num = this.m10;
					break;
				case 2:
					num = this.m20;
					break;
				case 3:
					num = this.m30;
					break;
				case 4:
					num = this.m01;
					break;
				case 5:
					num = this.m11;
					break;
				case 6:
					num = this.m21;
					break;
				case 7:
					num = this.m31;
					break;
				case 8:
					num = this.m02;
					break;
				case 9:
					num = this.m12;
					break;
				case 10:
					num = this.m22;
					break;
				case 11:
					num = this.m32;
					break;
				case 12:
					num = this.m03;
					break;
				case 13:
					num = this.m13;
					break;
				case 14:
					num = this.m23;
					break;
				case 15:
					num = this.m33;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid matrix index!");
				}
				return num;
			}
			set
			{
				switch (index)
				{
				case 0:
					this.m00 = value;
					break;
				case 1:
					this.m10 = value;
					break;
				case 2:
					this.m20 = value;
					break;
				case 3:
					this.m30 = value;
					break;
				case 4:
					this.m01 = value;
					break;
				case 5:
					this.m11 = value;
					break;
				case 6:
					this.m21 = value;
					break;
				case 7:
					this.m31 = value;
					break;
				case 8:
					this.m02 = value;
					break;
				case 9:
					this.m12 = value;
					break;
				case 10:
					this.m22 = value;
					break;
				case 11:
					this.m32 = value;
					break;
				case 12:
					this.m03 = value;
					break;
				case 13:
					this.m13 = value;
					break;
				case 14:
					this.m23 = value;
					break;
				case 15:
					this.m33 = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid matrix index!");
				}
			}
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0001CAB0 File Offset: 0x0001ACB0
		[MethodImpl(256)]
		public override int GetHashCode()
		{
			return this.GetColumn(0).GetHashCode() ^ (this.GetColumn(1).GetHashCode() << 2) ^ (this.GetColumn(2).GetHashCode() >> 2) ^ (this.GetColumn(3).GetHashCode() >> 1);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0001CB20 File Offset: 0x0001AD20
		[MethodImpl(256)]
		public override bool Equals(object other)
		{
			bool flag = !(other is Matrix4x4);
			return !flag && this.Equals((Matrix4x4)other);
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0001CB54 File Offset: 0x0001AD54
		[MethodImpl(256)]
		public bool Equals(Matrix4x4 other)
		{
			return this.GetColumn(0).Equals(other.GetColumn(0)) && this.GetColumn(1).Equals(other.GetColumn(1)) && this.GetColumn(2).Equals(other.GetColumn(2)) && this.GetColumn(3).Equals(other.GetColumn(3));
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0001CBCC File Offset: 0x0001ADCC
		public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			Matrix4x4 matrix4x;
			matrix4x.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20 + lhs.m03 * rhs.m30;
			matrix4x.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21 + lhs.m03 * rhs.m31;
			matrix4x.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 + lhs.m03 * rhs.m32;
			matrix4x.m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03 * rhs.m33;
			matrix4x.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 + lhs.m13 * rhs.m30;
			matrix4x.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31;
			matrix4x.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32;
			matrix4x.m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33;
			matrix4x.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 + lhs.m23 * rhs.m30;
			matrix4x.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31;
			matrix4x.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32;
			matrix4x.m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33;
			matrix4x.m30 = lhs.m30 * rhs.m00 + lhs.m31 * rhs.m10 + lhs.m32 * rhs.m20 + lhs.m33 * rhs.m30;
			matrix4x.m31 = lhs.m30 * rhs.m01 + lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31;
			matrix4x.m32 = lhs.m30 * rhs.m02 + lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32;
			matrix4x.m33 = lhs.m30 * rhs.m03 + lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33;
			return matrix4x;
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0001CFC0 File Offset: 0x0001B1C0
		public static Vector4 operator *(Matrix4x4 lhs, Vector4 vector)
		{
			Vector4 vector2;
			vector2.x = lhs.m00 * vector.x + lhs.m01 * vector.y + lhs.m02 * vector.z + lhs.m03 * vector.w;
			vector2.y = lhs.m10 * vector.x + lhs.m11 * vector.y + lhs.m12 * vector.z + lhs.m13 * vector.w;
			vector2.z = lhs.m20 * vector.x + lhs.m21 * vector.y + lhs.m22 * vector.z + lhs.m23 * vector.w;
			vector2.w = lhs.m30 * vector.x + lhs.m31 * vector.y + lhs.m32 * vector.z + lhs.m33 * vector.w;
			return vector2;
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0001D0CC File Offset: 0x0001B2CC
		public static bool operator ==(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return lhs.GetColumn(0) == rhs.GetColumn(0) && lhs.GetColumn(1) == rhs.GetColumn(1) && lhs.GetColumn(2) == rhs.GetColumn(2) && lhs.GetColumn(3) == rhs.GetColumn(3);
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0001D13C File Offset: 0x0001B33C
		public static bool operator !=(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0001D158 File Offset: 0x0001B358
		public Vector4 GetColumn(int index)
		{
			Vector4 vector;
			switch (index)
			{
			case 0:
				vector = new Vector4(this.m00, this.m10, this.m20, this.m30);
				break;
			case 1:
				vector = new Vector4(this.m01, this.m11, this.m21, this.m31);
				break;
			case 2:
				vector = new Vector4(this.m02, this.m12, this.m22, this.m32);
				break;
			case 3:
				vector = new Vector4(this.m03, this.m13, this.m23, this.m33);
				break;
			default:
				throw new IndexOutOfRangeException("Invalid column index!");
			}
			return vector;
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0001D214 File Offset: 0x0001B414
		public Vector4 GetRow(int index)
		{
			Vector4 vector;
			switch (index)
			{
			case 0:
				vector = new Vector4(this.m00, this.m01, this.m02, this.m03);
				break;
			case 1:
				vector = new Vector4(this.m10, this.m11, this.m12, this.m13);
				break;
			case 2:
				vector = new Vector4(this.m20, this.m21, this.m22, this.m23);
				break;
			case 3:
				vector = new Vector4(this.m30, this.m31, this.m32, this.m33);
				break;
			default:
				throw new IndexOutOfRangeException("Invalid row index!");
			}
			return vector;
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0001D2D0 File Offset: 0x0001B4D0
		public Vector3 GetPosition()
		{
			return new Vector3(this.m03, this.m13, this.m23);
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0001D2F9 File Offset: 0x0001B4F9
		public void SetColumn(int index, Vector4 column)
		{
			this[0, index] = column.x;
			this[1, index] = column.y;
			this[2, index] = column.z;
			this[3, index] = column.w;
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0001D338 File Offset: 0x0001B538
		public void SetRow(int index, Vector4 row)
		{
			this[index, 0] = row.x;
			this[index, 1] = row.y;
			this[index, 2] = row.z;
			this[index, 3] = row.w;
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0001D378 File Offset: 0x0001B578
		public Vector3 MultiplyPoint(Vector3 point)
		{
			Vector3 vector;
			vector.x = this.m00 * point.x + this.m01 * point.y + this.m02 * point.z + this.m03;
			vector.y = this.m10 * point.x + this.m11 * point.y + this.m12 * point.z + this.m13;
			vector.z = this.m20 * point.x + this.m21 * point.y + this.m22 * point.z + this.m23;
			float num = this.m30 * point.x + this.m31 * point.y + this.m32 * point.z + this.m33;
			num = 1f / num;
			vector.x *= num;
			vector.y *= num;
			vector.z *= num;
			return vector;
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0001D490 File Offset: 0x0001B690
		public Vector3 MultiplyPoint3x4(Vector3 point)
		{
			Vector3 vector;
			vector.x = this.m00 * point.x + this.m01 * point.y + this.m02 * point.z + this.m03;
			vector.y = this.m10 * point.x + this.m11 * point.y + this.m12 * point.z + this.m13;
			vector.z = this.m20 * point.x + this.m21 * point.y + this.m22 * point.z + this.m23;
			return vector;
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0001D548 File Offset: 0x0001B748
		public Vector3 MultiplyVector(Vector3 vector)
		{
			Vector3 vector2;
			vector2.x = this.m00 * vector.x + this.m01 * vector.y + this.m02 * vector.z;
			vector2.y = this.m10 * vector.x + this.m11 * vector.y + this.m12 * vector.z;
			vector2.z = this.m20 * vector.x + this.m21 * vector.y + this.m22 * vector.z;
			return vector2;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0001D5EC File Offset: 0x0001B7EC
		public Plane TransformPlane(Plane plane)
		{
			Matrix4x4 inverse = this.inverse;
			float x = plane.normal.x;
			float y = plane.normal.y;
			float z = plane.normal.z;
			float distance = plane.distance;
			float num = inverse.m00 * x + inverse.m10 * y + inverse.m20 * z + inverse.m30 * distance;
			float num2 = inverse.m01 * x + inverse.m11 * y + inverse.m21 * z + inverse.m31 * distance;
			float num3 = inverse.m02 * x + inverse.m12 * y + inverse.m22 * z + inverse.m32 * distance;
			float num4 = inverse.m03 * x + inverse.m13 * y + inverse.m23 * z + inverse.m33 * distance;
			return new Plane(new Vector3(num, num2, num3), num4);
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0001D6E4 File Offset: 0x0001B8E4
		public static Matrix4x4 Scale(Vector3 vector)
		{
			Matrix4x4 matrix4x;
			matrix4x.m00 = vector.x;
			matrix4x.m01 = 0f;
			matrix4x.m02 = 0f;
			matrix4x.m03 = 0f;
			matrix4x.m10 = 0f;
			matrix4x.m11 = vector.y;
			matrix4x.m12 = 0f;
			matrix4x.m13 = 0f;
			matrix4x.m20 = 0f;
			matrix4x.m21 = 0f;
			matrix4x.m22 = vector.z;
			matrix4x.m23 = 0f;
			matrix4x.m30 = 0f;
			matrix4x.m31 = 0f;
			matrix4x.m32 = 0f;
			matrix4x.m33 = 1f;
			return matrix4x;
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0001D7BC File Offset: 0x0001B9BC
		public static Matrix4x4 Translate(Vector3 vector)
		{
			Matrix4x4 matrix4x;
			matrix4x.m00 = 1f;
			matrix4x.m01 = 0f;
			matrix4x.m02 = 0f;
			matrix4x.m03 = vector.x;
			matrix4x.m10 = 0f;
			matrix4x.m11 = 1f;
			matrix4x.m12 = 0f;
			matrix4x.m13 = vector.y;
			matrix4x.m20 = 0f;
			matrix4x.m21 = 0f;
			matrix4x.m22 = 1f;
			matrix4x.m23 = vector.z;
			matrix4x.m30 = 0f;
			matrix4x.m31 = 0f;
			matrix4x.m32 = 0f;
			matrix4x.m33 = 1f;
			return matrix4x;
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0001D894 File Offset: 0x0001BA94
		public static Matrix4x4 Rotate(Quaternion q)
		{
			float num = q.x * 2f;
			float num2 = q.y * 2f;
			float num3 = q.z * 2f;
			float num4 = q.x * num;
			float num5 = q.y * num2;
			float num6 = q.z * num3;
			float num7 = q.x * num2;
			float num8 = q.x * num3;
			float num9 = q.y * num3;
			float num10 = q.w * num;
			float num11 = q.w * num2;
			float num12 = q.w * num3;
			Matrix4x4 matrix4x;
			matrix4x.m00 = 1f - (num5 + num6);
			matrix4x.m10 = num7 + num12;
			matrix4x.m20 = num8 - num11;
			matrix4x.m30 = 0f;
			matrix4x.m01 = num7 - num12;
			matrix4x.m11 = 1f - (num4 + num6);
			matrix4x.m21 = num9 + num10;
			matrix4x.m31 = 0f;
			matrix4x.m02 = num8 + num11;
			matrix4x.m12 = num9 - num10;
			matrix4x.m22 = 1f - (num4 + num5);
			matrix4x.m32 = 0f;
			matrix4x.m03 = 0f;
			matrix4x.m13 = 0f;
			matrix4x.m23 = 0f;
			matrix4x.m33 = 1f;
			return matrix4x;
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x0001D9FC File Offset: 0x0001BBFC
		public static Matrix4x4 zero
		{
			get
			{
				return Matrix4x4.zeroMatrix;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x0001DA14 File Offset: 0x0001BC14
		public static Matrix4x4 identity
		{
			[MethodImpl(256)]
			get
			{
				return Matrix4x4.identityMatrix;
			}
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0001DA2C File Offset: 0x0001BC2C
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0001DA48 File Offset: 0x0001BC48
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0001DA64 File Offset: 0x0001BC64
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F5";
			}
			bool flag2 = formatProvider == null;
			if (flag2)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("{0}\t{1}\t{2}\t{3}\n{4}\t{5}\t{6}\t{7}\n{8}\t{9}\t{10}\t{11}\n{12}\t{13}\t{14}\t{15}\n", new object[]
			{
				this.m00.ToString(format, formatProvider),
				this.m01.ToString(format, formatProvider),
				this.m02.ToString(format, formatProvider),
				this.m03.ToString(format, formatProvider),
				this.m10.ToString(format, formatProvider),
				this.m11.ToString(format, formatProvider),
				this.m12.ToString(format, formatProvider),
				this.m13.ToString(format, formatProvider),
				this.m20.ToString(format, formatProvider),
				this.m21.ToString(format, formatProvider),
				this.m22.ToString(format, formatProvider),
				this.m23.ToString(format, formatProvider),
				this.m30.ToString(format, formatProvider),
				this.m31.ToString(format, formatProvider),
				this.m32.ToString(format, formatProvider),
				this.m33.ToString(format, formatProvider)
			});
		}

		// Token: 0x060013F3 RID: 5107
		[MethodImpl(4096)]
		private static extern void GetRotation_Injected(ref Matrix4x4 _unity_self, out Quaternion ret);

		// Token: 0x060013F4 RID: 5108
		[MethodImpl(4096)]
		private static extern void GetLossyScale_Injected(ref Matrix4x4 _unity_self, out Vector3 ret);

		// Token: 0x060013F5 RID: 5109
		[MethodImpl(4096)]
		private static extern bool IsIdentity_Injected(ref Matrix4x4 _unity_self);

		// Token: 0x060013F6 RID: 5110
		[MethodImpl(4096)]
		private static extern float GetDeterminant_Injected(ref Matrix4x4 _unity_self);

		// Token: 0x060013F7 RID: 5111
		[MethodImpl(4096)]
		private static extern void DecomposeProjection_Injected(ref Matrix4x4 _unity_self, out FrustumPlanes ret);

		// Token: 0x060013F8 RID: 5112
		[MethodImpl(4096)]
		private static extern bool ValidTRS_Injected(ref Matrix4x4 _unity_self);

		// Token: 0x060013F9 RID: 5113
		[MethodImpl(4096)]
		private static extern void TRS_Injected(ref Vector3 pos, ref Quaternion q, ref Vector3 s, out Matrix4x4 ret);

		// Token: 0x060013FA RID: 5114
		[MethodImpl(4096)]
		private static extern bool Inverse3DAffine_Injected(ref Matrix4x4 input, ref Matrix4x4 result);

		// Token: 0x060013FB RID: 5115
		[MethodImpl(4096)]
		private static extern void Inverse_Injected(ref Matrix4x4 m, out Matrix4x4 ret);

		// Token: 0x060013FC RID: 5116
		[MethodImpl(4096)]
		private static extern void Transpose_Injected(ref Matrix4x4 m, out Matrix4x4 ret);

		// Token: 0x060013FD RID: 5117
		[MethodImpl(4096)]
		private static extern void Ortho_Injected(float left, float right, float bottom, float top, float zNear, float zFar, out Matrix4x4 ret);

		// Token: 0x060013FE RID: 5118
		[MethodImpl(4096)]
		private static extern void Perspective_Injected(float fov, float aspect, float zNear, float zFar, out Matrix4x4 ret);

		// Token: 0x060013FF RID: 5119
		[MethodImpl(4096)]
		private static extern void LookAt_Injected(ref Vector3 from, ref Vector3 to, ref Vector3 up, out Matrix4x4 ret);

		// Token: 0x06001400 RID: 5120
		[MethodImpl(4096)]
		private static extern void Frustum_Injected(float left, float right, float bottom, float top, float zNear, float zFar, out Matrix4x4 ret);

		// Token: 0x0400074B RID: 1867
		[NativeName("m_Data[0]")]
		public float m00;

		// Token: 0x0400074C RID: 1868
		[NativeName("m_Data[1]")]
		public float m10;

		// Token: 0x0400074D RID: 1869
		[NativeName("m_Data[2]")]
		public float m20;

		// Token: 0x0400074E RID: 1870
		[NativeName("m_Data[3]")]
		public float m30;

		// Token: 0x0400074F RID: 1871
		[NativeName("m_Data[4]")]
		public float m01;

		// Token: 0x04000750 RID: 1872
		[NativeName("m_Data[5]")]
		public float m11;

		// Token: 0x04000751 RID: 1873
		[NativeName("m_Data[6]")]
		public float m21;

		// Token: 0x04000752 RID: 1874
		[NativeName("m_Data[7]")]
		public float m31;

		// Token: 0x04000753 RID: 1875
		[NativeName("m_Data[8]")]
		public float m02;

		// Token: 0x04000754 RID: 1876
		[NativeName("m_Data[9]")]
		public float m12;

		// Token: 0x04000755 RID: 1877
		[NativeName("m_Data[10]")]
		public float m22;

		// Token: 0x04000756 RID: 1878
		[NativeName("m_Data[11]")]
		public float m32;

		// Token: 0x04000757 RID: 1879
		[NativeName("m_Data[12]")]
		public float m03;

		// Token: 0x04000758 RID: 1880
		[NativeName("m_Data[13]")]
		public float m13;

		// Token: 0x04000759 RID: 1881
		[NativeName("m_Data[14]")]
		public float m23;

		// Token: 0x0400075A RID: 1882
		[NativeName("m_Data[15]")]
		public float m33;

		// Token: 0x0400075B RID: 1883
		private static readonly Matrix4x4 zeroMatrix = new Matrix4x4(new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f));

		// Token: 0x0400075C RID: 1884
		private static readonly Matrix4x4 identityMatrix = new Matrix4x4(new Vector4(1f, 0f, 0f, 0f), new Vector4(0f, 1f, 0f, 0f), new Vector4(0f, 0f, 1f, 0f), new Vector4(0f, 0f, 0f, 1f));
	}
}
