using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000157 RID: 343
	public static class XGettable
	{
		// Token: 0x0600091C RID: 2332 RVA: 0x00027BD3 File Offset: 0x00025DD3
		public static object GetValue(this IGettable gettable, Type type)
		{
			return ConversionUtility.Convert(gettable.GetValue(), type);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00027BE1 File Offset: 0x00025DE1
		public static T GetValue<T>(this IGettable gettable)
		{
			return (T)((object)gettable.GetValue(typeof(T)));
		}
	}
}
