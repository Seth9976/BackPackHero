using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000131 RID: 305
	[IncludeInSettings(false)]
	public sealed class DictionaryAsset : LudiqScriptableObject, IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x17000189 RID: 393
		public object this[string key]
		{
			get
			{
				return this.dictionary[key];
			}
			set
			{
				this.dictionary[key] = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x000256D2 File Offset: 0x000238D2
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x000256DA File Offset: 0x000238DA
		[Serialize]
		public Dictionary<string, object> dictionary { get; private set; } = new Dictionary<string, object>();

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x000256E3 File Offset: 0x000238E3
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x000256F0 File Offset: 0x000238F0
		public ICollection<string> Keys
		{
			get
			{
				return this.dictionary.Keys;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x000256FD File Offset: 0x000238FD
		public ICollection<object> Values
		{
			get
			{
				return this.dictionary.Values;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x0002570A File Offset: 0x0002390A
		bool ICollection<KeyValuePair<string, object>>.IsReadOnly
		{
			get
			{
				return ((ICollection<KeyValuePair<string, object>>)this.dictionary).IsReadOnly;
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00025717 File Offset: 0x00023917
		protected override void OnAfterDeserialize()
		{
			base.OnAfterDeserialize();
			if (this.dictionary == null)
			{
				this.dictionary = new Dictionary<string, object>();
			}
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00025732 File Offset: 0x00023932
		public void Clear()
		{
			this.dictionary.Clear();
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0002573F File Offset: 0x0002393F
		public bool ContainsKey(string key)
		{
			return this.dictionary.ContainsKey(key);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0002574D File Offset: 0x0002394D
		public void Add(string key, object value)
		{
			this.dictionary.Add(key, value);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0002575C File Offset: 0x0002395C
		public void Merge(DictionaryAsset other, bool overwriteExisting = true)
		{
			foreach (string text in other.Keys)
			{
				if (overwriteExisting)
				{
					this.dictionary[text] = other[text];
				}
				else if (!this.dictionary.ContainsKey(text))
				{
					this.dictionary.Add(text, other[text]);
				}
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000257DC File Offset: 0x000239DC
		public bool Remove(string key)
		{
			return this.dictionary.Remove(key);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x000257EA File Offset: 0x000239EA
		public bool TryGetValue(string key, out object value)
		{
			return this.dictionary.TryGetValue(key, out value);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x000257F9 File Offset: 0x000239F9
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return this.dictionary.GetEnumerator();
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0002580B File Offset: 0x00023A0B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.dictionary).GetEnumerator();
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00025818 File Offset: 0x00023A18
		void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
		{
			((ICollection<KeyValuePair<string, object>>)this.dictionary).Add(item);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00025826 File Offset: 0x00023A26
		bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
		{
			return ((ICollection<KeyValuePair<string, object>>)this.dictionary).Contains(item);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00025834 File Offset: 0x00023A34
		void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<string, object>>)this.dictionary).CopyTo(array, arrayIndex);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00025843 File Offset: 0x00023A43
		bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
		{
			return ((ICollection<KeyValuePair<string, object>>)this.dictionary).Remove(item);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00025851 File Offset: 0x00023A51
		[ContextMenu("Show Data...")]
		protected override void ShowData()
		{
			base.ShowData();
		}
	}
}
