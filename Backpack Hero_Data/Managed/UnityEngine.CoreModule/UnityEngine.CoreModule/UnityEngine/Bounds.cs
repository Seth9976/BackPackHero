using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200010F RID: 271
	[NativeHeader("Runtime/Geometry/Ray.h")]
	[NativeHeader("Runtime/Geometry/AABB.h")]
	[NativeClass("AABB")]
	[NativeHeader("Runtime/Geometry/Intersection.h")]
	[NativeHeader("Runtime/Math/MathScripting.h")]
	[NativeType(Header = "Runtime/Geometry/AABB.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct Bounds : IEquatable<Bounds>, IFormattable
	{
		// Token: 0x0600067F RID: 1663 RVA: 0x00008D19 File Offset: 0x00006F19
		public Bounds(Vector3 center, Vector3 size)
		{
			this.m_Center = center;
			this.m_Extents = size * 0.5f;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00008D34 File Offset: 0x00006F34
		public override int GetHashCode()
		{
			return this.center.GetHashCode() ^ (this.extents.GetHashCode() << 2);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00008D74 File Offset: 0x00006F74
		public override bool Equals(object other)
		{
			bool flag = !(other is Bounds);
			return !flag && this.Equals((Bounds)other);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00008DA8 File Offset: 0x00006FA8
		public bool Equals(Bounds other)
		{
			return this.center.Equals(other.center) && this.extents.Equals(other.extents);
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x00008DEC File Offset: 0x00006FEC
		// (set) Token: 0x06000684 RID: 1668 RVA: 0x00008E04 File Offset: 0x00007004
		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00008E10 File Offset: 0x00007010
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x00008E32 File Offset: 0x00007032
		public Vector3 size
		{
			get
			{
				return this.m_Extents * 2f;
			}
			set
			{
				this.m_Extents = value * 0.5f;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00008E48 File Offset: 0x00007048
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x00008E60 File Offset: 0x00007060
		public Vector3 extents
		{
			get
			{
				return this.m_Extents;
			}
			set
			{
				this.m_Extents = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00008E6C File Offset: 0x0000706C
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x00008E8F File Offset: 0x0000708F
		public Vector3 min
		{
			get
			{
				return this.center - this.extents;
			}
			set
			{
				this.SetMinMax(value, this.max);
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00008EA0 File Offset: 0x000070A0
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x00008EC3 File Offset: 0x000070C3
		public Vector3 max
		{
			get
			{
				return this.center + this.extents;
			}
			set
			{
				this.SetMinMax(this.min, value);
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00008ED4 File Offset: 0x000070D4
		public static bool operator ==(Bounds lhs, Bounds rhs)
		{
			return lhs.center == rhs.center && lhs.extents == rhs.extents;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00008F14 File Offset: 0x00007114
		public static bool operator !=(Bounds lhs, Bounds rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00008F30 File Offset: 0x00007130
		public void SetMinMax(Vector3 min, Vector3 max)
		{
			this.extents = (max - min) * 0.5f;
			this.center = min + this.extents;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00008F5E File Offset: 0x0000715E
		public void Encapsulate(Vector3 point)
		{
			this.SetMinMax(Vector3.Min(this.min, point), Vector3.Max(this.max, point));
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00008F80 File Offset: 0x00007180
		public void Encapsulate(Bounds bounds)
		{
			this.Encapsulate(bounds.center - bounds.extents);
			this.Encapsulate(bounds.center + bounds.extents);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00008FB7 File Offset: 0x000071B7
		public void Expand(float amount)
		{
			amount *= 0.5f;
			this.extents += new Vector3(amount, amount, amount);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00008FDD File Offset: 0x000071DD
		public void Expand(Vector3 amount)
		{
			this.extents += amount * 0.5f;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00009000 File Offset: 0x00007200
		public bool Intersects(Bounds bounds)
		{
			return this.min.x <= bounds.max.x && this.max.x >= bounds.min.x && this.min.y <= bounds.max.y && this.max.y >= bounds.min.y && this.min.z <= bounds.max.z && this.max.z >= bounds.min.z;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000090B4 File Offset: 0x000072B4
		public bool IntersectRay(Ray ray)
		{
			float num;
			return Bounds.IntersectRayAABB(ray, this, out num);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x000090D4 File Offset: 0x000072D4
		public bool IntersectRay(Ray ray, out float distance)
		{
			return Bounds.IntersectRayAABB(ray, this, out distance);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x000090F4 File Offset: 0x000072F4
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00009110 File Offset: 0x00007310
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0000912C File Offset: 0x0000732C
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
			return UnityString.Format("Center: {0}, Extents: {1}", new object[]
			{
				this.m_Center.ToString(format, formatProvider),
				this.m_Extents.ToString(format, formatProvider)
			});
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00009193 File Offset: 0x00007393
		[NativeMethod("IsInside", IsThreadSafe = true)]
		public bool Contains(Vector3 point)
		{
			return Bounds.Contains_Injected(ref this, ref point);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0000919D File Offset: 0x0000739D
		[FreeFunction("BoundsScripting::SqrDistance", HasExplicitThis = true, IsThreadSafe = true)]
		public float SqrDistance(Vector3 point)
		{
			return Bounds.SqrDistance_Injected(ref this, ref point);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x000091A7 File Offset: 0x000073A7
		[FreeFunction("IntersectRayAABB", IsThreadSafe = true)]
		private static bool IntersectRayAABB(Ray ray, Bounds bounds, out float dist)
		{
			return Bounds.IntersectRayAABB_Injected(ref ray, ref bounds, out dist);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x000091B4 File Offset: 0x000073B4
		[FreeFunction("BoundsScripting::ClosestPoint", HasExplicitThis = true, IsThreadSafe = true)]
		public Vector3 ClosestPoint(Vector3 point)
		{
			Vector3 vector;
			Bounds.ClosestPoint_Injected(ref this, ref point, out vector);
			return vector;
		}

		// Token: 0x0600069E RID: 1694
		[MethodImpl(4096)]
		private static extern bool Contains_Injected(ref Bounds _unity_self, ref Vector3 point);

		// Token: 0x0600069F RID: 1695
		[MethodImpl(4096)]
		private static extern float SqrDistance_Injected(ref Bounds _unity_self, ref Vector3 point);

		// Token: 0x060006A0 RID: 1696
		[MethodImpl(4096)]
		private static extern bool IntersectRayAABB_Injected(ref Ray ray, ref Bounds bounds, out float dist);

		// Token: 0x060006A1 RID: 1697
		[MethodImpl(4096)]
		private static extern void ClosestPoint_Injected(ref Bounds _unity_self, ref Vector3 point, out Vector3 ret);

		// Token: 0x04000384 RID: 900
		private Vector3 m_Center;

		// Token: 0x04000385 RID: 901
		[NativeName("m_Extent")]
		private Vector3 m_Extents;
	}
}
