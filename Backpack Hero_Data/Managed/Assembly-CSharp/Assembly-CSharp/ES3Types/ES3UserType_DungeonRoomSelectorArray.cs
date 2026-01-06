using System;

namespace ES3Types
{
	// Token: 0x020001C8 RID: 456
	public class ES3UserType_DungeonRoomSelectorArray : ES3ArrayType
	{
		// Token: 0x06001158 RID: 4440 RVA: 0x000A3300 File Offset: 0x000A1500
		public ES3UserType_DungeonRoomSelectorArray()
			: base(typeof(DungeonRoomSelector[]), ES3UserType_DungeonRoomSelector.Instance)
		{
			ES3UserType_DungeonRoomSelectorArray.Instance = this;
		}

		// Token: 0x04000DF4 RID: 3572
		public static ES3Type Instance;
	}
}
