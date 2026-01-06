using System;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000CB RID: 203
	[Serializable]
	public class ES3IdRefDictionary : ES3SerializableDictionary<long, Object>
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x0001FCBC File Offset: 0x0001DEBC
		protected override bool KeysAreEqual(long a, long b)
		{
			return a == b;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001FCC2 File Offset: 0x0001DEC2
		protected override bool ValuesAreEqual(Object a, Object b)
		{
			return a == b;
		}
	}
}
