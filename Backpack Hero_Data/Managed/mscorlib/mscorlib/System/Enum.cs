using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System
{
	/// <summary>Provides the base class for enumerations.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F5 RID: 501
	[ComVisible(true)]
	[Serializable]
	public abstract class Enum : ValueType, IComparable, IFormattable, IConvertible
	{
		// Token: 0x06001575 RID: 5493 RVA: 0x00055010 File Offset: 0x00053210
		[SecuritySafeCritical]
		private static Enum.ValuesAndNames GetCachedValuesAndNames(RuntimeType enumType, bool getNames)
		{
			Enum.ValuesAndNames valuesAndNames = enumType.GenericCache as Enum.ValuesAndNames;
			if (valuesAndNames == null || (getNames && valuesAndNames.Names == null))
			{
				ulong[] array = null;
				string[] array2 = null;
				if (!Enum.GetEnumValuesAndNames(enumType, out array, out array2))
				{
					Array.Sort<ulong, string>(array, array2, Comparer<ulong>.Default);
				}
				valuesAndNames = new Enum.ValuesAndNames(array, array2);
				enumType.GenericCache = valuesAndNames;
			}
			return valuesAndNames;
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x00055064 File Offset: 0x00053264
		private static string InternalFormattedHexString(object value)
		{
			switch (Convert.GetTypeCode(value))
			{
			case TypeCode.Boolean:
				return Convert.ToByte((bool)value).ToString("X2", null);
			case TypeCode.Char:
				return ((ushort)((char)value)).ToString("X4", null);
			case TypeCode.SByte:
				return ((byte)((sbyte)value)).ToString("X2", null);
			case TypeCode.Byte:
				return ((byte)value).ToString("X2", null);
			case TypeCode.Int16:
				return ((ushort)((short)value)).ToString("X4", null);
			case TypeCode.UInt16:
				return ((ushort)value).ToString("X4", null);
			case TypeCode.Int32:
				return ((uint)((int)value)).ToString("X8", null);
			case TypeCode.UInt32:
				return ((uint)value).ToString("X8", null);
			case TypeCode.Int64:
				return ((ulong)((long)value)).ToString("X16", null);
			case TypeCode.UInt64:
				return ((ulong)value).ToString("X16", null);
			default:
				throw new InvalidOperationException(Environment.GetResourceString("Unknown enum type."));
			}
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0005519C File Offset: 0x0005339C
		private static string InternalFormat(RuntimeType eT, object value)
		{
			if (eT.IsDefined(typeof(FlagsAttribute), false))
			{
				return Enum.InternalFlagsFormat(eT, value);
			}
			string name = Enum.GetName(eT, value);
			if (name == null)
			{
				return value.ToString();
			}
			return name;
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x000551D8 File Offset: 0x000533D8
		private static string InternalFlagsFormat(RuntimeType eT, object value)
		{
			ulong num = Enum.ToUInt64(value);
			Enum.ValuesAndNames cachedValuesAndNames = Enum.GetCachedValuesAndNames(eT, true);
			string[] names = cachedValuesAndNames.Names;
			ulong[] values = cachedValuesAndNames.Values;
			int num2 = values.Length - 1;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			ulong num3 = num;
			while (num2 >= 0 && (num2 != 0 || values[num2] != 0UL))
			{
				if ((num & values[num2]) == values[num2])
				{
					num -= values[num2];
					if (!flag)
					{
						stringBuilder.Insert(0, ", ");
					}
					stringBuilder.Insert(0, names[num2]);
					flag = false;
				}
				num2--;
			}
			if (num != 0UL)
			{
				return value.ToString();
			}
			if (num3 != 0UL)
			{
				return stringBuilder.ToString();
			}
			if (values.Length != 0 && values[0] == 0UL)
			{
				return names[0];
			}
			return "0";
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x00055280 File Offset: 0x00053480
		internal static ulong ToUInt64(object value)
		{
			ulong num;
			switch (Convert.GetTypeCode(value))
			{
			case TypeCode.Boolean:
			case TypeCode.Char:
			case TypeCode.Byte:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.UInt64:
				num = Convert.ToUInt64(value, CultureInfo.InvariantCulture);
				break;
			case TypeCode.SByte:
			case TypeCode.Int16:
			case TypeCode.Int32:
			case TypeCode.Int64:
				num = (ulong)Convert.ToInt64(value, CultureInfo.InvariantCulture);
				break;
			default:
				throw new InvalidOperationException(Environment.GetResourceString("Unknown enum type."));
			}
			return num;
		}

		// Token: 0x0600157A RID: 5498
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InternalCompareTo(object o1, object o2);

		// Token: 0x0600157B RID: 5499
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType InternalGetUnderlyingType(RuntimeType enumType);

		// Token: 0x0600157C RID: 5500
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetEnumValuesAndNames(RuntimeType enumType, out ulong[] values, out string[] names);

		// Token: 0x0600157D RID: 5501
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object InternalBoxEnum(RuntimeType enumType, long value);

		/// <summary>Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object. The return value indicates whether the conversion succeeded.</summary>
		/// <returns>true if the <paramref name="value" /> parameter was converted successfully; otherwise, false.</returns>
		/// <param name="value">The string representation of the enumeration name or underlying value to convert.</param>
		/// <param name="result">When this method returns, <paramref name="result" /> contains an object of type <paramref name="TEnum" /> whose value is represented by <paramref name="value" /> if the parse operation succeeds. If the parse operation fails, <paramref name="result" /> contains the default value of the underlying type of <paramref name="TEnum" />. Note that this value need not be a member of the <paramref name="TEnum" /> enumeration. This parameter is passed uninitialized.</param>
		/// <typeparam name="TEnum">The enumeration type to which to convert <paramref name="value" />.</typeparam>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="TEnum" /> is not an enumeration type.</exception>
		// Token: 0x0600157E RID: 5502 RVA: 0x000552F3 File Offset: 0x000534F3
		public static bool TryParse<TEnum>(string value, out TEnum result) where TEnum : struct
		{
			return Enum.TryParse<TEnum>(value, false, out result);
		}

		/// <summary>Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object. A parameter specifies whether the operation is case-sensitive. The return value indicates whether the conversion succeeded.</summary>
		/// <returns>true if the <paramref name="value" /> parameter was converted successfully; otherwise, false.</returns>
		/// <param name="value">The string representation of the enumeration name or underlying value to convert.</param>
		/// <param name="ignoreCase">true to ignore case; false to consider case.</param>
		/// <param name="result">When this method returns, <paramref name="result" /> contains an object of type <paramref name="TEnum" /> whose value is represented by <paramref name="value" /> if the parse operation succeeds. If the parse operation fails, <paramref name="result" /> contains the default value of the underlying type of <paramref name="TEnum" />. Note that this value need not be a member of the <paramref name="TEnum" /> enumeration. This parameter is passed uninitialized.</param>
		/// <typeparam name="TEnum">The enumeration type to which to convert <paramref name="value" />.</typeparam>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="TEnum" /> is not an enumeration type.</exception>
		// Token: 0x0600157F RID: 5503 RVA: 0x00055300 File Offset: 0x00053500
		public static bool TryParse<TEnum>(string value, bool ignoreCase, out TEnum result) where TEnum : struct
		{
			result = default(TEnum);
			Enum.EnumResult enumResult = default(Enum.EnumResult);
			enumResult.Init(false);
			bool flag = Enum.TryParseEnum(typeof(TEnum), value, ignoreCase, ref enumResult);
			if (flag)
			{
				result = (TEnum)((object)enumResult.parsedEnum);
			}
			return flag;
		}

		/// <summary>Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.</summary>
		/// <returns>An object of type <paramref name="enumType" /> whose value is represented by <paramref name="value" />.</returns>
		/// <param name="enumType">An enumeration type. </param>
		/// <param name="value">A string containing the name or value to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.-or- <paramref name="value" /> is either an empty string or only contains white space.-or- <paramref name="value" /> is a name, but not one of the named constants defined for the enumeration. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is outside the range of the underlying type of <paramref name="enumType" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001580 RID: 5504 RVA: 0x0005534B File Offset: 0x0005354B
		[ComVisible(true)]
		public static object Parse(Type enumType, string value)
		{
			return Enum.Parse(enumType, value, false);
		}

		/// <summary>Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object. A parameter specifies whether the operation is case-insensitive.</summary>
		/// <returns>An object of type <paramref name="enumType" /> whose value is represented by <paramref name="value" />.</returns>
		/// <param name="enumType">An enumeration type. </param>
		/// <param name="value">A string containing the name or value to convert. </param>
		/// <param name="ignoreCase">true to ignore case; false to regard case. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.-or- <paramref name="value" /> is either an empty string ("") or only contains white space.-or- <paramref name="value" /> is a name, but not one of the named constants defined for the enumeration. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is outside the range of the underlying type of <paramref name="enumType" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001581 RID: 5505 RVA: 0x00055358 File Offset: 0x00053558
		[ComVisible(true)]
		public static object Parse(Type enumType, string value, bool ignoreCase)
		{
			Enum.EnumResult enumResult = default(Enum.EnumResult);
			enumResult.Init(true);
			if (Enum.TryParseEnum(enumType, value, ignoreCase, ref enumResult))
			{
				return enumResult.parsedEnum;
			}
			throw enumResult.GetEnumParseException();
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x00055390 File Offset: 0x00053590
		private static bool TryParseEnum(Type enumType, string value, bool ignoreCase, ref Enum.EnumResult parseResult)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			if (value == null)
			{
				parseResult.SetFailure(Enum.ParseFailureKind.ArgumentNull, "value");
				return false;
			}
			value = value.Trim();
			if (value.Length == 0)
			{
				parseResult.SetFailure(Enum.ParseFailureKind.Argument, "Must specify valid information for parsing in the string.", null);
				return false;
			}
			ulong num = 0UL;
			if (char.IsDigit(value[0]) || value[0] == '-' || value[0] == '+')
			{
				Type underlyingType = Enum.GetUnderlyingType(enumType);
				try
				{
					object obj = Convert.ChangeType(value, underlyingType, CultureInfo.InvariantCulture);
					parseResult.parsedEnum = Enum.ToObject(enumType, obj);
					return true;
				}
				catch (FormatException)
				{
				}
				catch (Exception ex)
				{
					if (parseResult.canThrow)
					{
						throw;
					}
					parseResult.SetFailure(ex);
					return false;
				}
			}
			string[] array = value.Split(Enum.enumSeperatorCharArray);
			Enum.ValuesAndNames cachedValuesAndNames = Enum.GetCachedValuesAndNames(runtimeType, true);
			string[] names = cachedValuesAndNames.Names;
			ulong[] values = cachedValuesAndNames.Values;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Trim();
				bool flag = false;
				int j = 0;
				while (j < names.Length)
				{
					if (ignoreCase)
					{
						if (string.Compare(names[j], array[i], StringComparison.OrdinalIgnoreCase) == 0)
						{
							goto IL_0158;
						}
					}
					else if (names[j].Equals(array[i]))
					{
						goto IL_0158;
					}
					j++;
					continue;
					IL_0158:
					ulong num2 = values[j];
					num |= num2;
					flag = true;
					break;
				}
				if (!flag)
				{
					parseResult.SetFailure(Enum.ParseFailureKind.ArgumentWithParameter, "Requested value '{0}' was not found.", value);
					return false;
				}
			}
			bool flag2;
			try
			{
				parseResult.parsedEnum = Enum.ToObject(enumType, num);
				flag2 = true;
			}
			catch (Exception ex2)
			{
				if (parseResult.canThrow)
				{
					throw;
				}
				parseResult.SetFailure(ex2);
				flag2 = false;
			}
			return flag2;
		}

		/// <summary>Returns the underlying type of the specified enumeration.</summary>
		/// <returns>The underlying type of <paramref name="enumType" />.</returns>
		/// <param name="enumType">The enumeration whose underlying type will be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001583 RID: 5507 RVA: 0x0005558C File Offset: 0x0005378C
		[ComVisible(true)]
		public static Type GetUnderlyingType(Type enumType)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.GetEnumUnderlyingType();
		}

		/// <summary>Retrieves an array of the values of the constants in a specified enumeration.</summary>
		/// <returns>An array that contains the values of the constants in <paramref name="enumType" />. </returns>
		/// <param name="enumType">An enumeration type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">The method is invoked by reflection in a reflection-only context, -or-<paramref name="enumType" /> is a type from an assembly loaded in a reflection-only context.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001584 RID: 5508 RVA: 0x000555A8 File Offset: 0x000537A8
		[ComVisible(true)]
		public static Array GetValues(Type enumType)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.GetEnumValues();
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x000555C4 File Offset: 0x000537C4
		internal static ulong[] InternalGetValues(RuntimeType enumType)
		{
			return Enum.GetCachedValuesAndNames(enumType, false).Values;
		}

		/// <summary>Retrieves the name of the constant in the specified enumeration that has the specified value.</summary>
		/// <returns>A string containing the name of the enumerated constant in <paramref name="enumType" /> whose value is <paramref name="value" />; or null if no such constant is found.</returns>
		/// <param name="enumType">An enumeration type. </param>
		/// <param name="value">The value of a particular enumerated constant in terms of its underlying type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.-or- <paramref name="value" /> is neither of type <paramref name="enumType" /> nor does it have the same underlying type as <paramref name="enumType" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001586 RID: 5510 RVA: 0x000555D2 File Offset: 0x000537D2
		[ComVisible(true)]
		public static string GetName(Type enumType, object value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.GetEnumName(value);
		}

		/// <summary>Retrieves an array of the names of the constants in a specified enumeration.</summary>
		/// <returns>A string array of the names of the constants in <paramref name="enumType" />. </returns>
		/// <param name="enumType">An enumeration type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> parameter is not an <see cref="T:System.Enum" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001587 RID: 5511 RVA: 0x000555EF File Offset: 0x000537EF
		[ComVisible(true)]
		public static string[] GetNames(Type enumType)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.GetEnumNames();
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0005560B File Offset: 0x0005380B
		internal static string[] InternalGetNames(RuntimeType enumType)
		{
			return Enum.GetCachedValuesAndNames(enumType, true).Names;
		}

		/// <summary>Converts the specified object with an integer value to an enumeration member.</summary>
		/// <returns>An enumeration object whose value is <paramref name="value" />.</returns>
		/// <param name="enumType">The enumeration type to return. </param>
		/// <param name="value">The value convert to an enumeration member. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.-or- <paramref name="value" /> is not type <see cref="T:System.SByte" />, <see cref="T:System.Int16" />, <see cref="T:System.Int32" />, <see cref="T:System.Int64" />, <see cref="T:System.Byte" />, <see cref="T:System.UInt16" />, <see cref="T:System.UInt32" />, or <see cref="T:System.UInt64" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001589 RID: 5513 RVA: 0x0005561C File Offset: 0x0005381C
		[ComVisible(true)]
		public static object ToObject(Type enumType, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			switch (Convert.GetTypeCode(value))
			{
			case TypeCode.Boolean:
				return Enum.ToObject(enumType, (bool)value);
			case TypeCode.Char:
				return Enum.ToObject(enumType, (char)value);
			case TypeCode.SByte:
				return Enum.ToObject(enumType, (sbyte)value);
			case TypeCode.Byte:
				return Enum.ToObject(enumType, (byte)value);
			case TypeCode.Int16:
				return Enum.ToObject(enumType, (short)value);
			case TypeCode.UInt16:
				return Enum.ToObject(enumType, (ushort)value);
			case TypeCode.Int32:
				return Enum.ToObject(enumType, (int)value);
			case TypeCode.UInt32:
				return Enum.ToObject(enumType, (uint)value);
			case TypeCode.Int64:
				return Enum.ToObject(enumType, (long)value);
			case TypeCode.UInt64:
				return Enum.ToObject(enumType, (ulong)value);
			default:
				throw new ArgumentException(Environment.GetResourceString("The value passed in must be an enum base or an underlying type for an enum, such as an Int32."), "value");
			}
		}

		/// <summary>Returns an indication whether a constant with a specified value exists in a specified enumeration.</summary>
		/// <returns>true if a constant in <paramref name="enumType" /> has a value equal to <paramref name="value" />; otherwise, false.</returns>
		/// <param name="enumType">An enumeration type. </param>
		/// <param name="value">The value or name of a constant in <paramref name="enumType" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an Enum.-or- The type of <paramref name="value" /> is an enumeration, but it is not an enumeration of type <paramref name="enumType" />.-or- The type of <paramref name="value" /> is not an underlying type of <paramref name="enumType" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> is not type <see cref="T:System.SByte" />, <see cref="T:System.Int16" />, <see cref="T:System.Int32" />, <see cref="T:System.Int64" />, <see cref="T:System.Byte" />, <see cref="T:System.UInt16" />, <see cref="T:System.UInt32" />, or <see cref="T:System.UInt64" />, or <see cref="T:System.String" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600158A RID: 5514 RVA: 0x00055709 File Offset: 0x00053909
		[ComVisible(true)]
		public static bool IsDefined(Type enumType, object value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.IsEnumDefined(value);
		}

		/// <summary>Converts the specified value of a specified enumerated type to its equivalent string representation according to the specified format.</summary>
		/// <returns>A string representation of <paramref name="value" />.</returns>
		/// <param name="enumType">The enumeration type of the value to convert. </param>
		/// <param name="value">The value to convert. </param>
		/// <param name="format">The output format to use. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="enumType" />, <paramref name="value" />, or <paramref name="format" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="enumType" /> parameter is not an <see cref="T:System.Enum" /> type.-or- The <paramref name="value" /> is from an enumeration that differs in type from <paramref name="enumType" />.-or- The type of <paramref name="value" /> is not an underlying type of <paramref name="enumType" />. </exception>
		/// <exception cref="T:System.FormatException">The <paramref name="format" /> parameter contains an invalid value. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="format" /> equals "X", but the enumeration type is unknown.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600158B RID: 5515 RVA: 0x00055728 File Offset: 0x00053928
		[ComVisible(true)]
		public static string Format(Type enumType, object value, string format)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			Type type = value.GetType();
			Type underlyingType = Enum.GetUnderlyingType(enumType);
			if (type.IsEnum)
			{
				Type underlyingType2 = Enum.GetUnderlyingType(type);
				if (!type.IsEquivalentTo(enumType))
				{
					throw new ArgumentException(Environment.GetResourceString("Object must be the same type as the enum. The type passed in was '{0}'; the enum type was '{1}'.", new object[]
					{
						type.ToString(),
						enumType.ToString()
					}));
				}
				value = ((Enum)value).GetValue();
			}
			else if (type != underlyingType)
			{
				throw new ArgumentException(Environment.GetResourceString("Enum underlying type and the object must be same type or object. Type passed in was '{0}'; the enum underlying type was '{1}'.", new object[]
				{
					type.ToString(),
					underlyingType.ToString()
				}));
			}
			if (format.Length != 1)
			{
				throw new FormatException(Environment.GetResourceString("Format String can be only \"G\", \"g\", \"X\", \"x\", \"F\", \"f\", \"D\" or \"d\"."));
			}
			char c = format[0];
			if (c == 'D' || c == 'd')
			{
				return value.ToString();
			}
			if (c == 'X' || c == 'x')
			{
				return Enum.InternalFormattedHexString(value);
			}
			if (c == 'G' || c == 'g')
			{
				return Enum.InternalFormat(runtimeType, value);
			}
			if (c == 'F' || c == 'f')
			{
				return Enum.InternalFlagsFormat(runtimeType, value);
			}
			throw new FormatException(Environment.GetResourceString("Format String can be only \"G\", \"g\", \"X\", \"x\", \"F\", \"f\", \"D\" or \"d\"."));
		}

		// Token: 0x0600158C RID: 5516
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object get_value();

		// Token: 0x0600158D RID: 5517 RVA: 0x000558AF File Offset: 0x00053AAF
		[SecuritySafeCritical]
		internal object GetValue()
		{
			return this.get_value();
		}

		// Token: 0x0600158E RID: 5518
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool InternalHasFlag(Enum flags);

		// Token: 0x0600158F RID: 5519
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int get_hashcode();

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is an enumeration value of the same type and with the same underlying value as this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001590 RID: 5520 RVA: 0x000558B7 File Offset: 0x00053AB7
		public override bool Equals(object obj)
		{
			return ValueType.DefaultEquals(this, obj);
		}

		/// <summary>Returns the hash code for the value of this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001591 RID: 5521 RVA: 0x000558C0 File Offset: 0x00053AC0
		[SecuritySafeCritical]
		public override int GetHashCode()
		{
			return this.get_hashcode();
		}

		/// <summary>Converts the value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001592 RID: 5522 RVA: 0x000558C8 File Offset: 0x00053AC8
		public override string ToString()
		{
			return Enum.InternalFormat((RuntimeType)base.GetType(), this.GetValue());
		}

		/// <summary>This method overload is obsolete; use <see cref="M:System.Enum.ToString(System.String)" />.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <param name="format">A format specification. </param>
		/// <param name="provider">(Obsolete.)</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> does not contain a valid format specification. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="format" /> equals "X", but the enumeration type is unknown.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001593 RID: 5523 RVA: 0x000558E0 File Offset: 0x00053AE0
		[Obsolete("The provider argument is not used. Please use ToString(String).")]
		public string ToString(string format, IFormatProvider provider)
		{
			return this.ToString(format);
		}

		/// <summary>Compares this instance to a specified object and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of this instance and <paramref name="target" />.Value Meaning Less than zero The value of this instance is less than the value of <paramref name="target" />. Zero The value of this instance is equal to the value of <paramref name="target" />. Greater than zero The value of this instance is greater than the value of <paramref name="target" />.-or- <paramref name="target" /> is null. </returns>
		/// <param name="target">An object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> and this instance are not the same type. </exception>
		/// <exception cref="T:System.InvalidOperationException">This instance is not type <see cref="T:System.SByte" />, <see cref="T:System.Int16" />, <see cref="T:System.Int32" />, <see cref="T:System.Int64" />, <see cref="T:System.Byte" />, <see cref="T:System.UInt16" />, <see cref="T:System.UInt32" />, or <see cref="T:System.UInt64" />. </exception>
		/// <exception cref="T:System.NullReferenceException">This instance is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001594 RID: 5524 RVA: 0x000558EC File Offset: 0x00053AEC
		[SecuritySafeCritical]
		public int CompareTo(object target)
		{
			if (this == null)
			{
				throw new NullReferenceException();
			}
			int num = Enum.InternalCompareTo(this, target);
			if (num < 2)
			{
				return num;
			}
			if (num == 2)
			{
				Type type = base.GetType();
				Type type2 = target.GetType();
				throw new ArgumentException(Environment.GetResourceString("Object must be the same type as the enum. The type passed in was '{0}'; the enum type was '{1}'.", new object[]
				{
					type2.ToString(),
					type.ToString()
				}));
			}
			throw new InvalidOperationException(Environment.GetResourceString("Unknown enum type."));
		}

		/// <summary>Converts the value of this instance to its equivalent string representation using the specified format.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <param name="format">A format string. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> contains an invalid specification. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="format" /> equals "X", but the enumeration type is unknown.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001595 RID: 5525 RVA: 0x0005595C File Offset: 0x00053B5C
		public string ToString(string format)
		{
			if (format == null || format.Length == 0)
			{
				format = "G";
			}
			if (string.Compare(format, "G", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return this.ToString();
			}
			if (string.Compare(format, "D", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return this.GetValue().ToString();
			}
			if (string.Compare(format, "X", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return Enum.InternalFormattedHexString(this.GetValue());
			}
			if (string.Compare(format, "F", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return Enum.InternalFlagsFormat((RuntimeType)base.GetType(), this.GetValue());
			}
			throw new FormatException(Environment.GetResourceString("Format String can be only \"G\", \"g\", \"X\", \"x\", \"F\", \"f\", \"D\" or \"d\"."));
		}

		/// <summary>This method overload is obsolete; use <see cref="M:System.Enum.ToString" />.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		/// <param name="provider">(obsolete) </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001596 RID: 5526 RVA: 0x000559F8 File Offset: 0x00053BF8
		[Obsolete("The provider argument is not used. Please use ToString().")]
		public string ToString(IFormatProvider provider)
		{
			return this.ToString();
		}

		/// <summary>Determines whether one or more bit fields are set in the current instance.</summary>
		/// <returns>true if the bit field or bit fields that are set in <paramref name="flag" /> are also set in the current instance; otherwise, false.</returns>
		/// <param name="flag">An enumeration value.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flag" /> is a different type than the current instance.</exception>
		// Token: 0x06001597 RID: 5527 RVA: 0x00055A00 File Offset: 0x00053C00
		[SecuritySafeCritical]
		public bool HasFlag(Enum flag)
		{
			if (flag == null)
			{
				throw new ArgumentNullException("flag");
			}
			if (!base.GetType().IsEquivalentTo(flag.GetType()))
			{
				throw new ArgumentException(Environment.GetResourceString("The argument type, '{0}', is not the same as the enum type '{1}'.", new object[]
				{
					flag.GetType(),
					base.GetType()
				}));
			}
			return this.InternalHasFlag(flag);
		}

		/// <summary>Returns the underlying <see cref="T:System.TypeCode" /> for this instance.</summary>
		/// <returns>The type for this instance.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumeration type is unknown.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001598 RID: 5528 RVA: 0x00055A60 File Offset: 0x00053C60
		public TypeCode GetTypeCode()
		{
			Type underlyingType = Enum.GetUnderlyingType(base.GetType());
			if (underlyingType == typeof(int))
			{
				return TypeCode.Int32;
			}
			if (underlyingType == typeof(sbyte))
			{
				return TypeCode.SByte;
			}
			if (underlyingType == typeof(short))
			{
				return TypeCode.Int16;
			}
			if (underlyingType == typeof(long))
			{
				return TypeCode.Int64;
			}
			if (underlyingType == typeof(uint))
			{
				return TypeCode.UInt32;
			}
			if (underlyingType == typeof(byte))
			{
				return TypeCode.Byte;
			}
			if (underlyingType == typeof(ushort))
			{
				return TypeCode.UInt16;
			}
			if (underlyingType == typeof(ulong))
			{
				return TypeCode.UInt64;
			}
			if (underlyingType == typeof(bool))
			{
				return TypeCode.Boolean;
			}
			if (underlyingType == typeof(char))
			{
				return TypeCode.Char;
			}
			throw new InvalidOperationException(Environment.GetResourceString("Unknown enum type."));
		}

		/// <summary>Converts the current value to a Boolean value based on the underlying type.</summary>
		/// <returns>This member always throws an exception.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases. </exception>
		// Token: 0x06001599 RID: 5529 RVA: 0x00055B54 File Offset: 0x00053D54
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a Unicode character based on the underlying type.</summary>
		/// <returns>This member always throws an exception.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases. </exception>
		// Token: 0x0600159A RID: 5530 RVA: 0x00055B66 File Offset: 0x00053D66
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to an 8-bit signed integer based on the underlying type.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		// Token: 0x0600159B RID: 5531 RVA: 0x00055B78 File Offset: 0x00053D78
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to an 8-bit unsigned integer based on the underlying type.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		// Token: 0x0600159C RID: 5532 RVA: 0x00055B8A File Offset: 0x00053D8A
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 16-bit signed integer based on the underlying type.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		// Token: 0x0600159D RID: 5533 RVA: 0x00055B9C File Offset: 0x00053D9C
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 16-bit unsigned integer based on the underlying type.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		// Token: 0x0600159E RID: 5534 RVA: 0x00055BAE File Offset: 0x00053DAE
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 32-bit signed integer based on the underlying type.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		// Token: 0x0600159F RID: 5535 RVA: 0x00055BC0 File Offset: 0x00053DC0
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 32-bit unsigned integer based on the underlying type.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		// Token: 0x060015A0 RID: 5536 RVA: 0x00055BD2 File Offset: 0x00053DD2
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 64-bit signed integer based on the underlying type.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		// Token: 0x060015A1 RID: 5537 RVA: 0x00055BE4 File Offset: 0x00053DE4
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 64-bit unsigned integer based on the underlying type.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		// Token: 0x060015A2 RID: 5538 RVA: 0x00055BF6 File Offset: 0x00053DF6
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a single-precision floating-point number based on the underlying type.</summary>
		/// <returns>This member always throws an exception.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases. </exception>
		// Token: 0x060015A3 RID: 5539 RVA: 0x00055C08 File Offset: 0x00053E08
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a double-precision floating point number based on the underlying type.</summary>
		/// <returns>This member always throws an exception.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases. </exception>
		// Token: 0x060015A4 RID: 5540 RVA: 0x00055C1A File Offset: 0x00053E1A
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a <see cref="T:System.Decimal" /> based on the underlying type.</summary>
		/// <returns>This member always throws an exception.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases. </exception>
		// Token: 0x060015A5 RID: 5541 RVA: 0x00055C2C File Offset: 0x00053E2C
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a <see cref="T:System.DateTime" /> based on the underlying type.</summary>
		/// <returns>This member always throws an exception.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases. </exception>
		// Token: 0x060015A6 RID: 5542 RVA: 0x00055C3E File Offset: 0x00053E3E
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "Enum", "DateTime" }));
		}

		/// <summary>Converts the current value to a specified type based on the underlying type.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="type">The type to convert to. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		// Token: 0x060015A7 RID: 5543 RVA: 0x0001B8BE File Offset: 0x00019ABE
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		/// <summary>Converts the specified 8-bit signed integer value to an enumeration member.</summary>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <param name="enumType">The enumeration type to return. </param>
		/// <param name="value">The value to convert to an enumeration member. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015A8 RID: 5544 RVA: 0x00055C68 File Offset: 0x00053E68
		[ComVisible(true)]
		[CLSCompliant(false)]
		[SecuritySafeCritical]
		public static object ToObject(Type enumType, sbyte value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)value);
		}

		/// <summary>Converts the specified 16-bit signed integer to an enumeration member.</summary>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <param name="enumType">The enumeration type to return. </param>
		/// <param name="value">The value to convert to an enumeration member. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015A9 RID: 5545 RVA: 0x00055CD4 File Offset: 0x00053ED4
		[ComVisible(true)]
		[SecuritySafeCritical]
		public static object ToObject(Type enumType, short value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)value);
		}

		/// <summary>Converts the specified 32-bit signed integer to an enumeration member.</summary>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <param name="enumType">The enumeration type to return. </param>
		/// <param name="value">The value to convert to an enumeration member. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015AA RID: 5546 RVA: 0x00055D40 File Offset: 0x00053F40
		[ComVisible(true)]
		[SecuritySafeCritical]
		public static object ToObject(Type enumType, int value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)value);
		}

		/// <summary>Converts the specified 8-bit unsigned integer to an enumeration member.</summary>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <param name="enumType">The enumeration type to return. </param>
		/// <param name="value">The value to convert to an enumeration member. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015AB RID: 5547 RVA: 0x00055DAC File Offset: 0x00053FAC
		[ComVisible(true)]
		[SecuritySafeCritical]
		public static object ToObject(Type enumType, byte value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)((ulong)value));
		}

		/// <summary>Converts the specified 16-bit unsigned integer value to an enumeration member.</summary>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <param name="enumType">The enumeration type to return. </param>
		/// <param name="value">The value to convert to an enumeration member. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015AC RID: 5548 RVA: 0x00055E18 File Offset: 0x00054018
		[CLSCompliant(false)]
		[ComVisible(true)]
		[SecuritySafeCritical]
		public static object ToObject(Type enumType, ushort value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)((ulong)value));
		}

		/// <summary>Converts the specified 32-bit unsigned integer value to an enumeration member.</summary>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <param name="enumType">The enumeration type to return. </param>
		/// <param name="value">The value to convert to an enumeration member. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015AD RID: 5549 RVA: 0x00055E84 File Offset: 0x00054084
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		[ComVisible(true)]
		public static object ToObject(Type enumType, uint value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)((ulong)value));
		}

		/// <summary>Converts the specified 64-bit signed integer to an enumeration member.</summary>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <param name="enumType">The enumeration type to return. </param>
		/// <param name="value">The value to convert to an enumeration member. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015AE RID: 5550 RVA: 0x00055EF0 File Offset: 0x000540F0
		[ComVisible(true)]
		[SecuritySafeCritical]
		public static object ToObject(Type enumType, long value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, value);
		}

		/// <summary>Converts the specified 64-bit unsigned integer value to an enumeration member.</summary>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <param name="enumType">The enumeration type to return. </param>
		/// <param name="value">The value to convert to an enumeration member. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015AF RID: 5551 RVA: 0x00055F58 File Offset: 0x00054158
		[ComVisible(true)]
		[CLSCompliant(false)]
		[SecuritySafeCritical]
		public static object ToObject(Type enumType, ulong value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)value);
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x00055FC0 File Offset: 0x000541C0
		[SecuritySafeCritical]
		private static object ToObject(Type enumType, char value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)((ulong)value));
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x0005602C File Offset: 0x0005422C
		[SecuritySafeCritical]
		private static object ToObject(Type enumType, bool value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, value ? 1L : 0L);
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x0005609B File Offset: 0x0005429B
		public static TEnum Parse<TEnum>(string value) where TEnum : struct
		{
			return Enum.Parse<TEnum>(value, false);
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x000560A4 File Offset: 0x000542A4
		public static TEnum Parse<TEnum>(string value, bool ignoreCase) where TEnum : struct
		{
			Enum.EnumResult enumResult = new Enum.EnumResult
			{
				canThrow = true
			};
			if (Enum.TryParseEnum(typeof(TEnum), value, ignoreCase, ref enumResult))
			{
				return (TEnum)((object)enumResult.parsedEnum);
			}
			throw enumResult.GetEnumParseException();
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x000560EC File Offset: 0x000542EC
		public static bool TryParse(Type enumType, string value, bool ignoreCase, out object result)
		{
			result = null;
			Enum.EnumResult enumResult = default(Enum.EnumResult);
			bool flag = Enum.TryParseEnum(enumType, value, ignoreCase, ref enumResult);
			if (flag)
			{
				result = enumResult.parsedEnum;
			}
			return flag;
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00056119 File Offset: 0x00054319
		public static bool TryParse(Type enumType, string value, out object result)
		{
			return Enum.TryParse(enumType, value, false, out result);
		}

		// Token: 0x0400150E RID: 5390
		private static readonly char[] enumSeperatorCharArray = new char[] { ',' };

		// Token: 0x0400150F RID: 5391
		private const string enumSeperator = ", ";

		// Token: 0x020001F6 RID: 502
		private enum ParseFailureKind
		{
			// Token: 0x04001511 RID: 5393
			None,
			// Token: 0x04001512 RID: 5394
			Argument,
			// Token: 0x04001513 RID: 5395
			ArgumentNull,
			// Token: 0x04001514 RID: 5396
			ArgumentWithParameter,
			// Token: 0x04001515 RID: 5397
			UnhandledException
		}

		// Token: 0x020001F7 RID: 503
		private struct EnumResult
		{
			// Token: 0x060015B8 RID: 5560 RVA: 0x0005613E File Offset: 0x0005433E
			internal void Init(bool canMethodThrow)
			{
				this.parsedEnum = 0;
				this.canThrow = canMethodThrow;
			}

			// Token: 0x060015B9 RID: 5561 RVA: 0x00056153 File Offset: 0x00054353
			internal void SetFailure(Exception unhandledException)
			{
				this.m_failure = Enum.ParseFailureKind.UnhandledException;
				this.m_innerException = unhandledException;
			}

			// Token: 0x060015BA RID: 5562 RVA: 0x00056163 File Offset: 0x00054363
			internal void SetFailure(Enum.ParseFailureKind failure, string failureParameter)
			{
				this.m_failure = failure;
				this.m_failureParameter = failureParameter;
				if (this.canThrow)
				{
					throw this.GetEnumParseException();
				}
			}

			// Token: 0x060015BB RID: 5563 RVA: 0x00056182 File Offset: 0x00054382
			internal void SetFailure(Enum.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
			{
				this.m_failure = failure;
				this.m_failureMessageID = failureMessageID;
				this.m_failureMessageFormatArgument = failureMessageFormatArgument;
				if (this.canThrow)
				{
					throw this.GetEnumParseException();
				}
			}

			// Token: 0x060015BC RID: 5564 RVA: 0x000561A8 File Offset: 0x000543A8
			internal Exception GetEnumParseException()
			{
				switch (this.m_failure)
				{
				case Enum.ParseFailureKind.Argument:
					return new ArgumentException(Environment.GetResourceString(this.m_failureMessageID));
				case Enum.ParseFailureKind.ArgumentNull:
					return new ArgumentNullException(this.m_failureParameter);
				case Enum.ParseFailureKind.ArgumentWithParameter:
					return new ArgumentException(Environment.GetResourceString(this.m_failureMessageID, new object[] { this.m_failureMessageFormatArgument }));
				case Enum.ParseFailureKind.UnhandledException:
					return this.m_innerException;
				default:
					return new ArgumentException(Environment.GetResourceString("Requested value '{0}' was not found."));
				}
			}

			// Token: 0x04001516 RID: 5398
			internal object parsedEnum;

			// Token: 0x04001517 RID: 5399
			internal bool canThrow;

			// Token: 0x04001518 RID: 5400
			internal Enum.ParseFailureKind m_failure;

			// Token: 0x04001519 RID: 5401
			internal string m_failureMessageID;

			// Token: 0x0400151A RID: 5402
			internal string m_failureParameter;

			// Token: 0x0400151B RID: 5403
			internal object m_failureMessageFormatArgument;

			// Token: 0x0400151C RID: 5404
			internal Exception m_innerException;
		}

		// Token: 0x020001F8 RID: 504
		private class ValuesAndNames
		{
			// Token: 0x060015BD RID: 5565 RVA: 0x00056229 File Offset: 0x00054429
			public ValuesAndNames(ulong[] values, string[] names)
			{
				this.Values = values;
				this.Names = names;
			}

			// Token: 0x0400151D RID: 5405
			public ulong[] Values;

			// Token: 0x0400151E RID: 5406
			public string[] Names;
		}
	}
}
