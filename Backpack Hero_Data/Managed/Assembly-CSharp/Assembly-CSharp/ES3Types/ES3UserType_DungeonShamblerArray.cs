using System;

namespace ES3Types
{
	// Token: 0x020001CA RID: 458
	public class ES3UserType_DungeonShamblerArray : ES3ArrayType
	{
		// Token: 0x0600115C RID: 4444 RVA: 0x000A3448 File Offset: 0x000A1648
		public ES3UserType_DungeonShamblerArray()
			: base(typeof(DungeonShambler[]), ES3UserType_DungeonShambler.Instance)
		{
			ES3UserType_DungeonShamblerArray.Instance = this;
		}

		// Token: 0x04000DF6 RID: 3574
		public static ES3Type Instance;
	}
}
