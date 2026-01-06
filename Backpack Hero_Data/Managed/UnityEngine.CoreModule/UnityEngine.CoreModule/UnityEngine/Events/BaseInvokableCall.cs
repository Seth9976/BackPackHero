using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x020002B6 RID: 694
	internal abstract class BaseInvokableCall
	{
		// Token: 0x06001D1D RID: 7453 RVA: 0x00008C2F File Offset: 0x00006E2F
		protected BaseInvokableCall()
		{
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x0002E8DC File Offset: 0x0002CADC
		protected BaseInvokableCall(object target, MethodInfo function)
		{
			bool flag = function == null;
			if (flag)
			{
				throw new ArgumentNullException("function");
			}
			bool isStatic = function.IsStatic;
			if (isStatic)
			{
				bool flag2 = target != null;
				if (flag2)
				{
					throw new ArgumentException("target must be null");
				}
			}
			else
			{
				bool flag3 = target == null;
				if (flag3)
				{
					throw new ArgumentNullException("target");
				}
			}
		}

		// Token: 0x06001D1F RID: 7455
		public abstract void Invoke(object[] args);

		// Token: 0x06001D20 RID: 7456 RVA: 0x0002E93C File Offset: 0x0002CB3C
		protected static void ThrowOnInvalidArg<T>(object arg)
		{
			bool flag = arg != null && !(arg is T);
			if (flag)
			{
				throw new ArgumentException(UnityString.Format("Passed argument 'args[0]' is of the wrong type. Type:{0} Expected:{1}", new object[]
				{
					arg.GetType(),
					typeof(T)
				}));
			}
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x0002E98C File Offset: 0x0002CB8C
		protected static bool AllowInvoke(Delegate @delegate)
		{
			object target = @delegate.Target;
			bool flag = target == null;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				Object @object = target as Object;
				bool flag3 = @object != null;
				flag2 = !flag3 || @object != null;
			}
			return flag2;
		}

		// Token: 0x06001D22 RID: 7458
		public abstract bool Find(object targetObj, MethodInfo method);
	}
}
