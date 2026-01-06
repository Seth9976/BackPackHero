using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using ES3Internal;
using ES3Types;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x02000007 RID: 7
public static class ES3
{
	// Token: 0x0600001B RID: 27 RVA: 0x0000258E File Offset: 0x0000078E
	public static void Save(string key, object value)
	{
		ES3.Save<object>(key, value, new ES3Settings(null, null));
	}

	// Token: 0x0600001C RID: 28 RVA: 0x0000259E File Offset: 0x0000079E
	public static void Save(string key, object value, string filePath)
	{
		ES3.Save<object>(key, value, new ES3Settings(filePath, null));
	}

	// Token: 0x0600001D RID: 29 RVA: 0x000025AE File Offset: 0x000007AE
	public static void Save(string key, object value, string filePath, ES3Settings settings)
	{
		ES3.Save<object>(key, value, new ES3Settings(filePath, settings));
	}

	// Token: 0x0600001E RID: 30 RVA: 0x000025BE File Offset: 0x000007BE
	public static void Save(string key, object value, ES3Settings settings)
	{
		ES3.Save<object>(key, value, settings);
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000025C8 File Offset: 0x000007C8
	public static void Save<T>(string key, T value)
	{
		ES3.Save<T>(key, value, new ES3Settings(null, null));
	}

	// Token: 0x06000020 RID: 32 RVA: 0x000025D8 File Offset: 0x000007D8
	public static void Save<T>(string key, T value, string filePath)
	{
		ES3.Save<T>(key, value, new ES3Settings(filePath, null));
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000025E8 File Offset: 0x000007E8
	public static void Save<T>(string key, T value, string filePath, ES3Settings settings)
	{
		ES3.Save<T>(key, value, new ES3Settings(filePath, settings));
	}

	// Token: 0x06000022 RID: 34 RVA: 0x000025F8 File Offset: 0x000007F8
	public static void Save<T>(string key, T value, ES3Settings settings)
	{
		if (settings.location == ES3.Location.Cache)
		{
			ES3File.GetOrCreateCachedFile(settings).Save<T>(key, value);
			return;
		}
		using (ES3Writer es3Writer = ES3Writer.Create(settings))
		{
			es3Writer.Write<T>(key, value);
			es3Writer.Save();
		}
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002654 File Offset: 0x00000854
	public static void SaveRaw(byte[] bytes)
	{
		ES3.SaveRaw(bytes, new ES3Settings(null, null));
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00002663 File Offset: 0x00000863
	public static void SaveRaw(byte[] bytes, string filePath)
	{
		ES3.SaveRaw(bytes, new ES3Settings(filePath, null));
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00002672 File Offset: 0x00000872
	public static void SaveRaw(byte[] bytes, string filePath, ES3Settings settings)
	{
		ES3.SaveRaw(bytes, new ES3Settings(filePath, settings));
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002684 File Offset: 0x00000884
	public static void SaveRaw(byte[] bytes, ES3Settings settings)
	{
		if (settings.location == ES3.Location.Cache)
		{
			ES3File.GetOrCreateCachedFile(settings).SaveRaw(bytes, settings);
			return;
		}
		using (Stream stream = ES3Stream.CreateStream(settings, ES3FileMode.Write))
		{
			stream.Write(bytes, 0, bytes.Length);
		}
		ES3IO.CommitBackup(settings);
	}

	// Token: 0x06000027 RID: 39 RVA: 0x000026E0 File Offset: 0x000008E0
	public static void SaveRaw(string str)
	{
		ES3.SaveRaw(str, new ES3Settings(null, null));
	}

	// Token: 0x06000028 RID: 40 RVA: 0x000026EF File Offset: 0x000008EF
	public static void SaveRaw(string str, string filePath)
	{
		ES3.SaveRaw(str, new ES3Settings(filePath, null));
	}

	// Token: 0x06000029 RID: 41 RVA: 0x000026FE File Offset: 0x000008FE
	public static void SaveRaw(string str, string filePath, ES3Settings settings)
	{
		ES3.SaveRaw(str, new ES3Settings(filePath, settings));
	}

	// Token: 0x0600002A RID: 42 RVA: 0x0000270D File Offset: 0x0000090D
	public static void SaveRaw(string str, ES3Settings settings)
	{
		ES3.SaveRaw(settings.encoding.GetBytes(str), settings);
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002721 File Offset: 0x00000921
	public static void AppendRaw(byte[] bytes)
	{
		ES3.AppendRaw(bytes, new ES3Settings(null, null));
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002730 File Offset: 0x00000930
	public static void AppendRaw(byte[] bytes, string filePath, ES3Settings settings)
	{
		ES3.AppendRaw(bytes, new ES3Settings(filePath, settings));
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002740 File Offset: 0x00000940
	public static void AppendRaw(byte[] bytes, ES3Settings settings)
	{
		if (settings.location == ES3.Location.Cache)
		{
			ES3File.GetOrCreateCachedFile(settings).AppendRaw(bytes, null);
			return;
		}
		using (Stream stream = ES3Stream.CreateStream(new ES3Settings(settings.path, settings)
		{
			encryptionType = ES3.EncryptionType.None,
			compressionType = ES3.CompressionType.None
		}, ES3FileMode.Append))
		{
			stream.Write(bytes, 0, bytes.Length);
		}
	}

	// Token: 0x0600002E RID: 46 RVA: 0x000027AC File Offset: 0x000009AC
	public static void AppendRaw(string str)
	{
		ES3.AppendRaw(str, new ES3Settings(null, null));
	}

	// Token: 0x0600002F RID: 47 RVA: 0x000027BB File Offset: 0x000009BB
	public static void AppendRaw(string str, string filePath, ES3Settings settings)
	{
		ES3.AppendRaw(str, new ES3Settings(filePath, settings));
	}

	// Token: 0x06000030 RID: 48 RVA: 0x000027CC File Offset: 0x000009CC
	public static void AppendRaw(string str, ES3Settings settings)
	{
		byte[] bytes = settings.encoding.GetBytes(str);
		ES3Settings es3Settings = new ES3Settings(settings.path, settings);
		es3Settings.encryptionType = ES3.EncryptionType.None;
		es3Settings.compressionType = ES3.CompressionType.None;
		if (settings.location == ES3.Location.Cache)
		{
			ES3File.GetOrCreateCachedFile(settings).SaveRaw(bytes, null);
			return;
		}
		using (Stream stream = ES3Stream.CreateStream(es3Settings, ES3FileMode.Append))
		{
			stream.Write(bytes, 0, bytes.Length);
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00002848 File Offset: 0x00000A48
	public static void SaveImage(Texture2D texture, string imagePath)
	{
		ES3.SaveImage(texture, new ES3Settings(imagePath, null));
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00002857 File Offset: 0x00000A57
	public static void SaveImage(Texture2D texture, string imagePath, ES3Settings settings)
	{
		ES3.SaveImage(texture, new ES3Settings(imagePath, settings));
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00002866 File Offset: 0x00000A66
	public static void SaveImage(Texture2D texture, ES3Settings settings)
	{
		ES3.SaveImage(texture, 75, settings);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002871 File Offset: 0x00000A71
	public static void SaveImage(Texture2D texture, int quality, string imagePath)
	{
		ES3.SaveImage(texture, new ES3Settings(imagePath, null));
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00002880 File Offset: 0x00000A80
	public static void SaveImage(Texture2D texture, int quality, string imagePath, ES3Settings settings)
	{
		ES3.SaveImage(texture, quality, new ES3Settings(imagePath, settings));
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00002890 File Offset: 0x00000A90
	public static void SaveImage(Texture2D texture, int quality, ES3Settings settings)
	{
		string text = ES3IO.GetExtension(settings.path).ToLower();
		if (string.IsNullOrEmpty(text))
		{
			throw new ArgumentException("File path must have a file extension when using ES3.SaveImage.");
		}
		byte[] array;
		if (text == ".jpg" || text == ".jpeg")
		{
			array = texture.EncodeToJPG(quality);
		}
		else
		{
			if (!(text == ".png"))
			{
				throw new ArgumentException("File path must have extension of .png, .jpg or .jpeg when using ES3.SaveImage.");
			}
			array = texture.EncodeToPNG();
		}
		ES3.SaveRaw(array, settings);
	}

	// Token: 0x06000037 RID: 55 RVA: 0x0000290D File Offset: 0x00000B0D
	public static object Load(string key)
	{
		return ES3.Load<object>(key, new ES3Settings(null, null));
	}

	// Token: 0x06000038 RID: 56 RVA: 0x0000291C File Offset: 0x00000B1C
	public static object Load(string key, string filePath)
	{
		return ES3.Load<object>(key, new ES3Settings(filePath, null));
	}

	// Token: 0x06000039 RID: 57 RVA: 0x0000292B File Offset: 0x00000B2B
	public static object Load(string key, string filePath, ES3Settings settings)
	{
		return ES3.Load<object>(key, new ES3Settings(filePath, settings));
	}

	// Token: 0x0600003A RID: 58 RVA: 0x0000293A File Offset: 0x00000B3A
	public static object Load(string key, ES3Settings settings)
	{
		return ES3.Load<object>(key, settings);
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00002943 File Offset: 0x00000B43
	public static T Load<T>(string key)
	{
		return ES3.Load<T>(key, new ES3Settings(null, null));
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00002952 File Offset: 0x00000B52
	public static T Load<T>(string key, string filePath)
	{
		return ES3.Load<T>(key, new ES3Settings(filePath, null));
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00002961 File Offset: 0x00000B61
	public static T Load<T>(string key, string filePath, ES3Settings settings)
	{
		return ES3.Load<T>(key, new ES3Settings(filePath, settings));
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00002970 File Offset: 0x00000B70
	public static T Load<T>(string key, ES3Settings settings)
	{
		if (settings.location == ES3.Location.Cache)
		{
			return ES3File.GetOrCreateCachedFile(settings).Load<T>(key);
		}
		T t;
		using (ES3Reader es3Reader = ES3Reader.Create(settings))
		{
			if (es3Reader == null)
			{
				throw new FileNotFoundException("File \"" + settings.FullPath + "\" could not be found.");
			}
			t = es3Reader.Read<T>(key);
		}
		return t;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x000029E0 File Offset: 0x00000BE0
	public static T Load<T>(string key, T defaultValue)
	{
		return ES3.Load<T>(key, defaultValue, new ES3Settings(null, null));
	}

	// Token: 0x06000040 RID: 64 RVA: 0x000029F0 File Offset: 0x00000BF0
	public static T Load<T>(string key, string filePath, T defaultValue)
	{
		return ES3.Load<T>(key, defaultValue, new ES3Settings(filePath, null));
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00002A00 File Offset: 0x00000C00
	public static T Load<T>(string key, string filePath, T defaultValue, ES3Settings settings)
	{
		return ES3.Load<T>(key, defaultValue, new ES3Settings(filePath, settings));
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00002A10 File Offset: 0x00000C10
	public static T Load<T>(string key, T defaultValue, ES3Settings settings)
	{
		if (settings.location == ES3.Location.Cache)
		{
			return ES3File.GetOrCreateCachedFile(settings).Load<T>(key, defaultValue);
		}
		T t;
		using (ES3Reader es3Reader = ES3Reader.Create(settings))
		{
			if (es3Reader == null)
			{
				t = defaultValue;
			}
			else
			{
				t = es3Reader.Read<T>(key, defaultValue);
			}
		}
		return t;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00002A68 File Offset: 0x00000C68
	public static void LoadInto<T>(string key, object obj) where T : class
	{
		ES3.LoadInto<object>(key, obj, new ES3Settings(null, null));
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00002A78 File Offset: 0x00000C78
	public static void LoadInto(string key, string filePath, object obj)
	{
		ES3.LoadInto<object>(key, obj, new ES3Settings(filePath, null));
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00002A88 File Offset: 0x00000C88
	public static void LoadInto(string key, string filePath, object obj, ES3Settings settings)
	{
		ES3.LoadInto<object>(key, obj, new ES3Settings(filePath, settings));
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00002A98 File Offset: 0x00000C98
	public static void LoadInto(string key, object obj, ES3Settings settings)
	{
		ES3.LoadInto<object>(key, obj, settings);
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00002AA2 File Offset: 0x00000CA2
	public static void LoadInto<T>(string key, T obj) where T : class
	{
		ES3.LoadInto<T>(key, obj, new ES3Settings(null, null));
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00002AB2 File Offset: 0x00000CB2
	public static void LoadInto<T>(string key, string filePath, T obj) where T : class
	{
		ES3.LoadInto<T>(key, obj, new ES3Settings(filePath, null));
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00002AC2 File Offset: 0x00000CC2
	public static void LoadInto<T>(string key, string filePath, T obj, ES3Settings settings) where T : class
	{
		ES3.LoadInto<T>(key, obj, new ES3Settings(filePath, settings));
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00002AD4 File Offset: 0x00000CD4
	public static void LoadInto<T>(string key, T obj, ES3Settings settings) where T : class
	{
		if (ES3Reflection.IsValueType(obj.GetType()))
		{
			throw new InvalidOperationException("ES3.LoadInto can only be used with reference types, but the data you're loading is a value type. Use ES3.Load instead.");
		}
		if (settings.location == ES3.Location.Cache)
		{
			ES3File.GetOrCreateCachedFile(settings).LoadInto<T>(key, obj);
			return;
		}
		if (settings == null)
		{
			settings = new ES3Settings(null, null);
		}
		using (ES3Reader es3Reader = ES3Reader.Create(settings))
		{
			if (es3Reader == null)
			{
				throw new FileNotFoundException("File \"" + settings.FullPath + "\" could not be found.");
			}
			es3Reader.ReadInto<T>(key, obj);
		}
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00002B6C File Offset: 0x00000D6C
	public static string LoadString(string key, string defaultValue, ES3Settings settings)
	{
		return ES3.Load<string>(key, null, defaultValue, settings);
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00002B77 File Offset: 0x00000D77
	public static string LoadString(string key, string defaultValue, string filePath = null, ES3Settings settings = null)
	{
		return ES3.Load<string>(key, filePath, defaultValue, settings);
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00002B82 File Offset: 0x00000D82
	public static byte[] LoadRawBytes()
	{
		return ES3.LoadRawBytes(new ES3Settings(null, null));
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00002B90 File Offset: 0x00000D90
	public static byte[] LoadRawBytes(string filePath)
	{
		return ES3.LoadRawBytes(new ES3Settings(filePath, null));
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00002B9E File Offset: 0x00000D9E
	public static byte[] LoadRawBytes(string filePath, ES3Settings settings)
	{
		return ES3.LoadRawBytes(new ES3Settings(filePath, settings));
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00002BAC File Offset: 0x00000DAC
	public static byte[] LoadRawBytes(ES3Settings settings)
	{
		if (settings.location == ES3.Location.Cache)
		{
			return ES3File.GetOrCreateCachedFile(settings).LoadRawBytes();
		}
		byte[] array2;
		using (Stream stream = ES3Stream.CreateStream(settings, ES3FileMode.Read))
		{
			if (stream == null)
			{
				throw new FileNotFoundException("File " + settings.path + " could not be found");
			}
			if (stream.GetType() == typeof(GZipStream))
			{
				GZipStream gzipStream = (GZipStream)stream;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					ES3Stream.CopyTo(gzipStream, memoryStream);
					return memoryStream.ToArray();
				}
			}
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, array.Length);
			array2 = array;
		}
		return array2;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00002C7C File Offset: 0x00000E7C
	public static string LoadRawString()
	{
		return ES3.LoadRawString(new ES3Settings(null, null));
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00002C8A File Offset: 0x00000E8A
	public static string LoadRawString(string filePath)
	{
		return ES3.LoadRawString(new ES3Settings(filePath, null));
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00002C98 File Offset: 0x00000E98
	public static string LoadRawString(string filePath, ES3Settings settings)
	{
		return ES3.LoadRawString(new ES3Settings(filePath, settings));
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00002CA8 File Offset: 0x00000EA8
	public static string LoadRawString(ES3Settings settings)
	{
		byte[] array = ES3.LoadRawBytes(settings);
		return settings.encoding.GetString(array, 0, array.Length);
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00002CCC File Offset: 0x00000ECC
	public static Texture2D LoadImage(string imagePath)
	{
		return ES3.LoadImage(new ES3Settings(imagePath, null));
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00002CDA File Offset: 0x00000EDA
	public static Texture2D LoadImage(string imagePath, ES3Settings settings)
	{
		return ES3.LoadImage(new ES3Settings(imagePath, settings));
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00002CE8 File Offset: 0x00000EE8
	public static Texture2D LoadImage(ES3Settings settings)
	{
		return ES3.LoadImage(ES3.LoadRawBytes(settings));
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00002CF5 File Offset: 0x00000EF5
	public static Texture2D LoadImage(byte[] bytes)
	{
		Texture2D texture2D = new Texture2D(1, 1);
		texture2D.LoadImage(bytes);
		return texture2D;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00002D06 File Offset: 0x00000F06
	public static AudioClip LoadAudio(string audioFilePath, AudioType audioType)
	{
		return ES3.LoadAudio(audioFilePath, audioType, new ES3Settings(null, null));
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00002D18 File Offset: 0x00000F18
	public static AudioClip LoadAudio(string audioFilePath, AudioType audioType, ES3Settings settings)
	{
		if (settings.location != ES3.Location.File)
		{
			throw new InvalidOperationException("ES3.LoadAudio can only be used with the File save location");
		}
		if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
			throw new InvalidOperationException("You cannot use ES3.LoadAudio with WebGL");
		}
		string text = ES3IO.GetExtension(audioFilePath).ToLower();
		if (text == ".mp3" && (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer))
		{
			throw new InvalidOperationException("You can only load Ogg, WAV, XM, IT, MOD or S3M on Unity Standalone");
		}
		if (text == ".ogg" && (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.MetroPlayerARM))
		{
			throw new InvalidOperationException("You can only load MP3, WAV, XM, IT, MOD or S3M on Unity Standalone");
		}
		ES3Settings es3Settings = new ES3Settings(audioFilePath, settings);
		AudioClip content;
		using (UnityWebRequest audioClip = UnityWebRequestMultimedia.GetAudioClip("file://" + es3Settings.FullPath, audioType))
		{
			audioClip.SendWebRequest();
			while (!audioClip.isDone)
			{
			}
			if (ES3WebClass.IsNetworkError(audioClip))
			{
				throw new Exception(audioClip.error);
			}
			content = DownloadHandlerAudioClip.GetContent(audioClip);
		}
		return content;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00002E18 File Offset: 0x00001018
	public static byte[] Serialize<T>(T value, ES3Settings settings = null)
	{
		return ES3.Serialize(value, ES3TypeMgr.GetOrCreateES3Type(typeof(T), true), settings);
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00002E38 File Offset: 0x00001038
	internal static byte[] Serialize(object value, ES3Type type, ES3Settings settings = null)
	{
		if (settings == null)
		{
			settings = new ES3Settings(null, null);
		}
		byte[] array;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using (Stream stream = ES3Stream.CreateStream(memoryStream, settings, ES3FileMode.Write))
			{
				using (ES3Writer es3Writer = ES3Writer.Create(stream, settings, false, false))
				{
					es3Writer.Write(value, type, settings.referenceMode);
				}
				array = memoryStream.ToArray();
			}
		}
		return array;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00002ECC File Offset: 0x000010CC
	public static T Deserialize<T>(byte[] bytes, ES3Settings settings = null)
	{
		return (T)((object)ES3.Deserialize(ES3TypeMgr.GetOrCreateES3Type(typeof(T), true), bytes, settings));
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00002EEC File Offset: 0x000010EC
	internal static object Deserialize(ES3Type type, byte[] bytes, ES3Settings settings = null)
	{
		if (settings == null)
		{
			settings = new ES3Settings(null, null);
		}
		object obj;
		using (MemoryStream memoryStream = new MemoryStream(bytes, false))
		{
			using (Stream stream = ES3Stream.CreateStream(memoryStream, settings, ES3FileMode.Read))
			{
				using (ES3Reader es3Reader = ES3Reader.Create(stream, settings, false))
				{
					obj = es3Reader.Read<object>(type);
				}
			}
		}
		return obj;
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00002F70 File Offset: 0x00001170
	public static void DeserializeInto<T>(byte[] bytes, T obj, ES3Settings settings = null) where T : class
	{
		ES3.DeserializeInto<T>(ES3TypeMgr.GetOrCreateES3Type(typeof(T), true), bytes, obj, settings);
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00002F8C File Offset: 0x0000118C
	public static void DeserializeInto<T>(ES3Type type, byte[] bytes, T obj, ES3Settings settings = null) where T : class
	{
		if (settings == null)
		{
			settings = new ES3Settings(null, null);
		}
		using (MemoryStream memoryStream = new MemoryStream(bytes, false))
		{
			using (ES3Reader es3Reader = ES3Reader.Create(memoryStream, settings, false))
			{
				es3Reader.ReadInto<T>(obj, type);
			}
		}
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00002FF8 File Offset: 0x000011F8
	public static byte[] EncryptBytes(byte[] bytes, string password = null)
	{
		if (string.IsNullOrEmpty(password))
		{
			password = ES3Settings.defaultSettings.encryptionPassword;
		}
		return new AESEncryptionAlgorithm().Encrypt(bytes, password, ES3Settings.defaultSettings.bufferSize);
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00003024 File Offset: 0x00001224
	public static byte[] DecryptBytes(byte[] bytes, string password = null)
	{
		if (string.IsNullOrEmpty(password))
		{
			password = ES3Settings.defaultSettings.encryptionPassword;
		}
		return new AESEncryptionAlgorithm().Decrypt(bytes, password, ES3Settings.defaultSettings.bufferSize);
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00003050 File Offset: 0x00001250
	public static string EncryptString(string str, string password = null)
	{
		return ES3Settings.defaultSettings.encoding.GetString(ES3.EncryptBytes(ES3Settings.defaultSettings.encoding.GetBytes(str), password));
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00003077 File Offset: 0x00001277
	public static string DecryptString(string str, string password = null)
	{
		return ES3Settings.defaultSettings.encoding.GetString(ES3.DecryptBytes(ES3Settings.defaultSettings.encoding.GetBytes(str), password));
	}

	// Token: 0x06000065 RID: 101 RVA: 0x0000309E File Offset: 0x0000129E
	public static void DeleteFile()
	{
		ES3.DeleteFile(new ES3Settings(null, null));
	}

	// Token: 0x06000066 RID: 102 RVA: 0x000030AC File Offset: 0x000012AC
	public static void DeleteFile(string filePath)
	{
		ES3.DeleteFile(new ES3Settings(filePath, null));
	}

	// Token: 0x06000067 RID: 103 RVA: 0x000030BA File Offset: 0x000012BA
	public static void DeleteFile(string filePath, ES3Settings settings)
	{
		ES3.DeleteFile(new ES3Settings(filePath, settings));
	}

	// Token: 0x06000068 RID: 104 RVA: 0x000030C8 File Offset: 0x000012C8
	public static void DeleteFile(ES3Settings settings)
	{
		if (settings.location == ES3.Location.File)
		{
			ES3IO.DeleteFile(settings.FullPath);
			return;
		}
		if (settings.location == ES3.Location.PlayerPrefs)
		{
			PlayerPrefs.DeleteKey(settings.FullPath);
			return;
		}
		if (settings.location == ES3.Location.Cache)
		{
			ES3File.RemoveCachedFile(settings);
			return;
		}
		if (settings.location == ES3.Location.Resources)
		{
			throw new NotSupportedException("Deleting files from Resources is not supported.");
		}
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00003122 File Offset: 0x00001322
	public static void CopyFile(string oldFilePath, string newFilePath)
	{
		ES3.CopyFile(new ES3Settings(oldFilePath, null), new ES3Settings(newFilePath, null));
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00003137 File Offset: 0x00001337
	public static void CopyFile(string oldFilePath, string newFilePath, ES3Settings oldSettings, ES3Settings newSettings)
	{
		ES3.CopyFile(new ES3Settings(oldFilePath, oldSettings), new ES3Settings(newFilePath, newSettings));
	}

	// Token: 0x0600006B RID: 107 RVA: 0x0000314C File Offset: 0x0000134C
	public static void CopyFile(ES3Settings oldSettings, ES3Settings newSettings)
	{
		if (oldSettings.location != newSettings.location)
		{
			throw new InvalidOperationException(string.Concat(new string[]
			{
				"Cannot copy file from ",
				oldSettings.location.ToString(),
				" to ",
				newSettings.location.ToString(),
				". Location must be the same for both source and destination."
			}));
		}
		if (oldSettings.location == ES3.Location.File)
		{
			if (ES3IO.FileExists(oldSettings.FullPath))
			{
				ES3IO.DeleteFile(newSettings.FullPath);
				ES3IO.CopyFile(oldSettings.FullPath, newSettings.FullPath);
				return;
			}
		}
		else
		{
			if (oldSettings.location == ES3.Location.PlayerPrefs)
			{
				PlayerPrefs.SetString(newSettings.FullPath, PlayerPrefs.GetString(oldSettings.FullPath));
				return;
			}
			if (oldSettings.location == ES3.Location.Cache)
			{
				ES3File.CopyCachedFile(oldSettings, newSettings);
				return;
			}
			if (oldSettings.location == ES3.Location.Resources)
			{
				throw new NotSupportedException("Modifying files from Resources is not allowed.");
			}
		}
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00003235 File Offset: 0x00001435
	public static void RenameFile(string oldFilePath, string newFilePath)
	{
		ES3.RenameFile(new ES3Settings(oldFilePath, null), new ES3Settings(newFilePath, null));
	}

	// Token: 0x0600006D RID: 109 RVA: 0x0000324A File Offset: 0x0000144A
	public static void RenameFile(string oldFilePath, string newFilePath, ES3Settings oldSettings, ES3Settings newSettings)
	{
		ES3.RenameFile(new ES3Settings(oldFilePath, oldSettings), new ES3Settings(newFilePath, newSettings));
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00003260 File Offset: 0x00001460
	public static void RenameFile(ES3Settings oldSettings, ES3Settings newSettings)
	{
		if (oldSettings.location != newSettings.location)
		{
			throw new InvalidOperationException(string.Concat(new string[]
			{
				"Cannot rename file in ",
				oldSettings.location.ToString(),
				" to ",
				newSettings.location.ToString(),
				". Location must be the same for both source and destination."
			}));
		}
		if (oldSettings.location == ES3.Location.File)
		{
			if (ES3IO.FileExists(oldSettings.FullPath))
			{
				ES3IO.DeleteFile(newSettings.FullPath);
				ES3IO.MoveFile(oldSettings.FullPath, newSettings.FullPath);
				return;
			}
		}
		else
		{
			if (oldSettings.location == ES3.Location.PlayerPrefs)
			{
				PlayerPrefs.SetString(newSettings.FullPath, PlayerPrefs.GetString(oldSettings.FullPath));
				PlayerPrefs.DeleteKey(oldSettings.FullPath);
				return;
			}
			if (oldSettings.location == ES3.Location.Cache)
			{
				ES3File.CopyCachedFile(oldSettings, newSettings);
				ES3File.RemoveCachedFile(oldSettings);
				return;
			}
			if (oldSettings.location == ES3.Location.Resources)
			{
				throw new NotSupportedException("Modifying files from Resources is not allowed.");
			}
		}
	}

	// Token: 0x0600006F RID: 111 RVA: 0x0000335A File Offset: 0x0000155A
	public static void CopyDirectory(string oldDirectoryPath, string newDirectoryPath)
	{
		ES3.CopyDirectory(new ES3Settings(oldDirectoryPath, null), new ES3Settings(newDirectoryPath, null));
	}

	// Token: 0x06000070 RID: 112 RVA: 0x0000336F File Offset: 0x0000156F
	public static void CopyDirectory(string oldDirectoryPath, string newDirectoryPath, ES3Settings oldSettings, ES3Settings newSettings)
	{
		ES3.CopyDirectory(new ES3Settings(oldDirectoryPath, oldSettings), new ES3Settings(newDirectoryPath, newSettings));
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00003384 File Offset: 0x00001584
	public static void CopyDirectory(ES3Settings oldSettings, ES3Settings newSettings)
	{
		if (oldSettings.location != ES3.Location.File)
		{
			throw new InvalidOperationException("ES3.CopyDirectory can only be used when the save location is 'File'");
		}
		if (!ES3.DirectoryExists(oldSettings))
		{
			throw new DirectoryNotFoundException("Directory " + oldSettings.FullPath + " not found");
		}
		if (!ES3.DirectoryExists(newSettings))
		{
			ES3IO.CreateDirectory(newSettings.FullPath);
		}
		foreach (string text in ES3.GetFiles(oldSettings))
		{
			ES3.CopyFile(ES3IO.CombinePathAndFilename(oldSettings.path, text), ES3IO.CombinePathAndFilename(newSettings.path, text));
		}
		foreach (string text2 in ES3.GetDirectories(oldSettings))
		{
			ES3.CopyDirectory(ES3IO.CombinePathAndFilename(oldSettings.path, text2), ES3IO.CombinePathAndFilename(newSettings.path, text2));
		}
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00003446 File Offset: 0x00001646
	public static void RenameDirectory(string oldDirectoryPath, string newDirectoryPath)
	{
		ES3.RenameDirectory(new ES3Settings(oldDirectoryPath, null), new ES3Settings(newDirectoryPath, null));
	}

	// Token: 0x06000073 RID: 115 RVA: 0x0000345B File Offset: 0x0000165B
	public static void RenameDirectory(string oldDirectoryPath, string newDirectoryPath, ES3Settings oldSettings, ES3Settings newSettings)
	{
		ES3.RenameDirectory(new ES3Settings(oldDirectoryPath, oldSettings), new ES3Settings(newDirectoryPath, newSettings));
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00003470 File Offset: 0x00001670
	public static void RenameDirectory(ES3Settings oldSettings, ES3Settings newSettings)
	{
		if (oldSettings.location == ES3.Location.File)
		{
			if (ES3IO.DirectoryExists(oldSettings.FullPath))
			{
				ES3IO.DeleteDirectory(newSettings.FullPath);
				ES3IO.MoveDirectory(oldSettings.FullPath, newSettings.FullPath);
				return;
			}
		}
		else
		{
			if (oldSettings.location == ES3.Location.PlayerPrefs || oldSettings.location == ES3.Location.Cache)
			{
				throw new NotSupportedException("Directories cannot be renamed when saving to Cache, PlayerPrefs, tvOS or using WebGL.");
			}
			if (oldSettings.location == ES3.Location.Resources)
			{
				throw new NotSupportedException("Modifying files from Resources is not allowed.");
			}
		}
	}

	// Token: 0x06000075 RID: 117 RVA: 0x000034E0 File Offset: 0x000016E0
	public static void DeleteDirectory(string directoryPath)
	{
		ES3.DeleteDirectory(new ES3Settings(directoryPath, null));
	}

	// Token: 0x06000076 RID: 118 RVA: 0x000034EE File Offset: 0x000016EE
	public static void DeleteDirectory(string directoryPath, ES3Settings settings)
	{
		ES3.DeleteDirectory(new ES3Settings(directoryPath, settings));
	}

	// Token: 0x06000077 RID: 119 RVA: 0x000034FC File Offset: 0x000016FC
	public static void DeleteDirectory(ES3Settings settings)
	{
		if (settings.location == ES3.Location.File)
		{
			ES3IO.DeleteDirectory(settings.FullPath);
			return;
		}
		if (settings.location == ES3.Location.PlayerPrefs || settings.location == ES3.Location.Cache)
		{
			throw new NotSupportedException("Deleting Directories using Cache or PlayerPrefs is not supported.");
		}
		if (settings.location == ES3.Location.Resources)
		{
			throw new NotSupportedException("Deleting directories from Resources is not allowed.");
		}
	}

	// Token: 0x06000078 RID: 120 RVA: 0x0000354E File Offset: 0x0000174E
	public static void DeleteKey(string key)
	{
		ES3.DeleteKey(key, new ES3Settings(null, null));
	}

	// Token: 0x06000079 RID: 121 RVA: 0x0000355D File Offset: 0x0000175D
	public static void DeleteKey(string key, string filePath)
	{
		ES3.DeleteKey(key, new ES3Settings(filePath, null));
	}

	// Token: 0x0600007A RID: 122 RVA: 0x0000356C File Offset: 0x0000176C
	public static void DeleteKey(string key, string filePath, ES3Settings settings)
	{
		ES3.DeleteKey(key, new ES3Settings(filePath, settings));
	}

	// Token: 0x0600007B RID: 123 RVA: 0x0000357C File Offset: 0x0000177C
	public static void DeleteKey(string key, ES3Settings settings)
	{
		if (settings.location == ES3.Location.Resources)
		{
			throw new NotSupportedException("Modifying files in Resources is not allowed.");
		}
		if (settings.location == ES3.Location.Cache)
		{
			ES3File.DeleteKey(key, settings);
			return;
		}
		if (ES3.FileExists(settings))
		{
			using (ES3Writer es3Writer = ES3Writer.Create(settings))
			{
				es3Writer.MarkKeyForDeletion(key);
				es3Writer.Save();
			}
		}
	}

	// Token: 0x0600007C RID: 124 RVA: 0x000035E8 File Offset: 0x000017E8
	public static bool KeyExists(string key)
	{
		return ES3.KeyExists(key, new ES3Settings(null, null));
	}

	// Token: 0x0600007D RID: 125 RVA: 0x000035F7 File Offset: 0x000017F7
	public static bool KeyExists(string key, string filePath)
	{
		return ES3.KeyExists(key, new ES3Settings(filePath, null));
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00003606 File Offset: 0x00001806
	public static bool KeyExists(string key, string filePath, ES3Settings settings)
	{
		return ES3.KeyExists(key, new ES3Settings(filePath, settings));
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00003618 File Offset: 0x00001818
	public static bool KeyExists(string key, ES3Settings settings)
	{
		if (settings.location == ES3.Location.Cache)
		{
			return ES3File.KeyExists(key, settings);
		}
		bool flag;
		using (ES3Reader es3Reader = ES3Reader.Create(settings))
		{
			if (es3Reader == null)
			{
				flag = false;
			}
			else
			{
				flag = es3Reader.Goto(key);
			}
		}
		return flag;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x0000366C File Offset: 0x0000186C
	public static bool FileExists()
	{
		return ES3.FileExists(new ES3Settings(null, null));
	}

	// Token: 0x06000081 RID: 129 RVA: 0x0000367A File Offset: 0x0000187A
	public static bool FileExists(string filePath)
	{
		return ES3.FileExists(new ES3Settings(filePath, null));
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00003688 File Offset: 0x00001888
	public static bool FileExists(string filePath, ES3Settings settings)
	{
		return ES3.FileExists(new ES3Settings(filePath, settings));
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00003698 File Offset: 0x00001898
	public static bool FileExists(ES3Settings settings)
	{
		if (settings.location == ES3.Location.File)
		{
			return ES3IO.FileExists(settings.FullPath);
		}
		if (settings.location == ES3.Location.PlayerPrefs)
		{
			return PlayerPrefs.HasKey(settings.FullPath);
		}
		if (settings.location == ES3.Location.Cache)
		{
			return ES3File.FileExists(settings);
		}
		return settings.location == ES3.Location.Resources && Resources.Load(settings.FullPath) != null;
	}

	// Token: 0x06000084 RID: 132 RVA: 0x000036FA File Offset: 0x000018FA
	public static bool DirectoryExists(string folderPath)
	{
		return ES3.DirectoryExists(new ES3Settings(folderPath, null));
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00003708 File Offset: 0x00001908
	public static bool DirectoryExists(string folderPath, ES3Settings settings)
	{
		return ES3.DirectoryExists(new ES3Settings(folderPath, settings));
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00003718 File Offset: 0x00001918
	public static bool DirectoryExists(ES3Settings settings)
	{
		if (settings.location == ES3.Location.File)
		{
			return ES3IO.DirectoryExists(settings.FullPath);
		}
		if (settings.location == ES3.Location.PlayerPrefs || settings.location == ES3.Location.Cache)
		{
			throw new NotSupportedException("Directories are not supported for the Cache and PlayerPrefs location.");
		}
		if (settings.location == ES3.Location.Resources)
		{
			throw new NotSupportedException("Checking existence of folder in Resources not supported.");
		}
		return false;
	}

	// Token: 0x06000087 RID: 135 RVA: 0x0000376B File Offset: 0x0000196B
	public static string[] GetKeys()
	{
		return ES3.GetKeys(new ES3Settings(null, null));
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00003779 File Offset: 0x00001979
	public static string[] GetKeys(string filePath)
	{
		return ES3.GetKeys(new ES3Settings(filePath, null));
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00003787 File Offset: 0x00001987
	public static string[] GetKeys(string filePath, ES3Settings settings)
	{
		return ES3.GetKeys(new ES3Settings(filePath, settings));
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00003798 File Offset: 0x00001998
	public static string[] GetKeys(ES3Settings settings)
	{
		if (settings.location == ES3.Location.Cache)
		{
			return ES3File.GetKeys(settings);
		}
		List<string> list = new List<string>();
		using (ES3Reader es3Reader = ES3Reader.Create(settings))
		{
			foreach (object obj in es3Reader.Properties)
			{
				string text = (string)obj;
				list.Add(text);
				es3Reader.Skip();
			}
		}
		return list.ToArray();
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00003834 File Offset: 0x00001A34
	public static string[] GetFiles()
	{
		ES3Settings es3Settings = new ES3Settings(null, null);
		if (es3Settings.location == ES3.Location.File)
		{
			if (es3Settings.directory == ES3.Directory.PersistentDataPath)
			{
				es3Settings.path = Application.persistentDataPath;
			}
			else
			{
				es3Settings.path = Application.dataPath;
			}
		}
		return ES3.GetFiles(es3Settings);
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00003877 File Offset: 0x00001A77
	public static string[] GetFiles(string directoryPath)
	{
		return ES3.GetFiles(new ES3Settings(directoryPath, null));
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00003885 File Offset: 0x00001A85
	public static string[] GetFiles(string directoryPath, ES3Settings settings)
	{
		return ES3.GetFiles(new ES3Settings(directoryPath, settings));
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00003893 File Offset: 0x00001A93
	public static string[] GetFiles(ES3Settings settings)
	{
		if (settings.location == ES3.Location.Cache)
		{
			return ES3File.GetFiles();
		}
		if (settings.location != ES3.Location.File)
		{
			throw new NotSupportedException("ES3.GetFiles can only be used when the location is set to File or Cache.");
		}
		return ES3IO.GetFiles(settings.FullPath, false);
	}

	// Token: 0x0600008F RID: 143 RVA: 0x000038C3 File Offset: 0x00001AC3
	public static string[] GetDirectories()
	{
		return ES3.GetDirectories(new ES3Settings(null, null));
	}

	// Token: 0x06000090 RID: 144 RVA: 0x000038D1 File Offset: 0x00001AD1
	public static string[] GetDirectories(string directoryPath)
	{
		return ES3.GetDirectories(new ES3Settings(directoryPath, null));
	}

	// Token: 0x06000091 RID: 145 RVA: 0x000038DF File Offset: 0x00001ADF
	public static string[] GetDirectories(string directoryPath, ES3Settings settings)
	{
		return ES3.GetDirectories(new ES3Settings(directoryPath, settings));
	}

	// Token: 0x06000092 RID: 146 RVA: 0x000038ED File Offset: 0x00001AED
	public static string[] GetDirectories(ES3Settings settings)
	{
		if (settings.location != ES3.Location.File)
		{
			throw new NotSupportedException("ES3.GetDirectories can only be used when the location is set to File.");
		}
		return ES3IO.GetDirectories(settings.FullPath, false);
	}

	// Token: 0x06000093 RID: 147 RVA: 0x0000390E File Offset: 0x00001B0E
	public static void CreateBackup()
	{
		ES3.CreateBackup(new ES3Settings(null, null));
	}

	// Token: 0x06000094 RID: 148 RVA: 0x0000391C File Offset: 0x00001B1C
	public static void CreateBackup(string filePath)
	{
		ES3.CreateBackup(new ES3Settings(filePath, null));
	}

	// Token: 0x06000095 RID: 149 RVA: 0x0000392A File Offset: 0x00001B2A
	public static void CreateBackup(string filePath, ES3Settings settings)
	{
		ES3.CreateBackup(new ES3Settings(filePath, settings));
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00003938 File Offset: 0x00001B38
	public static void CreateBackup(ES3Settings settings)
	{
		ES3Settings es3Settings = new ES3Settings(settings.path + ".bac", settings);
		ES3.CopyFile(settings, es3Settings);
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00003963 File Offset: 0x00001B63
	public static bool RestoreBackup(string filePath)
	{
		return ES3.RestoreBackup(new ES3Settings(filePath, null));
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00003971 File Offset: 0x00001B71
	public static bool RestoreBackup(string filePath, ES3Settings settings)
	{
		return ES3.RestoreBackup(new ES3Settings(filePath, settings));
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00003980 File Offset: 0x00001B80
	public static bool RestoreBackup(ES3Settings settings)
	{
		ES3Settings es3Settings = new ES3Settings(settings.path + ".bac", settings);
		if (!ES3.FileExists(es3Settings))
		{
			return false;
		}
		ES3.RenameFile(es3Settings, settings);
		return true;
	}

	// Token: 0x0600009A RID: 154 RVA: 0x000039B6 File Offset: 0x00001BB6
	public static DateTime GetTimestamp()
	{
		return ES3.GetTimestamp(new ES3Settings(null, null));
	}

	// Token: 0x0600009B RID: 155 RVA: 0x000039C4 File Offset: 0x00001BC4
	public static DateTime GetTimestamp(string filePath)
	{
		return ES3.GetTimestamp(new ES3Settings(filePath, null));
	}

	// Token: 0x0600009C RID: 156 RVA: 0x000039D2 File Offset: 0x00001BD2
	public static DateTime GetTimestamp(string filePath, ES3Settings settings)
	{
		return ES3.GetTimestamp(new ES3Settings(filePath, settings));
	}

	// Token: 0x0600009D RID: 157 RVA: 0x000039E0 File Offset: 0x00001BE0
	public static DateTime GetTimestamp(ES3Settings settings)
	{
		if (settings.location == ES3.Location.File)
		{
			return ES3IO.GetTimestamp(settings.FullPath);
		}
		if (settings.location == ES3.Location.PlayerPrefs)
		{
			return new DateTime(long.Parse(PlayerPrefs.GetString("timestamp_" + settings.FullPath, "0")), DateTimeKind.Utc);
		}
		if (settings.location == ES3.Location.Cache)
		{
			return ES3File.GetTimestamp(settings);
		}
		return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
	}

	// Token: 0x0600009E RID: 158 RVA: 0x00003A51 File Offset: 0x00001C51
	public static void StoreCachedFile()
	{
		ES3File.Store(null);
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00003A59 File Offset: 0x00001C59
	public static void StoreCachedFile(string filePath)
	{
		ES3.StoreCachedFile(new ES3Settings(filePath, null));
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00003A67 File Offset: 0x00001C67
	public static void StoreCachedFile(string filePath, ES3Settings settings)
	{
		ES3.StoreCachedFile(new ES3Settings(filePath, settings));
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00003A75 File Offset: 0x00001C75
	public static void StoreCachedFile(ES3Settings settings)
	{
		ES3File.Store(settings);
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00003A7D File Offset: 0x00001C7D
	public static void CacheFile()
	{
		ES3.CacheFile(new ES3Settings(null, null));
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x00003A8B File Offset: 0x00001C8B
	public static void CacheFile(string filePath)
	{
		ES3.CacheFile(new ES3Settings(filePath, null));
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00003A99 File Offset: 0x00001C99
	public static void CacheFile(string filePath, ES3Settings settings)
	{
		ES3.CacheFile(new ES3Settings(filePath, settings));
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00003AA7 File Offset: 0x00001CA7
	public static void CacheFile(ES3Settings settings)
	{
		ES3File.CacheFile(settings);
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00003AAF File Offset: 0x00001CAF
	public static void Init()
	{
		ES3Settings defaultSettings = ES3Settings.defaultSettings;
		ES3TypeMgr.Init();
	}

	// Token: 0x020000E2 RID: 226
	public enum Location
	{
		// Token: 0x04000167 RID: 359
		File,
		// Token: 0x04000168 RID: 360
		PlayerPrefs,
		// Token: 0x04000169 RID: 361
		InternalMS,
		// Token: 0x0400016A RID: 362
		Resources,
		// Token: 0x0400016B RID: 363
		Cache
	}

	// Token: 0x020000E3 RID: 227
	public enum Directory
	{
		// Token: 0x0400016D RID: 365
		PersistentDataPath,
		// Token: 0x0400016E RID: 366
		DataPath
	}

	// Token: 0x020000E4 RID: 228
	public enum EncryptionType
	{
		// Token: 0x04000170 RID: 368
		None,
		// Token: 0x04000171 RID: 369
		AES
	}

	// Token: 0x020000E5 RID: 229
	public enum CompressionType
	{
		// Token: 0x04000173 RID: 371
		None,
		// Token: 0x04000174 RID: 372
		Gzip
	}

	// Token: 0x020000E6 RID: 230
	public enum Format
	{
		// Token: 0x04000176 RID: 374
		JSON
	}

	// Token: 0x020000E7 RID: 231
	public enum ReferenceMode
	{
		// Token: 0x04000178 RID: 376
		ByRef,
		// Token: 0x04000179 RID: 377
		ByValue,
		// Token: 0x0400017A RID: 378
		ByRefAndValue
	}
}
