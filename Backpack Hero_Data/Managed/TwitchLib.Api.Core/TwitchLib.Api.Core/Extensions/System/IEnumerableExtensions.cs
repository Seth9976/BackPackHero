using System;
using System.Collections.Generic;

namespace TwitchLib.Api.Core.Extensions.System
{
	// Token: 0x02000011 RID: 17
	public static class IEnumerableExtensions
	{
		// Token: 0x06000069 RID: 105 RVA: 0x000035E7 File Offset: 0x000017E7
		public static void AddTo<T>(this IEnumerable<T> source, List<T> destination)
		{
			if (source != null)
			{
				destination.AddRange(source);
			}
		}
	}
}
