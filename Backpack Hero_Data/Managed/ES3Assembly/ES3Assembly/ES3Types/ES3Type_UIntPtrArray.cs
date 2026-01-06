using System;

namespace ES3Types
{
	// Token: 0x02000049 RID: 73
	public class ES3Type_UIntPtrArray : ES3ArrayType
	{
		// Token: 0x06000280 RID: 640 RVA: 0x00009514 File Offset: 0x00007714
		public ES3Type_UIntPtrArray()
			: base(typeof(UIntPtr[]), ES3Type_UIntPtr.Instance)
		{
			ES3Type_UIntPtrArray.Instance = this;
		}

		// Token: 0x04000081 RID: 129
		public static ES3Type Instance;
	}
}
