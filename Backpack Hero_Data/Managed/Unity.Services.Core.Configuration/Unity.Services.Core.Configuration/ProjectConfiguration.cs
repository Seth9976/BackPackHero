using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Configuration
{
	// Token: 0x02000009 RID: 9
	internal class ProjectConfiguration : IProjectConfiguration, IServiceComponent
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002222 File Offset: 0x00000422
		public ProjectConfiguration(IReadOnlyDictionary<string, ConfigurationEntry> configValues)
		{
			this.m_ConfigValues = configValues;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002234 File Offset: 0x00000434
		public bool GetBool(string key, bool defaultValue = false)
		{
			bool flag;
			if (bool.TryParse(this.GetString(key, null), out flag))
			{
				return flag;
			}
			return defaultValue;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002258 File Offset: 0x00000458
		public int GetInt(string key, int defaultValue = 0)
		{
			int num;
			if (int.TryParse(this.GetString(key, null), out num))
			{
				return num;
			}
			return defaultValue;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000227C File Offset: 0x0000047C
		public float GetFloat(string key, float defaultValue = 0f)
		{
			float num;
			if (float.TryParse(this.GetString(key, null), NumberStyles.Float, CultureInfo.InvariantCulture, out num))
			{
				return num;
			}
			return defaultValue;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022A8 File Offset: 0x000004A8
		public string GetString(string key, string defaultValue = null)
		{
			ConfigurationEntry configurationEntry;
			if (this.m_ConfigValues.TryGetValue(key, out configurationEntry))
			{
				return configurationEntry.Value;
			}
			return defaultValue;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022D0 File Offset: 0x000004D0
		public string ToJson()
		{
			if (this.m_JsonCache == null)
			{
				Dictionary<string, string> dictionary = this.m_ConfigValues.ToDictionary((KeyValuePair<string, ConfigurationEntry> pair) => pair.Key, (KeyValuePair<string, ConfigurationEntry> pair) => pair.Value.Value);
				this.m_JsonCache = JsonConvert.SerializeObject(dictionary);
			}
			return this.m_JsonCache;
		}

		// Token: 0x04000006 RID: 6
		private string m_JsonCache;

		// Token: 0x04000007 RID: 7
		private readonly IReadOnlyDictionary<string, ConfigurationEntry> m_ConfigValues;
	}
}
