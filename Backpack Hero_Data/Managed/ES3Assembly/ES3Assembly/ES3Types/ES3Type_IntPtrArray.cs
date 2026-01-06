using System;

namespace ES3Types
{
	// Token: 0x0200003D RID: 61
	public class ES3Type_IntPtrArray : ES3ArrayType
	{
		// Token: 0x06000268 RID: 616 RVA: 0x000092E7 File Offset: 0x000074E7
		public ES3Type_IntPtrArray()
			: base(typeof(IntPtr[]), ES3Type_IntPtr.Instance)
		{
			ES3Type_IntPtrArray.Instance = this;
		}

		// Token: 0x04000075 RID: 117
		public static ES3Type Instance;
	}
}
