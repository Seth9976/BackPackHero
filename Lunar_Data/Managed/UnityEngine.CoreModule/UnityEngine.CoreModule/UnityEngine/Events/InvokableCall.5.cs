using System;
using System.Diagnostics;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x020002BB RID: 699
	internal class InvokableCall<T1, T2, T3, T4> : BaseInvokableCall
	{
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06001D3F RID: 7487 RVA: 0x0002EF48 File Offset: 0x0002D148
		// (remove) Token: 0x06001D40 RID: 7488 RVA: 0x0002EF80 File Offset: 0x0002D180
		[field: DebuggerBrowsable(0)]
		protected event UnityAction<T1, T2, T3, T4> Delegate;

		// Token: 0x06001D41 RID: 7489 RVA: 0x0002EFB5 File Offset: 0x0002D1B5
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2, T3, T4>)global::System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2, T3, T4>), target, theFunction);
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x0002EFDD File Offset: 0x0002D1DD
		public InvokableCall(UnityAction<T1, T2, T3, T4> action)
		{
			this.Delegate += action;
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x0002EFF0 File Offset: 0x0002D1F0
		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 4;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			BaseInvokableCall.ThrowOnInvalidArg<T3>(args[2]);
			BaseInvokableCall.ThrowOnInvalidArg<T4>(args[3]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]), (T3)((object)args[2]), (T4)((object)args[3]));
			}
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x0002F078 File Offset: 0x0002D278
		public void Invoke(T1 args0, T2 args1, T3 args2, T4 args3)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0, args1, args2, args3);
			}
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x0002F0A8 File Offset: 0x0002D2A8
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
}
