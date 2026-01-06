using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000114 RID: 276
	public sealed class StaticActionInvoker<TParam0, TParam1> : StaticActionInvokerBase
	{
		// Token: 0x0600073F RID: 1855 RVA: 0x0002135B File Offset: 0x0001F55B
		public StaticActionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00021364 File Offset: 0x0001F564
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 2)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target, args[0], args[1]);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00021380 File Offset: 0x0001F580
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

		// Token: 0x06000742 RID: 1858 RVA: 0x000213F4 File Offset: 0x0001F5F4
		public object InvokeUnsafe(object target, object arg0, object arg1)
		{
			this.invoke((TParam0)((object)arg0), (TParam1)((object)arg1));
			return null;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0002140E File Offset: 0x0001F60E
		protected override Type[] GetParameterTypes()
		{
			return new Type[]
			{
				typeof(TParam0),
				typeof(TParam1)
			};
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00021430 File Offset: 0x0001F630
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Action<TParam0, TParam1>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00021444 File Offset: 0x0001F644
		protected override void CreateDelegate()
		{
			this.invoke = delegate(TParam0 param0, TParam1 param1)
			{
				((Action<TParam0, TParam1>)this.methodInfo.CreateDelegate(typeof(Action<TParam0, TParam1>)))(param0, param1);
			};
		}

		// Token: 0x040001B5 RID: 437
		private Action<TParam0, TParam1> invoke;
	}
}
