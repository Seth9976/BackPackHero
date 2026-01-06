using System;

namespace ES3Types
{
	// Token: 0x02000200 RID: 512
	public class ES3UserType_RunTypeArray : ES3ArrayType
	{
		// Token: 0x060011C8 RID: 4552 RVA: 0x000A7AF0 File Offset: 0x000A5CF0
		public ES3UserType_RunTypeArray()
			: base(typeof(RunType[]), ES3UserType_RunType.Instance)
		{
			ES3UserType_RunTypeArray.Instance = this;
		}

		// Token: 0x04000E2C RID: 3628
		public static ES3Type Instance;
	}
}
