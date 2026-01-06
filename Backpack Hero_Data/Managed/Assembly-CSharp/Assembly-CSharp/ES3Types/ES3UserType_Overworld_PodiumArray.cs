using System;

namespace ES3Types
{
	// Token: 0x020001EE RID: 494
	public class ES3UserType_Overworld_PodiumArray : ES3ArrayType
	{
		// Token: 0x060011A4 RID: 4516 RVA: 0x000A6454 File Offset: 0x000A4654
		public ES3UserType_Overworld_PodiumArray()
			: base(typeof(Overworld_Podium[]), ES3UserType_Overworld_Podium.Instance)
		{
			ES3UserType_Overworld_PodiumArray.Instance = this;
		}

		// Token: 0x04000E1A RID: 3610
		public static ES3Type Instance;
	}
}
