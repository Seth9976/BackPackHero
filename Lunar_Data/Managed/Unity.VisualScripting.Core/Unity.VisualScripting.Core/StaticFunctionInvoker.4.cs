using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x0200011D RID: 285
	public sealed class StaticFunctionInvoker<TParam0, TParam1, TParam2, TResult> : StaticFunctionInvokerBase<TResult>
	{
		// Token: 0x06000780 RID: 1920 RVA: 0x00021F1F File Offset: 0x0002011F
		public StaticFunctionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00021F28 File Offset: 0x00020128
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 3)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target, args[0], args[1], args[2]);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00021F48 File Offset: 0x00020148
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

		// Token: 0x06000783 RID: 1923 RVA: 0x00021FD0 File Offset: 0x000201D0
		public object InvokeUnsafe(object target, object arg0, object arg1, object arg2)
		{
			return this.invoke((TParam0)((object)arg0), (TParam1)((object)arg1), (TParam2)((object)arg2));
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00021FF5 File Offset: 0x000201F5
		protected override Type[] GetParameterTypes()
		{
			return new Type[]
			{
				typeof(TParam0),
				typeof(TParam1),
				typeof(TParam2)
			};
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00022024 File Offset: 0x00020224
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Func<TParam0, TParam1, TParam2, TResult>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00022038 File Offset: 0x00020238
		protected override void CreateDelegate()
		{
			this.invoke = (TParam0 param0, TParam1 param1, TParam2 param2) => ((Func<TParam0, TParam1, TParam2, TResult>)this.methodInfo.CreateDelegate(typeof(Func<TParam0, TParam1, TParam2, TResult>)))(param0, param1, param2);
		}

		// Token: 0x040001C0 RID: 448
		private Func<TParam0, TParam1, TParam2, TResult> invoke;
	}
}
