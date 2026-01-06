using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x020000BA RID: 186
	public interface IAotStubbable
	{
		// Token: 0x060004A9 RID: 1193
		IEnumerable<object> GetAotStubs(HashSet<object> visited);
	}
}
