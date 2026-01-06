using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x0200011F RID: 287
	public sealed class StaticFunctionInvoker<TParam0, TParam1, TParam2, TParam3, TParam4, TResult> : StaticFunctionInvokerBase<TResult>
	{
		// Token: 0x06000790 RID: 1936 RVA: 0x000221EE File Offset: 0x000203EE
		public StaticFunctionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x000221F7 File Offset: 0x000203F7
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 5)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target, args[0], args[1], args[2], args[3], args[4]);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0002221C File Offset: 0x0002041C
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

		// Token: 0x06000793 RID: 1939 RVA: 0x000222C8 File Offset: 0x000204C8
		public object InvokeUnsafe(object target, object arg0, object arg1, object arg2, object arg3, object arg4)
		{
			return this.invoke((TParam0)((object)arg0), (TParam1)((object)arg1), (TParam2)((object)arg2), (TParam3)((object)arg3), (TParam4)((object)arg4));
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x000222FC File Offset: 0x000204FC
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

		// Token: 0x06000795 RID: 1941 RVA: 0x00022350 File Offset: 0x00020550
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Func<TParam0, TParam1, TParam2, TParam3, TParam4, TResult>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00022364 File Offset: 0x00020564
		protected override void CreateDelegate()
		{
			this.invoke = (TParam0 param0, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4) => ((Func<TParam0, TParam1, TParam2, TParam3, TParam4, TResult>)this.methodInfo.CreateDelegate(typeof(Func<TParam0, TParam1, TParam2, TParam3, TParam4, TResult>)))(param0, param1, param2, param3, param4);
		}

		// Token: 0x040001C2 RID: 450
		private Func<TParam0, TParam1, TParam2, TParam3, TParam4, TResult> invoke;
	}
}
