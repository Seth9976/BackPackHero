using System;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Configuration.Internal
{
	// Token: 0x02000020 RID: 32
	public interface IProjectConfiguration : IServiceComponent
	{
		// Token: 0x06000060 RID: 96
		bool GetBool(string key, bool defaultValue = false);

		// Token: 0x06000061 RID: 97
		int GetInt(string key, int defaultValue = 0);

		// Token: 0x06000062 RID: 98
		float GetFloat(string key, float defaultValue = 0f);

		// Token: 0x06000063 RID: 99
		string GetString(string key, string defaultValue = null);
	}
}
