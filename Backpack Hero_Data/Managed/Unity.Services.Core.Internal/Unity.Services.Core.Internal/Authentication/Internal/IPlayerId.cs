using System;
using Unity.Services.Core.Internal;
using UnityEngine.Scripting;

namespace Unity.Services.Authentication.Internal
{
	// Token: 0x0200000D RID: 13
	[RequireImplementors]
	public interface IPlayerId : IServiceComponent
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20
		string PlayerId { get; }

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000015 RID: 21
		// (remove) Token: 0x06000016 RID: 22
		event Action<string> PlayerIdChanged;
	}
}
