using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000105 RID: 261
	public sealed class InstanceFunctionInvoker<TTarget, TParam0, TParam1, TParam2, TResult> : InstanceFunctionInvokerBase<TTarget, TResult>
	{
		// Token: 0x060006C2 RID: 1730 RVA: 0x0001F9D8 File Offset: 0x0001DBD8
		public InstanceFunctionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001F9E1 File Offset: 0x0001DBE1
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 3)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target, args[0], args[1], args[2]);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001FA00 File Offset: 0x0001DC00
		public override object Invoke(object target, object arg0, object arg1, object arg2)
		{
			if (OptimizedReflection.safeMode)
			{
				this.VerifyTarget(target);
				base.VerifyArgument<TParam0>(this.methodInfo, 0, arg0);
				base.VerifyArgument<TParam1>(this.methodInfo, 1, arg1);
				base.VerifyArgument<TParam2>(this.methodInfo, 2, arg2);
				try
				{
					return this.InvokeUnsafe(target, arg0, arg1, arg2);
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
			return this.InvokeUnsafe(target, arg0, arg1, arg2);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001FA88 File Offset: 0x0001DC88
		public object InvokeUnsafe(object target, object arg0, object arg1, object arg2)
		{
			return this.invoke((TTarget)((object)target), (TParam0)((object)arg0), (TParam1)((object)arg1), (TParam2)((object)arg2));
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001FAB3 File Offset: 0x0001DCB3
		protected override Type[] GetParameterTypes()
		{
			return new Type[]
			{
				typeof(TParam0),
				typeof(TParam1),
				typeof(TParam2)
			};
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001FAE2 File Offset: 0x0001DCE2
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Func<TTarget, TParam0, TParam1, TParam2, TResult>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001FAF6 File Offset: 0x0001DCF6
		protected override void CreateDelegate()
		{
			this.invoke = (Func<TTarget, TParam0, TParam1, TParam2, TResult>)this.methodInfo.CreateDelegate(typeof(Func<TTarget, TParam0, TParam1, TParam2, TResult>));
		}

		// Token: 0x040001A1 RID: 417
		private Func<TTarget, TParam0, TParam1, TParam2, TResult> invoke;
	}
}
