using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Unity.VisualScripting.FullSerializer.Internal
{
	// Token: 0x020001AE RID: 430
	public static class fsPortableReflection
	{
		// Token: 0x06000B71 RID: 2929 RVA: 0x00030C7A File Offset: 0x0002EE7A
		public static bool HasAttribute<TAttribute>(MemberInfo element)
		{
			return fsPortableReflection.HasAttribute(element, typeof(TAttribute));
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00030C8C File Offset: 0x0002EE8C
		public static bool HasAttribute<TAttribute>(MemberInfo element, bool shouldCache)
		{
			return fsPortableReflection.HasAttribute(element, typeof(TAttribute), shouldCache);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00030C9F File Offset: 0x0002EE9F
		public static bool HasAttribute(MemberInfo element, Type attributeType)
		{
			return fsPortableReflection.HasAttribute(element, attributeType, true);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00030CA9 File Offset: 0x0002EEA9
		public static bool HasAttribute(MemberInfo element, Type attributeType, bool shouldCache)
		{
			return Attribute.IsDefined(element, attributeType, true);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00030CB4 File Offset: 0x0002EEB4
		public static Attribute GetAttribute(MemberInfo element, Type attributeType, bool shouldCache)
		{
			fsPortableReflection.AttributeQuery attributeQuery = new fsPortableReflection.AttributeQuery
			{
				MemberInfo = element,
				AttributeType = attributeType
			};
			Attribute attribute;
			if (!fsPortableReflection._cachedAttributeQueries.TryGetValue(attributeQuery, out attribute))
			{
				Attribute[] array = Attribute.GetCustomAttributes(element, attributeType, true).ToArray<Attribute>();
				if (array.Length != 0)
				{
					attribute = array[0];
				}
				if (shouldCache)
				{
					fsPortableReflection._cachedAttributeQueries[attributeQuery] = attribute;
				}
			}
			return attribute;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00030D10 File Offset: 0x0002EF10
		public static TAttribute GetAttribute<TAttribute>(MemberInfo element, bool shouldCache) where TAttribute : Attribute
		{
			return (TAttribute)((object)fsPortableReflection.GetAttribute(element, typeof(TAttribute), shouldCache));
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00030D28 File Offset: 0x0002EF28
		public static TAttribute GetAttribute<TAttribute>(MemberInfo element) where TAttribute : Attribute
		{
			return fsPortableReflection.GetAttribute<TAttribute>(element, true);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00030D34 File Offset: 0x0002EF34
		public static PropertyInfo GetDeclaredProperty(this Type type, string propertyName)
		{
			PropertyInfo[] declaredProperties = type.GetDeclaredProperties();
			for (int i = 0; i < declaredProperties.Length; i++)
			{
				if (declaredProperties[i].Name == propertyName)
				{
					return declaredProperties[i];
				}
			}
			return null;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00030D6C File Offset: 0x0002EF6C
		public static MethodInfo GetDeclaredMethod(this Type type, string methodName)
		{
			MethodInfo[] declaredMethods = type.GetDeclaredMethods();
			for (int i = 0; i < declaredMethods.Length; i++)
			{
				if (declaredMethods[i].Name == methodName)
				{
					return declaredMethods[i];
				}
			}
			return null;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00030DA4 File Offset: 0x0002EFA4
		public static ConstructorInfo GetDeclaredConstructor(this Type type, Type[] parameters)
		{
			foreach (ConstructorInfo constructorInfo in type.GetDeclaredConstructors())
			{
				ParameterInfo[] parameters2 = constructorInfo.GetParameters();
				if (parameters.Length == parameters2.Length)
				{
					for (int j = 0; j < parameters2.Length; j++)
					{
						parameters2[j].ParameterType != parameters[j];
					}
					return constructorInfo;
				}
			}
			return null;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00030E01 File Offset: 0x0002F001
		public static ConstructorInfo[] GetDeclaredConstructors(this Type type)
		{
			return type.GetConstructors(fsPortableReflection.DeclaredFlags & ~BindingFlags.Static);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00030E14 File Offset: 0x0002F014
		public static MemberInfo[] GetFlattenedMember(this Type type, string memberName)
		{
			List<MemberInfo> list = new List<MemberInfo>();
			while (type != null)
			{
				MemberInfo[] declaredMembers = type.GetDeclaredMembers();
				for (int i = 0; i < declaredMembers.Length; i++)
				{
					if (declaredMembers[i].Name == memberName)
					{
						list.Add(declaredMembers[i]);
					}
				}
				type = type.Resolve().BaseType;
			}
			return list.ToArray();
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00030E74 File Offset: 0x0002F074
		public static MethodInfo GetFlattenedMethod(this Type type, string methodName)
		{
			while (type != null)
			{
				MethodInfo[] declaredMethods = type.GetDeclaredMethods();
				for (int i = 0; i < declaredMethods.Length; i++)
				{
					if (declaredMethods[i].Name == methodName)
					{
						return declaredMethods[i];
					}
				}
				type = type.Resolve().BaseType;
			}
			return null;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00030EC3 File Offset: 0x0002F0C3
		public static IEnumerable<MethodInfo> GetFlattenedMethods(this Type type, string methodName)
		{
			while (type != null)
			{
				MethodInfo[] methods = type.GetDeclaredMethods();
				int num;
				for (int i = 0; i < methods.Length; i = num)
				{
					if (methods[i].Name == methodName)
					{
						yield return methods[i];
					}
					num = i + 1;
				}
				type = type.Resolve().BaseType;
				methods = null;
			}
			yield break;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00030EDC File Offset: 0x0002F0DC
		public static PropertyInfo GetFlattenedProperty(this Type type, string propertyName)
		{
			while (type != null)
			{
				PropertyInfo[] declaredProperties = type.GetDeclaredProperties();
				for (int i = 0; i < declaredProperties.Length; i++)
				{
					if (declaredProperties[i].Name == propertyName)
					{
						return declaredProperties[i];
					}
				}
				type = type.Resolve().BaseType;
			}
			return null;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00030F2C File Offset: 0x0002F12C
		public static MemberInfo GetDeclaredMember(this Type type, string memberName)
		{
			MemberInfo[] declaredMembers = type.GetDeclaredMembers();
			for (int i = 0; i < declaredMembers.Length; i++)
			{
				if (declaredMembers[i].Name == memberName)
				{
					return declaredMembers[i];
				}
			}
			return null;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00030F63 File Offset: 0x0002F163
		public static MethodInfo[] GetDeclaredMethods(this Type type)
		{
			return type.GetMethods(fsPortableReflection.DeclaredFlags);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00030F70 File Offset: 0x0002F170
		public static PropertyInfo[] GetDeclaredProperties(this Type type)
		{
			return type.GetProperties(fsPortableReflection.DeclaredFlags);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00030F7D File Offset: 0x0002F17D
		public static FieldInfo[] GetDeclaredFields(this Type type)
		{
			return type.GetFields(fsPortableReflection.DeclaredFlags);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00030F8A File Offset: 0x0002F18A
		public static MemberInfo[] GetDeclaredMembers(this Type type)
		{
			return type.GetMembers(fsPortableReflection.DeclaredFlags);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00030F97 File Offset: 0x0002F197
		public static MemberInfo AsMemberInfo(Type type)
		{
			return type;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00030F9A File Offset: 0x0002F19A
		public static bool IsType(MemberInfo member)
		{
			return member is Type;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00030FA5 File Offset: 0x0002F1A5
		public static Type AsType(MemberInfo member)
		{
			return (Type)member;
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00030FAD File Offset: 0x0002F1AD
		public static Type Resolve(this Type type)
		{
			return type;
		}

		// Token: 0x040002C8 RID: 712
		public static Type[] EmptyTypes = new Type[0];

		// Token: 0x040002C9 RID: 713
		private static IDictionary<fsPortableReflection.AttributeQuery, Attribute> _cachedAttributeQueries = new Dictionary<fsPortableReflection.AttributeQuery, Attribute>(new fsPortableReflection.AttributeQueryComparator());

		// Token: 0x040002CA RID: 714
		private static BindingFlags DeclaredFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x02000221 RID: 545
		private struct AttributeQuery
		{
			// Token: 0x040009DE RID: 2526
			public MemberInfo MemberInfo;

			// Token: 0x040009DF RID: 2527
			public Type AttributeType;
		}

		// Token: 0x02000222 RID: 546
		private class AttributeQueryComparator : IEqualityComparer<fsPortableReflection.AttributeQuery>
		{
			// Token: 0x06001325 RID: 4901 RVA: 0x00039231 File Offset: 0x00037431
			public bool Equals(fsPortableReflection.AttributeQuery x, fsPortableReflection.AttributeQuery y)
			{
				return x.MemberInfo == y.MemberInfo && x.AttributeType == y.AttributeType;
			}

			// Token: 0x06001326 RID: 4902 RVA: 0x00039259 File Offset: 0x00037459
			public int GetHashCode(fsPortableReflection.AttributeQuery obj)
			{
				return obj.MemberInfo.GetHashCode() + 17 * obj.AttributeType.GetHashCode();
			}
		}
	}
}
