using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000A1 RID: 161
	[BurstCompatible]
	[BurstCompatible]
	[BurstCompatible]
	[BurstCompatible]
	public static class FixedStringMethods
	{
		// Token: 0x060004B9 RID: 1209 RVA: 0x0000BA00 File Offset: 0x00009C00
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static FormatError Append<T>(this T fs, Unicode.Rune rune) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			int length = fs.Length;
			int num = rune.LengthInUtf8Bytes();
			if (!fs.TryResize(length + num, NativeArrayOptions.UninitializedMemory))
			{
				return FormatError.Overflow;
			}
			return (ref fs).Write(ref length, rune);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000BA3F File Offset: 0x00009C3F
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static FormatError Append<T>(this T fs, char ch) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			return (ref fs).Append((Unicode.Rune)ch);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000BA50 File Offset: 0x00009C50
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static FormatError AppendRawByte<T>(this T fs, byte a) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			int length = fs.Length;
			if (!fs.TryResize(length + 1, NativeArrayOptions.UninitializedMemory))
			{
				return FormatError.Overflow;
			}
			fs.GetUnsafePtr()[length] = a;
			return FormatError.None;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000BA90 File Offset: 0x00009C90
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static FormatError Append<T>(this T fs, Unicode.Rune rune, int count) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			int length = fs.Length;
			if (!fs.TryResize(length + rune.LengthInUtf8Bytes() * count, NativeArrayOptions.UninitializedMemory))
			{
				return FormatError.Overflow;
			}
			int capacity = fs.Capacity;
			byte* unsafePtr = fs.GetUnsafePtr();
			int num = length;
			for (int i = 0; i < count; i++)
			{
				if (Unicode.UcsToUtf8(unsafePtr, ref num, capacity, rune) != ConversionError.None)
				{
					return FormatError.Overflow;
				}
			}
			return FormatError.None;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000BB04 File Offset: 0x00009D04
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static FormatError Append<T>(this T fs, long input) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			byte* ptr = stackalloc byte[(UIntPtr)20];
			int num = 20;
			if (input >= 0L)
			{
				do
				{
					byte b = (byte)(input % 10L);
					ptr[--num] = 48 + b;
					input /= 10L;
				}
				while (input != 0L);
			}
			else
			{
				do
				{
					byte b2 = (byte)(input % 10L);
					ptr[--num] = 48 - b2;
					input /= 10L;
				}
				while (input != 0L);
				ptr[--num] = 45;
			}
			return (ref fs).Append(ptr + num, 20 - num);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000BB74 File Offset: 0x00009D74
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static FormatError Append<T>(this T fs, int input) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			return (ref fs).Append((long)input);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000BB80 File Offset: 0x00009D80
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static FormatError Append<T>(this T fs, ulong input) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			byte* ptr = stackalloc byte[(UIntPtr)20];
			int num = 20;
			do
			{
				byte b = (byte)(input % 10UL);
				ptr[--num] = 48 + b;
				input /= 10UL;
			}
			while (input != 0UL);
			return (ref fs).Append(ptr + num, 20 - num);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000BBC1 File Offset: 0x00009DC1
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static FormatError Append<T>(this T fs, uint input) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			return (ref fs).Append((ulong)input);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000BBCC File Offset: 0x00009DCC
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static FormatError Append<T>(this T fs, float input, char decimalSeparator = '.') where T : struct, INativeList<byte>, IUTF8Bytes
		{
			FixedStringUtils.UintFloatUnion uintFloatUnion = new FixedStringUtils.UintFloatUnion
			{
				floatValue = input
			};
			uint num = uintFloatUnion.uintValue >> 31;
			uintFloatUnion.uintValue &= 2147483647U;
			if ((uintFloatUnion.uintValue & 2139095040U) == 2139095040U)
			{
				if (uintFloatUnion.uintValue != 2139095040U)
				{
					return (ref fs).Append('N', 'a', 'N');
				}
				FormatError formatError;
				if (num != 0U && (formatError = (ref fs).Append('-')) != FormatError.None)
				{
					return formatError;
				}
				return (ref fs).Append('I', 'n', 'f', 'i', 'n', 'i', 't', 'y');
			}
			else
			{
				FormatError formatError;
				if (num != 0U && uintFloatUnion.uintValue != 0U && (formatError = (ref fs).Append('-')) != FormatError.None)
				{
					return formatError;
				}
				ulong num2 = 0UL;
				int num3 = 0;
				FixedStringUtils.Base2ToBase10(ref num2, ref num3, uintFloatUnion.floatValue);
				char* ptr = stackalloc char[(UIntPtr)18];
				int i = 0;
				while (i < 9)
				{
					ulong num4 = num2 % 10UL;
					ptr[(IntPtr)(8 - i++) * 2] = (char)(48UL + num4);
					num2 /= 10UL;
					if (num2 <= 0UL)
					{
						char* ptr2 = ptr + 9 - i;
						int j = -num3 - i + 1;
						if (j > 0)
						{
							if (j > 4)
							{
								return (ref fs).AppendScientific(ptr2, i, num3, decimalSeparator);
							}
							if ((formatError = (ref fs).Append('0', decimalSeparator)) != FormatError.None)
							{
								return formatError;
							}
							for (j--; j > 0; j--)
							{
								if ((formatError = (ref fs).Append('0')) != FormatError.None)
								{
									return formatError;
								}
							}
							for (int k = 0; k < i; k++)
							{
								if ((formatError = (ref fs).Append(ptr2[k])) != FormatError.None)
								{
									return formatError;
								}
							}
							return FormatError.None;
						}
						else
						{
							int l = num3;
							if (l <= 0)
							{
								int num5 = i + num3;
								for (int m = 0; m < i; m++)
								{
									if (m == num5 && (formatError = (ref fs).Append(decimalSeparator)) != FormatError.None)
									{
										return formatError;
									}
									if ((formatError = (ref fs).Append(ptr2[m])) != FormatError.None)
									{
										return formatError;
									}
								}
								return FormatError.None;
							}
							if (l > 4)
							{
								return (ref fs).AppendScientific(ptr2, i, num3, decimalSeparator);
							}
							for (int n = 0; n < i; n++)
							{
								if ((formatError = (ref fs).Append(ptr2[n])) != FormatError.None)
								{
									return formatError;
								}
							}
							while (l > 0)
							{
								if ((formatError = (ref fs).Append('0')) != FormatError.None)
								{
									return formatError;
								}
								l--;
							}
							return FormatError.None;
						}
					}
				}
				return FormatError.Overflow;
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000BDEC File Offset: 0x00009FEC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public static FormatError Append<T, T2>(this T fs, in T2 input) where T : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			ref T2 ptr = ref UnsafeUtilityExtensions.AsRef<T2>(in input);
			return (ref fs).Append(ptr.GetUnsafePtr(), ptr.Length);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000BE1E File Offset: 0x0000A01E
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public static CopyError CopyFrom<T, T2>(this T fs, in T2 input) where T : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			fs.Length = 0;
			if ((ref fs).Append(in input) != FormatError.None)
			{
				return CopyError.Truncation;
			}
			return CopyError.None;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000BE3C File Offset: 0x0000A03C
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static FormatError Append<T>(this T fs, byte* utf8Bytes, int utf8BytesLength) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			int length = fs.Length;
			if (!fs.TryResize(length + utf8BytesLength, NativeArrayOptions.UninitializedMemory))
			{
				return FormatError.Overflow;
			}
			UnsafeUtility.MemCpy((void*)(fs.GetUnsafePtr() + length), (void*)utf8Bytes, (long)utf8BytesLength);
			return FormatError.None;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000BE84 File Offset: 0x0000A084
		[NotBurstCompatible]
		public unsafe static FormatError Append<T>(this T fs, string s) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			int num = s.Length * 4;
			byte* ptr = stackalloc byte[(UIntPtr)num];
			int num2;
			fixed (string text = s)
			{
				char* ptr2 = text;
				if (ptr2 != null)
				{
					ptr2 += RuntimeHelpers.OffsetToStringData / 2;
				}
				if (UTF8ArrayUnsafeUtility.Copy(ptr, out num2, num, ptr2, s.Length) != CopyError.None)
				{
					return FormatError.Overflow;
				}
			}
			return (ref fs).Append(ptr, num2);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000BED0 File Offset: 0x0000A0D0
		[NotBurstCompatible]
		public static CopyError CopyFrom<T>(this T fs, string s) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			fs.Length = 0;
			if ((ref fs).Append(s) != FormatError.None)
			{
				return CopyError.Truncation;
			}
			return CopyError.None;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000BEEC File Offset: 0x0000A0EC
		[NotBurstCompatible]
		public unsafe static void CopyFromTruncated<T>(this T fs, string s) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			fixed (string text = s)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				int num;
				UTF8ArrayUnsafeUtility.Copy(fs.GetUnsafePtr(), out num, fs.Capacity, ptr, s.Length);
				fs.Length = num;
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000BF40 File Offset: 0x0000A140
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static void AppendFormat<T, U, T0>(this T dest, in U format, in T0 arg0) where T : struct, INativeList<byte>, IUTF8Bytes where U : struct, INativeList<byte>, IUTF8Bytes where T0 : struct, INativeList<byte>, IUTF8Bytes
		{
			ref U ptr = ref UnsafeUtilityExtensions.AsRef<U>(in format);
			int length = ptr.Length;
			byte* unsafePtr = ptr.GetUnsafePtr();
			for (int i = 0; i < length; i++)
			{
				if (unsafePtr[i] == 123)
				{
					if (length - i >= 3 && unsafePtr[i + 1] != 123)
					{
						if (unsafePtr[i + 1] - 48 == 0)
						{
							(ref dest).Append(in arg0);
							i += 2;
						}
						else
						{
							(ref dest).AppendRawByte(unsafePtr[i]);
						}
					}
				}
				else
				{
					(ref dest).AppendRawByte(unsafePtr[i]);
				}
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000BFC4 File Offset: 0x0000A1C4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static void AppendFormat<T, U, T0, T1>(this T dest, in U format, in T0 arg0, in T1 arg1) where T : struct, INativeList<byte>, IUTF8Bytes where U : struct, INativeList<byte>, IUTF8Bytes where T0 : struct, INativeList<byte>, IUTF8Bytes where T1 : struct, INativeList<byte>, IUTF8Bytes
		{
			ref U ptr = ref UnsafeUtilityExtensions.AsRef<U>(in format);
			int length = ptr.Length;
			byte* unsafePtr = ptr.GetUnsafePtr();
			for (int i = 0; i < length; i++)
			{
				if (unsafePtr[i] == 123)
				{
					if (length - i >= 3 && unsafePtr[i + 1] != 123)
					{
						int num = (int)(unsafePtr[i + 1] - 48);
						if (num != 0)
						{
							if (num != 1)
							{
								(ref dest).AppendRawByte(unsafePtr[i]);
							}
							else
							{
								(ref dest).Append(in arg1);
								i += 2;
							}
						}
						else
						{
							(ref dest).Append(in arg0);
							i += 2;
						}
					}
				}
				else
				{
					(ref dest).AppendRawByte(unsafePtr[i]);
				}
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000C05C File Offset: 0x0000A25C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static void AppendFormat<T, U, T0, T1, T2>(this T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2) where T : struct, INativeList<byte>, IUTF8Bytes where U : struct, INativeList<byte>, IUTF8Bytes where T0 : struct, INativeList<byte>, IUTF8Bytes where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			ref U ptr = ref UnsafeUtilityExtensions.AsRef<U>(in format);
			int length = ptr.Length;
			byte* unsafePtr = ptr.GetUnsafePtr();
			for (int i = 0; i < length; i++)
			{
				if (unsafePtr[i] == 123)
				{
					if (length - i >= 3 && unsafePtr[i + 1] != 123)
					{
						switch (unsafePtr[i + 1])
						{
						case 48:
							(ref dest).Append(in arg0);
							i += 2;
							break;
						case 49:
							(ref dest).Append(in arg1);
							i += 2;
							break;
						case 50:
							(ref dest).Append(in arg2);
							i += 2;
							break;
						default:
							(ref dest).AppendRawByte(unsafePtr[i]);
							break;
						}
					}
				}
				else
				{
					(ref dest).AppendRawByte(unsafePtr[i]);
				}
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000C110 File Offset: 0x0000A310
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static void AppendFormat<T, U, T0, T1, T2, T3>(this T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3) where T : struct, INativeList<byte>, IUTF8Bytes where U : struct, INativeList<byte>, IUTF8Bytes where T0 : struct, INativeList<byte>, IUTF8Bytes where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes
		{
			ref U ptr = ref UnsafeUtilityExtensions.AsRef<U>(in format);
			int length = ptr.Length;
			byte* unsafePtr = ptr.GetUnsafePtr();
			for (int i = 0; i < length; i++)
			{
				if (unsafePtr[i] == 123)
				{
					if (length - i >= 3 && unsafePtr[i + 1] != 123)
					{
						switch (unsafePtr[i + 1])
						{
						case 48:
							(ref dest).Append(in arg0);
							i += 2;
							break;
						case 49:
							(ref dest).Append(in arg1);
							i += 2;
							break;
						case 50:
							(ref dest).Append(in arg2);
							i += 2;
							break;
						case 51:
							(ref dest).Append(in arg3);
							i += 2;
							break;
						default:
							(ref dest).AppendRawByte(unsafePtr[i]);
							break;
						}
					}
				}
				else
				{
					(ref dest).AppendRawByte(unsafePtr[i]);
				}
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000C1DC File Offset: 0x0000A3DC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static void AppendFormat<T, U, T0, T1, T2, T3, T4>(this T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4) where T : struct, INativeList<byte>, IUTF8Bytes where U : struct, INativeList<byte>, IUTF8Bytes where T0 : struct, INativeList<byte>, IUTF8Bytes where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes where T4 : struct, INativeList<byte>, IUTF8Bytes
		{
			ref U ptr = ref UnsafeUtilityExtensions.AsRef<U>(in format);
			int length = ptr.Length;
			byte* unsafePtr = ptr.GetUnsafePtr();
			for (int i = 0; i < length; i++)
			{
				if (unsafePtr[i] == 123)
				{
					if (length - i >= 3 && unsafePtr[i + 1] != 123)
					{
						switch (unsafePtr[i + 1])
						{
						case 48:
							(ref dest).Append(in arg0);
							i += 2;
							break;
						case 49:
							(ref dest).Append(in arg1);
							i += 2;
							break;
						case 50:
							(ref dest).Append(in arg2);
							i += 2;
							break;
						case 51:
							(ref dest).Append(in arg3);
							i += 2;
							break;
						case 52:
							(ref dest).Append(in arg4);
							i += 2;
							break;
						default:
							(ref dest).AppendRawByte(unsafePtr[i]);
							break;
						}
					}
				}
				else
				{
					(ref dest).AppendRawByte(unsafePtr[i]);
				}
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0000C2C4 File Offset: 0x0000A4C4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static void AppendFormat<T, U, T0, T1, T2, T3, T4, T5>(this T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5) where T : struct, INativeList<byte>, IUTF8Bytes where U : struct, INativeList<byte>, IUTF8Bytes where T0 : struct, INativeList<byte>, IUTF8Bytes where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes where T4 : struct, INativeList<byte>, IUTF8Bytes where T5 : struct, INativeList<byte>, IUTF8Bytes
		{
			ref U ptr = ref UnsafeUtilityExtensions.AsRef<U>(in format);
			int length = ptr.Length;
			byte* unsafePtr = ptr.GetUnsafePtr();
			for (int i = 0; i < length; i++)
			{
				if (unsafePtr[i] == 123)
				{
					if (length - i >= 3 && unsafePtr[i + 1] != 123)
					{
						switch (unsafePtr[i + 1])
						{
						case 48:
							(ref dest).Append(in arg0);
							i += 2;
							break;
						case 49:
							(ref dest).Append(in arg1);
							i += 2;
							break;
						case 50:
							(ref dest).Append(in arg2);
							i += 2;
							break;
						case 51:
							(ref dest).Append(in arg3);
							i += 2;
							break;
						case 52:
							(ref dest).Append(in arg4);
							i += 2;
							break;
						case 53:
							(ref dest).Append(in arg5);
							i += 2;
							break;
						default:
							(ref dest).AppendRawByte(unsafePtr[i]);
							break;
						}
					}
				}
				else
				{
					(ref dest).AppendRawByte(unsafePtr[i]);
				}
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000C3C0 File Offset: 0x0000A5C0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static void AppendFormat<T, U, T0, T1, T2, T3, T4, T5, T6>(this T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6) where T : struct, INativeList<byte>, IUTF8Bytes where U : struct, INativeList<byte>, IUTF8Bytes where T0 : struct, INativeList<byte>, IUTF8Bytes where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes where T4 : struct, INativeList<byte>, IUTF8Bytes where T5 : struct, INativeList<byte>, IUTF8Bytes where T6 : struct, INativeList<byte>, IUTF8Bytes
		{
			ref U ptr = ref UnsafeUtilityExtensions.AsRef<U>(in format);
			int length = ptr.Length;
			byte* unsafePtr = ptr.GetUnsafePtr();
			for (int i = 0; i < length; i++)
			{
				if (unsafePtr[i] == 123)
				{
					if (length - i >= 3 && unsafePtr[i + 1] != 123)
					{
						switch (unsafePtr[i + 1])
						{
						case 48:
							(ref dest).Append(in arg0);
							i += 2;
							break;
						case 49:
							(ref dest).Append(in arg1);
							i += 2;
							break;
						case 50:
							(ref dest).Append(in arg2);
							i += 2;
							break;
						case 51:
							(ref dest).Append(in arg3);
							i += 2;
							break;
						case 52:
							(ref dest).Append(in arg4);
							i += 2;
							break;
						case 53:
							(ref dest).Append(in arg5);
							i += 2;
							break;
						case 54:
							(ref dest).Append(in arg6);
							i += 2;
							break;
						default:
							(ref dest).AppendRawByte(unsafePtr[i]);
							break;
						}
					}
				}
				else
				{
					(ref dest).AppendRawByte(unsafePtr[i]);
				}
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000C4CC File Offset: 0x0000A6CC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static void AppendFormat<T, U, T0, T1, T2, T3, T4, T5, T6, T7>(this T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7) where T : struct, INativeList<byte>, IUTF8Bytes where U : struct, INativeList<byte>, IUTF8Bytes where T0 : struct, INativeList<byte>, IUTF8Bytes where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes where T4 : struct, INativeList<byte>, IUTF8Bytes where T5 : struct, INativeList<byte>, IUTF8Bytes where T6 : struct, INativeList<byte>, IUTF8Bytes where T7 : struct, INativeList<byte>, IUTF8Bytes
		{
			ref U ptr = ref UnsafeUtilityExtensions.AsRef<U>(in format);
			int length = ptr.Length;
			byte* unsafePtr = ptr.GetUnsafePtr();
			for (int i = 0; i < length; i++)
			{
				if (unsafePtr[i] == 123)
				{
					if (length - i >= 3 && unsafePtr[i + 1] != 123)
					{
						switch (unsafePtr[i + 1])
						{
						case 48:
							(ref dest).Append(in arg0);
							i += 2;
							break;
						case 49:
							(ref dest).Append(in arg1);
							i += 2;
							break;
						case 50:
							(ref dest).Append(in arg2);
							i += 2;
							break;
						case 51:
							(ref dest).Append(in arg3);
							i += 2;
							break;
						case 52:
							(ref dest).Append(in arg4);
							i += 2;
							break;
						case 53:
							(ref dest).Append(in arg5);
							i += 2;
							break;
						case 54:
							(ref dest).Append(in arg6);
							i += 2;
							break;
						case 55:
							(ref dest).Append(in arg7);
							i += 2;
							break;
						default:
							(ref dest).AppendRawByte(unsafePtr[i]);
							break;
						}
					}
				}
				else
				{
					(ref dest).AppendRawByte(unsafePtr[i]);
				}
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000C5F0 File Offset: 0x0000A7F0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static void AppendFormat<T, U, T0, T1, T2, T3, T4, T5, T6, T7, T8>(this T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8) where T : struct, INativeList<byte>, IUTF8Bytes where U : struct, INativeList<byte>, IUTF8Bytes where T0 : struct, INativeList<byte>, IUTF8Bytes where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes where T4 : struct, INativeList<byte>, IUTF8Bytes where T5 : struct, INativeList<byte>, IUTF8Bytes where T6 : struct, INativeList<byte>, IUTF8Bytes where T7 : struct, INativeList<byte>, IUTF8Bytes where T8 : struct, INativeList<byte>, IUTF8Bytes
		{
			ref U ptr = ref UnsafeUtilityExtensions.AsRef<U>(in format);
			int length = ptr.Length;
			byte* unsafePtr = ptr.GetUnsafePtr();
			for (int i = 0; i < length; i++)
			{
				if (unsafePtr[i] == 123)
				{
					if (length - i >= 3 && unsafePtr[i + 1] != 123)
					{
						switch (unsafePtr[i + 1])
						{
						case 48:
							(ref dest).Append(in arg0);
							i += 2;
							break;
						case 49:
							(ref dest).Append(in arg1);
							i += 2;
							break;
						case 50:
							(ref dest).Append(in arg2);
							i += 2;
							break;
						case 51:
							(ref dest).Append(in arg3);
							i += 2;
							break;
						case 52:
							(ref dest).Append(in arg4);
							i += 2;
							break;
						case 53:
							(ref dest).Append(in arg5);
							i += 2;
							break;
						case 54:
							(ref dest).Append(in arg6);
							i += 2;
							break;
						case 55:
							(ref dest).Append(in arg7);
							i += 2;
							break;
						case 56:
							(ref dest).Append(in arg8);
							i += 2;
							break;
						default:
							(ref dest).AppendRawByte(unsafePtr[i]);
							break;
						}
					}
				}
				else
				{
					(ref dest).AppendRawByte(unsafePtr[i]);
				}
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000C72C File Offset: 0x0000A92C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public unsafe static void AppendFormat<T, U, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8, in T9 arg9) where T : struct, INativeList<byte>, IUTF8Bytes where U : struct, INativeList<byte>, IUTF8Bytes where T0 : struct, INativeList<byte>, IUTF8Bytes where T1 : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes where T3 : struct, INativeList<byte>, IUTF8Bytes where T4 : struct, INativeList<byte>, IUTF8Bytes where T5 : struct, INativeList<byte>, IUTF8Bytes where T6 : struct, INativeList<byte>, IUTF8Bytes where T7 : struct, INativeList<byte>, IUTF8Bytes where T8 : struct, INativeList<byte>, IUTF8Bytes where T9 : struct, INativeList<byte>, IUTF8Bytes
		{
			ref U ptr = ref UnsafeUtilityExtensions.AsRef<U>(in format);
			int length = ptr.Length;
			byte* unsafePtr = ptr.GetUnsafePtr();
			for (int i = 0; i < length; i++)
			{
				if (unsafePtr[i] == 123)
				{
					if (length - i >= 3 && unsafePtr[i + 1] != 123)
					{
						switch (unsafePtr[i + 1])
						{
						case 48:
							(ref dest).Append(in arg0);
							i += 2;
							break;
						case 49:
							(ref dest).Append(in arg1);
							i += 2;
							break;
						case 50:
							(ref dest).Append(in arg2);
							i += 2;
							break;
						case 51:
							(ref dest).Append(in arg3);
							i += 2;
							break;
						case 52:
							(ref dest).Append(in arg4);
							i += 2;
							break;
						case 53:
							(ref dest).Append(in arg5);
							i += 2;
							break;
						case 54:
							(ref dest).Append(in arg6);
							i += 2;
							break;
						case 55:
							(ref dest).Append(in arg7);
							i += 2;
							break;
						case 56:
							(ref dest).Append(in arg8);
							i += 2;
							break;
						case 57:
							(ref dest).Append(in arg9);
							i += 2;
							break;
						default:
							(ref dest).AppendRawByte(unsafePtr[i]);
							break;
						}
					}
				}
				else
				{
					(ref dest).AppendRawByte(unsafePtr[i]);
				}
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000C87D File Offset: 0x0000AA7D
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		internal static FormatError Append<T>(this T fs, char a, char b) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			if ((FormatError.None | (ref fs).Append((Unicode.Rune)a) | (ref fs).Append((Unicode.Rune)b)) != FormatError.None)
			{
				return FormatError.Overflow;
			}
			return FormatError.None;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000C89F File Offset: 0x0000AA9F
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		internal static FormatError Append<T>(this T fs, char a, char b, char c) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			if ((FormatError.None | (ref fs).Append((Unicode.Rune)a) | (ref fs).Append((Unicode.Rune)b) | (ref fs).Append((Unicode.Rune)c)) != FormatError.None)
			{
				return FormatError.Overflow;
			}
			return FormatError.None;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000C8D0 File Offset: 0x0000AAD0
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		internal static FormatError Append<T>(this T fs, char a, char b, char c, char d, char e, char f, char g, char h) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			if ((FormatError.None | (ref fs).Append((Unicode.Rune)a) | (ref fs).Append((Unicode.Rune)b) | (ref fs).Append((Unicode.Rune)c) | (ref fs).Append((Unicode.Rune)d) | (ref fs).Append((Unicode.Rune)e) | (ref fs).Append((Unicode.Rune)f) | (ref fs).Append((Unicode.Rune)g) | (ref fs).Append((Unicode.Rune)h)) != FormatError.None)
			{
				return FormatError.Overflow;
			}
			return FormatError.None;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000C950 File Offset: 0x0000AB50
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		internal unsafe static FormatError AppendScientific<T>(this T fs, char* source, int sourceLength, int decimalExponent, char decimalSeparator = '.') where T : struct, INativeList<byte>, IUTF8Bytes
		{
			FormatError formatError;
			if ((formatError = (ref fs).Append(*source)) != FormatError.None)
			{
				return formatError;
			}
			if (sourceLength > 1)
			{
				if ((formatError = (ref fs).Append(decimalSeparator)) != FormatError.None)
				{
					return formatError;
				}
				for (int i = 1; i < sourceLength; i++)
				{
					if ((formatError = (ref fs).Append(source[i])) != FormatError.None)
					{
						return formatError;
					}
				}
			}
			if ((formatError = (ref fs).Append('E')) != FormatError.None)
			{
				return formatError;
			}
			if (decimalExponent < 0)
			{
				if ((formatError = (ref fs).Append('-')) != FormatError.None)
				{
					return formatError;
				}
				decimalExponent *= -1;
				decimalExponent -= sourceLength - 1;
			}
			else
			{
				if ((formatError = (ref fs).Append('+')) != FormatError.None)
				{
					return formatError;
				}
				decimalExponent += sourceLength - 1;
			}
			char* ptr = stackalloc char[(UIntPtr)4];
			for (int j = 0; j < 2; j++)
			{
				int num = decimalExponent % 10;
				ptr[1 - j] = (char)(48 + num);
				decimalExponent /= 10;
			}
			for (int k = 0; k < 2; k++)
			{
				if ((formatError = (ref fs).Append(ptr[k])) != FormatError.None)
				{
					return formatError;
				}
			}
			return FormatError.None;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000CA30 File Offset: 0x0000AC30
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		internal static bool Found<T>(this T fs, ref int offset, char a, char b, char c) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			int num = offset;
			if (((ref fs).Read(ref offset).value | 32) == (int)a && ((ref fs).Read(ref offset).value | 32) == (int)b && ((ref fs).Read(ref offset).value | 32) == (int)c)
			{
				return true;
			}
			offset = num;
			return false;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000CA80 File Offset: 0x0000AC80
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		internal static bool Found<T>(this T fs, ref int offset, char a, char b, char c, char d, char e, char f, char g, char h) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			int num = offset;
			if (((ref fs).Read(ref offset).value | 32) == (int)a && ((ref fs).Read(ref offset).value | 32) == (int)b && ((ref fs).Read(ref offset).value | 32) == (int)c && ((ref fs).Read(ref offset).value | 32) == (int)d && ((ref fs).Read(ref offset).value | 32) == (int)e && ((ref fs).Read(ref offset).value | 32) == (int)f && ((ref fs).Read(ref offset).value | 32) == (int)g && ((ref fs).Read(ref offset).value | 32) == (int)h)
			{
				return true;
			}
			offset = num;
			return false;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000CB30 File Offset: 0x0000AD30
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static int IndexOf<T>(this T fs, byte* bytes, int bytesLen) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			byte* unsafePtr = fs.GetUnsafePtr();
			int length = fs.Length;
			int i = 0;
			IL_003C:
			while (i <= length - bytesLen)
			{
				for (int j = 0; j < bytesLen; j++)
				{
					if (unsafePtr[i + j] != bytes[j])
					{
						i++;
						goto IL_003C;
					}
				}
				return i;
			}
			return -1;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000CB80 File Offset: 0x0000AD80
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static int IndexOf<T>(this T fs, byte* bytes, int bytesLen, int startIndex, int distance = 2147483647) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			byte* unsafePtr = fs.GetUnsafePtr();
			int length = fs.Length;
			int num = Math.Min(distance - 1, length - bytesLen);
			int i = startIndex;
			IL_004F:
			while (i <= num)
			{
				for (int j = 0; j < bytesLen; j++)
				{
					if (unsafePtr[i + j] != bytes[j])
					{
						i++;
						goto IL_004F;
					}
				}
				return i;
			}
			return -1;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000CBE4 File Offset: 0x0000ADE4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public static int IndexOf<T, T2>(this T fs, in T2 other) where T : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			ref T2 ptr = ref UnsafeUtilityExtensions.AsRef<T2>(in other);
			return (ref fs).IndexOf(ptr.GetUnsafePtr(), ptr.Length);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000CC18 File Offset: 0x0000AE18
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public static int IndexOf<T, T2>(this T fs, in T2 other, int startIndex, int distance = 2147483647) where T : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			ref T2 ptr = ref UnsafeUtilityExtensions.AsRef<T2>(in other);
			return (ref fs).IndexOf(ptr.GetUnsafePtr(), ptr.Length, startIndex, distance);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000CC4C File Offset: 0x0000AE4C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public static bool Contains<T, T2>(this T fs, in T2 other) where T : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			return (ref fs).IndexOf(in other) != -1;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000CC5C File Offset: 0x0000AE5C
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static int LastIndexOf<T>(this T fs, byte* bytes, int bytesLen) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			byte* unsafePtr = fs.GetUnsafePtr();
			int i = fs.Length - bytesLen;
			IL_003C:
			while (i >= 0)
			{
				for (int j = 0; j < bytesLen; j++)
				{
					if (unsafePtr[i + j] != bytes[j])
					{
						i--;
						goto IL_003C;
					}
				}
				return i;
			}
			return -1;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000CCAC File Offset: 0x0000AEAC
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static int LastIndexOf<T>(this T fs, byte* bytes, int bytesLen, int startIndex, int distance = 2147483647) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			byte* unsafePtr = fs.GetUnsafePtr();
			startIndex = Math.Min(fs.Length - bytesLen, startIndex);
			int num = Math.Max(0, startIndex - distance);
			int i = startIndex;
			IL_0050:
			while (i >= num)
			{
				for (int j = 0; j < bytesLen; j++)
				{
					if (unsafePtr[i + j] != bytes[j])
					{
						i--;
						goto IL_0050;
					}
				}
				return i;
			}
			return -1;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000CD10 File Offset: 0x0000AF10
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public static int LastIndexOf<T, T2>(this T fs, in T2 other) where T : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			ref T2 ptr = ref UnsafeUtilityExtensions.AsRef<T2>(in other);
			return (ref fs).LastIndexOf(ptr.GetUnsafePtr(), ptr.Length);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000CD44 File Offset: 0x0000AF44
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public static int LastIndexOf<T, T2>(this T fs, in T2 other, int startIndex, int distance = 2147483647) where T : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			ref T2 ptr = ref UnsafeUtilityExtensions.AsRef<T2>(in other);
			return (ref fs).LastIndexOf(ptr.GetUnsafePtr(), ptr.Length, startIndex, distance);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000CD78 File Offset: 0x0000AF78
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static int CompareTo<T>(this T fs, byte* bytes, int bytesLen) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			byte* unsafePtr = fs.GetUnsafePtr();
			int length = fs.Length;
			int num = ((length < bytesLen) ? length : bytesLen);
			for (int i = 0; i < num; i++)
			{
				if (unsafePtr[i] < bytes[i])
				{
					return -1;
				}
				if (unsafePtr[i] > bytes[i])
				{
					return 1;
				}
			}
			if (length < bytesLen)
			{
				return -1;
			}
			if (length > bytesLen)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000CDDC File Offset: 0x0000AFDC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public static int CompareTo<T, T2>(this T fs, in T2 other) where T : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			ref T2 ptr = ref UnsafeUtilityExtensions.AsRef<T2>(in other);
			return (ref fs).CompareTo(ptr.GetUnsafePtr(), ptr.Length);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000CE10 File Offset: 0x0000B010
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static bool Equals<T>(this T fs, byte* bytes, int bytesLen) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			byte* unsafePtr = fs.GetUnsafePtr();
			return fs.Length == bytesLen && (unsafePtr == bytes || (ref fs).CompareTo(bytes, bytesLen) == 0);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000CE4C File Offset: 0x0000B04C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(FixedString128Bytes),
			typeof(FixedString128Bytes)
		})]
		public static bool Equals<T, T2>(this T fs, in T2 other) where T : struct, INativeList<byte>, IUTF8Bytes where T2 : struct, INativeList<byte>, IUTF8Bytes
		{
			ref T2 ptr = ref UnsafeUtilityExtensions.AsRef<T2>(in other);
			return (ref fs).Equals(ptr.GetUnsafePtr(), ptr.Length);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000CE80 File Offset: 0x0000B080
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static Unicode.Rune Peek<T>(this T fs, int index) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			if (index >= fs.Length)
			{
				return Unicode.BadRune;
			}
			Unicode.Rune rune;
			Unicode.Utf8ToUcs(out rune, fs.GetUnsafePtr(), ref index, fs.Capacity);
			return rune;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000CEC8 File Offset: 0x0000B0C8
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static Unicode.Rune Read<T>(this T fs, ref int index) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			if (index >= fs.Length)
			{
				return Unicode.BadRune;
			}
			Unicode.Rune rune;
			Unicode.Utf8ToUcs(out rune, fs.GetUnsafePtr(), ref index, fs.Capacity);
			return rune;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000CF0D File Offset: 0x0000B10D
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static FormatError Write<T>(this T fs, ref int index, Unicode.Rune rune) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			if (Unicode.UcsToUtf8(fs.GetUnsafePtr(), ref index, fs.Capacity, rune) != ConversionError.None)
			{
				return FormatError.Overflow;
			}
			return FormatError.None;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000CF34 File Offset: 0x0000B134
		[NotBurstCompatible]
		public unsafe static string ConvertToString<T>(this T fs) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			char* ptr;
			int num;
			checked
			{
				ptr = stackalloc char[unchecked((UIntPtr)(fs.Length * 2)) * 2];
				num = 0;
			}
			Unicode.Utf8ToUtf16(fs.GetUnsafePtr(), fs.Length, ptr, out num, fs.Length * 2);
			return new string(ptr, 0, num);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000CF8E File Offset: 0x0000B18E
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public unsafe static int ComputeHashCode<T>(this T fs) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			return (int)CollectionHelper.Hash((void*)fs.GetUnsafePtr(), fs.Length);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000CFAD File Offset: 0x0000B1AD
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static int EffectiveSizeOf<T>(this T fs) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			return 2 + fs.Length + 1;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		[MethodImpl(256)]
		internal static bool ParseLongInternal<T>(ref T fs, ref int offset, out long value) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			int num = offset;
			int num2 = 1;
			if (offset < fs.Length)
			{
				if ((ref fs).Peek(offset).value == 43)
				{
					(ref fs).Read(ref offset);
				}
				else if ((ref fs).Peek(offset).value == 45)
				{
					num2 = -1;
					(ref fs).Read(ref offset);
				}
			}
			int num3 = offset;
			value = 0L;
			while (offset < fs.Length && Unicode.Rune.IsDigit((ref fs).Peek(offset)))
			{
				value *= 10L;
				value += (long)((ref fs).Read(ref offset).value - 48);
			}
			value = (long)num2 * value;
			if (offset == num3)
			{
				offset = num;
				return false;
			}
			return true;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000D070 File Offset: 0x0000B270
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static ParseError Parse<T>(this T fs, ref int offset, ref int output) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			long num;
			if (!FixedStringMethods.ParseLongInternal<T>(ref fs, ref offset, out num))
			{
				return ParseError.Syntax;
			}
			if (num > 2147483647L)
			{
				return ParseError.Overflow;
			}
			if (num < -2147483648L)
			{
				return ParseError.Overflow;
			}
			output = (int)num;
			return ParseError.None;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000D0A8 File Offset: 0x0000B2A8
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static ParseError Parse<T>(this T fs, ref int offset, ref uint output) where T : struct, INativeList<byte>, IUTF8Bytes
		{
			long num;
			if (!FixedStringMethods.ParseLongInternal<T>(ref fs, ref offset, out num))
			{
				return ParseError.Syntax;
			}
			if (num > (long)((ulong)(-1)))
			{
				return ParseError.Overflow;
			}
			if (num < 0L)
			{
				return ParseError.Overflow;
			}
			output = (uint)num;
			return ParseError.None;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000D0D8 File Offset: 0x0000B2D8
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(FixedString128Bytes) })]
		public static ParseError Parse<T>(this T fs, ref int offset, ref float output, char decimalSeparator = '.') where T : struct, INativeList<byte>, IUTF8Bytes
		{
			int num = offset;
			int num2 = 1;
			if (offset < fs.Length)
			{
				if ((ref fs).Peek(offset).value == 43)
				{
					(ref fs).Read(ref offset);
				}
				else if ((ref fs).Peek(offset).value == 45)
				{
					num2 = -1;
					(ref fs).Read(ref offset);
				}
			}
			if ((ref fs).Found(ref offset, 'n', 'a', 'n'))
			{
				output = new FixedStringUtils.UintFloatUnion
				{
					uintValue = 4290772992U
				}.floatValue;
				return ParseError.None;
			}
			if ((ref fs).Found(ref offset, 'i', 'n', 'f', 'i', 'n', 'i', 't', 'y'))
			{
				output = ((num2 == 1) ? float.PositiveInfinity : float.NegativeInfinity);
				return ParseError.None;
			}
			ulong num3 = 0UL;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			while (offset < fs.Length && Unicode.Rune.IsDigit((ref fs).Peek(offset)))
			{
				num6++;
				if (num4 < 9)
				{
					ulong num7 = num3 * 10UL + (ulong)((long)((ref fs).Peek(offset).value - 48));
					if (num7 > num3)
					{
						num4++;
					}
					num3 = num7;
				}
				else
				{
					num5--;
				}
				(ref fs).Read(ref offset);
			}
			if (offset < fs.Length && (ref fs).Peek(offset).value == (int)decimalSeparator)
			{
				(ref fs).Read(ref offset);
				while (offset < fs.Length && Unicode.Rune.IsDigit((ref fs).Peek(offset)))
				{
					num6++;
					if (num4 < 9)
					{
						ulong num8 = num3 * 10UL + (ulong)((long)((ref fs).Peek(offset).value - 48));
						if (num8 > num3)
						{
							num4++;
						}
						num3 = num8;
						num5++;
					}
					(ref fs).Read(ref offset);
				}
			}
			if (num6 == 0)
			{
				offset = num;
				return ParseError.Syntax;
			}
			int num9 = 0;
			int num10 = 1;
			if (offset < fs.Length && ((ref fs).Peek(offset).value | 32) == 101)
			{
				(ref fs).Read(ref offset);
				if (offset < fs.Length)
				{
					if ((ref fs).Peek(offset).value == 43)
					{
						(ref fs).Read(ref offset);
					}
					else if ((ref fs).Peek(offset).value == 45)
					{
						num10 = -1;
						(ref fs).Read(ref offset);
					}
				}
				int num11 = offset;
				while (offset < fs.Length && Unicode.Rune.IsDigit((ref fs).Peek(offset)))
				{
					num9 = num9 * 10 + ((ref fs).Peek(offset).value - 48);
					(ref fs).Read(ref offset);
				}
				if (offset == num11)
				{
					offset = num;
					return ParseError.Syntax;
				}
				if (num9 > 38)
				{
					if (num10 == 1)
					{
						return ParseError.Overflow;
					}
					return ParseError.Underflow;
				}
			}
			num9 = num9 * num10 - num5;
			ParseError parseError = FixedStringUtils.Base10ToBase2(ref output, num3, num9);
			if (parseError != ParseError.None)
			{
				return parseError;
			}
			output *= (float)num2;
			return ParseError.None;
		}
	}
}
