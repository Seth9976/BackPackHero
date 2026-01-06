using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using ES3Internal;
using ES3Types;
using UnityEngine;

// Token: 0x02000013 RID: 19
public abstract class ES3Writer : IDisposable
{
	// Token: 0x06000181 RID: 385
	internal abstract void WriteNull();

	// Token: 0x06000182 RID: 386 RVA: 0x00005D6C File Offset: 0x00003F6C
	internal virtual void StartWriteFile()
	{
		this.serializationDepth++;
	}

	// Token: 0x06000183 RID: 387 RVA: 0x00005D7C File Offset: 0x00003F7C
	internal virtual void EndWriteFile()
	{
		this.serializationDepth--;
	}

	// Token: 0x06000184 RID: 388 RVA: 0x00005D8C File Offset: 0x00003F8C
	internal virtual void StartWriteObject(string name)
	{
		this.serializationDepth++;
	}

	// Token: 0x06000185 RID: 389 RVA: 0x00005D9C File Offset: 0x00003F9C
	internal virtual void EndWriteObject(string name)
	{
		this.serializationDepth--;
	}

	// Token: 0x06000186 RID: 390 RVA: 0x00005DAC File Offset: 0x00003FAC
	internal virtual void StartWriteProperty(string name)
	{
		if (name == null)
		{
			throw new ArgumentNullException("Key or field name cannot be NULL when saving data.");
		}
		ES3Debug.Log("<b>" + name + "</b> (writing property)", null, this.serializationDepth);
	}

	// Token: 0x06000187 RID: 391 RVA: 0x00005DD8 File Offset: 0x00003FD8
	internal virtual void EndWriteProperty(string name)
	{
	}

	// Token: 0x06000188 RID: 392 RVA: 0x00005DDA File Offset: 0x00003FDA
	internal virtual void StartWriteCollection()
	{
		this.serializationDepth++;
	}

	// Token: 0x06000189 RID: 393 RVA: 0x00005DEA File Offset: 0x00003FEA
	internal virtual void EndWriteCollection()
	{
		this.serializationDepth--;
	}

	// Token: 0x0600018A RID: 394
	internal abstract void StartWriteCollectionItem(int index);

	// Token: 0x0600018B RID: 395
	internal abstract void EndWriteCollectionItem(int index);

	// Token: 0x0600018C RID: 396
	internal abstract void StartWriteDictionary();

	// Token: 0x0600018D RID: 397
	internal abstract void EndWriteDictionary();

	// Token: 0x0600018E RID: 398
	internal abstract void StartWriteDictionaryKey(int index);

	// Token: 0x0600018F RID: 399
	internal abstract void EndWriteDictionaryKey(int index);

	// Token: 0x06000190 RID: 400
	internal abstract void StartWriteDictionaryValue(int index);

	// Token: 0x06000191 RID: 401
	internal abstract void EndWriteDictionaryValue(int index);

	// Token: 0x06000192 RID: 402
	public abstract void Dispose();

	// Token: 0x06000193 RID: 403
	internal abstract void WriteRawProperty(string name, byte[] bytes);

	// Token: 0x06000194 RID: 404
	internal abstract void WritePrimitive(int value);

	// Token: 0x06000195 RID: 405
	internal abstract void WritePrimitive(float value);

	// Token: 0x06000196 RID: 406
	internal abstract void WritePrimitive(bool value);

	// Token: 0x06000197 RID: 407
	internal abstract void WritePrimitive(decimal value);

	// Token: 0x06000198 RID: 408
	internal abstract void WritePrimitive(double value);

	// Token: 0x06000199 RID: 409
	internal abstract void WritePrimitive(long value);

	// Token: 0x0600019A RID: 410
	internal abstract void WritePrimitive(ulong value);

	// Token: 0x0600019B RID: 411
	internal abstract void WritePrimitive(uint value);

	// Token: 0x0600019C RID: 412
	internal abstract void WritePrimitive(byte value);

	// Token: 0x0600019D RID: 413
	internal abstract void WritePrimitive(sbyte value);

	// Token: 0x0600019E RID: 414
	internal abstract void WritePrimitive(short value);

