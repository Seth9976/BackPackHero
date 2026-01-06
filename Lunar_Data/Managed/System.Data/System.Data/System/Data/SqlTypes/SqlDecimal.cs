using System;
using System.Data.Common;
using System.Diagnostics;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a numeric value between - 10^38 +1 and 10^38 - 1, with fixed precision and scale. </summary>
	// Token: 0x020002BD RID: 701
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlDecimal : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06001F79 RID: 8057 RVA: 0x000967CC File Offset: 0x000949CC
		private byte CalculatePrecision()
		{
			int num;
			uint[] array;
			uint num2;
			if (this._data4 != 0U)
			{
				num = 33;
				array = SqlDecimal.s_decimalHelpersHiHi;
				num2 = this._data4;
			}
			else if (this._data3 != 0U)
			{
				num = 24;
				array = SqlDecimal.s_decimalHelpersHi;
				num2 = this._data3;
			}
			else if (this._data2 != 0U)
			{
				num = 15;
				array = SqlDecimal.s_decimalHelpersMid;
				num2 = this._data2;
			}
			else
			{
				num = 5;
				array = SqlDecimal.s_decimalHelpersLo;
				num2 = this._data1;
			}
			if (num2 < array[num])
			{
				num -= 2;
				if (num2 < array[num])
				{
					num -= 2;
					if (num2 < array[num])
					{
						num--;
					}
					else
					{
						num++;
					}
				}
				else
				{
					num++;
				}
			}
			else
			{
				num += 2;
				if (num2 < array[num])
				{
					num--;
				}
				else
				{
					num++;
				}
			}
			if (num2 >= array[num])
			{
				num++;
				if (num == 37 && num2 >= array[num])
				{
					num++;
				}
			}
			byte b = (byte)(num + 1);
			if (b > 1 && this.VerifyPrecision(b - 1))
			{
				b -= 1;
			}
			return Math.Max(b, this._bScale);
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x000968B8 File Offset: 0x00094AB8
		private bool VerifyPrecision(byte precision)
		{
			int num = (int)(checked(precision - 1));
			if (this._data4 < SqlDecimal.s_decimalHelpersHiHi[num])
			{
				return true;
			}
			if (this._data4 == SqlDecimal.s_decimalHelpersHiHi[num])
			{
				if (this._data3 < SqlDecimal.s_decimalHelpersHi[num])
				{
					return true;
				}
				if (this._data3 == SqlDecimal.s_decimalHelpersHi[num])
				{
					if (this._data2 < SqlDecimal.s_decimalHelpersMid[num])
					{
						return true;
					}
					if (this._data2 == SqlDecimal.s_decimalHelpersMid[num] && this._data1 < SqlDecimal.s_decimalHelpersLo[num])
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x0009693C File Offset: 0x00094B3C
		private SqlDecimal(bool fNull)
		{
			this._bLen = (this._bPrec = (this._bScale = 0));
			this._bStatus = 0;
			this._data1 = (this._data2 = (this._data3 = (this._data4 = SqlDecimal.s_uiZero)));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure using the supplied <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Decimal" /> value to be stored as a <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001F7C RID: 8060 RVA: 0x00096990 File Offset: 0x00094B90
		public SqlDecimal(decimal value)
		{
			this._bStatus = SqlDecimal.s_bNotNull;
			int[] bits = decimal.GetBits(value);
			uint num = (uint)bits[3];
			this._data1 = (uint)bits[0];
			this._data2 = (uint)bits[1];
			this._data3 = (uint)bits[2];
			this._data4 = SqlDecimal.s_uiZero;
			this._bStatus |= (((num & 2147483648U) == 2147483648U) ? SqlDecimal.s_bNegative : 0);
			if (this._data3 != 0U)
			{
				this._bLen = 3;
			}
			else if (this._data2 != 0U)
			{
				this._bLen = 2;
			}
			else
			{
				this._bLen = 1;
			}
			this._bScale = (byte)((int)(num & 16711680U) >> 16);
			this._bPrec = 0;
			this._bPrec = this.CalculatePrecision();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure using the supplied integer value.</summary>
		/// <param name="value">The supplied integer value which will the used as the value of the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001F7D RID: 8061 RVA: 0x00096A4C File Offset: 0x00094C4C
		public SqlDecimal(int value)
		{
			this._bStatus = SqlDecimal.s_bNotNull;
			uint num = (uint)value;
			if (value < 0)
			{
				this._bStatus |= SqlDecimal.s_bNegative;
				if (value != -2147483648)
				{
					num = (uint)(-(uint)value);
				}
			}
			this._data1 = num;
			this._data2 = (this._data3 = (this._data4 = SqlDecimal.s_uiZero));
			this._bLen = 1;
			this._bPrec = SqlDecimal.BGetPrecUI4(this._data1);
			this._bScale = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure using the supplied long integer value.</summary>
		/// <param name="value">The supplied long integer value which will the used as the value of the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001F7E RID: 8062 RVA: 0x00096ACC File Offset: 0x00094CCC
		public SqlDecimal(long value)
		{
			this._bStatus = SqlDecimal.s_bNotNull;
			ulong num = (ulong)value;
			if (value < 0L)
			{
				this._bStatus |= SqlDecimal.s_bNegative;
				if (value != -9223372036854775808L)
				{
					num = (ulong)(-(ulong)value);
				}
			}
			this._data1 = (uint)num;
			this._data2 = (uint)(num >> 32);
			this._data3 = (this._data4 = 0U);
			this._bLen = ((this._data2 == 0U) ? 1 : 2);
			this._bPrec = SqlDecimal.BGetPrecUI8(num);
			this._bScale = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure using the supplied parameters.</summary>
		/// <param name="bPrecision">The maximum number of digits that can be used to represent the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property of the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="bScale">The number of decimal places to which the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property will be resolved for the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="fPositive">A Boolean value that indicates whether the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure represents a positive or negative number. </param>
		/// <param name="bits">The 128-bit unsigned integer that provides the value of the new <see cref="T:System.Data.SqlTypes.SqlDecimal" />. </param>
		// Token: 0x06001F7F RID: 8063 RVA: 0x00096B58 File Offset: 0x00094D58
		public SqlDecimal(byte bPrecision, byte bScale, bool fPositive, int[] bits)
		{
			SqlDecimal.CheckValidPrecScale(bPrecision, bScale);
			if (bits == null)
			{
				throw new ArgumentNullException("bits");
			}
			if (bits.Length != 4)
			{
				throw new ArgumentException(SQLResource.InvalidArraySizeMessage, "bits");
			}
			this._bPrec = bPrecision;
			this._bScale = bScale;
			this._data1 = (uint)bits[0];
			this._data2 = (uint)bits[1];
			this._data3 = (uint)bits[2];
			this._data4 = (uint)bits[3];
			this._bLen = 1;
			for (int i = 3; i >= 0; i--)
			{
				if (bits[i] != 0)
				{
					this._bLen = (byte)(i + 1);
					break;
				}
			}
			this._bStatus = SqlDecimal.s_bNotNull;
			if (!fPositive)
			{
				this._bStatus |= SqlDecimal.s_bNegative;
			}
			if (this.FZero())
			{
				this.SetPositive();
			}
			if (bPrecision < this.CalculatePrecision())
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure using the supplied parameters.</summary>
		/// <param name="bPrecision">The maximum number of digits that can be used to represent the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property of the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="bScale">The number of decimal places to which the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property will be resolved for the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="fPositive">A Boolean value that indicates whether the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure represents a positive or negative number. </param>
		/// <param name="data1">An 32-bit unsigned integer which will be combined with data2, data3, and data4 to make up the 128-bit unsigned integer that represents the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structures value. </param>
		/// <param name="data2">An 32-bit unsigned integer which will be combined with data1, data3, and data4 to make up the 128-bit unsigned integer that represents the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structures value. </param>
		/// <param name="data3">An 32-bit unsigned integer which will be combined with data1, data2, and data4 to make up the 128-bit unsigned integer that represents the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structures value. </param>
		/// <param name="data4">An 32-bit unsigned integer which will be combined with data1, data2, and data3 to make up the 128-bit unsigned integer that represents the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structures value. </param>
		// Token: 0x06001F80 RID: 8064 RVA: 0x00096C30 File Offset: 0x00094E30
		public SqlDecimal(byte bPrecision, byte bScale, bool fPositive, int data1, int data2, int data3, int data4)
		{
			SqlDecimal.CheckValidPrecScale(bPrecision, bScale);
			this._bPrec = bPrecision;
			this._bScale = bScale;
			this._data1 = (uint)data1;
			this._data2 = (uint)data2;
			this._data3 = (uint)data3;
			this._data4 = (uint)data4;
			this._bLen = 1;
			if (data4 == 0)
			{
				if (data3 == 0)
				{
					if (data2 == 0)
					{
						this._bLen = 1;
					}
					else
					{
						this._bLen = 2;
					}
				}
				else
				{
					this._bLen = 3;
				}
			}
			else
			{
				this._bLen = 4;
			}
			this._bStatus = SqlDecimal.s_bNotNull;
			if (!fPositive)
			{
				this._bStatus |= SqlDecimal.s_bNegative;
			}
			if (this.FZero())
			{
				this.SetPositive();
			}
			if (bPrecision < this.CalculatePrecision())
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure using the supplied double parameter.</summary>
		/// <param name="dVal">A double, representing the value for the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001F81 RID: 8065 RVA: 0x00096CEC File Offset: 0x00094EEC
		public SqlDecimal(double dVal)
		{
			this = new SqlDecimal(false);
			this._bStatus = SqlDecimal.s_bNotNull;
			if (dVal < 0.0)
			{
				dVal = -dVal;
				this._bStatus |= SqlDecimal.s_bNegative;
			}
			if (dVal >= SqlDecimal.s_DMAX_NUME)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			double num = Math.Floor(dVal);
			double num2 = dVal - num;
			this._bPrec = SqlDecimal.s_NUMERIC_MAX_PRECISION;
			this._bLen = 1;
			if (num > 0.0)
			{
				dVal = Math.Floor(num / SqlDecimal.s_DUINT_BASE);
				this._data1 = (uint)(num - dVal * SqlDecimal.s_DUINT_BASE);
				num = dVal;
				if (num > 0.0)
				{
					dVal = Math.Floor(num / SqlDecimal.s_DUINT_BASE);
					this._data2 = (uint)(num - dVal * SqlDecimal.s_DUINT_BASE);
					num = dVal;
					this._bLen += 1;
					if (num > 0.0)
					{
						dVal = Math.Floor(num / SqlDecimal.s_DUINT_BASE);
						this._data3 = (uint)(num - dVal * SqlDecimal.s_DUINT_BASE);
						num = dVal;
						this._bLen += 1;
						if (num > 0.0)
						{
							dVal = Math.Floor(num / SqlDecimal.s_DUINT_BASE);
							this._data4 = (uint)(num - dVal * SqlDecimal.s_DUINT_BASE);
							this._bLen += 1;
						}
					}
				}
			}
			uint num3 = (uint)(this.FZero() ? 0 : this.CalculatePrecision());
			if (num3 > SqlDecimal.s_DBL_DIG)
			{
				uint num4 = num3 - SqlDecimal.s_DBL_DIG;
				uint num5;
				do
				{
					num5 = this.DivByULong(10U);
					num4 -= 1U;
				}
				while (num4 > 0U);
				num4 = num3 - SqlDecimal.s_DBL_DIG;
				if (num5 >= 5U)
				{
					this.AddULong(1U);
					num3 = (uint)this.CalculatePrecision() + num4;
				}
				do
				{
					this.MultByULong(10U);
					num4 -= 1U;
				}
				while (num4 > 0U);
			}
			this._bScale = (byte)((num3 < SqlDecimal.s_DBL_DIG) ? (SqlDecimal.s_DBL_DIG - num3) : 0U);
			this._bPrec = (byte)(num3 + (uint)this._bScale);
			if (this._bScale > 0)
			{
				num3 = (uint)this._bScale;
				do
				{
					uint num6 = ((num3 >= 9U) ? 9U : num3);
					num2 *= SqlDecimal.s_rgulShiftBase[(int)(num6 - 1U)];
					num3 -= num6;
					this.MultByULong(SqlDecimal.s_rgulShiftBase[(int)(num6 - 1U)]);
					this.AddULong((uint)num2);
					num2 -= Math.Floor(num2);
				}
				while (num3 > 0U);
			}
			if (num2 >= 0.5)
			{
				this.AddULong(1U);
			}
			if (this.FZero())
			{
				this.SetPositive();
			}
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x00096F48 File Offset: 0x00095148
		private SqlDecimal(uint[] rglData, byte bLen, byte bPrec, byte bScale, bool fPositive)
		{
			SqlDecimal.CheckValidPrecScale(bPrec, bScale);
			this._bLen = bLen;
			this._bPrec = bPrec;
			this._bScale = bScale;
			this._data1 = rglData[0];
			this._data2 = rglData[1];
			this._data3 = rglData[2];
			this._data4 = rglData[3];
			this._bStatus = SqlDecimal.s_bNotNull;
			if (!fPositive)
			{
				this._bStatus |= SqlDecimal.s_bNegative;
			}
			if (this.FZero())
			{
				this.SetPositive();
			}
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure is null.</summary>
		/// <returns>true if this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure is null. Otherwise, false. </returns>
		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001F83 RID: 8067 RVA: 0x00096FC7 File Offset: 0x000951C7
		public bool IsNull
		{
			get
			{
				return (this._bStatus & SqlDecimal.s_bNullMask) == SqlDecimal.s_bIsNull;
			}
		}

		/// <summary>Gets the value of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. This property is read-only.</summary>
		/// <returns>A number in the range -79,228,162,514,264,337,593,543,950,335 through 79,228,162,514,162,514,264,337,593,543,950,335.</returns>
		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001F84 RID: 8068 RVA: 0x00096FDC File Offset: 0x000951DC
		public decimal Value
		{
			get
			{
				return this.ToDecimal();
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> of this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure is greater than zero.</summary>
		/// <returns>true if the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> is assigned to null. Otherwise, false.</returns>
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001F85 RID: 8069 RVA: 0x00096FE4 File Offset: 0x000951E4
		public bool IsPositive
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				return (this._bStatus & SqlDecimal.s_bSignMask) == SqlDecimal.s_bPositive;
			}
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x00097007 File Offset: 0x00095207
		private void SetPositive()
		{
			this._bStatus &= SqlDecimal.s_bReverseSignMask;
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x0009701C File Offset: 0x0009521C
		private void SetSignBit(bool fPositive)
		{
			this._bStatus = (fPositive ? (this._bStatus & SqlDecimal.s_bReverseSignMask) : (this._bStatus | SqlDecimal.s_bNegative));
		}

		/// <summary>Gets the maximum number of digits used to represent the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property.</summary>
		/// <returns>The maximum number of digits used to represent the Value of this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</returns>
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x00097042 File Offset: 0x00095242
		public byte Precision
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				return this._bPrec;
			}
		}

		/// <summary>Gets the number of decimal places to which <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> is resolved.</summary>
		/// <returns>The number of decimal places to which the Value property is resolved.</returns>
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001F89 RID: 8073 RVA: 0x00097058 File Offset: 0x00095258
		public byte Scale
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				return this._bScale;
			}
		}

		/// <summary>Gets the binary representation of this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure as an array of integers.</summary>
		/// <returns>An array of integers that contains the binary representation of this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</returns>
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001F8A RID: 8074 RVA: 0x0009706E File Offset: 0x0009526E
		public int[] Data
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				return new int[]
				{
					(int)this._data1,
					(int)this._data2,
					(int)this._data3,
					(int)this._data4
				};
			}
		}

		/// <summary>Get the binary representation of the value of this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure as an array of bytes.</summary>
		/// <returns>An array of bytes that contains the binary representation of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure's value.</returns>
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001F8B RID: 8075 RVA: 0x000970A8 File Offset: 0x000952A8
		public byte[] BinData
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				int num = (int)this._data1;
				int num2 = (int)this._data2;
				int num3 = (int)this._data3;
				int num4 = (int)this._data4;
				byte[] array = new byte[16];
				array[0] = (byte)(num & 255);
				num >>= 8;
				array[1] = (byte)(num & 255);
				num >>= 8;
				array[2] = (byte)(num & 255);
				num >>= 8;
				array[3] = (byte)(num & 255);
				array[4] = (byte)(num2 & 255);
				num2 >>= 8;
				array[5] = (byte)(num2 & 255);
				num2 >>= 8;
				array[6] = (byte)(num2 & 255);
				num2 >>= 8;
				array[7] = (byte)(num2 & 255);
				array[8] = (byte)(num3 & 255);
				num3 >>= 8;
				array[9] = (byte)(num3 & 255);
				num3 >>= 8;
				array[10] = (byte)(num3 & 255);
				num3 >>= 8;
				array[11] = (byte)(num3 & 255);
				array[12] = (byte)(num4 & 255);
				num4 >>= 8;
				array[13] = (byte)(num4 & 255);
				num4 >>= 8;
				array[14] = (byte)(num4 & 255);
				num4 >>= 8;
				array[15] = (byte)(num4 & 255);
				return array;
			}
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.String" />.</summary>
		/// <returns>A new <see cref="T:System.String" /> object that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure's <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property.</returns>
		// Token: 0x06001F8C RID: 8076 RVA: 0x000971D0 File Offset: 0x000953D0
		public override string ToString()
		{
			if (this.IsNull)
			{
				return SQLResource.NullString;
			}
			uint[] array = new uint[] { this._data1, this._data2, this._data3, this._data4 };
			int bLen = (int)this._bLen;
			char[] array2 = new char[(int)(SqlDecimal.s_NUMERIC_MAX_PRECISION + 1)];
			int i = 0;
			while (bLen > 1 || array[0] != 0U)
			{
				uint num;
				SqlDecimal.MpDiv1(array, ref bLen, SqlDecimal.s_ulBase10, out num);
				array2[i++] = SqlDecimal.ChFromDigit(num);
			}
			while (i <= (int)this._bScale)
			{
				array2[i++] = SqlDecimal.ChFromDigit(0U);
			}
			int num2 = 0;
			int num3 = 0;
			if (this._bScale > 0)
			{
				num2 = 1;
			}
			char[] array3;
			if (this.IsPositive)
			{
				array3 = new char[num2 + i];
			}
			else
			{
				array3 = new char[num2 + i + 1];
				array3[num3++] = '-';
			}
			while (i > 0)
			{
				if (i-- == (int)this._bScale)
				{
					array3[num3++] = '.';
				}
				array3[num3++] = array2[i];
			}
			return new string(array3);
		}

		/// <summary>Converts the <see cref="T:System.String" /> representation of a number to its <see cref="T:System.Data.SqlTypes.SqlDecimal" /> equivalent.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> equivalent to the value that is contained in the specified <see cref="T:System.String" />.</returns>
		/// <param name="s">The String to be parsed. </param>
		// Token: 0x06001F8D RID: 8077 RVA: 0x000972E0 File Offset: 0x000954E0
		public static SqlDecimal Parse(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s == SQLResource.NullString)
			{
				return SqlDecimal.Null;
			}
			SqlDecimal @null = SqlDecimal.Null;
			char[] array = s.ToCharArray();
			int num = array.Length;
			int num2 = -1;
			int num3 = 0;
			@null._bPrec = 1;
			@null._bScale = 0;
			@null.SetToZero();
			while (num != 0 && array[num - 1] == ' ')
			{
				num--;
			}
			if (num == 0)
			{
				throw new FormatException(SQLResource.FormatMessage);
			}
			while (array[num3] == ' ')
			{
				num3++;
				num--;
			}
			if (array[num3] == '-')
			{
				@null.SetSignBit(false);
				num3++;
				num--;
			}
			else
			{
				@null.SetSignBit(true);
				if (array[num3] == '+')
				{
					num3++;
					num--;
				}
			}
			while (num > 2 && array[num3] == '0')
			{
				num3++;
				num--;
			}
			if (2 == num && '0' == array[num3] && '.' == array[num3 + 1])
			{
				array[num3] = '.';
				array[num3 + 1] = '0';
			}
			if (num == 0 || num > (int)(SqlDecimal.s_NUMERIC_MAX_PRECISION + 1))
			{
				throw new FormatException(SQLResource.FormatMessage);
			}
			while (num > 1 && array[num3] == '0')
			{
				num3++;
				num--;
			}
			int i;
			for (i = 0; i < num; i++)
			{
				char c = array[num3];
				num3++;
				if (c >= '0' && c <= '9')
				{
					c -= '0';
					@null.MultByULong(SqlDecimal.s_ulBase10);
					@null.AddULong((uint)c);
				}
				else
				{
					if (c != '.' || num2 >= 0)
					{
						throw new FormatException(SQLResource.FormatMessage);
					}
					num2 = i;
				}
			}
			if (num2 < 0)
			{
				@null._bPrec = (byte)i;
				@null._bScale = 0;
			}
			else
			{
				@null._bPrec = (byte)(i - 1);
				@null._bScale = (byte)((int)@null._bPrec - num2);
			}
			if (@null._bPrec > SqlDecimal.s_NUMERIC_MAX_PRECISION)
			{
				throw new FormatException(SQLResource.FormatMessage);
			}
			if (@null._bPrec == 0)
			{
				throw new FormatException(SQLResource.FormatMessage);
			}
			if (@null.FZero())
			{
				@null.SetPositive();
			}
			return @null;
		}

		/// <summary>Returns the a double equal to the contents of the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property of this instance.</summary>
		/// <returns>The decimal representation of the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property.</returns>
		// Token: 0x06001F8E RID: 8078 RVA: 0x000974DC File Offset: 0x000956DC
		public double ToDouble()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			double num = this._data4;
			num = num * (double)SqlDecimal.s_lInt32Base + this._data3;
			num = num * (double)SqlDecimal.s_lInt32Base + this._data2;
			num = num * (double)SqlDecimal.s_lInt32Base + this._data1;
			num /= Math.Pow(10.0, (double)this._bScale);
			if (!this.IsPositive)
			{
				return -num;
			}
			return num;
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x00097564 File Offset: 0x00095764
		private decimal ToDecimal()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			if (this._data4 != 0U || this._bScale > 28)
			{
				throw new OverflowException(SQLResource.ConversionOverflowMessage);
			}
			return new decimal((int)this._data1, (int)this._data2, (int)this._data3, !this.IsPositive, this._bScale);
		}

		/// <summary>Converts the <see cref="T:System.Decimal" /> value to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property equals the value of the Decimal parameter.</returns>
		/// <param name="x">The <see cref="T:System.Decimal" /> value to be converted. </param>
		// Token: 0x06001F90 RID: 8080 RVA: 0x000975C2 File Offset: 0x000957C2
		public static implicit operator SqlDecimal(decimal x)
		{
			return new SqlDecimal(x);
		}

		/// <summary>Converts the <see cref="T:System.Double" /> parameter to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose value equals the value of the <see cref="T:System.Double" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Double" /> structure to be converted.</param>
		// Token: 0x06001F91 RID: 8081 RVA: 0x000975CA File Offset: 0x000957CA
		public static explicit operator SqlDecimal(double x)
		{
			return new SqlDecimal(x);
		}

		/// <summary>Converts the supplied <see cref="T:System.Int64" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property equals the value of the <see cref="T:System.Int64" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Int64" /> structure to be converted.</param>
		// Token: 0x06001F92 RID: 8082 RVA: 0x000975D2 File Offset: 0x000957D2
		public static implicit operator SqlDecimal(long x)
		{
			return new SqlDecimal(new decimal(x));
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter to <see cref="T:System.Decimal" />.</summary>
		/// <returns>A new Decimal structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to be converted. </param>
		// Token: 0x06001F93 RID: 8083 RVA: 0x000975DF File Offset: 0x000957DF
		public static explicit operator decimal(SqlDecimal x)
		{
			return x.Value;
		}

		/// <summary>The unary minus operator negates the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose value contains the results of the negation.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to be negated. </param>
		// Token: 0x06001F94 RID: 8084 RVA: 0x000975E8 File Offset: 0x000957E8
		public static SqlDecimal operator -(SqlDecimal x)
		{
			if (x.IsNull)
			{
				return SqlDecimal.Null;
			}
			SqlDecimal sqlDecimal = x;
			if (sqlDecimal.FZero())
			{
				sqlDecimal.SetPositive();
			}
			else
			{
				sqlDecimal.SetSignBit(!sqlDecimal.IsPositive);
			}
			return sqlDecimal;
		}

		/// <summary>Calculates the sum of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operators.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property contains the sum.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001F95 RID: 8085 RVA: 0x0009762C File Offset: 0x0009582C
		public static SqlDecimal operator +(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDecimal.Null;
			}
			bool flag = true;
			bool flag2 = x.IsPositive;
			bool flag3 = y.IsPositive;
			int bScale = (int)x._bScale;
			int bScale2 = (int)y._bScale;
			int num = Math.Max((int)x._bPrec - bScale, (int)y._bPrec - bScale2);
			int num2 = Math.Max(bScale, bScale2);
			int num3 = num + num2 + 1;
			num3 = Math.Min((int)SqlDecimal.MaxPrecision, num3);
			if (num3 - num < num2)
			{
				num2 = num3 - num;
			}
			if (bScale != num2)
			{
				x.AdjustScale(num2 - bScale, true);
			}
			if (bScale2 != num2)
			{
				y.AdjustScale(num2 - bScale2, true);
			}
			if (!flag2)
			{
				flag2 = !flag2;
				flag3 = !flag3;
				flag = !flag;
			}
			int num4 = (int)x._bLen;
			int num5 = (int)y._bLen;
			uint[] array = new uint[] { x._data1, x._data2, x._data3, x._data4 };
			uint[] array2 = new uint[] { y._data1, y._data2, y._data3, y._data4 };
			byte b;
			if (flag3)
			{
				ulong num6 = 0UL;
				int num7 = 0;
				while (num7 < num4 || num7 < num5)
				{
					if (num7 < num4)
					{
						num6 += (ulong)array[num7];
					}
					if (num7 < num5)
					{
						num6 += (ulong)array2[num7];
					}
					array[num7] = (uint)num6;
					num6 >>= 32;
					num7++;
				}
				if (num6 != 0UL)
				{
					if (num7 == SqlDecimal.s_cNumeMax)
					{
						throw new OverflowException(SQLResource.ArithOverflowMessage);
					}
					array[num7] = (uint)num6;
					num7++;
				}
				b = (byte)num7;
			}
			else
			{
				int num8 = 0;
				if (x.LAbsCmp(y) < 0)
				{
					flag = !flag;
					uint[] array3 = array2;
					array2 = array;
					array = array3;
					num4 = num5;
					num5 = (int)x._bLen;
				}
				ulong num6 = SqlDecimal.s_ulInt32Base;
				int num7 = 0;
				while (num7 < num4 || num7 < num5)
				{
					if (num7 < num4)
					{
						num6 += (ulong)array[num7];
					}
					if (num7 < num5)
					{
						num6 -= (ulong)array2[num7];
					}
					array[num7] = (uint)num6;
					if (array[num7] != 0U)
					{
						num8 = num7;
					}
					num6 >>= 32;
					num6 += SqlDecimal.s_ulInt32BaseForMod;
					num7++;
				}
				b = (byte)(num8 + 1);
			}
			SqlDecimal sqlDecimal = new SqlDecimal(array, b, (byte)num3, (byte)num2, flag);
			if (sqlDecimal.FGt10_38() || sqlDecimal.CalculatePrecision() > SqlDecimal.s_NUMERIC_MAX_PRECISION)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (sqlDecimal.FZero())
			{
				sqlDecimal.SetPositive();
			}
			return sqlDecimal;
		}

		/// <summary>Calculates the results of subtracting the second <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operand from the first.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose Value property contains the results of the subtraction.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001F96 RID: 8086 RVA: 0x000978A1 File Offset: 0x00095AA1
		public static SqlDecimal operator -(SqlDecimal x, SqlDecimal y)
		{
			return x + -y;
		}

		/// <summary>The multiplication operator computes the product of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameters.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property contains the product of the multiplication.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001F97 RID: 8087 RVA: 0x000978B0 File Offset: 0x00095AB0
		public static SqlDecimal operator *(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDecimal.Null;
			}
			int bLen = (int)y._bLen;
			int num = (int)(x._bScale + y._bScale);
			int num2 = num;
			int num3 = (int)(x._bPrec - x._bScale + (y._bPrec - y._bScale) + 1);
			int num4 = num2 + num3;
			if (num4 > (int)SqlDecimal.s_NUMERIC_MAX_PRECISION)
			{
				num4 = (int)SqlDecimal.s_NUMERIC_MAX_PRECISION;
			}
			if (num2 > (int)SqlDecimal.s_NUMERIC_MAX_PRECISION)
			{
				num2 = (int)SqlDecimal.s_NUMERIC_MAX_PRECISION;
			}
			num2 = Math.Min(num4 - num3, num2);
			num2 = Math.Max(num2, Math.Min(num, (int)SqlDecimal.s_cNumeDivScaleMin));
			int num5 = num2 - num;
			bool flag = x.IsPositive == y.IsPositive;
			uint[] array = new uint[] { x._data1, x._data2, x._data3, x._data4 };
			uint[] array2 = new uint[] { y._data1, y._data2, y._data3, y._data4 };
			uint[] array3 = new uint[9];
			int i = 0;
			for (int j = 0; j < (int)x._bLen; j++)
			{
				uint num6 = array[j];
				ulong num7 = 0UL;
				i = j;
				for (int k = 0; k < bLen; k++)
				{
					ulong num8 = num7 + (ulong)array3[i];
					ulong num9 = (ulong)array2[k];
					num7 = (ulong)num6 * num9;
					num7 += num8;
					if (num7 < num8)
					{
						num8 = SqlDecimal.s_ulInt32Base;
					}
					else
					{
						num8 = 0UL;
					}
					array3[i++] = (uint)num7;
					num7 = (num7 >> 32) + num8;
				}
				if (num7 != 0UL)
				{
					array3[i++] = (uint)num7;
				}
			}
			while (array3[i] == 0U && i > 0)
			{
				i--;
			}
			int num10 = i + 1;
			if (num5 != 0)
			{
				if (num5 < 0)
				{
					uint num11;
					uint num12;
					do
					{
						if (num5 <= -9)
						{
							num11 = SqlDecimal.s_rgulShiftBase[8];
							num5 += 9;
						}
						else
						{
							num11 = SqlDecimal.s_rgulShiftBase[-num5 - 1];
							num5 = 0;
						}
						SqlDecimal.MpDiv1(array3, ref num10, num11, out num12);
					}
					while (num5 != 0);
					if (num10 > SqlDecimal.s_cNumeMax)
					{
						throw new OverflowException(SQLResource.ArithOverflowMessage);
					}
					for (i = num10; i < SqlDecimal.s_cNumeMax; i++)
					{
						array3[i] = 0U;
					}
					SqlDecimal sqlDecimal = new SqlDecimal(array3, (byte)num10, (byte)num4, (byte)num2, flag);
					if (sqlDecimal.FGt10_38())
					{
						throw new OverflowException(SQLResource.ArithOverflowMessage);
					}
					if (num12 >= num11 / 2U)
					{
						sqlDecimal.AddULong(1U);
					}
					if (sqlDecimal.FZero())
					{
						sqlDecimal.SetPositive();
					}
					return sqlDecimal;
				}
				else
				{
					if (num10 > SqlDecimal.s_cNumeMax)
					{
						throw new OverflowException(SQLResource.ArithOverflowMessage);
					}
					for (i = num10; i < SqlDecimal.s_cNumeMax; i++)
					{
						array3[i] = 0U;
					}
					SqlDecimal sqlDecimal = new SqlDecimal(array3, (byte)num10, (byte)num4, (byte)num, flag);
					if (sqlDecimal.FZero())
					{
						sqlDecimal.SetPositive();
					}
					sqlDecimal.AdjustScale(num5, true);
					return sqlDecimal;
				}
			}
			else
			{
				if (num10 > SqlDecimal.s_cNumeMax)
				{
					throw new OverflowException(SQLResource.ArithOverflowMessage);
				}
				for (i = num10; i < SqlDecimal.s_cNumeMax; i++)
				{
					array3[i] = 0U;
				}
				SqlDecimal sqlDecimal = new SqlDecimal(array3, (byte)num10, (byte)num4, (byte)num2, flag);
				if (sqlDecimal.FGt10_38())
				{
					throw new OverflowException(SQLResource.ArithOverflowMessage);
				}
				if (sqlDecimal.FZero())
				{
					sqlDecimal.SetPositive();
				}
				return sqlDecimal;
			}
		}

		/// <summary>The division operator calculates the results of dividing the first <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operand by the second.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property contains the results of the division.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001F98 RID: 8088 RVA: 0x00097BF4 File Offset: 0x00095DF4
		public static SqlDecimal operator /(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDecimal.Null;
			}
			if (y.FZero())
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			bool flag = x.IsPositive == y.IsPositive;
			int num = Math.Max((int)(x._bScale + y._bPrec + 1), (int)SqlDecimal.s_cNumeDivScaleMin);
			int num2 = (int)(x._bPrec - x._bScale + y._bScale);
			int num3 = num + (int)x._bPrec + (int)y._bPrec + 1;
			int num4 = Math.Min(num, (int)SqlDecimal.s_cNumeDivScaleMin);
			num2 = Math.Min(num2, (int)SqlDecimal.s_NUMERIC_MAX_PRECISION);
			num3 = num2 + num;
			if (num3 > (int)SqlDecimal.s_NUMERIC_MAX_PRECISION)
			{
				num3 = (int)SqlDecimal.s_NUMERIC_MAX_PRECISION;
			}
			num = Math.Min(num3 - num2, num);
			num = Math.Max(num, num4);
			int num5 = num - (int)x._bScale + (int)y._bScale;
			x.AdjustScale(num5, true);
			uint[] array = new uint[] { x._data1, x._data2, x._data3, x._data4 };
			uint[] array2 = new uint[] { y._data1, y._data2, y._data3, y._data4 };
			uint[] array3 = new uint[SqlDecimal.s_cNumeMax + 1];
			uint[] array4 = new uint[SqlDecimal.s_cNumeMax];
			int num6;
			int num7;
			SqlDecimal.MpDiv(array, (int)x._bLen, array2, (int)y._bLen, array4, out num6, array3, out num7);
			SqlDecimal.ZeroToMaxLen(array4, num6);
			SqlDecimal sqlDecimal = new SqlDecimal(array4, (byte)num6, (byte)num3, (byte)num, flag);
			if (sqlDecimal.FZero())
			{
				sqlDecimal.SetPositive();
			}
			return sqlDecimal;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to be converted. </param>
		// Token: 0x06001F99 RID: 8089 RVA: 0x00097D93 File Offset: 0x00095F93
		public static explicit operator SqlDecimal(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal((int)x.ByteValue);
			}
			return SqlDecimal.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to be converted. </param>
		// Token: 0x06001F9A RID: 8090 RVA: 0x00097DB0 File Offset: 0x00095FB0
		public static implicit operator SqlDecimal(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal((int)x.Value);
			}
			return SqlDecimal.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" /></summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to be converted. </param>
		// Token: 0x06001F9B RID: 8091 RVA: 0x00097DCD File Offset: 0x00095FCD
		public static implicit operator SqlDecimal(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal((int)x.Value);
			}
			return SqlDecimal.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to be converted. </param>
		// Token: 0x06001F9C RID: 8092 RVA: 0x00097DEA File Offset: 0x00095FEA
		public static implicit operator SqlDecimal(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal(x.Value);
			}
			return SqlDecimal.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to SqlDecimal.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> equals the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to be converted. </param>
		// Token: 0x06001F9D RID: 8093 RVA: 0x00097E07 File Offset: 0x00096007
		public static implicit operator SqlDecimal(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal(x.Value);
			}
			return SqlDecimal.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlMoney" /> operand to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> equals the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to be converted. </param>
		// Token: 0x06001F9E RID: 8094 RVA: 0x00097E24 File Offset: 0x00096024
		public static implicit operator SqlDecimal(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal(x.ToDecimal());
			}
			return SqlDecimal.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to be converted. </param>
		// Token: 0x06001F9F RID: 8095 RVA: 0x00097E41 File Offset: 0x00096041
		public static explicit operator SqlDecimal(SqlSingle x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal((double)x.Value);
			}
			return SqlDecimal.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> equals the <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to be converted. </param>
		// Token: 0x06001FA0 RID: 8096 RVA: 0x00097E5F File Offset: 0x0009605F
		public static explicit operator SqlDecimal(SqlDouble x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal(x.Value);
			}
			return SqlDecimal.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlString" /> parameter to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> equals the value represented by the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlString" /> object to be converted. </param>
		// Token: 0x06001FA1 RID: 8097 RVA: 0x00097E7C File Offset: 0x0009607C
		public static explicit operator SqlDecimal(SqlString x)
		{
			if (!x.IsNull)
			{
				return SqlDecimal.Parse(x.Value);
			}
			return SqlDecimal.Null;
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x00097E9C File Offset: 0x0009609C
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			if (this.IsNull)
			{
				return;
			}
			object obj = (new uint[] { this._data1, this._data2, this._data3, this._data4 })[(int)(this._bLen - 1)];
			for (int i = (int)this._bLen; i < SqlDecimal.s_cNumeMax; i++)
			{
			}
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x00097EFC File Offset: 0x000960FC
		private static void ZeroToMaxLen(uint[] rgulData, int cUI4sCur)
		{
			switch (cUI4sCur)
			{
			case 1:
				rgulData[1] = (rgulData[2] = (rgulData[3] = 0U));
				return;
			case 2:
				rgulData[2] = (rgulData[3] = 0U);
				return;
			case 3:
				rgulData[3] = 0U;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x00097F3E File Offset: 0x0009613E
		private static byte CLenFromPrec(byte bPrec)
		{
			return SqlDecimal.s_rgCLenFromPrec[(int)(bPrec - 1)];
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x00097F49 File Offset: 0x00096149
		private bool FZero()
		{
			return this._data1 == 0U && this._bLen <= 1;
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x00097F64 File Offset: 0x00096164
		private bool FGt10_38()
		{
			return (ulong)this._data4 >= 1262177448UL && this._bLen == 4 && ((ulong)this._data4 > 1262177448UL || (ulong)this._data3 > 1518781562UL || ((ulong)this._data3 == 1518781562UL && (ulong)this._data2 >= 160047680UL));
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x00097FD0 File Offset: 0x000961D0
		private bool FGt10_38(uint[] rglData)
		{
			return (ulong)rglData[3] >= 1262177448UL && ((ulong)rglData[3] > 1262177448UL || (ulong)rglData[2] > 1518781562UL || ((ulong)rglData[2] == 1518781562UL && (ulong)rglData[1] >= 160047680UL));
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x00098024 File Offset: 0x00096224
		private static byte BGetPrecUI4(uint value)
		{
			int num;
			if (value < SqlDecimal.s_ulT4)
			{
				if (value < SqlDecimal.s_ulT2)
				{
					num = ((value >= SqlDecimal.s_ulT1) ? 2 : 1);
				}
				else
				{
					num = ((value >= SqlDecimal.s_ulT3) ? 4 : 3);
				}
			}
			else if (value < SqlDecimal.s_ulT8)
			{
				if (value < SqlDecimal.s_ulT6)
				{
					num = ((value >= SqlDecimal.s_ulT5) ? 6 : 5);
				}
				else
				{
					num = ((value >= SqlDecimal.s_ulT7) ? 8 : 7);
				}
			}
			else
			{
				num = ((value >= SqlDecimal.s_ulT9) ? 10 : 9);
			}
			return (byte)num;
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x0009809E File Offset: 0x0009629E
		private static byte BGetPrecUI8(uint ulU0, uint ulU1)
		{
			return SqlDecimal.BGetPrecUI8((ulong)ulU0 + ((ulong)ulU1 << 32));
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x000980B0 File Offset: 0x000962B0
		private static byte BGetPrecUI8(ulong dwlVal)
		{
			int num2;
			if (dwlVal < (ulong)SqlDecimal.s_ulT8)
			{
				uint num = (uint)dwlVal;
				if (num < SqlDecimal.s_ulT4)
				{
					if (num < SqlDecimal.s_ulT2)
					{
						num2 = ((num >= SqlDecimal.s_ulT1) ? 2 : 1);
					}
					else
					{
						num2 = ((num >= SqlDecimal.s_ulT3) ? 4 : 3);
					}
				}
				else if (num < SqlDecimal.s_ulT6)
				{
					num2 = ((num >= SqlDecimal.s_ulT5) ? 6 : 5);
				}
				else
				{
					num2 = ((num >= SqlDecimal.s_ulT7) ? 8 : 7);
				}
			}
			else if (dwlVal < SqlDecimal.s_dwlT16)
			{
				if (dwlVal < SqlDecimal.s_dwlT12)
				{
					if (dwlVal < SqlDecimal.s_dwlT10)
					{
						num2 = ((dwlVal >= (ulong)SqlDecimal.s_ulT9) ? 10 : 9);
					}
					else
					{
						num2 = ((dwlVal >= SqlDecimal.s_dwlT11) ? 12 : 11);
					}
				}
				else if (dwlVal < SqlDecimal.s_dwlT14)
				{
					num2 = ((dwlVal >= SqlDecimal.s_dwlT13) ? 14 : 13);
				}
				else
				{
					num2 = ((dwlVal >= SqlDecimal.s_dwlT15) ? 16 : 15);
				}
			}
			else if (dwlVal < SqlDecimal.s_dwlT18)
			{
				num2 = ((dwlVal >= SqlDecimal.s_dwlT17) ? 18 : 17);
			}
			else
			{
				num2 = ((dwlVal >= SqlDecimal.s_dwlT19) ? 20 : 19);
			}
			return (byte)num2;
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x000981B8 File Offset: 0x000963B8
		private void AddULong(uint ulAdd)
		{
			ulong num = (ulong)ulAdd;
			int bLen = (int)this._bLen;
			uint[] array = new uint[] { this._data1, this._data2, this._data3, this._data4 };
			int num2 = 0;
			for (;;)
			{
				num += (ulong)array[num2];
				array[num2] = (uint)num;
				num >>= 32;
				if (num == 0UL)
				{
					break;
				}
				num2++;
				if (num2 >= bLen)
				{
					goto Block_2;
				}
			}
			this.StoreFromWorkingArray(array);
			return;
			Block_2:
			if (num2 == SqlDecimal.s_cNumeMax)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			array[num2] = (uint)num;
			this._bLen += 1;
			if (this.FGt10_38(array))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			this.StoreFromWorkingArray(array);
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x00098264 File Offset: 0x00096464
		private void MultByULong(uint uiMultiplier)
		{
			int bLen = (int)this._bLen;
			ulong num = 0UL;
			uint[] array = new uint[] { this._data1, this._data2, this._data3, this._data4 };
			for (int i = 0; i < bLen; i++)
			{
				ulong num2 = (ulong)array[i] * (ulong)uiMultiplier;
				num += num2;
				if (num < num2)
				{
					num2 = SqlDecimal.s_ulInt32Base;
				}
				else
				{
					num2 = 0UL;
				}
				array[i] = (uint)num;
				num = (num >> 32) + num2;
			}
			if (num != 0UL)
			{
				if (bLen == SqlDecimal.s_cNumeMax)
				{
					throw new OverflowException(SQLResource.ArithOverflowMessage);
				}
				array[bLen] = (uint)num;
				this._bLen += 1;
			}
			if (this.FGt10_38(array))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			this.StoreFromWorkingArray(array);
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x00098328 File Offset: 0x00096528
		private uint DivByULong(uint iDivisor)
		{
			ulong num = (ulong)iDivisor;
			ulong num2 = 0UL;
			bool flag = true;
			if (num == 0UL)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			uint[] array = new uint[] { this._data1, this._data2, this._data3, this._data4 };
			for (int i = (int)this._bLen; i > 0; i--)
			{
				num2 = (num2 << 32) + (ulong)array[i - 1];
				uint num3 = (uint)(num2 / num);
				array[i - 1] = num3;
				num2 %= num;
				if (flag && num3 == 0U)
				{
					this._bLen -= 1;
				}
				else
				{
					flag = false;
				}
			}
			this.StoreFromWorkingArray(array);
			if (flag)
			{
				this._bLen = 1;
			}
			return (uint)num2;
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x000983DC File Offset: 0x000965DC
		internal void AdjustScale(int digits, bool fRound)
		{
			bool flag = false;
			int i = digits;
			if (i + (int)this._bScale < 0)
			{
				throw new SqlTruncateException();
			}
			if (i + (int)this._bScale > (int)SqlDecimal.s_NUMERIC_MAX_PRECISION)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			byte b = (byte)(i + (int)this._bScale);
			byte b2 = (byte)Math.Min((int)SqlDecimal.s_NUMERIC_MAX_PRECISION, Math.Max(1, i + (int)this._bPrec));
			if (i > 0)
			{
				this._bScale = b;
				this._bPrec = b2;
				while (i > 0)
				{
					uint num;
					if (i >= 9)
					{
						num = SqlDecimal.s_rgulShiftBase[8];
						i -= 9;
					}
					else
					{
						num = SqlDecimal.s_rgulShiftBase[i - 1];
						i = 0;
					}
					this.MultByULong(num);
				}
			}
			else if (i < 0)
			{
				uint num;
				uint num2;
				do
				{
					if (i <= -9)
					{
						num = SqlDecimal.s_rgulShiftBase[8];
						i += 9;
					}
					else
					{
						num = SqlDecimal.s_rgulShiftBase[-i - 1];
						i = 0;
					}
					num2 = this.DivByULong(num);
				}
				while (i < 0);
				flag = num2 >= num / 2U;
				this._bScale = b;
				this._bPrec = b2;
			}
			if (flag && fRound)
			{
				this.AddULong(1U);
				return;
			}
			if (this.FZero())
			{
				this.SetPositive();
			}
		}

		/// <summary>The scale of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operand will be adjusted to the number of digits indicated by the digits parameter. Depending on the value of the fRound parameter, the value will either be rounded to the appropriate number of digits or truncated.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property contains the adjusted number.</returns>
		/// <param name="n">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to be adjusted. </param>
		/// <param name="digits">The number of digits in the adjusted structure. </param>
		/// <param name="fRound">If this parameter is true, the new Value will be rounded, if false, the value will be truncated. </param>
		// Token: 0x06001FAF RID: 8111 RVA: 0x000984F8 File Offset: 0x000966F8
		public static SqlDecimal AdjustScale(SqlDecimal n, int digits, bool fRound)
		{
			if (n.IsNull)
			{
				return SqlDecimal.Null;
			}
			SqlDecimal sqlDecimal = n;
			sqlDecimal.AdjustScale(digits, fRound);
			return sqlDecimal;
		}

		/// <summary>Adjusts the value of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operand to the indicated precision and scale.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose Value has been adjusted to the precision and scale indicated in the parameters.</returns>
		/// <param name="n">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose value is to be adjusted. </param>
		/// <param name="precision">The precision for the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="scale">The scale for the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001FB0 RID: 8112 RVA: 0x00098520 File Offset: 0x00096720
		public static SqlDecimal ConvertToPrecScale(SqlDecimal n, int precision, int scale)
		{
			SqlDecimal.CheckValidPrecScale(precision, scale);
			if (n.IsNull)
			{
				return SqlDecimal.Null;
			}
			SqlDecimal sqlDecimal = n;
			int num = scale - (int)sqlDecimal._bScale;
			sqlDecimal.AdjustScale(num, true);
			byte b = SqlDecimal.CLenFromPrec((byte)precision);
			if (b < sqlDecimal._bLen)
			{
				throw new SqlTruncateException();
			}
			if (b == sqlDecimal._bLen && precision < (int)sqlDecimal.CalculatePrecision())
			{
				throw new SqlTruncateException();
			}
			sqlDecimal._bPrec = (byte)precision;
			return sqlDecimal;
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x00098594 File Offset: 0x00096794
		private int LAbsCmp(SqlDecimal snumOp)
		{
			int bLen = (int)snumOp._bLen;
			int bLen2 = (int)this._bLen;
			if (bLen != bLen2)
			{
				if (bLen2 <= bLen)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				uint[] array = new uint[] { this._data1, this._data2, this._data3, this._data4 };
				uint[] array2 = new uint[] { snumOp._data1, snumOp._data2, snumOp._data3, snumOp._data4 };
				int num = bLen - 1;
				while (array[num] == array2[num])
				{
					num--;
					if (num < 0)
					{
						return 0;
					}
				}
				if (array[num] <= array2[num])
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x00098638 File Offset: 0x00096838
		private static void MpMove(uint[] rgulS, int ciulS, uint[] rgulD, out int ciulD)
		{
			ciulD = ciulS;
			for (int i = 0; i < ciulS; i++)
			{
				rgulD[i] = rgulS[i];
			}
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x0009865A File Offset: 0x0009685A
		private static void MpSet(uint[] rgulD, out int ciulD, uint iulN)
		{
			ciulD = 1;
			rgulD[0] = iulN;
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x00098663 File Offset: 0x00096863
		private static void MpNormalize(uint[] rgulU, ref int ciulU)
		{
			while (ciulU > 1 && rgulU[ciulU - 1] == 0U)
			{
				ciulU--;
			}
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x0009867C File Offset: 0x0009687C
		private static void MpMul1(uint[] piulD, ref int ciulD, uint iulX)
		{
			uint num = 0U;
			int i;
			for (i = 0; i < ciulD; i++)
			{
				ulong num2 = (ulong)piulD[i];
				ulong num3 = (ulong)num + num2 * (ulong)iulX;
				num = SqlDecimal.HI(num3);
				piulD[i] = SqlDecimal.LO(num3);
			}
			if (num != 0U)
			{
				piulD[i] = num;
				ciulD++;
			}
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x000986C4 File Offset: 0x000968C4
		private static void MpDiv1(uint[] rgulU, ref int ciulU, uint iulD, out uint iulR)
		{
			uint num = 0U;
			ulong num2 = (ulong)iulD;
			int i = ciulU;
			while (i > 0)
			{
				i--;
				ulong num3 = ((ulong)num << 32) + (ulong)rgulU[i];
				rgulU[i] = (uint)(num3 / num2);
				num = (uint)(num3 - (ulong)rgulU[i] * num2);
			}
			iulR = num;
			SqlDecimal.MpNormalize(rgulU, ref ciulU);
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x00098709 File Offset: 0x00096909
		internal static ulong DWL(uint lo, uint hi)
		{
			return (ulong)lo + ((ulong)hi << 32);
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x00098713 File Offset: 0x00096913
		private static uint HI(ulong x)
		{
			return (uint)(x >> 32);
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x0009871A File Offset: 0x0009691A
		private static uint LO(ulong x)
		{
			return (uint)x;
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x00098720 File Offset: 0x00096920
		private static void MpDiv(uint[] rgulU, int ciulU, uint[] rgulD, int ciulD, uint[] rgulQ, out int ciulQ, uint[] rgulR, out int ciulR)
		{
			if (ciulD == 1 && rgulD[0] == 0U)
			{
				ciulQ = (ciulR = 0);
				return;
			}
			if (ciulU == 1 && ciulD == 1)
			{
				SqlDecimal.MpSet(rgulQ, out ciulQ, rgulU[0] / rgulD[0]);
				SqlDecimal.MpSet(rgulR, out ciulR, rgulU[0] % rgulD[0]);
				return;
			}
			if (ciulD > ciulU)
			{
				SqlDecimal.MpMove(rgulU, ciulU, rgulR, out ciulR);
				SqlDecimal.MpSet(rgulQ, out ciulQ, 0U);
				return;
			}
			if (ciulU <= 2)
			{
				ulong num = SqlDecimal.DWL(rgulU[0], rgulU[1]);
				ulong num2 = (ulong)rgulD[0];
				if (ciulD > 1)
				{
					num2 += (ulong)rgulD[1] << 32;
				}
				ulong num3 = num / num2;
				rgulQ[0] = SqlDecimal.LO(num3);
				rgulQ[1] = SqlDecimal.HI(num3);
				ciulQ = ((SqlDecimal.HI(num3) != 0U) ? 2 : 1);
				num3 = num % num2;
				rgulR[0] = SqlDecimal.LO(num3);
				rgulR[1] = SqlDecimal.HI(num3);
				ciulR = ((SqlDecimal.HI(num3) != 0U) ? 2 : 1);
				return;
			}
			if (ciulD == 1)
			{
				SqlDecimal.MpMove(rgulU, ciulU, rgulQ, out ciulQ);
				uint num4;
				SqlDecimal.MpDiv1(rgulQ, ref ciulQ, rgulD[0], out num4);
				rgulR[0] = num4;
				ciulR = 1;
				return;
			}
			ciulQ = (ciulR = 0);
			if (rgulU != rgulR)
			{
				SqlDecimal.MpMove(rgulU, ciulU, rgulR, out ciulR);
			}
			ciulQ = ciulU - ciulD + 1;
			uint num5 = rgulD[ciulD - 1];
			rgulR[ciulU] = 0U;
			int num6 = ciulU;
			uint num7 = (uint)(SqlDecimal.s_ulInt32Base / ((ulong)num5 + 1UL));
			if (num7 > 1U)
			{
				SqlDecimal.MpMul1(rgulD, ref ciulD, num7);
				num5 = rgulD[ciulD - 1];
				SqlDecimal.MpMul1(rgulR, ref ciulR, num7);
			}
			uint num8 = rgulD[ciulD - 2];
			do
			{
				ulong num9 = SqlDecimal.DWL(rgulR[num6 - 1], rgulR[num6]);
				uint num10;
				if (num5 == rgulR[num6])
				{
					num10 = (uint)(SqlDecimal.s_ulInt32Base - 1UL);
				}
				else
				{
					num10 = (uint)(num9 / (ulong)num5);
				}
				ulong num11 = (ulong)num10;
				uint num12 = (uint)(num9 - num11 * (ulong)num5);
				while ((ulong)num8 * num11 > SqlDecimal.DWL(rgulR[num6 - 2], num12))
				{
					num10 -= 1U;
					if (num12 >= -num5)
					{
						break;
					}
					num12 += num5;
					num11 = (ulong)num10;
				}
				num9 = SqlDecimal.s_ulInt32Base;
				ulong num13 = 0UL;
				int i = 0;
				int num14 = num6 - ciulD;
				while (i < ciulD)
				{
					ulong num15 = (ulong)rgulD[i];
					num13 += (ulong)num10 * num15;
					num9 += (ulong)rgulR[num14] - (ulong)SqlDecimal.LO(num13);
					num13 = (ulong)SqlDecimal.HI(num13);
					rgulR[num14] = SqlDecimal.LO(num9);
					num9 = (ulong)SqlDecimal.HI(num9) + SqlDecimal.s_ulInt32Base - 1UL;
					i++;
					num14++;
				}
				num9 += (ulong)rgulR[num14] - num13;
				rgulR[num14] = SqlDecimal.LO(num9);
				rgulQ[num6 - ciulD] = num10;
				if (SqlDecimal.HI(num9) == 0U)
				{
					rgulQ[num6 - ciulD] = num10 - 1U;
					uint num16 = 0U;
					i = 0;
					num14 = num6 - ciulD;
					while (i < ciulD)
					{
						num9 = (ulong)rgulD[i] + (ulong)rgulR[num14] + (ulong)num16;
						num16 = SqlDecimal.HI(num9);
						rgulR[num14] = SqlDecimal.LO(num9);
						i++;
						num14++;
					}
					rgulR[num14] += num16;
				}
				num6--;
			}
			while (num6 >= ciulD);
			SqlDecimal.MpNormalize(rgulQ, ref ciulQ);
			ciulR = ciulD;
			SqlDecimal.MpNormalize(rgulR, ref ciulR);
			if (num7 > 1U)
			{
				uint num17;
				SqlDecimal.MpDiv1(rgulD, ref ciulD, num7, out num17);
				SqlDecimal.MpDiv1(rgulR, ref ciulR, num7, out num17);
			}
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x00098A3C File Offset: 0x00096C3C
		private EComparison CompareNm(SqlDecimal snumOp)
		{
			int num = (this.IsPositive ? 1 : (-1));
			int num2 = (snumOp.IsPositive ? 1 : (-1));
			if (num == num2)
			{
				SqlDecimal sqlDecimal = this;
				SqlDecimal sqlDecimal2 = snumOp;
				int num3 = (int)(this._bScale - snumOp._bScale);
				if (num3 < 0)
				{
					try
					{
						sqlDecimal.AdjustScale(-num3, true);
						goto IL_0078;
					}
					catch (OverflowException)
					{
						return (num > 0) ? EComparison.GT : EComparison.LT;
					}
				}
				if (num3 > 0)
				{
					try
					{
						sqlDecimal2.AdjustScale(num3, true);
					}
					catch (OverflowException)
					{
						return (num > 0) ? EComparison.LT : EComparison.GT;
					}
				}
				IL_0078:
				int num4 = sqlDecimal.LAbsCmp(sqlDecimal2);
				if (num4 == 0)
				{
					return EComparison.EQ;
				}
				if (num * num4 < 0)
				{
					return EComparison.LT;
				}
				return EComparison.GT;
			}
			if (num != 1)
			{
				return EComparison.LT;
			}
			return EComparison.GT;
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x00098AFC File Offset: 0x00096CFC
		private static void CheckValidPrecScale(byte bPrec, byte bScale)
		{
			if (bPrec < 1 || bPrec > SqlDecimal.MaxPrecision || bScale < 0 || bScale > SqlDecimal.MaxScale || bScale > bPrec)
			{
				throw new SqlTypeException(SQLResource.InvalidPrecScaleMessage);
			}
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x00098AFC File Offset: 0x00096CFC
		private static void CheckValidPrecScale(int iPrec, int iScale)
		{
			if (iPrec < 1 || iPrec > (int)SqlDecimal.MaxPrecision || iScale < 0 || iScale > (int)SqlDecimal.MaxScale || iScale > iPrec)
			{
				throw new SqlTypeException(SQLResource.InvalidPrecScaleMessage);
			}
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operands to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001FBE RID: 8126 RVA: 0x00098B25 File Offset: 0x00096D25
		public static SqlBoolean operator ==(SqlDecimal x, SqlDecimal y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.CompareNm(y) == EComparison.EQ);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameters to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001FBF RID: 8127 RVA: 0x00098B4F File Offset: 0x00096D4F
		public static SqlBoolean operator !=(SqlDecimal x, SqlDecimal y)
		{
			return !(x == y);
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structures to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001FC0 RID: 8128 RVA: 0x00098B5D File Offset: 0x00096D5D
		public static SqlBoolean operator <(SqlDecimal x, SqlDecimal y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.CompareNm(y) == EComparison.LT);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structures to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001FC1 RID: 8129 RVA: 0x00098B87 File Offset: 0x00096D87
		public static SqlBoolean operator >(SqlDecimal x, SqlDecimal y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.CompareNm(y) == EComparison.GT);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameters to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001FC2 RID: 8130 RVA: 0x00098BB4 File Offset: 0x00096DB4
		public static SqlBoolean operator <=(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			EComparison ecomparison = x.CompareNm(y);
			return new SqlBoolean(ecomparison == EComparison.LT || ecomparison == EComparison.EQ);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameters to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001FC3 RID: 8131 RVA: 0x00098BF4 File Offset: 0x00096DF4
		public static SqlBoolean operator >=(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			EComparison ecomparison = x.CompareNm(y);
			return new SqlBoolean(ecomparison == EComparison.GT || ecomparison == EComparison.EQ);
		}

		/// <summary>Calculates the sum of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operators.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property contains the sum.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001FC4 RID: 8132 RVA: 0x00098C32 File Offset: 0x00096E32
		public static SqlDecimal Add(SqlDecimal x, SqlDecimal y)
		{
			return x + y;
		}

		/// <summary>Calculates the results of subtracting the second <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operand from the first.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose Value property contains the results of the subtraction.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001FC5 RID: 8133 RVA: 0x00098C3B File Offset: 0x00096E3B
		public static SqlDecimal Subtract(SqlDecimal x, SqlDecimal y)
		{
			return x - y;
		}

		/// <summary>The multiplication operator computes the product of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameters.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property contains the product of the multiplication.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001FC6 RID: 8134 RVA: 0x00098C44 File Offset: 0x00096E44
		public static SqlDecimal Multiply(SqlDecimal x, SqlDecimal y)
		{
			return x * y;
		}

		/// <summary>The division operator calculates the results of dividing the first <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operand by the second.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property contains the results of the division.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001FC7 RID: 8135 RVA: 0x00098C4D File Offset: 0x00096E4D
		public static SqlDecimal Divide(SqlDecimal x, SqlDecimal y)
		{
			return x / y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operands to determine whether they are equal.</summary>
		/// <returns>true if the two values are equal. Otherwise, false. If either instance is null, the value of the SqlDecimal will be null.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001FC8 RID: 8136 RVA: 0x00098C56 File Offset: 0x00096E56
		public static SqlBoolean Equals(SqlDecimal x, SqlDecimal y)
		{
			return x == y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameters to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001FC9 RID: 8137 RVA: 0x00098C5F File Offset: 0x00096E5F
		public static SqlBoolean NotEquals(SqlDecimal x, SqlDecimal y)
		{
			return x != y;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structures to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001FCA RID: 8138 RVA: 0x00098C68 File Offset: 0x00096E68
		public static SqlBoolean LessThan(SqlDecimal x, SqlDecimal y)
		{
			return x < y;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structures to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001FCB RID: 8139 RVA: 0x00098C71 File Offset: 0x00096E71
		public static SqlBoolean GreaterThan(SqlDecimal x, SqlDecimal y)
		{
			return x > y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameters to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001FCC RID: 8140 RVA: 0x00098C7A File Offset: 0x00096E7A
		public static SqlBoolean LessThanOrEqual(SqlDecimal x, SqlDecimal y)
		{
			return x <= y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameters to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001FCD RID: 8141 RVA: 0x00098C83 File Offset: 0x00096E83
		public static SqlBoolean GreaterThanOrEqual(SqlDecimal x, SqlDecimal y)
		{
			return x >= y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>true if the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> is non-zero; false if zero; otherwise Null.</returns>
		// Token: 0x06001FCE RID: 8142 RVA: 0x00098C8C File Offset: 0x00096E8C
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose Value equals the Value of this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. If the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure's Value is true, the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure's Value will be 1. Otherwise, the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure's Value will be 0.</returns>
		// Token: 0x06001FCF RID: 8143 RVA: 0x00098C99 File Offset: 0x00096E99
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure with the same value as this instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		// Token: 0x06001FD0 RID: 8144 RVA: 0x00098CA6 File Offset: 0x00096EA6
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure with the same value as this instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		// Token: 0x06001FD1 RID: 8145 RVA: 0x00098CB3 File Offset: 0x00096EB3
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure with the same value as this instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		// Token: 0x06001FD2 RID: 8146 RVA: 0x00098CC0 File Offset: 0x00096EC0
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure with the same value as this instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		// Token: 0x06001FD3 RID: 8147 RVA: 0x00098CCD File Offset: 0x00096ECD
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure with the same value as this instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		// Token: 0x06001FD4 RID: 8148 RVA: 0x00098CDA File Offset: 0x00096EDA
		public SqlMoney ToSqlMoney()
		{
			return (SqlMoney)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure with the same value as this instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		// Token: 0x06001FD5 RID: 8149 RVA: 0x00098CE7 File Offset: 0x00096EE7
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> structure whose value is a string representing the value contained in this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</returns>
		// Token: 0x06001FD6 RID: 8150 RVA: 0x00098CF4 File Offset: 0x00096EF4
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x00098D01 File Offset: 0x00096F01
		private static char ChFromDigit(uint uiDigit)
		{
			return (char)(uiDigit + 48U);
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x00098D08 File Offset: 0x00096F08
		private void StoreFromWorkingArray(uint[] rguiData)
		{
			this._data1 = rguiData[0];
			this._data2 = rguiData[1];
			this._data3 = rguiData[2];
			this._data4 = rguiData[3];
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x00098D30 File Offset: 0x00096F30
		private void SetToZero()
		{
			this._bLen = 1;
			this._data1 = (this._data2 = (this._data3 = (this._data4 = 0U)));
			this._bStatus = SqlDecimal.s_bNotNull | SqlDecimal.s_bPositive;
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x00098D78 File Offset: 0x00096F78
		private void MakeInteger(out bool fFraction)
		{
			int i = (int)this._bScale;
			fFraction = false;
			while (i > 0)
			{
				uint num;
				if (i >= 9)
				{
					num = this.DivByULong(SqlDecimal.s_rgulShiftBase[8]);
					i -= 9;
				}
				else
				{
					num = this.DivByULong(SqlDecimal.s_rgulShiftBase[i - 1]);
					i = 0;
				}
				if (num != 0U)
				{
					fFraction = true;
				}
			}
			this._bScale = 0;
		}

		/// <summary>The Abs method gets the absolute value of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property contains the unsigned number representing the absolute value of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</returns>
		/// <param name="n">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001FDB RID: 8155 RVA: 0x00098DCE File Offset: 0x00096FCE
		public static SqlDecimal Abs(SqlDecimal n)
		{
			if (n.IsNull)
			{
				return SqlDecimal.Null;
			}
			n.SetPositive();
			return n;
		}

		/// <summary>Returns the smallest whole number greater than or equal to the specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> representing the smallest whole number greater than or equal to the specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</returns>
		/// <param name="n">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure for which the ceiling value is to be calculated. </param>
		// Token: 0x06001FDC RID: 8156 RVA: 0x00098DE8 File Offset: 0x00096FE8
		public static SqlDecimal Ceiling(SqlDecimal n)
		{
			if (n.IsNull)
			{
				return SqlDecimal.Null;
			}
			if (n._bScale == 0)
			{
				return n;
			}
			bool flag;
			n.MakeInteger(out flag);
			if (flag && n.IsPositive)
			{
				n.AddULong(1U);
			}
			if (n.FZero())
			{
				n.SetPositive();
			}
			return n;
		}

		/// <summary>Rounds a specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> number to the next lower whole number.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure that contains the whole number part of this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</returns>
		/// <param name="n">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure for which the floor value is to be calculated. </param>
		// Token: 0x06001FDD RID: 8157 RVA: 0x00098E3C File Offset: 0x0009703C
		public static SqlDecimal Floor(SqlDecimal n)
		{
			if (n.IsNull)
			{
				return SqlDecimal.Null;
			}
			if (n._bScale == 0)
			{
				return n;
			}
			bool flag;
			n.MakeInteger(out flag);
			if (flag && !n.IsPositive)
			{
				n.AddULong(1U);
			}
			if (n.FZero())
			{
				n.SetPositive();
			}
			return n;
		}

		/// <summary>Gets a value that indicates the sign of a <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure's <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property.</summary>
		/// <returns>A number that indicates the sign of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</returns>
		/// <param name="n">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose sign is to be evaluated. </param>
		// Token: 0x06001FDE RID: 8158 RVA: 0x00098E90 File Offset: 0x00097090
		public static SqlInt32 Sign(SqlDecimal n)
		{
			if (n.IsNull)
			{
				return SqlInt32.Null;
			}
			if (n == new SqlDecimal(0))
			{
				return SqlInt32.Zero;
			}
			if (n.IsNull)
			{
				return SqlInt32.Null;
			}
			if (!n.IsPositive)
			{
				return new SqlInt32(-1);
			}
			return new SqlInt32(1);
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x00098EEC File Offset: 0x000970EC
		private static SqlDecimal Round(SqlDecimal n, int lPosition, bool fTruncate)
		{
			if (n.IsNull)
			{
				return SqlDecimal.Null;
			}
			if (lPosition >= 0)
			{
				lPosition = Math.Min((int)SqlDecimal.s_NUMERIC_MAX_PRECISION, lPosition);
				if (lPosition >= (int)n._bScale)
				{
					return n;
				}
			}
			else
			{
				lPosition = Math.Max((int)(-(int)SqlDecimal.s_NUMERIC_MAX_PRECISION), lPosition);
				if (lPosition < (int)(n._bScale - n._bPrec))
				{
					n.SetToZero();
					return n;
				}
			}
			uint num = 0U;
			int i = Math.Abs(lPosition - (int)n._bScale);
			uint num2 = 1U;
			while (i > 0)
			{
				if (i >= 9)
				{
					num = n.DivByULong(SqlDecimal.s_rgulShiftBase[8]);
					num2 = SqlDecimal.s_rgulShiftBase[8];
					i -= 9;
				}
				else
				{
					num = n.DivByULong(SqlDecimal.s_rgulShiftBase[i - 1]);
					num2 = SqlDecimal.s_rgulShiftBase[i - 1];
					i = 0;
				}
			}
			if (num2 > 1U)
			{
				num /= num2 / 10U;
			}
			if (n.FZero() && (fTruncate || num < 5U))
			{
				n.SetPositive();
				return n;
			}
			if (num >= 5U && !fTruncate)
			{
				n.AddULong(1U);
			}
			i = Math.Abs(lPosition - (int)n._bScale);
			while (i-- > 0)
			{
				n.MultByULong(SqlDecimal.s_ulBase10);
			}
			return n;
		}

		/// <summary>Gets the number nearest the specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure's value with the specified precision.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure that contains the results of the rounding operation.</returns>
		/// <param name="n">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to be rounded. </param>
		/// <param name="position">The number of significant fractional digits (precision) in the return value. </param>
		// Token: 0x06001FE0 RID: 8160 RVA: 0x00098FFB File Offset: 0x000971FB
		public static SqlDecimal Round(SqlDecimal n, int position)
		{
			return SqlDecimal.Round(n, position, false);
		}

		/// <summary>Truncates the specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure's value to the that you want position.</summary>
		/// <returns>Supply a negative value for the <paramref name="position" /> parameter in order to truncate the value to the corresponding position to the left of the decimal point.</returns>
		/// <param name="n">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to be truncated. </param>
		/// <param name="position">The decimal position to which the number will be truncated. </param>
		// Token: 0x06001FE1 RID: 8161 RVA: 0x00099005 File Offset: 0x00097205
		public static SqlDecimal Truncate(SqlDecimal n, int position)
		{
			return SqlDecimal.Round(n, position, true);
		}

		/// <summary>Raises the value of the specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to the specified exponential power.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure that contains the results.</returns>
		/// <param name="n">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to be raised to a power. </param>
		/// <param name="exp">A double value that indicates the power to which the number should be raised. </param>
		// Token: 0x06001FE2 RID: 8162 RVA: 0x00099010 File Offset: 0x00097210
		public static SqlDecimal Power(SqlDecimal n, double exp)
		{
			if (n.IsNull)
			{
				return SqlDecimal.Null;
			}
			byte precision = n.Precision;
			int scale = (int)n.Scale;
			double num = n.ToDouble();
			n = new SqlDecimal(Math.Pow(num, exp));
			n.AdjustScale(scale - (int)n.Scale, true);
			n._bPrec = SqlDecimal.MaxPrecision;
			return n;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> instance to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return Value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001FE3 RID: 8163 RVA: 0x00099070 File Offset: 0x00097270
		public int CompareTo(object value)
		{
			if (value is SqlDecimal)
			{
				SqlDecimal sqlDecimal = (SqlDecimal)value;
				return this.CompareTo(sqlDecimal);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlDecimal));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> instance to the supplied <see cref="T:System.Data.SqlTypes.SqlDecimal" /> object and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> to be compared. </param>
		// Token: 0x06001FE4 RID: 8164 RVA: 0x000990AC File Offset: 0x000972AC
		public int CompareTo(SqlDecimal value)
		{
			if (this.IsNull)
			{
				if (!value.IsNull)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (value.IsNull)
				{
					return 1;
				}
				if (this < value)
				{
					return -1;
				}
				if (this > value)
				{
					return 1;
				}
				return 0;
			}
		}

		/// <summary>Compares the supplied <see cref="T:System.Object" /> parameter to the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> instance.</summary>
		/// <returns>true if object is an instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> and the two are equal. Otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared.</param>
		// Token: 0x06001FE5 RID: 8165 RVA: 0x00099104 File Offset: 0x00097304
		public override bool Equals(object value)
		{
			if (!(value is SqlDecimal))
			{
				return false;
			}
			SqlDecimal sqlDecimal = (SqlDecimal)value;
			if (sqlDecimal.IsNull || this.IsNull)
			{
				return sqlDecimal.IsNull && this.IsNull;
			}
			return (this == sqlDecimal).Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001FE6 RID: 8166 RVA: 0x0009915C File Offset: 0x0009735C
		public override int GetHashCode()
		{
			if (this.IsNull)
			{
				return 0;
			}
			SqlDecimal sqlDecimal = this;
			int num = (int)sqlDecimal.CalculatePrecision();
			sqlDecimal.AdjustScale((int)SqlDecimal.s_NUMERIC_MAX_PRECISION - num, true);
			int bLen = (int)sqlDecimal._bLen;
			int num2 = 0;
			int[] data = sqlDecimal.Data;
			for (int i = 0; i < bLen; i++)
			{
				int num3 = (num2 >> 28) & 255;
				num2 <<= 4;
				num2 = num2 ^ data[i] ^ num3;
			}
			return num2;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An XmlSchema.</returns>
		// Token: 0x06001FE7 RID: 8167 RVA: 0x00003DF6 File Offset: 0x00001FF6
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader </param>
		// Token: 0x06001FE8 RID: 8168 RVA: 0x000991D0 File Offset: 0x000973D0
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this._bStatus = SqlDecimal.s_bReverseNullMask & this._bStatus;
				return;
			}
			SqlDecimal sqlDecimal = SqlDecimal.Parse(reader.ReadElementString());
			this._bStatus = sqlDecimal._bStatus;
			this._bLen = sqlDecimal._bLen;
			this._bPrec = sqlDecimal._bPrec;
			this._bScale = sqlDecimal._bScale;
			this._data1 = sqlDecimal._data1;
			this._data2 = sqlDecimal._data2;
			this._data3 = sqlDecimal._data3;
			this._data4 = sqlDecimal._data4;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter </param>
		// Token: 0x06001FE9 RID: 8169 RVA: 0x00099280 File Offset: 0x00097480
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(this.ToString());
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06001FEA RID: 8170 RVA: 0x000992B7 File Offset: 0x000974B7
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("decimal", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04001657 RID: 5719
		internal byte _bStatus;

		// Token: 0x04001658 RID: 5720
		internal byte _bLen;

		// Token: 0x04001659 RID: 5721
		internal byte _bPrec;

		// Token: 0x0400165A RID: 5722
		internal byte _bScale;

		// Token: 0x0400165B RID: 5723
		internal uint _data1;

		// Token: 0x0400165C RID: 5724
		internal uint _data2;

		// Token: 0x0400165D RID: 5725
		internal uint _data3;

		// Token: 0x0400165E RID: 5726
		internal uint _data4;

		// Token: 0x0400165F RID: 5727
		private static readonly byte s_NUMERIC_MAX_PRECISION = 38;

		/// <summary>A constant representing the largest possible value for the <see cref="P:System.Data.SqlTypes.SqlDecimal.Precision" /> property.</summary>
		// Token: 0x04001660 RID: 5728
		public static readonly byte MaxPrecision = SqlDecimal.s_NUMERIC_MAX_PRECISION;

		/// <summary>A constant representing the maximum value for the <see cref="P:System.Data.SqlTypes.SqlDecimal.Scale" /> property.</summary>
		// Token: 0x04001661 RID: 5729
		public static readonly byte MaxScale = SqlDecimal.s_NUMERIC_MAX_PRECISION;

		// Token: 0x04001662 RID: 5730
		private static readonly byte s_bNullMask = 1;

		// Token: 0x04001663 RID: 5731
		private static readonly byte s_bIsNull = 0;

		// Token: 0x04001664 RID: 5732
		private static readonly byte s_bNotNull = 1;

		// Token: 0x04001665 RID: 5733
		private static readonly byte s_bReverseNullMask = ~SqlDecimal.s_bNullMask;

		// Token: 0x04001666 RID: 5734
		private static readonly byte s_bSignMask = 2;

		// Token: 0x04001667 RID: 5735
		private static readonly byte s_bPositive = 0;

		// Token: 0x04001668 RID: 5736
		private static readonly byte s_bNegative = 2;

		// Token: 0x04001669 RID: 5737
		private static readonly byte s_bReverseSignMask = ~SqlDecimal.s_bSignMask;

		// Token: 0x0400166A RID: 5738
		private static readonly uint s_uiZero = 0U;

		// Token: 0x0400166B RID: 5739
		private static readonly int s_cNumeMax = 4;

		// Token: 0x0400166C RID: 5740
		private static readonly long s_lInt32Base = 4294967296L;

		// Token: 0x0400166D RID: 5741
		private static readonly ulong s_ulInt32Base = 4294967296UL;

		// Token: 0x0400166E RID: 5742
		private static readonly ulong s_ulInt32BaseForMod = SqlDecimal.s_ulInt32Base - 1UL;

		// Token: 0x0400166F RID: 5743
		internal static readonly ulong s_llMax = 9223372036854775807UL;

		// Token: 0x04001670 RID: 5744
		private static readonly uint s_ulBase10 = 10U;

		// Token: 0x04001671 RID: 5745
		private static readonly double s_DUINT_BASE = (double)SqlDecimal.s_lInt32Base;

		// Token: 0x04001672 RID: 5746
		private static readonly double s_DUINT_BASE2 = SqlDecimal.s_DUINT_BASE * SqlDecimal.s_DUINT_BASE;

		// Token: 0x04001673 RID: 5747
		private static readonly double s_DUINT_BASE3 = SqlDecimal.s_DUINT_BASE2 * SqlDecimal.s_DUINT_BASE;

		// Token: 0x04001674 RID: 5748
		private static readonly double s_DMAX_NUME = 1E+38;

		// Token: 0x04001675 RID: 5749
		private static readonly uint s_DBL_DIG = 17U;

		// Token: 0x04001676 RID: 5750
		private static readonly byte s_cNumeDivScaleMin = 6;

		// Token: 0x04001677 RID: 5751
		private static readonly uint[] s_rgulShiftBase = new uint[] { 10U, 100U, 1000U, 10000U, 100000U, 1000000U, 10000000U, 100000000U, 1000000000U };

		// Token: 0x04001678 RID: 5752
		private static readonly uint[] s_decimalHelpersLo = new uint[]
		{
			10U, 100U, 1000U, 10000U, 100000U, 1000000U, 10000000U, 100000000U, 1000000000U, 1410065408U,
			1215752192U, 3567587328U, 1316134912U, 276447232U, 2764472320U, 1874919424U, 1569325056U, 2808348672U, 2313682944U, 1661992960U,
			3735027712U, 2990538752U, 4135583744U, 2701131776U, 1241513984U, 3825205248U, 3892314112U, 268435456U, 2684354560U, 1073741824U,
			2147483648U, 0U, 0U, 0U, 0U, 0U, 0U, 0U
		};

		// Token: 0x04001679 RID: 5753
		private static readonly uint[] s_decimalHelpersMid = new uint[]
		{
			0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 2U,
			23U, 232U, 2328U, 23283U, 232830U, 2328306U, 23283064U, 232830643U, 2328306436U, 1808227885U,
			902409669U, 434162106U, 46653770U, 466537709U, 370409800U, 3704098002U, 2681241660U, 1042612833U, 1836193738U, 1182068202U,
			3230747430U, 2242703233U, 952195850U, 932023908U, 730304488U, 3008077584U, 16004768U, 160047680U
		};

		// Token: 0x0400167A RID: 5754
		private static readonly uint[] s_decimalHelpersHi = new uint[]
		{
			0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U,
			0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 5U,
			54U, 542U, 5421U, 54210U, 542101U, 5421010U, 54210108U, 542101086U, 1126043566U, 2670501072U,
			935206946U, 762134875U, 3326381459U, 3199043520U, 1925664130U, 2076772117U, 3587851993U, 1518781562U
		};

		// Token: 0x0400167B RID: 5755
		private static readonly uint[] s_decimalHelpersHiHi = new uint[]
		{
			0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U,
			0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U,
			0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 1U, 12U,
			126U, 1262U, 12621U, 126217U, 1262177U, 12621774U, 126217744U, 1262177448U
		};

		// Token: 0x0400167C RID: 5756
		private const int HelperTableStartIndexLo = 5;

		// Token: 0x0400167D RID: 5757
		private const int HelperTableStartIndexMid = 15;

		// Token: 0x0400167E RID: 5758
		private const int HelperTableStartIndexHi = 24;

		// Token: 0x0400167F RID: 5759
		private const int HelperTableStartIndexHiHi = 33;

		// Token: 0x04001680 RID: 5760
		private static readonly byte[] s_rgCLenFromPrec = new byte[]
		{
			1, 1, 1, 1, 1, 1, 1, 1, 1, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 4, 4,
			4, 4, 4, 4, 4, 4, 4, 4
		};

		// Token: 0x04001681 RID: 5761
		private static readonly uint s_ulT1 = 10U;

		// Token: 0x04001682 RID: 5762
		private static readonly uint s_ulT2 = 100U;

		// Token: 0x04001683 RID: 5763
		private static readonly uint s_ulT3 = 1000U;

		// Token: 0x04001684 RID: 5764
		private static readonly uint s_ulT4 = 10000U;

		// Token: 0x04001685 RID: 5765
		private static readonly uint s_ulT5 = 100000U;

		// Token: 0x04001686 RID: 5766
		private static readonly uint s_ulT6 = 1000000U;

		// Token: 0x04001687 RID: 5767
		private static readonly uint s_ulT7 = 10000000U;

		// Token: 0x04001688 RID: 5768
		private static readonly uint s_ulT8 = 100000000U;

		// Token: 0x04001689 RID: 5769
		private static readonly uint s_ulT9 = 1000000000U;

		// Token: 0x0400168A RID: 5770
		private static readonly ulong s_dwlT10 = 10000000000UL;

		// Token: 0x0400168B RID: 5771
		private static readonly ulong s_dwlT11 = 100000000000UL;

		// Token: 0x0400168C RID: 5772
		private static readonly ulong s_dwlT12 = 1000000000000UL;

		// Token: 0x0400168D RID: 5773
		private static readonly ulong s_dwlT13 = 10000000000000UL;

		// Token: 0x0400168E RID: 5774
		private static readonly ulong s_dwlT14 = 100000000000000UL;

		// Token: 0x0400168F RID: 5775
		private static readonly ulong s_dwlT15 = 1000000000000000UL;

		// Token: 0x04001690 RID: 5776
		private static readonly ulong s_dwlT16 = 10000000000000000UL;

		// Token: 0x04001691 RID: 5777
		private static readonly ulong s_dwlT17 = 100000000000000000UL;

		// Token: 0x04001692 RID: 5778
		private static readonly ulong s_dwlT18 = 1000000000000000000UL;

		// Token: 0x04001693 RID: 5779
		private static readonly ulong s_dwlT19 = 10000000000000000000UL;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlDecimal" />class.</summary>
		// Token: 0x04001694 RID: 5780
		public static readonly SqlDecimal Null = new SqlDecimal(true);

		/// <summary>A constant representing the minimum value for a <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</summary>
		// Token: 0x04001695 RID: 5781
		public static readonly SqlDecimal MinValue = SqlDecimal.Parse("-99999999999999999999999999999999999999");

		/// <summary>A constant representing the maximum value of a <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</summary>
		// Token: 0x04001696 RID: 5782
		public static readonly SqlDecimal MaxValue = SqlDecimal.Parse("99999999999999999999999999999999999999");
	}
}
