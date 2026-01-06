using System;
using Unity.Services.Core.Internal;
using UnityEngine.Scripting;

namespace Unity.Services.Authentication.Internal
{
	// Token: 0x0200000B RID: 11
	[RequireImplementors]
	public interface IAccessToken : IServiceComponent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18
		string AccessToken { get; }
	}
}