	// Token: 0x0600019F RID: 415
	internal abstract void WritePrimitive(ushort value);

	// Token: 0x060001A0 RID: 416
	internal abstract void WritePrimitive(char value);

	// Token: 0x060001A1 RID: 417
	internal abstract void WritePrimitive(string value);

	// Token: 0x060001A2 RID: 418
	internal abstract void WritePrimitive(byte[] value);

	// Token: 0x060001A3 RID: 419 RVA: 0x00005DFA File Offset: 0x00003FFA
	protected ES3Writer(ES3Settings settings, bool writeHeaderAndFooter, bool overwriteKeys)
	{
		this.settings = settings;
		this.writeHeaderAndFooter = writeHeaderAndFooter;
		this.overwriteKeys = overwriteKeys;
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x00005E30 File Offset: 0x00004030
	internal virtual void Write(string key, Type type, byte[] value)
	{
		this.StartWriteProperty(key);
		this.StartWriteObject(key);
		this.WriteType(type);
		this.WriteRawProperty("value", value);
		this.EndWriteObject(key);
		this.EndWriteProperty(key);
		this.MarkKeyForDeletion(key);
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x00005E68 File Offset: 0x00004068
	public virtual void Write<T>(string key, object value)
	{
		if (typeof(T) == typeof(object))
		{
			this.Write(value.GetType(), key, value);
			return;
		}
		this.Write(typeof(T), key, value);
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x00005EA8 File Offset: 0x000040A8
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void Write(Type type, string key, object value)
	{
		this.StartWriteProperty(key);
		this.StartWriteObject(key);
		this.WriteType(type);
		this.WriteProperty("value", value, ES3TypeMgr.GetOrCreateES3Type(type, true), this.settings.referenceMode);
		this.EndWriteObject(key);
		this.EndWriteProperty(key);
		this.MarkKeyForDeletion(key);
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x00005F00 File Offset: 0x00004100
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void Write(object value, ES3.ReferenceMode memberReferenceMode = ES3.ReferenceMode.ByRef)
	{
		if (value == null)
		{
			this.WriteNull();
			return;
		}
		ES3Type orCreateES3Type = ES3TypeMgr.GetOrCreateES3Type(value.GetType(), true);
		this.Write(value, orCreateES3Type, memberReferenceMode);
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x00005F30 File Offset: 0x00004130
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void Write(object value, ES3Type type, ES3.ReferenceMode memberReferenceMode = ES3.ReferenceMode.ByRef)
	{
		if (value == null || (ES3Reflection.IsAssignableFrom(typeof(Object), value.GetType()) && value as Object == null))
		{
			this.WriteNull();
			return;
		}
		if (type == null || type.type == typeof(object))
		{
			Type type2 = value.GetType();
			type = ES3TypeMgr.GetOrCreateES3Type(type2, true);
			if (type == null)
			{
				string text = "Types of ";
				Type type3 = type2;
				throw new NotSupportedException(text + ((type3 != null) ? type3.ToString() : null) + " are not supported. Please see the Supported Types guide for more information: https://docs.moodkie.com/easy-save-3/es3-supported-types/");
			}
			if (!type.isCollection && !type.isDictionary)
			{
				this.StartWriteObject(null);
				this.WriteType(type2);
				type.Write(value, this);
				this.EndWriteObject(null);
				return;
			}
		}
		if (type == null)
		{
			throw new ArgumentNullException("ES3Type argument cannot be null.");
		}
		if (type.isUnsupported)
		{
			if (type.isCollection || type.isDictionary)
			{
				Type type4 = type.type;
				throw new NotSupportedException(((type4 != null) ? type4.ToString() : null) + " is not supported because it's element type is not supported. Please see the Supported Types guide for more information: https://docs.moodkie.com/easy-save-3/es3-supported-types/");
			}
			string text2 = "Types of ";
			Type type5 = type.type;
			throw new NotSupportedException(text2 + ((type5 != null) ? type5.ToString() : null) + " are not supported. Please see the Supported Types guide for more information: https://docs.moodkie.com/easy-save-3/es3-supported-types/");
		}
		else
		{
			if (type.isPrimitive || type.isEnum)
			{
				type.Write(value, this);
				return;
			}
			if (type.isCollection)
			{
				this.StartWriteCollection();
				((ES3CollectionType)type).Write(value, this, memberReferenceMode);
				this.EndWriteCollection();
				return;
			}
			if (type.isDictionary)
			{
				this.StartWriteDictionary();
				((ES3DictionaryType)type).Write(value, this, memberReferenceMode);
				this.EndWriteDictionary();
				return;
			}
			if (type.type == typeof(GameObject))
			{
				((ES3Type_GameObject)type).saveChildren = this.settings.saveChildren;
			}
			this.StartWriteObject(null);
			if (type.isES3TypeUnityObject)
			{
				((ES3UnityObjectType)type).WriteObject(value, this, memberReferenceMode);
			}
			else
			{
				type.Write(value, this);
			}
			this.EndWriteObject(null);
			return;
		}
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x00006114 File Offset: 0x00004314
	internal virtual void WriteRef(Object obj)
	{
		ES3ReferenceMgrBase es3ReferenceMgrBase = ES3ReferenceMgrBase.Current;
		if (es3ReferenceMgrBase == null)
		{
			throw new InvalidOperationException("An Easy Save 3 Manager is required to save references. To add one to your scene, exit playmode and go to Assets > Easy Save 3 > Add Manager to Scene");
		}
		long num = es3ReferenceMgrBase.Get(obj);
		if (num == -1L)
		{
			num = es3ReferenceMgrBase.Add(obj);
		}
		this.WriteProperty("_ES3Ref", num.ToString());
	}

	// Token: 0x060001AA RID: 426 RVA: 0x00006162 File Offset: 0x00004362
	public virtual void WriteProperty(string name, object value)
	{
		this.WriteProperty(name, value, this.settings.memberReferenceMode);
	}

	// Token: 0x060001AB RID: 427 RVA: 0x00006177 File Offset: 0x00004377
	public virtual void WriteProperty(string name, object value, ES3.ReferenceMode memberReferenceMode)
	{
		if (this.SerializationDepthLimitExceeded())
		{
			return;
		}
		this.StartWriteProperty(name);
		this.Write(value, memberReferenceMode);
		this.EndWriteProperty(name);
	}

	// Token: 0x060001AC RID: 428 RVA: 0x00006198 File Offset: 0x00004398
	public virtual void WriteProperty<T>(string name, object value)
	{
		this.WriteProperty(name, value, ES3TypeMgr.GetOrCreateES3Type(typeof(T), true), this.settings.memberReferenceMode);
	}

	// Token: 0x060001AD RID: 429 RVA: 0x000061BD File Offset: 0x000043BD
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void WriteProperty(string name, object value, ES3Type type)
	{
		this.WriteProperty(name, value, type, this.settings.memberReferenceMode);
	}

	// Token: 0x060001AE RID: 430 RVA: 0x000061D3 File Offset: 0x000043D3
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void WriteProperty(string name, object value, ES3Type type, ES3.ReferenceMode memberReferenceMode)
	{
		if (this.SerializationDepthLimitExceeded())
		{
			return;
		}
		this.StartWriteProperty(name);
		this.Write(value, type, memberReferenceMode);
		this.EndWriteProperty(name);
	}

	// Token: 0x060001AF RID: 431 RVA: 0x000061F6 File Offset: 0x000043F6
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void WritePropertyByRef(string name, Object value)
	{
		if (this.SerializationDepthLimitExceeded())
		{
			return;
		}
		this.StartWriteProperty(name);
		if (value == null)
		{
			this.WriteNull();
			return;
		}
		this.StartWriteObject(name);
		this.WriteRef(value);
		this.EndWriteObject(name);
		this.EndWriteProperty(name);
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00006234 File Offset: 0x00004434
	public void WritePrivateProperty(string name, object objectContainingProperty)
	{
		ES3Reflection.ES3ReflectedMember es3ReflectedProperty = ES3Reflection.GetES3ReflectedProperty(objectContainingProperty.GetType(), name);
		if (es3ReflectedProperty.IsNull)
		{
			string text = "A private property named ";
			string text2 = " does not exist in the type ";
			Type type = objectContainingProperty.GetType();
			throw new MissingMemberException(text + name + text2 + ((type != null) ? type.ToString() : null));
		}
		this.WriteProperty(name, es3ReflectedProperty.GetValue(objectContainingProperty), ES3TypeMgr.GetOrCreateES3Type(es3ReflectedProperty.MemberType, true));
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x0000629C File Offset: 0x0000449C
	public void WritePrivateField(string name, object objectContainingField)
	{
		ES3Reflection.ES3ReflectedMember es3ReflectedMember = ES3Reflection.GetES3ReflectedMember(objectContainingField.GetType(), name);
		if (es3ReflectedMember.IsNull)
		{
			string text = "A private field named ";
			string text2 = " does not exist in the type ";
			Type type = objectContainingField.GetType();
			throw new MissingMemberException(text + name + text2 + ((type != null) ? type.ToString() : null));
		}
		this.WriteProperty(name, es3ReflectedMember.GetValue(objectContainingField), ES3TypeMgr.GetOrCreateES3Type(es3ReflectedMember.MemberType, true));
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x00006304 File Offset: 0x00004504
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void WritePrivateProperty(string name, object objectContainingProperty, ES3Type type)
	{
		ES3Reflection.ES3ReflectedMember es3ReflectedProperty = ES3Reflection.GetES3ReflectedProperty(objectContainingProperty.GetType(), name);
		if (es3ReflectedProperty.IsNull)
		{
			string text = "A private property named ";
			string text2 = " does not exist in the type ";
			Type type2 = objectContainingProperty.GetType();
			throw new MissingMemberException(text + name + text2 + ((type2 != null) ? type2.ToString() : null));
		}
		this.WriteProperty(name, es3ReflectedProperty.GetValue(objectContainingProperty), type);
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x00006360 File Offset: 0x00004560
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void WritePrivateField(string name, object objectContainingField, ES3Type type)
	{
		ES3Reflection.ES3ReflectedMember es3ReflectedMember = ES3Reflection.GetES3ReflectedMember(objectContainingField.GetType(), name);
		if (es3ReflectedMember.IsNull)
		{
			string text = "A private field named ";
			string text2 = " does not exist in the type ";
			Type type2 = objectContainingField.GetType();
			throw new MissingMemberException(text + name + text2 + ((type2 != null) ? type2.ToString() : null));
		}
		this.WriteProperty(name, es3ReflectedMember.GetValue(objectContainingField), type);
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x000063BC File Offset: 0x000045BC
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void WritePrivatePropertyByRef(string name, object objectContainingProperty)
	{
		ES3Reflection.ES3ReflectedMember es3ReflectedProperty = ES3Reflection.GetES3ReflectedProperty(objectContainingProperty.GetType(), name);
		if (es3ReflectedProperty.IsNull)
		{
			string text = "A private property named ";
			string text2 = " does not exist in the type ";
			Type type = objectContainingProperty.GetType();
			throw new MissingMemberException(text + name + text2 + ((type != null) ? type.ToString() : null));
		}
		this.WritePropertyByRef(name, (Object)es3ReflectedProperty.GetValue(objectContainingProperty));
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x0000641C File Offset: 0x0000461C
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void WritePrivateFieldByRef(string name, object objectContainingField)
	{
		ES3Reflection.ES3ReflectedMember es3ReflectedMember = ES3Reflection.GetES3ReflectedMember(objectContainingField.GetType(), name);
		if (es3ReflectedMember.IsNull)
		{
			string text = "A private field named ";
			string text2 = " does not exist in the type ";
			Type type = objectContainingField.GetType();
			throw new MissingMemberException(text + name + text2 + ((type != null) ? type.ToString() : null));
		}
		this.WritePropertyByRef(name, (Object)es3ReflectedMember.GetValue(objectContainingField));
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x0000647B File Offset: 0x0000467B
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void WriteType(Type type)
	{
		this.WriteProperty("__type", ES3Reflection.GetTypeString(type));
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x0000648E File Offset: 0x0000468E
	public static ES3Writer Create(string filePath, ES3Settings settings)
	{
		return ES3Writer.Create(new ES3Settings(filePath, settings));
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x0000649C File Offset: 0x0000469C
	public static ES3Writer Create(ES3Settings settings)
	{
		return ES3Writer.Create(settings, true, true, false);
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x000064A8 File Offset: 0x000046A8
	internal static ES3Writer Create(ES3Settings settings, bool writeHeaderAndFooter, bool overwriteKeys, bool append)
	{
		Stream stream = ES3Stream.CreateStream(settings, append ? ES3FileMode.Append : ES3FileMode.Write);
		if (stream == null)
		{
			return null;
		}
		return ES3Writer.Create(stream, settings, writeHeaderAndFooter, overwriteKeys);
	}

	// Token: 0x060001BA RID: 442 RVA: 0x000064D1 File Offset: 0x000046D1
	internal static ES3Writer Create(Stream stream, ES3Settings settings, bool writeHeaderAndFooter, bool overwriteKeys)
	{
		if (stream.GetType() == typeof(MemoryStream))
		{
			settings = (ES3Settings)settings.Clone();
			settings.location = ES3.Location.InternalMS;
		}
		if (settings.format == ES3.Format.JSON)
		{
			return new ES3JSONWriter(stream, settings, writeHeaderAndFooter, overwriteKeys);
		}
		return null;
	}

	// Token: 0x060001BB RID: 443 RVA: 0x00006511 File Offset: 0x00004711
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected bool SerializationDepthLimitExceeded()
	{
		if (this.serializationDepth > this.settings.serializationDepthLimit)
		{
			ES3Debug.LogWarning("Serialization depth limit of " + this.settings.serializationDepthLimit.ToString() + " has been exceeded, indicating that there may be a circular reference.\nIf this is not a circular reference, you can increase the depth by going to Window > Easy Save 3 > Settings > Advanced Settings > Serialization Depth Limit", null, 0);
			return true;
		}
		return false;
	}

	// Token: 0x060001BC RID: 444 RVA: 0x0000654F File Offset: 0x0000474F
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void MarkKeyForDeletion(string key)
	{
		this.keysToDelete.Add(key);
	}

	// Token: 0x060001BD RID: 445 RVA: 0x00006560 File Offset: 0x00004760
	protected void Merge()
	{
		using (ES3Reader es3Reader = ES3Reader.Create(this.settings))
		{
			if (es3Reader != null)
			{
				this.Merge(es3Reader);
			}
		}
	}

	// Token: 0x060001BE RID: 446 RVA: 0x000065A4 File Offset: 0x000047A4
	protected void Merge(ES3Reader reader)
	{
		foreach (object obj in reader.RawEnumerator)
		{
			KeyValuePair<string, ES3Data> keyValuePair = (KeyValuePair<string, ES3Data>)obj;
			if (!this.keysToDelete.Contains(keyValuePair.Key) || keyValuePair.Value.type == null)
			{
				this.Write(keyValuePair.Key, keyValuePair.Value.type.type, keyValuePair.Value.bytes);
			}
		}
	}

	// Token: 0x060001BF RID: 447 RVA: 0x00006644 File Offset: 0x00004844
	public virtual void Save()
	{
		this.Save(this.overwriteKeys);
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x00006652 File Offset: 0x00004852
	public virtual void Save(bool overwriteKeys)
	{
		if (overwriteKeys)
		{
			this.Merge();
		}
		this.EndWriteFile();
		this.Dispose();
		if (this.settings.location == ES3.Location.File || this.settings.location == ES3.Location.PlayerPrefs)
		{
			ES3IO.CommitBackup(this.settings);
		}
	}

	// Token: 0x04000043 RID: 67
	public ES3Settings settings;

	// Token: 0x04000044 RID: 68
	protected HashSet<string> keysToDelete = new HashSet<string>();

	// Token: 0x04000045 RID: 69
	internal bool writeHeaderAndFooter = true;

	// Token: 0x04000046 RID: 70
	internal bool overwriteKeys = true;

	// Token: 0x04000047 RID: 71
	protected int serializationDepth;
}
