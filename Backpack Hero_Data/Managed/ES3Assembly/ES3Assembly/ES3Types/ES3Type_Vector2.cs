using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000B7 RID: 183
	[Preserve]
	[ES3Properties(new string[] { "x", "y" })]
	public class ES3Type_Vector2 : ES3Type
	{
		// Token: 0x060003B9 RID: 953 RVA: 0x0001E0BC File Offset: 0x0001C2BC
		public ES3Type_Vector2()
			: base(typeof(Vector2))
		{
			ES3Type_Vector2.Instance = this;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001E0D4 File Offset: 0x0001C2D4
		public override void Write(object obj, ES3Writer writer)
		{
			Vector2 vector = (Vector2)obj;
			writer.WriteProperty("x", vector.x, ES3Type_float.Instance);
			writer.WriteProperty("y", vector.y, ES3Type_float.Instance);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001E11E File Offset: 0x0001C31E
		public override object Read<T>(ES3Reader reader)
		{
			return new Vector2(reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance));
		}

		// Token: 0x040000ED RID: 237
		public static ES3Type Instance;
	}
}
