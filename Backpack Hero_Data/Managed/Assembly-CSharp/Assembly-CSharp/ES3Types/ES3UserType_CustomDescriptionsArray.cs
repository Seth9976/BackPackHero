using System;

namespace ES3Types
{
	// Token: 0x020001BC RID: 444
	public class ES3UserType_CustomDescriptionsArray : ES3ArrayType
	{
		// Token: 0x06001140 RID: 4416 RVA: 0x000A260C File Offset: 0x000A080C
		public ES3UserType_CustomDescriptionsArray()
			: base(typeof(CustomDescriptions[]), ES3UserType_CustomDescriptions.Instance)
		{
			ES3UserType_CustomDescriptionsArray.Instance = this;
		}

		// Token: 0x04000DE8 RID: 3560
		public static ES3Type Instance;
	}
}
