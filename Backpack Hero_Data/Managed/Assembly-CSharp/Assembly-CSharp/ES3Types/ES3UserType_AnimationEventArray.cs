using System;

namespace ES3Types
{
	// Token: 0x020001B2 RID: 434
	public class ES3UserType_AnimationEventArray : ES3ArrayType
	{
		// Token: 0x0600112C RID: 4396 RVA: 0x000A1F44 File Offset: 0x000A0144
		public ES3UserType_AnimationEventArray()
			: base(typeof(AnimationEvent[]), ES3UserType_AnimationEvent.Instance)
		{
			ES3UserType_AnimationEventArray.Instance = this;
		}

		// Token: 0x04000DDE RID: 3550
		public static ES3Type Instance;
	}
}
