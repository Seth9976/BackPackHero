using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000B9 RID: 185
	[Preserve]
	[ES3Properties(new string[] { "x", "y" })]
	public class ES3Type_Vector2Int : ES3Type
	{
		// Token: 0x060003BD RID: 957 RVA: 0x0001E15D File Offset: 0x0001C35D
		public ES3Type_Vector2Int()
			: base(typeof(Vector2Int))
		{
			ES3Type_Vector2Int.Instance = this;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001E178 File Offset: 0x0001C378
		public override void Write(object obj, ES3Writer writer)
		{
			Vector2Int vector2Int = (Vector2Int)obj;
			writer.WriteProperty("x", vector2Int.x, ES3Type_int.Instance);
			writer.WriteProperty("y", vector2Int.y, ES3Type_int.Instance);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001E1C4 File Offset: 0x0001C3C4
		public override object Read<T>(ES3Reader reader)
		{
			return new Vector2Int(reader.ReadProperty<int>(ES3Type_int.Instance), reader.ReadProperty<int>(ES3Type_int.Instance));
		}

		// Token: 0x040000EF RID: 239
		public static ES3Type Instance;
	}
}
