using System;

namespace ES3Types
{
	// Token: 0x020001E2 RID: 482
	public class ES3UserType_ModularBackpackArray : ES3ArrayType
	{
		// Token: 0x0600118C RID: 4492 RVA: 0x000A5CA0 File Offset: 0x000A3EA0
		public ES3UserType_ModularBackpackArray()
			: base(typeof(ModularBackpack[]), ES3UserType_ModularBackpack.Instance)
		{
			ES3UserType_ModularBackpackArray.Instance = this;
		}

		// Token: 0x04000E0E RID: 3598
		public static ES3Type Instance;
	}
}
