using System;
using System.Collections.Generic;

namespace ES3Internal
{
	// Token: 0x020000DB RID: 219
	internal static class ES3Binary
	{
		// Token: 0x060004AE RID: 1198 RVA: 0x0002242C File Offset: 0x0002062C
		internal static ES3SpecialByte TypeToByte(Type type)
		{
			ES3SpecialByte es3SpecialByte;
			if (ES3Binary.TypeToId.TryGetValue(type, out es3SpecialByte))
			{
				return es3SpecialByte;
			}
			return ES3SpecialByte.Object;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0002244F File Offset: 0x0002064F
		internal static Type ByteToType(ES3SpecialByte b)
		{
			return ES3Binary.ByteToType((byte)b);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00022458 File Offset: 0x00020658
		internal static Type ByteToType(byte b)
		{
			Type type;
			if (ES3Binary.IdToType.TryGetValue((ES3SpecialByte)b, out type))
			{
				return type;
			}
			return typeof(object);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00022480 File Offset: 0x00020680
		internal static bool IsPrimitive(ES3SpecialByte b)
		{
			return b - ES3SpecialByte.Bool <= 14;
		}

		// Token: 0x04000155 RID: 341
		internal const string ObjectTerminator = ".";

		// Token: 0x04000156 RID: 342
		internal static readonly Dictionary<ES3SpecialByte, Type> IdToType = new Dictionary<ES3SpecialByte, Type>
		{
			{
				ES3SpecialByte.Null,
				null
			},
			{
				ES3SpecialByte.Bool,
				typeof(bool)
			},
			{
				ES3SpecialByte.Byte,
				typeof(byte)
			},
			{
				ES3SpecialByte.Sbyte,
				typeof(sbyte)
			},
			{
				ES3SpecialByte.Char,
				typeof(char)
			},
			{
				ES3SpecialByte.Decimal,
				typeof(decimal)
			},
			{
				ES3SpecialByte.Double,
				typeof(double)
			},
			{
				ES3SpecialByte.Float,
				typeof(float)
			},
			{
				ES3SpecialByte.Int,
				typeof(int)
			},
			{
				ES3SpecialByte.Uint,
				typeof(uint)
			},
			{
				ES3SpecialByte.Long,
				typeof(long)
			},
			{
				ES3SpecialByte.Ulong,
				typeof(ulong)
			},
			{
				ES3SpecialByte.Short,
				typeof(short)
			},
			{
				ES3SpecialByte.Ushort,
				typeof(ushort)
			},
			{
				ES3SpecialByte.String,
				typeof(string)
			},
			{
				ES3SpecialByte.ByteArray,
				typeof(byte[])
			}
		};

		// Token: 0x04000157 RID: 343
		internal static readonly Dictionary<Type, ES3SpecialByte> TypeToId = new Dictionary<Type, ES3SpecialByte>
		{
			{
				typeof(bool),
				ES3SpecialByte.Bool
			},
			{
				typeof(byte),
				ES3SpecialByte.Byte
			},
			{
				typeof(sbyte),
				ES3SpecialByte.Sbyte
			},
			{
				typeof(char),
				ES3SpecialByte.Char
			},
			{
				typeof(decimal),
				ES3SpecialByte.Decimal
			},
			{
				typeof(double),
				ES3SpecialByte.Double
			},
			{
				typeof(float),
				ES3SpecialByte.Float
			},
			{
				typeof(int),
				ES3SpecialByte.Int
			},
			{
				typeof(uint),
				ES3SpecialByte.Uint
			},
			{
				typeof(long),
				ES3SpecialByte.Long
			},
			{
				typeof(ulong),
				ES3SpecialByte.Ulong
			},
			{
				typeof(short),
				ES3SpecialByte.Short
			},
			{
				typeof(ushort),
				ES3SpecialByte.Ushort
			},
			{
				typeof(string),
				ES3SpecialByte.String
			},
			{
				typeof(byte[]),
				ES3SpecialByte.ByteArray
			}
		};
	}
}
