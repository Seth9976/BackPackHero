using System;

namespace ES3Types
{
	// Token: 0x020001CC RID: 460
	public class ES3UserType_EnergyEmitterArray : ES3ArrayType
	{
		// Token: 0x06001160 RID: 4448 RVA: 0x000A3964 File Offset: 0x000A1B64
		public ES3UserType_EnergyEmitterArray()
			: base(typeof(EnergyEmitter[]), ES3UserType_EnergyEmitter.Instance)
		{
			ES3UserType_EnergyEmitterArray.Instance = this;
		}

		// Token: 0x04000DF8 RID: 3576
		public static ES3Type Instance;
	}
}
