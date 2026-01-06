using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x02000012 RID: 18
	public class FlexibleDictionary<TKey, TValue> : Dictionary<TKey, TValue>
	{
		// Token: 0x1700001C RID: 28
		public TValue this[TKey key]
		{
			get
			{
				return base[key];
			}
			set
			{
				if (base.ContainsKey(key))
				{
					base[key] = value;
					return;
				}
				base.Add(key, value);
			}
		}
	}
}
