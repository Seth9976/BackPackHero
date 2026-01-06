using System;
using System.Diagnostics;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x020002BA RID: 698
	internal class InvokableCall<T1, T2, T3> : BaseInvokableCall
	{
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06001D38 RID: 7480 RVA: 0x0002EDCC File Offset: 0x0002CFCC
		// (remove) Token: 0x06001D39 RID: 7481 RVA: 0x0002EE04 File Offset: 0x0002D004
		[field: DebuggerBrowsable(0)]
		protected event UnityAction<T1, T2, T3> Delegate;

		// Token: 0x06001D3A RID: 7482 RVA: 0x0002EE39 File Offset: 0x0002D039
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2, T3>)global::System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2, T3>), target, theFunction);
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x0002EE61 File Offset: 0x0002D061
		public InvokableCall(UnityAction<T1, T2, T3> action)
		{
			this.Delegate += action;
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x0002EE74 File Offset: 0x0002D074
		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 3;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			BaseInvokableCall.ThrowOnInvalidArg<T3>(args[2]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]), (T3)((object)args[2]));
			}
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x0002EEE8 File Offset: 0x0002D0E8
		public void Invoke(T1 args0, T2 args1, T3 args2)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0, args1, args2);
			}
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x0002EF14 File Offset: 0x0002D114
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
}
