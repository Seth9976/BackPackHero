using System;

namespace Pathfinding.ECS.RVO
{
	// Token: 0x0200023B RID: 571
	public readonly struct AgentIndex
	{
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x00054CF0 File Offset: 0x00052EF0
		public int Index
		{
			get
			{
				return this.packedAgentIndex & 16777215;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x00054CFE File Offset: 0x00052EFE
		public int Version
		{
			get
			{
				return this.packedAgentIndex & 2130706432;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x00054D0C File Offset: 0x00052F0C
		public bool Valid
		{
			get
			{
				return (this.packedAgentIndex & int.MinValue) == 0;
			}
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00054D1D File Offset: 0x00052F1D
		public AgentIndex(int packedAgentIndex)
		{
			this.packedAgentIndex = packedAgentIndex;
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00054D26 File Offset: 0x00052F26
		public AgentIndex(int version, int index)
		{
			version <<= 24;
			this.packedAgentIndex = (version & 2130706432) | (index & 16777215);
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00054D43 File Offset: 0x00052F43
		public AgentIndex WithIncrementedVersion()
		{
			return new AgentIndex((((this.packedAgentIndex & 2130706432) + 16777216) & 2130706432) | this.Index);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00054D69 File Offset: 0x00052F69
		public AgentIndex WithDeleted()
		{
			return new AgentIndex(this.packedAgentIndex | int.MinValue);
		}

		// Token: 0x04000A72 RID: 2674
		internal const int DeletedBit = -2147483648;

		// Token: 0x04000A73 RID: 2675
		internal const int IndexMask = 16777215;

		// Token: 0x04000A74 RID: 2676
		internal const int VersionOffset = 24;

		// Token: 0x04000A75 RID: 2677
		internal const int VersionMask = 2130706432;

		// Token: 0x04000A76 RID: 2678
		public readonly int packedAgentIndex;
	}
}
