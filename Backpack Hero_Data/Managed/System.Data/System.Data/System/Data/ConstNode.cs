using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Data
{
	// Token: 0x02000094 RID: 148
	internal sealed class ConstNode : ExpressionNode
	{
		// Token: 0x060009F6 RID: 2550 RVA: 0x0002DAF0 File Offset: 0x0002BCF0
		internal ConstNode(DataTable table, ValueType type, object constant)
			: this(table, type, constant, true)
		{
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0002DAFC File Offset: 0x0002BCFC
		internal ConstNode(DataTable table, ValueType type, object constant, bool fParseQuotes)
			: base(table)
		{
			switch (type)
			{
			case ValueType.Null:
				this._val = DBNull.Value;
				return;
			case ValueType.Bool:
				this._val = Convert.ToBoolean(constant, CultureInfo.InvariantCulture);
				return;
			case ValueType.Numeric:
				this._val = this.SmallestNumeric(constant);
				return;
			case ValueType.Str:
				if (fParseQuotes)
				{
					this._val = ((string)constant).Replace("''", "'");
					return;
				}
				this._val = (string)constant;
				return;
			case ValueType.Float:
				this._val = Convert.ToDouble(constant, NumberFormatInfo.InvariantInfo);
				return;
			case ValueType.Decimal:
				this._val = this.SmallestDecimal(constant);
				return;
			case ValueType.Date:
				this._val = DateTime.Parse((string)constant, CultureInfo.InvariantCulture);
				return;
			}
			this._val = constant;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0002DBE1 File Offset: 0x0002BDE1
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002DBEA File Offset: 0x0002BDEA
		internal override object Eval()
		{
			return this._val;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0002DBF2 File Offset: 0x0002BDF2
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			return this.Eval();
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0002DBF2 File Offset: 0x0002BDF2
		internal override object Eval(int[] recordNos)
		{
			return this.Eval();
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0000CD07 File Offset: 0x0000AF07
		internal override bool IsConstant()
		{
			return true;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0000CD07 File Offset: 0x0000AF07
		internal override bool IsTableConstant()
		{
			return true;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00005AE9 File Offset: 0x00003CE9
		internal override bool HasLocalAggregate()
		{
			return false;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00005AE9 File Offset: 0x00003CE9
		internal override bool HasRemoteAggregate()
		{
			return false;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0000565A File Offset: 0x0000385A
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002DBFC File Offset: 0x0002BDFC
		private object SmallestDecimal(object constant)
		{
			if (constant == null)
			{
				return 0.0;
			}
			string text = constant as string;
			if (text != null)
			{
				decimal num;
				if (decimal.TryParse(text, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out num))
				{
					return num;
				}
				double num2;
				if (double.TryParse(text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out num2))
				{
					return num2;
				}
			}
			else
			{
				IConvertible convertible = constant as IConvertible;
				if (convertible != null)
				{
					try
					{
						return convertible.ToDecimal(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException ex)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
					}
					catch (FormatException ex2)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex2);
					}
					catch (InvalidCastException ex3)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex3);
					}
					catch (OverflowException ex4)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex4);
					}
					try
					{
						return convertible.ToDouble(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException ex5)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex5);
					}
					catch (FormatException ex6)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex6);
					}
					catch (InvalidCastException ex7)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex7);
					}
					catch (OverflowException ex8)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex8);
					}
					return constant;
				}
			}
			return constant;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0002DD34 File Offset: 0x0002BF34
		private object SmallestNumeric(object constant)
		{
			if (constant == null)
			{
				return 0;
			}
			string text = constant as string;
			if (text != null)
			{
				int num;
				if (int.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num))
				{
					return num;
				}
				long num2;
				if (long.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num2))
				{
					return num2;
				}
				double num3;
				if (double.TryParse(text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out num3))
				{
					return num3;
				}
			}
			else
			{
				IConvertible convertible = constant as IConvertible;
				if (convertible != null)
				{
					try
					{
						return convertible.ToInt32(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException ex)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
					}
					catch (FormatException ex2)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex2);
					}
					catch (InvalidCastException ex3)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex3);
					}
					catch (OverflowException ex4)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex4);
					}
					try
					{
						return convertible.ToInt64(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException ex5)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex5);
					}
					catch (FormatException ex6)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex6);
					}
					catch (InvalidCastException ex7)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex7);
					}
					catch (OverflowException ex8)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex8);
					}
					try
					{
						return convertible.ToDouble(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException ex9)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex9);
					}
					catch (FormatException ex10)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex10);
					}
					catch (InvalidCastException ex11)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex11);
					}
					catch (OverflowException ex12)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex12);
					}
					return constant;
				}
			}
			return constant;
		}

		// Token: 0x04000678 RID: 1656
		internal readonly object _val;
	}
}
