using System;
using System.Collections.Generic;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000012 RID: 18
	[PreferBinarySerialization]
	internal class ProbeVolumeAsset : ScriptableObject
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000628D File Offset: 0x0000448D
		[SerializeField]
		public int Version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00006295 File Offset: 0x00004495
		internal int maxSubdivision
		{
			get
			{
				return this.simplificationLevels + 1;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000629F File Offset: 0x0000449F
		internal float minBrickSize
		{
			get
			{
				return Mathf.Max(0.01f, this.minDistanceBetweenProbes * 3f);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000062B7 File Offset: 0x000044B7
		internal bool CompatibleWith(ProbeVolumeAsset otherAsset)
		{
			return this.maxSubdivision == otherAsset.maxSubdivision && this.minBrickSize == otherAsset.minBrickSize && this.cellSizeInBricks == otherAsset.cellSizeInBricks;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000062E5 File Offset: 0x000044E5
		public string GetSerializedFullPath()
		{
			return this.m_AssetFullPath;
		}

		// Token: 0x04000087 RID: 135
		[SerializeField]
		protected internal int m_Version = 3;

		// Token: 0x04000088 RID: 136
		[SerializeField]
		internal List<ProbeReferenceVolume.Cell> cells = new List<ProbeReferenceVolume.Cell>();

		// Token: 0x04000089 RID: 137
		[SerializeField]
		internal Vector3Int maxCellPosition;

		// Token: 0x0400008A RID: 138
		[SerializeField]
		internal Vector3Int minCellPosition;

		// Token: 0x0400008B RID: 139
		[SerializeField]
		internal Bounds globalBounds;

		// Token: 0x0400008C RID: 140
		[SerializeField]
		internal ProbeVolumeSHBands bands;

		// Token: 0x0400008D RID: 141
		[SerializeField]
		private string m_AssetFullPath = "UNINITIALIZED!";

		// Token: 0x0400008E RID: 142
		[SerializeField]
		internal int cellSizeInBricks;

		// Token: 0x0400008F RID: 143
		[SerializeField]
		internal float minDistanceBetweenProbes;

		// Token: 0x04000090 RID: 144
		[SerializeField]
		internal int simplificationLevels;

		// Token: 0x02000121 RID: 289
		[Serializable]
		internal enum AssetVersion
		{
			// Token: 0x040004AE RID: 1198
			First,
			// Token: 0x040004AF RID: 1199
			AddProbeVolumesAtlasEncodingModes,
			// Token: 0x040004B0 RID: 1200
			PV2,
			// Token: 0x040004B1 RID: 1201
			ChunkBasedIndex,
			// Token: 0x040004B2 RID: 1202
			Max,
			// Token: 0x040004B3 RID: 1203
			Current = 3
		}
	}
}
