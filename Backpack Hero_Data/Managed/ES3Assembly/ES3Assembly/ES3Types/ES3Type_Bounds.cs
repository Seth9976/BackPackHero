using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000070 RID: 112
	[Preserve]
	[ES3Properties(new string[] { "center", "size" })]
	public class ES3Type_Bounds : ES3Type
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x0000E544 File Offset: 0x0000C744
		public ES3Type_Bounds()
			: base(typeof(Bounds))
		{
			ES3Type_Bounds.Instance = this;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000E55C File Offset: 0x0000C75C
		public override void Write(object obj, ES3Writer writer)
		{
			Bounds bounds = (Bounds)obj;
			writer.WriteProperty("center", bounds.center, ES3Type_Vector3.Instance);
			writer.WriteProperty("size", bounds.size, ES3Type_Vector3.Instance);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000E5A8 File Offset: 0x0000C7A8
		public override object Read<T>(ES3Reader reader)
		{
			return new Bounds(reader.ReadProperty<Vector3>(ES3Type_Vector3.Instance), reader.ReadProperty<Vector3>(ES3Type_Vector3.Instance));
		}

		// Token: 0x040000A3 RID: 163
		public static ES3Type Instance;
	}
}
