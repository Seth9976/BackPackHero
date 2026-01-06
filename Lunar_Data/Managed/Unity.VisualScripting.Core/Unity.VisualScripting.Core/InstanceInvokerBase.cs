using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000108 RID: 264
	public abstract class InstanceInvokerBase<TTarget> : InvokerBase
	{
		// Token: 0x060006D7 RID: 1751 RVA: 0x0001FE28 File Offset: 0x0001E028
		protected InstanceInvokerBase(MethodInfo methodInfo)
			: base(methodInfo)
		{
			if (OptimizedReflection.safeMode)
			{
				if (methodInfo.DeclaringType != typeof(TTarget))
				{
					throw new ArgumentException("Declaring type of method info doesn't match generic type.", "methodInfo");
				}
				if (methodInfo.IsStatic)
				{
					throw new ArgumentException("The method is static.", "methodInfo");
				}
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001FE84 File Offset: 0x0001E084
		protected sealed override void CompileExpression()
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(TTarget), "target");
			ParameterExpression[] parameterExpressions = base.GetParameterExpressions();
			ParameterExpression[] array = new ParameterExpression[1 + parameterExpressions.Length];
			array[0] = parameterExpression;
			Array.Copy(parameterExpressions, 0, array, 1, parameterExpressions.Length);
			Expression expression = parameterExpression;
			MethodInfo methodInfo = this.methodInfo;
			Expression[] array2 = parameterExpressions;
			MethodCallExpression methodCallExpression = Expression.Call(expression, methodInfo, array2);
			this.CompileExpression(methodCallExpression, array);
		}

		// Token: 0x060006D9 RID: 1753
		protected abstract void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions);

		// Token: 0x060006DA RID: 1754 RVA: 0x0001FEE2 File Offset: 0x0001E0E2
		protected override void VerifyTarget(object target)
		{
			OptimizedReflection.VerifyInstanceTarget<TTarget>(target);
		}
	}
}
