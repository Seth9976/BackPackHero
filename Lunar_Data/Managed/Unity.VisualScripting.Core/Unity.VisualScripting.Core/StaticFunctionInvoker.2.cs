using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x0200011B RID: 283
	public sealed class StaticFunctionInvoker<TParam0, TResult> : StaticFunctionInvokerBase<TResult>
	{
		// Token: 0x06000770 RID: 1904 RVA: 0x00021CFE File Offset: 0x0001FEFE
		public StaticFunctionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00021D07 File Offset: 0x0001FF07
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 1)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target, args[0]);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00021D20 File Offset: 0x0001FF20
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

		// Token: 0x06000773 RID: 1907 RVA: 0x00021D84 File Offset: 0x0001FF84
		public object InvokeUnsafe(object target, object arg0)
		{
			return this.invoke((TParam0)((object)arg0));
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00021D9C File Offset: 0x0001FF9C
		protected override Type[] GetParameterTypes()
		{
			return new Type[] { typeof(TParam0) };
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00021DB1 File Offset: 0x0001FFB1
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Func<TParam0, TResult>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00021DC5 File Offset: 0x0001FFC5
		protected override void CreateDelegate()
		{
			this.invoke = (TParam0 param0) => ((Func<TParam0, TResult>)this.methodInfo.CreateDelegate(typeof(Func<TParam0, TResult>)))(param0);
		}

		// Token: 0x040001BE RID: 446
		private Func<TParam0, TResult> invoke;
	}
}
