using System;

namespace ES3Types
{
	// Token: 0x020001EA RID: 490
	public class ES3UserType_OVerworld_NPCDestinationArray : ES3ArrayType
	{
		// Token: 0x0600119C RID: 4508 RVA: 0x000A61F4 File Offset: 0x000A43F4
		public ES3UserType_OVerworld_NPCDestinationArray()
			: base(typeof(OVerworld_NPCDestination[]), ES3UserType_OVerworld_NPCDestination.Instance)
		{
			ES3UserType_OVerworld_NPCDestinationArray.Instance = this;
		}

		// Token: 0x04000E16 RID: 3606
		public static ES3Type Instance;
	}
}
