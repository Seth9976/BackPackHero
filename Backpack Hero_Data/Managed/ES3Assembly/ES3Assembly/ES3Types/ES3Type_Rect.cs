using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200009F RID: 159
	[Preserve]
	[ES3Properties(new string[] { "x", "y", "width", "height" })]
	public class ES3Type_Rect : ES3Type
	{
		// Token: 0x0600036E RID: 878 RVA: 0x00019755 File Offset: 0x00017955
		public ES3Type_Rect()
			: base(typeof(Rect))
		{
			ES3Type_Rect.Instance = this;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00019770 File Offset: 0x00017970
		public override void Write(object obj, ES3Writer writer)
		{
			Rect rect = (Rect)obj;
			writer.WriteProperty("x", rect.x, ES3Type_float.Instance);
			writer.WriteProperty("y", rect.y, ES3Type_float.Instance);
			writer.WriteProperty("width", rect.width, ES3Type_float.Instance);
			writer.WriteProperty("height", rect.height, ES3Type_float.Instance);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x000197F4 File Offset: 0x000179F4
		public override object Read<T>(ES3Reader reader)
		{
			return new Rect(reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance));
		}

		// Token: 0x040000D5 RID: 213
		public static ES3Type Instance;
	}
}
