using System;

namespace ES3Types
{
	// Token: 0x020001C0 RID: 448
	public class ES3UserType_DragColliderArray : ES3ArrayType
	{
		// Token: 0x06001148 RID: 4424 RVA: 0x000A2790 File Offset: 0x000A0990
		public ES3UserType_DragColliderArray()
			: base(typeof(DragCollider[]), ES3UserType_DragCollider.Instance)
		{
			ES3UserType_DragColliderArray.Instance = this;
		}

		// Token: 0x04000DEC RID: 3564
		public static ES3Type Instance;
	}
}
