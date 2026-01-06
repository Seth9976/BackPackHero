using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000082 RID: 130
	[Preserve]
	[ES3Properties(new string[] { "colorKeys", "alphaKeys", "mode" })]
	public class ES3Type_Gradient : ES3Type
	{
		// Token: 0x0600031E RID: 798 RVA: 0x0000FF78 File Offset: 0x0000E178
		public ES3Type_Gradient()
			: base(typeof(Gradient))
		{
			ES3Type_Gradient.Instance = this;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000FF90 File Offset: 0x0000E190
		public override void Write(object obj, ES3Writer writer)
		{
			Gradient gradient = (Gradient)obj;
			writer.WriteProperty("colorKeys", gradient.colorKeys, ES3Type_GradientColorKeyArray.Instance);
			writer.WriteProperty("alphaKeys", gradient.alphaKeys, ES3Type_GradientAlphaKeyArray.Instance);
			writer.WriteProperty("mode", gradient.mode);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000FFE8 File Offset: 0x0000E1E8
		public override object Read<T>(ES3Reader reader)
		{
			Gradient gradient = new Gradient();
			this.ReadInto<T>(reader, gradient);
			return gradient;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00010004 File Offset: 0x0000E204
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			Gradient gradient = (Gradient)obj;
			gradient.SetKeys(reader.ReadProperty<GradientColorKey[]>(ES3Type_GradientColorKeyArray.Instance), reader.ReadProperty<GradientAlphaKey[]>(ES3Type_GradientAlphaKeyArray.Instance));
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				if (text == "mode")
				{
					gradient.mode = reader.Read<GradientMode>();
				}
				else
				{
					reader.Skip();
				}
			}
		}

		// Token: 0x040000B8 RID: 184
		public static ES3Type Instance;
	}
}
