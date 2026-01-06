using System;

namespace ES3Types
{
	// Token: 0x020001B8 RID: 440
	public class ES3UserType_CircleRendererArray : ES3ArrayType
	{
		// Token: 0x06001138 RID: 4408 RVA: 0x000A2380 File Offset: 0x000A0580
		public ES3UserType_CircleRendererArray()
			: base(typeof(CircleRenderer[]), ES3UserType_CircleRenderer.Instance)
		{
			ES3UserType_CircleRendererArray.Instance = this;
		}

		// Token: 0x04000DE4 RID: 3556
		public static ES3Type Instance;
	}
}
