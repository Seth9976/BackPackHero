using System;
using System.Globalization;

namespace UnityEngine
{
	// Token: 0x02000115 RID: 277
	public struct Ray2D : IFormattable
	{
		// Token: 0x060006FD RID: 1789 RVA: 0x0000A32F File Offset: 0x0000852F
		public Ray2D(Vector2 origin, Vector2 direction)
		{
			this.m_Origin = origin;
			this.m_Direction = direction.normalized;
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x0000A348 File Offset: 0x00008548
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x0000A360 File Offset: 0x00008560
		public Vector2 origin
		{
			get
			{
				return this.m_Origin;
			}
			set
			{
				this.m_Origin = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0000A36C File Offset: 0x0000856C
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x0000A384 File Offset: 0x00008584
		public Vector2 direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				this.m_Direction = value.normalized;
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0000A394 File Offset: 0x00008594
		public Vector2 GetPoint(float distance)
		{
			return this.m_Origin + this.m_Direction * distance;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0000A3C0 File Offset: 0x000085C0
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0000A3DC File Offset: 0x000085DC
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0000A3F8 File Offset: 0x000085F8
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
			return UnityString.Format("Origin: {0}, Dir: {1}", new object[]
			{
				this.m_Origin.ToString(format, formatProvider),
				this.m_Direction.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000390 RID: 912
		private Vector2 m_Origin;

		// Token: 0x04000391 RID: 913
		private Vector2 m_Direction;
	}
}
