using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200009D RID: 157
	[Preserve]
	[ES3Properties(new string[] { "x", "y", "z", "w" })]
	public class ES3Type_Quaternion : ES3Type
	{
		// Token: 0x0600036A RID: 874 RVA: 0x00019665 File Offset: 0x00017865
		public ES3Type_Quaternion()
			: base(typeof(Quaternion))
		{
			ES3Type_Quaternion.Instance = this;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00019680 File Offset: 0x00017880
		public override void Write(object obj, ES3Writer writer)
		{
			Quaternion quaternion = (Quaternion)obj;
			writer.WriteProperty("x", quaternion.x, ES3Type_float.Instance);
			writer.WriteProperty("y", quaternion.y, ES3Type_float.Instance);
			writer.WriteProperty("z", quaternion.z, ES3Type_float.Instance);
			writer.WriteProperty("w", quaternion.w, ES3Type_float.Instance);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00019700 File Offset: 0x00017900
		public override object Read<T>(ES3Reader reader)
		{
			return new Quaternion(reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance));
		}

		// Token: 0x040000D3 RID: 211
		public static ES3Type Instance;
	}
}
