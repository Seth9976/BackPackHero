using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001CA RID: 458
	[Il2CppEagerStaticClassConstruction]
	[NativeType("Runtime/Math/Vector2Int.h")]
	[UsedByNativeCode]
	public struct Vector2Int : IEquatable<Vector2Int>, IFormattable
	{
		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x00020FF8 File Offset: 0x0001F1F8
		// (set) Token: 0x06001503 RID: 5379 RVA: 0x00021010 File Offset: 0x0001F210
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

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x0002101C File Offset: 0x0001F21C
		// (set) Token: 0x06001505 RID: 5381 RVA: 0x00021034 File Offset: 0x0001F234
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

		// Token: 0x06001506 RID: 5382 RVA: 0x0002103E File Offset: 0x0001F23E
		[MethodImpl(256)]
		public Vector2Int(int x, int y)
		{
			this.m_X = x;
			this.m_Y = y;
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0002103E File Offset: 0x0001F23E
		[MethodImpl(256)]
		public void Set(int x, int y)
		{
			this.m_X = x;
			this.m_Y = y;
		}

		// Token: 0x17000432 RID: 1074
		public int this[int index]
		{
			[MethodImpl(256)]
			get
			{
				int num;
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException(string.Format("Invalid Vector2Int index addressed: {0}!", index));
					}
					num = this.y;
				}
				else
				{
					num = this.x;
				}
				return num;
			}
			[MethodImpl(256)]
			set
			{
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException(string.Format("Invalid Vector2Int index addressed: {0}!", index));
					}
					this.y = value;
				}
				else
				{
					this.x = value;
				}
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x000210E0 File Offset: 0x0001F2E0
		public float magnitude
		{
			[MethodImpl(256)]
			get
			{
				return Mathf.Sqrt((float)(this.x * this.x + this.y * this.y));
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x00021114 File Offset: 0x0001F314
		public int sqrMagnitude
		{
			[MethodImpl(256)]
			get
			{
				return this.x * this.x + this.y * this.y;
			}
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x00021144 File Offset: 0x0001F344
		[MethodImpl(256)]
		public static float Distance(Vector2Int a, Vector2Int b)
		{
			float num = (float)(a.x - b.x);
			float num2 = (float)(a.y - b.y);
			return (float)Math.Sqrt((double)(num * num + num2 * num2));
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x00021188 File Offset: 0x0001F388
		[MethodImpl(256)]
		public static Vector2Int Min(Vector2Int lhs, Vector2Int rhs)
		{
			return new Vector2Int(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x000211C8 File Offset: 0x0001F3C8
		[MethodImpl(256)]
		public static Vector2Int Max(Vector2Int lhs, Vector2Int rhs)
		{
			return new Vector2Int(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x00021208 File Offset: 0x0001F408
		[MethodImpl(256)]
		public static Vector2Int Scale(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x * b.x, a.y * b.y);
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0002123D File Offset: 0x0001F43D
		[MethodImpl(256)]
		public void Scale(Vector2Int scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0002126C File Offset: 0x0001F46C
		[MethodImpl(256)]
		public void Clamp(Vector2Int min, Vector2Int max)
		{
			this.x = Math.Max(min.x, this.x);
			this.x = Math.Min(max.x, this.x);
			this.y = Math.Max(min.y, this.y);
			this.y = Math.Min(max.y, this.y);
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x000212E0 File Offset: 0x0001F4E0
		[MethodImpl(256)]
		public static implicit operator Vector2(Vector2Int v)
		{
			return new Vector2((float)v.x, (float)v.y);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00021308 File Offset: 0x0001F508
		[MethodImpl(256)]
		public static explicit operator Vector3Int(Vector2Int v)
		{
			return new Vector3Int(v.x, v.y, 0);
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x00021330 File Offset: 0x0001F530
		[MethodImpl(256)]
		public static Vector2Int FloorToInt(Vector2 v)
		{
			return new Vector2Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00021360 File Offset: 0x0001F560
		[MethodImpl(256)]
		public static Vector2Int CeilToInt(Vector2 v)
		{
			return new Vector2Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y));
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x00021390 File Offset: 0x0001F590
		[MethodImpl(256)]
		public static Vector2Int RoundToInt(Vector2 v)
		{
			return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x000213C0 File Offset: 0x0001F5C0
		[MethodImpl(256)]
		public static Vector2Int operator -(Vector2Int v)
		{
			return new Vector2Int(-v.x, -v.y);
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x000213E8 File Offset: 0x0001F5E8
		[MethodImpl(256)]
		public static Vector2Int operator +(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x + b.x, a.y + b.y);
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x00021420 File Offset: 0x0001F620
		[MethodImpl(256)]
		public static Vector2Int operator -(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x - b.x, a.y - b.y);
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x00021458 File Offset: 0x0001F658
		[MethodImpl(256)]
		public static Vector2Int operator *(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x * b.x, a.y * b.y);
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00021490 File Offset: 0x0001F690
		[MethodImpl(256)]
		public static Vector2Int operator *(int a, Vector2Int b)
		{
			return new Vector2Int(a * b.x, a * b.y);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x000214BC File Offset: 0x0001F6BC
		[MethodImpl(256)]
		public static Vector2Int operator *(Vector2Int a, int b)
		{
			return new Vector2Int(a.x * b, a.y * b);
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x000214E8 File Offset: 0x0001F6E8
		[MethodImpl(256)]
		public static Vector2Int operator /(Vector2Int a, int b)
		{
			return new Vector2Int(a.x / b, a.y / b);
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x00021514 File Offset: 0x0001F714
		[MethodImpl(256)]
		public static bool operator ==(Vector2Int lhs, Vector2Int rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y;
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0002154C File Offset: 0x0001F74C
		[MethodImpl(256)]
		public static bool operator !=(Vector2Int lhs, Vector2Int rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x00021568 File Offset: 0x0001F768
		[MethodImpl(256)]
		public override bool Equals(object other)
		{
			bool flag = !(other is Vector2Int);
			return !flag && this.Equals((Vector2Int)other);
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0002159C File Offset: 0x0001F79C
		[MethodImpl(256)]
		public bool Equals(Vector2Int other)
		{
			return this.x == other.x && this.y == other.y;
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x000215D0 File Offset: 0x0001F7D0
		[MethodImpl(256)]
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2);
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x00021604 File Offset: 0x0001F804
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x00021620 File Offset: 0x0001F820
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0002163C File Offset: 0x0001F83C
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = formatProvider == null;
			if (flag)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x00021698 File Offset: 0x0001F898
		public static Vector2Int zero
		{
			[MethodImpl(256)]
			get
			{
				return Vector2Int.s_Zero;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x000216B0 File Offset: 0x0001F8B0
		public static Vector2Int one
		{
			[MethodImpl(256)]
			get
			{
				return Vector2Int.s_One;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x000216C8 File Offset: 0x0001F8C8
		public static Vector2Int up
		{
			[MethodImpl(256)]
			get
			{
				return Vector2Int.s_Up;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x000216E0 File Offset: 0x0001F8E0
		public static Vector2Int down
		{
			[MethodImpl(256)]
			get
			{
				return Vector2Int.s_Down;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x000216F8 File Offset: 0x0001F8F8
		public static Vector2Int left
		{
			[MethodImpl(256)]
			get
			{
				return Vector2Int.s_Left;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x00021710 File Offset: 0x0001F910
		public static Vector2Int right
		{
			[MethodImpl(256)]
			get
			{
				return Vector2Int.s_Right;
			}
		}

		// Token: 0x04000785 RID: 1925
		private int m_X;

		// Token: 0x04000786 RID: 1926
		private int m_Y;

		// Token: 0x04000787 RID: 1927
		private static readonly Vector2Int s_Zero = new Vector2Int(0, 0);

		// Token: 0x04000788 RID: 1928
		private static readonly Vector2Int s_One = new Vector2Int(1, 1);

		// Token: 0x04000789 RID: 1929
		private static readonly Vector2Int s_Up = new Vector2Int(0, 1);

		// Token: 0x0400078A RID: 1930
		private static readonly Vector2Int s_Down = new Vector2Int(0, -1);

		// Token: 0x0400078B RID: 1931
		private static readonly Vector2Int s_Left = new Vector2Int(-1, 0);

		// Token: 0x0400078C RID: 1932
		private static readonly Vector2Int s_Right = new Vector2Int(1, 0);
	}
}
