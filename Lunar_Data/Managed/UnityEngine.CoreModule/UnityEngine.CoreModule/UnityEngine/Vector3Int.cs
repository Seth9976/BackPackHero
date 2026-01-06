using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001CB RID: 459
	[Il2CppEagerStaticClassConstruction]
	[UsedByNativeCode]
	public struct Vector3Int : IEquatable<Vector3Int>, IFormattable
	{
		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x00021780 File Offset: 0x0001F980
		// (set) Token: 0x0600152E RID: 5422 RVA: 0x00021798 File Offset: 0x0001F998
		public int x
		{
			[MethodImpl(256)]
			get
			{
				return this.m_X;
			}
			[MethodImpl(256)]
			set
			{
				this.m_X = value;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x000217A4 File Offset: 0x0001F9A4
		// (set) Token: 0x06001530 RID: 5424 RVA: 0x000217BC File Offset: 0x0001F9BC
		public int y
		{
			[MethodImpl(256)]
			get
			{
				return this.m_Y;
			}
			[MethodImpl(256)]
			set
			{
				this.m_Y = value;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x000217C8 File Offset: 0x0001F9C8
		// (set) Token: 0x06001532 RID: 5426 RVA: 0x000217E0 File Offset: 0x0001F9E0
		public int z
		{
			[MethodImpl(256)]
			get
			{
				return this.m_Z;
			}
			[MethodImpl(256)]
			set
			{
				this.m_Z = value;
			}
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x000217EA File Offset: 0x0001F9EA
		[MethodImpl(256)]
		public Vector3Int(int x, int y)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = 0;
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00021802 File Offset: 0x0001FA02
		[MethodImpl(256)]
		public Vector3Int(int x, int y, int z)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = z;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x00021802 File Offset: 0x0001FA02
		[MethodImpl(256)]
		public void Set(int x, int y, int z)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = z;
		}

		// Token: 0x1700043E RID: 1086
		public int this[int index]
		{
			[MethodImpl(256)]
			get
			{
				int num;
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
					throw new IndexOutOfRangeException(UnityString.Format("Invalid Vector3Int index addressed: {0}!", new object[] { index }));
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
					throw new IndexOutOfRangeException(UnityString.Format("Invalid Vector3Int index addressed: {0}!", new object[] { index }));
				}
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x000218E4 File Offset: 0x0001FAE4
		public float magnitude
		{
			[MethodImpl(256)]
			get
			{
				return Mathf.Sqrt((float)(this.x * this.x + this.y * this.y + this.z * this.z));
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x00021928 File Offset: 0x0001FB28
		public int sqrMagnitude
		{
			[MethodImpl(256)]
			get
			{
				return this.x * this.x + this.y * this.y + this.z * this.z;
			}
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x00021964 File Offset: 0x0001FB64
		[MethodImpl(256)]
		public static float Distance(Vector3Int a, Vector3Int b)
		{
			return (a - b).magnitude;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00021988 File Offset: 0x0001FB88
		[MethodImpl(256)]
		public static Vector3Int Min(Vector3Int lhs, Vector3Int rhs)
		{
			return new Vector3Int(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x000219D8 File Offset: 0x0001FBD8
		[MethodImpl(256)]
		public static Vector3Int Max(Vector3Int lhs, Vector3Int rhs)
		{
			return new Vector3Int(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x00021A28 File Offset: 0x0001FC28
		[MethodImpl(256)]
		public static Vector3Int Scale(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x00021A6C File Offset: 0x0001FC6C
		[MethodImpl(256)]
		public void Scale(Vector3Int scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x00021ABC File Offset: 0x0001FCBC
		[MethodImpl(256)]
		public void Clamp(Vector3Int min, Vector3Int max)
		{
			this.x = Math.Max(min.x, this.x);
			this.x = Math.Min(max.x, this.x);
			this.y = Math.Max(min.y, this.y);
			this.y = Math.Min(max.y, this.y);
			this.z = Math.Max(min.z, this.z);
			this.z = Math.Min(max.z, this.z);
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x00021B60 File Offset: 0x0001FD60
		[MethodImpl(256)]
		public static implicit operator Vector3(Vector3Int v)
		{
			return new Vector3((float)v.x, (float)v.y, (float)v.z);
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x00021B90 File Offset: 0x0001FD90
		[MethodImpl(256)]
		public static explicit operator Vector2Int(Vector3Int v)
		{
			return new Vector2Int(v.x, v.y);
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x00021BB8 File Offset: 0x0001FDB8
		[MethodImpl(256)]
		public static Vector3Int FloorToInt(Vector3 v)
		{
			return new Vector3Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y), Mathf.FloorToInt(v.z));
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00021BF0 File Offset: 0x0001FDF0
		[MethodImpl(256)]
		public static Vector3Int CeilToInt(Vector3 v)
		{
			return new Vector3Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y), Mathf.CeilToInt(v.z));
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00021C28 File Offset: 0x0001FE28
		[MethodImpl(256)]
		public static Vector3Int RoundToInt(Vector3 v)
		{
			return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x00021C60 File Offset: 0x0001FE60
		[MethodImpl(256)]
		public static Vector3Int operator +(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x00021CA4 File Offset: 0x0001FEA4
		[MethodImpl(256)]
		public static Vector3Int operator -(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x00021CE8 File Offset: 0x0001FEE8
		[MethodImpl(256)]
		public static Vector3Int operator *(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x00021D2C File Offset: 0x0001FF2C
		[MethodImpl(256)]
		public static Vector3Int operator -(Vector3Int a)
		{
			return new Vector3Int(-a.x, -a.y, -a.z);
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x00021D5C File Offset: 0x0001FF5C
		[MethodImpl(256)]
		public static Vector3Int operator *(Vector3Int a, int b)
		{
			return new Vector3Int(a.x * b, a.y * b, a.z * b);
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x00021D90 File Offset: 0x0001FF90
		[MethodImpl(256)]
		public static Vector3Int operator *(int a, Vector3Int b)
		{
			return new Vector3Int(a * b.x, a * b.y, a * b.z);
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x00021DC4 File Offset: 0x0001FFC4
		[MethodImpl(256)]
		public static Vector3Int operator /(Vector3Int a, int b)
		{
			return new Vector3Int(a.x / b, a.y / b, a.z / b);
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x00021DF8 File Offset: 0x0001FFF8
		[MethodImpl(256)]
		public static bool operator ==(Vector3Int lhs, Vector3Int rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x00021E40 File Offset: 0x00020040
		[MethodImpl(256)]
		public static bool operator !=(Vector3Int lhs, Vector3Int rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x00021E5C File Offset: 0x0002005C
		[MethodImpl(256)]
		public override bool Equals(object other)
		{
			bool flag = !(other is Vector3Int);
			return !flag && this.Equals((Vector3Int)other);
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x00021E90 File Offset: 0x00020090
		[MethodImpl(256)]
		public bool Equals(Vector3Int other)
		{
			return this == other;
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x00021EB0 File Offset: 0x000200B0
		[MethodImpl(256)]
		public override int GetHashCode()
		{
			int hashCode = this.y.GetHashCode();
			int hashCode2 = this.z.GetHashCode();
			return this.x.GetHashCode() ^ (hashCode << 4) ^ (hashCode >> 28) ^ (hashCode2 >> 4) ^ (hashCode2 << 28);
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x00021F00 File Offset: 0x00020100
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x00021F1C File Offset: 0x0002011C
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x00021F38 File Offset: 0x00020138
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = formatProvider == null;
			if (flag)
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

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x00021FA8 File Offset: 0x000201A8
		public static Vector3Int zero
		{
			[MethodImpl(256)]
			get
			{
				return Vector3Int.s_Zero;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x00021FC0 File Offset: 0x000201C0
		public static Vector3Int one
		{
			[MethodImpl(256)]
			get
			{
				return Vector3Int.s_One;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x00021FD8 File Offset: 0x000201D8
		public static Vector3Int up
		{
			[MethodImpl(256)]
			get
			{
				return Vector3Int.s_Up;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x00021FF0 File Offset: 0x000201F0
		public static Vector3Int down
		{
			[MethodImpl(256)]
			get
			{
				return Vector3Int.s_Down;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x00022008 File Offset: 0x00020208
		public static Vector3Int left
		{
			[MethodImpl(256)]
			get
			{
				return Vector3Int.s_Left;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x00022020 File Offset: 0x00020220
		public static Vector3Int right
		{
			[MethodImpl(256)]
			get
			{
				return Vector3Int.s_Right;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x00022038 File Offset: 0x00020238
		public static Vector3Int forward
		{
			[MethodImpl(256)]
			get
			{
				return Vector3Int.s_Forward;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x00022050 File Offset: 0x00020250
		public static Vector3Int back
		{
			[MethodImpl(256)]
			get
			{
				return Vector3Int.s_Back;
			}
		}

		// Token: 0x0400078D RID: 1933
		private int m_X;

		// Token: 0x0400078E RID: 1934
		private int m_Y;

		// Token: 0x0400078F RID: 1935
		private int m_Z;

		// Token: 0x04000790 RID: 1936
		private static readonly Vector3Int s_Zero = new Vector3Int(0, 0, 0);

		// Token: 0x04000791 RID: 1937
		private static readonly Vector3Int s_One = new Vector3Int(1, 1, 1);

		// Token: 0x04000792 RID: 1938
		private static readonly Vector3Int s_Up = new Vector3Int(0, 1, 0);

		// Token: 0x04000793 RID: 1939
		private static readonly Vector3Int s_Down = new Vector3Int(0, -1, 0);

		// Token: 0x04000794 RID: 1940
		private static readonly Vector3Int s_Left = new Vector3Int(-1, 0, 0);

		// Token: 0x04000795 RID: 1941
		private static readonly Vector3Int s_Right = new Vector3Int(1, 0, 0);

		// Token: 0x04000796 RID: 1942
		private static readonly Vector3Int s_Forward = new Vector3Int(0, 0, 1);

		// Token: 0x04000797 RID: 1943
		private static readonly Vector3Int s_Back = new Vector3Int(0, 0, -1);
	}
}
