using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000113 RID: 275
	public sealed class StaticActionInvoker<TParam0> : StaticActionInvokerBase
	{
		// Token: 0x06000737 RID: 1847 RVA: 0x00021262 File Offset: 0x0001F462
		public StaticActionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0002126B File Offset: 0x0001F46B
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 1)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target, args[0]);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00021284 File Offset: 0x0001F484
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

		// Token: 0x0600073A RID: 1850 RVA: 0x000212E8 File Offset: 0x0001F4E8
		private object InvokeUnsafe(object target, object arg0)
		{
			this.invoke((TParam0)((object)arg0));
			return null;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x000212FC File Offset: 0x0001F4FC
		protected override Type[] GetParameterTypes()
		{
			return new Type[] { typeof(TParam0) };
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00021311 File Offset: 0x0001F511
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Action<TParam0>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00021325 File Offset: 0x0001F525
		protected override void CreateDelegate()
		{
			this.invoke = delegate(TParam0 param0)
			{
				((Action<TParam0>)this.methodInfo.CreateDelegate(typeof(Action<TParam0>)))(param0);
			};
		}

		// Token: 0x040001B4 RID: 436
		private Action<TParam0> invoke;
	}
}
