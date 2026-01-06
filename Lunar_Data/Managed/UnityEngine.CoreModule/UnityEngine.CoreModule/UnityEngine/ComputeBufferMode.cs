using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200012C RID: 300
	[NativeType("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum ComputeBufferMode
	{
		// Token: 0x040003C3 RID: 963
		Immutable,
		// Token: 0x040003C4 RID: 964
		Dynamic,
		// Token: 0x040003C5 RID: 965
		[Obsolete("ComputeBufferMode.Circular is deprecated (legacy mode)")]
		Circular,
		// Token: 0x040003C6 RID: 966
		[Obsolete("ComputeBufferMode.StreamOut is deprecated (internal use only)")]
		StreamOut,
		// Token: 0x040003C7 RID: 967
		SubUpdates
	}
}
