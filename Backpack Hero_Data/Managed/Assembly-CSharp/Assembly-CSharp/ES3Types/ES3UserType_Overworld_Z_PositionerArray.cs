using System;

namespace ES3Types
{
	// Token: 0x020001F4 RID: 500
	public class ES3UserType_Overworld_Z_PositionerArray : ES3ArrayType
	{
		// Token: 0x060011B0 RID: 4528 RVA: 0x000A6D68 File Offset: 0x000A4F68
		public ES3UserType_Overworld_Z_PositionerArray()
			: base(typeof(Overworld_Z_Positioner[]), ES3UserType_Overworld_Z_Positioner.Instance)
		{
			ES3UserType_Overworld_Z_PositionerArray.Instance = this;
		}

		// Token: 0x04000E20 RID: 3616
		public static ES3Type Instance;
	}
}
