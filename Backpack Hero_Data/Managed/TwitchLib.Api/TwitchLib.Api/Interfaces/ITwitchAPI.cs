using System;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.Undocumented;
using TwitchLib.Api.Helix;
using TwitchLib.Api.ThirdParty;

namespace TwitchLib.Api.Interfaces
{
	// Token: 0x02000021 RID: 33
	public interface ITwitchAPI
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B7 RID: 183
		IApiSettings Settings { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000B8 RID: 184
		Helix Helix { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B9 RID: 185
		ThirdParty ThirdParty { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000BA RID: 186
		Undocumented Undocumented { get; }
	}
}
