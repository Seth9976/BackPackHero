using System;
using System.Globalization;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000116 RID: 278
	[NativeHeader("Runtime/Math/Rect.h")]
	[NativeClass("Rectf", "template<typename T> class RectT; typedef RectT<float> Rectf;")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct Rect : IEquatable<Rect>, IFormattable
	{
		// Token: 0x06000706 RID: 1798 RVA: 0x0000A45F File Offset: 0x0000865F
		public Rect(float x, float y, float width, float height)
		{
			this.m_XMin = x;
			this.m_YMin = y;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0000A47F File Offset: 0x0000867F
		public Rect(Vector2 position, Vector2 size)
		{
			this.m_XMin = position.x;
			this.m_YMin = position.y;
			this.m_Width = size.x;
			this.m_Height = size.y;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0000A4B2 File Offset: 0x000086B2
		public Rect(Rect source)
		{
			this.m_XMin = source.m_XMin;
			this.m_YMin = source.m_YMin;
			this.m_Width = source.m_Width;
			this.m_Height = source.m_Height;
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0000A4E5 File Offset: 0x000086E5
		public static Rect zero
		{
			get
			{
				return new Rect(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0000A500 File Offset: 0x00008700
		public static Rect MinMaxRect(float xmin, float ymin, float xmax, float ymax)
		{
			return new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0000A45F File Offset: 0x0000865F
		public void Set(float x, float y, float width, float height)
		{
			this.m_XMin = x;
			this.m_YMin = y;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0000A520 File Offset: 0x00008720
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x0000A538 File Offset: 0x00008738
		public float x
		{
			get
			{
				return this.m_XMin;
			}
			set
			{
				this.m_XMin = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0000A544 File Offset: 0x00008744
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x0000A55C File Offset: 0x0000875C
		public float y
		{
			get
			{
				return this.m_YMin;
			}
			set
			{
				this.m_YMin = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x0000A568 File Offset: 0x00008768
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x0000A58B File Offset: 0x0000878B
		public Vector2 position
		{
			get
			{
				return new Vector2(this.m_XMin, this.m_YMin);
			}
			set
			{
				this.m_XMin = value.x;
				this.m_YMin = value.y;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0000A5A8 File Offset: 0x000087A8
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x0000A5E5 File Offset: 0x000087E5
		public Vector2 center
		{
			get
			{
				return new Vector2(this.x + this.m_Width / 2f, this.y + this.m_Height / 2f);
			}
			set
			{
				this.m_XMin = value.x - this.m_Width / 2f;
				this.m_YMin = value.y - this.m_Height / 2f;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0000A61C File Offset: 0x0000881C
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x0000A63F File Offset: 0x0000883F
		public Vector2 min
		{
			get
			{
				return new Vector2(this.xMin, this.yMin);
			}
			set
			{
				this.xMin = value.x;
				this.yMin = value.y;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0000A65C File Offset: 0x0000885C
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x0000A67F File Offset: 0x0000887F
		public Vector2 max
		{
			get
			{
				return new Vector2(this.xMax, this.yMax);
			}
			set
			{
				this.xMax = value.x;
				this.yMax = value.y;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0000A69C File Offset: 0x0000889C
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x0000A6B4 File Offset: 0x000088B4
		public float width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0000A6C0 File Offset: 0x000088C0
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x0000A6D8 File Offset: 0x000088D8
		public float height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0000A6E4 File Offset: 0x000088E4
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x0000A707 File Offset: 0x00008907
		public Vector2 size
		{
			get
			{
				return new Vector2(this.m_Width, this.m_Height);
			}
			set
			{
				this.m_Width = value.x;
				this.m_Height = value.y;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0000A724 File Offset: 0x00008924
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x0000A73C File Offset: 0x0000893C
		public float xMin
		{
			get
			{
				return this.m_XMin;
			}
			set
			{
				float xMax = this.xMax;
				this.m_XMin = value;
				this.m_Width = xMax - this.m_XMin;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0000A768 File Offset: 0x00008968
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x0000A780 File Offset: 0x00008980
		public float yMin
		{
			get
			{
				return this.m_YMin;
			}
			set
			{
				float yMax = this.yMax;
				this.m_YMin = value;
				this.m_Height = yMax - this.m_YMin;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0000A7AC File Offset: 0x000089AC
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0000A7CB File Offset: 0x000089CB
		public float xMax
		{
			get
			{
				return this.m_Width + this.m_XMin;
			}
			set
			{
				this.m_Width = value - this.m_XMin;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0000A7DC File Offset: 0x000089DC
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x0000A7FB File Offset: 0x000089FB
		public float yMax
		{
			get
			{
				return this.m_Height + this.m_YMin;
			}
			set
			{
				this.m_Height = value - this.m_YMin;
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0000A80C File Offset: 0x00008A0C
		public bool Contains(Vector2 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0000A85C File Offset: 0x00008A5C
		public bool Contains(Vector3 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0000A8AC File Offset: 0x00008AAC
		public bool Contains(Vector3 point, bool allowInverse)
		{
			bool flag = !allowInverse;
			bool flag2;
			if (flag)
			{
				flag2 = this.Contains(point);
			}
			else
			{
				bool flag3 = (this.width < 0f && point.x <= this.xMin && point.x > this.xMax) || (this.width >= 0f && point.x >= this.xMin && point.x < this.xMax);
				bool flag4 = (this.height < 0f && point.y <= this.yMin && point.y > this.yMax) || (this.height >= 0f && point.y >= this.yMin && point.y < this.yMax);
				flag2 = flag3 && flag4;
			}
			return flag2;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0000A98C File Offset: 0x00008B8C
		private static Rect OrderMinMax(Rect rect)
		{
			bool flag = rect.xMin > rect.xMax;
			if (flag)
			{
				float xMin = rect.xMin;
				rect.xMin = rect.xMax;
				rect.xMax = xMin;
			}
			bool flag2 = rect.yMin > rect.yMax;
			if (flag2)
			{
				float yMin = rect.yMin;
				rect.yMin = rect.yMax;
				rect.yMax = yMin;
			}
			return rect;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0000AA10 File Offset: 0x00008C10
		public bool Overlaps(Rect other)
		{
			return other.xMax > this.xMin && other.xMin < this.xMax && other.yMax > this.yMin && other.yMin < this.yMax;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0000AA64 File Offset: 0x00008C64
		public bool Overlaps(Rect other, bool allowInverse)
		{
			Rect rect = this;
			if (allowInverse)
			{
				rect = Rect.OrderMinMax(rect);
				other = Rect.OrderMinMax(other);
			}
			return rect.Overlaps(other);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0000AA9C File Offset: 0x00008C9C
		public static Vector2 NormalizedToPoint(Rect rectangle, Vector2 normalizedRectCoordinates)
		{
			return new Vector2(Mathf.Lerp(rectangle.x, rectangle.xMax, normalizedRectCoordinates.x), Mathf.Lerp(rectangle.y, rectangle.yMax, normalizedRectCoordinates.y));
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0000AAE8 File Offset: 0x00008CE8
		public static Vector2 PointToNormalized(Rect rectangle, Vector2 point)
		{
			return new Vector2(Mathf.InverseLerp(rectangle.x, rectangle.xMax, point.x), Mathf.InverseLerp(rectangle.y, rectangle.yMax, point.y));
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0000AB34 File Offset: 0x00008D34
		public static bool operator !=(Rect lhs, Rect rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0000AB50 File Offset: 0x00008D50
		public static bool operator ==(Rect lhs, Rect rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.width == rhs.width && lhs.height == rhs.height;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0000ABA8 File Offset: 0x00008DA8
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.width.GetHashCode() << 2) ^ (this.y.GetHashCode() >> 2) ^ (this.height.GetHashCode() >> 1);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0000ABFC File Offset: 0x00008DFC
		public override bool Equals(object other)
		{
			bool flag = !(other is Rect);
			return !flag && this.Equals((Rect)other);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0000AC30 File Offset: 0x00008E30
		public bool Equals(Rect other)
		{
			return this.x.Equals(other.x) && this.y.Equals(other.y) && this.width.Equals(other.width) && this.height.Equals(other.height);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0000ACA0 File Offset: 0x00008EA0
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0000ACBC File Offset: 0x00008EBC
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0000ACD8 File Offset: 0x00008ED8
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
			return UnityString.Format("(x:{0}, y:{1}, width:{2}, height:{3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.width.ToString(format, formatProvider),
				this.height.ToString(format, formatProvider)
			});
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0000AD6C File Offset: 0x00008F6C
		[Obsolete("use xMin")]
		public float left
		{
			get
			{
				return this.m_XMin;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0000AD84 File Offset: 0x00008F84
		[Obsolete("use xMax")]
		public float right
		{
			get
			{
				return this.m_XMin + this.m_Width;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0000ADA4 File Offset: 0x00008FA4
		[Obsolete("use yMin")]
		public float top
		{
			get
			{
				return this.m_YMin;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0000ADBC File Offset: 0x00008FBC
		[Obsolete("use yMax")]
		public float bottom
		{
			get
			{
				return this.m_YMin + this.m_Height;
			}
		}

		// Token: 0x04000392 RID: 914
		[NativeName("x")]
		private float m_XMin;

		// Token: 0x04000393 RID: 915
		[NativeName("y")]
		private float m_YMin;

		// Token: 0x04000394 RID: 916
		[NativeName("width")]
		private float m_Width;

		// Token: 0x04000395 RID: 917
		[NativeName("height")]
		private float m_Height;
	}
}
