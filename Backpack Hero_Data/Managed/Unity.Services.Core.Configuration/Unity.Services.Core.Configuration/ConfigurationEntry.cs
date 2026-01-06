using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Unity.Services.Core.Configuration
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	internal class ConfigurationEntry
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000214C File Offset: 0x0000034C
		[JsonIgnore]
		public string Value
		{
			get
			{
				return this.m_Value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002154 File Offset: 0x00000354
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000215C File Offset: 0x0000035C
		[JsonIgnore]
		public bool IsReadOnly
		{
			get
			{
				return this.m_IsReadOnly;
			}
			internal set
			{
				this.m_IsReadOnly = value;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002165 File Offset: 0x00000365
		public ConfigurationEntry()
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000216D File Offset: 0x0000036D
		public ConfigurationEntry(string value, bool isReadOnly = false)
		{
			this.m_Value = value;
			this.m_IsReadOnly = isReadOnly;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002183 File Offset: 0x00000383
		public bool TrySetValue(string value)
		{
			if (this.IsReadOnly)
			{
				return false;
			}
			this.m_Value = value;
			return true;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002197 File Offset: 0x00000397
		public static implicit operator string(ConfigurationEntry entry)
		{
			return entry.Value;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000219F File Offset: 0x0000039F
		public static implicit operator ConfigurationEntry(string value)
		{
			return new ConfigurationEntry(value, false);
		}

		// Token: 0x04000001 RID: 1
		[JsonRequired]
		[SerializeField]
		private string m_Value;

		// Token: 0x04000002 RID: 2
		[JsonRequired]
		[SerializeField]
		private bool m_IsReadOnly;
	}
}
