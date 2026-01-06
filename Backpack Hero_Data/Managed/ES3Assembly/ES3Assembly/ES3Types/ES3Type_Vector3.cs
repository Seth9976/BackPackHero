using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000BB RID: 187
	[Preserve]
	[ES3Properties(new string[] { "x", "y", "z" })]
	public class ES3Type_Vector3 : ES3Type
	{
		// Token: 0x060003C1 RID: 961 RVA: 0x0001E203 File Offset: 0x0001C403
		public ES3Type_Vector3()
			: base(typeof(Vector3))
		{
			ES3Type_Vector3.Instance = this;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001E21C File Offset: 0x0001C41C
		public override void Write(object obj, ES3Writer writer)
		{
			Vector3 vector = (Vector3)obj;
			writer.WriteProperty("x", vector.x, ES3Type_float.Instance);
			writer.WriteProperty("y", vector.y, ES3Type_float.Instance);
			writer.WriteProperty("z", vector.z, ES3Type_float.Instance);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001E281 File Offset: 0x0001C481
		public override object Read<T>(ES3Reader reader)
		{
			return new Vector3(reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance));
		}

		// Token: 0x040000F1 RID: 241
		public static ES3Type Instance;
	}
}
