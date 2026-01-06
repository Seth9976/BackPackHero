using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200012B RID: 299
	public static class TypeUtility
	{
		// Token: 0x06000814 RID: 2068 RVA: 0x0002454C File Offset: 0x0002274C
		public static bool IsBasic(this Type type)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			return type == typeof(string) || type == typeof(decimal) || type.IsEnum || (type.IsPrimitive && !(type == typeof(IntPtr)) && !(type == typeof(UIntPtr)));
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x000245CA File Offset: 0x000227CA
		public static bool IsNumeric(this Type type)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			return TypeUtility._numericTypes.Contains(type);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x000245E7 File Offset: 0x000227E7
		public static bool IsNumericConstruct(this Type type)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			return TypeUtility._numericConstructTypes.Contains(type);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00024604 File Offset: 0x00022804
		public static Namespace Namespace(this Type type)
		{
			return Unity.VisualScripting.Namespace.FromFullName(type.Namespace);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00024614 File Offset: 0x00022814
		public static Func<object> Instantiator(this Type type, bool nonPublic = true)
		{
			Func<object[], object> instantiator = type.Instantiator(nonPublic, Empty<Type>.array);
			if (instantiator != null)
			{
				return () => instantiator(Empty<object>.array);
			}
			return null;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00024650 File Offset: 0x00022850
		public static Func<object[], object> Instantiator(this Type type, bool nonPublic = true, params Type[] parameterTypes)
		{
			if (typeof(Object).IsAssignableFrom(type))
			{
				return null;
			}
			if ((type.IsValueType || type.IsBasic()) && parameterTypes.Length == 0)
			{
				return (object[] args) => type.PseudoDefault();
			}
			ConstructorInfo constructor = type.GetConstructorAccepting(parameterTypes, nonPublic);
			if (constructor != null)
			{
				return (object[] args) => constructor.Invoke(args);
			}
			return null;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x000246E0 File Offset: 0x000228E0
		public static object TryInstantiate(this Type type, bool nonPublic = true, params object[] args)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			Func<object[], object> func = type.Instantiator(nonPublic, args.Select((object arg) => arg.GetType()).ToArray<Type>());
			if (func == null)
			{
				return null;
			}
			return func(args);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0002473C File Offset: 0x0002293C
		public static object Instantiate(this Type type, bool nonPublic = true, params object[] args)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			Type[] array = args.Select((object arg) => arg.GetType()).ToArray<Type>();
			Func<object[], object> func = type.Instantiator(nonPublic, array);
			if (func == null)
			{
				throw new ArgumentException(string.Format("Type {0} cannot be{1} instantiated with the provided parameter types: {2}", type, nonPublic ? "" : " publicly", array.ToCommaSeparatedString()));
			}
			return func(args);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x000247BC File Offset: 0x000229BC
		public static object Default(this Type type)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			if (type.IsReferenceType())
			{
				return null;
			}
			object obj;
			if (!TypeUtility.defaultPrimitives.TryGetValue(type, out obj))
			{
				obj = Activator.CreateInstance(type);
			}
			return obj;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x000247FC File Offset: 0x000229FC
		public static object PseudoDefault(this Type type)
		{
			if (type == typeof(Color))
			{
				return Color.white;
			}
			if (type == typeof(string))
			{
				return string.Empty;
			}
			if (!type.IsEnum)
			{
				return type.Default();
			}
			Array values = Enum.GetValues(type);
			if (values.Length == 0)
			{
				Debug.LogWarning(string.Format("Empty enum: {0}\nThis may cause problems with serialization.", type));
				return Activator.CreateInstance(type);
			}
			DefaultValueAttribute attribute = type.GetAttribute(true);
			if (attribute != null)
			{
				return attribute.Value;
			}
			return values.GetValue(0);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0002488C File Offset: 0x00022A8C
		public static bool IsStatic(this Type type)
		{
			return type.IsAbstract && type.IsSealed;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0002489E File Offset: 0x00022A9E
		public static bool IsAbstract(this Type type)
		{
			return type.IsAbstract && !type.IsSealed;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x000248B3 File Offset: 0x00022AB3
		public static bool IsConcrete(this Type type)
		{
			return !type.IsAbstract && !type.IsInterface && !type.ContainsGenericParameters;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000248D0 File Offset: 0x00022AD0
		public static IEnumerable<Type> GetInterfaces(this Type type, bool includeInherited)
		{
			if (includeInherited || type.BaseType == null)
			{
				return type.GetInterfaces();
			}
			return type.GetInterfaces().Except(type.BaseType.GetInterfaces());
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00024900 File Offset: 0x00022B00
		public static IEnumerable<Type> BaseTypeAndInterfaces(this Type type, bool inheritedInterfaces = true)
		{
			IEnumerable<Type> enumerable = Enumerable.Empty<Type>();
			if (type.BaseType != null)
			{
				enumerable = enumerable.Concat(type.BaseType.Yield<Type>());
			}
			return enumerable.Concat(type.GetInterfaces(inheritedInterfaces));
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00024942 File Offset: 0x00022B42
		public static IEnumerable<Type> Hierarchy(this Type type)
		{
			Type baseType = type.BaseType;
			while (baseType != null)
			{
				yield return baseType;
				foreach (Type type2 in baseType.GetInterfaces(false))
				{
					yield return type2;
				}
				IEnumerator<Type> enumerator = null;
				baseType = baseType.BaseType;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00024952 File Offset: 0x00022B52
		public static IEnumerable<Type> AndBaseTypeAndInterfaces(this Type type)
		{
			return type.Yield<Type>().Concat(type.BaseTypeAndInterfaces(true));
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00024966 File Offset: 0x00022B66
		public static IEnumerable<Type> AndInterfaces(this Type type)
		{
			return type.Yield<Type>().Concat(type.GetInterfaces());
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00024979 File Offset: 0x00022B79
		public static IEnumerable<Type> AndHierarchy(this Type type)
		{
			return type.Yield<Type>().Concat(type.Hierarchy());
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0002498C File Offset: 0x00022B8C
		public static Type GetListElementType(Type listType, bool allowNonGeneric)
		{
			if (listType == null)
			{
				throw new ArgumentNullException("listType");
			}
			if (listType.IsArray)
			{
				return listType.GetElementType();
			}
			if (!typeof(IList).IsAssignableFrom(listType))
			{
				return null;
			}
			Type type = listType.AndInterfaces().FirstOrDefault((Type i) => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IList<>));
			if (!(type == null))
			{
				return type.GetGenericArguments()[0];
			}
			if (allowNonGeneric)
			{
				return typeof(object);
			}
			return null;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00024A1C File Offset: 0x00022C1C
		public static Type GetEnumerableElementType(Type enumerableType, bool allowNonGeneric)
		{
			if (enumerableType == null)
			{
				throw new ArgumentNullException("enumerableType");
			}
			if (!typeof(IEnumerable).IsAssignableFrom(enumerableType))
			{
				return null;
			}
			Type type = enumerableType.AndInterfaces().FirstOrDefault((Type i) => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
			if (!(type == null))
			{
				return type.GetGenericArguments()[0];
			}
			if (allowNonGeneric)
			{
				return typeof(object);
			}
			return null;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00024AA0 File Offset: 0x00022CA0
		public static Type GetDictionaryItemType(Type dictionaryType, bool allowNonGeneric, int genericArgumentIndex)
		{
			if (dictionaryType == null)
			{
				throw new ArgumentNullException("dictionaryType");
			}
			if (!typeof(IDictionary).IsAssignableFrom(dictionaryType))
			{
				return null;
			}
			Type type = dictionaryType.AndInterfaces().FirstOrDefault((Type i) => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<, >));
			if (!(type == null))
			{
				return type.GetGenericArguments()[genericArgumentIndex];
			}
			if (allowNonGeneric)
			{
				return typeof(object);
			}
			return null;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00024B21 File Offset: 0x00022D21
		public static Type GetDictionaryKeyType(Type dictionaryType, bool allowNonGeneric)
		{
			return TypeUtility.GetDictionaryItemType(dictionaryType, allowNonGeneric, 0);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00024B2B File Offset: 0x00022D2B
		public static Type GetDictionaryValueType(Type dictionaryType, bool allowNonGeneric)
		{
			return TypeUtility.GetDictionaryItemType(dictionaryType, allowNonGeneric, 1);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00024B35 File Offset: 0x00022D35
		public static bool IsNullable(this Type type)
		{
			return type.IsReferenceType() || Nullable.GetUnderlyingType(type) != null;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00024B4D File Offset: 0x00022D4D
		public static bool IsReferenceType(this Type type)
		{
			return !type.IsValueType;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00024B58 File Offset: 0x00022D58
		public static bool IsStruct(this Type type)
		{
			return type.IsValueType && !type.IsPrimitive && !type.IsEnum;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00024B75 File Offset: 0x00022D75
		public static bool IsAssignableFrom(this Type type, object value)
		{
			if (value == null)
			{
				return type.IsNullable();
			}
			return type.IsInstanceOfType(value);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00024B88 File Offset: 0x00022D88
		public static bool CanMakeGenericTypeVia(this Type openConstructedType, Type closedConstructedType)
		{
			Ensure.That("openConstructedType").IsNotNull<Type>(openConstructedType);
			Ensure.That("closedConstructedType").IsNotNull<Type>(closedConstructedType);
			if (openConstructedType == closedConstructedType)
			{
				return true;
			}
			if (openConstructedType.IsGenericParameter)
			{
				GenericParameterAttributes genericParameterAttributes = openConstructedType.GenericParameterAttributes;
				if (genericParameterAttributes != GenericParameterAttributes.None)
				{
					if (genericParameterAttributes.HasFlag(GenericParameterAttributes.NotNullableValueTypeConstraint) && !closedConstructedType.IsValueType)
					{
						return false;
					}
					if (genericParameterAttributes.HasFlag(GenericParameterAttributes.ReferenceTypeConstraint) && closedConstructedType.IsValueType)
					{
						return false;
					}
					if (genericParameterAttributes.HasFlag(GenericParameterAttributes.DefaultConstructorConstraint) && closedConstructedType.GetConstructor(Type.EmptyTypes) == null)
					{
						return false;
					}
				}
				Type[] genericParameterConstraints = openConstructedType.GetGenericParameterConstraints();
				for (int i = 0; i < genericParameterConstraints.Length; i++)
				{
					if (!genericParameterConstraints[i].IsAssignableFrom(closedConstructedType))
					{
						return false;
					}
				}
				return true;
			}
			if (!openConstructedType.ContainsGenericParameters)
			{
				return openConstructedType.IsAssignableFrom(closedConstructedType);
			}
			if (openConstructedType.IsGenericType)
			{
				Type genericTypeDefinition = openConstructedType.GetGenericTypeDefinition();
				foreach (Type type in closedConstructedType.AndBaseTypeAndInterfaces())
				{
					if (type.IsGenericType && type.GetGenericTypeDefinition() == genericTypeDefinition)
					{
						Type[] genericArguments = type.GetGenericArguments();
						Type[] genericArguments2 = openConstructedType.GetGenericArguments();
						for (int j = 0; j < genericArguments2.Length; j++)
						{
							if (!genericArguments2[j].CanMakeGenericTypeVia(genericArguments[j]))
							{
								return false;
							}
						}
						return true;
					}
				}
				return false;
			}
			if (openConstructedType.IsArray)
			{
				if (!closedConstructedType.IsArray || closedConstructedType.GetArrayRank() != openConstructedType.GetArrayRank())
				{
					return false;
				}
				Type elementType = openConstructedType.GetElementType();
				Type elementType2 = closedConstructedType.GetElementType();
				return elementType.CanMakeGenericTypeVia(elementType2);
			}
			else
			{
				if (!openConstructedType.IsByRef)
				{
					throw new NotImplementedException();
				}
				if (!closedConstructedType.IsByRef)
				{
					return false;
				}
				Type elementType3 = openConstructedType.GetElementType();
				Type elementType4 = closedConstructedType.GetElementType();
				return elementType3.CanMakeGenericTypeVia(elementType4);
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00024D88 File Offset: 0x00022F88
		public static Type MakeGenericTypeVia(this Type openConstructedType, Type closedConstructedType, Dictionary<Type, Type> resolvedGenericParameters, bool safe = true)
		{
			Ensure.That("openConstructedType").IsNotNull<Type>(openConstructedType);
			Ensure.That("closedConstructedType").IsNotNull<Type>(closedConstructedType);
			Ensure.That("resolvedGenericParameters").IsNotNull<Dictionary<Type, Type>>(resolvedGenericParameters);
			if (safe && !openConstructedType.CanMakeGenericTypeVia(closedConstructedType))
			{
				throw new GenericClosingException(openConstructedType, closedConstructedType);
			}
			if (openConstructedType == closedConstructedType)
			{
				return openConstructedType;
			}
			if (openConstructedType.IsGenericParameter)
			{
				if (!closedConstructedType.ContainsGenericParameters)
				{
					if (resolvedGenericParameters.ContainsKey(openConstructedType))
					{
						if (resolvedGenericParameters[openConstructedType] != closedConstructedType)
						{
							throw new InvalidOperationException("Nested generic parameters resolve to different values.");
						}
					}
					else
					{
						resolvedGenericParameters.Add(openConstructedType, closedConstructedType);
					}
				}
				return closedConstructedType;
			}
			if (!openConstructedType.ContainsGenericParameters)
			{
				return openConstructedType;
			}
			if (openConstructedType.IsGenericType)
			{
				Type genericTypeDefinition = openConstructedType.GetGenericTypeDefinition();
				Type[] genericArguments = openConstructedType.GetGenericArguments();
				foreach (Type type in closedConstructedType.AndBaseTypeAndInterfaces())
				{
					if (type.IsGenericType && type.GetGenericTypeDefinition() == genericTypeDefinition)
					{
						Type[] genericArguments2 = type.GetGenericArguments();
						Type[] array = new Type[genericArguments.Length];
						for (int i = 0; i < genericArguments.Length; i++)
						{
							array[i] = genericArguments[i].MakeGenericTypeVia(genericArguments2[i], resolvedGenericParameters, false);
						}
						return genericTypeDefinition.MakeGenericType(array);
					}
				}
				throw new GenericClosingException(openConstructedType, closedConstructedType);
			}
			if (openConstructedType.IsArray)
			{
				int arrayRank = openConstructedType.GetArrayRank();
				if (!closedConstructedType.IsArray || closedConstructedType.GetArrayRank() != arrayRank)
				{
					throw new GenericClosingException(openConstructedType, closedConstructedType);
				}
				Type elementType = openConstructedType.GetElementType();
				Type elementType2 = closedConstructedType.GetElementType();
				return elementType.MakeGenericTypeVia(elementType2, resolvedGenericParameters, false).MakeArrayType(arrayRank);
			}
			else
			{
				if (!openConstructedType.IsByRef)
				{
					throw new NotImplementedException();
				}
				if (!closedConstructedType.IsByRef)
				{
					throw new GenericClosingException(openConstructedType, closedConstructedType);
				}
				Type elementType3 = openConstructedType.GetElementType();
				Type elementType4 = closedConstructedType.GetElementType();
				return elementType3.MakeGenericTypeVia(elementType4, resolvedGenericParameters, false).MakeByRefType();
			}
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00024F6C File Offset: 0x0002316C
		public static string ToShortString(this object o, int maxLength = 20)
		{
			Type type = ((o != null) ? o.GetType() : null);
			if (type == null || o.IsUnityNull())
			{
				return "Null";
			}
			if (type == typeof(float))
			{
				return ((float)o).ToString("0.##");
			}
			if (type == typeof(double))
			{
				return ((double)o).ToString("0.##");
			}
			if (type == typeof(decimal))
			{
				return ((decimal)o).ToString("0.##");
			}
			if (type.IsBasic() || TypeUtility.typesWithShortStrings.Contains(type))
			{
				return o.ToString().Truncate(maxLength, "...");
			}
			if (typeof(Object).IsAssignableFrom(type))
			{
				return ((Object)o).name.Truncate(maxLength, "...");
			}
			return null;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00025060 File Offset: 0x00023260
		public static IEnumerable<Type> GetTypesSafely(this Assembly assembly)
		{
			Type[] array;
			ReflectionTypeLoadException ex2;
			object obj;
			ReflectionTypeLoadException ex;
			bool flag;
			try
			{
				array = assembly.GetTypes();
			}
			catch when (delegate
			{
				// Failed to create a 'catch-when' expression
				ex = obj as ReflectionTypeLoadException;
				if (ex == null)
				{
					flag = false;
				}
				else
				{
					ex2 = ex;
					flag = ex2.Types.Any((Type t) => t != null) > false;
				}
				endfilter(flag);
			})
			{
				array = ex2.Types.Where((Type t) => t != null).ToArray<Type>();
			}
			catch (Exception ex3)
			{
				Debug.LogWarning(string.Format("Failed to load types in assembly '{0}'.\n{1}", assembly, ex3));
				yield break;
			}
			foreach (Type type in array)
			{
				if (!(type == typeof(void)))
				{
					yield return type;
				}
			}
			Type[] array2 = null;
			yield break;
		}

		// Token: 0x040001FE RID: 510
		private static readonly HashSet<Type> _numericTypes = new HashSet<Type>
		{
			typeof(byte),
			typeof(sbyte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(decimal)
		};

		// Token: 0x040001FF RID: 511
		private static readonly HashSet<Type> _numericConstructTypes = new HashSet<Type>
		{
			typeof(Vector2),
			typeof(Vector3),
			typeof(Vector4),
			typeof(Quaternion),
			typeof(Matrix4x4),
			typeof(Rect)
		};

		// Token: 0x04000200 RID: 512
		private static readonly HashSet<Type> typesWithShortStrings = new HashSet<Type>
		{
			typeof(string),
			typeof(Vector2),
			typeof(Vector3),
			typeof(Vector4)
		};

		// Token: 0x04000201 RID: 513
		private static readonly Dictionary<Type, object> defaultPrimitives = new Dictionary<Type, object>
		{
			{
				typeof(int),
				0
			},
			{
				typeof(uint),
				0U
			},
			{
				typeof(long),
				0L
			},
			{
				typeof(ulong),
				0UL
			},
			{
				typeof(short),
				0
			},
			{
				typeof(ushort),
				0
			},
			{
				typeof(byte),
				0
			},
			{
				typeof(sbyte),
				0
			},
			{
				typeof(float),
				0f
			},
			{
				typeof(double),
				0.0
			},
			{
				typeof(decimal),
				0m
			},
			{
				typeof(Vector2),
				default(Vector2)
			},
			{
				typeof(Vector3),
				default(Vector3)
			},
			{
				typeof(Vector4),
				default(Vector4)
			}
		};
	}
}
