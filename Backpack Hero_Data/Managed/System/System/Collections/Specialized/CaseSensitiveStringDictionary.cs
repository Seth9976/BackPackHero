using System;

namespace System.Collections.Specialized
{
	// Token: 0x020007CD RID: 1997
	internal class CaseSensitiveStringDictionary : StringDictionary
	{
		// Token: 0x17000E76 RID: 3702
		public override string this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return (string)this.contents[key];
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.contents[key] = value;
			}
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x000DE78C File Offset: 0x000DC98C
		public override void Add(string key, string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Add(key, value);
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x000DE7A9 File Offset: 0x000DC9A9
		public override bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.contents.ContainsKey(key);
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x000DE7C5 File Offset: 0x000DC9C5
		public override void Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Remove(key);
		}
	}
}
