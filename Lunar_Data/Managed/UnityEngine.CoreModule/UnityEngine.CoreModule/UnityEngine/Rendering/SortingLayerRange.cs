using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000412 RID: 1042
	public struct SortingLayerRange : IEquatable<SortingLayerRange>
	{
		// Token: 0x060023ED RID: 9197 RVA: 0x0003CA9E File Offset: 0x0003AC9E
		public SortingLayerRange(short lowerBound, short upperBound)
		{
			this.m_LowerBound = lowerBound;
			this.m_UpperBound = upperBound;
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x0003CAB0 File Offset: 0x0003ACB0
		// (set) Token: 0x060023EF RID: 9199 RVA: 0x0003CAC8 File Offset: 0x0003ACC8
		public short lowerBound
		{
			get
			{
				return this.m_LowerBound;
			}
			set
			{
				this.m_LowerBound = value;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x0003CAD4 File Offset: 0x0003ACD4
		// (set) Token: 0x060023F1 RID: 9201 RVA: 0x0003CAEC File Offset: 0x0003ACEC
		public short upperBound
		{
			get
			{
				return this.m_UpperBound;
			}
			set
			{
				this.m_UpperBound = value;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x0003CAF8 File Offset: 0x0003ACF8
		public static SortingLayerRange all
		{
			get
			{
				return new SortingLayerRange
				{
					m_LowerBound = short.MinValue,
					m_UpperBound = short.MaxValue
				};
			}
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x0003CB28 File Offset: 0x0003AD28
		public bool Equals(SortingLayerRange other)
		{
			return this.m_LowerBound == other.m_LowerBound && this.m_UpperBound == other.m_UpperBound;
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x0003CB5C File Offset: 0x0003AD5C
		public override bool Equals(object obj)
		{
			bool flag = !(obj is SortingLayerRange);
			return !flag && this.Equals((SortingLayerRange)obj);
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x0003CB90 File Offset: 0x0003AD90
		public static bool operator !=(SortingLayerRange lhs, SortingLayerRange rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x0003CBB0 File Offset: 0x0003ADB0
		public static bool operator ==(SortingLayerRange lhs, SortingLayerRange rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x0003CBCC File Offset: 0x0003ADCC
		public override int GetHashCode()
		{
			return ((int)this.m_UpperBound << 16) | ((int)this.m_LowerBound & 65535);
		}

		// Token: 0x04000D4A RID: 3402
		private short m_LowerBound;

		// Token: 0x04000D4B RID: 3403
		private short m_UpperBound;
	}
}
