using System;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000021 RID: 33
	[Preserve]
	public abstract class ES3ObjectType : ES3Type
	{
		// Token: 0x06000210 RID: 528 RVA: 0x00007DDD File Offset: 0x00005FDD
		public ES3ObjectType(Type type)
			: base(type)
		{
		}

		// Token: 0x06000211 RID: 529
		protected abstract void WriteObject(object obj, ES3Writer writer);

		// Token: 0x06000212 RID: 530
		protected abstract object ReadObject<T>(ES3Reader reader);

		// Token: 0x06000213 RID: 531 RVA: 0x00007DE6 File Offset: 0x00005FE6
		protected virtual void ReadObject<T>(ES3Reader reader, object obj)
		{
			string text = "ReadInto is not supported for type ";
			Type type = this.type;
			throw new NotSupportedException(text + ((type != null) ? type.ToString() : null));
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007E0C File Offset: 0x0000600C
		public override void Write(object obj, ES3Writer writer)
		{
			if (!base.WriteUsingDerivedType(obj, writer))
			{
				Type type = ES3Reflection.BaseType(obj.GetType());
				if (type != typeof(object))
				{
					ES3Type orCreateES3Type = ES3TypeMgr.GetOrCreateES3Type(type, true);
					if (orCreateES3Type.isDictionary || orCreateES3Type.isCollection)
					{
						writer.WriteProperty("_Values", obj, orCreateES3Type);
					}
				}
				this.WriteObject(obj, writer);
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00007E70 File Offset: 0x00006070
		public override object Read<T>(ES3Reader reader)
		{
			string text = base.ReadPropertyName(reader);
			if (text == "__type")
			{
				return ES3TypeMgr.GetOrCreateES3Type(reader.ReadType(), true).Read<T>(reader);
			}
			if (text == null)
			{
				return null;
			}
			reader.overridePropertiesName = text;
			return this.ReadObject<T>(reader);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007EB8 File Offset: 0x000060B8
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			for (;;)
			{
				string text = base.ReadPropertyName(reader);
				if (text == "__type")
				{
					break;
				}
				if (text == null)
				{
					return;
				}
				reader.overridePropertiesName = text;
				this.ReadObject<T>(reader, obj);
			}
			ES3TypeMgr.GetOrCreateES3Type(reader.ReadType(), true).ReadInto<T>(reader, obj);
		}
	}
}
