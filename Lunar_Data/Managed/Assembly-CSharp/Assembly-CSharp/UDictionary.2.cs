using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A1 RID: 161
[Serializable]
public class UDictionary<TKey, TValue> : UDictionary, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
{
	// Token: 0x17000010 RID: 16
	// (get) Token: 0x06000446 RID: 1094 RVA: 0x00015426 File Offset: 0x00013626
	public List<TKey> Keys
	{
		get
		{
			return this.keys;
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x06000447 RID: 1095 RVA: 0x0001542E File Offset: 0x0001362E
	ICollection<TKey> IDictionary<TKey, TValue>.Keys
	{
		get
		{
			return this.keys;
		}
	}

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x06000448 RID: 1096 RVA: 0x00015436 File Offset: 0x00013636
	public List<TValue> Values
	{
		get
		{
			return this.values;
		}
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x06000449 RID: 1097 RVA: 0x0001543E File Offset: 0x0001363E
	ICollection<TValue> IDictionary<TKey, TValue>.Values
	{
		get
		{
			return this.values;
		}
	}

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x0600044A RID: 1098 RVA: 0x00015446 File Offset: 0x00013646
	public int Count
	{
		get
		{
			return this.keys.Count;
		}
	}

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x0600044B RID: 1099 RVA: 0x00015453 File Offset: 0x00013653
	public bool IsReadOnly
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x0600044C RID: 1100 RVA: 0x00015456 File Offset: 0x00013656
	public bool Cached
	{
		get
		{
			return this.cache != null;
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x0600044D RID: 1101 RVA: 0x00015464 File Offset: 0x00013664
	public Dictionary<TKey, TValue> Dictionary
	{
		get
		{
			if (this.cache == null)
			{
				this.cache = new Dictionary<TKey, TValue>();
				for (int i = 0; i < this.keys.Count; i++)
				{
					if (this.keys[i] != null && !this.cache.ContainsKey(this.keys[i]))
					{
						this.cache.Add(this.keys[i], this.values[i]);
					}
				}
			}
			return this.cache;
		}
	}

	// Token: 0x17000018 RID: 24
	public TValue this[TKey key]
	{
		get
		{
			return this.Dictionary[key];
		}
		set
		{
			int num = this.keys.IndexOf(key);
			if (num < 0)
			{
				this.Add(key, value);
				return;
			}
			this.values[num] = value;
			if (this.Cached)
			{
				this.Dictionary[key] = value;
			}
		}
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x00015549 File Offset: 0x00013749
	public bool TryGetValue(TKey key, out TValue value)
	{
		return this.Dictionary.TryGetValue(key, out value);
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x00015558 File Offset: 0x00013758
	public bool ContainsKey(TKey key)
	{
		return this.Dictionary.ContainsKey(key);
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x00015566 File Offset: 0x00013766
	public bool Contains(KeyValuePair<TKey, TValue> item)
	{
		return this.ContainsKey(item.Key);
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x00015575 File Offset: 0x00013775
	public void Add(TKey key, TValue value)
	{
		this.keys.Add(key);
		this.values.Add(value);
		if (this.Cached)
		{
			this.Dictionary.Add(key, value);
		}
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x000155A4 File Offset: 0x000137A4
	public void Add(KeyValuePair<TKey, TValue> item)
	{
		this.Add(item.Key, item.Value);
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x000155BC File Offset: 0x000137BC
	public bool Remove(TKey key)
	{
		int num = this.keys.IndexOf(key);
		if (num < 0)
		{
			return false;
		}
		this.keys.RemoveAt(num);
		this.values.RemoveAt(num);
		if (this.Cached)
		{
			this.Dictionary.Remove(key);
		}
		return true;
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x0001560A File Offset: 0x0001380A
	public bool Remove(KeyValuePair<TKey, TValue> item)
	{
		return this.Remove(item.Key);
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x00015619 File Offset: 0x00013819
	public void Clear()
	{
		this.keys.Clear();
		this.values.Clear();
		if (this.Cached)
		{
			this.Dictionary.Clear();
		}
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x00015644 File Offset: 0x00013844
	public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
	{
		((ICollection)this.Dictionary).CopyTo(array, arrayIndex);
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x00015653 File Offset: 0x00013853
	public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
	{
		return this.Dictionary.GetEnumerator();
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x00015665 File Offset: 0x00013865
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.Dictionary.GetEnumerator();
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x00015677 File Offset: 0x00013877
	public UDictionary()
	{
		this.values = new List<TValue>();
		this.keys = new List<TKey>();
	}

	// Token: 0x0400034D RID: 845
	[SerializeField]
	private List<TKey> keys;

	// Token: 0x0400034E RID: 846
	[SerializeField]
	private List<TValue> values;

	// Token: 0x0400034F RID: 847
	private Dictionary<TKey, TValue> cache;
}
