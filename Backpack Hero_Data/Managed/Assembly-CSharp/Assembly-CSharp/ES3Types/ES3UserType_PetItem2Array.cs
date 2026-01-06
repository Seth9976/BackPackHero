using System;

namespace ES3Types
{
	// Token: 0x020001FA RID: 506
	public class ES3UserType_PetItem2Array : ES3ArrayType
	{
		// Token: 0x060011BC RID: 4540 RVA: 0x000A71F8 File Offset: 0x000A53F8
		public ES3UserType_PetItem2Array()
			: base(typeof(PetItem2[]), ES3UserType_PetItem2.Instance)
		{
			ES3UserType_PetItem2Array.Instance = this;
		}

		// Token: 0x04000E26 RID: 3622
		public static ES3Type Instance;
	}
}
