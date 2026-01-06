using System;

namespace ES3Types
{
	// Token: 0x02000204 RID: 516
	public class ES3UserType_ScriptedTriggerArray : ES3ArrayType
	{
		// Token: 0x060011D0 RID: 4560 RVA: 0x000A8090 File Offset: 0x000A6290
		public ES3UserType_ScriptedTriggerArray()
			: base(typeof(ScriptedTrigger[]), ES3UserType_ScriptedTrigger.Instance)
		{
			ES3UserType_ScriptedTriggerArray.Instance = this;
		}

		// Token: 0x04000E30 RID: 3632
		public static ES3Type Instance;
	}
}
