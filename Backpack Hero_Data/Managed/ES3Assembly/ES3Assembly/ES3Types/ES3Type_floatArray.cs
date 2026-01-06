using System;

namespace ES3Types
{
	// Token: 0x02000039 RID: 57
	public class ES3Type_floatArray : ES3ArrayType
	{
		// Token: 0x06000260 RID: 608 RVA: 0x0000921B File Offset: 0x0000741B
		public ES3Type_floatArray()
			: base(typeof(float[]), ES3Type_float.Instance)
		{
			ES3Type_floatArray.Instance = this;
		}

		// Token: 0x04000071 RID: 113
		public static ES3Type Instance;
	}
}
