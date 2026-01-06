using System;
using System.Threading.Tasks;

namespace Unity.Services.Core.Configuration
{
	// Token: 0x02000007 RID: 7
	internal interface IConfigurationLoader
	{
		// Token: 0x06000015 RID: 21
		Task<SerializableProjectConfiguration> GetConfigAsync();
	}
}
