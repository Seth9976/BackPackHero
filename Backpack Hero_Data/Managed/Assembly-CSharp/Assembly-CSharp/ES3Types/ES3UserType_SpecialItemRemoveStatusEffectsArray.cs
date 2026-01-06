using System;

namespace ES3Types
{
	// Token: 0x02000212 RID: 530
	public class ES3UserType_SpecialItemRemoveStatusEffectsArray : ES3ArrayType
	{
		// Token: 0x060011EC RID: 4588 RVA: 0x000A8D48 File Offset: 0x000A6F48
		public ES3UserType_SpecialItemRemoveStatusEffectsArray()
			: base(typeof(SpecialItemRemoveStatusEffects[]), ES3UserType_SpecialItemRemoveStatusEffects.Instance)
		{
			ES3UserType_SpecialItemRemoveStatusEffectsArray.Instance = this;
		}

		// Token: 0x04000E3E RID: 3646
		public static ES3Type Instance;
	}
}
