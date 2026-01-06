using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json.Linq;
using StbImageSharp;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000134 RID: 308
public class ModUtils
{
	// Token: 0x06000B96 RID: 2966 RVA: 0x0007A3B4 File Offset: 0x000785B4
	public static bool IsTextHidden(JToken json)
	{
		if (json.Type == JTokenType.String)
		{
			string text = (string)json;
			if (text.Equals("hide", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			if (text.Equals("hidden", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x0007A3F2 File Offset: 0x000785F2
	public static bool AlmostEquals(double double1, double double2, double precision)
	{
		return Math.Abs(double1 - double2) <= precision;
	}

	// Token: 0x06000B98 RID: 2968 RVA: 0x0007A402 File Offset: 0x00078602
	public static T ParseEnumOrDefault<T>(string str, T def) where T : struct, IConvertible
	{
		if (str == null)
		{
			return def;
		}
		return Enum.Parse<T>(str, true);
	}

	// Token: 0x06000B99 RID: 2969 RVA: 0x0007A410 File Offset: 0x00078610
	public static T CastValueOrDefault<T>(object val, object def)
	{
		T t;
		try
		{
			try
			{
				JToken jtoken = (JToken)val;
				if (jtoken != null)
				{
					return jtoken.ToObject<T>();
				}
			}
			catch
			{
			}
			if (val == null)
			{
				t = (T)((object)def);
			}
			else
			{
				t = (T)((object)val);
			}
		}
		catch (InvalidCastException ex)
		{
			throw new ModUtils.ParseException("Cannot convert value to " + typeof(T).ToString() + ". Invalid type.", ex);
		}
		return t;
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x0007A490 File Offset: 0x00078690
	public static T CastValue<T>(object val)
	{
		T t;
		try
		{
			try
			{
				JToken jtoken = (JToken)val;
				if (jtoken != null)
				{
					return jtoken.ToObject<T>();
				}
			}
			catch
			{
			}
			if (val is int)
			{
				int num = (int)val;
				if (typeof(T) == typeof(int))
				{
					return (T)((object)val);
				}
				if (typeof(T) == typeof(float))
				{
					return (T)((object)((float)num));
				}
			}
			if (val is float)
			{
				float num2 = (float)val;
				if (typeof(T) == typeof(int))
				{
					t = (T)((object)((int)num2));
				}
				else
				{
					typeof(T) == typeof(float);
					t = (T)((object)val);
				}
			}
			else
			{
				t = (T)((object)val);
			}
		}
		catch (Exception ex)
		{
			throw new ModUtils.ParseException(string.Concat(new string[]
			{
				"Cannot convert value ",
				(val != null) ? val.ToString() : null,
				" to ",
				typeof(T).ToString(),
				"."
			}), ex);
		}
		return t;
	}

	// Token: 0x06000B9B RID: 2971 RVA: 0x0007A5EC File Offset: 0x000787EC
	public static bool IsNullEmpty(JToken token)
	{
		if (token == null)
		{
			return true;
		}
		if (token.Type == JTokenType.Array || token.Type == JTokenType.Object)
		{
			return !token.HasValues;
		}
		return token.ToString() == string.Empty;
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x0007A628 File Offset: 0x00078828
	public static SerializedDictionary<string, string> ToSerializedDictionary(Dictionary<string, string> dictionary)
	{
		SerializedDictionary<string, string> serializedDictionary = new SerializedDictionary<string, string>();
		foreach (KeyValuePair<string, string> keyValuePair in dictionary)
		{
			serializedDictionary.Add(keyValuePair.Key, keyValuePair.Value);
		}
		return serializedDictionary;
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x0007A68C File Offset: 0x0007888C
	public static Sprite LoadNewSprite(byte[] bytes, float PixelsPerUnit = 100f)
	{
		Texture2D texture2D = ModUtils.LoadTexture(bytes);
		return Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), PixelsPerUnit);
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x0007A6D4 File Offset: 0x000788D4
	public static Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 100f)
	{
		Texture2D texture2D = ModUtils.LoadTexture(FilePath);
		return Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), PixelsPerUnit);
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x0007A71C File Offset: 0x0007891C
	public static Texture2D LoadTexture(string FilePath)
	{
		try
		{
			return ModUtils.LoadTexture(File.ReadAllBytes(FilePath));
		}
		catch (Exception ex)
		{
			string text = "Could not load ";
			string text2 = ". ----> ";
			Exception ex2 = ex;
			ModLog.LogError(text + FilePath + text2 + ((ex2 != null) ? ex2.ToString() : null));
		}
		return null;
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x0007A770 File Offset: 0x00078970
	public static Texture2D LoadTexture(byte[] FileData)
	{
		try
		{
			Texture2D texture2D = new Texture2D(2, 2, TextureFormat.RGBA32, false);
			if (texture2D.LoadImage(FileData))
			{
				texture2D.filterMode = FilterMode.Point;
				return texture2D;
			}
			ImageResult imageResult = ImageResult.FromMemory(FileData, ColorComponents.RedGreenBlueAlpha);
			Texture2D texture2D2 = new Texture2D(imageResult.Width, imageResult.Height, TextureFormat.RGBA32, false);
			texture2D2.filterMode = FilterMode.Point;
			texture2D2.LoadRawTextureData(imageResult.Data);
			texture2D2.Apply();
			Color32[] array = new Color32[imageResult.Width * imageResult.Height];
			for (int i = 0; i < imageResult.Height; i++)
			{
				for (int j = 0; j < imageResult.Width; j++)
				{
					int num = i * imageResult.Width + j;
					int num2 = (imageResult.Height - i - 1) * imageResult.Width + j;
					array[num2] = new Color32(imageResult.Data[num * 4], imageResult.Data[num * 4 + 1], imageResult.Data[num * 4 + 2], imageResult.Data[num * 4 + 3]);
				}
			}
			texture2D2.SetPixels32(array);
			texture2D2.Apply();
			return texture2D2;
		}
		catch (Exception ex)
		{
			string text = "Could not load sprite. ----> ";
			Exception ex2 = ex;
			ModLog.LogError(text + ((ex2 != null) ? ex2.ToString() : null));
		}
		return null;
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x0007A8D0 File Offset: 0x00078AD0
	public static void CopyTexture(Texture2D sourceTexture, Texture2D destinationTexture, int posX, int posY)
	{
		Color32[] pixels = sourceTexture.GetPixels32();
		int num = Mathf.Clamp(posX, 0, destinationTexture.width - sourceTexture.width);
		int num2 = Mathf.Clamp(posY, 0, destinationTexture.height - sourceTexture.height);
		destinationTexture.SetPixels32(num, num2, sourceTexture.width, sourceTexture.height, pixels);
		destinationTexture.Apply();
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x0007A92C File Offset: 0x00078B2C
	public static void ReplaceColorInTexture(Texture2D textureToModify, Color32 colorToReplace, Color32 replacementColor)
	{
		Color32[] pixels = textureToModify.GetPixels32();
		for (int i = 0; i < pixels.Length; i++)
		{
			if (pixels[i].Equals(colorToReplace))
			{
				pixels[i] = replacementColor;
			}
		}
		textureToModify.SetPixels32(pixels);
		textureToModify.Apply();
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x0007A980 File Offset: 0x00078B80
	public static Color ColorFromTextHash(string text)
	{
		byte[] bytes = BitConverter.GetBytes(ModUtils.CalculateHashInt(text));
		float num = (float)bytes[0] / 255f;
		float num2 = (float)bytes[1] / 255f;
		float num3 = (float)bytes[2] / 255f;
		return new Color(num, num2, num3);
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x0007A9C0 File Offset: 0x00078BC0
	public static ulong CalculateHashInt(string read)
	{
		ulong num = 3074457345618258791UL;
		for (int i = 0; i < read.Length; i++)
		{
			num += (ulong)read[i];
			num *= 3074457345618258799UL;
		}
		return num;
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x0007AA00 File Offset: 0x00078C00
	public static string CalculateHash(string read)
	{
		return ModUtils.CalculateHashInt(read).ToString();
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x0007AA1C File Offset: 0x00078C1C
	public static T DeepCopy<T>(T other)
	{
		T t;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, other);
			memoryStream.Position = 0L;
			t = (T)((object)binaryFormatter.Deserialize(memoryStream));
		}
		return t;
	}

	// Token: 0x020003DD RID: 989
	[Serializable]
	public class LoadingException : Exception
	{
		// Token: 0x060018C0 RID: 6336 RVA: 0x000CDAB7 File Offset: 0x000CBCB7
		public LoadingException()
		{
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x000CDABF File Offset: 0x000CBCBF
		public LoadingException(string message)
			: base(message)
		{
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x000CDAC8 File Offset: 0x000CBCC8
		public LoadingException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x000CDAD2 File Offset: 0x000CBCD2
		protected LoadingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	// Token: 0x020003DE RID: 990
	[Serializable]
	public class SyntaxException : Exception
	{
		// Token: 0x060018C4 RID: 6340 RVA: 0x000CDADC File Offset: 0x000CBCDC
		public SyntaxException()
		{
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x000CDAE4 File Offset: 0x000CBCE4
		public SyntaxException(string message)
			: base(message)
		{
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x000CDAED File Offset: 0x000CBCED
		public SyntaxException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x000CDAF7 File Offset: 0x000CBCF7
		protected SyntaxException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	// Token: 0x020003DF RID: 991
	[Serializable]
	public class ParseException : Exception
	{
		// Token: 0x060018C8 RID: 6344 RVA: 0x000CDB01 File Offset: 0x000CBD01
		public ParseException()
		{
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x000CDB09 File Offset: 0x000CBD09
		public ParseException(string message)
			: base(message)
		{
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x000CDB12 File Offset: 0x000CBD12
		public ParseException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x000CDB1C File Offset: 0x000CBD1C
		protected ParseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	// Token: 0x020003E0 RID: 992
	[Serializable]
	public class DuplicateException : Exception
	{
		// Token: 0x060018CC RID: 6348 RVA: 0x000CDB26 File Offset: 0x000CBD26
		public DuplicateException()
		{
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x000CDB2E File Offset: 0x000CBD2E
		public DuplicateException(string message)
			: base(message)
		{
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x000CDB37 File Offset: 0x000CBD37
		public DuplicateException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x000CDB41 File Offset: 0x000CBD41
		protected DuplicateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
