using System;

namespace ES3Types
{
	// Token: 0x020001F2 RID: 498
	public class ES3UserType_Overworld_StructureArray : ES3ArrayType
	{
		// Token: 0x060011AC RID: 4524 RVA: 0x000A6C8C File Offset: 0x000A4E8C
		public ES3UserType_Overworld_StructureArray()
			: base(typeof(Overworld_Structure[]), ES3UserType_Overworld_Structure.Instance)
		{
			ES3UserType_Overworld_StructureArray.Instance = this;
		}

		// Token: 0x04000E1E RID: 3614
		public static ES3Type Instance;
	}
}
