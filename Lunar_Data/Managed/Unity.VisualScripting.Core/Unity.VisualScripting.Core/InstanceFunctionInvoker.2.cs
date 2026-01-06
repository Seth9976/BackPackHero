using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000103 RID: 259
	public sealed class InstanceFunctionInvoker<TTarget, TParam0, TResult> : InstanceFunctionInvokerBase<TTarget, TResult>
	{
		// Token: 0x060006B4 RID: 1716 RVA: 0x0001F7D1 File Offset: 0x0001D9D1
		public InstanceFunctionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001F7DA File Offset: 0x0001D9DA
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 1)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target, args[0]);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001F7F4 File Offset: 0x0001D9F4
		public override object Invoke(object target, object arg0)
		{
			if (OptimizedReflection.safeMode)
			{
				this.VerifyTarget(target);
				base.VerifyArgument<TParam0>(this.methodInfo, 0, arg0);
				try
				{
					return this.InvokeUnsafe(target, arg0);
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
			return this.InvokeUnsafe(target, arg0);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001F858 File Offset: 0x0001DA58
		public object InvokeUnsafe(object target, object arg0)
		{
			return this.invoke((TTarget)((object)target), (TParam0)((object)arg0));
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001F876 File Offset: 0x0001DA76
		protected override Type[] GetParameterTypes()
		{
			return new Type[] { typeof(TParam0) };
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001F88B File Offset: 0x0001DA8B
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Func<TTarget, TParam0, TResult>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001F89F File Offset: 0x0001DA9F
		protected override void CreateDelegate()
		{
			this.invoke = (Func<TTarget, TParam0, TResult>)this.methodInfo.CreateDelegate(typeof(Func<TTarget, TParam0, TResult>));
		}

		// Token: 0x0400019F RID: 415
		private Func<TTarget, TParam0, TResult> invoke;
	}
}
