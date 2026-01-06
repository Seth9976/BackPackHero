using System;
using System.Diagnostics;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x020002B9 RID: 697
	internal class InvokableCall<T1, T2> : BaseInvokableCall
	{
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06001D31 RID: 7473 RVA: 0x0002EC60 File Offset: 0x0002CE60
		// (remove) Token: 0x06001D32 RID: 7474 RVA: 0x0002EC98 File Offset: 0x0002CE98
		[field: DebuggerBrowsable(0)]
		protected event UnityAction<T1, T2> Delegate;

		// Token: 0x06001D33 RID: 7475 RVA: 0x0002ECCD File Offset: 0x0002CECD
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2>)global::System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2>), target, theFunction);
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x0002ECF5 File Offset: 0x0002CEF5
		public InvokableCall(UnityAction<T1, T2> action)
		{
			this.Delegate += action;
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x0002ED08 File Offset: 0x0002CF08
		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 2;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]));
			}
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x0002ED6C File Offset: 0x0002CF6C
		public void Invoke(T1 args0, T2 args1)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0, args1);
			}
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x0002ED98 File Offset: 0x0002CF98
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
}
