using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using ES3Internal;
using ES3Types;

// Token: 0x0200000C RID: 12
public abstract class ES3Reader : IDisposable
{
	// Token: 0x060000E9 RID: 233
	internal abstract int Read_int();

	// Token: 0x060000EA RID: 234
	internal abstract float Read_float();

	// Token: 0x060000EB RID: 235
	internal abstract bool Read_bool();

	// Token: 0x060000EC RID: 236
	internal abstract char Read_char();

	// Token: 0x060000ED RID: 237
	internal abstract decimal Read_decimal();

	// Token: 0x060000EE RID: 238
	internal abstract double Read_double();

	// Token: 0x060000EF RID: 239
	internal abstract long Read_long();

	// Token: 0x060000F0 RID: 240
	internal abstract ulong Read_ulong();

	// Token: 0x060000F1 RID: 241
	internal abstract byte Read_byte();

	// Token: 0x060000F2 RID: 242
	internal abstract sbyte Read_sbyte();

	// Token: 0x060000F3 RID: 243
	internal abstract short Read_short();

	// Token: 0x060000F4 RID: 244
	internal abstract ushort Read_ushort();

	// Token: 0x060000F5 RID: 245
	internal abstract uint Read_uint();

	// Token: 0x060000F6 RID: 246
	internal abstract string Read_string();

	// Token: 0x060000F7 RID: 247
	internal abstract byte[] Read_byteArray();

	// Token: 0x060000F8 RID: 248
	internal abstract long Read_ref();

	// Token: 0x060000F9 RID: 249
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract string ReadPropertyName();

	// Token: 0x060000FA RID: 250
	protected abstract Type ReadKeyPrefix(bool ignore = false);

	// Token: 0x060000FB RID: 251
	protected abstract void ReadKeySuffix();

	// Token: 0x060000FC RID: 252
	internal abstract byte[] ReadElement(bool skip = false);

	// Token: 0x060000FD RID: 253
	public abstract void Dispose();

	// Token: 0x060000FE RID: 254 RVA: 0x00004B30 File Offset: 0x00002D30
	internal virtual bool Goto(string key)
	{
		if (key == null)
		{
			throw new ArgumentNullException("Key cannot be NULL when loading data.");
		}
		string text;
		while ((text = this.ReadPropertyName()) != key)
		{
			if (text == null)
			{
				return false;
			}
			this.Skip();
		}
		return true;
	}

