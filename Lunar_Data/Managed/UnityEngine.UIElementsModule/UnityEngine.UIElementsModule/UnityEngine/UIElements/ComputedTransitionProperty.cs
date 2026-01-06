using System;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x0200026A RID: 618
	internal struct ComputedTransitionProperty
	{
		// Token: 0x0400089A RID: 2202
		public StylePropertyId id;

		// Token: 0x0400089B RID: 2203
		public int durationMs;

		// Token: 0x0400089C RID: 2204
		public int delayMs;

		// Token: 0x0400089D RID: 2205
		public Func<float, float> easingCurve;
	}
}
