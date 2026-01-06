using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using ES3Internal;

// Token: 0x02000008 RID: 8
public class ES3File
{
	// Token: 0x060000A7 RID: 167 RVA: 0x00003ABC File Offset: 0x00001CBC
	public ES3File()
		: this(new ES3Settings(null, null), true)
	{
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x00003ACC File Offset: 0x00001CCC
	public ES3File(string filePath)
		: this(new ES3Settings(filePath, null), true)
	{
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x00003ADC File Offset: 0x00001CDC
	public ES3File(string filePath, ES3Settings settings)
		: this(new ES3Settings(filePath, settings), true)
	{
	}

	// Token: 0x060000AA RID: 170 RVA: 0x00003AEC File Offset: 0x00001CEC
	public ES3File(ES3Settings settings)
		: this(settings, true)
	{
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00003AF6 File Offset: 0x00001CF6
	public ES3File(bool syncWithFile)
		: this(new ES3Settings(null, null), syncWithFile)
	{
	}

	// Token: 0x060000AC RID: 172 RVA: 0x00003B06 File Offset: 0x00001D06
	public ES3File(string filePath, bool syncWithFile)
		: this(new ES3Settings(filePath, null), syncWithFile)
	{
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00003B16 File Offset: 0x00001D16
	public ES3File(string filePath, ES3Settings settings, bool syncWithFile)
		: this(new ES3Settings(filePath, settings), syncWithFile)
	{
	}

	// Token: 0x060000AE RID: 174 RVA: 0x00003B28 File Offset: 0x00001D28
	public ES3File(ES3Settings settings, bool syncWithFile)
	{
		this.cache = new Dictionary<string, ES3Data>();
		this.timestamp = DateTime.UtcNow;
		base..ctor();
		this.settings = settings;
		this.syncWithFile = syncWithFile;
		if (syncWithFile)
		{
			ES3Settings es3Settings = (ES3Settings)settings.Clone();
			es3Settings.typeChecking = true;
			using (ES3Reader es3Reader = ES3Reader.Create(es3Settings))
			{
				if (es3Reader != null)
				{
					foreach (object obj in es3Reader.RawEnumerator)
					{
						KeyValuePair<string, ES3Data> keyValuePair = (KeyValuePair<string, ES3Data>)obj;
						this.cache[keyValuePair.Key] = keyValuePair.Value;
					}
				}
			}
		}
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00003BF4 File Offset: 0x00001DF4
	public ES3File(byte[] bytes, ES3Settings settings = null)
	{
		this.cache = new Dictionary<string, ES3Data>();
		this.timestamp = DateTime.UtcNow;
		base..ctor();
		if (settings == null)
		{
			this.settings = new ES3Settings(null, null);
		}
		else
		{
			this.settings = settings;
		}
		this.SaveRaw(bytes, settings);
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x00003C33 File Offset: 0x00001E33
	public void Sync()
	{
		this.Sync(this.settings);
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x00003C41 File Offset: 0x00001E41
	public void Sync(string filePath, ES3Settings settings = null)
	{
		this.Sync(new ES3Settings(filePath, settings));
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00003C50 File Offset: 0x00001E50
	public void Sync(ES3Settings settings = null)
	{
		if (settings == null)
		{
			settings = new ES3Settings(null, null);
		}
		ES3.DeleteFile(settings);
		if (this.cache.Count == 0)
		{
			return;
		}
		using (ES3Writer es3Writer = ES3Writer.Create(settings, true, !this.syncWithFile, false))
		{
			foreach (KeyValuePair<string, ES3Data> keyValuePair in this.cache)
			{
				Type type;
				if (keyValuePair.Value.type == null)
				{
					type = typeof(object);
				}
				else
				{
					type = keyValuePair.Value.type.type;
				}
				es3Writer.Write(keyValuePair.Key, type, keyValuePair.Value.bytes);
			}
			es3Writer.Save(!this.syncWithFile);
		}
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x00003D3C File Offset: 0x00001F3C
	public void Clear()
	{
		this.cache.Clear();
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00003D4C File Offset: 0x00001F4C
	public string[] GetKeys()
	{
		Dictionary<string, ES3Data>.KeyCollection keys = this.cache.Keys;
		string[] array = new string[keys.Count];
		keys.CopyTo(array, 0);
		return array;
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00003D78 File Offset: 0x00001F78
	public void Save<T>(string key, T value)
	{
		ES3Settings es3Settings = (ES3Settings)this.settings.Clone();
		es3Settings.encryptionType = ES3.EncryptionType.None;
		es3Settings.compressionType = ES3.CompressionType.None;
		this.cache[key] = new ES3Data(ES3TypeMgr.GetOrCreateES3Type(typeof(T), true), ES3.Serialize<T>(value, es3Settings));
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00003DCC File Offset: 0x00001FCC
	public void SaveRaw(byte[] bytes, ES3Settings settings = null)
	{
		if (settings == null)
		{
			settings = new ES3Settings(null, null);
		}
		ES3Settings es3Settings = (ES3Settings)settings.Clone();
		es3Settings.typeChecking = true;
		using (ES3Reader es3Reader = ES3Reader.Create(bytes, es3Settings))
		{
			if (es3Reader != null)
			{
				foreach (object obj in es3Reader.RawEnumerator)
				{
					KeyValuePair<string, ES3Data> keyValuePair = (KeyValuePair<string, ES3Data>)obj;
					this.cache[keyValuePair.Key] = keyValuePair.Value;
				}
			}
		}
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00003E7C File Offset: 0x0000207C
	public void AppendRaw(byte[] bytes, ES3Settings settings = null)
	{
		if (settings == null)
		{
			settings = new ES3Settings(null, null);
		}
		this.SaveRaw(bytes, settings);
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00003E92 File Offset: 0x00002092
	public object Load(string key)
	{
		return this.Load<object>(key);
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00003E9B File Offset: 0x0000209B
	public object Load(string key, object defaultValue)
	{
		return this.Load<object>(key, defaultValue);
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00003EA8 File Offset: 0x000020A8
	public T Load<T>(string key)
	{
		ES3Data es3Data;
		if (!this.cache.TryGetValue(key, out es3Data))
		{
			throw new KeyNotFoundException("Key \"" + key + "\" was not found in this ES3File. Use Load<T>(key, defaultValue) if you want to return a default value if the key does not exist.");
		}
		ES3Settings es3Settings = (ES3Settings)this.settings.Clone();
		es3Settings.encryptionType = ES3.EncryptionType.None;
		es3Settings.compressionType = ES3.CompressionType.None;
		if (typeof(T) == typeof(object))
		{
			return (T)((object)ES3.Deserialize(es3Data.type, es3Data.bytes, es3Settings));
		}
		return ES3.Deserialize<T>(es3Data.bytes, es3Settings);
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00003F3C File Offset: 0x0000213C
	public T Load<T>(string key, T defaultValue)
	{
		ES3Data es3Data;
		if (!this.cache.TryGetValue(key, out es3Data))
		{
			return defaultValue;
		}
		ES3Settings es3Settings = (ES3Settings)this.settings.Clone();
		es3Settings.encryptionType = ES3.EncryptionType.None;
		es3Settings.compressionType = ES3.CompressionType.None;
		if (typeof(T) == typeof(object))
		{
			return (T)((object)ES3.Deserialize(es3Data.type, es3Data.bytes, es3Settings));
		}
		return ES3.Deserialize<T>(es3Data.bytes, es3Settings);
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00003FBC File Offset: 0x000021BC
	public void LoadInto<T>(string key, T obj) where T : class
	{
		ES3Data es3Data;
		if (!this.cache.TryGetValue(key, out es3Data))
		{
			throw new KeyNotFoundException("Key \"" + key + "\" was not found in this ES3File. Use Load<T>(key, defaultValue) if you want to return a default value if the key does not exist.");
		}
		ES3Settings es3Settings = (ES3Settings)this.settings.Clone();
		es3Settings.encryptionType = ES3.EncryptionType.None;
		es3Settings.compressionType = ES3.CompressionType.None;
		if (typeof(T) == typeof(object))
		{
			ES3.DeserializeInto<T>(es3Data.type, es3Data.bytes, obj, es3Settings);
			return;
		}
		ES3.DeserializeInto<T>(es3Data.bytes, obj, es3Settings);
	}

	// Token: 0x060000BD RID: 189 RVA: 0x0000404C File Offset: 0x0000224C
	public byte[] LoadRawBytes()
	{
		ES3Settings es3Settings = (ES3Settings)this.settings.Clone();
		es3Settings.encryptionType = ES3.EncryptionType.None;
		es3Settings.compressionType = ES3.CompressionType.None;
		return this.GetBytes(es3Settings);
	}

	// Token: 0x060000BE RID: 190 RVA: 0x0000407F File Offset: 0x0000227F
	public string LoadRawString()
	{
		if (this.cache.Count == 0)
		{
			return "";
		}
		return this.settings.encoding.GetString(this.LoadRawBytes());
	}

	// Token: 0x060000BF RID: 191 RVA: 0x000040AC File Offset: 0x000022AC
	internal byte[] GetBytes(ES3Settings settings = null)
	{
		if (this.cache.Count == 0)
		{
			return new byte[0];
		}
		if (settings == null)
		{
			settings = this.settings;
		}
		byte[] array;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			ES3Settings es3Settings = (ES3Settings)settings.Clone();
			es3Settings.location = ES3.Location.InternalMS;
			es3Settings.encryptionType = ES3.EncryptionType.None;
			es3Settings.compressionType = ES3.CompressionType.None;
			using (ES3Writer es3Writer = ES3Writer.Create(ES3Stream.CreateStream(memoryStream, es3Settings, ES3FileMode.Write), es3Settings, true, false))
			{
				foreach (KeyValuePair<string, ES3Data> keyValuePair in this.cache)
				{
					es3Writer.Write(keyValuePair.Key, keyValuePair.Value.type.type, keyValuePair.Value.bytes);
				}
				es3Writer.Save(false);
			}
			array = memoryStream.ToArray();
		}
		return array;
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x000041BC File Offset: 0x000023BC
	public void DeleteKey(string key)
	{
		this.cache.Remove(key);
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x000041CB File Offset: 0x000023CB
	public bool KeyExists(string key)
	{
		return this.cache.ContainsKey(key);
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x000041DC File Offset: 0x000023DC
	public int Size()
	{
		int num = 0;
		foreach (KeyValuePair<string, ES3Data> keyValuePair in this.cache)
		{
			num += keyValuePair.Value.bytes.Length;
		}
		return num;
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x0000423C File Offset: 0x0000243C
	public Type GetKeyType(string key)
	{
		ES3Data es3Data;
		if (!this.cache.TryGetValue(key, out es3Data))
		{
			throw new KeyNotFoundException("Key \"" + key + "\" was not found in this ES3File. Use Load<T>(key, defaultValue) if you want to return a default value if the key does not exist.");
		}
		return es3Data.type.type;
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x0000427C File Offset: 0x0000247C
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static ES3File GetOrCreateCachedFile(ES3Settings settings)
	{
		ES3File es3File;
		if (!ES3File.cachedFiles.TryGetValue(settings.path, out es3File))
		{
			es3File = new ES3File(settings, false);
			ES3File.cachedFiles.Add(settings.path, es3File);
		}
		es3File.settings = settings;
		return es3File;
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x000042C0 File Offset: 0x000024C0
	internal static void CacheFile(ES3Settings settings)
	{
		if (settings.location == ES3.Location.Cache)
		{
			settings = (ES3Settings)settings.Clone();
			settings.location = ES3.Location.File;
		}
		if (!ES3.FileExists(settings))
		{
			return;
		}
		ES3Settings es3Settings = (ES3Settings)settings.Clone();
		es3Settings.compressionType = ES3.CompressionType.None;
		es3Settings.encryptionType = ES3.EncryptionType.None;
		ES3File.cachedFiles[settings.path] = new ES3File(ES3.LoadRawBytes(es3Settings), settings);
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x0000432C File Offset: 0x0000252C
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static void Store(ES3Settings settings = null)
	{
		if (settings == null)
		{
			settings = new ES3Settings(new Enum[] { ES3.Location.File });
		}
		else if (settings.location == ES3.Location.Cache)
		{
			settings = (ES3Settings)settings.Clone();
			settings.location = ES3.Location.File;
		}
		ES3File es3File;
		if (!ES3File.cachedFiles.TryGetValue(settings.path, out es3File))
		{
			throw new FileNotFoundException("The file '" + settings.path + "' could not be stored because it could not be found in the cache.");
		}
		es3File.Sync(settings);
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x000043A7 File Offset: 0x000025A7
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static void RemoveCachedFile(ES3Settings settings)
	{
		ES3File.cachedFiles.Remove(settings.path);
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x000043BC File Offset: 0x000025BC
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static void CopyCachedFile(ES3Settings oldSettings, ES3Settings newSettings)
	{
		ES3File es3File;
		if (!ES3File.cachedFiles.TryGetValue(oldSettings.path, out es3File))
		{
			throw new FileNotFoundException("The file '" + oldSettings.path + "' could not be copied because it could not be found in the cache.");
		}
		if (ES3File.cachedFiles.ContainsKey(newSettings.path))
		{
			throw new InvalidOperationException(string.Concat(new string[] { "Cannot copy file '", oldSettings.path, "' to '", newSettings.path, "' because '", newSettings.path, "' already exists" }));
		}
		ES3File.cachedFiles.Add(newSettings.path, (ES3File)es3File.MemberwiseClone());
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00004474 File Offset: 0x00002674
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static void DeleteKey(string key, ES3Settings settings)
	{
		ES3File es3File;
		if (ES3File.cachedFiles.TryGetValue(settings.path, out es3File))
		{
			es3File.DeleteKey(key);
		}
	}

	// Token: 0x060000CA RID: 202 RVA: 0x0000449C File Offset: 0x0000269C
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static bool KeyExists(string key, ES3Settings settings)
	{
		ES3File es3File;
		return ES3File.cachedFiles.TryGetValue(settings.path, out es3File) && es3File.KeyExists(key);
	}

	// Token: 0x060000CB RID: 203 RVA: 0x000044C6 File Offset: 0x000026C6
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static bool FileExists(ES3Settings settings)
	{
		return ES3File.cachedFiles.ContainsKey(settings.path);
	}

	// Token: 0x060000CC RID: 204 RVA: 0x000044D8 File Offset: 0x000026D8
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static string[] GetKeys(ES3Settings settings)
	{
		ES3File es3File;
		if (!ES3File.cachedFiles.TryGetValue(settings.path, out es3File))
		{
			throw new FileNotFoundException("Could not get keys from the file '" + settings.path + "' because it could not be found in the cache.");
		}
		return es3File.cache.Keys.ToArray<string>();
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00004524 File Offset: 0x00002724
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static string[] GetFiles()
	{
		return ES3File.cachedFiles.Keys.ToArray<string>();
	}

	// Token: 0x060000CE RID: 206 RVA: 0x00004538 File Offset: 0x00002738
	internal static DateTime GetTimestamp(ES3Settings settings)
	{
		ES3File es3File;
		if (!ES3File.cachedFiles.TryGetValue(settings.path, out es3File))
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		}
		return es3File.timestamp;
	}

	// Token: 0x04000011 RID: 17
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static Dictionary<string, ES3File> cachedFiles = new Dictionary<string, ES3File>();

	// Token: 0x04000012 RID: 18
	public ES3Settings settings;

	// Token: 0x04000013 RID: 19
	private Dictionary<string, ES3Data> cache;

	// Token: 0x04000014 RID: 20
	private bool syncWithFile;

	// Token: 0x04000015 RID: 21
	private DateTime timestamp;
}
