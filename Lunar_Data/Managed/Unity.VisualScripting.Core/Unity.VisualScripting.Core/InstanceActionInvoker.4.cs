using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x020000FD RID: 253
	public sealed class InstanceActionInvoker<TTarget, TParam0, TParam1, TParam2> : InstanceActionInvokerBase<TTarget>
	{
		// Token: 0x0600068F RID: 1679 RVA: 0x0001EF44 File Offset: 0x0001D144
		public InstanceActionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001EF4D File Offset: 0x0001D14D
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 3)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target, args[0], args[1], args[2]);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001EF6C File Offset: 0x0001D16C
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

		// Token: 0x06000692 RID: 1682 RVA: 0x0001EFF4 File Offset: 0x0001D1F4
		public object InvokeUnsafe(object target, object arg0, object arg1, object arg2)
		{
			this.invoke((TTarget)((object)target), (TParam0)((object)arg0), (TParam1)((object)arg1), (TParam2)((object)arg2));
			return null;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001F01B File Offset: 0x0001D21B
		protected override Type[] GetParameterTypes()
		{
			return new Type[]
			{
				typeof(TParam0),
				typeof(TParam1),
				typeof(TParam2)
			};
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001F04A File Offset: 0x0001D24A
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Action<TTarget, TParam0, TParam1, TParam2>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001F05E File Offset: 0x0001D25E
		protected override void CreateDelegate()
		{
			this.invoke = (Action<TTarget, TParam0, TParam1, TParam2>)this.methodInfo.CreateDelegate(typeof(Action<TTarget, TParam0, TParam1, TParam2>));
		}

		// Token: 0x04000198 RID: 408
		private Action<TTarget, TParam0, TParam1, TParam2> invoke;
	}
}
