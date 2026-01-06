using System;
using System.Collections.Generic;

namespace ES3Types
{
	// Token: 0x02000037 RID: 55
	public class ES3Type_ES3RefDictionary : ES3DictionaryType
	{
		// Token: 0x0600025B RID: 603 RVA: 0x000091A9 File Offset: 0x000073A9
		public ES3Type_ES3RefDictionary()
			: base(typeof(Dictionary<ES3Ref, ES3Ref>), ES3Type_ES3Ref.Instance, ES3Type_ES3Ref.Instance)
		{
			ES3Type_ES3RefDictionary.Instance = this;
		}

		// Token: 0x0400006F RID: 111
		public static ES3Type Instance = new ES3Type_ES3RefDictionary();
	}
}
