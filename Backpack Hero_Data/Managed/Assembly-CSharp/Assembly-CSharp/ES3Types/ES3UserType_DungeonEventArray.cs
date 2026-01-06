using System;

namespace ES3Types
{
	// Token: 0x020001C2 RID: 450
	public class ES3UserType_DungeonEventArray : ES3ArrayType
	{
		// Token: 0x0600114C RID: 4428 RVA: 0x000A2C04 File Offset: 0x000A0E04
		public ES3UserType_DungeonEventArray()
			: base(typeof(DungeonEvent[]), ES3UserType_DungeonEvent.Instance)
		{
			ES3UserType_DungeonEventArray.Instance = this;
		}

		// Token: 0x04000DEE RID: 3566
		public static ES3Type Instance;
	}
}
