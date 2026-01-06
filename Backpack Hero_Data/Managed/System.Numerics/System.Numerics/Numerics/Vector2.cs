using System;
using System.Globalization;
using System.Numerics.Hashing;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Numerics
{
	// Token: 0x0200000A RID: 10
	public struct Vector2 : IEquatable<Vector2>, IFormattable
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00008CB0 File Offset: 0x00006EB0
		public static Vector2 Zero
		{
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00008CC6 File Offset: 0x00006EC6
		public static Vector2 One
		{
			get
			{
				return new Vector2(1f, 1f);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00008CD7 File Offset: 0x00006ED7
		public static Vector2 UnitX
		{
			get
			{
				return new Vector2(1f, 0f);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00008CE8 File Offset: 0x00006EE8
		public static Vector2 UnitY
		{
			get
			{
				return new Vector2(0f, 1f);
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00008CF9 File Offset: 0x00006EF9
		public override int GetHashCode()
		{
			return HashHelpers.Combine(this.X.GetHashCode(), this.Y.GetHashCode());
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00008D16 File Offset: 0x00006F16
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object obj)
		{
			return obj is Vector2 && this.Equals((Vector2)obj);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00008D2E File Offset: 0x00006F2E
		public override string ToString()
		{
			return this.ToString("G", CultureInfo.CurrentCulture);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00008D40 File Offset: 0x00006F40
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.CurrentCulture);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00008D50 File Offset: 0x00006F50
		public string ToString(string format, IFormatProvider formatProvider)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string numberGroupSeparator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
			stringBuilder.Append('<');
			stringBuilder.Append(this.X.ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(this.Y.ToString(format, formatProvider));
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00008DBE File Offset: 0x00006FBE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float Length()
		{
			if (Vector.IsHardwareAccelerated)
			{
				return MathF.Sqrt(Vector2.Dot(this, this));
			}
			return MathF.Sqrt(this.X * this.X + this.Y * this.Y);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00008DFE File Offset: 0x00006FFE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float LengthSquared()
		{
			if (Vector.IsHardwareAccelerated)
			{
				return Vector2.Dot(this, this);
			}
			return this.X * this.X + this.Y * this.Y;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00008E34 File Offset: 0x00007034
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector2 value1, Vector2 value2)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector2 vector = value1 - value2;
				return MathF.Sqrt(Vector2.Dot(vector, vector));
			}
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			return MathF.Sqrt(num * num + num2 * num2);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00008E84 File Offset: 0x00007084
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceSquared(Vector2 value1, Vector2 value2)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector2 vector = value1 - value2;
				return Vector2.Dot(vector, vector);
			}
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			return num * num + num2 * num2;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00008EC8 File Offset: 0x000070C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Normalize(Vector2 value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				float num = value.Length();
				return value / num;
			}
			float num2 = value.X * value.X + value.Y * value.Y;
			float num3 = 1f / MathF.Sqrt(num2);
			return new Vector2(value.X * num3, value.Y * num3);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00008F2C File Offset: 0x0000712C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Reflect(Vector2 vector, Vector2 normal)
		{
			if (Vector.IsHardwareAccelerated)
			{
				float num = Vector2.Dot(vector, normal);
				return vector - 2f * num * normal;
			}
			float num2 = vector.X * normal.X + vector.Y * normal.Y;
			return new Vector2(vector.X - 2f * num2 * normal.X, vector.Y - 2f * num2 * normal.Y);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00008FA8 File Offset: 0x000071A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Clamp(Vector2 value1, Vector2 min, Vector2 max)
		{
			float num = value1.X;
			num = ((num > max.X) ? max.X : num);
			num = ((num < min.X) ? min.X : num);
			float num2 = value1.Y;
			num2 = ((num2 > max.Y) ? max.Y : num2);
			num2 = ((num2 < min.Y) ? min.Y : num2);
			return new Vector2(num, num2);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00009016 File Offset: 0x00007216
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Lerp(Vector2 value1, Vector2 value2, float amount)
		{
			return new Vector2(value1.X + (value2.X - value1.X) * amount, value1.Y + (value2.Y - value1.Y) * amount);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000904C File Offset: 0x0000724C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Transform(Vector2 position, Matrix3x2 matrix)
		{
			return new Vector2(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M31, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M32);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000090A4 File Offset: 0x000072A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Transform(Vector2 position, Matrix4x4 matrix)
		{
			return new Vector2(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000090FA File Offset: 0x000072FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 TransformNormal(Vector2 normal, Matrix3x2 matrix)
		{
			return new Vector2(normal.X * matrix.M11 + normal.Y * matrix.M21, normal.X * matrix.M12 + normal.Y * matrix.M22);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00009137 File Offset: 0x00007337
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 TransformNormal(Vector2 normal, Matrix4x4 matrix)
		{
			return new Vector2(normal.X * matrix.M11 + normal.Y * matrix.M21, normal.X * matrix.M12 + normal.Y * matrix.M22);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00009174 File Offset: 0x00007374
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Transform(Vector2 value, Quaternion rotation)
		{
			float num = rotation.X + rotation.X;
			float num2 = rotation.Y + rotation.Y;
			float num3 = rotation.Z + rotation.Z;
			float num4 = rotation.W * num3;
			float num5 = rotation.X * num;
			float num6 = rotation.X * num2;
			float num7 = rotation.Y * num2;
			float num8 = rotation.Z * num3;
			return new Vector2(value.X * (1f - num7 - num8) + value.Y * (num6 - num4), value.X * (num6 + num4) + value.Y * (1f - num5 - num8));
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000921D File Offset: 0x0000741D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Add(Vector2 left, Vector2 right)
		{
			return left + right;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00009226 File Offset: 0x00007426
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Subtract(Vector2 left, Vector2 right)
		{
			return left - right;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000922F File Offset: 0x0000742F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Multiply(Vector2 left, Vector2 right)
		{
			return left * right;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00009238 File Offset: 0x00007438
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Multiply(Vector2 left, float right)
		{
			return left * right;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00009241 File Offset: 0x00007441
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Multiply(float left, Vector2 right)
		{
			return left * right;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000924A File Offset: 0x0000744A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Divide(Vector2 left, Vector2 right)
		{
			return left / right;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00009253 File Offset: 0x00007453
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Divide(Vector2 left, float divisor)
		{
			return left / divisor;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000925C File Offset: 0x0000745C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Negate(Vector2 value)
		{
			return -value;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00009264 File Offset: 0x00007464
		[Intrinsic]
		public Vector2(float value)
		{
			this = new Vector2(value, value);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000926E File Offset: 0x0000746E
		[Intrinsic]
		public Vector2(float x, float y)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000927E File Offset: 0x0000747E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void CopyTo(float[] array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00009288 File Offset: 0x00007488
		public void CopyTo(float[] array, int index)
		{
			if (array == null)
			{
				throw new NullReferenceException("The method was called with a null array argument.");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", SR.Format("Index was out of bounds:", index));
			}
			if (array.Length - index < 2)
			{
				throw new ArgumentException(SR.Format("Number of elements in source vector is greater than the destination array", index));
			}
			array[index] = this.X;
			array[index + 1] = this.Y;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000092FA File Offset: 0x000074FA
		[Intrinsic]
		public bool Equals(Vector2 other)
		{
			return this.X == other.X && this.Y == other.Y;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000931A File Offset: 0x0000751A
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Vector2 value1, Vector2 value2)
		{
			return value1.X * value2.X + value1.Y * value2.Y;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00009337 File Offset: 0x00007537
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Min(Vector2 value1, Vector2 value2)
		{
			return new Vector2((value1.X < value2.X) ? value1.X : value2.X, (value1.Y < value2.Y) ? value1.Y : value2.Y);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00009376 File Offset: 0x00007576
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Max(Vector2 value1, Vector2 value2)
		{
			return new Vector2((value1.X > value2.X) ? value1.X : value2.X, (value1.Y > value2.Y) ? value1.Y : value2.Y);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000093B5 File Offset: 0x000075B5
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Abs(Vector2 value)
		{
			return new Vector2(MathF.Abs(value.X), MathF.Abs(value.Y));
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000093D2 File Offset: 0x000075D2
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 SquareRoot(Vector2 value)
		{
			return new Vector2(MathF.Sqrt(value.X), MathF.Sqrt(value.Y));
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000093EF File Offset: 0x000075EF
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator +(Vector2 left, Vector2 right)
		{
			return new Vector2(left.X + right.X, left.Y + right.Y);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00009410 File Offset: 0x00007610
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator -(Vector2 left, Vector2 right)
		{
			return new Vector2(left.X - right.X, left.Y - right.Y);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00009431 File Offset: 0x00007631
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator *(Vector2 left, Vector2 right)
		{
			return new Vector2(left.X * right.X, left.Y * right.Y);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00009452 File Offset: 0x00007652
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator *(float left, Vector2 right)
		{
			return new Vector2(left, left) * right;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00009461 File Offset: 0x00007661
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator *(Vector2 left, float right)
		{
			return left * new Vector2(right, right);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00009470 File Offset: 0x00007670
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator /(Vector2 left, Vector2 right)
		{
			return new Vector2(left.X / right.X, left.Y / right.Y);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00009491 File Offset: 0x00007691
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator /(Vector2 value1, float value2)
		{
			return value1 / new Vector2(value2);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000949F File Offset: 0x0000769F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator -(Vector2 value)
		{
			return Vector2.Zero - value;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000094AC File Offset: 0x000076AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector2 left, Vector2 right)
		{
			return left.Equals(right);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000094B6 File Offset: 0x000076B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector2 left, Vector2 right)
		{
			return !(left == right);
		}

		// Token: 0x04000062 RID: 98
		public float X;

		// Token: 0x04000063 RID: 99
		public float Y;
	}
}
