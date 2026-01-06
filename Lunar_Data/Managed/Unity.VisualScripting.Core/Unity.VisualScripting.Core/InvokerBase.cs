using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x0200010A RID: 266
	public abstract class InvokerBase : IOptimizedInvoker
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x00020211 File Offset: 0x0001E411
		protected InvokerBase(MethodInfo methodInfo)
		{
			if (OptimizedReflection.safeMode && methodInfo == null)
			{
				throw new ArgumentNullException("methodInfo");
			}
			this.methodInfo = methodInfo;
			this.targetType = methodInfo.DeclaringType;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00020248 File Offset: 0x0001E448
		protected void VerifyArgument<TParam>(MethodInfo methodInfo, int argIndex, object arg)
		{
			if (!typeof(TParam).IsAssignableFrom(arg))
			{
				throw new ArgumentException(string.Format("The provided argument value for '{0}.{1}' does not match the parameter type.\nProvided: {2}\nExpected: {3}", new object[]
				{
					this.targetType,
					methodInfo.Name,
					((arg != null) ? arg.GetType().ToString() : null) ?? "null",
					typeof(TParam)
				}), methodInfo.GetParameters()[argIndex].Name);
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x000202C6 File Offset: 0x0001E4C6
		public void Compile()
		{
			if (OptimizedReflection.useJit)
			{
				this.CompileExpression();
				return;
			}
			this.CreateDelegate();
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x000202DC File Offset: 0x0001E4DC
		protected ParameterExpression[] GetParameterExpressions()
		{
			ParameterInfo[] parameters = this.methodInfo.GetParameters();
			Type[] parameterTypes = this.GetParameterTypes();
			if (parameters.Length != parameterTypes.Length)
			{
				throw new ArgumentException("Parameter count of method info doesn't match generic argument count.", "methodInfo");
			}
			for (int i = 0; i < parameterTypes.Length; i++)
			{
				if (parameterTypes[i] != parameters[i].ParameterType)
				{
					throw new ArgumentException("Parameter type of method info doesn't match generic argument.", "methodInfo");
				}
			}
			ParameterExpression[] array = new ParameterExpression[parameterTypes.Length];
			for (int j = 0; j < parameterTypes.Length; j++)
			{
				array[j] = Expression.Parameter(parameterTypes[j], "parameter" + j.ToString());
			}
			return array;
		}

		// Token: 0x060006E5 RID: 1765
		protected abstract Type[] GetParameterTypes();

		// Token: 0x060006E6 RID: 1766
		public abstract object Invoke(object target, params object[] args);

		// Token: 0x060006E7 RID: 1767 RVA: 0x0002037E File Offset: 0x0001E57E
		public virtual object Invoke(object target)
		{
			throw new TargetParameterCountException();
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00020385 File Offset: 0x0001E585
		public virtual object Invoke(object target, object arg0)
		{
			throw new TargetParameterCountException();
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0002038C File Offset: 0x0001E58C
		public virtual object Invoke(object target, object arg0, object arg1)
		{
			throw new TargetParameterCountException();
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00020393 File Offset: 0x0001E593
		public virtual object Invoke(object target, object arg0, object arg1, object arg2)
		{
			throw new TargetParameterCountException();
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0002039A File Offset: 0x0001E59A
		public virtual object Invoke(object target, object arg0, object arg1, object arg2, object arg3)
		{
			throw new TargetParameterCountException();
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x000203A1 File Offset: 0x0001E5A1
		public virtual object Invoke(object target, object arg0, object arg1, object arg2, object arg3, object arg4)
		{
			throw new TargetParameterCountException();
		}

		// Token: 0x060006ED RID: 1773
		protected abstract void CompileExpression();

		// Token: 0x060006EE RID: 1774
		protected abstract void CreateDelegate();

		// Token: 0x060006EF RID: 1775
		protected abstract void VerifyTarget(object target);

		// Token: 0x040001A7 RID: 423
		protected readonly Type targetType;

		// Token: 0x040001A8 RID: 424
		protected readonly MethodInfo methodInfo;
	}
}
