using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000078 RID: 120
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "C:\\Users\\jaspe\\Backpack Hero\\Library\\PackageCache\\com.unity.render-pipelines.core@12.1.10\\Runtime\\Lighting\\ProbeVolume\\ShaderVariablesProbeVolumes.cs", needAccessors = false, generateCBuffer = true, constantRegister = 5)]
	internal struct ShaderVariablesProbeVolumes
	{
		// Token: 0x04000253 RID: 595
		public Vector3 _PoolDim;

		// Token: 0x04000254 RID: 596
		public float _ViewBias;

		// Token: 0x04000255 RID: 597
		public Vector3 _MinCellPosition;

		// Token: 0x04000256 RID: 598
		public float _PVSamplingNoise;

		// Token: 0x04000257 RID: 599
		public Vector3 _CellIndicesDim;

		// Token: 0x04000258 RID: 600
		public float _CellInMeters;

		// Token: 0x04000259 RID: 601
		public float _CellInMinBricks;

		// Token: 0x0400025A RID: 602
		public float _MinBrickSize;

		// Token: 0x0400025B RID: 603
		public int _IndexChunkSize;

		// Token: 0x0400025C RID: 604
		public float _NormalBias;
	}
}
