using System;

namespace ES3Types
{
	// Token: 0x020001C6 RID: 454
	public class ES3UserType_DungeonRoomArray : ES3ArrayType
	{
		// Token: 0x06001154 RID: 4436 RVA: 0x000A31F0 File Offset: 0x000A13F0
		public ES3UserType_DungeonRoomArray()
			: base(typeof(DungeonRoom[]), ES3UserType_DungeonRoom.Instance)
		{
			ES3UserType_DungeonRoomArray.Instance = this;
		}

		// Token: 0x04000DF2 RID: 3570
		public static ES3Type Instance;
	}
}
