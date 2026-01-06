using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000102 RID: 258
	public sealed class InstanceFunctionInvoker<TTarget, TResult> : InstanceFunctionInvokerBase<TTarget, TResult>
	{
		// Token: 0x060006AD RID: 1709 RVA: 0x0001F709 File Offset: 0x0001D909
		public InstanceFunctionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001F712 File Offset: 0x0001D912
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 0)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001F728 File Offset: 0x0001D928
		public override object Invoke(object target)
		{
			if (OptimizedReflection.safeMode)
			{
				this.VerifyTarget(target);
				try
				{
					return this.InvokeUnsafe(target);
				}
				catch (TargetInvocationException)
				{
					throw;
				}
				catch (Exception ex)
				{
					throw new TargetInvocationException(ex);
				}
			}
			return this.InvokeUnsafe(target);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001F77C File Offset: 0x0001D97C
		public object InvokeUnsafe(object target)
		{
			return this.invoke((TTarget)((object)target));
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001F794 File Offset: 0x0001D994
		protected override Type[] GetParameterTypes()
		{
			return Type.EmptyTypes;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001F79B File Offset: 0x0001D99B
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Func<TTarget, TResult>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001F7AF File Offset: 0x0001D9AF
		protected override void CreateDelegate()
		{
			this.invoke = (Func<TTarget, TResult>)this.methodInfo.CreateDelegate(typeof(Func<TTarget, TResult>));
		}

		// Token: 0x0400019E RID: 414
		private Func<TTarget, TResult> invoke;
	}
}
