using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000C4 RID: 196
	public class JTokenEqualityComparer : IEqualityComparer<JToken>
	{
		// Token: 0x06000B52 RID: 2898 RVA: 0x0002C6D6 File Offset: 0x0002A8D6
		[NullableContext(2)]
		public bool Equals(JToken x, JToken y)
		{
			return JToken.DeepEquals(x, y);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0002C6DF File Offset: 0x0002A8DF
		[NullableContext(1)]
		public int GetHashCode(JToken obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetDeepHashCode();
		}
	}
}
