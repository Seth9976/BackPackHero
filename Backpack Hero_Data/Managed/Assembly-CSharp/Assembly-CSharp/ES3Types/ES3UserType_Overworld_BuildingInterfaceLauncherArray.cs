using System;

namespace ES3Types
{
	// Token: 0x020001E4 RID: 484
	public class ES3UserType_Overworld_BuildingInterfaceLauncherArray : ES3ArrayType
	{
		// Token: 0x06001190 RID: 4496 RVA: 0x000A5E28 File Offset: 0x000A4028
		public ES3UserType_Overworld_BuildingInterfaceLauncherArray()
			: base(typeof(Overworld_BuildingInterfaceLauncher[]), ES3UserType_Overworld_BuildingInterfaceLauncher.Instance)
		{
			ES3UserType_Overworld_BuildingInterfaceLauncherArray.Instance = this;
		}

		// Token: 0x04000E10 RID: 3600
		public static ES3Type Instance;
	}
}
