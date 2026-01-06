using System;

namespace ES3Types
{
	// Token: 0x020001D6 RID: 470
	public class ES3UserType_ItemMovementArray : ES3ArrayType
	{
		// Token: 0x06001174 RID: 4468 RVA: 0x000A4F84 File Offset: 0x000A3184
		public ES3UserType_ItemMovementArray()
			: base(typeof(ItemMovement[]), ES3UserType_ItemMovement.Instance)
		{
			ES3UserType_ItemMovementArray.Instance = this;
		}

		// Token: 0x04000E02 RID: 3586
		public static ES3Type Instance;
	}
}
