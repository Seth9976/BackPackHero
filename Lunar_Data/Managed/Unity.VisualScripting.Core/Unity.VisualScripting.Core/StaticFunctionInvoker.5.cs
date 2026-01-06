using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x0200011E RID: 286
	public sealed class StaticFunctionInvoker<TParam0, TParam1, TParam2, TParam3, TResult> : StaticFunctionInvokerBase<TResult>
	{
		// Token: 0x06000788 RID: 1928 RVA: 0x00022070 File Offset: 0x00020270
		public StaticFunctionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00022079 File Offset: 0x00020279
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 4)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target, args[0], args[1], args[2], args[3]);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0002209C File Offset: 0x0002029C
		public override object Invoke(object target, object arg0, object arg1, object arg2, object arg3)
		{
			if (OptimizedReflection.safeMode)
			{
				this.VerifyTarget(target);
				base.VerifyArgument<TParam0>(this.methodInfo, 0, arg0);
				base.VerifyArgument<TParam1>(this.methodInfo, 1, arg1);
				base.VerifyArgument<TParam2>(this.methodInfo, 2, arg2);
				base.VerifyArgument<TParam3>(this.methodInfo, 3, arg3);
				try
				{
					return this.InvokeUnsafe(target, arg0, arg1, arg2, arg3);
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
			return this.InvokeUnsafe(target, arg0, arg1, arg2, arg3);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00022138 File Offset: 0x00020338
		public object InvokeUnsafe(object target, object arg0, object arg1, object arg2, object arg3)
		{
			return this.invoke((TParam0)((object)arg0), (TParam1)((object)arg1), (TParam2)((object)arg2), (TParam3)((object)arg3));
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00022164 File Offset: 0x00020364
		protected override Type[] GetParameterTypes()
		{
			return new Type[]
			{
				typeof(TParam0),
				typeof(TParam1),
				typeof(TParam2),
				typeof(TParam3)
			};
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x000221A0 File Offset: 0x000203A0
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Func<TParam0, TParam1, TParam2, TParam3, TResult>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000221B4 File Offset: 0x000203B4
		protected override void CreateDelegate()
		{
			this.invoke = (TParam0 param0, TParam1 param1, TParam2 param2, TParam3 param3) => ((Func<TParam0, TParam1, TParam2, TParam3, TResult>)this.methodInfo.CreateDelegate(typeof(Func<TParam0, TParam1, TParam2, TParam3, TResult>)))(param0, param1, param2, param3);
		}

		// Token: 0x040001C1 RID: 449
		private Func<TParam0, TParam1, TParam2, TParam3, TResult> invoke;
	}
}
