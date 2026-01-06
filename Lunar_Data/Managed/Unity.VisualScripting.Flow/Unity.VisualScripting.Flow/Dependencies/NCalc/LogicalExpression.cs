using System;
using System.Text;

namespace Unity.VisualScripting.Dependencies.NCalc
{
	// Token: 0x02000193 RID: 403
	public abstract class LogicalExpression
	{
		// Token: 0x06000AC4 RID: 2756 RVA: 0x000147D1 File Offset: 0x000129D1
		public BinaryExpression And(LogicalExpression operand)
		{
			return new BinaryExpression(BinaryExpressionType.And, this, operand);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x000147DB File Offset: 0x000129DB
		public BinaryExpression And(object operand)
		{
			return new BinaryExpression(BinaryExpressionType.And, this, new ValueExpression(operand));
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x000147EA File Offset: 0x000129EA
		public BinaryExpression DividedBy(LogicalExpression operand)
		{
			return new BinaryExpression(BinaryExpressionType.Div, this, operand);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x000147F5 File Offset: 0x000129F5
		public BinaryExpression DividedBy(object operand)
		{
			return new BinaryExpression(BinaryExpressionType.Div, this, new ValueExpression(operand));
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00014805 File Offset: 0x00012A05
		public BinaryExpression EqualsTo(LogicalExpression operand)
		{
			return new BinaryExpression(BinaryExpressionType.Equal, this, operand);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0001480F File Offset: 0x00012A0F
		public BinaryExpression EqualsTo(object operand)
		{
			return new BinaryExpression(BinaryExpressionType.Equal, this, new ValueExpression(operand));
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0001481E File Offset: 0x00012A1E
		public BinaryExpression GreaterThan(LogicalExpression operand)
		{
			return new BinaryExpression(BinaryExpressionType.Greater, this, operand);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00014828 File Offset: 0x00012A28
		public BinaryExpression GreaterThan(object operand)
		{
			return new BinaryExpression(BinaryExpressionType.Greater, this, new ValueExpression(operand));
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00014837 File Offset: 0x00012A37
		public BinaryExpression GreaterOrEqualThan(LogicalExpression operand)
		{
			return new BinaryExpression(BinaryExpressionType.GreaterOrEqual, this, operand);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00014841 File Offset: 0x00012A41
		public BinaryExpression GreaterOrEqualThan(object operand)
		{
			return new BinaryExpression(BinaryExpressionType.GreaterOrEqual, this, new ValueExpression(operand));
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00014850 File Offset: 0x00012A50
		public BinaryExpression LesserThan(LogicalExpression operand)
		{
			return new BinaryExpression(BinaryExpressionType.Lesser, this, operand);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0001485A File Offset: 0x00012A5A
		public BinaryExpression LesserThan(object operand)
		{
			return new BinaryExpression(BinaryExpressionType.Lesser, this, new ValueExpression(operand));
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00014869 File Offset: 0x00012A69
		public BinaryExpression LesserOrEqualThan(LogicalExpression operand)
		{
			return new BinaryExpression(BinaryExpressionType.LesserOrEqual, this, operand);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00014873 File Offset: 0x00012A73
		public BinaryExpression LesserOrEqualThan(object operand)
		{
			return new BinaryExpression(BinaryExpressionType.LesserOrEqual, this, new ValueExpression(operand));
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00014882 File Offset: 0x00012A82
		public BinaryExpression Minus(LogicalExpression operand)
		{
			return new BinaryExpression(BinaryExpressionType.Minus, this, operand);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0001488C File Offset: 0x00012A8C
		public BinaryExpression Minus(object operand)
		{
			return new BinaryExpression(BinaryExpressionType.Minus, this, new ValueExpression(operand));
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0001489B File Offset: 0x00012A9B
		public BinaryExpression Modulo(LogicalExpression operand)
		{
			return new BinaryExpression(BinaryExpressionType.Modulo, this, operand);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x000148A6 File Offset: 0x00012AA6
		public BinaryExpression Modulo(object operand)
		{
			return new BinaryExpression(BinaryExpressionType.Modulo, this, new ValueExpression(operand));
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x000148B6 File Offset: 0x00012AB6
		public BinaryExpression NotEqual(LogicalExpression operand)
		{
			return new BinaryExpression(BinaryExpressionType.NotEqual, this, operand);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x000148C0 File Offset: 0x00012AC0
		public BinaryExpression NotEqual(object operand)
		{
			return new BinaryExpression(BinaryExpressionType.NotEqual, this, new ValueExpression(operand));
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x000148CF File Offset: 0x00012ACF
		public BinaryExpression Or(LogicalExpression operand)
		{
			return new BinaryExpression(BinaryExpressionType.Or, this, operand);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x000148D9 File Offset: 0x00012AD9
		public BinaryExpression Or(object operand)
		{
			return new BinaryExpression(BinaryExpressionType.Or, this, new ValueExpression(operand));
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x000148E8 File Offset: 0x00012AE8
		public BinaryExpression Plus(LogicalExpression operand)
		{
			return new BinaryExpression(BinaryExpressionType.Plus, this, operand);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x000148F3 File Offset: 0x00012AF3
		public BinaryExpression Plus(object operand)
		{
			return new BinaryExpression(BinaryExpressionType.Plus, this, new ValueExpression(operand));
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00014903 File Offset: 0x00012B03
		public BinaryExpression Mult(LogicalExpression operand)
		{
			return new BinaryExpression(BinaryExpressionType.Times, this, operand);
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0001490E File Offset: 0x00012B0E
		public BinaryExpression Mult(object operand)
		{
			return new BinaryExpression(BinaryExpressionType.Times, this, new ValueExpression(operand));
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0001491E File Offset: 0x00012B1E
		public BinaryExpression BitwiseOr(LogicalExpression operand)
		{
			return new BinaryExpression(BinaryExpressionType.BitwiseOr, this, operand);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00014929 File Offset: 0x00012B29
		public BinaryExpression BitwiseOr(object operand)
		{
			return new BinaryExpression(BinaryExpressionType.BitwiseOr, this, new ValueExpression(operand));
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00014939 File Offset: 0x00012B39
		public BinaryExpression BitwiseAnd(LogicalExpression operand)
		{
			return new BinaryExpression(BinaryExpressionType.BitwiseAnd, this, operand);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00014944 File Offset: 0x00012B44
		public BinaryExpression BitwiseAnd(object operand)
		{
			return new BinaryExpression(BinaryExpressionType.BitwiseAnd, this, new ValueExpression(operand));
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00014954 File Offset: 0x00012B54
		public BinaryExpression BitwiseXOr(LogicalExpression operand)
		{
			return new BinaryExpression(BinaryExpressionType.BitwiseXOr, this, operand);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0001495F File Offset: 0x00012B5F
		public BinaryExpression BitwiseXOr(object operand)
		{
			return new BinaryExpression(BinaryExpressionType.BitwiseXOr, this, new ValueExpression(operand));
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0001496F File Offset: 0x00012B6F
		public BinaryExpression LeftShift(LogicalExpression operand)
		{
			return new BinaryExpression(BinaryExpressionType.LeftShift, this, operand);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0001497A File Offset: 0x00012B7A
		public BinaryExpression LeftShift(object operand)
		{
			return new BinaryExpression(BinaryExpressionType.LeftShift, this, new ValueExpression(operand));
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0001498A File Offset: 0x00012B8A
		public BinaryExpression RightShift(LogicalExpression operand)
		{
			return new BinaryExpression(BinaryExpressionType.RightShift, this, operand);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00014995 File Offset: 0x00012B95
		public BinaryExpression RightShift(object operand)
		{
			return new BinaryExpression(BinaryExpressionType.RightShift, this, new ValueExpression(operand));
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000149A8 File Offset: 0x00012BA8
		public override string ToString()
		{
			SerializationVisitor serializationVisitor = new SerializationVisitor();
			this.Accept(serializationVisitor);
			return serializationVisitor.Result.ToString().TrimEnd(' ');
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x000149D4 File Offset: 0x00012BD4
		public virtual void Accept(LogicalExpressionVisitor visitor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x000149DC File Offset: 0x00012BDC
		private static string ExtractString(string text)
		{
			StringBuilder stringBuilder = new StringBuilder(text);
			int num = 1;
			int num2;
			while ((num2 = stringBuilder.ToString().IndexOf('\\', num)) != -1)
			{
				char c = stringBuilder[num2 + 1];
				if (c <= '\\')
				{
					if (c != '\'')
					{
						if (c != '\\')
						{
							goto IL_013E;
						}
						stringBuilder.Remove(num2, 2).Insert(num2, '\\');
					}
					else
					{
						stringBuilder.Remove(num2, 2).Insert(num2, '\'');
					}
				}
				else if (c != 'n')
				{
					switch (c)
					{
					case 'r':
						stringBuilder.Remove(num2, 2).Insert(num2, '\r');
						break;
					case 's':
						goto IL_013E;
					case 't':
						stringBuilder.Remove(num2, 2).Insert(num2, '\t');
						break;
					case 'u':
					{
						string text2 = stringBuilder[num2 + 4] + stringBuilder[num2 + 5];
						string text3 = stringBuilder[num2 + 2] + stringBuilder[num2 + 3];
						char c2 = Encoding.Unicode.GetChars(new byte[]
						{
							Convert.ToByte(text2, 16),
							Convert.ToByte(text3, 16)
						})[0];
						stringBuilder.Remove(num2, 6).Insert(num2, c2);
						break;
					}
					default:
						goto IL_013E;
					}
				}
				else
				{
					stringBuilder.Remove(num2, 2).Insert(num2, '\n');
				}
				num = num2 + 1;
				continue;
				IL_013E:
				throw new ApplicationException("Unvalid escape sequence: \\" + c.ToString());
			}
			stringBuilder.Remove(0, 1);
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			return stringBuilder.ToString();
		}

		// Token: 0x0400026B RID: 619
		private const char BS = '\\';
	}
}
