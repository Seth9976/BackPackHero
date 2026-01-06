using System;

namespace UnityEngine.U2D.Common.UTess
{
	// Token: 0x02000012 RID: 18
	internal interface ICondition2<in T, in U>
	{
		// Token: 0x0600004E RID: 78
		bool Test(T x, U y, ref float t);
	}
}
