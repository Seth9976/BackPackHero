using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x02000023 RID: 35
	public class VariantKeyedCollection<TBase, TImplementation, TKey> : VariantCollection<TBase, TImplementation>, IKeyedCollection<TKey, TBase>, ICollection<TBase>, IEnumerable<TBase>, IEnumerable where TImplementation : TBase
	{
		// Token: 0x0600014F RID: 335 RVA: 0x00004191 File Offset: 0x00002391
		public VariantKeyedCollection(IKeyedCollection<TKey, TImplementation> implementation)
			: base(implementation)
		{
			this.implementation = implementation;
		}

		// Token: 0x17000040 RID: 64
		public TBase this[TKey key]
		{
			get
			{
				return (TBase)((object)this.implementation[key]);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000041B9 File Offset: 0x000023B9
		// (set) Token: 0x06000152 RID: 338 RVA: 0x000041C1 File Offset: 0x000023C1
		public new IKeyedCollection<TKey, TImplementation> implementation { get; private set; }

		// Token: 0x06000153 RID: 339 RVA: 0x000041CC File Offset: 0x000023CC
		public bool TryGetValue(TKey key, out TBase value)
		{
			TImplementation timplementation;
			bool flag = this.implementation.TryGetValue(key, out timplementation);
			value = (TBase)((object)timplementation);
			return flag;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000041F8 File Offset: 0x000023F8
		public bool Contains(TKey key)
		{
			return this.implementation.Contains(key);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00004206 File Offset: 0x00002406
		public bool Remove(TKey key)
		{
			return this.implementation.Remove(key);
		}

		// Token: 0x17000042 RID: 66
		TBase IKeyedCollection<TKey, TBase>.this[int index]
		{
			get
			{
				return (TBase)((object)this.implementation[index]);
			}
		}
	}
}
