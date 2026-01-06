using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x020000FF RID: 255
	public sealed class InstanceActionInvoker<TTarget, TParam0, TParam1, TParam2, TParam3, TParam4> : InstanceActionInvokerBase<TTarget>
	{
		// Token: 0x0600069D RID: 1693 RVA: 0x0001F1E8 File Offset: 0x0001D3E8
		public InstanceActionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001F1F1 File Offset: 0x0001D3F1
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 5)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target, args[0], args[1], args[2], args[3], args[4]);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001F218 File Offset: 0x0001D418
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

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001F2C4 File Offset: 0x0001D4C4
		public object InvokeUnsafe(object target, object arg0, object arg1, object arg2, object arg3, object arg4)
		{
			this.invoke((TTarget)((object)target), (TParam0)((object)arg0), (TParam1)((object)arg1), (TParam2)((object)arg2), (TParam3)((object)arg3), (TParam4)((object)arg4));
			return null;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001F2FC File Offset: 0x0001D4FC
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

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001F350 File Offset: 0x0001D550
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Action<TTarget, TParam0, TParam1, TParam2, TParam3, TParam4>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001F364 File Offset: 0x0001D564
		protected override void CreateDelegate()
		{
			this.invoke = (Action<TTarget, TParam0, TParam1, TParam2, TParam3, TParam4>)this.methodInfo.CreateDelegate(typeof(Action<TTarget, TParam0, TParam1, TParam2, TParam3, TParam4>));
		}

		// Token: 0x0400019A RID: 410
		private Action<TTarget, TParam0, TParam1, TParam2, TParam3, TParam4> invoke;
	}
}
