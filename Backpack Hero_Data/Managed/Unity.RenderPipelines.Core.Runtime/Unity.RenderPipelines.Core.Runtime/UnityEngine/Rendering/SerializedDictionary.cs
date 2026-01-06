using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000063 RID: 99
	[Serializable]
	public class SerializedDictionary<K, V> : SerializedDictionary<K, V, K, V>
	{
		// Token: 0x06000320 RID: 800 RVA: 0x0000F1D1 File Offset: 0x0000D3D1
		public override K SerializeKey(K key)
		{
			return key;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000F1D4 File Offset: 0x0000D3D4
		public override V SerializeValue(V val)
		{
			return val;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000F1D7 File Offset: 0x0000D3D7
		public override K DeserializeKey(K key)
		{
			return key;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000F1DA File Offset: 0x0000D3DA
		public override V DeserializeValue(V val)
		{
			return val;
		}
	}
}
