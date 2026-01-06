using System;
using System.Globalization;
using System.Text;

namespace Unity.VisualScripting.Dependencies.NCalc
{
	// Token: 0x02000198 RID: 408
	public class SerializationVisitor : LogicalExpressionVisitor
	{
		// Token: 0x06000B4A RID: 2890 RVA: 0x00019BDC File Offset: 0x00017DDC
		public SerializationVisitor()
		{
			this.Result = new StringBuilder();
			this._numberFormatInfo = new NumberFormatInfo
			{
				NumberDecimalSeparator = "."
			};
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x00019C05 File Offset: 0x00017E05
		// (set) Token: 0x06000B4C RID: 2892 RVA: 0x00019C0D File Offset: 0x00017E0D
		public StringBuilder Result { get; protected set; }

		// Token: 0x06000B4D RID: 2893 RVA: 0x00019C18 File Offset: 0x00017E18
		public override void Visit(TernaryExpression ternary)
		{
			this.EncapsulateNoValue(ternary.LeftExpression);
			this.Result.Append("? ");
			this.EncapsulateNoValue(ternary.MiddleExpression);
			this.Result.Append(": ");
			this.EncapsulateNoValue(ternary.RightExpression);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00019C6C File Offset: 0x00017E6C
		public override void Visit(BinaryExpression binary)
		{
			this.EncapsulateNoValue(binary.LeftExpression);
			switch (binary.Type)
			{
			case BinaryExpressionType.And:
				this.Result.Append("and ");
				break;
			case BinaryExpressionType.Or:
				this.Result.Append("or ");
				break;
			case BinaryExpressionType.NotEqual:
				this.Result.Append("!= ");
				break;
			case BinaryExpressionType.LesserOrEqual:
				this.Result.Append("<= ");
				break;
			case BinaryExpressionType.GreaterOrEqual:
				this.Result.Append(">= ");
				break;
			case BinaryExpressionType.Lesser:
				this.Result.Append("< ");
				break;
			case BinaryExpressionType.Greater:
				this.Result.Append("> ");
				break;
			case BinaryExpressionType.Equal:
				this.Result.Append("= ");
				break;
			case BinaryExpressionType.Minus:
				this.Result.Append("- ");
				break;
			case BinaryExpressionType.Plus:
				this.Result.Append("+ ");
				break;
			case BinaryExpressionType.Modulo:
				this.Result.Append("% ");
				break;
			case BinaryExpressionType.Div:
				this.Result.Append("/ ");
				break;
			case BinaryExpressionType.Times:
				this.Result.Append("* ");
				break;
			case BinaryExpressionType.BitwiseOr:
				this.Result.Append("| ");
				break;
			case BinaryExpressionType.BitwiseAnd:
				this.Result.Append("& ");
				break;
			case BinaryExpressionType.BitwiseXOr:
				this.Result.Append("~ ");
				break;
			case BinaryExpressionType.LeftShift:
				this.Result.Append("<< ");
				break;
			case BinaryExpressionType.RightShift:
				this.Result.Append(">> ");
				break;
			}
			this.EncapsulateNoValue(binary.RightExpression);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00019E60 File Offset: 0x00018060
		public override void Visit(UnaryExpression unary)
		{
			switch (unary.Type)
			{
			case UnaryExpressionType.Not:
				this.Result.Append("!");
				break;
			case UnaryExpressionType.Negate:
				this.Result.Append("-");
				break;
			case UnaryExpressionType.BitwiseNot:
				this.Result.Append("~");
				break;
			}
			this.EncapsulateNoValue(unary.Expression);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00019ECC File Offset: 0x000180CC
		public override void Visit(ValueExpression value)
		{
			switch (value.Type)
			{
			case ValueType.Integer:
				this.Result.Append(value.Value).Append(" ");
				return;
			case ValueType.String:
				this.Result.Append("'").Append(value.Value).Append("'")
					.Append(" ");
				return;
			case ValueType.DateTime:
				this.Result.Append("#").Append(value.Value).Append("#")
					.Append(" ");
				return;
			case ValueType.Float:
				this.Result.Append(decimal.Parse(value.Value.ToString()).ToString(this._numberFormatInfo)).Append(" ");
				return;
			case ValueType.Boolean:
				this.Result.Append(value.Value).Append(" ");
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00019FCC File Offset: 0x000181CC
		public override void Visit(FunctionExpression function)
		{
			this.Result.Append(function.Identifier.Name);
			this.Result.Append("(");
			for (int i = 0; i < function.Expressions.Length; i++)
			{
				function.Expressions[i].Accept(this);
				if (i < function.Expressions.Length - 1)
				{
					this.Result.Remove(this.Result.Length - 1, 1);
					this.Result.Append(", ");
				}
			}
			while (this.Result[this.Result.Length - 1] == ' ')
			{
				this.Result.Remove(this.Result.Length - 1, 1);
			}
			this.Result.Append(") ");
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0001A0A3 File Offset: 0x000182A3
		public override void Visit(IdentifierExpression identifier)
		{
			this.Result.Append("[").Append(identifier.Name).Append("] ");
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0001A0CC File Offset: 0x000182CC
		protected void EncapsulateNoValue(LogicalExpression expression)
		{
			if (expression is ValueExpression)
			{
				expression.Accept(this);
				return;
			}
			this.Result.Append("(");
			expression.Accept(this);
			while (this.Result[this.Result.Length - 1] == ' ')
			{
				this.Result.Remove(this.Result.Length - 1, 1);
			}
			this.Result.Append(") ");
		}

		// Token: 0x04000335 RID: 821
		private readonly NumberFormatInfo _numberFormatInfo;
	}
}
