using System;

namespace UnityEngine
{
	// Token: 0x0200015E RID: 350
	[Flags]
	public enum ComputeBufferType
	{
		// Token: 0x0400044B RID: 1099
		Default = 0,
		// Token: 0x0400044C RID: 1100
		Raw = 1,
		// Token: 0x0400044D RID: 1101
		Append = 2,
		// Token: 0x0400044E RID: 1102
		Counter = 4,
		// Token: 0x0400044F RID: 1103
		Constant = 8,
		// Token: 0x04000450 RID: 1104
		Structured = 16,
		// Token: 0x04000451 RID: 1105
		[Obsolete("Enum member DrawIndirect has been deprecated. Use IndirectArguments instead (UnityUpgradable) -> IndirectArguments", false)]
		DrawIndirect = 256,
		// Token: 0x04000452 RID: 1106
		IndirectArguments = 256,
		// Token: 0x04000453 RID: 1107
		[Obsolete("Enum member GPUMemory has been deprecated. All compute buffers now follow the behavior previously defined by this member.", false)]
		GPUMemory = 512
	}
}
