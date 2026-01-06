using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Pathfinding.Util;
using Pathfinding.WindowsStore;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x020000B6 RID: 182
	public class TinyJsonSerializer
	{
		// Token: 0x0600080A RID: 2058 RVA: 0x000356C8 File Offset: 0x000338C8
		public static void Serialize(object obj, StringBuilder output)
		{
			new TinyJsonSerializer
			{
				output = output
			}.Serialize(obj);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x000356DC File Offset: 0x000338DC
		private TinyJsonSerializer()
		{
			this.serializers[typeof(float)] = delegate(object v)
			{
				this.output.Append(((float)v).ToString("R", TinyJsonSerializer.invariantCulture));
			};
			this.serializers[typeof(bool)] = delegate(object v)
			{
				this.output.Append(((bool)v) ? "true" : "false");
			};
			this.serializers[typeof(Version)] = (this.serializers[typeof(uint)] = (this.serializers[typeof(int)] = delegate(object v)
			{
				this.output.Append(v.ToString());
			}));
			this.serializers[typeof(string)] = delegate(object v)
			{
				this.output.AppendFormat("\"{0}\"", v.ToString().Replace("\"", "\\\""));
			};
			this.serializers[typeof(Vector2)] = delegate(object v)
			{
				StringBuilder stringBuilder = this.output;
				string text = "{{ \"x\": {0}, \"y\": {1} }}";
				Vector2 vector = (Vector2)v;
				object obj = vector.x.ToString("R", TinyJsonSerializer.invariantCulture);
				vector = (Vector2)v;
				stringBuilder.AppendFormat(text, obj, vector.y.ToString("R", TinyJsonSerializer.invariantCulture));
			};
			this.serializers[typeof(Vector3)] = delegate(object v)
			{
				StringBuilder stringBuilder2 = this.output;
				string text2 = "{{ \"x\": {0}, \"y\": {1}, \"z\": {2} }}";
				Vector3 vector2 = (Vector3)v;
				object obj2 = vector2.x.ToString("R", TinyJsonSerializer.invariantCulture);
				vector2 = (Vector3)v;
				object obj3 = vector2.y.ToString("R", TinyJsonSerializer.invariantCulture);
				vector2 = (Vector3)v;
				stringBuilder2.AppendFormat(text2, obj2, obj3, vector2.z.ToString("R", TinyJsonSerializer.invariantCulture));
			};
			this.serializers[typeof(Guid)] = delegate(object v)
			{
				this.output.AppendFormat("{{ \"value\": \"{0}\" }}", v.ToString());
			};
			this.serializers[typeof(LayerMask)] = delegate(object v)
			{
				this.output.AppendFormat("{{ \"value\": {0} }}", ((LayerMask)v).ToString());
			};
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00035840 File Offset: 0x00033A40
		private void Serialize(object obj)
		{
			if (obj == null)
			{
				this.output.Append("null");
				return;
			}
			Type type = obj.GetType();
			Type typeInfo = WindowsStoreCompatibility.GetTypeInfo(type);
			if (this.serializers.ContainsKey(type))
			{
				this.serializers[type](obj);
				return;
			}
			if (typeInfo.IsEnum)
			{
				this.output.Append("\"" + obj.ToString() + "\"");
				return;
			}
			if (obj is IList)
			{
				this.output.Append("[");
				IList list = obj as IList;
				for (int i = 0; i < list.Count; i++)
				{
					if (i != 0)
					{
						this.output.Append(", ");
					}
					this.Serialize(list[i]);
				}
				this.output.Append("]");
				return;
			}
			if (obj is Object)
			{
				this.SerializeUnityObject(obj as Object);
				return;
			}
			bool flag = typeInfo.GetCustomAttributes(typeof(JsonOptInAttribute), true).Length != 0;
			this.output.Append("{");
			bool flag2 = false;
			do
			{
				foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (!(fieldInfo.DeclaringType != type) && ((!flag && fieldInfo.IsPublic) || fieldInfo.GetCustomAttributes(typeof(JsonMemberAttribute), true).Length != 0))
					{
						if (flag2)
						{
							this.output.Append(", ");
						}
						flag2 = true;
						this.output.AppendFormat("\"{0}\": ", fieldInfo.Name);
						this.Serialize(fieldInfo.GetValue(obj));
					}
				}
				type = type.BaseType;
			}
			while (!(type == null));
			this.output.Append("}");
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00035A19 File Offset: 0x00033C19
		private void QuotedField(string name, string contents)
		{
			this.output.AppendFormat("\"{0}\": \"{1}\"", name, contents);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00035A30 File Offset: 0x00033C30
		private void SerializeUnityObject(Object obj)
		{
			if (obj == null)
			{
				this.Serialize(null);
				return;
			}
			this.output.Append("{");
			string name = obj.name;
			this.QuotedField("Name", name);
			this.output.Append(", ");
			this.QuotedField("Type", obj.GetType().FullName);
			Component component = obj as Component;
			GameObject gameObject = obj as GameObject;
			if (component != null || gameObject != null)
			{
				if (component != null && gameObject == null)
				{
					gameObject = component.gameObject;
				}
				UnityReferenceHelper unityReferenceHelper = gameObject.GetComponent<UnityReferenceHelper>();
				if (unityReferenceHelper == null)
				{
					Debug.Log("Adding UnityReferenceHelper to Unity Reference '" + obj.name + "'");
					unityReferenceHelper = gameObject.AddComponent<UnityReferenceHelper>();
				}
				unityReferenceHelper.Reset();
				this.output.Append(", ");
				this.QuotedField("GUID", unityReferenceHelper.GetGUID().ToString());
			}
			this.output.Append("}");
		}

		// Token: 0x040004C1 RID: 1217
		private StringBuilder output = new StringBuilder();

		// Token: 0x040004C2 RID: 1218
		private Dictionary<Type, Action<object>> serializers = new Dictionary<Type, Action<object>>();

		// Token: 0x040004C3 RID: 1219
		private static readonly CultureInfo invariantCulture = CultureInfo.InvariantCulture;
	}
}
