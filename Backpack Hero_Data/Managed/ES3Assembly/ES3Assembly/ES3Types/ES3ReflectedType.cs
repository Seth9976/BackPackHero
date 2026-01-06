using System;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000051 RID: 81
	[Preserve]
	internal class ES3ReflectedType : ES3Type
	{
		// Token: 0x06000293 RID: 659 RVA: 0x0000969A File Offset: 0x0000789A
		public ES3ReflectedType(Type type)
			: base(type)
		{
			this.isReflectedType = true;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000096AA File Offset: 0x000078AA
		public ES3ReflectedType(Type type, string[] members)
			: this(type)
		{
			base.GetMembers(false, members);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000096BC File Offset: 0x000078BC
		public override void Write(object obj, ES3Writer writer)
		{
			if (obj == null)
			{
				writer.WriteNull();
				return;
			}
			Object @object = obj as Object;
			bool flag = @object != null;
			Type type = obj.GetType();
			if (type != this.type)
			{
				writer.WriteType(type);
				ES3TypeMgr.GetOrCreateES3Type(type, true).Write(obj, writer);
				return;
			}
			if (flag)
			{
				writer.WriteRef(@object);
			}
			if (this.members == null)
			{
				base.GetMembers(writer.settings.safeReflection);
			}
			for (int i = 0; i < this.members.Length; i++)
			{
				ES3Member es3Member = this.members[i];
				if (ES3Reflection.IsAssignableFrom(typeof(Object), es3Member.type))
				{
					object value = es3Member.reflectedMember.GetValue(obj);
					Object object2 = ((value == null) ? null : ((Object)value));
					writer.WritePropertyByRef(es3Member.name, object2);
				}
				else
				{
					writer.WriteProperty(es3Member.name, es3Member.reflectedMember.GetValue(obj), ES3TypeMgr.GetOrCreateES3Type(es3Member.type, true));
				}
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000097C0 File Offset: 0x000079C0
		public override object Read<T>(ES3Reader reader)
		{
			if (this.members == null)
			{
				base.GetMembers(reader.settings.safeReflection);
			}
			string text = reader.ReadPropertyName();
			if (text == "__type")
			{
				return ES3TypeMgr.GetOrCreateES3Type(reader.ReadType(), true).Read<T>(reader);
			}
			object obj;
			if (text == "_ES3Ref")
			{
				long num = reader.Read_ref();
				obj = ES3ReferenceMgrBase.Current.Get(num, this.type, false);
				if (obj == null)
				{
					obj = ES3Reflection.CreateInstance(this.type);
					ES3ReferenceMgrBase.Current.Add((Object)obj, num);
				}
			}
			else
			{
				reader.overridePropertiesName = text;
				obj = ES3Reflection.CreateInstance(this.type);
			}
			base.ReadProperties(reader, obj);
			return obj;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00009874 File Offset: 0x00007A74
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			if (this.members == null)
			{
				base.GetMembers(reader.settings.safeReflection);
			}
			string text = reader.ReadPropertyName();
			if (text == "__type")
			{
				ES3TypeMgr.GetOrCreateES3Type(reader.ReadType(), true).ReadInto<T>(reader, obj);
				return;
			}
			reader.overridePropertiesName = text;
			base.ReadProperties(reader, obj);
		}
	}
}
