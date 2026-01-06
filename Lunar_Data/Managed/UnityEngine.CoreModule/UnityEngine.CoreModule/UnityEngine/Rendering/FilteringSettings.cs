using System;
using UnityEngine.Internal;

namespace UnityEngine.Rendering
{
	// Token: 0x020003FC RID: 1020
	public struct FilteringSettings : IEquatable<FilteringSettings>
	{
		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060022B6 RID: 8886 RVA: 0x0003A562 File Offset: 0x00038762
		public static FilteringSettings defaultValue
		{
			get
			{
				return new FilteringSettings(new RenderQueueRange?(RenderQueueRange.all), -1, uint.MaxValue, 0);
			}
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x0003A578 File Offset: 0x00038778
		public FilteringSettings([DefaultValue("RenderQueueRange.all")] RenderQueueRange? renderQueueRange = null, int layerMask = -1, uint renderingLayerMask = 4294967295U, int excludeMotionVectorObjects = 0)
		{
			this = default(FilteringSettings);
			this.m_RenderQueueRange = renderQueueRange ?? RenderQueueRange.all;
			this.m_LayerMask = layerMask;
			this.m_RenderingLayerMask = renderingLayerMask;
			this.m_ExcludeMotionVectorObjects = excludeMotionVectorObjects;
			this.m_SortingLayerRange = SortingLayerRange.all;
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060022B8 RID: 8888 RVA: 0x0003A5D0 File Offset: 0x000387D0
		// (set) Token: 0x060022B9 RID: 8889 RVA: 0x0003A5E8 File Offset: 0x000387E8
		public RenderQueueRange renderQueueRange
		{
			get
			{
				return this.m_RenderQueueRange;
			}
			set
			{
				this.m_RenderQueueRange = value;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060022BA RID: 8890 RVA: 0x0003A5F4 File Offset: 0x000387F4
		// (set) Token: 0x060022BB RID: 8891 RVA: 0x0003A60C File Offset: 0x0003880C
		public int layerMask
		{
			get
			{
				return this.m_LayerMask;
			}
			set
			{
				this.m_LayerMask = value;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060022BC RID: 8892 RVA: 0x0003A618 File Offset: 0x00038818
		// (set) Token: 0x060022BD RID: 8893 RVA: 0x0003A630 File Offset: 0x00038830
		public uint renderingLayerMask
		{
			get
			{
				return this.m_RenderingLayerMask;
			}
			set
			{
				this.m_RenderingLayerMask = value;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060022BE RID: 8894 RVA: 0x0003A63C File Offset: 0x0003883C
		// (set) Token: 0x060022BF RID: 8895 RVA: 0x0003A657 File Offset: 0x00038857
		public bool excludeMotionVectorObjects
		{
			get
			{
				return this.m_ExcludeMotionVectorObjects != 0;
			}
			set
			{
				this.m_ExcludeMotionVectorObjects = (value ? 1 : 0);
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060022C0 RID: 8896 RVA: 0x0003A668 File Offset: 0x00038868
		// (set) Token: 0x060022C1 RID: 8897 RVA: 0x0003A680 File Offset: 0x00038880
		public SortingLayerRange sortingLayerRange
		{
			get
			{
				return this.m_SortingLayerRange;
			}
			set
			{
				this.m_SortingLayerRange = value;
			}
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x0003A68C File Offset: 0x0003888C
		public bool Equals(FilteringSettings other)
		{
			return this.m_RenderQueueRange.Equals(other.m_RenderQueueRange) && this.m_LayerMask == other.m_LayerMask && this.m_RenderingLayerMask == other.m_RenderingLayerMask && this.m_ExcludeMotionVectorObjects == other.m_ExcludeMotionVectorObjects;
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x0003A6E0 File Offset: 0x000388E0
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is FilteringSettings && this.Equals((FilteringSettings)obj);
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x0003A718 File Offset: 0x00038918
		public override int GetHashCode()
		{
			int num = this.m_RenderQueueRange.GetHashCode();
			num = (num * 397) ^ this.m_LayerMask;
			num = (num * 397) ^ (int)this.m_RenderingLayerMask;
			return (num * 397) ^ this.m_ExcludeMotionVectorObjects;
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x0003A76C File Offset: 0x0003896C
		public static bool operator ==(FilteringSettings left, FilteringSettings right)
		{
			return left.Equals(right);
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x0003A788 File Offset: 0x00038988
		public static bool operator !=(FilteringSettings left, FilteringSettings right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000CDD RID: 3293
		private RenderQueueRange m_RenderQueueRange;

		// Token: 0x04000CDE RID: 3294
		private int m_LayerMask;

		// Token: 0x04000CDF RID: 3295
		private uint m_RenderingLayerMask;

		// Token: 0x04000CE0 RID: 3296
		private int m_ExcludeMotionVectorObjects;

		// Token: 0x04000CE1 RID: 3297
		private SortingLayerRange m_SortingLayerRange;
	}
}
