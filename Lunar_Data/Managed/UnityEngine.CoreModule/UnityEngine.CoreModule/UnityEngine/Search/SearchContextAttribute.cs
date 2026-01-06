using System;

namespace UnityEngine.Search
{
	// Token: 0x020002D1 RID: 721
	[AttributeUsage(256)]
	public class SearchContextAttribute : PropertyAttribute
	{
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x00030856 File Offset: 0x0002EA56
		// (set) Token: 0x06001DD1 RID: 7633 RVA: 0x0003085E File Offset: 0x0002EA5E
		public string query { get; private set; }

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001DD2 RID: 7634 RVA: 0x00030867 File Offset: 0x0002EA67
		// (set) Token: 0x06001DD3 RID: 7635 RVA: 0x0003086F File Offset: 0x0002EA6F
		public string[] providerIds { get; private set; }

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x00030878 File Offset: 0x0002EA78
		// (set) Token: 0x06001DD5 RID: 7637 RVA: 0x00030880 File Offset: 0x0002EA80
		public Type[] instantiableProviders { get; private set; }

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x00030889 File Offset: 0x0002EA89
		// (set) Token: 0x06001DD7 RID: 7639 RVA: 0x00030891 File Offset: 0x0002EA91
		public SearchViewFlags flags { get; private set; }

		// Token: 0x06001DD8 RID: 7640 RVA: 0x0003089A File Offset: 0x0002EA9A
		public SearchContextAttribute(string query)
			: this(query, null, SearchViewFlags.None)
		{
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x000308A7 File Offset: 0x0002EAA7
		public SearchContextAttribute(string query, SearchViewFlags flags)
			: this(query, null, flags)
		{
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x000308B4 File Offset: 0x0002EAB4
		public SearchContextAttribute(string query, string providerIdsCommaSeparated)
			: this(query, providerIdsCommaSeparated, SearchViewFlags.None)
		{
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x000308C1 File Offset: 0x0002EAC1
		public SearchContextAttribute(string query, string providerIdsCommaSeparated, SearchViewFlags flags)
			: this(query, flags, providerIdsCommaSeparated, null)
		{
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x000308CF File Offset: 0x0002EACF
		public SearchContextAttribute(string query, params Type[] instantiableProviders)
			: this(query, SearchViewFlags.None, null, instantiableProviders)
		{
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x000308DD File Offset: 0x0002EADD
		public SearchContextAttribute(string query, SearchViewFlags flags, params Type[] instantiableProviders)
			: this(query, flags, null, instantiableProviders)
		{
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x000308EC File Offset: 0x0002EAEC
		public SearchContextAttribute(string query, SearchViewFlags flags, string providerIdsCommaSeparated, params Type[] instantiableProviders)
		{
			this.query = ((string.IsNullOrEmpty(query) || query.EndsWith(" ")) ? query : (query + " "));
			this.providerIds = ((providerIdsCommaSeparated != null) ? providerIdsCommaSeparated.Split(new char[] { ',', ';' }) : null) ?? new string[0];
			this.instantiableProviders = instantiableProviders ?? new Type[0];
			this.flags = flags;
		}
	}
}
