using System;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000025 RID: 37
	[Preserve]
	public abstract class ES3UnityObjectType : ES3ObjectType
	{
		// Token: 0x0600022A RID: 554 RVA: 0x000085D6 File Offset: 0x000067D6
		public ES3UnityObjectType(Type type)
			: base(type)
		{
			this.isValueType = false;
			this.isES3TypeUnityObject = true;
		}

		// Token: 0x0600022B RID: 555
		protected abstract void WriteUnityObject(object obj, ES3Writer writer);

		// Token: 0x0600022C RID: 556
		protected abstract void ReadUnityObject<T>(ES3Reader reader, object obj);

		// Token: 0x0600022D RID: 557
		protected abstract object ReadUnityObject<T>(ES3Reader reader);

		// Token: 0x0600022E RID: 558 RVA: 0x000085ED File Offset: 0x000067ED
		protected override void WriteObject(object obj, ES3Writer writer)
		{
			this.WriteObject(obj, writer, ES3.ReferenceMode.ByRefAndValue);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000085F8 File Offset: 0x000067F8
		public virtual void WriteObject(object obj, ES3Writer writer, ES3.ReferenceMode mode)
		{
			if (this.WriteUsingDerivedType(obj, writer, mode))
			{
				return;
			}
			Object @object = obj as Object;
			if (obj != null && @object == null)
			{
				string text = "Only types of UnityEngine.Object can be written with this method, but argument given is type of ";
				Type type = obj.GetType();
				throw new ArgumentException(text + ((type != null) ? type.ToString() : null));
			}
			ES3ReferenceMgrBase es3ReferenceMgrBase = ES3ReferenceMgrBase.Current;
			if (mode != ES3.ReferenceMode.ByValue)
			{
				if (es3ReferenceMgrBase == null)
				{
					throw new InvalidOperationException("An Easy Save 3 Manager is required to load references. To add one to your scene, exit playmode and go to Assets > Easy Save 3 > Add Manager to Scene");
				}
				writer.WriteRef(@object);
				if (mode == ES3.ReferenceMode.ByRef)
				{
					return;
				}
			}
			this.WriteUnityObject(@object, writer);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00008678 File Offset: 0x00006878
		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			ES3ReferenceMgrBase es3ReferenceMgrBase = ES3ReferenceMgrBase.Current;
			if (es3ReferenceMgrBase != null)
			{
				foreach (object obj2 in reader.Properties)
				{
					string text = (string)obj2;
					if (!(text == "_ES3Ref"))
					{
						reader.overridePropertiesName = text;
						break;
					}
					es3ReferenceMgrBase.Add((Object)obj, reader.Read_ref());
				}
			}
			this.ReadUnityObject<T>(reader, obj);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000870C File Offset: 0x0000690C
		protected override object ReadObject<T>(ES3Reader reader)
		{
			ES3ReferenceMgrBase es3ReferenceMgrBase = ES3ReferenceMgrBase.Current;
			if (es3ReferenceMgrBase == null)
			{
				return this.ReadUnityObject<T>(reader);
			}
			long num = -1L;
			Object @object = null;
			foreach (object obj in reader.Properties)
			{
				string text = (string)obj;
				if (text == "_ES3Ref")
				{
					if (es3ReferenceMgrBase == null)
					{
						throw new InvalidOperationException("An Easy Save 3 Manager is required to load references. To add one to your scene, exit playmode and go to Assets > Easy Save 3 > Add Manager to Scene");
					}
					num = reader.Read_ref();
					@object = es3ReferenceMgrBase.Get(num, this.type, false);
					if (@object != null)
					{
						break;
					}
				}
				else
				{
					reader.overridePropertiesName = text;
					if (@object == null)
					{
						@object = (Object)this.ReadUnityObject<T>(reader);
						es3ReferenceMgrBase.Add(@object, num);
						break;
					}
					break;
				}
			}
			this.ReadUnityObject<T>(reader, @object);
			return @object;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000087F8 File Offset: 0x000069F8
		protected bool WriteUsingDerivedType(object obj, ES3Writer writer, ES3.ReferenceMode mode)
		{
			Type type = obj.GetType();
			if (type != this.type)
			{
				writer.WriteType(type);
				ES3Type orCreateES3Type = ES3TypeMgr.GetOrCreateES3Type(type, true);
				if (orCreateES3Type is ES3UnityObjectType)
				{
					((ES3UnityObjectType)orCreateES3Type).WriteObject(obj, writer, mode);
				}
				else
				{
					orCreateES3Type.Write(obj, writer);
				}
				return true;
			}
			return false;
		}
	}
}
