using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000120 RID: 288
	public abstract class StaticInvokerBase : InvokerBase
	{
		// Token: 0x06000798 RID: 1944 RVA: 0x000223A0 File Offset: 0x000205A0
		protected StaticInvokerBase(MethodInfo methodInfo)
			: base(methodInfo)
		{
			if (OptimizedReflection.safeMode && !methodInfo.IsStatic)
			{
				throw new ArgumentException("The method isn't static.", "methodInfo");
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x000223C8 File Offset: 0x000205C8
		protected sealed override void CompileExpression()
		{
			ParameterExpression[] parameterExpressions = base.GetParameterExpressions();
			MethodInfo methodInfo = this.methodInfo;
			Expression[] array = parameterExpressions;
			MethodCallExpression methodCallExpression = Expression.Call(methodInfo, array);
			this.CompileExpression(methodCallExpression, parameterExpressions);
		}

		// Token: 0x0600079A RID: 1946
		protected abstract void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions);

		// Token: 0x0600079B RID: 1947 RVA: 0x000223F3 File Offset: 0x000205F3
		protected override void VerifyTarget(object target)
		{
			OptimizedReflection.VerifyStaticTarget(this.targetType, target);
		}
	}
}
