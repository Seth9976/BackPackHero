using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000CC RID: 204
	public static class ConversionUtility
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x0000B1A9 File Offset: 0x000093A9
		private static bool RespectsIdentity(Type source, Type destination)
		{
			return source == destination;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000B1B2 File Offset: 0x000093B2
		private static bool IsUpcast(Type source, Type destination)
		{
			return destination.IsAssignableFrom(source);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000B1BB File Offset: 0x000093BB
		private static bool IsDowncast(Type source, Type destination)
		{
			return source.IsAssignableFrom(destination);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000B1C4 File Offset: 0x000093C4
		private static bool ExpectsString(Type source, Type destination)
		{
			return destination == typeof(string);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000B1D6 File Offset: 0x000093D6
		public static bool HasImplicitNumericConversion(Type source, Type destination)
		{
			return ConversionUtility.implicitNumericConversions.ContainsKey(source) && ConversionUtility.implicitNumericConversions[source].Contains(destination);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000B1F8 File Offset: 0x000093F8
		public static bool HasExplicitNumericConversion(Type source, Type destination)
		{
			return ConversionUtility.explicitNumericConversions.ContainsKey(source) && ConversionUtility.explicitNumericConversions[source].Contains(destination);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000B21A File Offset: 0x0000941A
		public static bool HasNumericConversion(Type source, Type destination)
		{
			return ConversionUtility.HasImplicitNumericConversion(source, destination) || ConversionUtility.HasExplicitNumericConversion(source, destination);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000B230 File Offset: 0x00009430
		private static IEnumerable<MethodInfo> FindUserDefinedConversionMethods(ConversionUtility.ConversionQuery query)
		{
			Type source = query.source;
			Type destination = query.destination;
			IEnumerable<MethodInfo> enumerable = from m in source.GetMethods(BindingFlags.Static | BindingFlags.Public)
				where m.IsUserDefinedConversion()
				select m;
			IEnumerable<MethodInfo> enumerable2 = from m in destination.GetMethods(BindingFlags.Static | BindingFlags.Public)
				where m.IsUserDefinedConversion()
				select m;
			return from m in enumerable.Concat(enumerable2)
				where m.GetParameters()[0].ParameterType.IsAssignableFrom(source) || source.IsAssignableFrom(m.GetParameters()[0].ParameterType)
				select m;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000B2CC File Offset: 0x000094CC
		private static MethodInfo[] GetUserDefinedConversionMethods(Type source, Type destination)
		{
			ConversionUtility.ConversionQuery conversionQuery = new ConversionUtility.ConversionQuery(source, destination);
			if (!ConversionUtility.userConversionMethodsCache.ContainsKey(conversionQuery))
			{
				ConversionUtility.userConversionMethodsCache.Add(conversionQuery, ConversionUtility.FindUserDefinedConversionMethods(conversionQuery).ToArray<MethodInfo>());
			}
			return ConversionUtility.userConversionMethodsCache[conversionQuery];
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000B310 File Offset: 0x00009510
		private static ConversionUtility.ConversionType GetUserDefinedConversionType(Type source, Type destination)
		{
			MethodInfo[] userDefinedConversionMethods = ConversionUtility.GetUserDefinedConversionMethods(source, destination);
			MethodInfo methodInfo = userDefinedConversionMethods.FirstOrDefault((MethodInfo m) => m.ReturnType == destination);
			if (methodInfo != null)
			{
				if (methodInfo.Name == "op_Implicit")
				{
					return ConversionUtility.ConversionType.UserDefinedImplicit;
				}
				if (methodInfo.Name == "op_Explicit")
				{
					return ConversionUtility.ConversionType.UserDefinedExplicit;
				}
			}
			else if (destination.IsPrimitive && destination != typeof(IntPtr) && destination != typeof(UIntPtr))
			{
				methodInfo = userDefinedConversionMethods.FirstOrDefault((MethodInfo m) => ConversionUtility.HasImplicitNumericConversion(m.ReturnType, destination));
				if (methodInfo != null)
				{
					if (methodInfo.Name == "op_Implicit")
					{
						return ConversionUtility.ConversionType.UserDefinedThenNumericImplicit;
					}
					if (methodInfo.Name == "op_Explicit")
					{
						return ConversionUtility.ConversionType.UserDefinedThenNumericExplicit;
					}
				}
				else
				{
					methodInfo = userDefinedConversionMethods.FirstOrDefault((MethodInfo m) => ConversionUtility.HasExplicitNumericConversion(m.ReturnType, destination));
					if (methodInfo != null)
					{
						return ConversionUtility.ConversionType.UserDefinedThenNumericExplicit;
					}
				}
			}
			return ConversionUtility.ConversionType.Impossible;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000B421 File Offset: 0x00009621
		private static bool HasEnumerableToArrayConversion(Type source, Type destination)
		{
			return source != typeof(string) && typeof(IEnumerable).IsAssignableFrom(source) && destination.IsArray && destination.GetArrayRank() == 1;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000B45C File Offset: 0x0000965C
		private static bool HasEnumerableToListConversion(Type source, Type destination)
		{
			return source != typeof(string) && typeof(IEnumerable).IsAssignableFrom(source) && destination.IsGenericType && destination.GetGenericTypeDefinition() == typeof(List<>);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000B4AC File Offset: 0x000096AC
		private static bool HasUnityHierarchyConversion(Type source, Type destination)
		{
			if (destination == typeof(GameObject))
			{
				return typeof(Component).IsAssignableFrom(source);
			}
			return (typeof(Component).IsAssignableFrom(destination) || destination.IsInterface) && (source == typeof(GameObject) || typeof(Component).IsAssignableFrom(source));
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000B51C File Offset: 0x0000971C
		private static bool IsValidConversion(ConversionUtility.ConversionType conversionType, bool guaranteed)
		{
			return conversionType != ConversionUtility.ConversionType.Impossible && (!guaranteed || conversionType != ConversionUtility.ConversionType.Downcast);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000B52D File Offset: 0x0000972D
		public static bool CanConvert(object value, Type type, bool guaranteed)
		{
			return ConversionUtility.IsValidConversion(ConversionUtility.GetRequiredConversion(value, type), guaranteed);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000B53C File Offset: 0x0000973C
		public static bool CanConvert(Type source, Type destination, bool guaranteed)
		{
			return ConversionUtility.IsValidConversion(ConversionUtility.GetRequiredConversion(source, destination), guaranteed);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000B54B File Offset: 0x0000974B
		public static object Convert(object value, Type type)
		{
			return ConversionUtility.Convert(value, type, ConversionUtility.GetRequiredConversion(value, type));
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000B55B File Offset: 0x0000975B
		public static T Convert<T>(object value)
		{
			return (T)((object)ConversionUtility.Convert(value, typeof(T)));
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000B574 File Offset: 0x00009774
		public static bool TryConvert(object value, Type type, out object result, bool guaranteed)
		{
			ConversionUtility.ConversionType requiredConversion = ConversionUtility.GetRequiredConversion(value, type);
			if (ConversionUtility.IsValidConversion(requiredConversion, guaranteed))
			{
				result = ConversionUtility.Convert(value, type, requiredConversion);
				return true;
			}
			result = value;
			return false;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000B5A4 File Offset: 0x000097A4
		public static bool TryConvert<T>(object value, out T result, bool guaranteed)
		{
			object obj;
			if (ConversionUtility.TryConvert(value, typeof(T), out obj, guaranteed))
			{
				result = (T)((object)obj);
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000B5DC File Offset: 0x000097DC
		public static bool IsConvertibleTo(this Type source, Type destination, bool guaranteed)
		{
			return ConversionUtility.CanConvert(source, destination, guaranteed);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000B5E6 File Offset: 0x000097E6
		public static bool IsConvertibleTo(this object source, Type type, bool guaranteed)
		{
			return ConversionUtility.CanConvert(source, type, guaranteed);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000B5F0 File Offset: 0x000097F0
		public static bool IsConvertibleTo<T>(this object source, bool guaranteed)
		{
			return ConversionUtility.CanConvert(source, typeof(T), guaranteed);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000B603 File Offset: 0x00009803
		public static object ConvertTo(this object source, Type type)
		{
			return ConversionUtility.Convert(source, type);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000B60C File Offset: 0x0000980C
		public static T ConvertTo<T>(this object source)
		{
			return (T)((object)ConversionUtility.Convert(source, typeof(T)));
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000B624 File Offset: 0x00009824
		public static ConversionUtility.ConversionType GetRequiredConversion(Type source, Type destination)
		{
			ConversionUtility.ConversionQuery conversionQuery = new ConversionUtility.ConversionQuery(source, destination);
			ConversionUtility.ConversionType conversionType;
			if (!ConversionUtility.conversionTypesCache.TryGetValue(conversionQuery, out conversionType))
			{
				conversionType = ConversionUtility.DetermineConversionType(conversionQuery);
				ConversionUtility.conversionTypesCache.Add(conversionQuery, conversionType);
			}
			return conversionType;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000B660 File Offset: 0x00009860
		private static ConversionUtility.ConversionType DetermineConversionType(ConversionUtility.ConversionQuery query)
		{
			Type source = query.source;
			Type destination = query.destination;
			if (source == null)
			{
				if (destination.IsNullable())
				{
					return ConversionUtility.ConversionType.Identity;
				}
				return ConversionUtility.ConversionType.Impossible;
			}
			else
			{
				Ensure.That("destination").IsNotNull<Type>(destination);
				if (ConversionUtility.RespectsIdentity(source, destination))
				{
					return ConversionUtility.ConversionType.Identity;
				}
				if (ConversionUtility.IsUpcast(source, destination))
				{
					return ConversionUtility.ConversionType.Upcast;
				}
				if (ConversionUtility.IsDowncast(source, destination))
				{
					return ConversionUtility.ConversionType.Downcast;
				}
				if (ConversionUtility.HasImplicitNumericConversion(source, destination))
				{
					return ConversionUtility.ConversionType.NumericImplicit;
				}
				if (ConversionUtility.HasExplicitNumericConversion(source, destination))
				{
					return ConversionUtility.ConversionType.NumericExplicit;
				}
				if (ConversionUtility.HasUnityHierarchyConversion(source, destination))
				{
					return ConversionUtility.ConversionType.UnityHierarchy;
				}
				if (ConversionUtility.HasEnumerableToArrayConversion(source, destination))
				{
					return ConversionUtility.ConversionType.EnumerableToArray;
				}
				if (ConversionUtility.HasEnumerableToListConversion(source, destination))
				{
					return ConversionUtility.ConversionType.EnumerableToList;
				}
				ConversionUtility.ConversionType userDefinedConversionType = ConversionUtility.GetUserDefinedConversionType(source, destination);
				if (userDefinedConversionType != ConversionUtility.ConversionType.Impossible)
				{
					return userDefinedConversionType;
				}
				return ConversionUtility.ConversionType.Impossible;
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000B709 File Offset: 0x00009909
		public static ConversionUtility.ConversionType GetRequiredConversion(object value, Type type)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			return ConversionUtility.GetRequiredConversion((value != null) ? value.GetType() : null, type);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000B72D File Offset: 0x0000992D
		private static object NumericConversion(object value, Type type)
		{
			return global::System.Convert.ChangeType(value, type);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000B738 File Offset: 0x00009938
		private static object UserDefinedConversion(ConversionUtility.ConversionType conversion, object value, Type type)
		{
			MethodInfo[] userDefinedConversionMethods = ConversionUtility.GetUserDefinedConversionMethods(value.GetType(), type);
			bool flag = conversion == ConversionUtility.ConversionType.UserDefinedThenNumericImplicit || conversion == ConversionUtility.ConversionType.UserDefinedThenNumericExplicit;
			MethodInfo methodInfo = null;
			if (flag)
			{
				foreach (MethodInfo methodInfo2 in userDefinedConversionMethods)
				{
					if (ConversionUtility.HasNumericConversion(methodInfo2.ReturnType, type))
					{
						methodInfo = methodInfo2;
						break;
					}
				}
			}
			else
			{
				foreach (MethodInfo methodInfo3 in userDefinedConversionMethods)
				{
					if (methodInfo3.ReturnType == type)
					{
						methodInfo = methodInfo3;
						break;
					}
				}
			}
			object obj = methodInfo.InvokeOptimized(null, value);
			if (flag)
			{
				obj = ConversionUtility.NumericConversion(obj, type);
			}
			return obj;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000B7E0 File Offset: 0x000099E0
		private static object EnumerableToArrayConversion(object value, Type arrayType)
		{
			Type elementType = arrayType.GetElementType();
			object[] array = ((IEnumerable)value).Cast<object>().Where(new Func<object, bool>(elementType.IsAssignableFrom)).ToArray<object>();
			Array array2 = Array.CreateInstance(elementType, array.Length);
			array.CopyTo(array2, 0);
			return array2;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000B82C File Offset: 0x00009A2C
		private static object EnumerableToListConversion(object value, Type listType)
		{
			Type type = listType.GetGenericArguments()[0];
			object[] array = ((IEnumerable)value).Cast<object>().Where(new Func<object, bool>(type.IsAssignableFrom)).ToArray<object>();
			IList list = (IList)Activator.CreateInstance(listType);
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(array[i]);
			}
			return list;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000B88C File Offset: 0x00009A8C
		private static object UnityHierarchyConversion(object value, Type type)
		{
			if (value.IsUnityNull())
			{
				return null;
			}
			if (type == typeof(GameObject) && value is Component)
			{
				return ((Component)value).gameObject;
			}
			if (typeof(Component).IsAssignableFrom(type) || type.IsInterface)
			{
				if (value is Component)
				{
					return ((Component)value).GetComponent(type);
				}
				if (value is GameObject)
				{
					return ((GameObject)value).GetComponent(type);
				}
			}
			throw new InvalidConversionException();
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000B914 File Offset: 0x00009B14
		private static object Convert(object value, Type type, ConversionUtility.ConversionType conversionType)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			if (conversionType == ConversionUtility.ConversionType.Impossible)
			{
				throw new InvalidConversionException(string.Format("Cannot convert from '{0}' to '{1}'.", ((value != null) ? value.GetType().ToString() : null) ?? "null", type));
			}
			object obj;
			try
			{
				switch (conversionType)
				{
				case ConversionUtility.ConversionType.Identity:
				case ConversionUtility.ConversionType.Upcast:
				case ConversionUtility.ConversionType.Downcast:
					obj = value;
					break;
				case ConversionUtility.ConversionType.NumericImplicit:
				case ConversionUtility.ConversionType.NumericExplicit:
					obj = ConversionUtility.NumericConversion(value, type);
					break;
				case ConversionUtility.ConversionType.UserDefinedImplicit:
				case ConversionUtility.ConversionType.UserDefinedExplicit:
				case ConversionUtility.ConversionType.UserDefinedThenNumericImplicit:
				case ConversionUtility.ConversionType.UserDefinedThenNumericExplicit:
					obj = ConversionUtility.UserDefinedConversion(conversionType, value, type);
					break;
				case ConversionUtility.ConversionType.UnityHierarchy:
					obj = ConversionUtility.UnityHierarchyConversion(value, type);
					break;
				case ConversionUtility.ConversionType.EnumerableToArray:
					obj = ConversionUtility.EnumerableToArrayConversion(value, type);
					break;
				case ConversionUtility.ConversionType.EnumerableToList:
					obj = ConversionUtility.EnumerableToListConversion(value, type);
					break;
				case ConversionUtility.ConversionType.ToString:
					obj = value.ToString();
					break;
				default:
					throw new UnexpectedEnumValueException<ConversionUtility.ConversionType>(conversionType);
				}
			}
			catch (Exception ex)
			{
				throw new InvalidConversionException(string.Format("Failed to convert from '{0}' to '{1}' via {2}.", ((value != null) ? value.GetType().ToString() : null) ?? "null", type, conversionType), ex);
			}
			return obj;
		}

		// Token: 0x04000121 RID: 289
		private const BindingFlags UserDefinedBindingFlags = BindingFlags.Static | BindingFlags.Public;

		// Token: 0x04000122 RID: 290
		private static readonly Dictionary<ConversionUtility.ConversionQuery, ConversionUtility.ConversionType> conversionTypesCache = new Dictionary<ConversionUtility.ConversionQuery, ConversionUtility.ConversionType>(default(ConversionUtility.ConversionQueryComparer));

		// Token: 0x04000123 RID: 291
		private static readonly Dictionary<ConversionUtility.ConversionQuery, MethodInfo[]> userConversionMethodsCache = new Dictionary<ConversionUtility.ConversionQuery, MethodInfo[]>(default(ConversionUtility.ConversionQueryComparer));

		// Token: 0x04000124 RID: 292
		private static readonly Dictionary<Type, HashSet<Type>> implicitNumericConversions = new Dictionary<Type, HashSet<Type>>
		{
			{
				typeof(sbyte),
				new HashSet<Type>
				{
					typeof(byte),
					typeof(int),
					typeof(long),
					typeof(float),
					typeof(double),
					typeof(decimal)
				}
			},
			{
				typeof(byte),
				new HashSet<Type>
				{
					typeof(short),
					typeof(ushort),
					typeof(int),
					typeof(uint),
					typeof(long),
					typeof(ulong),
					typeof(float),
					typeof(double),
					typeof(decimal)
				}
			},
			{
				typeof(short),
				new HashSet<Type>
				{
					typeof(int),
					typeof(long),
					typeof(float),
					typeof(double),
					typeof(decimal)
				}
			},
			{
				typeof(ushort),
				new HashSet<Type>
				{
					typeof(int),
					typeof(uint),
					typeof(long),
					typeof(ulong),
					typeof(float),
					typeof(double),
					typeof(decimal)
				}
			},
			{
				typeof(int),
				new HashSet<Type>
				{
					typeof(long),
					typeof(float),
					typeof(double),
					typeof(decimal)
				}
			},
			{
				typeof(uint),
				new HashSet<Type>
				{
					typeof(long),
					typeof(ulong),
					typeof(float),
					typeof(double),
					typeof(decimal)
				}
			},
			{
				typeof(long),
				new HashSet<Type>
				{
					typeof(float),
					typeof(double),
					typeof(decimal)
				}
			},
			{
				typeof(char),
				new HashSet<Type>
				{
					typeof(ushort),
					typeof(int),
					typeof(uint),
					typeof(long),
					typeof(ulong),
					typeof(float),
					typeof(double),
					typeof(decimal)
				}
			},
			{
				typeof(float),
				new HashSet<Type> { typeof(double) }
			},
			{
				typeof(ulong),
				new HashSet<Type>
				{
					typeof(float),
					typeof(double),
					typeof(decimal)
				}
			}
		};

		// Token: 0x04000125 RID: 293
		private static readonly Dictionary<Type, HashSet<Type>> explicitNumericConversions = new Dictionary<Type, HashSet<Type>>
		{
			{
				typeof(sbyte),
				new HashSet<Type>
				{
					typeof(byte),
					typeof(ushort),
					typeof(uint),
					typeof(ulong),
					typeof(char)
				}
			},
			{
				typeof(byte),
				new HashSet<Type>
				{
					typeof(sbyte),
					typeof(char)
				}
			},
			{
				typeof(short),
				new HashSet<Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(ushort),
					typeof(uint),
					typeof(ulong),
					typeof(char)
				}
			},
			{
				typeof(ushort),
				new HashSet<Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(char)
				}
			},
			{
				typeof(int),
				new HashSet<Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(ushort),
					typeof(uint),
					typeof(ulong),
					typeof(char)
				}
			},
			{
				typeof(uint),
				new HashSet<Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(ushort),
					typeof(int),
					typeof(char)
				}
			},
			{
				typeof(long),
				new HashSet<Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(ushort),
					typeof(int),
					typeof(uint),
					typeof(ulong),
					typeof(char)
				}
			},
			{
				typeof(ulong),
				new HashSet<Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(ushort),
					typeof(int),
					typeof(uint),
					typeof(long),
					typeof(char)
				}
			},
			{
				typeof(char),
				new HashSet<Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short)
				}
			},
			{
				typeof(float),
				new HashSet<Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(ushort),
					typeof(int),
					typeof(uint),
					typeof(long),
					typeof(ulong),
					typeof(char),
					typeof(decimal)
				}
			},
			{
				typeof(double),
				new HashSet<Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(ushort),
					typeof(int),
					typeof(uint),
					typeof(long),
					typeof(ulong),
					typeof(char),
					typeof(float),
					typeof(decimal)
				}
			},
			{
				typeof(decimal),
				new HashSet<Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(ushort),
					typeof(int),
					typeof(uint),
					typeof(long),
					typeof(ulong),
					typeof(char),
					typeof(float),
					typeof(double)
				}
			}
		};

		// Token: 0x020001C8 RID: 456
		public enum ConversionType
		{
			// Token: 0x0400030E RID: 782
			Impossible,
			// Token: 0x0400030F RID: 783
			Identity,
			// Token: 0x04000310 RID: 784
			Upcast,
			// Token: 0x04000311 RID: 785
			Downcast,
			// Token: 0x04000312 RID: 786
			NumericImplicit,
			// Token: 0x04000313 RID: 787
			NumericExplicit,
			// Token: 0x04000314 RID: 788
			UserDefinedImplicit,
			// Token: 0x04000315 RID: 789
			UserDefinedExplicit,
			// Token: 0x04000316 RID: 790
			UserDefinedThenNumericImplicit,
			// Token: 0x04000317 RID: 791
			UserDefinedThenNumericExplicit,
			// Token: 0x04000318 RID: 792
			UnityHierarchy,
			// Token: 0x04000319 RID: 793
			EnumerableToArray,
			// Token: 0x0400031A RID: 794
			EnumerableToList,
			// Token: 0x0400031B RID: 795
			ToString
		}

		// Token: 0x020001C9 RID: 457
		private struct ConversionQuery : IEquatable<ConversionUtility.ConversionQuery>
		{
			// Token: 0x06000C0F RID: 3087 RVA: 0x000327E8 File Offset: 0x000309E8
			public ConversionQuery(Type source, Type destination)
			{
				this.source = source;
				this.destination = destination;
			}

			// Token: 0x06000C10 RID: 3088 RVA: 0x000327F8 File Offset: 0x000309F8
			public bool Equals(ConversionUtility.ConversionQuery other)
			{
				return this.source == other.source && this.destination == other.destination;
			}

			// Token: 0x06000C11 RID: 3089 RVA: 0x00032820 File Offset: 0x00030A20
			public override bool Equals(object obj)
			{
				return obj is ConversionUtility.ConversionQuery && this.Equals((ConversionUtility.ConversionQuery)obj);
			}

			// Token: 0x06000C12 RID: 3090 RVA: 0x00032838 File Offset: 0x00030A38
			public override int GetHashCode()
			{
				return HashUtility.GetHashCode<Type, Type>(this.source, this.destination);
			}

			// Token: 0x0400031C RID: 796
			public readonly Type source;

			// Token: 0x0400031D RID: 797
			public readonly Type destination;
		}

		// Token: 0x020001CA RID: 458
		private struct ConversionQueryComparer : IEqualityComparer<ConversionUtility.ConversionQuery>
		{
			// Token: 0x06000C13 RID: 3091 RVA: 0x0003284B File Offset: 0x00030A4B
			public bool Equals(ConversionUtility.ConversionQuery x, ConversionUtility.ConversionQuery y)
			{
				return x.Equals(y);
			}

			// Token: 0x06000C14 RID: 3092 RVA: 0x00032855 File Offset: 0x00030A55
			public int GetHashCode(ConversionUtility.ConversionQuery obj)
			{
				return obj.GetHashCode();
			}
		}
	}
}
