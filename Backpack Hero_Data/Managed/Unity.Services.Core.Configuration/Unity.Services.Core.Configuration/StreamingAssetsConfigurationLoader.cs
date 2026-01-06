using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Unity.Services.Core.Configuration
{
	// Token: 0x0200000B RID: 11
	internal class StreamingAssetsConfigurationLoader : IConfigurationLoader
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000023FC File Offset: 0x000005FC
		public async Task<SerializableProjectConfiguration> GetConfigAsync()
		{
			return JsonConvert.DeserializeObject<SerializableProjectConfiguration>(await StreamingAssetsUtils.GetFileTextFromStreamingAssetsAsync("UnityServicesProjectConfiguration.json"));
		}
	}
}
