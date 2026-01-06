using System;
using System.Collections.Generic;
using System.Globalization;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Configuration
{
	// Token: 0x02000003 RID: 3
	internal static class ConfigurationCollectionHelper
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		public static void FillWith(this IDictionary<string, ConfigurationEntry> self, SerializableProjectConfiguration config)
		{
			for (int i = 0; i < config.Keys.Length; i++)
			{
				string text = config.Keys[i];
				ConfigurationEntry configurationEntry = config.Values[i];
				self.SetOrCreateEntry(text, configurationEntry);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000209C File Offset: 0x0000029C
		public static void FillWith(this IDictionary<string, ConfigurationEntry> self, InitializationOptions options)
		{
			foreach (KeyValuePair<string, object> keyValuePair in options.Values)
			{
				string text = Convert.ToString(keyValuePair.Value, CultureInfo.InvariantCulture);
				self.SetOrCreateEntry(keyValuePair.Key, text);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002108 File Offset: 0x00000308
		private static void SetOrCreateEntry(this IDictionary<string, ConfigurationEntry> self, string key, ConfigurationEntry entry)
		{
			ConfigurationEntry configurationEntry;
			if (self.TryGetValue(key, out configurationEntry))
			{
				if (!configurationEntry.TrySetValue(entry))
				{
					CoreLogger.LogWarning("You are attempting to initialize Operate Solution SDK with an option \"" + key + "\" which is readonly at runtime and can be modified only through Project Settings. The value provided as initialization option will be ignored. Please update InitializationOptions in order to remove this warning.");
					return;
				}
			}
			else
			{
				self[key] = entry;
			}
		}
	}
}
