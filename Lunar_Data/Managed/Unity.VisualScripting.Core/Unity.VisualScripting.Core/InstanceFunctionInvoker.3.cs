using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000104 RID: 260
	public sealed class InstanceFunctionInvoker<TTarget, TParam0, TParam1, TResult> : InstanceFunctionInvokerBase<TTarget, TResult>
	{
		// Token: 0x060006BB RID: 1723 RVA: 0x0001F8C1 File Offset: 0x0001DAC1
		public InstanceFunctionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001F8CA File Offset: 0x0001DACA
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 2)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target, args[0], args[1]);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001F8E8 File Offset: 0x0001DAE8
		public override object Invoke(object target, object arg0, object arg1)
		{
			if (OptimizedReflection.safeMode)
			{
				this.VerifyTarget(target);
				base.VerifyArgument<TParam0>(this.methodInfo, 0, arg0);
				base.VerifyArgument<TParam1>(this.methodInfo, 1, arg1);
				try
				{
					return this.InvokeUnsafe(target, arg0, arg1);
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
			return this.InvokeUnsafe(target, arg0, arg1);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001F95C File Offset: 0x0001DB5C
		public object InvokeUnsafe(object target, object arg0, object arg1)
		{
			return this.invoke((TTarget)((object)target), (TParam0)((object)arg0), (TParam1)((object)arg1));
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001F980 File Offset: 0x0001DB80
		protected override Type[] GetParameterTypes()
		{
			return new Type[]
			{
				typeof(TParam0),
				typeof(TParam1)
			};
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001F9A2 File Offset: 0x0001DBA2
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Func<TTarget, TParam0, TParam1, TResult>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001F9B6 File Offset: 0x0001DBB6
		protected override void CreateDelegate()
		{
			this.invoke = (Func<TTarget, TParam0, TParam1, TResult>)this.methodInfo.CreateDelegate(typeof(Func<TTarget, TParam0, TParam1, TResult>));
		}

		// Token: 0x040001A0 RID: 416
		private Func<TTarget, TParam0, TParam1, TResult> invoke;
	}
}
