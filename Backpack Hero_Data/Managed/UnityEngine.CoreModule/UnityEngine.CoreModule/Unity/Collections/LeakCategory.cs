using System;
using UnityEngine.Scripting;

namespace Unity.Collections
{
	// Token: 0x0200008D RID: 141
	[UsedByNativeCode]
	internal enum LeakCategory
	{
		// Token: 0x04000212 RID: 530
		Invalid,
		// Token: 0x04000213 RID: 531
		Malloc,
		// Token: 0x04000214 RID: 532
		TempJob,
		// Token: 0x04000215 RID: 533
		Persistent,
		// Token: 0x04000216 RID: 534
		LightProbesQuery,
		// Token: 0x04000217 RID: 535
		NativeTest,
		// Token: 0x04000218 RID: 536
		MeshDataArray,
		// Token: 0x04000219 RID: 537
		TransformAccessArray,
		// Token: 0x0400021A RID: 538
		NavMeshQuery
	}
}
