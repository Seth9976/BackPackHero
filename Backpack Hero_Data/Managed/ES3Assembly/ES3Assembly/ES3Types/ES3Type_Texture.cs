using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000B0 RID: 176
	[Preserve]
	[ES3Properties(new string[] { "filterMode", "anisoLevel", "wrapMode", "mipMapBias", "rawTextureData" })]
	public class ES3Type_Texture : ES3Type
	{
		// Token: 0x060003A2 RID: 930 RVA: 0x0001D0B1 File Offset: 0x0001B2B1
		public ES3Type_Texture()
			: base(typeof(Texture))
		{
			ES3Type_Texture.Instance = this;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001D0CC File Offset: 0x0001B2CC
		public override void Write(object obj, ES3Writer writer)
		{
			if (obj.GetType() == typeof(Texture2D))
			{
				ES3Type_Texture2D.Instance.Write(obj, writer);
				return;
			}
			string text = "Textures of type ";
			Type type = obj.GetType();
			throw new NotSupportedException(text + ((type != null) ? type.ToString() : null) + " are not currently supported.");
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001D124 File Offset: 0x0001B324
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			if (obj.GetType() == typeof(Texture2D))
			{
				ES3Type_Texture2D.Instance.ReadInto<T>(reader, obj);
				return;
			}
			string text = "Textures of type ";
			Type type = obj.GetType();
			throw new NotSupportedException(text + ((type != null) ? type.ToString() : null) + " are not currently supported.");
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0001D17B File Offset: 0x0001B37B
		public override object Read<T>(ES3Reader reader)
		{
			return ES3Type_Texture2D.Instance.Read<T>(reader);
		}

		// Token: 0x040000E6 RID: 230
		public static ES3Type Instance;
	}
}
