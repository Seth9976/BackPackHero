using System;

namespace ES3Types
{
	// Token: 0x0200003B RID: 59
	public class ES3Type_intArray : ES3ArrayType
	{
		// Token: 0x06000264 RID: 612 RVA: 0x0000927C File Offset: 0x0000747C
		public ES3Type_intArray()
			: base(typeof(int[]), ES3Type_int.Instance)
		{
			ES3Type_intArray.Instance = this;
		}

		// Token: 0x04000073 RID: 115
		public static ES3Type Instance;
	}
}
