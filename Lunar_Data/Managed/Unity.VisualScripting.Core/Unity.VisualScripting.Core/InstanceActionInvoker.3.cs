using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x020000FC RID: 252
	public sealed class InstanceActionInvoker<TTarget, TParam0, TParam1> : InstanceActionInvokerBase<TTarget>
	{
		// Token: 0x06000688 RID: 1672 RVA: 0x0001EE31 File Offset: 0x0001D031
		public InstanceActionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001EE3A File Offset: 0x0001D03A
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 2)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target, args[0], args[1]);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001EE58 File Offset: 0x0001D058
		public override object Invoke(object target, object arg0, object arg1)
		{
			if (OptimizedReflection.safeMode)
			{
				this.VerifyTarget(target);
				base.VerifyArgument<TParam0>(this.methodInfo, 0, arg0);
				base.VerifyArgument<TParam1>(this.methodInfo, 0, arg1);
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

		// Token: 0x0600068B RID: 1675 RVA: 0x0001EECC File Offset: 0x0001D0CC
		public object InvokeUnsafe(object target, object arg0, object arg1)
		{
			this.invoke((TTarget)((object)target), (TParam0)((object)arg0), (TParam1)((object)arg1));
			return null;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001EEEC File Offset: 0x0001D0EC
		protected override Type[] GetParameterTypes()
		{
			return new Type[]
			{
				typeof(TParam0),
				typeof(TParam1)
			};
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001EF0E File Offset: 0x0001D10E
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Action<TTarget, TParam0, TParam1>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001EF22 File Offset: 0x0001D122
		protected override void CreateDelegate()
		{
			this.invoke = (Action<TTarget, TParam0, TParam1>)this.methodInfo.CreateDelegate(typeof(Action<TTarget, TParam0, TParam1>));
		}

		// Token: 0x04000197 RID: 407
		private Action<TTarget, TParam0, TParam1> invoke;
	}
}
