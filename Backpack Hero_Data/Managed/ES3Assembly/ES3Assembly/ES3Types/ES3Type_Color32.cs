using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000075 RID: 117
	[Preserve]
	[ES3Properties(new string[] { "r", "g", "b", "a" })]
	public class ES3Type_Color32 : ES3Type
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x0000ECD1 File Offset: 0x0000CED1
		public ES3Type_Color32()
			: base(typeof(Color32))
		{
			ES3Type_Color32.Instance = this;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		public override void Write(object obj, ES3Writer writer)
		{
			Color32 color = (Color32)obj;
			writer.WriteProperty("r", color.r, ES3Type_byte.Instance);
			writer.WriteProperty("g", color.g, ES3Type_byte.Instance);
			writer.WriteProperty("b", color.b, ES3Type_byte.Instance);
			writer.WriteProperty("a", color.a, ES3Type_byte.Instance);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000ED6C File Offset: 0x0000CF6C
		public override object Read<T>(ES3Reader reader)
		{
			return new Color32(reader.ReadProperty<byte>(ES3Type_byte.Instance), reader.ReadProperty<byte>(ES3Type_byte.Instance), reader.ReadProperty<byte>(ES3Type_byte.Instance), reader.ReadProperty<byte>(ES3Type_byte.Instance));
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000EDA4 File Offset: 0x0000CFA4
		public static bool Equals(Color32 a, Color32 b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
		}

		// Token: 0x040000A8 RID: 168
		public static ES3Type Instance;
	}
}
