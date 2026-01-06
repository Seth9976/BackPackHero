using System;

namespace ES3Types
{
	// Token: 0x020001C4 RID: 452
	public class ES3UserType_DungeonPlayerArray : ES3ArrayType
	{
		// Token: 0x06001150 RID: 4432 RVA: 0x000A2DB8 File Offset: 0x000A0FB8
		public ES3UserType_DungeonPlayerArray()
			: base(typeof(DungeonPlayer[]), ES3UserType_DungeonPlayer.Instance)
		{
			ES3UserType_DungeonPlayerArray.Instance = this;
		}

		// Token: 0x04000DF0 RID: 3568
		public static ES3Type Instance;
	}
}
