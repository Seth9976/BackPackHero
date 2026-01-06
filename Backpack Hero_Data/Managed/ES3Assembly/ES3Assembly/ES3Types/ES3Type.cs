using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000023 RID: 35
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Preserve]
	public abstract class ES3Type
	{
		// Token: 0x0600021E RID: 542 RVA: 0x00008050 File Offset: 0x00006250
		protected ES3Type(Type type)
		{
			ES3TypeMgr.Add(type, this);
			this.type = type;
			this.isValueType = ES3Reflection.IsValueType(type);
		}

		// Token: 0x0600021F RID: 543
		public abstract void Write(object obj, ES3Writer writer);

		// Token: 0x06000220 RID: 544
		public abstract object Read<T>(ES3Reader reader);

		// Token: 0x06000221 RID: 545 RVA: 0x00008072 File Offset: 0x00006272
		public virtual void ReadInto<T>(ES3Reader reader, object obj)
		{
			throw new NotImplementedException("Self-assigning Read is not implemented or supported on this type.");
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008080 File Offset: 0x00006280
		protected bool WriteUsingDerivedType(object obj, ES3Writer writer)
		{
			Type type = obj.GetType();
			if (type != this.type)
			{
				writer.WriteType(type);
				ES3TypeMgr.GetOrCreateES3Type(type, true).Write(obj, writer);
				return true;
			}
			return false;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000080BA File Offset: 0x000062BA
		protected void ReadUsingDerivedType<T>(ES3Reader reader, object obj)
		{
			ES3TypeMgr.GetOrCreateES3Type(reader.ReadType(), true).ReadInto<T>(reader, obj);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000080CF File Offset: 0x000062CF
		internal string ReadPropertyName(ES3Reader reader)
		{
			if (reader.overridePropertiesName != null)
			{
				string overridePropertiesName = reader.overridePropertiesName;
				reader.overridePropertiesName = null;
				return overridePropertiesName;
			}
			return reader.ReadPropertyName();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000080F0 File Offset: 0x000062F0
		protected void WriteProperties(object obj, ES3Writer writer)
		{
			if (this.members == null)
			{
				this.GetMembers(writer.settings.safeReflection);
			}
			for (int i = 0; i < this.members.Length; i++)
			{
				ES3Member es3Member = this.members[i];
				writer.WriteProperty(es3Member.name, es3Member.reflectedMember.GetValue(obj), ES3TypeMgr.GetOrCreateES3Type(es3Member.type, true), writer.settings.memberReferenceMode);
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00008164 File Offset: 0x00006364
		protected object ReadProperties(ES3Reader reader, object obj)
		{
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				ES3Member es3Member = null;
				for (int i = 0; i < this.members.Length; i++)
				{
					if (this.members[i].name == text)
					{
						es3Member = this.members[i];
						break;
					}
				}
				if (text == "_Values")
				{
					ES3Type orCreateES3Type = ES3TypeMgr.GetOrCreateES3Type(ES3Reflection.BaseType(obj.GetType()), true);
					if (orCreateES3Type.isDictionary)
					{
						IDictionary dictionary = (IDictionary)obj;
						using (IDictionaryEnumerator enumerator2 = ((IDictionary)orCreateES3Type.Read<IDictionary>(reader)).GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object obj3 = enumerator2.Current;
								DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
								dictionary[dictionaryEntry.Key] = dictionaryEntry.Value;
							}
							goto IL_02AE;
						}
					}
					if (orCreateES3Type.isCollection)
					{
						IEnumerable enumerable = (IEnumerable)orCreateES3Type.Read<IEnumerable>(reader);
						Type type = orCreateES3Type.GetType();
						if (type == typeof(ES3ListType))
						{
							using (IEnumerator enumerator3 = enumerable.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									object obj4 = enumerator3.Current;
									((IList)obj).Add(obj4);
								}
								goto IL_02AE;
							}
						}
						if (type == typeof(ES3QueueType))
						{
							MethodInfo method = orCreateES3Type.type.GetMethod("Enqueue");
							using (IEnumerator enumerator3 = enumerable.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									object obj5 = enumerator3.Current;
									method.Invoke(obj, new object[] { obj5 });
								}
								goto IL_02AE;
							}
						}
						if (type == typeof(ES3StackType))
						{
							MethodInfo method2 = orCreateES3Type.type.GetMethod("Push");
							using (IEnumerator enumerator3 = enumerable.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									object obj6 = enumerator3.Current;
									method2.Invoke(obj, new object[] { obj6 });
								}
								goto IL_02AE;
							}
						}
						if (type == typeof(ES3HashSetType))
						{
							MethodInfo method3 = orCreateES3Type.type.GetMethod("Add");
							foreach (object obj7 in enumerable)
							{
								method3.Invoke(obj, new object[] { obj7 });
							}
						}
					}
				}
				IL_02AE:
				if (es3Member == null)
				{
					reader.Skip();
				}
				else
				{
					ES3Type orCreateES3Type2 = ES3TypeMgr.GetOrCreateES3Type(es3Member.type, true);
					if (ES3Reflection.IsAssignableFrom(typeof(ES3DictionaryType), orCreateES3Type2.GetType()))
					{
						es3Member.reflectedMember.SetValue(obj, ((ES3DictionaryType)orCreateES3Type2).Read(reader));
					}
					else if (ES3Reflection.IsAssignableFrom(typeof(ES3CollectionType), orCreateES3Type2.GetType()))
					{
						es3Member.reflectedMember.SetValue(obj, ((ES3CollectionType)orCreateES3Type2).Read(reader));
					}
					else
					{
						object obj8 = reader.Read<object>(orCreateES3Type2);
						es3Member.reflectedMember.SetValue(obj, obj8);
					}
				}
			}
			return obj;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00008570 File Offset: 0x00006770
		protected void GetMembers(bool safe)
		{
			this.GetMembers(safe, null);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000857C File Offset: 0x0000677C
		protected void GetMembers(bool safe, string[] memberNames)
		{
			ES3Reflection.ES3ReflectedMember[] serializableMembers = ES3Reflection.GetSerializableMembers(this.type, safe, memberNames);
			this.members = new ES3Member[serializableMembers.Length];
			for (int i = 0; i < serializableMembers.Length; i++)
			{
				this.members[i] = new ES3Member(serializableMembers[i]);
			}
		}

		// Token: 0x04000050 RID: 80
		public const string typeFieldName = "__type";

		// Token: 0x04000051 RID: 81
		public ES3Member[] members;

		// Token: 0x04000052 RID: 82
		public Type type;

		// Token: 0x04000053 RID: 83
		public bool isPrimitive;

		// Token: 0x04000054 RID: 84
		public bool isValueType;

		// Token: 0x04000055 RID: 85
		public bool isCollection;

		// Token: 0x04000056 RID: 86
		public bool isDictionary;

		// Token: 0x04000057 RID: 87
		public bool isEnum;

		// Token: 0x04000058 RID: 88
		public bool isES3TypeUnityObject;

		// Token: 0x04000059 RID: 89
		public bool isReflectedType;

		// Token: 0x0400005A RID: 90
		public bool isUnsupported;

		// Token: 0x0400005B RID: 91
		public int priority;
	}
}
