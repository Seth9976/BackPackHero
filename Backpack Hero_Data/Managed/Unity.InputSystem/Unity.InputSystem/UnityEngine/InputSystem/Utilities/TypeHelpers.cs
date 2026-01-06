using System;
using System.Reflection;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000146 RID: 326
	internal static class TypeHelpers
	{
		// Token: 0x060011BF RID: 4543 RVA: 0x00053B88 File Offset: 0x00051D88
		public static TObject As<TObject>(this object obj)
		{
			if (obj == null)
			{
				return default(TObject);
			}
			return (TObject)((object)obj);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00053BA8 File Offset: 0x00051DA8
		public static bool IsInt(this TypeCode type)
		{
			switch (type)
			{
			case TypeCode.SByte:
				return true;
			case TypeCode.Byte:
				return true;
			case TypeCode.Int16:
				return true;
			case TypeCode.UInt16:
				return true;
			case TypeCode.Int32:
				return true;
			case TypeCode.UInt32:
				return true;
			case TypeCode.Int64:
				return true;
			case TypeCode.UInt64:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00053BE8 File Offset: 0x00051DE8
		public static Type GetValueType(MemberInfo member)
		{
			FieldInfo fieldInfo = member as FieldInfo;
			if (fieldInfo != null)
			{
				return fieldInfo.FieldType;
			}
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo != null)
			{
				return propertyInfo.PropertyType;
			}
			MethodInfo methodInfo = member as MethodInfo;
			if (methodInfo != null)
			{
				return methodInfo.ReturnType;
			}
			return null;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00053C3C File Offset: 0x00051E3C
		public static string GetNiceTypeName(this Type type)
		{
			if (type.IsPrimitive)
			{
				if (type == typeof(int))
				{
					return "int";
				}
				if (type == typeof(float))
				{
					return "float";
				}
				if (type == typeof(char))
				{
					return "char";
				}
				if (type == typeof(byte))
				{
					return "byte";
				}
				if (type == typeof(short))
				{
					return "short";
				}
				if (type == typeof(long))
				{
					return "long";
				}
				if (type == typeof(double))
				{
					return "double";
				}
				if (type == typeof(uint))
				{
					return "uint";
				}
				if (type == typeof(sbyte))
				{
					return "sbyte";
				}
				if (type == typeof(ushort))
				{
					return "ushort";
				}
				if (type == typeof(ulong))
				{
					return "ulong";
				}
			}
			return type.Name;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00053D64 File Offset: 0x00051F64
		public static Type GetGenericTypeArgumentFromHierarchy(Type type, Type genericTypeDefinition, int argumentIndex)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (genericTypeDefinition == null)
			{
				throw new ArgumentNullException("genericTypeDefinition");
			}
			if (argumentIndex < 0)
			{
				throw new ArgumentOutOfRangeException("argumentIndex");
			}
			if (genericTypeDefinition.IsInterface)
			{
				Type genericTypeArgumentFromHierarchy;
				for (;;)
				{
					Type[] interfaces = type.GetInterfaces();
					bool flag = false;
					foreach (Type type2 in interfaces)
					{
						if (type2.IsConstructedGenericType && type2.GetGenericTypeDefinition() == genericTypeDefinition)
						{
							type = type2;
							flag = true;
							break;
						}
						genericTypeArgumentFromHierarchy = TypeHelpers.GetGenericTypeArgumentFromHierarchy(type2, genericTypeDefinition, argumentIndex);
						if (genericTypeArgumentFromHierarchy != null)
						{
							return genericTypeArgumentFromHierarchy;
						}
					}
					if (flag)
					{
						goto IL_00EB;
					}
					type = type.BaseType;
					if (type == null || type == typeof(object))
					{
						goto IL_00B7;
					}
				}
				return genericTypeArgumentFromHierarchy;
				IL_00B7:
				return null;
			}
			while (!type.IsConstructedGenericType || type.GetGenericTypeDefinition() != genericTypeDefinition)
			{
				type = type.BaseType;
				if (type == typeof(object))
				{
					return null;
				}
			}
			IL_00EB:
			return type.GenericTypeArguments[argumentIndex];
		}
	}
}
