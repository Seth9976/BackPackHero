using System;

namespace ES3Types
{
	// Token: 0x02000228 RID: 552
	public class ES3UserType_VisualEffectOnItemUseArray : ES3ArrayType
	{
		// Token: 0x06001218 RID: 4632 RVA: 0x000ABF78 File Offset: 0x000AA178
		public ES3UserType_VisualEffectOnItemUseArray()
			: base(typeof(VisualEffectOnItemUse[]), ES3UserType_VisualEffectOnItemUse.Instance)
		{
			ES3UserType_VisualEffectOnItemUseArray.Instance = this;
		}

		// Token: 0x04000E54 RID: 3668
		public static ES3Type Instance;
	}
}
