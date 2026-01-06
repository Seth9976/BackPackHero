using System;
using System.Diagnostics;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x020002B8 RID: 696
	internal class InvokableCall<T1> : BaseInvokableCall
	{
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06001D2A RID: 7466 RVA: 0x0002EB04 File Offset: 0x0002CD04
		// (remove) Token: 0x06001D2B RID: 7467 RVA: 0x0002EB3C File Offset: 0x0002CD3C
		[field: DebuggerBrowsable(0)]
		protected event UnityAction<T1> Delegate;

		// Token: 0x06001D2C RID: 7468 RVA: 0x0002EB71 File Offset: 0x0002CD71
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate += (UnityAction<T1>)global::System.Delegate.CreateDelegate(typeof(UnityAction<T1>), target, theFunction);
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x0002EB9A File Offset: 0x0002CD9A
		public InvokableCall(UnityAction<T1> action)
		{
			this.Delegate += action;
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x0002EBAC File Offset: 0x0002CDAC
		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 1;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]));
			}
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x0002EC00 File Offset: 0x0002CE00
		public virtual void Invoke(T1 args0)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0);
			}
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x0002EC2C File Offset: 0x0002CE2C
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
}
