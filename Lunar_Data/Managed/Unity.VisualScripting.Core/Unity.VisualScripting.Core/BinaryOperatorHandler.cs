using System;
using System.Collections.Generic;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x020000DB RID: 219
	public abstract class BinaryOperatorHandler : OperatorHandler
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x00011445 File Offset: 0x0000F645
		protected BinaryOperatorHandler(string name, string verb, string symbol, string customMethodName)
			: base(name, verb, symbol, customMethodName)
		{
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00011474 File Offset: 0x0000F674
		public virtual object Operate(object leftOperand, object rightOperand)
		{
			Type type = ((leftOperand != null) ? leftOperand.GetType() : null);
			Type type2 = ((rightOperand != null) ? rightOperand.GetType() : null);
			BinaryOperatorHandler.OperatorQuery operatorQuery;
			if (type != null && type2 != null)
			{
				operatorQuery = new BinaryOperatorHandler.OperatorQuery(type, type2);
			}
			else if (type != null && type.IsNullable())
			{
				operatorQuery = new BinaryOperatorHandler.OperatorQuery(type, type);
			}
			else if (type2 != null && type2.IsNullable())
			{
				operatorQuery = new BinaryOperatorHandler.OperatorQuery(type2, type2);
			}
			else
			{
				if (type == null && type2 == null)
				{
					return this.BothNullHandling();
				}
				return this.SingleNullHandling();
			}
			if (this.handlers.ContainsKey(operatorQuery))
			{
				return this.handlers[operatorQuery](leftOperand, rightOperand);
			}
			if (base.customMethodName != null)
			{
				if (!this.userDefinedOperators.ContainsKey(operatorQuery))
				{
					MethodInfo method = operatorQuery.leftType.GetMethod(base.customMethodName, BindingFlags.Static | BindingFlags.Public, null, new Type[] { operatorQuery.leftType, operatorQuery.rightType }, null);
					if (operatorQuery.leftType != operatorQuery.rightType)
					{
						MethodInfo method2 = operatorQuery.rightType.GetMethod(base.customMethodName, BindingFlags.Static | BindingFlags.Public, null, new Type[] { operatorQuery.leftType, operatorQuery.rightType }, null);
						if (method != null && method2 != null)
						{
							throw new AmbiguousOperatorException(base.symbol, operatorQuery.leftType, operatorQuery.rightType);
						}
						MethodInfo methodInfo = method ?? method2;
						if (methodInfo != null)
						{
							this.userDefinedOperandTypes.Add(operatorQuery, BinaryOperatorHandler.ResolveUserDefinedOperandTypes(methodInfo));
						}
						this.userDefinedOperators.Add(operatorQuery, (methodInfo != null) ? methodInfo.Prewarm() : null);
					}
					else
					{
						if (method != null)
						{
							this.userDefinedOperandTypes.Add(operatorQuery, BinaryOperatorHandler.ResolveUserDefinedOperandTypes(method));
						}
						this.userDefinedOperators.Add(operatorQuery, (method != null) ? method.Prewarm() : null);
					}
				}
				if (this.userDefinedOperators[operatorQuery] != null)
				{
					leftOperand = ConversionUtility.Convert(leftOperand, this.userDefinedOperandTypes[operatorQuery].leftType);
					rightOperand = ConversionUtility.Convert(rightOperand, this.userDefinedOperandTypes[operatorQuery].rightType);
					return this.userDefinedOperators[operatorQuery].Invoke(null, leftOperand, rightOperand);
				}
			}
			return this.CustomHandling(leftOperand, rightOperand);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000116BE File Offset: 0x0000F8BE
		protected virtual object CustomHandling(object leftOperand, object rightOperand)
		{
			throw new InvalidOperatorException(base.symbol, (leftOperand != null) ? leftOperand.GetType() : null, (rightOperand != null) ? rightOperand.GetType() : null);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000116E3 File Offset: 0x0000F8E3
		protected virtual object BothNullHandling()
		{
			throw new InvalidOperatorException(base.symbol, null, null);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000116F2 File Offset: 0x0000F8F2
		protected virtual object SingleNullHandling()
		{
			throw new InvalidOperatorException(base.symbol, null, null);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00011704 File Offset: 0x0000F904
		protected void Handle<TLeft, TRight>(Func<TLeft, TRight, object> handler, bool reverse = false)
		{
			BinaryOperatorHandler.OperatorQuery operatorQuery = new BinaryOperatorHandler.OperatorQuery(typeof(TLeft), typeof(TRight));
			if (this.handlers.ContainsKey(operatorQuery))
			{
				throw new ArgumentException(string.Format("A handler is already registered for '{0} {1} {2}'.", typeof(TLeft), base.symbol, typeof(TRight)));
			}
			this.handlers.Add(operatorQuery, (object left, object right) => handler((TLeft)((object)left), (TRight)((object)right)));
			if (reverse && typeof(TLeft) != typeof(TRight))
			{
				BinaryOperatorHandler.OperatorQuery operatorQuery2 = new BinaryOperatorHandler.OperatorQuery(typeof(TRight), typeof(TLeft));
				if (!this.handlers.ContainsKey(operatorQuery2))
				{
					this.handlers.Add(operatorQuery2, (object left, object right) => handler((TLeft)((object)left), (TRight)((object)right)));
				}
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000117E8 File Offset: 0x0000F9E8
		private static BinaryOperatorHandler.OperatorQuery ResolveUserDefinedOperandTypes(MethodInfo userDefinedOperator)
		{
			ParameterInfo[] parameters = userDefinedOperator.GetParameters();
			return new BinaryOperatorHandler.OperatorQuery(parameters[0].ParameterType, parameters[1].ParameterType);
		}

		// Token: 0x0400016C RID: 364
		private readonly Dictionary<BinaryOperatorHandler.OperatorQuery, Func<object, object, object>> handlers = new Dictionary<BinaryOperatorHandler.OperatorQuery, Func<object, object, object>>();

		// Token: 0x0400016D RID: 365
		private readonly Dictionary<BinaryOperatorHandler.OperatorQuery, IOptimizedInvoker> userDefinedOperators = new Dictionary<BinaryOperatorHandler.OperatorQuery, IOptimizedInvoker>();

		// Token: 0x0400016E RID: 366
		private readonly Dictionary<BinaryOperatorHandler.OperatorQuery, BinaryOperatorHandler.OperatorQuery> userDefinedOperandTypes = new Dictionary<BinaryOperatorHandler.OperatorQuery, BinaryOperatorHandler.OperatorQuery>();

		// Token: 0x020001DF RID: 479
		private struct OperatorQuery : IEquatable<BinaryOperatorHandler.OperatorQuery>
		{
			// Token: 0x06000D0A RID: 3338 RVA: 0x0003375D File Offset: 0x0003195D
			public OperatorQuery(Type leftType, Type rightType)
			{
				this.leftType = leftType;
				this.rightType = rightType;
			}

			// Token: 0x06000D0B RID: 3339 RVA: 0x0003376D File Offset: 0x0003196D
			public bool Equals(BinaryOperatorHandler.OperatorQuery other)
			{
				return this.leftType == other.leftType && this.rightType == other.rightType;
			}

			// Token: 0x06000D0C RID: 3340 RVA: 0x00033795 File Offset: 0x00031995
			public override bool Equals(object obj)
			{
				return obj is BinaryOperatorHandler.OperatorQuery && this.Equals((BinaryOperatorHandler.OperatorQuery)obj);
			}

			// Token: 0x06000D0D RID: 3341 RVA: 0x000337AD File Offset: 0x000319AD
			public override int GetHashCode()
			{
				return HashUtility.GetHashCode<Type, Type>(this.leftType, this.rightType);
			}

			// Token: 0x040003FA RID: 1018
			public readonly Type leftType;

			// Token: 0x040003FB RID: 1019
			public readonly Type rightType;
		}
	}
}
