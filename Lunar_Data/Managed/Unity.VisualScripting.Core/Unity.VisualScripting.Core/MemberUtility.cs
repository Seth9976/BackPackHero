using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Unity.VisualScripting
{
	// Token: 0x020000D4 RID: 212
	public static class MemberUtility
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x0000ED16 File Offset: 0x0000CF16
		public static bool IsOperator(this MethodInfo method)
		{
			return method.IsSpecialName && OperatorUtility.operatorNames.ContainsKey(method.Name);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000ED32 File Offset: 0x0000CF32
		public static bool IsUserDefinedConversion(this MethodInfo method)
		{
			return method.IsSpecialName && (method.Name == "op_Implicit" || method.Name == "op_Explicit");
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0000ED64 File Offset: 0x0000CF64
		public static MethodInfo MakeGenericMethodVia(this MethodInfo openConstructedMethod, params Type[] closedConstructedParameterTypes)
		{
			Ensure.That("openConstructedMethod").IsNotNull<MethodInfo>(openConstructedMethod);
			Ensure.That("closedConstructedParameterTypes").IsNotNull<Type[]>(closedConstructedParameterTypes);
			if (!openConstructedMethod.ContainsGenericParameters)
			{
				return openConstructedMethod;
			}
			Type[] array = (from p in openConstructedMethod.GetParameters()
				select p.ParameterType).ToArray<Type>();
			if (array.Length != closedConstructedParameterTypes.Length)
			{
				throw new ArgumentOutOfRangeException("closedConstructedParameterTypes");
			}
			Dictionary<Type, Type> resolvedGenericParameters = new Dictionary<Type, Type>();
			for (int i = 0; i < array.Length; i++)
			{
				Type type = array[i];
				Type type2 = closedConstructedParameterTypes[i];
				type.MakeGenericTypeVia(type2, resolvedGenericParameters, true);
			}
			Type[] array2 = openConstructedMethod.GetGenericArguments().Select(delegate(Type openConstructedGenericArgument)
			{
				if (resolvedGenericParameters.ContainsKey(openConstructedGenericArgument))
				{
					return resolvedGenericParameters[openConstructedGenericArgument];
				}
				return openConstructedGenericArgument;
			}).ToArray<Type>();
			return openConstructedMethod.MakeGenericMethod(array2);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000EE38 File Offset: 0x0000D038
		public static bool IsGenericExtension(this MethodInfo methodInfo)
		{
			return MemberUtility.GenericExtensionMethods.Value.Contains(methodInfo);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000EE4A File Offset: 0x0000D04A
		private static IEnumerable<MethodInfo> GetInheritedExtensionMethods(Type thisArgumentType)
		{
			MethodInfo[] cache = MemberUtility.ExtensionMethodsCache.Value.Cache;
			foreach (MethodInfo methodInfo in cache)
			{
				if (methodInfo.GetParameters()[0].ParameterType.CanMakeGenericTypeVia(thisArgumentType))
				{
					if (methodInfo.ContainsGenericParameters)
					{
						IEnumerable<Type> enumerable = thisArgumentType.Yield<Type>().Concat(from p in methodInfo.GetParametersWithoutThis()
							select p.ParameterType);
						MethodInfo methodInfo2 = methodInfo.MakeGenericMethodVia(enumerable.ToArray<Type>());
						MemberUtility.GenericExtensionMethods.Value.Add(methodInfo2);
						yield return methodInfo2;
					}
					else
					{
						yield return methodInfo;
					}
				}
			}
			MethodInfo[] array = null;
			yield break;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0000EE5C File Offset: 0x0000D05C
		public static IEnumerable<MethodInfo> GetExtensionMethods(this Type thisArgumentType, bool inherited = true)
		{
			if (inherited)
			{
				Lazy<Dictionary<Type, MethodInfo[]>> inheritedExtensionMethodsCache = MemberUtility.InheritedExtensionMethodsCache;
				lock (inheritedExtensionMethodsCache)
				{
					MethodInfo[] array;
					if (!MemberUtility.InheritedExtensionMethodsCache.Value.TryGetValue(thisArgumentType, out array))
					{
						array = MemberUtility.GetInheritedExtensionMethods(thisArgumentType).ToArray<MethodInfo>();
						MemberUtility.InheritedExtensionMethodsCache.Value.Add(thisArgumentType, array);
					}
					return array;
				}
			}
			return MemberUtility.ExtensionMethodsCache.Value.Cache.Where((MethodInfo method) => method.GetParameters()[0].ParameterType == thisArgumentType);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0000EF0C File Offset: 0x0000D10C
		public static bool IsExtension(this MethodInfo methodInfo)
		{
			return methodInfo.HasAttribute(false);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0000EF18 File Offset: 0x0000D118
		public static bool IsExtensionMethod(this MemberInfo memberInfo)
		{
			MethodInfo methodInfo = memberInfo as MethodInfo;
			return methodInfo != null && methodInfo.IsExtension();
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0000EF37 File Offset: 0x0000D137
		public static Delegate CreateDelegate(this MethodInfo methodInfo, Type delegateType)
		{
			return Delegate.CreateDelegate(delegateType, methodInfo);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0000EF40 File Offset: 0x0000D140
		public static bool IsAccessor(this MemberInfo memberInfo)
		{
			return memberInfo is FieldInfo || memberInfo is PropertyInfo;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0000EF55 File Offset: 0x0000D155
		public static Type GetAccessorType(this MemberInfo memberInfo)
		{
			if (memberInfo is FieldInfo)
			{
				return ((FieldInfo)memberInfo).FieldType;
			}
			if (memberInfo is PropertyInfo)
			{
				return ((PropertyInfo)memberInfo).PropertyType;
			}
			return null;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0000EF80 File Offset: 0x0000D180
		public static bool IsPubliclyGettable(this MemberInfo memberInfo)
		{
			if (memberInfo is FieldInfo)
			{
				return ((FieldInfo)memberInfo).IsPublic;
			}
			if (memberInfo is PropertyInfo)
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				return propertyInfo.CanRead && propertyInfo.GetGetMethod(false) != null;
			}
			if (memberInfo is MethodInfo)
			{
				return ((MethodInfo)memberInfo).IsPublic;
			}
			if (memberInfo is ConstructorInfo)
			{
				return ((ConstructorInfo)memberInfo).IsPublic;
			}
			throw new NotSupportedException();
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000EFF8 File Offset: 0x0000D1F8
		private static Type ExtendedDeclaringType(this MemberInfo memberInfo)
		{
			MethodInfo methodInfo = memberInfo as MethodInfo;
			if (methodInfo != null && methodInfo.IsExtension())
			{
				return methodInfo.GetParameters()[0].ParameterType;
			}
			return memberInfo.DeclaringType;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0000F02B File Offset: 0x0000D22B
		public static Type ExtendedDeclaringType(this MemberInfo memberInfo, bool invokeAsExtension)
		{
			if (invokeAsExtension)
			{
				return memberInfo.ExtendedDeclaringType();
			}
			return memberInfo.DeclaringType;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0000F03D File Offset: 0x0000D23D
		public static bool IsStatic(this PropertyInfo propertyInfo)
		{
			MethodInfo getMethod = propertyInfo.GetGetMethod(true);
			if (getMethod == null || !getMethod.IsStatic)
			{
				MethodInfo setMethod = propertyInfo.GetSetMethod(true);
				return setMethod != null && setMethod.IsStatic;
			}
			return true;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0000F068 File Offset: 0x0000D268
		public static bool IsStatic(this MemberInfo memberInfo)
		{
			if (memberInfo is FieldInfo)
			{
				return ((FieldInfo)memberInfo).IsStatic;
			}
			if (memberInfo is PropertyInfo)
			{
				return ((PropertyInfo)memberInfo).IsStatic();
			}
			if (memberInfo is MethodBase)
			{
				return ((MethodBase)memberInfo).IsStatic;
			}
			throw new NotSupportedException();
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0000F0B6 File Offset: 0x0000D2B6
		private static IEnumerable<ParameterInfo> GetParametersWithoutThis(this MethodBase methodBase)
		{
			return methodBase.GetParameters().Skip(methodBase.IsExtensionMethod() ? 1 : 0);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0000F0CF File Offset: 0x0000D2CF
		public static bool IsInvokedAsExtension(this MethodBase methodBase, Type targetType)
		{
			return methodBase.IsExtensionMethod() && methodBase.DeclaringType != targetType;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0000F0E7 File Offset: 0x0000D2E7
		public static IEnumerable<ParameterInfo> GetInvocationParameters(this MethodBase methodBase, bool invokeAsExtension)
		{
			if (invokeAsExtension)
			{
				return methodBase.GetParametersWithoutThis();
			}
			return methodBase.GetParameters();
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0000F0F9 File Offset: 0x0000D2F9
		public static IEnumerable<ParameterInfo> GetInvocationParameters(this MethodBase methodBase, Type targetType)
		{
			return methodBase.GetInvocationParameters(methodBase.IsInvokedAsExtension(targetType));
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0000F108 File Offset: 0x0000D308
		public static Type UnderlyingParameterType(this ParameterInfo parameterInfo)
		{
			if (parameterInfo.ParameterType.IsByRef)
			{
				return parameterInfo.ParameterType.GetElementType();
			}
			return parameterInfo.ParameterType;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0000F129 File Offset: 0x0000D329
		public static bool HasDefaultValue(this ParameterInfo parameterInfo)
		{
			return (parameterInfo.Attributes & ParameterAttributes.HasDefault) == ParameterAttributes.HasDefault;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0000F140 File Offset: 0x0000D340
		public static object DefaultValue(this ParameterInfo parameterInfo)
		{
			if (parameterInfo.HasDefaultValue())
			{
				object obj = parameterInfo.DefaultValue;
				if (obj == null && parameterInfo.ParameterType.IsValueType)
				{
					obj = parameterInfo.ParameterType.Default();
				}
				return obj;
			}
			return parameterInfo.UnderlyingParameterType().Default();
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0000F188 File Offset: 0x0000D388
		public static object PseudoDefaultValue(this ParameterInfo parameterInfo)
		{
			if (parameterInfo.HasDefaultValue())
			{
				object obj = parameterInfo.DefaultValue;
				if (obj == null && parameterInfo.ParameterType.IsValueType)
				{
					obj = parameterInfo.ParameterType.PseudoDefault();
				}
				return obj;
			}
			return parameterInfo.UnderlyingParameterType().PseudoDefault();
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0000F1D0 File Offset: 0x0000D3D0
		public static bool AllowsNull(this ParameterInfo parameterInfo)
		{
			Type parameterType = parameterInfo.ParameterType;
			return (parameterType.IsReferenceType() && parameterInfo.HasAttribute(true)) || Nullable.GetUnderlyingType(parameterType) != null;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0000F203 File Offset: 0x0000D403
		public static bool HasOutModifier(this ParameterInfo parameterInfo)
		{
			Ensure.That("parameterInfo").IsNotNull<ParameterInfo>(parameterInfo);
			return parameterInfo.IsOut && parameterInfo.ParameterType.IsByRef;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0000F22A File Offset: 0x0000D42A
		public static bool CanWrite(this FieldInfo fieldInfo)
		{
			return !fieldInfo.IsInitOnly && !fieldInfo.IsLiteral;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000F23F File Offset: 0x0000D43F
		public static Member ToManipulator(this MemberInfo memberInfo)
		{
			return memberInfo.ToManipulator(memberInfo.DeclaringType);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0000F250 File Offset: 0x0000D450
		public static Member ToManipulator(this MemberInfo memberInfo, Type targetType)
		{
			FieldInfo fieldInfo = memberInfo as FieldInfo;
			if (fieldInfo != null)
			{
				return fieldInfo.ToManipulator(targetType);
			}
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (propertyInfo != null)
			{
				return propertyInfo.ToManipulator(targetType);
			}
			MethodInfo methodInfo = memberInfo as MethodInfo;
			if (methodInfo != null)
			{
				return methodInfo.ToManipulator(targetType);
			}
			ConstructorInfo constructorInfo = memberInfo as ConstructorInfo;
			if (constructorInfo != null)
			{
				return constructorInfo.ToManipulator(targetType);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0000F2AA File Offset: 0x0000D4AA
		public static Member ToManipulator(this FieldInfo fieldInfo, Type targetType)
		{
			return new Member(targetType, fieldInfo);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0000F2B3 File Offset: 0x0000D4B3
		public static Member ToManipulator(this PropertyInfo propertyInfo, Type targetType)
		{
			return new Member(targetType, propertyInfo);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0000F2BC File Offset: 0x0000D4BC
		public static Member ToManipulator(this MethodInfo methodInfo, Type targetType)
		{
			return new Member(targetType, methodInfo);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0000F2C5 File Offset: 0x0000D4C5
		public static Member ToManipulator(this ConstructorInfo constructorInfo, Type targetType)
		{
			return new Member(targetType, constructorInfo);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0000F2D0 File Offset: 0x0000D4D0
		public static ConstructorInfo GetConstructorAccepting(this Type type, Type[] paramTypes, bool nonPublic)
		{
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
			if (nonPublic)
			{
				bindingFlags |= BindingFlags.NonPublic;
			}
			return type.GetConstructors(bindingFlags).FirstOrDefault(delegate(ConstructorInfo constructor)
			{
				ParameterInfo[] parameters = constructor.GetParameters();
				if (parameters.Length != paramTypes.Length)
				{
					return false;
				}
				for (int i = 0; i < parameters.Length; i++)
				{
					if (paramTypes[i] == null)
					{
						if (!parameters[i].ParameterType.IsNullable())
						{
							return false;
						}
					}
					else if (!parameters[i].ParameterType.IsAssignableFrom(paramTypes[i]))
					{
						return false;
					}
				}
				return true;
			});
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0000F30D File Offset: 0x0000D50D
		public static ConstructorInfo GetConstructorAccepting(this Type type, params Type[] paramTypes)
		{
			return type.GetConstructorAccepting(paramTypes, true);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0000F317 File Offset: 0x0000D517
		public static ConstructorInfo GetPublicConstructorAccepting(this Type type, params Type[] paramTypes)
		{
			return type.GetConstructorAccepting(paramTypes, false);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0000F321 File Offset: 0x0000D521
		public static ConstructorInfo GetDefaultConstructor(this Type type)
		{
			return type.GetConstructorAccepting(Array.Empty<Type>());
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0000F32E File Offset: 0x0000D52E
		public static ConstructorInfo GetPublicDefaultConstructor(this Type type)
		{
			return type.GetPublicConstructorAccepting(Array.Empty<Type>());
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0000F33C File Offset: 0x0000D53C
		public static MemberInfo[] GetExtendedMember(this Type type, string name, MemberTypes types, BindingFlags flags)
		{
			List<MemberInfo> list = type.GetMember(name, types, flags).ToList<MemberInfo>();
			if (types.HasFlag(MemberTypes.Method))
			{
				list.AddRange((from extension in type.GetExtensionMethods(true)
					where extension.Name == name
					select extension).Cast<MemberInfo>());
			}
			return list.ToArray();
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0000F3A8 File Offset: 0x0000D5A8
		public static MemberInfo[] GetExtendedMembers(this Type type, BindingFlags flags)
		{
			HashSet<MemberInfo> hashSet = type.GetMembers(flags).ToHashSet<MemberInfo>();
			foreach (MethodInfo methodInfo in type.GetExtensionMethods(true))
			{
				hashSet.Add(methodInfo);
			}
			return hashSet.ToArray<MemberInfo>();
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0000F40C File Offset: 0x0000D60C
		private static bool NameMatches(this MemberInfo member, string name)
		{
			return member.Name == name;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0000F41C File Offset: 0x0000D61C
		private static bool ParametersMatch(this MethodBase methodBase, IEnumerable<Type> parameterTypes, bool invokeAsExtension)
		{
			Ensure.That("parameterTypes").IsNotNull<IEnumerable<Type>>(parameterTypes);
			return (from paramInfo in methodBase.GetInvocationParameters(invokeAsExtension)
				select paramInfo.ParameterType).SequenceEqual(parameterTypes);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000F46A File Offset: 0x0000D66A
		private static bool GenericArgumentsMatch(this MethodInfo method, IEnumerable<Type> genericArgumentTypes)
		{
			Ensure.That("genericArgumentTypes").IsNotNull<IEnumerable<Type>>(genericArgumentTypes);
			return !method.ContainsGenericParameters && method.GetGenericArguments().SequenceEqual(genericArgumentTypes);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0000F492 File Offset: 0x0000D692
		public static bool SignatureMatches(this FieldInfo field, string name)
		{
			return field.NameMatches(name);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000F49B File Offset: 0x0000D69B
		public static bool SignatureMatches(this PropertyInfo property, string name)
		{
			return property.NameMatches(name);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000F4A4 File Offset: 0x0000D6A4
		public static bool SignatureMatches(this ConstructorInfo constructor, string name, IEnumerable<Type> parameterTypes)
		{
			return constructor.NameMatches(name) && constructor.ParametersMatch(parameterTypes, false);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0000F4B9 File Offset: 0x0000D6B9
		public static bool SignatureMatches(this MethodInfo method, string name, IEnumerable<Type> parameterTypes, bool invokeAsExtension)
		{
			return method.NameMatches(name) && method.ParametersMatch(parameterTypes, invokeAsExtension) && !method.ContainsGenericParameters;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0000F4D9 File Offset: 0x0000D6D9
		public static bool SignatureMatches(this MethodInfo method, string name, IEnumerable<Type> parameterTypes, IEnumerable<Type> genericArgumentTypes, bool invokeAsExtension)
		{
			return method.NameMatches(name) && method.ParametersMatch(parameterTypes, invokeAsExtension) && method.GenericArgumentsMatch(genericArgumentTypes);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0000F4F8 File Offset: 0x0000D6F8
		public static FieldInfo GetFieldUnambiguous(this Type type, string name, BindingFlags flags)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			Ensure.That("name").IsNotNull(name);
			flags |= BindingFlags.DeclaredOnly;
			while (type != null)
			{
				FieldInfo field = type.GetField(name, flags);
				if (field != null)
				{
					return field;
				}
				type = type.BaseType;
			}
			return null;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000F554 File Offset: 0x0000D754
		public static PropertyInfo GetPropertyUnambiguous(this Type type, string name, BindingFlags flags)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			Ensure.That("name").IsNotNull(name);
			flags |= BindingFlags.DeclaredOnly;
			while (type != null)
			{
				PropertyInfo property = type.GetProperty(name, flags);
				if (property != null)
				{
					return property;
				}
				type = type.BaseType;
			}
			return null;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0000F5B0 File Offset: 0x0000D7B0
		public static MethodInfo GetMethodUnambiguous(this Type type, string name, BindingFlags flags)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			Ensure.That("name").IsNotNull(name);
			flags |= BindingFlags.DeclaredOnly;
			while (type != null)
			{
				MethodInfo method = type.GetMethod(name, flags);
				if (method != null)
				{
					return method;
				}
				type = type.BaseType;
			}
			return null;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000F60C File Offset: 0x0000D80C
		private static TMemberInfo DisambiguateHierarchy<TMemberInfo>(this IEnumerable<TMemberInfo> members, Type type) where TMemberInfo : MemberInfo
		{
			while (type != null)
			{
				foreach (TMemberInfo tmemberInfo in members)
				{
					MethodInfo methodInfo = tmemberInfo as MethodInfo;
					bool flag = methodInfo != null && methodInfo.IsInvokedAsExtension(type);
					if (tmemberInfo.ExtendedDeclaringType(flag) == type)
					{
						return tmemberInfo;
					}
				}
				type = type.BaseType;
			}
			return default(TMemberInfo);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0000F6A8 File Offset: 0x0000D8A8
		public static FieldInfo Disambiguate(this IEnumerable<FieldInfo> fields, Type type)
		{
			Ensure.That("fields").IsNotNull<IEnumerable<FieldInfo>>(fields);
			Ensure.That("type").IsNotNull<Type>(type);
			return fields.DisambiguateHierarchy(type);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0000F6D1 File Offset: 0x0000D8D1
		public static PropertyInfo Disambiguate(this IEnumerable<PropertyInfo> properties, Type type)
		{
			Ensure.That("properties").IsNotNull<IEnumerable<PropertyInfo>>(properties);
			Ensure.That("type").IsNotNull<Type>(type);
			return properties.DisambiguateHierarchy(type);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0000F6FC File Offset: 0x0000D8FC
		public static ConstructorInfo Disambiguate(this IEnumerable<ConstructorInfo> constructors, Type type, IEnumerable<Type> parameterTypes)
		{
			Ensure.That("constructors").IsNotNull<IEnumerable<ConstructorInfo>>(constructors);
			Ensure.That("type").IsNotNull<Type>(type);
			Ensure.That("parameterTypes").IsNotNull<IEnumerable<Type>>(parameterTypes);
			return constructors.Where((ConstructorInfo m) => m.ParametersMatch(parameterTypes, false) && !m.ContainsGenericParameters).DisambiguateHierarchy(type);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000F764 File Offset: 0x0000D964
		public static MethodInfo Disambiguate(this IEnumerable<MethodInfo> methods, Type type, IEnumerable<Type> parameterTypes)
		{
			Ensure.That("methods").IsNotNull<IEnumerable<MethodInfo>>(methods);
			Ensure.That("type").IsNotNull<Type>(type);
			Ensure.That("parameterTypes").IsNotNull<IEnumerable<Type>>(parameterTypes);
			return methods.Where((MethodInfo m) => m.ParametersMatch(parameterTypes, m.IsInvokedAsExtension(type)) && !m.ContainsGenericParameters).DisambiguateHierarchy(type);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0000F7DC File Offset: 0x0000D9DC
		public static MethodInfo Disambiguate(this IEnumerable<MethodInfo> methods, Type type, IEnumerable<Type> parameterTypes, IEnumerable<Type> genericArgumentTypes)
		{
			Ensure.That("methods").IsNotNull<IEnumerable<MethodInfo>>(methods);
			Ensure.That("type").IsNotNull<Type>(type);
			Ensure.That("parameterTypes").IsNotNull<IEnumerable<Type>>(parameterTypes);
			Ensure.That("genericArgumentTypes").IsNotNull<IEnumerable<Type>>(genericArgumentTypes);
			return methods.Where((MethodInfo m) => m.ParametersMatch(parameterTypes, m.IsInvokedAsExtension(type)) && m.GenericArgumentsMatch(genericArgumentTypes)).DisambiguateHierarchy(type);
		}

		// Token: 0x0400014F RID: 335
		private static readonly Lazy<ExtensionMethodCache> ExtensionMethodsCache = new Lazy<ExtensionMethodCache>(() => new ExtensionMethodCache(), true);

		// Token: 0x04000150 RID: 336
		private static readonly Lazy<Dictionary<Type, MethodInfo[]>> InheritedExtensionMethodsCache = new Lazy<Dictionary<Type, MethodInfo[]>>(() => new Dictionary<Type, MethodInfo[]>(), true);

		// Token: 0x04000151 RID: 337
		private static readonly Lazy<HashSet<MethodInfo>> GenericExtensionMethods = new Lazy<HashSet<MethodInfo>>(() => new HashSet<MethodInfo>(), true);
	}
}
