using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000BF RID: 191
	[Preserve]
	[ES3Properties(new string[] { "x", "y", "z", "w" })]
	public class ES3Type_Vector4 : ES3Type
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x0001E396 File Offset: 0x0001C596
		public ES3Type_Vector4()
			: base(typeof(Vector4))
		{
			ES3Type_Vector4.Instance = this;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001E3B0 File Offset: 0x0001C5B0
		public override void Write(object obj, ES3Writer writer)
		{
			Vector4 vector = (Vector4)obj;
			writer.WriteProperty("x", vector.x, ES3Type_float.Instance);
			writer.WriteProperty("y", vector.y, ES3Type_float.Instance);
			writer.WriteProperty("z", vector.z, ES3Type_float.Instance);
			writer.WriteProperty("w", vector.w, ES3Type_float.Instance);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0001E430 File Offset: 0x0001C630
		public override object Read<T>(ES3Reader reader)
		{
			return new Vector4(reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance));
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0001E468 File Offset: 0x0001C668
		public static bool Equals(Vector4 a, Vector4 b)
		{
			return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y) && Mathf.Approximately(a.z, b.z) && Mathf.Approximately(a.w, b.w);
		}

		// Token: 0x040000F5 RID: 245
		public static ES3Type Instance;
	}
}
