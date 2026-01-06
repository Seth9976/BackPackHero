using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000116 RID: 278
	public sealed class StaticActionInvoker<TParam0, TParam1, TParam2, TParam3> : StaticActionInvokerBase
	{
		// Token: 0x0600074F RID: 1871 RVA: 0x000215C8 File Offset: 0x0001F7C8
		public StaticActionInvoker(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x000215D1 File Offset: 0x0001F7D1
		public override object Invoke(object target, params object[] args)
		{
			if (args.Length != 4)
			{
				throw new TargetParameterCountException();
			}
			return this.Invoke(target, args[0], args[1], args[2], args[3]);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x000215F4 File Offset: 0x0001F7F4
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

		// Token: 0x06000752 RID: 1874 RVA: 0x00021690 File Offset: 0x0001F890
		public object InvokeUnsafe(object target, object arg0, object arg1, object arg2, object arg3)
		{
			this.invoke((TParam0)((object)arg0), (TParam1)((object)arg1), (TParam2)((object)arg2), (TParam3)((object)arg3));
			return null;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x000216B8 File Offset: 0x0001F8B8
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

		// Token: 0x06000754 RID: 1876 RVA: 0x000216F4 File Offset: 0x0001F8F4
		protected override void CompileExpression(MethodCallExpression callExpression, ParameterExpression[] parameterExpressions)
		{
			this.invoke = Expression.Lambda<Action<TParam0, TParam1, TParam2, TParam3>>(callExpression, parameterExpressions).Compile();
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00021708 File Offset: 0x0001F908
		protected override void CreateDelegate()
		{
			this.invoke = delegate(TParam0 param0, TParam1 param1, TParam2 param2, TParam3 param3)
			{
				((Action<TParam0, TParam1, TParam2, TParam3>)this.methodInfo.CreateDelegate(typeof(Action<TParam0, TParam1, TParam2, TParam3>)))(param0, param1, param2, param3);
			};
		}

		// Token: 0x040001B7 RID: 439
		private Action<TParam0, TParam1, TParam2, TParam3> invoke;
	}
}
