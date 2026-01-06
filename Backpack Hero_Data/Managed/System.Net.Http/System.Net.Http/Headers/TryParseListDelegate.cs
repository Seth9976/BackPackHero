using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	// Token: 0x0200003D RID: 61
	// (Invoke) Token: 0x0600021C RID: 540
	internal delegate bool TryParseListDelegate<T>(string value, int minimalCount, out List<T> result);
}
