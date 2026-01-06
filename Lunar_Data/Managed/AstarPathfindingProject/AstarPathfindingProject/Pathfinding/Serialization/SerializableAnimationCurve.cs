using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x02000237 RID: 567
	[Preserve]
	internal class SerializableAnimationCurve
	{
		// Token: 0x04000A67 RID: 2663
		public WrapMode preWrapMode;

		// Token: 0x04000A68 RID: 2664
		public WrapMode postWrapMode;

		// Token: 0x04000A69 RID: 2665
		public Keyframe[] keys;
	}
}
