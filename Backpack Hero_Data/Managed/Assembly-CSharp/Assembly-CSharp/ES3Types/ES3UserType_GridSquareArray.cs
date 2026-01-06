using System;

namespace ES3Types
{
	// Token: 0x020001D0 RID: 464
	public class ES3UserType_GridSquareArray : ES3ArrayType
	{
		// Token: 0x06001168 RID: 4456 RVA: 0x000A3C1C File Offset: 0x000A1E1C
		public ES3UserType_GridSquareArray()
			: base(typeof(GridSquare[]), ES3UserType_GridSquare.Instance)
		{
			ES3UserType_GridSquareArray.Instance = this;
		}

		// Token: 0x04000DFC RID: 3580
		public static ES3Type Instance;
	}
}
