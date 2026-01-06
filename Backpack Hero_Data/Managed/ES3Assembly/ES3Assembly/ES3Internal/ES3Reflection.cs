using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ES3Types;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000CD RID: 205
	public static class ES3Reflection
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0001FCEC File Offset: 0x0001DEEC
		private static Assembly[] Assemblies
		{
			get
			{
				if (ES3Reflection._assemblies == null)
				{
					string[] assemblyNames = new ES3Settings(null, null).assemblyNames;
					List<Assembly> list = new List<Assembly>();
					for (int i = 0; i < assemblyNames.Length; i++)
					{
						try
						{
							Assembly assembly = Assembly.Load(new AssemblyName(assemblyNames[i]));
							if (assembly != null)
							{
								list.Add(assembly);
							}
						}
						catch
						{
						}
					}
					ES3Reflection._assemblies = list.ToArray();
				}
				return ES3Reflection._assemblies;
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0001FD68 File Offset: 0x0001DF68
		public static Type[] GetElementTypes(Type type)
		{
			if (ES3Reflection.IsGenericType(type))
			{
				return ES3Reflection.GetGenericArguments(type);
			}
			if (type.IsArray)
			{
				return new Type[] { ES3Reflection.GetElementType(type) };
			}
			return null;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001FD94 File Offset: 0x0001DF94
		public static List<FieldInfo> GetSerializableFields(Type type, List<FieldInfo> serializableFields = null, bool safe = true, string[] memberNames = null, BindingFlags bindings = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
		{
			if (type == null)
			{
				return new List<FieldInfo>();
			}
			FieldInfo[] fields = type.GetFields(bindings);
			if (serializableFields == null)
			{
				serializableFields = new List<FieldInfo>();
			}
			foreach (FieldInfo fieldInfo in fields)
			{
				string name = fieldInfo.Name;
				if (memberNames == null || memberNames.Contains(name))
				{
					Type fieldType = fieldInfo.FieldType;
					if (ES3Reflection.AttributeIsDefined(fieldInfo, ES3Reflection.es3SerializableAttributeType))
					{
						serializableFields.Add(fieldInfo);
					}
					else if (!ES3Reflection.AttributeIsDefined(fieldInfo, ES3Reflection.es3NonSerializableAttributeType) && (!safe || fieldInfo.IsPublic || ES3Reflection.AttributeIsDefined(fieldInfo, ES3Reflection.serializeFieldAttributeType)) && !fieldInfo.IsLiteral && !fieldInfo.IsInitOnly && (!(fieldType == type) || ES3Reflection.IsAssignableFrom(typeof(Object), fieldType)) && !ES3Reflection.AttributeIsDefined(fieldInfo, ES3Reflection.nonSerializedAttributeType) && !ES3Reflection.AttributeIsDefined(fieldInfo, ES3Reflection.obsoleteAttributeType) && ES3Reflection.TypeIsSerializable(fieldInfo.FieldType) && (!safe || !name.StartsWith("m_") || fieldInfo.DeclaringType.Namespace == null || !fieldInfo.DeclaringType.Namespace.Contains("UnityEngine")))
					{
						serializableFields.Add(fieldInfo);
					}
				}
			}
			Type type2 = ES3Reflection.BaseType(type);
			if (type2 != null && type2 != typeof(object) && type2 != typeof(Object))
			{
				ES3Reflection.GetSerializableFields(ES3Reflection.BaseType(type), serializableFields, safe, memberNames, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
			return serializableFields;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001FF1C File Offset: 0x0001E11C
		public static List<PropertyInfo> GetSerializableProperties(Type type, List<PropertyInfo> serializableProperties = null, bool safe = true, string[] memberNames = null, BindingFlags bindings = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
		{
			bool flag = ES3Reflection.IsAssignableFrom(typeof(Component), type);
			if (!safe)
			{
				bindings |= BindingFlags.NonPublic;
			}
			PropertyInfo[] properties = type.GetProperties(bindings);
			if (serializableProperties == null)
			{
				serializableProperties = new List<PropertyInfo>();
			}
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (ES3Reflection.AttributeIsDefined(propertyInfo, ES3Reflection.es3SerializableAttributeType))
				{
					serializableProperties.Add(propertyInfo);
				}
				else if (!ES3Reflection.AttributeIsDefined(propertyInfo, ES3Reflection.es3NonSerializableAttributeType))
				{
					string name = propertyInfo.Name;
					if (!ES3Reflection.excludedPropertyNames.Contains(name) && (memberNames == null || memberNames.Contains(name)) && (!safe || ES3Reflection.AttributeIsDefined(propertyInfo, ES3Reflection.serializeFieldAttributeType) || ES3Reflection.AttributeIsDefined(propertyInfo, ES3Reflection.es3SerializableAttributeType)))
					{
						Type propertyType = propertyInfo.PropertyType;
						if ((!(propertyType == type) || ES3Reflection.IsAssignableFrom(typeof(Object), propertyType)) && propertyInfo.CanRead && propertyInfo.CanWrite && (propertyInfo.GetIndexParameters().Length == 0 || propertyType.IsArray) && ES3Reflection.TypeIsSerializable(propertyType) && (!flag || (!(name == "tag") && !(name == "name"))) && !ES3Reflection.AttributeIsDefined(propertyInfo, ES3Reflection.obsoleteAttributeType) && !ES3Reflection.AttributeIsDefined(propertyInfo, ES3Reflection.nonSerializedAttributeType))
						{
							serializableProperties.Add(propertyInfo);
						}
					}
				}
			}
			Type type2 = ES3Reflection.BaseType(type);
			if (type2 != null && type2 != typeof(object))
			{
				ES3Reflection.GetSerializableProperties(type2, serializableProperties, safe, memberNames, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
			return serializableProperties;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000200B0 File Offset: 0x0001E2B0
		public static bool TypeIsSerializable(Type type)
		{
			if (type == null)
			{
				return false;
			}
			if (ES3Reflection.AttributeIsDefined(type, ES3Reflection.es3NonSerializableAttributeType))
			{
				return false;
			}
			if (ES3Reflection.IsPrimitive(type) || ES3Reflection.IsValueType(type) || ES3Reflection.IsAssignableFrom(typeof(Component), type) || ES3Reflection.IsAssignableFrom(typeof(ScriptableObject), type))
			{
				return true;
			}
			ES3Type orCreateES3Type = ES3TypeMgr.GetOrCreateES3Type(type, false);
			if (orCreateES3Type != null && !orCreateES3Type.isUnsupported)
			{
				return true;
			}
			if (ES3Reflection.TypeIsArray(type))
			{
				return ES3Reflection.TypeIsSerializable(type.GetElementType());
			}
			Type[] genericArguments = type.GetGenericArguments();
			for (int i = 0; i < genericArguments.Length; i++)
			{
				if (!ES3Reflection.TypeIsSerializable(genericArguments[i]))
				{
					return false;
				}
			}
			return ES3Reflection.HasParameterlessConstructor(type);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00020167 File Offset: 0x0001E367
		public static object CreateInstance(Type type)
		{
			if (ES3Reflection.IsAssignableFrom(typeof(Component), type))
			{
				return ES3ComponentType.CreateComponent(type);
			}
			if (ES3Reflection.IsAssignableFrom(typeof(ScriptableObject), type))
			{
				return ScriptableObject.CreateInstance(type);
			}
			return Activator.CreateInstance(type);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000201A1 File Offset: 0x0001E3A1
		public static object CreateInstance(Type type, params object[] args)
		{
			if (ES3Reflection.IsAssignableFrom(typeof(Component), type))
			{
				return ES3ComponentType.CreateComponent(type);
			}
			if (ES3Reflection.IsAssignableFrom(typeof(ScriptableObject), type))
			{
				return ScriptableObject.CreateInstance(type);
			}
			return Activator.CreateInstance(type, args);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000201DC File Offset: 0x0001E3DC
		public static Array ArrayCreateInstance(Type type, int length)
		{
			return Array.CreateInstance(type, new int[] { length });
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000201EE File Offset: 0x0001E3EE
		public static Array ArrayCreateInstance(Type type, int[] dimensions)
		{
			return Array.CreateInstance(type, dimensions);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000201F7 File Offset: 0x0001E3F7
		public static Type MakeGenericType(Type type, Type genericParam)
		{
			return type.MakeGenericType(new Type[] { genericParam });
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0002020C File Offset: 0x0001E40C
		public static ES3Reflection.ES3ReflectedMember[] GetSerializableMembers(Type type, bool safe = true, string[] memberNames = null)
		{
			if (type == null)
			{
				return new ES3Reflection.ES3ReflectedMember[0];
			}
			List<FieldInfo> serializableFields = ES3Reflection.GetSerializableFields(type, new List<FieldInfo>(), safe, memberNames, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			List<PropertyInfo> serializableProperties = ES3Reflection.GetSerializableProperties(type, new List<PropertyInfo>(), safe, memberNames, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			ES3Reflection.ES3ReflectedMember[] array = new ES3Reflection.ES3ReflectedMember[serializableFields.Count + serializableProperties.Count];
			for (int i = 0; i < serializableFields.Count; i++)
			{
				array[i] = new ES3Reflection.ES3ReflectedMember(serializableFields[i]);
			}
			for (int j = 0; j < serializableProperties.Count; j++)
			{
				array[j + serializableFields.Count] = new ES3Reflection.ES3ReflectedMember(serializableProperties[j]);
			}
			return array;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000202B2 File Offset: 0x0001E4B2
		public static ES3Reflection.ES3ReflectedMember GetES3ReflectedProperty(Type type, string propertyName)
		{
			return new ES3Reflection.ES3ReflectedMember(ES3Reflection.GetProperty(type, propertyName));
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000202C0 File Offset: 0x0001E4C0
		public static ES3Reflection.ES3ReflectedMember GetES3ReflectedMember(Type type, string fieldName)
		{
			return new ES3Reflection.ES3ReflectedMember(ES3Reflection.GetField(type, fieldName));
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000202D0 File Offset: 0x0001E4D0
		public static IList<T> GetInstances<T>()
		{
			List<T> list = new List<T>();
			Assembly[] assemblies = ES3Reflection.Assemblies;
			for (int i = 0; i < assemblies.Length; i++)
			{
				foreach (Type type in assemblies[i].GetTypes())
				{
					if (ES3Reflection.IsAssignableFrom(typeof(T), type) && ES3Reflection.HasParameterlessConstructor(type) && !ES3Reflection.IsAbstract(type))
					{
						list.Add((T)((object)Activator.CreateInstance(type)));
					}
				}
			}
			return list;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00020350 File Offset: 0x0001E550
		public static IList<Type> GetDerivedTypes(Type derivedType)
		{
			return (from assembly in ES3Reflection.Assemblies
				from type in assembly.GetTypes()
				where ES3Reflection.IsAssignableFrom(derivedType, type)
				select type).ToList<Type>();
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000203EC File Offset: 0x0001E5EC
		public static bool IsAssignableFrom(Type a, Type b)
		{
			return a.IsAssignableFrom(b);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000203F5 File Offset: 0x0001E5F5
		public static Type GetGenericTypeDefinition(Type type)
		{
			return type.GetGenericTypeDefinition();
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000203FD File Offset: 0x0001E5FD
		public static Type[] GetGenericArguments(Type type)
		{
			return type.GetGenericArguments();
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00020405 File Offset: 0x0001E605
		public static int GetArrayRank(Type type)
		{
			return type.GetArrayRank();
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0002040D File Offset: 0x0001E60D
		public static string GetAssemblyQualifiedName(Type type)
		{
			return type.AssemblyQualifiedName;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00020415 File Offset: 0x0001E615
		public static ES3Reflection.ES3ReflectedMethod GetMethod(Type type, string methodName, Type[] genericParameters, Type[] parameterTypes)
		{
			return new ES3Reflection.ES3ReflectedMethod(type, methodName, genericParameters, parameterTypes);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00020420 File Offset: 0x0001E620
		public static bool TypeIsArray(Type type)
		{
			return type.IsArray;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00020428 File Offset: 0x0001E628
		public static Type GetElementType(Type type)
		{
			return type.GetElementType();
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00020430 File Offset: 0x0001E630
		public static bool IsAbstract(Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00020438 File Offset: 0x0001E638
		public static bool IsInterface(Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00020440 File Offset: 0x0001E640
		public static bool IsGenericType(Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00020448 File Offset: 0x0001E648
		public static bool IsValueType(Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00020450 File Offset: 0x0001E650
		public static bool IsEnum(Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00020458 File Offset: 0x0001E658
		public static bool HasParameterlessConstructor(Type type)
		{
			return type.GetConstructor(Type.EmptyTypes) != null || ES3Reflection.IsValueType(type);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00020475 File Offset: 0x0001E675
		public static ConstructorInfo GetParameterlessConstructor(Type type)
		{
			return type.GetConstructor(Type.EmptyTypes);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00020482 File Offset: 0x0001E682
		public static string GetShortAssemblyQualifiedName(Type type)
		{
			if (ES3Reflection.IsPrimitive(type))
			{
				return type.ToString();
			}
			return type.FullName + "," + type.Assembly.GetName().Name;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000204B4 File Offset: 0x0001E6B4
		public static PropertyInfo GetProperty(Type type, string propertyName)
		{
			PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (property == null && ES3Reflection.BaseType(type) != typeof(object))
			{
				return ES3Reflection.GetProperty(ES3Reflection.BaseType(type), propertyName);
			}
			return property;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000204FC File Offset: 0x0001E6FC
		public static FieldInfo GetField(Type type, string fieldName)
		{
			FieldInfo field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (field == null && ES3Reflection.BaseType(type) != typeof(object))
			{
				return ES3Reflection.GetField(ES3Reflection.BaseType(type), fieldName);
			}
			return field;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00020544 File Offset: 0x0001E744
		public static MethodInfo[] GetMethods(Type type, string methodName)
		{
			return (from t in type.GetMethods()
				where t.Name == methodName
				select t).ToArray<MethodInfo>();
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0002057A File Offset: 0x0001E77A
		public static bool IsPrimitive(Type type)
		{
			return type.IsPrimitive || type == typeof(string) || type == typeof(decimal);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000205A8 File Offset: 0x0001E7A8
		public static bool AttributeIsDefined(MemberInfo info, Type attributeType)
		{
			return Attribute.IsDefined(info, attributeType, true);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000205B2 File Offset: 0x0001E7B2
		public static bool AttributeIsDefined(Type type, Type attributeType)
		{
			return type.IsDefined(attributeType, true);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000205BC File Offset: 0x0001E7BC
		public static bool ImplementsInterface(Type type, Type interfaceType)
		{
			return type.GetInterface(interfaceType.Name) != null;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000205D0 File Offset: 0x0001E7D0
		public static Type BaseType(Type type)
		{
			return type.BaseType;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000205D8 File Offset: 0x0001E7D8
		public static Type GetType(string typeString)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(typeString);
			if (num <= 2667225454U)
			{
				if (num <= 1625787317U)
				{
					if (num <= 520654156U)
					{
						if (num != 356760993U)
						{
							if (num != 398550328U)
							{
								if (num == 520654156U)
								{
									if (typeString == "decimal")
									{
										return typeof(decimal);
									}
								}
							}
							else if (typeString == "string")
							{
								return typeof(string);
							}
						}
						else if (typeString == "UnityEngine.Object")
						{
							return typeof(Object);
						}
					}
					else if (num != 718440320U)
					{
						if (num != 1416355490U)
						{
							if (num == 1625787317U)
							{
								if (typeString == "System.Object")
								{
									return typeof(object);
								}
							}
						}
						else if (typeString == "Texture2D")
						{
							return typeof(Texture2D);
						}
					}
					else if (typeString == "Component")
					{
						return typeof(Component);
					}
				}
				else if (num <= 2197844016U)
				{
					if (num != 1630192034U)
					{
						if (num != 1683620383U)
						{
							if (num == 2197844016U)
							{
								if (typeString == "Vector2")
								{
									return typeof(Vector2);
								}
							}
						}
						else if (typeString == "byte")
						{
							return typeof(byte);
						}
					}
					else if (typeString == "ushort")
					{
						return typeof(ushort);
					}
				}
				else if (num <= 2298509730U)
				{
					if (num != 2214621635U)
					{
						if (num == 2298509730U)
						{
							if (typeString == "Vector4")
							{
								return typeof(Vector4);
							}
						}
					}
					else if (typeString == "Vector3")
					{
						return typeof(Vector3);
					}
				}
				else if (num != 2515107422U)
				{
					if (num == 2667225454U)
					{
						if (typeString == "ulong")
						{
							return typeof(ulong);
						}
					}
				}
				else if (typeString == "int")
				{
					return typeof(int);
				}
			}
			else if (num <= 3270303571U)
			{
				if (num <= 2823553821U)
				{
					if (num != 2699759368U)
					{
						if (num != 2797886853U)
						{
							if (num == 2823553821U)
							{
								if (typeString == "char")
								{
									return typeof(char);
								}
							}
						}
						else if (typeString == "float")
						{
							return typeof(float);
						}
					}
					else if (typeString == "double")
					{
						return typeof(double);
					}
				}
				else if (num != 2911022011U)
				{
					if (num != 3122818005U)
					{
						if (num == 3270303571U)
						{
							if (typeString == "long")
							{
								return typeof(long);
							}
						}
					}
					else if (typeString == "short")
					{
						return typeof(short);
					}
				}
				else if (typeString == "Transform")
				{
					return typeof(Transform);
				}
			}
			else if (num <= 3415750305U)
			{
				if (num != 3289806692U)
				{
					if (num != 3365180733U)
					{
						if (num == 3415750305U)
						{
							if (typeString == "uint")
							{
								return typeof(uint);
							}
						}
					}
					else if (typeString == "bool")
					{
						return typeof(bool);
					}
				}
				else if (typeString == "GameObject")
				{
					return typeof(GameObject);
				}
			}
			else if (num <= 3847869726U)
			{
				if (num != 3419754368U)
				{
					if (num == 3847869726U)
					{
						if (typeString == "MeshFilter")
						{
							return typeof(MeshFilter);
						}
					}
				}
				else if (typeString == "Material")
				{
					return typeof(Material);
				}
			}
			else if (num != 3853794552U)
			{
				if (num == 4088464520U)
				{
					if (typeString == "sbyte")
					{
						return typeof(sbyte);
					}
				}
			}
			else if (typeString == "Color")
			{
				return typeof(Color);
			}
			return Type.GetType(typeString);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00020AC8 File Offset: 0x0001ECC8
		public static string GetTypeString(Type type)
		{
			if (type == typeof(bool))
			{
				return "bool";
			}
			if (type == typeof(byte))
			{
				return "byte";
			}
			if (type == typeof(sbyte))
			{
				return "sbyte";
			}
			if (type == typeof(char))
			{
				return "char";
			}
			if (type == typeof(decimal))
			{
				return "decimal";
			}
			if (type == typeof(double))
			{
				return "double";
			}
			if (type == typeof(float))
			{
				return "float";
			}
			if (type == typeof(int))
			{
				return "int";
			}
			if (type == typeof(uint))
			{
				return "uint";
			}
			if (type == typeof(long))
			{
				return "long";
			}
			if (type == typeof(ulong))
			{
				return "ulong";
			}
			if (type == typeof(short))
			{
				return "short";
			}
			if (type == typeof(ushort))
			{
				return "ushort";
			}
			if (type == typeof(string))
			{
				return "string";
			}
			if (type == typeof(Vector2))
			{
				return "Vector2";
			}
			if (type == typeof(Vector3))
			{
				return "Vector3";
			}
			if (type == typeof(Vector4))
			{
				return "Vector4";
			}
			if (type == typeof(Color))
			{
				return "Color";
			}
			if (type == typeof(Transform))
			{
				return "Transform";
			}
			if (type == typeof(Component))
			{
				return "Component";
			}
			if (type == typeof(GameObject))
			{
				return "GameObject";
			}
			if (type == typeof(MeshFilter))
			{
				return "MeshFilter";
			}
			if (type == typeof(Material))
			{
				return "Material";
			}
			if (type == typeof(Texture2D))
			{
				return "Texture2D";
			}
			if (type == typeof(Object))
			{
				return "UnityEngine.Object";
			}
			if (type == typeof(object))
			{
				return "System.Object";
			}
			return ES3Reflection.GetShortAssemblyQualifiedName(type);
		}

		// Token: 0x04000115 RID: 277
		public const string memberFieldPrefix = "m_";

		// Token: 0x04000116 RID: 278
		public const string componentTagFieldName = "tag";

		// Token: 0x04000117 RID: 279
		public const string componentNameFieldName = "name";

		// Token: 0x04000118 RID: 280
		public static readonly string[] excludedPropertyNames = new string[] { "runInEditMode", "useGUILayout", "hideFlags" };

		// Token: 0x04000119 RID: 281
		public static readonly Type serializableAttributeType = typeof(SerializableAttribute);

		// Token: 0x0400011A RID: 282
		public static readonly Type serializeFieldAttributeType = typeof(SerializeField);

		// Token: 0x0400011B RID: 283
		public static readonly Type obsoleteAttributeType = typeof(ObsoleteAttribute);

		// Token: 0x0400011C RID: 284
		public static readonly Type nonSerializedAttributeType = typeof(NonSerializedAttribute);

		// Token: 0x0400011D RID: 285
		public static readonly Type es3SerializableAttributeType = typeof(ES3Serializable);

		// Token: 0x0400011E RID: 286
		public static readonly Type es3NonSerializableAttributeType = typeof(ES3NonSerializable);

		// Token: 0x0400011F RID: 287
		public static Type[] EmptyTypes = new Type[0];

		// Token: 0x04000120 RID: 288
		private static Assembly[] _assemblies = null;

		// Token: 0x020000FA RID: 250
		public struct ES3ReflectedMember
		{
			// Token: 0x1700002E RID: 46
			// (get) Token: 0x06000556 RID: 1366 RVA: 0x00023CB5 File Offset: 0x00021EB5
			public bool IsNull
			{
				get
				{
					return this.fieldInfo == null && this.propertyInfo == null;
				}
			}

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000557 RID: 1367 RVA: 0x00023CD3 File Offset: 0x00021ED3
			public string Name
			{
				get
				{
					if (!this.isProperty)
					{
						return this.fieldInfo.Name;
					}
					return this.propertyInfo.Name;
				}
			}

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x06000558 RID: 1368 RVA: 0x00023CF4 File Offset: 0x00021EF4
			public Type MemberType
			{
				get
				{
					if (!this.isProperty)
					{
						return this.fieldInfo.FieldType;
					}
					return this.propertyInfo.PropertyType;
				}
			}

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x06000559 RID: 1369 RVA: 0x00023D15 File Offset: 0x00021F15
			public bool IsPublic
			{
				get
				{
					if (!this.isProperty)
					{
						return this.fieldInfo.IsPublic;
					}
					return this.propertyInfo.GetGetMethod(true).IsPublic && this.propertyInfo.GetSetMethod(true).IsPublic;
				}
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x0600055A RID: 1370 RVA: 0x00023D51 File Offset: 0x00021F51
			public bool IsProtected
			{
				get
				{
					if (!this.isProperty)
					{
						return this.fieldInfo.IsFamily;
					}
					return this.propertyInfo.GetGetMethod(true).IsFamily;
				}
			}

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x0600055B RID: 1371 RVA: 0x00023D78 File Offset: 0x00021F78
			public bool IsStatic
			{
				get
				{
					if (!this.isProperty)
					{
						return this.fieldInfo.IsStatic;
					}
					return this.propertyInfo.GetGetMethod(true).IsStatic;
				}
			}

			// Token: 0x0600055C RID: 1372 RVA: 0x00023DA0 File Offset: 0x00021FA0
			public ES3ReflectedMember(object fieldPropertyInfo)
			{
				if (fieldPropertyInfo == null)
				{
					this.propertyInfo = null;
					this.fieldInfo = null;
					this.isProperty = false;
					return;
				}
				this.isProperty = ES3Reflection.IsAssignableFrom(typeof(PropertyInfo), fieldPropertyInfo.GetType());
				if (this.isProperty)
				{
					this.propertyInfo = (PropertyInfo)fieldPropertyInfo;
					this.fieldInfo = null;
					return;
				}
				this.fieldInfo = (FieldInfo)fieldPropertyInfo;
				this.propertyInfo = null;
			}

			// Token: 0x0600055D RID: 1373 RVA: 0x00023E10 File Offset: 0x00022010
			public void SetValue(object obj, object value)
			{
				if (this.isProperty)
				{
					this.propertyInfo.SetValue(obj, value, null);
					return;
				}
				this.fieldInfo.SetValue(obj, value);
			}

			// Token: 0x0600055E RID: 1374 RVA: 0x00023E36 File Offset: 0x00022036
			public object GetValue(object obj)
			{
				if (this.isProperty)
				{
					return this.propertyInfo.GetValue(obj, null);
				}
				return this.fieldInfo.GetValue(obj);
			}

			// Token: 0x040001D0 RID: 464
			private FieldInfo fieldInfo;

			// Token: 0x040001D1 RID: 465
			private PropertyInfo propertyInfo;

			// Token: 0x040001D2 RID: 466
			public bool isProperty;
		}

		// Token: 0x020000FB RID: 251
		public class ES3ReflectedMethod
		{
			// Token: 0x0600055F RID: 1375 RVA: 0x00023E5C File Offset: 0x0002205C
			public ES3ReflectedMethod(Type type, string methodName, Type[] genericParameters, Type[] parameterTypes)
			{
				MethodInfo methodInfo = type.GetMethod(methodName, parameterTypes);
				this.method = methodInfo.MakeGenericMethod(genericParameters);
			}

			// Token: 0x06000560 RID: 1376 RVA: 0x00023E88 File Offset: 0x00022088
			public ES3ReflectedMethod(Type type, string methodName, Type[] genericParameters, Type[] parameterTypes, BindingFlags bindingAttr)
			{
				MethodInfo methodInfo = type.GetMethod(methodName, bindingAttr, null, parameterTypes, null);
				this.method = methodInfo.MakeGenericMethod(genericParameters);
			}

			// Token: 0x06000561 RID: 1377 RVA: 0x00023EB6 File Offset: 0x000220B6
			public object Invoke(object obj, object[] parameters = null)
			{
				return this.method.Invoke(obj, parameters);
			}

			// Token: 0x040001D3 RID: 467
			private MethodInfo method;
		}
	}
}
