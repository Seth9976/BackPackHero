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
	// Token: 0x02000238 RID: 568
	public class TinyJsonSerializer
	{
		// Token: 0x06000D43 RID: 3395 RVA: 0x00053C0E File Offset: 0x00051E0E
		public static void Serialize(object obj, StringBuilder output)
		{
			new TinyJsonSerializer
			{
				output = output
			}.Serialize(obj, false);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00053C24 File Offset: 0x00051E24
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

		// Token: 0x06000D45 RID: 3397 RVA: 0x00053D88 File Offset: 0x00051F88
		private void Serialize(object obj, bool serializePrivateFieldsByDefault = false)
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
					this.Serialize(list[i], serializePrivateFieldsByDefault);
				}
				this.output.Append("]");
				return;
			}
			if (obj is AnimationCurve)
			{
				AnimationCurve animationCurve = obj as AnimationCurve;
				this.Serialize(new SerializableAnimationCurve
				{
					preWrapMode = animationCurve.preWrapMode,
					postWrapMode = animationCurve.postWrapMode,
					keys = animationCurve.keys
				}, true);
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
			if (typeInfo.GetCustomAttributes(typeof(JsonDynamicTypeAttribute), true).Length != 0)
			{
				this.output.AppendFormat("\"@type\": \"{0}\"", typeInfo.AssemblyQualifiedName);
				flag2 = true;
			}
			do
			{
				foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (!(fieldInfo.DeclaringType != type) && ((!flag && (fieldInfo.IsPublic || serializePrivateFieldsByDefault)) || fieldInfo.GetCustomAttributes(typeof(JsonMemberAttribute), true).Length != 0))
					{
						if (flag2)
						{
							this.output.Append(", ");
						}
						flag2 = true;
						this.output.AppendFormat("\"{0}\": ", fieldInfo.Name);
						this.Serialize(fieldInfo.GetValue(obj), serializePrivateFieldsByDefault);
					}
				}
				type = type.BaseType;
			}
			while (!(type == null));
			this.output.Append("}");
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00053FD7 File Offset: 0x000521D7
		private void QuotedField(string name, string contents)
		{
			this.output.AppendFormat("\"{0}\": \"{1}\"", name, contents);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00053FEC File Offset: 0x000521EC
		private void SerializeUnityObject(Object obj)
		{
			if (obj == null)
			{
				this.Serialize(null, false);
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
				if (component != null)
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

		// Token: 0x04000A6A RID: 2666
		private StringBuilder output = new StringBuilder();

		// Token: 0x04000A6B RID: 2667
		private Dictionary<Type, Action<object>> serializers = new Dictionary<Type, Action<object>>();

		// Token: 0x04000A6C RID: 2668
		private static readonly CultureInfo invariantCulture = CultureInfo.InvariantCulture;
	}
}
