using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000073 RID: 115
	[Preserve]
	[ES3Properties(new string[] { "r", "g", "b", "a" })]
	public class ES3Type_Color : ES3Type
	{
		// Token: 0x060002EC RID: 748 RVA: 0x0000EBE4 File Offset: 0x0000CDE4
		public ES3Type_Color()
			: base(typeof(Color))
		{
			ES3Type_Color.Instance = this;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000EBFC File Offset: 0x0000CDFC
		public override void Write(object obj, ES3Writer writer)
		{
			Color color = (Color)obj;
			writer.WriteProperty("r", color.r, ES3Type_float.Instance);
			writer.WriteProperty("g", color.g, ES3Type_float.Instance);
			writer.WriteProperty("b", color.b, ES3Type_float.Instance);
			writer.WriteProperty("a", color.a, ES3Type_float.Instance);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000EC7C File Offset: 0x0000CE7C
		public override object Read<T>(ES3Reader reader)
		{
			return new Color(reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance));
		}

		// Token: 0x040000A6 RID: 166
		public static ES3Type Instance;
	}
}
