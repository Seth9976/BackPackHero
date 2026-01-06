using System;
using System.Collections.Generic;
using Unity.Services.Core.Configuration.Internal;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000022 RID: 34
	internal static class FactoryUtils
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00003358 File Offset: 0x00001558
		public static IDictionary<string, string> CreatePackageTags(IProjectConfiguration projectConfig, string packageName)
		{
			string @string = projectConfig.GetString(string.Format("{0}.version", packageName), string.Empty);
			string.IsNullOrEmpty(@string);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["package_name"] = packageName;
			dictionary["package_version"] = @string;
			return dictionary;
		}

		// Token: 0x04000045 RID: 69
		internal const string PackageVersionKeyFormat = "{0}.version";
	}
}
