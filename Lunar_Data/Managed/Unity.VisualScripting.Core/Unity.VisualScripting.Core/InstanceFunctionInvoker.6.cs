using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000107 RID: 263
	public sealed class InstanceFunctionInvoker<TTarget, TParam0, TParam1, TParam2, TParam3, TParam4, TResult> : InstanceFunctionInvokerBase<TTarget, TResult>
	{
		// Token: 0x060006D0 RID: 1744 RVA: 0x0001FC84 File Offset: 0x0001DE84
		public InstanceFunctionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001FC8D File Offset: 0x0001DE8D
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 5)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target, args[0], args[1], args[2], args[3], args[4]);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001FCB4 File Offset: 0x0001DEB4
		public override object Invoke(object target, object arg0, object arg1, object arg2, object arg3, object arg4)
		{
			if (OptimizedReflection.safeMode)
			{
				this.VerifyTarget(target);
				base.VerifyArgument<TParam0>(this.methodInfo, 0, arg0);
				base.VerifyArgument<TParam1>(this.methodInfo, 1, arg1);
				base.VerifyArgument<TParam2>(this.methodInfo, 2, arg2);
				base.VerifyArgument<TParam3>(this.methodInfo, 3, arg3);
				base.VerifyArgument<TParam4>(this.methodInfo, 4, arg4);
				try
				{
					return this.InvokeUnsafe(target, arg0, arg1, arg2, arg3, arg4);
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
			return this.InvokeUnsafe(target, arg0, arg1, arg2, arg3, arg4);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001FD60 File Offset: 0x0001DF60
		public object InvokeUnsafe(object target, object arg0, object arg1, object arg2, object arg3, object arg4)
		{
			return this.invoke((TTarget)((object)target), (TParam0)((object)arg0), (TParam1)((object)arg1), (TParam2)((object)arg2), (TParam3)((object)arg3), (TParam4)((object)arg4));
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001FD9C File Offset: 0x0001DF9C
		protected override Type[] GetParameterTypes()
		{
			return new Type[]
			{
				typeof(TParam0),
				typeof(TParam1),
				typeof(TParam2),
				typeof(TParam3),
				typeof(TParam4)
			};
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001FDF0 File Offset: 0x0001DFF0
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Func<TTarget, TParam0, TParam1, TParam2, TParam3, TParam4, TResult>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001FE04 File Offset: 0x0001E004
		protected override void CreateDelegate()
		{
			this.invoke = (Func<TTarget, TParam0, TParam1, TParam2, TParam3, TParam4, TResult>)this.methodInfo.CreateDelegate(typeof(Func<TTarget, TParam0, TParam1, TParam2, TParam3, TParam4, TResult>));
		}

		// Token: 0x040001A3 RID: 419
		private Func<TTarget, TParam0, TParam1, TParam2, TParam3, TParam4, TResult> invoke;
	}
}
