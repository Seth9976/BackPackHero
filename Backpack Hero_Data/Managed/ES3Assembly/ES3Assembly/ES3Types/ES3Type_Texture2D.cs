using System;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000B2 RID: 178
	[Preserve]
	[ES3Properties(new string[] { "filterMode", "anisoLevel", "wrapMode", "mipMapBias", "rawTextureData" })]
	public class ES3Type_Texture2D : ES3UnityObjectType
	{
		// Token: 0x060003A7 RID: 935 RVA: 0x0001D1A5 File Offset: 0x0001B3A5
		public ES3Type_Texture2D()
			: base(typeof(Texture2D))
		{
			ES3Type_Texture2D.Instance = this;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001D1C0 File Offset: 0x0001B3C0
		protected override void WriteUnityObject(object obj, ES3Writer writer)
		{
			Texture2D texture2D = (Texture2D)obj;
			if (!this.IsReadable(texture2D))
			{
				ES3Debug.LogWarning("Easy Save cannot save the pixels or properties of this Texture because it is not read/write enabled, so Easy Save will store it by reference instead. To save the pixel data, check the 'Read/Write Enabled' checkbox in the Texture's import settings. Clicking this warning will take you to the Texture, assuming it is not generated at runtime.", texture2D, 0);
				return;
			}
			writer.WriteProperty("width", texture2D.width, ES3Type_int.Instance);
			writer.WriteProperty("height", texture2D.height, ES3Type_int.Instance);
			writer.WriteProperty("format", texture2D.format);
			writer.WriteProperty("mipmapCount", texture2D.mipmapCount, ES3Type_int.Instance);
			writer.WriteProperty("filterMode", texture2D.filterMode);
			writer.WriteProperty("anisoLevel", texture2D.anisoLevel, ES3Type_int.Instance);
			writer.WriteProperty("wrapMode", texture2D.wrapMode);
			writer.WriteProperty("mipMapBias", texture2D.mipMapBias, ES3Type_float.Instance);
			writer.WriteProperty("rawTextureData", texture2D.GetRawTextureData(), ES3Type_byteArray.Instance);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001D2CC File Offset: 0x0001B4CC
		protected override void ReadUnityObject<T>(ES3Reader reader, object obj)
		{
			if (obj == null)
			{
				return;
			}
			if (obj.GetType() == typeof(RenderTexture))
			{
				ES3Type_RenderTexture.Instance.ReadInto<T>(reader, obj);
				return;
			}
			Texture2D texture2D = (Texture2D)obj;
			if (!this.IsReadable(texture2D))
			{
				ES3Debug.LogWarning("Easy Save cannot load the properties or pixels for this Texture because it is not read/write enabled, so it will be loaded by reference. To load the properties and pixels for this Texture, check the 'Read/Write Enabled' checkbox in its Import Settings.", texture2D, 0);
			}
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!this.IsReadable(texture2D))
				{
					reader.Skip();
				}
				else if (!(text == "filterMode"))
				{
					if (!(text == "anisoLevel"))
					{
						if (!(text == "wrapMode"))
						{
							if (!(text == "mipMapBias"))
							{
								if (text == "rawTextureData")
								{
									if (!this.IsReadable(texture2D))
									{
										ES3Debug.LogWarning("Easy Save cannot load the pixels of this Texture because it is not read/write enabled, so Easy Save will ignore the pixel data. To load the pixel data, check the 'Read/Write Enabled' checkbox in the Texture's import settings. Clicking this warning will take you to the Texture, assuming it is not generated at runtime.", texture2D, 0);
										reader.Skip();
										continue;
									}
									try
									{
										texture2D.LoadRawTextureData(reader.Read<byte[]>(ES3Type_byteArray.Instance));
										texture2D.Apply();
										continue;
									}
									catch (Exception ex)
									{
										ES3Debug.LogError("Easy Save encountered an error when trying to load this Texture, please see the end of this messasge for the error. This is most likely because the Texture format of the instance we are loading into is different to the Texture we saved.\n" + ex.ToString(), texture2D, 0);
										continue;
									}
								}
								reader.Skip();
							}
							else
							{
								texture2D.mipMapBias = reader.Read<float>(ES3Type_float.Instance);
							}
						}
						else
						{
							texture2D.wrapMode = reader.Read<TextureWrapMode>();
						}
					}
					else
					{
						texture2D.anisoLevel = reader.Read<int>(ES3Type_int.Instance);
					}
				}
				else
				{
					texture2D.filterMode = reader.Read<FilterMode>();
				}
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001D488 File Offset: 0x0001B688
		protected override object ReadUnityObject<T>(ES3Reader reader)
		{
			Texture2D texture2D = new Texture2D(reader.Read<int>(ES3Type_int.Instance), reader.ReadProperty<int>(ES3Type_int.Instance), reader.ReadProperty<TextureFormat>(), reader.ReadProperty<int>(ES3Type_int.Instance) > 1);
			this.ReadObject<T>(reader, texture2D);
			return texture2D;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001D4CE File Offset: 0x0001B6CE
		protected bool IsReadable(Texture2D instance)
		{
			return instance != null && instance.isReadable;
		}

		// Token: 0x040000E8 RID: 232
		public static ES3Type Instance;
	}
}
