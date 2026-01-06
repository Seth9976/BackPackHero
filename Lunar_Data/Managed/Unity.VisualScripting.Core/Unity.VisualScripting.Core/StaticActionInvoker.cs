using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000112 RID: 274
	public sealed class StaticActionInvoker : StaticActionInvokerBase
	{
		// Token: 0x0600072F RID: 1839 RVA: 0x00021194 File Offset: 0x0001F394
		public StaticActionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0002119D File Offset: 0x0001F39D
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 0)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x000211B0 File Offset: 0x0001F3B0
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

		// Token: 0x06000732 RID: 1842 RVA: 0x00021204 File Offset: 0x0001F404
		private object InvokeUnsafe(object target)
		{
			this.invoke();
			return null;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00021212 File Offset: 0x0001F412
		protected override Type[] GetParameterTypes()
		{
			return Type.EmptyTypes;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00021219 File Offset: 0x0001F419
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Action>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0002122D File Offset: 0x0001F42D
		protected override void CreateDelegate()
		{
			this.invoke = delegate
			{
				((Action)this.methodInfo.CreateDelegate(typeof(Action)))();
			};
		}

		// Token: 0x040001B3 RID: 435
		private Action invoke;
	}
}
