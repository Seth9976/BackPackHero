using System;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x02000127 RID: 295
	internal interface ICondition2<in T, in U>
	{
		// Token: 0x06000911 RID: 2321
		bool Test(T x, U y, ref float t);
	}
}
