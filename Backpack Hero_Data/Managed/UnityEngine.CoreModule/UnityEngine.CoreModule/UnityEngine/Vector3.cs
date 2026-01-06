using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001C5 RID: 453
	[NativeType(Header = "Runtime/Math/Vector3.h")]
	[NativeHeader("Runtime/Math/MathScripting.h")]
	[NativeHeader("Runtime/Math/Vector3.h")]
	[NativeClass("Vector3f")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[Il2CppEagerStaticClassConstruction]
	public struct Vector3 : IEquatable<Vector3>, IFormattable
	{
		// Token: 0x06001401 RID: 5121 RVA: 0x0001DCA0 File Offset: 0x0001BEA0
		[FreeFunction("VectorScripting::Slerp", IsThreadSafe = true)]
		public static Vector3 Slerp(Vector3 a, Vector3 b, float t)
		{
			Vector3 vector;
			Vector3.Slerp_Injected(ref a, ref b, t, out vector);
			return vector;
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0001DCBC File Offset: 0x0001BEBC
		[FreeFunction("VectorScripting::SlerpUnclamped", IsThreadSafe = true)]
		public static Vector3 SlerpUnclamped(Vector3 a, Vector3 b, float t)
		{
			Vector3 vector;
			Vector3.SlerpUnclamped_Injected(ref a, ref b, t, out vector);
			return vector;
		}

		// Token: 0x06001403 RID: 5123
		[FreeFunction("VectorScripting::OrthoNormalize", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void OrthoNormalize2(ref Vector3 a, ref Vector3 b);

		// Token: 0x06001404 RID: 5124 RVA: 0x0001DCD6 File Offset: 0x0001BED6
		public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent)
		{
			Vector3.OrthoNormalize2(ref normal, ref tangent);
		}

		// Token: 0x06001405 RID: 5125
		[FreeFunction("VectorScripting::OrthoNormalize", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void OrthoNormalize3(ref Vector3 a, ref Vector3 b, ref Vector3 c);

		// Token: 0x06001406 RID: 5126 RVA: 0x0001DCE1 File Offset: 0x0001BEE1
		public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent, ref Vector3 binormal)
		{
			Vector3.OrthoNormalize3(ref normal, ref tangent, ref binormal);
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0001DCF0 File Offset: 0x0001BEF0
		[FreeFunction(IsThreadSafe = true)]
		public static Vector3 RotateTowards(Vector3 current, Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta)
		{
			Vector3 vector;
			Vector3.RotateTowards_Injected(ref current, ref target, maxRadiansDelta, maxMagnitudeDelta, out vector);
			return vector;
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0001DD0C File Offset: 0x0001BF0C
		[MethodImpl(256)]
		public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0001DD70 File Offset: 0x0001BF70
		[MethodImpl(256)]
		public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
		{
			return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0001DDCC File Offset: 0x0001BFCC
		[MethodImpl(256)]
		public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
		{
			float num = target.x - current.x;
			float num2 = target.y - current.y;
			float num3 = target.z - current.z;
			float num4 = num * num + num2 * num2 + num3 * num3;
			bool flag = num4 == 0f || (maxDistanceDelta >= 0f && num4 <= maxDistanceDelta * maxDistanceDelta);
			Vector3 vector;
			if (flag)
			{
				vector = target;
			}
			else
			{
				float num5 = (float)Math.Sqrt((double)num4);
				vector = new Vector3(current.x + num / num5 * maxDistanceDelta, current.y + num2 / num5 * maxDistanceDelta, current.z + num3 / num5 * maxDistanceDelta);
			}
			return vector;
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0001DE78 File Offset: 0x0001C078
		[ExcludeFromDocs]
		[MethodImpl(256)]
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0001DE9C File Offset: 0x0001C09C
		[ExcludeFromDocs]
		[MethodImpl(256)]
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0001DEC8 File Offset: 0x0001C0C8
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			float num4 = current.x - target.x;
			float num5 = current.y - target.y;
			float num6 = current.z - target.z;
			Vector3 vector = target;
			float num7 = maxSpeed * smoothTime;
			float num8 = num7 * num7;
			float num9 = num4 * num4 + num5 * num5 + num6 * num6;
			bool flag = num9 > num8;
			if (flag)
			{
				float num10 = (float)Math.Sqrt((double)num9);
				num4 = num4 / num10 * num7;
				num5 = num5 / num10 * num7;
				num6 = num6 / num10 * num7;
			}
			target.x = current.x - num4;
			target.y = current.y - num5;
			target.z = current.z - num6;
			float num11 = (currentVelocity.x + num * num4) * deltaTime;
			float num12 = (currentVelocity.y + num * num5) * deltaTime;
			float num13 = (currentVelocity.z + num * num6) * deltaTime;
			currentVelocity.x = (currentVelocity.x - num * num11) * num3;
			currentVelocity.y = (currentVelocity.y - num * num12) * num3;
			currentVelocity.z = (currentVelocity.z - num * num13) * num3;
			float num14 = target.x + (num4 + num11) * num3;
			float num15 = target.y + (num5 + num12) * num3;
			float num16 = target.z + (num6 + num13) * num3;
			float num17 = vector.x - current.x;
			float num18 = vector.y - current.y;
			float num19 = vector.z - current.z;
			float num20 = num14 - vector.x;
			float num21 = num15 - vector.y;
			float num22 = num16 - vector.z;
			bool flag2 = num17 * num20 + num18 * num21 + num19 * num22 > 0f;
			if (flag2)
			{
				num14 = vector.x;
				num15 = vector.y;
				num16 = vector.z;
				currentVelocity.x = (num14 - vector.x) / deltaTime;
				currentVelocity.y = (num15 - vector.y) / deltaTime;
				currentVelocity.z = (num16 - vector.z) / deltaTime;
			}
			return new Vector3(num14, num15, num16);
		}

		// Token: 0x17000411 RID: 1041
		public float this[int index]
		{
			[MethodImpl(256)]
			get
			{
				float num;
				switch (index)
				{
				case 0:
					num = this.x;
					break;
				case 1:
					num = this.y;
					break;
				case 2:
					num = this.z;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
				return num;
			}
			[MethodImpl(256)]
			set
			{
				switch (index)
				{
				case 0:
					this.x = value;
					break;
				case 1:
					this.y = value;
					break;
				case 2:
					this.z = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0001E1EC File Offset: 0x0001C3EC
		[MethodImpl(256)]
		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0001E204 File Offset: 0x0001C404
		[MethodImpl(256)]
		public Vector3(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0001E1EC File Offset: 0x0001C3EC
		[MethodImpl(256)]
		public void Set(float newX, float newY, float newZ)
		{
			this.x = newX;
			this.y = newY;
			this.z = newZ;
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0001E220 File Offset: 0x0001C420
		[MethodImpl(256)]
		public static Vector3 Scale(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0001E25E File Offset: 0x0001C45E
		[MethodImpl(256)]
		public void Scale(Vector3 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0001E29C File Offset: 0x0001C49C
		[MethodImpl(256)]
		public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x0001E304 File Offset: 0x0001C504
		[MethodImpl(256)]
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2) ^ (this.z.GetHashCode() >> 2);
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0001E340 File Offset: 0x0001C540
		[MethodImpl(256)]
		public override bool Equals(object other)
		{
			bool flag = !(other is Vector3);
			return !flag && this.Equals((Vector3)other);
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0001E374 File Offset: 0x0001C574
		[MethodImpl(256)]
		public bool Equals(Vector3 other)
		{
			return this.x == other.x && this.y == other.y && this.z == other.z;
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0001E3B4 File Offset: 0x0001C5B4
		[MethodImpl(256)]
		public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
		{
			float num = -2f * Vector3.Dot(inNormal, inDirection);
			return new Vector3(num * inNormal.x + inDirection.x, num * inNormal.y + inDirection.y, num * inNormal.z + inDirection.z);
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0001E408 File Offset: 0x0001C608
		[MethodImpl(256)]
		public static Vector3 Normalize(Vector3 value)
		{
			float num = Vector3.Magnitude(value);
			bool flag = num > 1E-05f;
			Vector3 vector;
			if (flag)
			{
				vector = value / num;
			}
			else
			{
				vector = Vector3.zero;
			}
			return vector;
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0001E43C File Offset: 0x0001C63C
		[MethodImpl(256)]
		public void Normalize()
		{
			float num = Vector3.Magnitude(this);
			bool flag = num > 1E-05f;
			if (flag)
			{
				this /= num;
			}
			else
			{
				this = Vector3.zero;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x0001E484 File Offset: 0x0001C684
		public Vector3 normalized
		{
			[MethodImpl(256)]
			get
			{
				return Vector3.Normalize(this);
			}
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0001E4A4 File Offset: 0x0001C6A4
		[MethodImpl(256)]
		public static float Dot(Vector3 lhs, Vector3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0001E4E0 File Offset: 0x0001C6E0
		[MethodImpl(256)]
		public static Vector3 Project(Vector3 vector, Vector3 onNormal)
		{
			float num = Vector3.Dot(onNormal, onNormal);
			bool flag = num < Mathf.Epsilon;
			Vector3 vector2;
			if (flag)
			{
				vector2 = Vector3.zero;
			}
			else
			{
				float num2 = Vector3.Dot(vector, onNormal);
				vector2 = new Vector3(onNormal.x * num2 / num, onNormal.y * num2 / num, onNormal.z * num2 / num);
			}
			return vector2;
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0001E53C File Offset: 0x0001C73C
		[MethodImpl(256)]
		public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
		{
			float num = Vector3.Dot(planeNormal, planeNormal);
			bool flag = num < Mathf.Epsilon;
			Vector3 vector2;
			if (flag)
			{
				vector2 = vector;
			}
			else
			{
				float num2 = Vector3.Dot(vector, planeNormal);
				vector2 = new Vector3(vector.x - planeNormal.x * num2 / num, vector.y - planeNormal.y * num2 / num, vector.z - planeNormal.z * num2 / num);
			}
			return vector2;
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0001E5A8 File Offset: 0x0001C7A8
		[MethodImpl(256)]
		public static float Angle(Vector3 from, Vector3 to)
		{
			float num = (float)Math.Sqrt((double)(from.sqrMagnitude * to.sqrMagnitude));
			bool flag = num < 1E-15f;
			float num2;
			if (flag)
			{
				num2 = 0f;
			}
			else
			{
				float num3 = Mathf.Clamp(Vector3.Dot(from, to) / num, -1f, 1f);
				num2 = (float)Math.Acos((double)num3) * 57.29578f;
			}
			return num2;
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0001E60C File Offset: 0x0001C80C
		[MethodImpl(256)]
		public static float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)
		{
			float num = Vector3.Angle(from, to);
			float num2 = from.y * to.z - from.z * to.y;
			float num3 = from.z * to.x - from.x * to.z;
			float num4 = from.x * to.y - from.y * to.x;
			float num5 = Mathf.Sign(axis.x * num2 + axis.y * num3 + axis.z * num4);
			return num * num5;
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0001E6A4 File Offset: 0x0001C8A4
		[MethodImpl(256)]
		public static float Distance(Vector3 a, Vector3 b)
		{
			float num = a.x - b.x;
			float num2 = a.y - b.y;
			float num3 = a.z - b.z;
			return (float)Math.Sqrt((double)(num * num + num2 * num2 + num3 * num3));
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0001E6F4 File Offset: 0x0001C8F4
		[MethodImpl(256)]
		public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
		{
			float sqrMagnitude = vector.sqrMagnitude;
			bool flag = sqrMagnitude > maxLength * maxLength;
			Vector3 vector2;
			if (flag)
			{
				float num = (float)Math.Sqrt((double)sqrMagnitude);
				float num2 = vector.x / num;
				float num3 = vector.y / num;
				float num4 = vector.z / num;
				vector2 = new Vector3(num2 * maxLength, num3 * maxLength, num4 * maxLength);
			}
			else
			{
				vector2 = vector;
			}
			return vector2;
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0001E758 File Offset: 0x0001C958
		[MethodImpl(256)]
		public static float Magnitude(Vector3 vector)
		{
			return (float)Math.Sqrt((double)(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z));
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x0001E79C File Offset: 0x0001C99C
		public float magnitude
		{
			[MethodImpl(256)]
			get
			{
				return (float)Math.Sqrt((double)(this.x * this.x + this.y * this.y + this.z * this.z));
			}
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0001E7E0 File Offset: 0x0001C9E0
		[MethodImpl(256)]
		public static float SqrMagnitude(Vector3 vector)
		{
			return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x0001E81C File Offset: 0x0001CA1C
		public float sqrMagnitude
		{
			[MethodImpl(256)]
			get
			{
				return this.x * this.x + this.y * this.y + this.z * this.z;
			}
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0001E858 File Offset: 0x0001CA58
		[MethodImpl(256)]
		public static Vector3 Min(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0001E8A4 File Offset: 0x0001CAA4
		[MethodImpl(256)]
		public static Vector3 Max(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x0001E8F0 File Offset: 0x0001CAF0
		public static Vector3 zero
		{
			[MethodImpl(256)]
			get
			{
				return Vector3.zeroVector;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x0001E908 File Offset: 0x0001CB08
		public static Vector3 one
		{
			[MethodImpl(256)]
			get
			{
				return Vector3.oneVector;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x0001E920 File Offset: 0x0001CB20
		public static Vector3 forward
		{
			[MethodImpl(256)]
			get
			{
				return Vector3.forwardVector;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x0001E938 File Offset: 0x0001CB38
		public static Vector3 back
		{
			[MethodImpl(256)]
			get
			{
				return Vector3.backVector;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x0001E950 File Offset: 0x0001CB50
		public static Vector3 up
		{
			[MethodImpl(256)]
			get
			{
				return Vector3.upVector;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x0001E968 File Offset: 0x0001CB68
		public static Vector3 down
		{
			[MethodImpl(256)]
			get
			{
				return Vector3.downVector;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x0001E980 File Offset: 0x0001CB80
		public static Vector3 left
		{
			[MethodImpl(256)]
			get
			{
				return Vector3.leftVector;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x0001E998 File Offset: 0x0001CB98
		public static Vector3 right
		{
			[MethodImpl(256)]
			get
			{
				return Vector3.rightVector;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x0001E9B0 File Offset: 0x0001CBB0
		public static Vector3 positiveInfinity
		{
			[MethodImpl(256)]
			get
			{
				return Vector3.positiveInfinityVector;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0001E9C8 File Offset: 0x0001CBC8
		public static Vector3 negativeInfinity
		{
			[MethodImpl(256)]
			get
			{
				return Vector3.negativeInfinityVector;
			}
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0001E9E0 File Offset: 0x0001CBE0
		[MethodImpl(256)]
		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0001EA20 File Offset: 0x0001CC20
		[MethodImpl(256)]
		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0001EA60 File Offset: 0x0001CC60
		[MethodImpl(256)]
		public static Vector3 operator -(Vector3 a)
		{
			return new Vector3(-a.x, -a.y, -a.z);
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0001EA8C File Offset: 0x0001CC8C
		[MethodImpl(256)]
		public static Vector3 operator *(Vector3 a, float d)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0001EABC File Offset: 0x0001CCBC
		[MethodImpl(256)]
		public static Vector3 operator *(float d, Vector3 a)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0001EAEC File Offset: 0x0001CCEC
		[MethodImpl(256)]
		public static Vector3 operator /(Vector3 a, float d)
		{
			return new Vector3(a.x / d, a.y / d, a.z / d);
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0001EB1C File Offset: 0x0001CD1C
		[MethodImpl(256)]
		public static bool operator ==(Vector3 lhs, Vector3 rhs)
		{
			float num = lhs.x - rhs.x;
			float num2 = lhs.y - rhs.y;
			float num3 = lhs.z - rhs.z;
			float num4 = num * num + num2 * num2 + num3 * num3;
			return num4 < 9.9999994E-11f;
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0001EB70 File Offset: 0x0001CD70
		[MethodImpl(256)]
		public static bool operator !=(Vector3 lhs, Vector3 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0001EB8C File Offset: 0x0001CD8C
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0001EBA8 File Offset: 0x0001CDA8
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0001EBC4 File Offset: 0x0001CDC4
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F2";
			}
			bool flag2 = formatProvider == null;
			if (flag2)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("({0}, {1}, {2})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x0001EC3C File Offset: 0x0001CE3C
		[Obsolete("Use Vector3.forward instead.")]
		public static Vector3 fwd
		{
			get
			{
				return new Vector3(0f, 0f, 1f);
			}
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0001EC64 File Offset: 0x0001CE64
		[Obsolete("Use Vector3.Angle instead. AngleBetween uses radians instead of degrees and was deprecated for this reason")]
		public static float AngleBetween(Vector3 from, Vector3 to)
		{
			return (float)Math.Acos((double)Mathf.Clamp(Vector3.Dot(from.normalized, to.normalized), -1f, 1f));
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0001ECA0 File Offset: 0x0001CEA0
		[Obsolete("Use Vector3.ProjectOnPlane instead.")]
		public static Vector3 Exclude(Vector3 excludeThis, Vector3 fromThat)
		{
			return Vector3.ProjectOnPlane(fromThat, excludeThis);
		}

		// Token: 0x06001443 RID: 5187
		[MethodImpl(4096)]
		private static extern void Slerp_Injected(ref Vector3 a, ref Vector3 b, float t, out Vector3 ret);

		// Token: 0x06001444 RID: 5188
		[MethodImpl(4096)]
		private static extern void SlerpUnclamped_Injected(ref Vector3 a, ref Vector3 b, float t, out Vector3 ret);

		// Token: 0x06001445 RID: 5189
		[MethodImpl(4096)]
		private static extern void RotateTowards_Injected(ref Vector3 current, ref Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta, out Vector3 ret);

		// Token: 0x0400075D RID: 1885
		public const float kEpsilon = 1E-05f;

		// Token: 0x0400075E RID: 1886
		public const float kEpsilonNormalSqrt = 1E-15f;

		// Token: 0x0400075F RID: 1887
		public float x;

		// Token: 0x04000760 RID: 1888
		public float y;

		// Token: 0x04000761 RID: 1889
		public float z;

		// Token: 0x04000762 RID: 1890
		private static readonly Vector3 zeroVector = new Vector3(0f, 0f, 0f);

		// Token: 0x04000763 RID: 1891
		private static readonly Vector3 oneVector = new Vector3(1f, 1f, 1f);

		// Token: 0x04000764 RID: 1892
		private static readonly Vector3 upVector = new Vector3(0f, 1f, 0f);

		// Token: 0x04000765 RID: 1893
		private static readonly Vector3 downVector = new Vector3(0f, -1f, 0f);

		// Token: 0x04000766 RID: 1894
		private static readonly Vector3 leftVector = new Vector3(-1f, 0f, 0f);

		// Token: 0x04000767 RID: 1895
		private static readonly Vector3 rightVector = new Vector3(1f, 0f, 0f);

		// Token: 0x04000768 RID: 1896
		private static readonly Vector3 forwardVector = new Vector3(0f, 0f, 1f);

		// Token: 0x04000769 RID: 1897
		private static readonly Vector3 backVector = new Vector3(0f, 0f, -1f);

		// Token: 0x0400076A RID: 1898
		private static readonly Vector3 positiveInfinityVector = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

		// Token: 0x0400076B RID: 1899
		private static readonly Vector3 negativeInfinityVector = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
	}
}
