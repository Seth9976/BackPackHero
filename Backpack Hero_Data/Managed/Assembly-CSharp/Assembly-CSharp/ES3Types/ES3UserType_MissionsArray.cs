using System;

namespace ES3Types
{
	// Token: 0x020001E0 RID: 480
	public class ES3UserType_MissionsArray : ES3ArrayType
	{
		// Token: 0x06001188 RID: 4488 RVA: 0x000A5948 File Offset: 0x000A3B48
		public ES3UserType_MissionsArray()
			: base(typeof(Missions[]), ES3UserType_Missions.Instance)
		{
			ES3UserType_MissionsArray.Instance = this;
		}

		// Token: 0x04000E0C RID: 3596
		public static ES3Type Instance;
	}
}
