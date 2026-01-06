using System;
using System.Diagnostics;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x020002B7 RID: 695
	internal class InvokableCall : BaseInvokableCall
	{
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06001D23 RID: 7459 RVA: 0x0002E9D0 File Offset: 0x0002CBD0
		// (remove) Token: 0x06001D24 RID: 7460 RVA: 0x0002EA08 File Offset: 0x0002CC08
		[field: DebuggerBrowsable(0)]
		private event UnityAction Delegate;

		// Token: 0x06001D25 RID: 7461 RVA: 0x0002EA3D File Offset: 0x0002CC3D
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate += (UnityAction)global::System.Delegate.CreateDelegate(typeof(UnityAction), target, theFunction);
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x0002EA66 File Offset: 0x0002CC66
		public InvokableCall(UnityAction action)
		{
			this.Delegate += action;
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x0002EA78 File Offset: 0x0002CC78
		public override void Invoke(object[] args)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate();
			}
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x0002EAA4 File Offset: 0x0002CCA4
		public void Invoke()
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate();
			}
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x0002EAD0 File Offset: 0x0002CCD0
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
}
