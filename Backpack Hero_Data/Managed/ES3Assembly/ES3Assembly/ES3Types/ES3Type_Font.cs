using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200007D RID: 125
	[Preserve]
	[ES3Properties(new string[] { "material", "name" })]
	public class ES3Type_Font : ES3UnityObjectType
	{
		// Token: 0x0600030A RID: 778 RVA: 0x0000F396 File Offset: 0x0000D596
		public ES3Type_Font()
			: base(typeof(Font))
		{
			ES3Type_Font.Instance = this;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
		protected override void WriteUnityObject(object obj, ES3Writer writer)
		{
			Font font = (Font)obj;
			writer.WriteProperty("name", font.name, ES3Type_string.Instance);
			writer.WriteProperty("material", font.material);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000F3EC File Offset: 0x0000D5EC
		protected override void ReadUnityObject<T>(ES3Reader reader, object obj)
		{
			Font font = (Font)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				if (text == "material")
				{
					font.material = reader.Read<Material>(ES3Type_Material.Instance);
				}
				else
				{
					reader.Skip();
				}
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000F434 File Offset: 0x0000D634
		protected override object ReadUnityObject<T>(ES3Reader reader)
		{
			Font font = new Font(reader.ReadProperty<string>(ES3Type_string.Instance));
			this.ReadObject<T>(reader, font);
			return font;
		}

		// Token: 0x040000B0 RID: 176
		public static ES3Type Instance;
	}
}
