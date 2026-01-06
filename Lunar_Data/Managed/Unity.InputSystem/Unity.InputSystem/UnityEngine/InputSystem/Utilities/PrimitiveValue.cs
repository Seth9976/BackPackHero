using System;
using System.Globalization;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200013E RID: 318
	[StructLayout(LayoutKind.Explicit)]
	public struct PrimitiveValue : IEquatable<PrimitiveValue>, IConvertible
	{
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x00051B12 File Offset: 0x0004FD12
		internal unsafe byte* valuePtr
		{
			get
			{
				return (byte*)UnsafeUtility.AddressOf<PrimitiveValue>(ref this) + 4;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x00051B1C File Offset: 0x0004FD1C
		public TypeCode type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x00051B24 File Offset: 0x0004FD24
		public bool isEmpty
		{
			get
			{
				return this.type == TypeCode.Empty;
			}
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00051B2F File Offset: 0x0004FD2F
		public PrimitiveValue(bool value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.Boolean;
			this.m_BoolValue = value;
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x00051B46 File Offset: 0x0004FD46
		public PrimitiveValue(char value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.Char;
			this.m_CharValue = value;
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x00051B5D File Offset: 0x0004FD5D
		public PrimitiveValue(byte value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.Byte;
			this.m_ByteValue = value;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00051B74 File Offset: 0x0004FD74
		public PrimitiveValue(sbyte value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.SByte;
			this.m_SByteValue = value;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00051B8B File Offset: 0x0004FD8B
		public PrimitiveValue(short value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.Int16;
			this.m_ShortValue = value;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00051BA2 File Offset: 0x0004FDA2
		public PrimitiveValue(ushort value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.UInt16;
			this.m_UShortValue = value;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00051BB9 File Offset: 0x0004FDB9
		public PrimitiveValue(int value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.Int32;
			this.m_IntValue = value;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00051BD1 File Offset: 0x0004FDD1
		public PrimitiveValue(uint value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.UInt32;
			this.m_UIntValue = value;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00051BE9 File Offset: 0x0004FDE9
		public PrimitiveValue(long value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.Int64;
			this.m_LongValue = value;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00051C01 File Offset: 0x0004FE01
		public PrimitiveValue(ulong value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.UInt64;
			this.m_ULongValue = value;
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00051C19 File Offset: 0x0004FE19
		public PrimitiveValue(float value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.Single;
			this.m_FloatValue = value;
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00051C31 File Offset: 0x0004FE31
		public PrimitiveValue(double value)
		{
			this = default(PrimitiveValue);
			this.m_Type = TypeCode.Double;
			this.m_DoubleValue = value;
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00051C4C File Offset: 0x0004FE4C
		public PrimitiveValue ConvertTo(TypeCode type)
		{
			switch (type)
			{
			case TypeCode.Empty:
				return default(PrimitiveValue);
			case TypeCode.Boolean:
				return this.ToBoolean(null);
			case TypeCode.Char:
				return this.ToChar(null);
			case TypeCode.SByte:
				return this.ToSByte(null);
			case TypeCode.Byte:
				return this.ToByte(null);
			case TypeCode.Int16:
				return this.ToInt16(null);
			case TypeCode.UInt16:
				return this.ToInt16(null);
			case TypeCode.Int32:
				return this.ToInt32(null);
			case TypeCode.UInt32:
				return this.ToInt32(null);
			case TypeCode.Int64:
				return this.ToInt64(null);
			case TypeCode.UInt64:
				return this.ToUInt64(null);
			case TypeCode.Single:
				return this.ToSingle(null);
			case TypeCode.Double:
				return this.ToDouble(null);
			}
			throw new ArgumentException(string.Format("Don't know how to convert PrimitiveValue to '{0}'", type), "type");
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00051D60 File Offset: 0x0004FF60
		public unsafe bool Equals(PrimitiveValue other)
		{
			if (this.m_Type != other.m_Type)
			{
				return false;
			}
			void* ptr = UnsafeUtility.AddressOf<double>(ref this.m_DoubleValue);
			void* ptr2 = UnsafeUtility.AddressOf<double>(ref other.m_DoubleValue);
			return UnsafeUtility.MemCmp(ptr, ptr2, 8L) == 0;
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00051DA0 File Offset: 0x0004FFA0
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is PrimitiveValue)
			{
				PrimitiveValue primitiveValue = (PrimitiveValue)obj;
				return this.Equals(primitiveValue);
			}
			return (obj is bool || obj is char || obj is byte || obj is sbyte || obj is short || obj is ushort || obj is int || obj is uint || obj is long || obj is ulong || obj is float || obj is double) && this.Equals(PrimitiveValue.FromObject(obj));
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00051E37 File Offset: 0x00050037
		public static bool operator ==(PrimitiveValue left, PrimitiveValue right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00051E41 File Offset: 0x00050041
		public static bool operator !=(PrimitiveValue left, PrimitiveValue right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00051E50 File Offset: 0x00050050
		public unsafe override int GetHashCode()
		{
			fixed (double* ptr = &this.m_DoubleValue)
			{
				double* ptr2 = ptr;
				return (this.m_Type.GetHashCode() * 397) ^ ptr2->GetHashCode();
			}
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00051E88 File Offset: 0x00050088
		public override string ToString()
		{
			switch (this.type)
			{
			case TypeCode.Boolean:
				if (!this.m_BoolValue)
				{
					return "false";
				}
				return "true";
			case TypeCode.Char:
				return "'" + this.m_CharValue.ToString() + "'";
			case TypeCode.SByte:
				return this.m_SByteValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			case TypeCode.Byte:
				return this.m_ByteValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			case TypeCode.Int16:
				return this.m_ShortValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			case TypeCode.UInt16:
				return this.m_UShortValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			case TypeCode.Int32:
				return this.m_IntValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			case TypeCode.UInt32:
				return this.m_UIntValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			case TypeCode.Int64:
				return this.m_LongValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			case TypeCode.UInt64:
				return this.m_ULongValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			case TypeCode.Single:
				return this.m_FloatValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			case TypeCode.Double:
				return this.m_DoubleValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			default:
				return string.Empty;
			}
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00051FEC File Offset: 0x000501EC
		public static PrimitiveValue FromString(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return default(PrimitiveValue);
			}
			if (value.Equals("true", StringComparison.InvariantCultureIgnoreCase))
			{
				return new PrimitiveValue(true);
			}
			if (value.Equals("false", StringComparison.InvariantCultureIgnoreCase))
			{
				return new PrimitiveValue(false);
			}
			double num;
			if ((value.Contains('.') || value.Contains("e") || value.Contains("E") || value.Contains("infinity", StringComparison.InvariantCultureIgnoreCase)) && double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out num))
			{
				return new PrimitiveValue(num);
			}
			long num2;
			if (long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out num2))
			{
				return new PrimitiveValue(num2);
			}
			if (value.IndexOf("0x", StringComparison.InvariantCultureIgnoreCase) != -1)
			{
				string text = value.TrimStart();
				if (text.StartsWith("0x"))
				{
					text = text.Substring(2);
				}
				long num3;
				if (long.TryParse(text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num3))
				{
					return new PrimitiveValue(num3);
				}
			}
			throw new NotImplementedException();
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x000520E5 File Offset: 0x000502E5
		public TypeCode GetTypeCode()
		{
			return this.type;
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x000520F0 File Offset: 0x000502F0
		public bool ToBoolean(IFormatProvider provider = null)
		{
			switch (this.type)
			{
			case TypeCode.Boolean:
				return this.m_BoolValue;
			case TypeCode.Char:
				return this.m_CharValue > '\0';
			case TypeCode.SByte:
				return this.m_SByteValue != 0;
			case TypeCode.Byte:
				return this.m_ByteValue > 0;
			case TypeCode.Int16:
				return this.m_ShortValue != 0;
			case TypeCode.UInt16:
				return this.m_UShortValue > 0;
			case TypeCode.Int32:
				return this.m_IntValue != 0;
			case TypeCode.UInt32:
				return this.m_UIntValue > 0U;
			case TypeCode.Int64:
				return this.m_LongValue != 0L;
			case TypeCode.UInt64:
				return this.m_ULongValue > 0UL;
			case TypeCode.Single:
				return !Mathf.Approximately(this.m_FloatValue, 0f);
			case TypeCode.Double:
				return NumberHelpers.Approximately(this.m_DoubleValue, 0.0);
			default:
				return false;
			}
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x000521CE File Offset: 0x000503CE
		public byte ToByte(IFormatProvider provider = null)
		{
			return (byte)this.ToInt64(provider);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x000521D8 File Offset: 0x000503D8
		public char ToChar(IFormatProvider provider = null)
		{
			TypeCode type = this.type;
			if (type == TypeCode.Char)
			{
				return this.m_CharValue;
			}
			if (type - TypeCode.Int16 > 5)
			{
				return '\0';
			}
			return (char)this.ToInt64(provider);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00052209 File Offset: 0x00050409
		public DateTime ToDateTime(IFormatProvider provider = null)
		{
			throw new NotSupportedException("Converting PrimitiveValue to DateTime");
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00052215 File Offset: 0x00050415
		public decimal ToDecimal(IFormatProvider provider = null)
		{
			return new decimal(this.ToDouble(provider));
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00052224 File Offset: 0x00050424
		public double ToDouble(IFormatProvider provider = null)
		{
			switch (this.type)
			{
			case TypeCode.Boolean:
				if (this.m_BoolValue)
				{
					return 1.0;
				}
				return 0.0;
			case TypeCode.Char:
				return (double)this.m_CharValue;
			case TypeCode.SByte:
				return (double)this.m_SByteValue;
			case TypeCode.Byte:
				return (double)this.m_ByteValue;
			case TypeCode.Int16:
				return (double)this.m_ShortValue;
			case TypeCode.UInt16:
				return (double)this.m_UShortValue;
			case TypeCode.Int32:
				return (double)this.m_IntValue;
			case TypeCode.UInt32:
				return this.m_UIntValue;
			case TypeCode.Int64:
				return (double)this.m_LongValue;
			case TypeCode.UInt64:
				return this.m_ULongValue;
			case TypeCode.Single:
				return (double)this.m_FloatValue;
			case TypeCode.Double:
				return this.m_DoubleValue;
			default:
				return 0.0;
			}
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x000522F0 File Offset: 0x000504F0
		public short ToInt16(IFormatProvider provider = null)
		{
			return (short)this.ToInt64(provider);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x000522FA File Offset: 0x000504FA
		public int ToInt32(IFormatProvider provider = null)
		{
			return (int)this.ToInt64(provider);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00052304 File Offset: 0x00050504
		public long ToInt64(IFormatProvider provider = null)
		{
			switch (this.type)
			{
			case TypeCode.Boolean:
				if (this.m_BoolValue)
				{
					return 1L;
				}
				return 0L;
			case TypeCode.Char:
				return (long)((ulong)this.m_CharValue);
			case TypeCode.SByte:
				return (long)this.m_SByteValue;
			case TypeCode.Byte:
				return (long)((ulong)this.m_ByteValue);
			case TypeCode.Int16:
				return (long)this.m_ShortValue;
			case TypeCode.UInt16:
				return (long)((ulong)this.m_UShortValue);
			case TypeCode.Int32:
				return (long)this.m_IntValue;
			case TypeCode.UInt32:
				return (long)((ulong)this.m_UIntValue);
			case TypeCode.Int64:
				return this.m_LongValue;
			case TypeCode.UInt64:
				return (long)this.m_ULongValue;
			case TypeCode.Single:
				return (long)this.m_FloatValue;
			case TypeCode.Double:
				return (long)this.m_DoubleValue;
			default:
				return 0L;
			}
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x000523B8 File Offset: 0x000505B8
		public sbyte ToSByte(IFormatProvider provider = null)
		{
			return (sbyte)this.ToInt64(provider);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x000523C2 File Offset: 0x000505C2
		public float ToSingle(IFormatProvider provider = null)
		{
			return (float)this.ToDouble(provider);
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x000523CC File Offset: 0x000505CC
		public string ToString(IFormatProvider provider)
		{
			return this.ToString();
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x000523DA File Offset: 0x000505DA
		public object ToType(Type conversionType, IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000523E1 File Offset: 0x000505E1
		public ushort ToUInt16(IFormatProvider provider = null)
		{
			return (ushort)this.ToUInt64(null);
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x000523EB File Offset: 0x000505EB
		public uint ToUInt32(IFormatProvider provider = null)
		{
			return (uint)this.ToUInt64(null);
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x000523F8 File Offset: 0x000505F8
		public ulong ToUInt64(IFormatProvider provider = null)
		{
			switch (this.type)
			{
			case TypeCode.Boolean:
				if (this.m_BoolValue)
				{
					return 1UL;
				}
				return 0UL;
			case TypeCode.Char:
				return (ulong)this.m_CharValue;
			case TypeCode.SByte:
				return (ulong)((long)this.m_SByteValue);
			case TypeCode.Byte:
				return (ulong)this.m_ByteValue;
			case TypeCode.Int16:
				return (ulong)((long)this.m_ShortValue);
			case TypeCode.UInt16:
				return (ulong)this.m_UShortValue;
			case TypeCode.Int32:
				return (ulong)((long)this.m_IntValue);
			case TypeCode.UInt32:
				return (ulong)this.m_UIntValue;
			case TypeCode.Int64:
				return (ulong)this.m_LongValue;
			case TypeCode.UInt64:
				return this.m_ULongValue;
			case TypeCode.Single:
				return (ulong)this.m_FloatValue;
			case TypeCode.Double:
				return (ulong)this.m_DoubleValue;
			default:
				return 0UL;
			}
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x000524AC File Offset: 0x000506AC
		public object ToObject()
		{
			switch (this.m_Type)
			{
			case TypeCode.Boolean:
				return this.m_BoolValue;
			case TypeCode.Char:
				return this.m_CharValue;
			case TypeCode.SByte:
				return this.m_SByteValue;
			case TypeCode.Byte:
				return this.m_ByteValue;
			case TypeCode.Int16:
				return this.m_ShortValue;
			case TypeCode.UInt16:
				return this.m_UShortValue;
			case TypeCode.Int32:
				return this.m_IntValue;
			case TypeCode.UInt32:
				return this.m_UIntValue;
			case TypeCode.Int64:
				return this.m_LongValue;
			case TypeCode.UInt64:
				return this.m_ULongValue;
			case TypeCode.Single:
				return this.m_FloatValue;
			case TypeCode.Double:
				return this.m_DoubleValue;
			default:
				return null;
			}
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00052590 File Offset: 0x00050790
		public static PrimitiveValue From<TValue>(TValue value) where TValue : struct
		{
			Type type = typeof(TValue);
			if (type.IsEnum)
			{
				type = type.GetEnumUnderlyingType();
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				return new PrimitiveValue(Convert.ToBoolean(value));
			case TypeCode.Char:
				return new PrimitiveValue(Convert.ToChar(value));
			case TypeCode.SByte:
				return new PrimitiveValue(Convert.ToSByte(value));
			case TypeCode.Byte:
				return new PrimitiveValue(Convert.ToByte(value));
			case TypeCode.Int16:
				return new PrimitiveValue(Convert.ToInt16(value));
			case TypeCode.UInt16:
				return new PrimitiveValue(Convert.ToUInt16(value));
			case TypeCode.Int32:
				return new PrimitiveValue(Convert.ToInt32(value));
			case TypeCode.UInt32:
				return new PrimitiveValue(Convert.ToUInt32(value));
			case TypeCode.Int64:
				return new PrimitiveValue(Convert.ToInt64(value));
			case TypeCode.UInt64:
				return new PrimitiveValue(Convert.ToUInt64(value));
			case TypeCode.Single:
				return new PrimitiveValue(Convert.ToSingle(value));
			case TypeCode.Double:
				return new PrimitiveValue(Convert.ToDouble(value));
			default:
				throw new ArgumentException(string.Format("Cannot convert value '{0}' of type '{1}' to PrimitiveValue", value, typeof(TValue).Name), "value");
			}
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x000526F0 File Offset: 0x000508F0
		public static PrimitiveValue FromObject(object value)
		{
			if (value == null)
			{
				return default(PrimitiveValue);
			}
			string text = value as string;
			if (text != null)
			{
				return PrimitiveValue.FromString(text);
			}
			if (value is bool)
			{
				bool flag = (bool)value;
				return new PrimitiveValue(flag);
			}
			if (value is char)
			{
				char c = (char)value;
				return new PrimitiveValue(c);
			}
			if (value is byte)
			{
				byte b = (byte)value;
				return new PrimitiveValue(b);
			}
			if (value is sbyte)
			{
				sbyte b2 = (sbyte)value;
				return new PrimitiveValue(b2);
			}
			if (value is short)
			{
				short num = (short)value;
				return new PrimitiveValue(num);
			}
			if (value is ushort)
			{
				ushort num2 = (ushort)value;
				return new PrimitiveValue(num2);
			}
			if (value is int)
			{
				int num3 = (int)value;
				return new PrimitiveValue(num3);
			}
			if (value is uint)
			{
				uint num4 = (uint)value;
				return new PrimitiveValue(num4);
			}
			if (value is long)
			{
				long num5 = (long)value;
				return new PrimitiveValue(num5);
			}
			if (value is ulong)
			{
				ulong num6 = (ulong)value;
				return new PrimitiveValue(num6);
			}
			if (value is float)
			{
				float num7 = (float)value;
				return new PrimitiveValue(num7);
			}
			if (value is double)
			{
				double num8 = (double)value;
				return new PrimitiveValue(num8);
			}
			if (value is Enum)
			{
				switch (Type.GetTypeCode(value.GetType().GetEnumUnderlyingType()))
				{
				case TypeCode.SByte:
					return new PrimitiveValue((sbyte)value);
				case TypeCode.Byte:
					return new PrimitiveValue((byte)value);
				case TypeCode.Int16:
					return new PrimitiveValue((short)value);
				case TypeCode.UInt16:
					return new PrimitiveValue((ushort)value);
				case TypeCode.Int32:
					return new PrimitiveValue((int)value);
				case TypeCode.UInt32:
					return new PrimitiveValue((uint)value);
				case TypeCode.Int64:
					return new PrimitiveValue((long)value);
				case TypeCode.UInt64:
					return new PrimitiveValue((ulong)value);
				}
			}
			throw new ArgumentException(string.Format("Cannot convert '{0}' to primitive value", value), "value");
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x000528F3 File Offset: 0x00050AF3
		public static implicit operator PrimitiveValue(bool value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x000528FB File Offset: 0x00050AFB
		public static implicit operator PrimitiveValue(char value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00052903 File Offset: 0x00050B03
		public static implicit operator PrimitiveValue(byte value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0005290B File Offset: 0x00050B0B
		public static implicit operator PrimitiveValue(sbyte value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00052913 File Offset: 0x00050B13
		public static implicit operator PrimitiveValue(short value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0005291B File Offset: 0x00050B1B
		public static implicit operator PrimitiveValue(ushort value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00052923 File Offset: 0x00050B23
		public static implicit operator PrimitiveValue(int value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0005292B File Offset: 0x00050B2B
		public static implicit operator PrimitiveValue(uint value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00052933 File Offset: 0x00050B33
		public static implicit operator PrimitiveValue(long value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0005293B File Offset: 0x00050B3B
		public static implicit operator PrimitiveValue(ulong value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00052943 File Offset: 0x00050B43
		public static implicit operator PrimitiveValue(float value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0005294B File Offset: 0x00050B4B
		public static implicit operator PrimitiveValue(double value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00052953 File Offset: 0x00050B53
		public static PrimitiveValue FromBoolean(bool value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x0005295B File Offset: 0x00050B5B
		public static PrimitiveValue FromChar(char value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00052963 File Offset: 0x00050B63
		public static PrimitiveValue FromByte(byte value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0005296B File Offset: 0x00050B6B
		public static PrimitiveValue FromSByte(sbyte value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00052973 File Offset: 0x00050B73
		public static PrimitiveValue FromInt16(short value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0005297B File Offset: 0x00050B7B
		public static PrimitiveValue FromUInt16(ushort value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00052983 File Offset: 0x00050B83
		public static PrimitiveValue FromInt32(int value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0005298B File Offset: 0x00050B8B
		public static PrimitiveValue FromUInt32(uint value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00052993 File Offset: 0x00050B93
		public static PrimitiveValue FromInt64(long value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0005299B File Offset: 0x00050B9B
		public static PrimitiveValue FromUInt64(ulong value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x000529A3 File Offset: 0x00050BA3
		public static PrimitiveValue FromSingle(float value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x000529AB File Offset: 0x00050BAB
		public static PrimitiveValue FromDouble(double value)
		{
			return new PrimitiveValue(value);
		}

		// Token: 0x040006D5 RID: 1749
		[FieldOffset(0)]
		private TypeCode m_Type;

		// Token: 0x040006D6 RID: 1750
		[FieldOffset(4)]
		private bool m_BoolValue;

		// Token: 0x040006D7 RID: 1751
		[FieldOffset(4)]
		private char m_CharValue;

		// Token: 0x040006D8 RID: 1752
		[FieldOffset(4)]
		private byte m_ByteValue;

		// Token: 0x040006D9 RID: 1753
		[FieldOffset(4)]
		private sbyte m_SByteValue;

		// Token: 0x040006DA RID: 1754
		[FieldOffset(4)]
		private short m_ShortValue;

		// Token: 0x040006DB RID: 1755
		[FieldOffset(4)]
		private ushort m_UShortValue;

		// Token: 0x040006DC RID: 1756
		[FieldOffset(4)]
		private int m_IntValue;

		// Token: 0x040006DD RID: 1757
		[FieldOffset(4)]
		private uint m_UIntValue;

		// Token: 0x040006DE RID: 1758
		[FieldOffset(4)]
		private long m_LongValue;

		// Token: 0x040006DF RID: 1759
		[FieldOffset(4)]
		private ulong m_ULongValue;

		// Token: 0x040006E0 RID: 1760
		[FieldOffset(4)]
		private float m_FloatValue;

		// Token: 0x040006E1 RID: 1761
		[FieldOffset(4)]
		private double m_DoubleValue;
	}
}
