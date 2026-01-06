using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001CD RID: 461
	[Preserve]
	[ES3Properties(new string[] { })]
	public class ES3UserType_FlowerWobble : ES3ComponentType
	{
		// Token: 0x06001161 RID: 4449 RVA: 0x000A3981 File Offset: 0x000A1B81
		public ES3UserType_FlowerWobble()
			: base(typeof(FlowerWobble))
		{
			ES3UserType_FlowerWobble.Instance = this;
			this.priority = 1;
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x000A39A0 File Offset: 0x000A1BA0
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			FlowerWobble flowerWobble = (FlowerWobble)obj;
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x000A39AC File Offset: 0x000A1BAC
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			FlowerWobble flowerWobble = (FlowerWobble)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				reader.Skip();
			}
		}

		// Token: 0x04000DF9 RID: 3577
		public static ES3Type Instance;
	}
}
