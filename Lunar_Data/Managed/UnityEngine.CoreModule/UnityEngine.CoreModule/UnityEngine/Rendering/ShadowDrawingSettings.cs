using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200040E RID: 1038
	[UsedByNativeCode]
	public struct ShadowDrawingSettings : IEquatable<ShadowDrawingSettings>
	{
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060023CF RID: 9167 RVA: 0x0003C534 File Offset: 0x0003A734
		// (set) Token: 0x060023D0 RID: 9168 RVA: 0x0003C54C File Offset: 0x0003A74C
		public CullingResults cullingResults
		{
			get
			{
				return this.m_CullingResults;
			}
			set
			{
				this.m_CullingResults = value;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060023D1 RID: 9169 RVA: 0x0003C558 File Offset: 0x0003A758
		// (set) Token: 0x060023D2 RID: 9170 RVA: 0x0003C570 File Offset: 0x0003A770
		public int lightIndex
		{
			get
			{
				return this.m_LightIndex;
			}
			set
			{
				this.m_LightIndex = value;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060023D3 RID: 9171 RVA: 0x0003C57C File Offset: 0x0003A77C
		// (set) Token: 0x060023D4 RID: 9172 RVA: 0x0003C597 File Offset: 0x0003A797
		public bool useRenderingLayerMaskTest
		{
			get
			{
				return this.m_UseRenderingLayerMaskTest != 0;
			}
			set
			{
				this.m_UseRenderingLayerMaskTest = (value ? 1 : 0);
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060023D5 RID: 9173 RVA: 0x0003C5A8 File Offset: 0x0003A7A8
		// (set) Token: 0x060023D6 RID: 9174 RVA: 0x0003C5C0 File Offset: 0x0003A7C0
		public ShadowSplitData splitData
		{
			get
			{
				return this.m_SplitData;
			}
			set
			{
				this.m_SplitData = value;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060023D7 RID: 9175 RVA: 0x0003C5CC File Offset: 0x0003A7CC
		// (set) Token: 0x060023D8 RID: 9176 RVA: 0x0003C5E4 File Offset: 0x0003A7E4
		public ShadowObjectsFilter objectsFilter
		{
			get
			{
				return this.m_ObjectsFilter;
			}
			set
			{
				this.m_ObjectsFilter = value;
			}
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x0003C5EE File Offset: 0x0003A7EE
		public ShadowDrawingSettings(CullingResults cullingResults, int lightIndex)
		{
			this.m_CullingResults = cullingResults;
			this.m_LightIndex = lightIndex;
			this.m_UseRenderingLayerMaskTest = 0;
			this.m_SplitData = default(ShadowSplitData);
			this.m_SplitData.shadowCascadeBlendCullingFactor = 1f;
			this.m_ObjectsFilter = ShadowObjectsFilter.AllObjects;
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x0003C62C File Offset: 0x0003A82C
		public bool Equals(ShadowDrawingSettings other)
		{
			return this.m_CullingResults.Equals(other.m_CullingResults) && this.m_LightIndex == other.m_LightIndex && this.m_SplitData.Equals(other.m_SplitData) && this.m_UseRenderingLayerMaskTest.Equals(other.m_UseRenderingLayerMaskTest) && this.m_ObjectsFilter.Equals(other.m_ObjectsFilter);
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x0003C6A4 File Offset: 0x0003A8A4
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ShadowDrawingSettings && this.Equals((ShadowDrawingSettings)obj);
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x0003C6DC File Offset: 0x0003A8DC
		public override int GetHashCode()
		{
			int num = this.m_CullingResults.GetHashCode();
			num = (num * 397) ^ this.m_LightIndex;
			num = (num * 397) ^ this.m_UseRenderingLayerMaskTest;
			num = (num * 397) ^ this.m_SplitData.GetHashCode();
			return (num * 397) ^ (int)this.m_ObjectsFilter;
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x0003C74C File Offset: 0x0003A94C
		public static bool operator ==(ShadowDrawingSettings left, ShadowDrawingSettings right)
		{
			return left.Equals(right);
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x0003C768 File Offset: 0x0003A968
		public static bool operator !=(ShadowDrawingSettings left, ShadowDrawingSettings right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000D32 RID: 3378
		private CullingResults m_CullingResults;

		// Token: 0x04000D33 RID: 3379
		private int m_LightIndex;

		// Token: 0x04000D34 RID: 3380
		private int m_UseRenderingLayerMaskTest;

		// Token: 0x04000D35 RID: 3381
		private ShadowSplitData m_SplitData;

		// Token: 0x04000D36 RID: 3382
		private ShadowObjectsFilter m_ObjectsFilter;
	}
}
