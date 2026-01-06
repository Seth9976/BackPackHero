using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000115 RID: 277
	public sealed class StaticActionInvoker<TParam0, TParam1, TParam2> : StaticActionInvokerBase
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x0002147B File Offset: 0x0001F67B
		public StaticActionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00021484 File Offset: 0x0001F684
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 3)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target, args[0], args[1], args[2]);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x000214A4 File Offset: 0x0001F6A4
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

		// Token: 0x0600074A RID: 1866 RVA: 0x0002152C File Offset: 0x0001F72C
		public object InvokeUnsafe(object target, object arg0, object arg1, object arg2)
		{
			this.invoke((TParam0)((object)arg0), (TParam1)((object)arg1), (TParam2)((object)arg2));
			return null;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0002154D File Offset: 0x0001F74D
		protected override Type[] GetParameterTypes()
		{
			return new Type[]
			{
				typeof(TParam0),
				typeof(TParam1),
				typeof(TParam2)
			};
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0002157C File Offset: 0x0001F77C
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Action<TParam0, TParam1, TParam2>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00021590 File Offset: 0x0001F790
		protected override void CreateDelegate()
		{
			this.invoke = delegate(TParam0 param0, TParam1 param1, TParam2 param2)
			{
				((Action<TParam0, TParam1, TParam2>)this.methodInfo.CreateDelegate(typeof(Action<TParam0, TParam1, TParam2>)))(param0, param1, param2);
			};
		}

		// Token: 0x040001B6 RID: 438
		private Action<TParam0, TParam1, TParam2> invoke;
	}
}
