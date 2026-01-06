using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000A5 RID: 165
	[Preserve]
	[ES3Properties(new string[] { "name", "maximumLOD" })]
	public class ES3Type_Shader : ES3Type
	{
		// Token: 0x06000381 RID: 897 RVA: 0x0001ACEF File Offset: 0x00018EEF
		public ES3Type_Shader()
			: base(typeof(Shader))
		{
			ES3Type_Shader.Instance = this;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001AD08 File Offset: 0x00018F08
		public override void Write(object obj, ES3Writer writer)
		{
			Shader shader = (Shader)obj;
			writer.WriteProperty("name", shader.name, ES3Type_string.Instance);
			writer.WriteProperty("maximumLOD", shader.maximumLOD, ES3Type_int.Instance);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001AD50 File Offset: 0x00018F50
		public override object Read<T>(ES3Reader reader)
		{
			Shader shader = Shader.Find(reader.ReadProperty<string>(ES3Type_string.Instance));
			if (shader == null)
			{
				shader = Shader.Find("Diffuse");
			}
			this.ReadInto<T>(reader, shader);
			return shader;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001AD8C File Offset: 0x00018F8C
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			Shader shader = (Shader)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "name"))
				{
					if (!(text == "maximumLOD"))
					{
						reader.Skip();
					}
					else
					{
						shader.maximumLOD = reader.Read<int>(ES3Type_int.Instance);
					}
				}
				else
				{
					shader.name = reader.Read<string>(ES3Type_string.Instance);
				}
			}
		}

		// Token: 0x040000DB RID: 219
		public static ES3Type Instance;
	}
}
