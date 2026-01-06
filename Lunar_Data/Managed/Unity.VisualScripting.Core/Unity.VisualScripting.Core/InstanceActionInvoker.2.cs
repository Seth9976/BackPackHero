using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x020000FB RID: 251
	public sealed class InstanceActionInvoker<TTarget, TParam0> : InstanceActionInvokerBase<TTarget>
	{
		// Token: 0x06000681 RID: 1665 RVA: 0x0001ED45 File Offset: 0x0001CF45
		public InstanceActionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001ED4E File Offset: 0x0001CF4E
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 1)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target, args[0]);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001ED68 File Offset: 0x0001CF68
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

		// Token: 0x06000684 RID: 1668 RVA: 0x0001EDCC File Offset: 0x0001CFCC
		private object InvokeUnsafe(object target, object arg0)
		{
			this.invoke((TTarget)((object)target), (TParam0)((object)arg0));
			return null;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001EDE6 File Offset: 0x0001CFE6
		protected override Type[] GetParameterTypes()
		{
			return new Type[] { typeof(TParam0) };
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001EDFB File Offset: 0x0001CFFB
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Action<TTarget, TParam0>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001EE0F File Offset: 0x0001D00F
		protected override void CreateDelegate()
		{
			this.invoke = (Action<TTarget, TParam0>)this.methodInfo.CreateDelegate(typeof(Action<TTarget, TParam0>));
		}

		// Token: 0x04000196 RID: 406
		private Action<TTarget, TParam0> invoke;
	}
}
