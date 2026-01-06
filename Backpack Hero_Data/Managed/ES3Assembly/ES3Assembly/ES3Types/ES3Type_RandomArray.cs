using System;

namespace ES3Types
{
	// Token: 0x02000027 RID: 39
	public class ES3Type_RandomArray : ES3ArrayType
	{
		// Token: 0x06000237 RID: 567 RVA: 0x0000898C File Offset: 0x00006B8C
		public ES3Type_RandomArray()
			: base(typeof(Random[]), ES3Type_Random.Instance)
		{
			ES3Type_RandomArray.Instance = this;
		}

		// Token: 0x0400005E RID: 94
		public static ES3Type Instance;
	}
}
