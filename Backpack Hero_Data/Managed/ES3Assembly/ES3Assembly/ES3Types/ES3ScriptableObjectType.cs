using System;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000022 RID: 34
	[Preserve]
	public abstract class ES3ScriptableObjectType : ES3UnityObjectType
	{
		// Token: 0x06000217 RID: 535 RVA: 0x00007F02 File Offset: 0x00006102
		public ES3ScriptableObjectType(Type type)
			: base(type)
		{
		}

		// Token: 0x06000218 RID: 536
		protected abstract void WriteScriptableObject(object obj, ES3Writer writer);

		// Token: 0x06000219 RID: 537
		protected abstract void ReadScriptableObject<T>(ES3Reader reader, object obj);

		// Token: 0x0600021A RID: 538 RVA: 0x00007F0C File Offset: 0x0000610C
		protected override void WriteUnityObject(object obj, ES3Writer writer)
		{
			ScriptableObject scriptableObject = obj as ScriptableObject;
			if (obj != null && scriptableObject == null)
			{
				string text = "Only types of UnityEngine.ScriptableObject can be written with this method, but argument given is type of ";
				Type type = obj.GetType();
				throw new ArgumentException(text + ((type != null) ? type.ToString() : null));
			}
			if (ES3ReferenceMgrBase.Current != null)
			{
				writer.WriteRef(scriptableObject);
			}
			this.WriteScriptableObject(scriptableObject, writer);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007F6A File Offset: 0x0000616A
		protected override void ReadUnityObject<T>(ES3Reader reader, object obj)
		{
			this.ReadScriptableObject<T>(reader, obj);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007F74 File Offset: 0x00006174
		protected override object ReadUnityObject<T>(ES3Reader reader)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007F7C File Offset: 0x0000617C
		protected override object ReadObject<T>(ES3Reader reader)
		{
			ES3ReferenceMgrBase es3ReferenceMgrBase = ES3ReferenceMgrBase.Current;
			long num = -1L;
			Object @object = null;
			foreach (object obj in reader.Properties)
			{
				string text = (string)obj;
				if (text == "_ES3Ref" && es3ReferenceMgrBase != null)
				{
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
					if (!(@object == null))
					{
						break;
					}
					@object = ScriptableObject.CreateInstance(this.type);
					if (es3ReferenceMgrBase != null)
					{
						es3ReferenceMgrBase.Add(@object, num);
						break;
					}
					break;
				}
			}
			this.ReadScriptableObject<T>(reader, @object);
			return @object;
		}
	}
}
