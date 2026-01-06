using System;
using System.Collections;

namespace Unity.Collections
{
	// Token: 0x02000046 RID: 70
	internal struct ListPair<Key, Value> where Value : IList
	{
		// Token: 0x06000138 RID: 312 RVA: 0x000047E4 File Offset: 0x000029E4
		public ListPair(Key k, Value v)
		{
			this.key = k;
			this.value = v;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000047F4 File Offset: 0x000029F4
		public override string ToString()
		{
			string text = string.Format("{0} = [", this.key);
			for (int i = 0; i < this.value.Count; i++)
			{
				string text2 = text;
				object obj = this.value[i];
				text = text2 + ((obj != null) ? obj.ToString() : null);
				if (i < this.value.Count - 1)
				{
					text += ", ";
				}
			}
			return text + "]";
		}

		// Token: 0x0400009C RID: 156
		public Key key;

		// Token: 0x0400009D RID: 157
		public Value value;
	}
}
