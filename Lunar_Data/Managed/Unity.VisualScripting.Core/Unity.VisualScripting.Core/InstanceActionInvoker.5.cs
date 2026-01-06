using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x020000FE RID: 254
	public sealed class InstanceActionInvoker<TTarget, TParam0, TParam1, TParam2, TParam3> : InstanceActionInvokerBase<TTarget>
	{
		// Token: 0x06000696 RID: 1686 RVA: 0x0001F080 File Offset: 0x0001D280
		public InstanceActionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001F089 File Offset: 0x0001D289
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 4)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target, args[0], args[1], args[2], args[3]);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001F0AC File Offset: 0x0001D2AC
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

		// Token: 0x06000699 RID: 1689 RVA: 0x0001F148 File Offset: 0x0001D348
		public object InvokeUnsafe(object target, object arg0, object arg1, object arg2, object arg3)
		{
			this.invoke((TTarget)((object)target), (TParam0)((object)arg0), (TParam1)((object)arg1), (TParam2)((object)arg2), (TParam3)((object)arg3));
			return null;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001F176 File Offset: 0x0001D376
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

		// Token: 0x0600069B RID: 1691 RVA: 0x0001F1B2 File Offset: 0x0001D3B2
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Action<TTarget, TParam0, TParam1, TParam2, TParam3>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001F1C6 File Offset: 0x0001D3C6
		protected override void CreateDelegate()
		{
			this.invoke = (Action<TTarget, TParam0, TParam1, TParam2, TParam3>)this.methodInfo.CreateDelegate(typeof(Action<TTarget, TParam0, TParam1, TParam2, TParam3>));
		}

		// Token: 0x04000199 RID: 409
		private Action<TTarget, TParam0, TParam1, TParam2, TParam3> invoke;
	}
}
