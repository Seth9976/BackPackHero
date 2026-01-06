using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000085 RID: 133
	[Preserve]
	[ES3Properties(new string[] { "color", "time" })]
	public class ES3Type_GradientColorKey : ES3Type
	{
		// Token: 0x06000326 RID: 806 RVA: 0x00010105 File Offset: 0x0000E305
		public ES3Type_GradientColorKey()
			: base(typeof(GradientColorKey))
		{
			ES3Type_GradientColorKey.Instance = this;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00010120 File Offset: 0x0000E320
		public override void Write(object obj, ES3Writer writer)
		{
			GradientColorKey gradientColorKey = (GradientColorKey)obj;
			writer.WriteProperty("color", gradientColorKey.color, ES3Type_Color.Instance);
			writer.WriteProperty("time", gradientColorKey.time, ES3Type_float.Instance);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0001016A File Offset: 0x0000E36A
		public override object Read<T>(ES3Reader reader)
		{
			return new GradientColorKey(reader.ReadProperty<Color>(ES3Type_Color.Instance), reader.ReadProperty<float>(ES3Type_float.Instance));
		}

		// Token: 0x040000BB RID: 187
		public static ES3Type Instance;
	}
}
