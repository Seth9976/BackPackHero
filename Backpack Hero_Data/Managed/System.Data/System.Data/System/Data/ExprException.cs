using System;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000A0 RID: 160
	internal sealed class ExprException
	{
		// Token: 0x06000A55 RID: 2645 RVA: 0x00003D55 File Offset: 0x00001F55
		private ExprException()
		{
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0002FD2F File Offset: 0x0002DF2F
		private static OverflowException _Overflow(string error)
		{
			OverflowException ex = new OverflowException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002FD3E File Offset: 0x0002DF3E
		private static InvalidExpressionException _Expr(string error)
		{
			InvalidExpressionException ex = new InvalidExpressionException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0002FD4D File Offset: 0x0002DF4D
		private static SyntaxErrorException _Syntax(string error)
		{
			SyntaxErrorException ex = new SyntaxErrorException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0002FD5C File Offset: 0x0002DF5C
		private static EvaluateException _Eval(string error)
		{
			EvaluateException ex = new EvaluateException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0002FD5C File Offset: 0x0002DF5C
		private static EvaluateException _Eval(string error, Exception innerException)
		{
			EvaluateException ex = new EvaluateException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0002FD6B File Offset: 0x0002DF6B
		public static Exception InvokeArgument()
		{
			return ExceptionBuilder._Argument("Need a row or a table to Invoke DataFilter.");
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0002FD77 File Offset: 0x0002DF77
		public static Exception NYI(string moreinfo)
		{
			return ExprException._Expr(SR.Format("The feature not implemented. {0}.", moreinfo));
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0002FD89 File Offset: 0x0002DF89
		public static Exception MissingOperand(OperatorInfo before)
		{
			return ExprException._Syntax(SR.Format("Syntax error: Missing operand after '{0}' operator.", Operators.ToString(before._op)));
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002FDA5 File Offset: 0x0002DFA5
		public static Exception MissingOperator(string token)
		{
			return ExprException._Syntax(SR.Format("Syntax error: Missing operand after '{0}' operator.", token));
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0002FDB7 File Offset: 0x0002DFB7
		public static Exception TypeMismatch(string expr)
		{
			return ExprException._Eval(SR.Format("Type mismatch in expression '{0}'.", expr));
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0002FDC9 File Offset: 0x0002DFC9
		public static Exception FunctionArgumentOutOfRange(string arg, string func)
		{
			return ExceptionBuilder._ArgumentOutOfRange(arg, SR.Format("{0}() argument is out of range.", func));
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002FDDC File Offset: 0x0002DFDC
		public static Exception ExpressionTooComplex()
		{
			return ExprException._Eval("Expression is too complex.");
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0002FDE8 File Offset: 0x0002DFE8
		public static Exception UnboundName(string name)
		{
			return ExprException._Eval(SR.Format("Cannot find column [{0}].", name));
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0002FDFA File Offset: 0x0002DFFA
		public static Exception InvalidString(string str)
		{
			return ExprException._Syntax(SR.Format("The expression contains an invalid string constant: {0}.", str));
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0002FE0C File Offset: 0x0002E00C
		public static Exception UndefinedFunction(string name)
		{
			return ExprException._Eval(SR.Format("The expression contains undefined function call {0}().", name));
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0002FE1E File Offset: 0x0002E01E
		public static Exception SyntaxError()
		{
			return ExprException._Syntax("Syntax error in the expression.");
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0002FE2A File Offset: 0x0002E02A
		public static Exception FunctionArgumentCount(string name)
		{
			return ExprException._Eval(SR.Format("Invalid number of arguments: function {0}().", name));
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0002FE3C File Offset: 0x0002E03C
		public static Exception MissingRightParen()
		{
			return ExprException._Syntax("The expression is missing the closing parenthesis.");
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0002FE48 File Offset: 0x0002E048
		public static Exception UnknownToken(string token, int position)
		{
			return ExprException._Syntax(SR.Format("Cannot interpret token '{0}' at position {1}.", token, position.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0002FE66 File Offset: 0x0002E066
		public static Exception UnknownToken(Tokens tokExpected, Tokens tokCurr, int position)
		{
			return ExprException._Syntax(SR.Format("Expected {0}, but actual token at the position {2} is {1}.", tokExpected.ToString(), tokCurr.ToString(), position.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0002FE9D File Offset: 0x0002E09D
		public static Exception DatatypeConvertion(Type type1, Type type2)
		{
			return ExprException._Eval(SR.Format("Cannot convert from {0} to {1}.", type1.ToString(), type2.ToString()));
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0002FEBA File Offset: 0x0002E0BA
		public static Exception DatavalueConvertion(object value, Type type, Exception innerException)
		{
			return ExprException._Eval(SR.Format("Cannot convert value '{0}' to Type: {1}.", value.ToString(), type.ToString()), innerException);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002FED8 File Offset: 0x0002E0D8
		public static Exception InvalidName(string name)
		{
			return ExprException._Syntax(SR.Format("Invalid column name [{0}].", name));
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0002FEEA File Offset: 0x0002E0EA
		public static Exception InvalidDate(string date)
		{
			return ExprException._Syntax(SR.Format("The expression contains invalid date constant '{0}'.", date));
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002FEFC File Offset: 0x0002E0FC
		public static Exception NonConstantArgument()
		{
			return ExprException._Eval("Only constant expressions are allowed in the expression list for the IN operator.");
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0002FF08 File Offset: 0x0002E108
		public static Exception InvalidPattern(string pat)
		{
			return ExprException._Eval(SR.Format("Error in Like operator: the string pattern '{0}' is invalid.", pat));
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002FF1A File Offset: 0x0002E11A
		public static Exception InWithoutParentheses()
		{
			return ExprException._Syntax("Syntax error: The items following the IN keyword must be separated by commas and be enclosed in parentheses.");
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0002FF26 File Offset: 0x0002E126
		public static Exception InWithoutList()
		{
			return ExprException._Syntax("Syntax error: The IN keyword must be followed by a non-empty list of expressions separated by commas, and also must be enclosed in parentheses.");
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002FF32 File Offset: 0x0002E132
		public static Exception InvalidIsSyntax()
		{
			return ExprException._Syntax("Syntax error: Invalid usage of 'Is' operator. Correct syntax: <expression> Is [Not] Null.");
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0002FF3E File Offset: 0x0002E13E
		public static Exception Overflow(Type type)
		{
			return ExprException._Overflow(SR.Format("Value is either too large or too small for Type '{0}'.", type.Name));
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0002FF55 File Offset: 0x0002E155
		public static Exception ArgumentType(string function, int arg, Type type)
		{
			return ExprException._Eval(SR.Format("Type mismatch in function argument: {0}(), argument {1}, expected {2}.", function, arg.ToString(CultureInfo.InvariantCulture), type.ToString()));
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002FF79 File Offset: 0x0002E179
		public static Exception ArgumentTypeInteger(string function, int arg)
		{
			return ExprException._Eval(SR.Format("Type mismatch in function argument: {0}(), argument {1}, expected one of the Integer types.", function, arg.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0002FF97 File Offset: 0x0002E197
		public static Exception TypeMismatchInBinop(int op, Type type1, Type type2)
		{
			return ExprException._Eval(SR.Format("Cannot perform '{0}' operation on {1} and {2}.", Operators.ToString(op), type1.ToString(), type2.ToString()));
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002FFBA File Offset: 0x0002E1BA
		public static Exception AmbiguousBinop(int op, Type type1, Type type2)
		{
			return ExprException._Eval(SR.Format("Operator '{0}' is ambiguous on operands of type '{1}' and '{2}'. Cannot mix signed and unsigned types. Please use explicit Convert() function.", Operators.ToString(op), type1.ToString(), type2.ToString()));
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002FFDD File Offset: 0x0002E1DD
		public static Exception UnsupportedOperator(int op)
		{
			return ExprException._Eval(SR.Format("The expression contains unsupported operator '{0}'.", Operators.ToString(op)));
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0002FFF4 File Offset: 0x0002E1F4
		public static Exception InvalidNameBracketing(string name)
		{
			return ExprException._Syntax(SR.Format("The expression contains invalid name: '{0}'.", name));
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00030006 File Offset: 0x0002E206
		public static Exception MissingOperandBefore(string op)
		{
			return ExprException._Syntax(SR.Format("Syntax error: Missing operand before '{0}' operator.", op));
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00030018 File Offset: 0x0002E218
		public static Exception TooManyRightParentheses()
		{
			return ExprException._Syntax("The expression has too many closing parentheses.");
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00030024 File Offset: 0x0002E224
		public static Exception UnresolvedRelation(string name, string expr)
		{
			return ExprException._Eval(SR.Format("The table [{0}] involved in more than one relation. You must explicitly mention a relation name in the expression '{1}'.", name, expr));
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00030037 File Offset: 0x0002E237
		internal static EvaluateException BindFailure(string relationName)
		{
			return ExprException._Eval(SR.Format("Cannot find the parent relation '{0}'.", relationName));
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00030049 File Offset: 0x0002E249
		public static Exception AggregateArgument()
		{
			return ExprException._Syntax("Syntax error in aggregate argument: Expecting a single column argument with possible 'Child' qualifier.");
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00030055 File Offset: 0x0002E255
		public static Exception AggregateUnbound(string expr)
		{
			return ExprException._Eval(SR.Format("Unbound reference in the aggregate expression '{0}'.", expr));
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00030067 File Offset: 0x0002E267
		public static Exception EvalNoContext()
		{
			return ExprException._Eval("Cannot evaluate non-constant expression without current row.");
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00030073 File Offset: 0x0002E273
		public static Exception ExpressionUnbound(string expr)
		{
			return ExprException._Eval(SR.Format("Unbound reference in the expression '{0}'.", expr));
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00030085 File Offset: 0x0002E285
		public static Exception ComputeNotAggregate(string expr)
		{
			return ExprException._Eval(SR.Format("Cannot evaluate. Expression '{0}' is not an aggregate.", expr));
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00030097 File Offset: 0x0002E297
		public static Exception FilterConvertion(string expr)
		{
			return ExprException._Eval(SR.Format("Filter expression '{0}' does not evaluate to a Boolean term.", expr));
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x000300A9 File Offset: 0x0002E2A9
		public static Exception LookupArgument()
		{
			return ExprException._Syntax("Syntax error in Lookup expression: Expecting keyword 'Parent' followed by a single column argument with possible relation qualifier: Parent[(<relation_name>)].<column_name>.");
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x000300B5 File Offset: 0x0002E2B5
		public static Exception InvalidType(string typeName)
		{
			return ExprException._Eval(SR.Format("Invalid type name '{0}'.", typeName));
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000300C7 File Offset: 0x0002E2C7
		public static Exception InvalidHoursArgument()
		{
			return ExprException._Eval("'hours' argument is out of range. Value must be between -14 and +14.");
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x000300D3 File Offset: 0x0002E2D3
		public static Exception InvalidMinutesArgument()
		{
			return ExprException._Eval("'minutes' argument is out of range. Value must be between -59 and +59.");
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x000300DF File Offset: 0x0002E2DF
		public static Exception InvalidTimeZoneRange()
		{
			return ExprException._Eval("Provided range for time one exceeds total of 14 hours.");
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x000300EB File Offset: 0x0002E2EB
		public static Exception MismatchKindandTimeSpan()
		{
			return ExprException._Eval("Kind property of provided DateTime argument, does not match 'hours' and 'minutes' arguments.");
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x000300F7 File Offset: 0x0002E2F7
		public static Exception UnsupportedDataType(Type type)
		{
			return ExceptionBuilder._Argument(SR.Format("A DataColumn of type '{0}' does not support expression.", type.FullName));
		}
	}
}
