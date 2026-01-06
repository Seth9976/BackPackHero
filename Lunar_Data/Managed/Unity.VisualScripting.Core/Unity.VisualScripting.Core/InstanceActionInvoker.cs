using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x020000FA RID: 250
	public sealed class InstanceActionInvoker<TTarget> : InstanceActionInvokerBase<TTarget>
	{
		// Token: 0x0600067A RID: 1658 RVA: 0x0001EC83 File Offset: 0x0001CE83
		public InstanceActionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001EC8C File Offset: 0x0001CE8C
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 0)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001ECA0 File Offset: 0x0001CEA0
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

		// Token: 0x0600067D RID: 1661 RVA: 0x0001ECF4 File Offset: 0x0001CEF4
		private object InvokeUnsafe(object target)
		{
			this.invoke((TTarget)((object)target));
			return null;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001ED08 File Offset: 0x0001CF08
		protected override Type[] GetParameterTypes()
		{
			return Type.EmptyTypes;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001ED0F File Offset: 0x0001CF0F
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Action<TTarget>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001ED23 File Offset: 0x0001CF23
		protected override void CreateDelegate()
		{
			this.invoke = (Action<TTarget>)this.methodInfo.CreateDelegate(typeof(Action<TTarget>));
		}

		// Token: 0x04000195 RID: 405
		private Action<TTarget> invoke;
	}
}
