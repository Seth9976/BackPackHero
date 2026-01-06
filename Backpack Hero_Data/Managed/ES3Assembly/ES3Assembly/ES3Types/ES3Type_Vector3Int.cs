using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000BD RID: 189
	[Preserve]
	[ES3Properties(new string[] { "x", "y", "z" })]
	public class ES3Type_Vector3Int : ES3Type
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x0001E2CB File Offset: 0x0001C4CB
		public ES3Type_Vector3Int()
			: base(typeof(Vector3Int))
		{
			ES3Type_Vector3Int.Instance = this;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001E2E4 File Offset: 0x0001C4E4
		public override void Write(object obj, ES3Writer writer)
		{
			Vector3Int vector3Int = (Vector3Int)obj;
			writer.WriteProperty("x", vector3Int.x, ES3Type_int.Instance);
			writer.WriteProperty("y", vector3Int.y, ES3Type_int.Instance);
			writer.WriteProperty("z", vector3Int.z, ES3Type_int.Instance);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0001E34C File Offset: 0x0001C54C
		public override object Read<T>(ES3Reader reader)
		{
			return new Vector3Int(reader.ReadProperty<int>(ES3Type_int.Instance), reader.ReadProperty<int>(ES3Type_int.Instance), reader.ReadProperty<int>(ES3Type_int.Instance));
		}

		// Token: 0x040000F3 RID: 243
		public static ES3Type Instance;
	}
}
