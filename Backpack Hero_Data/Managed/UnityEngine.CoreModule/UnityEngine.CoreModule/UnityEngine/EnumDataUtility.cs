using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000201 RID: 513
	internal static class EnumDataUtility
	{
		// Token: 0x060016A5 RID: 5797 RVA: 0x0002424C File Offset: 0x0002244C
		internal static EnumData GetCachedEnumData(Type enumType, bool excludeObsolete = true, Func<string, string> nicifyName = null)
		{
			EnumData enumData;
			bool flag = excludeObsolete && EnumDataUtility.s_NonObsoleteEnumData.TryGetValue(enumType, ref enumData);
			EnumData enumData2;
			if (flag)
			{
				enumData2 = enumData;
			}
			else
			{
				bool flag2 = !excludeObsolete && EnumDataUtility.s_EnumData.TryGetValue(enumType, ref enumData);
				if (flag2)
				{
					enumData2 = enumData;
				}
				else
				{
					enumData = new EnumData
					{
						underlyingType = Enum.GetUnderlyingType(enumType)
					};
					enumData.unsigned = enumData.underlyingType == typeof(byte) || enumData.underlyingType == typeof(ushort) || enumData.underlyingType == typeof(uint) || enumData.underlyingType == typeof(ulong);
					FieldInfo[] fields = enumType.GetFields(24);
					List<FieldInfo> list = new List<FieldInfo>();
					int num = fields.Length;
					for (int i = 0; i < num; i++)
					{
						bool flag3 = EnumDataUtility.CheckObsoleteAddition(fields[i], excludeObsolete);
						if (flag3)
						{
							list.Add(fields[i]);
						}
					}
					bool flag4 = !Enumerable.Any<FieldInfo>(list);
					if (flag4)
					{
						string[] array = new string[] { "" };
						Enum[] array2 = new Enum[0];
						int[] array3 = new int[1];
						enumData.values = array2;
						enumData.flagValues = array3;
						enumData.displayNames = array;
						enumData.names = array;
						enumData.tooltip = array;
						enumData.flags = true;
						enumData.serializable = true;
						enumData2 = enumData;
					}
					else
					{
						try
						{
							string location = Enumerable.First<FieldInfo>(list).Module.Assembly.Location;
							bool flag5 = !string.IsNullOrEmpty(location);
							if (flag5)
							{
								list = Enumerable.ToList<FieldInfo>(Enumerable.OrderBy<FieldInfo, int>(list, (FieldInfo f) => f.MetadataToken));
							}
						}
						catch
						{
						}
						enumData.displayNames = Enumerable.ToArray<string>(Enumerable.Select<FieldInfo, string>(list, (FieldInfo f) => EnumDataUtility.EnumNameFromEnumField(f, nicifyName)));
						bool flag6 = Enumerable.Count<string>(Enumerable.Distinct<string>(enumData.displayNames)) != enumData.displayNames.Length;
						if (flag6)
						{
							Debug.LogWarning("Enum " + enumType.Name + " has multiple entries with the same display name, this prevents selection in EnumPopup.");
						}
						enumData.tooltip = Enumerable.ToArray<string>(Enumerable.Select<FieldInfo, string>(list, (FieldInfo f) => EnumDataUtility.EnumTooltipFromEnumField(f)));
						enumData.values = Enumerable.ToArray<Enum>(Enumerable.Select<FieldInfo, Enum>(list, (FieldInfo f) => (Enum)f.GetValue(null)));
						int[] array4;
						if (!enumData.unsigned)
						{
							array4 = Enumerable.ToArray<int>(Enumerable.Select<Enum, int>(enumData.values, (Enum v) => (int)Convert.ToInt64(v)));
						}
						else
						{
							array4 = Enumerable.ToArray<int>(Enumerable.Select<Enum, int>(enumData.values, (Enum v) => (int)Convert.ToUInt64(v)));
						}
						enumData.flagValues = array4;
						enumData.names = new string[enumData.values.Length];
						for (int j = 0; j < enumData.values.Length; j++)
						{
							enumData.names[j] = enumData.values[j].ToString();
						}
						bool flag7 = enumData.underlyingType == typeof(ushort);
						if (flag7)
						{
							int k = 0;
							int num2 = enumData.flagValues.Length;
							while (k < num2)
							{
								bool flag8 = (long)enumData.flagValues[k] == 65535L;
								if (flag8)
								{
									enumData.flagValues[k] = -1;
								}
								k++;
							}
						}
						else
						{
							bool flag9 = enumData.underlyingType == typeof(byte);
							if (flag9)
							{
								int l = 0;
								int num3 = enumData.flagValues.Length;
								while (l < num3)
								{
									bool flag10 = (long)enumData.flagValues[l] == 255L;
									if (flag10)
									{
										enumData.flagValues[l] = -1;
									}
									l++;
								}
							}
						}
						enumData.flags = enumType.IsDefined(typeof(FlagsAttribute), false);
						enumData.serializable = enumData.underlyingType != typeof(long) && enumData.underlyingType != typeof(ulong);
						if (excludeObsolete)
						{
							EnumDataUtility.s_NonObsoleteEnumData[enumType] = enumData;
						}
						else
						{
							EnumDataUtility.s_EnumData[enumType] = enumData;
						}
						enumData2 = enumData;
					}
				}
			}
			return enumData2;
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x000246EC File Offset: 0x000228EC
		internal static int EnumFlagsToInt(EnumData enumData, Enum enumValue)
		{
			bool unsigned = enumData.unsigned;
			int num;
			if (unsigned)
			{
				bool flag = enumData.underlyingType == typeof(uint);
				if (flag)
				{
					num = (int)Convert.ToUInt32(enumValue);
				}
				else
				{
					bool flag2 = enumData.underlyingType == typeof(ushort);
					if (flag2)
					{
						ushort num2 = Convert.ToUInt16(enumValue);
						num = ((num2 == ushort.MaxValue) ? (-1) : ((int)num2));
					}
					else
					{
						byte b = Convert.ToByte(enumValue);
						num = ((b == byte.MaxValue) ? (-1) : ((int)b));
					}
				}
			}
			else
			{
				num = Convert.ToInt32(enumValue);
			}
			return num;
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x00024778 File Offset: 0x00022978
		internal static Enum IntToEnumFlags(Type enumType, int value)
		{
			EnumData cachedEnumData = EnumDataUtility.GetCachedEnumData(enumType, true, null);
			bool unsigned = cachedEnumData.unsigned;
			Enum @enum;
			if (unsigned)
			{
				bool flag = cachedEnumData.underlyingType == typeof(uint);
				if (flag)
				{
					uint num = (uint)value;
					@enum = Enum.Parse(enumType, num.ToString()) as Enum;
				}
				else
				{
					bool flag2 = cachedEnumData.underlyingType == typeof(ushort);
					if (flag2)
					{
						@enum = Enum.Parse(enumType, ((ushort)value).ToString()) as Enum;
					}
					else
					{
						@enum = Enum.Parse(enumType, ((byte)value).ToString()) as Enum;
					}
				}
			}
			else
			{
				@enum = Enum.Parse(enumType, value.ToString()) as Enum;
			}
			return @enum;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00024830 File Offset: 0x00022A30
		private static bool CheckObsoleteAddition(FieldInfo field, bool excludeObsolete)
		{
			object[] customAttributes = field.GetCustomAttributes(typeof(ObsoleteAttribute), false);
			bool flag = customAttributes.Length != 0;
			return !flag || (!excludeObsolete && !((ObsoleteAttribute)Enumerable.First<object>(customAttributes)).IsError);
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00024880 File Offset: 0x00022A80
		private static string EnumTooltipFromEnumField(FieldInfo field)
		{
			object[] customAttributes = field.GetCustomAttributes(typeof(TooltipAttribute), false);
			bool flag = customAttributes.Length != 0;
			string text;
			if (flag)
			{
				text = ((TooltipAttribute)Enumerable.First<object>(customAttributes)).tooltip;
			}
			else
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x000248C8 File Offset: 0x00022AC8
		private static string EnumNameFromEnumField(FieldInfo field, Func<string, string> nicifyName)
		{
			EnumDataUtility.<>c__DisplayClass7_0 CS$<>8__locals1;
			CS$<>8__locals1.nicifyName = nicifyName;
			CS$<>8__locals1.field = field;
			object[] customAttributes = CS$<>8__locals1.field.GetCustomAttributes(typeof(InspectorNameAttribute), false);
			bool flag = customAttributes.Length != 0;
			string text;
			if (flag)
			{
				text = ((InspectorNameAttribute)Enumerable.First<object>(customAttributes)).displayName;
			}
			else
			{
				bool flag2 = CS$<>8__locals1.field.IsDefined(typeof(ObsoleteAttribute), false);
				if (flag2)
				{
					text = EnumDataUtility.<EnumNameFromEnumField>g__NicifyName|7_0(ref CS$<>8__locals1) + " (Obsolete)";
				}
				else
				{
					text = EnumDataUtility.<EnumNameFromEnumField>g__NicifyName|7_0(ref CS$<>8__locals1);
				}
			}
			return text;
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00024970 File Offset: 0x00022B70
		[CompilerGenerated]
		internal static string <EnumNameFromEnumField>g__NicifyName|7_0(ref EnumDataUtility.<>c__DisplayClass7_0 A_0)
		{
			return (A_0.nicifyName == null) ? A_0.field.Name : A_0.nicifyName.Invoke(A_0.field.Name);
		}

		// Token: 0x040007E3 RID: 2019
		private static readonly Dictionary<Type, EnumData> s_NonObsoleteEnumData = new Dictionary<Type, EnumData>();

		// Token: 0x040007E4 RID: 2020
		private static readonly Dictionary<Type, EnumData> s_EnumData = new Dictionary<Type, EnumData>();
	}
}
