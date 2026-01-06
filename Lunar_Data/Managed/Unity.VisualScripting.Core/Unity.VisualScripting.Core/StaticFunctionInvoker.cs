using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x0200011A RID: 282
	public sealed class StaticFunctionInvoker<TResult> : StaticFunctionInvokerBase<TResult>
	{
		// Token: 0x06000768 RID: 1896 RVA: 0x00021C29 File Offset: 0x0001FE29
		public StaticFunctionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00021C32 File Offset: 0x0001FE32
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 0)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00021C48 File Offset: 0x0001FE48
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

		// Token: 0x0600076B RID: 1899 RVA: 0x00021C9C File Offset: 0x0001FE9C
		public object InvokeUnsafe(object target)
		{
			return this.invoke();
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00021CAE File Offset: 0x0001FEAE
		protected override Type[] GetParameterTypes()
		{
			return Type.EmptyTypes;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00021CB5 File Offset: 0x0001FEB5
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Func<TResult>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00021CC9 File Offset: 0x0001FEC9
		protected override void CreateDelegate()
		{
			this.invoke = () => ((Func<TResult>)this.methodInfo.CreateDelegate(typeof(Func<TResult>)))();
		}

		// Token: 0x040001BD RID: 445
		private Func<TResult> invoke;
	}
}
