using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000083 RID: 131
	[Preserve]
	[ES3Properties(new string[] { "alpha", "time" })]
	public class ES3Type_GradientAlphaKey : ES3Type
	{
		// Token: 0x06000322 RID: 802 RVA: 0x00010061 File Offset: 0x0000E261
		public ES3Type_GradientAlphaKey()
			: base(typeof(GradientAlphaKey))
		{
			ES3Type_GradientAlphaKey.Instance = this;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001007C File Offset: 0x0000E27C
		public override void Write(object obj, ES3Writer writer)
		{
			GradientAlphaKey gradientAlphaKey = (GradientAlphaKey)obj;
			writer.WriteProperty("alpha", gradientAlphaKey.alpha, ES3Type_float.Instance);
			writer.WriteProperty("time", gradientAlphaKey.time, ES3Type_float.Instance);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000100C6 File Offset: 0x0000E2C6
		public override object Read<T>(ES3Reader reader)
		{
			return new GradientAlphaKey(reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance));
		}

		// Token: 0x040000B9 RID: 185
		public static ES3Type Instance;
	}
}
