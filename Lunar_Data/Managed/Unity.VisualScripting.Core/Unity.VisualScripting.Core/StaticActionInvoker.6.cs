using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000117 RID: 279
	public sealed class StaticActionInvoker<TParam0, TParam1, TParam2, TParam3, TParam4> : StaticActionInvokerBase
	{
		// Token: 0x06000757 RID: 1879 RVA: 0x00021742 File Offset: 0x0001F942
		public StaticActionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0002174B File Offset: 0x0001F94B
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 5)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target, args[0], args[1], args[2], args[3], args[4]);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00021770 File Offset: 0x0001F970
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

		// Token: 0x0600075A RID: 1882 RVA: 0x0002181C File Offset: 0x0001FA1C
		public object InvokeUnsafe(object target, object arg0, object arg1, object arg2, object arg3, object arg4)
		{
			this.invoke((TParam0)((object)arg0), (TParam1)((object)arg1), (TParam2)((object)arg2), (TParam3)((object)arg3), (TParam4)((object)arg4));
			return null;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0002184C File Offset: 0x0001FA4C
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

		// Token: 0x0600075C RID: 1884 RVA: 0x000218A0 File Offset: 0x0001FAA0
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Action<TParam0, TParam1, TParam2, TParam3, TParam4>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x000218B4 File Offset: 0x0001FAB4
		protected override void CreateDelegate()
		{
			this.invoke = delegate(TParam0 param0, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
			{
				((Action<TParam0, TParam1, TParam2, TParam3, TParam4>)this.methodInfo.CreateDelegate(typeof(Action<TParam0, TParam1, TParam2, TParam3, TParam4>)))(param0, param1, param2, param3, param4);
			};
		}

		// Token: 0x040001B8 RID: 440
		private Action<TParam0, TParam1, TParam2, TParam3, TParam4> invoke;
	}
}
