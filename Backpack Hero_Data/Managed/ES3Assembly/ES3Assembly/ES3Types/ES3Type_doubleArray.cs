using System;

namespace ES3Types
{
	// Token: 0x02000033 RID: 51
	public class ES3Type_doubleArray : ES3ArrayType
	{
		// Token: 0x06000251 RID: 593 RVA: 0x00008C18 File Offset: 0x00006E18
		public ES3Type_doubleArray()
			: base(typeof(double[]), ES3Type_double.Instance)
		{
			ES3Type_doubleArray.Instance = this;
		}

		// Token: 0x0400006A RID: 106
		public static ES3Type Instance;
	}
}
