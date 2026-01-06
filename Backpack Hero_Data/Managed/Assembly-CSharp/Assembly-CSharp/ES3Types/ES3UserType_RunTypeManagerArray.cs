using System;

namespace ES3Types
{
	// Token: 0x02000202 RID: 514
	public class ES3UserType_RunTypeManagerArray : ES3ArrayType
	{
		// Token: 0x060011CC RID: 4556 RVA: 0x000A7C38 File Offset: 0x000A5E38
		public ES3UserType_RunTypeManagerArray()
			: base(typeof(RunTypeManager[]), ES3UserType_RunTypeManager.Instance)
		{
			ES3UserType_RunTypeManagerArray.Instance = this;
		}

		// Token: 0x04000E2E RID: 3630
		public static ES3Type Instance;
	}
}
