using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000151 RID: 337
	public static class EnumUtility
	{
		// Token: 0x0600090F RID: 2319 RVA: 0x0002786C File Offset: 0x00025A6C
		public static bool HasFlag(this Enum value, Enum flag)
		{
			long num = Convert.ToInt64(value);
			long num2 = Convert.ToInt64(flag);
			return (num & num2) == num2;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0002788C File Offset: 0x00025A8C
		public static Dictionary<string, Enum> ValuesByNames(Type enumType, bool obsolete = false)
		{
			Ensure.That("enumType").IsNotNull<Type>(enumType);
			IEnumerable<FieldInfo> enumerable = enumType.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (!obsolete)
			{
				enumerable = enumerable.Where((FieldInfo f) => !f.IsDefined(typeof(ObsoleteAttribute), false));
			}
			return enumerable.ToDictionary((FieldInfo f) => f.Name, (FieldInfo f) => (Enum)f.GetValue(null));
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00027920 File Offset: 0x00025B20
		public static Dictionary<string, T> ValuesByNames<T>(bool obsolete = false)
		{
			IEnumerable<FieldInfo> enumerable = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (!obsolete)
			{
				enumerable = enumerable.Where((FieldInfo f) => !f.IsDefined(typeof(ObsoleteAttribute), false));
			}
			return enumerable.ToDictionary((FieldInfo f) => f.Name, (FieldInfo f) => (T)((object)f.GetValue(null)));
		}
	}
}
