using System;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000020 RID: 32
	[Preserve]
	public abstract class ES3ComponentType : ES3UnityObjectType
	{
		// Token: 0x06000207 RID: 519 RVA: 0x00007B9C File Offset: 0x00005D9C
		public ES3ComponentType(Type type)
			: base(type)
		{
		}

		// Token: 0x06000208 RID: 520
		protected abstract void WriteComponent(object obj, ES3Writer writer);

		// Token: 0x06000209 RID: 521
		protected abstract void ReadComponent<T>(ES3Reader reader, object obj);

		// Token: 0x0600020A RID: 522 RVA: 0x00007BA8 File Offset: 0x00005DA8
		protected override void WriteUnityObject(object obj, ES3Writer writer)
		{
			Component component = obj as Component;
			if (obj != null && component == null)
			{
				string text = "Only types of UnityEngine.Component can be written with this method, but argument given is type of ";
				Type type = obj.GetType();
				throw new ArgumentException(text + ((type != null) ? type.ToString() : null));
			}
			ES3ReferenceMgrBase es3ReferenceMgrBase = ES3ReferenceMgrBase.Current;
			if (es3ReferenceMgrBase != null)
			{
				writer.WriteProperty("goID", es3ReferenceMgrBase.Add(component.gameObject).ToString(), ES3Type_string.Instance);
			}
			this.WriteComponent(component, writer);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007C25 File Offset: 0x00005E25
		protected override void ReadUnityObject<T>(ES3Reader reader, object obj)
		{
			this.ReadComponent<T>(reader, obj);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007C2F File Offset: 0x00005E2F
		protected override object ReadUnityObject<T>(ES3Reader reader)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007C38 File Offset: 0x00005E38
		protected override object ReadObject<T>(ES3Reader reader)
		{
			ES3ReferenceMgrBase es3ReferenceMgrBase = ES3ReferenceMgrBase.Current;
			long num = -1L;
			Object @object = null;
			foreach (object obj in reader.Properties)
			{
				string text = (string)obj;
				if (text == "_ES3Ref")
				{
					num = reader.Read_ref();
					@object = es3ReferenceMgrBase.Get(num, true);
				}
				else
				{
					if (text == "goID")
					{
						long num2 = reader.Read_ref();
						GameObject gameObject = (GameObject)es3ReferenceMgrBase.Get(num2, this.type, false);
						if (gameObject == null)
						{
							gameObject = new GameObject("Easy Save 3 Loaded GameObject");
							es3ReferenceMgrBase.Add(gameObject, num2);
						}
						@object = ES3ComponentType.GetOrAddComponent(gameObject, this.type);
						es3ReferenceMgrBase.Add(@object, num);
						break;
					}
					reader.overridePropertiesName = text;
					if (@object == null)
					{
						GameObject gameObject2 = new GameObject("Easy Save 3 Loaded GameObject");
						@object = ES3ComponentType.GetOrAddComponent(gameObject2, this.type);
						es3ReferenceMgrBase.Add(@object, num);
						es3ReferenceMgrBase.Add(gameObject2);
						break;
					}
					break;
				}
			}
			if (@object != null)
			{
				this.ReadComponent<T>(reader, @object);
			}
			return @object;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00007D7C File Offset: 0x00005F7C
		private static Component GetOrAddComponent(GameObject go, Type type)
		{
			Component component = go.GetComponent(type);
			if (component != null)
			{
				return component;
			}
			return go.AddComponent(type);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007DA4 File Offset: 0x00005FA4
		public static Component CreateComponent(Type type)
		{
			GameObject gameObject = new GameObject("Easy Save 3 Loaded Component");
			if (type == typeof(Transform))
			{
				return gameObject.GetComponent(type);
			}
			return ES3ComponentType.GetOrAddComponent(gameObject, type);
		}

		// Token: 0x0400004F RID: 79
		protected const string gameObjectPropertyName = "goID";
	}
}
