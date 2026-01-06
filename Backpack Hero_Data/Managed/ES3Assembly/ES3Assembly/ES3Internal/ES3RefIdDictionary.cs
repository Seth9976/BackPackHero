using System;
using System.ComponentModel;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000CC RID: 204
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public class ES3RefIdDictionary : ES3SerializableDictionary<Object, long>
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x0001FCD3 File Offset: 0x0001DED3
		protected override bool KeysAreEqual(Object a, Object b)
		{
			return a == b;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001FCDC File Offset: 0x0001DEDC
		protected override bool ValuesAreEqual(long a, long b)
		{
			return a == b;
		}
	}
}
