using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200008A RID: 138
	[Preserve]
	[ES3Properties(new string[] { "time", "value", "inTangent", "outTangent" })]
	public class ES3Type_Keyframe : ES3Type
	{
		// Token: 0x06000332 RID: 818 RVA: 0x00010399 File Offset: 0x0000E599
		public ES3Type_Keyframe()
			: base(typeof(Keyframe))
		{
			ES3Type_Keyframe.Instance = this;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x000103B4 File Offset: 0x0000E5B4
		public override void Write(object obj, ES3Writer writer)
		{
			Keyframe keyframe = (Keyframe)obj;
			writer.WriteProperty("time", keyframe.time, ES3Type_float.Instance);
			writer.WriteProperty("value", keyframe.value, ES3Type_float.Instance);
			writer.WriteProperty("inTangent", keyframe.inTangent, ES3Type_float.Instance);
			writer.WriteProperty("outTangent", keyframe.outTangent, ES3Type_float.Instance);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00010438 File Offset: 0x0000E638
		public override object Read<T>(ES3Reader reader)
		{
			return new Keyframe(reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance));
		}

		// Token: 0x040000C0 RID: 192
		public static ES3Type Instance;
	}
}
