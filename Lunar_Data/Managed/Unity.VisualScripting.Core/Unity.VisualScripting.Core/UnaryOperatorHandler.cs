using System;
using System.Collections.Generic;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x020000F4 RID: 244
	public abstract class UnaryOperatorHandler : OperatorHandler
	{
		// Token: 0x06000664 RID: 1636 RVA: 0x0001EB11 File Offset: 0x0001CD11
		protected UnaryOperatorHandler(string name, string verb, string symbol, string customMethodName)
			: base(name, verb, symbol, customMethodName)
		{
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001EB40 File Offset: 0x0001CD40
		public object Operate(object operand)
		{
			Ensure.That("operand").IsNotNull<object>(operand);
			Type type = operand.GetType();
			if (this.manualHandlers.ContainsKey(type))
			{
				return this.manualHandlers[type](operand);
			}
			if (base.customMethodName != null)
			{
				if (!this.userDefinedOperators.ContainsKey(type))
				{
					MethodInfo method = type.GetMethod(base.customMethodName, BindingFlags.Static | BindingFlags.Public);
					if (method != null)
					{
						this.userDefinedOperandTypes.Add(type, UnaryOperatorHandler.ResolveUserDefinedOperandType(method));
					}
					this.userDefinedOperators.Add(type, (method != null) ? method.Prewarm() : null);
				}
				if (this.userDefinedOperators[type] != null)
				{
					operand = ConversionUtility.Convert(operand, this.userDefinedOperandTypes[type]);
					return this.userDefinedOperators[type].Invoke(null, operand);
				}
			}
			return this.CustomHandling(operand);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001EC1D File Offset: 0x0001CE1D
		protected virtual object CustomHandling(object operand)
		{
			throw new InvalidOperatorException(base.symbol, operand.GetType());
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001EC30 File Offset: 0x0001CE30
		protected void Handle<T>(Func<T, object> handler)
		{
			this.manualHandlers.Add(typeof(T), (object operand) => handler((T)((object)operand)));
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001EC6B File Offset: 0x0001CE6B
		private static Type ResolveUserDefinedOperandType(MethodInfo userDefinedOperator)
		{
			return userDefinedOperator.GetParameters()[0].ParameterType;
		}

		// Token: 0x04000192 RID: 402
		private readonly Dictionary<Type, Func<object, object>> manualHandlers = new Dictionary<Type, Func<object, object>>();

		// Token: 0x04000193 RID: 403
		private readonly Dictionary<Type, IOptimizedInvoker> userDefinedOperators = new Dictionary<Type, IOptimizedInvoker>();

		// Token: 0x04000194 RID: 404
		private readonly Dictionary<Type, Type> userDefinedOperandTypes = new Dictionary<Type, Type>();
	}
}
