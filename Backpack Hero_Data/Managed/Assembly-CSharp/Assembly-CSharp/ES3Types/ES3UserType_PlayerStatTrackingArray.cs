using System;

namespace ES3Types
{
	// Token: 0x020001FE RID: 510
	public class ES3UserType_PlayerStatTrackingArray : ES3ArrayType
	{
		// Token: 0x060011C4 RID: 4548 RVA: 0x000A7918 File Offset: 0x000A5B18
		public ES3UserType_PlayerStatTrackingArray()
			: base(typeof(PlayerStatTracking[]), ES3UserType_PlayerStatTracking.Instance)
		{
			ES3UserType_PlayerStatTrackingArray.Instance = this;
		}

		// Token: 0x04000E2A RID: 3626
		public static ES3Type Instance;
	}
}
