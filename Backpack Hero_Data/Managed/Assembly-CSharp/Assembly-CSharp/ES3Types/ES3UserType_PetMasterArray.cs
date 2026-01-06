using System;

namespace ES3Types
{
	// Token: 0x020001FC RID: 508
	public class ES3UserType_PetMasterArray : ES3ArrayType
	{
		// Token: 0x060011C0 RID: 4544 RVA: 0x000A77EC File Offset: 0x000A59EC
		public ES3UserType_PetMasterArray()
			: base(typeof(PetMaster[]), ES3UserType_PetMaster.Instance)
		{
			ES3UserType_PetMasterArray.Instance = this;
		}

		// Token: 0x04000E28 RID: 3624
		public static ES3Type Instance;
	}
}
