using System;

namespace ES3Types
{
	// Token: 0x020001DE RID: 478
	public class ES3UserType_ManaStoneArray : ES3ArrayType
	{
		// Token: 0x06001184 RID: 4484 RVA: 0x000A573C File Offset: 0x000A393C
		public ES3UserType_ManaStoneArray()
			: base(typeof(ManaStone[]), ES3UserType_ManaStone.Instance)
		{
			ES3UserType_ManaStoneArray.Instance = this;
		}

		// Token: 0x04000E0A RID: 3594
		public static ES3Type Instance;
	}
}
