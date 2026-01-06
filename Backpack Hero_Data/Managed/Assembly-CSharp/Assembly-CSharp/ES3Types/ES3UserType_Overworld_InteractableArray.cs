using System;

namespace ES3Types
{
	// Token: 0x020001E8 RID: 488
	public class ES3UserType_Overworld_InteractableArray : ES3ArrayType
	{
		// Token: 0x06001198 RID: 4504 RVA: 0x000A60C4 File Offset: 0x000A42C4
		public ES3UserType_Overworld_InteractableArray()
			: base(typeof(Overworld_Interactable[]), ES3UserType_Overworld_Interactable.Instance)
		{
			ES3UserType_Overworld_InteractableArray.Instance = this;
		}

		// Token: 0x04000E14 RID: 3604
		public static ES3Type Instance;
	}
}