	// Token: 0x060000FF RID: 255 RVA: 0x00004B67 File Offset: 0x00002D67
	internal virtual bool StartReadObject()
	{
		this.serializationDepth++;
		return false;
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00004B78 File Offset: 0x00002D78
	internal virtual void EndReadObject()
	{
		this.serializationDepth--;
	}

	// Token: 0x06000101 RID: 257
	internal abstract bool StartReadDictionary();

	// Token: 0x06000102 RID: 258
	internal abstract void EndReadDictionary();

	// Token: 0x06000103 RID: 259
	internal abstract bool StartReadDictionaryKey();

	// Token: 0x06000104 RID: 260
	internal abstract void EndReadDictionaryKey();

	// Token: 0x06000105 RID: 261
	internal abstract void StartReadDictionaryValue();

	// Token: 0x06000106 RID: 262
	internal abstract bool EndReadDictionaryValue();

	// Token: 0x06000107 RID: 263
	internal abstract bool StartReadCollection();

	// Token: 0x06000108 RID: 264
	internal abstract void EndReadCollection();

	// Token: 0x06000109 RID: 265
	internal abstract bool StartReadCollectionItem();

	// Token: 0x0600010A RID: 266
	internal abstract bool EndReadCollectionItem();

	// Token: 0x0600010B RID: 267 RVA: 0x00004B88 File Offset: 0x00002D88
	internal ES3Reader(ES3Settings settings, bool readHeaderAndFooter = true)
	{
		this.settings = settings;
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600010C RID: 268 RVA: 0x00004B97 File Offset: 0x00002D97
	public virtual ES3Reader.ES3ReaderPropertyEnumerator Properties
	{
		get
		{
			return new ES3Reader.ES3ReaderPropertyEnumerator(this);
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x0600010D RID: 269 RVA: 0x00004B9F File Offset: 0x00002D9F
	internal virtual ES3Reader.ES3ReaderRawEnumerator RawEnumerator
	{
		get
		{
			return new ES3Reader.ES3ReaderRawEnumerator(this);
		}
	}

	// Token: 0x0600010E RID: 270 RVA: 0x00004BA7 File Offset: 0x00002DA7
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void Skip()
	{
		this.ReadElement(true);
	}

	// Token: 0x0600010F RID: 271 RVA: 0x00004BB1 File Offset: 0x00002DB1
	public virtual T Read<T>()
	{
		return this.Read<T>(ES3TypeMgr.GetOrCreateES3Type(typeof(T), true));
	}

	// Token: 0x06000110 RID: 272 RVA: 0x00004BC9 File Offset: 0x00002DC9
	public virtual void ReadInto<T>(object obj)
	{
		this.ReadInto<T>(obj, ES3TypeMgr.GetOrCreateES3Type(typeof(T), true));
	}

	// Token: 0x06000111 RID: 273 RVA: 0x00004BE2 File Offset: 0x00002DE2
	[EditorBrowsable(EditorBrowsableState.Never)]
	public T ReadProperty<T>()
	{
		return this.ReadProperty<T>(ES3TypeMgr.GetOrCreateES3Type(typeof(T), true));
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00004BFA File Offset: 0x00002DFA
	[EditorBrowsable(EditorBrowsableState.Never)]
	public T ReadProperty<T>(ES3Type type)
	{
		this.ReadPropertyName();
		return this.Read<T>(type);
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00004C0A File Offset: 0x00002E0A
	[EditorBrowsable(EditorBrowsableState.Never)]
	public long ReadRefProperty()
	{
		this.ReadPropertyName();
		return this.Read_ref();
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00004C19 File Offset: 0x00002E19
	internal Type ReadType()
	{
		return ES3Reflection.GetType(this.Read<string>(ES3Type_string.Instance));
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00004C2C File Offset: 0x00002E2C
	public void SetPrivateProperty(string name, object value, object objectContainingProperty)
	{
		ES3Reflection.ES3ReflectedMember es3ReflectedProperty = ES3Reflection.GetES3ReflectedProperty(objectContainingProperty.GetType(), name);
		if (es3ReflectedProperty.IsNull)
		{
			string text = "A private property named ";
			string text2 = " does not exist in the type ";
			Type type = objectContainingProperty.GetType();
			throw new MissingMemberException(text + name + text2 + ((type != null) ? type.ToString() : null));
		}
		es3ReflectedProperty.SetValue(objectContainingProperty, value);
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00004C80 File Offset: 0x00002E80
	public void SetPrivateField(string name, object value, object objectContainingField)
	{
		ES3Reflection.ES3ReflectedMember es3ReflectedMember = ES3Reflection.GetES3ReflectedMember(objectContainingField.GetType(), name);
		if (es3ReflectedMember.IsNull)
		{
			string text = "A private field named ";
			string text2 = " does not exist in the type ";
			Type type = objectContainingField.GetType();
			throw new MissingMemberException(text + name + text2 + ((type != null) ? type.ToString() : null));
		}
		es3ReflectedMember.SetValue(objectContainingField, value);
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00004CD4 File Offset: 0x00002ED4
	public virtual T Read<T>(string key)
	{
		if (!this.Goto(key))
		{
			throw new KeyNotFoundException(string.Concat(new string[]
			{
				"Key \"",
				key,
				"\" was not found in file \"",
				this.settings.FullPath,
				"\". Use Load<T>(key, defaultValue) if you want to return a default value if the key does not exist."
			}));
		}
		Type type = this.ReadTypeFromHeader<T>();
		return this.Read<T>(ES3TypeMgr.GetOrCreateES3Type(type, true));
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00004D3C File Offset: 0x00002F3C
	public virtual T Read<T>(string key, T defaultValue)
	{
		if (!this.Goto(key))
		{
			return defaultValue;
		}
		Type type = this.ReadTypeFromHeader<T>();
		return this.Read<T>(ES3TypeMgr.GetOrCreateES3Type(type, true));
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00004D68 File Offset: 0x00002F68
	public virtual void ReadInto<T>(string key, T obj) where T : class
	{
		if (!this.Goto(key))
		{
			throw new KeyNotFoundException(string.Concat(new string[]
			{
				"Key \"",
				key,
				"\" was not found in file \"",
				this.settings.FullPath,
				"\""
			}));
		}
		Type type = this.ReadTypeFromHeader<T>();
		this.ReadInto<T>(obj, ES3TypeMgr.GetOrCreateES3Type(type, true));
	}

	// Token: 0x0600011A RID: 282 RVA: 0x00004DD3 File Offset: 0x00002FD3
	protected virtual void ReadObject<T>(object obj, ES3Type type)
	{
		if (this.StartReadObject())
		{
			return;
		}
		type.ReadInto<T>(this, obj);
		this.EndReadObject();
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00004DEC File Offset: 0x00002FEC
	protected virtual T ReadObject<T>(ES3Type type)
	{
		if (this.StartReadObject())
		{
			return default(T);
		}
		object obj = type.Read<T>(this);
		this.EndReadObject();
		return (T)((object)obj);
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00004E20 File Offset: 0x00003020
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual T Read<T>(ES3Type type)
	{
		if (type == null || type.isUnsupported)
		{
			throw new NotSupportedException("Type of " + ((type != null) ? type.ToString() : null) + " is not currently supported, and could not be loaded using reflection.");
		}
		if (type.isPrimitive)
		{
			return (T)((object)type.Read<T>(this));
		}
		if (type.isCollection)
		{
			return (T)((object)((ES3CollectionType)type).Read(this));
		}
		if (type.isDictionary)
		{
			return (T)((object)((ES3DictionaryType)type).Read(this));
		}
		return this.ReadObject<T>(type);
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00004EAC File Offset: 0x000030AC
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void ReadInto<T>(object obj, ES3Type type)
	{
		if (type == null || type.isUnsupported)
		{
			string text = "Type of ";
			Type type2 = obj.GetType();
			throw new NotSupportedException(text + ((type2 != null) ? type2.ToString() : null) + " is not currently supported, and could not be loaded using reflection.");
		}
		if (type.isCollection)
		{
			((ES3CollectionType)type).ReadInto(this, obj);
			return;
		}
		if (type.isDictionary)
		{
			((ES3DictionaryType)type).ReadInto(this, obj);
			return;
		}
		this.ReadObject<T>(obj, type);
	}

	// Token: 0x0600011E RID: 286 RVA: 0x00004F20 File Offset: 0x00003120
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal Type ReadTypeFromHeader<T>()
	{
		if (typeof(T) == typeof(object))
		{
			return this.ReadKeyPrefix(false);
		}
		if (!this.settings.typeChecking)
		{
			this.ReadKeyPrefix(true);
			return typeof(T);
		}
		Type type = this.ReadKeyPrefix(false);
		if (type != typeof(T))
		{
			string[] array = new string[5];
			array[0] = "Trying to load data of type ";
			int num = 1;
			Type typeFromHandle = typeof(T);
			array[num] = ((typeFromHandle != null) ? typeFromHandle.ToString() : null);
			array[2] = ", but data contained in file is type of ";
			int num2 = 3;
			Type type2 = type;
			array[num2] = ((type2 != null) ? type2.ToString() : null);
			array[4] = ".";
			throw new InvalidOperationException(string.Concat(array));
		}
		return type;
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00004FDD File Offset: 0x000031DD
	public static ES3Reader Create()
	{
		return ES3Reader.Create(new ES3Settings(null, null));
	}

	// Token: 0x06000120 RID: 288 RVA: 0x00004FEB File Offset: 0x000031EB
	public static ES3Reader Create(string filePath)
	{
		return ES3Reader.Create(new ES3Settings(filePath, null));
	}

	// Token: 0x06000121 RID: 289 RVA: 0x00004FF9 File Offset: 0x000031F9
	public static ES3Reader Create(string filePath, ES3Settings settings)
	{
		return ES3Reader.Create(new ES3Settings(filePath, settings));
	}

	// Token: 0x06000122 RID: 290 RVA: 0x00005008 File Offset: 0x00003208
	public static ES3Reader Create(ES3Settings settings)
	{
		Stream stream = ES3Stream.CreateStream(settings, ES3FileMode.Read);
		if (stream == null)
		{
			return null;
		}
		if (settings.format == ES3.Format.JSON)
		{
			return new ES3JSONReader(stream, settings, true);
		}
		return null;
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00005034 File Offset: 0x00003234
	public static ES3Reader Create(byte[] bytes)
	{
		return ES3Reader.Create(bytes, new ES3Settings(null, null));
	}

	// Token: 0x06000124 RID: 292 RVA: 0x00005044 File Offset: 0x00003244
	public static ES3Reader Create(byte[] bytes, ES3Settings settings)
	{
		Stream stream = ES3Stream.CreateStream(new MemoryStream(bytes), settings, ES3FileMode.Read);
		if (stream == null)
		{
			return null;
		}
		if (settings.format == ES3.Format.JSON)
		{
			return new ES3JSONReader(stream, settings, true);
		}
		return null;
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00005076 File Offset: 0x00003276
	internal static ES3Reader Create(Stream stream, ES3Settings settings)
	{
		stream = ES3Stream.CreateStream(stream, settings, ES3FileMode.Read);
		if (settings.format == ES3.Format.JSON)
		{
			return new ES3JSONReader(stream, settings, true);
		}
		return null;
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00005094 File Offset: 0x00003294
	internal static ES3Reader Create(Stream stream, ES3Settings settings, bool readHeaderAndFooter)
	{
		if (settings.format == ES3.Format.JSON)
		{
			return new ES3JSONReader(stream, settings, readHeaderAndFooter);
		}
		return null;
	}

	// Token: 0x0400001F RID: 31
	public ES3Settings settings;

	// Token: 0x04000020 RID: 32
	protected int serializationDepth;

	// Token: 0x04000021 RID: 33
	internal string overridePropertiesName;

	// Token: 0x020000E9 RID: 233
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ES3ReaderPropertyEnumerator
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x00022C7A File Offset: 0x00020E7A
		public ES3ReaderPropertyEnumerator(ES3Reader reader)
		{
			this.reader = reader;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00022C89 File Offset: 0x00020E89
		public IEnumerator GetEnumerator()
		{
			for (;;)
			{
				if (this.reader.overridePropertiesName != null)
				{
					string overridePropertiesName = this.reader.overridePropertiesName;
					this.reader.overridePropertiesName = null;
					yield return overridePropertiesName;
				}
				else
				{
					string text;
					if ((text = this.reader.ReadPropertyName()) == null)
					{
						break;
					}
					yield return text;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x0400017D RID: 381
		public ES3Reader reader;
	}

	// Token: 0x020000EA RID: 234
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ES3ReaderRawEnumerator
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x00022C98 File Offset: 0x00020E98
		public ES3ReaderRawEnumerator(ES3Reader reader)
		{
			this.reader = reader;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00022CA7 File Offset: 0x00020EA7
		public IEnumerator GetEnumerator()
		{
			for (;;)
			{
				string text = this.reader.ReadPropertyName();
				if (text == null)
				{
					break;
				}
				Type type = this.reader.ReadTypeFromHeader<object>();
				byte[] array = this.reader.ReadElement(false);
				this.reader.ReadKeySuffix();
				if (type != null)
				{
					yield return new KeyValuePair<string, ES3Data>(text, new ES3Data(type, array));
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x0400017E RID: 382
		public ES3Reader reader;
	}
}
