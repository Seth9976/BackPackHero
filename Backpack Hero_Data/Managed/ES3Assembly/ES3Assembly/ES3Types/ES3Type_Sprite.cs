using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000AC RID: 172
	[Preserve]
	[ES3Properties(new string[] { "texture", "rect", "pivot", "pixelsPerUnit", "border" })]
	public class ES3Type_Sprite : ES3UnityObjectType
	{
		// Token: 0x06000396 RID: 918 RVA: 0x0001C579 File Offset: 0x0001A779
		public ES3Type_Sprite()
			: base(typeof(Sprite))
		{
			ES3Type_Sprite.Instance = this;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001C594 File Offset: 0x0001A794
		protected override void WriteUnityObject(object obj, ES3Writer writer)
		{
			Sprite sprite = (Sprite)obj;
			writer.WriteProperty("texture", sprite.texture, ES3Type_Texture2D.Instance);
			writer.WriteProperty("rect", sprite.rect, ES3Type_Rect.Instance);
			writer.WriteProperty("pivot", new Vector2(sprite.pivot.x / (float)sprite.texture.width, sprite.pivot.y / (float)sprite.texture.height), ES3Type_Vector2.Instance);
			writer.WriteProperty("pixelsPerUnit", sprite.pixelsPerUnit, ES3Type_float.Instance);
			writer.WriteProperty("border", sprite.border, ES3Type_Vector4.Instance);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001C65C File Offset: 0x0001A85C
		protected override void ReadUnityObject<T>(ES3Reader reader, object obj)
		{
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				reader.Skip();
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001C6B4 File Offset: 0x0001A8B4
		protected override object ReadUnityObject<T>(ES3Reader reader)
		{
			Texture2D texture2D = null;
			Rect rect = Rect.zero;
			Vector2 vector = Vector2.zero;
			float num = 0f;
			Vector4 vector2 = Vector4.zero;
			foreach (object obj in reader.Properties)
			{
				string text = (string)obj;
				if (!(text == "texture"))
				{
					if (!(text == "textureRect") && !(text == "rect"))
					{
						if (!(text == "pivot"))
						{
							if (!(text == "pixelsPerUnit"))
							{
								if (!(text == "border"))
								{
									reader.Skip();
								}
								else
								{
									vector2 = reader.Read<Vector4>(ES3Type_Vector4.Instance);
								}
							}
							else
							{
								num = reader.Read<float>(ES3Type_float.Instance);
							}
						}
						else
						{
							vector = reader.Read<Vector2>(ES3Type_Vector2.Instance);
						}
					}
					else
					{
						rect = reader.Read<Rect>(ES3Type_Rect.Instance);
					}
				}
				else
				{
					texture2D = reader.Read<Texture2D>(ES3Type_Texture2D.Instance);
				}
			}
			return Sprite.Create(texture2D, rect, vector, num, 0U, SpriteMeshType.Tight, vector2);
		}

		// Token: 0x040000E2 RID: 226
		public static ES3Type Instance;
	}
}
