using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Unity.Services.Core.Configuration
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	internal struct SerializableProjectConfiguration
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002344 File Offset: 0x00000544
		public static SerializableProjectConfiguration Empty
		{
			get
			{
				return new SerializableProjectConfiguration
				{
					Keys = Array.Empty<string>(),
					Values = Array.Empty<ConfigurationEntry>()
				};
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002374 File Offset: 0x00000574
		public SerializableProjectConfiguration(IDictionary<string, ConfigurationEntry> configValues)
		{
			this.Keys = new string[configValues.Count];
			this.Values = new ConfigurationEntry[configValues.Count];
			int num = 0;
			foreach (KeyValuePair<string, ConfigurationEntry> keyValuePair in configValues)
			{
				this.Keys[num] = keyValuePair.Key;
				this.Values[num] = keyValuePair.Value;
				num++;
			}
		}

		// Token: 0x04000008 RID: 8
		[JsonRequired]
		[SerializeField]
		internal string[] Keys;

		// Token: 0x04000009 RID: 9
		[JsonRequired]
		[SerializeField]
		internal ConfigurationEntry[] Values;
	}
}
