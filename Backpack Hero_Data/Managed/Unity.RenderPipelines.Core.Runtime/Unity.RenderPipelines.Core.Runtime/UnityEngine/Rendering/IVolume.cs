using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x020000B5 RID: 181
	public interface IVolume
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000608 RID: 1544
		// (set) Token: 0x06000609 RID: 1545
		bool isGlobal { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600060A RID: 1546
		List<Collider> colliders { get; }
	}
}
