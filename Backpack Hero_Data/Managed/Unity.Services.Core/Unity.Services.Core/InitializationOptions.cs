using System;
using System.Collections.Generic;

namespace Unity.Services.Core
{
	// Token: 0x02000005 RID: 5
	public class InitializationOptions
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002118 File Offset: 0x00000318
		internal IDictionary<string, object> Values { get; }

		// Token: 0x0600000C RID: 12 RVA: 0x00002120 File Offset: 0x00000320
		public InitializationOptions()
			: this(new Dictionary<string, object>())
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000212D File Offset: 0x0000032D
		internal InitializationOptions(IDictionary<string, object> values)
		{
			this.Values = values;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000213C File Offset: 0x0000033C
		internal InitializationOptions(InitializationOptions source)
			: this(new Dictionary<string, object>(source.Values))
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000214F File Offset: 0x0000034F
		public bool TryGetOption(string key, out bool option)
		{
			return this.TryGetOption<bool>(key, out option);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002159 File Offset: 0x00000359
		public bool TryGetOption(string key, out int option)
		{
			return this.TryGetOption<int>(key, out option);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002163 File Offset: 0x00000363
		public bool TryGetOption(string key, out float option)
		{
			return this.TryGetOption<float>(key, out option);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000216D File Offset: 0x0000036D
		public bool TryGetOption(string key, out string option)
		{
			return this.TryGetOption<string>(key, out option);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002178 File Offset: 0x00000378
		private bool TryGetOption<T>(string key, out T option)
		{
			option = default(T);
			object obj;
			if (this.Values.TryGetValue(key, out obj) && obj is T)
			{
				T t = (T)((object)obj);
				option = t;
				return true;
			}
			return false;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021B5 File Offset: 0x000003B5
		public InitializationOptions SetOption(string key, bool value)
		{
			this.Values[key] = value;
			return this;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021CA File Offset: 0x000003CA
		public InitializationOptions SetOption(string key, int value)
		{
			this.Values[key] = value;
			return this;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021DF File Offset: 0x000003DF
		public InitializationOptions SetOption(string key, float value)
		{
			this.Values[key] = value;
			return this;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021F4 File Offset: 0x000003F4
		public InitializationOptions SetOption(string key, string value)
		{
			this.Values[key] = value;
			return this;
		}
	}
}
