using System;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.RateLimiter;

namespace TwitchLib.Api.Core.Extensions.RateLimiter
{
	// Token: 0x02000012 RID: 18
	public static class IAwaitableConstraintExtension
	{
		// Token: 0x0600006A RID: 106 RVA: 0x000035F4 File Offset: 0x000017F4
		public static IAwaitableConstraint Compose(this IAwaitableConstraint ac1, IAwaitableConstraint ac2)
		{
			if (ac1 != ac2)
			{
				return new ComposedAwaitableConstraint(ac1, ac2);
			}
			return ac1;
		}
	}
}
