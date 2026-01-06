using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x0200011C RID: 284
	public sealed class StaticFunctionInvoker<TParam0, TParam1, TResult> : StaticFunctionInvokerBase<TResult>
	{
		// Token: 0x06000778 RID: 1912 RVA: 0x00021DFB File Offset: 0x0001FFFB
		public StaticFunctionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00021E04 File Offset: 0x00020004
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 2)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target, args[0], args[1]);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00021E20 File Offset: 0x00020020
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

		// Token: 0x0600077B RID: 1915 RVA: 0x00021E94 File Offset: 0x00020094
		public object InvokeUnsafe(object target, object arg0, object arg1)
		{
			return this.invoke((TParam0)((object)arg0), (TParam1)((object)arg1));
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00021EB2 File Offset: 0x000200B2
		protected override Type[] GetParameterTypes()
		{
			return new Type[]
			{
				typeof(TParam0),
				typeof(TParam1)
			};
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00021ED4 File Offset: 0x000200D4
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Func<TParam0, TParam1, TResult>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00021EE8 File Offset: 0x000200E8
		protected override void CreateDelegate()
		{
			this.invoke = (TParam0 param0, TParam1 param1) => ((Func<TParam0, TParam1, TResult>)this.methodInfo.CreateDelegate(typeof(Func<TParam0, TParam1, TResult>)))(param0, param1);
		}

		// Token: 0x040001BF RID: 447
		private Func<TParam0, TParam1, TResult> invoke;
	}
}
