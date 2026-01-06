using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x020000EE RID: 238
	public static class OperatorUtility
	{
		// Token: 0x06000640 RID: 1600 RVA: 0x0001C210 File Offset: 0x0001A410
		static OperatorUtility()
		{
			OperatorUtility.unaryOperatorHandlers.Add(UnaryOperator.LogicalNegation, OperatorUtility.logicalNegationHandler);
			OperatorUtility.unaryOperatorHandlers.Add(UnaryOperator.NumericNegation, OperatorUtility.numericNegationHandler);
			OperatorUtility.unaryOperatorHandlers.Add(UnaryOperator.Increment, OperatorUtility.incrementHandler);
			OperatorUtility.unaryOperatorHandlers.Add(UnaryOperator.Decrement, OperatorUtility.decrementHandler);
			OperatorUtility.unaryOperatorHandlers.Add(UnaryOperator.Plus, OperatorUtility.plusHandler);
			OperatorUtility.binaryOpeatorHandlers.Add(BinaryOperator.Addition, OperatorUtility.additionHandler);
			OperatorUtility.binaryOpeatorHandlers.Add(BinaryOperator.Subtraction, OperatorUtility.subtractionHandler);
			OperatorUtility.binaryOpeatorHandlers.Add(BinaryOperator.Multiplication, OperatorUtility.multiplicationHandler);
			OperatorUtility.binaryOpeatorHandlers.Add(BinaryOperator.Division, OperatorUtility.divisionHandler);
			OperatorUtility.binaryOpeatorHandlers.Add(BinaryOperator.Modulo, OperatorUtility.moduloHandler);
			OperatorUtility.binaryOpeatorHandlers.Add(BinaryOperator.And, OperatorUtility.andHandler);
			OperatorUtility.binaryOpeatorHandlers.Add(BinaryOperator.Or, OperatorUtility.orHandler);
			OperatorUtility.binaryOpeatorHandlers.Add(BinaryOperator.ExclusiveOr, OperatorUtility.exclusiveOrHandler);
			OperatorUtility.binaryOpeatorHandlers.Add(BinaryOperator.Equality, OperatorUtility.equalityHandler);
			OperatorUtility.binaryOpeatorHandlers.Add(BinaryOperator.Inequality, OperatorUtility.inequalityHandler);
			OperatorUtility.binaryOpeatorHandlers.Add(BinaryOperator.GreaterThan, OperatorUtility.greaterThanHandler);
			OperatorUtility.binaryOpeatorHandlers.Add(BinaryOperator.LessThan, OperatorUtility.lessThanHandler);
			OperatorUtility.binaryOpeatorHandlers.Add(BinaryOperator.GreaterThanOrEqual, OperatorUtility.greaterThanOrEqualHandler);
			OperatorUtility.binaryOpeatorHandlers.Add(BinaryOperator.LessThanOrEqual, OperatorUtility.lessThanOrEqualHandler);
			OperatorUtility.binaryOpeatorHandlers.Add(BinaryOperator.LeftShift, OperatorUtility.leftShiftHandler);
			OperatorUtility.binaryOpeatorHandlers.Add(BinaryOperator.RightShift, OperatorUtility.rightShiftHandler);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001C826 File Offset: 0x0001AA26
		public static UnaryOperatorHandler GetHandler(UnaryOperator @operator)
		{
			if (OperatorUtility.unaryOperatorHandlers.ContainsKey(@operator))
			{
				return OperatorUtility.unaryOperatorHandlers[@operator];
			}
			throw new UnexpectedEnumValueException<UnaryOperator>(@operator);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001C847 File Offset: 0x0001AA47
		public static BinaryOperatorHandler GetHandler(BinaryOperator @operator)
		{
			if (OperatorUtility.binaryOpeatorHandlers.ContainsKey(@operator))
			{
				return OperatorUtility.binaryOpeatorHandlers[@operator];
			}
			throw new UnexpectedEnumValueException<BinaryOperator>(@operator);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001C868 File Offset: 0x0001AA68
		public static string Symbol(this UnaryOperator @operator)
		{
			return OperatorUtility.GetHandler(@operator).symbol;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001C875 File Offset: 0x0001AA75
		public static string Symbol(this BinaryOperator @operator)
		{
			return OperatorUtility.GetHandler(@operator).symbol;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001C882 File Offset: 0x0001AA82
		public static string Name(this UnaryOperator @operator)
		{
			return OperatorUtility.GetHandler(@operator).name;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001C88F File Offset: 0x0001AA8F
		public static string Name(this BinaryOperator @operator)
		{
			return OperatorUtility.GetHandler(@operator).name;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001C89C File Offset: 0x0001AA9C
		public static string Verb(this UnaryOperator @operator)
		{
			return OperatorUtility.GetHandler(@operator).verb;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001C8A9 File Offset: 0x0001AAA9
		public static string Verb(this BinaryOperator @operator)
		{
			return OperatorUtility.GetHandler(@operator).verb;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001C8B6 File Offset: 0x0001AAB6
		public static object Operate(UnaryOperator @operator, object x)
		{
			if (!OperatorUtility.unaryOperatorHandlers.ContainsKey(@operator))
			{
				throw new UnexpectedEnumValueException<UnaryOperator>(@operator);
			}
			return OperatorUtility.unaryOperatorHandlers[@operator].Operate(x);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001C8DD File Offset: 0x0001AADD
		public static object Operate(BinaryOperator @operator, object a, object b)
		{
			if (!OperatorUtility.binaryOpeatorHandlers.ContainsKey(@operator))
			{
				throw new UnexpectedEnumValueException<BinaryOperator>(@operator);
			}
			return OperatorUtility.binaryOpeatorHandlers[@operator].Operate(a, b);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001C905 File Offset: 0x0001AB05
		public static object Negate(object x)
		{
			return OperatorUtility.numericNegationHandler.Operate(x);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001C912 File Offset: 0x0001AB12
		public static object Not(object x)
		{
			return OperatorUtility.logicalNegationHandler.Operate(x);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001C91F File Offset: 0x0001AB1F
		public static object UnaryPlus(object x)
		{
			return OperatorUtility.plusHandler.Operate(x);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001C92C File Offset: 0x0001AB2C
		public static object Increment(object x)
		{
			return OperatorUtility.incrementHandler.Operate(x);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001C939 File Offset: 0x0001AB39
		public static object Decrement(object x)
		{
			return OperatorUtility.decrementHandler.Operate(x);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001C946 File Offset: 0x0001AB46
		public static object And(object a, object b)
		{
			return OperatorUtility.andHandler.Operate(a, b);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001C954 File Offset: 0x0001AB54
		public static object Or(object a, object b)
		{
			return OperatorUtility.orHandler.Operate(a, b);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001C962 File Offset: 0x0001AB62
		public static object ExclusiveOr(object a, object b)
		{
			return OperatorUtility.exclusiveOrHandler.Operate(a, b);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001C970 File Offset: 0x0001AB70
		public static object Add(object a, object b)
		{
			return OperatorUtility.additionHandler.Operate(a, b);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001C97E File Offset: 0x0001AB7E
		public static object Subtract(object a, object b)
		{
			return OperatorUtility.subtractionHandler.Operate(a, b);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001C98C File Offset: 0x0001AB8C
		public static object Multiply(object a, object b)
		{
			return OperatorUtility.multiplicationHandler.Operate(a, b);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001C99A File Offset: 0x0001AB9A
		public static object Divide(object a, object b)
		{
			return OperatorUtility.divisionHandler.Operate(a, b);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001C9A8 File Offset: 0x0001ABA8
		public static object Modulo(object a, object b)
		{
			return OperatorUtility.moduloHandler.Operate(a, b);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001C9B6 File Offset: 0x0001ABB6
		public static bool Equal(object a, object b)
		{
			return (bool)OperatorUtility.equalityHandler.Operate(a, b);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001C9C9 File Offset: 0x0001ABC9
		public static bool NotEqual(object a, object b)
		{
			return (bool)OperatorUtility.inequalityHandler.Operate(a, b);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001C9DC File Offset: 0x0001ABDC
		public static bool GreaterThan(object a, object b)
		{
			return (bool)OperatorUtility.greaterThanHandler.Operate(a, b);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001C9EF File Offset: 0x0001ABEF
		public static bool LessThan(object a, object b)
		{
			return (bool)OperatorUtility.lessThanHandler.Operate(a, b);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001CA02 File Offset: 0x0001AC02
		public static bool GreaterThanOrEqual(object a, object b)
		{
			return (bool)OperatorUtility.greaterThanOrEqualHandler.Operate(a, b);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001CA15 File Offset: 0x0001AC15
		public static bool LessThanOrEqual(object a, object b)
		{
			return (bool)OperatorUtility.lessThanOrEqualHandler.Operate(a, b);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001CA28 File Offset: 0x0001AC28
		public static object LeftShift(object a, object b)
		{
			return OperatorUtility.leftShiftHandler.Operate(a, b);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001CA36 File Offset: 0x0001AC36
		public static object RightShift(object a, object b)
		{
			return OperatorUtility.rightShiftHandler.Operate(a, b);
		}

		// Token: 0x04000173 RID: 371
		public static readonly Dictionary<string, string> operatorNames = new Dictionary<string, string>
		{
			{ "op_Addition", "+" },
			{ "op_Subtraction", "-" },
			{ "op_Multiply", "*" },
			{ "op_Division", "/" },
			{ "op_Modulus", "%" },
			{ "op_ExclusiveOr", "^" },
			{ "op_BitwiseAnd", "&" },
			{ "op_BitwiseOr", "|" },
			{ "op_LogicalAnd", "&&" },
			{ "op_LogicalOr", "||" },
			{ "op_Assign", "=" },
			{ "op_LeftShift", "<<" },
			{ "op_RightShift", ">>" },
			{ "op_Equality", "==" },
			{ "op_GreaterThan", ">" },
			{ "op_LessThan", "<" },
			{ "op_Inequality", "!=" },
			{ "op_GreaterThanOrEqual", ">=" },
			{ "op_LessThanOrEqual", "<=" },
			{ "op_MultiplicationAssignment", "*=" },
			{ "op_SubtractionAssignment", "-=" },
			{ "op_ExclusiveOrAssignment", "^=" },
			{ "op_LeftShiftAssignment", "<<=" },
			{ "op_ModulusAssignment", "%=" },
			{ "op_AdditionAssignment", "+=" },
			{ "op_BitwiseAndAssignment", "&=" },
			{ "op_BitwiseOrAssignment", "|=" },
			{ "op_Comma", "," },
			{ "op_DivisionAssignment", "/=" },
			{ "op_Decrement", "--" },
			{ "op_Increment", "++" },
			{ "op_UnaryNegation", "-" },
			{ "op_UnaryPlus", "+" },
			{ "op_OnesComplement", "~" }
		};

		// Token: 0x04000174 RID: 372
		public static readonly Dictionary<string, int> operatorRanks = new Dictionary<string, int>
		{
			{ "op_Addition", 2 },
			{ "op_Subtraction", 2 },
			{ "op_Multiply", 2 },
			{ "op_Division", 2 },
			{ "op_Modulus", 2 },
			{ "op_ExclusiveOr", 2 },
			{ "op_BitwiseAnd", 2 },
			{ "op_BitwiseOr", 2 },
			{ "op_LogicalAnd", 2 },
			{ "op_LogicalOr", 2 },
			{ "op_Assign", 2 },
			{ "op_LeftShift", 2 },
			{ "op_RightShift", 2 },
			{ "op_Equality", 2 },
			{ "op_GreaterThan", 2 },
			{ "op_LessThan", 2 },
			{ "op_Inequality", 2 },
			{ "op_GreaterThanOrEqual", 2 },
			{ "op_LessThanOrEqual", 2 },
			{ "op_MultiplicationAssignment", 2 },
			{ "op_SubtractionAssignment", 2 },
			{ "op_ExclusiveOrAssignment", 2 },
			{ "op_LeftShiftAssignment", 2 },
			{ "op_ModulusAssignment", 2 },
			{ "op_AdditionAssignment", 2 },
			{ "op_BitwiseAndAssignment", 2 },
			{ "op_BitwiseOrAssignment", 2 },
			{ "op_Comma", 2 },
			{ "op_DivisionAssignment", 2 },
			{ "op_Decrement", 1 },
			{ "op_Increment", 1 },
			{ "op_UnaryNegation", 1 },
			{ "op_UnaryPlus", 1 },
			{ "op_OnesComplement", 1 }
		};

		// Token: 0x04000175 RID: 373
		private static readonly Dictionary<UnaryOperator, UnaryOperatorHandler> unaryOperatorHandlers = new Dictionary<UnaryOperator, UnaryOperatorHandler>();

		// Token: 0x04000176 RID: 374
		private static readonly Dictionary<BinaryOperator, BinaryOperatorHandler> binaryOpeatorHandlers = new Dictionary<BinaryOperator, BinaryOperatorHandler>();

		// Token: 0x04000177 RID: 375
		private static readonly LogicalNegationHandler logicalNegationHandler = new LogicalNegationHandler();

		// Token: 0x04000178 RID: 376
		private static readonly NumericNegationHandler numericNegationHandler = new NumericNegationHandler();

		// Token: 0x04000179 RID: 377
		private static readonly IncrementHandler incrementHandler = new IncrementHandler();

		// Token: 0x0400017A RID: 378
		private static readonly DecrementHandler decrementHandler = new DecrementHandler();

		// Token: 0x0400017B RID: 379
		private static readonly PlusHandler plusHandler = new PlusHandler();

		// Token: 0x0400017C RID: 380
		private static readonly AdditionHandler additionHandler = new AdditionHandler();

		// Token: 0x0400017D RID: 381
		private static readonly SubtractionHandler subtractionHandler = new SubtractionHandler();

		// Token: 0x0400017E RID: 382
		private static readonly MultiplicationHandler multiplicationHandler = new MultiplicationHandler();

		// Token: 0x0400017F RID: 383
		private static readonly DivisionHandler divisionHandler = new DivisionHandler();

		// Token: 0x04000180 RID: 384
		private static readonly ModuloHandler moduloHandler = new ModuloHandler();

		// Token: 0x04000181 RID: 385
		private static readonly AndHandler andHandler = new AndHandler();

		// Token: 0x04000182 RID: 386
		private static readonly OrHandler orHandler = new OrHandler();

		// Token: 0x04000183 RID: 387
		private static readonly ExclusiveOrHandler exclusiveOrHandler = new ExclusiveOrHandler();

		// Token: 0x04000184 RID: 388
		private static readonly EqualityHandler equalityHandler = new EqualityHandler();

		// Token: 0x04000185 RID: 389
		private static readonly InequalityHandler inequalityHandler = new InequalityHandler();

		// Token: 0x04000186 RID: 390
		private static readonly GreaterThanHandler greaterThanHandler = new GreaterThanHandler();

		// Token: 0x04000187 RID: 391
		private static readonly LessThanHandler lessThanHandler = new LessThanHandler();

		// Token: 0x04000188 RID: 392
		private static readonly GreaterThanOrEqualHandler greaterThanOrEqualHandler = new GreaterThanOrEqualHandler();

		// Token: 0x04000189 RID: 393
		private static readonly LessThanOrEqualHandler lessThanOrEqualHandler = new LessThanOrEqualHandler();

		// Token: 0x0400018A RID: 394
		private static readonly LeftShiftHandler leftShiftHandler = new LeftShiftHandler();

		// Token: 0x0400018B RID: 395
		private static readonly RightShiftHandler rightShiftHandler = new RightShiftHandler();
	}
}
