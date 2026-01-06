using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000CB RID: 203
	public static class AttributeUtility
	{
		// Token: 0x060004E0 RID: 1248 RVA: 0x0000AE14 File Offset: 0x00009014
		private static AttributeUtility.AttributeCache GetAttributeCache(MemberInfo element)
		{
			Ensure.That("element").IsNotNull<MemberInfo>(element);
			Dictionary<object, AttributeUtility.AttributeCache> dictionary = AttributeUtility.optimizedCaches;
			AttributeUtility.AttributeCache attributeCache2;
			lock (dictionary)
			{
				AttributeUtility.AttributeCache attributeCache;
				if (!AttributeUtility.optimizedCaches.TryGetValue(element, out attributeCache))
				{
					attributeCache = new AttributeUtility.AttributeCache(element);
					AttributeUtility.optimizedCaches.Add(element, attributeCache);
				}
				attributeCache2 = attributeCache;
			}
			return attributeCache2;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000AE88 File Offset: 0x00009088
		private static AttributeUtility.AttributeCache GetAttributeCache(ParameterInfo element)
		{
			Ensure.That("element").IsNotNull<ParameterInfo>(element);
			Dictionary<object, AttributeUtility.AttributeCache> dictionary = AttributeUtility.optimizedCaches;
			AttributeUtility.AttributeCache attributeCache2;
			lock (dictionary)
			{
				AttributeUtility.AttributeCache attributeCache;
				if (!AttributeUtility.optimizedCaches.TryGetValue(element, out attributeCache))
				{
					attributeCache = new AttributeUtility.AttributeCache(element);
					AttributeUtility.optimizedCaches.Add(element, attributeCache);
				}
				attributeCache2 = attributeCache;
			}
			return attributeCache2;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000AEFC File Offset: 0x000090FC
		private static AttributeUtility.AttributeCache GetAttributeCache(IAttributeProvider element)
		{
			Ensure.That("element").IsNotNull<IAttributeProvider>(element);
			Dictionary<object, AttributeUtility.AttributeCache> dictionary = AttributeUtility.optimizedCaches;
			AttributeUtility.AttributeCache attributeCache2;
			lock (dictionary)
			{
				AttributeUtility.AttributeCache attributeCache;
				if (!AttributeUtility.optimizedCaches.TryGetValue(element, out attributeCache))
				{
					attributeCache = new AttributeUtility.AttributeCache(element);
					AttributeUtility.optimizedCaches.Add(element, attributeCache);
				}
				attributeCache2 = attributeCache;
			}
			return attributeCache2;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000AF70 File Offset: 0x00009170
		public static void CacheAttributes(MemberInfo element)
		{
			AttributeUtility.GetAttributeCache(element);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000AF79 File Offset: 0x00009179
		internal static IEnumerable<T> GetAttributeOfEnumMember<T>(this Enum enumVal) where T : Attribute
		{
			return enumVal.GetType().GetMember(enumVal.ToString())[0].GetCustomAttributes(typeof(T), false).Cast<T>();
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000AFA3 File Offset: 0x000091A3
		public static bool HasAttribute(this MemberInfo element, Type attributeType, bool inherit = true)
		{
			return AttributeUtility.GetAttributeCache(element).HasAttribute(attributeType, inherit);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000AFB2 File Offset: 0x000091B2
		public static Attribute GetAttribute(this MemberInfo element, Type attributeType, bool inherit = true)
		{
			return AttributeUtility.GetAttributeCache(element).GetAttribute(attributeType, inherit);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000AFC1 File Offset: 0x000091C1
		public static IEnumerable<Attribute> GetAttributes(this MemberInfo element, Type attributeType, bool inherit = true)
		{
			return AttributeUtility.GetAttributeCache(element).GetAttributes(attributeType, inherit);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000AFD0 File Offset: 0x000091D0
		public static bool HasAttribute<TAttribute>(this MemberInfo element, bool inherit = true) where TAttribute : Attribute
		{
			return AttributeUtility.GetAttributeCache(element).HasAttribute<TAttribute>(inherit);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000AFDE File Offset: 0x000091DE
		public static TAttribute GetAttribute<TAttribute>(this MemberInfo element, bool inherit = true) where TAttribute : Attribute
		{
			return AttributeUtility.GetAttributeCache(element).GetAttribute<TAttribute>(inherit);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000AFEC File Offset: 0x000091EC
		public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this MemberInfo element, bool inherit = true) where TAttribute : Attribute
		{
			return AttributeUtility.GetAttributeCache(element).GetAttributes<TAttribute>(inherit);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000AFFA File Offset: 0x000091FA
		public static void CacheAttributes(ParameterInfo element)
		{
			AttributeUtility.GetAttributeCache(element);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000B003 File Offset: 0x00009203
		public static bool HasAttribute(this ParameterInfo element, Type attributeType, bool inherit = true)
		{
			return AttributeUtility.GetAttributeCache(element).HasAttribute(attributeType, inherit);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000B012 File Offset: 0x00009212
		public static Attribute GetAttribute(this ParameterInfo element, Type attributeType, bool inherit = true)
		{
			return AttributeUtility.GetAttributeCache(element).GetAttribute(attributeType, inherit);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000B021 File Offset: 0x00009221
		public static IEnumerable<Attribute> GetAttributes(this ParameterInfo element, Type attributeType, bool inherit = true)
		{
			return AttributeUtility.GetAttributeCache(element).GetAttributes(attributeType, inherit);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000B030 File Offset: 0x00009230
		public static bool HasAttribute<TAttribute>(this ParameterInfo element, bool inherit = true) where TAttribute : Attribute
		{
			return AttributeUtility.GetAttributeCache(element).HasAttribute<TAttribute>(inherit);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000B03E File Offset: 0x0000923E
		public static TAttribute GetAttribute<TAttribute>(this ParameterInfo element, bool inherit = true) where TAttribute : Attribute
		{
			return AttributeUtility.GetAttributeCache(element).GetAttribute<TAttribute>(inherit);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000B04C File Offset: 0x0000924C
		public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this ParameterInfo element, bool inherit = true) where TAttribute : Attribute
		{
			return AttributeUtility.GetAttributeCache(element).GetAttributes<TAttribute>(inherit);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000B05A File Offset: 0x0000925A
		public static void CacheAttributes(IAttributeProvider element)
		{
			AttributeUtility.GetAttributeCache(element);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000B063 File Offset: 0x00009263
		public static bool HasAttribute(this IAttributeProvider element, Type attributeType, bool inherit = true)
		{
			return AttributeUtility.GetAttributeCache(element).HasAttribute(attributeType, inherit);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000B072 File Offset: 0x00009272
		public static Attribute GetAttribute(this IAttributeProvider element, Type attributeType, bool inherit = true)
		{
			return AttributeUtility.GetAttributeCache(element).GetAttribute(attributeType, inherit);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000B081 File Offset: 0x00009281
		public static IEnumerable<Attribute> GetAttributes(this IAttributeProvider element, Type attributeType, bool inherit = true)
		{
			return AttributeUtility.GetAttributeCache(element).GetAttributes(attributeType, inherit);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000B090 File Offset: 0x00009290
		public static bool HasAttribute<TAttribute>(this IAttributeProvider element, bool inherit = true) where TAttribute : Attribute
		{
			return AttributeUtility.GetAttributeCache(element).HasAttribute<TAttribute>(inherit);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000B09E File Offset: 0x0000929E
		public static TAttribute GetAttribute<TAttribute>(this IAttributeProvider element, bool inherit = true) where TAttribute : Attribute
		{
			return AttributeUtility.GetAttributeCache(element).GetAttribute<TAttribute>(inherit);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000B0AC File Offset: 0x000092AC
		public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this IAttributeProvider element, bool inherit = true) where TAttribute : Attribute
		{
			return AttributeUtility.GetAttributeCache(element).GetAttributes<TAttribute>(inherit);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000B0BC File Offset: 0x000092BC
		public static bool CheckCondition(Type type, object target, string conditionMemberName, bool fallback)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			bool flag;
			try
			{
				if (target != null && !type.IsInstanceOfType(target))
				{
					throw new ArgumentException("Target is not an instance of type.", "target");
				}
				if (conditionMemberName == null)
				{
					flag = fallback;
				}
				else
				{
					MemberInfo memberInfo = type.GetMember(conditionMemberName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault<MemberInfo>();
					Member member = ((memberInfo != null) ? memberInfo.ToManipulator() : null);
					if (member == null)
					{
						throw new MissingMemberException(type.ToString(), conditionMemberName);
					}
					flag = member.Get<bool>(target);
				}
			}
			catch (Exception ex)
			{
				string text = "Failed to check attribute condition: \n";
				Exception ex2 = ex;
				Debug.LogWarning(text + ((ex2 != null) ? ex2.ToString() : null));
				flag = fallback;
			}
			return flag;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000B168 File Offset: 0x00009368
		public static bool CheckCondition<T>(T target, string conditionMemberName, bool fallback)
		{
			return AttributeUtility.CheckCondition(((target != null) ? target.GetType() : null) ?? typeof(T), target, conditionMemberName, fallback);
		}

		// Token: 0x04000120 RID: 288
		private static readonly Dictionary<object, AttributeUtility.AttributeCache> optimizedCaches = new Dictionary<object, AttributeUtility.AttributeCache>();

		// Token: 0x020001C7 RID: 455
		private class AttributeCache
		{
			// Token: 0x17000208 RID: 520
			// (get) Token: 0x06000C00 RID: 3072 RVA: 0x000323B6 File Offset: 0x000305B6
			public List<Attribute> inheritedAttributes { get; } = new List<Attribute>();

			// Token: 0x17000209 RID: 521
			// (get) Token: 0x06000C01 RID: 3073 RVA: 0x000323BE File Offset: 0x000305BE
			public List<Attribute> definedAttributes { get; } = new List<Attribute>();

			// Token: 0x06000C02 RID: 3074 RVA: 0x000323C8 File Offset: 0x000305C8
			public AttributeCache(MemberInfo element)
			{
				Ensure.That("element").IsNotNull<MemberInfo>(element);
				try
				{
					try
					{
						this.Cache(Attribute.GetCustomAttributes(element, true), this.inheritedAttributes);
					}
					catch (InvalidCastException ex)
					{
						this.Cache(element.GetCustomAttributes(true).Cast<Attribute>().ToArray<Attribute>(), this.inheritedAttributes);
						Debug.LogWarning(string.Format("Failed to fetch inherited attributes on {0}.\n{1}", element, ex));
					}
				}
				catch (Exception ex2)
				{
					Debug.LogWarning(string.Format("Failed to fetch inherited attributes on {0}.\n{1}", element, ex2));
				}
				try
				{
					try
					{
						this.Cache(Attribute.GetCustomAttributes(element, false), this.definedAttributes);
					}
					catch (InvalidCastException)
					{
						this.Cache(element.GetCustomAttributes(false).Cast<Attribute>().ToArray<Attribute>(), this.definedAttributes);
					}
				}
				catch (Exception ex3)
				{
					Debug.LogWarning(string.Format("Failed to fetch defined attributes on {0}.\n{1}", element, ex3));
				}
			}

			// Token: 0x06000C03 RID: 3075 RVA: 0x000324E0 File Offset: 0x000306E0
			public AttributeCache(ParameterInfo element)
			{
				Ensure.That("element").IsNotNull<ParameterInfo>(element);
				try
				{
					try
					{
						this.Cache(Attribute.GetCustomAttributes(element, true), this.inheritedAttributes);
					}
					catch (InvalidCastException ex)
					{
						this.Cache(element.GetCustomAttributes(true).Cast<Attribute>().ToArray<Attribute>(), this.inheritedAttributes);
						Debug.LogWarning(string.Format("Failed to fetch inherited attributes on {0}.\n{1}", element, ex));
					}
				}
				catch (Exception ex2)
				{
					Debug.LogWarning(string.Format("Failed to fetch inherited attributes on {0}.\n{1}", element, ex2));
				}
				try
				{
					try
					{
						this.Cache(Attribute.GetCustomAttributes(element, false), this.definedAttributes);
					}
					catch (InvalidCastException)
					{
						this.Cache(element.GetCustomAttributes(false).Cast<Attribute>().ToArray<Attribute>(), this.definedAttributes);
					}
				}
				catch (Exception ex3)
				{
					Debug.LogWarning(string.Format("Failed to fetch defined attributes on {0}.\n{1}", element, ex3));
				}
			}

			// Token: 0x06000C04 RID: 3076 RVA: 0x000325F8 File Offset: 0x000307F8
			public AttributeCache(IAttributeProvider element)
			{
				Ensure.That("element").IsNotNull<IAttributeProvider>(element);
				try
				{
					this.Cache(element.GetCustomAttributes(true), this.inheritedAttributes);
				}
				catch (Exception ex)
				{
					Debug.LogWarning(string.Format("Failed to fetch inherited attributes on {0}.\n{1}", element, ex));
				}
				try
				{
					this.Cache(element.GetCustomAttributes(false), this.definedAttributes);
				}
				catch (Exception ex2)
				{
					Debug.LogWarning(string.Format("Failed to fetch defined attributes on {0}.\n{1}", element, ex2));
				}
			}

			// Token: 0x06000C05 RID: 3077 RVA: 0x000326A0 File Offset: 0x000308A0
			private void Cache(Attribute[] attributeObjects, List<Attribute> cache)
			{
				foreach (Attribute attribute in attributeObjects)
				{
					cache.Add(attribute);
				}
			}

			// Token: 0x06000C06 RID: 3078 RVA: 0x000326C8 File Offset: 0x000308C8
			private bool HasAttribute(Type attributeType, List<Attribute> cache)
			{
				for (int i = 0; i < cache.Count; i++)
				{
					Attribute attribute = cache[i];
					if (attributeType.IsInstanceOfType(attribute))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000C07 RID: 3079 RVA: 0x000326FC File Offset: 0x000308FC
			private Attribute GetAttribute(Type attributeType, List<Attribute> cache)
			{
				for (int i = 0; i < cache.Count; i++)
				{
					Attribute attribute = cache[i];
					if (attributeType.IsInstanceOfType(attribute))
					{
						return attribute;
					}
				}
				return null;
			}

			// Token: 0x06000C08 RID: 3080 RVA: 0x0003272E File Offset: 0x0003092E
			private IEnumerable<Attribute> GetAttributes(Type attributeType, List<Attribute> cache)
			{
				int num;
				for (int i = 0; i < cache.Count; i = num + 1)
				{
					Attribute attribute = cache[i];
					if (attributeType.IsInstanceOfType(attribute))
					{
						yield return attribute;
					}
					num = i;
				}
				yield break;
			}

			// Token: 0x06000C09 RID: 3081 RVA: 0x00032745 File Offset: 0x00030945
			public bool HasAttribute(Type attributeType, bool inherit = true)
			{
				if (inherit)
				{
					return this.HasAttribute(attributeType, this.inheritedAttributes);
				}
				return this.HasAttribute(attributeType, this.definedAttributes);
			}

			// Token: 0x06000C0A RID: 3082 RVA: 0x00032765 File Offset: 0x00030965
			public Attribute GetAttribute(Type attributeType, bool inherit = true)
			{
				if (inherit)
				{
					return this.GetAttribute(attributeType, this.inheritedAttributes);
				}
				return this.GetAttribute(attributeType, this.definedAttributes);
			}

			// Token: 0x06000C0B RID: 3083 RVA: 0x00032785 File Offset: 0x00030985
			public IEnumerable<Attribute> GetAttributes(Type attributeType, bool inherit = true)
			{
				if (inherit)
				{
					return this.GetAttributes(attributeType, this.inheritedAttributes);
				}
				return this.GetAttributes(attributeType, this.definedAttributes);
			}

			// Token: 0x06000C0C RID: 3084 RVA: 0x000327A5 File Offset: 0x000309A5
			public bool HasAttribute<TAttribute>(bool inherit = true) where TAttribute : Attribute
			{
				return this.HasAttribute(typeof(TAttribute), inherit);
			}

			// Token: 0x06000C0D RID: 3085 RVA: 0x000327B8 File Offset: 0x000309B8
			public TAttribute GetAttribute<TAttribute>(bool inherit = true) where TAttribute : Attribute
			{
				return (TAttribute)((object)this.GetAttribute(typeof(TAttribute), inherit));
			}

			// Token: 0x06000C0E RID: 3086 RVA: 0x000327D0 File Offset: 0x000309D0
			public IEnumerable<TAttribute> GetAttributes<TAttribute>(bool inherit = true) where TAttribute : Attribute
			{
				return this.GetAttributes(typeof(TAttribute), inherit).Cast<TAttribute>();
			}
		}
	}
}
