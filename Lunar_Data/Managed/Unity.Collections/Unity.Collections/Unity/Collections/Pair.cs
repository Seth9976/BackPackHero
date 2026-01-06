using System;

namespace Unity.Collections
{
	// Token: 0x02000045 RID: 69
	internal struct Pair<Key, Value>
	{
		// Token: 0x06000136 RID: 310 RVA: 0x000047B2 File Offset: 0x000029B2
		public Pair(Key k, Value v)
		{
			this.key = k;
			this.value = v;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000047C2 File Offset: 0x000029C2
		public override string ToString()
		{
			return string.Format("{0} = {1}", this.key, this.value);
		}

		// Token: 0x0400009A RID: 154
		public Key key;

		// Token: 0x0400009B RID: 155
		public Value value;
	}
}
