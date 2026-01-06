using System;
using System.Globalization;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000113 RID: 275
	[UsedByNativeCode]
	public struct Plane : IFormattable
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x00009EAC File Offset: 0x000080AC
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x00009EC4 File Offset: 0x000080C4
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
			set
			{
				this.m_Normal = value;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00009ED0 File Offset: 0x000080D0
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x00009EE8 File Offset: 0x000080E8
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
			set
			{
				this.m_Distance = value;
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00009EF2 File Offset: 0x000080F2
		public Plane(Vector3 inNormal, Vector3 inPoint)
		{
			this.m_Normal = Vector3.Normalize(inNormal);
			this.m_Distance = -Vector3.Dot(this.m_Normal, inPoint);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00009F14 File Offset: 0x00008114
		public Plane(Vector3 inNormal, float d)
		{
			this.m_Normal = Vector3.Normalize(inNormal);
			this.m_Distance = d;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00009F2A File Offset: 0x0000812A
		public Plane(Vector3 a, Vector3 b, Vector3 c)
		{
			this.m_Normal = Vector3.Normalize(Vector3.Cross(b - a, c - a));
			this.m_Distance = -Vector3.Dot(this.m_Normal, a);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00009F5E File Offset: 0x0000815E
		public void SetNormalAndPosition(Vector3 inNormal, Vector3 inPoint)
		{
			this.m_Normal = Vector3.Normalize(inNormal);
			this.m_Distance = -Vector3.Dot(inNormal, inPoint);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00009F2A File Offset: 0x0000812A
		public void Set3Points(Vector3 a, Vector3 b, Vector3 c)
		{
			this.m_Normal = Vector3.Normalize(Vector3.Cross(b - a, c - a));
			this.m_Distance = -Vector3.Dot(this.m_Normal, a);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00009F7B File Offset: 0x0000817B
		public void Flip()
		{
			this.m_Normal = -this.m_Normal;
			this.m_Distance = -this.m_Distance;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x00009F9C File Offset: 0x0000819C
		public Plane flipped
		{
			get
			{
				return new Plane(-this.m_Normal, -this.m_Distance);
			}
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00009FC5 File Offset: 0x000081C5
		public void Translate(Vector3 translation)
		{
			this.m_Distance += Vector3.Dot(this.m_Normal, translation);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00009FE4 File Offset: 0x000081E4
		public static Plane Translate(Plane plane, Vector3 translation)
		{
			return new Plane(plane.m_Normal, plane.m_Distance += Vector3.Dot(plane.m_Normal, translation));
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0000A01C File Offset: 0x0000821C
		public Vector3 ClosestPointOnPlane(Vector3 point)
		{
			float num = Vector3.Dot(this.m_Normal, point) + this.m_Distance;
			return point - this.m_Normal * num;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0000A054 File Offset: 0x00008254
		public float GetDistanceToPoint(Vector3 point)
		{
			return Vector3.Dot(this.m_Normal, point) + this.m_Distance;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0000A07C File Offset: 0x0000827C
		public bool GetSide(Vector3 point)
		{
			return Vector3.Dot(this.m_Normal, point) + this.m_Distance > 0f;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0000A0A8 File Offset: 0x000082A8
		public bool SameSide(Vector3 inPt0, Vector3 inPt1)
		{
			float distanceToPoint = this.GetDistanceToPoint(inPt0);
			float distanceToPoint2 = this.GetDistanceToPoint(inPt1);
			return (distanceToPoint > 0f && distanceToPoint2 > 0f) || (distanceToPoint <= 0f && distanceToPoint2 <= 0f);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0000A0F4 File Offset: 0x000082F4
		public bool Raycast(Ray ray, out float enter)
		{
			float num = Vector3.Dot(ray.direction, this.m_Normal);
			float num2 = -Vector3.Dot(ray.origin, this.m_Normal) - this.m_Distance;
			bool flag = Mathf.Approximately(num, 0f);
			bool flag2;
			if (flag)
			{
				enter = 0f;
				flag2 = false;
			}
			else
			{
				enter = num2 / num;
				flag2 = enter > 0f;
			}
			return flag2;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0000A160 File Offset: 0x00008360
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0000A17C File Offset: 0x0000837C
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0000A198 File Offset: 0x00008398
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
			return UnityString.Format("(normal:{0}, distance:{1})", new object[]
			{
				this.m_Normal.ToString(format, formatProvider),
				this.m_Distance.ToString(format, formatProvider)
			});
		}

		// Token: 0x0400038B RID: 907
		internal const int size = 16;

		// Token: 0x0400038C RID: 908
		private Vector3 m_Normal;

		// Token: 0x0400038D RID: 909
		private float m_Distance;
	}
}
