using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using ES3Types;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Internal
{
	// Token: 0x020000D8 RID: 216
	[Preserve]
	public static class ES3TypeMgr
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x00021E50 File Offset: 0x00020050
		public static ES3Type GetOrCreateES3Type(Type type, bool throwException = true)
		{
			if (ES3TypeMgr.types == null)
			{
				ES3TypeMgr.Init();
			}
			if (type != typeof(object) && ES3TypeMgr.lastAccessedType != null && ES3TypeMgr.lastAccessedType.type == type)
			{
				return ES3TypeMgr.lastAccessedType;
			}
			if (ES3TypeMgr.types.TryGetValue(type, out ES3TypeMgr.lastAccessedType))
			{
				return ES3TypeMgr.lastAccessedType;
			}
			return ES3TypeMgr.lastAccessedType = ES3TypeMgr.CreateES3Type(type, throwException);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00021EBF File Offset: 0x000200BF
		public static ES3Type GetES3Type(Type type)
		{
			if (ES3TypeMgr.types == null)
			{
				ES3TypeMgr.Init();
			}
			if (ES3TypeMgr.types.TryGetValue(type, out ES3TypeMgr.lastAccessedType))
			{
				return ES3TypeMgr.lastAccessedType;
			}
			return null;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00021EE8 File Offset: 0x000200E8
		internal static void Add(Type type, ES3Type es3Type)
		{
			if (ES3TypeMgr.types == null)
			{
				ES3TypeMgr.Init();
			}
			ES3Type es3Type2 = ES3TypeMgr.GetES3Type(type);
			if (es3Type2 != null && es3Type2.priority > es3Type.priority)
			{
				return;
			}
			object @lock = ES3TypeMgr._lock;
			lock (@lock)
			{
				ES3TypeMgr.types[type] = es3Type;
			}
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00021F54 File Offset: 0x00020154
		internal static ES3Type CreateES3Type(Type type, bool throwException = true)
		{
			if (ES3Reflection.IsEnum(type))
			{
				return new ES3Type_enum(type);
			}
			ES3Type es3Type;
			if (ES3Reflection.TypeIsArray(type))
			{
				int arrayRank = ES3Reflection.GetArrayRank(type);
				if (arrayRank == 1)
				{
					es3Type = new ES3ArrayType(type);
				}
				else if (arrayRank == 2)
				{
					es3Type = new ES32DArrayType(type);
				}
				else if (arrayRank == 3)
				{
					es3Type = new ES33DArrayType(type);
				}
				else
				{
					if (throwException)
					{
						throw new NotSupportedException("Only arrays with up to three dimensions are supported by Easy Save.");
					}
					return null;
				}
			}
			else if (ES3Reflection.IsGenericType(type) && ES3Reflection.ImplementsInterface(type, typeof(IEnumerable)))
			{
				Type genericTypeDefinition = ES3Reflection.GetGenericTypeDefinition(type);
				if (genericTypeDefinition == typeof(List<>))
				{
					es3Type = new ES3ListType(type);
				}
				else if (genericTypeDefinition == typeof(Dictionary<, >))
				{
					es3Type = new ES3DictionaryType(type);
				}
				else if (genericTypeDefinition == typeof(Queue<>))
				{
					es3Type = new ES3QueueType(type);
				}
				else if (genericTypeDefinition == typeof(Stack<>))
				{
					es3Type = new ES3StackType(type);
				}
				else if (genericTypeDefinition == typeof(HashSet<>))
				{
					es3Type = new ES3HashSetType(type);
				}
				else
				{
					if (throwException)
					{
						throw new NotSupportedException("Generic type \"" + type.ToString() + "\" is not supported by Easy Save.");
					}
					return null;
				}
			}
			else if (ES3Reflection.IsPrimitive(type))
			{
				if (ES3TypeMgr.types == null || ES3TypeMgr.types.Count == 0)
				{
					throw new TypeLoadException("ES3Type for primitive could not be found, and the type list is empty. Please contact Easy Save developers at http://www.moodkie.com/contact");
				}
				throw new TypeLoadException("ES3Type for primitive could not be found, but the type list has been initialised and is not empty. Please contact Easy Save developers on mail@moodkie.com");
			}
			else if (ES3Reflection.IsAssignableFrom(typeof(Component), type))
			{
				es3Type = new ES3ReflectedComponentType(type);
			}
			else if (ES3Reflection.IsValueType(type))
			{
				es3Type = new ES3ReflectedValueType(type);
			}
			else if (ES3Reflection.IsAssignableFrom(typeof(ScriptableObject), type))
			{
				es3Type = new ES3ReflectedScriptableObjectType(type);
			}
			else if (ES3Reflection.HasParameterlessConstructor(type) || ES3Reflection.IsAbstract(type) || ES3Reflection.IsInterface(type))
			{
				es3Type = new ES3ReflectedObjectType(type);
			}
			else
			{
				if (throwException)
				{
					throw new NotSupportedException("Type of " + ((type != null) ? type.ToString() : null) + " is not supported as it does not have a parameterless constructor. Only value types, Components or ScriptableObjects are supportable without a parameterless constructor. However, you may be able to create an ES3Type script to add support for it.");
				}
				return null;
			}
			if (!(es3Type.type == null) && !es3Type.isUnsupported)
			{
				ES3TypeMgr.Add(type, es3Type);
				return es3Type;
			}
			if (throwException)
			{
				throw new NotSupportedException(string.Format("ES3Type.type is null when trying to create an ES3Type for {0}, possibly because the element type is not supported.", type));
			}
			return null;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00022190 File Offset: 0x00020390
		internal static void Init()
		{
			object @lock = ES3TypeMgr._lock;
			lock (@lock)
			{
				ES3TypeMgr.types = new Dictionary<Type, ES3Type>();
				ES3Reflection.GetInstances<ES3Type>();
				if (ES3TypeMgr.types == null || ES3TypeMgr.types.Count == 0)
				{
					throw new TypeLoadException("Type list could not be initialised. Please contact Easy Save developers on mail@moodkie.com.");
				}
			}
		}

		// Token: 0x04000135 RID: 309
		private static object _lock = new object();

		// Token: 0x04000136 RID: 310
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static Dictionary<Type, ES3Type> types = null;

		// Token: 0x04000137 RID: 311
		private static ES3Type lastAccessedType = null;
	}
}
